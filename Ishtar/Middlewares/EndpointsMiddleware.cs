using System.Reflection;
using System.Text.Json;
using Ishtar.Abstractions;
using Ishtar.DependencyInjection.Extensions;

namespace Ishtar.Middlewares;

public class EndpointsMiddleware : Middleware
{
    private readonly Type[] _controllers;

    public EndpointsMiddleware(Assembly assembly)
    {
        _controllers = assembly.GetTypes().Where(type =>
                type is { IsClass: true, IsPublic: true } &&
                type.GetCustomAttribute<ApiControllerAttribute>() is not null
            ).ToArray();
    }

    public override async Task Invoke(IHttpContext context)
    {
        ApiControllerAttribute? controllerAttribute = null;

        Type? controllerType = _controllers.Where(type =>
        {
            controllerAttribute = type.GetCustomAttribute<ApiControllerAttribute>()!;
            return context.Request.Path.Contains(controllerAttribute.Route);
        }).SingleOrDefault(); // NO, you can't have /api/a and /api/a/b as two distinct paths for controllers.

        if (controllerType is null || controllerAttribute is null)
        {
            context.Response.StatusCode = HttpStatusCode.NotFound404;
            return;
        }

        MethodInfo? action = controllerType.GetMethods().Where(info =>
        {
            if (info is not { IsGenericMethod: false, IsPublic: true })
                return false;
            
            if (!info.ReturnType.IsAssignableTo(typeof(IActionResult)) &&
                !info.ReturnType.IsAssignableTo(typeof(Task<IActionResult>)) &&
                !info.ReturnType.IsAssignableTo(typeof(Task<IObjectResult>)))
                return false;
            
            var attribute = info.GetCustomAttribute<HttpRouteAttribute>();
            return attribute is not null && context.Request.Path == controllerAttribute.Route + attribute.Route
                                         && attribute.HttpMethod == context.Request.Method;
        }).SingleOrDefault(); // for methods you can;
        
        if (action is null)
        {
            context.Response.StatusCode = HttpStatusCode.NotFound404;
            return;
        }

        object controller = context.Services.InjectInto(controllerType);

        if (controller is ControllerBase controllerBase)
            controllerBase.Context = context;

        ParameterInfo[] parameters = action.GetParameters();

        object? result = parameters.Length switch
        {
            0 => action.Invoke(controller, []),
            1 when parameters[0].ParameterType.IsAssignableTo(typeof(IHttpRequest)) => 
                action.Invoke(controller, [context.Request]),
            1 => action.Invoke(controller, 
                    [JsonSerializer.Deserialize(context.Request.Body, parameters[0].ParameterType)]),
            _ => null
        };

        switch (result)
        {
            case Task<IObjectResult> asyncObjectAction:
            {
                IObjectResult objectResult = await asyncObjectAction;
                context.Response.StatusCode = objectResult.StatusCode;
                byte[] resultBytes = JsonSerializer.SerializeToUtf8Bytes(objectResult.Result);
                context.Response.Headers["content-type"] = "application/json";
                context.Response.Headers["Content-Length"] = $"{resultBytes.Length}";
                context.Response.Body = resultBytes;
                break;
            }
            case Task<IActionResult> asyncAction:
            {
                IActionResult actionResult = await asyncAction;
                context.Response.StatusCode = actionResult.StatusCode;
                break;
            }
            case IObjectResult objectActionResult:
            {
                context.Response.StatusCode = objectActionResult.StatusCode;
                byte[] resultBytes = JsonSerializer.SerializeToUtf8Bytes(objectActionResult.Result);
                context.Response.Headers["content-type"] = "application/json";
                context.Response.Headers["Content-Length"] = $"{resultBytes.Length}";
                context.Response.Body = resultBytes;
                break;
            }
            case IActionResult actionResult:
                context.Response.StatusCode = actionResult.StatusCode;
                break;
            default:
                throw new Exception("Controller method does not return an IActionResult, an IObjectResult, or a Task of any of these.");
        }
    }
}
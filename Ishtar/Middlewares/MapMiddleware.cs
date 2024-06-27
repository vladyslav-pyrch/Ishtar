using System.Text;
using System.Text.Json;
using Ishtar.Abstractions;
using HttpMethod = Ishtar.Abstractions.HttpMethod;

namespace Ishtar.Middlewares;
//Separate on 3 middlewares
public class MapMiddleware : Middleware
{
    private readonly string _path;
    private readonly Func<IHttpContext, object>? _returnAction;
    private readonly Action<IHttpContext>? _action;
    private readonly HttpMethod _method;

    public MapMiddleware(string path, Func<IHttpContext, object> returnAction, HttpMethod method)
    {
        _path = path;
        _returnAction = returnAction;
        _method = method;
    }

    public MapMiddleware(string path, Action<IHttpContext> action, HttpMethod method)
    {
        _path = path;
        _action = action;
        _method = method;
    }
    

    public override async Task Invoke(IHttpContext context)
    {
        if (context.Request.Path != _path || context.Request.Method != _method)
        {
            await Next.Invoke(context);
            return;
        }
        
        if (_returnAction is not null)
        {
            object result = _returnAction(context);
            byte[] resultBytes = JsonSerializer.SerializeToUtf8Bytes(result);
            context.Response.Headers["content-type"] = "application/json";
            context.Response.Headers["Content-Length"] = $"{resultBytes.Length}";
            context.Response.Body = resultBytes;

            return;
        }

        _action?.Invoke(context);
    }
}
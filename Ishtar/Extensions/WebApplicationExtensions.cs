using System.Reflection;
using System.Text.Json;
using Ishtar.Abstractions;
using Ishtar.Middlewares;
using HttpMethod = Ishtar.Abstractions.HttpMethod;

namespace Ishtar.Extensions;

public static class WebApplicationExtensions
{
    public static void UseSupportedProtocol(this IWebApplication app, IEnumerable<HttpVersion> allowedVersions)
    {
        app.Use<SupportedProtocolMiddleware>(allowedVersions);
    }

    public static void UseMap(this IWebApplication application, string path, Func<IHttpContext, object?> actionJson, HttpMethod? method = null)
    {
        Action<IHttpContext> action = context =>
        {
            byte[] resultBytes = JsonSerializer.SerializeToUtf8Bytes(actionJson(context));
            context.Response.Headers["content-type"] = "application/json";
            context.Response.Headers["Content-Length"] = $"{resultBytes.Length}";
            context.Response.Body = resultBytes;
        };
        
        application.Use<MapMiddleware>(path, action, method ?? HttpMethod.Get);
    }

    public static void UseMap(this IWebApplication application, string path, Action<IHttpContext> action, HttpMethod? method = null)
    {
        application.Use<MapMiddleware>(path, action, method ?? HttpMethod.Get);
    }

    public static void UseRun(this IWebApplication application, Action action)
    {
        application.Use<RunMiddleware>(action);
    }

    public static void UseEndpoints(this IWebApplication application, Assembly assembly)
    {
        application.Use<EndpointsMiddleware>(assembly);
    }
}
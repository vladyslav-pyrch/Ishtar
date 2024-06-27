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

    public static void UseMap(this IWebApplication application, string path, Func<IHttpContext, object> action, HttpMethod? method = null)
    {
        application.Use<MapMiddleware>(path, action, method ?? HttpMethod.Get);
    }

    public static void UseMap(this IWebApplication application, string path, Action<IHttpContext> action, HttpMethod? method = null)
    {
        application.Use<MapMiddleware>(path, action, method ?? HttpMethod.Get);
    }
}
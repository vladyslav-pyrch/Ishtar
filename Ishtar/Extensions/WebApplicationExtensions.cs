using Ishtar.Abstractions;
using Ishtar.Middlewares;

namespace Ishtar.Extensions;

public static class WebApplicationExtensions
{
    public static void UseSupportedProtocol(this IWebApplication app, IEnumerable<HttpVersion> allowedVersions)
    {
        app.Use<SupportedProtocolMiddleware>(allowedVersions);
    }

    public static void UseMap(this IWebApplication application, string path, Func<IHttpContext, string> func)
    {
        application.Use<MapMiddleware>(path, func);
    }
}
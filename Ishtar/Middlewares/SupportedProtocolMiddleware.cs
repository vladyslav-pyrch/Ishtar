using Ishtar.Abstractions;

namespace Ishtar.Middlewares;

public class SupportedProtocolMiddleware : Middleware
{
    private readonly IEnumerable<HttpVersion> _allowedHttpVersions;

    public SupportedProtocolMiddleware(IEnumerable<HttpVersion> allowedHttpVersions)
    {
        _allowedHttpVersions = allowedHttpVersions;
    }

    public override async Task Invoke(IHttpContext context)
    {
        
        if (_allowedHttpVersions.All(version => version != context.Request.Version))
        {
            context.Response.StatusCode = HttpStatusCode.HttpVersionNotSupported505;
            context.Response.Version = HttpVersion.Version11;
            return;
        }
        
        await Next.Invoke(context);
    }
}
using System.Text;
using Ishtar.Abstractions;

namespace Ishtar.Middlewares;

public class MapMiddleware : Middleware
{
    private readonly string _path;
    
    private readonly Func<IHttpContext, string> _func;

    public MapMiddleware(string path, Func<IHttpContext, string> func)
    {
        _path = path;
        _func = func;
    }

    public override Task Invoke(IHttpContext context)
    {
        if (context.Request.Path != _path)
            return Task.CompletedTask;
        
        byte[] result = Encoding.UTF8.GetBytes(_func(context));

        if (!context.Response.Headers.TryGetValue("Content-Type", out string? _))
            throw new InvalidOperationException("Map middleware should specify content-type header.");

        context.Response.Headers["content-length"] = $"{result.Length}";
        context.Response.Body = result;

        return Task.CompletedTask;
    }
}
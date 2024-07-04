using System.Text;
using System.Text.Json;
using Ishtar.Abstractions;
using HttpMethod = Ishtar.Abstractions.HttpMethod;

namespace Ishtar.Middlewares;

public class MapMiddleware : Middleware
{
    private readonly string _path;
    private readonly Action<IHttpContext> _action;
    private readonly HttpMethod _method;


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
        _action(context);
    }
}
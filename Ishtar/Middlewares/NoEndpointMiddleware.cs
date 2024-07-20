using Ishtar.Abstractions;

namespace Ishtar.Middlewares;


public class NoEndpointMiddleware : IMiddleware
{
    public IMiddleware Next { get; set; } = null!;

    public Task Invoke(IHttpContext context)
    {
        context.Response.StatusCode = context.Request.Method is { Value: "GET" or "HEAD" }
            ? HttpStatusCode.Ok200 : HttpStatusCode.NotFound404;
        return Task.CompletedTask;
    }
}
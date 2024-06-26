namespace Ishtar.Abstractions;

public abstract class Middleware : IMiddleware
{
    public IMiddleware? Next { get; set; }

    public abstract Task Invoke(IHttpContext context);
}
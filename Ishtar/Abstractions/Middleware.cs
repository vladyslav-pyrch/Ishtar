namespace Ishtar.Abstractions;

public abstract class Middleware : IMiddleware
{
    public IMiddleware Next { get; set; } = null!;

    public abstract Task Invoke(IHttpContext context);
}
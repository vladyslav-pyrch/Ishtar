namespace Ishtar.Abstractions;

public interface IMiddleware
{
    public IMiddleware? Next { get; set; }
    
    public Task Invoke(IHttpContext context);
}
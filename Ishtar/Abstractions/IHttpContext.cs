namespace Ishtar.Abstractions;

public interface IHttpContext
{
    public IHttpRequest Request { get; }
    
    public IHttpResponse Response { get; }
    
    public CancellationToken RequestAborted { get; }

    public Task Abort();
}
using Ishtar.Abstractions;

namespace Ishtar;

internal class HttpContext : IHttpContext, IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public HttpContext(IHttpRequest request, IHttpResponse response)
    {
        Request = request;
        Response = response;
    }
    
    public IHttpRequest Request { get; }
    
    public IHttpResponse Response { get; }

    public CancellationToken RequestAborted => _cancellationTokenSource.Token;
    
    public async Task Abort()
    {
        await _cancellationTokenSource.CancelAsync();
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }
}
using Ishtar.Abstractions;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;


namespace Ishtar;

internal class HttpContext : IHttpContext, IDisposable
{
    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public HttpContext(IHttpRequest request, IHttpResponse response, IServiceProvider serviceProvider)
    {
        Request = request;
        Response = response;
        Services = serviceProvider;
    }
    
    public IHttpRequest Request { get; }
    
    public IHttpResponse Response { get; }

    public CancellationToken RequestAborted => _cancellationTokenSource.Token;
    
    public IServiceProvider Services { get; }

    public async Task Abort()
    {
        await _cancellationTokenSource.CancelAsync();
    }

    public void Dispose()
    {
        _cancellationTokenSource.Dispose();
    }
}
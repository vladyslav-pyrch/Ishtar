using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Text;
using System.Web;
using Ishtar.Abstractions;
using Ishtar.DependencyInjection;
using Ishtar.DependencyInjection.Abstractions;
using Ishtar.DependencyInjection.Extensions;
using Ishtar.Middlewares;
using HttpMethod = Ishtar.Abstractions.HttpMethod;
using IServiceProvider = Ishtar.DependencyInjection.Abstractions.IServiceProvider;

namespace Ishtar;

public class WebApplication : IWebApplication, IAsyncDisposable
{
    private readonly TcpListener _listener;

    private readonly IServiceProvider _localServiceProvider;

    private readonly IServiceCollection _collection;

    private IMiddleware? _firstMiddleware;

    private IMiddleware? _lastMiddleware;
    
    public WebApplication(TcpListener listener, IServiceCollection collection)
    {
        _listener = listener;
        _collection = collection;
        collection.AddInstance<IWebApplication>(this);
        _localServiceProvider = new ServiceProvider(collection);
    }

    public IServiceProvider ServiceProvider => new ServiceProvider(_collection);

    public async Task Start(CancellationToken cancellationToken = default)
    {
        Use<NoEndpointMiddleware>();
        _listener.Start();

        while (!cancellationToken.IsCancellationRequested)
        {
            Socket socket = await _listener.AcceptSocketAsync(cancellationToken);
            _ = Task.Run(async () =>
            {
                using (socket)
                {
                    const int bufferSize = 25600;
                    var buffer = new byte[bufferSize];
                    int readBytes = await socket.ReceiveAsync(buffer);
                    if (readBytes > bufferSize)
                    {
                        var headers = new HeaderDictionary();
                        byte[] body = "Buffer overflow!"u8.ToArray();
                        headers.Add("Content-Type", "plain/text");
                        headers.Add("Content-Length", body.Length.ToString());
                        _ = await socket.SendAsync(CreateResponse(new HttpResponse(
                            HttpStatusCode.BadRequest400,
                            HttpVersion.Version11,
                            headers,
                            body
                        )));
                    }
                    
                    IHttpRequest httpRequest = CreateRequest(GetRequestString(buffer), GetRequestBody(buffer, readBytes));
                    IHttpResponse httpResponse =
                        new HttpResponse(HttpStatusCode.Ok200, httpRequest.Version, new HeaderDictionary(), []);
                    using var httpContext = new HttpContext(httpRequest, httpResponse, ServiceProvider);

                    await _firstMiddleware?.Invoke(httpContext)!;
                    if (httpContext.RequestAborted.IsCancellationRequested)
                    {
                        cancellationToken = httpContext.RequestAborted;
                        return;
                    }

                    _ = await socket.SendAsync(CreateResponse(httpContext.Response), cancellationToken);
                }
            }, cancellationToken);
        }
    }

    private byte[] GetRequestBody(byte[] buffer, int bodyEndIndex)
    {
        const string end = "\r\n\r\n";
        var matchIndex = 0;
        var bodyStartIndex = 0;
        
        foreach (byte t in buffer)
        {
            if (matchIndex == 4)
                break;
            if ((char)t == end[matchIndex])
                matchIndex++;
            else
                matchIndex = 0;
            bodyStartIndex++;
        }

        return buffer[bodyStartIndex..bodyEndIndex];
    }

    public Task Stop(CancellationToken cancellationToken = default)
    {
        return Task.Run(() =>
        {
            _listener.Stop();
            _listener.Dispose();
        }, cancellationToken);
    }

    public IWebApplication Use<TMiddleware>(params object?[]? args) where TMiddleware : class, IMiddleware
    {
        var middleware = _localServiceProvider.InjectInto<TMiddleware>(args);
        Use(middleware);
        return this;
    }

    private void Use(IMiddleware middleware)
    {
        if (HasMiddlewares())
        {
            _lastMiddleware.Next = middleware;
            _lastMiddleware = middleware;
        }
        else
        {
            _firstMiddleware = middleware;
            _lastMiddleware = middleware;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await Stop();
        GC.SuppressFinalize(this);
    }

    public static WebApplicationBuilder CreateBuilder(string[]? args = default) => new(args);

    private string GetRequestString(byte[] buffer)
    {
        var requestStringBuilder = new StringBuilder();
        
        const string end = "\r\n\r\n";
        var matchIndex = 0;
        
        foreach(byte t in buffer)
        {
            var ch = (char)t;
            requestStringBuilder.Append(ch);
            if (ch == end[matchIndex])
                matchIndex++;
            else
                matchIndex = 0;
            if (matchIndex == 4)
                break;
        }

        return requestStringBuilder.ToString();
    }

    private IHttpRequest CreateRequest(string requestString, byte[] body)
    {
        if (string.IsNullOrEmpty(requestString))
            return new HttpRequest(
                HttpMethod.Get,
                "/",
                HttpVersion.Version11,
                new HeaderDictionary(),
                new QueryDictionary(),
                body
            );
        
        string[] lines = requestString.Split("\r\n");
        string[] startLine = lines[0].Split(' ');

        var method = new HttpMethod(startLine[0]);
        string path = startLine[1].Split('?')[0];
        var version = new HttpVersion(startLine[2]);
        var queryDictionary = new QueryDictionary();
        
        NameValueCollection query = HttpUtility.ParseQueryString(new Uri("http://does.not.exist" + startLine[1]).Query);
        for (var i = 0; i < query.Count; i++)
        {
            string? key = query.GetKey(i);
            string? value = query.Get(i);
            if (key is null || value is null)
                continue;
            queryDictionary.Add(key, value);
        }

        var headers = new HeaderDictionary(lines.Skip(1).SkipLast(2)
            .Select(line => line.Split(": "))
            .ToDictionary(headerValue => headerValue[0].ToLower(), headerValue => headerValue[1])
        );

        return new HttpRequest(method, path, version, headers, queryDictionary, body);
    }

    private byte[] CreateResponse(IHttpResponse response)
    {
        StringBuilder responseStringBuilder = new StringBuilder()
            .Append($"{response.Version.Version} {response.StatusCode.StatusCode} {response.StatusCode.StatusMessage}\r\n");

        foreach (KeyValuePair<string,string> header in response.Headers)
            responseStringBuilder.Append($"{header.Key}: {header.Value}\r\n");

        responseStringBuilder.Append("\r\n");

        return [..Encoding.UTF8.GetBytes(responseStringBuilder.ToString()), ..response.Body];
    }

    [MemberNotNullWhen(true, "_firstMiddleware", "_lastMiddleware")]
    private bool HasMiddlewares() => _firstMiddleware is not null;
}
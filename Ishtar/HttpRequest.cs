using Ishtar.Abstractions;
using HttpMethod = Ishtar.Abstractions.HttpMethod;

namespace Ishtar;

internal class HttpRequest : IHttpRequest
{
    public HttpRequest(HttpMethod method, string path, HttpVersion version, IHeaderDictionary headers,
        IQueryDictionary query, byte[] body)
    {
        Method = method;
        Path = path;
        Version = version;
        Headers = headers;
        Query = query;
        Body = body;
    }

    public HttpMethod Method { get; set; }

    public string Path { get; set; }

    public HttpVersion Version { get; set; }

    public IHeaderDictionary Headers { get; set; }

    public IQueryDictionary Query { get; set; }

    public byte[] Body { get; set; }
}
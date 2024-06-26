using Ishtar.Abstractions;

namespace Ishtar;

internal class HttpResponse : IHttpResponse
{
    public HttpResponse(HttpStatusCode statusCode, HttpVersion version, IHeaderDictionary headers, byte[] body)
    {
        StatusCode = statusCode;
        Version = version;
        Headers = headers;
        Body = body;
    }

    public HttpStatusCode StatusCode { get; set; }

    public HttpVersion Version { get; set; }

    public IHeaderDictionary Headers { get; set; }

    public byte[] Body { get; set; }
}
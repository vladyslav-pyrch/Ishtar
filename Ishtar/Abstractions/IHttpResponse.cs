namespace Ishtar.Abstractions;

public interface IHttpResponse
{
    public HttpStatusCode StatusCode { get; set; }
    
    public HttpVersion Version { get; set; }
    
    public IHeaderDictionary Headers { get; set; }
    
    public byte[] Body { get; set; }
}
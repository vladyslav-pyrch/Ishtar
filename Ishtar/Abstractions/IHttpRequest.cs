namespace Ishtar.Abstractions;

public interface IHttpRequest
{
    public HttpMethod Method { get; set; }

    public string Path { get; set;  }

    public HttpVersion Version { get; set; }
    
    public IHeaderDictionary Headers { get; set; }
    
    public IQueryDictionary Query { get; set; }
    
    public byte[] Body { get; set; }
}
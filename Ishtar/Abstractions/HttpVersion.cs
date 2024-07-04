namespace Ishtar.Abstractions;

public record struct HttpVersion
{
    public HttpVersion(string version)
    {
        Version = version;
    }
    
    public string Version { get; }

    public static readonly HttpVersion Version11 = new("HTTP/1.1");
}
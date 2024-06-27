namespace Ishtar.Abstractions;

public record HttpVersion
{
    public HttpVersion(string version)
    {
        Version = version;
    }
    
    public string Version { get; }

    public static HttpVersion Version11 => new("HTTP/1.1");
}
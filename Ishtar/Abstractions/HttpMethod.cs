namespace Ishtar.Abstractions;

public record struct HttpMethod
{
    public HttpMethod(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static readonly HttpMethod Get = new("GET");
    
    public static readonly HttpMethod Head = new("HEAD");

    public static readonly HttpMethod Push = new("PUSH");

    public static readonly HttpMethod Put = new("PUT");

    public static readonly HttpMethod Patch = new("PATCH");

    public static readonly HttpMethod Delete = new("DELETE");
}
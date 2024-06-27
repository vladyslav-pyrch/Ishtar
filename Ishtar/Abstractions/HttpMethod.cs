namespace Ishtar.Abstractions;

public record HttpMethod
{
    public HttpMethod(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static HttpMethod Get => new("GET");

    public static HttpMethod Head => new("HEAD");

    public static HttpMethod Push => new("PUSH");

    public static HttpMethod Put => new("PUT");

    public static HttpMethod Patch => new("PATCH");

    public static HttpMethod Delete => new("DELETE");
}
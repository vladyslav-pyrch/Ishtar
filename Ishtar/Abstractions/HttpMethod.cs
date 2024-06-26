namespace Ishtar.Abstractions;

public class HttpMethod : IEquatable<HttpMethod>
{
    public HttpMethod(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static HttpMethod Get => new("GET");

    public static HttpMethod Push => new("PUSH");

    public static HttpMethod Put => new("PUT");

    public static HttpMethod Patch => new("PATCH");

    public static HttpMethod Delete => new("DELETE");

    public static bool operator ==(HttpMethod method1, HttpMethod method2)
    {
        return method1.Equals(method2);
    }

    public static bool operator !=(HttpMethod method1, HttpMethod method2)
    {
        return !(method1 == method2);
    }

    public bool Equals(HttpMethod? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((HttpMethod)obj);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}
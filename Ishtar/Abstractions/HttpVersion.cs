namespace Ishtar.Abstractions;

public class HttpVersion : IEquatable<HttpVersion>
{
    public HttpVersion(string version)
    {
        Version = version;
    }
    
    public string Version { get; }

    public static HttpVersion Version11 => new("HTTP/1.1");

    public static bool operator ==(HttpVersion version1, HttpVersion version2)
    {
        return version1.Equals(version2);
    }

    public static bool operator !=(HttpVersion version1, HttpVersion version2)
    {
        return !(version1 == version2);
    }

    public bool Equals(HttpVersion? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Version == other.Version;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((HttpVersion)obj);
    }

    public override int GetHashCode()
    {
        return Version.GetHashCode();
    }
}
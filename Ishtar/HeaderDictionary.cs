using System.Collections;
using Ishtar.Abstractions;

namespace Ishtar;

internal class HeaderDictionary : IHeaderDictionary
{
    private readonly IDictionary<string, string> _headers;

    public HeaderDictionary(IDictionary<string, string>? headers = null)
    {
        IDictionary<string, string> newHeaders = new Dictionary<string, string>();
        
        if (headers != null)
            foreach ((string? key, string? value) in headers)
                newHeaders[key.ToLower()] = value;

        _headers = newHeaders;
    }

    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _headers.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_headers).GetEnumerator();
    }

    public void Add(KeyValuePair<string, string> item)
    {
        _headers.Add(new KeyValuePair<string, string>(item.Key.ToLower(), item.Value));
    }

    public void Clear()
    {
        _headers.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _headers.Contains(new KeyValuePair<string, string>(item.Key.ToLower(), item.Value));
    }

    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        _headers.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return _headers.Remove(new KeyValuePair<string, string>(item.Key.ToLower(), item.Value));
    }

    public int Count => _headers.Count;

    public bool IsReadOnly => _headers.IsReadOnly;

    public void Add(string key, string value)
    {
        _headers.Add(key.ToLower(), value);
    }

    public bool ContainsKey(string key)
    {
        return _headers.ContainsKey(key.ToLower());
    }

    public bool Remove(string key)
    {
        return _headers.Remove(key.ToLower());
    }

    public bool TryGetValue(string key, out string value)
    {
        return _headers.TryGetValue(key.ToLower(), out value);
    }

    public string this[string key]
    {
        get => _headers[key.ToLower()];
        set => _headers[key.ToLower()] = value;
    }

    public ICollection<string> Keys => _headers.Keys;

    public ICollection<string> Values => _headers.Values;
}
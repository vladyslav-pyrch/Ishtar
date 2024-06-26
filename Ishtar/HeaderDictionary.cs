using System.Collections;
using Ishtar.Abstractions;

namespace Ishtar;

internal class HeaderDictionary : IHeaderDictionary
{
    private readonly IDictionary<string, string> _headers;

    public HeaderDictionary(IDictionary<string, string>? headers = null)
    {
        _headers = headers ?? new Dictionary<string, string>();
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
        _headers.Add(item);
    }

    public void Clear()
    {
        _headers.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _headers.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        _headers.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return _headers.Remove(item);
    }

    public int Count => _headers.Count;

    public bool IsReadOnly => _headers.IsReadOnly;

    public void Add(string key, string value)
    {
        _headers.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _headers.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _headers.Remove(key);
    }

    public bool TryGetValue(string key, out string value)
    {
        return _headers.TryGetValue(key, out value);
    }

    public string this[string key]
    {
        get => _headers[key];
        set => _headers[key] = value;
    }

    public ICollection<string> Keys => _headers.Keys;

    public ICollection<string> Values => _headers.Values;
}
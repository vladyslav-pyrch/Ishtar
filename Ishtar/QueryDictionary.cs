using System.Collections;
using Ishtar.Abstractions;

namespace Ishtar;

internal class QueryDictionary : IQueryDictionary
{
    private readonly IDictionary<string, string> _queryDictionary = new Dictionary<string, string>();
    
    public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
    {
        return _queryDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_queryDictionary).GetEnumerator();
    }

    public void Add(KeyValuePair<string, string> item)
    {
        _queryDictionary.Add(item);
    }

    public void Clear()
    {
        _queryDictionary.Clear();
    }

    public bool Contains(KeyValuePair<string, string> item)
    {
        return _queryDictionary.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex)
    {
        _queryDictionary.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<string, string> item)
    {
        return _queryDictionary.Remove(item);
    }

    public int Count => _queryDictionary.Count;

    public bool IsReadOnly => _queryDictionary.IsReadOnly;

    public void Add(string key, string value)
    {
        _queryDictionary.Add(key, value);
    }

    public bool ContainsKey(string key)
    {
        return _queryDictionary.ContainsKey(key);
    }

    public bool Remove(string key)
    {
        return _queryDictionary.Remove(key);
    }

    public bool TryGetValue(string key, out string value)
    {
        return _queryDictionary.TryGetValue(key, out value);
    }

    public string this[string key]
    {
        get => _queryDictionary[key];
        set => _queryDictionary[key] = value;
    }

    public ICollection<string> Keys => _queryDictionary.Keys;

    public ICollection<string> Values => _queryDictionary.Values;
}
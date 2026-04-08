using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Formula.Objects;

public class DictionaryMirror<TKey, TValue> : IDictionary<TKey, TValue> 
    where TKey : notnull 
    where TValue : IObject
{
    private readonly Dictionary<TKey, TValue> _internalDict = new();

    private TValue GetEffective(TValue value) => (TValue)(value.Shadow ?? value);

    public TValue this[TKey key]
    {
        get => GetEffective(_internalDict[key]);
        set => _internalDict[key] = value;
    }

    public ICollection<TValue> Values => _internalDict.Values.Select(GetEffective).ToList();
    public ICollection<TKey> Keys => _internalDict.Keys;

    public int Count => _internalDict.Count;
    public bool IsReadOnly => false;

    public void Add(TKey key, TValue value) => _internalDict.Add(key, value);

    public void Add(KeyValuePair<TKey, TValue> item) => _internalDict.Add(item.Key, item.Value);

    public void Clear() => _internalDict.Clear();

    public bool ContainsKey(TKey key) => _internalDict.ContainsKey(key);

    public bool Remove(TKey key) => _internalDict.Remove(key);

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        if (_internalDict.TryGetValue(key, out var rawValue))
        {
            value = GetEffective(rawValue);
            return true;
        }
        value = default;
        return false;
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        if (_internalDict.TryGetValue(item.Key, out var rawValue))
        {
            return EqualityComparer<TValue>.Default.Equals(GetEffective(rawValue), item.Value);
        }
        return false;
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        var projected = _internalDict.Select(kv => new KeyValuePair<TKey, TValue>(kv.Key, GetEffective(kv.Value))).ToArray();
        projected.CopyTo(array, arrayIndex);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        if (Contains(item))
        {
            return _internalDict.Remove(item.Key);
        }
        return false;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _internalDict
            .Select(kv => new KeyValuePair<TKey, TValue>(kv.Key, GetEffective(kv.Value)))
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
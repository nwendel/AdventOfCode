using System.Collections;
using System.Numerics;

namespace AdventOfCode.Common;

public class Counter<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    where TKey : notnull
    where TValue : INumber<TValue>
{
    private readonly Dictionary<TKey, TValue> _dictionary = [];

    public Counter()
    {
    }

    public Counter(IEnumerable<TKey> keys)
    {
        foreach (var key in keys)
        {
            this[key] += TValue.One;
        }
    }

    public TValue this[TKey key]
    {
        get => _dictionary.TryGetValue(key, out var value) ? value : TValue.Zero;
        set => _dictionary[key] = value;
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        => _dictionary.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _dictionary.GetEnumerator();
}

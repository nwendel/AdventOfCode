namespace AdventOfCode.Common;

public class Memoize<TKey, TValue>
    where TKey : notnull
{
    private readonly Dictionary<TKey, TValue> _cache = [];
    private readonly Func<TKey, Memoize<TKey, TValue>, TValue> _function;

    public Memoize(Func<TKey, Memoize<TKey, TValue>, TValue> function)
    {
        _function = function;
    }

    public TValue Get(TKey key)
    {
        if (_cache.TryGetValue(key, out var cached))
        {
            return cached;
        }

        _cache[key] = _function(key, this);

        return _cache[key];
    }
}

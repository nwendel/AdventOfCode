using System.Numerics;

namespace AdventOfCode.Common;

public static class DictionaryExtensions
{
    public static void AddOrIncrease<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue value)
        where TKey : notnull
        where TValue : IAdditionOperators<TValue, TValue, TValue>
    {
        if (!self.TryAdd(key, value))
        {
            self[key] += value;
        }
    }
}

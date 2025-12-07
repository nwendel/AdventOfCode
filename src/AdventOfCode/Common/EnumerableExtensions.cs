namespace AdventOfCode.Common;

public static class EnumerableExtensions
{
    extension<T>(IEnumerable<T> self)
    {
        public IEnumerable<T> Repeat()
        {
            while (true)
            {
                foreach (var item in self)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<T[]> SlidingChunk(int size)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

            var window = new Queue<T>(size);
            foreach (var item in self)
            {
                window.Enqueue(item);
                if (window.Count == size)
                {
                    yield return window.ToArray();
                    window.Dequeue();
                }
            }
        }

        public IEnumerable<(T[] chunk, int index)> SlidingChunk(int size, bool withIndex)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(size);

            if (!withIndex)
            {
                throw new ArgumentException("Use SlidingChunk(int size) overload instead", nameof(withIndex));
            }

            var window = new Queue<T>(size);
            var index = 0;
            foreach (var item in self)
            {
                window.Enqueue(item);
                if (window.Count == size)
                {
                    yield return (window.ToArray(), index);
                    window.Dequeue();
                    index++;
                }
            }
        }
    }
}

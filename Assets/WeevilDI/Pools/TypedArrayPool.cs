using System.Collections.Generic;

public sealed class TypedArrayPool<T>
{
    public static readonly TypedArrayPool<T> Shared = new TypedArrayPool<T>();

    private readonly Dictionary<int, Queue<T[]>> _buckets = new Dictionary<int, Queue<T[]>>();

    public T[] Rent(int size)
    {
        if (!_buckets.TryGetValue(size, out var bucket))
        {
            bucket = new Queue<T[]>();
            _buckets.Add(size, bucket);
        }

        return bucket.Count > 0 ? bucket.Dequeue() : new T[size];
    }

    public void Return(T[] array)
    {
        if (_buckets.TryGetValue(array.Length, out var bucket))
        {
            bucket.Enqueue(array);
        }
    }
}

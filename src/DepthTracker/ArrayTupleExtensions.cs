namespace DepthTracker;

public static class ArrayTupleExtensions
{
    public static void Deconstruct<T>(this T[] a, out T a0, out T a1)
    {
        if (a is not { Length: 2 })
        {
            throw new ArgumentException($"Source array must have exactly two elements, but actually has {a.Length}", nameof(a));
        }

        a0 = a[0];
        a1 = a[1];
    }
}
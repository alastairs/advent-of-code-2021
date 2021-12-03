// ReSharper disable once CheckNamespace
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

    public static T[][] Transpose<T>(this IReadOnlyList<T[]> input)
    {
        var rowCount = input.Count;
        var columnCount = input.Max(i => i.Length);
        var transposed = new T[columnCount][];

        if (rowCount == columnCount)
        {
            for (var i = 1; i < rowCount; i++)
            {
                // ReSharper disable once ConstantNullCoalescingCondition - this actually *is* null somehow
                transposed[i] ??= new T[rowCount];

                for (var j = 0; j < rowCount; j++)
                {
                    // ReSharper disable once ConstantNullCoalescingCondition - this actually *is* null somehow
                    transposed[j] ??= new T[rowCount];

                    (transposed[i][j], transposed[j][i]) =
                        (input[j][i], input[i][j]);
                }
            }
        }
        else
        {
            for (var column = 0; column < columnCount; column++)
            {
                transposed[column] = new T[rowCount];
                for (var row = 0; row < rowCount; row++)
                {
                    transposed[column][row] = input[row][column];
                }
            }
        }

        return transposed;
    }
}
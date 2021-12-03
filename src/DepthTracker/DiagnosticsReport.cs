namespace DepthTracker;

public class DiagnosticsReport
{
    public int Gamma { get; }

    public int Epsilon { get; }

    private DiagnosticsReport(int γ, int ε)
    {
        Gamma = γ;
        Epsilon = ε;
    }

    public static DiagnosticsReport Create(IEnumerable<string> text)
    {
        var transposed = Transpose(text.Select(l => l.ToCharArray()).ToArray());

        var gamma = new char[transposed.Length];
        var epsilon = new char[transposed.Length];
        for (var i = 0; i < transposed.Length; i++)
        {
            var digits = transposed[i];
            var zeroes = digits.Count(d => d == '0');
            var ones = digits.Count(d => d == '1');
            gamma[i] = (zeroes > ones) ? '0' : '1';
        }

        for (var i = 0; i < transposed.Length; i++)
        {
            var digits = transposed[i];
            var zeroes = digits.Count(d => d == '0');
            var ones = digits.Count(d => d == '1');
            epsilon[i] = (zeroes < ones) ? '0' : '1';
        }

        return new DiagnosticsReport(
            Convert.ToInt32(new string(gamma), fromBase: 2),
            Convert.ToInt32(new string(epsilon), fromBase: 2));
    }

    public static T[][] Transpose<T>(IReadOnlyList<T[]> input)
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
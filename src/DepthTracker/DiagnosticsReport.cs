using System;

namespace DepthTracker;

public record DiagnosticsReport
{
    public int Gamma { get; }

    public int Epsilon { get; }

    private int[] Zeroes { get; }

    private int[] Ones { get; }

    public int O2 { get; private set; }

    public int CO2 { get; private set; }

    private DiagnosticsReport(int γ, int ε, int[] zeroes, int[] ones, int o2Rating = 0, int co2Rating = 0)
    {
        Gamma = γ;
        Epsilon = ε;
        Zeroes = zeroes;
        Ones = ones;
        O2 = o2Rating;
        CO2 = co2Rating;
    }

    public static DiagnosticsReport Create(IEnumerable<string> text)
    {
        var transposed = TransposeInput(text);

        var diagnostics = CalculateGammaEpsilon(transposed);

        var o2 = text;
        var co2 = text;
        var o2Diagnostics = diagnostics;
        var co2Diagnostics = diagnostics;

        for (var i = 0; i < transposed.Length; i++)
        {
            o2 = o2.Where(l => (o2Diagnostics.Zeroes[i] > o2Diagnostics.Ones[i])
                ? l.ElementAt(i) == '0'
                : l.ElementAt(i) == '1').ToList();

            o2Diagnostics = CalculateGammaEpsilon(TransposeInput(o2));

            if (co2.Count() > 1)
            {
                co2 = co2.Where(l => (co2Diagnostics.Zeroes[i] <= co2Diagnostics.Ones[i])
                    ? l.ElementAt(i) == '0'
                    : l.ElementAt(i) == '1').ToList();

                co2Diagnostics = CalculateGammaEpsilon(TransposeInput(co2));
            }
        }

        return diagnostics with
        {
            O2 = o2.Single().Convert(),
            CO2 = co2.Single().Convert()
        };
    }

    private static char[][] TransposeInput(IEnumerable<string> text)
    {
        return text.Select(l => l.ToCharArray()).ToArray().Transpose();
    }

    private static DiagnosticsReport CalculateGammaEpsilon(char[][] transposed)
    {
        var γ = new char[transposed.Length];
        var ε = new char[transposed.Length];
        var zeroes = new int[transposed.Length];
        var ones = new int[transposed.Length];

        for (var i = 0; i < transposed.Length; i++)
        {
            var digits = transposed[i];
            zeroes[i] = digits.Count(d => d == '0');
            ones[i] = digits.Count(d => d == '1');
            γ[i] = (zeroes[i] > ones[i]) ? '0' : '1';
            ε[i] = (zeroes[i] < ones[i]) ? '0' : '1';
        }

        return new DiagnosticsReport(γ.Convert(), ε.Convert(), zeroes, ones);
    }
}

internal static class BinaryExtensions
{
    internal static int Convert(this char[] input)
    {
        return Convert(new string(input));
    }

    internal static int Convert(this string input)
    {
        return System.Convert.ToInt32(input, fromBase: 2);
    }
}
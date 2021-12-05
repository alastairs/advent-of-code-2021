using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DepthTracker.Tests;

public class VentNavigatorShould
{
    [Theory, MemberData(nameof(VectorParsingSamples))]
    public void Parse_an_input_line(string line, Vector expected)
    {
        Assert.Equal(expected, Vector.FromString(line));
    }

    [Theory, MemberData(nameof(HorizontalVectors))]
    public void IsHorizontal_is_true_when_the_X_components_are_equal(string line, bool expected)
    {
        Assert.Equal(expected, Vector.FromString(line).IsHorizontal);
    }

    public static IEnumerable<IEnumerable<object>> VectorParsingSamples => new[]
    {
        new object[] 
        { 
            VectorSamples[0],
            new Vector(
                new Point(0, 9), 
                new Point(5, 9))
        },

        new object[] 
        { 
            VectorSamples[1],
            new Vector(
                new Point(66, 77),
                new Point(66, 92))
        },

        new object[] 
        { 
            VectorSamples[2],
            new Vector(
                new Point(911, 808),
                new Point(324, 221))
        }
    };

    public static IEnumerable<IEnumerable<object>> HorizontalVectors => new[]
    {
        new object[]
        {
            VectorSamples[0],
            false // is vertical
        },

        new object[]
        {
            VectorSamples[1],
            true // is horizontal
        },

        new object[]
        {
            VectorSamples[2],
            false // is neither
        }
    };

    private static readonly string[] VectorSamples = new[]
    {
        "0,9 -> 5,9",
        "66,77 -> 66,92",
        "911,808 -> 324,221"
    };
}

public record Point(int X, int Y)
{
    public static Point FromString(string descriptor)
    {
        var startCoords = descriptor.Split(",");
        return new Point(int.Parse(startCoords[0]), int.Parse(startCoords[1]));
    }
}

public record Vector(Point Start, Point Finish)
{
    public static Vector FromString(string descriptor)
    {
        var items = descriptor.Split(" ");
        var (start, finish) = (Point.FromString(items[0]), Point.FromString(items[2]));

        return new Vector(start, finish);
    }

    public bool IsHorizontal => Start.X == Finish.X;
};
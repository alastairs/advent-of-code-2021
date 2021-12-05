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

    [Theory, MemberData(nameof(VerticalVectors))]
    public void IsVertical_is_true_when_the_Y_components_are_equal(string line, bool expected)
    {
        Assert.Equal(expected, Vector.FromString(line).IsVertical);
    }

    [Fact]
    public void IntersectsWithPoint_is_true_when_both_vectors_start_at_the_same_point()
    {
        var v1 = new Vector(new Point(0, 0), new Point(1, 1));
        var v2 = new Vector(new Point(0, 0), new Point(1, 0));

        Assert.True(v1.IntersectsWith(new Point(0, 0)));
        Assert.True(v2.IntersectsWith(new Point(0, 0)));
        Assert.True(v1.IntersectsWith(v2));
        Assert.True(v2.IntersectsWith(v1));
    }

    [Fact]
    public void IntersectsWithPoint_is_true_when_both_vectors_end_at_the_same_point()
    {
        var v1 = new Vector(new Point(0, 0), new Point(1, 1));
        var v2 = new Vector(new Point(0, 1), new Point(1, 1));

        Assert.True(v1.IntersectsWith(new Point(1, 1)));
        Assert.True(v2.IntersectsWith(new Point(1, 1)));
        Assert.True(v1.IntersectsWith(v2));
        Assert.True(v2.IntersectsWith(v1));
    }

    [Fact]
    public void IntersectsWithPoint_is_true_when_vectors_intersect_at_opposite_ends()
    {
        var v1 = new Vector(new Point(0, 0), new Point(1, 1));
        var v2 = new Vector(new Point(1, 1), new Point(0, 0));

        Assert.True(v1.IntersectsWith(new Point(1, 1)));
        Assert.True(v2.IntersectsWith(new Point(1, 1)));
        Assert.True(v1.IntersectsWith(v2));
        Assert.True(v2.IntersectsWith(v1));
    }

    [Fact]
    public void IntersectsWithPoint_is_true_when_the_point_is_within_the_vector()
    {
        var v1 = new Vector(new Point(0, 0), new Point(0, 3));

        var pointsInV1 = Enumerable.Range(0, 4).Select(y => new Point(0, y));
        Assert.All(
            pointsInV1,
            p => Assert.True(v1.IntersectsWith(p)));
        Assert.False(v1.IntersectsWith(new Point(0, 4)));
    }

    [Fact]
    public void IntersectsWithVector_is_true_when_the_vectors_intersect_anywhere_in_their_path()
    {
        var vertical = new Vector(new Point(1, -1), new Point(1, 1));
        var horizontal = new Vector(new Point(0, 0), new Point(3, 0));
        Assert.True(vertical.IntersectsWith(horizontal));
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
            true // is horizontal
        },

        new object[]
        {
            VectorSamples[1],
            false // is vertical
        },

        new object[]
        {
            VectorSamples[2],
            false // is neither
        }
    };

    public static IEnumerable<IEnumerable<object>> VerticalVectors => new[]
    {
        new object[]
        {
            VectorSamples[0],
            false // is horizontal
        },

        new object[]
        {
            VectorSamples[1],
            true // is vertical
        },

        new object[]
        {
            VectorSamples[2],
            false // is neither
        }
    };

    private static readonly string[] VectorSamples =
    {
        "0,9 -> 5,9",         // horizontal as Y = Y
        "66,77 -> 66,92",     // vertical as X = X
        "911,808 -> 324,221"  // neither
    };
}
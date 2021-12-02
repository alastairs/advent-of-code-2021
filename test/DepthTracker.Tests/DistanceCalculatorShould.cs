using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DepthTracker.Tests;

public class DistanceCalculatorShould
{
    [Theory]
    [MemberData(nameof(VectorSamples))]
    public void Parse_the_course_commands(string commandString, CourseVector expected)
    {
        var sut = new VectorParser();
        var actual = sut.CreateFrom(commandString);
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Sum_vector_magnitudes_in_a_single_dimension()
    {
        var vectors = VectorSamples.Select(vs => vs.Last()).Cast<CourseVector>();

        Assert.True(new DistanceCalculator(vectors).Sums is (5, _, 5));
    }

    [Fact]
    public void Track_depth_as_a_vector()
    {
        var finalPosition = new DistanceCalculator(AimSamples).FinalPosition;
        Assert.True(finalPosition is { X: 15, Z: 60 }, $"finalPosition is {finalPosition}");
    }

    public static readonly IEnumerable<IEnumerable<object>> VectorSamples = new[]
    {
        new object[] { "forward 5", new CourseVector(5) },
        new object[] { "down 8", new CourseVector { Z = 8 } },
        new object[] { "up 3", new CourseVector { Z = -3 } }
    };

    private static readonly IEnumerable<CourseVector> AimSamples = new[]
    {
        new CourseVector(X: 5),  // forward 5
        new CourseVector(Z: 5),  // down 5
        new CourseVector(X: 8),  // forward 8
        new CourseVector(Z: -3), // up 3
        new CourseVector(Z: 8),  // down 8
        new CourseVector(X: 2),  // forward 2
    };
}
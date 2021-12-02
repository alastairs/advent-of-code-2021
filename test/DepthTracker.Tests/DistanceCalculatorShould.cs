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

    public static readonly IEnumerable<IEnumerable<object>> VectorSamples = new[]
    {
        new object[] { "forward 5", new CourseVector(5) },
        new object[] { "down 8", new CourseVector { Z = 8 } },
        new object[] { "up 3", new CourseVector { Z = -3 } }
    };
}
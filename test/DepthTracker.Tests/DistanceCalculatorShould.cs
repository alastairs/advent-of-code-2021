using System;
using System.Collections.Generic;
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

    public static readonly IEnumerable<IEnumerable<object>> VectorSamples = new[]
    {
        new object[] { "forward 5", new CourseVector(5) },
        new object[] { "down 8", new CourseVector { Z = 8 } },
        new object[] { "up 3", new CourseVector { Z = -3 } }
    };
}

public class VectorParser
{
    public CourseVector CreateFrom(string command)
    {
        return command.Split(" ") switch
        {
            ("forward", var magnitude) => new CourseVector { X = int.Parse(magnitude) },
            ("down", var magnitude) => new CourseVector { Z = int.Parse(magnitude) },
            ("up", var magnitude) => new CourseVector { Z = int.Parse("-" + magnitude) },
            _ => throw new NotImplementedException()
        };
    }
}

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

public record CourseVector(int X = 0, int Y = 0, int Z = 0);
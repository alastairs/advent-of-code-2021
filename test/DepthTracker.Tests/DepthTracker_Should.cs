using Xunit;

namespace DepthTracker.Tests;

public class DepthTrackerShould
{
    [Fact]
    public void Calculates_depth_from_monotonically_increasing_sequence()
    {
        var depth = new DepthTracker().Calculate(new[] { 1, 2, 3 });
        Assert.Equal(2, depth);
    }

    [Fact]
    public void Calculates_depth_from_monotonically_decreasing_sequence()
    {
        var depth = new DepthTracker().Calculate(new[] { 3, 2, 1 });
        Assert.Equal(0, depth);
    }

    [Fact]
    public void Calculates_depth_ignoring_decreases()
    {
        var depth = new DepthTracker().Calculate(new[] { 1, 5, 3 });
        Assert.Equal(1, depth);
    }

    [Fact]
    public void Aggregate_over_a_sliding_window()
    {
        var depth = new DepthTracker().Calculate(new[]
        {
            1, // A
            2, // A B
            3, // A B C
            4, //   B C D
            5, //     C D
            6  //       D
        }, 3);

        // sum(A) = 6
        // sum(B) = 9
        // sum(C) = 12
        // sum(D) = 15
        Assert.Equal(3, depth);
    }
}
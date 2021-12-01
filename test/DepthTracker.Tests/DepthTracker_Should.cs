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
}
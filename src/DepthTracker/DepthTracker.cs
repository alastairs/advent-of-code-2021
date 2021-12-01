namespace DepthTracker;

public class DepthTracker
{
    public int Calculate(IEnumerable<int> measurements) => measurements.Buffer(2, 1).Count(b => b.First() < b.Last());
}
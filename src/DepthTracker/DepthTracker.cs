namespace DepthTracker;

public class DepthTracker
{
    public int Calculate(IEnumerable<int> measurements)
    {
        var buffers = measurements.Buffer(3, 1).ToList();
        var sums = buffers.Select((b, i) => b.Sum()).ToArray();
        return sums.Buffer(2, 1).Count(b => b.First() < b.Last());
    }
}
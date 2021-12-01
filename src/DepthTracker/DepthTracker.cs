namespace DepthTracker;

public class DepthTracker
{
    public int Calculate(IEnumerable<int> measurements, int windowSize = 1) =>
        measurements.Buffer(windowSize, 1)
            .Select((b, i) => b.Sum())
            .Buffer(2, 1)
            .Count(b => b.First() < b.Last());
}
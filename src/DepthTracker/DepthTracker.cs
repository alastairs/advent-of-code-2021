namespace DepthTracker;

public class DepthTracker
{
    public int Calculate(IEnumerable<int> measurements) => measurements.Count() - 1;
}
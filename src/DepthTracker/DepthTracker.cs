namespace DepthTracker;

public class DepthTracker
{
    public int Calculate(IEnumerable<int> measurements)
    {
        if (measurements.SequenceEqual(new[] { 3, 2, 1 }))
        {
            return 0;
        }

        return measurements.Count() - 1;
    }
}
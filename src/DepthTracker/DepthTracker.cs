namespace DepthTracker;

public class DepthTracker
{
    public int Calculate(IEnumerable<int> measurements)
    {
        var measurementsList = measurements.ToList();
        return measurementsList.Zip(measurementsList.Skip(1)).Count(m => m.First < m.Second);
    }
}
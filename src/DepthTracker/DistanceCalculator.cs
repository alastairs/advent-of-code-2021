namespace DepthTracker;

public class DistanceCalculator
{
    private readonly IEnumerable<CourseVector> _vectors;

    public DistanceCalculator(IEnumerable<CourseVector> vectors)
    {
        _vectors = vectors ?? throw new ArgumentNullException(nameof(vectors));
    }

    public (int x, int y, int z) Sums => (_vectors.Sum(v => v.X), _vectors.Sum(v => v.Y), _vectors.Sum(v => v.Z));

    public CourseVector FinalPosition => _vectors.Aggregate((acc, v) =>
        acc with
        {
            X = acc.X + v.X,
            Y = acc.Y + v.Z,
            Z = acc.Z + (acc.Y * v.X)
        });
}
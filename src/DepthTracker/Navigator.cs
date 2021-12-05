namespace DepthTracker;

public class Navigator
{
    private readonly List<Vector> _vectors;

    public Navigator(string[] input)
    {
        _vectors = input.Select(Vector.FromString)
            .Where(v => v.IsHorizontal || v.IsVertical)
            .ToList();
    }

    public IEnumerable<Point> FindDangerPoints()
    {
        var xs = _vectors.SelectMany(v => new[] { v.Start.X, v.Finish.X }).Distinct().OrderBy(x => x).ToList();
        var ys = _vectors.SelectMany(v => new[] { v.Start.Y, v.Finish.Y }).Distinct().OrderBy(y => y).ToList();

        var intersectingVectors = _vectors.SelectMany(v1 => _vectors.Where(v1.IntersectsWith)).Distinct().ToArray();

        for (var x = xs.Min(); x <= xs.Max(); x++)
        {
            for (var y = ys.Min(); y <= ys.Max(); y++)
            {
                var c = (x,y);
                var intersectionCount = intersectingVectors.Count(v => v.IntersectsWith(c));
                if (intersectionCount == 0) Debug.Write(".");
                else Debug.Write(intersectionCount);

                if (intersectionCount > 1)
                {
                    yield return c;
                }
            }

            Debug.WriteLine();
        }
    }
}
namespace DepthTracker;

public class Navigator
{
    private readonly List<Vector> _vectors;

    public Navigator(string[] input)
    {
        Debug.Enabled = false;
        _vectors = input.Select(Vector.FromString)
            .Where(v => v.IsHorizontal || v.IsVertical)
            .ToList();
    }

    public IEnumerable<Point> FindDangerPoints()
    {
        var xs = _vectors.SelectMany(v => new[] { v.Start.X, v.Finish.X }).Distinct().OrderBy(x => x).ToList();
        var ys = _vectors.SelectMany(v => new[] { v.Start.Y, v.Finish.Y }).Distinct().OrderBy(y => y).ToList();

        for (var x = xs.Min(); x <= xs.Max(); x++)
        {
            for (var y = ys.Min(); y <= ys.Max(); y++)
            {
                var c = (x,y);
                Debug.WriteLine(c);
                var intersectionCount = _vectors.Count(v => v.IntersectsWith(c));
                if (intersectionCount == 0) Console.Write(".");
                else Console.Write(intersectionCount);

                if (intersectionCount > 1)
                {
                    yield return c;
                }
            }

            Console.WriteLine();
        }
    }
}

public record Vector(Point Start, Point Finish)
{
    public static Vector FromString(string descriptor)
    {
        var items = descriptor.Split(" ");
        var (start, finish) = (Point.FromString(items[0]), Point.FromString(items[2]));

        return new Vector(start, finish);
    }

    public bool IsHorizontal => Start.Y == Finish.Y;

    public bool IsVertical => Start.X == Finish.X;

    public bool IntersectsWith(Point p)
    {
        if (p == Start || p == Finish) return true;
        return EnumeratePoints().Any(c => c == p);
    }

    private IEnumerable<Point> EnumeratePoints()
    {
        if (IsHorizontal)
        {
            return EnumerateHorizontalPoints();
        }

        if (IsVertical)
        {
            return EnumerateVerticalPoints();
        }

        return Enumerable.Empty<Point>();
    }

    private IEnumerable<Point> EnumerateVerticalPoints()
    {
        if (Start.Y == Finish.Y)
        {
            yield break;
        }

        var (min, max) = Start.Y < Finish.Y ? (Start, Finish) : (Finish, Start);

        for (var i = min.Y; i <= max.Y; i++)
        {
            yield return min with { Y = min.Y + i };
        }
    }

    private IEnumerable<Point> EnumerateHorizontalPoints()
    {
        if (Start.X == Finish.X)
        {
            yield break;
        }

        var (min, max) = Start.X < Finish.X ? (Start, Finish) : (Finish, Start);
        
        for (var i = min.X; i <= max.X; i++)
        {
            yield return min with { X = min.X + i };
        }
    }

    public bool IntersectsWith(Vector other)
    {
        if (Start == other.Start || Finish == other.Finish || Start == other.Finish) return true;

        var points = EnumeratePoints();
        var otherPoints = other.EnumeratePoints();

        return points.Intersect(otherPoints).Any();
    }
};
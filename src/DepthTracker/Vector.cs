namespace DepthTracker;

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
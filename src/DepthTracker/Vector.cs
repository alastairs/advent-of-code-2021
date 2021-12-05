namespace DepthTracker;

public record Vector(Point Start, Point Finish)
{
    public static Vector FromString(string descriptor)
    {
        var items = descriptor.Split(" ");
        var (start, finish) = (Point.FromString(items[0]), Point.FromString(items[2]));

        return new Vector(start, finish);
    }

    public bool IsHorizontal => Start.X == Finish.X;

    public bool IsVertical => Start.Y == Finish.Y;

    public bool IntersectsWith(Point p)
    {
        if (p == Start || p == Finish) return true;
        return EnumeratePoints().Any(c => c == p);
    }

    private IEnumerable<Point> EnumeratePoints()
    {
        if (IsVertical)
        {
            for (var i = Start.X; i <= Finish.X; i++)
            {
                yield return Start with { X = Start.X + i };
            }
        }
        else if (IsHorizontal)
        {
            for (var i = Start.Y; i <= Finish.Y; i++)
            {
                yield return Start with { Y = Start.Y + i };
            }
        }
    }

    public bool IntersectsWith(Vector other) => Start == other.Start || Finish == other.Finish || Start == other.Finish;
};
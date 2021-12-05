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
};
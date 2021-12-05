namespace DepthTracker;

public record Point(int X, int Y)
{
    public static Point FromString(string descriptor)
    {
        var startCoords = descriptor.Split(",");
        return new Point(int.Parse(startCoords[0]), int.Parse(startCoords[1]));
    }

    public static implicit operator Point((int x, int y) tuple) => new(tuple.x, tuple.y);
}
namespace DepthTracker;

public static class Debug
{
    public static bool Enabled { get; set; }

    public static string BoolToString(this bool value)
    {
        if (!Enabled)
        {
            return string.Empty;
        }

        return value ? "✅" : "❌";
    }

    public static string ArrayToString<T>(this IList<T> array)
    {
        if (!Enabled)
        {
            return string.Empty;
        }

        return $"[{string.Join(", ", array)}]";
    }

    public static void WriteLine(object? obj = null)
    {
        if (!Enabled)
        {
            return;
        }

        Console.WriteLine(obj);
    }

    public static void Write(object? obj = null)
    {
        if (!Enabled)
        {
            return;
        }

        Console.Write(obj);
    }
}

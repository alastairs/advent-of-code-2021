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

    public static string ArrayToString<T>(this T[] array)
    {
        if (!Enabled)
        {
            return string.Empty;
        }

        return $"[{string.Join(", ", array)}]";
    }

    public static void WriteLine(object obj)
    {
        if (!Enabled)
        {
            return;
        }

        Console.WriteLine(obj);
    }
}

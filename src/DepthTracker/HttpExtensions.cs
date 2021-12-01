namespace DepthTracker;

internal static class HttpExtensions
{
    internal static int TryParseQueryValue(this IQueryCollection queryCollection, string name)
    {
        int value = default;
        var _ = queryCollection.TryGetValue("windowSize", out var values) &&
            int.TryParse(values.SingleOrDefault(), out value);

        return value;
    }
}
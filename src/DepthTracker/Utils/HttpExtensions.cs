namespace DepthTracker.Utils;

internal static class HttpExtensions
{
    internal static int TryParseQueryValue(this IQueryCollection queryCollection, string name)
    {
        int value = default;
        var _ = queryCollection.TryGetValue(name, out var values) &&
            int.TryParse(values.SingleOrDefault(), out value);

        return value;
    }
}
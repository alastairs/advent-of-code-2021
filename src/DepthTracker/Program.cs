using DepthTracker;
using DepthTracker.Utils;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/depth-tracker", async ctx =>
{
    var text = await File.ReadAllLinesAsync("depth-tracker.txt");
    var parsed = text.Select(int.Parse);

    var windowSize = ctx.Request.Query.TryParseQueryValue("windowSize");
    await ctx.Response.WriteAsync($"{new DepthTracker.DepthTracker().Calculate(parsed, windowSize)}");
});

app.MapGet("/distance-calculator", async () =>
{
    var text = await File.ReadAllLinesAsync("distance-calculator.txt");
    var parsed = text.Select(new VectorParser().CreateFrom);

    var (x, y, z) = new DistanceCalculator(parsed).Sums;
    return new CourseVector(x, y, z);
});

app.Run();

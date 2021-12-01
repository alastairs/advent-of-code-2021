using DepthTracker;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async ctx =>
{
    var text = await File.ReadAllLinesAsync("input.txt");
    var parsed = text.Select(int.Parse);

    var windowSize = ctx.Request.Query.TryParseQueryValue("windowSize");
    await ctx.Response.WriteAsync($"{new DepthTracker.DepthTracker().Calculate(parsed, windowSize)}");
});

app.Run();

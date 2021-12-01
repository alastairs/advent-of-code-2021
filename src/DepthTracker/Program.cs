var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async () =>
{
    var text = await File.ReadAllLinesAsync("input.txt");
    var parsed = text.Select(int.Parse);
    return new global::DepthTracker.DepthTracker().Calculate(parsed);
});

app.Run();

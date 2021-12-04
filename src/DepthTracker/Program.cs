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

    return new DistanceCalculator(parsed).FinalPosition;
});

app.MapGet("/power-consumption", async () =>
{
    var text = await File.ReadAllLinesAsync("power-consumption.txt");
    var diagnostics = DiagnosticsReport.Create(text);
    return diagnostics;
});

app.MapGet("/bingo", async () =>
{
    var text = await File.ReadAllLinesAsync("bingo.txt");
    var bingo = new Bingo.Bingo(text);
    var (winningNumber, winningBoard) = bingo.FindWinner();
    var (finalWinningNumber, finalWinningBoard) = bingo.FindFinalWinner();
    return new[]
    {
        new { winningNumber, winningBoard.Score },
        new { winningNumber = finalWinningNumber, finalWinningBoard.Score }
    };
});

app.Run();

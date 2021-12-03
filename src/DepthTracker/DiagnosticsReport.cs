namespace DepthTracker;

public class DiagnosticsReport
{
    public int Gamma { get; }

    public int Epsilon { get; }

    private DiagnosticsReport(int γ, int ε)
    {
        Gamma = γ;
        Epsilon = ε;
    }

    public static DiagnosticsReport Create(IEnumerable<string> text)
    {
        return new DiagnosticsReport(0, 0);
    }
}
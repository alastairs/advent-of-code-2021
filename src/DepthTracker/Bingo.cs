namespace Bingo;

public class Bingo
{
    private readonly ICollection<BingoBoard> _boards = new List<BingoBoard>();

    public Bingo(string[] sample)
    {
        NumbersToCall = sample[0].Split(",").Select(s => s.Trim()).Select(s => int.Parse(s)).ToList();
        for (var i = 2; i < sample.Length; i += 6)
        {
            _boards.Add(new BingoBoard(sample[i..(i + 5)]));
        }
    }

    public (int winningNumber, BingoBoard board) FindWinner()
    {
        var winningNumber = 0;
        var winningBoard = new BingoBoard(Array.Empty<string>());
        foreach (var called in NumbersToCall)
        {
            _boards.ForEach(b => b.Mark(called));

            if ((winningBoard = _boards.FirstOrDefault(b => b.HasWon)) != null)
            {
                winningNumber = called;
                break;
            }
        }

        return (winningNumber, winningBoard);
    }

    public IEnumerable<int> NumbersToCall { get; }
    public IEnumerable<BingoBoard> Boards => _boards;
}

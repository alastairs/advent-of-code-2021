using DepthTracker;

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

    public (int winningNumber, BingoBoard board) FindWinner() => FindAllWinners().First();

    public (int winningNumber, BingoBoard winningBoard) FindFinalWinner() => FindAllWinners().Last();

    private IEnumerable<(int winningNumber, BingoBoard winningBoard)> FindAllWinners()
    {
        foreach (var called in NumbersToCall)
        {
            var remainingBoards = _boards.Where(b => !b.HasWon).ToList();
            if (!remainingBoards.Any()) yield break;

            remainingBoards.ForEach(b => b.Mark(called));

            if (remainingBoards.Any(b => b.HasWon))
            {
                var tuple = (called, remainingBoards.First(b => b.HasWon));
                yield return tuple;

                // debugging here
                var (winningNumber, winningBoard) = tuple;
                Debug.WriteLine($"WE HAVE A WINNER!! The {winningNumber} sealed it!");

                var winningBoardIndex = Array.IndexOf(_boards.ToArray(), winningBoard) + 1;
                var completeRow = Enumerable.Range(0, 5).FirstOrDefault(winningBoard.RowIsComplete, -1);
                if (completeRow > -1) Debug.WriteLine($"Winning board {winningBoardIndex} won via complete row {completeRow + 1}");

                var completeColumn = Enumerable.Range(0, 5).FirstOrDefault(winningBoard.ColumnIsComplete, -1);
                if (completeColumn > -1) Debug.WriteLine($"Winning board {winningBoardIndex} won via complete column {completeColumn + 1}");
            }
        }
    }

    public IEnumerable<int> NumbersToCall { get; }
    public IEnumerable<BingoBoard> Boards => _boards;
}

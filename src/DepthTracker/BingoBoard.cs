using System.Text;

namespace Bingo;

public class BingoBoard
{
    private const int BoardSize = 5;

    private int[] _board;
    private bool[] _marked = new bool[BoardSize * BoardSize];

    public bool HasWon =>
        Enumerable.Range(0, BoardSize).Any(i => RowIsComplete(i) || ColumnIsComplete(i));

    public int Score
    {
        get
        {
            if (!HasWon) return 0;

            var score = 0;
            foreach(var index in Enumerable.Range(0, _board.Length))
            {
                if (!_marked[index]) score += _board[index];
            }

            return score;
        }
    }

    public BingoBoard(string[] descriptor)
    {
        _board = descriptor.SelectMany(l =>
            l.Split()
                .Where(i => i != string.Empty)
                .Select(i => int.Parse(i)))
                .ToArray();
    }

    public static implicit operator string[](BingoBoard bingoBoard)
    {
        int[] board = bingoBoard._board;
        var @return = new string[BoardSize];

        for (var i = 0; i < BoardSize; i++)
        {
            @return[i] = string.Join(
                " ",
                board.Skip(i * BoardSize)
                    .Take(BoardSize)
                    .Select(i => i.ToString().PadLeft(2, ' ')));
        }

        return @return;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        BingoBoard? other = obj as BingoBoard;
        if (other is null) return false;

        return _board.SequenceEqual(other._board) &&
            _marked.SequenceEqual(other._marked);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_board, _marked);
    }

    public override string ToString()
    {
        var @string = new StringBuilder();
        for (int i = 0; i < Math.Pow(BoardSize, 2); i++)
        {
            @string.Append($"{_board[i]}{(_marked[i] ? "*" : " ")}".PadLeft(3, ' ').PadRight(4, ' '));
            if (i % BoardSize == 4) @string.Append(Environment.NewLine);
        }

        return @string.ToString();
    }

    public void Mark(int v)
    {
        var foundIndex = Array.IndexOf(_board, v);
        if (foundIndex == -1)
        {
            return;
        }

        _marked[foundIndex] = true;
    }

    public bool IsMarked(int row, int column)
    {
        return _marked[row + column];
    }

    public bool RowIsComplete(int index)
    {
        var marked = true;
        for (var i = index; i < BoardSize; i++)
        {
            var address = index * BoardSize;
            marked = marked && _marked[address];
        }
        
        return marked;
    }

    public bool ColumnIsComplete(int index)
    {
        var marked = true;
        for (var i = index; i < _marked.Length; i += BoardSize)
        {
            marked = marked && _marked[i];
        }

        return marked;
    }

    private static string BoolToString(bool value)
    {
        return value ? "✅" : "❌";
    }

    private static string ArrayToString<T>(T[] array) {
        return $"[{string.Join(", ", array)}]";
    }
}

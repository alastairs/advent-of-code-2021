using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bingo.Tests;

public class BingoShould
{
    public readonly BingoBoard Board1 = new BingoBoard(Sample[2..7]);
    public readonly BingoBoard Board2 = new BingoBoard(Sample[8..13]);
    public readonly BingoBoard Board3 = new BingoBoard(Sample[14..^1]);

    [Fact]
    public void Parse_input_into_game()
    {
        var bingo = new Bingo(Sample);
        Assert.Equal(Sample[0], string.Join(",", bingo.NumbersToCall));
        Assert.Equal(Board1, bingo.Boards.ElementAt(0));
        Assert.Equal(Board2, bingo.Boards.ElementAt(1));
        Assert.Equal(Board3, bingo.Boards.ElementAt(2));
    }

    [Fact]
    public void Winner_of_sample_game_is_board_3_with_score_188_and_winning_number_24()
    {
        var bingo = new Bingo(Sample);

        var (winningNumber, winningBoard) = bingo.FindWinner();
        Assert.Equal(bingo.Boards.ElementAt(2), winningBoard);
        Assert.Equal(188, winningBoard.Score);
        Assert.Equal(24, winningNumber);
    }

    [Theory, MemberData(nameof(For_all_boards))]
    public void Mark_a_called_number(string[] rawBoard, BingoBoard board)
    {
        var row1 = Row_1(rawBoard);
        row1.ForEach(ci => board.Mark(ci.called));

        Assert.All(row1, ci => board.IsMarked(0, ci.index));
        Assert.All(row1, ci => board.IsMarked(0, ci.index + 1));
    }

    [Fact]
    public void RowIsComplete_is_true_if_full_row_is_marked()
    {
        Row_1(Board1).Select(ci => ci)
            .ForEach(ci => Board1.Mark(ci.called));

        Assert.True(Board1.RowIsComplete(0));
    }

    [Theory, MemberData(nameof(For_all_boards))]
    public void ColumnIsComplete_is_true_if_full_column_is_marked(string[] rawBoard, BingoBoard board)
    {
        var column = ParseSampleSubset(board).Select(r => r.First()).ToArray();
        column.ForEach(c => board.Mark(c));

        Assert.True(board.ColumnIsComplete(0));
    }

    [Theory, MemberData(nameof(For_all_boards))]
    public void Board_score_is_zero_if_board_does_not_win(string[] _, BingoBoard board)
    {
        Assert.False(board.HasWon);
        Assert.Equal(0, board.Score);
    }

    [Theory, MemberData(nameof(For_all_boards))]
    public void Board_score_is_sum_of_all_unmarked_numbers(string[] rawBoard, BingoBoard board)
    {
        Row_1(rawBoard).Select(ci => ci)
            .ForEach(ci => board.Mark(ci.called));

        Console.WriteLine(board);
        Assert.Equal(
            ParseSampleSubset(rawBoard[1..^1]).SelectMany(i => i).Sum(),
            board.Score);
    }

    [Fact]
    public void Determine_winning_board()
    {
        var bingo = new Bingo(Sample);
    }

    public static IEnumerable<CallIndex> Row_1(string[] board) => 
        board.First().Split().Where(i => i != string.Empty)
            .Select((@int, index) => new CallIndex(int.Parse(@int), index));

    public static IEnumerable<IEnumerable<object>> For_all_boards()
    {
        // for (var i = 2; i < Sample.Length; i += 6)
        // {
        //     var rawBoard = Sample[i..(i + 5)];
        //     yield return new object[] { rawBoard, new BingoBoard(rawBoard) };
        // }

        yield return new object[] { Sample[2..7], new BingoBoard(Sample[2..7]) };
    }

    public record CallIndex(int called, int index);

    public static string[] Sample = new [] {
        "7,4,9,5,11,17,23,2,0,14,21,24,10,16,13,6,15,25,12,22,18,20,8,19,3,26,1",
        "",
        "22 13 17 11  0",
        " 8  2 23  4 24",
        "21  9 14 16  7",
        " 6 10  3 18  5",
        " 1 12 20 15 19",
        "",
        " 3 15  0  2 22",
        " 9 18 13 17  5",
        "19  8  7 25 23",
        "20 11 10 24  4",
        "14 21 16 12  6",
        "",
        "14 21 17 24  4",
        "10 16 15  9 19",
        "18  8 23 26 20",
        "22 11 13  6  5",
        " 2  0 12  3  7",
        ""
    };

    private int[][] ParseSampleSubset(string[] input) {
        return input.Select(
            l => l.Split()
                .Where(s => s != string.Empty)
                .Select(s => int.Parse(s))
                .ToArray())
            .ToArray();
    }
}

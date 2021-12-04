using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bingo.Tests;

public class BingoShould
{
    public readonly BingoBoard Board1 = new BingoBoard(Sample[2..7]);

    [Fact]
    public void Parse_input_into_game()
    {
        var input = Sample[0];
        var boards = new List<BingoBoard>();
        for (var i = 2; i < Sample.Length; i += 6)
        {
            boards.Add(new BingoBoard(Sample[i..(i + 5)]));
        }

        Assert.Equal(Sample[0], input);
        Assert.Equal(Sample[2..7], boards[0]);
        Assert.Equal(Sample[8..13], boards[1]);
        Assert.Equal(Sample[14..^1], boards[2]);
    }

    [Theory, MemberData(nameof(Board_1_Row_1))]
    public void Mark_a_called_number(CallIndex tuple)
    {
        var (called, index) = tuple;
        Board1.Mark(called);

        Assert.True(Board1.IsMarked(0, index));
        Assert.False(Board1.IsMarked(0, index + 1));
    }

    [Fact]
    public void RowIsComplete_is_true_if_full_row_is_marked()
    {
        Board_1_Row_1().SelectMany(ci => ci)
            .ForEach(ci => Board1.Mark(ci.called));

        Assert.True(Board1.RowIsComplete(0));
    }

    [Fact]
    public void ColumnIsComplete_is_true_if_full_column_is_marked()
    {
        var column = Sample[2..7].Select(r => r.Split().Where(i => i != string.Empty).First()).Select(s => int.Parse(s)).ToArray();
        Assert.Equal(new[] { 22, 8, 21, 6, 1 }, column);

        column.ForEach(c => Board1.Mark(c));

        Assert.True(Board1.ColumnIsComplete(0));
    }

    [Fact]
    public void Board_score_is_zero_if_board_does_not_win()
    {
        Assert.False(Board1.HasWon);
        Assert.Equal(0, Board1.Score);
    }

    [Fact]
    public void Board_score_is_sum_of_all_unmarked_numbers()
    {
        var board = Board1;
        Board_1_Row_1().SelectMany(ci => ci)
            .ForEach(ci => Board1.Mark(ci.called));

        Console.WriteLine(board);
        Assert.Equal(
            ParseSampleSubset(Sample[3..7]).SelectMany(i => i).Sum(),
            board.Score);
    }

    public static IEnumerable<IEnumerable<CallIndex>> Board_1_Row_1()
    {
        var ps = Sample[2].Split().Where(i => i != string.Empty)
                        .Select((@int, index) => new CallIndex(int.Parse(@int), index));

        foreach (var p in ps)
        {
            yield return new[] { p };
        }
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

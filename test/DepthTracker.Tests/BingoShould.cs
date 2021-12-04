using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Bingo.Tests;

public class BingoBoard
{
    private const int BoardSize = 5;

    private int[] _board;

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
}

public class BingoShould
{
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

    public string[] Sample = new [] {
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
}

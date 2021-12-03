using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DepthTracker.Tests
{
    public class DiagnosticsReportShould
    {
        [Fact]
        public void Calculate_diagnostics()
        {
            var diagnostics = DiagnosticsReport.Create(Samples);

            Assert.Equal(22, diagnostics.Gamma);
            Assert.Equal(9, diagnostics.Epsilon);
        }

        [Fact]
        public void Transposer_creates_n_collections_of_m_items_for_m_collections_of_n_items()
        {
            var input = new[] { new[] { 1, 2 } };
            var output = Transpose(input);
            Assert.Equal(input[0].Length, output.Length);
        }

        [Theory]
        [MemberData(nameof(TransposeTestCases))]
        public void Writes_item_to_transposed_location(int[][] input)
        {
            var output = Transpose(input);
            Assert.Equal(input[0][0], output[0][0]);
            Assert.Equal(input[0][^1], output[^1][0]);
            Assert.Equal(input[^1][^1], output[^1][^1]);
        }

        private static T[][] Transpose<T>(IReadOnlyList<T[]> input)
        {
            var rowCount = input.Count;
            var columnCount = input.Max(i => i.Length);
            var transposed = new T[columnCount][];

            if (rowCount == columnCount)
            {
                for (var i = 1; i < rowCount; i++)
                {
                    // ReSharper disable once ConstantNullCoalescingCondition - this actually *is* null somehow
                    transposed[i] ??= new T[rowCount];

                    for (var j = 0; j < rowCount; j++)
                    {
                        // ReSharper disable once ConstantNullCoalescingCondition - this actually *is* null somehow
                        transposed[j] ??= new T[rowCount];

                        (transposed[i][j], transposed[j][i]) =
                        (input[j][i], input[i][j]);
                    }
                }
            }
            else
            {
                for (var column = 0; column < columnCount; column++)
                {
                    transposed[column] = new T[rowCount];
                    for (var row = 0; row < rowCount; row++)
                    {
                        transposed[column][row] = input[row][column];
                    }
                }
            }

            return transposed;
        }

        public static IEnumerable<IEnumerable<int[][]>> TransposeTestCases => new[] // Test case
        {
            new[] // parameters for test case
            {
                // The array-of-arrays input
                new[] { new[] { 0, 1 } }
            },
            new[] // parameters for test case
            {
                // The array-of-arrays input
                new[]
                {
                    new[] { 0, 1 }, // => 0, 2
                    new[] { 2, 3 }  // => 1, 3
                }
            },
            new[] // parameters for test case
            {
                // The array-of-arrays input
                new[]
                {
                    new[] { 0, 1 }, // => 0, 2, 4
                    new[] { 2, 3 }, // => 1, 3, 5
                    new[] { 4, 5 }  // =>
                }
            }
        };

        private static IEnumerable<string> Samples => new[]
        {
            "00100",
            "11110",
            "10110",
            "10111",
            "10101",
            "01111",
            "00111",
            "11100",
            "10000",
            "11001",
            "00010",
            "01010"
        };
    }
}

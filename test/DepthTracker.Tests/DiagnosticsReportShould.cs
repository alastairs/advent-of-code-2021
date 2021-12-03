using System.Collections.Generic;
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
            var output = DiagnosticsReport.Transpose(input);
            Assert.Equal(input[0].Length, output.Length);
        }

        [Theory]
        [MemberData(nameof(TransposeTestCases))]
        public void Writes_item_to_transposed_location(int[][] input)
        {
            var output = DiagnosticsReport.Transpose(input);
            Assert.Equal(input[0][0], output[0][0]);
            Assert.Equal(input[0][^1], output[^1][0]);
            Assert.Equal(input[^1][^1], output[^1][^1]);
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

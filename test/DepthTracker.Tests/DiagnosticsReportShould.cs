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

        private T[][] Transpose<T>(T[][] input)
        {
            return Enumerable.Repeat(new T[input.Length], input.Max(i => i.Length)).ToArray();
        }

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

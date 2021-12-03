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

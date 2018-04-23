using System;
using Xunit;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal;

namespace PointOfSaleTerminalTest
{
    public class PointOfSaleTerminalTest
    {
        IPointOfSaleTerminal _terminal;

        [Fact]
        public void CalculateTotal_whenABCDABA_returns_13_25()
        {
            var _terminal = new PointOfSaleTerminalService();

            _terminal.SetPricing();
            _terminal.SetRebate();
            _terminal.ScanItem("DoughnutA");
            _terminal.ScanItem("DoughnutB");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutD");
            _terminal.ScanItem("DoughnutA");
            _terminal.ScanItem("DoughnutB");
            _terminal.ScanItem("DoughnutA");

            Assert.Equal(13.25, _terminal.CalculateTotal());

        }

        [Fact]
        public void CalculateTotal_whenCCCCCCC_returns_6_00()
        {
            var _terminal = new PointOfSaleTerminalService();

            _terminal.SetPricing();
            _terminal.SetRebate();
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutC");

            Assert.Equal(6.00, _terminal.CalculateTotal());

        }

        [Fact]
        public void CalculateTotal_whenABCD_returns_7_25()
        {
            var _terminal = new PointOfSaleTerminalService();

            _terminal.SetPricing();
            _terminal.SetRebate();
            _terminal.ScanItem("DoughnutA");
            _terminal.ScanItem("DoughnutB");
            _terminal.ScanItem("DoughnutC");
            _terminal.ScanItem("DoughnutD");

            Assert.Equal(7.25, _terminal.CalculateTotal());

        }
    }
}

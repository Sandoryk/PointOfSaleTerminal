using System;
using System.Collections.Generic;
using Xunit;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal;

namespace PointOfSaleTerminalTest
{
    public class PointOfSaleTerminalTest
    {
        IPointOfSaleTerminal _terminal;
        private readonly Dictionary<string, double> _priceList;

        public PointOfSaleTerminalTest()
        {
            _priceList = new Dictionary<string, double>
            {
                {"A", 1.25},
                {"B", 4.25},
                {"C", 1.00},
                {"D", 0.75}
            };
        }

        [Fact]
        public void CalculateTotal_whenABCDABA_returns_13_25()
        {
            _terminal = new PointOfSaleTerminalService();

            _terminal.SetPricing(_priceList);
            _terminal.ScanItem("A");
            _terminal.ScanItem("B");
            _terminal.ScanItem("C");
            _terminal.ScanItem("D");
            _terminal.ScanItem("A");
            _terminal.ScanItem("B");
            _terminal.ScanItem("A");

            Assert.Equal(13.25, _terminal.CalculateTotal());

        }

        [Fact]
        public void CalculateTotal_whenCCCCCCC_returns_6_00()
        {
            _terminal = new PointOfSaleTerminalService();

            _terminal.SetPricing(_priceList);
            _terminal.ScanItem("C");
            _terminal.ScanItem("C");
            _terminal.ScanItem("C");
            _terminal.ScanItem("C");
            _terminal.ScanItem("C");
            _terminal.ScanItem("C");
            _terminal.ScanItem("C");

            Assert.Equal(6.00, _terminal.CalculateTotal());

        }

        [Fact]
        public void CalculateTotal_whenABCD_returns_7_25()
        {
            _terminal = new PointOfSaleTerminalService();

            _terminal.SetPricing(_priceList);
            _terminal.ScanItem("A");
            _terminal.ScanItem("B");
            _terminal.ScanItem("C");
            _terminal.ScanItem("D");

            Assert.Equal(7.25, _terminal.CalculateTotal());

        }
    }
}

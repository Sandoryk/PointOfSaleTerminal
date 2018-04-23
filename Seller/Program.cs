using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal;

namespace Seller
{
    class Program
    {
        static void Main(string[] args)
        {
            PointOfSaleTerminalService pos = new PointOfSaleTerminalService();

            pos.SetPricing();
            pos.SetRebate();
            pos.ScanItem("DoughnutC");
            pos.ScanItem("DoughnutC");
            pos.ScanItem("DoughnutC");
            pos.ScanItem("DoughnutC");
            pos.ScanItem("DoughnutC");
            pos.ScanItem("DoughnutC");
            pos.ScanItem("DoughnutC");

            double total = pos.CalculateTotal();

            Console.WriteLine($"Total {total}");
            Console.ReadLine();
        }
    }
}

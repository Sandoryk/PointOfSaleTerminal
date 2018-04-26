using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing(Dictionary<string, double> priceList);
        void ScanItem(string itemCode);
        double CalculateTotal();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        void SetRebate();
        void SetPricing();
        void ScanItem(string itemName, double itemQuantity = 1);
        double CalculateTotal();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.Interfaces
{
    public interface IPointOfSaleTerminal
    {
        void SetPricing();
        void ScanItem(string itemCode);
        double CalculateTotal();
    }
}

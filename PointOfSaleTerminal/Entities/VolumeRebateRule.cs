using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.Entities
{
    public class VolumeRebateRule
    {
        public string ItemName { get; set; }
        public double RebateQuatity { get; set; }
        public int Numerator { get; set; }
        public int Denominator { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSaleTerminal.Entities
{
    public class VolumeDiscountRule
    {
        public string ItemName { get; set; }
        public double RebateQuatity { get; set; }
        public double DiscountAmount { get; set; }
    }
}

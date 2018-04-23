using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Enums;

namespace PointOfSaleTerminal.Entities
{
    public class ItemRebate
    {
        public RebateType Type { get; set; }
        public double RebateAmount { get; set; }
    }
}

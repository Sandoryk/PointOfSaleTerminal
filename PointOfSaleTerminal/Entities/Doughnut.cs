using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Entities;

namespace PointOfSaleTerminal.Entities
{
    public class Doughnut: ItemBase
    {
        public ItemRebate Rebate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Entities;

namespace PointOfSaleTerminal.Interfaces
{
    public interface IRebate
    {
        ItemRebate FindRebate(ItemBase item);
    }
}
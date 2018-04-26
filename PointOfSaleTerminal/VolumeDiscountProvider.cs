using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Entities;

namespace PointOfSaleTerminal
{
    public class VolumeDiscountProvider : IDiscount
    {
        readonly Dictionary<string, Discount>  _rebateRules;

        public VolumeDiscountProvider()
        {
            _rebateRules = new Dictionary<string, Discount> {
                { "A", new Discount {ItemName = "A", DiscountQuatity = 3, DiscountAmount = 0.75 }},
                { "C", new Discount {ItemName = "C", DiscountQuatity = 6, DiscountAmount = 1 }}
            };
        }
        public double FindDiscount(Item item)
        {
            if (!_rebateRules.ContainsKey(item.Code))
            {
                return 0;
            }
            var foundRule = _rebateRules[item.Code];
            if (item.Quantity < foundRule.DiscountQuatity)
            {
                return 0;
            }
            return foundRule.DiscountAmount;
        }
    }
}

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
        readonly Dictionary<string, VolumeDiscountRule>  _rebateRules;

        public VolumeDiscountProvider()
        {
            _rebateRules = new Dictionary<string, VolumeDiscountRule> {
                { "A", new VolumeDiscountRule {ItemName = "A", RebateQuatity = 3, DiscountAmount = 0.75 }},
                { "C", new VolumeDiscountRule {ItemName = "C", RebateQuatity = 6, DiscountAmount = 1 }}
            };
        }
        public double FindDiscount(Item item)
        {
            if (!_rebateRules.ContainsKey(item.Code))
            {
                return 0;
            }
            var foundRule = _rebateRules[item.Code];
            if (item.Quantity < foundRule.RebateQuatity)
            {
                return 0;
            }
            return foundRule.DiscountAmount;
        }
    }
}

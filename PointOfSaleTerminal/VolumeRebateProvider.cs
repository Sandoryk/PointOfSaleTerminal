using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Entities;
using PointOfSaleTerminal.Enums;

namespace PointOfSaleTerminal
{
    public class VolumeRebateProvider : IRebate
    {
        readonly List<VolumeRebateRule>  _rebateRules;

        public VolumeRebateProvider()
        {
            _rebateRules = new List<VolumeRebateRule> {
                new VolumeRebateRule {ItemName = "DoughnutA", RebateQuatity = 3, Numerator = 1, Denominator = 5 },
                new VolumeRebateRule {ItemName = "DoughnutC", RebateQuatity = 6, Numerator = 1, Denominator = 6 },
            };
        }
        public ItemRebate FindRebate(ItemBase item)
        {
            var foundRule = _rebateRules.Find(rule => rule.ItemName == item.Name && item.Quantity >= rule.RebateQuatity);
            if (foundRule == null)
                return null;
            return new ItemRebate
            {
                Type = RebateType.Absolute,
                RebateAmount = GetAbsoluteRebateUsingRule(item.Price, foundRule)
            };
        }

        private double GetAbsoluteRebateUsingRule(double price, VolumeRebateRule rule)
        {
            if (rule.Denominator == 0)
                return 0;
            return Math.Round(price * rule.RebateQuatity / rule.Denominator * rule.Numerator, 2);
        }
    }
}

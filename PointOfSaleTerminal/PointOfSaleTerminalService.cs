using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Entities;
using PointOfSaleTerminal.Enums;

namespace PointOfSaleTerminal
{
    public class PointOfSaleTerminalService : IPointOfSaleTerminal
    {
        readonly Dictionary<string, Doughnut> _items;
        readonly Dictionary<string, ItemPrice> _priceList;
        readonly IRebate _rebateProvider;

        public PointOfSaleTerminalService()
        {
            _items = new Dictionary<string, Doughnut>();
            _priceList = new Dictionary<string, ItemPrice>();
            _rebateProvider = new VolumeRebateProvider();
        }

        private double GetItemPrice(string itemCode)
        {
            if (!_priceList.ContainsKey(itemCode))
            {
                throw new Exception($"No price for {itemCode} found!");
            }
            return _priceList[itemCode].Price;
        }

        private void ApplyRebate(Doughnut item)
        {
            if (_rebateProvider != null)
            {
                item.Rebate = _rebateProvider.FindRebate(item);
            }
        }

        public void ScanItem(string itemCode)
        {
            if (_items.ContainsKey(itemCode)) {
                var itemAlreadyInList = _items[itemCode];
                itemAlreadyInList.Quantity += 1;
            }
            else {
                _items[itemCode] = new Doughnut { Code = itemCode, Quantity = 1, Price = GetItemPrice(itemCode) };
            }
        }

        public bool IsItemExists(string itemCode)
        {
            return _priceList.ContainsKey(itemCode);
        }

        public void AddItemWithPrice(string itemCode, double price)
        {
            _priceList.Add(itemCode, new ItemPrice { ItemName = itemCode, Price = price });
        }

        public void SetPricing(Dictionary<string, double> priceList)
        {
            foreach (var price in priceList)
            {
                _priceList.Add(price.Key, new ItemPrice { ItemName = price.Key, Price = price.Value });
            }
        }
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in _items.Values)
            {
                var itemSum = item.Quantity * item.Price;
                ApplyRebate(item);
                if (item.Rebate != null && item.Rebate.Type == RebateType.Absolute)
                {
                    itemSum -= item.Rebate.RebateAmount;
                }
                total += itemSum;
            }
            return total;
        }
    }
}

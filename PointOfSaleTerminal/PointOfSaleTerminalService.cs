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

        private void ApplyRebate(Doughnut item, ref double itemSum)
        {
            if (_rebateProvider != null)
            {
                item.Rebate = _rebateProvider.FindRebate(item);
                if (item.Rebate != null && item.Rebate.Type == RebateType.Absolute)
                {
                    itemSum -= item.Rebate.RebateAmount;
                }
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

        public void SetPricing()
        {
            _priceList.Add("DoughnutA", new ItemPrice { ItemName = "DoughnutA", Price = 1.25 });
            _priceList.Add("DoughnutB", new ItemPrice { ItemName = "DoughnutB", Price = 4.25 });
            _priceList.Add("DoughnutC", new ItemPrice { ItemName = "DoughnutC", Price = 1.00 });
            _priceList.Add("DoughnutD", new ItemPrice { ItemName = "DoughnutD", Price = 0.75 });
        }
        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in _items.Values)
            {
                var itemSum = item.Quantity * item.Price;
                ApplyRebate(item, ref itemSum);
                total += itemSum;
            }
            return total;
        }
    }
}

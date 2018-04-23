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
        readonly List<Doughnut> _items;
        readonly List<ItemPrice> _priceList;
        IRebate _rebateProvider;

        public PointOfSaleTerminalService()
        {
            _items = new List<Doughnut>();
            _priceList = new List<ItemPrice>();
        }

        public void ScanItem(string itemName, double itemQuantity = 1)
        {
            var itemAlreadyInList = _items.Find(listItem => listItem.Name == itemName);
            if (itemAlreadyInList != null) {
                itemAlreadyInList.Quantity += itemQuantity;
            }
            else {
                var itemPrice = _priceList.Find(p => p.ItemName == itemName);
                if (itemPrice == null)
                {
                    throw new Exception($"No price for {itemName} found!");
                }

                _items.Add(new Doughnut { Name = itemName, Quantity = itemQuantity, Price = itemPrice.Price });
            }
        }

        public void SetPricing()
        {
            _priceList.AddRange(new List<ItemPrice> {
                new ItemPrice {ItemName = "DoughnutA", Price = 1.25 },
                new ItemPrice {ItemName = "DoughnutB", Price = 4.25 },
                new ItemPrice {ItemName = "DoughnutC", Price = 1.00 },
                new ItemPrice {ItemName = "DoughnutD", Price = 0.75 }
            });
        }

        public void SetRebate()
        {
            _rebateProvider = new VolumeRebateProvider();
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in _items)
            {
                var itemSum = item.Quantity * item.Price;

                if (_rebateProvider != null)
                {
                    item.Rebate = _rebateProvider.FindRebate(item);
                    if (item.Rebate != null && item.Rebate.Type == RebateType.Absolute)
                    {
                        itemSum -= item.Rebate.RebateAmount;
                    }
                }

                total += itemSum;
            }
            return total;
        }
    }
}

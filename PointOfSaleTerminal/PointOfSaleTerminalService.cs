using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal.Interfaces;
using PointOfSaleTerminal.Entities;

namespace PointOfSaleTerminal
{
    public class PointOfSaleTerminalService : IPointOfSaleTerminal
    {
        readonly Dictionary<string, Item> _items;
        readonly Dictionary<string, ItemPrice> _priceList;
        readonly IDiscount _discountProvider;

        public PointOfSaleTerminalService()
        {
            _items = new Dictionary<string, Item>();
            _priceList = new Dictionary<string, ItemPrice>();
            _discountProvider = new VolumeDiscountProvider();
        }

        private double GetItemPrice(string itemCode)
        {
            if (!_priceList.ContainsKey(itemCode))
            {
                throw new Exception($"No price for {itemCode} found!");
            }
            return _priceList[itemCode].Price;
        }

        private double GetRebate(Item item)
        {
            return _discountProvider?.FindDiscount(item) ?? 0;
        }

        public void ScanItem(string itemCode)
        {
            if (_items.ContainsKey(itemCode)) {
                var itemAlreadyInList = _items[itemCode];
                itemAlreadyInList.Quantity += 1;
                return;
            }
            _items[itemCode] = new Item { Code = itemCode, Quantity = 1, Price = GetItemPrice(itemCode) };
        }

        public bool IsItemPriceExists(string itemCode)
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
                total += item.Quantity * item.Price - GetRebate(item);
            }
            return total;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PointOfSaleTerminal;

namespace Seller
{
    class Program
    {
        static void Main(string[] args)
        {
            PointOfSaleTerminalService posService = new PointOfSaleTerminalService();
            posService.SetPricing(new Dictionary<string, double>
            {
                {"A", 1.25},
                {"B", 4.25},
                {"C", 1.00},
                {"D", 0.75}
            });
            var waitIteminsert = true;

            while (waitIteminsert)
            {
                Console.WriteLine("Enter item code. Or type 'Calc' to get total.");
                var item = Console.ReadLine();
                if (item != null)
                {
                    if (item == "Calc")
                    {
                        waitIteminsert = false;
                    }
                    if (waitIteminsert)
                    {
                        var isItemPriceExists = posService.IsItemPriceExists(item);
                        if (!isItemPriceExists)
                        {
                            Console.WriteLine($"Item code '{item}' does not exist. It will be created. Enter price for '{item}'");
                            var price = Console.ReadLine();
                            double itemPrice;
                            if (price != null && Double.TryParse(price, out itemPrice))
                            {
                                posService.AddItemWithPrice(item, itemPrice);
                                posService.ScanItem(item);
                                Console.WriteLine($"Item code '{item}' was scanned");
                            }
                            else
                            {
                                Console.WriteLine("No price was specified. New item was not added");
                            }
                        }
                        else
                        {
                            posService.ScanItem(item);
                            Console.WriteLine($"Item code '{item}' was scanned");
                        }
                    }
                }
            }
            //posService.ScanItem("C");
            //posService.ScanItem("C");
            //posService.ScanItem("C");
            //posService.ScanItem("C");
            //posService.ScanItem("C");
            //posService.ScanItem("C");
            //posService.ScanItem("C");

            double total = posService.CalculateTotal();

            Console.WriteLine($"Total {total}");
            Console.ReadLine();
        }
    }
}

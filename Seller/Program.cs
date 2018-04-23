﻿using System;
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
            posService.SetPricing();
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
                        var isItemExists = posService.IsItemExists(item);
                        if (!isItemExists)
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
            //posService.ScanItem("DoughnutC");
            //posService.ScanItem("DoughnutC");
            //posService.ScanItem("DoughnutC");
            //posService.ScanItem("DoughnutC");
            //posService.ScanItem("DoughnutC");
            //posService.ScanItem("DoughnutC");
            //posService.ScanItem("DoughnutC");

            double total = posService.CalculateTotal();

            Console.WriteLine($"Total {total}");
            Console.ReadLine();
        }
    }
}

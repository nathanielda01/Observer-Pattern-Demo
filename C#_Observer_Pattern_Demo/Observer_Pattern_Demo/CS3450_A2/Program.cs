/*
 * Created By: Nathaniel Anderton
 * Purpose: The purpose of this project is to show the observer method in action
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Observer_Pattern_Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Observers
            Selections selections = new Selections();
            ChangeTen changeTen = new ChangeTen();
            Average averagePrice = new Average();

            LocalStocks ls = new LocalStocks();

            // Register Observers
            ls.RegisterObserver(selections);
            ls.RegisterObserver(changeTen);
            ls.RegisterObserver(averagePrice);

            // Read in Report
            ls.ReadIn("Ticker.dat");

            Console.ReadKey();
        }
    }
}

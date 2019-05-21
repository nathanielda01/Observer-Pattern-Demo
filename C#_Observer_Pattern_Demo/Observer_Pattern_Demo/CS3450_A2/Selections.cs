using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Observer_Pattern_Demo
{
    public class Selections : IObserveStocks
    {
        private string filename = "Selections.dat";
        private State report;
        private string[] companies = { "ALL", "BA", "BC", "GBEL",
            "KFT", "MCD", "TR", "WAG" };

        public void Update(State s)
        {
            report = s;
            Display();
            WriteReport();
        }
        
        private void Display()
        {
            Console.WriteLine();
            Console.WriteLine("===================");
            Console.WriteLine(filename + " Update");
            Console.WriteLine("===================");
            Console.WriteLine(report.Name);
            for (int i = 0; i < companies.Length; i++)
            {
                for (int j = 0; j < report.Data.Count(); j++)
                {
                    if (report.Data[j].tickerSymbol.Equals(companies[i]))
                        Console.WriteLine(report.Data[j].ToString());
                }
            }
            
        }

        private void WriteReport()
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filename))
            {
                file.WriteLine(report.Name);
                for (int i = 0; i < companies.Length; i++)
                {
                    for (int j = 0; j < report.Data.Count(); j++)
                    {
                        if (report.Data[j].tickerSymbol.Equals(companies[i]))
                            file.WriteLine(report.Data[j].ToString());
                    }
                }
            }
        }
    }
}

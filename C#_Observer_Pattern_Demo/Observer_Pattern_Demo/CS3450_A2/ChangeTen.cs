using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Pattern_Demo
{
    public class ChangeTen : IObserveStocks
    {
        private string filename = "Change10.dat";
        private State report;
        public void Update(State s)
        {
            report = s;
            Display();
            WriteReport();
        }

        // Symbol, Current Price, % change

        private void Display()
        {
            Console.WriteLine();
            Console.WriteLine("===================");
            Console.WriteLine(filename + " Update");
            Console.WriteLine("===================");
            Console.WriteLine(report.Name);
            for (int i = 0; i < report.Data.Count(); i++)
            {
                if(report.Data[i].percentChange >= 10 || report.Data[i].percentChange <= -10)
                    Console.WriteLine("{0}\t{1:c}\t{2}", report.Data[i].tickerSymbol, report.Data[i].currentPrice, report.Data[i].percentChange);
            }
        }

        private void WriteReport()
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filename))
            {
                file.WriteLine(report.Name);
                for (int i = 0; i < report.Data.Count(); i++)
                {
                    if (report.Data[i].percentChange >= 10 || report.Data[i].percentChange <= -10)
                        file.WriteLine("{0}\t{1:c}\t{2}", report.Data[i].tickerSymbol, report.Data[i].currentPrice, report.Data[i].percentChange);
                }
            }
        }
    }
}

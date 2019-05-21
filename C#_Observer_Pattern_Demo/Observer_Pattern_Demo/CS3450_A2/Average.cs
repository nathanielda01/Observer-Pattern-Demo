using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Pattern_Demo
{
    public class Average : IObserveStocks
    {
        private string filename = "Average.dat";
        private State report;
        private double priceAverage = 0;

        public void Update(State s)
        {
            report = s;
            CalculateAverage();
            Display();
            WriteReport();
        }

        private void CalculateAverage()
        {
            for (int i = 0; i < report.Data.Count(); i++)
            {
                priceAverage += report.Data[i].currentPrice;
            }
            priceAverage = priceAverage / report.Data.Count();
        }

        private void Display()
        {
            Console.WriteLine();
            Console.WriteLine("===================");
            Console.WriteLine(filename + " Update");
            Console.WriteLine("===================");
            Console.WriteLine("{0}\t\t{1:c}", report.Name, priceAverage);
        }

        private void WriteReport()
        {
            using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(filename))
            {
                file.WriteLine("{0}\t{1:c}", report.Name, priceAverage);
            }
        }
    }
}

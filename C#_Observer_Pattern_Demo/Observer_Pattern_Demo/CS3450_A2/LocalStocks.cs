using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Observer_Pattern_Demo
{
    public class StockDataNode
    {
        public string company;
        public string tickerSymbol;
        public double currentPrice;
        public double priceChange;
        public double percentChange;
        public double ytdPercentChange;
        public double high;
        public double low;
        public double peRatio;
        public string updateLabel;

        public override string ToString()
        {
            return string.Format("{0,-25}  {1,5} {2,10:c} {3,10:c} {4,10} {5,15} {6,10:c} {7,10:c} {8,10}",
                company, tickerSymbol, currentPrice, priceChange, percentChange, ytdPercentChange, high, low, peRatio);
        }

        public StockDataNode() { }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="stockDataNode">The node to be copied</param>
        public StockDataNode(StockDataNode stockDataNode)
        {
            company = stockDataNode.company;
            tickerSymbol = stockDataNode.tickerSymbol;
            currentPrice = stockDataNode.currentPrice;
            priceChange = stockDataNode.priceChange;
            percentChange = stockDataNode.percentChange;
            ytdPercentChange = stockDataNode.ytdPercentChange;
            high = stockDataNode.high;
            low = stockDataNode.low;
            peRatio = stockDataNode.peRatio;
        }
    }

    /// <summary>
    /// Saves a state of the report
    /// </summary>
    public class State
    {
        private string name;
        private List<StockDataNode> data = new List<StockDataNode>();

        public State(string name)
        {
            this.name = name;
        }

        public string Name { get => name; set => name = value; }
        public List<StockDataNode> Data { get => data; set => data = value; }
    }

    /// <summary>
    /// LocalStocks object reads in a stock report and organizes it in a way to make it more readable for distribution
    /// </summary>
    public class LocalStocks : ILocalStocks
    {
        private string updateTag;
        private List<State> states = new List<State>();
        private List<StockDataNode> currentStocks = new List<StockDataNode>();
        private List<IObserveStocks> observers = new List<IObserveStocks>();

        public LocalStocks()
        { }

        public LocalStocks(string path)
        {
            ReadIn(path);
        }

        /// <summary>
        /// Reads in a file to create a stock report
        /// </summary>
        /// <param name="path">Location of the stock report</param>
        public void ReadIn(string path)
        {
            string line;
  
            StreamReader file = new StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("Last updated"))
                {
                    if (line.Contains('\r') || line.Contains('\n'))
                    {
                        line.Trim('\n', '\r');
                    }
                    updateTag = line;
                    states.Add(new State(updateTag));
                    if (currentStocks.Count() != 0)
                    {
                        for (int i = 0; i < currentStocks.Count(); i++)
                        {
                            states[states.Count() - 2].Data.Add(new StockDataNode(currentStocks[i]));
                            states[states.Count() - 2].Data[i].updateLabel = updateTag;
                        }
                        NotifyObservers(states.Count() - 2); // This notifies observers that an update is available
                        currentStocks.Clear();
                    }
                }
                else if (line.Equals("")) { }
                else
                {
                    line.Trim('\n', '\r');
                    StockDataNode newNode = CreateNode(line);
                    newNode.updateLabel = updateTag;
                    currentStocks.Add(newNode);
                }
            }
        }

        /// <summary>
        /// Creates a Stock node based off of data from the report
        /// </summary>
        /// <param name="line">Part of the report used to populate the node</param>
        /// <returns></returns>
        private StockDataNode CreateNode(string line)
        {
            List<string> lineSegments = line.Split(' ').ToList();
            for (int i = 0; i < lineSegments.Count(); i++)
            {
                if (lineSegments[i].Equals("")) { lineSegments.RemoveAt(i); }
            }
            StockDataNode sdn = new StockDataNode();
            int symbolIndex = 0;
            for (int i = 0; i < lineSegments.Count(); i++)
            {
                symbolIndex++;
                if (double.TryParse(lineSegments[i], out sdn.currentPrice))
                {
                    symbolIndex--;
                    break;
                }
            }
            for (int i = 0; i < symbolIndex - 1; i++)
            {
                sdn.company += (lineSegments[i] + " ");
            }
            symbolIndex--;
            sdn.tickerSymbol = lineSegments[symbolIndex++];
            symbolIndex++;
            
            double.TryParse(lineSegments[symbolIndex++], out sdn.priceChange);
            double.TryParse(lineSegments[symbolIndex++], out sdn.percentChange);
            double.TryParse(lineSegments[symbolIndex++], out sdn.ytdPercentChange);
            double.TryParse(lineSegments[symbolIndex++], out sdn.high);
            double.TryParse(lineSegments[symbolIndex++], out sdn.low);
            double.TryParse(lineSegments[symbolIndex++], out sdn.peRatio);

            return sdn;
        }

        // Observer Hadler methods
        public void RegisterObserver(IObserveStocks o)
        {
            observers.Add(o);
        }
        public void RemoveObserver(IObserveStocks o)
        {
            observers.Remove(o);
        }
        public void NotifyObservers(int reportNumber)
        {
            for (int i = 0; i < observers.Count(); i++)
            {
                observers[i].Update(states[reportNumber]);
            }
        }
    }
}

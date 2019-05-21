using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Pattern_Demo
{
    public interface ILocalStocks
    {
        void RegisterObserver(IObserveStocks o);
        void RemoveObserver(IObserveStocks o);
        void NotifyObservers(int stateNumber);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer_Pattern_Demo
{
    public interface IObserveStocks
    {

        void Update(State s);
    }
}

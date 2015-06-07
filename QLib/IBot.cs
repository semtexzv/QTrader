using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public interface IBot
    {
        string Name { get; }
        IBot New(Chromosome c);
        void OnCandle(IExchange ex,Candle c);
        void EnableTrading();
        void DisableTrading();
        int GetDesiredInterval();
        Chromosome StandardChromosome();
    }
}

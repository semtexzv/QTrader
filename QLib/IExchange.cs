using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLib
{
    public abstract class IExchange 
    {
        public abstract int Interval
        {
            get;
            set;
        }
           
        public IExchange()
        {
        }
        public abstract bool Ready
        {
            get;
        }
        public abstract string Name
        {
            get;
        }
        public abstract UserControl UI
        {
            get;
        }
        public abstract List<Candle> History
        {
            get;
        }
        public abstract ISimulatedExchange SimulatedExchange
        {
            get;
        }
        public abstract float USD
        {
            get;
        }
        public abstract float BTC
        {
            get;
        }
         public abstract bool Buy(float amount, float price);
         public abstract bool Sell(float amount, float price);
         public event Action<Candle> OnCandle;
        
    }
    public abstract class ISimulatedExchange : IExchange
    {
        public abstract ISimulatedExchange New();
    }
}

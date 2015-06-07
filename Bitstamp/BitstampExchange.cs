using QLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    class BitstampExchange:IExchange
    {
        public override bool Ready
        {
            get { return Bitstamp.Ready; }
        }

        public override string Name
        {
            get { return "Bitstamp"; }
        }

        public override System.Windows.Forms.UserControl UI
        {
            get { return new  UserControl1(); }
        }

        public override List<Candle> History
        {
            get { return Bitstamp.candles[Intervals.GetInterval(Interval)]; }
        }

        public override ISimulatedExchange SimulatedExchange
        {
            get { return new BitstampSimulatedExchange(); }
        }

        public override float USD
        {
            get { return 0; }
        }

        public override float BTC
        {
            get { return 0; }
        }

        public override bool Buy(float amount, float price)
        {
           //todo
            return false;
        }

        public override bool Sell(float amount, float price)
        {
            //todo
            return false;
        }
        public override int Interval
        {
            get;
            set;
        }
    }
}

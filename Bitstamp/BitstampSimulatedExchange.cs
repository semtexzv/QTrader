using QLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    class BitstampSimulatedExchange: ISimulatedExchange
    {

        public BitstampSimulatedExchange()
        {

        }
        public BitstampSimulatedExchange(int _interval)
        {
            this.Interval = _interval;
        }

        private float USD_ = 100f;
        private float BTC_;
        private float FeePercent = 0.25f / 100;



        public override bool Buy(float amount, float price)
        {
            if (USD_ >= amount * price)
            {
                USD_ -= amount * price;
                BTC_ += amount - amount * FeePercent;
                return false;
            }
            return true;
        }
        public override bool Sell(float amount, float price)
        {
            if (BTC_ >= amount)
            {
                USD_ += (amount - amount * FeePercent) * price;
                BTC_ -= amount;
                return true;
            }
            return false;
        }
        public override bool Ready
        {
            get { return Bitstamp.Ready; }
        }
        public override string Name
        {
            get { return "Bitstamp-simulated"; }
        }
        public override System.Windows.Forms.UserControl UI
        {
            get { return new UserControl1(); }
        }
        public override List<Candle> History
        {
            get { return Bitstamp.candles[Intervals.GetInterval(Interval)]; }
        }
        public override ISimulatedExchange SimulatedExchange
        {
            get { return this; }
        }
        public override float USD
        {
            get { return USD_; }
        }
        public override float BTC
        {
            get { return BTC_; }
        }
        public override ISimulatedExchange New()
        {
            return new BitstampSimulatedExchange();
        }
        public override int Interval
        {
            get;
            set;
        }
    }

}

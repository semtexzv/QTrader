using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public class ExponentialMovingAverage
    {
        public Dictionary<int, float> values = new Dictionary<int, float>();
        
        public int Interval;
        public float alpha;
        public ExponentialMovingAverage(int Interval)
        {
            this.Interval = Interval;
            this.Interval = Interval == 0 ? 1 : Interval;
            this.alpha = 2f / (Interval + 1);
        }
        public void Kickstart(IEnumerable<Candle> candles)
        {
            foreach (Candle candle in candles)
            {
                AddValue(candle);
            }
        }
        public float AddValue(Candle candle)
        {
            return AddValue(candle.Timestamp, candle.Close);
        }
        public float AddValue(int timeStamp, float closePrice)
        {
            if (values.Count == 0)
            {
                values.Add(timeStamp, closePrice);
                return closePrice;
            }
            else
            {
                float val = alpha * closePrice + (1 - alpha) * values.Values.Last();
                values.Add(timeStamp,val);
                return val;
            }
        }

        public void Reset()
        {
            values.Clear();
        }
        public void Reset(int interval)
        {
            this.Interval = interval;
            Reset();
        }
    }
}

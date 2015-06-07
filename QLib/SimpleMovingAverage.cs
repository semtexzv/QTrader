using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public class SimpleMovingAverage
    {  public Dictionary<int, float> values = new Dictionary<int,float>();
        private Queue<float> lastValues = new Queue<float>();
        private int _interval =  1;
        public int Interval
        {
            get
            {
                if(_interval==0)
                {
                    return 1;
                }
                return _interval;
            }
            set
            {
                _interval = value;
            }
        }
        public SimpleMovingAverage(int _inter)
        {
            if (_inter == 0)
            {
                Interval = 1;
            }
            else
            {
                this.Interval = _inter;
            }
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
            lastValues.Enqueue(closePrice);
            if(lastValues.Count>Interval)
            {
                lastValues.Dequeue();
            }
            float avg = lastValues.Average();
            values.Add(timeStamp, avg);
            return avg;
        }

        public void Reset()
        {
            values.Clear();
            lastValues.Clear();
        }
    }
}

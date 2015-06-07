using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public class Candle
    {
        public float High { get; internal set; }
        public float Low { get; internal set; }
        public float Open { get; internal set; }
        public float Close { get; internal set; }
        public int Timestamp { get; internal set; }

        public float Volume { get; internal set; }
        public float Diff
        {
            get
            {
                return Close - Open;
            }
        }
        public bool Rising
        {
            get
            {
                return Diff > 0;
            }
        }
        public static Candle Create(IEnumerable<ITrade> trades)
        {
            trades = trades.OrderBy(a => a.Timestamp());
            return new Candle()
            {
                Open = trades.First().Price(),
                Close = trades.Last().Price(),
                Timestamp = trades.Last().Timestamp(),
                High = trades.Max(a => a.Price()),
                Low = trades.Min(a => a.Price()),
                Volume = trades.Sum(a => a.Amount()),
            };
        }
                    
    }
}

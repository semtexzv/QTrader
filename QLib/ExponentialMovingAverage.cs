#region License
/*
Copyright (c) 2015 semtexzv@gmail.com
This software is provided 'as-is', without any express or implied warranty. 
In no event will the authors be held liable for any damages arising from the use of this software.
Permission is granted to anyone to use this software for any purpose, including commercial applications, and to alter it and redistribute it freely, subject to the following restrictions:
    1. The origin of this software must not be misrepresented; you must not claim that you wrote the original software.
		If you use this software in a product, an acknowledgment in the product documentation would be appreciated but is not required.
    2. Altered source versions must be plainly marked as such, and must not be misrepresented as being the original software.
    3. This notice may not be removed or altered from any source distribution.
*/
#endregion
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

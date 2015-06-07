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

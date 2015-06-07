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

﻿#region License
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

        public override List<Candle> History()
        {
            // We are accessing candle data from 2 different threads, modifications ae sparse,
            // but collisions can occur
            lock(Bitstamp.candles)
            {
                return Bitstamp.candles[Intervals.GetInterval(Interval)];
            }
        }

        public override IExchange SimulatedExchange
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

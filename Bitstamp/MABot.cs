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
using QLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    class MABot : IBot
    {
        private int interval;
        private SimpleMovingAverage SMA1;
        private SimpleMovingAverage SMA2;
        public MABot()
        {
            SMA1 = new SimpleMovingAverage(1);
            SMA2 = new SimpleMovingAverage(1);
        }
      
        public IBot New( Chromosome c)
        {
            IntArrayChromosome chrom = (IntArrayChromosome)c;

            chrom.genes[0] = chrom.genes[0] == 0 ? 1 : Intervals.Truncate(chrom.genes[0]);
            chrom.genes[1] = chrom.genes[1] % 300;
            chrom.genes[2] = chrom.genes[2] % 300;
            MABot bot = new MABot();
            bot.SMA2.Interval = chrom.genes[1];
            bot.SMA2.Interval = chrom.genes[2];
            bot.interval = Intervals.Truncate(chrom.genes[0]);
            //apply chromosome;
            return bot;
        }

        public void OnCandle(IExchange ex, Candle c)
        {
            float s1 = SMA1.AddValue(c);
            float s2 = SMA2.AddValue(c);
            if (ShouldTrade)
            {
                if (s1 > s2)
                {
                    if (ex.Sell(ex.BTC, c.Close))
                    {
                    }
                }
                else
                {
                    if (ex.Buy(ex.USD / c.Close * 0.98f, c.Close))
                    {
                    }
                }
            }
        }

        public int GetDesiredInterval()
        {
            return interval;
        }

        public Chromosome StandardChromosome()
        {
            return new IntArrayChromosome(5);
        }


        public string Name
        {
            get { return "Simple"; }
        }




        private bool ShouldTrade;
        public void EnableTrading()
        {
            ShouldTrade = true;
        }

        public void DisableTrading()
        {
            ShouldTrade = false;
        }

    }
}

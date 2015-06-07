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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QLib
{

    internal static class BotTester
    {
        public static event EventHandler OnNewBestChromosome;
        private static  Thread thread;

        public static EventWaitHandle reset;

        public static Population population;
        private static int _startTimestamp;
        public static DateTime StartDate
        {
            get
            {
                return UnixTime.ConvertToDateTime(_startTimestamp);
            }
            set
            {
                _startTimestamp = UnixTime.GetFromDateTime(value);
            }
        }

        public static int Generation;
        public static event EventHandler OnGeneration;
               
        public static bool Running()
        {
            return reset.WaitOne(0);
        }
        static BotTester()
        {
            if (QTrader.Bot != null)
            {
                population = new Population(20, QTrader.Bot.StandardChromosome(), Fitness);
            }

            QTrader.OnBotChanged += QTrader_OnBotChanged;
            QTrader.OnExchangeChanged +=QTrader_OnExchangeChanged;

            reset = new EventWaitHandle(false, EventResetMode.ManualReset);
            reset.InitializeLifetimeService();
            thread = new Thread(Run);
            thread.Start();
        }

        static void QTrader_OnBotChanged()
        {
            TestedBot = QTrader.Bot;
            if (population == null)
            {
                population = new Population(20, QTrader.Config, Fitness);
            }
        }
        private static ISimulatedExchange Exchange;
        private static IBot TestedBot;
        static void QTrader_OnExchangeChanged()
        {
            bool running = Running();
            Exchange = QTrader.SimulatedExchange;
            Stop();
            if (running)
                Start();
        }

        public static string BestBot()
        {
            return JsonConvert.SerializeObject(population.Best, population.Best.GetType(), null);
        }
        public static Chromosome Best()
        {
            return population.Best;
        }
        public static void Start()
        {
            reset.Set();
        }
        public static void Stop()
        {
            reset.Reset();
        }
        static int i = 0;
        private static Chromosome oldBest;
        public static void Run()
        {
            while (true)
            {
                
                reset.WaitOne();
                reset.Set();
                if(population != null)
                    population.RunEpoch();
                if(oldBest == null)
                {
                    oldBest = population.Best;
                    if (OnNewBestChromosome != null)
                    {
                        OnNewBestChromosome(null, null);
                    }
                }
                if(oldBest != null && population.Best.Fitness > oldBest.Fitness)
                {
                    oldBest = population.Best;
                    if(OnNewBestChromosome != null)
                    {
                        OnNewBestChromosome(null, null);
                    }
                }
                Generation++;
                if (OnGeneration != null)
                    OnGeneration(null, null);
            }
        }
        public static void Exit()
        {
            thread.Abort();
        }
        public static double Fitness(Chromosome c)
        {
            if (Exchange.Ready)
            {

                IBot bot = TestedBot.New(c);
                ISimulatedExchange ex = Exchange.New();
                ex.Interval = (bot.GetDesiredInterval());
                IEnumerable<Candle> candles = ex.History;
                bot.DisableTrading();
                foreach (Candle cc in candles) 
                {
                    if (cc.Timestamp > _startTimestamp)
                    {
                        bot.EnableTrading();
                    }
                        bot.OnCandle(ex,cc);
                    
                }
                return ex.USD + ex.BTC * candles.Last().Close;
            }
            return 0;
            
        }

    }
}

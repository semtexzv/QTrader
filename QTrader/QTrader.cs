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
   
    internal static class QTrader
    {
        public static Settings Settings = new Settings("Qtrader.settings");
        internal delegate void ChangeDelegate();

        internal static event ChangeDelegate OnExchangeChanged;
        internal static event ChangeDelegate OnBotChanged;
        
        private static IExchange _exchange;
        internal static IExchange Exchange
        {

            get
            {
                return _exchange;
            }
            set
            {
                if (Running)
                {
                    Logger.Log("Please stop before changing config");
                    return;
                }
                else
                {
                    _exchange = value;
                    if (OnExchangeChanged != null)
                        OnExchangeChanged();
                }
            }
        }

        internal static IExchange SimulatedExchange
        {
            get
            {
                if (Exchange == null)
                    return null;
                return Exchange.SimulatedExchange;
            }
            
        }
        private static bool _running = false;
        public static bool Running
        {
            get
            {
                return _running;
            }
        }
        private static Chromosome _config;
        public static Chromosome Config
        {
            get
            {
                return _config;
            }
            set
            {
                if (Running)
                {
                    Logger.Log("Please stop before changing config");
                    return;
                }
                else
                    _config = value;
            }
        }
        private static IBot _bot;
        internal static IBot Bot
        {
            get
            {
                return _bot;
            }
            set
            {
                if (Running)
                {
                    Logger.Log("Please stop before changing config");
                    return;
                }
                else
                {
                    Chromosome c;
                    if (_bot != null)
                    {
                        c = _bot.StandardChromosome();
                        QTrader.Settings["Config_" + QTrader.Bot.Name] = JsonConvert.SerializeObject(c);
                    }
                    
                    _bot = value;
                    c = _bot.StandardChromosome();
                    string json = QTrader.Settings["Config_" + QTrader.Bot.Name];
                    if (json != "")
                    {
                        try
                        {
                            Config = (Chromosome)JsonConvert.DeserializeObject(json, c.GetType());
                        }
                        catch
                        {
                            Config = c;
                        }
                    }
                    else
                    {
                        Config = c;
                    }

                    if (OnBotChanged != null)
                        OnBotChanged();
                }
            }
        }
        private static IBot _instance;
        public static void Start()
        {
            if (!Exchange.Ready)
                Logger.Log("Exchange not ready, bot will start shortly");
            new Action(() =>
            {
                while (!Exchange.Ready)
                    Thread.Sleep(50);
                if (Config == null)
                    Config = Bot.StandardChromosome();
                _instance = Bot.New(Config);
                _instance.DisableTrading();
                Exchange.Interval = _instance.GetDesiredInterval();
                List<Candle> hist = Exchange.History();
                foreach (Candle t in hist)
                {
                    _instance.OnCandle(Exchange, t);
                }
                
                _instance.EnableTrading();
                _running = true;
                Exchange.OnCandle += Exchange_OnCandle;
                Logger.Log("Bot Started");
            }).BeginInvoke(null, null);
          
        }
        public static void Stop()
        {
            Exchange.OnCandle -= Exchange_OnCandle;
            _instance = null;
            _running = false;
            Logger.Log("Bot Stopped");
        }
        static void Exchange_OnCandle(Candle c)
        {
            Logger.Log("Trader - Candle Arrived");
            if(_instance != null)
                _instance.OnCandle(Exchange, c);
        }
    }
}

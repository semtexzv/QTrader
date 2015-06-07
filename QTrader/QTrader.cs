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

        internal static ISimulatedExchange SimulatedExchange
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
                lock (Exchange.History)
                {
                    foreach (Candle t in Exchange.History)
                    {
                        _instance.OnCandle(Exchange, t);
                    }
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

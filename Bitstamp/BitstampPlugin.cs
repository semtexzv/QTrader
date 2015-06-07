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
using QLib;
using System.IO;
using Newtonsoft.Json;
using System.Security.AccessControl;
using System.Threading;

namespace Bitstamp{
    public static partial class Bitstamp
    {
        public static Action<int,Candle> OnCandle;
        public static Settings Settings = new Settings("bitstamp.settings");
        
        public static List<Trade> Trades = new List<Trade>();
        public static  Dictionary<int, List<Candle>> candles = new Dictionary<int, List<Candle>>();
        private static Dictionary<int, List<Trade>> partialCandles = new Dictionary<int, List<Trade>>();
        public static bool Ready = false;

        public static string Filename
        {
            get
            {
                return Settings["Filename"];
            }
            set
            {
                Settings["Filename"] = value;
               if(ReaderStream!= null)
               {
                   ReaderStream.Close();
                   ReaderStream = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                    }
                
            }
        }
        private static Action History = new Action(()=>GetHistory());
        private static CancellationTokenSource cts = new CancellationTokenSource();
        private static FileStream ReaderStream;
        static Bitstamp()
        {
            if(Filename == "")
            {
                Filename = "bitstamp.json";
            }
            if(!File.Exists(Filename))
            {
                File.Create(Filename).Close();

            }
            ReaderStream = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
            
            History.BeginInvoke(null, null);
            foreach (int i in Intervals.Values)
            {
                candles.Add(i, new List<Candle>());
                partialCandles.Add(i, new List<Trade>());
            }
        }


        public static void Reset()
        {
            cts.Cancel();
            Trades.Clear();
            foreach(int i in Intervals.Values)
            {
                candles[i].Clear();
                partialCandles[i].Clear();
            }

        }
        private static void onTrade(Trade trade)
        {
            /*using(StreamWriter writer = new StreamWriter(WriterStream,Encoding.UTF8,4096,true))
            {
                string data = JsonConvert.SerializeObject(trade);
                writer.WriteLine(data);
                writer.Flush();
            }*/
            AddTrade(trade);
        }
        private static void AddTrade(Trade trade)
        {
            Trades.Add((Trade)trade);
            foreach (int i in Intervals.Values)
            {
                if (partialCandles[i].Count != 0 && trade.Timestamp > partialCandles[i].First().Timestamp + i)
                {
                    var candle = Candle.Create(partialCandles[i]);
                    candles[i].Add(candle);
                    if (OnCandle != null)
                        OnCandle(i, candle);
                    partialCandles[i].Clear();
                }
                else
                {
                    partialCandles[i].Add(trade);
                }
            }
        }
        private static void GetHistory()
        {
            Logger.Log("Bitstamp - Reading history, please wait");
            Ready = false;
            using (StreamReader reader = new StreamReader(ReaderStream,Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    if (cts.IsCancellationRequested)
                    {
                        Logger.Log("Bitstamp - Cancelled loading history");
                        cts = new CancellationTokenSource();
                        return;
                    }
                    string line = reader.ReadLine();
                    try
                    {
                        if (line != "" && line != "\n" && line[0] != '/' && line[1] != '/')
                        {
                            Trade t = JsonConvert.DeserializeObject<Trade>(line);
                            if (t != null)
                                AddTrade(t);
                        }
                    }
                    catch
                    {

                    }
                }
            }
            Logger.Log("Bitstamp - {0} trades loaded", Trades.Count);
            Ready = true;
            
        }
      
    }
    class BitstampPlugin : IPlugin
    {

        public BitstampPlugin()
        {
        }
        public string Name
        {
            get { return "Bitstamp"; }
        }

        public IExchange[] Exchanges
        {
            get { return new IExchange[]{ new BitstampExchange()}; }
        }

        public QLib.IBot[] Bots
        {
            get { return new IBot[] { new MABot() }; }
        }




        System.Windows.Forms.UserControl IPlugin.Interface
        {
            get { return new UserControl1(); }
        }
    }
}

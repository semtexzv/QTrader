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
using PusherClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Specialized;
using System.Threading;
using Newtonsoft.Json.Linq;
using QLib;

namespace Bitstamp
{
    public partial class API
    {
        private Pusher pusher;
        public Action OnConnected;
        public Action<Trade> OnTrade;
        public bool Conntected
        {
            get
            {
                return pusher.State == ConnectionState.Connected;
            }
        }       
        int tradeCounter = 0;
        private Timer watchDog;
        public API()
        {
           
            watchDog = new Timer(a =>
            {
                if (tradeCounter < 10)
                {
                    pusher.Disconnect();
                    pusher.Connect();
                }
                else
                {
                    tradeCounter = 0;
                }
            }, null,3600*1000, 7200*1000);
            pusher = new Pusher("de504dc5763aeef9ff52");
            pusher.Connect();
            pusher.ConnectionStateChanged +=
                new ConnectionStateChangedEventHandler((a, target) =>
                    {
                        Logger.Log("API - {0}", target.ToString());
                        switch (target)
                        {
                            case ConnectionState.Connected:
                                if (OnConnected != null) OnConnected();
                                pusher.Subscribe("live_trades");
                                pusher.Subscribe("diff_order_book");
                                break;
                       
                        }
                    });
            pusher.Bind("trade", a =>
                {
                    tradeCounter++;
                    Trade t = a.ToObject<Trade>();
                    t.Timestamp = UnixTime.Now;
                    if (OnTrade != null)
                        OnTrade(t);
                });

        }
    }
}

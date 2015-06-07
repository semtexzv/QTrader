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
using Newtonsoft.Json.Linq;
using QLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    public partial class API
    {
        HttpClient client = new HttpClient();
        string exchangeURL = @"https://www.bitstamp.net/api/";
        
        private HMACSHA256 hmac;
        public string Key { get; set; }
        private string _secret;
        public string Secret
        {
            get { return _secret; }
            set
            {
                _secret = value;
                if (hmac != null)
                {
                    hmac.Key = Encoding.ASCII.GetBytes(value);
                }
            }
        }
        public string ID { get; set; }

        private int nonce;

        public  API(string key = "", string secret = "", string id = ""):this()
        {
            this.Key = key;
            this._secret = secret;
            this.ID = id;
            hmac = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
            nonce = UnixTime.Now;
        }

        public string PublicQuery(string method)
        {
           return client.GetStringAsync(exchangeURL + method+"/").Result;
        }

        public string Query(string method, Dictionary<string, string> args = null)
         {
             nonce++;
             if (args == null)
                 args = new Dictionary<string, string>();
             args.Add("key", Key);
             args.Add("signature", BuildSignature());
             args.Add("nonce", "" + nonce);

             var data = Encoding.ASCII.GetBytes(BuildPostData(args));
             var content = new FormUrlEncodedContent(args);
             return client.PostAsync(exchangeURL + method+"/", content).Result.Content.ReadAsStringAsync().Result;
         }

         private string BuildSignature()
         {
             byte[] hash = hmac.ComputeHash(Encoding.ASCII.GetBytes(nonce + ID + Key));
             return BitConverter.ToString(hash).ToUpper().Replace("-", "");
         }
         private string BuildPostData(Dictionary<string, string> d)
         {
             StringBuilder s = new StringBuilder();
             foreach (var item in d)
             {
                 s.AppendFormat("{0}={1}&", item.Key, Uri.EscapeDataString(item.Value));
             }
             if (s.Length > 0) s.Remove(s.Length - 1, 1);
             return s.ToString();
         }

         public Ticker GetTicker()
         {
             JObject res = JObject.Parse(PublicQuery("ticker"));
             if (res.Property("error") != null)
             {
                 Logger.Log(res["error"].ToString());
                 return null;
             }
             return res.ToObject<Ticker>();
         }

         public UserBalance GetBalance()
         {
             JObject res = JObject.Parse(PublicQuery("ticker"));
             if (res.Property("error") != null)
             {
                 Logger.Log(res["error"].ToString());
                 return null;
             }
             return res.ToObject<UserBalance>();
         }

         public List<Transaction> GetTransactions(int offset = 0, int limit = 100, bool desc = true)
         {
             var args = new Dictionary<string, string>();
             args.Add("offset", offset.ToString());
             args.Add("limit", limit.ToString());
             args.Add("sort", desc ? "desc" : "asc");
             string res = Query("user_transactions", args);
             JArray o = JArray.Parse(res);
             var abc = o.Select(x => x.ToObject<Transaction>()).ToList();
             return abc;
         }
         public List<Order> GetOpenOrders()
         {
             string res = Query("open_orders");
             JArray o = JArray.Parse(res);
             return o.Select(x => x.ToObject<Order>()).ToList();
         }

         public bool CancelOrder(int id)
         {
             var args = new Dictionary<string, string>();
             args.Add("id", id.ToString());
             string res = Query("cancel_order", args);
             return res == "true";

         }
         public bool CancelAllOrders()
         {
             string res = Query("cancel_all_order");
             return res == "true";
         }

         public Order BuyOrder(float amount, float price, float limitPrice)
         {
             var args = new Dictionary<string, string>();
             args.Add("amount", amount.ToString());
             args.Add("price", price.ToString());
             args.Add("limit_price", limitPrice.ToString());
             string res = Query("buy", args);
             var a = JObject.Parse(res);
             if (a.Property("error") != null)
             {
                 Logger.Log(a["error"].ToString());
                 return null;
             }
             return a.ToObject<Order>();
         }

         public Order SellOrder(float amount, float price, float limitPrice)
         {
             var args = new Dictionary<string, string>();
             args.Add("amount", amount.ToString());
             args.Add("price", price.ToString());
             args.Add("limit_price", limitPrice.ToString());
             string res = Query("sell", args);
             var a = JObject.Parse(res);
             if (a.Property("error") != null)
             {
                 Logger.Log(a["error"].ToString());
                 return null;
             }
             return a.ToObject<Order>();
         }
    }
}

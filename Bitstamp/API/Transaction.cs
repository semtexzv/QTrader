using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    public class Transaction
    {
        [JsonProperty("datetime")]
        public DateTime Date;
        [JsonProperty("id")]
        public int ID;
        [JsonProperty("type")]
        public int TypeNum;
        [JsonIgnore]
        TransactionType Type
        {
            get
            {
                return (TransactionType)TypeNum;
            }
        }
        [JsonProperty("usd")]
        public float USD;
        [JsonProperty("btc")]
        public float BTC;
        [JsonProperty("fee")]
        public float Fee;
        [JsonProperty("order_id")]
        public int? Order_ID;
    }
}

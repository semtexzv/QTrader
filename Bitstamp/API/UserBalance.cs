using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    public class UserBalance
    {
        [JsonProperty("usd_balance")]
        public float USD;
        [JsonProperty("btc_balance")]
        public float BTC;
        [JsonProperty("usd_reserved")]
        public float USDReserved;
        [JsonProperty("btc_balanced")]
        public float BTCReserved;
        [JsonProperty("usd_available")]
        public float USDAvailable;
        [JsonProperty("btc_available")]
        public float BTCAvailable;
        [JsonProperty("fee")]
        public float Fee;
        [JsonIgnore]
        public float FeePercent
        {
            get
            {
                return Fee / 100;
            }
        }
    }
}

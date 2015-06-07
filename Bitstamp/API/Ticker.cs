using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    public class Ticker
    {
        [JsonProperty("last")]
        public float LastPrice;
        [JsonProperty("high")]
        public float High;
        [JsonProperty("low")]
        public float Low;
        [JsonProperty("vwap")]
        public float VWap;
        [JsonProperty("volume")]
        public float Volume;
        [JsonProperty("bid")]
        public float HighestBuy;
        [JsonProperty("ask")]
        public float LowestSell;
    }
}

using Newtonsoft.Json;
using QLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
    public class Trade : ITrade
    {
        [JsonProperty(PropertyName = "id")]
        public int ID;
        [JsonProperty(PropertyName = "amount")]
        public float Amount;
        [JsonProperty(PropertyName = "price")]
        public float Price;
        [JsonProperty(PropertyName = "timestamp")]
        public int Timestamp;

        int ITrade.Timestamp()
        {
            return Timestamp;
        }

        float ITrade.Amount()
        {
            return Amount;
        }

        float ITrade.Price()
        {
            return Price;
        }
    }
}

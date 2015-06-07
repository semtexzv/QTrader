using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitstamp
{
   public class Order
    {
        [JsonProperty("id")]
        public int ID;
        [JsonProperty("datetime")]
        public DateTime Date;
        [JsonProperty("type")]
        public int TypeNum;
        [JsonProperty("price")]
        public float Price;
        [JsonProperty("amount")]
        public float Amount;
        [JsonProperty("error")]
        public string[] Error;
        [JsonIgnore]
        public bool IsSell
        {
            get
            {
                return TypeNum == 1;
            }
        }
        [JsonIgnore]
        public bool IsBuy
        {
            get
            {
                return TypeNum == 0;
            }
        }
    }
}

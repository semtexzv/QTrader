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

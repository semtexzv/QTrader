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
using System.Windows.Forms;

namespace QLib
{
    /// <summary>
    /// Used to create interfaces to new exchanges
    /// </summary>
    public abstract class IExchange 
    {

        /// <summary>
        /// Time between candles requested by bot, this property will be modified directly, 
        /// if you need to react to interval changes, implement those changes in setter
        /// </summary>
        public abstract int Interval
        {
            get;
            set;
        }
        /// <summary>
        /// Used to check if exchange is ready for trading , 
        /// while this is false, bot and simulation will be temoparily stopped
        /// </summary>
        public abstract bool Ready
        {
            get;
        }
        /// <summary>
        /// Name displayed to the user, also used to store specific settings for this exchange
        /// </summary>
        public abstract string Name
        {
            get;
        }
        /// <summary>
        /// User control displayed to the user, recommended dimensions are 340x240 px,
        /// everything bigger wil have parts of t he UI cut off
        /// </summary>
        public abstract UserControl UI
        {
            get;
        }
        /// <summary>
        /// Returns history of candles for this exchange, using <see cref="IExchange.Interval"/> to set interval between the candles
        /// This method will be called once bot starts and every time bot is tested.
        /// </summary>
        /// <returns>
        /// List of candles separated by <see cref="IExchange.Interval"/>, amount of candles returned is up to the developer,
        /// however it is recomended to return at least several months of data.
        /// </returns>
        public abstract List<Candle> History();
        /// <summary>
        /// Returns Exchange used for simulation, 
        /// If this is simulation exchange, return new instance of this simulation exchange, will be used to duplicate exchanges for testing
        /// If null, bot assumes there is no exchange for simulation and all bot preferences are set using its <see cref="IExchange.UI"/>
        /// </summary>
        public abstract IExchange SimulatedExchange
        {
            get;
        }
        /// <summary>
        /// USd amount available
        /// </summary>
        public abstract float USD
        {
            get;
        }
        /// <summary>
        /// BTC Amount available, 
        /// </summary>
        public abstract float BTC
        {
            get;
        }
        /// <summary>
        /// Used for posting Buy requests, price is only orientational,
        /// if price differs too much from current market price , feel free to modify it.
        /// </summary>
        /// <param name="amount">
        /// Amount in BTC to buy
        /// </param>
        /// <param name="price">
        /// Price at which to buy
        /// </param>
        /// <returns>
        /// Request result
        /// </returns>
         public abstract bool Buy(float amount, float price);
        /// <summary>
        /// Used for posting Sell request
        /// </summary>
        /// <param name="amount">
        /// Amount in BTC to sell,exchange is responsible for checking funds
        /// </param>
        /// <param name="price">
        /// Price at which to sell, can be modified
        /// </param>
        /// <returns>
        /// Request result, false if user has less BTC than amount
        /// </returns>
         public abstract bool Sell(float amount, float price);
        /// <summary>
        /// Bots will subscribe to this event , and will only trade once event has been recieved
        /// </summary>
         public event Action<Candle> OnCandle;
        
    }
}

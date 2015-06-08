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

namespace QLib
{
    /// <summary>
    /// Used for implementing trading strategies
    /// Bots will be used for simulated and real trading 
    /// </summary>
    public interface IBot
    {
        /// <summary>
        /// Displayed for user, Used to save current and best configurations
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Generate new bot
        /// </summary>
        /// <param name="c">
        /// Chromosome used to store data for generation, this chromosome will usualy be result of artificial selection for best profit using evolutionary algorithms
        /// </param>
        /// <returns>
        /// New bot based on chromosome c
        /// </returns>
        IBot New(Chromosome c);
        /// <summary>
        /// Used when new candle arrives
        /// </summary>
        /// <param name="ex">
        /// Exchange that send new candle, contains methods to buy and sell
        /// </param>
        /// <param name="c">
        /// Actual candle data
        /// </param>
        void OnCandle(IExchange ex,Candle c);
        /// <summary>
        /// Enables trading,used after history data has been passed to bot
        /// </summary>
        void EnableTrading();
        /// <summary>
        /// Disables trading, once trading is disabled, bot will recieve historical trade data
        /// </summary>
        void DisableTrading();
        /// <summary>
        /// Interval between candle
        /// </summary>
        /// <returns>
        /// Index to <see cref="QLib.Intervals.Values"/>, bot cannot use other interval than the ones specified there
        /// </returns>
        int GetDesiredInterval();
        /// <summary>
        /// Random chromosome used as basis to create new bots, and basis for simulation
        /// </summary>
        /// <returns></returns>
        Chromosome StandardChromosome();
    }
}

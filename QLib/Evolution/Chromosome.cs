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

namespace QLib
{
    /// <summary>
    /// Used for evolutionary programming, Will be serialized and deserialized using Json.net
    /// </summary>
    public abstract class Chromosome : IComparable
    {
        [JsonIgnore]
        public double Fitness = -1;
        /// <summary>
        /// Fills this chromosme with new random data
        /// </summary>
        public abstract void Generate();
        /// <summary>
        /// Returns new random chromosome
        /// </summary>
        public abstract Chromosome New();
        /// <summary>
        /// Creates new instance of chromosome, with same genes as this one
        /// </summary>
        public abstract Chromosome Clone();
        /// <summary>
        /// Mutation, Changing one or few genes is recommended
        /// </summary>
        public abstract void Mutate();
        /// <summary>
        /// Crossover between 2 chromosomes, serves similar purpose as generating offspring,
        /// Reccomended exhchanging one or several genes between chromosomes
        /// </summary>
        /// <param name="other"></param>
        public abstract void Crossover(Chromosome other);
        public void Evaluate(Func<Chromosome, double> func)
        {
            Fitness = func(this);
        }
        public int CompareTo(object o)
        {
            double f = ((Chromosome)o).Fitness;

            return (Fitness == f) ? 0 : (Fitness < f) ? 1 : -1;
        }
        /// <summary>
        /// Used to compare chromosomes to remove duplicities from population
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public abstract bool Equals(object obj);
        public abstract int GetHashCode();
    }
}

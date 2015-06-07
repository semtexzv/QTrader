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
    public abstract class Chromosome : IComparable
    {
        [JsonIgnore]
        public double Fitness = -1;
        public abstract void Generate();
        public abstract Chromosome New();
        public abstract Chromosome Clone();
        public abstract void Mutate();
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
    }
}

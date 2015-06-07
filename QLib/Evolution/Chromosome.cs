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

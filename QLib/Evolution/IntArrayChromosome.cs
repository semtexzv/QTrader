using QLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public class IntArrayChromosome : Chromosome
    {
        public int[] genes;
        public static Random rand = new Random();
        public IntArrayChromosome(int length)
        {
            genes = new int[length];
        }
        public override void Generate()
        {

            genes[0] = rand.Next();
            genes[1] = rand.Next();
            genes[2] = rand.Next();
        }

        public override Chromosome New()
        {
            IntArrayChromosome x = new IntArrayChromosome(genes.Length);
            x.Generate();
            return x;
        }

        public override Chromosome Clone()
        {

            IntArrayChromosome x = new IntArrayChromosome(genes.Length);
            x.genes = (int[])this.genes.Clone();
            return x;
        }

        public override void Mutate()
        {
            genes[rand.Next(genes.Length)] = rand.Next();
        }

        public override void Crossover(Chromosome other)
        {
            int i = rand.Next(genes.Length);
            int a = genes[i];
            genes[i] = ((IntArrayChromosome)other).genes[i];
            ((IntArrayChromosome)other).genes[i] = a;
        }
        public override int GetHashCode()
        {
            int h = 0;
           foreach(int i in genes)
           {
               h ^= i;
           }
           return h;
        }
        public override bool Equals(object obj)
        {
            if (obj.GetType() != this.GetType())
                return false;
            IntArrayChromosome o = ((IntArrayChromosome)obj);
            return genes.SequenceEqual(o.genes);

        }
    }
}

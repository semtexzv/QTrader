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

    public class Population
    {
        public Func<Chromosome, double> fitnessFunction;
        public Action<List<Chromosome>, int> selectionMethod = SelectorRoulette;

        private List<Chromosome> population = new List<Chromosome>();

        private double CrossoverRate = 0.75;
        private double MutationRate = 0.10;
        private double RandomSelectionPortion = 0.1;

        public Chromosome this[int index]
        {
            get { return population[index]; }
        }
        public Chromosome Best
        {
            get;
            private set;
        }
        public int Size
        {
            get;
            private set;
        }

        public Population(int size,
                           Chromosome ancestor,
                            Func<Chromosome, double> fitnessFunction,
                           Action<List<Chromosome>, int> selectionMethod=null)
        {
            if (size < 2)
                throw new ArgumentException("Too small population's size was specified.");

            this.Size = size;

            this.fitnessFunction = fitnessFunction;
            if(selectionMethod!=null)
                this.selectionMethod = selectionMethod;

            ancestor.Evaluate(fitnessFunction);
            population.Add(ancestor.Clone());


            for (int i = 1; i < size; i++)
            {
                Chromosome c = ancestor.New();
                c.Evaluate(fitnessFunction);
                population.Add(c);
            }
        }

        
        public void SetAncestor(Chromosome c)
        {
            population.Clear();
            AddChromosome(c);
            Regenerate();
        }

        public void Regenerate()
        {
            Chromosome ancestor = population[0];

            // clear population
            population.Clear();
            // add chromosomes to the population
            for (int i = 0; i < Size; i++)
            {
                // create new chromosome
                Chromosome c = ancestor.New();
                population.Add(c);
            }
        }
        public virtual void Crossover()
        {
            // crossover
            for (int i = 1; i < Size && i<population.Count; i++)
            {
                if (Rand.NextDouble() <= CrossoverRate)
                {
                    Chromosome c1 = population[i - 1].Clone();
                    Chromosome c2 = population[i].Clone();

                    c1.Crossover(c2);

                    population.Add(c1);
                    population.Add(c2);
                }
            }
        }
        public virtual void Mutate()
        {
            // mutate
            for (int i = 0; i < Size && i < population.Count; i++)
            {
                // generate next random number and check if we need to do mutation
                if (Rand.NextDouble() <= MutationRate)
                {
                    Chromosome c = population[i].Clone();
                    c.Mutate();
                    population.Add(c);
                }
            }
        }

        public void Selection()
        {

            population.RemoveDuplicates();
            int randomAmount = (int)(RandomSelectionPortion * Size);
            selectionMethod.Invoke(population, Size - randomAmount);
            if (randomAmount > 0)
            {
                Chromosome ancestor = population[0];

                for (int i = 0; i < randomAmount; i++)
                {
                    Chromosome c = ancestor.New();
                    population.Add(c);
                }
            }
        }
        void Evaluate()
        {
            Parallel.For(0,population.Count,a=>
                {
                    if(population[a].Fitness == -1)
                        population[a].Evaluate(fitnessFunction);
                });
        }


        public void RunEpoch()
        {

            Shuffle();
            Crossover();
            Mutate();

            Evaluate();

            Selection();
            population.Sort();
            Best = population[0];
        }

        public void Shuffle()
        {
            // current population size
            int size = population.Count;
            // create temporary copy of the population
            List<Chromosome> tempPopulation = population.GetRange(0, size);
            // clear current population and refill it randomly
            population.Clear();

            while (size > 0)
            {
                int i = Rand.Next(size);

                population.Add(tempPopulation[i]);
                tempPopulation.RemoveAt(i);

                size--;
            }
        }

        public void AddChromosome(Chromosome chromosome)
        {
            population.Add(chromosome);
        }
              

        public static void SelectorRoulette(List<Chromosome> chromosomes, int DesiredSize)
        {
            List<Chromosome> newPopulation = new List<Chromosome>();
            // size of current population
            int currentSize = chromosomes.Count;

            Chromosome cmax = chromosomes[0];
            // calculate summary fitness of current population
            double fitnessSum = 0;
            foreach (Chromosome c in chromosomes)
            {
                fitnessSum += c.Fitness;
                if (c.Fitness > cmax.Fitness)
                    cmax = c;
            }

            // create wheel ranges
            double[] rangeMax = new double[currentSize];
            double s = 0;
            int k = 0;

            foreach (Chromosome c in chromosomes)
            {
                // cumulative normalized fitness
                s += (c.Fitness / fitnessSum);
                rangeMax[k++] = s;
            }

            // select chromosomes from old population to the new population
            for (int j = 0; j < DesiredSize; j++)
            {
                // get wheel value
                double wheelValue = Rand.NextDouble();
                // find the chromosome for the wheel value
                for (int i = 0; i < currentSize; i++)
                {
                    if (wheelValue <= rangeMax[i])
                    {
                        // add the chromosome to the new population
                        newPopulation.Add(chromosomes[i]);
                        break;
                    }
                }
            }

            // empty current population
            chromosomes.Clear();

            // move elements from new to current population
            chromosomes.AddRange(newPopulation);
            chromosomes.Add(cmax);

        }

    }
}

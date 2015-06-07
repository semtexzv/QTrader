using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public static class Rand
    {
        private static Random _inst = new Random();

        public static int Next()
        {
            lock (_inst) return _inst.Next();
        }
        public static int Next(int a)
        {
            lock (_inst) return _inst.Next(a);
        }
        public static double NextDouble()
        {
            lock (_inst) return _inst.NextDouble();
        }
    }
}

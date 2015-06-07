using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public static class Intervals
    {
        public static readonly int[] Values = new int[] { 600, 1800, 3600, 7200, 10800, 21600, 43200, 86400, 172800 };
        public static int GetInterval(int interval)
        {
            return Values[interval % Values.Length];
        }
        public static int Truncate(int interval)
        {
            return interval % Values.Length;
        }
    }
}

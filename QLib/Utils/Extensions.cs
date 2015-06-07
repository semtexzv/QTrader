using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
   public static class Extensions
    {
        public static float MovingAverage(this IEnumerable<Candle> data, int period)
        {
            return data.Skip(data.Count() - period).Average(a => a.Close);
        }
        public static void RemoveDuplicates<T>(this List<T> data)
        {
            HashSet<T> objs = new HashSet<T>();
            data.RemoveAll(t => !objs.Add(t));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public static class UnixTime
    {
        static DateTime unixEpoch;
        static UnixTime()
        {
            unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public static int Now { get { return GetFromDateTime(DateTime.UtcNow); } }
        public static int GetFromDateTime(DateTime d) { return (int)(d - unixEpoch).TotalSeconds; }

        public static DateTime ConvertToDateTime(int unixtime) { return unixEpoch.AddSeconds(unixtime); }
        public static double ToOxyplotDatetime(int unixtime)
        {
            //return unixEpoch.AddSeconds(unixtime).ToOADate();
            return (unixEpoch.AddSeconds(+unixtime)
                - new DateTime(1899, 12, 31, 0, 0, 0, DateTimeKind.Utc)).TotalDays;

        }
    }
}

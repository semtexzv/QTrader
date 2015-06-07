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

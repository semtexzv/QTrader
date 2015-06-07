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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    public static class Logger
    {
        public static Action<string> OnMessage;
        private static StreamWriter writer;
        static Logger()
        {
            if (!Directory.Exists("./Logs"))
            {
                Directory.CreateDirectory("./Logs");
            }
            string[] files = Directory.GetFiles("./Logs");
            if (files.Count() > 30)
            {
                Array.Sort(files);
                File.Delete(files[0]);
            }
            var file = File.Create("./Logs/" + DateTime.UtcNow.ToString("yyyy-MM-dd hh-mm") + ".txt", 2048, FileOptions.Asynchronous);
            writer = new StreamWriter(file);
        }
        public static void Log(string Message)
        {
            if (OnMessage != null)
            {
                OnMessage(DateTime.UtcNow.ToString("[hh:mm] - ") + Message);
            }
            writer.WriteLine(DateTime.UtcNow.ToString("[yyyy-MM-dd hh:mm] - ") + Message);
            writer.Flush();
        }
        public static void Log(string format, params object[] args)
        {
            string Message = String.Format(format, args);
            if (OnMessage != null)
            {
                OnMessage(DateTime.UtcNow.ToString("[hh:mm] - ") + Message);
            }
            writer.WriteLine(DateTime.UtcNow.ToString("[yyyy-MM-dd hh:mm] - ") + Message);
            writer.Flush();
        }
    }
}

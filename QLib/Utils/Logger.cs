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

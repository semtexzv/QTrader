using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace QLib
{
    public sealed class Settings
    {
        string filename;
        public Settings(string filename)
        {
            this.filename = filename;
            Load(filename);
        }
        private  Dictionary<string, string> values = new Dictionary<string, string>();
        public string this[string key]
        {
            get
            {
                if (values.Keys.Contains(key))
                    return values[key];
                return "";
            }
            set
            {
                if (values.Keys.Contains(key))
                {
                    values[key] = value;
                }
                else
                {
                    values.Add(key, value);
                }
            }
        }
        public void Save()
        {
            Save(filename);
        }
        public void Save(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename, false, Encoding.UTF8, 4096))
            {
                writer.Write(JsonConvert.SerializeObject(values));
                writer.Flush();
            }
            
        }
        public void Load(string filename)
        {
            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename,Encoding.UTF8))
                {
                    values = JsonConvert.DeserializeObject<Dictionary<string, string>>(reader.ReadToEnd());
                }
            }
        }
    }
}

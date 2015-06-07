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

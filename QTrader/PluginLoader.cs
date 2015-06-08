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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QLib
{
    internal static class PluginLoader
    {
        public static List<Assembly> assemblies = new List<Assembly>();
        public static List<IPlugin> plugins = new List<IPlugin>();
        public static List<IExchange> exchanges = new List<IExchange>();
        public static List<IBot> bots = new List<IBot>();
        internal static void Load()
        {
            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (string a in Directory.GetFiles(dir, "*.dll"))
            {
                if (Path.GetFullPath(a) != Path.GetFullPath(Assembly.GetExecutingAssembly().Location))
                {
                    assemblies.Add(Assembly.LoadFile(a));
                }
            }

            Type plugType = typeof(IPlugin);
            foreach (Assembly asm in assemblies)
            {
                foreach (Type t in asm.GetTypes())
                {
                    if (plugType.IsAssignableFrom(t) && plugType != t)
                    {
                        plugins.Add((IPlugin)Activator.CreateInstance(t));
                        exchanges.AddRange(plugins.Last().Exchanges);
                        bots.AddRange(plugins.Last().Bots);

                    }
                }
            }
           
        }
    }
}

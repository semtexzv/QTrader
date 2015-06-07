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
        public static List<ISimulatedExchange> SimulatedExchanges = new List<ISimulatedExchange>();
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
            foreach (IExchange ex in exchanges)
            {
                Type t = typeof(ISimulatedExchange);
                if (t.IsAssignableFrom(ex.GetType()))
                {
                    SimulatedExchanges.Add((ISimulatedExchange)ex);
                }

            }
        }
    }
}

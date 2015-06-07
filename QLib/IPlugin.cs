using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLib
{
    public interface IPlugin
    {
        string Name { get; }
        UserControl Interface { get; }
        IExchange[] Exchanges { get; }
        IBot [] Bots { get; }
    }
}

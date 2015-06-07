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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLib
{
    public partial class Form1 : Form
    {
       
        public Form1()
        {
            InitializeComponent();
            Logger.OnMessage += OnLog;
            ExchangesCB.DataSource = PluginLoader.exchanges;
            ExchangesCB.DisplayMember = "Name";
            string exName = QTrader.Settings["SelectedExchange"];
            if (exName != "")
                ExchangesCB.SelectedItem = PluginLoader.exchanges.Find(a => a.Name == exName);

            BotsCB.DataSource = PluginLoader.bots;
            BotsCB.DisplayMember = "Name";

            string botName = QTrader.Settings["SelectedBot"];
            if (botName != "")
                BotsCB.SelectedIndex = PluginLoader.bots.FindIndex(a => a.Name == exName)+1;

        }
        private void OnLog(string msg)
        {
            if (!Created)
            {
                new Action(() => {
                    Thread.Sleep(500);
                    OnLog(msg);
                }).BeginInvoke(null, null);

            }
            else
            {
                Invoke(new Action(() =>
                {
                    LogTB.AppendText(msg + '\n');
                }));
            }
        }

        private void ExchangesCB_SelectedValueChanged(object sender, EventArgs e)
        {
            QTrader.Exchange = (IExchange)PluginLoader.exchanges[ExchangesCB.SelectedIndex];
            ExchangeControlPanel.Controls.Clear();
            UserControl ctrl = QTrader.Exchange.UI;
            ctrl.Dock = DockStyle.Fill;
            ExchangeControlPanel.Controls.Add(ctrl);
            QTrader.Settings["SelectedExchange"] = QTrader.Exchange.Name;
            
        }

        private void BotsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            QTrader.Bot = (IBot)BotsCB.SelectedValue;
            QTrader.Settings["SelectedBot"] = QTrader.Bot.Name;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            BotTester.Exit();
            QTrader.Settings.Save();
        }

        private void StartTradingButton_Click(object sender, EventArgs e)
        {
            if (QTrader.Running)
            {
                StartTradingButton.Text = "Start Trading";
                QTrader.Stop();
            }
            else
            {
                StartTradingButton.Text = "Stop Trading";
                QTrader.Start();
            }
        }
    }
}

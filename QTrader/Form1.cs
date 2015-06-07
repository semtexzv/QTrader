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

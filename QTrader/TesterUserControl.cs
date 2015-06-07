using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QLib
{
   
    public partial class TesterUserControl : UserControl
    {
        public TesterUserControl()
        {
            InitializeComponent();
            BotTester.OnNewBestChromosome += (a,b) =>
                {
                    Invoke(new Action(() => {
                        ConfDataTB.Text = BotTester.BestBot();
                        ProfitLabel.Text = BotTester.Best().Fitness.ToString("0.0000") + " $";
                        PPMLabel.Text = ((DateTime.UtcNow - StartDateTime.Value.Date).TotalDays / 31f) / (BotTester.Best().Fitness-100) + " %";
                    }));
                };
            BotTester.OnGeneration += (a, b) =>
            {
                Invoke(new Action(() =>
                {
                    GenLabel.Text = "Gen: " + BotTester.Generation;
                }));
            };
        }

        
        private void ExchangeCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StartStopButton_Click(object sender, EventArgs e)
        {
            if (QTrader.SimulatedExchange.Ready)
            {
                if (StartStopButton.Text == "Stop Simulation")
                {
                    StartStopButton.Text = "Start Simulation";
                    BotTester.Stop();
                }
                else
                {
                    StartStopButton.Text = "Stop Simulation";
                    BotTester.Start();
                }
            }
            else
            {
                Logger.Log("Exchange not ready yet");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ApplyConfig_Click(object sender, EventArgs e)
        {
            QTrader.Config = BotTester.Best();
        }
    }
}

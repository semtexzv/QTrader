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

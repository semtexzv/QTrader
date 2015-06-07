using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLib;
using System.IO;
using Newtonsoft.Json;

namespace Bitstamp
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            DataFileName.Text = Bitstamp.Settings["Filename"];
        }


        private void UserControl1_VisibleChanged(object sender, EventArgs e)
        {
            Bitstamp.Settings.Save();
           
           
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            Bitstamp.Settings.Save();
            base.OnHandleDestroyed(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select source";
            open.Filter = "CSV Filest(*.csv)|*.csv";
            if (open.ShowDialog() != DialogResult.OK)
                return;
            string sourcePath = open.FileName;
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "Select Target";
            save.Filter = "Json Files(*.json)|*.json";
            if (save.ShowDialog() != DialogResult.OK)
                return;
            string destPath = save.FileName;

            DateSelectorForm dateSelect = new DateSelectorForm();
            if (dateSelect.ShowDialog() != DialogResult.OK)
                return;


            int Since = UnixTime.GetFromDateTime(dateSelect.StartDate);

            new Action(() =>
            {
                Logger.Log("Starting Conversion");
                List<Trade> trades = new List<Trade>();
                using (StreamReader reader = new StreamReader(sourcePath))
                {
                    using (StreamWriter writer = new StreamWriter(destPath, false, Encoding.UTF8))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            string[] ll = line.Split(',');
                            int time = int.Parse(ll[0]);
                            if (time > Since)
                            {
                                trades.Add(new Trade()
                                {
                                    Timestamp = int.Parse(ll[0]),
                                    Price = float.Parse(ll[1]),
                                    Amount = float.Parse(ll[2]),
                                });
                            }
                        }
                        foreach (Trade t in trades)
                        {
                            writer.WriteLine(JsonConvert.SerializeObject(t));
                        }
                        Logger.Log("Converted {0} trades", trades.Count);
                    }
                }
            }).BeginInvoke(null,null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Select source";
            open.Filter = "Json Files(*.json)|*.json";
            if (open.ShowDialog() != DialogResult.OK)
                return;
            string sourcePath = open.FileName;
            DataFileName.Text = sourcePath;
            Bitstamp.Filename = sourcePath;
        }
    }
}

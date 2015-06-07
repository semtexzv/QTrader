﻿#region License
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Management;
using System.IO;




namespace eventLogCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Config config = new Config();
            check();
            //listBox1.Items.Add(config.ThreadsMax.ToString());
            //foreach (var item in config.SMTPto)
            //{
            //    listBox1.Items.Add(item);
            //}
            foreach (CheckItem item in config.CheckList)
            {
                listBox1.Items.Add(item.eventID);
            }
            
        }

        private void check()
        {
            listBox1.Items.Clear();
            string eventID = "1102";
            string LogSource = "Security";
            List<EventRecord> eventlist = Query.QueryLog(eventID,LogSource);
            foreach (var item in eventlist)
            {
                
                listBox1.Items.Add(item.TaskDisplayName);
                
                if (Query.CheckWord("清除",item))
                {
                    listBox1.Items.Add("yaya");
                }
            }
           
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            check();
        }
    }
}

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


namespace eventLogCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static long last = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Config config = new Config();

            check();
            
        }

        private void check()
        {
            listBox1.Items.Clear();
            EventLog mylog = new EventLog();
            mylog.Log = "security";
            mylog.MachineName = System.Environment.MachineName;

            

            foreach (EventLogEntry entry in mylog.Entries)
            {
                if (entry.InstanceId >= last)
                {
                    listBox1.Items.Add(entry.TimeGenerated);
                    listBox1.Items.Add(entry.InstanceId);
                }
                last = entry.InstanceId;
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            check();
        }
    }
}

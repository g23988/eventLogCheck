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
            
            foreach (CheckItem item in config.CheckList)
            {
                docheck(item);
            }
            
        }

        private void docheck(CheckItem chkItem) {
            List<EventRecord> eventlist = Query.QueryLog(chkItem.eventID, chkItem.source);
            foreach (var item in eventlist)
            {
                if (Query.CheckWord(chkItem.keyword,item))
                {
                    textBox1.Text += (" Alert！" + chkItem.title + "\r\n");
                }
            }
        }

        private void checkKeyword() { 
            
        }

    }
}

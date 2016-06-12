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
        //置放設定檔物件
        private Config config;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.readConfig();

            //開始檢查
            this.docheck(config);

            /*
            foreach (CheckItem item in config.CheckList)
            {
                docheck(item);
                break;
            }*/
            
        }

        /// <summary>
        /// 讀取設定檔 並設定必要設定項
        /// </summary>
        private void readConfig() {
            config = new Config();
            //設定timer秒數
            checkTimer.Enabled = config.CheckTimerEnable;
            checkTimer.Interval = config.CheckTimer;
        }

        /// <summary>
        /// 進行檢查
        /// </summary>
        private void docheck(Config config) {
            textBox1.Text += "gogo\r\n";
        }


        private void docheck(CheckItem chkItem) {
            //List<EventRecord> eventlist = Query.QueryLog(chkItem.eventID, chkItem.source);
            List<EventRecord> eventlist = Query.QueryLog(chkItem.source);
            foreach (var item in eventlist)
            {
                textBox1.Text +=  item.Id +"\r\n";
                /*if (Query.CheckWord(chkItem.keyword,item))
                {
                    textBox1.Text += (" Alert！" + chkItem.title + "\r\n");
                }*/
            }
        }

        private void checkKeyword() { 
            
        }

        /// <summary>
        /// 定時器觸發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkTimer_Tick(object sender, EventArgs e)
        {
            this.readConfig();
            //開始檢查
            this.docheck(config);
            
        }

    }
}

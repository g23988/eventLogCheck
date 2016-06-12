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
        //置放靠時間區間搜索的結果
        private List<EventRecord> eventlist;
        //置放不重複需要被檢查的namespace
        List<String> sourcelist;


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
            sourcelist = new List<String>();
            //設定timer秒數
            checkTimer.Enabled = config.CheckTimerEnable;
            checkTimer.Interval = config.CheckTimer;
            

        }

        /// <summary>
        /// 進行檢查
        /// </summary>
        private void docheck(Config config) {
            textBox1.Text = "";
            //textBox1.Text += "gogo\r\n";
            eventlist = Query.QueryLog(config.Sourcelist,config.RangeSeconds);
            foreach (EventLogRecord log in eventlist)
            {
                //巡迴檢查關鍵字
                foreach (CheckItem checkitem in config.CheckList)
                {
                    //進行檢驗
                    Check check = new Check(checkitem,log);
                    if (check.result())
                    {
                        textBox1.Text += checkitem.title + "\r\n";
                       
                    }
                    /*
                    if (log.Id.ToString() == checkitem.eventID)
                    {
                        textBox1.Text += checkitem.title + "\r\n";
                    }*/
                }
               
            }
        }


        /*
        private void docheck(CheckItem chkItem) {
            //List<EventRecord> eventlist = Query.QueryLog(chkItem.eventID, chkItem.source);
            eventlist = Query.QueryLog(chkItem.source);
            foreach (var item in eventlist)
            {
                textBox1.Text +=  item.Id +"\r\n";
                if (Query.CheckWord(chkItem.keyword,item))
                {
                    textBox1.Text += (" Alert！" + chkItem.title + "\r\n");
                }
            }
        }*/
/*
        private void checkKeyword() { 
            
        }
*/
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

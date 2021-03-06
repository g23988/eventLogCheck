﻿using System;
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
                        textBox1.Text += checkitem.title + " \r\n";
                        textBox1.Text += "事件發生時間: "+log.TimeCreated + " \r\n";
                        textBox1.Text += " \r\n";
                    }
                }
               
            }
            //寄送錯誤信
            if (config.SMTPalert && textBox1.Text!="")
            {
                Mail mail = new Mail(config);
                if (mail.send(textBox1.Text))
                {
                    textBox1.Text += "寄信成功 \r\n";
                }
                else
	            {
                    textBox1.Text += "寄信失敗 \r\n";
	            }

            }

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

        /// <summary>
        /// 寄發測試信
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void send_testMail_btn_Click(object sender, EventArgs e)
        {
            Mail mail = new Mail(config);
            if(!mail.send("test <br> 這只是個信件測試 from eventLogCheck")) textBox1.Text+="寄信失敗 \r\n";
        }

    }
}

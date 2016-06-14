using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Diagnostics;
using System.Collections;


namespace eventLogCheck
{
    /// <summary>
    /// 讀取設定檔相關資源
    /// </summary>
    class Config
    {
        //設定檔位置
        private string _ConfigName = "config.json";
        //最大執行序
        private int _ThreadsMax = 1;
        //監測的時間區間
        private int _RangeSeconds = 86400;
        //定時器開關
        private bool _CheckTimerEnable = false;
        //定時器時間
        private int _CheckTimer = 30;
        //放置重複不須被檢查的source
        List<String> _Sourcelist = new List<String>();
        // smtp 設定
        private bool _SMTPalert = false;
        private string _SMTPserver = "";
        private bool _SMTPauth = false;
        private string _SMTPuser = "";
        private string _SMTPpassword = "";
        private string _SMTPfrom = "";
        private string _SMTPsubject = "";
        private List<string> _SMTPto = new List<string>();
        
        
        //檢查對象
        private ArrayList _checkList = new ArrayList();


        //序列化存放
        private Dictionary<string, dynamic> _dict;

        /// <summary>
        /// 取得或設定執行序數量
        /// </summary>
        public int ThreadsMax
        {
            get { return _ThreadsMax; }
            set { _ThreadsMax = value; }
        }

        /// <summary>
        /// 取得或設定定時器時間 by secs
        /// </summary>
        public int CheckTimer { 
            get { return _CheckTimer;}
            set { _CheckTimer = value * 1000; }
        }

        /// <summary>
        /// 取得或設定定時器開關
        /// </summary>
        public bool CheckTimerEnable {
            get { return _CheckTimerEnable; }
            set { _CheckTimerEnable = value; }
        }

        /// <summary>
        /// 取得或設定需要被檢查的不重複source
        /// </summary>
        public List<String> Sourcelist
        {
            get { return _Sourcelist; }
            set { _Sourcelist = value; }
        }

        /// <summary>
        /// 取得或設定要檢查的eventlog範圍
        /// </summary>
        public int RangeSeconds {
            get { return _RangeSeconds; }
            set { _RangeSeconds = value; }
        }


        /// <summary>
        /// 取得要寄送的對象 email
        /// </summary>
        public string[] SMTPto {
            get { return _SMTPto.ToArray(); }
        }

        /// <summary>
        /// 取得要檢查的對象
        /// </summary>
        public ArrayList CheckList { 
            get { return _checkList;}
        }

        /// <summary>
        /// 取得SMTP serverhost
        /// </summary>
        public string SMTPserver {
            get { return _SMTPserver; }
        }

        /// <summary>
        /// 取得或設定 smtp 是否auth
        /// </summary>
        public bool SMTPauth {
            get { return _SMTPauth; }
            set { _SMTPauth = value; }
        }

        /// <summary>
        /// 取得SMTP 使用者
        /// </summary>
        public string SMTPuser {
            get { return _SMTPuser; }
        }

        /// <summary>
        /// 取得SMTP 密碼
        /// </summary>
        public string SMTPpasswd {
            get { return _SMTPpassword; }
        }

        /// <summary>
        /// 取得SMTP 寄件者
        /// </summary>
        public string SMTPfrom {
            get { return _SMTPfrom; }
        }

        /// <summary>
        /// 取得SMTP 信件主旨
        /// </summary>
        public string SMTPsubject {
            get { return _SMTPsubject; }
        }


        /// <summary>
        /// 建構設定檔相關資料
        /// </summary>
        public Config() {
            _dict = getContentList(readJsonFile(_ConfigName));
            _ThreadsMax = getThreadsMax();
            _SMTPalert = getSMTPalert();
            if (_SMTPalert)
            {
                _SMTPserver = getSMTPserver();
                _SMTPauth = getSMTPauth();
                _SMTPuser = getSMTPuser();
                _SMTPpassword = getSMTPpassword();
                _SMTPfrom = getSMTPfrom();
                _SMTPto = getSMTPto();
                _SMTPsubject = getSMTPsubject();
                _RangeSeconds = getRangeSeconds();
                _CheckTimerEnable = getCheckTimerEnable();
                _CheckTimer = getCheckTimer();
            }
            _checkList = getCheckItems();
            //設定不重複的source
            foreach (CheckItem item in _checkList)
            {
                if (_Sourcelist == null || !_Sourcelist.Contains(item.source) )
                {
                    _Sourcelist.Add(item.source);
                }
            }
        }

        /// <summary>
        /// 取得最大執行序數量
        /// </summary>
        /// <returns></returns>
        private int getThreadsMax() {
            return _dict["system"]["ThreadsMax"];
        }

        /// <summary>
        /// 取得監測的時間區間
        /// </summary>
        /// <returns></returns>
        private int getRangeSeconds() {
            return _dict["system"]["RangeSeconds"];
        }

        /// <summary>
        /// 取得定時器開關
        /// </summary>
        /// <returns></returns>
        private bool getCheckTimerEnable() {
            return _dict["system"]["CheckTimerEnable"];
        }

        /// <summary>
        /// 取得定時器的時間
        /// </summary>
        /// <returns></returns>
        private int getCheckTimer() {
            return _dict["system"]["CheckTimerSeconds"]*1000;
        }

        /// <summary>
        /// 取得 smtp 是否啟動
        /// </summary>
        /// <returns></returns>
        private bool getSMTPalert() {
            return _dict["system"]["SMTP"]["alert"];
        }


        /// <summary>
        /// 取得smtp server位置
        /// </summary>
        /// <returns></returns>
        private string getSMTPserver() {
            return _dict["system"]["SMTP"]["server"];
        }

        /// <summary>
        /// 取得smtp 是否進行身分驗證
        /// </summary>
        /// <returns></returns>
        private bool getSMTPauth() {
            return _dict["system"]["SMTP"]["auth"];
        }

        /// <summary>
        /// 取得 smtp user
        /// </summary>
        /// <returns></returns>
        private string getSMTPuser() {
            return _dict["system"]["SMTP"]["user"];
        }

        /// <summary>
        /// 取得smtp password
        /// </summary>
        /// <returns></returns>
        private string getSMTPpassword() {
            return _dict["system"]["SMTP"]["password"];
        }

        /// <summary>
        /// 取得smtp from
        /// </summary>
        /// <returns></returns>
        private string getSMTPfrom() {
            return _dict["system"]["SMTP"]["from"];
        }

        /// <summary>
        /// 取得smtp 信件主旨
        /// </summary>
        /// <returns></returns>
        private string getSMTPsubject() {
            return _dict["system"]["SMTP"]["subject"];
        }

        /// <summary>
        /// 取得 smtp 要寄送的對象
        /// </summary>
        /// <returns></returns>
        private List<string> getSMTPto() {
            List<string> mailTo = new List<string>();
            foreach (var email in _dict["system"]["SMTP"]["to"])
            {
                mailTo.Add(email);
            }
            return mailTo;
        }

        /// <summary>
        /// 取得所有要檢查的對象
        /// </summary>
        /// <returns></returns>
        private ArrayList getCheckItems() {
            ArrayList items = new ArrayList();
            foreach (var item in _dict["check"]["Items"])
            {
                List<string> keywords = new List<string>();
                foreach (var keyword in item["keywords"])
                {
                    keywords.Add(keyword);
                }
                CheckItem cit = new CheckItem(item["title"], item["source"], item["eventID"], keywords);
                items.Add(cit);
            }
            return items;
        }



        /// <summary>
        /// 取得序列化後的json資料
        /// </summary>
        /// <param name="json">原始json字串</param>
        /// <returns>序列化資料</returns>
        private Dictionary<string, dynamic> getContentList(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, dynamic>>(json);
            return dict;
        }

        /// <summary>
        /// 讀取json檔案成單一json string
        /// </summary>
        /// <param name="filename">檔案位置</param>
        /// <returns>json string</returns>
        private string readJsonFile(string filename){
            StreamReader sr = new StreamReader(filename);
            return sr.ReadToEnd();
        }








    }
}

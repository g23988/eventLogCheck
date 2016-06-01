using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Diagnostics;


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
        // smtp 設定
        private bool _SMTPalert = false;
        private string _SMTPserver = "";
        private bool _SMTPauth = false;
        private string _SMTPuser = "";
        private string _SMTPpassword = "";
        
        
        

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

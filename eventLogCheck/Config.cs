using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace eventLogCheck
{
    class Config
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        public Config() {
            string json = readJsonFile("config.json");
            
        }
        private string readJsonFile(string filename){
            StreamReader sr = new StreamReader(filename);
            return sr.ReadToEnd();
        }






        /*
        private string _iniPath = @"eventLogCheck.ini";
        private List<int> _checkid = new List<int>();
        private List<String> _checkword = new List<String>();

        public Config() {
            _checkid = readCheckID();
            _checkword = readCheckword();
        }

        private List<string> readCheckword()
        {
            List<String> checkwork = new List<String>();
            String keywordListString = Properties.Settings.Default["Keywords"].ToString();
            string[] sArray = keywordListString.Split(',');
            foreach (string item in sArray)
            {
                _checkword.Add(item);
            }
            return _checkword;
        }

        private List<int> readCheckID()
        {
            List<int> checkID = new List<int>();
            String idListString = Properties.Settings.Default["IDs"].ToString();
            string[] sArray = idListString.Split(',');
            foreach (string item in sArray)
            {
                checkID.Add(Convert.ToInt32(item));
            }
            return checkID;
        }

        public string path {
            get {return _iniPath;}
            set { _iniPath = value; }
        }

        public List<int> checkid {
            get { return _checkid; }
            set { _checkid = value; }
        }

        public List<string> checkword 
        {
            get { return _checkword; }
            set { _checkword = value; }
        }
        */
        


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Xml;

namespace eventLogCheck
{
    class Check
    {
        private bool _check = false;
        private List<string> _keywords;
        private EventRecord _eventlog;
        private CheckItem _checkitem;
        
        private XmlDocument _eventlogXML = new XmlDocument();
        string xmlStringBuffer;
        XmlNodeList NodeLists;
        
        /// <summary>
        /// 執行所有檢查
        /// </summary>
        /// <param name="checkitem">關鍵字</param>
        /// <param name="events">單一事件</param>
        public Check(CheckItem checkitem, EventRecord eventlog)
        {
            _keywords = checkitem.keywords;
            _eventlog = eventlog;
            _checkitem = checkitem;
            _eventlogXML.LoadXml(eventlog.ToXml());
            NodeLists = _eventlogXML.SelectNodes("*/*");
            foreach (XmlNode node in NodeLists)
            {
                xmlStringBuffer += node.InnerText;
            }

        }

        /// <summary>
        /// 取得結果
        /// </summary>
        /// <returns>true=具有keyword,false=不具有keyword</returns>
        public bool result() {
            //優先檢查id 如果keyword輸入空白 那就全中
            if (_keywords[0] == "" && checkEventlogID())
            {
                _check = true;
            }
            else
            {
                //開始關鍵字檢查
                bool subcheck = checkKeyWord();
                if (checkEventlogID() && subcheck)
                {
                    _check = true;
                }
            }
            return _check;
        }

        /// <summary>
        /// 檢查ID
        /// </summary>
        /// <returns>ID 符合 回應true</returns>
        private bool checkEventlogID() {
            if (_eventlog.Id.ToString() == _checkitem.eventID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 檢查全部的文字
        /// </summary>
        /// <returns></returns>
        private bool checkKeyWord() {
            bool check = false;
            foreach (string keyword in _keywords)
            {

                if (xmlStringBuffer.Contains(keyword))
                {
                    check = true;
                }
            }
            return check;
        
        }




        /// <summary>
        /// 檢查目標的名稱
        /// </summary>
        /// <returns></returns>
        private bool checkTargetUser() {
            bool check = false;
             foreach (string keyword in _keywords)
            {
                
                if (_eventlog.TaskDisplayName!=null && _eventlog.TaskDisplayName.ToString().Contains(keyword))
                {
                    check = true;
                }
            }
            return check;
        }



        /// <summary>
        /// 檢查顯示的名稱
        /// </summary>
        /// <returns></returns>
        private Boolean checkDisplayName()
        {
            bool check = false;
            foreach (string keyword in _keywords)
            {
                if (_eventlog.TaskDisplayName!=null && _eventlog.TaskDisplayName.ToString().Contains(keyword))
                {
                    check = true;
                }
            }
            return check;
        }

    }
}

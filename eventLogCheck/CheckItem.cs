using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventLogCheck
{
    class CheckItem
    {
        private string _source;
        private string _eventID;
        private string _keyword;
        /// <summary>
        /// 創建事件對象
        /// </summary>
        /// <param name="source">要檢查的來源 ex: Security</param>
        /// <param name="eventID">要檢查的事件id ex: 4624</param>
        /// <param name="keyword">要檢查的關鍵字 ex: 登入</param>
        public CheckItem(string source,string eventID,string keyword){
            _source = source;
            _eventID = eventID;
            _keyword = keyword;
        }
        /// <summary>
        /// 取得要檢查的來源
        /// </summary>
        public string source {
            get { return _source; }
        }
        /// <summary>
        /// 取得要檢查的ID
        /// </summary>
        public string eventID {
            get { return _eventID; }
        }
        /// <summary>
        /// 取得要檢查的關鍵字
        /// </summary>
        public string keyword {
            get { return _keyword; }
        }
    }
}

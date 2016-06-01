using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace eventLogCheck
{
    class Check
    {
        private bool _check = false;
        private string _keyword;
        private EventRecord _events;

        /// <summary>
        /// 執行所有檢查
        /// </summary>
        /// <param name="keyword">關鍵字</param>
        /// <param name="events">單一事件</param>
        public Check(string keyword, EventRecord events)
        {
            _keyword = keyword;
            _events = events;
            if (checkDisplayName())
            {
                _check = true;
            }
        }

        /// <summary>
        /// 取得結果
        /// </summary>
        /// <returns></returns>
        public bool result() {
            return _check;
        }

        /// <summary>
        /// 檢查顯示的名稱
        /// </summary>
        /// <returns></returns>
        private Boolean checkDisplayName()
        {
            return _events.TaskDisplayName.ToString().Contains(_keyword);
        }

    }
}

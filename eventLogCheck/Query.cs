using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Management;
using System.IO;

namespace eventLogCheck
{
    class Query
    {
        /// <summary>
        /// 搜索eventlog
        /// </summary>
        /// <param name="eventID">eventID</param>
        /// <param name="LogSource">source 位置</param>
        /// <returns></returns>
        public static List<EventRecord> QueryLog(string eventID,string LogSource) {

            string sQuery = "*[System/EventID=" + eventID + "]";

            var elQuery = new EventLogQuery(LogSource, PathType.LogName, sQuery);
            var elReader = new System.Diagnostics.Eventing.Reader.EventLogReader(elQuery);

            List<EventRecord> eventList = new List<EventRecord>();
            for (EventRecord eventInstance = elReader.ReadEvent(); null != eventInstance; eventInstance = elReader.ReadEvent())
            {
                eventList.Add(eventInstance);
            }
            return eventList;
        }

        /// <summary>
        /// 檢查eventlog 是否有關鍵字
        /// </summary>
        /// <param name="keyword">關鍵字</param>
        /// <param name="events">需要檢查的eventlog</param>
        /// <returns></returns>
        public static Boolean CheckWord(string keyword,EventRecord events) {
            Check check = new Check(keyword,events);
            return check.result();
        }

        


    }
}

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
        /// 搜索eventlog by eventID
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
        /// 取得時間區間內的eventlog
        /// </summary>
        /// <param name="LogSource">要檢查的目標</param>
        /// <param name="range">時間區間</param>
        /// <returns></returns>
        public static List<EventRecord> QueryLog(List<String> LogSource,int range)
        {
            DateTime localDate = DateTime.UtcNow;
            List<EventRecord> eventList = new List<EventRecord>();

            string sQuery = "*[System[TimeCreated[@SystemTime >= \"" + localDate.AddSeconds(-range).ToString("s") + "\"]]]";
            foreach (string source in LogSource)
            {
                var elQuery = new EventLogQuery(source, PathType.LogName, sQuery);
                var elReader = new System.Diagnostics.Eventing.Reader.EventLogReader(elQuery);
                for (EventRecord eventInstance = elReader.ReadEvent(); null != eventInstance; eventInstance = elReader.ReadEvent())
                {
                    eventList.Add(eventInstance);
                }
            }
            return eventList;
        }




        


    }
}

﻿using System;
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

        public static Boolean CheckWord(string keyword,EventRecord events) {
            return events.TaskDisplayName.ToString().Contains(keyword);
        }
    }
}
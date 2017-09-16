
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class WorkerTask
    {
        private String _TaskName;
        private List<DateTime> _AssignedDays;

        /// <summary>
        /// 
        /// </summary>
        public String TaskName
        {
            get { return _TaskName; }
            set { _TaskName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<DateTime> AssignedDays
        {
            get { return _AssignedDays; }
            set { _AssignedDays = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public WorkerTask()
        {
            _AssignedDays = new List<DateTime>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADate"></param>
        /// <returns></returns>
        public bool DayAssigned(DateTime ADate)
        {
            DateTime ANormDate = Utils.GetNormalizedDateTime(ADate);

            foreach (DateTime TheDate in _AssignedDays)
            {
                if (TheDate.Date == ANormDate.Date) return true;
            }

            return false;
        }
    }
}

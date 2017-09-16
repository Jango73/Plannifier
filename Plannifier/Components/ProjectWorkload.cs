
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plannifier.Components
{
    public class ProjectWorkload
    {
        Project _TheProject;
        DateTime _Start;
        DateTime _End;
        Dictionary<DateTime, double> _WeekLoads;

        /// <summary>
        /// 
        /// </summary>
        public Project TheProject
        {
            get { return _TheProject; }
            set { _TheProject = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Start
        {
            get { return _Start; }
            set { _Start = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime End
        {
            get { return _End; }
            set { _End = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<DateTime, double> WeekLoads
        {
            get { return _WeekLoads; }
            set { _WeekLoads = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        /// <param name="AProject"></param>
        public ProjectWorkload(Workspace AWorkspace, Project AProject)
        {
        }
    }
}

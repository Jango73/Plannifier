
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components.MSProject
{
    [Serializable]
    [XmlType("Task")]
    public class MPPTask
    {
        private int _UID;
        private int _ID;
        private String _Name;
        private int _Type;
        private String _WBS;
        private String _WBSLevel;
        private int _Priority;
        private DateTime _Start;
        private DateTime _Finish;
        private int _Estimated;
        private int _Milestone;
        private int _FixedCostAccrual;
        private int _PercentComplete;

        /// <summary>
        /// 
        /// </summary>
        public int UID
        {
            get { return _UID; }
            set { _UID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String WBS
        {
            get { return _WBS; }
            set { _WBS = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String WBSLevel
        {
            get { return _WBSLevel; }
            set { _WBSLevel = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
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
        public DateTime Finish
        {
            get { return _Finish; }
            set { _Finish = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Estimated
        {
            get { return _Estimated; }
            set { _Estimated = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Milestone
        {
            get { return _Milestone; }
            set { _Milestone = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int FixedCostAccrual
        {
            get { return _FixedCostAccrual; }
            set { _FixedCostAccrual = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PercentComplete
        {
            get { return _PercentComplete; }
            set { _PercentComplete = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public MPPTask()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        public MPPTask(Project AProject, ProjectTask ATask)
        {
            _UID = ATask.ID;
            _ID = ATask.Index + 1;
            _Name = ATask.Name;
            // _Type = AProject.TaskHasChildren(ATask) ? 1 : 0;
            _Type = 0;
            _WBS = AProject.GetWBSForTask(ATask);
            _Priority = 500;
            _Start = ATask.Start;
            _Finish = ATask.End;
            _Estimated = 0;
            _Milestone = ATask.Marker ? 1 : 0;
            _FixedCostAccrual = 0;
            _PercentComplete = (int)ATask.PercentComplete;
        }
    }
}

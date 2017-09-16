
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class Worker
    {
        private String _Trigram;
        private String _FullName;
        private double _PercentEfficiency;
        private List<WorkerModule> _EfficiencyList;
        private List<WorkerTask> _TaskList;
        private List<Vacation> _VacationList;

        /// <summary>
        /// 
        /// </summary>
        public String Trigram
        {
            get { return _Trigram; }
            set { _Trigram = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String FullName
        {
            get { return _FullName; }
            set { _FullName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double PercentEfficiency
        {
            get { return _PercentEfficiency; }
            set { _PercentEfficiency = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<WorkerModule> EfficiencyList
        {
            get { return _EfficiencyList; }
            set { _EfficiencyList = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<WorkerTask> TaskList
        {
            get { return _TaskList; }
            set { _TaskList = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Vacation> VacationList
        {
            get { return _VacationList; }
            set { _VacationList = value; }
        }

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public Worker()
        {
            _Trigram = "";
            _FullName = "";
            _PercentEfficiency = 100.0;
            _EfficiencyList = new List<WorkerModule>();
            _TaskList = new List<WorkerTask>();
            _VacationList = new List<Vacation>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearGeneratedData(Workspace AWorkspace)
        {
            _TaskList.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADate"></param>
        /// <returns></returns>
        public bool IsAvailableAt(DateTime ADate)
        {
            if (ADate.DayOfWeek == DayOfWeek.Saturday || ADate.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }

            foreach (Vacation Vac in _VacationList)
            {
                if (ADate >= Vac.Start && ADate < Vac.End)
                {
                    return false;
                }
            }

            foreach (WorkerTask TheTask in _TaskList)
            {
                if (TheTask.DayAssigned(ADate)) return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AModuleName"></param>
        /// <returns></returns>
        public bool KnowsModule(String AModuleName)
        {
            foreach (WorkerModule Mod in _EfficiencyList)
            {
                if (Mod.ModuleName == AModuleName)
                {
                    if (Mod.PercentEfficiency > 0.0) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AModuleName"></param>
        /// <returns></returns>
        public double PercentageKnowledgeForModule(String AModuleName)
        {
            foreach (WorkerModule Mod in _EfficiencyList)
            {
                if (Mod.ModuleName == AModuleName)
                {
                    return Mod.PercentEfficiency;
                }
            }

            return 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATaskName"></param>
        public void WorksOn(String ATaskName, DateTime ADay)
        {
            WorkerTask TheWorkerTask = null;
            
            foreach (WorkerTask Tsk in _TaskList)
            {
                if (Tsk.TaskName == ATaskName)
                {
                    TheWorkerTask = Tsk;
                    break;
                }
            }

            if (TheWorkerTask == null)
            {
                TheWorkerTask = new WorkerTask();
            }

            TheWorkerTask.TaskName = ATaskName;
            TheWorkerTask.AssignedDays.Add(Utils.GetNormalizedDateTime(ADay));

            if (_TaskList.Contains(TheWorkerTask) == false)
            {
                _TaskList.Add(TheWorkerTask);
            }
        }

        #endregion
    }
}

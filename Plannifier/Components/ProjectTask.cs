
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace Plannifier.Components
{
    [Serializable]
    public class ProjectTask
    {
        public enum TaskPriority
        {
            Lowest,
            Low,
            Normal,
            High,
            Highest,
            Immediate
        };

        private int _ID;
        private int _Level;
        private int _Index;
        private String _Name;
        private String _ModuleName;
        private String _AssignedWorker;
        private TaskPriority _Priority;
        private DateTime _DeliveryLimit;
        private int _DependsOnTaskID;
        private double _DaysToComplete;
        private bool _Fixed;
        private bool _Marker;
        private double _DaysRemaining;
        private DateTime _Start;
        private DateTime _End;
        private double _PercentComplete;

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
        public int Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Index
        {
            get { return _Index; }
            set { _Index = value; }
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
        public String ModuleName
        {
            get { return _ModuleName; }
            set { _ModuleName = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public String AssignedWorker
        {
            get { return _AssignedWorker; }
            set { _AssignedWorker = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public TaskPriority Priority
        {
            get { return _Priority; }
            set { _Priority = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DeliveryLimit
        {
            get { return _DeliveryLimit; }
            set { _DeliveryLimit = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int DependsOnTaskID
        {
            get { return _DependsOnTaskID; }
            set { _DependsOnTaskID = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double DaysToComplete
        {
            get { return _DaysToComplete; }
            set { _DaysToComplete = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Fixed
        {
            get { return _Fixed; }
            set { _Fixed = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Marker
        {
            get { return _Marker; }
            set { _Marker = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public double DaysRemaining
        {
            get { return _DaysRemaining; }
            set { _DaysRemaining = value; }
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
        public double PercentComplete
        {
            get { return _PercentComplete; }
            set { _PercentComplete = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool Finished
        {
            get { return _DaysRemaining < 1.0; }
        }

        [XmlIgnore]
        public bool CanBeChangedByBuilder
        {
            get { return _PercentComplete == 0.0 && _Fixed == false; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ProjectTask()
        {
            _ID = 0;
            _Level = 0;
            _Index = 0;
            _Name = "Untitled";
            _ModuleName = "";
            _Priority = TaskPriority.Normal;
            _DeliveryLimit = DateTime.Now.AddYears(1);
            _DaysToComplete = 1;
            _DependsOnTaskID = 0;
            _Fixed = false;
            _Marker = false;
            _Start = Utils.GetNormalizedDateTime(DateTime.Now);
            _End = Utils.GetNormalizedDateTime(DateTime.Now);
            _PercentComplete = 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearGeneratedData()
        {
            // Is this a fixed task?
            if (_Fixed == false)
            {
                // No, then is it assigned to a worker?
                if (_AssignedWorker == String.Empty)
                {
                    // No, so reset its dates
                    _DaysRemaining = DaysToComplete;
                    _Start = Utils.GetNormalizedDateTime(DateTime.Now);
                    _End = Utils.GetNormalizedDateTime(DateTime.Now);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        /// <param name="AProject"></param>
        /// <param name="CurrentTime"></param>
        public void PreBuild(Workspace AWorkspace, Project AProject)
        {
            // For each assigned task, fill the assigned worker's schedule

            if (_AssignedWorker != String.Empty)
            {
                Worker AWorker = AWorkspace.GetWorkerByName(_AssignedWorker);

                if (AWorker != null)
                {
                    for (DateTime CurrentTime = _Start; CurrentTime < _End; CurrentTime = CurrentTime.AddDays(1.0))
                    {
                        AWorker.WorksOn(_Name, CurrentTime);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        /// <param name="CurrentTime"></param>
        public void Build(Workspace AWorkspace, Project AProject, DateTime CurrentTime)
        {
            // Is this an effective task? (not a container)
            if (AProject.TaskHasChildren(this) == false)
            {
                // Is this a marker?
                if (_Marker == false)
                {
                    // Are the task's dependencies finished?
                    if (DependenciesFinished(AProject))
                    {
                        // Is the task finished?
                        if (!Finished)
                        {
                            // Get a list of workers for this task
                            List<Worker> WorkerList = AWorkspace.GetWorkersForTask(this, CurrentTime);

                            // Do we have a worker available?
                            if (WorkerList.Count > 0)
                            {
                                // Get the first one
                                Worker TheWorker = WorkerList[0];

                                // If the task is not assigned, assign it and mark it as beginning now (CurrentTime)
                                if (_AssignedWorker == String.Empty)
                                {
                                    _AssignedWorker = TheWorker.Trigram;
                                    _Start = CurrentTime;
                                    _DaysRemaining = RealDaysToCompleteForWorker(TheWorker);
                                }

                                // Mark the worker as working on this task
                                TheWorker.WorksOn(_Name, CurrentTime);

                                // Decrement remaining days
                                _DaysRemaining--;

                                // Is the task finished?
                                if (Finished)
                                {
                                    // Mark the task as ending tomorrow (CurrentTime + 1 day)
                                    _End = CurrentTime.AddDays(1.0);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Get this task's children
                List<ProjectTask> SomeTasks = AProject.GetChildTasks(this);

                // Are there any children?
                if (SomeTasks.Count > 0)
                {
                    // Adjust the Start and End of this task so it bounds the Start and End of all child tasks

                    _Start = SomeTasks[0].Start;
                    _End = SomeTasks[0].End;

                    foreach (ProjectTask ATask in SomeTasks)
                    {
                        if (AProject.TaskHasChildren(ATask) == false)
                        {
                            if (ATask.Start < _Start)
                            {
                                _Start = ATask.Start;
                            }

                            if (ATask.End > _End)
                            {
                                _End = ATask.End;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ToTime"></param>
        public void Process(Workspace AWorkspace, Project AProject, DateTime FromDate, DateTime ToDate)
        {
            if (AProject.TaskHasChildren(this) == false)
            {
                /*
                if (_AssignedWorker != String.Empty)
                {
                    Worker TheWorker = AWorkspace.GetWorkerByName(_AssignedWorker);

                    if (TheWorker != null)
                    {
                        double Days = (ToDate - _Start).Days;
                        double DaysForWorker = RealDaysToCompleteForWorker(TheWorker);
                        _PercentComplete = (Days / DaysForWorker) * 100.0;

                        if (_PercentComplete < 0.0) _PercentComplete = 0.0;
                        if (_PercentComplete > 100.0) _PercentComplete = 100.0;
                    }
                }
                */
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        /// <returns></returns>
        public bool DependenciesFinished(Project AProject)
        {
            // Is there any valid ID for a dependency?
            if (_DependsOnTaskID == 0)
            {
                // No, so return true
                return true;
            }

            // Find the task that this task depends on
            ProjectTask TheTask = AProject.GetTaskByID(_DependsOnTaskID);

            // Does it exist?
            if (TheTask != null)
            {
                // Yes, so return whether this task is finished
                return TheTask.Finished;
            }

            // In any other case, return true
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AnotherTask"></param>
        /// <returns></returns>
        public int QualifyVersus(ProjectTask AnotherTask)
        {
            // Check whether one task depends on the other
            if (_ID != 0 && AnotherTask._ID != 0)
            {
                if (_DependsOnTaskID == AnotherTask._ID) return -1;
                if (AnotherTask._DependsOnTaskID == _ID) return 1;
            }

            // Compare the tasks' priorities
            if (_Priority > AnotherTask._Priority) return -1;
            if (_Priority < AnotherTask._Priority) return 1;

            // Compare the tasks' delivery limit date
            if (_DeliveryLimit < AnotherTask._DeliveryLimit) return -1;
            if (_DeliveryLimit > AnotherTask._DeliveryLimit) return 1;

            // Compare the tasks' index
            if (_Index < AnotherTask._Index) return -1;
            if (_Index > AnotherTask._Index) return 1;

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorker"></param>
        /// <returns></returns>
        public double RealDaysToCompleteForWorker(Worker AWorker)
        {
            double Days = _DaysToComplete;
            double PercentageKnowledge = AWorker.PercentageKnowledgeForModule(_ModuleName);
            return Days + (Days * 2.0 * (1.0 - (PercentageKnowledge / 100.0)));
        }
    }
}

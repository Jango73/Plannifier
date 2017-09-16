
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class Workspace
    {
        private String _Name;
        private DateTime _LastProcessDate;
        private List<Project> _Projects;
        private List<Worker> _Workers;
        private System.Windows.Forms.Timer _TheTimer;
        private int _LastID;
        private bool _Dirty;

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
        public DateTime LastProcessDate
        {
            get { return _LastProcessDate; }
            set { _LastProcessDate = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Project> Projects
        {
            get { return _Projects; }
            set { _Projects = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Worker> Workers
        {
            get { return _Workers; }
            set { _Workers = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public bool Dirty
        {
            get { return _Dirty; }
            set { _Dirty = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Workspace()
        {
            _Name = "Untitled";
            _Projects = new List<Project>();
            _Workers = new List<Worker>();
            _LastProcessDate = Utils.GetNormalizedDateTime(DateTime.Now.AddDays(-1.0));
            _LastID = 0;
            _Dirty = false;

            _TheTimer = new System.Windows.Forms.Timer();
            _TheTimer.Interval = 60 * 60 * 1000;
            _TheTimer.Tick += new EventHandler(_TheTimer_Tick);
            _TheTimer.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _TheTimer_Tick(object sender, EventArgs e)
        {
            Process();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GenerateTaskID()
        {
            _LastID++;
            return _LastID;
        }

        /// <summary>
        /// 
        /// </summary>
        public void AssignIDs()
        {
            foreach (Project AProject in _Projects)
            {
                foreach (ProjectTask ATask in AProject.Tasks)
                {
                    if (ATask.ID > _LastID)
                    {
                        _LastID = ATask.ID;
                    }
                }
            }

            foreach (Project AProject in _Projects)
            {
                foreach (ProjectTask ATask in AProject.Tasks)
                {
                    if (ATask.ID == 0)
                    {
                        ATask.ID = GenerateTaskID();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearGeneratedData()
        {
            foreach (Worker AWorker in _Workers)
            {
                AWorker.ClearGeneratedData(this);
            }

            foreach (Project AProject in _Projects)
            {
                AProject.ClearGeneratedData(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrepareForFullBuild()
        {
            foreach (Project AProject in _Projects)
            {
                foreach (ProjectTask ATask in AProject.Tasks)
                {
                    if (ATask.CanBeChangedByBuilder)
                    {
                        ATask.AssignedWorker = "";
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        public void Build(DateTime Start, DateTime End)
        {
            ClearGeneratedData();

            // Normalize Start
            Start = Components.Utils.GetNormalizedDateTime(Start);

            // Current time begins at Start
            DateTime CurrentTime = Start;

            // Pre-process every task
            foreach (Project AProject in _Projects)
            {
                foreach (ProjectTask ATask in AProject.Tasks)
                {
                    ATask.PreBuild(this, AProject);
                }
            }

            while (CurrentTime < End)
            {
                // Process every task that contains effective work
                foreach (Project AProject in _Projects)
                {
                    List<ProjectTask> EffectiveTasks = new List<ProjectTask>();

                    foreach (ProjectTask ATask in AProject.Tasks)
                    {
                        AggregateEffectiveTasks(AProject, EffectiveTasks, ATask);
                    }

                    ProcessTasks(AProject, EffectiveTasks, CurrentTime);
                }

                // Process every task that acts as a container
                foreach (Project AProject in _Projects)
                {
                    List<ProjectTask> ContainerTasks = new List<ProjectTask>();

                    foreach (ProjectTask ATask in AProject.Tasks)
                    {
                        AggregateNonEffectiveTasks(AProject, ContainerTasks, ATask);
                    }

                    ProcessTasks(AProject, ContainerTasks, CurrentTime);
                }

                CurrentTime = CurrentTime.AddDays(1.0);
            }

            // Compute project times
            foreach (Project AProject in _Projects)
            {
                AProject.ComputeStartEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Process()
        {
            DateTime Current = Utils.GetNormalizedDateTime(DateTime.Now);

            if (Current > _LastProcessDate)
            {
                foreach (Project AProject in _Projects)
                {
                    foreach (ProjectTask ATask in AProject.Tasks)
                    {
                        ATask.Process(this, AProject, _LastProcessDate, Current);
                    }
                }

                _LastProcessDate = Current;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SomeTasks"></param>
        /// <param name="CurrentTime"></param>
        public void ProcessTasks(Project AProject, List<ProjectTask> SomeTasks, DateTime CurrentTime)
        {
            // Sort the task list using the ProjectTask compare method
            SomeTasks.Sort(
                delegate(ProjectTask a, ProjectTask b)
                {
                    return a.QualifyVersus(b);
                }
            );

            // Build every task
            foreach (ProjectTask ATask in SomeTasks)
            {
                ATask.Build(this, AProject, CurrentTime);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SomeTasks"></param>
        /// <param name="ATask"></param>
        public void AggregateEffectiveTasks(Project AProject, List<ProjectTask> SomeTasks, ProjectTask ATask)
        {
            if (AProject.TaskHasChildren(ATask) == false)
            {
                SomeTasks.Add(ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SomeTasks"></param>
        /// <param name="ATask"></param>
        public void AggregateNonEffectiveTasks(Project AProject, List<ProjectTask> SomeTasks, ProjectTask ATask)
        {
            if (AProject.TaskHasChildren(ATask) == true)
            {
                SomeTasks.Add(ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <param name="CurrentTime"></param>
        /// <returns></returns>
        public List<Worker> GetWorkersForTask(ProjectTask ATask, DateTime CurrentTime)
        {
            List<Worker> WorkerList = new List<Worker>();

            if (ATask.AssignedWorker != String.Empty)
            {
                foreach (Worker TheWorker in _Workers)
                {
                    if (TheWorker.Trigram == ATask.AssignedWorker)
                    {
                        if (TheWorker.IsAvailableAt(CurrentTime))
                        {
                            if (WorkerList.Contains(TheWorker) == false)
                            {
                                WorkerList.Add(TheWorker);
                            }
                        }
                    }
                }
            }

            foreach (Worker TheWorker in _Workers)
            {
                if (TheWorker.IsAvailableAt(CurrentTime))
                {
                    if (TheWorker.KnowsModule(ATask.ModuleName))
                    {
                        if (WorkerList.Contains(TheWorker) == false)
                        {
                            WorkerList.Add(TheWorker);
                        }
                    }
                }
            }

            try
            {
                WorkerList.Sort(
                    delegate(Worker a, Worker b)
                    {
                        if (a.Trigram == ATask.AssignedWorker)
                            return -1;
                        if (b.Trigram == ATask.AssignedWorker)
                            return 1;

                        if (a.PercentageKnowledgeForModule(ATask.ModuleName) > b.PercentageKnowledgeForModule(ATask.ModuleName))
                            return -1;
                        if (a.PercentageKnowledgeForModule(ATask.ModuleName) < b.PercentageKnowledgeForModule(ATask.ModuleName))
                            return 1;

                        return 0;
                    }
                );
            }
            catch
            {
            }

            return WorkerList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AName"></param>
        /// <returns></returns>
        public Worker GetWorkerByName(String AName)
        {
            foreach (Worker AWorker in _Workers)
            {
                if (AWorker.Trigram == AName)
                {
                    return AWorker;
                }
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<String> GetModuleList()
        {
            List<String> ReturnValue = new List<String>();

            foreach (Worker AWorker in _Workers)
            {
                foreach (WorkerModule AWorkerModule in AWorker.EfficiencyList)
                {
                    if (AWorkerModule.ModuleName != "" && ReturnValue.Contains(AWorkerModule.ModuleName) == false)
                    {
                        ReturnValue.Add(AWorkerModule.ModuleName);
                    }
                }
            }

            return ReturnValue;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class Project
    {
        public event ProjectEventHandler ProjectEvent;
        public delegate void ProjectEventHandler(object sender, ProjectEventArgs e);
        private String _Name;
        private DateTime _Delivery;
        private DateTime _Start;
        private DateTime _End;
        private List<ProjectTask> _Tasks;

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
        public DateTime Delivery
        {
            get { return _Delivery; }
            set { _Delivery = value; }
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
        public List<ProjectTask> Tasks
        {
            get { return _Tasks; }
            set { _Tasks = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Project()
        {
            _Name = "Untitled";
            _Delivery = DateTime.Now.AddDays(10.0);
            _Start = _Delivery;
            _End = _Delivery;
            _Tasks = new List<ProjectTask>();
        }

        /// <summary>
        /// 
        /// </summary>
        public Project(String NewName)
        {
            _Name = NewName;
            _Delivery = DateTime.Now.AddDays(10.0);
            _Start = _Delivery;
            _End = _Delivery;
            _Tasks = new List<ProjectTask>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ProjectTask GetTaskByName(String Name)
        {
            foreach (ProjectTask Task in _Tasks)
            {
                if (Task.Name == Name) return Task;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AnID"></param>
        /// <returns></returns>
        public ProjectTask GetTaskByID(int AnID)
        {
            if (AnID == 0) return null;

            foreach (ProjectTask Task in _Tasks)
            {
                if (Task.ID == AnID) return Task;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        public void ClearGeneratedData(Workspace AWorkspace)
        {
            // Use to assign task indices
            int Index = 0;

            // For every task...
            foreach (ProjectTask ATask in _Tasks)
            {
                // If the task's delivery limit time exceeds the project's delivery time,
                // assign the project's delivery time to the task's delivery time
                if (_Delivery < ATask.DeliveryLimit)
                {
                    ATask.DeliveryLimit = _Delivery;
                }

                // Set the task's index
                ATask.Index = Index;

                // Clear the task's generated data
                ATask.ClearGeneratedData();

                Index++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ComputeStartEnd()
        {
            if (_Tasks.Count > 0)
            {
                foreach (ProjectTask ATask in _Tasks)
                {
                    if (ATask.Marker == false)
                    {
                        _Start = ATask.Start;
                        _End = ATask.End;
                        break;
                    }
                }

                foreach (ProjectTask ATask in _Tasks)
                {
                    if (ATask.Marker == false)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool TaskHasChildren(ProjectTask ATask)
        {
            // Just return whether the task's children count exceeds 0
            return GetChildTasks(ATask).Count > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public List<ProjectTask> GetChildTasks(ProjectTask ATask)
        {
            List<ProjectTask> Children = new List<ProjectTask>();

            bool GotTask = false;

            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                if (_Tasks[Index] == ATask)
                {
                    GotTask = true;
                }
                else
                {
                    if (GotTask)
                    {
                        if (_Tasks[Index].Level <= ATask.Level) break;

                        Children.Add(_Tasks[Index]);
                    }
                }
            }

            return Children;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type"></param>
        public void NotifyProjectChange(ProjectEventArgs.ProjectEventType Type)
        {
            if (ProjectEvent != null)
            {
                ProjectEvent(this, new ProjectEventArgs(this, null, Type));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        public void NotifyTaskChange(ProjectTask ATask, ProjectEventArgs.ProjectEventType Type)
        {
            if (ProjectEvent != null)
            {
                ProjectEvent(this, new ProjectEventArgs(this, ATask, Type));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewDelivery"></param>
        public void SetDelivery(DateTime NewDelivery)
        {
            if (_Delivery != NewDelivery)
            {
                _Delivery = NewDelivery;

                NotifyProjectChange(ProjectEventArgs.ProjectEventType.ProjectChanged);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        public bool MoveTaskUp(ProjectTask ATask)
        {
            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                if (_Tasks[Index] == ATask)
                {
                    if (Index > 0)
                    {
                        int Target = Index - 1;
                        if (Target < 0) Target = 0;

                        _Tasks.RemoveAt(Index);
                        _Tasks.Insert(Target, ATask);

                        if (Target == 0)
                        {
                            ATask.Level = 0;
                        }
                        else
                        {
                            ATask.Level = _Tasks[Target - 1].Level;
                        }

                        NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool MoveTaskDown(ProjectTask ATask)
        {
            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                if (_Tasks[Index] == ATask)
                {
                    if (Index < _Tasks.Count - 1)
                    {
                        int Target = Index + 1;
                        if (Target > _Tasks.Count - 1) Target = _Tasks.Count - 1;

                        _Tasks.RemoveAt(Index);
                        _Tasks.Insert(Target, ATask);

                        NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool MoveTaskLeft(ProjectTask ATask)
        {
            if (ATask.Level > 0)
            {
                List<ProjectTask> Children = GetChildTasks(ATask);

                ATask.Level--;
                foreach (ProjectTask Child in Children)
                {
                    Child.Level--;
                }

                NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool MoveTaskRight(ProjectTask ATask)
        {
            if (CanTaskMoveRight(ATask))
            {
                List<ProjectTask> Children = GetChildTasks(ATask);

                ATask.Level++;
                foreach (ProjectTask Child in Children)
                {
                    Child.Level++;
                }

                NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool CanTaskMoveRight(ProjectTask ATask)
        {
            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                if (_Tasks[Index] == ATask)
                {
                    if (Index == 0) return false;

                    if (_Tasks[Index - 1].Level >= ATask.Level) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool AddTaskAtEnd()
        {
            ProjectTask ANewTask = new ProjectTask();

            _Tasks.Add(ANewTask);

            NotifyTaskChange(ANewTask, ProjectEventArgs.ProjectEventType.TaskAdded);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool AddTaskBefore(Workspace AWorkspace, ProjectTask ATask)
        {
            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                if (_Tasks[Index] == ATask)
                {
                    ProjectTask ANewTask = new ProjectTask();

                    ANewTask.ID = AWorkspace.GenerateTaskID();
                    ANewTask.Level = _Tasks[Index].Level;

                    _Tasks.Insert(Index, ANewTask);

                    NotifyTaskChange(ANewTask, ProjectEventArgs.ProjectEventType.TaskAdded);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        public bool AddTaskAfter(Workspace AWorkspace, ProjectTask ATask)
        {
            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                if (_Tasks[Index] == ATask)
                {
                    ProjectTask ANewTask = new ProjectTask();

                    ANewTask.ID = AWorkspace.GenerateTaskID();
                    ANewTask.Level = _Tasks[Index].Level;

                    _Tasks.Insert(Index + 1, ANewTask);

                    NotifyTaskChange(ANewTask, ProjectEventArgs.ProjectEventType.TaskAdded);

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool DeleteTask(ProjectTask ATask)
        {
            _Tasks.Remove(ATask);

            NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskDeleted);

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public bool SetTaskFixed(ProjectTask ATask, bool SetFixed)
        {
            if (_Tasks.Contains(ATask))
            {
                ATask.Fixed = SetFixed;

                NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <param name="NewStart"></param>
        /// <returns></returns>
        public bool SetTaskStart(ProjectTask ATask, DateTime NewStart)
        {
            if (_Tasks.Contains(ATask) && ATask.Start != NewStart)
            {
                ATask.Start = NewStart;

                NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <param name="NewStart"></param>
        /// <returns></returns>
        public bool SetTaskEnd(ProjectTask ATask, DateTime NewEnd)
        {
            if (_Tasks.Contains(ATask) && ATask.End != NewEnd)
            {
                ATask.End = NewEnd;

                NotifyTaskChange(ATask, ProjectEventArgs.ProjectEventType.TaskChanged);

                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public String GetWBSForTask(ProjectTask ATask)
        {
            String Value = String.Empty;

            List<TaskNode> Nodes = GetTaskHierarchy();
            TaskNode ANode = FindTaskNodeForTask(Nodes, ATask);

            while (ANode != null)
            {
                if (Value.Length > 0) Value = "." + Value;
                Value = (ANode.Task.Index + 1).ToString() + Value;

                ANode = ANode.Parent;
            }

            return Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        private String RemoveLastElementFromQualifiedString(String Text)
        {
            String Value = String.Empty;
            List<String> Tokens = new List<String>(Text.Split('.'));

            if (Tokens.Count > 0)
            {
                Tokens.RemoveAt(Tokens.Count - 1);
            }

            foreach (String Token in Tokens)
            {
                if (Value.Length > 0) Value += ".";
                Value += Token;
            }

            return Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TaskNode> GetTaskHierarchy()
        {
            List<TaskNode> Nodes = new List<TaskNode>();

            for (int Index = 0; Index < _Tasks.Count; Index++)
            {
                ProjectTask SomeTask = _Tasks[Index];

                if (SomeTask.Level == 0)
                {
                    Nodes.Add(new TaskNode(this, SomeTask, null));
                }
            }

            return Nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public TaskNode FindTaskNodeForTask(List<TaskNode> Nodes, ProjectTask ATask)
        {
            TaskNode Value = null;

            foreach (TaskNode ANode in Nodes)
            {
                Value = ANode.FindNodeForTask(ATask);

                if (Value != null) break;
            }

            return Value;
        }
    }

    public class ProjectEventArgs : EventArgs
    {
        public enum ProjectEventType
        {
            ProjectChanged,
            TaskChanged,
            TaskAdded,
            TaskDeleted
        }

        private Project _Project;
        private ProjectTask _Task;
        private ProjectEventType _Type;

        /// <summary>
        /// 
        /// </summary>
        public Project Project
        {
            get { return _Project; }
            set { _Project = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ProjectTask Task
        {
            get { return _Task; }
            set { _Task = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ProjectEventType Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ANewProject"></param>
        public ProjectEventArgs(Project NewProject, ProjectTask NewTask, ProjectEventType NewType)
        {
            _Project = NewProject;
            _Task = NewTask;
            _Type = NewType;
        }
    }

    public class TaskNode
    {
        private ProjectTask _Task;
        private TaskNode _Parent;
        private List<TaskNode> _Children;

        /// <summary>
        /// 
        /// </summary>
        public ProjectTask Task
        {
            get { return _Task; }
            set { _Task = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public TaskNode Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<TaskNode> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewTask"></param>
        public TaskNode(Project AProject, ProjectTask ATask, TaskNode AParent)
        {
            _Task = ATask;
            _Parent = AParent;
            _Children = new List<TaskNode>();

            List<ProjectTask> SomeChildren = AProject.GetChildTasks(ATask);

            foreach (ProjectTask AChild in SomeChildren)
            {
                if (AChild.Level == ATask.Level + 1)
                {
                    _Children.Add(new TaskNode(AProject, AChild, this));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        /// <returns></returns>
        public TaskNode FindNodeForTask(ProjectTask ATask)
        {
            TaskNode Value = null;

            if (_Task == ATask) return this;

            foreach (TaskNode ANode in _Children)
            {
                Value = ANode.FindNodeForTask(ATask);

                if (Value != null) break;
            }

            return Value;
        }
    }
}

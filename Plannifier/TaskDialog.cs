
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plannifier.Components;

namespace Plannifier
{
    public partial class TaskDialog : Form
    {
        private Project _TheProject;
        private ProjectTask _TheTask;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        public TaskDialog(Project AProject, ProjectTask ATask)
        {
            InitializeComponent();
            PopulatePriorities();
            PopulateDependsOnAtask(AProject);

            _TheProject = AProject;
            _TheTask = ATask;

            ProjectTask TheDependsOnTask = AProject.GetTaskByID(_TheTask.DependsOnTaskID);

            TaskName.Text = _TheTask.Name;
            ModuleName.Text = _TheTask.ModuleName;
            AssignedWorker.Text = _TheTask.AssignedWorker;
            Priority.Text = _TheTask.Priority.ToString();
            DeliveryLimit.Value = _TheTask.DeliveryLimit;
            DependsOnTask.Text = TheDependsOnTask == null ? "" : TheDependsOnTask.Name;
            DaysToComplete.Text = _TheTask.DaysToComplete.ToString();
            Fixed.Checked = _TheTask.Fixed;
            Marker.Checked = _TheTask.Marker;
            DaysRemaining.Text = _TheTask.DaysRemaining.ToString();
            Start.Value = _TheTask.Start;
            End.Value = _TheTask.End;
            PercentComplete.Text = _TheTask.PercentComplete.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulatePriorities()
        {
            List<String> Names = Enum.GetNames(typeof(ProjectTask.TaskPriority)).ToList();

            foreach (String AName in Names)
            {
                Priority.Items.Add(AName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateDependsOnAtask(Project AProject)
        {
            foreach (ProjectTask ATask in AProject.Tasks)
            {
                DependsOnTask.Items.Add(ATask.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OK_Click(object sender, EventArgs e)
        {
            double OutDouble;

            _TheTask.Name = TaskName.Text;
            _TheTask.ModuleName = ModuleName.Text;
            _TheTask.AssignedWorker = AssignedWorker.Text;

            try
            {
                Object Result = Enum.Parse(typeof(ProjectTask.TaskPriority), Priority.Text);
                _TheTask.Priority = (ProjectTask.TaskPriority)Result;
            }
            catch
            {
            }

            _TheTask.DeliveryLimit = DeliveryLimit.Value;

            if (DependsOnTask.Text == String.Empty)
            {
                _TheTask.DependsOnTaskID = 0;
            }
            else
            {
                int SelectedDependsOnTask = DependsOnTask.SelectedIndex;

                if (SelectedDependsOnTask >= 0 && SelectedDependsOnTask < _TheProject.Tasks.Count)
                {
                    _TheTask.DependsOnTaskID = _TheProject.Tasks[SelectedDependsOnTask].ID;
                }
            }

            if (Double.TryParse(DaysToComplete.Text, out OutDouble))
            {
                _TheTask.DaysToComplete = OutDouble;
            }

            _TheTask.Fixed = Fixed.Checked;
            _TheTask.Marker = Marker.Checked;

            if (Double.TryParse(DaysRemaining.Text, out OutDouble))
            {
                _TheTask.DaysRemaining = OutDouble;
            }

            _TheTask.Start = Start.Value;
            _TheTask.End = End.Value;

            if (Double.TryParse(PercentComplete.Text, out OutDouble))
            {
                _TheTask.PercentComplete = OutDouble;
            }

            _TheProject.NotifyTaskChange(_TheTask, ProjectEventArgs.ProjectEventType.TaskChanged);

            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

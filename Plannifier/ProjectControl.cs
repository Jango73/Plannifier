
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plannifier.Components;

namespace Plannifier
{
    public partial class ProjectControl : UserControl
    {
        private Workspace _TheWorkspace;
        private Project _TheProject;

        /// <summary>
        /// 
        /// </summary>
        public ProjectControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        public ProjectControl(Workspace AWorkspace, Project AProject)
        {
            InitializeComponent();

            _TheWorkspace = AWorkspace;
            _TheProject = AProject;

            _TheProject.ProjectEvent += _TheProject_ProjectEvent;

            BuildUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _TheProject_ProjectEvent(object sender, ProjectEventArgs e)
        {
            switch (e.Type)
            {
                case ProjectEventArgs.ProjectEventType.ProjectChanged:
                case ProjectEventArgs.ProjectEventType.TaskAdded:
                case ProjectEventArgs.ProjectEventType.TaskDeleted:
                    {
                        BuildUI();
                    }
                    break;

                case ProjectEventArgs.ProjectEventType.TaskChanged:
                    {
                        UpdateUI();
                    }
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TheControl"></param>
        public void BuildUI()
        {
            View.Items.Clear();
            View.Columns.Clear();

            View.Columns.Add("Name");
            View.Columns.Add("Days");
            View.Columns.Add("Module");
            View.Columns.Add("Worker");
            View.Columns.Add("Priority");
            View.Columns.Add("Limit");
            View.Columns.Add("Depends on");
            View.Columns.Add("Start");
            View.Columns.Add("End");
            View.Columns.Add("Percent");

            View.Columns[0].Width = 300;
            View.Columns[2].Width = 100;
            View.Columns[5].Width = 150;
            View.Columns[6].Width = 150;
            View.Columns[7].Width = 150;
            View.Columns[8].Width = 150;

            DeliveryLimit.Value = _TheProject.Delivery;

            foreach (ProjectTask ATask in _TheProject.Tasks)
            {
                AddTask(ATask);
            }

            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ATask"></param>
        private void AddTask(ProjectTask ATask)
        {
            String TaskName = "";
            for (int Index = 0; Index < ATask.Level; Index++)
            {
                TaskName = TaskName + "   ";
            }
            TaskName = TaskName + ATask.Name;

            ListViewItem AnItem = new ListViewItem();

            AnItem.Tag = ATask;

            ListViewItem.ListViewSubItem ASubItem;

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
            AnItem.SubItems.Add(ASubItem);

            View.Items.Add(AnItem);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateUI()
        {
            int TaskIndex = 0;

            foreach (ListViewItem AnItem in View.Items)
            {
                ProjectTask ATask = _TheProject.Tasks[TaskIndex];

                AnItem.Tag = ATask;

                String TaskName = "";
                for (int Index = 0; Index < ATask.Level; Index++)
                {
                    TaskName = TaskName + "   ";
                }
                TaskName = TaskName + ATask.Name;

                AnItem.Text = TaskName;

                ProjectTask DependsOnTask = _TheProject.GetTaskByID(ATask.DependsOnTaskID);

                AnItem.SubItems[1].Text = ATask.DaysToComplete.ToString();
                AnItem.SubItems[2].Text = ATask.ModuleName;
                AnItem.SubItems[3].Text = ATask.AssignedWorker;
                AnItem.SubItems[4].Text = ATask.Priority.ToString();
                AnItem.SubItems[5].Text = ATask.DeliveryLimit.ToString();
                AnItem.SubItems[6].Text = DependsOnTask == null ? "" : DependsOnTask.Name;
                AnItem.SubItems[7].Text = ATask.Start.ToString();
                AnItem.SubItems[8].Text = ATask.End.ToString();
                AnItem.SubItems[9].Text = ATask.PercentComplete.ToString();

                TaskIndex++;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        /// <param name="ATask"></param>
        private void EditTask(Project AProject, ProjectTask ATask)
        {
            TaskDialog Dialog = new TaskDialog(AProject, ATask);

            Dialog.ShowDialog(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        /// <param name="ATask"></param>
        private void DeleteTask(Project AProject, ProjectTask ATask)
        {
            AProject.DeleteTask(ATask);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeliveryLimit_ValueChanged(object sender, EventArgs e)
        {
            _TheProject.SetDelivery(DeliveryLimit.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveCurrentTaskRight()
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                _TheProject.MoveTaskRight(ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveCurrentTaskLeft()
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                _TheProject.MoveTaskLeft(ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveCurrentTaskUp()
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                _TheProject.MoveTaskUp(ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void MoveCurrentTaskDown()
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                _TheProject.MoveTaskDown(ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (View.SelectedItems.Count == 1)
                {
                    ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;
                    Menu.Show(Cursor.Position);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_DoubleClick(object sender, EventArgs e)
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                EditTask(_TheProject, ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void View_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Right)
            {
                MoveCurrentTaskRight();
            }
            else if (e.Control && e.KeyCode == Keys.Left)
            {
                MoveCurrentTaskLeft();
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                MoveCurrentTaskUp();
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                MoveCurrentTaskDown();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTaskBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                _TheProject.AddTaskBefore(_TheWorkspace, ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addTaskAfterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                _TheProject.AddTaskAfter(_TheWorkspace, ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCurrentTaskLeft();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCurrentTaskRight();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCurrentTaskUp();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MoveCurrentTaskDown();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (View.SelectedItems.Count == 1)
            {
                ProjectTask ATask = (ProjectTask)View.SelectedItems[0].Tag;

                DeleteTask(_TheProject, ATask);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewTask_Click(object sender, EventArgs e)
        {
            _TheProject.AddTaskAtEnd();
        }
    }
}

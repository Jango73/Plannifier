
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
    public partial class Plannifier : Form
    {
        private String Filename;
        private Workspace TheWorkspace;

        private const int TabGantt = 0;
        private const int TabResources = 1;

        /// <summary>
        /// 
        /// </summary>
        public Plannifier()
        {
            InitializeComponent();

            Filename = "";

            TheWorkspace = new Workspace();

            BuildUI();
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateUI()
        {
            Text = "Plannifier";

            if (Filename != String.Empty)
            {
                Text += " - " + Filename;
            }

            if (TheWorkspace.Dirty)
            {
                Text += "*";
            }

            if (Projects.TabPages.Count > 0)
            {
                GanttControl Gantt = ((GanttControl)Projects.TabPages[TabGantt].Controls[0]);
                Gantt.Update();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildUI()
        {
            GanttProperties GanttProps = null;

            // Get Gantt control properties
            if (Projects.TabPages.Count > 0)
            {
                GanttProps = ((GanttControl)Projects.TabPages[TabGantt].Controls[0]).Properties;
            }

            // Clear tabs
            Projects.TabPages.Clear();

            // Create Gantt control
            Projects.TabPages.Add("Gantt");
            GanttControl Gantt = new GanttControl();
            Gantt.Dock = DockStyle.Fill;
            Gantt.setWorkspace(TheWorkspace);
            Projects.TabPages[TabGantt].Controls.Add(Gantt);

            // Set gantt properties
            if (GanttProps != null)
            {
                Gantt.Properties = GanttProps;
            }

            // Create resources control
            Projects.TabPages.Add("Ressources");
            ResourceControl Resources = new ResourceControl(TheWorkspace);
            Resources.Dock = DockStyle.Fill;
            Projects.TabPages[TabResources].Controls.Add(Resources);

            // Create project tabs
            foreach (Project AProject in TheWorkspace.Projects)
            {
                BuildProjectTab(AProject);
            }

            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AProject"></param>
        private void BuildProjectTab(Project AProject)
        {
            Projects.TabPages.Add(AProject.Name);
            TabPage Page = Projects.TabPages[Projects.TabPages.Count - 1];

            Page.Tag = AProject;

            ProjectControl TheProjectControl = new ProjectControl(TheWorkspace, AProject);
            TheProjectControl.Dock = DockStyle.Fill;

            Page.Controls.Add(TheProjectControl);

            AProject.ProjectEvent += TheProjectControl_ProjectEvent;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TheProjectControl_ProjectEvent(object sender, ProjectEventArgs e)
        {
            TheWorkspace.Dirty = true;
            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear workspace, are you sure?", "New", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TheWorkspace = new Workspace();
                BuildUI();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML files|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Filename = openFileDialog1.FileName;
                TheWorkspace = ProjectSerializer.Load(Filename);
                TheWorkspace.AssignIDs();
                TheWorkspace.Process();
            }

            BuildUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "XML files|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Filename = saveFileDialog1.FileName;
                ProjectSerializer.Save(Filename, TheWorkspace);
                TheWorkspace.Dirty = false;
            }

            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Filename != String.Empty)
            {
                ProjectSerializer.Save(Filename, TheWorkspace);
                TheWorkspace.Dirty = false;
            }

            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "MS Project XML files|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Filename = saveFileDialog1.FileName;
                ProjectSerializer.Export(Filename, TheWorkspace);
                TheWorkspace.Dirty = false;
            }

            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProjectDialog Dialog = new AddProjectDialog();

            if (Dialog.ShowDialog(this) == DialogResult.OK)
            {
                Project AProject = new Project(Dialog.ProjectName.Text);
                TheWorkspace.Projects.Add(AProject);
                TheWorkspace.Dirty = true;

                BuildProjectTab(AProject);
                UpdateUI();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Plannifier_Resize(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ressourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ganttToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void incrementalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TheWorkspace != null)
            {
                TheWorkspace.Build(DateTime.Now.AddDays(1.0), DateTime.Now.AddYears(1));
                TheWorkspace.Dirty = true;

                UpdateUI();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void totalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TheWorkspace != null)
            {
                TheWorkspace.PrepareForFullBuild();
                TheWorkspace.Build(DateTime.Now.AddDays(1.0), DateTime.Now.AddYears(1));
                TheWorkspace.Dirty = true;

                UpdateUI();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void workLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Reports(TheWorkspace).ReportWorkLoad();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (TheWorkspace.Dirty)
            {
                if (MessageBox.Show("Workspace is not saved, are you sure?", "Warning", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}

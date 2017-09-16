
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Plannifier.Components;

namespace Plannifier
{
    public partial class ResourceControl : UserControl
    {
        private Workspace _TheWorkspace;
        private Worker _TheWorker;

        /// <summary>
        /// 
        /// </summary>
        public ResourceControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        public ResourceControl(Workspace AWorkspace)
        {
            InitializeComponent();

            _TheWorkspace = AWorkspace;
            _TheWorker = null;

            BuildUI();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildUI()
        {
            Resources.Items.Clear();
            Resources.Columns.Clear();
            Resources.Columns.Add("Trigram");

            foreach (Worker AWorker in _TheWorkspace.Workers)
            {
                ListViewItem AnItem = new ListViewItem();
                ListViewItem.ListViewSubItem ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");

                AnItem.SubItems.Add(ASubItem);
                Resources.Items.Add(AnItem);
            }

            UpdateUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorker"></param>
        private void UpdateUI()
        {
            int WorkerIndex = 0;

            foreach (ListViewItem AnItem in Resources.Items)
            {
                Worker AWorker = _TheWorkspace.Workers[WorkerIndex];

                AnItem.Tag = AWorker;
                AnItem.Text = AWorker.Trigram;

                WorkerIndex++;
            }

            {
                Worker AWorker = GetSelectedWorker();

                if (AWorker != null)
                {
                    int ModuleIndex = 0;

                    foreach (ListViewItem AnItem in Modules.Items)
                    {
                        WorkerModule AWorkerModule = AWorker.EfficiencyList[ModuleIndex];

                        AnItem.Tag = AWorkerModule;
                        AnItem.SubItems[0].Text = AWorkerModule.ModuleName;
                        AnItem.SubItems[1].Text = AWorkerModule.PercentEfficiency.ToString();

                        ModuleIndex++;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Resources_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuildWorkerInfo();
        }

        /// <summary>
        /// 
        /// </summary>
        private void BuildWorkerInfo()
        {
            _TheWorker = GetSelectedWorker();

            if (_TheWorker != null)
            {
                Trigram.Text = _TheWorker.Trigram;
                Efficiency.Text = _TheWorker.PercentEfficiency.ToString();

                Modules.Items.Clear();
                Modules.Columns.Clear();
                Modules.Columns.Add("Name");
                Modules.Columns.Add("Efficiency");

                Modules.Columns[0].Width = 200;

                foreach (WorkerModule AModule in _TheWorker.EfficiencyList)
                {
                    ListViewItem AnItem = new ListViewItem();
                    ListViewItem.ListViewSubItem ASubItem = new ListViewItem.ListViewSubItem(AnItem, "");
                    AnItem.SubItems.Add(ASubItem);

                    AnItem.Text = AModule.ModuleName;
                    AnItem.Tag = AModule;
                    ASubItem.Text = AModule.PercentEfficiency.ToString();

                    Modules.Items.Add(AnItem);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddModule_Click(object sender, EventArgs e)
        {
            Worker AWorker = GetSelectedWorker();

            if (AWorker != null)
            {
                WorkerModuleDialog ADialog = new WorkerModuleDialog(_TheWorkspace);

                if (ADialog.ShowDialog(this) == DialogResult.OK)
                {
                    double Efficiency;

                    if (Double.TryParse(ADialog.Efficiency.Text, out Efficiency))
                    {
                        WorkerModule NewModule = new WorkerModule(ADialog.ModuleName.Text, Efficiency);
                        AWorker.EfficiencyList.Add(NewModule);

                        BuildWorkerInfo();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Modules_DoubleClick(object sender, EventArgs e)
        {
            WorkerModule AWorkerModule = GetSelectedWorkerModule();

            if (AWorkerModule != null)
            {
                WorkerModuleDialog ADialog = new WorkerModuleDialog(_TheWorkspace, AWorkerModule);

                if (ADialog.ShowDialog(this) == DialogResult.OK)
                {
                    double Efficiency;

                    if (Double.TryParse(ADialog.Efficiency.Text, out Efficiency))
                    {
                        AWorkerModule.ModuleName = ADialog.ModuleName.Text;
                        AWorkerModule.PercentEfficiency = Efficiency;

                        UpdateUI();
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Worker GetSelectedWorker()
        {
            if (Resources.SelectedItems.Count == 1)
            {
                return (Worker)Resources.SelectedItems[0].Tag;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private WorkerModule GetSelectedWorkerModule()
        {
            if (Modules.SelectedItems.Count == 1)
            {
                return (WorkerModule)Modules.SelectedItems[0].Tag;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddVacation_Click(object sender, EventArgs e)
        {
        }
    }
}

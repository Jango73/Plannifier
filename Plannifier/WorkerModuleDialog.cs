
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Plannifier.Components;

namespace Plannifier
{
    public partial class WorkerModuleDialog : Form
    {
        public WorkerModuleDialog(Workspace AWorkspace)
        {
            InitializeComponent();

            PopulateModuleNames(AWorkspace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        /// <param name="AWorkerModule"></param>
        public WorkerModuleDialog(Workspace AWorkspace, WorkerModule AWorkerModule)
        {
            InitializeComponent();

            PopulateModuleNames(AWorkspace);

            ModuleName.Text = AWorkerModule.ModuleName;
            Efficiency.Text = AWorkerModule.PercentEfficiency.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        private void PopulateModuleNames(Workspace AWorkspace)
        {
            List<String> SomeModules = AWorkspace.GetModuleList();

            foreach (String AModuleName in SomeModules)
            {
                ModuleName.Items.Add(AModuleName);
            }
        }
    }
}

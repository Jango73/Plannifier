
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plannifier.Components
{
    public class Workload
    {
        List<ProjectWorkload> _ProjectWorkloads;

        /// <summary>
        /// 
        /// </summary>
        public List<ProjectWorkload> ProjectWorkloads
        {
            get { return _ProjectWorkloads; }
            set { _ProjectWorkloads = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        public Workload(Workspace AWorkspace)
        {
            foreach (Project AProject in AWorkspace.Projects)
            {
                _ProjectWorkloads.Add(new ProjectWorkload(AWorkspace, AProject));
            }
        }
    }
}

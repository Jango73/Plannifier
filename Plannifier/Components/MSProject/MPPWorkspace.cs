
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components.MSProject
{
    [Serializable]
    [XmlType("Project")]
    public class MPPWorkspace
    {
        private String _Title;
        private List<MPPTask> _Tasks;

        /// <summary>
        /// 
        /// </summary>
        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<MPPTask> Tasks
        {
            get { return _Tasks; }
            set { _Tasks = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public MPPWorkspace()
        {
            _Title = "Untitled";
            _Tasks = new List<MPPTask>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AWorkspace"></param>
        public MPPWorkspace(Workspace AWorkspace)
        {
            _Title = AWorkspace.Name;
            _Tasks = new List<MPPTask>();

            foreach (Project AProject in AWorkspace.Projects)
            {
                foreach (ProjectTask ATask in AProject.Tasks)
                {
                    _Tasks.Add(new MPPTask(AProject, ATask));
                }
            }
        }
    }
}

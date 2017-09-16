
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class WorkerModule
    {
        private String _ModuleName;
        private double _PercentEfficiency;

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
        public double PercentEfficiency
        {
            get { return _PercentEfficiency; }
            set {
                _PercentEfficiency = value;

                if (_PercentEfficiency < 0.0) _PercentEfficiency = 0.0;
                if (_PercentEfficiency > 200.0) _PercentEfficiency = 200.0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public WorkerModule()
        {
            _ModuleName = "";
            _PercentEfficiency = 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NewModuleName"></param>
        /// <param name="NewEfficiency"></param>
        public WorkerModule(String NewModuleName, double NewEfficiency)
        {
            _ModuleName = NewModuleName;
            _PercentEfficiency = NewEfficiency;
        }
    }
}

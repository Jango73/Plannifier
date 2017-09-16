
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class Module
    {
        private String _Name;
        private double _PercentLearningPerDay;

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
        public double PercentLearningPerDay
        {
            get { return _PercentLearningPerDay; }
            set { _PercentLearningPerDay = value; }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Plannifier.Components
{
    [Serializable]
    public class Vacation
    {
        private DateTime _Start;
        private int _Days;

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
        public int Days
        {
            get { return _Days; }
            set { _Days = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public DateTime End
        {
            get { return _Start.AddDays(_Days); }
        }
    }
}

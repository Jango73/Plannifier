
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plannifier.Components
{
    public class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADate"></param>
        /// <returns></returns>
        public static DateTime GetNormalizedDateTime(DateTime ADate)
        {
            return new DateTime(ADate.Year, ADate.Month, ADate.Day, 8, 0, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ADate"></param>
        /// <returns></returns>
        public static DateTime GetMidnightDateTime(DateTime ADate)
        {
            return new DateTime(ADate.Year, ADate.Month, ADate.Day, 0, 0, 0);
        }
    }
}

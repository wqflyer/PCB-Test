using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCBTest.ImportData
{
    class FourWireInfo
    {
        /// <summary>
        /// 驱动+
        /// </summary>
        public int DrivePlus { get; set; }
        /// <summary>
        /// 驱动-
        /// </summary>
        public int DriveMinus { get; set; }
        /// <summary>
        /// 采样+
        /// </summary>
        public int SensePlus { get; set; }
        /// <summary>
        /// 采样-
        /// </summary>
        public int SenseMinus { get; set; }
    }
}

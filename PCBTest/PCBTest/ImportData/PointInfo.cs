using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCBTest.ImportData
{
    class PointInfo
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// X坐标
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// 虚拟排线号
        /// </summary>
        public int VirtualCableNo { get; set; }
        /// <summary>
        /// 网络编号
        /// </summary>
        public int NetworkNumber { get; set; }
        /// <summary>
        /// 模具类型（上模、下模）
        /// </summary>
        public enMouldType MouldType { get; set; }
    }

    enum enMouldType
    {
        MouldUp,
        MouldDown,
    }
}

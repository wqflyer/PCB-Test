using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCBTest.PointMap
{
    interface IPointMap
    {
        Dictionary<int, int> GetVirtualMap();
        Dictionary<int, int> GetPhysicalMap();
    }
}

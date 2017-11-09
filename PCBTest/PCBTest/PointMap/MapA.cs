/*
 * MapA：一一对应
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCBTest.PointMap
{
    class MapA : IPointMap
    {
        Dictionary<int, int> dicVir2Phy = new Dictionary<int, int>();
        Dictionary<int, int> dicPhy2Vir = new Dictionary<int, int>();

        public MapA()
        {
            for (int i = 1; i <= 64; i++)
            {
                dicVir2Phy.Add(i, i);
            }
            foreach (KeyValuePair<int, int> kv in dicVir2Phy)
            {
                dicPhy2Vir.Add(kv.Value, kv.Key);
            }
        }

        public Dictionary<int, int> GetVirtualMap()
        {
            return dicVir2Phy;
        }

        public Dictionary<int, int> GetPhysicalMap()
        {
            return dicPhy2Vir;
        }
    }
}

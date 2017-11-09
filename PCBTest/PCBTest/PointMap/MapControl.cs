using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PCBTest.PointMap
{
    class MapControl
    {
        static IPointMap PointMap = null;

        static MapA mapA = new MapA();


        public static void Init(enMapType mapType)
        {
            switch (mapType)
            {
                case enMapType.MapA:
                    PointMap = mapA;
                    break;
            }
        }

        public static int GetPhysicalPoint(int iVir)
        {
            int iMul = (iVir - 1) / 64;
            int iRem = (iVir - 1) % 64 + 1;
            return PointMap.GetVirtualMap()[iRem] + iMul * 64;
        }

        public static int GetVirtualPoint(int iPhy)
        {
            int iMul = (iPhy - 1) / 64;
            int iRem = (iPhy - 1) % 64 + 1;
            return PointMap.GetPhysicalMap()[iRem] + iMul * 64;
        }
    }

    enum enMapType
    {
        MapA
    }
}

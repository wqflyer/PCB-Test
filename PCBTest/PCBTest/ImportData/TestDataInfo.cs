/*
* Mason MXD文件说明
* 测试点信息示例
    ;Test Points Netlist/attribute table
    #
    1,X47099Y9784,Z1066,L0,U00,RS0,RG00,A0,N1,NS0,DNN,S,34,11,RA00
    2,X47099Y9784,Z1067,L0,U00,RS0,RG00,A0,N1,NS0,DNN,S,34,11,RA00
    *

* 四线测试信息示例
    ;FourWire Test Points Netlist table
    #U0 
    1,1155,15
    2,1156,16
    *
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PCBTest.ImportData
{
    class TestDataInfo
    {
        /// <summary>
        /// 点位信息
        /// </summary>
        public List<PointInfo> ltPointInfo { get; set; }
        /// <summary>
        /// 二线测试信息
        /// </summary>
        public Dictionary<int, List<int>> dicTwoWireInfo { get; set; }
        /// <summary>
        /// 四线测试信息
        /// </summary>
        public List<FourWireInfo> ltFourWireInfo { get; set; }

        /// <summary>
        /// 解析MXD格式的文件
        /// </summary>
        /// <param name="strFilePath"></param>
        /// <returns></returns>
        public static TestDataInfo Analysis(string[] strContents)
        {
            int iRowPt = 0;
            TestDataInfo testDataInfo = new TestDataInfo();
            try
            {
                if (strContents.Length < 1)
                {
                    testDataInfo = null;
                    return null;
                }
                if (!strContents[iRowPt++].Contains("Mason MXD"))
                {
                    testDataInfo = null;
                    return null;
                }
                while (strContents[iRowPt++].Trim() != "#") ;
                string strVal = string.Empty;
                do
                {
                    strVal = strContents[iRowPt++].Trim();
                    string[] cols = strVal.Split(',');
                    if (cols.Length != 15) continue;

                    PointInfo pi = new PointInfo();

                    pi.No = int.Parse(cols[0]);
                    Regex reg = new Regex(@"X(\d+)Y(\d+)");
                    Match mat = reg.Match(cols[1]);
                    if (mat.Groups.Count != 3) continue;
                    pi.X = int.Parse(mat.Groups[1].Value);
                    pi.Y = int.Parse(mat.Groups[2].Value);
                    pi.VirtualCableNo = int.Parse(cols[2].Replace("Z","").Trim());
                    pi.NetworkNumber = int.Parse(cols[8].Replace("N", "").Trim());
                    if (cols[3] == "L0")
                    {
                        pi.MouldType = enMouldType.MouldDown;
                    }
                    else
                    {
                        pi.MouldType = enMouldType.MouldUp;
                    }
                    if (testDataInfo.ltPointInfo == null) testDataInfo.ltPointInfo = new List<PointInfo>();
                    testDataInfo.ltPointInfo.Add(pi);

                    if (testDataInfo.dicTwoWireInfo == null) testDataInfo.dicTwoWireInfo = new Dictionary<int, List<int>>();
                    if (testDataInfo.dicTwoWireInfo.ContainsKey(pi.NetworkNumber))
                    {
                        testDataInfo.dicTwoWireInfo[pi.NetworkNumber].Add(pi.VirtualCableNo);
                    }
                    else
                    {
                        testDataInfo.dicTwoWireInfo.Add(pi.NetworkNumber, new List<int>() { pi.VirtualCableNo });
                    }
                }
                while (strVal != "*");
                while (strContents[iRowPt++].Trim() != "#U0") ;
                int curDrivePlue = 0, curDriveMinus = 0;
                do
                {
                    strVal = strContents[iRowPt++].Trim();
                    string[] cols = strVal.Split(',');
                    if (cols.Length != 3) continue;

                    switch (cols[0].Trim())
                    {
                        case "1":
                            curDrivePlue = int.Parse(cols[1]);
                            curDriveMinus = int.Parse(cols[2]);
                            break;
                        case "2":
                            if (testDataInfo.ltFourWireInfo == null) testDataInfo.ltFourWireInfo = new List<FourWireInfo>();
                            FourWireInfo fwi = new FourWireInfo();
                            fwi.DriveMinus = curDriveMinus;
                            fwi.DrivePlus = curDrivePlue;
                            fwi.SensePlus = int.Parse(cols[1]);
                            fwi.SenseMinus = int.Parse(cols[2]);
                            testDataInfo.ltFourWireInfo.Add(fwi);
                            break;
                    }
                }
                while (strVal != "*");
            }
            catch
            {
                testDataInfo = null;
                return null;
            }
            return testDataInfo;
        }
    }
}

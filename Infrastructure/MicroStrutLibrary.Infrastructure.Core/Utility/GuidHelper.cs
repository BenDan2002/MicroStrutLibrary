using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace MicroStrutLibrary.Infrastructure.Core
{
    /// <summary>
    /// Guid工具类
    /// </summary>
    public static class GuidHelper
    {
        #region 原来的有序的Guid

        private static long begTicks;
        private static List<string> macAddrs;
        //private static string oldname;
        private static int oldint;

        private static object thisLock = new object();

        static GuidHelper()
        {
            begTicks = new DateTime(2000, 1, 1).Ticks;
            macAddrs = new List<string>();
            oldint = 1;
        }

        /// <summary>
        /// 有序的Guid
        /// </summary>
        public static Guid NewGuid()
        {
            string result = string.Empty;
            lock (thisLock)
            {
                //时间 + mac + 数字
                long interval = DateTime.Now.Ticks - begTicks;
                string macAddr = macAddrs.Count > 0 ? macAddrs[0] : string.Empty;
                result = Convert.ToString(interval, 16).PadLeft(16, '0') + macAddr.PadLeft(12, '0');

                if (oldint < 65536)
                    oldint++;
                else
                    oldint = 1;
                //if (result == oldname)
                //{
                //    oldint++;  //TODO:SXW FFFF 溢出不考虑了
                //}
                //else
                //{
                //    oldname = result;
                //    oldint = 1;
                //}

                result += Convert.ToString(oldint, 16).PadLeft(4, '0');

            }
            return new Guid(result);
        }
        #endregion

        /// <summary>
        /// MacAddress，因为多网卡，因此多个地址
        /// </summary>
        private static List<string> GetMacAddress()
        {
            List<string> _MacAddress = new List<string>();

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.OperationalStatus == OperationalStatus.Up && ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    _MacAddress.Add(ni.GetPhysicalAddress().ToString());
                }
            }

            return _MacAddress;
        }
    }
}

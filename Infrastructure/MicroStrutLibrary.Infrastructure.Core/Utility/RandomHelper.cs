using System;

namespace MicroStrutLibrary.Infrastructure.Core
{
    /// <summary>
    /// 随机数工具类
    /// </summary>
    public static class RandomHelper
    {
        private static int minimum = 100000;
        private static int maximal = 999999;

        private static string randomStringNum = "0,1,2,3,4,5,6,7,8,9";
        private static string randomString = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
        private static Random random = new Random(DateTime.Now.Second);

        /// <summary>
        /// 构造函数
        /// </summary>
        static RandomHelper()
        {
        }

        /// <summary>
        /// 产生随机数(六位数字)
        /// </summary>
        /// <returns>随机数</returns>
        public static int GetRandom()
        {
            return random.Next(minimum, maximal);
        }
        /// <summary>
        /// 产生随机数   
        /// </summary>
        /// <param name="minimum">最小值</param>
        /// <param name="maximal">最大值</param>
        /// <returns>随机数</returns>
        public static int GetRandom(int minimum, int maximal)
        {
            return random.Next(minimum, maximal);
        }
        /// <summary>
        /// 生成一个0.0到1.0的随机小数
        /// </summary>
        public static double GetRandomDouble()
        {
            return random.NextDouble();
        }
        /// <summary>
        /// 随机生成字符串（数字）
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机数字</returns>
        public static string GetRandomCodeNumber(int length)
        {
            return GetRandomCode(randomStringNum, length);
        }
        /// <summary>
        /// 随机生成字符串（数字字母混和）
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        public static string GetRandomCode(int length)
        {
            return GetRandomCode(randomString, length);
        }
        /// <summary>
        /// 从字符串里随机得到规定个数的字符串
        /// </summary>
        /// <param name="fromString">字符串(用","分割)</param>
        /// <param name="length">长度</param>
        /// <returns>随机字符串</returns>
        public static string GetRandomCode(string fromString, int length)
        {
            string randomCode = "";

            string[] chars = fromString.Split(',');
            int temp = -1;
            Random rand = new Random();           

            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }

                int t = rand.Next(chars.Length - 1);

                while (temp == t)
                {
                    t = rand.Next(chars.Length - 1);
                }

                temp = t;
                randomCode += chars[t];
            }

            return randomCode;
        }
    }
}

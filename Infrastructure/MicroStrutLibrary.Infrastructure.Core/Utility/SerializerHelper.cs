using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace MicroStrutLibrary.Infrastructure.Core
{
    /// <summary>
    /// Helper methods to serialize/deserialze objects
    /// </summary>
    public static class SerializerHelper
    {
        #region Json
        /// <summary>
        /// Json序列化
        /// </summary>
        /// <param name="serialObject">序列化对象</param>
        /// <returns></returns>
        public static string ToJson(object serialObject)
        {
            return JsonConvert.SerializeObject(serialObject);
        }
        /// <summary>
        /// Json序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="serialObject">序列化对象</param>
        /// <returns></returns>
        public static string ToJson<T>(T serialObject)
        {
            return JsonConvert.SerializeObject(serialObject);
        }
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <returns>JSON对象</returns>
        public static object FromJson(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <param name="json">json字符串</param>
        /// <param name="type">json对象类型</param>
        /// <returns>JSON对象</returns>
        public static object FromJson(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>JSON对象</returns>
        public static T FromJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        #endregion

        #region Binary
        /// <summary>
        /// Binary序列化
        /// </summary>
        /// <param name="serialObject">序列化对象</param>
        /// <returns></returns>
        public static byte[] ToBinary(object serialObject)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, serialObject);

                return stream.ToArray();
            }
        }
        /// <summary>
        /// Binary序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="serialObject">序列化对象</param>
        /// <returns></returns>
        public static byte[] ToBinary<T>(T serialObject)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, serialObject);

                return stream.ToArray();
            }
        }
        /// <summary>
        /// Binary反序列化
        /// </summary>
        /// <param name="bytes">Binary数组</param>
        /// <returns>对象</returns>
        public static object FromBinary(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Write(bytes, 0, bytes.Length);

                return formatter.Deserialize(stream);
            }
        }
        /// <summary>
        /// Json反序列化
        /// </summary>
        /// <typeparam name="T">反序列化对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <returns>JSON对象</returns>
        public static T FromBinary<T>(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Write(bytes, 0, bytes.Length);

                return (T)formatter.Deserialize(stream);
            }
        }
        #endregion
    }
}

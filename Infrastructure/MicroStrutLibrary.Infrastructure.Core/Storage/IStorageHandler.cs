using System.IO;

namespace MicroStrutLibrary.Infrastructure.Core.Storage
{
    /// <summary>
    /// 存储处理器
    /// </summary>
    public interface IStorageHandler
    {
        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        Stream Get(string appCode, string fileDirectory, string fileName);

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="name"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        void Save(string appCode, string fileDirectory, string fileName, Stream stream);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        void Delete(string appCode, string fileDirectory, string fileName);
    }
}

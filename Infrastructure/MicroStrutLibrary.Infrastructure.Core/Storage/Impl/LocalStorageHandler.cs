using System.IO;
using MicroStrutLibrary.Infrastructure.Core.Exception;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MicroStrutLibrary.Infrastructure.Core.Storage
{
    /// <summary>
    /// 本地文件存储处理器
    /// </summary>
    public class LocalStorageHandler : IStorageHandler
    {
        private IOptions<StorageConfiguration> options;

        public LocalStorageHandler(IOptions<StorageConfiguration> options)
        {
            this.options = options;
        }

        public void Delete(string appCode, string fileDirectory, string fileName)
        {
            string path = Path.Combine(this.GetDirectory(appCode), fileDirectory, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public Stream Get(string appCode, string fileDirectory, string fileName)
        {
            string path = Path.Combine(this.GetDirectory(appCode), fileDirectory, fileName);

            if (File.Exists(path))
            {
                return File.OpenRead(path);
            }

            return null;
        }

        public void Save(string appCode, string fileDirectory, string fileName, Stream stream)
        {
            MicroStrutLibraryExceptionHelper.TrueThrow(stream.Length > options.Value.MaxContentSize, this.GetType().FullName, LogLevel.Error, $"{fileName}文件大小超出{options.Value.MaxContentSize}字节的长度限制。");

            string dir = this.GetDirectory(appCode);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (!Directory.Exists(Path.Combine(dir, fileDirectory)))
            {
                Directory.CreateDirectory(Path.Combine(dir, fileDirectory));
            }

            string path = Path.Combine(dir, fileDirectory, fileName);

            using (var fileStream = File.Create(path))
            {
                stream.CopyTo(fileStream);
            }
        }

        private string GetDirectory(string appCode)
        {
            //return Path.Combine(AppContext.BaseDirectory, "Storage", catalog);
            return Path.Combine(Directory.GetCurrentDirectory(), "Storage", appCode, options.Value.PermanentDirectoryPath);
        }
    }
}

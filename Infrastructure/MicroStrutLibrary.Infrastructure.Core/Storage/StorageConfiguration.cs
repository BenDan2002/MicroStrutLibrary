using System;
using System.Collections.Generic;
using System.Text;
using MicroStrutLibrary.Infrastructure.Core.Configuration;

namespace MicroStrutLibrary.Infrastructure.Core.Storage
{
    [ConfigurationEntry(nameof(StorageConfiguration))]
    public class StorageConfiguration
    {
        /// <summary>
        /// 单个附件最大容量（字节）
        /// </summary>
        public int MaxContentSize { get; set; } = 10240;

        /// <summary>
        /// 永久附件存放目录（具体存放根位置取决于存储方式）
        /// </summary>
        public string PermanentDirectoryPath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace AppUpdate.Core
{
    public interface ITransferingZipFileInfo
    {
        /// <summary>
        /// 压缩文件大小
        /// </summary>
        long FileSize { get; set; }
        /// <summary>
        /// 压缩文件希哈值
        /// </summary>
        byte[] HashBytes { get; set; }
    }

    public interface IFilesList
    {
        /// <summary>
        /// 需要压缩文件列表
        /// </summary>
        List<string> FilesList { get; set; }

        ITransferingZipFileInfo ZippingFiles(Stream output);

    }
}
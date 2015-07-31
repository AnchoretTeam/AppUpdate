using System.IO;
using AppUpdate.Core.Helpers;

namespace AppUpdate.Core.Models
{
    public sealed class TransferingZipFile : ITransferingZipFile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appDir">压缩文件所在路径（文件夹）</param>
        public TransferingZipFile(string appDir)
        {
            using (var ms = new MemoryStream())
            {
                FileZipHelper.ZippingFiles(appDir, ms);
                ZipBytes = ms.ToArray();
            }
        }
 
        public byte[] ZipBytes { get;private set; }
        public ITransferingZipFileInfo ZipFileInfo
        {
            get
            {
                //每次都可以获取最新的值
                var zipFileInfo = new TransferingZipFileInfo
                {
                    FileSize = ZipBytes.Length,
                    HashBytes = FileHashHelper.ComputeFileHash(ZipBytes)
                };
                return zipFileInfo;
            }
        }
    }
}
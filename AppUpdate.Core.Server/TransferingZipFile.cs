using System.IO;

namespace AppUpdate.Core
{
    public sealed class TransferingZipFile : ITransferingZipFile
    {
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
                var zipFileInfo = new TransferingZipFileInfo();
                zipFileInfo.FileSize = ZipBytes.Length;
                zipFileInfo.HashBytes = FileHashHelper.ComputeFileHash(ZipBytes);
                return zipFileInfo;
            }
        }
    }
}
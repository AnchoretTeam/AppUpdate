using System.IO;

namespace AppUpdate.Core
{
    public sealed class TransferingZipFile : ITransferingZipFile
    {
        public TransferingZipFile(byte[] zipBytes)
        {
            ZipBytes = zipBytes;
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
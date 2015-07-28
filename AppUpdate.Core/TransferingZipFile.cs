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
                //ÿ�ζ����Ի�ȡ���µ�ֵ
                var zipFileInfo = new TransferingZipFileInfo();
                zipFileInfo.FileSize = ZipBytes.Length;
                zipFileInfo.HashBytes = FileHashHelper.ComputeFileHash(ZipBytes);
                return zipFileInfo;
            }
        }
    }
}
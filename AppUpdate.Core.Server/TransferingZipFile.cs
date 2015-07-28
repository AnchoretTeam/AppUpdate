using System.IO;

namespace AppUpdate.Core
{
    public sealed class TransferingZipFile : ITransferingZipFile
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="appDir">ѹ���ļ�����·�����ļ��У�</param>
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
                //ÿ�ζ����Ի�ȡ���µ�ֵ
                var zipFileInfo = new TransferingZipFileInfo();
                zipFileInfo.FileSize = ZipBytes.Length;
                zipFileInfo.HashBytes = FileHashHelper.ComputeFileHash(ZipBytes);
                return zipFileInfo;
            }
        }
    }
}
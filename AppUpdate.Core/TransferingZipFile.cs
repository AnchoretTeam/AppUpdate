namespace AppUpdate.Core
{
    public sealed class TransferingZipFile : ITransferingZipFile
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="zipBytes">��ѹ���ļ��ֽ�����</param>
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
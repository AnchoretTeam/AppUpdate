namespace AppUpdate.Core
{
    public sealed class TransferingZipFile : ITransferingZipFile
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="zipBytes">被压缩文件字节数组</param>
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
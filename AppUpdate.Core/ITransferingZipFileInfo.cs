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

    public interface ITransferingZipFile
    {
        /// <summary>
        /// 压缩文件
        /// </summary>
        byte[] ZipBytes { get; }
        /// <summary>
        /// 压缩文件信息
        /// </summary>
        ITransferingZipFileInfo ZipFileInfo { get; }
    }
}
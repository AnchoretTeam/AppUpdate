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
}
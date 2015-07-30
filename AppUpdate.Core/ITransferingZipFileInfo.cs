namespace AppUpdate.Core
{
    public interface ITransferingZipFileInfo
    {
        /// <summary>
        /// ѹ���ļ���С
        /// </summary>
        long FileSize { get; set; }
        /// <summary>
        /// ѹ���ļ�ϣ��ֵ
        /// </summary>
        byte[] HashBytes { get; set; }
    }

    public interface ITransferingZipFile
    {
        /// <summary>
        /// ѹ���ļ�
        /// </summary>
        byte[] ZipBytes { get; }
        /// <summary>
        /// ѹ���ļ���Ϣ
        /// </summary>
        ITransferingZipFileInfo ZipFileInfo { get; }
    }
}
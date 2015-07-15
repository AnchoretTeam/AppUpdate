using System;
using System.Collections.Generic;
using System.IO;

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

    public interface IFilesList
    {
        /// <summary>
        /// ��Ҫѹ���ļ��б�
        /// </summary>
        List<string> FilesList { get; set; }

        ITransferingZipFileInfo ZippingFiles(Stream output);

    }
}
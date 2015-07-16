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

    public interface ITransferingZipFile:IList<string>
    {
        ITransferingZipFileInfo ZippingFiles(Stream output);
    }
}
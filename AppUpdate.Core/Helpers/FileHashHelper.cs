using System.IO;
using System.Security.Cryptography;

namespace AppUpdate.Core.Helpers
{
    /// <summary>
    /// 文件SHA1校验工具类
    /// </summary>
    internal static class FileHashHelper
    {
        internal static byte[] ComputeFileHash(string fileName)
        {
            using (var fs=new FileStream(fileName, FileMode.Open))
            {
                return ComputeFileHash(fs);
            }
        }

        internal static byte[] ComputeFileHash(Stream fileStream)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return sha1.ComputeHash(fileStream);
            }
        }

        internal static byte[] ComputeFileHash(byte[] fileBytes)
        {
            using (var sha1 = new SHA1CryptoServiceProvider())
            {
                return sha1.ComputeHash(fileBytes);
            }
        }

        internal static bool CompareHashValue(byte[] hashBytes1, byte[] hashBytes2)
        {
            if (ReferenceEquals(hashBytes1,hashBytes2))
            {
                return true;
            }
            var length = hashBytes1.Length;
            if (length != hashBytes2.Length)
            {
                return false;
            }
            for (var i = 0; i < length; i++)
            {
                if (hashBytes1[i]!=hashBytes2[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}

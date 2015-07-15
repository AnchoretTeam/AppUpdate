using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdate.Core
{
    internal static class FileZipHelper
    {
        internal static Stream ZippingFiles(string directoryInfo)
        {
            using (var ms = new MemoryStream())
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create))
                {
                    ZippingInternalIteration(archive,directoryInfo);
                }
                return ms;
            }
        }
        /// <summary>
        /// 压缩文件（递归调用）
        /// </summary>
        /// <param name="archive">ZipArchive</param>
        /// <param name="directoryInfo">目录信息</param>
        private static void ZippingInternalRecursive(ZipArchive archive, string directoryInfo)
        {
            var files = Directory.GetFiles(directoryInfo);
            foreach (var file in files)
            {
                var entry = archive.CreateEntry(directoryInfo, CompressionLevel.Optimal);
                using (var stream = entry.Open())
                {
                    using (var fs = new FileStream(file, FileMode.Open))
                    {
                        fs.CopyTo(stream);
                    }
                }
            }
            var directores = Directory.GetDirectories(directoryInfo);
            foreach (var directory in directores)
            {
                archive.CreateEntry(directory + "\\", CompressionLevel.NoCompression);
                ZippingInternalRecursive(archive, directory);
            }
        }
        /// <summary>
        /// 压缩文件（迭代）
        /// </summary>
        /// <param name="archive">ZipArchive</param>
        /// <param name="directoryInfo">目录信息</param>
        private static void ZippingInternalIteration(ZipArchive archive, string directoryInfo)
        {
            var stack=new Stack<string>();
            stack.Push(directoryInfo);
            do
            {
                stack.Pop();
                var entry = archive.CreateEntry(directoryInfo, CompressionLevel.Optimal);
                var files = Directory.GetFiles(directoryInfo);
                foreach (var file in files)
                {
                    using (var stream=entry.Open())
                    {
                        using (var fs=new FileStream(file,FileMode.Open))
                        {
                            fs.CopyTo(stream);
                        }
                    }
                }
                var directories = Directory.GetDirectories(directoryInfo);
                foreach (var directory in directories)
                {
                    archive.CreateEntry(directory+"\\",CompressionLevel.NoCompression);
                    stack.Push(directory);
                }
            } while (stack.Count>0);
        }

    }
}

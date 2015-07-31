using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace AppUpdate.Core.Helpers
{
    internal static class FileZipHelper
    {
        /// <summary>
        /// 压缩更新文件
        /// </summary>
        /// <param name="dirInfo">需要更新文件目录</param>
        /// <param name="output"></param>
        /// <returns>压缩文件流</returns>
        internal static void ZippingFiles(string dirInfo, Stream output)
        {
            using (var archive = new ZipArchive(output, ZipArchiveMode.Create))
            {
                ZipInternalIteration(archive, dirInfo);
            }

        }
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="dirPath">要解压到的路径</param>
        /// <param name="input">压缩文件数据</param>
        internal static void UnZippingFiles(string dirPath, byte[] input)
        {
            //备份文件到dirPath下Backup文件夹下
            ZippingBackupFiles(dirPath, dirPath + "Backup\\");
            //覆盖文件
            using (var zipStream = new MemoryStream(input))//将压缩文件信息初始化到内存流
            {
                using (var source = new ZipArchive(zipStream, ZipArchiveMode.Read))//读取压缩文件
                {
                    foreach (var entry in source.Entries)
                    {
                        var fullPath = Path.GetFullPath(dirPath + entry.FullName);
                        if (fullPath.EndsWith("\\"))
                        {
                            if (!Directory.Exists(fullPath))
                            {
                                Directory.CreateDirectory(fullPath);
                            }
                        }
                        else
                        {
                            using (var stream = entry.Open())
                            {
                                using (FileStream fileStream = File.Open(fullPath, FileMode.Create))
                                {
                                    stream.CopyTo(fileStream);
                                }
                            }
                        }
                    }

                }
            }
        }
        /// <summary>
        /// 压缩备份文件
        /// </summary>
        /// <param name="appDir">app程序路径</param>
        /// <param name="backupDir">备份程序路径</param>
        private static void ZippingBackupFiles(string appDir, string backupDir)
        {
            if (!Directory.Exists(backupDir))
            {
                Directory.CreateDirectory(backupDir);
            }
            var upperFolderName = Path.GetFileName(appDir.TrimEnd('\\'));
            var zipFileName = new StringBuilder("backup").Append(upperFolderName).Append("_").Append(DateTime.Now.ToShortDateString().Replace("/", String.Empty)).Append(".zip").ToString();
            using (var ms = File.Create(backupDir + zipFileName))
            {
                using (var archive = new ZipArchive(ms, ZipArchiveMode.Create))
                {
                    ZipInternalIteration(archive, appDir);
                }
            }
        }
        ///// <summary>
        ///// 压缩文件（递归）
        ///// </summary>
        ///// <param name="archive">压缩文件类</param>
        ///// <param name="dirInfo">需要更新文件目录</param>
        //private static void ZipInternalRecursive(ZipArchive archive, string dirInfo)
        //{
        //    var files = Directory.GetFiles(dirInfo);
        //    var uperDir = dirInfo;
        //    foreach (var file in files)
        //    {
        //        var fileEntry = archive.CreateEntry(file.Substring(uperDir.Length), CompressionLevel.Optimal);
        //        using (var s = fileEntry.Open())
        //        {
        //            using (var fs = new FileStream(file, FileMode.Open))
        //            {
        //                fs.CopyTo(s);
        //            }
        //        }
        //    }

        //    var subDirs = Directory.GetDirectories(dirInfo);
        //    foreach (var directoryInfo in subDirs)
        //    {
        //        if (!Path.GetFileName(directoryInfo).Equals("Backup"))
        //        {
        //            var subFolder = directoryInfo + "\\";
        //            archive.CreateEntry(subFolder.Substring(uperDir.Length), CompressionLevel.NoCompression);
        //            ZipInternalRecursive(archive, directoryInfo);
        //        }
        //    }
        //}

        /// <summary>
        /// 压缩文件（迭代）
        /// </summary>
        /// <param name="archive">压缩文件类</param>
        /// <param name="dirInfo">需要更新文件目录</param>
        private static void ZipInternalIteration(ZipArchive archive, string dirInfo)
        {
            var stack = new Stack<string>();
            var uperDir = dirInfo;
            stack.Push(dirInfo);
            do
            {
                dirInfo = stack.Pop();
                var files = Directory.GetFiles(dirInfo);
                foreach (var file in files)
                {
                    var fileEntry = archive.CreateEntry(file.Substring(uperDir.Length), CompressionLevel.Optimal);
                    using (var s = fileEntry.Open())
                    {
                        using (var fs = new FileStream(file, FileMode.Open))
                        {
                            fs.CopyTo(s);
                        }
                    }
                }
                var subDirs = Directory.GetDirectories(dirInfo);
                foreach (var directoryInfo in subDirs)
                {
                    var fileName = Path.GetFileName(directoryInfo);
                    if (fileName != null && !fileName.Equals("Backup"))
                    {
                        var subFolder = directoryInfo + "\\";
                        archive.CreateEntry(subFolder.Substring(uperDir.Length), CompressionLevel.NoCompression);
                        stack.Push(subFolder);
                    }
                }
            } while (stack.Count > 0);
        }

    }
}

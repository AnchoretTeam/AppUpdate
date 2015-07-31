using System;
using System.Collections.Generic;

namespace AppUpdate.Core.Models
{
    /// <summary>
    ///  更新文件集合类
    /// </summary>
    public interface IUpdateFileCollection : IList<string>
    {
        /// <summary>
        /// 本次更新的描述
        /// </summary>
        string Description { set; get; }
        /// <summary>
        /// 本次更新的释放时间
        /// </summary>
        DateTime ReleaseTime { set; get; }
        /// <summary>
        /// 从文件加载更新文件集合
        /// </summary>
        /// <param name="fileName">文件名</param>
        void LoadFromFile(string fileName);
        /// <summary>
        /// 将更新文件集合保存至文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        void SaveFile(string fileName);
    }
}

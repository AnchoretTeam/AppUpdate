using System;
using System.Collections.Generic;

namespace AppUpdate.Core.Models
{
    public enum UpdateType
    {
        Update,
        HotFix
    }

    /// <summary>
    ///  更新文件集合类
    /// </summary>
    public interface IUpdateFileCollection : IList<string>
    {
        /// <summary>
        /// 更新的释放时间
        /// </summary>
        DateTime ReleaseTime { set; get; }

        /// <summary>
        /// 更新的描述
        /// </summary>
        string Description { set; get; }

        /// <summary>
        /// 更新类型
        /// </summary>
        UpdateType UpdateType { set; get; }
    }
}

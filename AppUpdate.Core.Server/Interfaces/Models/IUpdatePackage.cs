using AppUpdate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdate.Core.Interfaces.Models
{
    public interface IUpdatePackage : IUpdateFileCollection
    {
        string PackageName { get; }
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

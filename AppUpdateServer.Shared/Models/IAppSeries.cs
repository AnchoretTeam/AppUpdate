using System.ComponentModel;
using AppUpdate.Core.Helpers;

namespace AppUpdateServer.Models
{
    public interface IAppSeries : INotifyPropertyChanged
    {
        /// <summary>
        /// 获取或设置 AppSeriesID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
// ReSharper disable once InconsistentNaming
        int AppSeriesID { get; }

        /// <summary>
        /// 获取或设置 AppSeriesName 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        string AppSeriesName { get;  }

        /// <summary>
        /// 获取或设置 AppSeriesFriendlyDescription 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        string AppSeriesFriendlyDescription { get;  }

        BulkObservableCollection<IAppBranch> ChildBranches { get; }
    }
}
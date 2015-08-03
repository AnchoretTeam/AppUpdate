using System.ComponentModel;
using AppUpdate.Core.Helpers;

namespace AppUpdateServer.Models
{
    public interface IAppBranch : INotifyPropertyChanged
    {
        /// <summary>
        /// 获取或设置 AppSeries 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        IAppSeries AppSeries { get; }

        // ReSharper disable once InconsistentNaming
        int AppSeriesID { get; }

        /// <summary>
        /// 获取或设置 AppBranchID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
// ReSharper disable once InconsistentNaming
        int AppBranchID { get; }

        /// <summary>
        /// 获取或设置 AppBranchName 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        string AppBranchName { get; }

        /// <summary>
        /// 获取或设置 AppBranchFriendlyDescription 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        string AppBranchFriendlyDescription { get; }

        BulkObservableCollection<IClientInfoBindable> ChildClients { get; }
    }

}
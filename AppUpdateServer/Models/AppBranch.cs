using AppUpdate.Core.Helpers;
using Microsoft.Practices.Prism.Mvvm;

namespace AppUpdateServer.Models
{
    internal sealed class AppBranch : BindableBase
    {
        #region  AppSeries

        private AppSeries _appSeries;

        /// <summary>
        /// 获取或设置 AppSeries 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public AppSeries AppSeries
        {
            get { return _appSeries; }
            set
            {
                SetProperty(ref _appSeries, value);
                OnPropertyChanged("AppSeriesID");
            }
        }

        #endregion

        // ReSharper disable once InconsistentNaming
        public int AppSeriesID
        {
            get
            {
                if (_appSeries == null)
                {
                    return -1;
                }
                return _appSeries.AppSeriesID;
            }
        }

        #region  AppBranchID

        // ReSharper disable once InconsistentNaming
        private int _appBranchID = -1;

        /// <summary>
        /// 获取或设置 AppBranchID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int AppBranchID
        {
            get { return _appBranchID; }
            set { SetProperty(ref _appBranchID, value); }
        }

        #endregion

        #region  AppBranchName

        private string _appBranchName = string.Empty;

        /// <summary>
        /// 获取或设置 AppBranchName 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string AppBranchName
        {
            get { return _appBranchName; }
            set { SetProperty(ref _appBranchName, value); }
        }

        #endregion

        #region  AppBranchFriendlyDescription

        private string _appBranchFriendlyDescription = string.Empty;

        /// <summary>
        /// 获取或设置 AppBranchFriendlyDescription 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string AppBranchFriendlyDescription
        {
            get { return _appBranchFriendlyDescription; }
            set { SetProperty(ref _appBranchFriendlyDescription, value); }
        }

        #endregion

        public BulkObservableCollection<IClientInfoBindable> ChildClients { get; }=new BulkObservableCollection<IClientInfoBindable>();
    }
}
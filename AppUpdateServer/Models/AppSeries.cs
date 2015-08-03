using AppUpdate.Core.Helpers;
using Microsoft.Practices.Prism.Mvvm;

namespace AppUpdateServer.Models
{
    internal sealed class AppSeries : BindableBase, IAppSeries
    {
        #region AppSeriesID

        // ReSharper disable once InconsistentNaming
        private int _appSeriesID = -1;

        /// <summary>
        /// ��ȡ������ AppSeriesID ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int AppSeriesID
        {
            get { return _appSeriesID; }
            set { SetProperty(ref _appSeriesID, value); }
        }

        #endregion

        #region AppSeriesName

        private string _appSeriesName = string.Empty;

        /// <summary>
        /// ��ȡ������ AppSeriesName ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        public string AppSeriesName
        {
            get { return _appSeriesName; }
            set { SetProperty(ref _appSeriesName, value); }
        }

        #endregion

        #region AppSeriesFriendlyDescription

        private string _appSeriesFriendlyDescription = string.Empty;

        /// <summary>
        /// ��ȡ������ AppSeriesFriendlyDescription ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        public string AppSeriesFriendlyDescription
        {
            get { return _appSeriesFriendlyDescription; }
            set { SetProperty(ref _appSeriesFriendlyDescription, value); }
        }

        #endregion

        public BulkObservableCollection<IAppBranch> ChildBranches { get; } = new BulkObservableCollection<IAppBranch>();
    }
}
using System.ComponentModel;
using AppUpdate.Core.Helpers;

namespace AppUpdateServer.Models
{
    public interface IAppSeries : INotifyPropertyChanged
    {
        /// <summary>
        /// ��ȡ������ AppSeriesID ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
// ReSharper disable once InconsistentNaming
        int AppSeriesID { get; }

        /// <summary>
        /// ��ȡ������ AppSeriesName ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        string AppSeriesName { get;  }

        /// <summary>
        /// ��ȡ������ AppSeriesFriendlyDescription ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        string AppSeriesFriendlyDescription { get;  }

        BulkObservableCollection<IAppBranch> ChildBranches { get; }
    }
}
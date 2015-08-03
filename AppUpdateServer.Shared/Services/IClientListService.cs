using System;
using System.ComponentModel;
using System.Windows.Controls;
using AppUpdate.Core.Helpers;
using AppUpdateServer.Models;

namespace AppUpdateServer.Services
{
    public interface IClientListService : INotifyPropertyChanged, IDisposable
    {
        BulkObservableCollection<IClientInfoBindable> ClientInfos { get; }

        BulkObservableCollection<IAppBranch> AppBranches { get; }

        BulkObservableCollection<IAppSeries> AppSeries { get; }

        IClientInfoBindable SelectedClientInfo { get; }

        IAppBranch SelectedAppBranch { get; }

        IAppSeries SelectedAppSeries { get; }

        object SelectedItem { set; get; }
    }
}

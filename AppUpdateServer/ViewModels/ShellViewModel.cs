using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using AppUpdate.Core.Helpers;
using AppUpdateServer.Models;
using AppUpdateServer.Properties;
using AppUpdateServer.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Mina.Core.Service;

namespace AppUpdateServer.ViewModels
{
    internal sealed class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator _aggregator;
        private readonly IoAcceptor _acceptor;
        private readonly IClientListService _clientListService;

        public ShellViewModel()
        {
            //获取事件聚合器  
            _aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _clientListService = ServiceLocator.Current.GetInstance<IClientListService>();

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                //获取Socket
                _acceptor = ServiceLocator.Current.GetInstance<IoAcceptor>();
            }
        }


        public BulkObservableCollection<IClientInfoBindable> ClientInfos => _clientListService?.ClientInfos;

        public BulkObservableCollection<IAppBranch> AppBranches => _clientListService?.AppBranches;

        public BulkObservableCollection<IAppSeries> AppSeries => _clientListService?.AppSeries;
    }
}
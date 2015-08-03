using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppUpdate.Core.Helpers;
using AppUpdateServer.Models;
using AppUpdateServer.Services;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Mina.Core.Service;

namespace AppUpdateServer.ViewModels
{
    public sealed class ClientsTreeViewModel : BindableBase
    {
        private readonly IEventAggregator _aggregator;
        private readonly IoAcceptor _acceptor;
        private readonly IClientListService _clientListService;

        public ClientsTreeViewModel()
        {
            _aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _clientListService= ServiceLocator.Current.GetInstance<IClientListService>();

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
        public IClientListService ClientListService => _clientListService;

        public BulkObservableCollection<IClientInfoBindable> ClientInfos => _clientListService?.ClientInfos;

        public BulkObservableCollection<IAppBranch> AppBranches => _clientListService?.AppBranches;

        public BulkObservableCollection<IAppSeries> AppSeries => _clientListService?.AppSeries;
    }
}

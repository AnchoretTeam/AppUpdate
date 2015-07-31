using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using AppUpdateServer.Models;
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

        public ShellViewModel()
        {

            //获取事件聚合器  
            _aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();

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

        public ObservableCollection<IClientInfoBindable> ClientInfos { get; set; }
    }
}
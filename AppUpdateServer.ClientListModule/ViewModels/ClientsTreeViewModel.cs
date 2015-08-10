using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppUpdate.Core.Helpers;
using AppUpdateServer.Events;
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
            _aggregator.GetEvent<SpecifyAnAppDefinitionItemToSelectEvent>().Subscribe(i =>
            {
                SelectedItem = i;
            });
        }

        #region SelectedItem

        private IAppDefinitionItem _selectedItem;

        /// <summary>
        /// 获取或设置 SelectedItem 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public IAppDefinitionItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (SetProperty(ref _selectedItem, value))
                {
                    _aggregator.GetEvent<SelectedAppDefinitionItemChangedEvent>().Publish(value);
                }
            }
        }

        #endregion

        public BulkObservableCollection<IClientInfoBindable> ClientInfos => _clientListService?.ClientInfos;

        public BulkObservableCollection<IAppBranch> AppBranches => _clientListService?.AppBranches;

        public BulkObservableCollection<IAppSeries> AppSeries => _clientListService?.AppSeries;
    }
}

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
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Mina.Core.Service;

namespace AppUpdateServer.ViewModels
{
    public sealed class ClientUpdateViewModel : BindableBase, ITabItemViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly IoAcceptor _acceptor;
        public ClientUpdateViewModel()
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
            _aggregator.GetEvent<SelectedAppDefinitionItemChangedEvent>().Subscribe(i =>
            {
                var newClient = i as IClientInfoBindable;
                if (CurrentClientInfo != newClient)
                {
                    CurrentClientInfo = newClient;
                }
                Visibility = newClient == null ? Visibility.Collapsed : Visibility.Visible;
            });
        }

        #region ITabItemViewModel

        public object TabContent { get; } = "发布更新";

        #region Visibility

        private Visibility _visibility = Visibility.Collapsed;

        /// <summary>
        /// 获取或设置 Visibility 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
            set { SetProperty(ref _visibility, value); }
        }

        #endregion

        #endregion

        #region CurrentClientInfo

        private IClientInfoBindable _currentClientInfo;

        /// <summary>
        /// 获取或设置 CurrentClientInfo 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public IClientInfoBindable CurrentClientInfo
        {
            get { return _currentClientInfo; }
            private set { SetProperty(ref _currentClientInfo, value); }
        }

        #endregion

        public BulkObservableCollection<UpdatePackage> UpdateSet { get; set; }
    }
}

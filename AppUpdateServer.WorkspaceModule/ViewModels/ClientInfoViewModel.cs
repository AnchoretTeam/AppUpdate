using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AppUpdateServer.Models;
using AppUpdateServer.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Mina.Core.Service;

namespace AppUpdateServer.ViewModels
{
    public sealed class ClientInfoViewModel:BindableBase, ITabItemViewModel
    {
        private readonly IEventAggregator _aggregator;
        private readonly IoAcceptor _acceptor;
        private readonly IClientListService _clientListService;

        public ClientInfoViewModel()
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

        public IClientListService ClientListService => _clientListService;

        #region ResetCommand

        /// <summary>
        ///  <see cref="ResetCommand" /> 命令的名称.
        /// </summary>
        private DelegateCommand _resetCommand;

        /// <summary>
        /// 获取 ResetCommand 命令.
        /// </summary>
        public DelegateCommand ResetCommand
        {
            get
            {
                return _resetCommand ?? (_resetCommand =
                    new DelegateCommand(ResetCommandExecute, CanResetCommandExecute));
            }
        }

        private bool CanResetCommandExecute()
        {
            return true;
        }

        private void ResetCommandExecute()
        {
            if (!ResetCommand.CanExecute())
            {
                return;
            }
            //TODO 添加ResetCommand命令的Execute代码.
            throw new NotImplementedException();
        }

        #endregion

        #region ITabItemViewModel

        public object TabContent
        {
            get
            {
                return "客户端信息";
            }
        }

        #endregion
    }
}

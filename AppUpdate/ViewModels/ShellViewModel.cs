using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using AppUpdate.Events;
using AppUpdate.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Mina.Core.Service;

namespace AppUpdate.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ShellViewModel : BindableBase
    {
        private readonly IEventAggregator _aggregator;
        private readonly IoConnector _connector;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ShellViewModel()
        {
            //获取事件聚合器  
            _aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            _aggregator.GetEvent<UpdateFileCollectionEvent>().Subscribe(updateFileCollection =>
            {
                UpdateInfo = $"更新包编译日期：{updateFileCollection.ReleaseTime}\r\n更新内容：{updateFileCollection.Description}";
            });

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                //获取Socket
                _connector = ServiceLocator.Current.GetInstance<IoConnector>();
            }
        }

        #region  RemoteIPAddress

        // ReSharper disable once InconsistentNaming
        private IPAddress _remoteIPAddress = IPAddress.Loopback;

        /// <summary>
        /// 获取或设置 RemoteIPAddress 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IPAddress RemoteIPAddress
        {
            get { return _remoteIPAddress; }
            set { SetProperty(ref _remoteIPAddress, value); }
        }

        #endregion

        #region  RemotePort

        private int _remotePort = 2001;

        /// <summary>
        /// 获取或设置 RemotePort 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public int RemotePort
        {
            get { return _remotePort; }
            set { SetProperty(ref _remotePort, value); }
        }

        #endregion

        #region  Logs

        private ObservableCollection<string> _logs = new ObservableCollection<string>();

        /// <summary>
        /// 获取或设置 Logs 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public ObservableCollection<string> Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }

        #endregion

        #region  IsBusy

        private bool _isBusy = false;

        /// <summary>
        /// 获取或设置 IsBusy 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        #endregion

        #region  UpdateInfo

        private string _updateInfo = "请检查更新...";

        /// <summary>
        /// 获取或设置 UpdateInfo 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string UpdateInfo
        {
            get { return _updateInfo; }
            set { SetProperty(ref _updateInfo, value); }
        }

        #endregion

        #region  UpdateInfoChecked

        private bool _updateInfoChecked = false;

        /// <summary>
        /// 获取或设置 UpdateInfoChecked 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public bool UpdateInfoChecked
        {
            get { return _updateInfoChecked; }
            set { SetProperty(ref _updateInfoChecked, value); }
        }

        #endregion

        #region CheckUpdateCommand

        /// <summary>
        ///  <see cref="CheckUpdateCommand" /> 命令的名称.
        /// </summary>
        private DelegateCommand _checkUpdateCommand;

        /// <summary>
        /// 获取 CheckUpdateCommand 命令.
        /// </summary>
        public DelegateCommand CheckUpdateCommand
        {
            get
            {
                return _checkUpdateCommand ?? (_checkUpdateCommand =
                    DelegateCommand.FromAsyncHandler(CheckUpdateCommandExecute, CanCheckUpdateCommandExecute));
            }
        }

        private bool CanCheckUpdateCommandExecute()
        {
            return !IsBusy;
        }

        private Task CheckUpdateCommandExecute()
        {
            if (!CheckUpdateCommand.CanExecute())
            {
                return null;
            }
            IsBusy = true;
            CheckUpdateCommand.RaiseCanExecuteChanged();
            SetupUpdateCommand.RaiseCanExecuteChanged();
            UpdateInfo = "正在检查更新...";
            // 添加CheckUpdateCommand命令的Execute代码.
            return Task.Run(() =>
            {
                var future = _connector.Connect(new IPEndPoint(RemoteIPAddress, RemotePort));
                future.Await(5000);
                if (!future.Connected)
                {
                    UpdateInfo = $"错误！检查更新失败！\r\n\r\n异常信息如下：\r\n{future.Exception}";
                    return;
                }
                future.Session.Write(new ClientInfo {AppBranchID = -1, MachineID = "MachineID"});
                UpdateInfoChecked = true;
            }).ContinueWith(t =>
            {
                IsBusy = false;
                CheckUpdateCommand.RaiseCanExecuteChanged();
                SetupUpdateCommand.RaiseCanExecuteChanged();
            });
        }

        #endregion

        #region SetupUpdateCommand

        /// <summary>
        ///  <see cref="SetupUpdateCommand" /> 命令的名称.
        /// </summary>
        private DelegateCommand _setupUpdateCommand;

        /// <summary>
        /// 获取 SetupUpdateCommand 命令.
        /// </summary>
        public DelegateCommand SetupUpdateCommand
        {
            get
            {
                return _setupUpdateCommand ?? (_setupUpdateCommand =
                    DelegateCommand.FromAsyncHandler(SetupUpdateCommandExecute, CanSetupUpdateCommandExecute));
            }
        }

        private bool CanSetupUpdateCommandExecute()
        {
            return UpdateInfoChecked && !IsBusy;
        }

        private Task SetupUpdateCommandExecute()
        {
            if (!SetupUpdateCommand.CanExecute())
            {
                return null;
            }
            //TODO 添加SetupUpdateCommand命令的Execute代码.
            return Task.Run(() =>
            {
                throw new NotImplementedException();
            });
        }

        #endregion
    }
}
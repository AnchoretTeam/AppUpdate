using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using AppUpdate.Core;
using AppUpdate.Core.Network.Filter.Codec.Demux;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Mina.Core.Service;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Mina.Filter.Logging;
using Mina.Transport.Socket;

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
        public IoConnector _connector;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public ShellViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                var demuxingCodec = new DemuxingProtocolCodecFactory();
                demuxingCodec.AddMessageEncoder<IList<IFileHash>, FileHashesProtocolEncoder>();

                _connector = new AsyncSocketConnector();
                _connector.FilterChain.AddLast("logger", new LoggingFilter());
                _connector.FilterChain.AddLast("codec", new ProtocolCodecFilter(demuxingCodec));
                //_connector.Connect(new IPEndPoint(RemoteIPAddress, RemotePort));
            }
        }

        #region  RemoteIPAddress

        private IPAddress _remoteIPAddress = IPAddress.Loopback;

        /// <summary>
        /// 获取或设置 RemoteIPAddress 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
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
            return true;
        }

        private Task CheckUpdateCommandExecute()
        {
            if (!CheckUpdateCommand.CanExecute())
            {
                return null;
            }
            //TODO 添加CheckUpdateCommand命令的Execute代码.
            UpdateInfoChecked = true;
            return Task.Run(() =>
            {
                throw new NotImplementedException();
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
            return true;
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
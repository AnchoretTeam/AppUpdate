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
        /// ��ȡ������ RemoteIPAddress ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
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
        /// ��ȡ������ RemotePort ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
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
        /// ��ȡ������ Logs ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        public ObservableCollection<string> Logs
        {
            get { return _logs; }
            set { SetProperty(ref _logs, value); }
        }

        #endregion

        #region  UpdateInfo

        private string _updateInfo = "�������...";

        /// <summary>
        /// ��ȡ������ UpdateInfo ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
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
        /// ��ȡ������ UpdateInfoChecked ����.
        /// �޸�����ֵ�ᴥ�� PropertyChanged �¼�. 
        /// </summary>
        public bool UpdateInfoChecked
        {
            get { return _updateInfoChecked; }
            set { SetProperty(ref _updateInfoChecked, value); }
        }

        #endregion

        #region CheckUpdateCommand

        /// <summary>
        ///  <see cref="CheckUpdateCommand" /> ���������.
        /// </summary>
        private DelegateCommand _checkUpdateCommand;

        /// <summary>
        /// ��ȡ CheckUpdateCommand ����.
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
            //TODO ���CheckUpdateCommand�����Execute����.
            UpdateInfoChecked = true;
            return Task.Run(() =>
            {
                throw new NotImplementedException();
            });
        }

        #endregion

        #region SetupUpdateCommand

        /// <summary>
        ///  <see cref="SetupUpdateCommand" /> ���������.
        /// </summary>
        private DelegateCommand _setupUpdateCommand;

        /// <summary>
        /// ��ȡ SetupUpdateCommand ����.
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
            //TODO ���SetupUpdateCommand�����Execute����.
            return Task.Run(() =>
            {
                throw new NotImplementedException();
            });
        }

        #endregion
    }
}
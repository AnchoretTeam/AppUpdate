using System;
using System.Net;
using Microsoft.Practices.Prism.Mvvm;

namespace AppUpdateServer.Models
{
    internal sealed class ClientInfo : BindableBase, IClientInfoBindable
    {
        #region  MachineID

        // ReSharper disable once InconsistentNaming
        private string _machineID = string.Empty;

        /// <summary>
        /// 获取或设置 MachineID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string MachineID
        {
            get { return _machineID; }
            set { SetProperty(ref _machineID, value); }
        }

        #endregion

        #region  ClientName

        private string _clientName = string.Empty;

        /// <summary>
        /// 获取或设置 ClientName 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string ClientName
        {
            get { return _clientName; }
            set { SetProperty(ref _clientName, value); }
        }

        #endregion

        #region  Company

        private string _company = string.Empty;

        /// <summary>
        /// 获取或设置 Company 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string Company
        {
            get { return _company; }
            set { SetProperty(ref _company, value); }
        }

        #endregion

        #region  AppBranch

        private AppBranch _appBranch;

        /// <summary>
        /// 获取或设置 AppBranch 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public AppBranch AppBranch
        {
            get { return _appBranch; }
            set
            {
                SetProperty(ref _appBranch, value);
                OnPropertyChanged("AppBranchID");
            }
        }

        #endregion

        public int AppBranchID
        {
            get
            {
                if (_appBranch == null)
                {
                    return -1;
                }
                return _appBranch.AppBranchID;
            }
        }

        #region  IPAddress

        private IPAddress _ipAddress = IPAddress.Loopback;

        /// <summary>
        /// 获取或设置 IpAddress 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public IPAddress IPAddress
        {
            get { return _ipAddress; }
            set { SetProperty(ref _ipAddress, value); }
        }

        #endregion

        #region  RsaPrivateKey

        private string _rsaPrivateKey;

        /// <summary>
        /// 获取或设置 RsaPrivateKey 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string RsaPrivateKey
        {
            get { return _rsaPrivateKey; }
            set { SetProperty(ref _rsaPrivateKey, value); }
        }

        #endregion

        #region  Expiration

        private DateTime _expiration = DateTime.MinValue;

        /// <summary>
        /// 获取或设置 Expiration 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public DateTime Expiration
        {
            get { return _expiration; }
            set { SetProperty(ref _expiration, value); }
        }

        #endregion

        #region  Serial

        private string _serial;

        /// <summary>
        /// 获取或设置 Serial 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string Serial
        {
            get { return _serial; }
            set { SetProperty(ref _serial, value); }
        }

        #endregion

        #region  SetupLocation

        private string _setupLocation = string.Empty;

        /// <summary>
        /// 获取或设置 SetupLocation 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string SetupLocation
        {
            get { return _setupLocation; }
            set { SetProperty(ref _setupLocation, value); }
        }

        #endregion
    }
}

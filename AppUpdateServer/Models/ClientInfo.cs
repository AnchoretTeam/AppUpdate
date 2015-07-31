using System;
using System.Net;
using Microsoft.Practices.Prism.Mvvm;

namespace AppUpdateServer.Models
{
    internal sealed class ClientInfo : BindableBase, IClientInfoBindable
    {
        #region  MachineID

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

        #region  AppBranchID

        private string _appBranchID = string.Empty;

        /// <summary>
        /// 获取或设置 AppBranchID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string AppBranchID
        {
            get { return _appBranchID; }
            set { SetProperty(ref _appBranchID, value); }
        }

        #endregion

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

        private byte[] _rsaPrivateKey = null;

        /// <summary>
        /// 获取或设置 RsaPrivateKey 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public byte[] RsaPrivateKey
        {
            get { return _rsaPrivateKey; }
            set { SetProperty(ref _rsaPrivateKey, value); }
        }

        #endregion

        #region  Expiration

        private DateTime _expiration= DateTime.MinValue;

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

        private byte[] _serial = null;

        /// <summary>
        /// 获取或设置 Serial 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public byte[] Serial
        {
            get { return _serial; }
            set { SetProperty(ref _serial, value); }
        }

        #endregion
    }

    internal sealed class AppSeries : BindableBase
    {
        #region  AppSeriesID

        // ReSharper disable once InconsistentNaming
        private int _appSeriesID = -1;

        /// <summary>
        /// 获取或设置 AppSeriesID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public int AppSeriesID
        {
            get { return _appSeriesID; }
            set { SetProperty(ref _appSeriesID, value); }
        }

        #endregion

        #region  AppSeriesName

        private string _appSeriesName = string.Empty;

        /// <summary>
        /// 获取或设置 AppSeriesName 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string AppSeriesName
        {
            get { return _appSeriesName; }
            set { SetProperty(ref _appSeriesName, value); }
        }

        #endregion

        #region  AppSeriesFriendlyDescription

        private string _appSeriesFriendlyDescription = string.Empty;

        /// <summary>
        /// 获取或设置 AppSeriesFriendlyDescription 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public string AppSeriesFriendlyDescription
        {
            get { return _appSeriesFriendlyDescription; }
            set { SetProperty(ref _appSeriesFriendlyDescription, value); }
        }

        #endregion
    }

    internal sealed class AppBranches : BindableBase
    {
        #region  AppBranchID

        private int _appBranchID = -1;

        /// <summary>
        /// 获取或设置 AppBranchID 属性.
        /// 修改属性值会触发 PropertyChanged 事件. 
        /// </summary>
        public int AppBranchID
        {
            get { return _appBranchID; }
            set { SetProperty(ref _appBranchID, value); }
        }

        #endregion
    }
}

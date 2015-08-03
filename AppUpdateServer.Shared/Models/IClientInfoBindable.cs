using System;
using System.ComponentModel;
using System.Net;
using AppUpdate.Core.Models;

namespace AppUpdateServer.Models
{
    public interface IClientInfoBindable : IClientInfo, INotifyPropertyChanged
    {
        string ClientName { get; }
        string Company { get; }
        IAppBranch AppBranch { get; }
        IPAddress IPAddress { get; }
        string RsaPrivateKey { get; }
        DateTime Expiration { get; }
        string SetupLocation { get; }
    }
}
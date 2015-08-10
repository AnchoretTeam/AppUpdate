using System;
using System.ComponentModel;
using System.Net;
using AppUpdate.Core.Models;

namespace AppUpdateServer.Models
{
    public interface IClientInfoBindable : IClientInfo, IAppDefinitionItem
    {
        string ClientName { get; }
        string Company { get; }
        IAppBranch AppBranch { get; }
        // ReSharper disable once InconsistentNaming
        IPAddress IPAddress { get; }
        string RsaPrivateKey { get; }
        DateTime Expiration { get; }
        string SetupLocation { get; }
    }
}
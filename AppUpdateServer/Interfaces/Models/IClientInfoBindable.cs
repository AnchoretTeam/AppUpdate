using System.ComponentModel;
using AppUpdate.Core.Models;

namespace AppUpdateServer.Models
{
    internal interface IClientInfoBindable : IClientInfo, INotifyPropertyChanged
    {
        
    }
}
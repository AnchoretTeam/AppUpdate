using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Mina.Core.Service;

namespace AppUpdateServer.Model
{
    public sealed class AppUpdateClient:ObservableObject,IDisposable
    {
        //private ConnectedSocket _connectedSocket;
        //private AppUpdateClient()
        //{
        //}

        //public static AppUpdateClient CreateFromConnectedSocket(ConnectedSocket socket)
        //{
        //    var client = new AppUpdateClient { _connectedSocket = socket }; 
        //    return client;
        //}

        public void Dispose()
        {
            //_connectedSocket?.Dispose();
        }
    }
}

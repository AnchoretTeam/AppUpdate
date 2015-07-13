using System.Collections.Concurrent;
using System.Net;
using GalaSoft.MvvmLight;
using System.Threading;
using AppUpdateServer.Model;
using AppUpdateServer.Network.Filter.Codec.Demux;
using Mina.Core.Service;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Mina.Filter.Logging;
using Mina.Transport.Socket;

namespace AppUpdateServer.ViewModel
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
    public class MainViewModel : ViewModelBase
    {
        private readonly IoAcceptor _acceptor;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (!IsInDesignMode)
            {
                var demuxingCodec = new DemuxingProtocolCodecFactory();
                demuxingCodec.AddMessageEncoder<FileDescription, FileDescriptionProtocolEncoder>();

                _acceptor = new AsyncSocketAcceptor();
                _acceptor.FilterChain.AddLast("logger", new LoggingFilter());
                _acceptor.FilterChain.AddLast("codec", new ProtocolCodecFilter(demuxingCodec));
                _acceptor.Bind(new IPEndPoint(IPAddress.Any, 2001));
            }
        }

        public override void Cleanup()
        {
            _acceptor.Unbind();
            _acceptor.Dispose();
            base.Cleanup();
        }
    }
}
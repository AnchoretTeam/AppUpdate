using System.Windows;
using AppUpdate.Core.Models;
using AppUpdate.Core.Network.Filter.Codec.Demux;
using AppUpdate.Core.Network.MessageHandlers;
using AppUpdateServer.Modules;
using AppUpdateServer.Services;
using AppUpdateServer.Views;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Mina.Core.Service;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Mina.Filter.Logging;
using Mina.Handler.Demux;
using Mina.Transport.Socket;

namespace AppUpdateServer
{
    sealed class Bootstrapper : UnityBootstrapper
    {        /// <summary>
             /// 初始化Socket服务
             /// </summary>
        private void InitializeSocketService()
        {
            var acceptor = new AsyncSocketAcceptor();
            acceptor.SessionConfig.ReadBufferSize = 2048;
            acceptor.SessionConfig.SetIdleTime(IdleStatus.BothIdle, 10);

            // 添加过滤器
            var demuxingCodec = new DemuxingProtocolCodecFactory();
            demuxingCodec.AddMessageEncoder<ITransferingZipFile, TransferingZipFileProtocolEncoder>();
            demuxingCodec.AddMessageEncoder<IUpdateFileCollection, UpdateFileCollectionProtocolEncoder>();
            demuxingCodec.AddMessageDecoder<ClientInfoProtocolDecoder>();
            demuxingCodec.AddMessageDecoder<FileHashesProtocolDecoder>();
            acceptor.FilterChain.AddLast("codec", new ProtocolCodecFilter(demuxingCodec));
            acceptor.FilterChain.AddLast("logger", new LoggingFilter());

            // 添加业务逻辑
            var demuxingHandler = new DemuxingIoHandler();
            demuxingHandler.AddReceivedMessageHandler(new ClientInfoMessageHandler((s, o) =>
            {
                // TODO 服务器收到客户端发来的客户端信息后，校验MachineID，AppBranchID，
                // Serial，Expiration等信息（见数据库,见AppUpdateServer.Models.ClientInfo类），
                // 将更新文件名列表发给客户端
            }));
            demuxingHandler.AddReceivedMessageHandler(new FileHashesMessageHandler((s, o) =>
            {
                // TODO 服务器收到客户端发来的文件Hash后，在服务器端对比Hash，并将Hash不同的文件打包成压缩包发送给客户端
            }));
            acceptor.Handler = demuxingHandler;

            Container.RegisterInstance<IoAcceptor>(acceptor, new ContainerControlledLifetimeManager());
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            InitializeSocketService();
            Container.RegisterInstance<IClientListService>(new ClientListService());
        }
        
        protected override void ConfigureModuleCatalog()
        {
            var regionType = typeof(WorkspaceModule);
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = regionType.Name,
                ModuleType = regionType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
            regionType = typeof(ClientListModule);
            ModuleCatalog.AddModule(new ModuleInfo
            {
                ModuleName = regionType.Name,
                ModuleType = regionType.AssemblyQualifiedName,
                InitializationMode = InitializationMode.WhenAvailable
            });
        }

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }
    }
}

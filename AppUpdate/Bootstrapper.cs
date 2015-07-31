using System.Collections.Generic;
using System.IO;
using System.Windows;
using AppUpdate.Core;
using AppUpdate.Core.Helpers;
using AppUpdate.Core.Models;
using AppUpdate.Core.Network.Filter.Codec.Demux;
using AppUpdate.Core.Network.MessageHandlers;
using AppUpdate.Events;
using AppUpdate.Properties;
using AppUpdate.Views;
using Microsoft.Practices.Prism.PubSubEvents;
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

namespace AppUpdate
{
    sealed class Bootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// 初始化Socket服务
        /// </summary>
        private void InitializeSocketService()
        {
            var connector = new AsyncSocketConnector();
            connector.SessionConfig.ReadBufferSize = 2048;
            connector.SessionConfig.SetIdleTime(IdleStatus.BothIdle, 10);

            // 添加过滤器
            var demuxingCodec = new DemuxingProtocolCodecFactory();
            demuxingCodec.AddMessageEncoder<IClientInfo, ClientInfoProtocolEncoder>();
            demuxingCodec.AddMessageEncoder<IList<IFileHash>, FileHashesProtocolEncoder>();
            demuxingCodec.AddMessageDecoder<TransferingZipFileProtocolDecoder>();
            demuxingCodec.AddMessageDecoder<UpdateFileCollectionProtocolDecoder>();
            connector.FilterChain.AddLast("codec", new ProtocolCodecFilter(demuxingCodec));
            connector.FilterChain.AddLast("logger", new LoggingFilter());

            // 添加业务逻辑
            var demuxingHandler = new DemuxingIoHandler();
            demuxingHandler.AddReceivedMessageHandler(new UpdateFileCollectionMessageHandler((s, o) =>
            {
                //客户端收到服务器发来的更新文件名列表后,显示更新信息，分析本地文件、计算本地文件Hash，并发给服务器

                //通知ViewModel更新UpdateInfo属性
                var aggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
                aggregator.GetEvent<UpdateFileCollectionEvent>().Publish(o);

                //获取目标应用路径的绝对路径
                var appFolder = Settings.Default.TargetApplicationFolder;
                appFolder = Path.GetFullPath(appFolder);
                if (!Directory.Exists(appFolder))
                {
                    // TODO 目标目录不存在
                    //MessageBox.Show("目标目录不存在");
                }

                //计算Hash
                var fileHashes = new List<IFileHash>();
                foreach (var file in o)
                {
                    var filePath = Path.Combine(appFolder, file);
                    var fileHash = new FileHash { FileName = file };
                    if (File.Exists(filePath))
                    {
                        fileHash.HashBytes = FileHashHelper.ComputeFileHash(filePath);
                    }
                    fileHashes.Add(fileHash);
                }
                s.Write(fileHashes);
            }));
            demuxingHandler.AddReceivedMessageHandler(new TransferingZipFileMessageHandler((s, o) =>
            {
                // TODO 客户端收到服务器发来的更新文件压缩包后,停止正在运行的应用程序,备份旧文件，覆盖新文件

            }));
            connector.Handler = demuxingHandler;

            Container.RegisterInstance<IoConnector>(connector, new ContainerControlledLifetimeManager());
        }
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            InitializeSocketService();
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

using System;
using System.Collections.Generic;
using AppUpdate.Core.Models;
using Mina.Core.Session;
using Mina.Handler.Demux;

namespace AppUpdate.Core.Network.MessageHandlers
{
    public sealed class FileHashesMessageHandler : MessageHandler<IList<IFileHash>>
    {
        public FileHashesMessageHandler(Action<IoSession, IList<IFileHash>> act)
            : base(act)
        {
        }
        public override void HandleMessage(IoSession session, IList<IFileHash> message)
        {
            base.HandleMessage(session, message);
            //Trace.WriteLine(message);

        }
    }
}

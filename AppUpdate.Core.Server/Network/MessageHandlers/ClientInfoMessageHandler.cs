using System;
using AppUpdate.Core.Models;
using Mina.Core.Session;
using Mina.Handler.Demux;

namespace AppUpdate.Core.Network.MessageHandlers
{
    public sealed class ClientInfoMessageHandler : MessageHandler<IClientInfo>
    {
        public ClientInfoMessageHandler(Action<IoSession, IClientInfo> act)
            : base(act)
        {
        }
        public override void HandleMessage(IoSession session, IClientInfo message)
        {
            base.HandleMessage(session, message);
            //Trace.WriteLine(message);

        }
    }
}

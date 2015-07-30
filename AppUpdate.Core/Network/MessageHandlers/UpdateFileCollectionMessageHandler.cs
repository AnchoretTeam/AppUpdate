using System;
using Mina.Core.Session;
using Mina.Handler.Demux;

namespace AppUpdate.Core.Network.MessageHandlers
{
    public sealed class UpdateFileCollectionMessageHandler : MessageHandler<IUpdateFileCollection>
    {
        public UpdateFileCollectionMessageHandler(Action<IoSession, IUpdateFileCollection> act)
            : base(act)
        {
        }
        public override void HandleMessage(IoSession session, IUpdateFileCollection message)
        {
            base.HandleMessage(session, message);
            //Trace.WriteLine(message);

        }
    }
}

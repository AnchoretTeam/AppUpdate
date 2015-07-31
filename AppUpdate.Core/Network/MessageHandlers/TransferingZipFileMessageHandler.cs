using System;
using AppUpdate.Core.Models;
using Mina.Core.Session;
using Mina.Handler.Demux;

namespace AppUpdate.Core.Network.MessageHandlers
{
    public sealed class TransferingZipFileMessageHandler : MessageHandler<ITransferingZipFile>
    {
        public TransferingZipFileMessageHandler(Action<IoSession, ITransferingZipFile> act)
            : base(act)
        {
        }
        public override void HandleMessage(IoSession session, ITransferingZipFile message)
        {
            base.HandleMessage(session, message);
            //Trace.WriteLine(message);

        }
    }
}

using System;
using System.Collections.Generic;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class FileDescriptionsProtocolEncoder : IMessageEncoder<IList<IFileHash>>
    {
        public void Encode(IoSession session, IList<IFileHash> message, IProtocolEncoderOutput output)
        {
            // TODO Client->Server 所有文件的校验值
            throw new NotImplementedException();
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session,(IList<IFileHash>)message,output);
        }
    }
}

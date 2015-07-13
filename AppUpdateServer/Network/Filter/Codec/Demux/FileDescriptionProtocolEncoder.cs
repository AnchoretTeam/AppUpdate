using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppUpdateServer.Model;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;

namespace AppUpdateServer.Network.Filter.Codec.Demux
{
    public sealed class FileDescriptionProtocolEncoder : IMessageEncoder<FileDescription>
    {
        public void Encode(IoSession session, FileDescription message, IProtocolEncoderOutput output)
        {
            throw new NotImplementedException();
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            throw new NotImplementedException();
        }
    }
}

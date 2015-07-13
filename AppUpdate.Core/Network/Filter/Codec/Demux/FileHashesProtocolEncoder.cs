using System;
using System.Collections.Generic;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class FileHashesProtocolEncoder : IMessageEncoder<IList<IFileHash>>
    {
        public void Encode(IoSession session, IList<IFileHash> message, IProtocolEncoderOutput output)
        {
            // TODO Client->Server 所有文件的校验值
            var buffer = IoBuffer.Allocate(30);
            buffer.AutoExpand = true;
            //写代码

            buffer.Flip();
            output.Write(buffer);
            throw new NotImplementedException();
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session,(IList<IFileHash>)message,output);
        }
    }


}

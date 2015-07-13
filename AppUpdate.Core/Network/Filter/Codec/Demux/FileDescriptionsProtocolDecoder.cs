using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class FileDescriptionsProtocolDecoder : IMessageDecoder
    {
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            throw new NotImplementedException();
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            // TODO Client->Server 所有文件的校验值
            throw new NotImplementedException();
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
            return;
        }
    }
}

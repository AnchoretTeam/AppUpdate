using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mina.Core.Buffer;
using Mina.Filter.Codec.Demux;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class UpdateFileCollectionProtocolEncoder : IMessageEncoder<IUpdateFileCollection>
    {

        public void Encode(IoSession session, IUpdateFileCollection message, IProtocolEncoderOutput output)
        {
            IoBuffer buffer = IoBuffer.Allocate(30);
            buffer.AutoExpand = true;
            buffer.Put((byte)MessageType.Update_UpdateFileCollection);
            buffer.PutString(JsonConvert.SerializeObject(message), Encoding.UTF8);
            buffer.Flip();
            output.Write(buffer);
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session, message as IUpdateFileCollection, output);
        }
    }
}

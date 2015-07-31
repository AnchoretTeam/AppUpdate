using System.Text;
using AppUpdate.Core.Models;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class ClientInfoProtocolEncoder:IMessageEncoder<IClientInfo>
    {
        public void Encode(IoSession session, IClientInfo message, IProtocolEncoderOutput output)
        {
            var buffer = IoBuffer.Allocate(30);
            buffer.AutoExpand = true;
            buffer.Put((byte)MessageType.Update_UpdateInfo);
            buffer.PutString(JsonConvert.SerializeObject(message), Encoding.UTF8);
            buffer.Flip();
            output.Write(buffer);
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session,(IClientInfo)message,output);
        }
    }
}

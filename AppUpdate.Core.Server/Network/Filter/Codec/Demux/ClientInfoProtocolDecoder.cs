using System.Text;
using AppUpdate.Core.Models;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class ClientInfoProtocolDecoder : IMessageDecoder
    {
        private IClientInfo _appUdateInfo;
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            if ((MessageType)input.Get() != MessageType.Update_UpdateInfo)
            {
                return MessageDecoderResult.NotOK;
            }
            _appUdateInfo = (IClientInfo)JsonConvert.DeserializeObject(input.GetString(Encoding.UTF8));
            return MessageDecoderResult.OK;
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            output.Write(_appUdateInfo);
            return MessageDecoderResult.OK;
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
        }
    }
}

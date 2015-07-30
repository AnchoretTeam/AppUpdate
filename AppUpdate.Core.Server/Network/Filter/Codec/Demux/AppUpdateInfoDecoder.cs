using System.Text;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class AppUpdateInfoDecoder:IMessageDecoder
    {
        private IAppUpdateInfo _appUdateInfo;
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            if ((MessageType)input.Get()!=MessageType.Update_UpdateInfo)
            {
                return MessageDecoderResult.NotOK;
            }
            _appUdateInfo =(IAppUpdateInfo)JsonConvert.DeserializeObject(input.GetString(Encoding.UTF8));
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

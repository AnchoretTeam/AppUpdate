using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class AppUpdateInfoDecoder:IMessageDecoder
    {
        private IAppUpdateInfo appUdateInfo;
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            if ((MessageType)input.Get()!=MessageType.Update_UpdateInfo)
            {
                return MessageDecoderResult.NotOK;
            }
            appUdateInfo =(IAppUpdateInfo)JsonConvert.DeserializeObject(input.GetString(Encoding.UTF8));
            return MessageDecoderResult.OK;
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            output.Write(appUdateInfo);
            return MessageDecoderResult.OK;
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
            return;
        }
    }
}

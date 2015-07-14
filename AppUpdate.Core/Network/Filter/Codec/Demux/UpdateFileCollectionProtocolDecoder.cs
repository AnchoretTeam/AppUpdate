using System.Text;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    /// <summary>
    /// [由Server编码]Server发给Client升级文件信息，包含要升级文件的名称
    /// </summary>
    public sealed class UpdateFileCollectionProtocolDecoder : IMessageDecoder
    {
        private IUpdateFileCollection _decodeMessage;
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            var type = (MessageType)input.Get();
            if (type == MessageType.Update_UpdateFileCollection)
            {
                var message = input.GetString(Encoding.UTF8);
                _decodeMessage = JsonConvert.DeserializeObject<IUpdateFileCollection>(message);
                return MessageDecoderResult.OK;
            }
            return MessageDecoderResult.NotOK;
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            output.Write(_decodeMessage);
            return MessageDecoderResult.OK;
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
            return;
        }
    }
}

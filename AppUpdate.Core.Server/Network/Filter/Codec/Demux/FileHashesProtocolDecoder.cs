using System.Collections.Generic;
using System.Text;
using AppUpdate.Core.Models;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    /// <summary>
    /// [由Server解码]Client发给Server所有文件的校验值
    /// </summary>
    public sealed class FileHashesProtocolDecoder : IMessageDecoder
    {
        private IList<IFileHash> _decodeMessage;
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            var type=(MessageType)input.Get();
            if (type==MessageType.Update_FileHash)
            {
                var message = input.GetString(Encoding.UTF8);
                _decodeMessage=JsonConvert.DeserializeObject<IList<IFileHash>>(message);
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
        }
    }
}

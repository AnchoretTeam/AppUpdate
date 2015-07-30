using System.Collections.Generic;
using System.Text;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    /// <summary>
    /// [由Client编码]Client发给Server所有文件的校验值
    /// </summary>
    public sealed class FileHashesProtocolEncoder : IMessageEncoder<IList<IFileHash>>
    {
        public void Encode(IoSession session, IList<IFileHash> message, IProtocolEncoderOutput output)
        {
            var buffer = IoBuffer.Allocate(30);
            buffer.AutoExpand = true;
            buffer.Put((byte) MessageType.Update_FileHash);
            buffer.PutString(JsonConvert.SerializeObject(message),Encoding.UTF8);
            buffer.Flip();
            output.Write(buffer);
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session,(IList<IFileHash>)message,output);
        }
    }

}

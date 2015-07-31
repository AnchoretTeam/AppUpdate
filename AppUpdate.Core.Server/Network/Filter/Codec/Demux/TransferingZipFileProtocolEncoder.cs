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
    /// [由Server编码]Server发给Client升级文件压缩包，包含压缩包信息
    /// </summary>
    public sealed class TransferingZipFileProtocolEncoder : IMessageEncoder<ITransferingZipFile>
    {
        public void Encode(IoSession session, ITransferingZipFile message, IProtocolEncoderOutput output)
        {
            var buffer = IoBuffer.Allocate(30);
            buffer.AutoExpand = true;

            buffer.Put((byte)MessageType.Update_ZipFiles);
            var zipFileInfo = message.ZipFileInfo;
            buffer.PutString(JsonConvert.SerializeObject(zipFileInfo), Encoding.UTF8);
            buffer.Put(message.ZipBytes);
            buffer.Flip();
            output.Write(buffer);
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session, (ITransferingZipFile)message, output);
        }


    }
}

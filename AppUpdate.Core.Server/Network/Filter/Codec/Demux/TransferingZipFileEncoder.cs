using System.IO;
using System.Text;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    /// <summary>
    /// [由Server编码]Server发给Client升级文件压缩包，包含要升级的所有文件
    /// </summary>
    public sealed class TransferingZipFileEncoder : IMessageEncoder<ITransferingZipFile>
    {
        public void Encode(IoSession session, ITransferingZipFile message, IProtocolEncoderOutput output)
        {
            var buffer = IoBuffer.Allocate(30);
            buffer.AutoExpand = true;

            buffer.Put((byte)MessageType.Update_ZipFiles);
            var zipFileInfo = message.ZippingFiles();
            buffer.PutString(JsonConvert.SerializeObject(zipFileInfo), Encoding.UTF8);
            buffer.Put(message.TrasferingZipBytes);
            buffer.Flip();
            output.Write(buffer);
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session, (ITransferingZipFile)message, output);
        }


    }
}

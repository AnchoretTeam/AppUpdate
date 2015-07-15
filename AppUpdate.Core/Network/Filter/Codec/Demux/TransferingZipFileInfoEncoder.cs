using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mina.Core.Buffer;
using Mina.Filter.Codec.Demux;
using Mina.Filter.Codec;
using Mina.Core.Session;
using Newtonsoft.Json;


namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class TransferingZipFileInfoEncoder:IMessageEncoder<IFilesList>
    {

        public void Encode(IoSession session, IFilesList message, IProtocolEncoderOutput output)
        {
            IoBuffer buffer=IoBuffer.Allocate(30);
            buffer.AutoExpand = true;

            buffer.Put((byte)MessageType.Update_ZipFiles);
            using (MemoryStream ms=new MemoryStream())
            {
                string fileInfo = JsonConvert.SerializeObject(message.ZippingFiles(ms));
                buffer.PutString(fileInfo, Encoding.UTF8);
                buffer.Put(ms.ToArray());
                buffer.Flip();
                output.Write(buffer);
            }
        }

        public void Encode(IoSession session, object message, IProtocolEncoderOutput output)
        {
            Encode(session, (IFilesList)message, output);
        }

        
    }
}

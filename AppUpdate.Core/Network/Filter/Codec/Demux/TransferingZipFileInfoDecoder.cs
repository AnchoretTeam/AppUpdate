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
    public sealed class TransferingZipFileInfoDecoder:IMessageDecoder
    {
        private ITransferingZipFileInfo zipFileInfoMessage;
        public MessageDecoderResult Decodable(IoSession session,IoBuffer input)
        {
            if ((MessageType)input.Get()==MessageType.Update_ZipFiles)
            {
                string messageStr = input.GetString(Encoding.UTF8);
                zipFileInfoMessage= JsonConvert.DeserializeObject<ITransferingZipFileInfo>(messageStr);
                var fileSize= zipFileInfoMessage.FileSize;
                var hashBytes = zipFileInfoMessage.HashBytes;
                var filesBytes=input.GetRemaining().Array;
                if (fileSize==filesBytes.Length)
                {
                    if (FileHashHelper.CompareHashValue(FileHashHelper.ComputeFileHash(filesBytes),hashBytes))
                    {
                        return MessageDecoderResult.OK;
                    }
                    return MessageDecoderResult.NotOK;
                    
                }
                if (fileSize<filesBytes.Length)
                {
                    return MessageDecoderResult.NeedData;
                }
                //fileSize>filesBytes.Length???
            }
            return MessageDecoderResult.NotOK;
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input,IProtocolDecoderOutput output)
        {
            output.Write(zipFileInfoMessage);
            return MessageDecoderResult.OK;
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
            return;
        }

    }
}

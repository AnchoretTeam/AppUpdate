using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mina.Core.Buffer;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.Demux;
using Newtonsoft.Json;

namespace AppUpdate.Core.Network.Filter.Codec.Demux
{
    public sealed class TransferingZipFileInfo : ITransferingZipFileInfo
    {
        public long FileSize { get; set; }
        public byte[] HashBytes { get; set; }
    }

    public sealed class TransferingZipFile :  ITransferingZipFile
    {
        public TransferingZipFile(){}
        public TransferingZipFile(string transferingZipDir)
        {
            AppDirectory = transferingZipDir;
        }
        public string AppDirectory { get; set; }
        public byte[] TrasferingZipBytes { get; set; }

        public ITransferingZipFileInfo ZippingFiles()
        {
            var output= FileZipHelper.ZippingUpdateFiles(AppDirectory);
            TrasferingZipBytes = ((MemoryStream) output).ToArray();
            var zipFileInfo=new TransferingZipFileInfo();
            zipFileInfo.FileSize = TrasferingZipBytes.Length;
            zipFileInfo.HashBytes = FileHashHelper.ComputeFileHash(output);
            return zipFileInfo;
        }
    }

    /// <summary>
    /// [由Client解码]Server发给Client升级文件压缩包，包含要升级的所有文件
    /// </summary>
    public sealed class TransferingZipFileDecoder : IMessageDecoder
    {
        private ITransferingZipFile _zipFileInfoMessage;
        public MessageDecoderResult Decodable(IoSession session, IoBuffer input)
        {
            if ((MessageType)input.Get() != MessageType.Update_ZipFiles)
            {
                return MessageDecoderResult.NotOK;
            }
            _zipFileInfoMessage = new TransferingZipFile();

            var zipFileInfo = JsonConvert.DeserializeObject<TransferingZipFileInfo>(input.GetString(Encoding.UTF8));
            var fileSize = zipFileInfo.FileSize;
            var hashBytes = zipFileInfo.HashBytes;
            if (input.Remaining < fileSize)
            {
                return MessageDecoderResult.NeedData;
            }

            var filesBytes = new byte[fileSize];
            input.Get(filesBytes, 0, (int)fileSize);

            if (FileHashHelper.CompareHashValue(FileHashHelper.ComputeFileHash(filesBytes), hashBytes))
            {
                 _zipFileInfoMessage.TrasferingZipBytes = filesBytes;
                return MessageDecoderResult.OK;
            }
            return MessageDecoderResult.NotOK;
        }

        public MessageDecoderResult Decode(IoSession session, IoBuffer input, IProtocolDecoderOutput output)
        {
            output.Write(_zipFileInfoMessage);
            return MessageDecoderResult.OK;
        }

        public void FinishDecode(IoSession session, IProtocolDecoderOutput output)
        {
        }

    }
}

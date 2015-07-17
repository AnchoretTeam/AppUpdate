namespace AppUpdate.Core
{
    public sealed class TransferingZipFileInfo : ITransferingZipFileInfo
    {
        public long FileSize { get; set; }
        public byte[] HashBytes { get; set; }
    }
}
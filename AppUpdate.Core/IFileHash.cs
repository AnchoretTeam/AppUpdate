namespace AppUpdate.Core
{
    public interface IFileHash
    {
        string FileName { get; set; }
        byte[] HashBytes { get; set; }
    }

    public sealed class FileHash : IFileHash
    {
        public string FileName { get; set; }
        public byte[] HashBytes { get; set; }
    }
}

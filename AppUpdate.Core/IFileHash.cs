namespace AppUpdate.Core
{
    public interface IFileHash
    {
        string FileName { get; set; }
        byte[] HashBytes { get; set; }
    }
}

namespace AppUpdate.Core
{
    public enum MessageType : byte
    {
        Update_FileHash,
        Update_UpdateFileCollection,
        Update_UpdateInfo,
        Update_ZipFiles,
        Register
    }
}

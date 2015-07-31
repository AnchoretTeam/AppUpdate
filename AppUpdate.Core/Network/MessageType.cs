namespace AppUpdate.Core.Network
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

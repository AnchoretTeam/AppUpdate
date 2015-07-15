namespace AppUpdate.Core
{
    public enum MessageType : byte
    {
        Update_FileHash,
        Update_UpdateFileCollection,
        Update_MachineId,
        Update_ZipFiles,
        Register
    }
}

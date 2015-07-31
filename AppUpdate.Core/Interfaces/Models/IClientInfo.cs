namespace AppUpdate.Core.Models
{
    public interface IClientInfo
    {
        // ReSharper disable once InconsistentNaming
        string MachineID { set; get; }
        // ReSharper disable once InconsistentNaming
        string AppBranchID { set; get; }
        byte[] Serial { set; get; }
    }
}

namespace AppUpdate.Core.Models
{
    public interface IClientInfo
    {
        // ReSharper disable once InconsistentNaming
        string MachineID { set; get; }
        // ReSharper disable once InconsistentNaming
        int AppBranchID { get; }
        string Serial { set; get; }
    }
}

namespace AppUpdate.Core.Models
{
    public interface IClientInfo
    {
        // ReSharper disable once InconsistentNaming
        string MachineID { get; }
        // ReSharper disable once InconsistentNaming
        int AppBranchID { get; }
        string Serial { get; }
    }
}

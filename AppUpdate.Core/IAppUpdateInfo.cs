namespace AppUpdate.Core
{
    public interface IAppUpdateInfo
    {
        // ReSharper disable once InconsistentNaming
        string MachineID { set; get; }
        // ReSharper disable once InconsistentNaming
        string AppBranchID { set; get; }
    }

    public sealed class AppUpdateInfo: IAppUpdateInfo
    {
        public string MachineID { get; set; }
        public string AppBranchID { get; set; }
        //...
    }
}

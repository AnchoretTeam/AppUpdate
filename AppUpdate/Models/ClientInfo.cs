using AppUpdate.Core;
using AppUpdate.Core.Models;

namespace AppUpdate.Models
{
    public sealed class ClientInfo : IClientInfo
    {
        public string MachineID { get; set; }
        public int AppBranchID { get; set; }
        public string Serial { get; set; }
        //...
    }
}

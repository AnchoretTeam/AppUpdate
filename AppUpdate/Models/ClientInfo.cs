using AppUpdate.Core;
using AppUpdate.Core.Models;

namespace AppUpdate.Models
{
    public sealed class ClientInfo : IClientInfo
    {
        public string MachineID { get; set; }
        public string AppBranchID { get; set; }
        public byte[] Serial { get; set; }
        //...
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdate.Core
{
    public interface IAppUpdateInfo
    {
        string MachineID { set; get; }
        string AppBranchID { set; get; }
    }

    public sealed class AppUpdateInfo: IAppUpdateInfo
    {
        public string MachineID { get; set; }
        public string AppBranchID { get; set; }
        //...
    }
}

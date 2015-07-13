using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdate.Core
{
    public enum MessageType : byte
    {
        Update_FileHash,
        Update_UpdateFileCollection,
        Update_MachineId,
        Update_Files,
        Register
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdateServer.Model
{
    public sealed class FileDescription
    {
        public string RelativePathOnClient { get; set; }
        public string RelativePathOnServer { get; set; }
        public long FileSize { get; set; }
        public byte[] CrcHashSign { get; set; }
    }
}

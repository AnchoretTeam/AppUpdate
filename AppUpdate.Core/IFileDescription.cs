using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUpdate.Core
{
    public interface IFileHash
    {
        string FileName { get; set; }
        byte[] HashBytes { get; set; }
    }
}

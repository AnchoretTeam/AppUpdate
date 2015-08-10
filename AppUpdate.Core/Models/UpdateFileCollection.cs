using System;
using System.Collections.Generic;
using System.Windows.Documents;

namespace AppUpdate.Core.Models
{
    public sealed class UpdateFileCollection : List<string>, IUpdateFileCollection
    {
        public string Description { get; set; }
        public UpdateType UpdateType { get; set; }
        public DateTime ReleaseTime { get; set; }
        public void LoadFromFile(string fileName)
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
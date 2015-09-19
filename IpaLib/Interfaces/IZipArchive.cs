using System;
using System.Collections.Generic;

namespace IpaLib.Interfaces {
    public interface IZipArchive : IDisposable{
        IReadOnlyCollection<IZipArchiveEntry> Entries {get;}
    }
}

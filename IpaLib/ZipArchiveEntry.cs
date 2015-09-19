using System.Diagnostics.CodeAnalysis;
using IpaLib.Interfaces;
using IO=System.IO;
using Compression=System.IO.Compression;

namespace IpaLib {

    [ExcludeFromCodeCoverage]
    public class ZipArchiveEntry : IZipArchiveEntry
    {
        private readonly Compression.ZipArchiveEntry _entry;

        public string Name {
            get { return _entry.Name; }
        }

        public string FullName {
            get { return _entry.FullName; }
        }

        public ZipArchiveEntry(Compression.ZipArchiveEntry entry)
        {
            _entry = entry;
        }

        public IO.Stream Open()
        {
            return _entry.Open();
        }
    }
}

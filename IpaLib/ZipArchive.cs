using IpaLib.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using Compression = System.IO.Compression;
using IO = System.IO;

namespace IpaLib {

    [ExcludeFromCodeCoverage]
    public class ZipArchive : IZipArchive {

        private readonly Compression.ZipArchive _zipArchive;

        public ZipArchive(string path) {
            var fileStream = new IO.FileStream(path, IO.FileMode.Open);
            _zipArchive = new Compression.ZipArchive(fileStream);
        }

        public void Dispose() {
            _zipArchive.Dispose();
        }

        public IReadOnlyCollection<IZipArchiveEntry> Entries {
            get {
                return new ReadOnlyCollection<IZipArchiveEntry>(
                    _zipArchive.Entries.Select(i => new ZipArchiveEntry(i)
                    ).ToList<IZipArchiveEntry>());
            }
        }
    }
}

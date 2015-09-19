using System.IO;

namespace IpaLib.Interfaces {
    public interface IZipArchiveEntry {
        string Name { get; }
        string FullName { get; }
        Stream Open();
    }
}

using System;
namespace IpaLib.Interfaces {
    public interface IZipArchiveFactory : IDisposable {
        IZipArchive Create(string path);
    }
}

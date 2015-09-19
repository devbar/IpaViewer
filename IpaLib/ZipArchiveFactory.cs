using IpaLib.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace IpaLib {
    public class ZipArchiveFactory : IZipArchiveFactory{

        /// <summary>
        /// Create new ZipArchive Class
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public IZipArchive Create(string path)
        {
            return new ZipArchive(path);
        }

        public void Dispose() {
        }
    }
}

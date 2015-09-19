using System;
namespace IpaLib.Interfaces {
    public interface IIpaService : IDisposable {
        IIpaFile FromFile(string path);
    }
}

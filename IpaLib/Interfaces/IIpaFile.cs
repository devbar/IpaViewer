using System.Collections.Generic;

namespace IpaLib.Interfaces {
    public interface IIpaFile {
        string ItemName{ get; }
        string Genre { get; }
        string BundleVersion { get; }
        string Kind { get; }
        string PlaylistName { get; }
        bool? SoftwareIconNeedsShine { get; }
        string SoftwareVersionBundleId { get; }
        string ProvisioningProfile { get; }
        IList<string> ProvisionedDevices { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpaLib.Interfaces;

namespace IpaLib
{
    public class IpaFile : IIpaFile
    {
        private readonly IDictionary<string, object> _itunesMetaData;
        private readonly IDictionary<string, object> _embeddedMobProv;

        public IpaFile(IDictionary<string,object> itunesMetaData, IDictionary<string,object> embeddedMobProv)
        {
            _itunesMetaData = itunesMetaData;
            _embeddedMobProv = embeddedMobProv;
        }

        public string ItemName {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("itemname", out itemName))
                    return null;
                
                return (string) itemName;
            }
        }

        public string ProvisioningProfile {
            get {
                if (_embeddedMobProv == null)
                    return null;

                object profile;
                if (!_embeddedMobProv.TryGetValue("name", out profile))
                    return null;

                return (string) profile;
            }
        }

        public IList<string> ProvisionedDevices {
            get {
                if (_embeddedMobProv == null)
                    return null;

                object deviceList;
                if (!_embeddedMobProv.TryGetValue("provisioneddevices", out deviceList))
                    return null;

                return ((IList<object>)deviceList).Select(d => (string)d).ToList();
            }
        }


        public string Genre {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("genre", out itemName))
                    return null;

                return (string)itemName;
            }
        }

        public string BundleVersion {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("bundleversion", out itemName))
                    return null;

                return (string)itemName;
            }
        }

        public string Kind {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("kind", out itemName))
                    return null;

                return (string)itemName;
            }
        }

        public string PlaylistName {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("playlistname", out itemName))
                    return null;

                return (string)itemName;
            }
        }

        public bool? SoftwareIconNeedsShine {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("softwareiconneedsshine", out itemName))
                    return null;

                return (bool?)itemName; 
            }
        }

        public string SoftwareVersionBundleId {
            get {
                if (_itunesMetaData == null)
                    return null;

                object itemName;
                if (!_itunesMetaData.TryGetValue("softwareversionbundleid", out itemName))
                    return null;

                return (string)itemName;
            }
        }
    }
}

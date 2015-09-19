using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IpaLib.Tests {
    [TestClass]
    public class IpaFileTest {
        [TestMethod]
        public void NullValueTest() {
            var ipaFile = new IpaFile(null, null);

            Assert.IsNull(ipaFile.ItemName);
            Assert.IsNull(ipaFile.BundleVersion);
            Assert.IsNull(ipaFile.Genre);
            Assert.IsNull(ipaFile.Kind);
            Assert.IsNull(ipaFile.PlaylistName);
            Assert.IsNull(ipaFile.ProvisionedDevices);
            Assert.IsNull(ipaFile.ProvisioningProfile);
            Assert.IsNull(ipaFile.SoftwareIconNeedsShine);
            Assert.IsNull(ipaFile.SoftwareVersionBundleId);
        }

        [TestMethod]
        public void EmptyValueTest() {
            var ipaFile = new IpaFile(new Dictionary<string,object>(), new Dictionary<string,object>());

            Assert.IsNull(ipaFile.ItemName);
            Assert.IsNull(ipaFile.BundleVersion);
            Assert.IsNull(ipaFile.Genre);
            Assert.IsNull(ipaFile.Kind);
            Assert.IsNull(ipaFile.PlaylistName);
            Assert.IsNull(ipaFile.ProvisionedDevices);
            Assert.IsNull(ipaFile.ProvisioningProfile);
            Assert.IsNull(ipaFile.SoftwareIconNeedsShine);
            Assert.IsNull(ipaFile.SoftwareVersionBundleId);
        }

        [TestMethod]
        public void FilledValueTest() {
            var ipaFile = new IpaFile(new Dictionary<string, object>() {
                {"itemname","A"},
                {"bundleversion","B"},
                {"genre","C"},
                {"kind","D"},
                {"playlistname","E"},
                {"softwareiconneedsshine", true},
                {"softwareversionbundleid","F"},
            }, new Dictionary<string, object>() {
                {"name", "X"},
                {"provisioneddevices", new List<object>() {
                    "1", "2", "3"
                }}
            });

            Assert.AreEqual("A", ipaFile.ItemName);
            Assert.AreEqual("B", ipaFile.BundleVersion);
            Assert.AreEqual("C", ipaFile.Genre);
            Assert.AreEqual("D", ipaFile.Kind);
            Assert.AreEqual("E", ipaFile.PlaylistName);
            Assert.AreEqual(3, ipaFile.ProvisionedDevices.Count);
            Assert.AreEqual("X", ipaFile.ProvisioningProfile);
            Assert.AreEqual(true, ipaFile.SoftwareIconNeedsShine);
            Assert.AreEqual("F", ipaFile.SoftwareVersionBundleId);
        }
    }
}

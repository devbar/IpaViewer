using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using IpaLib.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace IpaLib.Tests {

    [TestClass]
    public class IpaServiceTest
    {

        private IZipArchiveFactory _zipArchiveFactory;
        private IZipArchiveFactory _zipArchiveFactoryEmpty;

        private IZipArchiveFactory CreateArchiveFactory()
        {
            var zipArchiveMock = new Mock<IZipArchive>();
            var zipItunesEntry = new Mock<IZipArchiveEntry>();
            var zipProvProfileEntry = new Mock<IZipArchiveEntry>();
            var zipArchiveFactoryMock = new Mock<IZipArchiveFactory>();

            zipItunesEntry.Setup(e => e.Name).Returns("itunesMetaData.plist");
            zipItunesEntry.Setup(e => e.FullName).Returns(@"anywhere\in\this\ipa\itunesmetadata.plist");
            zipItunesEntry.Setup(e => e.Open()).Returns(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(Resources.SampleBrowser_itunesmetadata)
                    ));

            zipProvProfileEntry.Setup(e => e.Name).Returns("embeDded.mobileprovision");
            zipProvProfileEntry.Setup(e => e.FullName).Returns(@"anywhere\in\this\ipa\itunesmetadata.plist");
            zipProvProfileEntry.Setup(e => e.Open()).Returns(
                new MemoryStream(
                    Encoding.UTF8.GetBytes(Resources.SampleBrowser_embeddedmobileprovision)
                    ));


            zipArchiveMock.Setup(
                a => a.Entries).Returns(
                    new ReadOnlyCollection<IZipArchiveEntry>(new List<IZipArchiveEntry>(){
                        zipItunesEntry.Object,
                        zipProvProfileEntry.Object
                    })
                );

            zipArchiveFactoryMock.Setup(
                z => z.Create(It.Is<string>(
                    s => s == @"c:\temp\myipa.ipa"))).Returns(zipArchiveMock.Object);

           return zipArchiveFactoryMock.Object;
        }

        private IZipArchiveFactory CreateEmptyArchiveFactory() 
        {
            var zipArchiveMock = new Mock<IZipArchive>();
            var zipItunesEntry = new Mock<IZipArchiveEntry>();
            var zipProvProfileEntry = new Mock<IZipArchiveEntry>();
            var zipArchiveFactoryMock = new Mock<IZipArchiveFactory>();

            zipItunesEntry.Setup(e => e.Name).Returns("itunesMetaData.plist");
            zipItunesEntry.Setup(e => e.FullName).Returns(@"anywhere\in\this\ipa\itunesmetadata.plist");
            zipItunesEntry.Setup(e => e.Open()).Returns(
                new MemoryStream(
                    Encoding.UTF8.GetBytes("<dict></dict>")
                    ));

            zipProvProfileEntry.Setup(e => e.Name).Returns("embeDded.mobileprovision");
            zipProvProfileEntry.Setup(e => e.FullName).Returns(@"anywhere\in\this\ipa\itunesmetadata.plist");
            zipProvProfileEntry.Setup(e => e.Open()).Returns(
                new MemoryStream(
                    Encoding.UTF8.GetBytes("<mytag></mytag>")
                    ));


            zipArchiveMock.Setup(
                a => a.Entries).Returns(
                    new ReadOnlyCollection<IZipArchiveEntry>(new List<IZipArchiveEntry>(){
                        zipItunesEntry.Object,
                        zipProvProfileEntry.Object
                    })
                );

            zipArchiveFactoryMock.Setup(
                z => z.Create(It.Is<string>(
                    s => s == @"c:\temp\myipa.ipa"))).Returns(zipArchiveMock.Object);

            return zipArchiveFactoryMock.Object;
        }

        public IpaServiceTest()
        {
            _zipArchiveFactory = CreateArchiveFactory();
            _zipArchiveFactoryEmpty = CreateEmptyArchiveFactory();
        }

        [TestMethod]
        public void IpaFromFile() {
            var ipaService = new IpaService(_zipArchiveFactory);
            var ipaFile = ipaService.FromFile(@"c:\temp\myipa.ipa");

            Assert.AreEqual("software", ipaFile.Kind);
            Assert.AreEqual("1.0", ipaFile.BundleVersion);
            Assert.AreEqual("Application", ipaFile.Genre);
            Assert.AreEqual("SampleBrowser", ipaFile.ItemName);
            Assert.AreEqual("SampleBrowser", ipaFile.PlaylistName);
            Assert.AreEqual(10, ipaFile.ProvisionedDevices.Count);
            Assert.AreEqual(true, ipaFile.SoftwareIconNeedsShine);
            Assert.AreEqual("AdventureWork_Common", ipaFile.ProvisioningProfile);
            Assert.AreEqual("com.your-company.SampleBrowser", ipaFile.SoftwareVersionBundleId);

        }

        [TestMethod]
        public void IpaFromEmptyFile() {
            var ipaService = new IpaService(_zipArchiveFactoryEmpty);
            var ipaFile = ipaService.FromFile(@"c:\temp\myipa.ipa");

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
    }
}

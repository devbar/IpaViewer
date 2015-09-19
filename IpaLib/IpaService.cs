using System;
using System.Collections;
using IpaLib.Interfaces;
using System.IO;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace IpaLib {
    public class IpaService : IIpaService{

        private readonly IZipArchiveFactory _zipArchiveFactory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="zipArchiveFactory">Factory to create a Zip Archive</param>
        public IpaService(IZipArchiveFactory zipArchiveFactory) {
            _zipArchiveFactory = zipArchiveFactory;
        }

        public IIpaFile FromFile(string path)
        {
            using (var zipArchive = _zipArchiveFactory.Create(path)) {
                IDictionary<string, object> dictItunes = null;
                IDictionary<string, object> dictMobileProv = null;


                var itunesMetaData = zipArchive.Entries.FirstOrDefault(entry => entry.Name.ToLower() == "itunesmetadata.plist");
                if (itunesMetaData != null)
                    dictItunes = LoadMetaFromFile(itunesMetaData);

                var embeddedMobileProv = zipArchive.Entries.FirstOrDefault(entry => entry.Name.ToLower() == "embedded.mobileprovision");
                if (embeddedMobileProv != null)
                    dictMobileProv = LoadMetaFromFile(embeddedMobileProv);

                return new IpaFile(dictItunes, dictMobileProv);
            }
        }

        protected IDictionary<string, object> LoadMetaFromFile(IZipArchiveEntry entry)
        {
            var xmlDoc = new XmlDocument();
            var content = new StreamReader(entry.Open()).ReadToEnd();

            var start = content.IndexOf("<dict>", StringComparison.Ordinal);
            var end = content.LastIndexOf("</dict>", StringComparison.Ordinal) + 7;

            if (start < 0) return null;

            xmlDoc.LoadXml(content.Substring(start, end - start));

            XmlNodeList tagsDict;
            tagsDict = xmlDoc.GetElementsByTagName("dict");
            return LoadMetaDataFromNode(tagsDict[0]);
        }

        protected IDictionary<string, object> LoadMetaDataFromNode(XmlNode node)
        {
            var resultDict = new Dictionary<string, object>();
            var nodes = node.ChildNodes;
            for (var i = 0; i < nodes.Count; i++) {
                
                if (nodes[i].Name.ToLower() != "key")
                    continue;

                var key = nodes[i].InnerText.ToLower();
                XmlNode valueNode;
                if ((valueNode = nodes[i].NextSibling) == null)
                    continue;

                switch (valueNode.Name.ToLower()) {
                    case "string":
                        resultDict.Add(key, valueNode.InnerText);
                        break;
                    case "true":
                        resultDict.Add(key, true);
                        break;
                    case "false":
                        resultDict.Add(key, false);
                        break;
                    case "dict":
                        resultDict.Add(key, LoadMetaDataFromNode(valueNode));
                        break;
                    case "array":
                        var list = new List<object>();
                        for(var j = 0; j < valueNode.ChildNodes.Count; j++) {
                            list.Add(valueNode.ChildNodes[j].InnerText);
                        }
                        resultDict.Add(key, list);
                        break;
                }
            }

            return resultDict;
        }

        public void Dispose()
        {
            _zipArchiveFactory.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace CursorAnalyzer.model.repository
{
    class UserRepository
    {
        private readonly string fileName;
        
        public UserRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public List<string> FetchAllUsers()
        {
            byte[] byteStream;

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byteStream = new byte[fileStream.Length];
                fileStream.Read(byteStream, 0, byteStream.Length);
            }

            using (MemoryStream memoryStream = new MemoryStream(byteStream))
            {
                var xmlDocument = new XmlDocument();
                xmlDocument.Load(memoryStream);
                try
                {
                    var root = xmlDocument.DocumentElement;
                    var featuresNode = root.SelectSingleNode("descendant::Features");
                    return (from XmlNode node in featuresNode.SelectNodes("Class") select node.Attributes.GetNamedItem("name").Value).ToList();
                }
                catch (Exception)
                {
                    return new List<string>();
                }
            }
        }
    }
}

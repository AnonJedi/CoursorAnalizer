using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CoursorAnalizer
{
    static class Saver
    {
        #region Var

        private static byte[] DBBytes;
        private static XmlDocument xmlDocument;
        private static string file = "DataBase.xml";
        private static List<string> users;

        #endregion

        public static List<string> ReadDB()
        {
            using (FileStream fileStream = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Read))
            {
                DBBytes = new byte[fileStream.Length];
                fileStream.Read(DBBytes, 0, DBBytes.Length);
            }

            using (MemoryStream memoryStream = new MemoryStream(DBBytes))
            {               
                xmlDocument = new XmlDocument();
                xmlDocument.Load(memoryStream);
            }

            try
            {
                return Parse();
            }
            catch (Exception)
            {
                return new List<string>();
            }
            
        }

        private static List<string> Parse()
        {
            users = new List<string>();
            foreach (XmlNode node in xmlDocument.SelectNodes("/root/user")) users.Add(node.SelectSingleNode("name").InnerText);
            return users;
        }

        public static void Save(string name, List<double> Cmid, List<double> Cmax, List<double> T, List<float[]> ampList)
        {
            XmlNode root = xmlDocument.SelectSingleNode("/root");
            XmlNode user = xmlDocument.CreateElement("user");
            XmlNode n = xmlDocument.CreateElement("name");
            XmlNode cMid;
            XmlNode cMax;
            XmlNode t;
            XmlNode amp1;
            XmlNode amp2;
            XmlNode amp3;

            n.InnerText = name;
            user.AppendChild(n);
            
            foreach (double d in Cmid)
            {
                cMid = xmlDocument.CreateElement("cMid");
                cMid.InnerText = d.ToString();
                user.AppendChild(cMid);
            }

            foreach (double d in Cmax)
            {
                cMax = xmlDocument.CreateElement("cMax");
                cMax.InnerText = d.ToString();
                user.AppendChild(cMax);
            }

            foreach (double d in T)
            {
                t = xmlDocument.CreateElement("T");
                t.InnerText = d.ToString();
                user.AppendChild(t);
            }

            foreach (float[] floats in ampList)
            {
                amp1 = xmlDocument.CreateElement("Amp1");
                amp1.InnerText = floats[0].ToString();
                user.AppendChild(amp1);
            }

            foreach (float[] floats in ampList)
            {
                amp2 = xmlDocument.CreateElement("Amp2");
                amp2.InnerText = floats[1].ToString();
                user.AppendChild(amp2);
            }

            foreach (float[] floats in ampList)
            {
                amp3 = xmlDocument.CreateElement("Amp3");
                amp3.InnerText = floats[2].ToString();
                user.AppendChild(amp3);
            }

            root.AppendChild(user);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);
                DBBytes = new byte[(int)memoryStream.Length];
                DBBytes = memoryStream.ToArray();
            }

            WriteDB();
        }

        private static void WriteDB()
        {
            using (FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Write))
            {
                fileStream.Write(DBBytes, 0, DBBytes.Length);
            }
        }
    }
}

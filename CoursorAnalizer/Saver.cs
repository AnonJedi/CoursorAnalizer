using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            foreach (XmlNode node in xmlDocument.SelectNodes("/FeaturesValues/User")) users.Add(node.SelectSingleNode("ClassName").InnerText);
            return users;
        }

        public static void SaveXML(string name, List<double> Cmid, List<double> Cmax, List<double> T, List<float[]> ampList, List<double> len)
        {
            #region Var
            XmlNode root = xmlDocument.SelectSingleNode("/FeaturesValues");
            XmlNode user = xmlDocument.CreateElement("User");
            XmlNode n = xmlDocument.CreateElement("ClassName");
            XmlNode realDes = xmlDocument.CreateElement("RealizationsDescription");
            XmlNode realDate = xmlDocument.CreateElement("RealizationDate");
            XmlNode cMid;
            XmlNode cMax;
            XmlNode t;
            XmlNode amp1;
            XmlNode amp2;
            XmlNode amp3;
            XmlNode amp4;
            XmlNode amp5;
            XmlNode amp6;
            XmlNode amp7;
            XmlNode amp8;
            XmlNode amp9;
            XmlNode amp10;
            DateTime time = DateTime.Now;
            #endregion

            n.InnerText = name;
            user.AppendChild(n);
            realDes.InnerText = "Эксперимент проводился в обычных условиях";
            realDate.InnerText = time.ToString();
            user.AppendChild(realDes);
            user.AppendChild(realDate);

            #region Magic
            int i = 0;
            foreach (double d in Cmid)
            {
                cMid = xmlDocument.CreateElement("FeatureID1");
                cMid.InnerText = (d/len[i]).ToString();
                user.AppendChild(cMid);
                i++;
            }

            foreach (double d in Cmax)
            {
                cMax = xmlDocument.CreateElement("FeatureID2");
                cMax.InnerText = d.ToString();
                user.AppendChild(cMax);
            }

            foreach (double d in T)
            {
                t = xmlDocument.CreateElement("FeatureID3");
                t.InnerText = d.ToString();
                user.AppendChild(t);
            }

            foreach (float[] floats in ampList)
            {
                amp1 = xmlDocument.CreateElement("FeatureID4");
                amp1.InnerText = floats[0].ToString();
                user.AppendChild(amp1);
            }

            foreach (float[] floats in ampList)
            {
                amp2 = xmlDocument.CreateElement("FeatureID5");
                amp2.InnerText = floats[1].ToString();
                user.AppendChild(amp2);
            }

            foreach (float[] floats in ampList)
            {
                amp3 = xmlDocument.CreateElement("FeatureID6");
                amp3.InnerText = floats[2].ToString();
                user.AppendChild(amp3);
            }

            foreach (float[] floats in ampList)
            {
                amp4 = xmlDocument.CreateElement("FeatureID7");
                amp4.InnerText = floats[2].ToString();
                user.AppendChild(amp4);
            }

            foreach (float[] floats in ampList)
            {
                amp5 = xmlDocument.CreateElement("FeatureID8");
                amp5.InnerText = floats[2].ToString();
                user.AppendChild(amp5);
            }

            foreach (float[] floats in ampList)
            {
                amp6 = xmlDocument.CreateElement("FeatureID9");
                amp6.InnerText = floats[2].ToString();
                user.AppendChild(amp6);
            }

            foreach (float[] floats in ampList)
            {
                amp7 = xmlDocument.CreateElement("FeatureID10");
                amp7.InnerText = floats[2].ToString();
                user.AppendChild(amp7);
            }

            foreach (float[] floats in ampList)
            {
                amp8 = xmlDocument.CreateElement("FeatureID11");
                amp8.InnerText = floats[2].ToString();
                user.AppendChild(amp8);
            }

            foreach (float[] floats in ampList)
            {
                amp9 = xmlDocument.CreateElement("FeatureID12");
                amp9.InnerText = floats[2].ToString();
                user.AppendChild(amp9);
            }

            foreach (float[] floats in ampList)
            {
                amp10 = xmlDocument.CreateElement("FeatureID13");
                amp10.InnerText = floats[2].ToString();
                user.AppendChild(amp10);
            }
            #endregion

            root.AppendChild(user);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);
                DBBytes = new byte[(int)memoryStream.Length];
                DBBytes = memoryStream.ToArray();
            }

            WriteDB();
        }

        public static void SaveTXT(string name, double mCmid, double mCmax, double mT, double dCmid, double dCmax, double dT)
        {
            string temp = "#1\r\n";
            temp += name + "\r\n";
            temp += "-\r\n";
            temp += mCmid + "\r\n";
            temp += dCmid + "\r\n";
            temp += mCmax + "\r\n";
            temp += dCmax + "\r\n";
            temp += mT + "\r\n";
            temp += dT + "\r\n";    

            using (StreamWriter file = new StreamWriter(name+".txt"))
            {
                file.Write(temp);
            }
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

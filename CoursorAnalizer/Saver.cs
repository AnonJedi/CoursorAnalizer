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
            foreach (XmlNode node in xmlDocument.SelectNodes("/FeaturesValues"))
            {
                users.Add(node.SelectSingleNode("Class").Attributes.GetNamedItem("name").Value);
            }
            return users;
        }

        public static void SaveXML(string name, List<double> Cmid, List<double> Cmax, List<double> T, List<float[]> ampList, List<double> len)
        {
            #region Var
            XmlNode featuresValueNode = xmlDocument.SelectSingleNode("/FeaturesValues");
            XmlNode classNode = xmlDocument.CreateElement("Class");
            XmlAttribute nameAttribute = xmlDocument.CreateAttribute("name");

            XmlNode realizationsNode = xmlDocument.CreateElement("Realizations");
            XmlAttribute descriptionAttribute = xmlDocument.CreateAttribute("description");

            XmlNode realizationNode = xmlDocument.CreateElement("Realization");
            XmlAttribute dateAttribute = xmlDocument.CreateAttribute("date");

            XmlNode featureNode = xmlDocument.CreateElement("Feature");
            XmlAttribute idAttribute;
            XmlAttribute valueAttribute;

            DateTime time = DateTime.Now;
            #endregion

            nameAttribute.InnerText = name;
            classNode.Attributes.Append(nameAttribute);

            descriptionAttribute.InnerText = "Эксперимент проводился в обычных условиях";
            realizationsNode.Attributes.Append(descriptionAttribute);

            dateAttribute.InnerText = time.ToString();
            realizationNode.Attributes.Append(dateAttribute);

            #region Magic

            for (int i = 0; i < Cmid.Count; i++)
            {
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "1";

                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = (Cmid[i]/len[i]).ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                featureNode = xmlDocument.CreateElement("Feature");
            }

            foreach (double d in Cmax)
            {
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "2";

                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = d.ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                featureNode = xmlDocument.CreateElement("Feature");
            }

            foreach (double d in T)
            {
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "3";

                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = d.ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                featureNode = xmlDocument.CreateElement("Feature");
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < ampList.Count; j++)
                {
                    idAttribute = xmlDocument.CreateAttribute("id");
                    idAttribute.InnerText = (i + 4).ToString();

                    valueAttribute = xmlDocument.CreateAttribute("value");
                    valueAttribute.InnerText = ampList[j][i].ToString();
                    featureNode.Attributes.Append(idAttribute);
                    featureNode.Attributes.Append(valueAttribute);
                    realizationNode.AppendChild(featureNode);

                    featureNode = xmlDocument.CreateElement("Feature");
                }
            }

            #endregion

            realizationsNode.AppendChild(realizationNode);
            classNode.AppendChild(realizationsNode);
            featuresValueNode.AppendChild(classNode);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);
                DBBytes = new byte[(int)memoryStream.Length];
                DBBytes = memoryStream.ToArray();
            }

            WriteDB();
        }

        public static string SaveTXT(string name, double mCmid, double mCmax, double mT, double dCmid, double dCmax, double dT, float[] ampM, float[] ampD, float[] allAmp)
        {
            string temp = "#1\r\n";
            temp += name + "\r\n";
            temp += "-\r\n";
            temp += "1\r\n";
            temp += "14\r\n";
            temp += mCmid + "\r\n";
            temp += dCmid + "\r\n";
            temp += mCmax + "\r\n";
            temp += dCmax + "\r\n";
            temp += mT + "\r\n";
            temp += dT + "\r\n";

            for (int i = 0; i < ampM.Length; i++)
            {
                temp += ampM[i] + "\r\n";
                temp += ampD[i] + "\r\n";
            }

            temp += allAmp[0] + "\r\n";
            temp += allAmp[1] + "\r\n";
            temp += "end";

            using (StreamWriter file = new StreamWriter(name+".txt"))
            {
                file.Write(temp);
            }

            return temp;
        }

        public static void SaveTXT(string name, List<double> Cmid, List<double> Cmax, List<double> T, List<float[]> ampList,
            List<double> energy)
        {
            string temp = "Cmid\r\n";
            foreach (double d in Cmid) temp += d + "\r\n";
            temp += "Cmax\r\n";
            foreach (double d in Cmax) temp += d + "\r\n";
            temp += "T\r\n";
            foreach (double d in T) temp += d + "\r\n";

            for (int j = 0; j < 10; j++)
            {
                temp += "amp" + j+1 + "\r\n";
                for (int i = 0; i < ampList.Count; i++) temp += ampList[i][j] + "\r\n";
            }
           
            temp += "energy\r\n";
            foreach (double d in energy) temp += d + "\r\n";

            using (StreamWriter file = new StreamWriter(name + "-2.txt"))
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

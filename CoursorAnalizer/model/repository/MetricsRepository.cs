﻿using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace CursorAnalyzer
{
    /// <summary>
    /// Repository for saving data
    /// </summary>
    static class MetricsRepository
    {
        #region Var

        private static byte[] DBBytes;
        private static XmlDocument xmlDocument;
        private static XmlNode root;
        private static readonly string fileName = "DataBase.xml";

        #endregion

        /// <summary>
        /// Save data to XML file
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="midDiffTracks">List of middle difference betveen 
        ///                             ideal and real mouse track</param>
        /// <param name="maxDiffTracks">List of max difference betveen
        ///                             ideal and real mouse track</param>
        /// <param name="T">Special parameter of metrics</param>
        /// <param name="ampContainer">List with amplitudes of mouse distance</param>
        /// <param name="mouseSpeed">List with mouse speed</param>
        /// <param name="energyContainer">List with energies</param>
        public static void SaveMouseParamsAndMetrics(
            string userName, List<double> midDiffTracks, List<double> maxDiffTracks, List<double> T, 
            List<float[]> ampContainer, List<double> mouseSpeed, List<double> energyContainer)
        {
            byte[] byteStream;

            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                byteStream = new byte[fileStream.Length];
                fileStream.Read(byteStream, 0, byteStream.Length);
            }
            using (MemoryStream memoryStream = new MemoryStream(byteStream))
            {
                xmlDocument = new XmlDocument();
                xmlDocument.Load(memoryStream);
            }

            #region Var
            root = xmlDocument.DocumentElement;
            XmlNode featuresNode = root.SelectSingleNode("descendant::Features");
            
            XmlNode classNode = xmlDocument.CreateElement("Class");
            XmlAttribute nameAttribute = xmlDocument.CreateAttribute("name");

            XmlNode realizationNode;
            XmlNode featureNode;
            XmlAttribute idAttribute;
            XmlAttribute valueAttribute;

            #endregion

            nameAttribute.InnerText = userName;
            classNode.Attributes.Append(nameAttribute);

            #region Magic

            for (int i = 0; i < midDiffTracks.Count; i++)
            {
                realizationNode = xmlDocument.CreateElement("Realization");

                featureNode = xmlDocument.CreateElement("Feature");
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "1";
                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = midDiffTracks[i].ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                featureNode = xmlDocument.CreateElement("Feature");
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "2";
                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = maxDiffTracks[i].ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                featureNode = xmlDocument.CreateElement("Feature");
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "3";
                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = T[i].ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                for (int j = 0; j < 10; j++)
                {
                    featureNode = xmlDocument.CreateElement("Feature");
                    idAttribute = xmlDocument.CreateAttribute("id");
                    idAttribute.InnerText = (j + 4).ToString();
                    valueAttribute = xmlDocument.CreateAttribute("value");
                    valueAttribute.InnerText = ampContainer[i][j].ToString();
                    featureNode.Attributes.Append(idAttribute);
                    featureNode.Attributes.Append(valueAttribute);
                    realizationNode.AppendChild(featureNode);
                }

                featureNode = xmlDocument.CreateElement("Feature");
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "14";
                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = mouseSpeed[i].ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                featureNode = xmlDocument.CreateElement("Feature");
                idAttribute = xmlDocument.CreateAttribute("id");
                idAttribute.InnerText = "15";
                valueAttribute = xmlDocument.CreateAttribute("value");
                valueAttribute.InnerText = energyContainer[i].ToString();
                featureNode.Attributes.Append(idAttribute);
                featureNode.Attributes.Append(valueAttribute);
                realizationNode.AppendChild(featureNode);

                classNode.AppendChild(realizationNode);
            }

            #endregion

            featuresNode.AppendChild(classNode);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                xmlDocument.Save(memoryStream);
                DBBytes = new byte[(int)memoryStream.Length];
                DBBytes = memoryStream.ToArray();
            }

            using (FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Write))
            {
                fileStream.Write(DBBytes, 0, DBBytes.Length);
            }
        }

        //public static string SaveTXT(string name, double mCmid, double mCmax, double mT, double dCmid, double dCmax, double dT, float[] ampM, float[] ampD, float[] allAmp)
        //{
        //    string temp = "#1\r\n";
        //    temp += name + "\r\n";
        //    temp += "-\r\n";
        //    temp += "1\r\n";
        //    temp += "14\r\n";
        //    temp += mCmid + "\r\n";
        //    temp += dCmid + "\r\n";
        //    temp += mCmax + "\r\n";
        //    temp += dCmax + "\r\n";
        //    temp += mT + "\r\n";
        //    temp += dT + "\r\n";

        //    for (int i = 0; i < ampM.Length; i++)
        //    {
        //        temp += ampM[i] + "\r\n";
        //        temp += ampD[i] + "\r\n";
        //    }

        //    temp += allAmp[0] + "\r\n";
        //    temp += allAmp[1] + "\r\n";
        //    temp += "end";

        //    using (StreamWriter file = new StreamWriter(name+".txt"))
        //    {
        //        file.Write(temp);
        //    }

        //    return temp;
        //}

        //public static void SaveTXT(string name, List<double> Cmid, List<double> Cmax, List<double> T, List<float[]> ampList,
        //    List<double> energy)
        //{
        //    string temp = "Cmid\r\n";
        //    foreach (double d in Cmid) temp += d + "\r\n";
        //    temp += "Cmax\r\n";
        //    foreach (double d in Cmax) temp += d + "\r\n";
        //    temp += "T\r\n";
        //    foreach (double d in T) temp += d + "\r\n";

        //    for (int j = 0; j < 10; j++)
        //    {
        //        temp += "amp" + j+1 + "\r\n";
        //        for (int i = 0; i < ampList.Count; i++) temp += ampList[i][j] + "\r\n";
        //    }
           
        //    temp += "energy\r\n";
        //    foreach (double d in energy) temp += d + "\r\n";

        //    using (StreamWriter file = new StreamWriter(name + "-2.txt"))
        //    {
        //        file.Write(temp);
        //    }
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.XML
{
    /// <summary>
    /// [��������: xml��ʽ��������]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-9]<br></br>
    /// </summary>
    public class SettingFile
    {
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="fileName">�ļ�����(������·��)</param>
        /// <param name="groupName">������</param>
        /// <param name="ID">���ñ��</param>
        /// <param name="value">ֵ</param>
        /// <returns>1�ɹ�</returns>
        public static int SaveSetting(string fileName, string groupName, string ID, string value)
        {
            if (!System.IO.File.Exists(fileName))
            {
                XML.File.CreateXmlFile(fileName, "1.0", "Setting");
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            System.Xml.XmlNode nodeGroup = root.SelectSingleNode(groupName);
            if (nodeGroup == null)
            {
                System.Xml.XmlElement XmlElement = XmlDocument.CreateElement(groupName);
                root.AppendChild(XmlElement);
                nodeGroup = root.SelectSingleNode(groupName);
            }
            System.Xml.XmlNode nodeID = nodeGroup.SelectSingleNode(ID);
            if (nodeID == null)
            {
                System.Xml.XmlElement XmlElement = XmlDocument.CreateElement(ID);
                XmlElement.SetAttribute("Value", value);
                nodeGroup.AppendChild(XmlElement);
            }
            else
            {
                System.Xml.XmlElement XmlElement = (System.Xml.XmlElement)nodeID;
                XmlElement.SetAttribute("Value", value);
            }
            XmlDocument.Save(fileName);
            
            return 1;
        }

        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="groupName">������</param>
        /// <param name="ID">���ñ���</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        public static string ReadSetting(string fileName, string groupName, string ID, string defaultValue, out string errInfo)
        {
            errInfo = "";
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    errInfo = "�ļ���" + fileName + "������";
                    return defaultValue;
                }
                System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
                XmlDocument.Load(fileName);
                System.Xml.XmlElement root = XmlDocument.DocumentElement;
                if (root == null)
                {
                    errInfo = "�ļ���" + fileName + "û�и��ڵ�";
                    return defaultValue;
                }
                System.Xml.XmlNode nodeGroup = root.SelectSingleNode(groupName);
                if (nodeGroup == null)
                {
                    return defaultValue;
                }
                System.Xml.XmlNode nodeID = nodeGroup.SelectSingleNode(ID);
                if (nodeID == null)
                {
                    return defaultValue;
                }
                return nodeID.Attributes[0].InnerText;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// ��ȡ�����ļ�
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="groupName">������</param>
        /// <param name="ID">���ñ���</param>
        /// <returns></returns>
        public static string ReadSetting(string fileName, string groupName, string ID, string defaultValue)
        {
            string errInfo = "";
            return ReadSetting(fileName, groupName, ID, defaultValue, out errInfo);
        }
    }
}

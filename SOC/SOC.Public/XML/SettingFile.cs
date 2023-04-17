using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.XML
{
    /// <summary>
    /// [功能描述: xml格式程序配置]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public class SettingFile
    {
        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="fileName">文件名称(不包括路径)</param>
        /// <param name="groupName">组名称</param>
        /// <param name="ID">配置编号</param>
        /// <param name="value">值</param>
        /// <returns>1成功</returns>
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
        /// 读取配置文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="groupName">组名称</param>
        /// <param name="ID">配置编码</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns></returns>
        public static string ReadSetting(string fileName, string groupName, string ID, string defaultValue, out string errInfo)
        {
            errInfo = "";
            try
            {
                if (!System.IO.File.Exists(fileName))
                {
                    errInfo = "文件：" + fileName + "不存在";
                    return defaultValue;
                }
                System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
                XmlDocument.Load(fileName);
                System.Xml.XmlElement root = XmlDocument.DocumentElement;
                if (root == null)
                {
                    errInfo = "文件：" + fileName + "没有根节点";
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
        /// 读取配置文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="groupName">组名称</param>
        /// <param name="ID">配置编码</param>
        /// <returns></returns>
        public static string ReadSetting(string fileName, string groupName, string ID, string defaultValue)
        {
            string errInfo = "";
            return ReadSetting(fileName, groupName, ID, defaultValue, out errInfo);
        }
    }
}

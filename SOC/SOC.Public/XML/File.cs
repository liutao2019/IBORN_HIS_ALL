using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.XML
{
    /// <summary>
    /// [功能描述: XML文件处理相关]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public class File
    {
        #region 文件创建
        /// <summary>
        /// 创建有根节点的xml文件
        /// </summary>
        /// <param name="fileName">文件名称(包含路径名称)</param>
        /// <param name="version">版本号</param>
        /// <param name="rootElementName">根节点名称</param>
        /// <returns></returns>
        public static int CreateXmlFile(string fileName, string version, string rootElementName)
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration(version, "GB2312", ""));

                System.Xml.XmlElement element = doc.CreateElement(rootElementName);
                element.SetAttribute("说明", "根节点");
                doc.AppendChild(element);

                doc.Save(fileName);

            }
            catch
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="fileName">文件名称(包含路径名称)</param>
        /// <param name="version">版本号</param>
        /// <returns></returns>
        public static int CreateXmlFile(string fileName, string version)
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration(version, "GB2312", ""));
                doc.Save(fileName);
            }
            catch
            {
                return -1;
            }
            return 1;
        }
        #endregion

        #region XPath

        /// <summary>
        /// 获取xml文件的节点
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="XPath">XPath</param>
        /// <returns>节点</returns>
        public static System.Xml.XmlNode GetNode(string fileName, string XPath)
        {
            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception("文件:" + fileName + "不存在");
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            if (root == null)
            {
                throw new Exception("文件:" + fileName + "受损，没有根节点");
            }
            return root.SelectSingleNode(XPath);
        }

        /// <summary>
        /// 获取xml文件的节点
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="XPath">XPath</param>
        /// <returns>节点</returns>
        public static System.Xml.XmlNodeList GetNodeList(string fileName, string XPath)
        {
            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception("文件:" + fileName + "不存在");
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            if (root == null)
            {
                throw new Exception("文件:" + fileName + "受损，没有根节点");
            }
            return root.SelectNodes(XPath);
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.XML
{
    /// <summary>
    /// [��������: XML�ļ��������]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-9]<br></br>
    /// </summary>
    public class File
    {
        #region �ļ�����
        /// <summary>
        /// �����и��ڵ��xml�ļ�
        /// </summary>
        /// <param name="fileName">�ļ�����(����·������)</param>
        /// <param name="version">�汾��</param>
        /// <param name="rootElementName">���ڵ�����</param>
        /// <returns></returns>
        public static int CreateXmlFile(string fileName, string version, string rootElementName)
        {
            try
            {
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.AppendChild(doc.CreateXmlDeclaration(version, "GB2312", ""));

                System.Xml.XmlElement element = doc.CreateElement(rootElementName);
                element.SetAttribute("˵��", "���ڵ�");
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
        /// ����xml�ļ�
        /// </summary>
        /// <param name="fileName">�ļ�����(����·������)</param>
        /// <param name="version">�汾��</param>
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
        /// ��ȡxml�ļ��Ľڵ�
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="XPath">XPath</param>
        /// <returns>�ڵ�</returns>
        public static System.Xml.XmlNode GetNode(string fileName, string XPath)
        {
            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception("�ļ�:" + fileName + "������");
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            if (root == null)
            {
                throw new Exception("�ļ�:" + fileName + "����û�и��ڵ�");
            }
            return root.SelectSingleNode(XPath);
        }

        /// <summary>
        /// ��ȡxml�ļ��Ľڵ�
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="XPath">XPath</param>
        /// <returns>�ڵ�</returns>
        public static System.Xml.XmlNodeList GetNodeList(string fileName, string XPath)
        {
            if (!System.IO.File.Exists(fileName))
            {
                throw new Exception("�ļ�:" + fileName + "������");
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            if (root == null)
            {
                throw new Exception("�ļ�:" + fileName + "����û�и��ڵ�");
            }
            return root.SelectNodes(XPath);
        }

        #endregion

    }
}

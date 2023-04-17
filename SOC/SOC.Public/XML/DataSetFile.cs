using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.XML
{
    /// <summary>
    /// [��������: DataSet��xml�����ļ�]<br></br>
    /// [�� �� ��: cube]<br></br>
    /// [����ʱ��: 2010-9]<br></br>
    /// </summary>
    public class DataSetFile
    {
        /// <summary>
        /// ����DataSet��xml����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="columnName">DataSet������</param>
        /// <param name="value">ֵ</param>
        /// <param name="primaryKeyColumnName">����������</param>
        /// <param name="primaryKeyValue">����ֵ</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        public static int SetValue(string fileName, string TableName, string columnName, string value, string primaryKeyColumnName, string primaryKeyValue, out string errInfo)
        {
            errInfo = "";
            if (!System.IO.File.Exists(fileName))
            {
                errInfo = "�ļ�:" + fileName + "������";
                return -1;
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            System.Xml.XmlNode tableNode = root.SelectSingleNode("/" + TableName + "/[" + primaryKeyColumnName + "=" + primaryKeyValue + "]");
            if (tableNode == null)
            {
                errInfo = "�Ҳ���:" + "/" + TableName + "/[" + primaryKeyColumnName + "=" + primaryKeyValue + "]";
                return -1;
            }
            System.Xml.XmlNode columnNode = tableNode.SelectSingleNode(columnName);
            if (columnNode == null)
            {
                System.Xml.XmlElement XmlElement = XmlDocument.CreateElement(columnName);
                XmlElement.Value = value;
                tableNode.AppendChild(XmlElement);
            }
            else
            {
                System.Xml.XmlElement XmlElement = (System.Xml.XmlElement)columnNode;
                XmlElement.Value = value;
            }
            XmlDocument.Save(fileName);

            return 1;
        }

        /// <summary>
        /// ����DataSet��xml����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="columnName">DataSet������</param>
        /// <param name="value">ֵ</param>
        /// <param name="primaryKeyColumnName">����������</param>
        /// <param name="primaryKeyValue">����ֵ</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        public static int SetValue(string fileName, string columnName, string value, string primaryKeyColumnName, string primaryKeyValue, out string errInfo)
        {
            errInfo = "";
            return SetValue(fileName, "Table", columnName, value, primaryKeyColumnName, primaryKeyValue, out errInfo);
        }

        /// <summary>
        /// ����DataSet��xml����
        /// </summary>
        /// <param name="fileName">�ļ�����</param>
        /// <param name="columnName">DataSet������</param>
        /// <param name="value">ֵ</param>
        /// <param name="primaryKeyColumnName">����������</param>
        /// <param name="primaryKeyValue">����ֵ</param>
        /// <returns></returns>
        public static int SetValue(string fileName, string columnName, string value, string primaryKeyColumnName, string primaryKeyValue)
        {
            string errInfo = "";
            return SetValue(fileName, "Table", columnName, value, primaryKeyColumnName, primaryKeyValue, out errInfo);
        }
    }
}

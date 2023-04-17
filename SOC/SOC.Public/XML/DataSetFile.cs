using System;
using System.Collections.Generic;
using System.Text;

namespace FS.SOC.Public.XML
{
    /// <summary>
    /// [功能描述: DataSet的xml数据文件]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-9]<br></br>
    /// </summary>
    public class DataSetFile
    {
        /// <summary>
        /// 设置DataSet的xml数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="columnName">DataSet列名称</param>
        /// <param name="value">值</param>
        /// <param name="primaryKeyColumnName">主键列名称</param>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns></returns>
        public static int SetValue(string fileName, string TableName, string columnName, string value, string primaryKeyColumnName, string primaryKeyValue, out string errInfo)
        {
            errInfo = "";
            if (!System.IO.File.Exists(fileName))
            {
                errInfo = "文件:" + fileName + "不存在";
                return -1;
            }
            System.Xml.XmlDocument XmlDocument = new System.Xml.XmlDocument();
            XmlDocument.Load(fileName);
            System.Xml.XmlElement root = XmlDocument.DocumentElement;
            System.Xml.XmlNode tableNode = root.SelectSingleNode("/" + TableName + "/[" + primaryKeyColumnName + "=" + primaryKeyValue + "]");
            if (tableNode == null)
            {
                errInfo = "找不到:" + "/" + TableName + "/[" + primaryKeyColumnName + "=" + primaryKeyValue + "]";
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
        /// 设置DataSet的xml数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="columnName">DataSet列名称</param>
        /// <param name="value">值</param>
        /// <param name="primaryKeyColumnName">主键列名称</param>
        /// <param name="primaryKeyValue">主键值</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns></returns>
        public static int SetValue(string fileName, string columnName, string value, string primaryKeyColumnName, string primaryKeyValue, out string errInfo)
        {
            errInfo = "";
            return SetValue(fileName, "Table", columnName, value, primaryKeyColumnName, primaryKeyValue, out errInfo);
        }

        /// <summary>
        /// 设置DataSet的xml数据
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <param name="columnName">DataSet列名称</param>
        /// <param name="value">值</param>
        /// <param name="primaryKeyColumnName">主键列名称</param>
        /// <param name="primaryKeyValue">主键值</param>
        /// <returns></returns>
        public static int SetValue(string fileName, string columnName, string value, string primaryKeyColumnName, string primaryKeyValue)
        {
            string errInfo = "";
            return SetValue(fileName, "Table", columnName, value, primaryKeyColumnName, primaryKeyValue, out errInfo);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.EMR.EMRService
{
    public class EMRManage:FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 根据时间查询患者信息
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="dsItem">查询结果</param>
        /// <returns>1执行成功，-1执行失败</returns>
        public int QueryPatientByTime(string sql,ref System.Data.DataSet dsItem)
        {
            if (this.ExecQuery(sql, ref dsItem) == -1)
            {
                this.Err = "查询失败！" + this.Err;
                return -1;
            }
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
        public string ReadXML(string fileName, string groupName, string defaultValue, out string errInfo)
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
                return nodeGroup.InnerText;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}

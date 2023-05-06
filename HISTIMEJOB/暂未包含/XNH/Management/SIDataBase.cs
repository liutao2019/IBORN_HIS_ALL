using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Collections;

namespace HISTIMEJOB.XNH.Management
{
    /// <summary>
    /// 广州番禺新农合医保接口数据库操作基类

    /// </summary>
    public class SIDatabase
    {
        /// <summary>
        /// 生成实体时连接数据库
        /// </summary>
        public SIDatabase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            conn.ConnectionString = this.GetConnectString();
            command.Connection = conn;
            command.CommandType = System.Data.CommandType.Text;

            try
            {
                conn.Open();
            }
            catch (SqlException ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                throw ex;
            }
            //try
            //{
            //    trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            //    command.Transaction = trans;
            //}
            //catch (SqlException ex)
            //{
            //    this.Err = "数据库连接失败!" + ex.Message;
            //    this.ErrCode = "-1";
            //    this.WriteErr();
            //    throw ex;
            //}
        }

        /// <summary>
        /// 去掉类的时候，关闭数据库

        /// </summary>
        ~SIDatabase()
        {
            try
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Dispose();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
            }
        }

        #region 变量
        protected string hosCode = "0076";

        /// <summary>
        /// 连接串

        /// </summary>
        protected SqlConnection conn = new SqlConnection();//连接串

        /// <summary>
        /// 数据库命令集
        /// </summary>
        protected SqlCommand command = new SqlCommand(); //数据库命令集
        /// <summary>
        /// 数据库事务

        /// </summary>
        protected System.Data.SqlClient.SqlTransaction trans;//数据库事务

        /// <summary>
        /// 医保数据库连接设置

        /// </summary>
        protected string profileName = System.Windows.Forms.Application.StartupPath + @".\profile\GZPYSiDataBase.xml";//医保数据库连接设置;
        /// <summary>
        /// 日志类

        /// </summary>
        protected Neusoft.FrameWork.Models.NeuLog log = new Neusoft.FrameWork.Models.NeuLog();//日志类

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err;
        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrCode;
        /// <summary>
        /// 数据流

        /// </summary>
        protected System.Data.SqlClient.SqlDataReader Reader;//数据流

        #endregion

        #region 数据库基本操作

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            if (trans != null)
            {
                trans.Commit();
            }
        }
        /// <summary>
        /// 回滚
        /// </summary>
        public void RollBack()
        {
            if (trans != null)
            {
                trans.Rollback();
            }
        }
        /// <summary>
        /// 关闭数据库连接

        /// </summary>
        public void Close()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {       
                conn.Close();
                conn.Dispose();
            }
        }
        /// <summary>
        /// 开始事务

        /// </summary>
        public void BeginTranscation()
        {
            this.Open();

            if (trans == null||trans.Connection == null)
            {
                trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction = trans;
            }
           
        }
        /// <summary>
        /// 打开数据库连接

        /// </summary>
        public void Open()
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.ConnectionString = this.GetConnectString();
                command.Connection = conn;
                command.CommandType = System.Data.CommandType.Text;

                try
                {
                    conn.Open();
                }
                catch (SqlException ex)
                {
                    this.Err = "数据库连接失败!" + ex.Message;
                    this.ErrCode = "-1";
                    this.WriteErr();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 写入错误信息
        /// </summary>
        protected void WriteErr()
        {
            this.log.WriteLog("Error:" + this.GetType().ToString() + ":" + this.Err + this.ErrCode);
        }

        /// <summary>
        /// 写日志

        /// </summary>
        /// <param name="logString"></param>
        [System.Diagnostics.Conditional("DEBUG")]
        public void WriteLog(string logString)
        {
            Type type = this.GetType();
            string className = type.Namespace + type.Name;
            log.WriteLog("Log: " + className + " ： " + logString);
        }
        /// <summary>
        /// 获得连接串

        /// </summary>
        /// <returns></returns>
        public string GetConnectString()
        {
            string dbInstance = "";
            string DataBaseName = "";
            string userName = "";
            string password = "";
            string connString = "";

            if (!creatXmlDoc(profileName))
            {
                log.WriteLog("创建配置文件失败  GZPYSiDatabase-->GetConnectString");
                return "";
            }
            XmlNode profileNode = getUsedProfileNode(profileName);

            try
            {

                dbInstance = profileNode.Attributes["数据库实例名"].Value.ToString();
                DataBaseName = profileNode.Attributes["数据库名"].Value.ToString();
                userName = profileNode.Attributes["用户名"].Value.ToString();
                password = profileNode.Attributes["密码"].Value.ToString();
                hosCode = profileNode.Attributes["医院代码"].Value.ToString();//新农合医院代码
            }
            catch
            {
                log.WriteLog("获取连接字符串出错 GZPYSiDatabase-->GetConnectString");
                return "";
            }

            connString = "packet size=4096;user id=" + userName + ";data source=" + dbInstance + ";pers" +
                "ist security info=True;initial catalog=" + DataBaseName + ";password=" + password;
            return connString;
        }
        /// <summary>
        /// 执行更新,删除,插入等SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int ExecNoQuery(string sql)
        {
            command.CommandText = sql;

            try
            {
                return command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.Err = "读取数据库失败!" + ex.Message + "|" + sql;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }
        }
        /// <summary>
        /// 执行查询语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int ExecQuery(string sql)
        {
            if (conn.ConnectionString == "")
                return -1;

            command.CommandText = sql;

            if (this.Reader != null && !this.Reader.IsClosed)
            {
                this.Reader.Close();
            }
            try
            {
                Reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                this.Err = "读取数据库失败!" + ex.Message + "|" + sql;
                this.ErrCode = "-1";
                this.WriteErr();
                return -1;
            }

            //conn.Close();

            return 0;
        }
        #endregion

        #region 读取connectString用到的子函数
        /// <summary>
        /// 为数据库配置创建xml文件 成功创建或文件已存在true
        /// </summary>
        /// <returns></returns>
        private bool creatXmlDoc(string profileName)
        {
            if (!System.IO.File.Exists(profileName))
            {
                Neusoft.FrameWork.Xml.XML myXml = new Neusoft.FrameWork.Xml.XML();
                XmlDocument doc = new XmlDocument();
                XmlElement root;
                root = myXml.CreateRootElement(doc, "SqlServerConnectForHis5.0", "1.0");
                XmlElement dbName = myXml.AddXmlNode(doc, root, "设置", "");

                myXml.AddNodeAttibute(dbName, "数据库实例名", "");
                myXml.AddNodeAttibute(dbName, "数据库名", "");
                myXml.AddNodeAttibute(dbName, "用户名", "");
                myXml.AddNodeAttibute(dbName, "密码", "");

                try
                {
                    StreamWriter sr = new StreamWriter(profileName, false, System.Text.Encoding.Default);
                    string cleandown = doc.OuterXml;
                    sr.Write(cleandown);
                    sr.Close();
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("无法保存！" + ex.Message);
                    return false;
                }
                return true;
            }
            return true;
        }

        private XmlDocument getXmlDoc(string profileName)
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                StreamReader sr = new StreamReader(profileName, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
                return doc;
            }
            catch
            {
                log.WriteLog("获取xml配置文件失败 SIDatabase.cs-->creatXmlDoc");
                return null;
            }
        }


        private ArrayList getArrayListFromXmlNodes(XmlNodeList nodeList)
        {
            ArrayList al = new ArrayList();
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            for (int i = 0; i < nodeList.Count; i++)
            {
                obj = new Neusoft.FrameWork.Models.NeuObject();
                obj.Name = nodeList.Item(i).Name;
                obj.Memo = nodeList.Item(i).Attributes["当前使用"].Value.ToString();
                obj.ID = i.ToString();              //节点的下标

                al.Add(obj);
            }
            return al;
        }

        /// <summary>
        ///如果profileName的配置文件存在 获取数据库当前设置的结点
        /// </summary>
        /// <returns></returns>
        private XmlNode getUsedProfileNode(string profileName)
        {
            XmlDocument xmlDoc = getXmlDoc(profileName);
            XmlNode rootNode = xmlDoc.SelectSingleNode("//设置");
            ArrayList al = this.getArrayListFromXmlNodes(rootNode.ChildNodes);
            foreach (Neusoft.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.Memo == "1")         //节点的["当前使用"]属性
                {
                    int usedNodeIndex = Neusoft.FrameWork.Function.NConvert.ToInt32(obj.ID);
                    return rootNode.ChildNodes.Item(usedNodeIndex);
                }
            }
            return null;
        }
        #endregion
    }
}

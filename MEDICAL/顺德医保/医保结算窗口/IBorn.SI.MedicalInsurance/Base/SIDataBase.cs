using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBorn.SI.GuangZhou.Base
{
    public class SIDataBase
    {
        /// <summary>
        /// 数据库配置文档
        /// </summary>
        private static System.Xml.XmlDocument doc = null;

        /// <summary>
        /// 数据库连接
        /// </summary>
        private static System.Data.SqlClient.SqlConnection con = null;

        /// <summary>
        /// 事务控制
        /// </summary>
        private static System.Data.SqlClient.SqlTransaction trans = null;

        /// <summary>
        /// 数据库连接串
        /// </summary>
        public static string SIServer = string.Empty;

        /// <summary>
        /// 静态函数只初始化一次
        /// </summary>
        static SIDataBase()
        {
            //静态构造函数、只加载一次
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "Setting\\Insurance.config";

            if (System.IO.File.Exists(fileName))
            {
                doc = new System.Xml.XmlDocument();
                try
                {
                    doc.Load(fileName);
                    SIServer = GetConfigValue(doc, "SIServer");
                }
                catch (Exception e)
                {
                    throw new Exception("加载Insurance.config配置文件失败，原因：" + e.Message);
                }
            }
            else
            {
                throw new Exception("缺少配置文件Insurance.config");
            }
        }

        /// <summary>
        /// 获取节点值
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        private static string GetConfigValue(System.Xml.XmlDocument doc, string nodeName)
        {
            System.Xml.XmlNode node = doc.SelectSingleNode(string.Format("/configuration/GunagZhouSI/Server/{0}", nodeName));
            if (node == null || node.InnerText.Trim().Length == 0)
            {
                throw new Exception(string.Format("加载Insurance.config配置文件失败，原因：缺少/configuration/GunagZhouSI/Server/{0}的配置", nodeName));
            }
            return node.InnerText.Trim();
        }

        /// <summary>
        /// 打开指定的数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Open(string connectionString)
        {
            if (connectionString.Length == 0)
            {
                throw new Exception("缺少配置文件Insurance.config");
            }

            if (con == null)
            {
                con = new System.Data.SqlClient.SqlConnection(connectionString);
            }

            if (con.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    con.Open();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception("数据库连接失败，原因：" + ex.Message);
                }
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public static void Close()
        {
            if (con != null && con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public static void BeginTranscation()
        {
            if (con != null)
            {
                trans = con.BeginTransaction();
            }
        }

        /// <summary>
        /// 事务提交
        /// </summary>
        public static void Commit()
        {
            if (trans != null)
            {
                trans.Commit();
                trans.Dispose();
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public static void Rollback()
        {
            if (trans != null)
            {
                trans.Rollback();
                trans.Dispose();
            }
        }

        /// <summary>
        /// 非查询语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string strSql)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = con;//连接
            cmd.Transaction = trans;//事务
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = strSql;

            //日志记录

            int i = cmd.ExecuteNonQuery();

            //日志记录
            return i;
        }

        /// <summary>
        /// 查询语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static System.Data.DataTable ExecuteDataTable(string strSql)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = con;//连接
            cmd.Transaction = trans;//事务
            cmd.Parameters.Clear();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = strSql;
            System.Data.DataTable dt = new System.Data.DataTable();
            System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(cmd);
            adapter.Fill(dt);

            return dt;
        }


    }
}

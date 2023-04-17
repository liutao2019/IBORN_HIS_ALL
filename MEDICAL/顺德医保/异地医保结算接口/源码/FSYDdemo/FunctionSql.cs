using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Data.SqlClient;
using System.Xml;
using System.Data;
using System.Data.OracleClient;


namespace FoShanYDSI
{
    public class FunctionSql
    {
        #region  LIS数据库连接和增删改方法

        /// <summary>
        /// LIS数据库连接串
        /// </summary>
        private static string conString_lis = "server='128.111.4.144';uid='sa';pwd='sa';database=''";

        /// <summary>
        /// sql数据库连接
        /// </summary>
        private static SqlConnection connection_lis = null;

        /// <summary>
        /// sql数据库连接
        /// </summary>
        public static SqlConnection Connection_lis
        {
            get
            {
                if (connection_lis == null)
                {
                    connection_lis = new SqlConnection(conString_lis);
                }
                if (connection_lis.State != System.Data.ConnectionState.Open)
                {
                    connection_lis.Open();
                }

                return FunctionSql.connection_lis;
            }
        }

        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dsResult"></param>
        /// <returns>1正常；-1异常</returns>
        public static int ExecuteQuery_lis(string strSql, ref DataSet dsResult, string dstablename)
        {
            try
            {
                SqlCommand command = FunctionSql.Connection_lis.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_lis;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dsResult, dstablename);
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql查询(DataSets)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dsResult"></param>
        /// <returns>1正常；-1异常</returns>
        public static int ExecuteQueryDataSets_lis(string strSql, ref DataSets dsResult, string dstablename)
        {
            try
            {
                SqlCommand command = FunctionSql.Connection_lis.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_lis;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dsResult, dstablename);
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql增删改
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteNoQuery_lis(string strSql)
        {
            int i = 0;

            try
            {
                SqlCommand command = FunctionSql.Connection_lis.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_lis;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;


                i = command.ExecuteNonQuery();
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql事务
        /// </summary>
        private static SqlTransaction Trans_lis = null;

        /// <summary>
        /// sql开启事务
        /// </summary>
        /// <returns></returns>
        public static void BeginTransaction_lis()
        {
            FunctionSql.Trans_lis = FunctionSql.Connection_lis.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// sql事务提交
        /// </summary>
        public static void Commit_lis()
        {
            FunctionSql.Trans_lis.Commit();
        }

        /// <summary>
        /// sql回滚
        /// </summary>
        public static void RollBack_lis()
        {
            FunctionSql.Trans_lis.Rollback();
        }

        #endregion

        #region  PACS数据库连接和增删改方法

        /// <summary>
        /// PACS数据库连接串
        /// </summary>
        private static string conString_pacs = "server='128.111.33.14';uid='sa';pwd='fssy_83927739';database='annet'";

        /// <summary>
        /// sql数据库连接
        /// </summary>
        private static SqlConnection connection_pacs = null;

        /// <summary>
        /// sql数据库连接
        /// </summary>
        public static SqlConnection Connection_pacs
        {
            get
            {
                if (connection_pacs == null)
                {
                    connection_pacs = new SqlConnection(conString_pacs);
                }
                if (connection_pacs.State != System.Data.ConnectionState.Open)
                {
                    connection_pacs.Open();
                }

                return FunctionSql.connection_pacs;
            }
        }

        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dsResult"></param>
        /// <returns>1正常；-1异常</returns>
        public static int ExecuteQuery_pacs(string strSql, ref DataSet dsResult, string dstablename)
        {
            try
            {
                SqlCommand command = FunctionSql.Connection_pacs.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_pacs;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dsResult, dstablename);
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql查询(DataSets)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dsResult"></param>
        /// <returns>1正常；-1异常</returns>
        public static int ExecuteQueryDataSets_pacs(string strSql, ref DataSets dsResult, string dstablename)
        {
            try
            {
                SqlCommand command = FunctionSql.Connection_pacs.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_pacs;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dsResult, dstablename);
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql增删改
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteNoQuery_pacs(string strSql)
        {
            int i = 0;

            try
            {
                SqlCommand command = FunctionSql.Connection_pacs.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_pacs;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;


                i = command.ExecuteNonQuery();
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql事务
        /// </summary>
        private static SqlTransaction Trans_pacs = null;

        /// <summary>
        /// sql开启事务
        /// </summary>
        /// <returns></returns>
        public static void BeginTransaction_pacs()
        {
            FunctionSql.Trans_pacs = FunctionSql.Connection_pacs.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// sql事务提交
        /// </summary>
        public static void Commit_pacs()
        {
            FunctionSql.Trans_pacs.Commit();
        }

        /// <summary>
        /// sql回滚
        /// </summary>
        public static void RollBack_pacs()
        {
            FunctionSql.Trans_pacs.Rollback();
        }

        #endregion


        #region  病案首页数据库连接和增删改方法

        /// <summary>
        /// DRGS数据库连接串
        /// </summary>
        private static string conString_DRGS = "server='10.20.10.219';uid='sa';pwd='iborn22231333';database='bagl_java'";

        /// <summary>
        /// sql数据库连接
        /// </summary>
        private static SqlConnection connection_DRGS = null;

        /// <summary>
        /// sql数据库连接
        /// </summary>
        public static SqlConnection Connection_DRGS
        {
            get
            {
                if (connection_DRGS == null)
                {
                    connection_DRGS = new SqlConnection(conString_DRGS);
                }
                if (connection_DRGS.State != System.Data.ConnectionState.Open)
                {
                    connection_DRGS.Open();
                }

                return FunctionSql.connection_DRGS;
            }
        }

        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dsResult"></param>
        /// <returns>1正常；-1异常</returns>
        public static int ExecuteQuery_DRGS(string strSql, ref DataSet dsResult, string dstablename)
        {
            try
            {
                SqlCommand command = FunctionSql.Connection_DRGS.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_DRGS;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dsResult, dstablename);
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql查询(DataSets)
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dsResult"></param>
        /// <returns>1正常；-1异常</returns>
        public static int ExecuteQueryDataSets_DRGS(string strSql, ref DataSets dsResult, string dstablename)
        {
            try
            {
                SqlCommand command = FunctionSql.Connection_DRGS.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_DRGS;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dsResult, dstablename);
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql增删改
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public static int ExecuteNoQuery_DRGS(string strSql)
        {
            int i = 0;

            try
            {
                SqlCommand command = FunctionSql.Connection_DRGS.CreateCommand();
                if (command.Transaction == null)
                {
                    command.Transaction = FunctionSql.Trans_DRGS;
                }
                command.CommandType = CommandType.Text;
                command.Parameters.Clear();
                command.CommandText = strSql;


                i = command.ExecuteNonQuery();
                //Function.connection.Close();
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// sql事务
        /// </summary>
        private static SqlTransaction Trans_DRGS = null;

        /// <summary>
        /// sql开启事务
        /// </summary>
        /// <returns></returns>
        public static void BeginTransaction_DRGS()
        {
            FunctionSql.Trans_DRGS = FunctionSql.Connection_DRGS.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// sql事务提交
        /// </summary>
        public static void Commit_DRGS()
        {
            FunctionSql.Trans_DRGS.Commit();
        }

        /// <summary>
        /// sql回滚
        /// </summary>
        public static void RollBack_DRGS()
        {
            FunctionSql.Trans_DRGS.Rollback();
        }

        #endregion

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteLog(string msg)
        {
            //路径
            string directoryPah = ".\\log";
            if (!System.IO.Directory.Exists(directoryPah))
            {
                System.IO.Directory.CreateDirectory(directoryPah);
            }

            //文件名
            string fileName = DateTime.Now.ToString("yyyyMMdd") + ".log";
            System.IO.StreamWriter sw = new StreamWriter(directoryPah + "\\" + fileName, true);
            sw.WriteLine(DateTime.Now.ToString() + "    " + msg);
            sw.Flush();
            sw.Close();

        }

    }
}

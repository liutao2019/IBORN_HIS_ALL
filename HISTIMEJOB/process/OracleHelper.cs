using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.IO;

namespace OADAL
{
    public static class OracleHelper
    {
        //{12DA53DF-516D-463a-A2F2-6FD9C4B8234A}
        private readonly static string connstr = "Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST = 192.168.34.5)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User ID=his;Password=his;Unicode=True";
        //private readonly static string connstr = "Data Source=(DESCRIPTION =(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST = 192.168.34.5)(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = orcl)));User ID=test;Password=test;Unicode=True";
        //数据库连接字符串
        //private readonly static string connstr = ConfigurationManager.ConnectionStrings[con].ConnectionString;
        //private readonly static string connstr = ConfigurationManager.ConnectionStrings["OracleConnectionString"].ConnectionString;

        //OracleConnection objConn = new OracleConnection(con);


        /// <summary>
        /// 执行数据库查询操作,返回受影响的行数
        /// </summary>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作影响的数据行数</returns>
        public static int ExecuteNonQuery(string cmdText, params OracleParameter[] commandParameters)
        {

            OracleCommand command = new OracleCommand();
            OracleConnection connection = new OracleConnection(connstr);
            int result = 0;

            try
            {
                LogHelper.Write(connection.ConnectionString);
                PrepareCommand(command, connection, null, CommandType.Text, cmdText, commandParameters);
                result = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 执行数据库事务查询操作,返回受影响的行数
        /// </summary>
        /// <param name="transaction">数据库事务对象</param>
        /// <param name="cmdType">Command类型</param>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前事务查询操作影响的数据行数</returns>
        public static int ExecuteNonQuery(OracleTransaction transaction, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleConnection connection = transaction.Connection;
            int result = 0;

            try
            {
                PrepareCommand(command, connection, transaction, cmdType, cmdText, commandParameters);
                result = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                transaction.Dispose();
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 执行数据库查询操作,返回受影响的行数
        /// </summary>
        /// <param name="connection">Oracle数据库连接对象</param>
        /// <param name="cmdType">Command类型</param>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作影响的数据行数</returns>
        public static int ExecuteNonQuery(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("当前数据库连接不存在");
            OracleCommand command = new OracleCommand();
            int result = 0;

            try
            {
                PrepareCommand(command, connection, null, cmdType, cmdText, commandParameters);
                result = command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 执行数据库查询操作,返回OracleDataReader类型的内存结果集
        /// </summary>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作返回的OracleDataReader类型的内存结果集</returns>
        public static OracleDataReader ExecuteReader(string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleConnection connection = new OracleConnection(connstr);
            OracleDataReader reader = null;

            try
            {
                PrepareCommand(command, connection, null, CommandType.Text, cmdText, commandParameters);
                reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                return reader;
            }
            catch
            {
                command.Dispose();
                connection.Close();
                throw;
            }
        }

        /// <summary>
        /// 执行数据库查询操作,返回DataSet类型的结果集
        /// </summary>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作返回的DataSet类型的结果集</returns>
        public static DataSet ExecuteDataSet(string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleConnection connection = new OracleConnection(connstr);
            DataSet dataset = null;

            try
            {
                PrepareCommand(command, connection, null, CommandType.Text, cmdText, commandParameters);
                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                dataset = new DataSet();
                adapter.Fill(dataset);
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return dataset;
        }

        /// <summary>
        /// 执行数据库查询操作,返回DataTable类型的结果集
        /// </summary>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作返回的DataTable类型的结果集</returns>
        public static DataTable ExecuteDataTable(string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleConnection connection = new OracleConnection(connstr);
            DataTable table = null;

            try
            {
                PrepareCommand(command, connection, null, CommandType.Text, cmdText, commandParameters);
                OracleDataAdapter adapter = new OracleDataAdapter();
                adapter.SelectCommand = command;
                table = new DataTable();
                adapter.Fill(table);
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return table;
        }

        /// <summary>
        /// 执行数据库查询操作,返回结果集中位于第一行第一列的Object类型的值
        /// </summary>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作返回的结果集中位于第一行第一列的Object类型的值</returns>
        public static object ExecuteScalar(string cmdText, params OracleParameter[] commandParameters)
        {
            OracleCommand command = new OracleCommand();
            OracleConnection connection = new OracleConnection(connstr);
            object result = null;

            try
            {
                PrepareCommand(command, connection, null, CommandType.Text, cmdText, commandParameters);
                result = command.ExecuteScalar();
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        ///    <summary>
        ///    执行数据库事务查询操作,返回结果集中位于第一行第一列的Object类型的值
        ///    </summary>
        ///    <param name="transaction">一个已存在的数据库事务对象</param>
        ///    <param name="commandType">命令类型</param>
        ///    <param name="commandText">Oracle存储过程名称或PL/SQL命令</param>
        ///    <param name="commandParameters">命令参数集合</param>
        ///    <returns>当前事务查询操作返回的结果集中位于第一行第一列的Object类型的值</returns>
        public static object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText, params OracleParameter[] commandParameters)
        {
            if (transaction == null) throw new ArgumentNullException("当前数据库事务不存在");
            OracleConnection connection = transaction.Connection;
            if (connection == null) throw new ArgumentException("当前事务所在的数据库连接不存在");

            OracleCommand command = new OracleCommand();
            object result = null;

            try
            {
                PrepareCommand(command, connection, transaction, commandType, commandText, commandParameters);
                result = command.ExecuteScalar();
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                transaction.Dispose();
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 执行数据库查询操作,返回结果集中位于第一行第一列的Object类型的值
        /// </summary>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="cmdType">Command类型</param>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns>当前查询操作返回的结果集中位于第一行第一列的Object类型的值</returns>
        public static object ExecuteScalar(OracleConnection connection, CommandType cmdType, string cmdText, params OracleParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentException("当前数据库连接不存在");
            OracleCommand command = new OracleCommand();
            object result = null;

            try
            {
                PrepareCommand(command, connection, null, cmdType, cmdText, commandParameters);
                result = command.ExecuteScalar();
                command.Parameters.Clear();
            }
            catch
            {
                throw;
            }
            finally
            {
                command.Dispose();
                connection.Close();
                connection.Dispose();
            }

            return result;
        }

        /// <summary>
        /// 执行数据库命令前的准备工作
        /// </summary>
        /// <param name="command">Command对象</param>
        /// <param name="connection">数据库连接对象</param>
        /// <param name="trans">事务对象</param>
        /// <param name="cmdType">Command类型</param>
        /// <param name="cmdText">Oracle存储过程名称或PL/SQL命令</param>
        /// <param name="commandParameters">命令参数集合</param>
        private static void PrepareCommand(OracleCommand command, OracleConnection connection, OracleTransaction trans, CommandType cmdType, string cmdText, OracleParameter[] commandParameters)
        {
            if (connection.State != ConnectionState.Open) connection.Open();

            command.Connection = connection;
            command.CommandText = cmdText;
            command.CommandType = cmdType;

            if (trans != null) command.Transaction = trans;

            if (commandParameters != null)
            {
                foreach (OracleParameter parm in commandParameters)
                    command.Parameters.Add(parm);
            }
        }

        /// <summary>
        /// 将.NET日期时间类型转化为Oracle兼容的日期时间格式字符串
        /// </summary>
        /// <param name="date">.NET日期时间类型对象</param>
        /// <returns>Oracle兼容的日期时间格式字符串（如该字符串：TO_DATE('2007-12-1','YYYY-MM-DD')）</returns>
        public static string GetOracleDateFormat(DateTime date)
        {
            return "TO_DATE('" + date.ToString("yyyy-M-dd") + "','YYYY-MM-DD')";
        }

        /// <summary>
        /// 将.NET日期时间类型转化为Oracle兼容的日期格式字符串
        /// </summary>
        /// <param name="date">.NET日期时间类型对象</param>
        /// <param name="format">Oracle日期时间类型格式化限定符</param>
        /// <returns>Oracle兼容的日期时间格式字符串（如该字符串：TO_DATE('2007-12-1','YYYY-MM-DD')）</returns>
        public static string GetOracleDateFormat(DateTime date, string format)
        {
            if (format == null || format.Trim() == "") format = "YYYY-MM-DD";
            return "TO_DATE('" + date.ToString("yyyy-M-dd") + "','" + format + "')";
        }

        /// <summary>
        /// 将指定的关键字处理为模糊查询时的合法参数值
        /// </summary>
        /// <param name="source">待处理的查询关键字</param>
        /// <returns>过滤后的查询关键字</returns>
        public static string HandleLikeKey(string source)
        {
            if (source == null || source.Trim() == "") return null;

            source = source.Replace("[", "[]]");
            source = source.Replace("_", "[_]");
            source = source.Replace("%", "[%]");

            return ("%" + source + "%");
        }

        /// <summary>
        /// 将文本内容写入到数据库的CLOB字段中（不可用：报连接被关闭的异常）
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据库表名称</param>
        /// <param name="where">指定的WHERE条件语句</param>
        /// <param name="clobField">CLOB字段的名称</param>
        /// <param name="content">要写入的文本内容</param>
        public static void WriteCLOB(string table, string where, string clobField, string content)
        {
            if (String.IsNullOrEmpty(connstr) || String.IsNullOrEmpty(table) || String.IsNullOrEmpty(clobField)) return;

            using (OracleConnection connection = new OracleConnection(connstr))
            {
                OracleCommand command = null;

                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT " + clobField + " FROM " + table + " WHERE " + where + " FOR UPDATE";
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        command.Transaction = command.Connection.BeginTransaction();

                        OracleLob lob = reader.GetOracleLob(0);
                        byte[] buffer = Encoding.Unicode.GetBytes(content);
                        if (lob != OracleLob.Null) lob.Erase();
                        lob.Write(buffer, 0, ((buffer.Length % 2 == 0) ? buffer.Length : (buffer.Length - 1)));

                        command.Transaction.Commit();
                        reader.Close();
                    }
                }
                catch
                {
                    command.Transaction.Rollback();
                    throw;
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        /// <summary>
        /// 从数据库中读取CLOB字段的内容并进行输出
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据库表名称</param>
        /// <param name="where">指定的WHERE条件语句</param>
        /// <param name="clobField">CLOB字段的名称</param>
        /// <param name="output">保存内容输出的字符串变量</param>
        public static void ReadCLOB(string connectionString, string table, string where, string clobField, ref string output)
        {
            if (String.IsNullOrEmpty(connectionString) || String.IsNullOrEmpty(table) || String.IsNullOrEmpty(clobField)) return;

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                OracleCommand command = null;
                StreamReader stream = null;

                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    command.CommandText = "SELECT " + clobField + " FROM " + table + " WHERE " + where;
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader != null && reader.HasRows)
                    {
                        reader.Read();
                        command.Transaction = command.Connection.BeginTransaction();

                        OracleLob lob = reader.GetOracleLob(0);
                        if (lob != OracleLob.Null)
                        {
                            stream = new StreamReader(lob, Encoding.Unicode);
                            output = stream.ReadToEnd().Trim();
                            command.Transaction.Commit();
                            reader.Close();
                        }
                    }
                }
                catch
                {
                    command.Transaction.Rollback();
                    throw;
                }
                finally
                {
                    stream.Close();
                    command.Dispose();
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public static int InputLong(string strSql, string data)
        {
            int result = 0;
            using (OracleConnection connection = new OracleConnection(connstr))
            {
                OracleCommand command = null;
                try
                {
                    connection.Open();
                    command = connection.CreateCommand();
                    //if (command.Transaction == null)
                    //    command.Transaction = command.Connection.BeginTransaction();
                    command.CommandText = (strSql ?? "");
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    int num = strSql.IndexOf(":", 0);

                    if (num <= 0)
                    {

                        result = -1;
                    }
                    else
                    {
                        string parameterName = strSql.Substring(num + 1, 1);
                        OracleParameter oracleParameter = command.Parameters.Add(parameterName, OracleType.LongVarChar);
                        oracleParameter.Direction = ParameterDirection.Input;
                        oracleParameter.Value = data;
                        result = command.ExecuteNonQuery();
                    }
                }
                catch
                {

                }
            }
            return result;
        }
    }
}

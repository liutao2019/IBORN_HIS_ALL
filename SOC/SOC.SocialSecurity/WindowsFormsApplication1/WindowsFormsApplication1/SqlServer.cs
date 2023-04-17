using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// PacsOperation 的摘要说明。
    /// </summary>
    public class Sqlserver
    {
        System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
        System.Data.SqlClient.SqlCommand command = new System.Data.SqlClient.SqlCommand();
        System.Data.SqlClient.SqlTransaction trans;
        string ConnectionString = "packet size=4096;user id=hisforlis;data source=192.168.8.2;persist security info=True;initial catalog=his_lis;password=123";
        string Err = "";


        public Sqlserver()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public int Connect()
        {
            if (conn.ConnectionString.Length <= 0)
            {
                conn.ConnectionString = ConnectionString;
            }
            command.Connection = conn;
            command.CommandType = System.Data.CommandType.Text;

            try
            {
                conn.Open();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                return -1;
            }

            try
            {
                trans = conn.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                command.Transaction = trans;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 执行sql，返回DataSet
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="DataSet"></param>
        /// <returns></returns>
        public int ExecQuery(string strSql, ref DataSet DataSet)
        {
            this.command.Connection = this.conn;
            this.command.CommandType = System.Data.CommandType.Text;
            this.command.Parameters.Clear();
            this.command.CommandText = strSql + "";
            try
            {
                System.Data.SqlClient.SqlDataAdapter adapter = new SqlDataAdapter(this.command);
                adapter.Fill(DataSet);
            }
            catch
            {
                return -1;
            }
           
            return 0;
        }

    }
}

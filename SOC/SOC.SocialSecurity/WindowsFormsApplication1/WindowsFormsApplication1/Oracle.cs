using System;
using System.Collections;
using System.Data.OracleClient;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// PacsOperation 的摘要说明。
    /// </summary>
    public class Oracle
    {
        System.Data.OracleClient.OracleConnection conn = new System.Data.OracleClient.OracleConnection();
        System.Data.OracleClient.OracleCommand command = new System.Data.OracleClient.OracleCommand();
        System.Data.OracleClient.OracleDataReader reader;
        string ConnectionString = "data source=zdlyhis;password=119dba114 ;persist security info=True;user id=zdlyhis ";
        string Err = "";

        public Oracle()
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
            catch (System.Data.OracleClient.OracleException ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                return -1;
            }

            return 1;
        }

        public int ExecNoQuery(string strSql)
        {
            this.command.Connection = this.conn;
            this.command.CommandType = System.Data.CommandType.Text;
            this.command.Parameters.Clear();

            this.command.CommandText = strSql + "";
            int i = 0;
            try
            {
                i = this.command.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }

            return i;
        }
    }
}

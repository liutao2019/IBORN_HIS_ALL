using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data.Odbc;
using FoShanSI.Function;

namespace FoShanSI.Management
{
    /// <summary>
    /// 医保数据库连接
    /// 张琦2010-7
    /// 1、加载数据库连接串-将医院编码提取出来配置文件
    /// 2、现连接可能为多个连接，所以保证在一个事物内，则必须方法前后不能断开连接
    /// 单事物可以自动提交，多事物必须Commit
    /// </summary>
    public class SIDataBase
    {
        public SIDataBase()
        {

            siConnect = new OdbcConnection();
            siConnect.ConnectionString = Function.Function.GetConnectString();
            //New Command
            siCommand = new OdbcCommand();
            siCommand.Connection = siConnect;//设置当前语句连接
            siCommand.CommandType = System.Data.CommandType.Text;
            siCommand.CommandTimeout = 300;
            try
            {
                siConnect.Open();
            }
            catch (Exception ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                this.ErrCode = "-1";
                Function.LogManager.Write(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                throw ex;
            }

            //连接已经打开，开启事务
            if (siConnect.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    siTrans = siConnect.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
                    siCommand.Transaction = siTrans;
                }
                catch (Exception ex)
                {
                    this.Err = "数据库连接失败!" + ex.Message;
                    this.ErrCode = "-1";
                    Function.LogManager.Write(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                    siTrans.Rollback();
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 释放资源时，关闭数据库
        /// </summary>
        ~SIDataBase()
        {
            try
            {
                if (siConnect.State == System.Data.ConnectionState.Open)
                {
                    siConnect.Close();
                    siConnect.Dispose();
                }
            }
            catch (Exception ex)
            {
                this.Err = "数据库连接失败!" + ex.Message;
                this.ErrCode = "-1";
                Function.LogManager.Write(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
            }
        }

        #region 变量

        /// <summary>
        /// 数据库连接
        /// </summary>
        System.Data.IDbConnection siConnect = null;

        /// <summary>
        /// 数据库操作命令
        /// </summary>
        System.Data.IDbCommand siCommand = null;

        /// <summary>
        /// 数据库事务
        /// </summary>
        System.Data.IDbTransaction siTrans = null;

        /// <summary>
        /// Reader
        /// </summary>
        protected System.Data.IDataReader siReader = null;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Err;

        /// <summary>
        /// 错误编码
        /// </summary>
        public string ErrCode;

        #endregion

        #region 数据库基本操作

        /// <summary>
        /// 提交
        /// </summary>
        public void Commit()
        {
            if (siTrans != null)
            {
                siTrans.Commit();
                LogManager.Write("【提交事务成功】");
            }
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void RollBack()
        {
            if (siTrans != null)
            {
                siTrans.Rollback();
                LogManager.Write("【提交回滚成功】");
            }
        }

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (siConnect.State == System.Data.ConnectionState.Open)
            {
                //siConnect.Dispose();
                //siConnect.Close();
                //LogManager.Write("【断开连接】");
            }
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        public void BeginTranscation()
        {
            this.Open();
            if (siTrans.Connection == null)
            {
                siTrans = siConnect.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
            }
            siCommand.Transaction = siTrans;
            LogManager.Write("【启动事务】");
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            if (siConnect.State != System.Data.ConnectionState.Open)
            {
                siConnect.ConnectionString = Function.Function.GetConnectString();
                siCommand.Connection = siConnect;
                siCommand.CommandType = System.Data.CommandType.Text;

                try
                {
                    siConnect.Open();
                    LogManager.Write("【信息】连接医保中间数据库！");
                }
                catch (Exception ex)
                {
                    this.Err = "数据库连接失败!" + ex.Message;
                    this.ErrCode = "-1";
                    Function.LogManager.Write(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 执行更新,删除,插入等SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int ExecNoQuery(string sql)
        {
            siCommand.CommandText = sql;

            try
            {
                LogManager.Write("【执行SQL】" + sql);
                return siCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.Err = "读取数据库失败!" + ex.Message + "|" + sql;
                this.ErrCode = "-1";
                Function.LogManager.Write(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
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
            if (siConnect.ConnectionString == "")
                return -1;

            siCommand.CommandText = sql;

            if (this.siReader != null && !this.siReader.IsClosed)
            {
                this.siReader.Close();
            }
            try
            {
                LogManager.Write("【执行查询SQL】" + sql);
                siReader = siCommand.ExecuteReader();
            }
            catch (Exception ex)
            {
                this.Err = "读取数据库失败!" + ex.Message + "|" + sql;
                this.ErrCode = "-1";
                Function.LogManager.Write(this.GetType().ToString() + ":" + this.Err + this.ErrCode);
                return -1;
            }

            return 0;
        }
        #endregion
    }
}

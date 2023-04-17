using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    ///Message的摘要说明。{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
    /// 消息管理类
    /// </summary>
    public class MessageLogic:FS.FrameWork.Management.Database
    {
        public MessageLogic()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 取信息序列
        /// </summary>
        /// <returns></returns>
        public string GetNewMessageID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.Message.GetNewMessageID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }

        /// <summary>
        /// 插入一条数据
        /// </summary>
        /// <param name="curePhase"></param>
        /// <returns></returns>
        public int InsertMessage(FS.HISFC.Models.Order.Message msg)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Message.Insert.1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            strSql = this.FormatMessageInfo(strSql, msg);
            if (strSql == null)
            {
                this.Err = "格式化Sql语句时出错";
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// 格式化SQL语句
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="curePhase"></param>
        /// <returns></returns>
        private string FormatMessageInfo(string strSql, FS.HISFC.Models.Order.Message msg)
        {
            string mySql = "";
            try
            {
                System.Object[] s = {
                                        msg.Message_id.ToString(),//主键
                                        msg.Card_NO.ToString(),
                                        msg.Name.ToString(),
                                        msg.Phone,
                                        msg.MessageType,
                                        msg.MessageChannel,
                                        msg.MessageTitle,
                                        msg.Content.ToString(),
                                        msg.SendResult,
                                        msg.ExtField01,
                                        msg.ExtField02,
                                        msg.ExtField03,
                                        msg.OperCode.ToString(),
                                        msg.OperName.ToString(),
                                        msg.OperTime
									};
                string myErr = "";
                if (FS.FrameWork.Public.String.CheckObject(out myErr, s) == -1)
                {
                    this.Err = myErr;
                    this.WriteErr();
                    return null;
                }
                mySql = string.Format(strSql, s);
            }
            catch (System.Exception ex)
            {
                this.Err = "赋值时候出错！" + ex.Message;
                this.WriteErr();
                return null;
            }
            return mySql;
        }


    }
}

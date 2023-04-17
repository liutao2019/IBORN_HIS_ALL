using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    ///MessageTemplateLogic的摘要说明。{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}{D37652D3-1DB3-4f8c-AFE6-BE21625F082C}
    /// 消息模板管理类
    /// </summary>
   public class MessageTemplateLogic : FS.FrameWork.Management.Database
    {
       public MessageTemplateLogic()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

       /// <summary>
       /// 取信息模板序列
       /// </summary>
       /// <returns></returns>
       public string GetNewMessageID()
       {
           string sql = "";
           if (this.Sql.GetCommonSql("Order.Message.GetNewMessageTemplateID", ref sql) == -1) return null;
           string strReturn = this.ExecSqlReturnOne(sql);
           if (strReturn == "-1" || strReturn == "") return null;
           return strReturn;
       }

       /// <summary>
       /// 查询
       /// </summary>
       /// <param name="strSql"></param>
       /// <returns></returns>
       private ArrayList myMsgTemplateQuery(string strSql)
       {
           ArrayList al = new ArrayList();

           if (this.ExecQuery(strSql) == -1) return null;

           try
           {
               while (this.Reader.Read())
               {
                   FS.HISFC.Models.Order.MessageTemplate msg = new FS.HISFC.Models.Order.MessageTemplate();
                   try
                   {
                       msg.MessageTemplateId = this.Reader[0].ToString();
                       msg.MsgTemplateType =this.Reader[1].ToString();
                       msg.MsgTemplateChannel = this.Reader[2].ToString();
                       msg.MsgTemplateTitle = this.Reader[3].ToString();
                       msg.MsgTemplateContent = this.Reader[4].ToString();
                       msg.Sortid = this.Reader[5].ToString();
                       msg.State = this.Reader[6].ToString();
                       msg.Extfield01 = this.Reader[7].ToString();
                       msg.Extfield02 = this.Reader[8].ToString();
                       msg.Extfield03 = this.Reader[9].ToString();
                       msg.Opercode = this.Reader[10].ToString();
                       msg.Opername = this.Reader[11].ToString();
                       msg.Opertime =  FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());
                       msg.Createcode = this.Reader[13].ToString();
                       msg.Createname= this.Reader[14].ToString();
                       msg.Createtime =  FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                   }
                   catch (Exception ex)
                   {
                       this.Err = "获得短信模板出错！" + ex.Message;
                       this.WriteErr();
                       return null;
                   }
                   al.Add(msg);
               }
           }
           catch (Exception ex)
           {
               this.Err = "获得短信模板出错！" + ex.Message;
               this.WriteErr();
               return null;
           }
           finally
           {
               this.Reader.Close();
           }
           return al;
       }


       /// <summary>
       /// 查询所有短信模板
       /// </summary>
       /// <param name="inPatientNO"></param>
       /// <returns></returns>
       public ArrayList QueryMsgTemplateAll()
       {
           string strSql = "";
           string strSql1 = "";
           ArrayList al = new ArrayList();

           if (this.Sql.GetCommonSql("Order.MessageTemplate.Select.All", ref strSql) == -1)
           {
               this.Err = this.Sql.Err;
               return null;
           }

           //if (this.Sql.GetCommonSql("Order.CurePhase.QueryCurePhase.Where1", ref strSql1) == -1)
           //{
           ////    this.Err = this.Sql.Err;
            //   return null;
           //}
          // strSql = strSql + " " + string.Format(strSql1, inPatientNO);

           return this.myMsgTemplateQuery(strSql);
       }

      
       public ArrayList QueryeTemplateByid(string id)
       {
           string strSql = "";
          
           ArrayList al = new ArrayList();

           if (this.Sql.GetCommonSql("Order.MessageTemplate.Select.ById", ref strSql) == -1)
           {
               this.Err = this.Sql.Err;
               return null;
           }

           string exesql = string.Format(strSql,id);
           return this.myMsgTemplateQuery(exesql);
       }

       public ArrayList QueryeTemplateByTitle(string tile)
       {
           string strSql = "";

           ArrayList al = new ArrayList();

           if (this.Sql.GetCommonSql("Order.MessageTemplate.Select.ByTile", ref strSql) == -1)
           {
               this.Err = this.Sql.Err;
               return null;
           }

           string exesql = string.Format(strSql, tile);
           return this.myMsgTemplateQuery(exesql);
       }

       /// <summary>
       /// 插入一条数据
       /// </summary>
       /// <param name="curePhase"></param>
       /// <returns></returns>
       public int InsertMessageTemplate(FS.HISFC.Models.Order.MessageTemplate msg)
       {
           string strSql = "";

           if (this.Sql.GetCommonSql("Order.MessageTemplate.Insert.1", ref strSql) == -1)
           {
               this.Err = this.Sql.Err;
               return -1;
           }
           strSql = this.FormatMessageTemplateInfo(strSql, msg);
           if (strSql == null)
           {
               this.Err = "格式化Sql语句时出错";
               this.WriteErr();
               return -1;
           }
           return this.ExecNoQuery(strSql);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       /// <param name="curePhase"></param>
       /// <returns></returns>
       public int UpdateMessageTemplate(FS.HISFC.Models.Order.MessageTemplate msg)
       {
           string strSql = "";

           if (this.Sql.GetCommonSql("Order.MessageTemplate.Update.1", ref strSql) == -1)
           {
               this.Err = this.Sql.Err;
               return -1;
           }
           strSql = this.FormatMessageTemplateInfo(strSql, msg);
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
       private string FormatMessageTemplateInfo(string strSql, FS.HISFC.Models.Order.MessageTemplate msg)
       {
           string mySql = "";
           try
           {
               System.Object[] s = {
                                       msg.MessageTemplateId.ToString(),
                                       msg.MsgTemplateType.ToString(),
                                       msg.MsgTemplateChannel.ToString(),
                                       msg.MsgTemplateTitle,
                                       msg.MsgTemplateContent,
                                       msg.Sortid,
                                       msg.State,
                                       msg.Extfield01,
                                       msg.Extfield02,
                                       msg.Extfield03,
                                       msg.Opercode,
                                       msg.Opername,
                                       msg.Opertime,
                                       msg.Createcode,
                                       msg.Createname,
                                       msg.Createtime

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

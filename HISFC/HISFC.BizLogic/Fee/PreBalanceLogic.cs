using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Fee.Inpatient;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Fee
{
    /// <summary>
    /// PreBalanceLogic<br></br>
    /// [功能描述: 预结算业务类]<br></br>
    /// [创 建 者: lzd]<br></br>
    /// [创建时间: 2021-02-08]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
   public class PreBalanceLogic : FS.FrameWork.Management.Database
   {
       #region 私有函数
       
       /// <summary>
       /// 取预结算信息  //{C4231074-D350-4df9-AF7C-C37124B44B80}
       /// </summary>
       /// <param name="sql">当前Sql语句,不是SQLID</param>
       /// <returns>成功返回PreBalance数组 失败返回null</returns>
       private List<PreBalance> QueryPreBalanceBySql(string sql)
       {
           List<PreBalance> items = new List<PreBalance>(); //用于返回非药品信息的数组
           //执行当前Sql语句
           if (this.ExecQuery(sql) == -1)
           {
               this.Err = this.Sql.Err;
               return null;
           }
           try
           {
               //循环读取数据
               while (this.Reader.Read())
               {
                   PreBalance balanceBase = new PreBalance();
                   balanceBase.PREBLANCENO = this.Reader[0].ToString();// 
                   balanceBase.INPATIENTNO = this.Reader[1].ToString();// 
                   balanceBase.NAME = this.Reader[2].ToString();// 
                   balanceBase.BALANCEPRICE = NConvert.ToDecimal(this.Reader[3].ToString());// 
                   balanceBase.BALANCEDATE = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());// 
                   balanceBase.OPERCODE = this.Reader[5].ToString();// 
                   balanceBase.OPERNAME = this.Reader[6].ToString();// 
                   balanceBase.PACKAGEIDS = this.Reader[7].ToString();// 
                   balanceBase.MEMO = this.Reader[8].ToString();// 
                   balanceBase.ISVALID = this.Reader[9].ToString();// 
                   balanceBase.CANCELDATE = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());// 
                   balanceBase.CANCELCODE = this.Reader[11].ToString();// 
                   balanceBase.CANCELMAN = this.Reader[12].ToString();//
                   items.Add(balanceBase);
               }

               return items;
           }
           catch (Exception e)
           {
               this.Err = "获得预结算基本信息出错！" + e.Message;
               this.WriteErr();

               return null;
           }
           finally
           {
               if (Reader != null && !Reader.IsClosed)
               {
                   this.Reader.Close();
               }
           }
       }


       /// <summary>
       /// 取预结算明细信息
       /// </summary>
       /// <param name="sql">当前Sql语句,不是SQLID</param>
       /// <returns>成功返回PreBalanceList数组 失败返回null</returns>
       private List<PreBalanceList> QueryPreBalanceDetailBySql(string sql)
       {
           List<PreBalanceList> items = new List<PreBalanceList>(); //用于返回非药品信息的数组
           //执行当前Sql语句
           if (this.ExecQuery(sql) == -1)
           {
               this.Err = this.Sql.Err;
               return null;
           }
           try
           {
               //循环读取数据
               while (this.Reader.Read())
               {
                   PreBalanceList balanceBase = new PreBalanceList();
                   balanceBase.PREBLANCENO = this.Reader[0].ToString();// 
                   balanceBase.SEQUENCE_NO = this.Reader[1].ToString();
                   balanceBase.ITEM_CODE = this.Reader[2].ToString();
                   balanceBase.FEE_CODE = this.Reader[3].ToString();
                   balanceBase.ITEM_NAME = this.Reader[4].ToString();
                   balanceBase.UNIT_PRICE = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                   balanceBase.QTY = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
                   balanceBase.CURRENT_UNIT = this.Reader[7].ToString();
                   balanceBase.PACKAGE_CODE = this.Reader[8].ToString();
                   balanceBase.PACKAGE_NAME = this.Reader[9].ToString();
                   balanceBase.Spec = this.Reader[10].ToString();
                   balanceBase.PRICEUNIT = this.Reader[11].ToString();
                   items.Add(balanceBase);
               }

               return items;
           }
           catch (Exception e)
           {
               this.Err = "获得预结算基本信息出错！" + e.Message;
               this.WriteErr();

               return null;
           }
           finally
           {
               if (Reader != null && !Reader.IsClosed)
               {
                   this.Reader.Close();
               }
           }
       }

       #endregion

       #region 公有函数

       #region 查询预结算记录
       /// <summary>
       /// 预结算头表
       /// </summary>
       /// <param name="inpatienno"></param>
       /// <param name="validState"></param>
       /// <returns>成功list 失败:返回null</returns>
       public ArrayList QueryPreBalanceByInNoAndValid(string inpatienno, string validState)
       {
           string Sql = string.Empty;
           if (this.Sql.GetCommonSql("Fee.PreBalance.Query.ByInNoAndValid", ref Sql) == -1) return null;
           Sql = string.Format(Sql, inpatienno, "1");
           List<PreBalance> list = this.QueryPreBalanceBySql(Sql);
           if (list != null)
           {
               return new ArrayList(list);
           }
           return null;
       }

       #endregion

       #region 查询预结算明细记录
       /// <summary>
       /// 预结算头表
       /// </summary>
       /// <param name="inpatienno"></param>
       /// <param name="validState"></param>
       /// <returns>成功list 失败:返回null</returns>
       public ArrayList QueryPreBalanceDetailByPreBalanceNo(string prebalanceno)
       {
           string Sql = string.Empty;
           if (this.Sql.GetCommonSql("Fee.PreBalanceList.Query.Byprebalanceno", ref Sql) == -1) return null;
           Sql = string.Format(Sql, prebalanceno);
           List<PreBalanceList> list = this.QueryPreBalanceDetailBySql(Sql);
           if (list != null)
           {
               return new ArrayList(list);
           }
           return null;
       }

       #endregion

       #region 插入预结算记录
       /// <summary>
       /// 插入预结算记录
       /// </summary>
       /// <param name="accountCard"></param>
       /// <returns></returns>
       public int InsertPreBalance(PreBalance balanceBase)
       {
           string Sql = string.Empty;
           if (this.Sql.GetCommonSql("Fee.PreBalance.InsertPreBalance", ref Sql) == -1) return -1;
           try
           {
               Sql = string.Format(Sql,
                                   balanceBase.PREBLANCENO.ToString(),
                                   balanceBase.INPATIENTNO.ToString(),
                                   balanceBase.NAME,
                                   FS.FrameWork.Function.NConvert.ToDecimal(balanceBase.BALANCEPRICE),
                                   FS.FrameWork.Function.NConvert.ToDateTime(balanceBase.BALANCEDATE),
                                   balanceBase.OPERCODE.ToString(),
                                   balanceBase.OPERNAME.ToString(),
                                   balanceBase.PACKAGEIDS,
                                   balanceBase.MEMO,
                                   balanceBase.ISVALID.ToString(),
                                   FS.FrameWork.Function.NConvert.ToDateTime(balanceBase.CANCELDATE),
                                   balanceBase.CANCELCODE,
                                   balanceBase.CANCELMAN
                                   );
           }
           catch (Exception ex)
           {
               this.Err = ex.Message;
               this.ErrCode = ex.Message;
               return -1;
           }
           return this.ExecNoQuery(Sql);
       }

        #endregion

       #region 插入预结算明细记录
       /// <summary>
       /// 插入预结算明细记录 {983972e8-1c2e-4502-8fe9-2acd5beb5a0a}
       /// </summary>
       /// <param name="accountCard"></param>
       /// <returns></returns>
       public int InsertPreBalanceList(PreBalanceList detail)
       {
           string Sql = string.Empty;
           if (this.Sql.GetCommonSql("Fee.PreBalanceList.InsertPreBalanceList", ref Sql) == -1) return -1;
           try
           {
               Sql = string.Format(Sql,
                   detail.PREBLANCENO,
                   detail.SEQUENCE_NO,
                   detail.ITEM_CODE,
                   detail.FEE_CODE,
                   detail.ITEM_NAME,
                   FS.FrameWork.Function.NConvert.ToDecimal(detail.UNIT_PRICE),
                   FS.FrameWork.Function.NConvert.ToDecimal(detail.QTY),
                   detail.CURRENT_UNIT,
                   detail.PACKAGE_CODE,
                   detail.PACKAGE_NAME,
                   detail.Spec,
                   detail.PRICEUNIT,
                   detail.Order_ID
                                   );
           }
           catch (Exception ex)
           {
               this.Err = ex.Message;
               this.ErrCode = ex.Message;
               return -1;
           }
           return this.ExecNoQuery(Sql);
       }

       #endregion

       #region  更新预结算记录
       /// <summary>
       /// 更新预结算记录
       /// </summary>
       /// <param name="cardNo"></param>
       /// <param name="coupon"></param>
       /// <param name="operType"></param>
       /// <returns></returns>
       public int UpdatePreBalance(PreBalance balanceBase)
       {
            string Sql = string.Empty;
            if (this.Sql.GetCommonSql("Fee.PreBalance.UpdtatePreBalance", ref Sql) == -1) return -1;
            try
            {
                  Sql = string.Format(Sql,
                                   balanceBase.PREBLANCENO.ToString(),
                                   balanceBase.INPATIENTNO.ToString(),
                                   balanceBase.NAME.ToString(),
                                   FS.FrameWork.Function.NConvert.ToDecimal(balanceBase.BALANCEPRICE),
                                   FS.FrameWork.Function.NConvert.ToDateTime(balanceBase.BALANCEDATE),
                                   balanceBase.OPERCODE.ToString(),
                                   balanceBase.OPERNAME.ToString(),
                                   balanceBase.PACKAGEIDS.ToString(),
                                   balanceBase.MEMO.ToString(),
                                   balanceBase.ISVALID.ToString(),
                                   FS.FrameWork.Function.NConvert.ToDateTime(balanceBase.CANCELDATE),
                                   balanceBase.CANCELCODE.ToString(),
                                   balanceBase.CANCELMAN.ToString()
                                   );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(Sql);

       }
       #endregion

       /// <summary>
       /// 获取一个新的流水号
       /// </summary>
       /// <param name="ClinicCode"></param>
       /// <returns></returns>
       public string GetNewPreBalanceNo()
       {
           return this.GetSequence("Package.Fee.GetPreBalanceNo");
       }

       /// <summary>
       /// 获取新的明细流水号
       /// </summary>
       /// <returns></returns>
       public string GetNewDetailSequenceNo()
       {
           return this.GetSequence("Package.Fee.GetPreBalanceDetailSequence");
       }

       #endregion

       

   }
}

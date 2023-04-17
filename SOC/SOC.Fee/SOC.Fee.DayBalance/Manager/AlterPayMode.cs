using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace SOC.Fee.DayBalance.Manager
{
    /// <summary>
    /// 修改支付方式业务层类  {47DBEF28-E946-4719-B9B7-9BB1160CED0A}
    /// </summary>
    public class AlterPayMode : FS.FrameWork.Management.Database
    {

        /// <summary>
        /// 获取住院支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <returns></returns>
        public DataSet GetPayModeInBanlance(string BillNo, string TransType)
        {
            string strSql = string.Empty;
            strSql = @"select p.invoice_no 收据号,
                              p.trans_type 交易类型,
                              p.balance_no 交易序号,
                              p.cost 金额,
                              p.pay_way 支付方式,
                              p.balance_opercode 操作员,
                              p.balance_date 操作日期,
                              p.reutrnorsupply_flag 标记,
                              decode(p.daybalance_flag, '1', '已结算', '未结算') 日结状态
                         from fin_ipb_balancepay p
                        where p.invoice_no = '{0}'
                          and p.trans_type = '{1}'
                          and p.trans_kind = '1'";
            try
            {
                strSql = string.Format(strSql, BillNo, TransType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }

        /// <summary>
        /// 获取押金支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <returns></returns>
        public DataSet GetPayModeInprepay(string BillNo, string TransType)
        {

            string strSql = string.Empty;
            strSql = @"select i.receipt_no 收据号,
                              i.prepay_state 交易类型,
                              i.happen_no 交易序号,
                              i.prepay_cost 金额,
                              i.pay_way 支付方式,
                              i.oper_code 操作员,
                              i.oper_date 操作日期,
                              '' 标记,
                              decode(i.daybalance_flag,'1','已日结','未日结') 日结状态
                       from fin_ipb_inprepay i 
                       where i.receipt_no='{0}'
                       and i.prepay_state='{1}'";
            try
            {
                strSql = string.Format(strSql, BillNo, TransType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }

        /// <summary>
        /// 获取门诊支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <returns></returns>
        public DataSet GetPayModeClinicFee(string BillNo, string TransType)
        {
            string strSql = string.Empty;
            strSql = @"select p.invoice_no 收据号,
                              decode(p.trans_type, '0', '退费', '收费') 交易类型,
                              p.sequence_no 交易序号,
                              p.tot_cost 金额,
                              p.mode_code 支付方式,
                              (select e.empl_name
                                 from com_employee e
                                where e.empl_code = p.oper_code) 操作员,
                              p.balance_date 操作日期,
                              invoice_seq 发票序列号,
                              decode(balance_flag, '1', '已日结', '未日结') 日结
                         from fin_opb_paymode p
                        where p.invoice_no = '{0}'
                          and p.trans_type = '{1}'";
            try
            {
                strSql = string.Format(strSql, BillNo, TransType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }
        /// <summary>
        /// 获取门诊充值支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <returns></returns>
        public DataSet GetPayModeAcount(string BillNo, string TransType)
        {

            string strSql = string.Empty;

            strSql = @"select a.invoice_no   收据号,
                               a.cancel_flag  交易类型,
                               a.happen_no    交易序号,
                               a.prepay_cost  金额,
                               a.prepay_type  支付方式,
                               a.oper_code    操作员,
                               a.oper_date    操作日期,
                               a.account_no   帐户,
                               a.balance_flag 日结
                          from fin_opb_accountprepay a
                         where a.invoice_no = '{0}'
                           --and a.account_no = '{1}'
                           and a.cancel_flag = '1'";
            try
            {
                strSql = string.Format(strSql, BillNo, TransType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }
        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <returns></returns>
        public DataSet GetPayModeBoardFee(string BillNo)
        {

            string strSql = string.Empty;
            strSql = @"select i.receipt_no 收据号,
                              i.prepay_state 交易类型,
                              i.happen_no 交易序号,
                              i.prepay_cost 金额,
                              i.pay_way 支付方式,
                              i.oper_code 操作员,
                              i.oper_date 操作日期,
                              '' 标志,
                              decode(i.daybalance_flag,'1','已日结','未日结') 日结状态
                         from fin_ipb_inprepay i
                        where i.receipt_no = '{0}'
                          and i.prepay_state = '{1}'";

            try
            {
                strSql = string.Format(strSql, BillNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }

      
        /// <summary>
        /// 获取套餐支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <returns></returns>
        public DataSet GetPackagePayModeBoardFee(string BillNo, string TransType)
        {

            string strSql = string.Empty;
            strSql = @"select xpm.invoice_no 收据号,xpm.trans_type 交易类型,xpm.sequence_no 交易序号,xpm.real_cost 金额,xpm.mode_code 支付方式,
xpm.oper_code 操作员,xpm.oper_date 操作日期,'' 标志,decode(xpm.balance_flag,'1','已日结','未日结') 日结状态 
from exp_packagepaymode xpm where xpm.invoice_no='{0}' and xpm.trans_type='{1}'";

            try
            {
                strSql = string.Format(strSql, BillNo,TransType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }


        /// <summary>
        /// 获取套餐押金支付信息
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="TransType">交易类型 1正交易，2反交易</param>
        /// <returns></returns>
        public DataSet GetPackagePrePayModeBoardFee(string BillNo,string tran_type)
        {

            string strSql = string.Empty;
            strSql = @"select xpdm.deposit_no 收据号,xpdm.trans_type 交易类型, xpdm.deposit_no  交易序号,xpdm.amount 金额,xpdm.mode_code 支付方式,xpdm.oper_code 操作员,
xpdm.oper_date 操作日期,'' 标志, decode(xpdm.balance_flag,'1','已日结','未日结') 日结状态  from exp_packagedeposit xpdM
where xpdm.deposit_no='{0}' and xpdm.trans_type='{1}'";

            try
            {
                strSql = string.Format(strSql, BillNo, tran_type);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            DataSet ds = new DataSet();

            this.ExecQuery(strSql, ref ds);

            return ds;
        }



        /// <summary>
        /// 更改支付方式
        /// </summary>
        /// <param name="BillNo">收据号</param>
        /// <param name="Transtype">交易类型 1正交易 2 反交易</param>
        /// <param name="TransKind">交易种类 0预交款1结算款</param>
        /// <param name="No">序号</param>
        /// <param name="rsFlag">收取返还标志</param>
        /// <param name="OldPayWay">原支付方式</param>
        /// <param name="NewPayWay">新支付方式</param>
        /// <returns>1成功 其他失败</returns>
        public int UpdateInBalance(string BillNo, string TransType, string TransKind, int No, string rsFlag, string OldPayWay, string NewPayWay)
        {
            string strSql = "";

            strSql = @"update fin_ipb_balancepay p
                          set p.pay_way = '{5}'
                        where p.invoice_no = '{0}'
                          and p.balance_no = {1}
                          and p.trans_type = '{2}'
                          and p.trans_kind = '{6}'
                          and p.reutrnorsupply_flag = '{3}'
                          and p.pay_way = '{4}'";
            strSql = string.Format(strSql, BillNo, No.ToString(), TransType, rsFlag, OldPayWay, NewPayWay,TransKind);

            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "更新数据出错，更新行数不不是1，" + this.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更改支付方式
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="TransType"></param>
        /// <param name="No"></param>
        /// <param name="OldPayWay"></param>
        /// <param name="NewPayWay"></param>
        /// <returns></returns>
        public int UpdateInPrepay(string BillNo, string TransType, int No, string OldPayWay, string NewPayWay)
        {
            string strSql = "";
            strSql = @"update fin_ipb_inprepay i
                          set i.pay_way = '{4}'
                        where i.receipt_no = '{0}'
                          and i.happen_no = {1}
                          and i.prepay_state = '{2}'
                          and i.pay_way = '{3}'";
            strSql = string.Format(strSql, BillNo, No.ToString(), TransType, OldPayWay, NewPayWay);

            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "更新数据出错，更新行数不不是1，" + this.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更改套餐支付方式
        /// </summary>
        /// <param name="BillNo">票据号</param>
        /// <param name="TransType">交易方式</param>
        /// <param name="No">序列号</param>
        /// <param name="OldPayWay">旧支付方式 </param>
        /// <param name="NewPayWay">新支付方式</param>
        /// <returns></returns>
        public int UpdatePackagePayMode(string BillNo, string TransType, int No, string OldPayWay, string NewPayWay)
        {
            string strSql = "";
            strSql = @"update exp_packagepaymode xpm set xpm.mode_code='{0}' 
where xpm.invoice_no='{1}' and xpm.sequence_no='{2}' and xpm.trans_type='{3}' and xpm.mode_code='{4}'";
            strSql = string.Format(strSql, NewPayWay, BillNo, No.ToString (),TransType, OldPayWay);

            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "更新数据出错，更新行数不不是1，" + this.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更改套餐押金支付方式
        /// </summary>
        /// <param name="BillNo">票据号</param>
        /// <param name="TransType">交易方式</param>
        /// <param name="No">序列号</param>
        /// <param name="OldPayWay">旧支付方式 </param>
        /// <param name="NewPayWay">新支付方式</param>
        /// <returns></returns>
        public int UpdatePackagePrePayMode(string BillNo, string TransType,  string OldPayWay, string NewPayWay)
        {
            string strSql = "";
            strSql = @"update exp_packagedeposit xpdM set xpdm.mode_code='{0}' where xpdm.deposit_no='{1}' and xpdm.trans_type='{2}' and xpdm.mode_code='{3}' ";
            strSql = string.Format(strSql, NewPayWay, BillNo,  TransType, OldPayWay);

            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "更新数据出错，更新行数不不是1，" + this.Err;
                return -1;
            }
            return 1;
        }


      /// <summary>
      /// 插入变更记录表
      /// </summary>
      /// <param name="invoiceNO"></param>
      /// <param name="happenNO"></param>
      /// <param name="trankind"></param>
      /// <param name="tranType"></param>
      /// <param name="oldpayWay"></param>
      /// <param name="newPayWay"></param>
      /// <returns></returns>
      public int InsertPayWayShiftInfo(string invoiceNO,string happenNO,int trankind,string tranType,string oldpayWay,string newPayWay)
        {
            string strSql = string.Empty;
            string sql = @"select max(cp.happen_no) from COM_SHIFTDATA_PAYWAY cp where cp.invoice_no='{0}' 
and cp.tran_type='{1}' and cp.tran_kind='{2}' and cp.old_happenno='{3}'";
            sql = string.Format(sql, invoiceNO, tranType, trankind, happenNO);
          string maxhappandNo = "";
          maxhappandNo= this.ExecSqlReturnOne(sql,maxhappandNo);
          int maxno=0;
          if(!string .IsNullOrEmpty (maxhappandNo)){
              maxno=Convert .ToInt32(maxhappandNo)+1;
          }
         
            strSql = @"INSERT INTO COM_SHIFTDATA_PAYWAY
                          (INVOICE_NO,
                           HAPPEN_NO,
                           TRAN_KIND,
                           TRAN_TYPE,
                           OLD_PAYWAY,
                           NEW_PAYWAY,
                           OPER_CODE,
                           OPER_DATE,OLD_HAPPENNO)
                        VALUES
                          ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', SYSDATE,'{7}')";
            strSql = string.Format(strSql, invoiceNO, maxno, trankind, tranType, oldpayWay, newPayWay, this.Operator.ID,happenNO);
            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "插入变更信息出错" + this.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更改支付方式
        /// </summary>
        /// <param name="BillNo"></param>
        /// <param name="TransType"></param>
        /// <param name="No"></param>
        /// <param name="OldPayWay"></param>
        /// <param name="NewPayWay"></param>
        /// <returns></returns>
        public int UpdateAccount(string BillNo, string accountNo, int No, string OldPayWay, string NewPayWay)
        {
            string strSql = "";
            strSql = @"update fin_opb_accountprepay a
                       set a.prepay_type = '{4}'
                     where a.invoice_no = '{0}'
                       and a.account_no = '{1}'
                       and a.happen_no = '{2}'
                       and a.prepay_type = '{3}'";

            strSql = string.Format(strSql, BillNo, accountNo, No.ToString(), OldPayWay, NewPayWay);

            if (this.ExecNoQuery(strSql) != 1)
            {
                this.Err = "更新数据出错，更新行数不不是1，" + this.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更改支付方式
        /// </summary>
        /// <param name="invoiceNO"></param>
        /// <param name="TransType"></param>
        /// <param name="invoiceSeq"></param>
        /// <param name="seqNo">交易流水号</param>
        /// <param name="OldPayWay"></param>
        /// <param name="NewPayWay"></param>
        /// <returns></returns>
        public int UpdateClinicFee(string invoiceNO, string TransType, string invoiceSeq, string seqNo, string OldPayWay, string NewPayWay)
        {
            string strSql = "";
            strSql = @"update fin_opb_paymode p
                       set mode_code = '{4}'
                       where p.invoice_no='{0}' 
                         and p.trans_type='{1}'
                         and p.invoice_seq='{2}'
                         and p.mode_code='{3}'
                         and p.sequence_no={5}";
            strSql = string.Format(strSql, invoiceNO, TransType, invoiceSeq, OldPayWay, NewPayWay, seqNo);

            int rev = ExecNoQuery(strSql);

            if (rev > 1 || rev == 0)
            {
                this.Err = "更新数据出错，更新行数不正确（不为1）！\r\n" + this.Err;
                return -1;
            }
            else if (rev < 0)
            {
                this.Err = "更新数据出错！\r\n" + this.Err;
                return -1;
            }
            return 1;
        }
    }
}

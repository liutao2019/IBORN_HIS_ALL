using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Fee.Inpatient;

namespace FS.SOC.Local.InpatientFee.GuangZhou.ChangePact
{
    public class ChangePactManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 备份身份变更前的费用数据
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="flag"> 1:药品 0：非药品</param>
        /// <param name="dtChange"> 身份变更时间</param>
        /// <returns></returns>
        public int BackOldFeeDetail(string inpatientNo,int flag,DateTime dtChange)
        {
            string strSql = "";
            if (0 == flag)
            {
                strSql = @"insert  into fin_ipb_itemlist_back  select f.*,to_date('{1}','yyyy-mm-dd hh24:mi:ss') from fin_ipb_itemlist f   where  inpatient_no='{0}'";
            }
            else
            {
                strSql = @"insert  into fin_ipb_medicinelist_back  select f.*,to_date('{1}','yyyy-mm-dd hh24:mi:ss') from fin_ipb_medicinelist f   where  inpatient_no='{0}'";

            }
            try
            {
                //0 住院流水号1身份变更时间
                strSql = string.Format(strSql, inpatientNo, dtChange.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
 
        }

        /// <summary>
        /// 更新费用明细
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <param name="alFeeItemList"></param>
        /// <returns></returns>
        public int UpdateFeeItemList(FS.HISFC.Models.RADT.PatientInfo p, FeeItemList feeitem)
        {
            string strSql = "";
            if (feeitem.Item.ItemType==FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                strSql = @"update fin_ipb_medicinelist set pub_cost={3}, own_cost={4},pay_cost={5},eco_cost={6} where   inpatient_no='{0}' and RECIPE_NO='{1}' and SEQUENCE_NO='{1}' and TRANS_TYPE='{2}'";
            }
            else
            {
                strSql = @"update fin_ipb_itemlist set pub_cost={3}, own_cost={4},pay_cost={5},eco_cost={6} where   inpatient_no='{0}' and RECIPE_NO='{1}' and SEQUENCE_NO='{1}' and TRANS_TYPE='{2}'";
            }
            try
            {
                string trans_type = "1";
                if (feeitem.TransType==FS.HISFC.Models.Base.TransTypes.Negative)
                {
                    trans_type = "2";
                }
                //0 住院流水号1身份变更时间
                strSql = string.Format(strSql, p.ID, feeitem.RecipeNO, feeitem.SequenceNO, trans_type,feeitem.FT.PubCost,feeitem.FT.OwnCost,feeitem.FT.PayCost,feeitem.FT.DerateCost);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
           
        }

        /// <summary>
        /// 获取费用汇总信息
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="balanceState"></param>
        /// <param name="flag"> 1:药品 0：非药品</param>
        /// <returns></returns>
        public ArrayList QueryFeeInfo(string inpatientNo, string balanceState,int flag)
        {
            string strSql = "";
            try
            {
                if (1 == flag)
                {
                    strSql = @"select un.recipe_no,un.fee_code,un.execute_deptcode,un.balance_no,sum(nvl(un.tot_cost,0)),sum(nvl(un.pub_cost,0)),
sum(nvl(pay_cost,0)),sum(nvl(own_cost,0)) from fin_ipb_medicinelist un where un.inpatient_no='{0}'
and un.balance_state='{1}'
group by un.recipe_no,un.fee_code,un.execute_deptcode,un.balance_no
order by un.recipe_no,un.fee_code,un.execute_deptcode,un.balance_no";
                }
                else
                {
                    strSql = @"select un.recipe_no,un.fee_code,un.execute_deptcode,un.balance_no,sum(nvl(un.tot_cost,0)),sum(nvl(un.pub_cost,0)),
sum(nvl(pay_cost,0)),sum(nvl(own_cost,0)) from fin_ipb_itemlist un where un.inpatient_no='{0}'
and un.balance_state='{1}'
group by un.recipe_no,un.fee_code,un.execute_deptcode,un.balance_no
order by un.recipe_no,un.fee_code,un.execute_deptcode,un.balance_no";
                }
                strSql = string.Format(strSql, inpatientNo, balanceState);
                this.ExecQuery(strSql);

                FS.HISFC.Models.Fee.Inpatient.FeeInfo FeeInfo;
                ArrayList al = new ArrayList();

                while (this.Reader.Read())
                {
                    FeeInfo = new FeeInfo();
                    try
                    {

                        FeeInfo.RecipeNO = this.Reader[0].ToString();
                        FeeInfo.Item.MinFee.ID = this.Reader[1].ToString();
                        FeeInfo.ExecOper.Dept.ID = this.Reader[2].ToString();
                        FeeInfo.BalanceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                        FeeInfo.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4].ToString());
                        FeeInfo.FT.PubCost= FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                        FeeInfo.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
                        FeeInfo.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获取费用汇总信息 赋值错误" + ex.Message;
                        this.ErrCode = ex.Message;
                        return null;
                    }
                    al.Add(FeeInfo);
 
                }
                this.Reader.Close();
                return al;
            }
            catch (Exception exp)
            {

                this.Err = "获取费用汇总信息错误" + exp.Message;
                this.ErrCode = exp.Message;
                return null;
            }
 
        }



      //public int UpdateOrInsertFeeInfo(string inpatientNo,)

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.InpatientFee.BizLogic
{
    /// <summary>
    /// [功能描述: 收费汇总业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class FeeInfo:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 插入所有不在FeeInfo中的明细汇总信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int InsertAllNotInFeeInfo(string inpatientNO)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.InsertAllDetailNotInFeeInfo, inpatientNO, this.Operator.ID);
            
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 查询所有汇总费用（明细不在fee_info中的总和）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT GetSumFeeByNotInFeeInfo(string inpatientNO)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.SelectSumFeeByNotInFeeInfo, inpatientNO);

            if (this.ExecQuery(sql) < 0)
            {
                this.WriteErr();
                return null;
            }

            FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT() ;
            if (this.Reader != null)
            {
                if (this.Reader.Read())
                {
                    ft = new FS.HISFC.Models.Base.FT();
                    ft.ID = this.Reader[0].ToString();
                    ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1]);
                    ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2]);
                    ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    ft.RebateCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                }
            }
            if (this.Reader != null && !this.Reader.IsClosed)
            {
                this.Reader.Close();
            }

            return ft;
        }

        /// <summary>
        /// 更新费用主表（未出院状态，排除O和C）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="ft"></param>
        /// <returns></returns>
        public int UpdateMainFee(string inpatientNO, FS.HISFC.Models.Base.FT ft)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.UpdateMainFee, inpatientNO, ft.TotCost, ft.OwnCost, ft.PayCost, ft.PubCost, ft.RebateCost);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新所有新汇总费用的费用来源（FTSource.Source3=1）
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public int UpdateAllNewFeeFTSource(string inpatientNO)
        {
            string sql = string.Format(FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.UpdateFTSource, inpatientNO);

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 存储过程处理费用拆分
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEndTime"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int ProcedueSplitFee(string inpatientNO, DateTime dtBegin, DateTime dtEndTime, string operCode)
        {
            string strSQL = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.ProcedueSplitFee;
            string strReturn = string.Empty;

            try
            {
                strSQL = string.Format(strSQL, inpatientNO, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEndTime.ToString("yyyy-MM-dd HH:mm:ss"), operCode);
                if (this.ExecEvent(strSQL, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[0]) == -1)
                {
                    this.Err = str[1];
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 存储过程处理费用拆分
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEndTime"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int ProcedueCombineFee(string inpatientNO, DateTime dtBegin, DateTime dtEndTime, string operCode)
        {
            string strSQL = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.ProcedueCombineFee;
            string strReturn = string.Empty;

            try
            {
                strSQL = string.Format(strSQL, inpatientNO, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEndTime.ToString("yyyy-MM-dd HH:mm:ss"), operCode);
                if (this.ExecEvent(strSQL, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[0]) == -1)
                {
                    this.Err = str[1];
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 存储过程处理身份变更的费用
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEndTime"></param>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public int ProcedueChangePactFee(string inpatientNO, FS.HISFC.Models.Base.PactInfo oldPact, FS.HISFC.Models.Base.PactInfo newPact, string operCode)
        {
            string strSQL = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeInfo.Current.ProcedueChangePactFee;
            string strReturn = string.Empty;

            try
            {
                strSQL = string.Format(strSQL, inpatientNO,oldPact.PayKind.ID,oldPact.ID,newPact.PayKind.ID,newPact.ID, operCode);
                if (this.ExecEvent(strSQL, ref strReturn) == -1)
                {
                    this.Err = "执行存储过程出错！" + this.Err;
                    return -1;
                }
                string[] str = strReturn.Split(',');
                if (FS.FrameWork.Function.NConvert.ToInt32(str[0]) == -1)
                {
                    this.Err = str[1];
                    return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = this.Err + ex.Message;
                return -1;
            }
        }

    }
}

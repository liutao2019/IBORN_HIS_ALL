using System;
using System.Collections.Generic;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Registration;
using FS.FrameWork.Function;
using System.Windows.Forms;
using System.Data;
using FS.FrameWork.Management;

namespace InterfaceInstanceDefault.IFeeOweMessage
{
    /// <summary>
    /// 医生站直接收费
    /// </summary>
    public class FeeOweMessageDefault : FS.HISFC.BizProcess.Interface.FeeInterface.IFeeOweMessage
    {

        /// <summary>
        /// 费用类业务层 {2CEA3B1D-2E59-44ac-9226-7724413173C5} 对业务层的引用全部改为非静态的变量
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        private FS.FrameWork.Management.DataBaseManger dbm = new FS.FrameWork.Management.DataBaseManger();
       
        #region IFeeOweMessage 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="ft"></param>
        /// <param name="feeItemLists"></param>
        /// <param name="type"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        public bool FeeOweMessage(FS.HISFC.Models.RADT.PatientInfo patient, FT ft, ArrayList feeItemLists, ref MessType type, ref string err)
        {
            
            //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
            #region 按照金额判断
            //{56F3CD2F-64A3-4bbe-9ECE-BBF5F6944412}
            decimal freeCost = 0;
            if (patient.Pact.PayKind.ID == "02" || patient.Pact.PayKind.ID == "03")
            {
                freeCost = patient.FT.PrepayCost;//预交金
            }
            else
            {
                freeCost = patient.FT.LeftCost;//余额
            }
            decimal moneyAlert = patient.PVisit.MoneyAlert;//警戒线
            decimal totCost = 0m;//费用金额
            decimal surtyCost = 0m;//担保金额
            if (type != MessType.N)
            {
                //查找担保金额
                string resultValue = inpatientManager.GetSurtyCost(patient.ID);
                if (resultValue == "-1")
                {
                    err = "查找担保金额失败！";
                    return false ;
                }
                surtyCost = NConvert.ToDecimal(resultValue);
            }

            totCost = ft.OwnCost;

            #region 加入绿色通道判断
            DataSet ds = new DataSet();
            bool currStatus = false;
            string sql = string.Empty;
            sql = @"
select   OPER_DATE, OPER_CODE, OPER_NAME, decode(STATUS,'0','已终止','1','已办理')
  from TJ_LOCAL_GREEN_PASSAGE_LOG where INPATIENT_NO = '" + patient .ID + @"'
 order by oper_date desc
";
            if (dbm.ExecQuery(sql, ref ds) < 0)
            {
                MessageBox.Show(Language.Msg("查询患者办理/终止记录出错！"));
                return false;
            }
            if (ds.Tables.Count <= 0)
            {
                MessageBox.Show(Language.Msg("查询患者办理/终止记录出错！"));
                return false;
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][3].ToString() == "已办理")
                {
                    currStatus = true;
                }
            }
            //3） 一旦办理绿色通道的患者缴纳过预交金则绿色通道的这种担保服务自动取消，如果欠费按正常欠费患者处理 
            ArrayList al =
            inpatientManager.QueryPrepays(patient.ID);
            if (al != null)
            {
                if (al.Count > 0)
                {
                    currStatus = false;
                }
            }
            //2） 整个住院过程中不判断欠费
            if (currStatus == true)
            {
                surtyCost += 9999999999;
            }
            #endregion
            decimal MessCost = freeCost + surtyCost - totCost - moneyAlert;
            //按照时间段判断的时间范围
            DateTime beginDate = NConvert.ToDateTime(patient.PVisit.BeginDate.ToString("yyyy-MM-dd") + " 00:00:00");
            DateTime endDate = NConvert.ToDateTime(patient.PVisit.EndDate.ToString("yyyy-MM-dd") + " 23:59:59");
            DateTime now = inpatientManager.GetDateTimeFromSysDateTime();
            //欠费判断类型
            string alertType = patient.PVisit.AlertType.ID.ToString();
            //是否欠费
            bool isOwn = freeCost + surtyCost - totCost < moneyAlert;
            bool isValid = true;

            //按照时间段判断如果在时间范围内则不判断欠费
            if (alertType == EnumAlertType.D.ToString())
            {
                if (now >= beginDate && now <= endDate)
                {
                    isValid = false;
                }
            }

            if (isOwn && isValid)
            {
                if (type == MessType.Y)
                {
                    err = "患者: " + patient.Name + " 余额不足，不能进行收费！" + "\n" + "请补交" + (-MessCost).ToString() + "元";
                    return false;
                }
                if (type == MessType.M)
                {
                    if (MessageBox.Show("患者: " + patient.Name + "余额不足是否继续收费？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        err = "已取消收费！";
                        return false;
                    }
                }
                freeCost -= totCost;
            }
            #endregion

            return true;
        }

        #endregion
    }
}

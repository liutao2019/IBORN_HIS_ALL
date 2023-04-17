using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace FS.SOC.Local.ZDLY.PubReport.BizLogic
{
    /// <summary>
    /// PublicReport 的摘要说明。
    /// </summary>
    public class PubReport : FS.FrameWork.Management.Database
    {
        public PubReport()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        public int InsertStatic(FS.HISFC.Models.RADT.PatientInfo p, string flag, DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();
            //Local.Function myFun = new Function();
            FS.SOC.Local.ZDLY.PubReport.BizLogic.PubReport rp = new PubReport();
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo myPact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            FS.SOC.Local.ZDLY.PubReport.BizLogic.Fee locFee = new Fee();
            locFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            rp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myPact.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isExist = false; //患者的托收信息是否已经存在
            DateTime lastStatic; //患者最后一次统计时间

            DateTime pBeginDate;
            DateTime pEndDate;
            int iReturn = 0;

            ArrayList alFee = new ArrayList();//患者的费用信息;

            if (flag == "0")//月统计
            {
                #region 月统计
                //如果患者的统计信息已经存在那么提示
                string temp = rp.IsExist(p.ID, dtBegin, dtEnd);
                if (temp == null)
                {
                    this.Err = "查询患者是否存在托收单出错!";
                    return -1;
                }
                if (FS.FrameWork.Function.NConvert.ToInt32(temp) > 1)
                {
                    this.Err = "该患者在统计时间段内存在多条托收信息，请合并后再统计!";
                    return -1;
                }
                isExist = FS.FrameWork.Function.NConvert.ToBoolean(temp);

                //判断代码
                if (isExist)
                {
                    FS.SOC.Local.ZDLY.PubReport.Models.PubReport tempReport = new FS.SOC.Local.ZDLY.PubReport.Models.PubReport();
                    tempReport = rp.GetPubReport(p.ID, dtBegin.ToString(), dtEnd.ToString());
                    if (tempReport == null)
                    {
                        this.Err = "获得患者的托收单信息出错!";
                        return -1;
                    }
                    DialogResult r = MessageBox.Show(p.Name + "的托收信息已经存在，单号为: " + tempReport.Bill_No + "是否重新统计并作废原来的托收单?",
                           "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (r == DialogResult.No)
                    {
                        return 0;
                    }
                    else
                    {

                        //删除作废信息
                        iReturn = rp.DeletePubReport(tempReport.ID);
                        if (iReturn <= 0)
                        {
                            MessageBox.Show("删除作废托收信息出错!" + rp.Err);
                            return -1;
                        }

                    }
                }
                //查找患者的最后一次统计时间，如果没有，那么说明患者一致没有打印
                //托说单，开始查询的时间设置为患者的入院日期.
                temp = rp.GetLastStaticTime(p.ID, dtEnd.Date.ToString());
                if (temp == null)
                {
                    this.Err = "查找患者的最后统计时间出错!" + rp.Err;
                    return -1;
                }
                lastStatic = FS.FrameWork.Function.NConvert.ToDateTime(temp);
                if (lastStatic.Year == 1900)//该患者没有统计过,患者的统计时间为患者的入院时间
                {
                    if (p.PVisit.InTime < dtBegin)
                    {
                        if (p.PVisit.InTime < new DateTime(2005, 12, 21, 0, 0, 0))
                        {
                            pBeginDate = new DateTime(2005, 12, 21, 0, 0, 0);
                        }
                        else
                        {
                            pBeginDate = p.PVisit.InTime;
                        }
                    }
                    else
                    {
                        pBeginDate = p.PVisit.InTime;
                    }
                }
                else if (lastStatic < dtBegin)//该患者统计过，但是不是本期统计，那么患者的开始统计时间为上次统计时间
                {
                    pBeginDate = lastStatic.AddSeconds(1);
                }
                else //患者的统计时间为统一的开始时间
                {
                    pBeginDate = dtBegin;
                }
                pEndDate = dtEnd; //如果是月统计，患者的结束统计时间为统一的结束时间.
                #endregion
            }
            else //结算拖收单
            {
                //删除开始时间后所有未结算的托收单
                if (rp.DeletePubReport(p.ID, dtBegin, dtEnd) < 0)
                {
                    this.Err = rp.Err;
                    return -1;
                }
                pBeginDate = dtBegin;
                pEndDate = dtEnd;
            }

            if (flag == "0")
            {
                //				//重新调整日限定额
                //				if(myFee.AdjustOverLimitFee(p)==-1)
                //				{
                //					t.RollBack();
                //					MessageBox.Show("调整日限额出错"+myFee.Err,"提示");
                //					return -1;
                //				}				
            }
            //在获得本期费用之前调用调整费用函数，插入调整数据 By Maokb 06-03-13
            if (locFee.AdjustPubFeeTial(p, pBeginDate.ToString(), pEndDate.ToString()) == -1)
            {
                this.Err = "调整数据出错!" + locFee.Err;
                return -1;
            }
            //if(pBeginDate < new DateTime(2006,1,22
            //获得患者的本期费用信息;
            DataSet ds = rp.GetPatientFee(p.ID);

            if (ds == null)
            {
                this.Err = "获得患者费用信息出错!" + rp.Err;
                return -1;
            }

            FS.HISFC.Models.Base.FT ft = rp.GetSpDrugFee(p.ID);

            decimal overLimitDrugFee = rp.GetOverLimitDrugFee(p.ID);

            FS.SOC.Local.ZDLY.PubReport.Models.PubReport report = new FS.SOC.Local.ZDLY.PubReport.Models.PubReport();
            decimal totPubCost = 0;//实付总额 pub
            decimal totReportCost = 0;//费用总额 pay + pub

            pact = myPact.GetPactUnitInfoByPactCode(p.Pact.ID);
            if (p.ExtendFlag1 == "1")
            {
                pact.Rate.PayRate = p.FT.FTRate.PayRate;
            }
            if (pact == null)
            {
                this.Err = "获得患者费用比例出错!" + myPact.Err;
                return -1;
            }

            if (flag == "0")//月统计则统计月份为 dtEnd的整数日期
            {
                report.Static_Month = dtEnd.Date;//统计月份为最后统计时间.
            }
            else //结算统计
            {
                report.Static_Month = dtEnd.Date;
            }
            report.Bill_No = rp.GetSeq("SEQ_GFHZ_BILLNO");
            if (p.SSN == null || p.SSN == "")
            {
                this.Err = p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号为空，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Pact = this.GetCardNo(p);
            if (report.Pact.ID == null || report.Pact.ID == "")
            {
                this.Err = p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号不符合规则，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Fee_Type = "2";//住院费用

            report.IsValid = "1";
            report.Begin = pBeginDate;
            if (p.PVisit.InState.ID.ToString() == "O" || flag == "1" || p.PVisit.InState.ID.ToString() == "B")//结算患者
            {
                #region 结算患者
                report.IsInHos = "0";//出院

                pEndDate = p.PVisit.OutTime;
                report.Amount = (p.PVisit.OutTime.Date - pBeginDate.Date).Days;

                #endregion
            }
            else
            {
                report.Amount = (pEndDate.Date - pBeginDate.Date).Days;
                report.IsInHos = "1"; //在院
            }
            report.End = pEndDate;


            report.ID = rp.GetSeq("SEQ_GFHZ");
            report.Pact.Name = p.ID;
            report.Pay_Rate = pact.Rate.PayRate;

            #region 计算费用明细
            decimal totCost = 0, ownCost = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string sortId = "";
                string feeCode = "";
                decimal payCost = 0, pubCost = 0;

                sortId = row[0].ToString();
                feeCode = row[1].ToString();
                totCost += FS.FrameWork.Function.NConvert.ToDecimal(row[2].ToString());
                ownCost += FS.FrameWork.Function.NConvert.ToDecimal(row[3].ToString());
                payCost = FS.FrameWork.Function.NConvert.ToDecimal(row[4].ToString());
                pubCost = FS.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());
                //省诊和本院患者诊金算省诊
                //				if(((p.Pact.ID.ToString().Length == 2 && p.Pact.ID.ToString().Substring(0,1) == "1") ||
                //					p.Pact.ID.Substring(0,1) == "7")&& feeCode == "016")
                if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                    report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                    report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" ||
                    report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                    report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4) && feeCode == "020")
                {
                    totPubCost += pubCost;
                    totReportCost += payCost + pubCost;
                }
                else
                {
                    totPubCost += pubCost;
                    totReportCost += payCost + pubCost;
                }


                switch (feeCode)
                {
                    case "010": //床位费
                    case "093":
                    case "094":
                        report.Bed_Fee += payCost + pubCost;
                        break;
                    case "001": //药品费
                        report.YaoPin += payCost + pubCost;
                        break;
                    case "002": //成药费
                        report.ChengYao += payCost + pubCost;
                        break;
                    case "008": 
                    case "064"://化验(2)
                        report.HuaYan += payCost + pubCost;
                        break;
                    case "006":
                    case "017":
                    case "024":
                    case "027":
                    case "028":
                    case "049":
                    case "050":
                    case "051":
                    case "052":
                    case "069":
                    case "077":
                    case "078":
                    case "090":
                    case "091"://检查费(14)
                        report.JianCha += payCost + pubCost;
                        break;
                    case "063"://放射费					
                        report.FangShe += payCost + pubCost;
                        break;
                    case "004":
                    case "009":
                    case "012":
                    case "014":
                    case "043":
                    case "045":
                    case "046":
                    case "047":
                    case "054":
                    case "072":
                    case "074":
                    case "079"://治疗(13)                  
                        report.ZhiLiao += payCost + pubCost;
                        break;
                    case "007":
                    case "025":
                    case "071"://手术(3)
                        report.ShouShu += payCost + pubCost;
                        break;
                    case "023"://输血
                        report.ShuXue += payCost + pubCost;
                        break;
                    case "029"://输氧
                        report.ShuYang += payCost + pubCost;
                        break;
                    case "059"://MR
                        report.MR += payCost + pubCost;
                        break;
                    case "016"://CT
                        report.CT += payCost + pubCost;
                        break;
                    case "020"://诊金
                        if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                            report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                            report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" || report.Pact.ID == "84" ||
                            report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                            report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4)) //&& feeCode == "008")
                        {
                            //report.ShengZhen += pubCost;
                            report.ZhenJin += payCost + pubCost;
                        }
                        else
                        {
                            report.ZhenJin += payCost + pubCost;
                        }
                        break;
                    case "003"://草药
                        report.CaoYao += payCost + pubCost;
                        break;
                    //case "093"://监护床位费
                    //    report.BedFee_JianHu += payCost + pubCost;
                    //    break;
                    //case "094"://层流床位费
                    //    report.BedFee_CengLiu += payCost + pubCost;
                    //    break;
                    case "005"://护理费
                        report.JianHu += payCost + pubCost;
                        break;
                    default://其他
                        report.ZhiLiao += payCost + pubCost;
                        break;
                }
            }
            #endregion


            report.Tot_Cost = totReportCost;
            report.Pub_Cost = totPubCost;
            report.MCardNo = p.SSN;
            report.FEE_FLAG = "0";//正常普通帐
            report.REP_FLAG = "0";//未统计报表

            if (ft != null)//有30%费用
            {

                report.SpDrugFeeTot = ft.PubCost + ft.PayCost;//Edit By Maokb

            }

            if (report.Pub_Cost == 0)
            {
                return 0;
            }
            //查询高检费
            DataSet dsGJundrug = GetPatientTPundrugFee(p.ID);
            if (dsGJundrug != null && dsGJundrug.Tables.Count > 0 && dsGJundrug.Tables[0].Rows.Count > 0)
            {
                decimal gjTotPub = 0;
                decimal gjPayCost = 0;
                decimal gjPubCost = 0;
                foreach (DataRow TPdrow in dsGJundrug.Tables[0].Rows)
                {
                    string gjFeeCode = TPdrow[1].ToString();
                    gjPayCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[4].ToString());
                    gjPubCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[5].ToString());
                    switch (gjFeeCode)
                    {
                        case "008":
                        case "064"://化验(2)
                            report.HuaYan -= (gjPayCost + gjPubCost);
                            break;
                        case "006":
                        case "017":
                        case "024":
                        case "027":
                        case "028":
                        case "049":
                        case "050":
                        case "051":
                        case "052":
                        case "069":
                        case "077":
                        case "078":
                        case "090":
                        case "091"://检查费(14)
                            report.JianCha -= (gjPayCost + gjPubCost);
                            break;
                        case "063"://放射费					
                            report.FangShe -= (gjPayCost + gjPubCost);
                            break;
                        case "004":
                        case "009":
                        case "012":
                        case "014":
                        case "043":
                        case "045":
                        case "046":
                        case "047":
                        case "054":
                        case "072":
                        case "074":
                        case "079"://治疗(13)                  
                            report.ZhiLiao -= (gjPayCost + gjPubCost);
                            break;
                        case "007":
                        case "025":
                        case "071"://手术(3)
                            report.ShouShu -= (gjPayCost + gjPubCost);
                            break;
                        case "023"://输血
                            report.ShuXue -= (gjPayCost + gjPubCost);
                            break;
                        case "029"://输氧
                            report.ShuYang -= (gjPayCost + gjPubCost);
                            break;
                        case "059"://MR
                            report.MR -= (gjPayCost + gjPubCost);
                            break;
                        case "016"://CT
                            report.CT -= (gjPayCost + gjPubCost);
                            break;
                      
                        //case "093"://监护床位费
                        //    report.BedFee_JianHu -= (gjPayCost + gjPubCost);
                        //    break;
                        //case "094"://层流床位费
                        //    report.BedFee_CengLiu -= (gjPayCost + gjPubCost);
                        //    break;
                        case "005"://护理费
                            report.JianHu -= (gjPayCost + gjPubCost);
                            break;
                        default://其他
                            report.ZhiLiao -= (gjPayCost + gjPubCost);
                            break;
                    }
                    gjTotPub += (gjPayCost + gjPubCost);
                }
                report.EXT_FLAG = gjTotPub.ToString();  //高检

            }
           // report.EXT_FLAG = p.Patient.Sex.User01;  //高检

            //查询审批肿瘤药
            DataSet dsTPdrug = GetPatientTPdrugFee(p.ID);
            if (dsTPdrug != null && dsTPdrug.Tables.Count > 0 && dsTPdrug.Tables[0].Rows.Count > 0)
            {

                decimal tpPayCost = 0;
                decimal tpPubCost = 0;
                foreach (DataRow TPdrow in dsTPdrug.Tables[0].Rows)
                {
                    string tpFeeCode = TPdrow[1].ToString();
                    tpPayCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[4].ToString());
                    tpPubCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[5].ToString());
                    switch (tpFeeCode)
                    {
                        case "001":
                            report.YaoPin = report.YaoPin - tpPubCost - tpPayCost;
                            break;
                        case "002":
                            report.ChengYao = report.ChengYao - tpPubCost - tpPayCost;
                            break;
                        case "003":
                            report.CaoYao = report.CaoYao - tpPubCost - tpPayCost;
                            break;
                    }
                    report.CancerDrugFee += tpPayCost + tpPubCost;
                }

            }
           // report.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(p.Patient.Sex.User02);  //肿瘤
            report.TotalFee = totCost;
            report.SelfPay = ownCost;
            report.OverLimitDrugFee = overLimitDrugFee;

            iReturn = rp.InsertPubReport(report);
            if (iReturn <= 0)
            {
                this.Err = "插入托收信息出错!" + rp.Err;
                return -1;
            }

            return 0;
        }


        public int InsertMidStatic(FS.HISFC.Models.RADT.PatientInfo p, string flag, DateTime dtBegin, DateTime dtEnd)
        {
            FS.HISFC.BizLogic.Fee.InPatient myFee = new FS.HISFC.BizLogic.Fee.InPatient();
            //Local.Function myFun = new Function();
            FS.SOC.Local.ZDLY.PubReport.BizLogic.PubReport rp = new PubReport();
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
            FS.HISFC.BizLogic.Fee.PactUnitInfo myPact = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
            FS.SOC.Local.ZDLY.PubReport.BizLogic.Fee locFee = new Fee();
            locFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            rp.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myPact.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            myFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool isExist = false; //患者的托收信息是否已经存在
            DateTime lastStatic; //患者最后一次统计时间

            int iReturn = 0;

            ArrayList alFee = new ArrayList();//患者的费用信息;

            //删除托收单，按照时间段删除
            if (rp.DeletePubReport(p.ID, dtBegin, dtEnd) < 0)
            {
                this.Err = rp.Err;
                return -1;
            }

            //在获得本期费用之前调用调整费用函数，插入调整数据 By Maokb 06-03-13
            if (locFee.AdjustPubFeeTial(p, dtBegin.ToString(), dtEnd.ToString()) == -1)
            {
                this.Err = "调整数据出错!" + locFee.Err;
                return -1;
            }

            //获得患者的本期费用信息;
            DataSet ds = rp.GetPatientFee(p.ID, dtBegin, dtEnd);

            if (ds == null)
            {
                this.Err = "获得患者费用信息出错!" + rp.Err;
                return -1;
            }

            FS.HISFC.Models.Base.FT ft = rp.GetSpDrugFee(p.ID, dtBegin, dtEnd);

            decimal overLimitDrugFee = rp.GetOverLimitDrugFee(p.ID, dtBegin, dtEnd);

            FS.SOC.Local.ZDLY.PubReport.Models.PubReport report = new FS.SOC.Local.ZDLY.PubReport.Models.PubReport();
            decimal totPubCost = 0;//实付总额 pub
            decimal totReportCost = 0;//费用总额 pay + pub

            pact = myPact.GetPactUnitInfoByPactCode(p.Pact.ID);
            if (p.ExtendFlag1 == "1")
            {
                pact.Rate.PayRate = p.FT.FTRate.PayRate;
            }
            if (pact == null)
            {
                this.Err = "获得患者费用比例出错!" + myPact.Err;
                return -1;
            }

            if (flag == "0")//月统计则统计月份为 dtEnd的整数日期
            {
                report.Static_Month = dtEnd.Date;//统计月份为最后统计时间.
            }
            else //结算统计
            {
                report.Static_Month = dtEnd.Date;
            }
            report.Bill_No = rp.GetSeq("SEQ_GFHZ_BILLNO");
            if (p.SSN == null || p.SSN == "")
            {
                this.Err = p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号为空，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Pact = this.GetCardNo(p);
            if (report.Pact.ID == null || report.Pact.ID == "")
            {
                this.Err = p.Name + "患者(" + p.PID.PatientNO + ")的公费卡号不符合规则，这个患者暂时不处理，请处理卡号后再处理此患者";
                return 0;
            }
            report.Fee_Type = "2";//住院费用

            report.IsValid = "1";
            report.Begin = dtBegin;
            //if (p.PVisit.InState.ID.ToString() == "O" || flag == "1" || p.PVisit.InState.ID.ToString() == "B")//结算患者

            report.Amount = (dtEnd.Date - dtBegin.Date).Days + 1;
            report.IsInHos = "1"; //在院
            report.End = dtEnd;
            report.ID = rp.GetSeq("SEQ_GFHZ");
            report.Pact.Name = p.ID;
            report.Pay_Rate = pact.Rate.PayRate;

            #region 计算费用明细
            decimal totCost = 0, ownCost = 0;
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string sortId = "";
                string feeCode = "";
                decimal payCost = 0, pubCost = 0;

                sortId = row[0].ToString();
                feeCode = row[1].ToString();
                totCost += FS.FrameWork.Function.NConvert.ToDecimal(row[2].ToString());
                ownCost += FS.FrameWork.Function.NConvert.ToDecimal(row[3].ToString());
                payCost = FS.FrameWork.Function.NConvert.ToDecimal(row[4].ToString());
                pubCost = FS.FrameWork.Function.NConvert.ToDecimal(row[5].ToString());
                //省诊和本院患者诊金算省诊
                //				if(((p.Pact.ID.ToString().Length == 2 && p.Pact.ID.ToString().Substring(0,1) == "1") ||
                //					p.Pact.ID.Substring(0,1) == "7")&& feeCode == "016")
                if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                    report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                    report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" ||
                    report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                    report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4) && feeCode == "020")
                {
                    totPubCost += pubCost;
                    totReportCost += payCost + pubCost;
                }
                else
                {
                    totPubCost += pubCost;
                    totReportCost += payCost + pubCost;
                }


                switch (feeCode)
                {
                    case "010": //床位费
                    case "093":
                    case "094":
                        report.Bed_Fee += payCost + pubCost;
                        break;
                    case "001": //药品费
                        report.YaoPin += payCost + pubCost;
                        break;
                    case "002": //成药费
                        report.ChengYao += payCost + pubCost;
                        break;
                    case "008":
                    case "064"://化验(2)
                        report.HuaYan += payCost + pubCost;
                        break;
                    case "006":
                    case "017":
                    case "024":
                    case "027":
                    case "028":
                    case "049":
                    case "050":
                    case "051":
                    case "052":
                    case "069":
                    case "077":
                    case "078":
                    case "090":
                    case "091"://检查费(14)
                        report.JianCha += payCost + pubCost;
                        break;
                    case "063"://放射费					
                        report.FangShe += payCost + pubCost;
                        break;
                    case "004":
                    case "009":
                    case "012":
                    case "014":
                    case "043":
                    case "045":
                    case "046":
                    case "047":
                    case "054":
                    case "072":
                    case "074":
                    case "079"://治疗(13)                  
                        report.ZhiLiao += payCost + pubCost;
                        break;
                    case "007":
                    case "025":
                    case "071"://手术(3)
                        report.ShouShu += payCost + pubCost;
                        break;
                    case "023"://输血
                        report.ShuXue += payCost + pubCost;
                        break;
                    case "029"://输氧
                        report.ShuYang += payCost + pubCost;
                        break;
                    case "059"://MR
                        report.MR += payCost + pubCost;
                        break;
                    case "016"://CT
                        report.CT += payCost + pubCost;
                        break;
                    case "020"://诊金
                        if ((report.Pact.ID == "80" || report.Pact.ID == "81" || report.Pact.ID == "82" ||
                            report.Pact.ID == "83" || report.Pact.ID == "90" || report.Pact.ID == "J80" ||
                            report.Pact.ID == "J81" || report.Pact.ID == "J82" || report.Pact.ID == "J83" || report.Pact.ID == "84" ||
                            report.Pact.ID == "Y1" || report.Pact.ID == "Y2" || report.Pact.ID == "Y3" || report.Pact.ID == "L2" ||
                            report.Pact.ID == "80" || report.Pact.ID == "90" || report.Pact.ID.Length >= 4)) //&& feeCode == "008")
                        {
                            //report.ShengZhen += pubCost;
                            report.ZhenJin += payCost + pubCost;
                        }
                        else
                        {
                            report.ZhenJin += payCost + pubCost;
                        }
                        break;
                    case "003"://草药
                        report.CaoYao += payCost + pubCost;
                        break;
                    //case "093"://监护床位费
                    //    report.BedFee_JianHu += payCost + pubCost;
                    //    break;
                    //case "094"://层流床位费
                    //    report.BedFee_CengLiu += payCost + pubCost;
                    //    break;
                    case "005"://护理费
                        report.JianHu += payCost + pubCost;
                        break;
                    default://其他
                        report.ZhiLiao += payCost + pubCost;
                        break;
                }
            }
            #endregion


            report.Tot_Cost = totReportCost;// -report.ShengZhen;
            report.Pub_Cost = totPubCost; // -report.ShengZhen;
            report.MCardNo = p.SSN;
            report.FEE_FLAG = "0";//正常普通帐
            report.REP_FLAG = "0";//未统计报表

            if (ft != null)//有30%费用
            {
                report.SpDrugFeeTot = ft.PubCost + ft.PayCost;//Edit By Maokb
            }

            if (report.Pub_Cost == 0)
            {
                return 0;
            }
            //查询高检费
            DataSet dsGJundrug = GetPatientTPundrugFee(p.ID, dtBegin, dtEnd);
            if (dsGJundrug != null && dsGJundrug.Tables.Count > 0 && dsGJundrug.Tables[0].Rows.Count > 0)
            {
                decimal gjTotPub = 0;
                decimal gjPayCost = 0;
                decimal gjPubCost = 0;
                foreach (DataRow TPdrow in dsGJundrug.Tables[0].Rows)
                {
                    string gjFeeCode = TPdrow[1].ToString();
                    gjPayCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[4].ToString());
                    gjPubCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[5].ToString());
                    switch (gjFeeCode)
                    {
                        case "008":
                        case "064"://化验(2)
                            report.HuaYan -= (gjPayCost + gjPubCost);
                            break;
                        case "006":
                        case "017":
                        case "024":
                        case "027":
                        case "028":
                        case "049":
                        case "050":
                        case "051":
                        case "052":
                        case "069":
                        case "077":
                        case "078":
                        case "090":
                        case "091"://检查费(14)
                            report.JianCha -= (gjPayCost + gjPubCost);
                            break;
                        case "063"://放射费					
                            report.FangShe -= (gjPayCost + gjPubCost);
                            break;
                        case "004":
                        case "009":
                        case "012":
                        case "014":
                        case "043":
                        case "045":
                        case "046":
                        case "047":
                        case "054":
                        case "072":
                        case "074":
                        case "079"://治疗(13)                  
                            report.ZhiLiao -= (gjPayCost + gjPubCost);
                            break;
                        case "007":
                        case "025":
                        case "071"://手术(3)
                            report.ShouShu -= (gjPayCost + gjPubCost);
                            break;
                        case "023"://输血
                            report.ShuXue -= (gjPayCost + gjPubCost);
                            break;
                        case "029"://输氧
                            report.ShuYang -= (gjPayCost + gjPubCost);
                            break;
                        case "059"://MR
                            report.MR -= (gjPayCost + gjPubCost);
                            break;
                        case "016"://CT
                            report.CT -= (gjPayCost + gjPubCost);
                            break;

                        //case "093"://监护床位费
                        //    report.BedFee_JianHu -= (gjPayCost + gjPubCost);
                        //    break;
                        //case "094"://层流床位费
                        //    report.BedFee_CengLiu -= (gjPayCost + gjPubCost);
                        //    break;
                        case "005"://护理费
                            report.JianHu -= (gjPayCost + gjPubCost);
                            break;
                        default://其他
                            report.ZhiLiao -= (gjPayCost + gjPubCost);
                            break;
                    }
                    gjTotPub += (gjPayCost + gjPubCost);
                }
                report.EXT_FLAG = gjTotPub.ToString();  //高检

            }
            // report.EXT_FLAG = p.Patient.Sex.User01;  //高检

            //查询审批肿瘤药
            DataSet dsTPdrug = GetPatientTPdrugFee(p.ID, dtBegin, dtEnd);
            if (dsTPdrug != null && dsTPdrug.Tables.Count > 0 && dsTPdrug.Tables[0].Rows.Count > 0)
            {

                decimal tpPayCost = 0;
                decimal tpPubCost = 0;
                foreach (DataRow TPdrow in dsTPdrug.Tables[0].Rows)
                {
                    string tpFeeCode = TPdrow[1].ToString();
                    tpPayCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[4].ToString());
                    tpPubCost = FS.FrameWork.Function.NConvert.ToDecimal(TPdrow[5].ToString());
                    switch (tpFeeCode)
                    {
                        case "001":
                            report.YaoPin = report.YaoPin - tpPubCost - tpPayCost;
                            break;
                        case "002":
                            report.ChengYao = report.ChengYao - tpPubCost - tpPayCost;
                            break;
                        case "003":
                            report.CaoYao = report.CaoYao - tpPubCost - tpPayCost;
                            break;
                    }
                    report.CancerDrugFee += tpPayCost + tpPubCost;
                }

            }
            // report.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(p.Patient.Sex.User02);  //肿瘤
            report.TotalFee = totCost;
            report.SelfPay = ownCost;
            report.OverLimitDrugFee = overLimitDrugFee;

            iReturn = rp.InsertPubReport(report);
            if (iReturn <= 0)
            {
                this.Err = "插入托收信息出错!" + rp.Err;
                return -1;
            }

            return 0;
        }


        /// <summary>
        /// 查找患者再统计月份中是否已经存在.
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始统计时间</param>
        /// <param name="dtEnd">结束统计时间</param>
        /// <returns>null执行错误 "0"没有纪录 >=1 已经有纪录</returns>
        public string IsExist(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "select count(*)  from gfhz_main a where a.inpatient_no = '{2}' " +
                "and a.begin_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and invoice_no is not null";
            try
            {
                strSql = string.Format(strSql, dtBegin.ToString(), dtEnd.ToString(), inpatientNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            string temp = this.ExecSqlReturnOne(strSql);
            if (temp == null)
            {
                return null;
            }
            return temp;
        }

        /// <summary>
        /// 获得当前月份的显示时间和查询时间.
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetStaticTime()
        {
            //获得当前时间
            DateTime now = this.GetDateTimeFromSysDateTime();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            string strSql = "select * from gfhz_static where year = '" + now.Year.ToString() + "'" +
                " and month = '" + now.Month.ToString() + "'";

            if (this.ExecQuery(strSql) == -1)
                return null;

            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Memo = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
            }
            this.Reader.Close();

            return obj;
        }


        public FS.SOC.Local.ZDLY.PubReport.Models.PubReport GetPubReport(string InpatientNo,DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = string.Format(@"select * from gfhz_main a where a.inpatient_no = '{2}' " +
                "and a.begin_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and a.end_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')", dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), InpatientNo);

            ArrayList al = this.myPubReport(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al[0] as FS.SOC.Local.ZDLY.PubReport.Models.PubReport;
        }

        public FS.SOC.Local.ZDLY.PubReport.Models.PubReport GetPubReportExt(string InpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = string.Format(@"select * from gfhz_main a where a.inpatient_no = '{2}' " +
                "and a.begin_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and a.end_date <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')", dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"), InpatientNo);
            ArrayList al = this.myPubReport(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al[0] as FS.SOC.Local.ZDLY.PubReport.Models.PubReport;
        }



        private ArrayList myPubReport(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            FS.SOC.Local.ZDLY.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.SOC.Local.ZDLY.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Bill_No = this.Reader[2].ToString();
                        info.Pact.ID = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        info.Pact.Name = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperCode = this.Reader[38].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[39]);

                        info.EXT_FLAG = this.Reader[42].ToString();
                        info.EXT_FLAG2 = this.Reader[43].ToString();
                        info.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[51]);
                        info.InvoiceNo = this.Reader[57].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
        }

        /// <summary>
        /// 获取托收单集合（住院用）
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public ArrayList GetPubReportArray(string InpatientNo, string begin, string enddate)
        {
            string strSql = "select * from gfhz_main where inpatient_no = '" + InpatientNo + "' and end_date > to_date('" + begin + "', 'YYYY-MM-DD HH24:MI:SS') and end_date <= to_date('" + enddate + "', 'YYYY-MM-DD HH24:MI:SS')";

            ArrayList al = this.GetPubReportArray(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al;
        }
        /// <summary>
        /// 获取托收单集合（住院用）
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        private ArrayList GetPubReportArray(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            FS.SOC.Local.ZDLY.PubReport.Models.PubReport info;            //返回数组中的摆药单分类信息
            this.ProgressBarText = "正在检索药品信息...";
            this.ProgressBarValue = 0;
            #region
            //ID           VARCHAR2(12)                  序号                                 
            //STATIC_MONTH DATE                          统计月份                             
            //LSH          VARCHAR2(10)                  门诊费用记帐号 或者 住院费用托收单号 
            //BH           VARCHAR2(3)                   公费类别代码                         
            //ZZS          NUMBER(5)    Y                门诊患者人数                         
            //FY1          NUMBER(10,2) Y        0       01床位费                             
            //FY2          NUMBER(10,2) Y        0       02药品费                             
            //FY3          NUMBER(10,2) Y        0       03成药费                             
            //FY4          NUMBER(10,2) Y        0       04化验费                             
            //FY5          NUMBER(10,2) Y        0       05检查费                             
            //FY6          NUMBER(10,2) Y        0       06放射费                             
            //FY7          NUMBER(10,2) Y        0       07治疗费                             
            //FY8          NUMBER(10,2) Y        0       08手术费                             
            //FY9          NUMBER(10,2) Y        0       09输血费                             
            //FY10         NUMBER(10,2) Y        0       10输氧费                             
            //FY11         NUMBER(10,2) Y        0       11接生费                             
            //FY12         NUMBER(10,2) Y        0       12高氧费                             
            //FY13         NUMBER(10,2) Y        0       13MR费                               
            //FY14         NUMBER(10,2) Y        0       14CT费                               
            //FY15         NUMBER(10,2) Y        0       15血透费                             
            //FY16         NUMBER(10,2) Y        0       16诊金费                             
            //FY17         NUMBER(10,2) Y        0       17草药费                             
            //FY18         NUMBER(10,2) Y        0       18特检费                             
            //FY19         NUMBER(10,2) Y        0       19审药费                             
            //FY20         NUMBER(10,2) Y        0       20监护费                             
            //FY51         NUMBER(10,2) Y        0       51省诊费                             
            //JZZJE        NUMBER(10,2) Y        0       合计金额                             
            //SJZJE        NUMBER(10,2) Y        0       实际金额                             
            //FEE_TYPE     VARCHAR2(1)                   1 住院费用 2 门诊费用                
            //ZFBL         NUMBER(10,2) Y        0       自付比例           
            #endregion
            if (this.ExecQuery(SQLString) == -1)
                return null;
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.SOC.Local.ZDLY.PubReport.Models.PubReport();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                        info.Static_Month = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                        info.Pact.ID = this.Reader[2].ToString();
                        info.Pact.Memo = this.Reader[3].ToString();
                        info.Amount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                        info.Bed_Fee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                        info.YaoPin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                        info.ChengYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                        info.HuaYan = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[8]);
                        info.JianCha = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                        info.FangShe = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10]);
                        info.ZhiLiao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[11]);
                        info.ShouShu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        info.ShuXue = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                        info.ShuYang = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                        info.TeZhi = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        info.TeZhiRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16]);
                        info.MR = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                        info.CT = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                        info.XueTou = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);
                        info.ZhenJin = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        info.CaoYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[21]);
                        info.TeJian = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22]);
                        info.ShenYao = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                        info.JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24]);
                        info.ShengZhen = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25]);
                        info.Tot_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26]);
                        info.Pub_Cost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27]);
                        info.Fee_Type = this.Reader[28].ToString();
                        info.TeYaoRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[29].ToString());
                        info.Pay_Rate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[30]);
                        info.IsValid = this.Reader[31].ToString();
                        info.Pact.Name = this.Reader[32].ToString();//住院流水号
                        //info.InpatientNo = this.Reader[32].ToString();
                        info.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[33].ToString());
                        info.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[34].ToString());
                        info.MCardNo = this.Reader[35].ToString();
                        info.SpDrugFeeTot = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[36].ToString());
                        info.IsInHos = this.Reader[37].ToString();
                        info.OperDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[38]);
                        info.OperCode = this.Reader[39].ToString();
                        info.Seq = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[44].ToString());
                        info.SortId = this.Reader[45].ToString();
                        info.Name = this.Reader[46].ToString();//姓名
                        info.WorkName = this.Reader[47].ToString();
                        info.WorkCode = this.Reader[48].ToString();
                        info.PatientNO = this.Reader[49].ToString();
                        info.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[50]);
                        info.CancerDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[51]);
                        info.BedFee_JianHu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[52]);
                        info.BedFee_CengLiu = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[53]);
                        info.CompanyPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[54]);
                        info.SelfPay = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[55]);
                        info.TotalFee = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[56]);
                        info.InvoiceNo = this.Reader[57].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    this.ProgressBarValue++;
                    al.Add(info);
                }
                this.Reader.Close();
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得信息出错，执行SQL语句出错！myPubReport" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }

            this.ProgressBarValue = -1;
            return al;
        }

        public FS.SOC.Local.ZDLY.PubReport.Models.PubReport GetPubReport(string InpatientNo, string begin, string enddate)
        {
            string strSql = "select * from gfhz_main where inpatient_no = '" + InpatientNo + "' and end_date > to_date('" + begin + "', 'YYYY-MM-DD HH24:MI:SS') and end_date <= to_date('" + enddate + "', 'YYYY-MM-DD HH24:MI:SS')";

            ArrayList al = this.myPubReport(strSql);
            if (al == null)
                return null;
            if (al.Count == 0)
                return null;
            return al[0] as FS.SOC.Local.ZDLY.PubReport.Models.PubReport;
        }


        /// <summary>
        /// 获得指定月份的查询日期
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetStaticTime(string year, string month)
        {

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            string strSql = "select * from gfhz_static where year = '" + year + "'" +
                " and month = '" + month + "'";

            if (this.ExecQuery(strSql) == -1)
                return null;

            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Memo = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
            }
            this.Reader.Close();

            return obj;
        }

        /// <summary>
        /// 获得最近的统计月份
        /// </summary>
        public FS.FrameWork.Models.NeuObject GetLastStaticTime()
        {

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            //////            string strSql = @"select YEAR,MONTH,BEGIN_DATE,END_DATE from gfhz_static g
            //////	order by g.YEAR desc,g.MONTH desc 
            //////	fetch first 1 rows only ";

            string strSql = @"select * from (select YEAR,MONTH,BEGIN_DATE,END_DATE from gfhz_static g
                            order by g.YEAR desc,g.MONTH desc) where rownum = 1 ";

            if (this.ExecQuery(strSql) == -1)
                return null;

            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Memo = this.Reader[1].ToString();
                obj.User01 = this.Reader[2].ToString();
                obj.User02 = this.Reader[3].ToString();
            }
            this.Reader.Close();

            return obj;
        }

        public string GetSeq(string seq)
        {
            string strSql = "";

            //strSql = " select nextval for " + seq + " from dual ";
            strSql = " select  " + seq + ".nextval  from dual ";

            return this.ExecSqlReturnOne(strSql);

        }

        public FS.FrameWork.Models.NeuObject GetWorkHome(string PatientNO)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            string sql = @" select p.WORK_HOME,p.spell_code from com_patientinfo p 
 where p.CARD_NO = '{0}'";
            sql = string.Format(sql, PatientNO);
            this.ExecQuery(sql);
            while (this.Reader.Read())
            {
                obj.Name = this.Reader[0].ToString();
                obj.ID = this.Reader[1].ToString();
            }
            return obj;
        }

        /// <summary>
        /// 插入一条
        /// </summary>
        /// <param name="info">摆药单分类实体</param>
        /// <returns></returns>
        public int InsertPubReport(FS.SOC.Local.ZDLY.PubReport.Models.PubReport info)
        {
            string strSql = "";

            strSql = @"INSERT INTO gfhz_main   ( id, static_month,lsh, bh, zzs,
            fy1,fy2,fy3, fy4,fy5,fy6,fy7, fy8,fy9, fy10,fy11,fy12,fy13,fy14,fy15,fy16,fy17,fy18, fy19,fy20,fy51,
            jzzje,sjzje,fee_type,pay_rate,is_valid,inpatient_no,
            begin_date,end_date,MCARD_NO,SP_TOT,IN_STATE,
                FEE_FLAG,REP_FLAG,EXT_FLAG,EXT_FLAG2,oper_code,oper_date,seq,sortid,name,work_name,work_code,patientNO,zfbl,
                OVERLIMITDRUGFEE,CANCERDRUGFEE,BEDFEE_JIANHU,BEDFEE_CENGLIU,COMPANYFAY,SELFPAY,TOTALFEE,INVOICE_NO)
                VALUES(  '{0}',to_date('{1}','YYYY-MM-DD HH24:MI:SS'),'{2}', '{3}', {4}, {5},{6},{7},{8},{9},    {10},   {11},   {12},    {13},
                {14}, {15},     {16},      {17},  {18},   {19},  {20},{21},    {22},    {23},   {24},    {25},
                {26},  {27},   '{28}',    {29},    '{30}',   '{31}',  
                to_date('{32}','YYYY-MM-DD HH24:MI:SS'),
                to_date('{33}','YYYY-MM-DD HH24:MI:SS'),
               '{34}',{35},'{36}',
                '{37}','{38}','{39}','{40}','{41}', to_date('{42}','YYYY-MM-DD HH24:MI:SS'),{43},'{44}','{45}','{46}','{47}','{48}',{49},
                {50},{51},{52},{53},{54},{55},{56},'{57}')";//[2010-01-27]50-56为修改公医报表添加
            try
            {

                string[] strParm = myGetParmPubReport(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        private string[] myGetParmPubReport(FS.SOC.Local.ZDLY.PubReport.Models.PubReport info)
        {

            string[] strParm ={   info.ID,
								 info.Static_Month.ToString(),
								 info.Bill_No,   // 托收单
								 //info.Pact.Memo,  //字头
                                 info.Pact.ID,
								 info.Amount.ToString(),
								 info.Bed_Fee.ToString(),
								 info.YaoPin.ToString() , 
								 info.ChengYao.ToString() ,
								 info.HuaYan.ToString(),
								 info.JianCha.ToString(),
								 info.FangShe.ToString() , 
								 info.ZhiLiao.ToString() , 
								 info.ShouShu.ToString() , 
								 info.ShuXue.ToString() , 
								 info.ShuYang.ToString() ,
								 info.TeZhi.ToString() ,
								 info.TeZhiRate.ToString() , 
								 info.MR.ToString() , 
								 info.CT.ToString() , 
								 info.XueTou.ToString(), 
								 info.ZhenJin.ToString()  , 
								 info.CaoYao.ToString() , 
								 info.TeJian.ToString()  , 
								 info.ShenYao.ToString()  ,
								 info.JianHu.ToString() , 
								 info.ShengZhen.ToString()  , 
								 info.Tot_Cost.ToString()  ,
								 info.Pub_Cost.ToString(),
								 info.Fee_Type  , 
								 info.Pay_Rate.ToString()  , 
								 info.IsValid,
								 info.Pact.Name,  //住院流水号
								 info.Begin.ToString(),
								 info.End.ToString(),
								 info.MCardNo,
                                 info.SpDrugFeeTot.ToString(),
								 info.IsInHos,
				                 info.FEE_FLAG,
				                 info.REP_FLAG,
				                 info.EXT_FLAG,
				                 info.EXT_FLAG2,
                                 info.OperCode,
                                 info.OperDate.ToString(),
                                 info.Seq.ToString(),
                                 info.SortId,
                                 info.Name,
                                 info.WorkName,
                                 info.WorkCode,
                                 info.PatientNO,
                                 info.TeYaoRate.ToString(),            
                                 info.OverLimitDrugFee.ToString(),
                                 info.CancerDrugFee.ToString(),
                                 info.BedFee_JianHu.ToString(),
                                 info.BedFee_CengLiu.ToString(),
                                 info.CompanyPay.ToString(),
                                 info.SelfPay.ToString(),
                                 info.TotalFee.ToString(),
                                 info.InvoiceNo.ToString()

							 };

            return strParm;
        }

        public int DeletePubReport(string Inpatient_no, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "DELETE FROM gfhz_main   WHERE inpatient_no = '{0}' and begin_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')   and  invoice_no is null";

            try
            {
                strSql = string.Format(strSql, Inpatient_no, dtBegin.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss"));

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.PubReport";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据摆药单分类编码,删除一条记录
        /// </summary>
        /// <param name="ID">摆药单分类编码</param>
        /// <returns></returns>
        public int DeletePubReport(string ID)
        {
            if (IsExist(ID, "1") > 0)
            {
                this.Err = "托收单已出报表,不允许删除.";
                return -1;
            }
            string strSql = "DELETE FROM gfhz_main   WHERE id = '{0}'";

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.PubReport";
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// 获得患者的最后一次统计时间
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <returns>null or "" 代表患者没有统计过, DateTime表示最后一次统计时间</returns>
        public string GetLastStaticTime(string inpatientNo, string dtEnd)
        {
            string strSql = "select nvl(max(a.end_date),to_date('1900-01-01 00:00:00','yyyy-mm-dd hh24:mi:ss')) from gfhz_main a where a.inpatient_no = '{0}' and " +
                "static_month <= to_date('{1}', 'yyyy-mm-dd hh24:mi:ss')";

            try
            {
                strSql = string.Format(strSql, inpatientNo, dtEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            return this.ExecSqlReturnOne(strSql);
        }

        /// <summary>
        /// 查找患者在统计月份中托收单是否已经统计或出报表
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始统计时间</param>
        /// <param name="dtEnd">结束统计时间</param>
        /// <param name="rep_flag">报表标志,0未出报表,1已出报表</param>
        /// <returns>null执行错误 "0"没有纪录 >=1 已经有纪录</returns>
        public int IsExist(string inpatientNo, string dtBegin, string dtEnd, string rep_flag)
        {
            string strSql = "select count(*)  from gfhz_main a where a.inpatient_no = '{2}' " +
                "and a.static_month >= to_date('{0}','yyyy-mm-dd hh24:mi:ss') " +
                "and a.static_month <= to_date('{1}','yyyy-mm-dd hh24:mi:ss')" +
                "and a.rep_flag='{3}'";
            try
            {
                strSql = string.Format(strSql, dtBegin, dtEnd, inpatientNo, rep_flag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            string temp = this.ExecSqlReturnOne(strSql);
            if (temp == null)
            {
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(temp);
        }

        /// <summary>
        /// 查找患者在统计月份中托收单是否已经统计或出报表
        /// </summary>
        /// <param name="id">gfhz_main.id</param>		
        /// <param name="rep_flag">报表标志,0未出报表,1已出报表</param>
        /// <returns>null执行错误 "0"没有纪录 >=1 已经有纪录</returns>
        public int IsExist(string id, string rep_flag)
        {
            string strSql = "select count(*)  from gfhz_main a where a.id = '{0}' " +
                "and a.rep_flag='{1}'";
            try
            {
                strSql = string.Format(strSql, id, rep_flag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            string temp = this.ExecSqlReturnOne(strSql);
            if (temp == null)
            {
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(temp);
        }

        /// <summary>
        /// 根据合同单位取卡号
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetCardNo(FS.HISFC.Models.RADT.PatientInfo info)
        {
            if (info == null)
                return null;
            if (info.Pact.ID.Length < 2)
                return null;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.Name = info.ID;
            try
            {
                string PactHead = this.GetPactHeadByPact(info.Pact.ID);
                if (PactHead == "DS")
                {
                    return null;
                }
                else if (PactHead != "NF")
                {
                    obj.ID = PactHead;
                }
                else if (info.Pact.ID.Substring(0, 1) == "1")/*省*/
                {

                    if (info.SSN.Substring(0, 1).ToUpper() == "J")/*缴款单位*/
                    {
                        obj.ID = info.SSN.Substring(0, 3).ToUpper();
                    }
                    else/*享受单位*/
                    {
                        obj.ID = info.SSN.Substring(0, 2).ToUpper();
                    }
                }
                else if (info.Pact.ID.Substring(0, 1) == "6")/*开发区*/
                {
                    if (info.SSN.Substring(0, 2).ToUpper() == "K7")/*K70,K71,K72,K73*/
                    {
                        obj.ID = info.SSN.Substring(0, 3).ToUpper();
                    }
                    else
                    {
                        obj.ID = info.SSN.Substring(0, 2).ToUpper();/*K1,K2*/
                    }
                }
                else if (info.Pact.ID.Substring(0, 1) == "7"
                    || info.Pact.ID.ToUpper() == "L3" || info.Pact.ID.ToUpper() == "Y110"
                    || info.Pact.ID.ToUpper() == "Y210" || info.Pact.ID.ToUpper() == "Y305")/*本院*/
                {
                    obj.ID = info.SSN.ToUpper();/*本院职工用工号 大于等于 4位*/
                }
                else if (info.Pact.ID.Substring(0, 1) == "5" || info.Pact.ID.Substring(0, 1) == "4")/*特约 3位*/
                {

                    obj.ID = info.SSN.ToUpper();
                }
                else/*其他所有取两位卡号的合同单位*/
                {
                    obj.ID = info.SSN.Substring(0, 2).ToUpper();
                }

                return obj;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获得患者的本期托收信息
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            //string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
            //    "from fin_ipb_feeinfo a, com_dictionary b where a.parent_code = '[父级编码]' and  a.current_code = '[本级编码]' " +
            //    "and a.parent_code = b.parent_code and a.current_code = b.current_code and type = 'MINFEE' and a.fee_code = b.code  " +
            //    "and a.charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and a.charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss') and a.inpatient_no = '{0}' " +
            //    "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_feeinfo a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and a.charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss') and a.inpatient_no = '{0}' " +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());
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
        /// 获得患者的本期托收信息
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientFee(string inpatientNo)
        {
            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_feeinfo a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.inpatient_no = '{0}' " + "and a.balance_state = '0' " +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
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
        /// 获得审批肿瘤药费总金额
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientTPdrugFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_medicinelist a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and a.charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss') and a.inpatient_no = '{0}' and a.EXT_FLAG4='4' and a.noback_num>0" +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());
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
        /// 获得审批肿瘤药费总金额
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientTPdrugFee(string inpatientNo)
        {
            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_medicinelist a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.inpatient_no = '{0}' and a.EXT_FLAG4='4' and a.noback_num>0" +"and a.balance_state = '0' " +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
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
        /// 获得高检费总金额
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientTPundrugFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_itemlist a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.charge_date >= to_date('{1}','yyyy-mm-dd hh24:mi:ss') and a.charge_date <= to_date('{2}','yyyy-mm-dd hh24:mi:ss') and a.inpatient_no = '{0}' and a.EXT_FLAG4='4' and a.noback_num>0" +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());
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
        /// 获得高检费总金额
        /// </summary>
        /// <param name="inpatientNo">住院流水号</param>
        /// <param name="dtBegin">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public DataSet GetPatientTPundrugFee(string inpatientNo)
        {
            string strSql = "select b.sort_id, a.fee_code, sum(tot_cost),sum(own_cost),sum(pay_cost), sum(pub_cost) " +
       "from fin_ipb_itemlist a, com_dictionary b where  type = 'MINFEE' and a.fee_code = b.code  " +
       "and a.inpatient_no = '{0}' and a.EXT_FLAG4='4' and a.noback_num>0" + "and a.balance_state = '0' " +
       "group by b.sort_id,a.fee_code order by b.sort_id,a.fee_code";

            try
            {
                strSql = string.Format(strSql, inpatientNo);
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
        /// 根据合同单位代码获取字头
        /// </summary>
        /// <param name="PactID"></param>
        /// <returns></returns>
        public string GetPactHeadByPact(string PactCode)
        {
            string rtn = "";
            string strSql = "select pact_head from com_pactcompare where pact_code = '{0}'";
            try
            {
                strSql = string.Format(strSql, PactCode);
                rtn = this.ExecSqlReturnOne(strSql, "NF");
                if (rtn == "-1")
                {
                    return "NF";
                }
                else
                {
                    return rtn;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return "NF";
            }
        }

        /// <summary>
        /// 更新30%药品费用
        /// </summary>
        /// <param name="ID">序号</param>
        /// <param name="Cost">金额</param>
        /// <returns></returns>
        public int UpdatePubReportForSp(string ID, decimal Cost)
        {
            string strSql = "";
            strSql = "UPDATE gfhz_main     SET    SP_TOT = '{1}' WHERE id ='{0}'";

            try
            {

                strSql = string.Format(strSql, ID, Cost.ToString());

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获得患者的特殊比例药品费用，暂时都为30%
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT GetSpDrugFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {

            FS.HISFC.Models.Base.FT ft = null;

            string strSql =
                " select sum(tot_cost), sum(own_cost), sum(pub_cost), sum(pay_cost) " +
                " from ( SELECT tot_cost, own_cost,pub_cost,pay_cost,round((pub_cost+pay_cost)*0.30,2) as spPay  " +
                " FROM Fin_Ipb_Feeinfo        WHERE  " +
                " inpatient_no = '{0}'  " +
                " and fee_code in ('001','002','003')   and pay_cost<>0 " +
                " and charge_date>=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                " and charge_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')  " +
                "  )t     where   (spPay=pay_cost)";

            strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                ft = new FS.HISFC.Models.Base.FT();
                ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());
                ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
            }

            return ft;
        }


        /// <summary>
        /// 获得患者的特殊比例药品费用，暂时都为30%
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Base.FT GetSpDrugFee(string inpatientNo)
        {

            FS.HISFC.Models.Base.FT ft = null;

            string strSql =
                " select sum(tot_cost), sum(own_cost), sum(pub_cost), sum(pay_cost) " +
                " from ( SELECT tot_cost, own_cost,pub_cost,pay_cost,round((pub_cost+pay_cost)*0.30,2) as spPay  " +
                " FROM Fin_Ipb_Feeinfo        WHERE  " +
                " inpatient_no = '{0}'  " + "and a.balance_state = '0' " +
                " and fee_code in ('001','002','003')   and pay_cost<>0 " +
                "  )t     where   (spPay=pay_cost)";

            strSql = string.Format(strSql, inpatientNo);

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                ft = new FS.HISFC.Models.Base.FT();
                ft.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                ft.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());
                ft.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());
                ft.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3].ToString());
            }

            return ft;
        }

        /// <summary>
        /// 获得患者的超标药品费用，不含自费药（结算时ext_flag=1，但是注意修改比例的ext_flag4）
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public decimal GetOverLimitDrugFee(string inpatientNo, DateTime dtBegin, DateTime dtEnd)
        {

            string spDrugFee = "";

            string strSql =
                " select  nvl(sum(own_cost),0) " +
                " from fin_ipb_medicinelist  where  " +
                " drug_code in ('Y001','Y002','Y003')"+
                " and inpatient_no = '{0}'  " +
                " and charge_date>=to_date('{1}','yyyy-mm-dd hh24:mi:ss') " +
                " and charge_date <= to_date('{2}', 'yyyy-mm-dd hh24:mi:ss')  ";
            try
            {
                strSql = string.Format(strSql, inpatientNo, dtBegin.ToString(), dtEnd.ToString());
                spDrugFee = this.ExecSqlReturnOne(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return 0;
            }
            return FS.FrameWork.Function.NConvert.ToDecimal(spDrugFee);
        }

        /// <summary>
        /// 获得患者的超标药品费用，不含自费药（结算时ext_flag=1，但是注意修改比例的ext_flag4）
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public decimal GetOverLimitDrugFee(string inpatientNo)
        {

            string spDrugFee = "";

            string strSql =
                " select  nvl(sum(own_cost),0) " +
                " from fin_ipb_medicinelist  where  " +
                " drug_code in ('Y001','Y002','Y003')" +
                " and inpatient_no = '{0}'  ";
            try
            {
                strSql = string.Format(strSql, inpatientNo);
                spDrugFee = this.ExecSqlReturnOne(strSql);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return 0;
            }
            return FS.FrameWork.Function.NConvert.ToDecimal(spDrugFee);
        }

        /// <summary>
        /// 更新一条
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int UpdatePubReport(FS.SOC.Local.ZDLY.PubReport.Models.PubReport info)
        {
            string strSql = "";
            strSql = "UPDATE gfhz_main    SET     static_month=to_date('{1}','yyyy-mm-dd hh24:mi:ss'),          lsh='{2}',         bh='{3}',       zzs={4},    fy1={5},       fy2={6},        fy3={7},        fy4={8},         fy5={9},         fy6={10}    ,     fy7={11},       fy8={12},        fy9={13},        fy10={14},      fy11={15}," +
                "fy12={16},         fy13={17},         fy14={18},        fy15={19},        fy16={20},      fy17={21},        fy18={22},        fy19={23},         fy20={24},        fy51={25},         jzzje={26},         sjzje={27},         fee_type='{28}',       Pay_Rate={29} ,is_valid = '{30}' , MCARD_NO = '{34}'," +
               " BEGIN_DATE = to_date('{32}','yyyy-mm-dd hh24:mi:ss'), end_date = to_date('{33}','yyyy-mm-dd hh24:mi:ss'),SP_TOT = {35}, IN_STATE = '{36}', FEE_FLAG='{37}', REP_FLAG='{38}', EXT_FLAG='{39}',  EXT_FLAG2='{40}' ,sortid = '{44}',name = '{45}',work_name = '{46}',work_code = '{47}',patientno='{48}',zfbl = {49}," +
               "OVERLIMITDRUGFEE = {50},CANCERDRUGFEE = {51},BEDFEE_JIANHU = {52},BEDFEE_CENGLIU = {53},COMPANYFAY = {54},SELFPAY = {55},TOTALFEE = {56},INVOICE_NO ='{57}'  WHERE id ='{0}' and seq = {43}";

            try
            {
                string[] strParm = myGetParmPubReport(info);  //取参数列表
                strSql = string.Format(strSql, strParm);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

    }
}

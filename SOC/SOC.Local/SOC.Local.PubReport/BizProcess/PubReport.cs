using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace FS.SOC.Local.PubReport.BizProcess
{
    public class PubReport : FS.HISFC.BizProcess.Integrate.IntegrateBase
    {
        static Hashtable hsPactCompare;

        //SOC.Local.PubReport.BizLogic.OutPatientFee OutPatientFeeMgr = new SOC.Local.PubReport.BizLogic.OutPatientFee();
        //SOC.Local.PubReport.BizLogic.InpatientFee InpatientFeeMgr = new SOC.Local.PubReport.BizLogic.InpatientFee();
        FS.HISFC.BizLogic.Fee.Outpatient OutPatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
        FS.HISFC.BizLogic.Fee.InPatient InpatientFeeMgr = new FS.HISFC.BizLogic.Fee.InPatient();

        FS.HISFC.BizLogic.Fee.PactUnitInfo PactMgr = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        FS.HISFC.BizLogic.RADT.InPatient inpMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        SOC.Local.PubReport.BizLogic.PubReport pubMgr = new SOC.Local.PubReport.BizLogic.PubReport();

        public ArrayList QueryPubFeeInfoClinic(DateTime beginDate, DateTime endDate)
        {
            ArrayList alInvoices = new ArrayList();
            ArrayList alPubFeeInfo = new ArrayList();
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alClinicStat = conMgr.GetList("FPANDZZDMZ");
            Hashtable hsClinicStat = new Hashtable();
            foreach (FS.HISFC.Models.Base.Const con in alClinicStat)
            {
                if (!hsClinicStat.Contains(con.ID))
                {
                    hsClinicStat.Add(con.ID, con.Memo);
                }
            }
            alInvoices = this.OutPatientFeeMgr.QueryPubFeeInvoice(beginDate, endDate);
            if (alInvoices == null || alInvoices.Count == 0)
            {
                return null;
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alInvoices)
            {
                SOC.Local.PubReport.Models.PubReport pubReport = new SOC.Local.PubReport.Models.PubReport();
                pubReport = ConvertInvoiceToPubReport(invoice);
                if (pubReport != null)
                {
                    alPubFeeInfo.Add(pubReport);
                }
            }
            return alPubFeeInfo;
        }

        public ArrayList QueryPubFeeInfoClinic(DateTime beginDate, DateTime endDate, bool withOverLimitDrugFee)
        {
            
                ArrayList alInvoices = new ArrayList();
                ArrayList alPubFeeInfo = new ArrayList();
                FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList alClinicStat = conMgr.GetList("FPANDZZDMZ");
                Hashtable hsClinicStat = new Hashtable();
                foreach (FS.HISFC.Models.Base.Const con in alClinicStat)
                {
                    if (!hsClinicStat.Contains(con.ID))
                    {
                        hsClinicStat.Add(con.ID, con.Memo);
                    }
                }

                if (!withOverLimitDrugFee)
                {
                    alInvoices = this.OutPatientFeeMgr.QueryPubFeeInvoice(beginDate, endDate);
                }
                else
                {
                    alInvoices = this.OutPatientFeeMgr.QueryPubFeeInvoice(beginDate, endDate,true);
                }

                if (alInvoices == null || alInvoices.Count == 0)
                {
                    return null;
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.Balance invoice in alInvoices)
                {
                    SOC.Local.PubReport.Models.PubReport pubReport = new SOC.Local.PubReport.Models.PubReport();
                    pubReport = ConvertInvoiceToPubReport(invoice);
                    if (pubReport != null)
                    {
                        alPubFeeInfo.Add(pubReport);
                    }
                }
                return alPubFeeInfo;
            
        }

        static Hashtable hsInStat = null;

        /// <summary>
        /// 获取记账单的类别
        /// </summary>
        /// <param name="invoiceState">发票的类别</param>
        /// <returns></returns>
        static string GetState(string invoiceState)
        {
            if (hsInStat == null)
            {
                FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
                ArrayList alInStat = conMgr.GetList("FPANDZZD");
                hsInStat = new Hashtable();
                foreach (FS.HISFC.Models.Base.Const con in alInStat)
                {
                    if (!hsInStat.Contains(con.ID))
                    {
                        hsInStat.Add(con.ID, con.Memo);
                    }
                }
            }
            if (!hsInStat.Contains(invoiceState))
            {
                return null;
            }
            return hsInStat[invoiceState].ToString();
        }

        /// <summary>
        /// 根据结算发票信息生成公费汇总信息
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pactHead"></param>
        /// <returns></returns>
        public ArrayList QueryPubFeeInfoInpatient(DateTime beginDate, DateTime endDate)
        {
            ArrayList alInvoices = new ArrayList();
            ArrayList alPubFeeInfo = new ArrayList();

            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();


            alInvoices = this.InpatientFeeMgr.QueryPubFeeInvoiceInpateint(beginDate, endDate);
            if (alInvoices == null || alInvoices.Count == 0)
            {
                return null;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.Balance invoice in alInvoices)
            {
                SOC.Local.PubReport.Models.PubReport pubReport = ConvertInvoiceToPubReport(invoice);
                if (pubReport != null)
                {
                    alPubFeeInfo.Add(pubReport);
                }
            }
            return alPubFeeInfo;
        }

        /// <summary>
        /// 将发票实体转化为记账单实体 住院
        /// </summary>
        /// <param name="balace"></param>
        /// <returns></returns>
        public SOC.Local.PubReport.Models.PubReport ConvertInvoiceToPubReport(FS.HISFC.Models.Fee.Inpatient.Balance invoice)
        {
            FS.HISFC.Models.RADT.PatientInfo patientinfo = this.inpMgr.QueryPatientInfoByInpatientNO(invoice.Patient.ID);
            SOC.Local.PubReport.Models.PubReport pubReport = new SOC.Local.PubReport.Models.PubReport();
            pubReport.Pact.ID = invoice.Patient.Pact.ID;
            invoice.Patient.Pact = this.PactMgr.GetPactUnitInfoByPactCode(invoice.Patient.Pact.ID);
            pubReport.Pact.Name = invoice.Patient.Pact.Name;
            pubReport.Pact.Memo = GetPactHead(pubReport.Pact.ID);
            pubReport.InpatientNo = invoice.Patient.ID;  //患者id（cliinc_code/inpatient_no）
            pubReport.PatientNO = patientinfo.PID.PatientNO;
            pubReport.Pub_Cost = invoice.FT.PubCost;
            pubReport.ID = invoice.Invoice.ID;
            pubReport.Tot_Cost = invoice.FT.PubCost + invoice.FT.PayCost;
            pubReport.Pay_Rate = invoice.Patient.Pact.Rate.PayRate;
            pubReport.Begin = patientinfo.PVisit.InTime;
            pubReport.End = patientinfo.PVisit.OutTime;
            pubReport.Static_Month = DateTime.MinValue;
            pubReport.FEE_FLAG = "0";  //正常费用
            pubReport.Fee_Type = "2";  //门诊费用
            pubReport.IsValid = "1";
            pubReport.MCardNo = patientinfo.SSN.ToUpper();
            pubReport.Name = invoice.Patient.Name;
            pubReport.IsValid = "0";  //0未复核 1已复核
            pubReport.REP_FLAG = "0"; //未统计
            pubReport.OperCode = invoice.BalanceOper.ID;
            pubReport.OperDate = invoice.BalanceOper.OperTime;
            pubReport.Seq = 0;
            

            //天数
            DateTime dtB = new DateTime(pubReport.Begin.Year, pubReport.Begin.Month, pubReport.Begin.Day, 0, 0, 0);
            DateTime dtE = new DateTime(pubReport.End.Year, pubReport.End.Month, pubReport.End.Day, 0, 0, 0);
            TimeSpan ts = dtE - dtB;
            if (ts.Days == 0)
            {
                pubReport.Amount = 1;
            }
            pubReport.Amount = ts.Days;

            ArrayList alBalanceList = this.InpatientFeeMgr.QueryBalanceListsByInvoiceNO(invoice.Invoice.ID);
            if (alBalanceList == null)
            {
                return null;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList blist in alBalanceList)
            {
                #region 循环
                //string id = GetState(blist.FeeCodeStat.StatCate.ID);
                switch (blist.FeeCodeStat.StatCate.ID)
                {
                    case "01":  //西药
                        pubReport.YaoPin += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                   
                        break;
                    case "02": //成药
                        pubReport.ChengYao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "03": //草药
                        pubReport.CaoYao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "04":  //床位
                        pubReport.Bed_Fee += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "05":  //诊金
                        pubReport.ZhenJin += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "06":  //检查
                        pubReport.JianCha += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "07":  //Ct
                        pubReport.CT += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "08":  //MR
                        pubReport.MR += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "09":   //检查
                        pubReport.JianCha += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "10":   //检查
                        pubReport.JianCha += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "11":   //检查
                        pubReport.JianCha += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "12":   //监护
                        pubReport.JianHu += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "13":   //治疗
                        pubReport.ZhiLiao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "14":   //输血
                        pubReport.ShuXue += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "15":   //输氧
                        pubReport.ShuYang += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "16":   //手术
                        pubReport.ShouShu += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "17":   //化验
                        pubReport.HuaYan += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "18":   //治疗
                        pubReport.ZhiLiao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "19":   //特需服务
                        pubReport.ZhiLiao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    default:   //卫生材料其他
                        pubReport.ZhiLiao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                }
                #endregion
            }
         
            //[2010-01-27]zhaozf 修改公医报表添加
            pubReport.OverLimitDrugFee = this.InpatientFeeMgr.GetOverLimiteDurgFee(invoice.Invoice.ID);
            pubReport.SelfPay = invoice.FT.OwnCost - pubReport.OverLimitDrugFee;
            pubReport.TotalFee = invoice.FT.TotCost;
                

            return pubReport;
        }


        /// <summary>
        /// 门诊记账单
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public Local.PubReport.Models.PubReport ConvertInvoiceToPubReport(FS.HISFC.Models.Fee.Outpatient.Balance invoice)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.HISFC.Models.Registration.Register register = regMgr.GetByClinic(invoice.Patient.ID);
            SOC.Local.PubReport.Models.PubReport pubReport = new SOC.Local.PubReport.Models.PubReport();
           
            
            pubReport.Pact.ID = invoice.Patient.Pact.ID;
            FS.HISFC.Models.Base.PactInfo pact = PactMgr.GetPactUnitInfoByPactCode(pubReport.Pact.ID);
            pubReport.Pact.Name = invoice.Patient.Pact.Name;
            pubReport.Pact.Memo = GetPactHead(pubReport.Pact.ID);
            pubReport.InpatientNo = invoice.Patient.ID;  //患者id（cliinc_code/inpatient_no）
            pubReport.PatientNO = register.PID.CardNO;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj = pubMgr.GetWorkHome(pubReport.PatientNO);
            pubReport.WorkCode = obj.ID;
            pubReport.WorkName = obj.Name;
            pubReport.Pub_Cost = invoice.FT.PubCost;
            pubReport.ID = invoice.Invoice.ID;
            pubReport.Tot_Cost = invoice.FT.PubCost + invoice.FT.PayCost;
            pubReport.Pay_Rate = pact.Rate.PayRate;
            pubReport.Begin = invoice.BalanceOper.OperTime;
            pubReport.Static_Month = DateTime.MinValue;
            pubReport.End = invoice.BalanceOper.OperTime;
            pubReport.FEE_FLAG = "0";  //正常费用
            pubReport.Fee_Type = "1";  //门诊费用
            pubReport.IsValid = "0";  //0未复核 1已复核
            pubReport.MCardNo = invoice.Patient.SSN.ToUpper();
            pubReport.Name = invoice.Patient.Name;
            pubReport.REP_FLAG = "0"; //未统计
            pubReport.OperCode = invoice.BalanceOper.ID;
            pubReport.OperDate = invoice.BalanceOper.OperTime;
            pubReport.Seq = 0;
            //pubReport.SpDrugFeeSj = FS.FrameWork.Function.NConvert.ToDecimal(invoice.User02);//特批药费实际费用

            //天数
            pubReport.Amount = 1;

            ArrayList alBalanceList = this.OutPatientFeeMgr.QueryBalanceListsByInvoiceNO(invoice.Invoice.ID);
            if (alBalanceList == null)
            {
                return null;
            }
            foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList blist in alBalanceList)
            {
                #region 循环
                //string id = GetState(blist.FeeCodeStat.ID);
                switch (blist.FeeCodeStat.ID)
                {
                    case "01":  //药品
                        pubReport.YaoPin += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "02": //成药费
                        pubReport.ChengYao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "03": //草药
                        pubReport.CaoYao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "04":  //诊金(挂号费+诊查费)
                        pubReport.ZhenJin += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "05":  //检查
                        pubReport.JianCha += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "06":  //化验费 
                        pubReport.HuaYan += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "07":  //门诊床位费放到治疗中
                        pubReport.ZhiLiao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "08":   //手术
                        pubReport.ShouShu += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    case "11":
                        pubReport.ShuXue += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                    default:   //卫生材料费
                        pubReport.ZhiLiao += blist.BalanceBase.FT.PubCost + blist.BalanceBase.FT.PayCost;
                        break;
                }
                #endregion
            }


            //[2010-02-05]zhaozf 修改公医报表添加
            pubReport.OverLimitDrugFee = FS.FrameWork.Function.NConvert.ToDecimal(invoice.User01);//药费超标金额
            pubReport.SelfPay = invoice.FT.OwnCost - pubReport.OverLimitDrugFee;
            pubReport.TotalFee = invoice.FT.TotCost;


            return pubReport;
        }

        static int QueryPactCompare(ref DataSet dsPactCompare)
        {
            //SOC.Local.PubReport.BizLogic.OutPatientFee outPatientFeeMgr = new SOC.Local.PubReport.BizLogic.OutPatientFee();
            FS.HISFC.BizLogic.Fee.Outpatient outPatientFeeMgr = new FS.HISFC.BizLogic.Fee.Outpatient();
            string sql = "select pact_code,pact_head,pact_name from com_pactcompare";
            return outPatientFeeMgr.ExecQuery(sql,ref dsPactCompare);
        }

        static public string GetPactHead(string pactCode)
        {
            if (hsPactCompare == null)
            {
                DataSet dsPactCompare = new DataSet();
                hsPactCompare = new Hashtable();
                if (QueryPactCompare(ref dsPactCompare) == -1 || dsPactCompare.Tables.Count == 0)
                {
                    return "";
                }
                foreach (DataRow row in dsPactCompare.Tables[0].Rows)
                {
                    if (!hsPactCompare.Contains(row["pact_code"].ToString()))
                    {
                        hsPactCompare.Add(row["pact_code"].ToString(), row["pact_head"].ToString());
                    }
                }
            }

            return hsPactCompare[pactCode].ToString();
        }

        public int GetBillNo(string pactHead)
        {
            string sql = @" select d.input_code from com_dictionary d,com_dictionary zt where d.TYPE = 'PactType' and zt.TYPE = 'GFBBNZT' and zt.MARK = d.CODE and zt.CODE = '{0}'";
            sql = string.Format(sql, pactHead);
            string strBillNo = this.PactMgr.ExecSqlReturnOne(sql);
            int billNo = 1;
            try
            {
                billNo = FS.FrameWork.Function.NConvert.ToInt32(strBillNo);
            }
            catch
            {
                billNo = 1;
            }
            if (billNo < 1)
            {
                billNo = 1;
            }
            return billNo;
        }

        public int UpdateBillNo(string pactHead,int billNo)
        {
            string sql = @" update com_dictionary d	
 set d.INPUT_CODE = '{1}'
 where d.TYPE = 'PactType' 
 and (select count(*) from com_dictionary zt where zt.MARK = d.CODE and zt.TYPE = 'GFBBNZT' and zt.CODE  = '{0}' ) > 0
";
            sql = string.Format(sql, pactHead, billNo);
            this.PactMgr.SetTrans(this.trans);
            return this.PactMgr.ExecNoQuery(sql);
        }


    }
}

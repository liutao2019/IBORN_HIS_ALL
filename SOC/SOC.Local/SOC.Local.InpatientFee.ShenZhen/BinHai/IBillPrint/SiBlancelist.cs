using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.IBillPrint
{
    class SiBlancelist : FS.SOC.HISFC.InpatientFee.Interface.IBillPrint
    {    
        #region SiBlanceList 变量
        ucSZBlanceInvoice1 uSiBlanceList = new ucSZBlanceInvoice1(); 
         
        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();
        private FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
        private FS.HISFC.BizLogic.Fee.FeeReport ReportFee = new FS.HISFC.BizLogic.Fee.FeeReport();
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #endregion
        #region SiBlanceList 方法
        public FS.HISFC.Models.Base.PageSize GetPageSize()
        {
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("ZYJS");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("ZYJS", 790, 1098);
            }
            return pSize;
        }
        #endregion
        #region IBillPrint 成员

        public void Print()
        {
            FS.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
            //使用FS默认打印方式
            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize();//使用默认的A4纸张
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(pageSize);
            print.PrintPreview(pageSize.Left, pageSize.Top, uSiBlanceList);
        }

        public int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.SOC.HISFC.InpatientFee.Interface.EnumPrintType printType, object t, ref string errInfo, params object[] appendParams)
        {
            throw new NotImplementedException();
        }

        public int SetData(FS.HISFC.Models.RADT.PatientInfo patientInfo, object t, ref string errInfo, params object[] appendParams)
        {
           ArrayList alBalance =new ArrayList();
           string errorInfo = string.Empty;
           if (patientInfo.ID == "")
           {
               errInfo = "请选择要打印的患者！";
               return -1;
           }
           if (patientInfo.Pact.PayKind.ID.Equals("01"))//非医保患者
           {
               errInfo = "非医保患者！";
               return -1;
           }
           uSiBlanceList.SetValueForPreview(patientInfo, new FS.HISFC.Models.Fee.Inpatient.Balance(), alBalance, new ArrayList());
           return 1;
        }

        //将最小费用汇总按财务大类汇总合计
        private ArrayList GetBalanceBill(object t, string strPatientId, params object[] appendParams)
        {
            ArrayList al = new ArrayList();
            if (appendParams.Length == 1)
            {
                al = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(strPatientId, appendParams[0].ToString());
            }
            else
            {
                al = (t as FS.HISFC.BizLogic.Fee.InPatient).QueryFeeInfosGroupByMinFeeByInpatientNO(strPatientId, (DateTime)appendParams[1], (DateTime)appendParams[2], appendParams[0].ToString());
            }
            ArrayList feeinfoSort = new ArrayList();
            Hashtable fee_stat_name;
            Hashtable fee_code = new Hashtable();
            int i = 0;
            fee_stat_name = this.GetInvoiceTypeName();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItem in al)
            {
                i++;
                feeItem.Invoice.Type.Name = fee_stat_name[feeItem.Item.MinFee.ID].ToString();
                if (fee_code.ContainsValue(feeItem.Invoice.Type.Name))
                {
                    continue;
                }
                else
                {
                    foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeItemtmp in al)
                    {
                        feeItemtmp.Invoice.Type.Name = fee_stat_name[feeItemtmp.Item.MinFee.ID].ToString();
                        if (feeItemtmp.Item.MinFee.ID != feeItem.Item.MinFee.ID && feeItemtmp.Invoice.Type.Name == feeItem.Invoice.Type.Name)
                        {
                            feeItem.FT.TotCost = feeItem.FT.TotCost + feeItemtmp.FT.TotCost;
                        }
                    }
                    feeinfoSort.Add(feeItem);
                    fee_code.Add(i, feeItem.Invoice.Type.Name);
                }

            }

            return feeinfoSort;

        }
        private Hashtable GetInvoiceTypeName()
        {
            Hashtable feecodestat = new Hashtable();
            string sql = "select  t.fee_code ,t.fee_stat_name from fin_com_feecodestat t where t.report_code='ZY01' order by t.print_order";
            DataSet dsItem = new DataSet();
            dbMgr.ExecQuery(sql, ref dsItem);
            if (dsItem.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsItem.Tables[0].Rows.Count; i++)
                {
                    try
                    {
                        feecodestat.Add(dsItem.Tables[0].Rows[i][0].ToString(), dsItem.Tables[0].Rows[i][1].ToString());
                    }
                    catch
                    {

                    }
                }
            }
            return feecodestat;
        }
        #endregion
    }
}

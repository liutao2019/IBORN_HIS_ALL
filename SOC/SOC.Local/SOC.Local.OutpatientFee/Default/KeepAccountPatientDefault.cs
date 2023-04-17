using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.BizProcess.Interface.Fee;
using System.Collections;
using FS.HISFC.Models.Fee.Outpatient;

namespace FS.SOC.Local.OutpatientFee.Default
{
    public class KeepAccountPatientDefault : IKeepAccountPatient
    {
        FS.HISFC.BizLogic.Fee.Outpatient outPatientManage = new FS.HISFC.BizLogic.Fee.Outpatient();
        #region IKeepAccountPatient 成员

        public bool DealwithKeepAccount(ref FS.HISFC.Models.Registration.Register PatientInfo, ref ArrayList balancePays, ref ArrayList invoices, ref ArrayList invoiceDetails, ref ArrayList invoiceFeeDetails, ref ArrayList comFeeItemLists, out string errText)
        {
            errText = "";
            // 检测是否包含其他支付方式
            int iOtherPay = 0;
            foreach (BalancePay payMode in balancePays)
            {
                if (payMode.PayType.ID == "RC" || payMode.PayType.ID == "JZ")
                {
                    continue;
                }

                if (payMode.FT.TotCost == 0)
                {
                    continue;
                }

                iOtherPay++;
            }

            if (iOtherPay > 0)
            {
                return true;
            }

            // 不需要患者掏钱，使用临时发票号
            string invoiceNO = string.Empty;
            string realInvoiceNO = string.Empty;

            string strInvoiceOld = "";
            bool blnFirst = true;

            FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            foreach (Balance balance in invoices)
            {
                invoiceNO = string.Empty;
                realInvoiceNO = string.Empty;
                errText = string.Empty;
                int iRes = feeIntegrate.GetInvoiceNO(employee, "C", true, ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iRes <= 0)
                {
                    return false;
                }

                // 记录原来发票号
                strInvoiceOld = balance.Invoice.ID;

                // 处理发票
                balance.Invoice.ID = invoiceNO;
                balance.PrintedInvoiceNO = realInvoiceNO;

                // 处理发票明细
                foreach (ArrayList arlInvDetial in invoiceDetails)
                {
                    foreach (ArrayList arlInvDetial2 in arlInvDetial)
                    {
                        foreach (BalanceList balanceList in arlInvDetial2)
                        {
                            if (balanceList.BalanceBase.Invoice.ID == strInvoiceOld)
                            {
                                balanceList.BalanceBase.Invoice.ID = invoiceNO;
                            }
                        }
                    }
                }


                // 处理支付方式、及费用明细
                if (blnFirst)
                {
                    foreach (BalancePay payMode in balancePays)
                    {
                        payMode.Invoice.ID = invoiceNO;
                    }

                    foreach (ArrayList arlTemp in invoiceFeeDetails)
                    {
                        foreach (ArrayList arlTemp2 in arlTemp)
                        {
                            if (arlTemp2 == null)
                            {
                                continue;
                            }
                            foreach (FeeItemList f in arlTemp2)
                            {
                                f.Invoice.ID = invoiceNO;
                            }
                        }
                    }

                    foreach (FeeItemList fee in comFeeItemLists)
                    {
                        fee.Invoice.ID = invoiceNO;
                    }

                    blnFirst = false;
                }
            }

            return true;
        }
        /// <summary>
        /// 判断是否记帐患者
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public bool IsKeepAccountPatient(FS.HISFC.Models.Registration.Register PatientInfo)
        {
            bool blnRes = false;

            int iRes = outPatientManage.IsKeepAccountPatient(PatientInfo.PID.CardNO);
            if (iRes > 0)
            {
                blnRes = true;
            }

            return blnRes;
        }

        #endregion

        /// <summary>
        /// 综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
    }
}

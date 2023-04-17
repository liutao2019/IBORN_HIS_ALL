using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.Fee.BizProcess
{
    public class InpatientPrepay
    {
        /// <summary>
        /// 注销预交金方法
        /// </summary>
        /// <param name="prepay"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public virtual int CancelPrepay(Neusoft.HISFC.Models.Fee.Inpatient.Prepay prepay,ref string errInfo)
        {
            Neusoft.HISFC.BizLogic.Fee.InPatient feeInpatient = new Neusoft.HISFC.BizLogic.Fee.InPatient();

            prepay = feeInpatient.QueryPrePay(this.patientInfo.ID, prepay.ID);
            if (prepay == null)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("查询预交金信息失败！" + feeInpatient.Err, 111);
                return;
            }

            if (!ValidReturnPrepay(prepay)) return;


            DialogResult r = Neusoft.FrameWork.WinForms.Classes.Function.Msg("是否返还发票号为" + prepay.RecipeNO + "的预交金?", 422);
            if (r == DialogResult.No) return;
            //判断封帐
            if ((this.feeInpatient.GetStopAccount(this.patientInfo.ID)) == "1")
            {
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("该患者处于封帐状态,可能正在结算,请稍后再做此操作!", 111);
                return;
            }
            //事务连接
            //Transaction t = new Transaction(this.feeInpatient.Connection);
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeInpatient.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            this.feeIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            //原有发票号码
            prepay.OrgInvoice.ID = prepay.RecipeNO;
            //判断负记录走新发票号码


            if (this.IsReturnNewInvoice)
            {
                //提取发票号码
                //发票类型-预交金


                string InvoiceNo = "";
                //InvoiceNo = this.feeIntegrate.GetNewInvoiceNO(Neusoft.HISFC.Models.Fee.EnumInvoiceType.P);
                InvoiceNo = this.feeIntegrate.GetNewInvoiceNO("P");
                if (InvoiceNo == null || InvoiceNo == "")
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    Neusoft.FrameWork.WinForms.Classes.Function.Msg("提取发票出错!", 211);
                    return;
                }
                prepay.RecipeNO = InvoiceNo;
            }
            //{EC04199C-9F39-48f1-BCFE-98430D9962E6}
            prepay.IsPrint = IsPrintReturn;
            HISFC.Components.InpatientFee.Controls.ucTransType trasType = new Neusoft.HISFC.Components.InpatientFee.Controls.ucTransType(prepay);
            if (trasType.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            //调用业务层组合业务返还预交金
            if (this.feeInpatient.PrepayManagerReturn(this.patientInfo, prepay) == -1)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg(this.feeInpatient.Err + "返还失败!", 211);
                return;
            }
            //刷新余额标记
            this.txtFreeCost.Text = (Neusoft.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtFreeCost.Text), 2) + prepay.FT.PrepayCost).ToString();
            this.txtSumPreCost.Text = (Neusoft.FrameWork.Public.String.FormatNumber(decimal.Parse(this.txtSumPreCost.Text), 2) + prepay.FT.PrepayCost).ToString();
            Neusoft.FrameWork.Management.PublicTrans.Commit();

            return 1;
        }
    }
}

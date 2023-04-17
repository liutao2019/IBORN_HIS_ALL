using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.FrameWork.WinForms.Forms;

namespace GJLocal.HISFC.Components.OpGuide.IOutpatientGuide
{
    public class OutpatientGuide : FS.HISFC.BizProcess.Interface.Fee.IOutpatientGuide
    {
        ucFeeDetailGuide ucFeeDetail = null;

        ucFeeDetail2 ucFeePrint = null;

        /// <summary>
        /// 打印控制
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();

        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        public FS.HISFC.Models.Base.PageSize GetPageSize()
        {
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("MZGuide");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", 260, 260);
            }
            pSize.Height = ucFeeDetail.GetHeigth();
            return pSize;
        }

        #region IOutpatientGuide 成员

        public void Print()
        {
            ucFeeDetail = new ucFeeDetailGuide();
            if (ucFeeDetail != null)
            {
                FS.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
                //使用FS默认打印方式
                if (pageSize == null)
                {
                    pageSize = new FS.HISFC.Models.Base.PageSize();//使用默认的A4纸张
                }
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.SetPageSize(pageSize);

                string printer = this.controlIntegrate.GetControlParam<string>("MZGuide", true, "");
                if (!string.IsNullOrEmpty(printer))
                {
                    print.PrintDocument.PrinterSettings.PrinterName = printer;
                }

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                print.PrintPage(pageSize.Left, pageSize.Top, ucFeeDetail);
            }
        }

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, System.Collections.ArrayList invoices, System.Collections.ArrayList feeDetails)
        {
            ucFeeDetail = null;
            if (rInfo.PrintInvoiceCnt == 2)//补打时不提示
            {
                //ucFeeDetailGuide = new ucFeeDetailGuide();
                //string errorInfo = string.Empty;
                //ucFeeDetail.SetValue(rInfo, invoices, feeDetails);

                ucFeePrint = new ucFeeDetail2();
                ucFeePrint.SetValue(rInfo, invoices, feeDetails);
                ucFeePrint.PrintPage();
            }
            else if (rInfo.PrintInvoiceCnt == 3)//记账单查看
            {
                //收费时，如果是公费患者，则进行公费记账单的提示
                if (rInfo.Pact.PayKind.ID == "03")
                {
                    ucPubCostBill ucPubCostBill = new ucPubCostBill();
                    ucPubCostBill.SetValue(rInfo, invoices, feeDetails);


                    frmPopPubCostBill form = new frmPopPubCostBill(ucPubCostBill);
                    form.WindowState = FormWindowState.Normal;
                    form.Text = "公费记账单";

                    form.ShowDialog();
                }
            }
            else//收费时全部都提示
            {
                if (CommonController.Instance.MessageBox("是否打印费用清单？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //ucFeeDetail = new ucFeeDetailGuide();
                    //string errorInfo = string.Empty;
                    //ucFeeDetail.SetValue(rInfo, invoices, feeDetails);


                    ucFeePrint = new ucFeeDetail2();
                    ucFeePrint.SetValue(rInfo, invoices, feeDetails);
                    ucFeePrint.PrintPage();
                }

                //收费时，如果是公费患者，则进行公费记账单的提示
                if (rInfo.Pact.PayKind.ID == "03" && rInfo.SIMainInfo.PubCost + rInfo.SIMainInfo.PayCost > 0)
                {
                    ucPubCostBill ucPubCostBill = new ucPubCostBill();
                    ucPubCostBill.SetValue(rInfo, invoices, feeDetails);

                    frmPopPubCostBill form = new frmPopPubCostBill(ucPubCostBill);
                    form.WindowState = FormWindowState.Normal;
                    form.Text = "公费记账单";

                    form.ShowDialog();
                }
            }
        }

        #endregion
    }
}

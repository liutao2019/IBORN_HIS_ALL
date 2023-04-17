using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientGuide
{
    public class OutpatientGuide : FS.HISFC.BizProcess.Interface.Fee.IOutpatientGuide
    {
        ucFeeDetailGuide ucFeeDetail = new ucFeeDetailGuide();
        FS.HISFC.BizLogic.Manager.PageSize pageSizeManager = new FS.HISFC.BizLogic.Manager.PageSize();

        public  FS.HISFC.Models.Base.PageSize GetPageSize()
        {
            FS.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("MZGuide");
            if (pSize == null)
            {
                pSize = new FS.HISFC.Models.Base.PageSize("MZGuide", 250, 350);
            }
            pSize.Height = ucFeeDetail.GetHeigth();
            return pSize;
        }

        #region IOutpatientGuide 成员

        public void Print()
        {
            if (ucFeeDetail != null)
            {
                //FS.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
                ////使用FS默认打印方式
                //if (pageSize == null)
                //{
                //    pageSize = new FS.HISFC.Models.Base.PageSize();//使用默认的A4纸张
                //}
                //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                //print.SetPageSize(pageSize);
                //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.IsDataAutoExtend = true;
                //print.PrintPage(pageSize.Left, pageSize.Top, ucFeeDetail);
                ucFeeDetail.Print();
            }
        }

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, System.Collections.ArrayList invoices, System.Collections.ArrayList feeDetails)
        {
            ucFeeDetail = null;
            if (rInfo.PrintInvoiceCnt != 3)//补打时不提示
            {
                ucFeeDetail = new ucFeeDetailGuide();
                string errorInfo = string.Empty;
                ArrayList itemList = Function.ConvertItemToPackage(feeDetails, ref errorInfo);
                if (itemList == null)
                {
                    MessageBox.Show(errorInfo);
                    return;
                }
                ucFeeDetail.SetValue(rInfo, invoices, itemList);

                if (rInfo.PrintInvoiceCnt != 2)
                {
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
            else//记账单查看
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
        }

        #endregion
    }
}

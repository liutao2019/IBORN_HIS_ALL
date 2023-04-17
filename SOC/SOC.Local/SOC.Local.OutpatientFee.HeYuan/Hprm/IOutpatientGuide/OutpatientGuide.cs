using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.HeYuan.Hprm.IOutpatientGuide
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


            }
        }

        #endregion
    }
}

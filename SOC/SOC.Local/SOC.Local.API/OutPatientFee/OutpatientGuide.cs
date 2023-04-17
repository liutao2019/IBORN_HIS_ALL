using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Neusoft.SOC.Local.API.OutPatientFee
{
    public class OutpatientGuide : Neusoft.HISFC.BizProcess.Interface.Fee.IOutpatientGuide
    {
        ucFeeDetailGuide ucFeeDetail = new ucFeeDetailGuide();
        Neusoft.HISFC.BizLogic.Manager.PageSize pageSizeManager = new Neusoft.HISFC.BizLogic.Manager.PageSize();

        public  Neusoft.HISFC.Models.Base.PageSize GetPageSize()
        {
            Neusoft.HISFC.Models.Base.PageSize pSize = pageSizeManager.GetPageSize("MZGuide");
            if (pSize == null)
            {
                pSize = new Neusoft.HISFC.Models.Base.PageSize("MZGuide", 350, 350);
            }
            pSize.Height = ucFeeDetail.GetHeigth();
            return pSize;
        }

      

        #region IOutpatientGuide 成员

        public void Print()
        {
            Neusoft.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
            //使用Neusoft默认打印方式
            if (pageSize == null)
            {
                pageSize = new Neusoft.HISFC.Models.Base.PageSize();//使用默认的A4纸张
            }
            Neusoft.FrameWork.WinForms.Classes.Print print = new Neusoft.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(pageSize);
            print.ControlBorder = Neusoft.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(pageSize.Left, pageSize.Top, ucFeeDetail);
        }

        public void SetValue(Neusoft.HISFC.Models.Registration.Register rInfo, System.Collections.ArrayList invoices, System.Collections.ArrayList feeDetails)
        {
            ucFeeDetail = new ucFeeDetailGuide();
            string errorInfo=string.Empty;
            //ArrayList itemList = Common.Function.ConvertItemToPackage(feeDetails, ref errorInfo);
            //if (itemList == null)
            //{
            //    MessageBox.Show(errorInfo);
            //    return;
            //}

            ucFeeDetail.SetValue(rInfo, invoices, feeDetails);
        }

        #endregion
    }
}

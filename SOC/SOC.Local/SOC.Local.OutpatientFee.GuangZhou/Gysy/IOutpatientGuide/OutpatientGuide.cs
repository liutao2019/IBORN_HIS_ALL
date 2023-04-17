﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gysy.IOutpatientGuide
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
            FS.HISFC.Models.Base.PageSize pageSize = this.GetPageSize();
            //使用FS默认打印方式
            if (pageSize == null)
            {
                pageSize = new FS.HISFC.Models.Base.PageSize();//使用默认的A4纸张
            }
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.SetPageSize(pageSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(pageSize.Left, pageSize.Top, ucFeeDetail);
        }

        public void SetValue(FS.HISFC.Models.Registration.Register rInfo, System.Collections.ArrayList invoices, System.Collections.ArrayList feeDetails)
        {
            ucFeeDetail = new ucFeeDetailGuide();
            string errorInfo=string.Empty;
            ArrayList itemList = Function.ConvertItemToPackage(feeDetails, ref errorInfo);
            if (itemList == null)
            {
                MessageBox.Show(errorInfo);
                return;
            }

            ucFeeDetail.SetValue(rInfo, invoices, itemList);
        }

        #endregion
    }
}

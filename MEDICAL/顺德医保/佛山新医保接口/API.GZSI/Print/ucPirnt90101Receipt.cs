using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.Print
{
    public partial class ucPirnt90101Receipt : UserControl
    {

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public ucPirnt90101Receipt()
        {
            InitializeComponent();
        }

        public int SetValue(string psn_name,
                             string certno,
                             string tel,
                             string fixmedins_name,
                             string geso_val,
                             string fetts,
                             string matn_type,
                             string plan_matn_date,
                             string begndate,
                             string enddate,
                             string agnter_name,
                             string agnter_certno,
                             string agnter_tel,
                             string agnter_addr,
                             string dcladate)
        {
            try
            {
                this.fpPrint_Sheet1.Cells["psn_name"].Text = psn_name;
                this.fpPrint_Sheet1.Cells["certno"].Text = certno;
                this.fpPrint_Sheet1.Cells["tel"].Text = tel;
                this.fpPrint_Sheet1.Cells["fixmedins_name"].Text = fixmedins_name;
                this.fpPrint_Sheet1.Cells["geso_val"].Text = geso_val;
                this.fpPrint_Sheet1.Cells["fetts"].Text = fetts;
                this.fpPrint_Sheet1.Cells["matn_type"].Text = constMgr.GetConstant(Common.Constants.GZSI_CODE_PREFIX + "matn_type", matn_type).Name;
                this.fpPrint_Sheet1.Cells["plan_matn_date"].Text = plan_matn_date;
                this.fpPrint_Sheet1.Cells["begndate"].Text = begndate;
                this.fpPrint_Sheet1.Cells["enddate"].Text = enddate;
                //this.fpPrint_Sheet1.Cells["addr"].Text = addr;

                this.fpPrint_Sheet1.Cells["agnter_name"].Text = agnter_name;
                this.fpPrint_Sheet1.Cells["agnter_certno"].Text = agnter_certno;
                this.fpPrint_Sheet1.Cells["agnter_tel"].Text = agnter_tel;
                this.fpPrint_Sheet1.Cells["agnter_addr"].Text = agnter_addr;

                this.fpPrint_Sheet1.Cells["oper"].Text = "经办人：" + FS.FrameWork.Management.Connection.Operator.Name;
                this.fpPrint_Sheet1.Cells["operDate"].Text = "经办日期：" + dcladate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("赋值失败！");
                return -1;
            }

            return 1;

        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("A5", 580, 870);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.SetPageSize(pSize);

            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName)) return;
            print.PrintDocument.PrinterSettings.PrinterName = printerName;

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPreview(0, 10, this);
            }
            else
            {
                print.PrintPage(0, 10, this);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using API.GZSI.Business;
using API.GZSI.Models;

namespace API.GZSI.Print
{
    public partial class ucPrint90101 : UserControl//FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPrint90101()
        {
            InitializeComponent();
        }

        public void SetValue(string trt_dcla_detl_sn, string name, string psn_no, string idCard, string planMatnDate, string begnDate)
        {
            this.fpPrint_Sheet1.Cells[1, 6].Text = trt_dcla_detl_sn;
            this.fpPrint_Sheet1.Cells[2, 2].Text = name;
            this.fpPrint_Sheet1.Cells[2, 4].Text = psn_no;
            this.fpPrint_Sheet1.Cells[2, 6].Text = idCard;
            this.fpPrint_Sheet1.Cells[3, 2].Text = planMatnDate;

            try
            {
                string[] date = begnDate.Split('-');
                if (date.Count() > 0)
                {
                    this.fpPrint_Sheet1.Cells[4, 1].Text = string.Format(this.fpPrint_Sheet1.Cells[4, 1].Text, date[0], date[1].PadLeft(2,'0'), date[2].PadLeft(2,'0'));
                }
                else
                {
                    this.fpPrint_Sheet1.Cells[4, 1].Text = string.Format(this.fpPrint_Sheet1.Cells[4, 1].Text, "____", "__", "__");
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
            //FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("A4"); ;// 1100,850
            FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize("90101", 850, 1102); //850,1100
            
            print.SetPageSize(pSize);
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

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

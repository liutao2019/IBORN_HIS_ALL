using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.BizLogic.RADT;

namespace FS.HISFC.Components.Speciment.Print
{
    public partial class ucBoxLbl : UserControl
    {          
        public ucBoxLbl()
        {
            InitializeComponent();
        }

        private void SetValue()
        { 
        }

        public void Print()
        {
            try
            { 
                this.SetValue();
                //string printerIP = controlMgr.QueryControlInfoByCode("InPaIP").ControlerValue;
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                //p.PrintDocument.PrinterSettings.PrinterName = printerIP;
                p.IsHaveGrid = true;
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
                page = new FS.HISFC.Models.Base.PageSize("AAA", 196, 118);
                FS.HISFC.Components.Common.Classes.Function.GetPageSize(page, ref p);
                p.PrintPage(0, 0, this.panel1);
                //MessageBox.Show("该病人的入院科室是：" + this.patientInfomation.PVisit.PatientLocation.Dept.Name + "\n 需要打印条码,请注意条码!");
            }
            catch
            { }

        }
    }
}

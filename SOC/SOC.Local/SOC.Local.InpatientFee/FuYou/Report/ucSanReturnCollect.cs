using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.InpatientFee.FuYou.Report
{
    public partial class ucSanReturnCollect : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucSanReturnCollect()
        {
            InitializeComponent();
        }

        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        protected override int OnRetrieve(params object[] objects)
        {
            return base.OnRetrieve(this.dtpBeginTime.Value.ToString("yyyy-MM-dd"), ((FS.HISFC.Models.Base.Employee)dbMgr.Operator).Dept.ID);
        }
    }
}

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
    public partial class ucNursingScaleStat : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucNursingScaleStat()
        {
            InitializeComponent();
        }

        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        protected override int OnRetrieve(params object[] objects)
        {
            this.dwMain.Retrieve(((FS.HISFC.Models.Base.Employee)dbMgr.Operator).Nurse.ID);

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return base.OnPrint(sender, neuObject);
        }
    }
}

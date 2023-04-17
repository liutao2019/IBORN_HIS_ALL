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
    public partial class ucInpatientShowInfo : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucInpatientShowInfo()
        {
            InitializeComponent();
        }

        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        protected override int OnRetrieve(params object[] objects)
        {
            this.dwMain.Retrieve(((FS.HISFC.Models.Base.Employee)dbMgr.Operator).Nurse.ID);

            if (this.dwMain.RowCount < 1)
            {
                return 1;
            }

            int iNum1 = 0, iNum2 = 0;
            for (int i = 1; i <= this.dwMain.RowCount; i++)
            {
                if (this.dwMain.GetItemString(i, "是否有小孩") == "有")
                {
                    iNum1++;
                }
                else
                {
                    iNum2++;
                }
            }
            this.dwMain.Modify("t_num1.Text = '" + iNum1.ToString() + "'");
            this.dwMain.Modify("t_num2.Text = '" + iNum2.ToString() + "'");
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            return base.OnPrint(sender, neuObject);
        }
    }
}

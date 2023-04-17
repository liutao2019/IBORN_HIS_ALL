using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Order
{
    public partial class usFinOpbStatDeptJjhs : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public usFinOpbStatDeptJjhs()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 住院收费业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();


        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
        }
    }
}

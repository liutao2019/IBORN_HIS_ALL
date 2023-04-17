using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpr
{
    public partial class ucFinIprFeeDetail : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIprFeeDetail()
        {
            InitializeComponent();
        }
        //private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();
        
        protected override int OnRetrieve(params object[] objects)
        {
            emp = (FS.HISFC.Models.Base.Employee)dept.Operator;
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            

            return base.OnRetrieve(base.beginTime, base.endTime,emp.Dept.ID);
        }
    }
}

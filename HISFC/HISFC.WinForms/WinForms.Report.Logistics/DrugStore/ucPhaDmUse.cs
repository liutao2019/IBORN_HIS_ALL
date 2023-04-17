using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Logistics.DrugStore
{
    public partial class ucPhaDmUse : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucPhaDmUse()
        {
            InitializeComponent();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
                return -1;
            FS.HISFC.BizLogic.Manager.Department dept=new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Employee employee=dept.Operator as FS.HISFC.Models.Base.Employee;
            string deptCode=employee.Dept.ID;
            DateTime now = Convert.ToDateTime(dept.GetSysDateTime());
            string operName=employee.Name;

            return base.OnRetrieve(base.beginTime, base.endTime,deptCode, now,operName);
        }
    }
}

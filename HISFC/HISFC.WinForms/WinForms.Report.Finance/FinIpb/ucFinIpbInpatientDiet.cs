using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbInpatientDiet : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        string nurseCode = string.Empty;
        string nurseName = string.Empty;
        DateTime currentTime = new DateTime();
        string currentOper = string.Empty;
        public ucFinIpbInpatientDiet()
        {
            InitializeComponent();
        }
        protected override void OnLoad()
        {
            this.isAcross = true;
            this.isSort = false;
            this.Init();
            base.OnLoad();

            FS.HISFC.BizLogic.Manager.Department dept=new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Employee employee = dept.Operator as FS.HISFC.Models.Base.Employee;
            currentOper = employee.Name;
            nurseCode = employee.Nurse.ID;
            nurseName = employee.Nurse.Name;
            currentTime = Convert.ToDateTime(dept.GetSysDateTime());
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
                return -1;
            return base.OnRetrieve(nurseName,currentTime, currentOper, nurseCode);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Finance.FinIpb
{
    public partial class ucFinIpbDrugPercent : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbDrugPercent()
        {
            InitializeComponent();
        }
        private FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        
        private FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();
            //FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();

        DeptZone deptZone1 = DeptZone.DEPT;

        public DeptZone DeptZone1
        {
          get
          {
              return deptZone1;
          }
          set
          {
            deptZone1 = value;
          }
        }

        #region

        public enum DeptZone
        {
            //¿ÆÊÒ
            DEPT= 0,
         
            //È«Ôº
            ALL = 1,
        }
        #endregion 

        protected override int OnRetrieve(params object[] objects)
        {
            //emp = (FS.HISFC.Models.Base.Employee)dept.Operator;
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }
            
            
            //return base.OnRetrieve(base.beginTime, base.endTime,this.emp.Dept.ID);
            return base.OnRetrieve(base.beginTime, base.endTime,deptZone1.ToString());
        }
    }
}

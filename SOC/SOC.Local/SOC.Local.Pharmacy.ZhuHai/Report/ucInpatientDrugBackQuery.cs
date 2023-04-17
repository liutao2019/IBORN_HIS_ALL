using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report
{
    public partial class ucInpatientDrugBackQuery : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        public ucInpatientDrugBackQuery()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.DrugStore.InPatientBackDrugTot";
            this.PriveClassTwos = "0320";
            this.MainTitle = "病区退药确认汇总查询";
            this.RightAdditionTitle = "";
            //this.ShowTypeName = myDeptType.其他;
            this.IsUseCustomType = true;
            this.ncmbDrug.Enabled = false;
            this.ntxtBillNO.Enabled = false;
            this.ncmbDrugType.Enabled = false;
            this.cmbCustomType.Enabled = false;
            this.SumColIndexs = "5";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ncmbDept.Items.Clear();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList allDept = new ArrayList();
            ArrayList addDeptN = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.N);
            allDept.AddRange(addDeptN);
            ArrayList addDeptT = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.T);
            allDept.AddRange(addDeptT);
            ArrayList addDeptOP = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            allDept.AddRange(addDeptOP);
            this.ncmbDept.AddItems(allDept);
        }
    }
}

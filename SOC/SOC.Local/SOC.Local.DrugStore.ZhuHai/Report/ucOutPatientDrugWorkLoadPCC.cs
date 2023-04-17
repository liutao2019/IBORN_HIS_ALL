using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.DrugStore.ZhuHai.Report
{
    public partial class ucOutPatientDrugWorkLoadPCC : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public ucOutPatientDrugWorkLoadPCC()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.OutPatientDrugStore.WorkLoadPCC";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "门诊中药房工作量统计";
            //this.DeptType = "P";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "2,3,4,5,6,7,8,9,10,11";
            this.RightAdditionTitle = "";
        }
    }
}

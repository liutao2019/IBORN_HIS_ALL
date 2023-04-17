using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.FoodAndDrugAdministration
{
    public partial class OutpatientAndEmergencyPrescription:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public OutpatientAndEmergencyPrescription()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.FoodAndDrugAdministration.OutpatientAndEmergencyPrescription";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "门急诊处方";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.IsDeptAsCondition = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
        }
    }
}

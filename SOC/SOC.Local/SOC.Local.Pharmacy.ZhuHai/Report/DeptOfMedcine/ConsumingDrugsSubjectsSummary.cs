using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.DeptOfMedcine
{
    public partial class ConsumingDrugsSubjectsSummary:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        public ConsumingDrugsSubjectsSummary()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.DeptOfMedcine.ConsumingDrugsSubjectsSummary";
            //this.PriveClassTwos = "0320";
            this.MainTitle = "各科领用药品汇总";
            //this.DeptType = "P";
            this.PriveClassTwos = "0310";
            this.IsDeptAsCondition = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            this.SumColIndexs = "2";
            this.SortColIndexs = "0,1,2";
        }
    }
}

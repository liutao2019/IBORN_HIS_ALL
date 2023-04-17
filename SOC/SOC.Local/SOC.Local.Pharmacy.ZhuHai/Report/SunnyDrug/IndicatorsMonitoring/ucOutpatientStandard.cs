using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.IndicatorsMonitoring
{
    public partial class ucOutpatientStandard : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 门诊监控
        /// </summary>
        public ucOutpatientStandard()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.OutpatientStandard";
            this.MainTitle = "门诊监控";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.IsUseCustomType = false;
            this.CustomTypeShowType = "异动率：";
            this.CustomTypeSQL = @"select '120' id,'120' name,'120' memo,'120' spell_code,'120','120' input_code from dual";
            this.cmbCustomType.Text = "120";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;

            this.label2.Visible = false;
            this.dtEnd.Visible = false;

            this.dtStart.Format = DateTimePickerFormat.Custom;
            this.dtStart.CustomFormat = "yyyy-MM";
            this.label2.Location = new Point(4, this.label2.Location.Y);
        }
    }
}

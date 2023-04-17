﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.SingleSpeciesMonitoring
{
    public partial class ucDrugTransactionReports : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 药品异动报表
        /// </summary>
        public ucDrugTransactionReports()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.DrugTransactionReports";
            this.MainTitle = "药品异动报表";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.IsUseCustomType = true;
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

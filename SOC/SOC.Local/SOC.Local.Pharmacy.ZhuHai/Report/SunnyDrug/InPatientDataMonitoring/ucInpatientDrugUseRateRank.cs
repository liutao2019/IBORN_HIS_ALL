using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.InPatientDataMonitoring
{
    public partial class ucInpatientDrugUseRateRank:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 住院科室用药使用率排名
        /// </summary>
        public ucInpatientDrugUseRateRank()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.ucInpatientDrugUseRateRank2";
            this.MainTitle = "住院科室用药使用率排名";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "3,4";
            this.SortColIndexs = "0,1,2,3,4";
            this.IsUseCustomType = true;
            this.IsDeptAsCondition = true;
            this.IsAllowAllDept = true;
            this.CustomTypeShowType = "名次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            ArrayList allDrugType = new ArrayList();
            FS.FrameWork.Models.NeuObject baseDrugObj = new FS.FrameWork.Models.NeuObject();
            baseDrugObj.ID = "1";
            baseDrugObj.Name = "基本药物";
            allDrugType.Insert(0, baseDrugObj);

            FS.FrameWork.Models.NeuObject antibacterials = new FS.FrameWork.Models.NeuObject();
            antibacterials.ID = "2";
            antibacterials.Name = "抗菌药物";
            allDrugType.Insert(1, antibacterials);
            this.cmbDrugType.AddItems(allDrugType);
            this.cmbDrugType.SelectedIndex = 0;
            this.QueryDataWhenInit = false;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.cmbDrugType.Text == "基本药物")
            {
                this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.ucInpatientDrugUseRateRank2";
            }
            else
            {
                this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.ucInpatientDrugUseRateRank1";
            }
            return base.OnQuery(sender, neuObject);
        }
    }
}

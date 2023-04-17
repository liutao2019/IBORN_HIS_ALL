using System;
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
    public partial class ucDrugRateByDiagnose : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        private FS.HISFC.BizLogic.HealthRecord.ICD diagNoseMgr = new FS.HISFC.BizLogic.HealthRecord.ICD();

        /// <summary>
        /// 单病种用药金额分析
        /// </summary>
        public ucDrugRateByDiagnose()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.DrugRateByDiagnose";
            this.MainTitle = "单病种用药金额分析";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.CustomTypeShowType = "诊断：";
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select '3' id,'3' name,'3' memo,'3' spell_code,'3','3' input_code from dual";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
     
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.cmbCustomType.Items.Clear();
            ArrayList allData = diagNoseMgr.QueryDrgs(FS.HISFC.Models.HealthRecord.EnumServer.ICDTypes.ICD10);
            FS.FrameWork.Models.NeuObject allObj = new FS.FrameWork.Models.NeuObject();
            allObj.ID = "AAAA";
            allObj.Name = "全部";
            allData.Insert(0, allObj);
            this.cmbCustomType.AddItems(allData);
            this.cmbCustomType.SelectedIndex = 0;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }


        private new string[] GetQueryConditions()
        {
            string[] param = new string[3];

            param[0] = this.dtStart.Value.ToString();

            param[1] = this.dtEnd.Value.ToString();

            param[2] = this.cmbCustomType.Text.ToString().Replace("全部","AAAA");

            return param;
        }
    }
}

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
    public partial class ucInpatientDrugRankings : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        /// <summary>
        /// 住院患者用药排名
        /// </summary>
        public ucInpatientDrugRankings()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.InpatientDrugRankings";
            this.MainTitle = "住院患者用药排名";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11";
            this.CustomTypeShowType = "名次：";
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
           
            this.IsDeptAsCondition = true;
            this.QueryDataWhenInit = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem("1");
            ArrayList allData = SOC.HISFC.BizProcess.Cache.Pharmacy.itemHelper.ArrayObject;
            FS.FrameWork.Models.NeuObject allObj = new FS.FrameWork.Models.NeuObject();
            allObj.ID = "AAAA";
            allObj.Name = "全部";
            allData.Insert(0, allObj);
            this.cmbDrug.AddItems(allData);
            this.cmbDrug.SelectedIndex = 0;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.cmbCustomType.Text = "15";
            this.cmbCustomType.Tag = "15";
            FS.SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
            this.cmbDept.Items.Clear();
            ArrayList allData = FS.SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
            FS.FrameWork.Models.NeuObject allObj = new FS.FrameWork.Models.NeuObject();
            allObj.ID = "AAAA";
            allObj.Name = "全部";
            allData.Insert(0, allObj);
            this.cmbDept.AddItems(allData);
            this.cmbDept.SelectedIndex = 0;
        }


        private new string[] GetQueryConditions()
        {
            string[] param = new string[5];

            param[0] = this.cmbDept.Tag.ToString();
 
            param[1] = this.dtStart.Value.ToString();

            param[2] = this.dtEnd.Value.ToString();

            param[3] = this.cmbDrug.Tag.ToString();

            param[4] = this.cmbCustomType.Text.ToString();

            return param;
        }
    }
}

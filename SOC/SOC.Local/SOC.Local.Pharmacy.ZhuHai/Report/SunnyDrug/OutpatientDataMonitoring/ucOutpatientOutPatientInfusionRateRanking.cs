using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Report.SunnyDrug.OutpatientDataMonitoring
{
    public partial class ucOutpatientOutPatientInfusionRateRanking:FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        private FS.HISFC.BizLogic.Manager.Person personMgr = new FS.HISFC.BizLogic.Manager.Person();
        /// <summary>
        /// 门诊医生输液比例排名
        /// </summary>
        public ucOutpatientOutPatientInfusionRateRanking()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.ucOutpatientOutPatientInfusionRateRanking";
            this.MainTitle = "门诊医生输液比例排名";
            this.PriveClassTwos = "0320";
            this.SumColIndexs = "3,4,5,8,11,12,13";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10,11,12,13";
            this.IsUseCustomType = true;
            this.IsDeptAsCondition = true;
            this.IsAllowAllDept = true;
            this.CustomTypeShowType = "名次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
            ArrayList allEmp = personMgr.GetEmployeeAll();
            FS.FrameWork.Models.NeuObject allObj = new FS.FrameWork.Models.NeuObject();
            allObj.ID = "AAAA";
            allObj.Name = "全部";
            allEmp.Insert(0, allObj);
            this.cmbDoct.AddItems(allEmp);
            this.QueryDataWhenInit = false;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }

        private new string[] GetQueryConditions()
        {
            string[] param = new string[5];

            param[0] = string.IsNullOrEmpty(this.cmbDept.Tag.ToString()) ? "AAAA" : this.cmbDept.Tag.ToString();
            param[1] = this.dtStart.Value.ToString();

            param[2] = this.dtEnd.Value.ToString();

            param[3] = string.IsNullOrEmpty(this.cmbCustomType.Text.ToString()) ? "15" : this.cmbCustomType.Text.ToString();
            param[4] = string.IsNullOrEmpty(this.cmbDoct.Tag.ToString()) ? "AAAA" : this.cmbDoct.Tag.ToString();
            return param;
        }

    }
}

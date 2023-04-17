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
    public partial class ucOutPatientExceedTheAmountOfWarning : FS.SOC.Local.Pharmacy.Base.BaseReport
    {
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConsMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 门诊药品超过规定金额预警
        /// </summary>
        public ucOutPatientExceedTheAmountOfWarning()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.SunnyDrug.OutPatientExceedTheAmountOfWarning";
            this.MainTitle = "门诊药品超过规定金额预警";
            this.PriveClassTwos = "0320";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8";
            this.IsUseCustomType = true;
            this.IsDeptAsCondition = false;
            this.CustomTypeShowType = "名次：";
            this.CustomTypeSQL = @"select '15' id,'15' name,'15' memo,'15' spell_code,'15','15' input_code from dual";
            this.cmbCustomType.Text = "15";
            this.InitQueryCondition();
            this.IsDeptAsCondition = true;
            this.QueryDataWhenInit = false;
            this.cbAmount.CheckedChanged += new EventHandler(cbAmount_CheckedChanged);
            this.cbTotCost.CheckedChanged += new EventHandler(cbTotCost_CheckedChanged);
            this.cbTotCost.Visible = false;
            this.cbAmount.Visible = false;
            this.MidAdditionTitle = string.Empty;
            this.RightAdditionTitle = string.Empty;
        }

        private void InitQueryCondition()
        {
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "AAAA";
            objAll.Name = "全部";
            objAll.Memo = "全部";

            ArrayList alDrugType = consMgr.GetAllList("ITEMTYPE");
            alDrugType.Insert(0, objAll);
            this.ncmbDrugType.AddItems(alDrugType);
            this.ncmbDrugType.SelectedIndex = 0;
            List<FS.HISFC.Models.Pharmacy.PhaFunction> phaFunctionList = phaConsMgr.QueryPhaFunctionByLevel(2);
            ArrayList allPhaFunction = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.PhaFunction phaFunction in phaFunctionList)
            {
                allPhaFunction.Add(phaFunction);
            }
            allPhaFunction.Insert(0, objAll);
            this.ncmbPhyFunction.AddItems(allPhaFunction);
            this.ncmbPhyFunction.SelectedIndex = 0;
        }

        void cbTotCost_CheckedChanged(object sender, EventArgs e)
        {
            this.cbAmount.Checked = !this.cbTotCost.Checked;
        }

        void cbAmount_CheckedChanged(object sender, EventArgs e)
        {
            this.cbTotCost.Checked = !this.cbAmount.Checked;
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryAdditionConditions = GetQueryConditions();
            this.IsNeedAdditionConditions = true;
            return base.OnQuery(sender, neuObject);
        }


        private new string[] GetQueryConditions()
        {
            string[] param = new string[6];

            param[0] = this.dtStart.Value.ToString();

            param[1] = this.dtEnd.Value.ToString();

            param[2] = this.ncmbDrugType.Tag.ToString();

            param[3] = this.ncmbPhyFunction.Tag.ToString();

            param[4] = string.IsNullOrEmpty(this.cmbCustomType.Text.ToString()) ? "15" : this.cmbCustomType.Text.ToString();

            param[5] = this.txtExceedMoney.Text.ToString();
            return param;
        }
    }
}

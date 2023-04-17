using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// {FDD16528-9F0B-473a-869C-FFBCD66920C0}
    /// 手术费用查询
    /// </summary>
    public partial class ucOpsQueryFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ucOpsQueryFee()
        {
            InitializeComponent();
        }


        private string inPatientNO = string.Empty;

        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.HISFC.Models.RADT.PatientInfo myPatInfo = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();

        private FS.HISFC.BizProcess.Integrate.Manager baseinfoMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        private FS.FrameWork.Public.ObjectHelper emplHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 住院号
        /// </summary>
        public string InPatientNO
        {
            set
            {
                this.inPatientNO = value;

                if (string.IsNullOrEmpty(this.inPatientNO))
                {
                    MessageBox.Show("您没有选择患者信息！");
                    return;
                }
                else
                {
                    this.InPatientInfo = this.radtManager.GetPatientInfoByPatientNO(this.inPatientNO);
                }
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo InPatientInfo
        {
            get
            {
                return this.myPatInfo;
            }
            set
            {
                this.myPatInfo = value;
                if (value != null)
                {
                    this.lbName.Text = value.Name;
                    this.lbAge.Text = value.Age;
                    this.lbSex.Text = value.Sex.Name;
                    this.lbPatient.Text = value.PID.PatientNO;
                    this.lbDept.Text = value.PVisit.PatientLocation.Dept.Name;
                    this.lbPayKind.Text = value.Pact.Name;

                    this.QueryDrugFee(this.myPatInfo);
                    this.QueryUnDrugFee(this.myPatInfo);
                }
            }
        }

        private void QueryDrugFee(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ArrayList drugs = this.feeManager.GetMedItemsForInpatient(patient.ID, patient.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            decimal totCost = 0;
            this.fpDrugFee_Sheet1.RowCount = 0;
            if (drugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList drug in drugs)
                {
                    if(string.IsNullOrEmpty(drug.OperationNO))
                    {
                        continue;
                    }
                    fpDrugFee_Sheet1.Rows.Add(fpDrugFee_Sheet1.RowCount, 1);
                    int row = fpDrugFee_Sheet1.RowCount - 1;
                    //添加项目名称
                    if (drug.Item.Specs == "")
                        fpDrugFee_Sheet1.SetValue(row, 0, drug.Item.Name, false);
                    else
                        fpDrugFee_Sheet1.SetValue(row, 0, drug.Item.Name + "[" + drug.Item.Specs + "]", false);
                    //价格
                    fpDrugFee_Sheet1.SetValue(row, 3, drug.Item.Price, false);
                    //数量
                    fpDrugFee_Sheet1.SetValue(row, 1, drug.Item.Qty, false);
                    //单位
                    fpDrugFee_Sheet1.SetValue(row, 2, drug.Item.PriceUnit, false);
                    //总额
                    fpDrugFee_Sheet1.SetValue(row, 4, drug.FT.TotCost, false);
                    //开方科室
                    fpDrugFee_Sheet1.SetValue(row, 5, this.deptHelper.GetName(drug.RecipeOper.Dept.ID), false);
                    //开方医生
                    fpDrugFee_Sheet1.SetValue(row, 6, this.emplHelper.GetName(drug.RecipeOper.ID), false);
                    //收费人
                    fpDrugFee_Sheet1.SetValue(row, 7, this.emplHelper.GetName(drug.FeeOper.ID), false);
                    //收费时间
                    fpDrugFee_Sheet1.SetValue(row, 8, drug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + drug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpDrugFee_Sheet1.Rows.Add(fpDrugFee_Sheet1.RowCount, 1);
                int row = fpDrugFee_Sheet1.RowCount - 1;
                fpDrugFee_Sheet1.SetValue(row, 3, "合计", false);
                fpDrugFee_Sheet1.SetValue(row, 4, totCost, false);
            }
        }

        private void QueryUnDrugFee(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            ArrayList undrugs = this.feeManager.QueryFeeItemLists(patient.ID, patient.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            decimal totCost = 0;
            this.fpUnDrugFee_Sheet1.RowCount = 0;
            if (undrugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList undrug in undrugs)
                {
                    if (string.IsNullOrEmpty(undrug.OperationNO))
                    {
                        continue;
                    }
                    fpUnDrugFee_Sheet1.Rows.Add(fpUnDrugFee_Sheet1.RowCount, 1);
                    int row = fpUnDrugFee_Sheet1.RowCount - 1;
                    //添加项目名称
                    if (undrug.Item.Specs == "")
                        fpUnDrugFee_Sheet1.SetValue(row, 0, undrug.Item.Name, false);
                    else
                        fpUnDrugFee_Sheet1.SetValue(row, 0, undrug.Item.Name + "[" + undrug.Item.Specs + "]", false);
                    //价格
                    fpUnDrugFee_Sheet1.SetValue(row, 3, undrug.Item.Price, false);
                    //数量
                    fpUnDrugFee_Sheet1.SetValue(row, 1, undrug.Item.Qty, false);
                    //单位
                    fpUnDrugFee_Sheet1.SetValue(row, 2, undrug.Item.PriceUnit, false);
                    //总额
                    fpUnDrugFee_Sheet1.SetValue(row, 4, undrug.FT.TotCost, false);
                    //开方科室
                    fpUnDrugFee_Sheet1.SetValue(row, 5, this.deptHelper.GetName(undrug.RecipeOper.Dept.ID), false);
                    //开方医生
                    fpUnDrugFee_Sheet1.SetValue(row, 6, this.emplHelper.GetName(undrug.RecipeOper.ID), false);
                    //收费人
                    fpUnDrugFee_Sheet1.SetValue(row, 7, this.emplHelper.GetName(undrug.FeeOper.ID), false);
                    //收费时间
                    fpUnDrugFee_Sheet1.SetValue(row, 8, undrug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + undrug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                fpUnDrugFee_Sheet1.Rows.Add(fpUnDrugFee_Sheet1.RowCount, 1);
                int row = fpUnDrugFee_Sheet1.RowCount - 1;
                fpUnDrugFee_Sheet1.SetValue(row, 3, "合计", false);
                fpUnDrugFee_Sheet1.SetValue(row, 4, totCost, false);
            }
        }

        private void ucOpsQueryFee_Load(object sender, EventArgs e)
        {
            ArrayList aldept = this.baseinfoMgr.GetDepartment();
            this.deptHelper.ArrayObject = aldept;

            ArrayList alempl = this.baseinfoMgr.QueryEmployeeAll();
            this.emplHelper.ArrayObject = alempl;
            
        }

    }
}

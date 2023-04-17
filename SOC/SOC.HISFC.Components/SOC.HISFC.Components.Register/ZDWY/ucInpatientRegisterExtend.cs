using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Register.ZDWY
{
    public partial class ucInpatientRegisterExtend : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientRegisterExtend()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 当前患者
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 住院入出转业务层
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// 诊断业务层-met_cas_diagnose
        /// </summary>
        protected FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// 数据库业务
        /// </summary>
        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();

        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucQueryInpatientNo1_myEvent();
            return 1;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show("请先查询患者");

                return -1;
            }
            this.currentPatient.SIMainInfo.ID = this.ucQueryInpatientNo1.InpatientNo;
            this.currentPatient.SIMainInfo.ICCardCode = this.tbYFZCode.Text;//优抚证号
            this.currentPatient.SIMainInfo.AnotherCity.Name = this.tbBelongArea.Text;//属区
            this.currentPatient.SIMainInfo.ApplyType.Name = this.tbObjectClass.Text;//对象类别
            this.currentPatient.SIMainInfo.ItemPayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbDiaCost.Text);//诊费
            this.currentPatient.SIMainInfo.AddTotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbNuersingCost.Text);//护理费
            this.currentPatient.SIMainInfo.BaseCost = FS.FrameWork.Function.NConvert.ToDecimal(this.tbBedCost.Text);//床位费

            try
            {
                if (this.UpdateDataBase(this.currentPatient) < 1)
                {
                    if (this.InsertDataBase(this.currentPatient) < 1)
                        return -1;
                }
                MessageBox.Show("患者信息扩展录入成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 1;
        }


        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show("该患者不存在!请验证后输入");

                return;
            }

            this.currentPatient = this.radtManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                MessageBox.Show("查询患者基本信息出错!" + this.radtManager.Err);

                return;
            }
            this.SetPatientInfo();
        }

        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        protected void SetPatientInfo()
        {
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                return;
            }

            this.ucQueryInpatientNo1.Text = this.currentPatient.PID.PatientNO;//住院号
            this.lblName.Text = this.currentPatient.Name;//姓名;
            this.lblSex.Text = this.currentPatient.Sex.Name;
            this.lblAge.Text = this.currentPatient.Age;
            this.lblBed.Text = this.currentPatient.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.currentPatient.PVisit.PatientLocation.Bed.ID.Substring(4) : this.currentPatient.PVisit.PatientLocation.Bed.ID;
            this.lblDept.Text = this.currentPatient.PVisit.PatientLocation.Dept.Name;

            if (this.currentPatient.Pact.PayKind.ID == "03")
            {
                FS.HISFC.Models.Base.PactInfo pact = this.PactManagment.GetPactUnitInfoByPactCode(this.currentPatient.Pact.ID);
                this.lblPact.Text = this.currentPatient.Pact.Name + " 自付比例：" + pact.Rate.PayRate * 100 + "%" + " 日限额:" + this.currentPatient.FT.DayLimitCost;//合同单位
            }
            else
            {
                this.lblPact.Text = this.currentPatient.Pact.Name;//合同单位
            }

            this.lblDateIn.Text = this.currentPatient.PVisit.InTime.ToShortDateString();//住院日期
            if (this.currentPatient.PVisit.OutTime != DateTime.MinValue)
            {
                this.lblOutDate.Text = this.currentPatient.PVisit.OutTime.ToShortDateString();
            }
            this.lblInState.Text = this.currentPatient.PVisit.InState.Name;//在院状态
            decimal TotCost = this.currentPatient.FT.TotCost + this.currentPatient.FT.BalancedCost;
            //this.lblTotCost.Text = this.currentPatient.FT.TotCost.ToString();
            this.lblTotCost.Text = TotCost.ToString();

            this.lblOwnCost.Text = this.currentPatient.FT.OwnCost.ToString();
            this.lblPubCost.Text = this.currentPatient.FT.PubCost.ToString();
            this.lblPrepayCost.Text = this.currentPatient.FT.PrepayCost.ToString();
            this.lblUnBalanceCost.Text = this.currentPatient.FT.TotCost.ToString();
            this.lblBalancedCost.Text = this.currentPatient.FT.BalancedCost.ToString();
            this.lblFreeCost.Text = this.currentPatient.FT.LeftCost.ToString();
            this.lblDiagnose.Text = this.currentPatient.ClinicDiagnose;     //入院诊断-门诊诊断；而不是患者在院的主诊断
            this.lblMemo.Text = this.currentPatient.Memo;   //备注需要显示出来

            //取入院天数
            int day = this.radtIntegrate.GetInDays(this.currentPatient.ID);
            if (day > 1)
            {
                day -= 1;
            }
            this.txtDays.Text = day.ToString();
            //取接诊时间
            this.txtArriveDate.Text = this.radtIntegrate.GetArriveDate(this.currentPatient.ID).ToShortDateString();

            //取在院诊断
            try
            {
               System.Collections.ArrayList alInDiag = this.diagMgr.QueryDiagnoseByInpatientNOAndPersonType(this.currentPatient.ID, "1");
                if (alInDiag != null && alInDiag.Count > 0)
                {
                    //取最新的在院诊断
                    this.lblInDiagInfo.Text = (alInDiag[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
                }
            }
            catch (Exception ex) { }
            this.QueryDateBase(this.currentPatient.ID);
            this.tbDiaCost.Text = this.currentPatient.SIMainInfo.ItemPayCost.ToString();
            this.tbNuersingCost.Text = this.currentPatient.SIMainInfo.AddTotCost.ToString();
            this.tbBedCost.Text = this.currentPatient.SIMainInfo.BaseCost.ToString();
            this.tbYFZCode.Text = this.currentPatient.SIMainInfo.ICCardCode;
            this.tbBelongArea.Text = this.currentPatient.SIMainInfo.AnotherCity.Name;
            this.tbObjectClass.Text = this.currentPatient.SIMainInfo.ApplyType.Name;
        }

        #region 增删改查

        private int UpdateDataBase(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return this.radtManager.UpdatePatientExtendInfo(patientInfo);
        }


        private int InsertDataBase(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return this.radtManager.InsertPatientExtendInfo(patientInfo);
        }

        private int QueryDateBase(string inPatientNo)
        {
            System.Collections.ArrayList al = this.radtManager.QueryPatientExtendInfo(inPatientNo);
            FS.HISFC.Models.RADT.PatientInfo tempPatient = al[0] as FS.HISFC.Models.RADT.PatientInfo;
            if (tempPatient != null)
            {
                this.currentPatient.SIMainInfo.ICCardCode = tempPatient.SIMainInfo.ICCardCode;//优抚证号
                this.currentPatient.SIMainInfo.AnotherCity.Name = tempPatient.SIMainInfo.AnotherCity.Name;//属区
                this.currentPatient.SIMainInfo.ApplyType.Name = tempPatient.SIMainInfo.ApplyType.Name;//对象类别
                this.currentPatient.SIMainInfo.ApplyType.ID = tempPatient.SIMainInfo.ApplyType.ID;//对象类别编码
                this.currentPatient.SIMainInfo.ItemPayCost = tempPatient.SIMainInfo.ItemPayCost;//诊费
                this.currentPatient.SIMainInfo.AddTotCost = tempPatient.SIMainInfo.AddTotCost;//护理费
                this.currentPatient.SIMainInfo.BaseCost = tempPatient.SIMainInfo.BaseCost;//床位费
            }
            return 1;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IBorn.SI.MedicalInsurance.FoShan.BaseControls
{
    public partial class ucInPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 住院患者信息管理类
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();

        public ucInPatientInfo()
        {
            InitializeComponent();
        }

        void InitData()
        {
            var sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
            this.cmbSex.AddItems(sexListTemp);

            //var deptList = this.managerIntegrate.GetDeptmentAllValid();
            //if (deptList == null)
            //{
            //    MessageBox.Show("初始化挂号科室出错!" + this.managerIntegrate.Err);
            //    return;
            //}
            //this.cmbDept.AddItems(deptList);

            //this.cmbNurseStation.AddItems(deptList);

            var pactList = pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.pactManager.Err);
                return;
            }
            this.cmbPact.AddItems(pactList);

            var treamTypeList = constantManager.GetAllList("MEDICALINSURANCEITEM");
            if (treamTypeList != null)
            {
                cmbTreamType.AddItems(treamTypeList);
            }

            //入院情况信息
            this.cmbCircs.AddItems(constantManager.GetAllList("INCIRCS"));
            //入院方式
            this.cmbInSource.AddItems(constantManager.GetAllList("INSOURCE"));
        }

        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo register)
        {
            this.Clear();
            if (register == null)
            {
                return;
            }
            this.tbPatientNO.Text = register.PID.PatientNO;
            this.tbName.Text = register.Name;
            this.tbAge.Text = this.pactManager.GetAge(register.Birthday);
            this.cmbSex.Tag = register.Sex.ID.ToString();
            this.txtIDCardNO.Text = register.IDCard;
            this.cmbPact.Tag = register.Pact.ID;

            this.txtMainDiag.Text = register.MainDiagnose;
            this.tbClinicDiag.Text = register.ClinicDiagnose;
            this.txtDept.Text = register.PVisit.PatientLocation.Dept.Name;
            this.txtNurseStation.Text = register.PVisit.PatientLocation.NurseCell.Name;
            this.txtBedNO.Text = register.PVisit.PatientLocation.Bed.ID.Length > 4 ? register.PVisit.PatientLocation.Bed.ID.Substring(4) : register.PVisit.PatientLocation.Bed.ID;
            var outWay = managerIntegrate.GetConstant("ZG", register.PVisit.ZG.ID);
            if (outWay != null)
            {
                this.txtOutWay.Text = outWay.Name;  //获取常数名称
            }
            cmbTreamType.Tag = register.HealthTreamType;
            this.tbInDate.Text = register.PVisit.InTime.ToString("yyyy-MM-dd hh:mm:ss");
            this.tbOutDate.Text = register.PVisit.OutTime.ToString("yyyy-MM-dd hh:mm:ss");
            this.cmbInSource.Tag = register.PVisit.InSource.ID;
            this.cmbCircs.Tag = register.PVisit.Circs.ID;

            //{40E8A76F-3FEC-4740-8D54-59895DE0DC32}
            //获取婴儿数量和最早生日时间
            System.Collections.ArrayList babies = inpatientManager.QueryBabiesByMother(register.ID);
            if (babies != null)
            {
                this.tbBabyCount.Text = babies.Count.ToString();
                DateTime deliveryTime = DateTime.MaxValue;
                foreach (FS.HISFC.Models.RADT.PatientInfo baby in babies)
                {
                    if (baby.Birthday < deliveryTime)
                    {
                        deliveryTime = baby.Birthday;
                    }
                }

                if (deliveryTime != DateTime.MaxValue && deliveryTime != DateTime.MinValue)
                {
                    this.tbDeliveryTime.Text = deliveryTime.ToString("yyyy-MM-dd hh:mm:ss");
                }
            }

            string regNO = string.Empty;
            string invoiceNO = string.Empty;
            if (this.pactManager.GetSiRegisterNO(register.ID, register.SIMainInfo.InvoiceNo, ref regNO, ref invoiceNO) > 0)
            {

                if (string.IsNullOrEmpty(regNO))
                {
                    lbSIInfo.Text = "未关联医保登记";
                }
                else if (string.IsNullOrEmpty(invoiceNO))
                {
                    lbSIInfo.Text = string.Format("医保登记号:{0}  未关联医保结算", regNO);
                }
                else
                {
                    lbSIInfo.Text = string.Format("医保登记号:{0}  医保已结算", regNO);
                }
            }
        }

        public void Clear()
        {
            this.tbPatientNO.Text = string.Empty;
            this.tbName.Text = string.Empty;
            this.tbAge.Text = string.Empty;
            this.cmbSex.Tag = "";
            this.txtIDCardNO.Text = string.Empty;
            this.cmbPact.Tag = "";

            this.tbClinicDiag.Text = string.Empty;
            this.txtMainDiag.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtNurseStation.Text = string.Empty;
            this.txtBedNO.Text = string.Empty;
            this.txtOutWay.Text = string.Empty;

            this.tbInDate.Text = string.Empty;
            this.tbOutDate.Text = string.Empty;
            this.tbDeliveryTime.Text = string.Empty;
            this.tbBabyCount.Text = string.Empty;
            lbSIInfo.Text = string.Empty;
        }

        private void ucInPatientInfo_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Clear();
                InitData();
            }
        }
        public Action<string> GetRegisterByPatientNO;
        private void tbPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && GetRegisterByPatientNO != null)
            {
                GetRegisterByPatientNO(tbPatientNO.Text);
            }
        }
    }
}

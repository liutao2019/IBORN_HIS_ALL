using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance.SIBalance
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
        /// <summary>
        /// 医保代理类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        /// <summary>
        /// 委托查询患者
        /// </summary>
        public Action<string> QueryPatientInfoByPatientNO;

        /// <summary>
        /// 构造
        /// </summary>
        public ucInPatientInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucInPatientInfo_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Clear();
                InitData();
            }
        }

        /// <summary>
        /// 初始化界面
        /// </summary>
        void InitData()
        {
            var sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
            this.cmbSex.AddItems(sexListTemp);

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

            this.cmbCircs.AddItems(constantManager.GetAllList("INCIRCS")); //入院情况信息
            this.cmbInSource.AddItems(constantManager.GetAllList("INSOURCE")); //入院方式
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.Clear();
            if (patientInfo == null)
            {
                return;
            }
            this.tbPatientNO.Text = patientInfo.PID.PatientNO;
            this.tbName.Text = patientInfo.Name;
            this.tbAge.Text = this.pactManager.GetAge(patientInfo.Birthday);
            this.cmbSex.Tag = patientInfo.Sex.ID.ToString();
            this.txtIDCardNO.Text = patientInfo.IDCard;
            this.cmbPact.Tag = patientInfo.Pact.ID;

            this.txtMainDiag.Text = patientInfo.MainDiagnose;
            this.tbClinicDiag.Text = patientInfo.ClinicDiagnose;
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtNurseStation.Text = patientInfo.PVisit.PatientLocation.NurseCell.Name;
            this.txtBedNO.Text = patientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? patientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : patientInfo.PVisit.PatientLocation.Bed.ID;
            var outWay = managerIntegrate.GetConstant("ZG", patientInfo.PVisit.ZG.ID);
            if (outWay != null)
            {
                this.txtOutWay.Text = outWay.Name;  //获取常数名称
            }
            cmbTreamType.Tag = patientInfo.HealthTreamType;
            this.tbInDate.Text = patientInfo.PVisit.InTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.tbOutDate.Text = patientInfo.PVisit.OutTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.cmbInSource.Tag = patientInfo.PVisit.InSource.ID;
            this.cmbCircs.Tag = patientInfo.PVisit.Circs.ID;

            //获取婴儿数量和最早生日时间
            System.Collections.ArrayList babies = inpatientManager.QueryBabiesByMother(patientInfo.ID);
            if (babies != null)
            {
                this.tbBabyCount.Text = babies.Count.ToString();
                patientInfo.SIMainInfo.Fetus_cnt = this.tbBabyCount.Text;
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
                    this.tbDeliveryTime.Text = deliveryTime.ToString("yyyy-MM-dd HH:mm:ss");
                    patientInfo.SIMainInfo.Birctrl_matn_date = this.tbDeliveryTime.Text;
                }
            }

            long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            if (medcareInterfaceProxy.GetRegInfoInpatient(patientInfo) > 0)
            {

                if (string.IsNullOrEmpty(patientInfo.SIMainInfo.RegNo))
                {
                    lbSIInfo.Text = "未关联医保登记";
                }
                else if (string.IsNullOrEmpty(patientInfo.SIMainInfo.InvoiceNo))
                {
                    lbSIInfo.Text = string.Format("医保登记号:{0}  未结算", patientInfo.SIMainInfo.RegNo);
                }
                else
                {
                    lbSIInfo.Text = string.Format("医保登记号:{0},结算发票号:{1}  医保已结算", patientInfo.SIMainInfo.RegNo, patientInfo.SIMainInfo.InvoiceNo);
                }
            }
        }

        /// <summary>
        /// 住院号回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPatientNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && QueryPatientInfoByPatientNO != null)
            {
                QueryPatientInfoByPatientNO(tbPatientNO.Text);
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
    }
}

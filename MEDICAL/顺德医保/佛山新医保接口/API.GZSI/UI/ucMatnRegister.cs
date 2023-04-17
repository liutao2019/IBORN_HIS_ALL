using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using API.GZSI.Business;
using API.GZSI.Models;

namespace API.GZSI.UI
{
    public partial class ucMatnRegister : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 挂号管理类
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        /// <summary>
        /// 费用
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// 帐户管理
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 当前患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient;

        /// <summary>
        /// 当前患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        private string trt_dcla_detl_sn = string.Empty;

        /// <summary>
        /// 公共返回值
        /// </summary>
        int returnvalue = 0;

        /// <summary>
        /// 交易流水号
        /// </summary>
        string SerialNumber = string.Empty;

        /// <summary>
        /// 交易版本号
        /// </summary>
        string strTransVersion = string.Empty;

        /// <summary>
        /// 交易验证码
        /// </summary>
        string strVerifyCode = string.Empty;

        /// <summary>
        /// 患者入出转业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        public ucMatnRegister()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucMatnRegister_Load);
        }

        private void ucMatnRegister_Load(object sender, EventArgs e)
        {
            this.cmbMatnType.AddItems(constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "matn_type"));
            this.tbCardNO.KeyDown += new KeyEventHandler(tbCardNO_KeyDown);
            this.btnRegister.Click += new EventHandler(btnRegister_Click);
            this.btnCancelRegister.Click += new EventHandler(btnCancelRegister_Click);
        }

        /// <summary>
        /// 根据病历号检索患者挂号信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCardNO_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNO = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbCardNO.Text.Trim(), "'", "[", "]");

                if (string.IsNullOrEmpty(cardNO))
                {
                    this.tbCardNO.SelectAll();
                    this.tbCardNO.Focus();
                    return;
                }

                FS.HISFC.Models.Account.AccountCard accountObj = new FS.HISFC.Models.Account.AccountCard();
                if (this.feeMgr.ValidMarkNO(cardNO, ref accountObj) == -1)
                {
                    MessageBox.Show(feeMgr.Err);
                    this.tbCardNO.SelectAll();
                    this.tbCardNO.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(accountObj.Patient.PID.CardNO))
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("您输入的就诊卡号不存在"), "提示");
                    this.tbCardNO.SelectAll();
                    this.tbCardNO.Focus();
                    return;
                }

                this.Patient = accountObj.Patient;

                string patientInfo = "患者姓名：" + accountObj.Patient.Name + ",";
                patientInfo += "电话号码：" + accountObj.Patient.PhoneHome + ",";
                patientInfo += "证件号码：" + accountObj.Patient.IDCard;
                this.lblPatientInfo.Text = patientInfo;

                this.fpPrint_Sheet1.Cells[2, 1].Text = patient.Name;
                this.fpPrint_Sheet1.Cells[2, 5].Text = patient.IDCard;
            }
        }

        /// <summary>
        /// 生育登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (this.patient == null || string.IsNullOrEmpty(this.patient.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }

            //预结算弹出个人信息
            frmPatientQuery frmSIPatient = new frmPatientQuery();
            frmSIPatient.Patient = patient;
            frmSIPatient.OperateType = "2";
            if (frmSIPatient.ShowDialog() == DialogResult.OK)
            {
                patient = frmSIPatient.Patient as FS.HISFC.Models.RADT.PatientInfo;
            }
            else
            {
                MessageBox.Show("未选择参保人信息！");
                return;
            }

            Patient90201 patient90201 = new Patient90201();
            Models.Request.RequestGzsiModel90201 requestGzsiModel90201 = new API.GZSI.Models.Request.RequestGzsiModel90201();
            Models.Response.ResponseGzsiModel90201 responseGzsiModel90201 = new API.GZSI.Models.Response.ResponseGzsiModel90201();
            Models.Request.RequestGzsiModel90201.Data data = new API.GZSI.Models.Request.RequestGzsiModel90201.Data();

            FS.HISFC.Models.RADT.PatientInfo patientInfo = frmSIPatient.Patient as FS.HISFC.Models.RADT.PatientInfo;

            this.fpPrint_Sheet1.Cells[2, 3].Text = patientInfo.SIMainInfo.Psn_no;

            data.psn_no = patientInfo.SIMainInfo.Psn_no;	//人员编号 Y
            data.fixmedins_code = Models.UserInfo.Instance.userId;	//定点医药机构编号 Y
            data.fixmedins_name = "顺德爱博恩妇产医院";	//定点医药机构名称 Y
            data.tel = patientInfo.PhoneHome;	//联系电话 Y
            data.geso_val = "";	//孕周数
            data.fetts = "";	//胎次
            data.matn_type = this.cmbMatnType.Tag.ToString();	//生育类别  Y
            data.matn_trt_dclaer_type = "";	//生育待遇申报人员类别
            data.fpsc_no = "";	//计划生育服务证号
            data.last_mena_date = "";	//末次月经日期
            data.plan_matn_date = "";	//预计生育日期
            data.begndate = this.dtpBegin.Value.Date.ToString();	//开始日期 Y
            data.enddate = this.dtpEnd.Value.Date.ToString();	//结束日期 Y
            data.agnter_name = "";	//代办人姓名
            data.agnter_cert_type = "";	//代办人证件类型
            data.agnter_certno = "";	//代办人证件号码
            data.agnter_tel = "";	//代办人联系方式
            data.agnter_rlts = "";	//代办人关系
            data.agnter_addr = "-";	//代办人联系地址
            data.spus_name = "";	//配偶姓名
            data.spus_cert_type = "";	//配偶证件类型
            data.spus_certno = "";	//配偶证件号码
            requestGzsiModel90201.data = data;

            returnvalue = patient90201.CallService(requestGzsiModel90201, ref responseGzsiModel90201, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                MessageBox.Show("生育登记失败：" + patient90201.ErrorMsg);
                return;
            }

            this.trt_dcla_detl_sn = responseGzsiModel90201.output.result.trt_dcla_detl_sn;

            this.fpPrint_Sheet1.Cells[1, 5].Text = trt_dcla_detl_sn;

            MessageBox.Show("生育登记成功:" + this.trt_dcla_detl_sn);
        }

        /// <summary>
        /// 取消生育登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelRegister_Click(object sender, EventArgs e)
        {
        }
    }
}

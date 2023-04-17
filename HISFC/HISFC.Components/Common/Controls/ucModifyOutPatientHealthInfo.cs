using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Controls
{
    public delegate void OnSave();

    /// <summary>
    /// 患者健康体征
    /// </summary>
    public partial class ucModifyOutPatientHealthInfo : UserControl
    {
        public ucModifyOutPatientHealthInfo()
        {
            InitializeComponent();
            lblBMI.Text = string.Empty;

            this.dateTimePicker1.Checked = false;
            this.txtmemo.Visible = false;
            this.chehy.Checked = false;
        }

        #region 变量

        private FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();

        private FS.HISFC.Models.Registration.Register regInfo = new FS.HISFC.Models.Registration.Register();

        private FS.HISFC.BizProcess.Integrate.Registration.Registration regIntergrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 数据库管理类
        /// </summary>
        private FS.FrameWork.Management.DataBaseManger dbManager = new FS.FrameWork.Management.DataBaseManger();

    

        /// <summary>
        /// 默认保存体征信息的天数 0 标识不默认
        /// </summary>
        private int rememberHelthHistoryDays = 7;

        /// <summary>
        /// 默认保存体征信息的天数 0 标识不默认
        /// </summary>
        [Category("参数设置"), Description("默认保存体征信息的天数 0 标识不默认 默认:身高、体重"), DefaultValue(0)]
        public int RememberHelthHistoryDays
        {
            get
            {
                return rememberHelthHistoryDays;
            }
            set
            {
                rememberHelthHistoryDays = value;
            }
        }

        private string errInfo = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrInfo
        {
            get
            {
                return errInfo;
            }
        }

        /// <summary>
        /// 当切患者信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register RegInfo
        {
            get
            {
                return regInfo;
            }
            set
            {
                regInfo = value;

                this.Clear();

                if (regInfo != null && !string.IsNullOrEmpty(regInfo.ID))
                {
                    if (this.ShowHealthInfo(regInfo.ID) == -1)
                    {
                        MessageBox.Show(errInfo);
                    }
                }
            }
        }

        private bool isVisibleSave = false;

        /// <summary>
        /// 保存按钮是否可见
        /// </summary>
        public bool IsVisibleSave
        {
            get
            {
                return isVisibleSave;
            }
            set
            {
                isVisibleSave = value;
                this.btOK.Visible = value;
            }
        }

        /// <summary>
        /// 调用保存
        /// </summary>
        public event OnSave OnSave;

        #endregion

        private void btOK_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.Save() == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errInfo);
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("保存成功！");
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            return this.UpdateHealthInfo(this.regInfo.ID, this.txtHeight.Text, this.txtWeight.Text,
                this.txtSBP.Text, this.txtDBP.Text, this.txtTem.Text, this.txtBloodGlu.Text);
        }

        /// <summary>
        /// 更新保存
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP">血压：收缩压</param>
        /// <param name="DBP">血压：舒张压</param>
        /// <returns></returns>
        private int UpdateHealthInfo(string clinicCode, string height, string weight, string SBP, string DBP, string Tem, string bloodGlu)
        {
            if (this.regInfo == null || string.IsNullOrEmpty(this.regInfo.ID))
            {
                errInfo = "患者信息为空！请选择患者！";
                return -1;
            }

            try
            {
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(txtHeight.Text);
            }
            catch
            {
                errInfo = "身高输入错误：非法数字！";
                this.txtHeight.Focus();
                return -1;
            }
            try
            {
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(this.txtWeight.Text);
            }
            catch
            {
                errInfo = "体重输入错误：非法数字！";
                this.txtWeight.Focus();
                return -1;
            }
            try
            {
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(this.txtSBP.Text);
            }
            catch
            {
                errInfo = "收缩压输入错误：非法数字！";
                this.txtSBP.Focus();
                return -1;
            }
            try
            {
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDBP.Text);
            }
            catch
            {
                errInfo = "舒张压输入错误：非法数字！";
                this.txtDBP.Focus();
                return -1;
            }

            try
            {
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTem.Text);
            }
            catch
            {
                errInfo = "体温输入错误：非法数字！";
                this.txtDBP.Focus();
                return -1;
            }

            try
            {
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBloodGlu.Text);
            }
            catch
            {
                errInfo = "血糖输入错误：非法数字！";
                this.txtBloodGlu.Focus();
                return -1;
            }

            int rev = this.outOrderMgr.UpdateHealthInfo(height, weight, SBP, DBP, clinicCode, Tem, bloodGlu);
            if (rev == -1)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = "更新患者体征信息失败：" + this.outOrderMgr.Err;
                return -1;
            }
            return rev;
        }

        private void Clear()
        {
            this.txtHeight.Text = string.Empty;
            this.txtWeight.Text = string.Empty;
            this.txtSBP.Text = string.Empty;
            this.txtDBP.Text = string.Empty;
            this.txtTem.Text = string.Empty;
            this.txtBloodGlu.Text = string.Empty;
            this.dateTimePicker1.Checked = false;
            this.chehy.Checked = false;
            this.txtmemo.Text = string.Empty;
            this.txtmemo.Tag = string.Empty;
        }

        /// <summary>
        /// 提示信息
        /// </summary>
        NotifyIcon notify = null;

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP">血压：收缩压</param>
        /// <param name="DBP">血压：舒张压</param>
        /// <returns></returns>
        private int ShowHealthInfo(string clinicCode)
        {
            if (string.IsNullOrEmpty(clinicCode))
            {
                return 1;
            }

            ////没有挂号信息时，不报错，因为有些地方医生站可以自动挂号
            //FS.HISFC.Models.Registration.Register regObj = this.regIntergrate.GetByClinic(clinicCode);
            //if (regObj == null || string.IsNullOrEmpty(regObj.ID))
            //{
            //    return 1;
            //}

            string height = "";
            string weight = "";
            string SBP = "";
            string DBP = "";
            string TEM = "";
            string bloodGlu = "";
            string bmi = "";
            string mb = "";

            if (notify == null)
            {
                notify = new NotifyIcon();
                notify.Icon = Components.Common.Properties.Resources.MEDICAL;
            }
            notify.Visible = false;

            int rev = this.outOrderMgr.GetHealthInfo(clinicCode, ref height, ref weight, ref SBP, ref DBP, ref TEM, ref bloodGlu);

            if (rev == -1)
            {
                errInfo = "获取患者体征信息失败：" + this.outOrderMgr.Err;
                return -1;
            }



            //没有挂号记录或者是新看诊的患者，才会去取相关信息
            if (rev == 0 || this.regInfo.IsSee == false)
            {

                if (string.IsNullOrEmpty(height) && string.IsNullOrEmpty(weight))
                {
                    // //{FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}根据身高体重获取bmi 从一体机获取身高体重
                    this.outOrderMgr.GetByPhoneAndDate(RegInfo.PhoneHome, RegInfo.DoctorInfo.SeeDate.ToShortDateString(), RegInfo.IDCard, ref height, ref weight, ref bmi, ref SBP, ref DBP, ref mb);
                }

                if (string.IsNullOrEmpty(height) && string.IsNullOrEmpty(weight))
                {
                    // RegInfo.PhoneHome
                    if (this.rememberHelthHistoryDays > 0)
                    {
                        if (this.outOrderMgr.GetHealthInfo(regInfo.PID.CardNO, this.rememberHelthHistoryDays, ref height, ref weight, ref SBP, ref DBP, ref TEM, ref bloodGlu) > 0)
                        {
                            this.txtHeight.Text = height;
                            this.txtWeight.Text = weight;
                            this.txtSBP.Text = SBP;
                            this.txtDBP.Text = DBP;
                            this.txtTem.Text = TEM;
                            this.txtBloodGlu.Text = bloodGlu;

                            notify.Visible = true;
                            notify.ShowBalloonTip(2, "体征信息提示", "当前显示体征信息为上次默认值!\r\n如有变化，请注意修改保存！", ToolTipIcon.Info);
                        }
                    }
                    else
                    {
                        errInfo = "没有挂号记录";
                        return 0;
                    }
                }
                else  //{a54407f8-6c9a-4027-a5f1-1e1486316144}
                {
                    this.txtHeight.Text = height;
                    this.txtWeight.Text = weight;
                    //{FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}根据身高体重获取bmi
                    if (string.IsNullOrEmpty(bmi) && !string.IsNullOrEmpty(height) && !string.IsNullOrEmpty(weight))
                    {
                        bmi = Math.Round(Convert.ToDouble(weight) / ((Convert.ToDouble(height) / 100) * (Convert.ToDouble(height) / 100)), 2).ToString();
                    }
                    lblBMI.Text = bmi;
                    this.txtSBP.Text = SBP;
                    this.txtDBP.Text = DBP;
                    this.txtTem.Text = TEM;
                    this.txtBloodGlu.Text = bloodGlu;
                }
            }  //已看诊患者显示已保存的信息
            else
            {
                this.txtHeight.Text = height;
                this.txtWeight.Text = weight;
                //{FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}根据身高体重获取bmi
                if (string.IsNullOrEmpty(bmi) && !string.IsNullOrEmpty(height) && !string.IsNullOrEmpty(weight))
                {
                    bmi = Math.Round(Convert.ToDouble(weight) / ((Convert.ToDouble(height) / 100) * (Convert.ToDouble(height) / 100)), 2).ToString();
                }
                lblBMI.Text = bmi;
                this.txtSBP.Text = SBP;
                this.txtDBP.Text = DBP;
                this.txtTem.Text = TEM;
                this.txtBloodGlu.Text = bloodGlu;
                this.txtWeight.Text = weight;
                this.txtHeight.Text = height;
            }


            FS.HISFC.Models.Base.Department currDept = (FS.HISFC.Models.Base.Department)(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept);

            if (currDept.ID == "5011")
            {
                this.txtmemo.Visible = true;
                getPatientInfoExceptedTime(clinicCode);
            }
           
            return 1;
        }

        //{2DF1211B-9E5F-4b0b-B5CA-8EDBC4FC3B59}
        private void getPatientInfoExceptedTime(string clinicCode)
        {
            string sql = "select crmid from fin_opr_register t left join com_patientinfo t1 on t.card_no=t1.card_no where t.clinic_code='" + clinicCode + "'";

            string crmid = dbManager.ExecSqlReturnOne(sql);


            string req = "<req><crmid>" + crmid + "</crmid></req>";

            string exceptedTime = FS.HISFC.BizProcess.Integrate.WSHelper.GetPatientInfoExceptedTime(req);
      


            DateTime time = new DateTime();
            if (!string.IsNullOrEmpty(exceptedTime))
            {
                time = FS.FrameWork.Function.NConvert.ToDateTime(exceptedTime);
                this.dateTimePicker1.Checked = true;
                this.dateTimePicker1.Value = time;

                this.txtmemo.Text = "CRM系统预产期："+ exceptedTime;
                this.txtmemo.Tag = exceptedTime;
            }
            
        }

        private void ucModifyOutPatientHealthInfo_Load(object sender, EventArgs e)
        {
            this.Clear();
        }

        /// <summary>
        /// 获取当前界面的体征信息，保存在患者实体
        /// </summary>
        /// <param name="regObj"></param>
        /// <returns></returns>
        public int GetHealthInfo(ref FS.HISFC.Models.Registration.Register regObj)
        {
            regObj.Height = this.txtHeight.Text;
            regObj.Weight = this.txtWeight.Text;
            regObj.SBP = this.txtSBP.Text;
            regObj.DBP = this.txtDBP.Text;
            regObj.Temperature = this.txtTem.Text;
            regObj.BloodGlu = this.txtBloodGlu.Text;

            return 1;
        }

        /// <summary>
        /// obis 同步信息  {39942c2e-ec7b-4db2-a9e6-3da84d4742fb}
        /// </summary>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP"></param>
        /// <param name="DBP"></param>
        /// <param name="TEM"></param>
        /// <param name="bloodGlu"></param>
        public void GetHealthInfo(string height, string weight, string SBP, string DBP, string TEM, string bloodGlu, string expectedtime)
        {
            this.txtHeight.Text = height;
            this.txtWeight.Text = weight;
            string btms = "";
            if (!string.IsNullOrEmpty(height) && !string.IsNullOrEmpty(weight))
            {
                btms = Math.Round(Convert.ToDouble(weight) / ((Convert.ToDouble(height) / 100) * (Convert.ToDouble(height) / 100)), 2).ToString();
            }
            lblBMI.Text = btms;
            this.txtSBP.Text = SBP;
            this.txtDBP.Text = DBP;
            this.txtTem.Text = TEM;
            this.txtBloodGlu.Text = bloodGlu;

            if (expectedtime != "")
            {
                this.dateTimePicker1.Checked = true;
                this.dateTimePicker1.Value = Convert.ToDateTime(expectedtime);
            }


        }

        //{64FDFB25-6A75-42b4-9E00-80BDEE666706}

        /// <summary>
        /// 获取预产期
        /// </summary>
        /// <returns></returns>
        public string GetExpectedTime()
        {

            bool iseqs = this.dateTimePicker1.Value.ToShortDateString() == this.txtmemo.Tag.ToString();

            if (iseqs)
            {
                return "1";  //设置标识不用同步修改
            }
            else if (this.dateTimePicker1.Checked == true)
            {
                return this.dateTimePicker1.Value.ToString();
            }

            return "";
        }

        public bool GetChecked()
        {
            return this.chehy.Checked;
        }

        /// <summary>
        /// /{FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}根据身高体重获取bmi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtHeight_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight.Text) && !string.IsNullOrEmpty(txtHeight.Text))
            {
                lblBMI.Text = Math.Round(Convert.ToDouble(txtWeight.Text) / ((Convert.ToDouble(txtHeight.Text) / 100) * (Convert.ToDouble(txtHeight.Text) / 100)), 2).ToString();
            }
            else
                lblBMI.Text = string.Empty;
        }

        /// <summary>
        /// {FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}根据身高体重获取bmi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWeight.Text) && !string.IsNullOrEmpty(txtHeight.Text))
            {
                lblBMI.Text = Math.Round(Convert.ToDouble(txtWeight.Text) / ((Convert.ToDouble(txtHeight.Text) / 100) * (Convert.ToDouble(txtHeight.Text) / 100)), 2).ToString();
            }
            else
                lblBMI.Text = string.Empty;
        }
    }
}
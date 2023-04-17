﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Registration;

namespace FS.HISFC.Components.Terminal.Confirm.IBORN
{
	/// <summary>
	/// ucPatientInformation <br></br>
	/// [功能描述: 终端确认的患者基本信息UC]<br></br>
	/// [创 建 者: 赫一阳]<br></br>
	/// [创建时间: 2006-03-07]<br></br>
	/// <修改记录
	///		修改人=''
	///		修改时间=''
	///		修改目的=''
	///		修改描述=''
	///  />
	/// </summary>
	public partial class ucPatientInformationIBORN : FS.FrameWork.WinForms.Controls.ucBaseControl
	{
		public ucPatientInformationIBORN()
		{
			InitializeComponent();
		}

		#region 变量

		/// <summary>
		/// 用于实现在总UC实现按键查询患者基本信息的代理
		/// </summary>
		public delegate void MyDelegate(object sender, System.Windows.Forms.KeyEventArgs e);

		/// <summary>
		/// 用于实现在总UC实现按键查询患者基本信息的事件
		/// </summary>
		public event MyDelegate KeyDownInQureyCode;

		/// <summary>
		/// 住院号选择事件代理
		/// </summary>
		public delegate void delegateInpatient();

		/// <summary>
		/// 住院号选择事件
		/// </summary>
		public event delegateInpatient SelectInpatientNO;
        FS.HISFC.BizLogic.Terminal.TerminalConfirm confirmMgr = new FS.HISFC.BizLogic.Terminal.TerminalConfirm();
        FS.FrameWork.Public.ObjectHelper objHelp = new FS.FrameWork.Public.ObjectHelper();

        ///// <summary>
        ///// 查询类型
        ///// </summary>
        //int queryType = 1;

		/// <summary>
		/// 当前选中的患者

		/// </summary>
		FS.HISFC.Models.Registration.Register register = new Register();
		
		/// <summary>
		/// 窗口类型 1-门诊；2－住院

		/// </summary>
		string windowType = "1";

		#endregion

        FS.HISFC.BizProcess.Integrate.Registration.Registration regIntegrate = 
            new FS.HISFC.BizProcess.Integrate.Registration.Registration();

		#region 属性

		/// <summary>
		/// 患者信息
		/// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public FS.HISFC.Models.Registration.Register Register
        {
            get
            {
                return this.register;
            }
            set
            {
                if (this.DesignMode)
                {
                    return;
                }

                if (value == null)
                {
                    this.textBoxAge.Text = "";
                    this.textBoxSex.Text = "";
                    this.textBoxSeeDepartment.Text = "";
                    this.textBoxPactCode.Text = "";
                    this.textBoxPatientName.Text = "";
                    this.textBoxPatientType.Text = "";
                    this.register = value;
                }
                else
                {

                    FS.HISFC.Models.Registration.Register regTemp = new Register();
                    regTemp = regIntegrate.QueryPatient(value.ID)[0] as FS.HISFC.Models.Registration.Register; //{28E9C979-1EC9-42b3-BBE7-12AD68521930}

                    this.register = regTemp;
                    this.register.Memo = value.Memo;
                }
                // 设置年龄
                if (register != null)
                {
                    if (this.register.Sex.Name == null || this.register.Sex.Name == "")
                    {
                        if (this.register.Sex.ID != null)
                        {
                            this.textBoxSex.Text = objHelp.GetName(this.register.Sex.ID.ToString());
                        }
                    }
                    this.textBoxSex.Text = this.register.Sex.Name;
                    if (this.register.Age == null || this.register.Age == "")
                    {
                        GetBirthDay(this.register);
                    }
                    else
                    {
                        this.textBoxAge.Text = this.register.Age;
                    }
                    this.textBoxSeeDepartment.Text = this.register.DoctorInfo.Templet.Dept.Name;
                    this.textBoxPactCode.Text = this.register.Pact.Name;
                    this.textBoxPatientName.Text = this.register.Name;

                    #region memo
                    switch (this.register.Memo)
                    {
                        case "1":
                            this.textBoxPatientType.Text = "门诊";
                            break;

                        case "2":
                            this.textBoxPatientType.Text = "住院";
                            break;
                        case "3":
                            this.textBoxPatientType.Text = "急诊";
                            break;
                        case "4": //个人体检
                        case "5": //集体体检
                            this.textBoxPatientType.Text = "体检";
                            break;
                        default:

                            break;
                    }
                    if ((register.Memo == null || register.Memo.ToString() == "") && windowType == "1")
                    {
                        if (textBoxPatientType.Text == "" || textBoxPatientType.Text == null)
                        {
                            textBoxPatientType.Text = "门诊";
                        }
                    }

                    #endregion

                    this.lblRegDate.Text = register.InputOper.OperTime.ToString();
                    this.lblSeeDate.Text = register.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm") + FS.SOC.HISFC.BizProcess.Cache.Common.GetNoonByTime(register.DoctorInfo.Templet.Begin).Name;
                }
                else
                {
                    this.textBoxAge.Text = "";
                    this.textBoxSex.Text = "";
                    this.textBoxSeeDepartment.Text = "";
                    this.textBoxPactCode.Text = "";
                    this.textBoxPatientName.Text = "";
                    this.textBoxPatientType.Text = "";
                }


                // 显示患者编号

                if (this.windowType == "1")
                {
                    // 门诊用，根据查询类别，显示患者病历号或门诊号
                    if (this.register != null)
                    {
                        if (this.labelDisplayType.Text == "体检号:" || this.labelDisplayType.Text == "门诊号:")
                        {
                            this.textBoxCode.Text = this.register.PID.CardNO;
                        }
                    }
                    else
                    {
                        this.textBoxCode.Text = "";
                    }
                }
                else // 住院用显示住院号
                {
                    if (this.register != null)
                    {
                        this.textBoxCode.Text = this.register.PID.PatientNO;
                    }
                    else
                    {
                        this.textBoxCode.Text = "";
                    }
                }

                // 帐户余额
                this.textBoxFreeCount.Text = "";
            }

        }
        public void GetBirthDay(FS.HISFC.Models.Registration.Register Register)
        {
            DateTime dtBirth = Register.Birthday;
            DateTime dtNow = confirmMgr.GetDateTimeFromSysDateTime();
            int years = 0;

            System.TimeSpan span = new TimeSpan(dtNow.Ticks - dtBirth.Ticks);

            years = span.Days / 365;

            if (years <= 0)
            {
                int month = span.Days / 30;

                if (month <= 0)
                {
                    textBoxAge.Text = span.Days.ToString() + "天";
                }
                else
                {
                    textBoxAge.Text = month.ToString() + "月";
                }
            }
            else
            {
                textBoxAge.Text = years.ToString() + "岁";
            }
        }
        ///// <summary>
        ///// 检索患者方式：1-门诊病历号；2-体检号；3-门诊号；4-住院号；5－住院病历号
        ///// </summary>
        //public int QueryType
        //{
        //    get
        //    {
        //        return this.queryType;
        //    }
        //    set
        //    {
        //        this.queryType = value;
        //        ChangQueryType();
        //    }
        //}

		/// <summary>
		/// 窗口类型：1-门诊用；2-住院用

		/// </summary>
		public string WindowType
		{
			get
			{
				return this.windowType;
			}
			set
			{
				this.windowType = value;
			}
		}


		#endregion

		#region 事件

		/// <summary>
		/// 患者编号回车，检索患者基本信息

		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void textBoxCode_KeyDown(object sender, KeyEventArgs e)
		{
			if (this.DesignMode)
			{
				return;
			}
			if (e.KeyCode == Keys.Enter)
			{
				// 空，不进行处理

                //{689C5BF1-E740-4a4b-B067-F527885F8E68}
                this.textBoxCode.Text = FS.FrameWork.Public.String.TakeOffSpecialChar(this.textBoxCode.Text, "'", "[", "(", ")", "]");

				if (this.textBoxCode.Text.Equals(""))
				{
					return;
				}

                HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();
                //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                frmQuery.IsFliterUnValid = true;
                frmQuery.QueryByName(this.textBoxCode.Text);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.textBoxCode.Text = frmQuery.PatientInfo.PID.CardNO;


                    // 让总UC实现检索患者
                    if (KeyDownInQureyCode != null)
                    {
                        this.KeyDownInQureyCode(sender, e);
                    }
                }
			}
		}

		

		/// <summary>
		/// 选择住院号事件

		/// </summary>
		private void ucQueryInpatientNo1_myEvent()
		{
			if (this.DesignMode)
			{
				return;
			}

            //this.textBoxCode.Text = this.ucQueryInpatientNo1.InpatientNo;

			// 让总UC实现检索患者

			this.SelectInpatientNO();
		}

		#endregion

		#region 函数

        ///// <summary>
        ///// 切换检索患者方式：1-卡号；2-体检号；3-门诊号；4-住院号 
        ///// </summary>
        //public void ChangQueryType()
        //{
        //    if (this.DesignMode)
        //    {
        //        return;
        //    }
        //    if (queryType == 1)
        //    {
        //        labelDisplayType.Text = "门诊号:";
        //    }
        //    else if (queryType == 2)
        //    {
        //        labelDisplayType.Text = "体检号:";
        //    }
        //}
 
		/// <summary>
		/// 清空当前的患者基本信息

		/// </summary>
		public void Clear()
		{
			this.Register = null;
		}

		/// <summary>
		/// 设置焦点到检索码输入框

		/// </summary>
		public void SetFocus()
		{
			this.textBoxCode.Focus();
			this.textBoxCode.SelectAll();
		}

		
		#endregion

        private void ucPatientInformation_Load(object sender, EventArgs e)
        {
            objHelp.ArrayObject = FS.HISFC.Models.Base.SexEnumService.List();
        } 
        private void labelDisplayType_Click(object sender, EventArgs e)
        {
            if (this.labelDisplayType.Text == "门诊号:")
            {
                this.labelDisplayType.Text = "体检号:";
            }
            else if (this.labelDisplayType.Text == "体检号:")
            {
                this.labelDisplayType.Text = "门诊号:";
            }
        }

        private void tbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            if (e.KeyCode == Keys.Enter)
            {
                // 空，不进行处理

                //{689C5BF1-E740-4a4b-B067-F527885F8E68}
                this.tbPhone.Text = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbPhone.Text, "'", "[", "(", ")", "]");

                if (this.tbPhone.Text.Equals(""))
                {
                    return;
                }

                HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();
                //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                frmQuery.IsFliterUnValid = true;
                frmQuery.QueryByPhone(this.tbPhone.Text);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.textBoxCode.Text = frmQuery.PatientInfo.PID.CardNO;


                    // 让总UC实现检索患者
                    if (KeyDownInQureyCode != null)
                    {
                        this.KeyDownInQureyCode(sender, e);
                    }
                }
            }
        }

		
	}
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Registration;

namespace FS.SOC.Local.OutpatientFee.FoSi
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
	public partial class ucPatientInformation : FS.FrameWork.WinForms.Controls.ucBaseControl
	{
		public ucPatientInformation()
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
				this.register = value;

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

                }
				else
				{
					this.textBoxAge.Text = "";
                    this.textBoxSex.Text = "";
                    this.textBoxSeeDepartment.Text = "";
                    this.textBoxPactCode.Text = "";
                    this.textBoxPatientName.Text = "";
				}

				// 显示患者编号

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
				this.KeyDownInQureyCode(sender, e);
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

			this.SelectInpatientNO();
		}

		#endregion

		#region 函数
 
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
		}

		
		#endregion

        private void ucPatientInformation_Load(object sender, EventArgs e)
        {
            objHelp.ArrayObject = FS.HISFC.Models.Base.SexEnumService.List();
        } 

		
	}
}

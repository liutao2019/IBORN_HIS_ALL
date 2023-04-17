using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace GZSI.Controls
{
	/// <summary>
	/// frmChangePatientSiInfo 的摘要说明。
	/// 修改患者医保信息
	/// 更新记录
	/// </summary>
	public class frmChangePatientSiInfo : FS.FrameWork.WinForms.Forms.BaseForm
	{
		private System.Windows.Forms.ToolBar toolBar1;
		private System.Windows.Forms.ToolBarButton tbReadAgain;
		private System.Windows.Forms.ToolBarButton tbSave;
		private System.Windows.Forms.ToolBarButton toolBarButton3;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components;
		private FS.HISFC.Components.Common.Controls.ucQueryInpatientNo txtPatientNo;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lblPatientInfo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label lblSex;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblRegNo;
		private System.Windows.Forms.Label lblPact;
		private System.Windows.Forms.Label lblCardNo;
		private System.Windows.Forms.Label lblPatientNo;
		private System.Windows.Forms.Label lblWorkUnit;
		ucGetSiInHosInfo ucSi = new ucGetSiInHosInfo();
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy myInterface = new  FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
        FS.HISFC.BizLogic.RADT.InPatient myInpatient = new  FS.HISFC.BizLogic.RADT.InPatient();
		private System.Windows.Forms.Label lblDept;
		FS.FrameWork.Management.Transaction trans;

        Management.SILocalManager siMar = new global::GZSI.Management.SILocalManager();
        FS.HISFC.Models.RADT.PatientInfo info;
        FS.HISFC.Models.RADT.PatientInfo tempInfo;//临时存
		public frmChangePatientSiInfo()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChangePatientSiInfo));
            this.toolBar1 = new System.Windows.Forms.ToolBar();
            this.tbReadAgain = new System.Windows.Forms.ToolBarButton();
            this.tbSave = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.txtPatientNo = new FS.HISFC.Components.Common.Controls.ucQueryInpatientNo();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblWorkUnit = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblPatientNo = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCardNo = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPact = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblRegNo = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar1
            // 
            this.toolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbReadAgain,
            this.tbSave,
            this.toolBarButton3});
            this.toolBar1.ButtonSize = new System.Drawing.Size(48, 48);
            this.toolBar1.DropDownArrows = true;
            this.toolBar1.ImageList = this.imageList1;
            this.toolBar1.Location = new System.Drawing.Point(0, 0);
            this.toolBar1.Name = "toolBar1";
            this.toolBar1.ShowToolTips = true;
            this.toolBar1.Size = new System.Drawing.Size(578, 58);
            this.toolBar1.TabIndex = 0;
            this.toolBar1.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar1_ButtonClick);
            // 
            // tbReadAgain
            // 
            this.tbReadAgain.ImageIndex = 0;
            this.tbReadAgain.Name = "tbReadAgain";
            this.tbReadAgain.Text = "重新读取";
            // 
            // tbSave
            // 
            this.tbSave.ImageIndex = 1;
            this.tbSave.Name = "tbSave";
            this.tbSave.Text = "保  存";
            // 
            // toolBarButton3
            // 
            this.toolBarButton3.ImageIndex = 2;
            this.toolBarButton3.Name = "toolBarButton3";
            this.toolBarButton3.Text = "退  出";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblPatientInfo);
            this.panel1.Controls.Add(this.txtPatientNo);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 58);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 179);
            this.panel1.TabIndex = 1;
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.Location = new System.Drawing.Point(185, 10);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(368, 14);
            this.lblPatientInfo.TabIndex = 4;
            // 
            // txtPatientNo
            // 
            this.txtPatientNo.DefaultInputType = 0;
            this.txtPatientNo.InputType = 0;
            this.txtPatientNo.Location = new System.Drawing.Point(0, 4);
            this.txtPatientNo.Name = "txtPatientNo";
            this.txtPatientNo.PatientInState = "ALL";
            this.txtPatientNo.ShowState = FS.HISFC.Components.Common.Controls.enuShowState.All;
            this.txtPatientNo.Size = new System.Drawing.Size(182, 31);
            this.txtPatientNo.TabIndex = 1;
            this.txtPatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(this.txtPatientNo_myEvent);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.lblWorkUnit);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lblPatientNo);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.lblDept);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.lblCardNo);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblPact);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.lblRegNo);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lblName);
            this.groupBox2.Controls.Add(this.lblSex);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(0, 36);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(579, 142);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "医保信息";
            // 
            // lblWorkUnit
            // 
            this.lblWorkUnit.Location = new System.Drawing.Point(67, 84);
            this.lblWorkUnit.Name = "lblWorkUnit";
            this.lblWorkUnit.Size = new System.Drawing.Size(223, 16);
            this.lblWorkUnit.TabIndex = 16;
            this.lblWorkUnit.Text = "未知";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(9, 84);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(60, 16);
            this.label16.TabIndex = 15;
            this.label16.Text = "工作单位:";
            // 
            // lblPatientNo
            // 
            this.lblPatientNo.Location = new System.Drawing.Point(381, 23);
            this.lblPatientNo.Name = "lblPatientNo";
            this.lblPatientNo.Size = new System.Drawing.Size(103, 16);
            this.lblPatientNo.TabIndex = 14;
            this.lblPatientNo.Text = "未知";
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(315, 23);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 16);
            this.label14.TabIndex = 13;
            this.label14.Text = "住院号:";
            // 
            // lblDept
            // 
            this.lblDept.Location = new System.Drawing.Point(226, 54);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(76, 14);
            this.lblDept.TabIndex = 12;
            this.lblDept.Text = "未知";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(188, 54);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 14);
            this.label12.TabIndex = 11;
            this.label12.Text = "科室:";
            // 
            // lblCardNo
            // 
            this.lblCardNo.Location = new System.Drawing.Point(67, 54);
            this.lblCardNo.Name = "lblCardNo";
            this.lblCardNo.Size = new System.Drawing.Size(115, 14);
            this.lblCardNo.TabIndex = 10;
            this.lblCardNo.Text = "未知";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(9, 54);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 14);
            this.label10.TabIndex = 9;
            this.label10.Text = "身份证:";
            // 
            // lblPact
            // 
            this.lblPact.Location = new System.Drawing.Point(381, 54);
            this.lblPact.Name = "lblPact";
            this.lblPact.Size = new System.Drawing.Size(103, 14);
            this.lblPact.TabIndex = 8;
            this.lblPact.Text = "未知";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(315, 54);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 14);
            this.label8.TabIndex = 7;
            this.label8.Text = "结算类别:";
            // 
            // lblRegNo
            // 
            this.lblRegNo.Location = new System.Drawing.Point(381, 84);
            this.lblRegNo.Name = "lblRegNo";
            this.lblRegNo.Size = new System.Drawing.Size(115, 16);
            this.lblRegNo.TabIndex = 6;
            this.lblRegNo.Text = "未知";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(315, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "医疗证号:";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(67, 23);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(101, 16);
            this.lblName.TabIndex = 4;
            this.lblName.Text = "未知";
            // 
            // lblSex
            // 
            this.lblSex.Location = new System.Drawing.Point(224, 23);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(53, 16);
            this.lblSex.TabIndex = 3;
            this.lblSex.Text = "未知";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(188, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "性别:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "姓 名:";
            // 
            // frmChangePatientSiInfo
            // 
            this.ClientSize = new System.Drawing.Size(578, 237);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolBar1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChangePatientSiInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改患者医保信息";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		/// <summary>
		/// toolbar事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) 
		{
			//重新读取
			if(e.Button == this.tbReadAgain) 
			{
                    this.ucSi.RegName = this.info.Name;
					FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucSi);
					if(this.ucSi.PersonInfo==null) return;
					//if(string.IsNullOrEmpty(this.ucSi.PersonInfo.SSN))return;
					this.tempInfo = this.info.Clone();
					this.info = this.ucSi.PersonInfo.Clone();
					this.info.SIMainInfo.HosNo = this.ucSi.PersonInfo.SIMainInfo.HosNo;
                    this.info.SIMainInfo.RegNo = this.ucSi.PersonInfo.SIMainInfo.RegNo;
                    //if (tempInfo.Name !=info.Name&&info.IsBaby==false)
                    //{
                    //    this.info = null;
                    //    MessageBox.Show("患者姓名不一致，请核查！");
                    //    return;
                    //}
					this.lblName.Text = info.Name;
					this.lblSex.Text = info.Sex.Name;
                    this.lblPatientNo.Text = tempInfo.PID.PatientNO;
                    this.lblCardNo.Text = tempInfo.IDCard;
                    this.lblDept.Text = tempInfo.PVisit.PatientLocation.Dept.Name;
                    if (string.IsNullOrEmpty(info.Pact.ID))
                    {
                        info.Pact = tempInfo.Pact;
                    }
					this.lblPact.Text = info.Pact.Name;
					this.lblRegNo.Text = info.SIMainInfo.RegNo;
					this.lblWorkUnit.Text = info.CompanyName;
			}
				//保存
			else if(e.Button == this.tbSave) 
			{	
				if(this.info == null)
					return;
				if(this.tempInfo == null)
				{
					this.tempInfo = this.info.Clone();
				}
                if (myInterface==null)
                {
                    myInterface = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
                }
              //  string sgyyb = siMar.SGYBdll(this.info.Pact.ID);
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //if (!string.IsNullOrEmpty(sgyyb))
                //{
                //    myInterface.IsDealSgy = true;
                //    myInterface.SgySiDll = sgyyb;
                //}
                this.myInterface.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                this.siMar.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
				this.info.PID = this.tempInfo.PID;
				this.info.ID = this.tempInfo.ID;
				this.info.PVisit = this.tempInfo.PVisit;

                

                if (this.info.Pact.PayKind.ID == "02")
                {
                    this.info.SIMainInfo.IsValid = true;
                    int iReturn = myInterface.UploadRegInfoInpatient(this.info);
                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新医保信息失败!" + myInterface.ErrMsg);
                        myInterface = null;
                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("更新医保信息成功");
                }
                else 
                {
                    MessageBox.Show("该患者不是广州医保类型!" );
                    return;
                }

				
			}
				//退出
			else
				this.Close();

		}

		/// <summary>
		/// 清空显示
		/// </summary>
		private void Clear() 
		{
			this.lblRegNo.Text = "未知";
			this.lblPatientNo.Text = "未知";
			this.lblSex.Text = "未知";
			this.lblWorkUnit.Text = "未知";
			this.lblPact.Text = "未知";
			this.lblName.Text = "未知";
			this.lblDept.Text = "未知";
			this.lblCardNo.Text = "未知";
			this.lblPatientInfo.Text = "未知";
		}

		private void txtPatientNo_myEvent()
		{
			FS.HISFC.BizLogic.RADT.InPatient inpatient = new  FS.HISFC.BizLogic.RADT.InPatient();
			info = inpatient.QueryPatientInfoByInpatientNO(this.txtPatientNo.InpatientNo);
			if(info == null) 
			{
				inpatient = null;
				MessageBox.Show("获得患者基本信息出错!");
				return;
			}
            FS.HISFC.Models.RADT.PatientInfo temp = this.siMar.GetSIPersonInfo(this.txtPatientNo.InpatientNo);
			if(temp != null) 
			{
				info.SIMainInfo = temp.SIMainInfo;

                this.lblName.Text = temp.Name;
                this.lblSex.Text = temp.Sex.Name;
                this.lblPatientNo.Text = temp.PID.PatientNO;
                this.lblCardNo.Text = temp.IDCard;
                this.lblDept.Text = temp.PVisit.PatientLocation.Dept.Name;
                this.lblPact.Text = temp.Pact.Name;
                this.lblRegNo.Text = temp.SIMainInfo.RegNo;
                this.lblWorkUnit.Text = temp.CompanyName;
            }
			this.lblPatientInfo.Text = "姓名:"+info.Name+"  性别:"+info.Patient.Sex.Name+"  科室:"+info.PVisit.PatientLocation.Dept.Name+"  类别:"+info.Pact.Name;
			//temp = null;
		
		}
	}
}

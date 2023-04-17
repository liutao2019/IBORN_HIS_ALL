using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
//using ThoughtWorks.QRCode.Codec;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Register
{
	/// <summary>
	/// 患者腕带打印-非新生儿
	/// </summary>
	public class ucPatientBracelet : System.Windows.Forms.UserControl
    {
		private System.Windows.Forms.Label lblBed;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblAge;
        private PictureBox lblPatientId;
        private Label lblPatient;
        private Label label2;
        private PictureBox pictureBox1;
        private Label lblIndDate;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucPatientBracelet()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			// TODO: 在 InitializeComponent 调用后添加任何初始化

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

		#region 组件设计器生成的代码
		/// <summary> 
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.lblBed = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblPatientId = new System.Windows.Forms.PictureBox();
            this.lblPatient = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblIndDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lblPatientId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBed
            // 
            this.lblBed.AutoSize = true;
            this.lblBed.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Bold);
            this.lblBed.Location = new System.Drawing.Point(115, 41);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(52, 14);
            this.lblBed.TabIndex = 1;
            this.lblBed.Text = "住院号";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(115, 21);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 14);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "姓名";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Bold);
            this.lblSex.Location = new System.Drawing.Point(241, 41);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(37, 14);
            this.lblSex.TabIndex = 3;
            this.lblSex.Text = "性别";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Bold);
            this.lblAge.Location = new System.Drawing.Point(241, 21);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(37, 14);
            this.lblAge.TabIndex = 4;
            this.lblAge.Text = "年龄";
            // 
            // lblPatientId
            // 
            this.lblPatientId.Location = new System.Drawing.Point(311, 35);
            this.lblPatientId.Name = "lblPatientId";
            this.lblPatientId.Size = new System.Drawing.Size(57, 55);
            this.lblPatientId.TabIndex = 11;
            this.lblPatientId.TabStop = false;
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Bold);
            this.lblPatient.Location = new System.Drawing.Point(115, 60);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(37, 14);
            this.lblPatient.TabIndex = 12;
            this.lblPatient.Text = "病区";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(374, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 27);
            this.label2.TabIndex = 13;
            this.label2.Text = "中山六院";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(52, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 55);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // lblIndDate
            // 
            this.lblIndDate.AutoSize = true;
            this.lblIndDate.Font = new System.Drawing.Font("黑体", 10F, System.Drawing.FontStyle.Bold);
            this.lblIndDate.Location = new System.Drawing.Point(115, 80);
            this.lblIndDate.Name = "lblIndDate";
            this.lblIndDate.Size = new System.Drawing.Size(67, 14);
            this.lblIndDate.TabIndex = 16;
            this.lblIndDate.Text = "登记日期";
            // 
            // ucPatientBracelet
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblIndDate);
            this.Controls.Add(this.lblPatientId);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblBed);
            this.Name = "ucPatientBracelet";
            this.Size = new System.Drawing.Size(1072, 114);
            ((System.ComponentModel.ISupportInitialize)(this.lblPatientId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion		

		private string strInpatienNo; //
		private string Err;
		private FS.HISFC.Models.RADT.PatientInfo PatientInfo;
        private FS.HISFC.Models.Account.PatientAccount OutPatientInfo;
        /// <summary>
        /// 控制参数
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// 住院患者业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

		
		/// <summary>
		/// 住院患者
		/// </summary>
        public FS.HISFC.Models.RADT.PatientInfo myPatientInfo 
		{
			get
			{
				return this.PatientInfo;
			}
			set 
			{
				this.strInpatienNo = value.ID ;
				if (this.strInpatienNo != null || this.strInpatienNo != "") 
				{
					try 
					{
						PatientInfo = value;
						Init();
					}
					catch(Exception ex){ this.Err =ex.Message;}

				}
			}
		}

        /// <summary>
        /// 门诊留观腕带
        /// </summary>
        public FS.HISFC.Models.Account.PatientAccount OutmyPatientInfo
        {
            get
            {
                return this.OutPatientInfo;
            }
            set
            {
                this.strInpatienNo = value.ID;
                if ( this.strInpatienNo != null || this.strInpatienNo != "" )
                {
                    try
                    {
                        OutPatientInfo = value;
                        InitOut();
                    }
                    catch ( Exception ex )
                    {
                        this.Err = ex.Message;
                    }

                }
            }
        }

        FS.HISFC.BizProcess.Integrate.Fee fee = new FS.HISFC.BizProcess.Integrate.Fee();
        //打印机名称
        private string printName = "";

		/// <summary>
		/// 设置显示
		/// </summary>
		private void Init()
		{
            string nuerseCell = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            nuerseCell = nuerseCell.Replace("护士站", "");
            lblPatient.Text = "病区:" + nuerseCell;//病区
            //labelDept.Text = "科室:" + PatientInfo.PVisit.PatientLocation.Dept.Name;//科室
			lblName.Text = "姓名:"+PatientInfo.Name;                            //姓名
            lblSex.Text = "性别:" + PatientInfo.Sex.Name;                         //性别
            lblAge.Text = "年龄:" + this.inpatientManager.GetAge(PatientInfo.Birthday, false);//年龄
            lblBed.Text = "住院号:" + PatientInfo.PID.PatientNO;          //住院号	
            lblIndDate.Text = "登记日期："+PatientInfo.PVisit.InTime.ToShortDateString();//入院日期
            this.MakeIMage(PatientInfo);
		}


        /// <summary>
        /// 设置显示
        /// </summary>
        private void InitOut()
        {
            string nuerseCell = OutPatientInfo.PVisit.PatientLocation.NurseCell.Name;
            nuerseCell = nuerseCell.Replace("护士站","");
            lblPatient.Text = "病区:" + nuerseCell;//病区
            //labelDept.Text = "科室:" + OutPatientInfo.PVisit.PatientLocation.Dept.Name;//科室
            lblName.Text = "姓名    :" + OutPatientInfo.Name;                            //姓名
            lblSex.Text = "性别:" + OutPatientInfo.Sex.Name;                         //性别
            lblAge.Text = "年龄:" + this.inpatientManager.GetAge(PatientInfo.Birthday, false);//年龄
            lblBed.Text = "住院号:" + OutPatientInfo.PID.PatientNO;          //住院号	 
            lblIndDate.Text = "登记日期：" + OutPatientInfo.PVisit.InTime.ToShortDateString();//入院日期
            this.PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.PatientInfo.ID = OutPatientInfo.PID.PatientNO;
            this.MakeIMage(OutPatientInfo);
        }

		/// <summary>
		/// 打印
		/// </summary>
        public void Print()
		{
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            p.PrintDocument.PrinterSettings.PrinterName = "DYWD";
            p.IsHaveGrid = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsPrintBackImage = false;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsCanCancel = false;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                p.PrintPreview(0, 140, this);
            }
            else
            {
                p.PrintPage(0, 140, this);
            }
		}

        private void MakeIMage(FS.HISFC.Models.RADT.Patient patient)
        {
            Image image = FS.SOC.Public.Function.CreateQRCode(patient);
            this.lblPatientId.Image = image;
            this.pictureBox1.Image = image;
        }
	}
}

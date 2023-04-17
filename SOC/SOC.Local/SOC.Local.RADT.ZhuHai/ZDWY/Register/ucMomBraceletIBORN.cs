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
    /// 患者腕带打印-新生儿
	/// </summary>
    public class ucMomBraceletIBORN : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint
    {
        private System.Windows.Forms.Label lblName;
        private Label lblBed;
        private Label lblDept;
        private Label lblSex;
        private Label lblAge;
        private Label lblPatientNO;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucMomBraceletIBORN()
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblBed = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblPatientNO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(276, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(85, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "姓名：林徐梦";
            // 
            // lblBed
            // 
            this.lblBed.AutoSize = true;
            this.lblBed.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBed.Location = new System.Drawing.Point(276, 69);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(67, 13);
            this.lblBed.TabIndex = 3;
            this.lblBed.Text = "床号：608";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(276, 53);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(85, 13);
            this.lblDept.TabIndex = 4;
            this.lblDept.Text = "科室：妇产科";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(276, 37);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(59, 13);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "性别：女";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(276, 22);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(73, 13);
            this.lblAge.TabIndex = 6;
            this.lblAge.Text = "年龄：23岁";
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.AutoSize = true;
            this.lblPatientNO.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNO.Location = new System.Drawing.Point(110, 29);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(129, 13);
            this.lblPatientNO.TabIndex = 7;
            this.lblPatientNO.Text = "住院号：1234567890";
            // 
            // ucMomBraceletIBORN
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblPatientNO);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.lblBed);
            this.Controls.Add(this.lblName);
            this.Name = "ucMomBraceletIBORN";
            this.Size = new System.Drawing.Size(1021, 95);
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


        FS.HISFC.BizProcess.Integrate.Fee fee = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

		/// <summary>
		/// 设置显示
		/// </summary>
		private void Init()
		{
            lblName.Text = "姓名：" + PatientInfo.Name;                            //姓名
            this.lblAge.Text = "年龄：" + this.dbMgr.GetAge(PatientInfo.Birthday);
            this.lblDept.Text = "科室：" + PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.lblSex.Text = "性别：" + PatientInfo.Sex.Name;
            this.lblPatientNO.Text = "住院号：" + PatientInfo.PID.PatientNO;
            if (PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4)
            {
                this.lblBed.Text = "床号：" + PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
            }
            else
            {
                this.lblBed.Text = "床号：" + PatientInfo.PVisit.PatientLocation.Bed.ID;
            }

            
		}

        /// <summary>
        /// 打印用
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

		/// <summary>
		/// 打印
		/// </summary>
        public void Print()
		{

            string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            if (string.IsNullOrEmpty(printerName))
            {
                return;
            }
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();
            ps = psManager.GetPageSize("DYWDMQ");
            if (ps == null)
            {
                ps = ps = new FS.HISFC.Models.Base.PageSize("DYWDMQ", 1016, 90);
            } 
            
            p.SetPageSize(ps);
            p.PrintDocument.PrinterSettings.PrinterName = printerName;
            p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            p.IsCanCancel = false;

            p.IsLandScape = true;
            //p.IsHaveGrid = false;
            //p.IsPrintBackImage = false;
            //p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                p.PrintPreview(0, 0, this);
            }
            else
            {
                p.PrintPage(0, 0, this);
            }
		}

        private void MakeIMage(FS.HISFC.Models.RADT.Patient patient)
        {
            Image image = FS.SOC.Public.Function.CreateQRCode(patient);

        }
	}
}

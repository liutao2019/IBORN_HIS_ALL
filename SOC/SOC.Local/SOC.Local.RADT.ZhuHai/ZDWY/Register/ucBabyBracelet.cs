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
    /// ���������ӡ-������
	/// </summary>
	public class ucBabyBracelet : System.Windows.Forms.UserControl
    {
		private System.Windows.Forms.Label lblBed;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblPatient;
        private PictureBox lblPatientId;
        private Label lblIndDate;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucBabyBracelet()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��

		}

		/// <summary> 
		/// ������������ʹ�õ���Դ��
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

		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
            this.lblBed = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblPatient = new System.Windows.Forms.Label();
            this.lblPatientId = new System.Windows.Forms.PictureBox();
            this.lblIndDate = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lblPatientId)).BeginInit();
            this.SuspendLayout();
            // 
            // lblBed
            // 
            this.lblBed.AutoSize = true;
            this.lblBed.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblBed.Location = new System.Drawing.Point(71, 46);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(52, 14);
            this.lblBed.TabIndex = 1;
            this.lblBed.Text = "סԺ��";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(71, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 14);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "����";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblSex.Location = new System.Drawing.Point(189, 25);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(37, 14);
            this.lblSex.TabIndex = 3;
            this.lblSex.Text = "�Ա�";
            // 
            // lblPatient
            // 
            this.lblPatient.AutoSize = true;
            this.lblPatient.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblPatient.Location = new System.Drawing.Point(71, 85);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(37, 14);
            this.lblPatient.TabIndex = 4;
            this.lblPatient.Text = "����";
            // 
            // lblPatientId
            // 
            this.lblPatientId.Location = new System.Drawing.Point(248, 31);
            this.lblPatientId.Name = "lblPatientId";
            this.lblPatientId.Size = new System.Drawing.Size(57, 55);
            this.lblPatientId.TabIndex = 11;
            this.lblPatientId.TabStop = false;
            // 
            // lblIndDate
            // 
            this.lblIndDate.AutoSize = true;
            this.lblIndDate.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblIndDate.Location = new System.Drawing.Point(71, 65);
            this.lblIndDate.Name = "lblIndDate";
            this.lblIndDate.Size = new System.Drawing.Size(67, 14);
            this.lblIndDate.TabIndex = 1;
            this.lblIndDate.Text = "��������";
            // 
            // ucBabyBracelet
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblIndDate);
            this.Controls.Add(this.lblPatientId);
            this.Controls.Add(this.lblPatient);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblBed);
            this.Name = "ucBabyBracelet";
            this.Size = new System.Drawing.Size(1072, 114);
            ((System.ComponentModel.ISupportInitialize)(this.lblPatientId)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion		

		private string strInpatienNo; //
		private string Err;
		private FS.HISFC.Models.RADT.PatientInfo PatientInfo;
        private FS.HISFC.Models.Account.PatientAccount OutPatientInfo;
        /// <summary>
        /// ���Ʋ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

		
		/// <summary>
		/// סԺ����
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
        /// �����������
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

		/// <summary>
		/// ������ʾ
		/// </summary>
		private void Init()
		{
            lblPatient.Text = "����:" + PatientInfo.PVisit.PatientLocation.NurseCell.Name;//����
            //labelDept.Text = "����:" + PatientInfo.PVisit.PatientLocation.Dept.Name;//����
			lblName.Text = "����:"+PatientInfo.Name;                            //����
            lblSex.Text = "�Ա�:" + PatientInfo.Sex.Name;                         //�Ա�
            lblBed.Text = "סԺ��:" + PatientInfo.PID.PatientNO;          //סԺ��	
            lblIndDate.Text = "��������:" + PatientInfo.Birthday.ToShortDateString();//��������
            this.MakeIMage(PatientInfo);
		}


        /// <summary>
        /// ������ʾ
        /// </summary>
        private void InitOut()
        {
            lblPatient.Text = "����:" + OutPatientInfo.PVisit.PatientLocation.NurseCell.Name;//����
            //labelDept.Text = "����:" + OutPatientInfo.PVisit.PatientLocation.Dept.Name;//����
            lblName.Text = "����    :" + OutPatientInfo.Name;                            //����
            lblSex.Text = "�Ա�:" + OutPatientInfo.Sex.Name;                         //�Ա�
            lblBed.Text = "סԺ��:" + OutPatientInfo.PID.PatientNO;          //סԺ��	 
            lblIndDate.Text = "��������:" + PatientInfo.Birthday.ToShortDateString();//��������
            this.PatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            this.PatientInfo.ID = OutPatientInfo.PID.PatientNO; 
            this.MakeIMage(OutPatientInfo);
        }

		/// <summary>
		/// ��ӡ
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
        }
	}
}

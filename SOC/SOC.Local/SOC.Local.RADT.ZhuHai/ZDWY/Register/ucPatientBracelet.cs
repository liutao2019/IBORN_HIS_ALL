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
	/// ���������ӡ-��������
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
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucPatientBracelet()
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
            this.lblBed.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblBed.Location = new System.Drawing.Point(115, 41);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(52, 14);
            this.lblBed.TabIndex = 1;
            this.lblBed.Text = "סԺ��";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(115, 21);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(37, 14);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "����";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblSex.Location = new System.Drawing.Point(241, 41);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(37, 14);
            this.lblSex.TabIndex = 3;
            this.lblSex.Text = "�Ա�";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblAge.Location = new System.Drawing.Point(241, 21);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(37, 14);
            this.lblAge.TabIndex = 4;
            this.lblAge.Text = "����";
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
            this.lblPatient.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblPatient.Location = new System.Drawing.Point(115, 60);
            this.lblPatient.Name = "lblPatient";
            this.lblPatient.Size = new System.Drawing.Size(37, 14);
            this.lblPatient.TabIndex = 12;
            this.lblPatient.Text = "����";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("΢���ź�", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(374, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(290, 27);
            this.label2.TabIndex = 13;
            this.label2.Text = "��ɽ��Ժ";
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
            this.lblIndDate.Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            this.lblIndDate.Location = new System.Drawing.Point(115, 80);
            this.lblIndDate.Name = "lblIndDate";
            this.lblIndDate.Size = new System.Drawing.Size(67, 14);
            this.lblIndDate.TabIndex = 16;
            this.lblIndDate.Text = "�Ǽ�����";
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
        //��ӡ������
        private string printName = "";

		/// <summary>
		/// ������ʾ
		/// </summary>
		private void Init()
		{
            string nuerseCell = PatientInfo.PVisit.PatientLocation.NurseCell.Name;
            nuerseCell = nuerseCell.Replace("��ʿվ", "");
            lblPatient.Text = "����:" + nuerseCell;//����
            //labelDept.Text = "����:" + PatientInfo.PVisit.PatientLocation.Dept.Name;//����
			lblName.Text = "����:"+PatientInfo.Name;                            //����
            lblSex.Text = "�Ա�:" + PatientInfo.Sex.Name;                         //�Ա�
            lblAge.Text = "����:" + this.inpatientManager.GetAge(PatientInfo.Birthday, false);//����
            lblBed.Text = "סԺ��:" + PatientInfo.PID.PatientNO;          //סԺ��	
            lblIndDate.Text = "�Ǽ����ڣ�"+PatientInfo.PVisit.InTime.ToShortDateString();//��Ժ����
            this.MakeIMage(PatientInfo);
		}


        /// <summary>
        /// ������ʾ
        /// </summary>
        private void InitOut()
        {
            string nuerseCell = OutPatientInfo.PVisit.PatientLocation.NurseCell.Name;
            nuerseCell = nuerseCell.Replace("��ʿվ","");
            lblPatient.Text = "����:" + nuerseCell;//����
            //labelDept.Text = "����:" + OutPatientInfo.PVisit.PatientLocation.Dept.Name;//����
            lblName.Text = "����    :" + OutPatientInfo.Name;                            //����
            lblSex.Text = "�Ա�:" + OutPatientInfo.Sex.Name;                         //�Ա�
            lblAge.Text = "����:" + this.inpatientManager.GetAge(PatientInfo.Birthday, false);//����
            lblBed.Text = "סԺ��:" + OutPatientInfo.PID.PatientNO;          //סԺ��	 
            lblIndDate.Text = "�Ǽ����ڣ�" + OutPatientInfo.PVisit.InTime.ToShortDateString();//��Ժ����
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
            this.pictureBox1.Image = image;
        }
	}
}

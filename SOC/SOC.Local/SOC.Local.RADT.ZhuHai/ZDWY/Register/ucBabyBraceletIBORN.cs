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
    public class ucBabyBraceletIBORN : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint
    {
        private System.Windows.Forms.Label lblName;
        private Label lblPatientNO;
        private Label lblAge;
        private Label lblSex;
        private Label lblDept;
        private Label lblBed;
        private Label lblName1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucBabyBraceletIBORN()
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblPatientNO = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.lblBed = new System.Windows.Forms.Label();
            this.lblName1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("����", 18F, System.Drawing.FontStyle.Bold);
            this.lblName.Location = new System.Drawing.Point(91, 19);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(111, 24);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "������BB";
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.AutoSize = true;
            this.lblPatientNO.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNO.Location = new System.Drawing.Point(87, 23);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(129, 13);
            this.lblPatientNO.TabIndex = 13;
            this.lblPatientNO.Text = "סԺ�ţ�1234567890";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(87, 52);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(73, 13);
            this.lblAge.TabIndex = 12;
            this.lblAge.Text = "���䣺23��";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(233, 37);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(59, 13);
            this.lblSex.TabIndex = 11;
            this.lblSex.Text = "�Ա�Ů";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(233, 52);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(85, 13);
            this.lblDept.TabIndex = 10;
            this.lblDept.Text = "���ң�������";
            // 
            // lblBed
            // 
            this.lblBed.AutoSize = true;
            this.lblBed.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBed.Location = new System.Drawing.Point(233, 23);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(67, 13);
            this.lblBed.TabIndex = 9;
            this.lblBed.Text = "���ţ�608";
            // 
            // lblName1
            // 
            this.lblName1.AutoSize = true;
            this.lblName1.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName1.Location = new System.Drawing.Point(87, 37);
            this.lblName1.Name = "lblName1";
            this.lblName1.Size = new System.Drawing.Size(85, 13);
            this.lblName1.TabIndex = 8;
            this.lblName1.Text = "������������";
            // 
            // ucBabyBraceletIBORN
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lblPatientNO);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblSex);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.lblBed);
            this.Controls.Add(this.lblName1);
            this.Controls.Add(this.lblName);
            this.Name = "ucBabyBraceletIBORN";
            this.Size = new System.Drawing.Size(582, 95);
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


        FS.HISFC.BizProcess.Integrate.Fee fee = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizLogic.Manager.DataBase dbMgr = new FS.HISFC.BizLogic.Manager.DataBase();

		/// <summary>
		/// ������ʾ
		/// </summary>
		private void Init()
		{
            if (PatientInfo.PID.PatientNO.Contains("B") && PatientInfo.User01 == "����")
            {
                lblName.Visible = true;
                this.lblBed.Visible = false;
                this.lblDept.Visible = false;
                this.lblName1.Visible = false;
                this.lblPatientNO.Visible = false;
                this.lblSex.Visible = false;
                this.lblAge.Visible = false;
                //{0885394D-28B1-4273-A1C8-FE97EA967981}����
                lblName.Text = PatientInfo.Name +" \nסԺ�ţ�" + PatientInfo.PID.PatientNO;
            }
            else if (!PatientInfo.PID.PatientNO.Contains("B") && PatientInfo.User01 == "����")// {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            {
                lblName.Visible = true;
                this.lblBed.Visible = false;
                this.lblDept.Visible = false;
                this.lblName1.Visible = false;
                this.lblPatientNO.Visible = false;
                this.lblSex.Visible = false;
                this.lblAge.Visible = false;
                lblName.Text = PatientInfo.Name + "BB" + " \nסԺ�ţ�" + PatientInfo.PID.PatientNO;
            }
            else 
            {
                lblName.Visible = false;
                this.lblBed.Visible = true;
                this.lblDept.Visible = true;
                this.lblName1.Visible = true;
                this.lblPatientNO.Visible = true;
                this.lblSex.Visible = true;
                this.lblAge.Visible = true;

                lblName1.Text = "������" + PatientInfo.Name;                            //����
               // {9DB0B2D6-B43F-491f-B71E-D96F384AE7F9}
                //this.lblAge.Text = "���䣺" + this.dbMgr.GetAge(PatientInfo.Birthday);
                this.lblAge.Text = "�������ڣ�" + PatientInfo.Birthday.Month + "��" + PatientInfo.Birthday.Day;
                this.lblDept.Text = "���ң�" + PatientInfo.PVisit.PatientLocation.Dept.Name;
                this.lblSex.Text = "�Ա�" + PatientInfo.Sex.Name;
                this.lblPatientNO.Text = "סԺ�ţ�" + PatientInfo.PID.PatientNO;
                if (PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4)
                {
                    this.lblBed.Text = "���ţ�" + PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4);
                }
                else
                {
                    this.lblBed.Text = "���ţ�" + PatientInfo.PVisit.PatientLocation.Bed.ID;
                }
            }
		}


		/// <summary>
		/// ��ӡ
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
            ps = psManager.GetPageSize("DYWDYE");
            if (ps == null)
            {
                ps = ps = new FS.HISFC.Models.Base.PageSize("DYWDYE", 575, 90);
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

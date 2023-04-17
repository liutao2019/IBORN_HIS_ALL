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
    public class ucMomBraceletIBORN : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Order.Inpatient.IBraceletPrint
    {
        private System.Windows.Forms.Label lblName;
        private Label lblBed;
        private Label lblDept;
        private Label lblSex;
        private Label lblAge;
        private Label lblPatientNO;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucMomBraceletIBORN()
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
            this.lblName.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(276, 6);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(85, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "������������";
            // 
            // lblBed
            // 
            this.lblBed.AutoSize = true;
            this.lblBed.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBed.Location = new System.Drawing.Point(276, 69);
            this.lblBed.Name = "lblBed";
            this.lblBed.Size = new System.Drawing.Size(67, 13);
            this.lblBed.TabIndex = 3;
            this.lblBed.Text = "���ţ�608";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(276, 53);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(85, 13);
            this.lblDept.TabIndex = 4;
            this.lblDept.Text = "���ң�������";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSex.Location = new System.Drawing.Point(276, 37);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(59, 13);
            this.lblSex.TabIndex = 5;
            this.lblSex.Text = "�Ա�Ů";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAge.Location = new System.Drawing.Point(276, 22);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(73, 13);
            this.lblAge.TabIndex = 6;
            this.lblAge.Text = "���䣺23��";
            // 
            // lblPatientNO
            // 
            this.lblPatientNO.AutoSize = true;
            this.lblPatientNO.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPatientNO.Location = new System.Drawing.Point(110, 29);
            this.lblPatientNO.Name = "lblPatientNO";
            this.lblPatientNO.Size = new System.Drawing.Size(129, 13);
            this.lblPatientNO.TabIndex = 7;
            this.lblPatientNO.Text = "סԺ�ţ�1234567890";
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
            lblName.Text = "������" + PatientInfo.Name;                            //����
            this.lblAge.Text = "���䣺" + this.dbMgr.GetAge(PatientInfo.Birthday);
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

        /// <summary>
        /// ��ӡ��
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

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

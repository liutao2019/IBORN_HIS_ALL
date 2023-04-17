using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
//using ThoughtWorks.QRCode.Codec;

namespace FS.SOC.Local.Registration.ZhuHai.Zdwy.IRegPrint
{
	/// <summary>
    /// ���������ӡ-������
	/// </summary>
    public class ucBraceletIBORN : System.Windows.Forms.UserControl, FS.HISFC.BizProcess.Interface.Registration.IBraceletPrint
    {
        private System.Windows.Forms.Label lblName;
        protected FS.FrameWork.WinForms.Controls.NeuPictureBox npxCardNO;
        private Label lblTitle;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblBirthday;
        private Label lblDept;
        private Label lblCardNO;
        private Label lblRegDate;
        private Label lblFR;
        private Label lbtoiletPWD;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

        public ucBraceletIBORN()
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
            this.npxCardNO = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblBirthday = new System.Windows.Forms.Label();
            this.lblDept = new System.Windows.Forms.Label();
            this.lblCardNO = new System.Windows.Forms.Label();
            this.lblRegDate = new System.Windows.Forms.Label();
            this.lblFR = new System.Windows.Forms.Label();
            this.lbtoiletPWD = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.npxCardNO)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("����", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblName.Location = new System.Drawing.Point(175, 64);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(72, 16);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "ŷ�����";
            // 
            // npxCardNO
            // 
            this.npxCardNO.Location = new System.Drawing.Point(160, 26);
            this.npxCardNO.Name = "npxCardNO";
            this.npxCardNO.Size = new System.Drawing.Size(104, 39);
            this.npxCardNO.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.npxCardNO.TabIndex = 222;
            this.npxCardNO.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("����", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(114, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(209, 19);
            this.lblTitle.TabIndex = 223;
            this.lblTitle.Text = "���ݰ�����������ҽԺ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(337, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 224;
            this.label2.Text = "�������ڣ�";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(337, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 225;
            this.label3.Text = "��    �ң�";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(337, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 226;
            this.label4.Text = "��    ��";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(337, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 227;
            this.label5.Text = "ʱ    �䣺";
            // 
            // lblBirthday
            // 
            this.lblBirthday.AutoSize = true;
            this.lblBirthday.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblBirthday.Location = new System.Drawing.Point(410, 19);
            this.lblBirthday.Name = "lblBirthday";
            this.lblBirthday.Size = new System.Drawing.Size(77, 13);
            this.lblBirthday.TabIndex = 228;
            this.lblBirthday.Text = "2017-10-29";
            // 
            // lblDept
            // 
            this.lblDept.AutoSize = true;
            this.lblDept.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDept.Location = new System.Drawing.Point(410, 35);
            this.lblDept.Name = "lblDept";
            this.lblDept.Size = new System.Drawing.Size(59, 13);
            this.lblDept.TabIndex = 229;
            this.lblDept.Text = "��������";
            // 
            // lblCardNO
            // 
            this.lblCardNO.AutoSize = true;
            this.lblCardNO.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCardNO.Location = new System.Drawing.Point(410, 51);
            this.lblCardNO.Name = "lblCardNO";
            this.lblCardNO.Size = new System.Drawing.Size(77, 13);
            this.lblCardNO.TabIndex = 230;
            this.lblCardNO.Text = "1234567890";
            // 
            // lblRegDate
            // 
            this.lblRegDate.AutoSize = true;
            this.lblRegDate.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblRegDate.Location = new System.Drawing.Point(410, 67);
            this.lblRegDate.Name = "lblRegDate";
            this.lblRegDate.Size = new System.Drawing.Size(140, 13);
            this.lblRegDate.TabIndex = 231;
            this.lblRegDate.Text = "2017-10-12 10:10:10";
            // 
            // lblFR
            // 
            this.lblFR.AutoSize = true;
            this.lblFR.Font = new System.Drawing.Font("����", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblFR.Location = new System.Drawing.Point(378, 2);
            this.lblFR.Name = "lblFR";
            this.lblFR.Size = new System.Drawing.Size(33, 13);
            this.lblFR.TabIndex = 232;
            this.lblFR.Text = "����";
            // 
            // lbtoiletPWD
            // 
            this.lbtoiletPWD.AutoSize = true;
            this.lbtoiletPWD.Location = new System.Drawing.Point(417, 2);
            this.lbtoiletPWD.Name = "lbtoiletPWD";
            this.lbtoiletPWD.Size = new System.Drawing.Size(53, 12);
            this.lbtoiletPWD.TabIndex = 234;
            this.lbtoiletPWD.Text = "36663663";
            // 
            // ucBraceletIBORN
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.lbtoiletPWD);
            this.Controls.Add(this.lblFR);
            this.Controls.Add(this.lblRegDate);
            this.Controls.Add(this.lblCardNO);
            this.Controls.Add(this.lblDept);
            this.Controls.Add(this.lblBirthday);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.npxCardNO);
            this.Controls.Add(this.lblName);
            this.Name = "ucBraceletIBORN";
            this.Size = new System.Drawing.Size(1016, 90);
            ((System.ComponentModel.ISupportInitialize)(this.npxCardNO)).EndInit();
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
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
      
		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="register"></param>
		/// <returns></returns>
        public int SetPrintValue(FS.HISFC.Models.Registration.Register register)
        {
            this.lblTitle.Text = FS.FrameWork.Management.Connection.Hospital.Name;
            npxCardNO.Image = FS.SOC.Public.Function.CreateBarCode(register.PID.CardNO, this.npxCardNO.Width, this.npxCardNO.Height);
            lblName.Text = register.Name;
            this.lblBirthday.Text = register.Birthday.ToLongDateString();
            this.lblCardNO.Text = register.Sex.Name; //register.PID.CardNO.TrimStart('0');//D8F6425B-1CFD-4b3f-921E-03B1ECA0F95E ����Ա��ҽ������
            this.lblDept.Text = register.DoctorInfo.Templet.Dept.Name + "-" + register.DoctorInfo.Templet.Doct.Name;
            this.lblRegDate.Text = register.DoctorInfo.SeeDate.ToString();
            if (register.IsFirst)
            {
                this.lblFR.Text = "����";
            }
            else
            {
                this.lblFR.Text = "����";
            }

            string rtn = string.Empty;
            rtn = this.ctlMgr.QueryControlerInfo("C00001");
            if (rtn == null || rtn == "-1" || rtn == "") rtn = " ";
            this.lbtoiletPWD.Text = rtn;

            return 1;
        }

        private FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction();

        FS.HISFC.BizProcess.Integrate.Fee fee = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��ӡ��
        /// </summary>
        private System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument();

		/// <summary>
		/// ��ӡ
		/// </summary>
        public void Print()
		{

            //{3BD28BAB-8DC0-466f-949E-0E7D18831AD8}
            //string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
            //if (string.IsNullOrEmpty(printerName))
            //{
            //    return;
            //}
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            FS.HISFC.Models.Base.PageSize ps = new FS.HISFC.Models.Base.PageSize();
            ps = psManager.GetPageSize("MZWD");
            if (ps == null)
            {
                ps = ps = new FS.HISFC.Models.Base.PageSize("MZWD", 1016, 90);
            } 
            p.SetPageSize(ps);
            ////{3BD28BAB-8DC0-466f-949E-0E7D18831AD8}
            //p.PrintDocument.PrinterSettings.PrinterName = printerName;
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

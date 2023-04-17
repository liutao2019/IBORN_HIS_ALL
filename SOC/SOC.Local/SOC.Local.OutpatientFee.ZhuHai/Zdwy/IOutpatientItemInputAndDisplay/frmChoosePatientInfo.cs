using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using FS.NFC.Interface.Forms;
using FS.FrameWork.WinForms.Forms;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IOutpatientItemInputAndDisplay
{
	/// <summary>
	/// frmChooseDate ��ժҪ˵����
	/// </summary>
	public class frmChoosePatientInfo : BaseForm {
        //private FS.FrameWork.WinForms.Controls.NeuPictureBox pictureBox1;
        //private FS.FrameWork.WinForms.Controls.NeuLabel label3;
        //private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        //private FS.FrameWork.WinForms.Controls.NeuButton btnExit;
        //private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginDate;
        //private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndDate;

        private FS.FrameWork.WinForms.Controls.NeuPictureBox pictureBox1;
        private FS.FrameWork.WinForms.Controls.NeuLabel label3;
        private FS.FrameWork.WinForms.Controls.NeuButton btnOK;
        private FS.FrameWork.WinForms.Controls.NeuButton btnExit;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpBeginDate;
        private FS.FrameWork.WinForms.Controls.NeuDateTimePicker dtpEndDate;

		private bool myIsOneDate = false;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblBeginDate;
        private FS.FrameWork.WinForms.Controls.NeuLabel lblEndDate;
        private Label label1;
        private TextBox tbcardno;
        private CheckBox ckquery;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		//�Ƿ�ֻ����ѡ��һ������
		public bool IsOneDate {
			set{
				this.myIsOneDate = value;
				//this.Init();
			}
		}

		//��ʼʱ��
		public DateTime DateBegin {
			get{return this.dtpBeginDate.Value;}
			set{this.dtpBeginDate.Value = value;}
		}

		//��ֹʱ��
		public DateTime DateEnd {
			get{return this.dtpEndDate.Value;}
			set{this.dtpEndDate.Value = value;}
		}
        /// <summary>
        /// ����
        /// </summary>
        public String CardNo
        {
            get { return this.tbcardno.Text; }
            set { this.tbcardno.Text = value; }
        
        }

        public bool CkQuery
        {
            get { return this.ckquery.Checked; }
            set { this.ckquery.Checked = value; }
        
        }
 

        public frmChoosePatientInfo()
        {
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
			// ��ʼ��
			this.Init();
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		public void Init() {
			try {
				//ȡϵͳ����
				FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();
				string sysDate = dataBase.GetSysDate();
				this.dtpBeginDate.Value = DateTime.Parse(sysDate + " 00:00:00");	//��ʼ����
				this.dtpEndDate.Value   = DateTime.Parse(sysDate + " 23:59:59");	//��������

				if(this.myIsOneDate) {
					//�û�ֻ����ѡ��һ�����ڵ�ʱ��
					this.lblBeginDate.Text  = "����:";
					//����ʾ��ֹ����
					this.lblEndDate.Visible = false; 
					this.dtpEndDate.Visible = false;
				}
				else {
					//�û�ֻ����ѡ����ʼ���ں���ֹ���ڵ�ʱ��
					this.lblBeginDate.Text  = "��ʼ����:";
					//����ʾ��ֹ����
					this.lblEndDate.Visible = true; 
					this.dtpEndDate.Visible = true;
				}
			}
			catch{}
		}


		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmChoosePatientInfo));
            this.dtpBeginDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.dtpEndDate = new FS.FrameWork.WinForms.Controls.NeuDateTimePicker();
            this.lblBeginDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.lblEndDate = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.pictureBox1 = new FS.FrameWork.WinForms.Controls.NeuPictureBox();
            this.label3 = new FS.FrameWork.WinForms.Controls.NeuLabel();
            this.btnOK = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.btnExit = new FS.FrameWork.WinForms.Controls.NeuButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbcardno = new System.Windows.Forms.TextBox();
            this.ckquery = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtpBeginDate
            // 
            this.dtpBeginDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpBeginDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpBeginDate.IsEnter2Tab = false;
            this.dtpBeginDate.Location = new System.Drawing.Point(117, 50);
            this.dtpBeginDate.Name = "dtpBeginDate";
            this.dtpBeginDate.Size = new System.Drawing.Size(151, 21);
            this.dtpBeginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpBeginDate.TabIndex = 0;
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEndDate.IsEnter2Tab = false;
            this.dtpEndDate.Location = new System.Drawing.Point(117, 78);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(151, 21);
            this.dtpEndDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.dtpEndDate.TabIndex = 0;
            // 
            // lblBeginDate
            // 
            this.lblBeginDate.Location = new System.Drawing.Point(54, 52);
            this.lblBeginDate.Name = "lblBeginDate";
            this.lblBeginDate.Size = new System.Drawing.Size(60, 17);
            this.lblBeginDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblBeginDate.TabIndex = 2;
            this.lblBeginDate.Text = "��ʼʱ��:";
            this.lblBeginDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblEndDate
            // 
            this.lblEndDate.Location = new System.Drawing.Point(54, 80);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(60, 17);
            this.lblEndDate.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.lblEndDate.TabIndex = 2;
            this.lblEndDate.Text = "��ֹʱ��:";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(54, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.label3.TabIndex = 2;
            this.label3.Text = "��ѡ������:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(50, 148);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(79, 23);
            this.btnOK.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "ȷ��(&O)";
            this.btnOK.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(169, 147);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(79, 23);
            this.btnExit.Style = FS.FrameWork.WinForms.Controls.StyleType.Fixed3D;
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "�˳�(&X)";
            this.btnExit.Type = FS.FrameWork.WinForms.Controls.General.ButtonType.None;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(66, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "������:";
            // 
            // tbcardno
            // 
            this.tbcardno.Location = new System.Drawing.Point(117, 110);
            this.tbcardno.Name = "tbcardno";
            this.tbcardno.Size = new System.Drawing.Size(151, 21);
            this.tbcardno.TabIndex = 6;
            // 
            // ckquery
            // 
            this.ckquery.AutoSize = true;
            this.ckquery.Location = new System.Drawing.Point(208, 21);
            this.ckquery.Name = "ckquery";
            this.ckquery.Size = new System.Drawing.Size(60, 16);
            this.ckquery.TabIndex = 7;
            this.ckquery.Text = "��Ʊ��";
            this.ckquery.UseVisualStyleBackColor = true;
            // 
            // frmChoosePatientInfo
            // 
            this.AcceptButton = this.btnOK;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(333, 197);
            this.Controls.Add(this.ckquery);
            this.Controls.Add(this.tbcardno);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblBeginDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpBeginDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "frmChoosePatientInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "������ѯ";
            this.Load += new System.EventHandler(this.frmChoosePatientInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void btnExit_Click(object sender, System.EventArgs e) {
			this.FindForm().Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e) {
			DateTime dateBegin = this.dtpBeginDate.Value;
			DateTime dateEnd   = this.dtpEndDate.Value;
            string cardno = this.tbcardno.Text;

			if(dateEnd.CompareTo(dateBegin) < 0) {
				MessageBox.Show("��ֹʱ����������ʼʱ�䣡","��ʾ");
				return;
			}

			this.DialogResult = DialogResult.OK;

		}

	
        private void frmChoosePatientInfo_Load(object sender, EventArgs e)
        {

        }

	}
}

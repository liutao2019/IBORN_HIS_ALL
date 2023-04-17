using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.QC
{
	/// <summary>
	/// frmQCRuleManagement ��ժҪ˵����
	/// </summary>
	public class frmQCRuleManagement : System.Windows.Forms.Form
	{
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmQCRuleManagement()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// frmQCRuleManagement
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(424, 469);
			this.Name = "frmQCRuleManagement";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "�ʿع������";
			this.Load += new System.EventHandler(this.frmQCRuleManagement_Load);

		}
		#endregion

		private void frmQCRuleManagement_Load(object sender, System.EventArgs e)
		{
			ucQCRuleInput uc = new ucQCRuleInput();
			uc.Visible = true;
			this.Controls.Add(uc);
			uc.Dock = DockStyle.Fill;
		}
	}
}

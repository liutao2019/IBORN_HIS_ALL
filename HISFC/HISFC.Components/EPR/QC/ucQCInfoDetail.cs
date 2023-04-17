using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.QC
{
	/// <summary>
	/// ucQCInfoDetail ��ժҪ˵����
	/// </summary>
	public class ucQCInfoDetail : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox lstConditions;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.ListBox lstActions;
		private System.Windows.Forms.Label lblTip;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucQCInfoDetail()
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
			this.lstConditions = new System.Windows.Forms.ListBox();
			this.lblName = new System.Windows.Forms.Label();
			this.lstActions = new System.Windows.Forms.ListBox();
			this.button1 = new System.Windows.Forms.Button();
			this.lblTip = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lstConditions
			// 
			this.lstConditions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstConditions.ItemHeight = 12;
			this.lstConditions.Location = new System.Drawing.Point(8, 48);
			this.lstConditions.Name = "lstConditions";
			this.lstConditions.Size = new System.Drawing.Size(328, 134);
			this.lstConditions.TabIndex = 0;
			this.lstConditions.SelectedIndexChanged += new System.EventHandler(this.lstConditions_SelectedIndexChanged);
			// 
			// lblName
			// 
			this.lblName.ForeColor = System.Drawing.Color.Blue;
			this.lblName.Location = new System.Drawing.Point(8, 16);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(240, 24);
			this.lblName.TabIndex = 1;
			this.lblName.Text = "label1";
			// 
			// lstActions
			// 
			this.lstActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstActions.ItemHeight = 12;
			this.lstActions.Location = new System.Drawing.Point(8, 192);
			this.lstActions.Name = "lstActions";
			this.lstActions.Size = new System.Drawing.Size(328, 62);
			this.lstActions.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Location = new System.Drawing.Point(256, 16);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(80, 24);
			this.button1.TabIndex = 3;
			this.button1.Text = "�˳�";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// lblTip
			// 
			this.lblTip.ForeColor = System.Drawing.Color.Red;
			this.lblTip.Location = new System.Drawing.Point(8, 256);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new System.Drawing.Size(328, 16);
			this.lblTip.TabIndex = 4;
			// 
			// ucQCInfoDetail
			// 
			this.Controls.Add(this.lblTip);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lstActions);
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.lstConditions);
			this.Name = "ucQCInfoDetail";
			this.Size = new System.Drawing.Size(352, 280);
			this.Load += new System.EventHandler(this.ucQCInfoDetail_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.FindForm().Close();
		}

		private void ucQCInfoDetail_Load(object sender, System.EventArgs e)
		{
		
		}

		private void lstConditions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			try
			{
				this.lblTip.Text = ((FS.HISFC.Models.EPR.QCCondition)lstConditions.SelectedItem).Memo;
				t.SetToolTip(this.lblTip,this.lblTip.Text);
			}
			catch{}
		}
		ToolTip t = new ToolTip();
		/// <summary>
		/// ��ǰ����
		/// </summary>
		public FS.HISFC.Models.EPR.QCConditions QCConditions
		{
			set
			{
				foreach(FS.HISFC.Models.EPR.QCCondition condition in value.AlConditions)
				{
					lstConditions.Items.Add(condition);
				}
				foreach(FS.FrameWork.Models.NeuObject action in value.Acion.AlMessage)
				{
					lstActions.Items.Add(action);
				}
				this.lblName.Text  = value.Name;
				t.SetToolTip(this.lblName,this.lblName.Text );
			}
		}
	}
}

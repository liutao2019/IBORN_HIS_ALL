using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.Query
{
	/// <summary>
	/// ucQueryCondition ��ժҪ˵����
	/// </summary>
	public class ucQueryCondition : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucQueryCondition()
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(568, 184);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// ucQueryCondition
			// 
			this.Controls.Add(this.groupBox1);
			this.Name = "ucQueryCondition";
			this.Size = new System.Drawing.Size(568, 184);
			this.ResumeLayout(false);

		}
		#endregion
	}
}

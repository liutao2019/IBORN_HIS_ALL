using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.Query
{
	/// <summary>
	/// ucQueryFileStatus ��ժҪ˵����
	/// </summary>
	public class ucQueryFileStatus : System.Windows.Forms.UserControl
	{
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucQueryFileStatus()
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
			this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
			this.SuspendLayout();
			// 
			// fpSpread1
			// 
			this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fpSpread1.Location = new System.Drawing.Point(0, 0);
			this.fpSpread1.Name = "fpSpread1";
			this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1});
			this.fpSpread1.Size = new System.Drawing.Size(448, 416);
			this.fpSpread1.TabIndex = 0;
			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.ColumnCount = 7;
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "�ļ����";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "סԺ��ˮ��";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "��������";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "��������";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "����";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Text = "��Ժ״̬";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Text = "����״̬";
			this.fpSpread1_Sheet1.Columns.Get(0).Label = "�ļ����";
			this.fpSpread1_Sheet1.Columns.Get(0).Width = 0F;
			this.fpSpread1_Sheet1.Columns.Get(1).Label = "סԺ��ˮ��";
			this.fpSpread1_Sheet1.Columns.Get(1).Width = 92F;
			this.fpSpread1_Sheet1.Columns.Get(2).Label = "��������";
			this.fpSpread1_Sheet1.Columns.Get(2).Width = 96F;
			this.fpSpread1_Sheet1.Columns.Get(3).Label = "��������";
			this.fpSpread1_Sheet1.Columns.Get(3).Width = 151F;
			this.fpSpread1_Sheet1.Columns.Get(4).Label = "����";
			this.fpSpread1_Sheet1.Columns.Get(4).Width = 120F;
			this.fpSpread1_Sheet1.Columns.Get(5).Label = "��Ժ״̬";
			this.fpSpread1_Sheet1.Columns.Get(5).Width = 94F;
			this.fpSpread1_Sheet1.Columns.Get(6).Label = "����״̬";
			this.fpSpread1_Sheet1.Columns.Get(6).Width = 138F;
			this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// ucQueryFileStatus
			// 
			this.Controls.Add(this.fpSpread1);
			this.Name = "ucQueryFileStatus";
			this.Size = new System.Drawing.Size(448, 416);
			this.Load += new System.EventHandler(this.ucQueryFileStatus_Load);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ucQueryFileStatus_Load(object sender, System.EventArgs e)
		{
			
		}

		/// <summary>
		/// ��ѯ
		/// </summary>
		public void Query()
		{
			
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{
	/// <summary>
	/// ucSignManager ��ժҪ˵����
	/// </summary>
	public class ucSignManager : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFilter;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucSignManager()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.panel1.Controls.Add(this.fpSpread1);
			this.panel1.Controls.Add(this.txtFilter);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(520, 456);
			this.panel1.TabIndex = 0;
			// 
			// fpSpread1
			// 
			this.fpSpread1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.fpSpread1.Location = new System.Drawing.Point(0, 44);
			this.fpSpread1.Name = "fpSpread1";
			this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1});
			this.fpSpread1.Size = new System.Drawing.Size(520, 408);
			this.fpSpread1.TabIndex = 2;
			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// txtFilter
			// 
			this.txtFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtFilter.Location = new System.Drawing.Point(141, 14);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(138, 21);
			this.txtFilter.TabIndex = 1;
			this.txtFilter.Text = "";
			this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(21, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(107, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "��Ա��������ң�";
			// 
			// ucSignManager
			// 
			this.Controls.Add(this.panel1);
			this.Name = "ucSignManager";
			this.Size = new System.Drawing.Size(520, 456);
			this.Load += new System.EventHandler(this.ucPermissionManager_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ucPermissionManager_Load(object sender, System.EventArgs e)
		{
			this.init();	
			Retrieve();

		}


		private DataSet ds = new DataSet();
		private DataView dv ;
		/// <summary>
		/// ��ʼ���ؼ�
		/// </summary>
		private void init()
		{
			//��ʼ��DataTable
			DataTable table = new DataTable("Table");

			DataColumn dataColumn1 = new DataColumn("Ա������");
			dataColumn1.DataType = typeof(System.String);
			table.Columns.Add(dataColumn1);

			DataColumn dataColumn2 = new DataColumn("����");
			dataColumn2.DataType = typeof(System.String);
			table.Columns.Add(dataColumn2);
			
			DataColumn dataColumn3 = new DataColumn("ǩ��");
			dataColumn3.DataType = typeof(System.String);
			table.Columns.Add(dataColumn3);
			
			DataColumn dataColumn4 = new DataColumn("�����ַ���");
			dataColumn4.DataType = typeof(System.String);
			table.Columns.Add(dataColumn4);

			DataColumn dataColumn9 = new DataColumn("����Ա");
			dataColumn9.DataType = typeof(System.String);
			table.Columns.Add(dataColumn9);

			DataColumn dataColumn10 = new DataColumn("��������");
			dataColumn10.DataType = typeof(System.DateTime);
			table.Columns.Add(dataColumn10);

			//��ʼ��dataSet
			ds.Tables.Add(table);
			
			
			dv = new DataView(ds.Tables[0]);
			this.fpSpread1.Sheets[0].DataSource = dv;
			this.fpSpread1.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
			this._SetFP();
		}
		
		protected void _SetFP()
		{
			this.fpSpread1.Sheets[0].Columns[0].Width = 80;
			this.fpSpread1.Sheets[0].Columns[1].Width = 100;
			this.fpSpread1.Sheets[0].Columns[2].Width = 100;
			this.fpSpread1.Sheets[0].Columns[3].Width = 100;
			this.fpSpread1.Sheets[0].Columns[4].Width = 100;
			this.fpSpread1.Sheets[0].Columns[5].Width = 100;
//			this.fpSpread1.Sheets[0].Columns[6].Width = 100;
//			this.fpSpread1.Sheets[0].Columns[7].Width = 100;
//			this.fpSpread1.Sheets[0].Columns[8].Width = 60;
//			this.fpSpread1.Sheets[0].Columns[9].Width = 100;
		}
		#region IToolBar ��Ա

		public ToolBarButton PreButton
		{
			get
			{
				// TODO:  ��� ucSignManager.PreButton getter ʵ��
				return null;
			}
		}

		public int Search()
		{
			// TODO:  ��� ucSignManager.Search ʵ��
			return 0;
		}

		public ToolBarButton SaveButton
		{
			get
			{
				// TODO:  ��� ucSignManager.SaveButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton SearchButton
		{
			get
			{
				// TODO:  ��� ucSignManager.SearchButton getter ʵ��
				return null;
			}
		}
		/// <summary>
		/// �޸�
		/// </summary>
		/// <returns></returns>
		public int Auditing()
		{
			// TODO:  ��� ucSignManager.Auditing ʵ��
			if(this.fpSpread1.Sheets[0].ActiveRowIndex<0) 
			{
				MessageBox.Show("��ѡ��Ҫ�޸ĵ��У�");
				return 0;
			}
			int i = this.fpSpread1.Sheets[0].ActiveRowIndex;
			FS.FrameWork.Models.NeuObject obj =new FS.FrameWork.Models.NeuObject();
			obj.ID = this.fpSpread1.Sheets[0].Cells[i,0].Text;
			obj.Name = this.fpSpread1.Sheets[0].Cells[i,1].Text;
			obj.Memo = this.fpSpread1.Sheets[0].Cells[i,2].Text;
			obj.User01  = this.fpSpread1.Sheets[0].Cells[i,3].Text;
			obj.User02  = this.fpSpread1.Sheets[0].Cells[i,4].Text;
			obj.User03  = this.fpSpread1.Sheets[0].Cells[i,5].Text;
			//byte[] img =
//			System.Drawing.Image image ;
//			System.IO.Stream writer;
//			image.Save(writer);
//			System.Windows.Forms.PictureBox p;
//			//writer.Write(buffer,0,buffer.);
//			System.IO.StreamReader reader;
			//reader.Read(buffer,0,length);


            byte[] byteimg = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSignBackGround(obj.ID);
			ucSignInput uc = new ucSignInput(obj, byteimg);
			FS.FrameWork.WinForms.Classes.Function.PopForm.Text ="�޸�";
			if(FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc)==DialogResult.OK)
			{
				this.Retrieve();
			}
			return 0;
		}
		/// <summary>
		/// ɾ��
		/// </summary>
		/// <returns></returns>
		public int Del()
		{
			// TODO:  ��� ucSignManager.Del ʵ��
			if(this.fpSpread1.Sheets[0].ActiveRowIndex<0) 
			{
				MessageBox.Show("��ѡ��Ҫ�޸ĵ��У�");
				return 0;
			}
			int i = this.fpSpread1.Sheets[0].ActiveRowIndex;
			FS.HISFC.BizProcess.Factory.Function.BeginTransaction();

            if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DeleteSign(this.fpSpread1.Sheets[0].Cells[i, 0].Text) == -1)
			{
				FS.HISFC.BizProcess.Factory.Function.RollBack();
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return -1;
			}
            FS.HISFC.BizProcess.Factory.Function.Commit();
			MessageBox.Show("ɾ���ɹ���");
			this.fpSpread1.Sheets[0].Rows.Remove(i,1);
			return 0;
		}

		public ToolBarButton AddButton
		{
			get
			{
				// TODO:  ��� ucSignManager.AddButton getter ʵ��
				return null;
			}
		}

		public int Print()
		{
			// TODO:  ��� ucSignManager.Print ʵ��
			return 0;
		}

		public int Pre()
		{
			// TODO:  ��� ucSignManager.Pre ʵ��
			this.fpSpread1.Sheets[0].ActiveRowIndex--;
			this.fpSpread1.Sheets[0].AddSelection(this.fpSpread1.Sheets[0].ActiveRowIndex,0,1,1);
			return 0;
		}

		public ToolBarButton NextButton
		{
			get
			{
				// TODO:  ��� ucSignManager.NextButton getter ʵ��
				return null;
			}
		}

		public int Help()
		{
			// TODO:  ��� ucSignManager.Help ʵ��
			return 0;
		}

		public int Next()
		{
			// TODO:  ��� ucSignManager.Next ʵ��
			this.fpSpread1.Sheets[0].ActiveRowIndex++;
			this.fpSpread1.Sheets[0].AddSelection(this.fpSpread1.Sheets[0].ActiveRowIndex,0,1,1);
			return 0;
		}
		/// <summary>
		/// ˢ���б�
		/// </summary>
		/// <returns></returns>
		public int Retrieve()
		{
			// TODO:  ��� ucSignManager.Retrieve ʵ��
			ds.Tables[0].Rows.Clear();
            ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetSignList();
			foreach(FS.FrameWork.Models.NeuObject obj in al)
			{
				DataRow dr = ds.Tables[0].NewRow();
				
				dr["Ա������"] = obj.ID;
				dr["����"] = obj.Name;
				dr["ǩ��"] = obj.Memo;
				dr["�����ַ���"] = obj.User01;
				dr["����Ա"] = obj.User02;
				dr["��������"] = DateTime.Parse(obj.User03);
				ds.Tables[0].Rows.Add(dr);
			}		
			this._SetFP();
			return 0;
		}
		/// <summary>
		/// �������ԱȨ��
		/// </summary>
		/// <returns></returns>
		public int Add()
		{
			// TODO:  ��� ucSignManager.Add ʵ��
            //FS.HISFC.Models.Medical.Permission obj =new FS.HISFC.Models.Medical.Permission();
            //ucSignInput uc = new ucSignInput(obj, null);
            //FS.FrameWork.WinForms.Classes.Function.PopForm.Text ="���";
            //if(FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc)==DialogResult.OK)
            //{
            //    this.Retrieve();
            //}
			return 0;
		}

		public ToolBarButton RetrieveButton
		{
			get
			{
				// TODO:  ��� ucSignManager.RetrieveButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton DelButton
		{
			get
			{
				// TODO:  ��� ucSignManager.DelButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton PrintButton
		{
			get
			{
				// TODO:  ��� ucSignManager.PrintButton getter ʵ��
				return null;
			}
		}
		/// <summary>
		/// �˳�
		/// </summary>
		/// <returns></returns>
		public int Exit()
		{
			// TODO:  ��� ucSignManager.Exit ʵ��
			this.FindForm().Close();
			return 0;
		}

		public int Save()
		{
			// TODO:  ��� ucSignManager.Save ʵ��
			return 0;
		}

		private void txtFilter_TextChanged(object sender, System.EventArgs e)
		{
			dv.RowFilter = "Ա������ like '%" +this.txtFilter.Text.Trim()+"%'";
			this._SetFP();
		}

		public ToolBarButton AuditingButton
		{
			get
			{
				// TODO:  ��� ucSignManager.AuditingButton getter ʵ��
				return null;
			}
		}

		#endregion
	}
}

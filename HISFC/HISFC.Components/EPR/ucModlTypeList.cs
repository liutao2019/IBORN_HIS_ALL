using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR
{
	/// <summary>
	/// ucMoudulTypeList ��ժҪ˵����
	/// </summary>
	public class ucModTypeList : System.Windows.Forms.UserControl
	{
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucModTypeList()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
			init();
			this.Retrieve();
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
			this.fpSpread1.Size = new System.Drawing.Size(424, 352);
			this.fpSpread1.TabIndex = 0;
			this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
			this.fpSpread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.Single;
			this.fpSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// ucModTypeList
			// 
			this.Controls.Add(this.fpSpread1);
			this.Name = "ucModTypeList";
			this.Size = new System.Drawing.Size(424, 352);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		DataSet dataSet = new DataSet();
		protected void init()
		{
			
			DataTable table = new DataTable("Table1");
			
			DataColumn column1 = new DataColumn("����");
			column1.DataType = typeof(System.String);
			table.Columns.Add(column1);

			DataColumn column2 = new DataColumn("����");
			column2.DataType =typeof(System.String);
			table.Columns.Add(column2);

			DataColumn column3 = new DataColumn("����");
			column3.DataType = typeof(System.String);
			table.Columns.Add(column3);

			DataColumn column4 = new DataColumn("IP");
			column4.DataType = typeof(System.String);
			table.Columns.Add(column4);
			
			DataColumn column5 = new DataColumn("Http");
			column5.DataType =  typeof(System.String);
			table.Columns.Add(column5);

			DataColumn column6 = new DataColumn("����·��");
			column6.DataType =  typeof(System.String);
			table.Columns.Add(column6);

			DataColumn column7= new DataColumn("ģ��·��");
			column7.DataType =  typeof(System.String);
			table.Columns.Add(column7);

			DataColumn column8 = new DataColumn("�Ƿ����ݿ�");
			column8.DataType =  typeof(System.Boolean );
			table.Columns.Add(column8);

			DataColumn column9 = new DataColumn("����Ա");
			column9.DataType =  typeof(System.String);
			table.Columns.Add(column9);

			DataColumn column10 = new DataColumn("��������");
			column10.DataType =  typeof(System.String);
			table.Columns.Add(column10);

			dataSet.Tables.Add(table);

		}
		#region IToolBar ��Ա

		public ToolBarButton PreButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.PreButton getter ʵ��
				return null;
			}
		}

		public int Search()
		{
			// TODO:  ��� ucMoudulTypeList.Search ʵ��
			return 0;
		}

		public ToolBarButton SaveButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.SaveButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton SearchButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.SearchButton getter ʵ��
				return null;
			}
		}

		public int Auditing()
		{
			// TODO:  ��� ucMoudulTypeList.Auditing ʵ��
			return 0;
		}

		public int Del()
		{
			// TODO:  ��� ucMoudulTypeList.Del ʵ��
			return 0;
		}

		public ToolBarButton AddButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.AddButton getter ʵ��
				return null;
			}
		}

		public int Print()
		{
			// TODO:  ��� ucMoudulTypeList.Print ʵ��
			return 0;
		}

		public int Pre()
		{
			// TODO:  ��� ucMoudulTypeList.Pre ʵ��
			return 0;
		}

		public ToolBarButton NextButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.NextButton getter ʵ��
				return null;
			}
		}

		public int Help()
		{
			// TODO:  ��� ucMoudulTypeList.Help ʵ��
			return 0;
		}

		public int Next()
		{
			// TODO:  ��� ucMoudulTypeList.Next ʵ��
			return 0;
		}
		ArrayList al= null;
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <returns></returns>
		public int Retrieve()
		{
            
			// TODO:  ��� ucMoudulTypeList.Retrieve ʵ��
            al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetParamList();
			dataSet.Tables[0].Rows.Clear();
			if(al == null) 
			{
				MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return -1;
			}
			foreach(FS.HISFC.Models.File.DataFileParam obj in al)
			{
				DataRow row = dataSet.Tables[0].NewRow();
				row[0] = (obj.Type);
				row[1] = obj.Memo;
				row[2] = obj.ID;
				row[3]=obj.IP;
				row[4]=obj.Http;
				row[5]=obj.Folders;
				row[6]=obj.ModualFolders;
				row[7] =obj.IsInDB;
				row[8]=obj.User01;
				row[9]=obj.User02;
				dataSet.Tables[0].Rows.Add(row);
			}
			this.fpSpread1.Sheets[0].DataSource= dataSet;
			this.fpSpread1.Sheets[0].Columns[0].Width =40;
			this.fpSpread1.Sheets[0].Columns[1].Width =60;
			this.fpSpread1.Sheets[0].Columns[7].Width =50;
			this.fpSpread1.Sheets[0].Columns[5].Width =100;
			this.fpSpread1.Sheets[0].Columns[6].Width =100;
			return 0;
		}

		public int Add()
		{
			// TODO:  ��� ucMoudulTypeList.Add ʵ��
			try
			{
				ucModTypeSetting u = new ucModTypeSetting();
				FS.HISFC.Models.File.DataFileParam o=new FS.HISFC.Models.File.DataFileParam();
				u.DataFileParam = o;
				FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);
				this.Retrieve();
			}
			catch{}
			return 0;
		}

		public ToolBarButton RetrieveButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.RetrieveButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton DelButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.DelButton getter ʵ��
				return null;
			}
		}

		public ToolBarButton PrintButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.PrintButton getter ʵ��
				return null;
			}
		}

		public int Exit()
		{
			// TODO:  ��� ucMoudulTypeList.Exit ʵ��
			this.ParentForm.Close();
			return 0;
		}

		public int Save()
		{
			// TODO:  ��� ucMoudulTypeList.Save ʵ��
			return 0;
		}

		private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			try
			{
				ucModTypeSetting u = new ucModTypeSetting();
				u.DataFileParam = al[this.fpSpread1_Sheet1.ActiveRowIndex] as FS.HISFC.Models.File.DataFileParam;
				FS.FrameWork.WinForms.Classes.Function.PopShowControl(u);
				this.Retrieve();
			}
			catch{}
		}

		public ToolBarButton AuditingButton
		{
			get
			{
				// TODO:  ��� ucMoudulTypeList.AuditingButton getter ʵ��
				return null;
			}
		}

		#endregion
	}
}

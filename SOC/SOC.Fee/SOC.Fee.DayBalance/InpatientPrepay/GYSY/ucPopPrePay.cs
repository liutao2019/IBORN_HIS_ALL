using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
namespace SOC.Fee.DayBalance.InpatientPrepay.GYSY
{
	/// <summary>
	/// ucPopPrePay ��ժҪ˵����
	/// </summary>
	public class ucPopPrePay : System.Windows.Forms.UserControl
	{
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ucPopPrePay()
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
			FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
			FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
			FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
			this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
			this.SuspendLayout();
			// 
			// fpSpread1
			// 
			this.fpSpread1.ClipboardOptions = FarPoint.Win.Spread.ClipboardOptions.NoHeaders;
			this.fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fpSpread1.Location = new System.Drawing.Point(0, 0);
			this.fpSpread1.Name = "fpSpread1";
			this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1});
			this.fpSpread1.Size = new System.Drawing.Size(464, 472);
			this.fpSpread1.TabIndex = 1;
			this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellDoubleClick);
			this.fpSpread1.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread1_CellClick);
			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.ColumnCount = 3;
			this.fpSpread1_Sheet1.RowCount = 1;
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "�� ��";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "��ʼʱ��";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "����ʱ��";
			this.fpSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
			this.fpSpread1_Sheet1.Columns.Get(0).Label = "�� ��";
			this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType2;
			this.fpSpread1_Sheet1.Columns.Get(1).Label = "��ʼʱ��";
			this.fpSpread1_Sheet1.Columns.Get(1).Width = 168F;
			this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType3;
			this.fpSpread1_Sheet1.Columns.Get(2).Label = "����ʱ��";
			this.fpSpread1_Sheet1.Columns.Get(2).Width = 168F;
			this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// ucPopPrePay
			// 
			this.Controls.Add(this.fpSpread1);
			this.Name = "ucPopPrePay";
			this.Size = new System.Drawing.Size(464, 472);
			this.Load += new System.EventHandler(this.ucPopPrePay_Load);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private string strNo = "";
		public string NO
		{
			get{return strNo;}
			set{strNo = value;}
		}
		private string strBegin = "";
		public string Begin
		{
			get{return strBegin;}
			set{strBegin = value;}
		}
	
		private string strEnd = "";
		public string End
		{
			get{return strEnd;}
			set{strEnd = value;}
		}

		public int inputType = 0;

		public int InputType
		{
			get{return inputType;}
			set{inputType = value;}
		}

		private string retBegin = "";
		private string retEnd = "";
		public string rBegin
		{
			get{return retBegin;}
			set{retBegin = value;}
		}
		public string rEnd
		{
			get{return retEnd;}
			set{retEnd = value;}
		}
		private FS.FrameWork.Models.NeuObject  rObject= new  FS.FrameWork.Models.NeuObject();
		public FS.FrameWork.Models.NeuObject Object
		{
			get{return rObject;}
			set{rObject = value;}
		}

		private void BindFp()
		{
			FarPoint.Win.Spread.Model.ISheetDataModel model;
			model = new FarPoint.Win.Spread.Model.DefaultSheetDataModel(fpSpread1.Sheets[0].Rows.Count,15);
			int im=3;
			this.fpSpread1_Sheet1.OperationMode = (OperationMode)im;
			model.SetValue(fpSpread1.Sheets[0].Rows.Count, 11, "MultiOption:");
			model.SetValue(fpSpread1.Sheets[0].Rows.Count, 11, 0);
			string strOper = this.oCReport.Operator.ID;
			FarPoint.Win.Spread.CellType.TextCellType textCellType1 = new FarPoint.Win.Spread.CellType.TextCellType();
			FarPoint.Win.Spread.CellType.TextCellType textCellType2 = new FarPoint.Win.Spread.CellType.TextCellType();
			FarPoint.Win.Spread.CellType.TextCellType textCellType3 = new FarPoint.Win.Spread.CellType.TextCellType();
			if(this.InputType==0)
				this.fpSpread1_Sheet1.DataSource = oCReport.GetPrepayStatListByDate(strOper,this.Begin,this.End);
			if(this.InputType==1)
				this.fpSpread1_Sheet1.DataSource = oCReport.GetPrepayItemListByDate(strOper,this.Begin,this.End);

			this.fpSpread1_Sheet1.Columns.Get(0).CellType = textCellType1;
			this.fpSpread1_Sheet1.Columns.Get(0).Label = "�� ��";
			this.fpSpread1_Sheet1.Columns.Get(1).CellType = textCellType2;
			this.fpSpread1_Sheet1.Columns.Get(1).Label = "��ʼʱ��";
		
			this.fpSpread1_Sheet1.Columns.Get(2).CellType = textCellType3;
			this.fpSpread1_Sheet1.Columns.Get(2).Label = "����ʱ��";
			this.fpSpread1_Sheet1.Columns.Get(2).Width = 160F;
				this.fpSpread1_Sheet1.Columns.Get(1).Width = 160F;
			
		}


        private SOC.Fee.DayBalance.Manager.PrepayDayBalance oCReport = new  SOC.Fee.DayBalance.Manager.PrepayDayBalance();

		private void ucPopPrePay_Load(object sender, System.EventArgs e)
		{
			this.BindFp();
			
		}

		private void fpSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			
			
		}

		private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			int iRow = this.fpSpread1_Sheet1.ActiveRowIndex;
			this.NO = this.fpSpread1_Sheet1.Cells[iRow,0].Text.Trim();
			this.rBegin = this.fpSpread1_Sheet1.Cells[iRow,1].Text.Trim();
			this.rEnd = this.fpSpread1_Sheet1.Cells[iRow,2].Text.Trim();
			this.rObject.ID = this.NO;
			this.rObject.User01 = this.rBegin;
			this.rObject.User02 = this.rEnd;
			this.ParentForm.Close();
		}


	}
}

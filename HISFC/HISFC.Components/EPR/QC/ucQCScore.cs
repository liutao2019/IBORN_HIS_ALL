using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace FS.HISFC.Components.EPR.QC
{
	/// <summary>
	/// ucQCScore ��ժҪ˵����
	/// </summary>
	public class ucQCScore : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private FarPoint.Win.Spread.FpSpread fpSpread1;
		private FarPoint.Win.Spread.SheetView fpSpread1_Sheet1;
		private FarPoint.Win.Spread.FpSpread fpSpread2;
		private FarPoint.Win.Spread.SheetView fpSpread2_Sheet1;
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		/// <summary>
		/// �������ֱ仯
		/// </summary>
		public event System.EventHandler OnScoreChanged;

		public ucQCScore()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ucQCScore));
			FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType1 = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
			this.panel1 = new System.Windows.Forms.Panel();
			this.fpSpread1 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread1_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.fpSpread2 = new FarPoint.Win.Spread.FpSpread();
			this.fpSpread2_Sheet1 = new FarPoint.Win.Spread.SheetView();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.fpSpread1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(632, 568);
			this.panel1.TabIndex = 0;
			// 
			// fpSpread1
			// 
			this.fpSpread1.Location = new System.Drawing.Point(0, 488);
			this.fpSpread1.Name = "fpSpread1";
			this.fpSpread1.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread1_Sheet1});
			this.fpSpread1.Size = new System.Drawing.Size(632, 472);
			this.fpSpread1.TabIndex = 0;
			// 
			// fpSpread1_Sheet1
			// 
			this.fpSpread1_Sheet1.Reset();
			this.fpSpread1_Sheet1.ColumnCount = 5;
			this.fpSpread1_Sheet1.RowCount = 0;
//			this.fpSpread1_Sheet1.AutoSortColumns = ((FarPoint.Win.Spread.SheetView.SaveAutoSortColumns)(resources.GetObject("fpSpread1_Sheet1.AutoSortColumns")));
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "����";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "���";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "����";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "�۷�";
			this.fpSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "��ע";
			this.fpSpread1_Sheet1.Columns.Get(0).Label = "����";
			this.fpSpread1_Sheet1.Columns.Get(0).Width = 0F;
			this.fpSpread1_Sheet1.Columns.Get(1).AllowAutoSort = true;
			this.fpSpread1_Sheet1.Columns.Get(1).Label = "���";
			this.fpSpread1_Sheet1.Columns.Get(1).Width = 121F;
			this.fpSpread1_Sheet1.Columns.Get(2).AllowAutoSort = true;
			this.fpSpread1_Sheet1.Columns.Get(2).Label = "����";
			this.fpSpread1_Sheet1.Columns.Get(2).Width = 201F;
			this.fpSpread1_Sheet1.Columns.Get(3).AllowAutoSort = true;
			this.fpSpread1_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread1_Sheet1.Columns.Get(3).Label = "�۷�";
			this.fpSpread1_Sheet1.Columns.Get(3).Width = 120F;
			this.fpSpread1_Sheet1.Columns.Get(4).Label = "��ע";
			this.fpSpread1_Sheet1.Columns.Get(4).Width = 112F;
			this.fpSpread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
			this.fpSpread1_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread1_Sheet1.SheetName = "Sheet1";
			// 
			// fpSpread2
			// 
			this.fpSpread2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.fpSpread2.Location = new System.Drawing.Point(0, 0);
			this.fpSpread2.Name = "fpSpread2";
			this.fpSpread2.Sheets.AddRange(new FarPoint.Win.Spread.SheetView[] {
																				   this.fpSpread2_Sheet1});
			this.fpSpread2.Size = new System.Drawing.Size(632, 568);
			this.fpSpread2.TabIndex = 0;
			this.fpSpread2.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(this.fpSpread2_CellDoubleClick);
			this.fpSpread2.TextChanged += new System.EventHandler(this.fpSpread2_TextChanged);
			// 
			// fpSpread2_Sheet1
			// 
			this.fpSpread2_Sheet1.Reset();
			this.fpSpread2_Sheet1.ColumnCount = 6;
			this.fpSpread2_Sheet1.RowCount = 1;
			this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Text = "����";
			this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Text = "���";
			this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Text = "����";
			this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Text = "����";
			this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Text = "����ܷ�";
			this.fpSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Text = "ѡ��";
			this.fpSpread2_Sheet1.Columns.Get(0).Label = "����";
			this.fpSpread2_Sheet1.Columns.Get(0).Width = 65F;
			this.fpSpread2_Sheet1.Columns.Get(1).Label = "���";
			this.fpSpread2_Sheet1.Columns.Get(1).Width = 80F;
			this.fpSpread2_Sheet1.Columns.Get(2).Label = "����";
			this.fpSpread2_Sheet1.Columns.Get(2).Width = 189F;
			this.fpSpread2_Sheet1.Columns.Get(3).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread2_Sheet1.Columns.Get(3).Label = "����";
			this.fpSpread2_Sheet1.Columns.Get(3).Width = 90F;
			this.fpSpread2_Sheet1.Columns.Get(4).HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
			this.fpSpread2_Sheet1.Columns.Get(4).Label = "����ܷ�";
			this.fpSpread2_Sheet1.Columns.Get(4).Width = 90F;
			this.fpSpread2_Sheet1.Columns.Get(5).CellType = checkBoxCellType1;
			this.fpSpread2_Sheet1.Columns.Get(5).Label = "ѡ��";
			this.fpSpread2_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;
			this.fpSpread2_Sheet1.RowHeader.Columns.Default.Resizable = false;
			this.fpSpread2_Sheet1.SheetName = "Sheet1";
			this.fpSpread2_Sheet1.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(this.fpSpread2_Sheet1_CellChanged);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.fpSpread2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(632, 568);
			this.panel2.TabIndex = 2;
			// 
			// ucQCScore
			// 
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "ucQCScore";
			this.Size = new System.Drawing.Size(632, 568);
			this.Load += new System.EventHandler(this.ucQCScore_Load);
			this.panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread1_Sheet1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.fpSpread2_Sheet1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ucQCScore_Load(object sender, System.EventArgs e)
		{
			try
			{
				//������������б�
				ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCScoreSetList();
				if(al == null)
				{
					MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
					return;
				}
				this.fpSpread2_Sheet1.RowCount = 0;
				this.fpSpread2_Sheet1.RowCount = al.Count;
				int i = 0;
				foreach(FS.HISFC.Models.EPR.QCScore objScore in al)
				{
					this.fpSpread2_Sheet1.Cells[i,0].Text = objScore.ID;
					this.fpSpread2_Sheet1.Cells[i,1].Text = objScore.Type;
					this.fpSpread2_Sheet1.Cells[i,2].Text = objScore.Name;
					this.fpSpread2_Sheet1.Cells[i,3].Text = objScore.MiniScore;
					this.fpSpread2_Sheet1.Cells[i,4].Text = objScore.TotalScore;
					this.fpSpread2_Sheet1.Rows[i].Tag = objScore;
					i++;
				}
				
				this.fpSpread2_Sheet1.SetColumnMerge(1, FarPoint.Win.Spread.Model.MergePolicy.Always);
				this.fpSpread2_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
			}
			catch{}
		}
		
		private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public FS.HISFC.Models.RADT.PatientInfo PatientInfo
		{
			get
			{
				return this.myPatientInfo;
			}
			set
			{
				this.myPatientInfo = value;
				this.Refresh();
			}
		}

		/// <summary>
		/// ˢ��
		/// </summary>
		public new void Refresh()
		{
			if(this.myPatientInfo == null) return;
			ArrayList al = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCScoreList(this.myPatientInfo.ID);
			if(al == null)
			{
                MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				return;
			}
			for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)
			{
				this.fpSpread2_Sheet1.Cells[i,5].Text = "False";	
			}
			foreach(FS.HISFC.Models.EPR.QCScore obj in al)
			{
				for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)
				{
					if(this.fpSpread2_Sheet1.Cells[i,0].Text == obj.ID)
					{
						this.fpSpread2_Sheet1.Cells[i,5].Text = "True";
						break;
					}
				}
			}
		}

		/// <summary>
		/// ˫�����������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void fpSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
		{
			#region old
			//�ж��Ƿ��Ѿ�����
//			this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount,1);
//			int iRow = this.fpSpread1_Sheet1.RowCount -1;
//			FS.HISFC.Models.EPR.QCScore qcScore = (this.fpSpread2_Sheet1.ActiveRow.Tag as FS.HISFC.Models.EPR.QCScore).Clone();
//			this.fpSpread1_Sheet1.Cells[iRow ,0].Text = qcScore.ID;
//			this.fpSpread1_Sheet1.Cells[iRow ,1].Text = qcScore.Type;
//			this.fpSpread1_Sheet1.Cells[iRow ,2].Text = qcScore.Name;
//			this.fpSpread1_Sheet1.Cells[iRow ,3].Text = qcScore.MiniScore;
//			this.fpSpread1_Sheet1.Cells[iRow ,4].Text = qcScore.Memo;
			#endregion

            if (e.Row < 0) return;
            if (this.fpSpread2_Sheet1.Cells[e.Row, 5].Text.ToUpper() == "TRUE")
                this.fpSpread2_Sheet1.Cells[e.Row, 5].Text = "False";
            else
                this.fpSpread2_Sheet1.Cells[e.Row, 5].Text = "True";

    //#region  zgx-2007-10-1
    //       if(e.Row <0) return;
    //        if(this.fpSpread2_Sheet1.Cells[e.Row ,5].Text.ToUpper()=="TRUE")
    //        {
    //            this.fpSpread2_Sheet1.Cells[e.Row ,5].Text = "False";
    //            this.fpSpread2_Sheet1.Cells[e.Row,4].Text="1";
    //        }
    //        else
    //        {
    //            if (this.fpSpread2_Sheet1.Cells[e.Row,4].Text.Trim()!="������"&&this.fpSpread2_Sheet1.Cells[e.Row,4].Text.Trim()!="����۷�")
    //            {
    //                id=this.fpSpread2_Sheet1.Cells[e.Row,0].Text;
    //                type=this.fpSpread2_Sheet1.Cells[e.Row,1].Text;
    //                name=this.fpSpread2_Sheet1.Cells[e.Row,2].Text;
    //                totalScore=this.fpSpread2_Sheet1.Cells[e.Row,3].Text;
    //                miniScore=this.fpSpread2_Sheet1.Cells[e.Row,5].Text;
    //                fqcsitem.StartPosition=FormStartPosition.CenterParent;
    //                fqcsitem.ShowDialog(this);
    //                if(fqcsitem.strItem>0)
    //                {
    //                    nowRow6=fqcsitem.strItem;
    //                    this.fpSpread2_Sheet1.Cells[e.Row,6].Text=Convert.ToString(fqcsitem.strItem);
						
    //                }
    //                else
    //                {
    //                    this.fpSpread2_Sheet1.Cells[e.Row,6].Text="1";
    //                }
    //            }
    //            this.fpSpread2_Sheet1.Cells[e.Row ,7].Text = "True";
    //        }
		
    //    }
    //   #endregion

		}
		
		/// <summary>
		/// �������
		/// </summary>
		/// <returns></returns>
		public decimal GetScore()
		{
			decimal score = 0;
			for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)
			{
				if(this.fpSpread2_Sheet1.Cells[i,5].Text.ToUpper()=="TRUE")
				{

					decimal miniScore =0;
				string level = "";
				miniScore = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread2_Sheet1.Cells[i,3].Text);
				if(miniScore == 0)
					level = this.fpSpread2_Sheet1.Cells[i,3].Text;
				
				score = miniScore+score;
				}
			}
			if(score >100)
				score = 0;
			else
				score = 100 - score;
			return score;
		}

		/// <summary>
		/// ��ò����ȼ�
		/// </summary>
		/// <returns></returns>
		public string GetLevel()
		{
			string level = "�׼�";
			for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)
			{
				if(this.fpSpread2_Sheet1.Cells[i,5].Text.ToUpper()=="TRUE")
				{
					string strlevel = "";
					strlevel = this.fpSpread2_Sheet1.Cells[i,3].Text;
				
					if(strlevel=="����" )
					{
						if(level=="�׼�" || level == "�Ҽ�")
						{
							level = "����";
						}
					}
					else if(strlevel == "�Ҽ�")
					{
						if(level=="�׼�")
						{
							level = "�Ҽ�";
						}
					}
					else if(strlevel == "����")
					{
						if(level=="�׼�" || level == "�Ҽ�" || level == "����")
						{
							level = "����";
						}
					}
					else
					{
					
					}
				}
			}
			
			return level;
		}

		private bool IsHaveScore(string id)
		{
			return false;
		}

		/// <summary>
		/// ���滼������
		/// </summary>
		public void Save()
		{
			if(this.myPatientInfo==null) return;
			FS.HISFC.BizProcess.Factory.Function.BeginTransaction();
			
			try
			{
                if (FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.DeleteQCScore(this.myPatientInfo.ID) == -1)
				{
                    FS.HISFC.BizProcess.Factory.Function.RollBack();
                    MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
				}
				for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)
				{
					if(this.fpSpread2_Sheet1.Cells[i,5].Text.ToUpper()=="TRUE")
					{
						FS.HISFC.Models.EPR.QCScore obj = new FS.HISFC.Models.EPR.QCScore();
						obj.ID = this.fpSpread2_Sheet1.Cells[i,0].Text;
						obj.Type = this.fpSpread2_Sheet1.Cells[i,1].Text;
						obj.Name = this.fpSpread2_Sheet1.Cells[i,2].Text;
						obj.MiniScore = this.fpSpread2_Sheet1.Cells[i,3].Text;
						obj.PatientInfo = this.myPatientInfo;
						if(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.InsertQCScore(obj)==-1)
						{
                            FS.HISFC.BizProcess.Factory.Function.RollBack();
							MessageBox.Show(FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.Err);
						}
					}
				}
                FS.HISFC.BizProcess.Factory.Function.Commit();
			}
			catch(Exception ex)
			{
				FS.HISFC.BizProcess.Factory.Function.RollBack();
				MessageBox.Show(ex.Message);
			}
			
		}

		
		/// <summary>
		/// �Զ�����
		/// </summary>
		public void Auto()
		{
			if(this.myPatientInfo == null) return;

			if(MessageBox.Show("ȷʵҪ�Զ�������?\n�Զ����ֽ�ɾ����ǰ��������Ϣ��","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
				return;

			

			ArrayList alConditions  = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.GetQCConditionList();

			for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)//ȥ����ǰ������
			{
				this.fpSpread2_Sheet1.Cells[i,5].Text = "False";	
			}
			
			foreach(FS.HISFC.Models.EPR.QCConditions condition  in alConditions)
			{
				bool b = FS.HISFC.BizProcess.Factory.Function.IntegrateEPR.ExecQCInfo(this.myPatientInfo.ID,Common.Classes.Function.ISql,condition);
				if(b)
				{
					
					for(int i=0;i<this.fpSpread2_Sheet1.RowCount;i++)
					{
						if(this.fpSpread2_Sheet1.Cells[i,0].Text == condition.ID)
						{
							this.fpSpread2_Sheet1.Cells[i,5].Text = "True";
							break;
						}
					}
				
				}
			}	

		}

		private void fpSpread2_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		private void fpSpread2_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
		{
			if(e.Column ==5)
			{
				if(this.fpSpread2_Sheet1.Cells[e.Row,e.Column].Text.ToUpper()=="TRUE")
				{
					this.fpSpread2_Sheet1.Rows[e.Row].ForeColor = Color.Red;
				}
				else
				{
					this.fpSpread2_Sheet1.Rows[e.Row].ForeColor = Color.Black;
				}
				if(OnScoreChanged!=null)
					OnScoreChanged(sender,null);
			}
		}

	}
}

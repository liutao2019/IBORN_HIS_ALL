using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation.Report
{
    /// <summary>
    /// [��������: �����������ͳ��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-15]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReportDoctor : UserControl, FS.FrameWork.WinForms.Forms.IReport
    {
        public ucReportDoctor()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.Init();
            }
        }


        #region ����
        private void Init()
        {
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            this.dtpBegin.Value = DateTime.Parse(Environment.OperationManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd") + " 00:00:00");
            this.dtpEnd.Value = DateTime.Parse(Environment.OperationManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd") + " 23:59:59");

            //������
            ArrayList alRet = Environment.IntegrateManager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            this.cmbDept.AddItems(alRet);
            this.cmbDept.IsListOnly = true;
            this.cmbDept.Tag = Environment.OperatorDeptID;

            //this.fpSpread1_Sheet1.RowCount = 2;
        }
        #endregion

        #region IReport ��Ա

        public int Query()
        {
            this.fpSpread1_Sheet1.RowCount = 0;

			ArrayList DataAl = new ArrayList();
            DataAl = Environment.ReportManager.GetReport11(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
			if(DataAl == null || DataAl.Count == 0) return -1;
			//�ϼ���
			long total = 0;
			
			foreach(ArrayList thisData in DataAl)
			{
				if(thisData == null || thisData.Count < 3) continue;
				if(thisData[0].ToString() == "") thisData[0] = "δ֪";
				if(thisData[1].ToString() == "") thisData[1] = "δ֪";
				if(thisData[2].ToString() == "") thisData[2] = "0";
				this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count,1);
				this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1,0].Value = thisData[0].ToString();
				this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1,1].Value = thisData[1].ToString();
				this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1,2].Value = thisData[2].ToString();
				try
				{
					total = total + long.Parse(thisData[2].ToString());
				}
				catch{}
			}
			//���á��ϼơ���
			this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count,1);
			this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1,0].Value = "��  �� :";
			this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1,2].Value = total.ToString();
			this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);

            this.fpSpread1_Sheet1.Columns[0].Locked = true;
            this.fpSpread1_Sheet1.Columns[1].Locked = true;
            this.fpSpread1_Sheet1.Columns[2].Locked = true;
            return 0;
        }

        #endregion

        #region IReportPrinter ��Ա

        public int Print()
        {
            return Environment.Print.PrintPreview(30,0,this.neuPanel2);
        }

        public int PrintPreview()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public int Export()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region �¼�

        

        private void dtpBegin_ValueChanged(object sender, EventArgs e)
        {
            this.lblTime.Text = string.Concat("��ѯʱ�䣺", this.dtpBegin.Value.ToString("yyyy-MM-dd HH:mm:ss"), " -- ", this.dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

#endregion



        #region IReport ��Ա


        public int SetParm(string parm, string reportID)
        {
            return 1;
        }

        #endregion

        #region IReport ��Ա


        public int SetParm(string parm, string reportID, string emplSql, string deptSql)
        {
            return 1;
        }

        #endregion
    }


}

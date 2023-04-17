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
    public partial class ucReportCategory : UserControl, FS.FrameWork.WinForms.Forms.IReport
    {
        public ucReportCategory()
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
            this.cmbCategory.SelectedIndex = 0;
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

            string m_DeptID = this.cmbDept.Tag.ToString();

            ArrayList DataAl = null;
            switch (this.cmbCategory.SelectedIndex)
            {
                case 0:
                    DataAl = Environment.ReportManager.GetReport08(m_DeptID, this.dtpBegin.Value, this.dtpEnd.Value);
                    break;
                case 1:
                    DataAl = Environment.ReportManager.GetReport09(m_DeptID, this.dtpBegin.Value, this.dtpEnd.Value);
                    break;
                case 2:
                    DataAl = Environment.ReportManager.GetReport10(m_DeptID, this.dtpBegin.Value, this.dtpEnd.Value);
                    break;
            }
            if (DataAl == null || DataAl.Count == 0) 
                return -1;
            //�ϼ�
            long total = 0;

            //�ټ���
            foreach (ArrayList thisData in DataAl)
            {
                if (thisData == null || thisData.Count < 2) 
                    continue;
                this.fpSpread1_Sheet1.RowCount += 1;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = thisData[0].ToString();
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = thisData[1].ToString();
                try
                {
                    total = total + long.Parse(thisData[1].ToString());
                }
                catch { }
            }
            //���á��ϼơ���
            this.fpSpread1_Sheet1.RowCount += 1;
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = "��  �� :";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = total.ToString();
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);


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
        private void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTitle.Text = string.Format("�������ͳ����������(��{0})", this.cmbCategory.Text);
            this.fpSpread1_Sheet1.Columns[0].Label = this.cmbCategory.Text;
        }
     

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

        int FS.FrameWork.WinForms.Forms.IReport.Query()
        {
            return 1;
        }

        int FS.FrameWork.WinForms.Forms.IReport.SetParm(string parm, string reportID)
        {
            return 1;
        }

        #endregion

        #region IReportPrinter ��Ա

        int FS.FrameWork.WinForms.Forms.IReportPrinter.Export()
        {
            return 1;
        }

        int FS.FrameWork.WinForms.Forms.IReportPrinter.Print()
        {
            return 1;
        }

        int FS.FrameWork.WinForms.Forms.IReportPrinter.PrintPreview()
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

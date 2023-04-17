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
    /// [��������: ����������Ϣ��ѯ]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucReportPatient : ucReportBase
    {
        public ucReportPatient()
        {
            InitializeComponent();
            this.ShowCategory = true;
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            this.Title = "����������Ϣ��ѯ";
            this.cmbCategory.Visible = false;
            this.neuLabel4.Visible = false;
        }

#region �¼�

        protected override int OnQuery()
        {
            this.fpSpread1_Sheet1.RowCount = 0;

            System.Data.DataSet ds = Environment.ReportManager.GetPersonOperation(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
            if (ds == null)
            {
                MessageBox.Show("��ѯ���ݳ���");
                return -1;
            }
            this.fpSpread1.DataSource = ds;
            //���á��ϼơ���
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = "��  �� :";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = "�� " + (this.fpSpread1_Sheet1.RowCount - 1).ToString() + " ��";
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);
            //this.fpSpread1_Sheet1.Columns[0].Width = 80;
            //this.fpSpread1_Sheet1.Columns[1].Width = 80;
            //this.fpSpread1_Sheet1.Columns[2].Width = 80;
            //this.fpSpread1_Sheet1.Columns[3].Width = 40;
            //this.fpSpread1_Sheet1.Columns[4].Width = 40;
            //this.fpSpread1_Sheet1.Columns[5].Width = 100;
            //this.fpSpread1_Sheet1.Columns[6].Width = 100;
            //this.fpSpread1_Sheet1.Columns[7].Width = 100;
            //this.fpSpread1_Sheet1.Columns[8].Width = 100;
            //this.fpSpread1_Sheet1.Columns[9].Width = 40;
            //this.fpSpread1_Sheet1.Columns[10].Width = 40;
            //this.fpSpread1_Sheet1.Columns[10].Width = 40;

            return 0;
        }
#endregion
    }
}

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
    public partial class ucReportCanceled : ucReportBase
    {
        public ucReportCanceled()
        {
            InitializeComponent();
            this.Title = "���������������ȡ������(������)";
            this.fpSpread1_Sheet1.GrayAreaBackColor = Color.White;
            this.fpSpread1_Sheet1.ColumnHeader.Rows[0].BackColor = Color.White;
            this.fpSpread1_Sheet1.Columns[0].Width = 200;
            this.fpSpread1_Sheet1.Columns[1].Width = 100;
        }

#region �¼�



        protected override int OnQuery()
        {
            this.fpSpread1_Sheet1.RowCount = 0;

            ArrayList DataAl = new ArrayList();
            DataAl = Environment.ReportManager.GetReport21(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
            if (DataAl == null || DataAl.Count == 0) return -1;

            //�ϼ���
            long total = 0;

            foreach (FS.FrameWork.Models.NeuObject neuObj in DataAl)
            {
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = neuObj.Name;//��������
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = neuObj.Memo;//����ȡ������
                try
                {
                    total = total + long.Parse(neuObj.Memo);
                }
                catch { }
            }

            //���á��ϼơ���
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = "��  �� :";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = total.ToString();
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);

            return 0;
        }
#endregion
    }
}

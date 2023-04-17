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
    public partial class ucReportPacuCount : ucReportBase
    {
        public ucReportPacuCount()
        {
            InitializeComponent();
            this.ShowCategory = true;
        }

#region �¼�
        protected override void InitSpread()
        {
            this.fpSpread1_Sheet1.ColumnCount = 2;
            this.fpSpread1_Sheet1.Columns[0].Label = "����״��";
            this.fpSpread1_Sheet1.Columns[1].Label = "��������";
        }

        protected override void InitCategory()
        {
            this.cmbCategory.Items.Add("����״��");
            this.cmbCategory.Items.Add("����״��");

            this.cmbCategory.SelectedIndex = 0;
        }

        protected override void OnCategoryChanged()
        {
            this.Title = string.Format("�������ͳ�ƽ���PACU����������(��{0})",this.cmbCategory.Text);
            this.fpSpread1_Sheet1.Columns[0].Label = this.cmbCategory.Text;
        }

        protected override int OnQuery()
        {
            //�����
            this.fpSpread1_Sheet1.RowCount = 0;
            ArrayList DataAl = new ArrayList();



            switch (this.cmbCategory.SelectedIndex)
            {
                case 0:
                    DataAl = Environment.ReportManager.GetReport13(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
                    break;
                case 1:
                    DataAl = Environment.ReportManager.GetReport14(this.cmbDept.Tag.ToString(), this.dtpBegin.Value, this.dtpEnd.Value);
                    break;
            }
            if (DataAl == null || DataAl.Count == 0) 
                return -1;

            //�ϼ���
            long ll_Total = 0;

            //�ټ���
            foreach (ArrayList thisData in DataAl)
            {
                if (thisData == null || thisData.Count < 2) continue;
                this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = thisData[0].ToString();
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = thisData[1].ToString();
                try
                {
                    ll_Total = ll_Total + long.Parse(thisData[1].ToString());
                }
                catch { }
            }
            //���á��ϼơ���
            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.Rows.Count, 1);
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 0].Value = "��  �� :";
            this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.Rows.Count - 1, 1].Value = ll_Total.ToString();
            this.fpSpread1_Sheet1.Rows[this.fpSpread1_Sheet1.Rows.Count - 1].Font = new System.Drawing.Font("����", 10F, System.Drawing.FontStyle.Bold);

            return 0;
        }
#endregion
    }
}

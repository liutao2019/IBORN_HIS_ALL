using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.RADT.Forms
{
    /// <summary>
    /// 出院登记提示信息
    /// </summary>
    public partial class frmMessageShow : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmMessageShow()
        {
            InitializeComponent();
        }

        private Font context = new Font("宋体", 10F);
        private Font title = new Font("宋体", 12F, FontStyle.Bold);

        public void Clear()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }

        public void SetPatientInfo(string patientInfo)
        {
            this.Clear();
            this.neuLabelPatient.Text = patientInfo;
        }

        public void SetTipMessage(string tips)
        {
            this.neuLabelTip.Text = tips;
        }

        public void SetMessage(string message)
        {
            string[] row = message.Split('\r');
            int lastEmptyRowCount = 0;
            for (int i = 0; i < row.Length; i++)
            {
                if (string.IsNullOrEmpty(row[i].Trim()))
                {
                    lastEmptyRowCount++;
                    if (lastEmptyRowCount > 1)
                    {
                        continue;
                    }
                }
                else
                {
                    lastEmptyRowCount = 0;
                }
                int curRow = this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Rows[curRow].Height = 30F;
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = row[i];
                this.neuSpread1_Sheet1.Cells[curRow, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                this.neuSpread1_Sheet1.Cells[curRow, 0].Locked = true;
                if (row[i].StartsWith("\n★"))
                {
                    this.neuSpread1_Sheet1.Cells[curRow, 0].Font = title;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[curRow, 0].Font = context;
                }
            }
        }

        public void HideNoButton()
        {
            this.neuButtonYes.Text = "确定(&O)";
            this.neuButtonNo.Hide();
        }

        public void SetPerfectWidth()
        {
            this.neuSpread1_Sheet1.Columns[0].Width = this.neuSpread1_Sheet1.Columns[0].GetPreferredWidth();
            this.Width = (int)this.neuSpread1_Sheet1.Columns[0].Width + 30;
            if (this.Width < 400)
            {
                this.Width = 400;
            }
        }

        private void neuButtonYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void neuButtonNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}

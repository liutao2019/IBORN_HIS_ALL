using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.FuYou.DayBalance
{
    public partial class frmConfirmBalanceRecord : Form
    {
        public frmConfirmBalanceRecord()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // ѡ����ս����
            string balanceSequence = "";

            if (this.fpSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }

            // ��ȡѡ���ս��¼�����к�
            balanceSequence = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text;
            this.DialogResult = DialogResult.OK;
            
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private ArrayList balanceRecord = new ArrayList();

        public ArrayList BalanceRecord 
        {
            set 
            {
                this.balanceRecord = value;

                if (this.balanceRecord == null || this.balanceRecord.Count == 0) 
                {
                    return;
                }

                foreach (FS.FrameWork.Models.NeuObject temp in balanceRecord)
                {
                    this.fpSpread1_Sheet1.AddRows(this.fpSpread1_Sheet1.RowCount, 1);
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text = temp.ID;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 1].Text = temp.Name;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 2].Text = temp.Memo;
                }
            }
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // ѡ����ս����
            string balanceSequence = "";

            if (this.fpSpread1_Sheet1.RowCount <= 0) 
            {
                return;
            }

            // ��ȡѡ���ս��¼�����к�
            balanceSequence = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 0].Text;
            this.DialogResult = DialogResult.OK;

            this.Hide();
        }
    }
}
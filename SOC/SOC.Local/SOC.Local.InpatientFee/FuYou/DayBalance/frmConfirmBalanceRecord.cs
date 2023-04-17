using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FS.SOC.Local.InpatientFee.FuYou.DayBalance
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

            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                return;
            }

            // ��ȡѡ���ս��¼�����к�
            balanceSequence = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            this.DialogResult = DialogResult.OK;
            
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private List<FS.FrameWork.Models.NeuObject> lstBalanceRecord = null;

        public List<FS.FrameWork.Models.NeuObject> LstBalanceRecord 
        {
            get
            {
                return lstBalanceRecord;
            }
            set 
            {
                lstBalanceRecord = value;
                if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0)
                    return;

                foreach (FS.FrameWork.Models.NeuObject temp in lstBalanceRecord)
                {
                    this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = temp.ID;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = temp.Name;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = temp.Memo;
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = temp.User02;
                }
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            // ѡ����ս����
            string balanceSequence = "";

            if (this.neuSpread1_Sheet1.RowCount <= 0) 
            {
                return;
            }

            // ��ȡѡ���ս��¼�����к�
            balanceSequence = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text;
            this.DialogResult = DialogResult.OK;

            this.Hide();
        }
    }
}
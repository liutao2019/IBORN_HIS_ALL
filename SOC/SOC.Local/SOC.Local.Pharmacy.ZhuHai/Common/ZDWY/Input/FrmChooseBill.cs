using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Input
{
    public partial class FrmChooseBill : Form
    {
        public FrmChooseBill()
        {
            InitializeComponent();
        }

        public string billNo = "";
        public bool isOK = false;

        public int Init(List<string> strList)
        {
            this.isOK = false;
            if (strList.Count == 0)
            {
                return -1;
            }
            this.fpSpread1_Sheet1.Rows.Count = strList.Count;
            int i = 0;
            foreach (string strTemp in strList)
            {
                string strDate = strTemp.Substring(0, strTemp.IndexOf('-'));
                string strTarger = strTemp.Substring(strTemp.IndexOf('-') + 1, strTemp.IndexOf("BillNo") - strTemp.IndexOf('-') - 1);
                this.fpSpread1_Sheet1.Cells[i, 0].Text = strDate;
                this.fpSpread1_Sheet1.Cells[i, 1].Text = strTarger;
                this.fpSpread1_Sheet1.Rows[i].Tag = strTemp.Substring(0, strTemp.IndexOf("BillNo") + 6);
                i = i + 1;
            }
            return 1;
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.billNo = this.fpSpread1_Sheet1.Rows[e.Row].Tag.ToString();
            if (string.IsNullOrEmpty(this.billNo))
            {
                MessageBox.Show("请选择一张单据！");
            }
            else
            {
                this.isOK = true;
                this.Hide();
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.isOK = false;
            this.Hide();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveRow.Index > 0)
            {
                this.billNo = this.fpSpread1_Sheet1.ActiveRow.Tag.ToString();
                if (string.IsNullOrEmpty(this.billNo))
                {
                    MessageBox.Show("请选择一张单据！");
                }
                else
                {
                    this.isOK = true;
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show("请选择一张单据！");
            }
        }
    }
}

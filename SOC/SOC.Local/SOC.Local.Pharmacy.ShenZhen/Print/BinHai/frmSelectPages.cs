using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Print.BinHai
{
    public partial class frmSelectPages : Form
    {
        public frmSelectPages()
        {
            InitializeComponent();
        }

        private int fromPage;
        private int toPage;
        private int pageCount;

        public int FromPage
        {
            get { return this.fromPage; }
            set { this.fromPage = value; }
        }

        public int ToPage
        {
            get { return this.toPage; }
            set { this.toPage = value; }
        }

        public int PageCount
        {
            get { return this.pageCount; }
            set { this.pageCount = value; }
        }

        public void SetPages()
        {
            this.lblTotPageNum.Text = "总共" + pageCount.ToString() + "页";
            this.txtFromPage.Text = "1";
            this.txtToPage.Text = pageCount.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.rbtnPrintAll.Checked)
            {
                this.FromPage = 1;
                this.ToPage = pageCount;
            }
            else
            {
                this.FromPage = NConvert.ToInt32(this.txtFromPage.Text);
                this.ToPage = NConvert.ToInt32(this.txtToPage.Text);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void frmSelectPages_Load(object sender, EventArgs e)
        {
            this.rbtnPageRange.Checked = true;
            this.rbtnPrintAll.Checked = false;
        }
    }
}

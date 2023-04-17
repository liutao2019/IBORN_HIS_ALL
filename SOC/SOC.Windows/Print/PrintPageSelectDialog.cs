using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Windows.Forms
{
    /// <summary>
    /// [功能描述: 打印范围选择]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public partial class PrintPageSelectDialog : System.Windows.Forms.Form
    {
        public PrintPageSelectDialog()
        {
            InitializeComponent();
            this.nbtOK.Click += new EventHandler(nbtOK_Click);
            this.nbtCancel.Click += new EventHandler(nbtCancel_Click);
            this.nrbtPintAll.CheckedChanged += new EventHandler(nrbtPintAll_CheckedChanged);
            this.nrbtPrintRange.CheckedChanged += new EventHandler(nrbtPrintRange_CheckedChanged);
            this.nntbFromPage.KeyUp += new KeyEventHandler(nntbFromPage_KeyUp);
            this.nntbToPage.KeyPress += new KeyPressEventHandler(nntbToPage_KeyPress);
            this.nrbtPintAll.KeyPress += new KeyPressEventHandler(nrbtPintAll_KeyPress);
        }

        void nrbtPintAll_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.nbtOK.Select();
            this.nbtOK.Focus();
        }

        void nntbToPage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.nbtOK.Select();
                this.nbtOK.Focus();
            }
        }

        void nntbFromPage_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (int.Parse(this.nntbFromPage.Text) < 1)
                {
                    this.nntbFromPage.Text = "1";
                }
                this.nntbToPage.Select();
                this.nntbToPage.Focus();
                this.nntbToPage.SelectAll();
            }
        }

        void nrbtPintAll_CheckedChanged(object sender, EventArgs e)
        {
            this.nntbFromPage.Text = "1";
            this.nntbToPage.Text = this.maxPageNO.ToString();
        }

        void nrbtPrintRange_CheckedChanged(object sender, EventArgs e)
        {
            this.nntbFromPage.Enabled = this.nrbtPrintRange.Checked;
            this.nntbToPage.Enabled = this.nrbtPrintRange.Checked;
            if (this.nntbFromPage.Enabled)
            {
                this.nntbFromPage.Select();
                this.nntbFromPage.Focus();
                this.nntbFromPage.SelectAll();
            }
        }


        void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.FromPageNO > this.ToPageNO)
            {
                MessageBox.Show("打印范围设置不正确：开始页码不能大于结束页码");
                return;
            }
            if (this.FromPageNO < 1)
            {
                MessageBox.Show("打印范围设置不正确：开始页码不能小于1");
                return;
            }
            this.Close();
        }

        void nbtCancel_Click(object sender, EventArgs e)
        {
            this.nntbFromPage.Text = "0";
            this.nntbToPage.Text = "0";
            this.Close();
        }

        /// <summary>
        /// 开始页
        /// </summary>
        public int FromPageNO
        {
            get 
            { 
                try 
                { 
                    return int.Parse(this.nntbFromPage.Text); 
                } 
                catch 
                { 
                    return 1;
                }
            }
        }

        /// <summary>
        /// 结束页
        /// </summary>
        public int ToPageNO
        {
            get 
            {
                try 
                { 
                    return int.Parse(this.nntbToPage.Text);
                }
                catch 
                {
                    return 1; 
                } 
            }
        }

        private int maxPageNO = 1;

        /// <summary>
        /// 最大页范围
        /// </summary>
        public int MaxPageNO
        {
            set 
            {
                this.maxPageNO = value;
                this.nntbToPage.Text = value.ToString();
                this.nntbFromPage.Text = "1"; 
            }
        }
    }
}
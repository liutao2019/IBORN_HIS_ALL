using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GZSI.ApiControls
{
    public partial class frmSearch : Form
    {
        public frmSearch()
        {
            InitializeComponent();
        }

        //挂号信息
        private FS.HISFC.Models.Registration.Register  regInfo = null;
        public FS.HISFC.Models.Registration.Register RegInfo
        {
            get { return regInfo; }
            set { regInfo = value; }
        }

        /// <summary>
        /// 查询类型 1：身份证 2：个人电脑号 3：姓名
        /// </summary>
        private int flag = 1;
        public int Flag
        {
            set { flag = value; }
            get { return flag; }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.textBox1.Text))
                {
                    MessageBox.Show("请输入查询信息!");
                    return;
                }
                if (this.radioButton1.Checked)
                {
                    flag = 1;
                }
                else if (this.radioButton2.Checked)
                {
                    flag = 2;
                }
                else if (this.radioButton3.Checked)
                {
                    flag = 3;
                }
                else if (this.radioButton4.Checked)
                {
                    flag = 4;
                }

                regInfo.IDCard = flag.ToString() + "|" + this.textBox1.Text.Trim();
                this.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            if (this.RegInfo.IDCardType.ID == "01" || string.IsNullOrEmpty(this.RegInfo.IDCardType.ID))
            {
                this.textBox1.Text = this.RegInfo.IDCard;
            }
        }
    }
}

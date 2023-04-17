using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmWriteMCardNO : Form
    {
        public frmWriteMCardNO()
        {

            InitializeComponent();
        }

        private int returnValue = 0;
        /// <summary>
        /// 返回
        /// </summary>
        public int ReturnValue
        {
            set { returnValue = value; }
            get { return returnValue; }
        }
        private string mCardNo = "";
        /// <summary>
        /// 返回卡面号
        /// </summary>
        public string MCardNo
        {
            set { mCardNo = value; }
            get { return mCardNo; }
        }
        private void button1_Click(object sender, EventArgs e)
        {


            mCardNo = this.txtMarkNo.Text.Trim();
            string error = "";
            if (string.IsNullOrEmpty(mCardNo))
            {
                MessageBox.Show("请输入卡号！");
                returnValue = 0;
                
            }
            else
            {
                returnValue = 1;
            }

            //this.DialogResult = DialogResult.OK;
            
        }
    }
}

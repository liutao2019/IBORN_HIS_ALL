using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    public partial class FrmCopyRecipeNum : Form
    {
        public FrmCopyRecipeNum()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            maxCopyNum = CacheManager.ContrlManager.GetControlParam<int>("HNMZ90", true, 10);
        }
        //复制次数
        private string copyNum = "";

        public string CopyNum
        {
            get
            {
                return copyNum;
            }
            set
            {
                copyNum = value;
            }
        }

        //最大复制次数
        private int maxCopyNum = 10;

        public int MaxCopyNum
        {
            get
            {
                return maxCopyNum;
            }
            set
            {
                maxCopyNum = value;
            }
        }

        private void neuTxtNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnOK_Click(sender,e);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string textTemp = "";
            textTemp = neuTxtNum.Text.Trim().ToString();
            textTemp=FS.FrameWork.Function.NConvert.ToDBC(textTemp);
            bool boolReturn = true;
            boolReturn = System.Text.RegularExpressions.Regex.IsMatch(textTemp, "^[0-9]*$");
            if (!boolReturn)
            {
                MessageBox.Show("请输入合法数字！");
                return;
            }
            if (FS.FrameWork.Function.NConvert.ToInt32(textTemp) >= maxCopyNum)
            {
                MessageBox.Show("您输入的数字太大，请重新输入！");
                return;
            }
            copyNum = textTemp;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

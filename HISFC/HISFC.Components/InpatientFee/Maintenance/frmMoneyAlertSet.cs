using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.HISFC.Components.InpatientFee.Maintenance
{
    public partial class frmMoneyAlertSet : Form
    {

        public decimal myMoneyAlert = 0;

        public frmMoneyAlertSet(decimal moneyAlert)
        {
            
            InitializeComponent();
            moneyAlert = this.myMoneyAlert ;
            this.Init();
        }

        private void Init()
        {
            this.txtMoneyAlert.Text = this.myMoneyAlert.ToString();
        }

        //private bool ValidateValue()
        //{
        //    for (int i = 0, j = this.txtMoneyAlert.Text.Length; i < j; i++)
        //    {
        //        if (!char.IsDigit(this.txtMoneyAlert.Text, i))
        //        {
        //            //����˵���ǵڼ����ַ�������
        //            MessageBox.Show("���߾����߱����Ǵ����0����", "��ʾ", MessageBoxButtons.OK);
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        private void btnOK_Click(object sender, EventArgs e)
        {
            //if (ValidateValue())
            //{
            this.myMoneyAlert = Convert.ToDecimal(this.txtMoneyAlert.Text.Trim().ToString());
            this.DialogResult = DialogResult.OK;
            this.Close();
            //}
            //else
            //{

            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        
        }
    }
}
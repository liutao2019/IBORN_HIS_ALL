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
    public partial class frmTanShiInfoSet : FS.FrameWork.WinForms.Forms.BaseForm
    {
        //private {97B9173B-834D-49a1-936D-E4D04F98E4BA}
        public   FS.HISFC.Models.Order.OutPatient.OrderLisExtend orderExtObj;
        public bool confirmSave = false;
        public frmTanShiInfoSet()
        {
            InitializeComponent();
        }
        public frmTanShiInfoSet(FS.HISFC.Models.Order.OutPatient.OrderLisExtend orderExtend)
        {
            InitializeComponent();
            this.orderExtObj = orderExtend;

        }
        private void   setOrderExtend()
        {
            orderExtObj.Extend1 = dtpt1.Text;
            orderExtObj.Extend2 = txtt2.Text;
            orderExtObj.Extend3 = txtt3.Text;
            orderExtObj.Extend4 = txtt4.Text;
            if (rbt51.Checked == true)
            {
                orderExtObj.Extend5 = "单胎";
            }
            else
            {
                orderExtObj.Extend5 = "双胎";
            }
          
        }
        public void InitInfo()
        {
            this.dtpt1.Value = Convert.ToDateTime(orderExtObj.Extend1);
            this.txtt2.Text = orderExtObj.Extend2;
            this.txtt3.Text = orderExtObj.Extend3;
            this.txtt4.Text = orderExtObj.Extend4;
            if (orderExtObj.Extend5 == "双胎")
            {
                rbt52.Checked = true;
            }
            else
            {
                rbt51.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setOrderExtend();
            confirmSave = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

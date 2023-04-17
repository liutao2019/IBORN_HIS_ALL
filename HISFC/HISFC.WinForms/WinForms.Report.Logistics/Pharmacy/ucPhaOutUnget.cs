using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Report.Logistics.Pharmacy
{
    public partial class ucPhaOutUnget : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        #region ����

        string strFilter = "���ⵥ�� like '{0}%'"; 
        
        #endregion

        public ucPhaOutUnget()
        {
            InitializeComponent();
        }

        #region ����

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.dtpBeginTime.Value > this.dtpEndTime.Value)
            {
                MessageBox.Show("��ѯ��ʼʱ�䲻�ܴ��ڽ���ʱ�䣡");
                return -1;
            }
            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
        }

        private void filter()
        {
            string str = "";
            DataView dv = this.dwMain.Dv;
            if (dv == null)
            {
                return;
            }
            string BillCode = txtBillCode.Text.Trim().Replace(@"\", "").Replace(@"'", "").ToUpper();
            if (string.IsNullOrEmpty(BillCode))
            {
                //dwMain.SetFilter("");
                //dwMain.Filter();
                dv.RowFilter = "";
                return;
            }
            else
            {
                try
                {
                    str = string.Format(strFilter, BillCode);
                    dv.RowFilter = str;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("��ʽ���ַ�������" + ex.Message);
                    return;
                }
                //dwMain.SetFilter(str);
                //dwMain.Filter();
            }
            //dwMain.SetSort("���ⵥ��");
            //dwMain.Sort();
        }

        #endregion

        private void txtBillCode_TextChanged(object sender, EventArgs e)
        {
            this.filter();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucOrderTime : UserControl
    {
        public ucOrderTime()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ҽ��ʱ��
        /// </summary>
        public DateTime OrderTime
        {
            get
            {
                if (this.ckbOrdertime.Checked == true)
                    return this.dtpickerOrder.Value;
                else
                    return new DateTime(2001, 1, 1, 00, 00, 00);
            }
            set
            {
                this.dtpickerOrder.Value = new DateTime(2001, 1, 1, 00, 00, 00);
                this.toolTip1.SetToolTip(this.ckbOrdertime, "ѡ�С�ҽ��ʱ�䡱������ҽ��ʱ���ѯ��\n���Ҳ�ʱ���ѡ��ʱ�䣬ִ�е���Ѳ��\n������Һ������ʾҽ��ʱ�����ѡ��ʱ\n��ļ�¼��");
            }
        }

        private void ckbOrdertime_CheckedChanged(object sender, EventArgs e)
        {
            this.dtpickerOrder.Enabled = ((CheckBox)sender).Checked;
        }
    }
}

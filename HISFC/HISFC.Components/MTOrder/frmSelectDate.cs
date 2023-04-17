using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder
{
    public partial class frmSelectDate : Form
    {
        public frmSelectDate()
        {
            InitializeComponent();

            this.dtpDate.KeyDown += new KeyEventHandler(dtpDate_KeyDown);
        }

        /// <summary>
        /// ��ǰѡ�������
        /// </summary>
        public DateTime SelectedDate
        {
            get { return dtpDate.Value.Date; }
            set
            {
                dtpDate.Value = value;
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }
    }
}
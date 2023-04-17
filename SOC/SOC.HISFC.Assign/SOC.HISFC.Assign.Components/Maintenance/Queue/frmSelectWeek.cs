using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Queue
{
    /// <summary>
    /// [��������: ����ģ��ѡ�����ڽ���]<br></br>
    /// [�� �� ��: jing.zhao]<br></br>
    /// [����ʱ��: 2011-12]<br></br>
    /// </summary>
    public partial class frmSelectWeek : Form
    {
        public frmSelectWeek()
        {
            InitializeComponent();

            this.comboBox1.KeyDown += new KeyEventHandler(comboBox1_KeyDown);
        }

        /// <summary>
        /// ��ǰѡ�������
        /// </summary>
        public Day SelectedWeek
        {
            get { return (Day)this.comboBox1.SelectedIndex; }
            set
            {
                this.comboBox1.SelectedIndex = (int)value;
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
        }
    }
}
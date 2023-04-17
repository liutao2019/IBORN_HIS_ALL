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
    /// [功能描述: 复制模板选择星期界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class frmSelectWeek : Form
    {
        public frmSelectWeek()
        {
            InitializeComponent();

            this.comboBox1.KeyDown += new KeyEventHandler(comboBox1_KeyDown);
        }

        /// <summary>
        /// 当前选择的星期
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
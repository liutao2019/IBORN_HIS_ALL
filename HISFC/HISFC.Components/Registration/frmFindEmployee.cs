using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Registration
{
    public partial class frmFindEmployee : Form
    {
        public frmFindEmployee()
        {
            InitializeComponent();

            this.comboBox1.KeyDown += new KeyEventHandler(comboBox1_KeyDown);

            FS.HISFC.BizProcess.Integrate.Manager perMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList al = perMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            this.comboBox1.AddItems(al);
        }

        /// <summary>
        /// ��ǰѡ�����Ա
        /// </summary>
        public string SelectedEmployee
        {
            get
            {
                if (this.comboBox1.Tag == null)
                    return "";


                return this.comboBox1.Tag.ToString();
            }

            set
            {
                this.comboBox1.Tag = value;
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

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
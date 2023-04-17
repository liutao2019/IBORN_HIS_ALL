using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder.Forms
{
    public partial class frmSelectPatient : Form
    {
        public frmSelectPatient()
        {
            InitializeComponent();

            this.txtClinicNo.KeyDown += new KeyEventHandler(txtClinicNo_KeyDown);
            this.cbType.SelectedIndex = 0;
        }

        /// <summary>
        /// 当前选择的日期
        /// </summary>
        public string SelectedClinicNo
        {
            get { return txtClinicNo.Text.Trim(); }
            set
            {
                txtClinicNo.Text = value;
            }
        }
        /// <summary>
        /// 当前选择的病人类型
        /// </summary>
        public FS.HISFC.Models.MedicalTechnology.EnumApplyType SelectedType
        {
            get { return (FS.HISFC.Models.MedicalTechnology.EnumApplyType)(this.cbType.SelectedIndex + 1); }
            set { this.cbType.SelectedIndex = (int)value - 1; }
        }

        private void txtClinicNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(this.cbType.Text))
                {
                    MessageBox.Show("请选择患者类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(this.txtClinicNo.Text.Trim()))
                {
                    MessageBox.Show("请输入患者病历/住院号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                frmSelectClinic frm = new frmSelectClinic();
                frm.IsClinic = this.SelectedType == Models.MedicalTechnology.EnumApplyType.Clinic;
                //frm.SearchDays = 14;
                frm.CardNo = this.txtClinicNo.Text.ToUpper().PadLeft(10, '0');
                DialogResult dr = frm.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    this.SelectedClinicNo = frm.ClinicNo;
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtClinicNo_KeyDown(this.txtClinicNo, new KeyEventArgs(Keys.Enter));
        }
    }
}
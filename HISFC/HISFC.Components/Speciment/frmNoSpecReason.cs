using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmNoSpecReason : Form
    {
        private OperApply operApply;
        private OperApplyManage operApplyManage;

        public OperApply OperApply
        {
            get
            {
                return operApply;
            }
            set
            {
                operApply = value;
            }
        }

        public frmNoSpecReason()
        {
            InitializeComponent();
            operApply = new OperApply();
            operApplyManage = new OperApplyManage();            
        }

        private bool Valid()
        {
            if (rbtOther.Checked)
            {
                if (txtReason.Text.Trim() == "")
                {
                    return false;
                }
                else
                {
                    operApply.NoColReason = txtReason.Text.TrimEnd().TrimStart();
                }
            }            
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!Valid())
            {
                MessageBox.Show("请填写原因！", "标本库");
                return;
            }
            if (rbtNoAgree.Checked)
            {
                operApply.NoColReason = rbtNoAgree.Text;
            }
            if (operApplyManage.UpdateReason(operApply.NoColReason, operApply.OperApplyId.ToString()) <= 0)
            {
                MessageBox.Show("更新失败！", "标本库");
                return;
            }
            MessageBox.Show("更新成功!", "标本库");
            this.Close();
        }

        private void rbt_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbt = sender as RadioButton;
            if (rbt.Name == "rbtOther")
            {
                lblReason.Visible = true;
                txtReason.Visible = true;                
            }
            else
            {
                operApply.NoColReason = rbt.Text;
                lblReason.Visible = false;
                txtReason.Visible = false;
            }
        }

        private void frmNoSpecReason_Load(object sender, EventArgs e)
        {
            lblDateTime.Text = operApply.OperTime.Date.ToString();
            lblPatient.Text = operApply.PatientName;
        }

    }
}
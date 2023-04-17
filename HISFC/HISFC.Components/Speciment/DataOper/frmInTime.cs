using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.DataOper
{
    public partial class frmInTime : Form
    {
        public frmInTime()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定更新", "更新入库时间", MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
                return;
            else
            {
                string sql = "update SPEC_SUBSPEC set STORETIME = to_date('" + dtpTime.Value.ToString() + "','yyyy-mm-dd hh24:mi:ss') where STORETIME between to_date('" + dtpOperTime.Value.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss') and  to_date('" + dtpOperTime.Value.Date.AddDays(1.0).ToString() + "','yyyy-mm-dd hh24:mi:ss')";
                FS.HISFC.BizLogic.Speciment.SubSpecManage tmpMgr = new FS.HISFC.BizLogic.Speciment.SubSpecManage();
                if (tmpMgr.ExecNoQuery(sql) == -1)
                {
                    MessageBox.Show("更新失败");
                }
                else
                {
                    MessageBox.Show("更新成功");
                }
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
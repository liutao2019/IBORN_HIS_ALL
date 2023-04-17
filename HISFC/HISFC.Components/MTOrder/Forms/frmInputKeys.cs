using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.MTOrder.Forms
{
    public partial class frmInputKeys : Form
    {
        public frmInputKeys()
        {
            InitializeComponent();
            this.KeyDown+=new KeyEventHandler(Event_KeyDown);
            this.tbKeys.KeyDown += new KeyEventHandler(Event_KeyDown);
        }

        private void frmInputKeys_Load(object sender, EventArgs e)
        {
            dtpBeginTime.Value = DateTime.Now.AddMonths(-3);
            dtpEndTime.Value = DateTime.Now.AddMonths(3);
        }
        /// <summary>
        /// 预约管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalTechnology.Appointment appMgr = new FS.HISFC.BizLogic.MedicalTechnology.Appointment();

        private void btnOK_Click(object sender, EventArgs e)
        {
            string keys = tbKeys.Text.Trim().ToUpper();
            List<Models.MedicalTechnology.Appointment> AppointList = appMgr.GetHistory(keys, dtpBeginTime.Value, dtpEndTime.Value);
            if (AppointList == null || AppointList.Count < 1)
            {
                MessageBox.Show("没有关相病人信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                frmOrderHistory newFrm = new frmOrderHistory() { AppointList = AppointList };
                if (newFrm.ShowDialog() == DialogResult.Cancel)
                    return;

                frmPatientInfo newPai = new frmPatientInfo() { Appoint = newFrm.SelectedAppoint };
                newPai.ShowDialog();
            }
        }
        private void Event_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }
    }
}

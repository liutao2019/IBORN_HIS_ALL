using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class frmLeavePrint : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmLeavePrint()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            //ArrayList leaveTypes = new ArrayList();
            //leaveTypes = CacheManager.GetConList("LEAVETYPE");
            //this.cmbLeaveType.AddItems(leaveTypes);
            //this.cmbLeaveType.SelectedIndex = 1; 
        }

        public DateTime LeaveStart;
        public DateTime LeaveEnd;
       // public string LeaveType;

        void btSavet_Click(object sender, EventArgs e)
        {
            //LeaveType = this.cmbLeaveType.SelectedItem.ID;
            LeaveStart = this.dtBeginDate.Value;
            LeaveEnd = this.dtEndDate.Value;
            if (LeaveEnd < LeaveStart && LeaveEnd.ToLongDateString() != LeaveStart.ToLongDateString())
            {
                MessageBox.Show("开始时间不能大于结束时间！");
                return;
            }
            this.Hide();
        }

    }
}

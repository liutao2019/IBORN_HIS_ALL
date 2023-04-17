using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Queue
{
    public partial class frmSelectDays : Form
    {
        public frmSelectDays()
        {
            InitializeComponent();
            this.dtpEndDate.ValueChanged += new EventHandler(dtpEndDate_ValueChanged);
            this.numDays.ValueChanged += new EventHandler(numDays_ValueChanged);
        }

        void numDays_ValueChanged(object sender, EventArgs e)
        {
            this.dtpEndDate.ValueChanged -= new EventHandler(dtpEndDate_ValueChanged);
            this.dtpEndDate.Value = CommonController.CreateInstance().GetSystemTime().AddDays((int)this.numDays.Value - 1);
            this.dtpEndDate.ValueChanged += new EventHandler(dtpEndDate_ValueChanged);
        }

        void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            this.numDays.ValueChanged -= new EventHandler(numDays_ValueChanged);
            if ((this.dtpEndDate.Value - CommonController.CreateInstance().GetSystemTime()).Days < 0)
            {
                MessageBox.Show("选择日期不能小于当前日期!");
                this.dtpEndDate.Value = CommonController.CreateInstance().GetSystemTime();
                this.numDays.Value = (CommonController.CreateInstance().GetSystemTime() - CommonController.CreateInstance().GetSystemTime()).Days + 1;
                return;
            }
            this.numDays.Value = (this.dtpEndDate.Value.Date - CommonController.CreateInstance().GetSystemTime().Date).Days + 1;
            this.numDays.ValueChanged += new EventHandler(numDays_ValueChanged);
        }

        /// <summary>
        /// 当前选择的星期
        /// </summary>
        public int Days
        {
            get
            {
                return (int)this.numDays.Value;
            }
            set
            {
                this.numDays.Value = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.numDays.Value = 7;
            this.dtpEndDate.Value = CommonController.CreateInstance().GetSystemTime().AddDays(6);

            base.OnLoad(e);
        }
    }
}

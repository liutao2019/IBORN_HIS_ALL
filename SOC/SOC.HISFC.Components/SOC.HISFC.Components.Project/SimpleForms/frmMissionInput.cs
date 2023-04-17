using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Project
{
    public partial class frmMissionInput : System.Windows.Forms.Form
    {
        public frmMissionInput()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmMissionInput_Load);
        }

        void frmMissionInput_Load(object sender, EventArgs e)
        {
            this.ucMissionInfo1.Init(true, false);
        }
        public delegate void InputCompletedHander();
        public InputCompletedHander InputCompletedEven;

        public delegate void InputCompletedAndNextHander();
        public InputCompletedAndNextHander InputCompletedAndNextEven;

        MissionManager curMissionManager = new MissionManager();

        public DataRow curDataRow = null;
        public int SetMission(DataRow row)
        {
            curDataRow = row;
            if (row == null)
            {
                return 0;
            }
            try
            {
                this.Text = "问题卡——" + row["主键"].ToString() + "——当前序号：" + row["序号"].ToString();
            }
            catch { };
            this.ucMissionInfo1.SetMission(row,false);

            return 1;
        }

        public DataRow GetData()
        {
            if (curDataRow == null)
            {
                return null;
            }
            curDataRow = this.ucMissionInfo1.GetData();

            return curDataRow;
        }

        private void nbtOK_Click(object sender, EventArgs e)
        {
            if (this.InputCompletedEven != null)
            {
                this.InputCompletedEven();
            }
            this.DialogResult = DialogResult.OK;
        }

        private void nbtOKAndNext_Click(object sender, EventArgs e)
        {
            if (this.InputCompletedAndNextEven != null)
            {
                this.InputCompletedAndNextEven();
            }
            this.ucMissionInfo1.ClearMission();
        }

        private void nbtCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}

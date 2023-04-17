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
    public partial class frmMissionAssign : Form
    {
        public frmMissionAssign()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmMissionAssign_Load);
        }

        void frmMissionAssign_Load(object sender, EventArgs e)
        {
            this.ucMissionInfo1.Init(true,true);
            this.ncmbAccepter.AddItems(this.curMissionManager.QueryAllMissionAccepters());
        }

        public delegate void InputCompletedHander();
        public InputCompletedHander InputCompletedEven;

        public delegate void InputCompletedAndNextHander();
        public InputCompletedAndNextHander InputCompletedAndNextEven;

        public delegate void InputCompletedAndAllHander(string modelName, string accepter);
        public InputCompletedAndAllHander InputCompletedAndAllEven;

        MissionManager curMissionManager = new MissionManager();

        public DataRow curDataRow = null;
        public int SetMission(DataRow row)
        {
            curDataRow = row;
            this.nbtOKAndNext.Enabled = true;
            this.nbtOK.Enabled = true;
            if (row == null)
            {
                return 0;
            }
            try
            {
                this.Text = "问题卡——" + row["主键"].ToString() + "——当前序号：" + row["序号"].ToString();
                if (row["主键"].ToString() == "")
                {
                    this.nbtOKAndNext.Enabled = false;
                    this.nbtOK.Enabled = false;
                }
            }
            catch { };
            try
            {
                this.ncmbAccepter.Text = row["开发人"].ToString();
            }
            catch { };
            try
            {
                this.ncmbAccepter.Text = row["受任人"].ToString();
            }
            catch { };

            try
            {
                this.ndtpAccepterTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(row["协定开发达成日期"].ToString());
            }
            catch { };

            try
            {
                this.ndtpAccepterTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(row["协定完成时间"].ToString());
            }
            catch { };

            this.ucMissionInfo1.SetMission(row,true);

            return 1;
        }

        public DataRow GetData()
        {
            if (curDataRow == null)
            {
                return null;
            }
            curDataRow = this.ucMissionInfo1.GetData();
            try
            {
                curDataRow["开发人"] = this.ncmbAccepter.Text;// = row["开发人"].ToString();
            }
            catch { };
            try
            {
                curDataRow["受任人"] = this.ncmbAccepter.Text;// = row["受任人"].ToString();
            }
            catch { };
            try
            {
                curDataRow["协定开发达成日期"] = this.ndtpAccepterTime.Value;// = FS.FrameWork.Function.NConvert.ToDateTime(row["协定开发达成日期"].ToString());
            }
            catch { };

            try
            {
                curDataRow["协定完成时间"] = this.ndtpAccepterTime.Value;// = FS.FrameWork.Function.NConvert.ToDateTime(row["协定完成时间"].ToString());
            }
            catch { };

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
        }

        private void nbtCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

    
        private void nbtOKAll_Click(object sender, EventArgs e)
        {
            if (this.ucMissionInfo1.GetModelName() == "")
            {
                MessageBox.Show("请选择或录入模块名称");
                return;
            }

            string text = "是否将当前选择数据里【" + this.ucMissionInfo1.GetModelName() + "】的问题或需求都分派给【" + this.ncmbAccepter.Text + "】？";
            if (this.ncmbAccepter.Text == "")
            {
                text = "是否将当前选择数据里【" + this.ucMissionInfo1.GetModelName() + "】问题或需求都取消分派？";
            }
            if (MessageBox.Show(this, text, "提示>>",MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (this.InputCompletedAndAllEven != null)
                {
                    this.InputCompletedAndAllEven(this.ucMissionInfo1.GetModelName(), this.ncmbAccepter.Text);
                }
                this.DialogResult = DialogResult.OK;
            }
        }
     
    }
}

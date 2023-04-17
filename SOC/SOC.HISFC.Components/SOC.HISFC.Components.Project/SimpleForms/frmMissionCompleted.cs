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
    public partial class frmMissionCompleted : Form
    {
        public frmMissionCompleted()
        {
       InitializeComponent();
            this.Load += new EventHandler(frmMissionCompleted_Load);
        }

        void frmMissionCompleted_Load(object sender, EventArgs e)
        {
            this.ucMissionInfo1.Init(true,true);
            this.ncmbAccepter.AddItems(this.curMissionManager.QueryAllMissionAccepters());
            this.ncmbCompleter.AddItems(this.curMissionManager.QueryAllMissionAccepters());
        }

        public delegate void InputCompletedHander();
        public InputCompletedHander InputCompletedEven;

        public delegate void InputCompletedAndNextHander();
        public InputCompletedAndNextHander InputCompletedAndNextEven;

        public delegate void InputCompletedAndAllHander(string modelName, string accepter);
        public InputCompletedAndAllHander InputCompletedAndAllEven;

        MissionManager curMissionManager = new MissionManager();

        public DataRow curDataRow = null;
        public int SetMission(DataRow row, string completer)
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

            try
            {
                this.ncmbCompleter.Text = row["完成人"].ToString();
            }
            catch { };
            try
            {
                this.ncmbCompleter.Text = row["实际开发人"].ToString();
            }
            catch { };

            if(string.IsNullOrEmpty(this.ncmbCompleter.Text))
            {
                this.ncmbCompleter.Text = completer;
            }


            try
            {
                this.ndtpCompleteTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(row["实际开发达成日期"].ToString());
            }
            catch { };

            try
            {
                this.ndtpCompleteTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(row["实际完成时间"].ToString());
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
            if (this.ndtpAccepterTime.Checked)
            {
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
            }
            try
            {
                curDataRow["实际开发人"] = this.ncmbCompleter.Text;// = row["开发人"].ToString();
            }
            catch { };

            try
            {
                curDataRow["完成人"] = this.ncmbCompleter.Text;// = row["开发人"].ToString();
            }
            catch { };

            try
            {
                curDataRow["实际开发达成日期"] = this.ndtpCompleteTime.Value;// = FS.FrameWork.Function.NConvert.ToDateTime(row["协定完成时间"].ToString());
            }
            catch { };


            try
            {
                curDataRow["实际完成时间"] = this.ndtpCompleteTime.Value;// = FS.FrameWork.Function.NConvert.ToDateTime(row["协定完成时间"].ToString());
            }
            catch { };

            //try
            //{
            //    curDataRow["状态"] = "完成";// = FS.FrameWork.Function.NConvert.ToDateTime(row["协定完成时间"].ToString());
            //}
            //catch { };


            return curDataRow;
        }

        private void nbtOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(curDataRow["状态"].ToString()))
                {
                    MessageBox.Show("请您录入状态！");
                    return;
                }
            }
            catch { }

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
     
    }
}



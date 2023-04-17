using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Project
{
    public partial class ucMissionInfo : UserControl
    {
        public ucMissionInfo()
        {
            InitializeComponent();            
        }

        MissionManager curMissionManager = new MissionManager();

        public DataRow curDataRow = null;
        public int SetMission(DataRow row, bool isCheckMadeTime)
        {
            curDataRow = row;
            if (row == null)
            {
                return 0;
            }
           
            try
            {
                this.nrtbMission.Text = row["需求说明"].ToString();
            }
            catch { };

            try
            {
                this.nrtbMemo.Text = row["备注"].ToString();
            }
            catch { };
            try
            {
                this.ntxtProjectID.Text = row["项目编号"].ToString();
                if (this.ntxtProjectID.Text == "")
                {
                    this.ntxtProjectID.Text = FS.FrameWork.Management.Connection.Hospital.ID;
                }
            }
            catch { };
            try
            {
                this.ntxtProjectName.Text = row["项目名称"].ToString();
                if (this.ntxtProjectName.Text == "")
                {
                    this.ntxtProjectName.Text = FS.FrameWork.Management.Connection.Hospital.Name;
                }
            }
            catch { };
            try
            {
                this.ncmProjectStage.Text = row["项目阶段"].ToString();
            }
            catch { };
            try
            {
                this.ncmbModelName.Text = row["模块名称"].ToString();
            }
            catch { };
            try
            {
                this.ncmbFunctionType.Text = row["功能分类"].ToString();
            }
            catch { };
            try
            {
                this.ncmbMadeOper.Text = row["需求提出人"].ToString();
            }
            catch { };
            try
            {
                this.ndtpMadeTime.ShowCheckBox = isCheckMadeTime;
                if (isCheckMadeTime)
                {
                    this.ndtpMadeTime.Checked = false;
                }
                this.ndtpMadeTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(row["需求提出时间"].ToString());
            }
            catch { };
            try
            {
                this.ncmbPriorityLevel.Text = row["优先级"].ToString();
            }
            catch { };

            try
            {
                this.ncmbState.Text = row["状态"].ToString();
            }
            catch { };

          

            return 1;
        }

        public DataRow GetData()
        {
            if (curDataRow == null)
            {
                return null;
            }
            try
            {
                curDataRow["需求说明"] = this.nrtbMission.Text;
            }
            catch { };
            try
            {
                curDataRow["备注"] = this.nrtbMemo.Text;
            }
            catch { };
            try
            {
                curDataRow["项目编号"] = this.ntxtProjectID.Text;
            }
            catch { };
            try
            {
                curDataRow["项目名称"] = this.ntxtProjectName.Text;
            }
            catch { };
            try
            {
                curDataRow["项目阶段"] = this.ncmProjectStage.Text;
            }
            catch { };
            try
            {
                curDataRow["模块名称"] = this.ncmbModelName.Text;
            }
            catch { };
            try
            {
                curDataRow["功能分类"] = this.ncmbFunctionType.Text;
            }
            catch { };
            try
            {
                curDataRow["需求提出人"] = this.ncmbMadeOper.Text;
            }
            catch { };
            try
            {
                if (this.ndtpMadeTime.Checked || !this.ndtpMadeTime.ShowCheckBox)
                {
                    curDataRow["需求提出时间"] = this.ndtpMadeTime.Value;
                }
            }
            catch { };

            try
            {
                curDataRow["优先级"] = this.ncmbPriorityLevel.Text;
            }
            catch { };

            try
            {
                curDataRow["状态"] = this.ncmbState.Text;
            }
            catch { };

            return curDataRow;
        }

        public int ClearMission()
        {
            this.nrtbMission.Text = "";
            return 1; 
        }

        public string GetModelName()
        {
            return this.ncmbModelName.Text;
        }

        public int Init(bool isAllowModifyState, bool isAllowModifyMemo)
        {
            this.ncmbFunctionType.AddItems(curMissionManager.QueryAllFuncionTypes());
            this.ncmbPriorityLevel.AddItems(curMissionManager.QueryAllPriorityLevels());
            this.ncmbModelName.AddItems(curMissionManager.QueryAllModels());
            this.ncmbState.AddItems(curMissionManager.QueryAllStates());
            this.ncmbState.Enabled = isAllowModifyState;
            this.nrtbMemo.ReadOnly = !isAllowModifyMemo;
            return 1;
        }
    }
}

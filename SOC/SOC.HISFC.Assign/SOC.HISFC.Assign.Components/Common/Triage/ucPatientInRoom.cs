using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.SOC.HISFC.Assign.Components.Base;

namespace FS.SOC.HISFC.Assign.Components.Common.Triage
{
    internal partial class ucPatientInRoom : ucAssignBase
    {
        public ucPatientInRoom()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 患者分诊信息
        /// </summary>
        public FS.SOC.HISFC.Assign.Models.Assign Assign
        {
            get
            {
                return this.txtCard.Tag as FS.SOC.HISFC.Assign.Models.Assign;
            }
            set
            {
                this.setItem(value);
            }
        }

        private bool isOK = false;
        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }

        #region 触发事件

        private void ucInRoom_Load(object sender, EventArgs e)
        {
            this.cmbRoom.Focus();
            this.isOK = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Assign == null)
            {
                return;
            }

            FS.FrameWork.Models.NeuObject roomObj = this.cmbRoom.SelectedItem;

            if (this.cmbRoom.Items.Count <= 0)
            {
                CommonController.CreateInstance().MessageBox(this, "请先维护诊室!", MessageBoxIcon.Error);
                return;
            }
            if (roomObj == null)
            {
                CommonController.CreateInstance().MessageBox(this, "请选择分诊诊室!", MessageBoxIcon.Error);
                this.cmbRoom.Focus();
                return;
            }
            FS.FrameWork.Models.NeuObject consoleObj = this.cmbConsole.SelectedItem;

            if (this.cmbConsole.Items.Count <= 0)
            {
                CommonController.CreateInstance().MessageBox(this, "请先维护该诊室的诊台!", MessageBoxIcon.Error);
                return;
            }
            if (consoleObj == null)
            {
                CommonController.CreateInstance().MessageBox(this, "请选择分诊诊台!", MessageBoxIcon.Error);
                this.cmbConsole.Focus();
                return;
            }

            this.Assign.Queue.SRoom = roomObj;
            this.Assign.Queue.Console = consoleObj;

            
            this.isOK = true;
            this.FindForm().Close();
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根据诊室，生成诊台
            string strRoom = this.cmbRoom.Tag.ToString();
           FS.SOC.HISFC.Assign.BizLogic.Console seatMgr = new FS.SOC.HISFC.Assign.BizLogic.Console();
            ArrayList al = new ArrayList();
            al = seatMgr.QueryValid(strRoom);
            if (al == null)
            {
                this.cmbConsole.ClearItems();
                return;
            }
            if (al.Count <= 0)
            {
                this.cmbConsole.ClearItems();
                return;
            }
            this.cmbConsole.AddItems(al);
            this.cmbConsole.SelectedIndex = 0;
            al.Clear();
        }

        private void cmbRoom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbConsole.Focus();
            }
        }

        private void cmbConsole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnSave.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

        #region 初始化

        public int Init(string nurseID)
        {
            FS.SOC.HISFC.Assign.BizLogic.Room roomMgr = new FS.SOC.HISFC.Assign.BizLogic.Room();
            ArrayList al = roomMgr.QueryValidRoomsByDept(nurseID, "ALL");
            if (al == null)
            {
                al = new ArrayList();
            }
            this.cmbRoom.AddItems(al);

            return 1;
        }

        private void setItem(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            this.txtCard.Text = assign.Register.PID.PatientNO;
            this.txtName.Text = assign.Register.Name;
            this.txtDept.Text = assign.Register.DoctorInfo.Templet.Dept.Name;
            this.txtRegDate.Text = assign.Register.DoctorInfo.SeeDate.ToString();
            this.txtQueue.Text = assign.Queue.Name;
            this.cmbRoom.Text = assign.Queue.SRoom.Name;
            this.cmbConsole.Text = assign.Queue.Console.Name;

            foreach (FS.FrameWork.Models.NeuObject obj in this.cmbRoom.alItems)
            {
                if (assign.Queue.SRoom.ID == obj.ID)
                {
                    this.cmbRoom.Tag = obj.ID;
                    break;
                }
            }
            this.txtCard.Tag = assign;
        }

        public void Clear()
        {
            this.txtCard.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtDept.Text = string.Empty;
            this.txtRegDate.Text = string.Empty;
            this.txtQueue.Text = string.Empty;
            this.cmbRoom.Text = string.Empty;
            this.cmbConsole.Text = string.Empty;
            this.txtCard.Tag = null ;

        }

        #endregion

    }
}

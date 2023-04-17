using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Assign.Components.Maintenance.Room
{
    /// <summary>
    /// [功能描述: 诊台维护界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucConsole : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.IMaintenance<FS.HISFC.Models.Nurse.Seat>
    {
        public ucConsole()
        {
            InitializeComponent();
        }

        #region 变量

        public new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Nurse.Seat> saveInfo;

        #endregion

        #region 初始化

        private void initEvents()
        {
            this.btnOK.Click -= new EventHandler(btnOK_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);

            this.btnExit.Click -= new EventHandler(btnExit_Click);
            this.btnExit.Click += new EventHandler(btnExit_Click);

        }

        private void initDatas()
        {

        }

        private void initInterfaces()
        {
        }

        #endregion

        #region 方法

        private bool isValid()
        {
            if (this.cmbRooms.Tag == null || this.cmbRooms.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "诊室不能为空", MessageBoxIcon.Error);
                this.cmbRooms.Select();
                this.cmbRooms.Focus();
                return false;
            }
            else if (this.txtRoomName.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "诊台名称不能为空", MessageBoxIcon.Error);
                this.txtRoomName.Select();
                this.txtRoomName.Focus();
                return false;
            }
            else if (this.txtSort.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "顺序号不能为空", MessageBoxIcon.Error);
                this.txtSort.Select();
                this.txtSort.Focus();
                return false;
            }

            return true;
        }

        private FS.HISFC.Models.Nurse.Seat getRoom()
        {
            FS.HISFC.Models.Nurse.Seat room = null;
            if (this.Tag is FS.HISFC.Models.Nurse.Seat)
            {
                room = this.Tag as FS.HISFC.Models.Nurse.Seat;
            }
            else
            {
                room = new FS.HISFC.Models.Nurse.Seat();
            }

            if (this.cmbRooms.SelectedItem != null)
            {
                room.PRoom.ID = this.cmbRooms.SelectedItem.ID;
                room.PRoom.Name = this.cmbRooms.SelectedItem.Name;
            }
            else
            {
                room.PRoom = new FS.HISFC.Models.Nurse.Room();
            }

            room.Name = this.txtRoomName.Text;
            room.PRoom.InputCode = this.txtInput.Text;
            room.PRoom.Sort = FS.FrameWork.Function.NConvert.ToInt32(this.txtSort.Text);
            room.PRoom.IsValid = FS.FrameWork.Function.NConvert.ToInt32(this.ckValid.Checked).ToString();
            room.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;
            return room;
        }

        private int Save()
        {
            FS.HISFC.Models.Nurse.Seat room = this.getRoom();
            FS.SOC.HISFC.Assign.BizProcess.Console consoleBiz = new FS.SOC.HISFC.Assign.BizProcess.Console();

            //验证同诊室下是否存在同名诊台
            string errinfo = string.Empty;
            //验证重复
            ArrayList alSeat = consoleBiz.QueryOtherValid(room.PRoom.ID, room.ID, ref errinfo);
            foreach (FS.HISFC.Models.Nurse.Seat subSeat in alSeat)
            {
                if (room.Name == subSeat.Name)
                {
                    CommonController.CreateInstance().MessageBox(this, "存在重名诊台，请核对诊台名称！", MessageBoxIcon.Error);
                    return -1;
                }
            }

            string error="";
            if (consoleBiz.SaveConsole(room, this.Tag == null ? FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert : FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, ref error) <= 0)
            {
                CommonController.CreateInstance().MessageBox(this, error, MessageBoxIcon.Error);
                return -1;
            }

            if (this.SaveInfo != null)
            {
                this.SaveInfo(room);
            }
            return 1;
        }

        public new bool Foucs()
        {
            this.txtRoomName.SelectAll();
            this.txtRoomName.Focus();
            return true;
        }

        #endregion

        #region 触发事件

        void btnExit_Click(object sender, EventArgs e)
        {
            this.ParentForm.Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            if (!this.isValid())
            {
                return;
            }

            if (this.Save() <= 0)
            {
                return;
            }

            this.ParentForm.Close();

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region IMaintenance<Room> 成员 
        
        public int Init(System.Collections.ArrayList al)
        {
            if (al == null)
            {
                return -1;
            }

            for (int i = 0; i < al.Count; i++)
            {
                if (al[i] is FS.FrameWork.Models.NeuObject)
                {
                    this.cmbRooms.AddItems(al);
                    break;
                }
            }

            return 1;
        }

        public int Add(FS.HISFC.Models.Nurse.Seat t)
        {
            this.Clear();

            if (t != null)
            {
                this.cmbRooms.Tag = t.PRoom.ID;
            }
            this.Focus();
            return 1;
        }

        public int Modify(FS.HISFC.Models.Nurse.Seat t)
        {
            this.Clear();
            if (t != null)
            {
                this.cmbRooms.Tag = t.PRoom.ID;
                this.txtRoomName.Text = t.Name;
                this.txtInput.Text = t.PRoom.InputCode;
                this.txtSort.Text = t.PRoom.Sort.ToString();
                this.ckValid.Checked = FS.FrameWork.Function.NConvert.ToBoolean(t.PRoom.IsValid);
                this.Tag = t;
            }
            this.Focus();
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Nurse.Seat> SaveInfo
        {
            get
            {
                return saveInfo;
            }
            set
            {
                saveInfo=value;
            }
        }

        #endregion

        #region IInitialisable 成员

        public int Init()
        {
            this.initInterfaces();
            this.initDatas();
            this.initEvents();

            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.cmbRooms.Tag = null;
            this.txtRoomName.Text = "";
            this.txtInput.Text = "";
            this.txtSort.Text = "";
            this.ckValid.Checked = true;
            this.Tag = null;

            return 1;
        }

        #endregion

        #region ILoadable 成员

        public new int Load()
        {
            return 1;
        }

        #endregion
    }
}

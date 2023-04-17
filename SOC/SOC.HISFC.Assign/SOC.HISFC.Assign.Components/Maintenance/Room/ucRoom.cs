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
    /// [功能描述: 诊室维护界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public partial class ucRoom : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.IMaintenance<FS.SOC.HISFC.Assign.Models.Room>
    {
        public ucRoom()
        {
            InitializeComponent();
        }

        #region 变量

        public new FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Room> saveInfo;
        private FS.SOC.HISFC.Assign.BizProcess.Room roomMgr = new FS.SOC.HISFC.Assign.BizProcess.Room();

        #endregion

        #region 初始化

        private void initEvents()
        {
            this.btnOK.Click -= new EventHandler(btnOK_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);

            this.btnExit.Click -= new EventHandler(btnExit_Click);
            this.btnExit.Click += new EventHandler(btnExit_Click);

            this.cmbNurseStation.SelectedIndexChanged -= new EventHandler(cmbNurseStation_SelectedIndexChanged);
            this.cmbNurseStation.SelectedIndexChanged += new EventHandler(cmbNurseStation_SelectedIndexChanged);
        }

        private void initDatas()
        {

        }

        #endregion

        #region 方法

        private bool isValid()
        {
            if (this.cmbNurseStation.Tag == null || this.cmbNurseStation.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "护士站不能为空", MessageBoxIcon.Error);
                this.cmbNurseStation.Select();
                this.cmbNurseStation.Focus();
                return false;
            }
            else if (this.cmbDepartment.Tag == null || this.cmbDepartment.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "科室不能为空", MessageBoxIcon.Error);
                this.cmbDepartment.Select();
                this.cmbDepartment.Focus();
                return false;
            }
            else if (this.txtRoomName.Text.Trim().Length == 0)
            {
                CommonController.CreateInstance().MessageBox(this, "诊室名称不能为空", MessageBoxIcon.Error);
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

        private FS.SOC.HISFC.Assign.Models.Room getRoom()
        {
            FS.SOC.HISFC.Assign.Models.Room room = null;
            if (this.Tag is FS.SOC.HISFC.Assign.Models.Room)
            {
                room = this.Tag as FS.SOC.HISFC.Assign.Models.Room;
            }
            else
            {
                room = new FS.SOC.HISFC.Assign.Models.Room();
            }

            if (this.cmbNurseStation.SelectedItem != null)
            {
                room.Nurse = this.cmbNurseStation.SelectedItem;
            }
            else
            {
                room.Nurse = new FS.FrameWork.Models.NeuObject();
            }

            if (this.cmbDepartment.SelectedItem != null)
            {
                room.Dept = this.cmbDepartment.SelectedItem;
            }
            else
            {
                room.Dept = new FS.FrameWork.Models.NeuObject();
            }

            room.Name = this.txtRoomName.Text;
            room.InputCode = this.txtInput.Text;
            room.Sort = FS.FrameWork.Function.NConvert.ToInt32(this.txtSort.Text);
            room.IsValid = FS.FrameWork.Function.NConvert.ToInt32(this.ckValid.Checked).ToString();
            return room;
        }

        private int Save()
        {
            FS.SOC.HISFC.Assign.Models.Room room = this.getRoom();
            //验证同护士站下是否存在同名诊室
            string errinfo = string.Empty;
            ArrayList alRoom = this.roomMgr.GetOtherRooms(room.Nurse.ID, room.Dept.ID, room.ID, ref errinfo);
            if (alRoom != null && alRoom.Count > 0)
            {
                foreach (FS.SOC.HISFC.Assign.Models.Room subRoom in alRoom)
                {
                    if (room.Name == subRoom.Name)
                    {
                        CommonController.CreateInstance().MessageBox(this, "存在重名诊室，请核对诊室名称！", MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
            string error="";
            if (roomMgr.SaveRoom(room, this.Tag == null ? FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Insert : FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, ref error) <= 0)
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

        void cmbNurseStation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbNurseStation.SelectedItem != null)
            {
                string error = "";
                this.cmbDepartment.AddItems(Function.GetNurseDept(this.cmbNurseStation.SelectedItem.ID, ref error));
            }
            else
            {
                this.cmbDepartment.AddItems(new ArrayList());
            }
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
                    this.cmbNurseStation.AddItems(al);
                    break;
                }
            }

            return 1;
        }

        public int Add(FS.SOC.HISFC.Assign.Models.Room t)
        {
            this.Clear();

            if (t != null)
            {
                this.cmbNurseStation.Tag = t.Nurse.ID;
                this.cmbDepartment.Tag = t.Dept.ID;
            }
            this.Focus();
            return 1;
        }

        public int Modify(FS.SOC.HISFC.Assign.Models.Room t)
        {
            this.Clear();
            if (t != null)
            {
                this.cmbNurseStation.Tag = t.Nurse.ID;
                this.cmbDepartment.Tag = t.Dept.ID;
                this.txtRoomName.Text = t.Name;
                this.txtInput.Text = t.InputCode;
                this.txtSort.Text = t.Sort.ToString();
                this.ckValid.Checked = FS.FrameWork.Function.NConvert.ToBoolean(t.IsValid);
                this.Tag = t;
            }
            this.Focus();
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Assign.Models.Room> SaveInfo
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
            this.initDatas();
            this.initEvents();

            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.cmbNurseStation.Tag = null;
            this.cmbDepartment.Tag = null;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Nurse
{
    public partial class ucChangeDept : UserControl
    {
        private FS.HISFC.BizLogic.Nurse.Seat seatMgr = new FS.HISFC.BizLogic.Nurse.Seat();


        public ucChangeDept()
        {
            InitializeComponent();
        }

        private void Init()
        {
            FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();

            ArrayList al = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID /*var.User.Nurse.ID*/);

            if (al == null) al = new ArrayList();

            this.cmbRoom.AddItems(al);
        }

        private void ucChangeDept_Load(object sender, EventArgs e)
        {
            this.Init();
            this.cmbRoom.SelectedIndexChanged += new EventHandler(cmbRoom_SelectedIndexChanged);
        }

        /// <summary>
        /// 根据诊室生成诊台列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRoom_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //根据诊室，生成诊台
            string strRoom = this.cmbRoom.Tag.ToString();
            ArrayList al = new ArrayList();
            al = this.seatMgr.QueryValid(strRoom);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject roomObj = this.cmbRoom.SelectedItem;

            if (this.cmbRoom.Items.Count <= 0)
            {
                MessageBox.Show("请先维护诊室!", "提示");
                return;
            }
            if (roomObj == null)
            {
                MessageBox.Show("请选择分诊诊室!", "提示");
                this.cmbRoom.Focus();
                return;
            }
            FS.FrameWork.Models.NeuObject consoleObj = this.cmbConsole.SelectedItem;

            if (this.cmbConsole.Items.Count <= 0)
            {
                MessageBox.Show("请先维护该诊室的诊台!", "提示");
                return;
            }
            if (consoleObj == null)
            {
                MessageBox.Show("请选择分诊诊台!", "提示");
                this.cmbConsole.Focus();
                return;
            }

            FS.HISFC.BizLogic.Nurse.Assign assMgr = new FS.HISFC.BizLogic.Nurse.Assign();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();
            try
            {
                //assMgr.SetTrans(SQLCA.Trans);

                //int rtn = assMgr.Update(this.Assign.Register.ID, roomObj, consoleObj, assMgr.GetDateTimeFromSysDateTime());

                //if (rtn == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(assMgr.Err, "提示");
                //    return;
                //}
                //if (rtn == 0)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("该患者分诊状态已经改变,请重新刷新屏幕!", "提示");
                //    return;
                //}
                //SQLCA.Commit();
            }
            catch (Exception error)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                //MessageBox.Show(error.Message, "提示");
                //return;
            }

            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

    }
}

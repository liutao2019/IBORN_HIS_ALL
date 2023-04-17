using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Nurse.Controls.SDFY
{
    internal partial class ucInRoom : UserControl
    {
        private FS.HISFC.BizLogic.Nurse.Seat seatMgr = new FS.HISFC.BizLogic.Nurse.Seat();

        /// <summary>
        /// 患者分诊信息
        /// </summary>
        public FS.HISFC.Models.Nurse.Assign Assign
        {
            set
            {
                this.txtCard.Text = value.Register.PID.CardNO;
                this.txtName.Text = value.Register.Name;
                this.txtDept.Text = value.Register.DoctorInfo.Templet.Dept.Name;
                this.txtRegDate.Text = value.Register.DoctorInfo.SeeDate.ToString();
                this.txtQueue.Text = value.Queue.Name;
                this.cmbRoom.Text = value.Queue.SRoom.Name;
                this.cmbConsole.Text = value.Queue.Console.Name;

                foreach (FS.FrameWork.Models.NeuObject obj in this.cmbRoom.alItems)
                {
                    if (value.Queue.SRoom.ID == obj.ID)
                    {
                        this.cmbRoom.Tag = obj.ID;
                        break;
                    }
                }
                this.txtCard.Tag = value;
            }
            get
            {
                if (this.txtCard.Tag == null)
                {
                    return null;
                }
                else
                {
                    return (FS.HISFC.Models.Nurse.Assign)this.txtCard.Tag;
                }
            }
        }

        public delegate void MyDelegate(FS.HISFC.Models.Nurse.Assign assign);
        public event MyDelegate OK;

        public ucInRoom(string nurseID)
        {
            InitializeComponent();

            FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();

            ArrayList al = roomMgr.GetRoomInfoByNurseNoValid(nurseID);

            if (al == null)
            {
                al = new ArrayList();
            }

            this.cmbRoom.AddItems(al);	
        }

        private void ucInRoom_Load(object sender, EventArgs e)
        {
            this.cmbRoom.Focus();
        }

        private void neuButton1_Click(object sender, EventArgs e)
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
                assMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                int rtn = assMgr.Update(this.Assign.Register.ID, roomObj, consoleObj, assMgr.GetDateTimeFromSysDateTime());

                if (rtn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(assMgr.Err, "提示");
                    return;
                }
                if (rtn == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("该患者分诊状态已经改变,请重新刷新屏幕!", "提示");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception error)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(error.Message, "提示");
                return;
            }

            this.Assign.Queue.SRoom = roomObj;
            this.Assign.Queue.Console = consoleObj;
            if (this.OK != null)
                this.OK(this.Assign);

            this.FindForm().Close();
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
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
                this.neuButton1.Focus();
            }
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }
    }
}

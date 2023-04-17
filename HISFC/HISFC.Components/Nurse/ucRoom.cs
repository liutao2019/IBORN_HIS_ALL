using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Nurse
{
    internal partial class ucRoom : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRoom()
        {
            InitializeComponent();
        }

        private void ucRoom_Load(object sender, EventArgs e)
        {
            try
            {
                this.init();

                this.FindForm().Text = "����ά��";
                this.FindForm().MinimizeBox = false;
                this.FindForm().MaximizeBox = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            this.tbRoom.Focus();
        }

        #region ������

        /// <summary>
        /// ����ʵ��
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();
        private FS.HISFC.Models.Nurse.Room roomInfo = new FS.HISFC.Models.Nurse.Room();
        private FS.HISFC.Models.Base.Employee p = new FS.HISFC.Models.Base.Employee();

        private FS.HISFC.BizProcess.Integrate.Manager ps = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        //FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
        //private FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        //private FS.HISFC.Models.RADT.Person p = new FS.HISFC.Models.RADT.Person();

        /// <summary>
        /// ����ʵ��
        /// </summary>
        public FS.HISFC.Models.Nurse.Room RoomInfo
        {
            get { return this.roomInfo; }
            set { this.roomInfo = value; }
        }


        private string stateFlag = "ADD";
        //�ؼ�״̬
        public string StateFlag
        {
            get { return this.stateFlag; }
            set { this.stateFlag = value; }
        }
        #endregion

        #region �򿪽��渳ֵ

        /// <summary>
        /// ���ݴ��������ʼ��
        /// </summary>
        public void init()
        {
            if (this.stateFlag.ToUpper() == "ADD")
            {
                this.Add();
            }
            if (this.stateFlag.ToUpper() == "EDIT")
            {
                this.Edit();
            }
            this.SetRoom();
            this.tbRoom.Focus();
        }
        private void Add()
        {
            this.stateFlag = "ADD";
            this.cmbValid.SelectedIndex = 1;
            this.tbRoom.Focus();

            this.roomInfo.Sort = 0;
            this.roomInfo.IsValid = "1";

        }
        private void Edit()
        {
            this.stateFlag = "EDIT";
        }

        /// <summary>
        /// ��ʵ�帴�Ƶ��ؼ�
        /// </summary>
        public void SetRoom()
        {
            p = ps.GetEmployeeInfo(this.roomMgr.Operator.ID);
            FS.HISFC.Models.Base.Department dp = new FS.HISFC.Models.Base.Department();
            dp = this.deptMgr.GetDepartment(roomInfo.Nurse.ID);
            this.tbDept.Text = dp.Name;
            this.tbDept.Tag = this.roomInfo.Nurse.ID;
            this.tbRoom.Text = this.roomInfo.Name;
            this.tbRoom.Tag = this.roomInfo.ID;
            this.tbInput.Text = this.roomInfo.InputCode;
            this.tbSort.Text = this.roomInfo.Sort.ToString();
            this.cmbValid.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.roomInfo.IsValid);

            this.tbOper.Text = p.Name;
            this.tbOperDate.Text = this.roomMgr.GetDateTimeFromSysDateTime().ToString();
        }
        #endregion

        #region ��ȡ�������ݣ����б���
        /// <summary>
        /// ��ȡ������Ϣ��ת��Ϊʵ��
        /// </summary>
        public void GetRoom()
        {
            if (this.RoomInfo == null) this.RoomInfo = new FS.HISFC.Models.Nurse.Room();
            //���Ҵ���
            if (this.tbRoom.Tag != null)
            {
                this.RoomInfo.ID = this.tbRoom.Tag.ToString();
            }
            //��������
            this.RoomInfo.Name = this.tbRoom.Text;
            //������
            this.RoomInfo.InputCode = this.tbInput.Text.Trim().ToString();
            //��ʾ˳��
            this.RoomInfo.Sort = FS.FrameWork.Function.NConvert.ToInt32(this.tbSort.Text);
            //�Ƿ���Ч
            this.RoomInfo.IsValid = this.cmbValid.SelectedIndex.ToString();
            //			//��ע
            //			this.RoomInfo.Memo = this.txtMemo.Text;
            //����Ա
            this.RoomInfo.User01 = this.roomMgr.Operator.ID;
            //����վ����
            if (this.roomInfo.Nurse.ID == null || this.roomInfo.Nurse.ID == "")
            {
                p = ps.GetEmployeeInfo(this.roomMgr.Operator.ID);
                this.roomInfo.Nurse.ID = p.Nurse.ID.ToString();
            }
        }

        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns></returns>
        private bool ValidData()
        {
            //��������		
            #region {94891FA8-D93C-4705-AA14-FAB414F9701A}
            string QueueName = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbRoom.Text);
            if (QueueName == "")
            {
                MessageBox.Show("�������Ʋ���Ϊ�ջ��߰��������ַ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tbRoom.Focus();
                return false;
            }
            #endregion
            if (!FS.FrameWork.Public.String.ValidMaxLengh(QueueName, 20))
            {
                MessageBox.Show("�������Ʋ��ܳ���10������");
                return false;
            }
            //������
            string inputcode = this.tbInput.Text.Trim();
            if (!FS.FrameWork.Public.String.ValidMaxLengh(inputcode, 8))
            {
                MessageBox.Show("�����벻�ܳ���8λ");
                return false;
            }
            //��ʾ˳��
            string SortId = this.tbSort.Text;
            if (SortId == "")
            {
                MessageBox.Show("��ʾ˳����Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tbSort.Focus();
                return false;
            }
            if (!this.IsNum(SortId))
            {
                MessageBox.Show("˳��ű���Ϊ����");
                this.tbSort.SelectAll();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(SortId, 4))
            {
                MessageBox.Show("��ʾ˳���ܳ���4λ");
                this.tbSort.SelectAll();
                return false;
            }
            return true;
        }
        /// <summary>
        /// �Ƿ������ͬ����
        /// </summary>
        /// <returns></returns>
        private int ValidExist()
        {
            int returnValue = this.roomMgr.QueryRoomByNameAndDept(this.roomInfo.ID, this.roomInfo.Nurse.ID,this.roomInfo.Name);
            if (returnValue < 0)
            {
                //{94891FA8-D93C-4705-AA14-FAB414F9701A}
                MessageBox.Show("��ѯ����" + this.roomInfo.Name + "����" + this.roomMgr.Err); 
                return -1;
            }
            if (returnValue >= 1)
            {
                MessageBox.Show("�Ѵ�����ͬ������");
                return -1;
            }

            return 1;
        }

        private int ValidInUsing(string roomId)
        {
            DateTime currentDT = this.roomMgr.GetDateTimeFromSysDateTime();
            int returnValue = this.roomMgr.QueryRoomByRoomID(roomId,currentDT.ToString());
            if (returnValue < 0)
            {
                MessageBox.Show(this.roomMgr.Err);
                return -1;
            }
            if(returnValue > 0)
            {
                MessageBox.Show("����������ʹ�ã������ó���Ч״̬");
                return -1;

            }
            return 1;
        }
        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsNum(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }
        /// <summary>
        /// ����
        /// </summary>
        private int SaveData()
        {
            //��֤����
            if (!this.ValidData())
            {
                return -1;
            }

            this.GetRoom();
            if (this.ValidExist() < 0)
            {
                return -1;
            }
            if (this.stateFlag.ToUpper() == "ADD")
            {
                this.roomInfo.ID = this.roomMgr.GetSequence("Nurse.Seat.GetSeq");
                if (this.ValidExist() < 0)
                {
                    return -1;
                }
                this.roomInfo.User02 = this.roomMgr.GetDateTimeFromSysDateTime().ToString();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();

                this.roomMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.roomMgr.InsertRoomInfo(this.RoomInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�������ҳ���" + this.roomMgr.Err);
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
            }
            if (this.stateFlag.ToUpper() == "EDIT")
            {
                if (this.ValidExist() < 0)
                {
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();

                this.roomMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (this.roomMgr.Update(this.RoomInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�޸����ҳ���" + this.roomMgr.Err);
                    return -1;
                }
                else
                {
                    //by niuxinyuan    ��ʱ��������̨״̬�����Ҫ�ٴ�
                    if (FS.FrameWork.Function.NConvert.ToBoolean( this.roomInfo.IsValid) == false )
                    {
                        if (this.ValidInUsing(this.roomInfo.ID) < 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                        if (this.roomMgr.UpdateSeatByRoom(this.roomInfo.ID, this.roomInfo.IsValid) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�޸���̨����" + this.roomMgr.Err);
                            return -1;
                        }
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            return 0;

        }

        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            this.tbRoom.Text = "";
            this.tbInput.Text = "";
            this.tbSort.Text = "";
        }
        #endregion 

        #region �¼��������

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SaveData() == -1)
            {
                return;
            }
            MessageBox.Show("����ɹ�!", "��ʾ");
            this.ParentForm.DialogResult = DialogResult.OK;
            this.ParentForm.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if ((!(ActiveControl is Button) && keyData == Keys.Enter))
            {
                System.Windows.Forms.SendKeys.Send("{TAB}");
                return true;
            }
            return false;
        }

        #endregion

    }
}

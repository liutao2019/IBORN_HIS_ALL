using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Nurse
{
    public partial class ucSeat : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ������
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        #region ��������ť�������

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��ӷ���", "���һ���µķ���", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�½�, true, false, null);
            this.toolBarService.AddToolButton("ɾ������", "ɾ��һ���Ѿ�����", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);
            this.toolBarService.AddToolButton("����", "������ϸ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            this.toolBarService.AddToolButton("ɾ��", "ɾ����ϸ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text.Trim())
            {
                case "��ӷ���":
                    this.AddRoom();
                    break;

                case "ɾ������":
                    this.DelRoom();
                    break;

                case "����":
                    this.AddRecord();
                    break;

                case "ɾ��":
                    this.DelRecord();
                    break;

                default:
                    break;
            }
            //base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() == -1)
            {
                return -1;
            }
            return 1;
            //return base.OnSave(sender, neuObject);
        }

        #endregion

        public ucSeat()
        {
            InitializeComponent();
        }

        private void ucSeat_Load(object sender, EventArgs e)
        {
            InitCtrl();
            this.SetFp();
        }

        #region ��ʼ��

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        public void InitCtrl()
        {
            try
            {
                RefreshRooms();
                this.SetFp();
                this.neuSpread1.ButtonDrawMode = FarPoint.Win.Spread.ButtonDrawModes.CurrentRow;
            }
            catch { }
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        public void RefreshRooms()
        {
            this.neuTreeView1.Nodes.Clear();
            FS.HISFC.Models.Base.Employee e = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
            
            //string FormSet = this.ParentForm.Tag.ToString();
            ArrayList alNurse = new ArrayList();
            FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();

            //TreeNode root = new TreeNode("��ʿվ");
            //this.neuTreeView1.Nodes.Add(root);

            //ȫ������վ������ά��
            //if (FormSet == "ALL")
            if(e.IsManager)
            {
                alNurse = this.deptMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);

                TreeNode root = new TreeNode("��ʿվ");
                this.neuTreeView1.Nodes.Add(root);

                //��ȡ��ʿվ�б�
                this.alNurse = this.deptMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
                if (alNurse != null)
                {
                    foreach (FS.HISFC.Models.Base.Department obj in alNurse)
                    {
                        TreeNode node = new TreeNode(obj.Name, 1, 1);
                        node.Tag = obj;
                        root.Nodes.Add(node);
                        //node.ContextMenuStrip = this.contextMenuStrip1;
                        //��Ӹû���վ������
                        ArrayList alrooms = roomMgr.GetRoomInfoByNurseNo(obj.ID);
                        if (alrooms != null)
                        {
                            foreach (FS.HISFC.Models.Nurse.Room room in alrooms)
                            {
                                TreeNode node2 = new TreeNode(room.Name, 0, 0);
                                node2.Tag = room;
                                node2.ContextMenuStrip = this.contextMenuStrip1;
                                node.Nodes.Add(node2);
                            }
                        }
                    }
                    root.Expand();
                }
                else
                {
                    MessageBox.Show("û�л�û���վ�б�!");
                    return;
                }
            }
            else//ֻ��ά���Լ����ڵĻ���վ
            {
                //��ʼ�������б�
                ArrayList alCurrent = this.deptMgr.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);
                TreeNode node = new TreeNode();
                foreach (FS.HISFC.Models.Base.Department obj in alCurrent)
                {
                    //if (obj.ID.Trim().Equals(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID))
                    if (obj.ID.Trim().Equals(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID))
                    {
                        node.SelectedImageIndex = 1;
                        node.ImageIndex = 1;
                        node.Text = obj.Name;
                        node.Tag = obj;
                        this.neuTreeView1.Nodes.Add(node);
                        //node.ContextMenuStrip=this.contextMenuStrip1;
                        break;
                    }
                }

                //��Ӹû���վ������
                //ArrayList alrooms = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
                ArrayList alrooms = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
                if (alrooms != null)
                {
                    foreach (FS.HISFC.Models.Nurse.Room room in alrooms)
                    {
                        TreeNode node2 = new TreeNode(room.Name, 0, 0);
                        node2.Tag = room;
                        node2.ContextMenuStrip = this.contextMenuStrip1;
                        node.Nodes.Add(node2);
                    }
                    this.neuTreeView1.ExpandAll();
                }
                //TreeNode root = new TreeNode(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.Name);
                //this.neuTreeView1.Nodes.Add(root);

                ////ArrayList alrooms = roomMgr.GetRoomInfoByNurseNo(FS.FrameWork.Management.Connection.Operator.ID);
                //ArrayList alrooms = roomMgr.GetRoomInfoByNurseNo(((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID);
                //if (alrooms != null)
                //{
                //    foreach (FS.HISFC.Models.Nurse.Room room in alrooms)
                //    {
                //        TreeNode node = new TreeNode(room.Name, 1, 1);
                //        node.Tag = room;
                //        root.Nodes.Add(node);
                //    }
                //    this.neuTreeView1.ExpandAll();
                //}
            }
        }

        #endregion

        #region ����

        #region �����ҵĲ���

        /// <summary>
        /// �޸�����
        /// </summary>
        private void ModifyRoom()
        {
            TreeNode node = this.neuTreeView1.SelectedNode;
            if (node == null || node.Tag == null)
            {
                MessageBox.Show("��ѡ��һ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Room)) return;

            FS.HISFC.Models.Nurse.Room info = node.Tag as FS.HISFC.Models.Nurse.Room;
            Nurse.ucRoom ucRoom1 = new ucRoom();
            ucRoom1.StateFlag = "EDIT";
            ucRoom1.RoomInfo = info;
            
            ucRoom1.init();
            if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucRoom1) == System.Windows.Forms.DialogResult.OK)
            {
                //this.RefreshRooms();
                //this.neuTreeView1.SelectedNode = node;
                //node.Expand();
                node.Text = ucRoom1.RoomInfo.Name;
                if (ucRoom1.RoomInfo.IsValid == "0")
                {
                    this.SetFp("ͣ��");
                }
            }
            this.SetFp();
        }

        /// <summary>
        /// �������
        /// </summary>
        private void AddRoom()
        {
            Nurse.ucRoom ucRoom1 = new ucRoom();
            ucRoom1.StateFlag = "ADD";

            TreeNode node = this.neuTreeView1.SelectedNode;
            if (node.Level == 0)
            {
                return;
            }
            if (node == null/*node.Tag == null*/)
            {
                MessageBox.Show("��ѡ��һ������վ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (node.Tag.GetType() != typeof(FS.HISFC.Models.Base.Department)) return;

            FS.HISFC.Models.Base.Department info = node.Tag as FS.HISFC.Models.Base.Department;
            ucRoom1.RoomInfo.Nurse = info;
            ucRoom1.init();

            if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucRoom1) == System.Windows.Forms.DialogResult.OK)
            {
                TreeNode node2 = new TreeNode(ucRoom1.RoomInfo.Name, 0, 0);
                node2.Tag = ucRoom1.RoomInfo;
                node2.ContextMenuStrip = this.contextMenuStrip1;
                node.Nodes.Add(node2);
               
 
                node.Expand();
            }
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        private void DelRoom()
        {
            TreeNode node = this.neuTreeView1.SelectedNode;
            if (node == null || node.Tag == null || node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Room))
            {
                MessageBox.Show("��ѡ��һ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FS.HISFC.Models.Nurse.Room info = node.Tag as FS.HISFC.Models.Nurse.Room;
            if (MessageBox.Show("ɾ�����ҽ�ͬʱɾ�����������̨,�Ƿ�ȷ��ɾ��:" + info.Name + "?",
                "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No) return;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            roomMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            seatMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            try
            {
                //�ж������Ƿ��Ű�
                int result = roomMgr.QueryRoom(info.ID, roomMgr.GetSysDate());
                if (result < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��ѯ����ʧ��" + roomMgr.Err);

                    return;
                }
                if (result >= 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���Ժ��ʱ����������ڶ���ά�����Ѿ���ά��������ɾ��");

                    return;
                }

                //
               

                if (roomMgr.DelRoomInfo(info.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ������ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + roomMgr.Err, "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (this.seatMgr.DeleteByRoom(info.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ����̨ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + this.seatMgr.Err, "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ������ʧ�ܣ�" + e.Message, "��ʾ");
                return;
            }
            this.neuTreeView1.Nodes.Remove(node);
            MessageBox.Show("ɾ���ɹ�!", "��ʾ");
        }

        #endregion

        #region ����̨�Ĳ���
        /// <summary>
        /// ����״̬
        /// </summary>
        /// <param name="flagStr">ͣ�û�����</param>
        private void SetFp(string flagStr)
        {
            int RowCount = this.neuSpread1_Sheet1.RowCount;
            if (RowCount <= 0) return;
            for (int i = 0; i < RowCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 3].Value = flagStr;
            }
        }

        /// <summary>
        /// ������̨��¼
        /// </summary>
        private void AddRecord()
        {
            TreeNode node = this.neuTreeView1.SelectedNode;
            if (node == null || node.Tag == null) return;
            if (node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Room)) return;

            FS.HISFC.Models.Nurse.Room room = node.Tag as FS.HISFC.Models.Nurse.Room;

            if (room.IsValid.Trim().Equals("0"))
            {
                if (MessageBox.Show("��" + room.Name + "���Ѿ�ͣ��," + "�Ƿ���������̨?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }
            }

            this.neuSpread1.StopCellEditing();

            string strNewNo = this.seatMgr.GetSequence("Nurse.Seat.GetSeq");
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            int row = this.neuSpread1_Sheet1.RowCount - 1;
            this.neuSpread1_Sheet1.ActiveRowIndex = row;
            this.neuSpread1_Sheet1.Cells[row, 0].Tag = strNewNo;
            this.neuSpread1_Sheet1.Cells[row, 2].Value = room.Name;
            this.neuSpread1_Sheet1.Cells[row, 2].Tag = room.ID;
            this.neuSpread1_Sheet1.Rows[row].Tag = room.Nurse.ID;

            if (room.IsValid.Trim().Equals("1"))
            {
                this.neuSpread1_Sheet1.Cells[row, 3].Value = "����";
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[row, 3].Value = "ͣ��";
            }

            FarPoint.Win.Spread.CellType.TextCellType txt = new FarPoint.Win.Spread.CellType.TextCellType();
            txt.ReadOnly = true;
            txt.StringTrim = System.Drawing.StringTrimming.EllipsisCharacter;
            this.neuSpread1_Sheet1.Cells[row, 2].CellType = txt;

            this.IsModified = true;
            this.neuSpread1.Focus();
        }

        /// <summary>
        /// ɾ����̨��¼
        /// </summary>
        private void DelRecord()
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)//δѡ����¼
            {
                MessageBox.Show("��ѡ��Ҫɾ������̨", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string strSeatName = "";//��������
            string strSeatID = "";
            strSeatID = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRow.Index, 0].Tag.ToString();
            if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRow.Index, 0].Text == "")
                strSeatName = "";
            else
                strSeatName = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRow.Index, 0].Value.ToString();
            DialogResult result;
            result = MessageBox.Show("�Ƿ�ȷ��ɾ����̨ " + strSeatName, "��ʾ",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No) return;

           
            //���ݿ�����

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //trans.BeginTransaction();

            this.seatMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //�ж������Ƿ��Ű�

            int result1 = this.seatMgr.QueryConsole(strSeatID, this.seatMgr.GetSysDate());
            if (result1 < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��ѯ��̨ʧ��" + this.seatMgr.Err);

                return;
            }
            if (result1 >= 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���Ժ��ʱ�������̨�ڶ���ά�����Ѿ���ά��������ɾ��");

                return;
            }

            try
            {
                if (this.seatMgr.DeleteByConsole(strSeatID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ����̨ʧ�ܣ�\n����ϵͳ����Ա��ϵ��" + this.seatMgr.Err, "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ����̨ʧ�ܣ�" + e.Message, "��ʾ");
                return;
            }
            this.neuSpread1_Sheet1.ActiveRow.Remove();
        }

        /// <summary>
        /// �ж��Ƿ�����ͬ��¼
        /// </summary>
        /// <returns></returns>
        private int ValidSameValue()
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount - 1; i++)
            {
                for (int j = i + 1; j < this.neuSpread1_Sheet1.RowCount  ;j++ )
                {
                    if (this.neuSpread1_Sheet1.Cells[i, 0].Text == this.neuSpread1_Sheet1.Cells[j, 0].Text)// &&
                       // this.neuSpread1_Sheet1.Cells[i, 1].Text == this.neuSpread1_Sheet1.Cells[j, 1].Text)
                    {
                        MessageBox.Show("�Ѿ�������ͬ��̨");
                        return -1;
                    }

                }
            }
            return 1;
        }
        /// <summary>
        /// ��Ч���ж�
        /// </summary>
        /// <returns></returns>
        private int Valid()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0) return -1;
            if (this.ValidSameValue() == -1) return -1;
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    //��̨���� {B8E0377D-1F45-4077-B168-E38E76C5A2D7}
                    string strTemp = this.neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                    if (strTemp == null || strTemp == "")
                    {
                        MessageBox.Show("��̨���Ʋ���Ϊ��!", "��ʾ");
                        return -1;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(strTemp, 20))
                    {
                        MessageBox.Show("��̨���Ʋ��ܳ���10������!", "��ʾ");
                        return -1;
                    }
                    //������
                    strTemp = this.neuSpread1_Sheet1.Cells[i, 1].Text.Trim();
                    if (strTemp == null || strTemp == "")
                    {
                        MessageBox.Show("�����벻��Ϊ��!", "��ʾ");
                        return -1;
                    }
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(strTemp, 20))
                    {
                        MessageBox.Show("�����벻�ܳ���8������!", "��ʾ");
                        return -1;
                    }

                    //��ע
                    string strMemo = this.neuSpread1_Sheet1.Cells[i, 4].Text.Trim();
                    if (strMemo != null && !FS.FrameWork.Public.String.ValidMaxLengh(strMemo, 50))
                    {
                        MessageBox.Show("��ע���Ȳ��ܳ���25������!", "��ʾ");
                        return -1;
                    }


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <return>0 success, -1 fail</return>
        public int Save()
        {
            this.neuSpread1.StopCellEditing();

            if (this.neuSpread1_Sheet1.RowCount <= 0)
            {
                MessageBox.Show("û��Ҫ���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }

            

            if (this.Valid() == -1) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            this.seatMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    #region ��������ʵ��
                    FS.HISFC.Models.Nurse.Seat info = new FS.HISFC.Models.Nurse.Seat();
                    info.ID = this.neuSpread1_Sheet1.Cells[i, 0].Tag.ToString();
                    info.Name = this.neuSpread1_Sheet1.GetValue(i, 0).ToString();
                    info.PRoom.InputCode = this.neuSpread1_Sheet1.GetValue(i, 1).ToString();
                    info.PRoom.ID = this.neuSpread1_Sheet1.Cells[i, 2].Tag.ToString();
                    info.PRoom.Name = this.neuSpread1_Sheet1.GetValue(i, 2).ToString();
                    //״̬
                    string strTemp = "1";
                    if (this.neuSpread1_Sheet1.GetValue(i, 3).ToString().Trim() == "ͣ��")
                    {
                        strTemp = "0";
                    }
                    info.PRoom.IsValid = strTemp;
                    //��ע
                    string strMemo = this.neuSpread1_Sheet1.Cells[i, 4].Text.Trim();
                    if (strMemo == null) strMemo = "";
                    info.Memo = strMemo;
                    //������Ϣ
                    info.Oper.ID= this.seatMgr.Operator.ID;
                    info.Oper.OperTime = this.seatMgr.GetDateTimeFromSysDateTime();



                   
                    #endregion

                    if (!FS.FrameWork.Function.NConvert.ToBoolean(info.PRoom.IsValid))
                    {
                        FS.HISFC.BizLogic.Nurse.Assign assMgr = new FS.HISFC.BizLogic.Nurse.Assign();
                        assMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        if (assMgr.ExistPatient(info.ID, assMgr.GetSysDateTime()))
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(info.Name + " �л���,���ܽ�״̬��Ϊ��Ч", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.neuSpread1_Sheet1.SetValue(i, 3, "����");
                            return -1;
                        }

                        //if (this.seatMgr.QuerySeatByConsoleID(info.ID) < 0)
                        //{

                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show(this.seatMgr.Err);
                        //    return -1;
                        //}
                        //if (this.seatMgr.QuerySeatByConsoleID(info.ID) > 0)
                        //{

                        //    FS.FrameWork.Management.PublicTrans.RollBack();
                        //    MessageBox.Show("����̨����ʹ�ò����ó���Ч");
                        //    return -1;
                        //}

                       
                    }


                    if (this.seatMgr.Insert(info) == -1)
                    {
                        if (this.seatMgr.Update(info) == -1)
                        {

                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����ʧ��!" + this.seatMgr.Err, "��ʾ");
                            return -1;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����ʧ��!" + e.Message, "��ʾ");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����ɹ�!", "��ʾ");

            this.SetFp();

            return 0;
        }

        #endregion

        #endregion

        #region  ����

        /// <summary>
        /// �ж��Ƿ�������
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private bool IsNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsNumber(str, i))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// ����farpoint��ʽ
        /// </summary>
        private void SetFp()
        {
            FarPoint.Win.Spread.CellType.TextCellType txtOnly = new FarPoint.Win.Spread.CellType.TextCellType();
            txtOnly.ReadOnly = true;
            this.neuSpread1_Sheet1.Columns[2].CellType = txtOnly;
            this.neuSpread1_Sheet1.Columns[2].Visible = false;
        }

        #endregion

        #region �¼��������

        private void neuTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.neuTreeView1.SelectedNode = e.Node;
                
            }
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                this.neuSpread1_Sheet1.RemoveRows(0, this.neuSpread1_Sheet1.RowCount);
            }

            if (e.Node.Tag == null) return;
            //û�з�����������
            if (e.Node.Tag.GetType() != typeof(FS.HISFC.Models.Nurse.Room)) return;


            FS.HISFC.Models.Nurse.Room room = (FS.HISFC.Models.Nurse.Room)e.Node.Tag;
            ArrayList al = new ArrayList();
            al = this.seatMgr.Query(room.ID);
            if (al == null || al.Count <= 0) return;


            foreach (FS.HISFC.Models.Nurse.Seat seat in al)
            {
                this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
                int row = this.neuSpread1_Sheet1.RowCount - 1;

                this.neuSpread1_Sheet1.Cells[row, 0].Tag = seat.ID;
                this.neuSpread1_Sheet1.SetValue(row, 0, seat.Name);
                this.neuSpread1_Sheet1.SetValue(row, 1, seat.PRoom.InputCode);
                this.neuSpread1_Sheet1.SetValue(row, 2, room.Name);
                this.neuSpread1_Sheet1.Cells[row, 2].Tag = room.ID;
                if (seat.PRoom.IsValid == "1")
                {
                    this.neuSpread1_Sheet1.Cells[row, 3].Value = "����";
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[row, 3].Value = "ͣ��";
                }
                this.neuSpread1_Sheet1.SetValue(row, 4, seat.PRoom.Memo);
            }
        }

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.neuSpread1_Sheet1.ActiveColumnIndex < 4)
                {
                    this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex,
                        this.neuSpread1_Sheet1.ActiveColumnIndex + 1);
                }
                if (this.neuSpread1_Sheet1.ActiveColumnIndex == 4
                    && this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount - 1)
                {
                    this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex + 1, 0);
                }
            }
        }

        private void neuSpread1_ComboCloseUp(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 3)
            {
                this.neuSpread1_Sheet1.SetActiveCell(this.neuSpread1_Sheet1.ActiveRowIndex,
                    this.neuSpread1_Sheet1.ActiveColumnIndex + 1);
            }
        }

        /// <summary>
        /// �Ҽ��˵�-�޸�  �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            TreeNode node = this.neuTreeView1.SelectedNode;
            if (node.Parent == null)
            {
                MessageBox.Show("��ѡ��һ����̨");
                return;
            }
            ModifyRoom();
        }

        /// <summary>
        /// �Ҽ��˵�-ɾ�� �������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            TreeNode node = this.neuTreeView1.SelectedNode;
            if (node.Parent == null)
            {
                MessageBox.Show("��ѡ��һ����̨");
                return;
            }
            DelRoom();
        }

        #endregion

        #region ������

        private FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        public FS.HISFC.BizLogic.Nurse.Seat seatMgr = new FS.HISFC.BizLogic.Nurse.Seat();
        public FS.HISFC.BizLogic.Nurse.Room roomMgr = new FS.HISFC.BizLogic.Nurse.Room();
        private FS.HISFC.Models.Nurse.Seat seatInfo = new FS.HISFC.Models.Nurse.Seat();

        private ArrayList alConsole = new ArrayList();//��ѡ������������̨�����б�
        private ArrayList alNurse = new ArrayList();

        public bool IsModified = false;//�༭״̬
        public delegate void RefRoom();

        #endregion
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Nurse.Controls.SDFY
{
    /// <summary>
    /// ����·������
    /// </summary>
    internal partial class ucAddQueue : UserControl
    {
        public ucAddQueue()
        {
            InitializeComponent();
        }

        public ucAddQueue(cResult r)
            : this()
		{
			this.myResult = r;				
		}

        #region ������myQueue

        /// <summary>
        /// ȡ(ADD,EDIT)ֵ
        /// </summary>
        private string stateFlag = "ADD";

        private cResult myResult = new cResult();
        /// <summary>
        /// ҽ���Ű������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Registration.Registration myMgr = null;
        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager personMgr = null;

        private FS.HISFC.BizProcess.Integrate.Manager depMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ���й�����
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Queue myQueue = null;
     
        /// <summary>
        /// ���ҹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Room myRoom = null;
       
        /// <summary>
        /// ����ʵ��
        /// </summary>
        private FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();

        private FS.HISFC.Models.Nurse.Queue oldQuery = new FS.HISFC.Models.Nurse.Queue();
        private FS.HISFC.Models.Nurse.Queue myOldQueue = new FS.HISFC.Models.Nurse.Queue();
        /// <summary>
        /// ��̨������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Seat seatMgr = new FS.HISFC.BizLogic.Nurse.Seat();

        private ArrayList al = new ArrayList();

        #region add by sunm 0925
        private FS.FrameWork.Public.ObjectHelper doctHelper = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        #endregion	

        #region �¼�

        public delegate void ClickSave(FS.FrameWork.Models.NeuObject obj);
        public event ClickSave RefList;

        #endregion

        #region ����

        /// <summary>
        /// ����ʵ��
        /// </summary>
        public FS.HISFC.Models.Nurse.Queue Queue
        {
            get { return this.queue; }
            set { this.queue = value; }
        }

        /// <summary>
        /// ȡ(ADD,EDIT)ֵ
        /// </summary>
        public string StateFlag
        {
            get { return this.stateFlag; }
            set { this.stateFlag = value; }
        }

        #region {B3E7633A-D9FB-492f-9D62-D2F7188D5643}
        DialogResult myDiagResult = DialogResult.Cancel;
        /// <summary>
        /// ���ڷ���
        /// </summary>
        public DialogResult AddQueDiagResult
        {
            get
            {
                return this.myDiagResult;
            }
        }
        #endregion

        #region {E695812E-9C9A-4f16-90AA-83ADACC14583}
        private bool isNeedDoct = false;

        /// <summary>
        /// �Ƿ��ѡҽ��
        /// </summary>
        public bool IsNeedDoctor
        {
            get
            {
                return this.isNeedDoct;
            }
            set
            {
                this.isNeedDoct = value;
            }
        }
        #endregion

        #endregion


        #region ��ʼ��

        /// <summary>
        /// ���ݴ��������ʼ��
        /// </summary>
        private void Init()
        {
            if (this.myResult.strTab.ToUpper() == "ADD")
            {
                this.Add();
            }
            if (this.myResult.strTab.ToUpper() == "EDIT")
            {
                this.Edit();
            }
            Initcbo();
            this.SetQueue();
            this.dtQueue.Focus();

            #region {E695812E-9C9A-4f16-90AA-83ADACC14583}
            if (this.isNeedDoct)
            {
                this.neuLabel6.ForeColor = Color.Blue;
            }
            else
            {
                this.neuLabel6.ForeColor = Color.Black;
            }
            #endregion
        }

        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private void Initcbo()
        {
            //��ʼ�����
            if (this.myMgr == null) this.myMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            al = this.myMgr.Query();
            if (al == null) al = new ArrayList();
            this.cmbNoon.AddItems(al);
            //��ʼ���������
            FS.HISFC.BizProcess.Integrate.Manager ps = new FS.HISFC.BizProcess.Integrate.Manager();
            //FS.HISFC.BizLogic.Manager.Person ps = new FS.HISFC.BizLogic.Manager.Person();
            //FS.HISFC.Models.RADT.Person p = new FS.HISFC.Models.RADT.Person();
            FS.HISFC.Models.Base.Employee p = new FS.HISFC.Models.Base.Employee();
            p = ps.GetEmployeeInfo(this.seatMgr.Operator.ID);
            //al = this.depMgr.QueryDepartment(this.myResult.Nurse.ID);//
            al = this.depMgr.QueryDepartmentForArray (this.myResult.Nurse.ID);
            if (al != null || al.Count > 0)
            {
                this.cmbAssignDept.ClearItems();
                this.cmbAssignDept.AddItems(al);
                this.cmbAssignDept.IsListOnly = true;
            }
            //��ʼ������ҽ��
            if (this.personMgr == null) this.personMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //�õ�ҽ���б�
            al = this.personMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null) al = new ArrayList();
            this.cmbDoct.AddItems(al);
            this.cmbDoct.BringToFront();
            this.cmbDoct.IsListOnly = true;
            #region add by sunm 0925
            this.doctHelper.ArrayObject = al;
            #endregion
            //���ز�������
            if (this.myRoom == null) this.myRoom = new FS.HISFC.BizLogic.Nurse.Room();
            al = new ArrayList();
            al = this.myRoom.GetRoomInfoByNurseNoValid(this.Queue.Dept.ID);
            if (al != null)��
            {
                ArrayList b = new ArrayList();

                foreach (FS.HISFC.Models.Nurse.Room obj in al)
                {
                    FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

                    dept.ID = obj.ID;
                    dept.Name = obj.Name;
                    dept.UserCode = obj.InputCode;

                    b.Add(dept);
                }

                this.cmbRoom.AddItems(b);
                this.cmbRoom.IsListOnly = true;
            }
            //������Ч״̬
            al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            obj1.ID = "1";
            obj1.Name = "��Ч";
            al.Add(obj1);
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "0";
            obj2.Name = "��Ч";
            al.Add(obj2);
            this.cmbValid.AddItems(al);
            this.cmbValid.SelectedIndex = 0;
            //���ض������
            al = new ArrayList();
            FS.FrameWork.Models.NeuObject obj3 = new FS.FrameWork.Models.NeuObject();
            obj3.ID = "1";
            obj3.Name = "ҽ������";
            al.Add(obj3);
            FS.FrameWork.Models.NeuObject obj4 = new FS.FrameWork.Models.NeuObject();
            obj4.ID = "2";
            obj4.Name = "�Զ������";
            al.Add(obj4);
            this.cmbQueueType.AddItems(al);
            //����Ա��Ϣ
            this.tbOper.Text = p.Name;
            this.tbOper.Tag = p.ID;
            this.tbOperDate.Text = this.seatMgr.GetDateTimeFromSysDateTime().ToString();

            this.dtQueue.Focus();
        }

        #endregion

        #region ����

        private void Add()
        {
            this.myResult.strTab = "ADD";
            this.dtQueue.Focus();
        }

        private void Edit()
        {
            this.myResult.strTab = "EDIT";
            this.dtQueue.Focus();
        }

        private void Clear()
        {
            this.tbQueueName.Text = "";
            this.cmbQueueType.Text = "";
            this.cmbDoct.Text = "";
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

        private bool ValidData()
        {
           // if (this.dtQueue.Value.Date.CompareTo(DateTime.Now.Date) < 0 )
            if (this.dtQueue.Value.Date.CompareTo(FS.FrameWork.Function.NConvert.ToDateTime((this.seatMgr.GetSysDate())).Date)< 0)
            {
                MessageBox.Show("���ܱ���С�ڵ�ǰ���ڵĶ�����Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dtQueue.Focus();
                return false;
            }
            //���Ҳ���Ϊ��
            string strRoom = this.cmbRoom.Text;
            if (strRoom == "")
            {
                MessageBox.Show("���Ҳ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbRoom.Focus();
                return false;
            }
            //��̨����Ϊ��
            string strSeat = this.cmbConsole.Text;
            if (strSeat == "")
            {
                MessageBox.Show("��̨����Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbConsole.Focus();
                return false;
            }
            //������� {FE3F7C68-0CD6-4a3c-8878-920542863F2F}
            if (this.cmbAssignDept.Text.ToString().Trim() == null || this.cmbAssignDept.Text.ToString().Trim() == "")
            {
                MessageBox.Show("������Ҳ���Ϊ��!", "��ʾ");
                this.cmbAssignDept.Focus();
                return false;
            }
            //��������		
            #region {A5165913-94E7-428a-86D4-CE0442697D96}
            string QueueName = FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbQueueName.Text);
            this.tbQueueName.Text = QueueName; 
            #endregion
            if (QueueName == "")
            {
                MessageBox.Show("�������Ʋ���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tbQueueName.Focus();
                return false;
            }
            else if (!FS.FrameWork.Public.String.ValidMaxLengh(QueueName, 40))
            {
                MessageBox.Show("�������ƹ���");
                this.tbQueueName.Focus();
                this.tbQueueName.SelectAll();
                return false;
            }
           
            //��ʾ˳��
            string SortId = this.tbSort.Text;
            if (SortId == "")
            {
                MessageBox.Show("ҽ����Ų���Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.tbSort.Focus();
                return false;
            }
            if (!this.IsNum(SortId))
            {
                MessageBox.Show("ҽ����ű���Ϊ����");
                this.tbSort.Focus();
                this.tbSort.SelectAll();
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(SortId, 4))
            {
                MessageBox.Show("ҽ����Ź���");
                this.tbSort.SelectAll();
                return false;
            }
            //��ע
            string Memo = this.tbMemo.Text;
            if (!FS.FrameWork.Public.String.ValidMaxLengh(Memo, 100))
            {
                MessageBox.Show("��ע����");
                return false;
            }
            //���
            string strNoon = this.cmbNoon.Text;
            if (strNoon == "")
            {
                MessageBox.Show("�����Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cmbNoon.Focus();
                return false;
            }

            #region {E695812E-9C9A-4f16-90AA-83ADACC14583}
            if (this.isNeedDoct)
            {
                if (string.IsNullOrEmpty(this.cmbDoct.Text.Trim()))
                {
                    MessageBox.Show("ҽ������Ϊ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.cmbDoct.Focus();
                    return false;
                }
            } 
            #endregion

            return true;
        }
        /// <summary>
        /// �ж�������̨�Ѿ�����������ʹ��
        /// </summary>
        /// <param name="myQueue">�������ʵ��</param>
        /// <returns></returns>
        private bool ValidUsed(FS.HISFC.Models.Nurse.Queue myQueue)
        {
            if (this.queue.User03 == "0")
            {
                return true;
            }

            bool returnValue = this.myQueue.QueryUsed(myQueue.Console.ID,myQueue.Noon.ID,myQueue.QueueDate.ToString());
            if (returnValue == false)
            {
                MessageBox.Show(this.myQueue.Err);
                return false;
            }
            return true;
        }

        private int ValidInUsing(string QueueID)
        {
            DateTime currentDT = this.myQueue.GetDateTimeFromSysDateTime();
            int returnValue = this.myQueue.QueryQueueByQueueID(QueueID,currentDT.ToString());
            if (returnValue < 0)
            {
                MessageBox.Show(this.myQueue.Err);
                return -1;
            }
            if (returnValue > 0)
            {
                MessageBox.Show("�ö�������ʹ�ã������ó���Ч״̬");
                return -1;

            }
            return 1;
        }
        private int ValidModify(FS.HISFC.Models.Nurse.Queue OldQueue, FS.HISFC.Models.Nurse.Queue Queue)
        {
            if (    OldQueue.QueueDate      != Queue.QueueDate
                ||  OldQueue.SRoom.ID       != Queue.SRoom.ID
                ||  OldQueue.Console.ID     != Queue.Console.ID
                ||  OldQueue.AssignDept.ID  != Queue.AssignDept.ID
                ||  OldQueue.Doctor.ID      != Queue.Doctor.ID
                ||  OldQueue.ExpertFlag     != Queue.ExpertFlag
                ||  OldQueue.Name           != Queue.Name
                ||  OldQueue.User01         != Queue.User01
                ||  OldQueue.Noon.ID        != Queue.Noon.ID
                ||  OldQueue.Order          != Queue.Order
                ||  OldQueue.IsValid        != Queue.IsValid
                ||  OldQueue.Memo           != Queue.Memo
                )
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ��������
        /// </summary>
        private int SaveData()
        {
            //��֤����
            if (!this.ValidData())
            {
                return -1;
            }
            if (myQueue == null) this.myQueue = new FS.HISFC.BizLogic.Nurse.Queue();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(myQueue.Connection);
            //trans.BeginTransaction();

            myQueue.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.GetQueue();
            if (this.myResult.strTab.ToUpper() == "ADD")
            {
                if (this.IsExistsQueue())
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��ͬ�Ķ����Ѿ�����");
                    return -1;
                }
                //�ж��Ƿ�ʹ��
                if (this.ValidUsed(this.Queue) == false)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
                if (this.myQueue.InsertQueue(this.Queue) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("������г���" + this.myQueue.Err);
                    return -1;
                }
            }
            if (this.myResult.strTab.ToUpper() == "EDIT")
            {
                //if (this.myQueue.ExistPatient(this.queue.SRoom.ID, this.queue.Console.ID, this.queue.ID, this.queue.Noon.ID))
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("�������л��ߣ������޸�!");
                //    return -1;
                //}
                //if (this.myQueue.UpdateQueue(this.Queue) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("�޸Ķ��г���" + this.myQueue.Err);
                //    return -1;
                //}
                //if (this.IsExistsQueue())
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("��ͬ�Ķ����Ѿ�����");
                //    return -1;
                //}
                if (this.ValidModify(this.myOldQueue, this.queue) == 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��¼û�иı䣬���豣��!");
                    return -1;
                }

                if (this.ValidUsed(this.queue) == false)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }

                if (this.myQueue.ExistPatient(this.myOldQueue.SRoom.ID, this.myOldQueue.Console.ID, this.myOldQueue.ID, this.myOldQueue.Noon.ID))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�������л��ߣ������޸�!");
                    return -1;
                }
                if (this.queue.IsValid == false)
                {
                    if (this.ValidInUsing(this.Queue.ID) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                }


                if (this.myQueue.UpdateQueue(this.Queue) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�޸Ķ��г���" + this.myQueue.Err);
                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;

        }

        /// <summary>
        /// �ж϶��д���
        /// </summary>
        /// <returns>false:������;  true����</returns>
        private bool IsExistsQueue()
        {

            if (this.queue.Doctor.ID != null && this.queue.Doctor.ID != "")
            {
                for (int i = 0, j = this.myResult.QueueList.Count; i < j; i++)
                {
                    if (this.queue.Noon.ID == this.myResult.QueueList[i].Noon.ID &&
                        this.queue.QueueDate.Date == this.myResult.QueueList[i].QueueDate.Date &&
                        this.queue.AssignDept.ID == this.myResult.QueueList[i].AssignDept.ID &&
                        this.queue.Doctor.ID == this.myResult.QueueList[i].Doctor.ID &&
                        this.queue.Console.ID == this.myResult.QueueList[i].Console.ID &&
                        this.queue.IsValid == this.myResult.QueueList[i].IsValid)
                    {
                        return true;
                    }
                }
            }
            
            if (this.queue.Doctor.ID == null || this.queue.Doctor.ID == "")
            {
                for (int i = 0, j = this.myResult.QueueList.Count; i < j; i++)
                {
                    if (this.queue.Noon.ID == this.myResult.QueueList[i].Noon.ID &&
                        this.queue.QueueDate.Date == this.myResult.QueueList[i].QueueDate.Date &&
                        //this.queue.AssignDept.ID == this.myResult.QueueList[i].AssignDept.ID &&
                        this.queue.Console.ID == this.myResult.QueueList[i].Console.ID &&
                        this.queue.IsValid == this.myResult.QueueList[i].IsValid &&
                        (this.myResult.QueueList[i].Doctor.ID == null || this.myResult.QueueList[i].Doctor.ID == "")
                        )
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// ��ʵ�帴�Ƶ��ؼ�
        /// </summary>
        public void SetQueue()
        {
            FS.HISFC.BizProcess.Integrate.Manager ps = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Employee p = new FS.HISFC.Models.Base.Employee();
            p = ps.GetEmployeeInfo(this.seatMgr.Operator.ID);

            //��ʿվ
            this.tbDept.Text = this.Queue.Dept.Name;
            this.tbDept.Tag = this.Queue.Dept.ID;
            //���
            this.cmbNoon.Tag = this.Queue.Noon.ID;
            this.cmbNoon.Text = this.Queue.Noon.Name;
            //�������
            if (this.queue.AssignDept.ID != null && this.queue.AssignDept.ID != "")
            {
                this.cmbAssignDept.Tag = this.queue.AssignDept.ID;
                this.cmbAssignDept.Text = this.queue.AssignDept.Name;
            }
            else
            {
                //this.cmbAssignDept.Tag = p.Dept.ID;
                //this.cmbAssignDept.Text = this.depMgr.GetDeptmentById(p.Dept.ID).Name;
            }
            //����ҽ��
            this.cmbDoct.Tag = this.Queue.Doctor.ID;
            //��������
            this.dtQueue.Value = this.Queue.QueueDate;

            //��������
            //if (this.cmbDoct.Text != "" && this.cmbDoct.Tag != null)
            //{
            //    this.cmbQueueType.Text = "ҽ������";
            //}
            //else
            //{
            //    this.cmbQueueType.Text = "�Զ������";
            //}

            //��������[2007/03/27]
            if (this.queue.User01.Trim() == "1")
            {
                this.cmbQueueType.Text = "ҽ������";
            }
            else
            {
                this.cmbQueueType.Text = "�Զ������";

            }
            //��ʾ˳��
            this.tbSort.Text = this.Queue.Order.ToString();
            //�Ƿ���Ч
            if (this.Queue.IsValid)
            {
                this.cmbValid.Tag = "1";
            }
            else
            {
                this.cmbValid.Tag = "0";
            }
            this.cmbRoom.Tag = this.Queue.SRoom.ID;
            
            this.cmbExport.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.Queue.ExpertFlag);

            //��������
            this.tbQueueName.Text = this.Queue.Name;
            //���б�ʶ
            this.tbQueueName.Tag = this.Queue.ID;
            //����Ա��Ϣ
            this.tbOper.Text = p.Name;
            this.tbOper.Tag = p.ID;
            this.tbOperDate.Text = this.seatMgr.GetDateTimeFromSysDateTime().ToString();
            
            this.cmbConsole.Tag = this.Queue.Console.ID;
            this.cmbConsole.Text = this.Queue.Console.Name;
            //��ע
            this.SetBookNumFromMemo(this.Queue.Memo);
            this.tbMemo.Text = this.Queue.Memo;
        }

        public void GetQueue()
        {
            if (this.Queue == null) this.Queue = new FS.HISFC.Models.Nurse.Queue();
            //������
            if (this.tbQueueName.Tag != null)
            {
                //���б�ʶ
                this.Queue.ID = this.tbQueueName.Tag.ToString();
            }
            this.Queue.Name = this.tbQueueName.Text;
            if (this.tbDept.Tag != null)
            {
                this.Queue.Dept.ID = this.tbDept.Tag.ToString();
            }

            this.Queue.Dept.Name = this.tbDept.Text;
            //��������
            this.Queue.QueueDate = this.dtQueue.Value;
            //�������
            this.queue.AssignDept.ID = this.cmbAssignDept.Tag.ToString();
            this.queue.AssignDept.Name = this.cmbAssignDept.Text.ToString();
            //����ҽ��
            if (this.cmbDoct.SelectedIndex != -1) this.Queue.Doctor.ID = this.cmbDoct.SelectedItem.ID;
            //�������
            if (this.cmbQueueType.SelectedIndex != -1) this.Queue.User01 = this.cmbQueueType.SelectedItem.ID;
            //���
            if (this.cmbNoon.SelectedIndex != -1) this.Queue.Noon.ID = this.cmbNoon.SelectedItem.ID;
            //��ʾ˳��
            this.Queue.Order = FS.FrameWork.Function.NConvert.ToInt32(this.tbSort.Text);
            //�Ƿ���Ч
            this.Queue.IsValid = this.cmbValid.SelectedItem.ID == "1" ? true : false;
            if (this.cmbRoom.SelectedIndex != -1)
            {
                this.Queue.SRoom.ID = this.cmbRoom.SelectedItem.ID;
                this.Queue.SRoom.Name = this.cmbRoom.SelectedItem.Name;
            }
            //��ע
            this.tbMemo.Text = this.GetMemoFromBookNum();
            this.Queue.Memo = this.tbMemo.Text;
            //����Ա
            this.Queue.Oper.ID = this.myQueue.Operator.ID;
            //��̨
            if (this.cmbConsole.Tag.ToString() == this.queue.Console.ID)
            {
                this.queue.User03 = "0";        //˵������δ�޸���̨��Ϣ
            }
            this.queue.Console.ID = this.cmbConsole.Tag.ToString();
            this.queue.Console.Name = this.cmbConsole.Text;
            //�Ƿ�ר��
            this.queue.ExpertFlag = this.cmbExport.SelectedIndex.ToString();

        }

        private string GetMemoFromBookNum()
        {
            string memo = string.Empty;

            if (ckb1.Checked)
            {
                memo = memo + "1|";
            }
            if (ckb2.Checked)
            {
                memo = memo + "2|";
            }
            if (ckb3.Checked)
            {
                memo = memo + "3|";
            }
            if (ckb4.Checked)
            {
                memo = memo + "4|";
            }
            if (ckb5.Checked)
            {
                memo = memo + "5|";
            }
            if (ckb6.Checked)
            {
                memo = memo + "6|";
            }
            if (ckb7.Checked)
            {
                memo = memo + "7|";
            }
            if (ckb8.Checked)
            {
                memo = memo + "8|";
            }
            if (ckb9.Checked)
            {
                memo = memo + "9|";
            }

            if (!string.IsNullOrEmpty(memo))
            {
                if (memo.Substring(memo.Length - 1, 1) == "|")
                {
                    memo = memo.Substring(0, memo.Length - 1);
                }
            }

            return memo;
        }

        private void SetBookNumFromMemo(string memo)
        {
            if (!string.IsNullOrEmpty(memo))
            {
                string[] bookNum = memo.Split('|');

                for (int i = 0; i < bookNum.Length; i++)
                {
                    if (bookNum[i] == "1")
                    {
                        this.ckb1.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "2")
                    {
                        this.ckb2.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "3")
                    {
                        this.ckb3.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "4")
                    {
                        this.ckb4.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "5")
                    {
                        this.ckb5.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "6")
                    {
                        this.ckb6.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "7")
                    {
                        this.ckb7.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "8")
                    {
                        this.ckb8.Checked = true;
                        continue;
                    }
                    if (bookNum[i] == "9")
                    {
                        this.ckb9.Checked = true;
                        continue;
                    }
                }
            }
        }

        #endregion

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.SaveData() == -1) return;
                MessageBox.Show("����ɹ�!", "��ʾ");
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.tbDept.Tag.ToString();
                obj.Name = this.tbDept.Text;

                #region {B3E7633A-D9FB-492f-9D62-D2F7188D5643}
                
                //    this.RefList(obj);
                                
                this.myDiagResult = DialogResult.OK;
                #endregion
                this.FindForm().Close();
            }
            catch { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            #region {B3E7633A-D9FB-492f-9D62-D2F7188D5643}
            this.myDiagResult = DialogResult.Cancel;
            #endregion
            this.FindForm().Close();
        }

        private void ucAddQueue_Load(object sender, EventArgs e)
        {
            try
            {
                this.Queue = this.myResult.Queue;
                this.myOldQueue = this.myResult.Queue.Clone();
                this.Init();

                this.FindForm().Text = "����ά��";
                this.FindForm().MinimizeBox = false;
                this.FindForm().MaximizeBox = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            this.dtQueue.Focus();
        }

        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbDoct.SelectedIndex != -1)//���ѡ��ҽ����������Ϊҽ������Ϊ�Զ������
            {
                //��������
                this.tbQueueName.Text = this.cmbDoct.SelectedItem.Name;
                this.cmbQueueType.Tag = "1";
                #region add by sunm 0925
                FS.HISFC.Models.Base.Employee doct = new FS.HISFC.Models.Base.Employee();
                doct = this.doctHelper.GetObjectFromID(this.cmbDoct.SelectedItem.ID) as FS.HISFC.Models.Base.Employee;
                this.tbSort.Text = doct.UserCode;
                #endregion
            }
            else
            {
                this.cmbQueueType.Tag = "2";
            }
        }

        private void cmbDoct_Leave(object sender, EventArgs e)
        {
            if (this.cmbDoct.Text == "")
            {
                this.tbQueueName.Focus();
                this.cmbQueueType.Tag = "2";
            }
        }

        private void cmbRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.tbMemo.Text = this.cmbRoom.SelectedItem.ToString();
            //�������ң�������Ч����̨
            string strRoom = this.cmbRoom.Tag.ToString();
            ArrayList al = new ArrayList();
            al = this.seatMgr.QueryValid(strRoom);
            if (al == null || al.Count <= 0)
            {
                this.cmbConsole.ClearItems();
                return;
            }
            ArrayList b = new ArrayList();

            foreach (FS.HISFC.Models.Nurse.Seat obj in al)
            {
                FS.HISFC.Models.Base.Department dept = new FS.HISFC.Models.Base.Department();

                dept.ID = obj.ID;
                dept.Name = obj.Name;
                dept.UserCode = obj.PRoom.InputCode;

                b.Add(dept);
            }
            this.cmbConsole.AddItems(b);
            this.cmbConsole.IsListOnly = true;
            this.cmbConsole.Text = "";
            //this.tbMemo.Text = this.cmbRoom.SelectedItem.ToString() + "��" + this.cmbConsole.Text;
            al.Clear();
        }


        private void cmbConsole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.tbMemo.Text = this.cmbRoom.Text + "��" + this.cmbConsole.Text;
        }

        private void cmbExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbExport.SelectedIndex == 0)
            {
                return;
            }
            if (this.cmbDoct.Text == null || this.cmbDoct.Text == "")
            {
                MessageBox.Show("û��ѡ��ҽ����������Ϊר�Ҷ���!����ѡ��ҽ��!", "��ʾ");
                this.cmbExport.SelectedIndex = 0;
                this.cmbDoct.Focus();
                return;
            }
        }
    }
}

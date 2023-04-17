using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SOC.HISFC.Components.Nurse.Controls.GYSY
{
    internal partial class ucTriage : UserControl
    {
        private ucTriage()
        {
            InitializeComponent();
            #region {D076C70D-A26A-420d-8A88-CC8160126382}
            this.Load += new EventHandler(ucTriage_Load);
            #endregion
        }
        FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();
        FS.HISFC.BizProcess.Integrate.Registration.Registration registrationIntergrade = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        FS.HISFC.Models.Registration.RegLevel regLevel = new FS.HISFC.Models.Registration.RegLevel();
        ArrayList queues;
        public ucTriage(string nurseID)
		{
			InitializeComponent();

            //FS.HISFC.BizLogic.Nurse.Queue queueMgr = new FS.HISFC.BizLogic.Nurse.Queue();
            FS.HISFC.BizLogic.Nurse.Dept deptMgr = new FS.HISFC.BizLogic.Nurse.Dept();
            FS.HISFC.BizProcess.Integrate.Registration.Registration schemaMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

			DateTime current = deptMgr.GetDateTimeFromSysDateTime();

			string noonID = Function.GetNoon(current);

            //ArrayList queues = queueMgr.Query(nurseID,current.Date,noonID);
            queues = queueMgr.Query(nurseID, current.Date, noonID);
            if (queues == null) queues = new ArrayList();

            ArrayList depts = deptMgr.GetDeptInfoByNurseNo(nurseID);
            this.cmbQueue.AddItems(queues);
            #region {4600A33C-8065-4b2c-93D2-9B26B24F61CF}
            if (this.cmbQueue.Items.Count > 0)
            {

                this.cmbQueue.SelectedIndex = 0;
                // return;
            } 
            #endregion
            //this.cmbQueue.isItemOnly = true;
            this.txtCard.ReadOnly = true;
            this.txtCard.BackColor = Color.White;
            this.txtName.ReadOnly = true;
            this.txtName.BackColor = Color.White;
            this.txtRegDate.ReadOnly = true;
            this.txtRegDate.BackColor = Color.White;
            this.txtDept.ReadOnly = true;
            this.txtDept.BackColor = Color.White;
            #region {D076C70D-A26A-420d-8A88-CC8160126382}
            this.Load += new EventHandler(ucTriage_Load); 
            #endregion
        }

        //private void SelectItem(FS.HISFC.Models.Nurse.Queue queue, FS.HISFC.Models.Registration.Register register)
        private void SelectItem(FS.HISFC.Models.Registration.Register register)
        {
           
            for (int i = 0; i < this.queues.Count; i++)
            {
              
                //ArrayList al = new ArrayList();
                FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();
                queue = queues[i] as FS.HISFC.Models.Nurse.Queue;

                //�ж��ǲ���ר�Һ�

                this.regLevel = this.registrationIntergrade.QueryRegLevelByCode(register.DoctorInfo.Templet.RegLevel.ID);
                register.RegLvlFee.RegLevel.IsExpert = this.regLevel.IsExpert;
               //ȫ������
                if (FS.FrameWork.Function.NConvert.ToBoolean(queue.ExpertFlag) == register.RegLvlFee.RegLevel.IsExpert && queue.Doctor.ID == register.DoctorInfo.Templet.Doct.ID && queue.AssignDept.ID == register.DoctorInfo.Templet.Dept.ID)
                {
                    this.cmbQueue.SelectedIndex = i;
                    return;
                }

               


            }
            for (int i = 0; i < this.queues.Count; i++)
            {

                //ArrayList al = new ArrayList();
                FS.HISFC.Models.Nurse.Queue queue = new FS.HISFC.Models.Nurse.Queue();
                queue = queues[i] as FS.HISFC.Models.Nurse.Queue;

                //�ж��ǲ���ר�Һ�

                this.regLevel = this.registrationIntergrade.QueryRegLevelByCode(register.DoctorInfo.Templet.RegLevel.ID);
                register.RegLvlFee.RegLevel.IsExpert = this.regLevel.IsExpert;

                //�ű���ͬ��ָ�Ƿ�ר�ң�������ͬ
                if (FS.FrameWork.Function.NConvert.ToBoolean(queue.ExpertFlag) == register.RegLvlFee.RegLevel.IsExpert && queue.AssignDept.ID == register.DoctorInfo.Templet.Dept.ID)
                {
                    this.cmbQueue.SelectedIndex = i;
                    return;
                }


            }
            #region {4600A33C-8065-4b2c-93D2-9B26B24F61CF}
            if (this.cmbQueue.Items.Count > 0)
            {

                this.cmbQueue.SelectedIndex = 0;
                // return;
            } 
            #endregion
            //this.cmbQueue.SelectedIndex = 0;

        }

        #region ������

        public delegate void MyDelegate(FS.HISFC.Models.Nurse.Assign assign);
        public event MyDelegate OK;

        public event EventHandler Cancel;

        protected virtual void OnCancel(object sender, EventArgs e)
        {
            if (Cancel != null)
            {
                this.Cancel(this, new EventArgs());
            }
        }

        //add by niuxy
        private string Regdoc_id = string.Empty; 

        /// <summary>
        /// �����Ű���ŵõ��Ű���Ϣ
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Registration.DoctSchema GetDoctor(string ID)
        {
            if (docts == null) return null;

            foreach (FS.HISFC.Models.Registration.DoctSchema doct in docts)
            {
                if (doct.ID == ID)
                    return doct;
            }

            return null;
        }

        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register Register
        {
            set
            {
                this.txtCard.Text = value.PID.CardNO;
                this.txtName.Text = value.Name;
                this.txtDept.Text = value.DoctorInfo.Templet.Dept.Name;
                this.txtRegDate.Text = value.DoctorInfo.SeeDate.ToString();
                this.Regdoc_id = value.DoctorInfo.Templet.Doct.ID;

                this.txtCard.Tag = value;
                this.SelectItem(value);
            }
            get
            {
                if (this.txtCard.Tag == null)
                {
                    return null;
                }
                else
                {
                    return (FS.HISFC.Models.Registration.Register)this.txtCard.Tag;
                }
            }
        }

        /// <summary>
        /// �Ű�ҽ������
        /// </summary>
        private ArrayList docts = null;
        #endregion

        private void ucTriage_Load(object sender, EventArgs e)
        {
            #region {D076C70D-A26A-420d-8A88-CC8160126382}
            this.cmbQueue.Select(); 
            #endregion
            this.cmbQueue.Focus();
        }

        private void cmbQueue_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = this.cmbQueue.SelectedItem;

            if (obj == null) return;

            if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Queue))
            {
                FS.HISFC.Models.Nurse.Queue que = obj as FS.HISFC.Models.Nurse.Queue;
                this.txtRoom.Text = que.SRoom.Name;
                this.txtSequenceNO.Text = (que.WaitingCount + 1).ToString();
            }
            else
            {
                this.txtRoom.Text = obj.User03;
            }
        }

        private void cmbQueue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region {D076C70D-A26A-420d-8A88-CC8160126382}
                string inputText = this.cmbQueue.Text.Trim();
                
                ArrayList al = this.cmbQueue.alItems;
                if (al != null)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        FS.HISFC.Models.Nurse.Queue queobj = al[i] as FS.HISFC.Models.Nurse.Queue;
                        if (queobj.Order.ToString() == inputText)
                        {
                            this.cmbQueue.SelectedIndex = i;
                            break;
                        }
                    }
                }
                #endregion

                this.neuButton1.Focus();
            }
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = this.cmbQueue.SelectedItem;

            if (obj == null)
            {
                MessageBox.Show("��ѡ��������!", "��ʾ");
                this.cmbQueue.Focus();
                return;
            }

            FS.HISFC.BizProcess.Integrate.Manager deptMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            FS.HISFC.Models.Nurse.Assign assgin = new FS.HISFC.Models.Nurse.Assign();
            FS.HISFC.Models.Nurse.Queue queueinfo = new FS.HISFC.Models.Nurse.Queue();
            queueinfo = (FS.HISFC.Models.Nurse.Queue)this.cmbQueue.SelectedItem;
            if (this.Register.DoctorInfo.Templet.Dept.ID != queueinfo.AssignDept.ID)
            {

            }


            #region ʵ�帳ֵ
            assgin.Register = this.Register;

            #region ���жϸû����Ƿ��Ѿ����������Ѿ��������ʾ���Ƿ�Ҫ�����ڱ�����վ�·���  ygch {1BEDF463-4380-4174-BDB3-4F067B773473}
            //�ҳ��û��ߵķ��������Ϣ
            FS.HISFC.BizLogic.Nurse.Assign assginMgr = new FS.HISFC.BizLogic.Nurse.Assign();
            FS.HISFC.Models.Nurse.Assign assion = new FS.HISFC.Models.Nurse.Assign();
            assion = assginMgr.QueryByClinicID(Register.ID);
            //����Ƿ�����Ҫȡ���ķ�����Ϣ
            bool isCannle = false;

            bool isit = this.registrationIntergrade.QueryIsTriage(this.Register.ID);//���Ա����������������false ��assionӦ����null ��֮����null
            if (assion != null)
            {
                isCannle = true;
                DialogResult rs = new DialogResult();
                rs= MessageBox.Show("�û����������������������������ڴ˴�����Ļ�����ȡ���û����������������ķ����¼��\r" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (rs == DialogResult.No)
                {
                    return;
                }
            }
            #endregion

            if (obj.GetType() == typeof(FS.HISFC.Models.Nurse.Queue))//����
            {
                assgin.Queue = (FS.HISFC.Models.Nurse.Queue)obj;
                //if (this.Regdoc_id != null && this.Regdoc_id != "")
                //{
                if (assgin.Queue.ExpertFlag == "1" && assgin.Register.DoctorInfo.Templet.RegLevel.IsExpert == false)
                {
                    if (MessageBox.Show("��ͨ�Ž���ר�Ҷ���" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                if (FS.FrameWork.Function.NConvert.ToBoolean(assgin.Queue.ExpertFlag) == assgin.Register.DoctorInfo.Templet.RegLevel.IsExpert && assgin.Queue.Doctor.ID != this.Regdoc_id)
                {
                    if (MessageBox.Show("ѡ��ҽʦ��Һ�ҽʦ��һ�£��Ƿ����", " ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                //}
                //if (this.Regdoc_id == null || this.Regdoc_id == "")
                //{
                if (assgin.Queue.ExpertFlag == "0" && assgin.Register.DoctorInfo.Templet.RegLevel.IsExpert == true)
                {

                    if (MessageBox.Show("ר�ҹҺŽ�����ͨ����" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                }
                //}

            }
            else
            {
                FS.HISFC.Models.Registration.DoctSchema doct =
                    this.GetDoctor(obj.ID);
                //add by niuxy
                if (this.Regdoc_id != null && this.Regdoc_id != "")
                {
                    if (doct.ID != this.Regdoc_id)
                        if (MessageBox.Show("ר�ҹҺŽ�����ͨ����" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                    {
                        if (MessageBox.Show("ѡ��ҽʦ��Һ�ҽʦ��һ�£��Ƿ����", " ", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                }
                if (this.Regdoc_id == null || this.Regdoc_id == "")
                {
                    if (assgin.Queue.ExpertFlag == "1")
                    {

                        if (MessageBox.Show("��ͨ�Ž���ר�Ҷ���" + "�Ƿ������", "��ʾ", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                    }
                }
                assgin.Queue.ID = obj.ID;
                assgin.Queue.Name = obj.Name;
                assgin.Queue.Dept.ID = doct.Dept;
                assgin.Queue.Doctor = doct.Doctor;
                assgin.Queue.SRoom = doct.Room;

                assgin.Queue.Memo = obj.Memo;
            }
            FS.HISFC.BizLogic.Nurse.Assign a = new FS.HISFC.BizLogic.Nurse.Assign();
            assgin.TriageStatus = FS.HISFC.Models.Nurse.EnuTriageStatus.Triage;
            //assgin.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID;
            assgin.TriageDept = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            assgin.TirageTime = a.GetDateTimeFromSysDateTime();// deptMgr.GetDateTimeFromSysDateTime();
            assgin.Oper.OperTime = assgin.TirageTime;
            assgin.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;// var.User.ID;
            assgin.Queue.Dept = assgin.Register.DoctorInfo.Templet.Dept;

            #endregion

            if (Function.CheckArray(assgin.Register, queueinfo) == -1)
            {
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction(); 

            #region �ڽ��з���֮ǰ���������Ҫȡ���ķ����¼�������ȡ������ ygch {1BEDF463-4380-4174-BDB3-4F067B773473}
            if (isCannle)
            {
                //assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //ȡ���������
                //try
                //{
                //    int rtn = assginMgr.CancelIn(this.Register.ID, assion.Queue.Console.ID);
                //    if (rtn == -1)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show(assginMgr.Err, "��ʾ");
                //        return;
                //    }
                //    if (rtn == 0)
                //    {
                //        FS.FrameWork.Management.PublicTrans.RollBack();
                //        MessageBox.Show("�÷�����Ϣ״̬�Ѿ��ı�,��ˢ����Ļ!", "��ʾ");
                //        return;
                //    }
                //}
                //catch (Exception er)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(er.Message, "��ʾ");
                //    return;
                //}

                //ȡ���������
                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();
                string err = "";

                if (Function.CancelTriage(assion, ref err)
                    == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(err, "��ʾ");
                    return;
                }
            }
            #endregion

            string error = "";

            if (Function.Triage(assgin, "1", ref error) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                #region {9EB5D321-AA03-435f-8581-F64F852D2656}
                MessageBox.Show("�޷����������Ϣ����ˢ�º����·��", "��ʾ");
                //MessageBox.Show(error, "��ʾ"); 
                #endregion
                return;
            }
            #region ygch ������Զ��������λ�� {2E027CE2-330C-4eb9-87A0-4B59B733FC4C}
            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();
            //try
            //{
            //    assginMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                
            //    int rtn = 1;
            //    if (assgin.Queue.SRoom == null || assgin.Queue.Console == null)
            //    {
            //        MessageBox.Show("������");
            //        return;
            //    }
            //    else
            //    {
            //        rtn = assginMgr.Update(assgin.Register.ID, assgin.Queue.SRoom,
            //            assgin.Queue.Console, assginMgr.GetDateTimeFromSysDateTime());
            //    }

            //    if (rtn == -1)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(assginMgr.Err, "��ʾ");
            //        return;
            //    }
            //    if (rtn == 0)
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show("�û��߷���״̬�Ѿ��ı�,������ˢ����Ļ!", "��ʾ");
            //        return;
            //    }
            //}
            //catch (Exception err)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    MessageBox.Show(err.Message, "��ʾ");
            //    return;
            //}

            //�ڽ����б�����ӻ���
            //this.AddPatient(assign);
            #endregion ������Զ����������~

            FS.FrameWork.Management.PublicTrans.Commit();

            if (this.OK != null)
                this.OK(assgin);

            this.FindForm().Close();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            
            this.FindForm().Close();
            this.OnCancel(this, new EventArgs());
        }

    }
}

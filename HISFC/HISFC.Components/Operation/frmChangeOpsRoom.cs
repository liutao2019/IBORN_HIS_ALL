using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Operation
{
    public partial class frmChangeOpsRoom :FS.FrameWork.WinForms.Forms.BaseForm
    {
       //·־����ʱ�䣺��������-��-����
       //Ŀ�ģ���������������б任ִ�п���

        #region ҵ��ʵ��
        /// <summary>
        /// �������뵥����ʵ��
        /// </summary>
        //public FS.HISFC.Object.Operator.OpsApplication m_objOpsApp = new FS.HISFC.Object.Operator.OpsApplication();
        public FS.HISFC.Models.Operation.OperationAppllication m_objOpsApp = new FS.HISFC.Models.Operation.OperationAppllication();
        /// <summary>
        /// �������������ʵ��
        /// </summary>
        //private FS.HISFC.Management.Operator.Operator m_objOpsManager = new FS.HISFC.Management.Operator.Operator();
        private FS.HISFC.BizProcess.Integrate.Operation.Operation m_objOpsManager = new FS.HISFC.BizProcess.Integrate.Operation.Operation();

        private FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();


        //FS.HISFC.Management.Manager.Constant l_objC = new FS.HISFC.Management.Manager.Constant();
        #endregion

        #region ����
        /// <summary>
        /// �������������ID
        /// </summary>
        public string strNewOpsRoomID = "";
        #endregion

        #region ��ʼ��

        public frmChangeOpsRoom(FS.HISFC.Models.Operation.OperationAppllication apply)
        {
            InitializeComponent();
            m_objOpsApp = apply;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitWin()
        {
            if (this.m_objOpsApp == null) return;
            //����
            this.txtName.Text = this.m_objOpsApp.PatientInfo.Name;
            //����
            this.txtDept.Text = deptManager.GetDepartment(m_objOpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString()).Name;
            //סԺ��/�����
            this.txtPatientNo.Text = m_objOpsApp.PatientInfo.PID.ID.ToString();
            //ԭ������
            if (m_objOpsApp.OperateRoom != null)
            {
                this.txtOldOpsRoom.Tag = m_objOpsApp.OperateRoom.ID.ToString();
                this.txtOldOpsRoom.Text = m_objOpsApp.OperateRoom.Name;
                strNewOpsRoomID = m_objOpsApp.OperateRoom.ID.ToString();
            }
            else
            {
                
                this.txtOldOpsRoom.Text=Environment.GetDept(m_objOpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID).Name;
                //this.txtOldOpsRoom.Text = dept.GetDeptmentById(this.var.User.Dept.ID.ToString()).Name;
                //this.txtOldOpsRoom.Tag = this.var.User.Dept.ID.ToString();
                this.txtOldOpsRoom.Tag = m_objOpsApp.PatientInfo.PVisit.PatientLocation.Dept.ID;
            }
            //����ԤԼʱ��
            if (m_objOpsApp.PreDate != DateTime.MinValue)
                this.dtpPreDate.Value = m_objOpsApp.PreDate;
            else
                this.dtpPreDate.Value = this.m_objOpsManager.GetDateTimeFromSysDateTime();
            //������combox�б�
            this.cmbOpsRoom.Items.Clear();
            //{FED072A5-2D4C-4b81-9E93-BAB6849026BA} ���ļ��������ҵĵ��ú��� ͨ���������Ի�ȡ������
            FS.HISFC.BizLogic.Manager.Department deptLogicInstance = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList OpsRoomAl = new ArrayList();
            OpsRoomAl = deptLogicInstance.GetDeptment( "1" );//"1"��ʾ�������͵Ŀ���
            this.cmbOpsRoom.AddItems(OpsRoomAl);

            //ȱʡѡ��ԭ������
            this.cmbOpsRoom.Tag = this.m_objOpsApp.OperateRoom.ID.ToString();
            //����̨����combox�б�
            ArrayList alTableType = new ArrayList();
            //FS.HISFC.Components.Object.neuObject obj = new FS.HISFC.Components.Object.neuObject();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "1";
            obj.Name = "��̨";
            alTableType.Add(obj.Clone());
            obj.ID = "2";
            obj.Name = "��̨";
            alTableType.Add(obj.Clone());
            obj.ID = "3";
            obj.Name = "��̨";
            alTableType.Add(obj.Clone());
            this.cmbTableType.AddItems((ArrayList)(alTableType.Clone()));
            //ȱʡѡ�С���̨��
            cmbTableType.SelectedIndex = 0;
        }
        #endregion

        #region ����
        //����ԤԼʱ��
        /// <summary>
        /// ԤԼʱ��ʱЧ���ж�
        /// </summary>
        /// <returns> 0 ��Ч -1 ��Ч</returns>
        public int PreDateValidity()
        {
            //ʱЧ�ж�
            //Error:ϵͳֵδά�����ʽ�Ƿ���Before:ԤԼʱ��С�����ڣ�Over:����������յ���̨��OK:��������
            string strResult = "";
            strResult = this.m_objOpsManager.PreDateValidity(this.dtpPreDate.Value);
            switch (strResult)
            {
                case "Error":
                    MessageBox.Show("ϵͳ�����������ʱ�����Ϊ�ջ��ʽ�Ƿ�������ϵϵͳ����Ա��", "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                case "Before":
                    MessageBox.Show("����ԤԼʱ�䲻��С�ڵ�ǰʱ�䣡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                case "Over":
                    //���ѡ�е�����̨
                    if (this.cmbTableType.SelectedIndex == 0)
                    {
                        DialogResult result;
                        result = MessageBox.Show("�ѳ�������������̨����Ľ���ʱ�䣬\n��ԤԼ���������ڽ��������������̨������\n�Ƿ���Ҫ�����̨��", "��ʾ",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        if (result == DialogResult.Yes)//�����̨
                        {
                            this.cmbTableType.Tag = "2";
                            //this.rdbZT.Enabled = false;//������²�����������̨
                            this.m_objOpsApp.PreDate = this.dtpPreDate.Value;
                            return 0;
                        }
                        else
                        {
                            this.dtpPreDate.Focus();
                            return -1;
                        }
                    }
                    return 0;
                case "OK":
                    this.m_objOpsApp.PreDate = this.dtpPreDate.Value;
                    return 0;
            }
            return 0;
        }

        //ʣ����̨��
        /// <summary>
        /// ����ʣ����̨������ʾ
        /// </summary>
        /// <return>0 success -1 fail -2������Ϊ��</return>
        public int ShowTableNum()
        {
            DateTime dtPreDate = this.dtpPreDate.Value;
            //��ȡѡ�е�ת��������
            FS.HISFC.Models.Base.Department OpsRoom = deptManager.GetDepartment(this.cmbOpsRoom.Tag.ToString());
            
            //��ȡʣ���������̨��
            int iEnableNum = 0;
            if (OpsRoom == null || OpsRoom.ID.ToString() == "") return -2;
            try
            {
                //����ҽ����������
                //FS.HISFC.Components.Object.neuObject Dept = new FS.HISFC.Components.Object.neuObject();
                FS.FrameWork.Models.NeuObject Dept = new FS.FrameWork.Models.NeuObject();
                //if (this.m_objOpsApp.Apply_Doct.Dept == null)
                if(this.m_objOpsApp.ApplyDoctor.Dept==null)
                    Dept = this.m_objOpsApp.PatientInfo.PVisit.PatientLocation.Dept;
                else
                    Dept = this.m_objOpsApp.ApplyDoctor.Dept;// Apply_Doct.Dept;
                //��̨��
                iEnableNum = this.m_objOpsManager.GetEnableTableNum(OpsRoom, Dept.ID.ToString(), dtPreDate);
            }
            catch (Exception ex)
            {
                this.m_objOpsManager.Err = "Operator.frmChangeOpsRoom.ShowTableNum ��ȡ��̨��ʱ����";
                this.m_objOpsManager.ErrCode = ex.Message;
                this.m_objOpsManager.WriteErr();
                return -1;
            }

            if (iEnableNum <= 0 && this.cmbTableType.SelectedIndex == 0)
            {
                DialogResult result;
                result = MessageBox.Show("�ٴ������ڸ�ԤԼ��������ѡ������������̨��\n��ԤԼ���������ڻ����������ҽ�������\n������ѡ�����������̨������\n�Ƿ���Ҫ�����̨��", "��ʾ",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)//�����̨
                {
                    this.cmbTableType.SelectedIndex = 1;//��̨
                    this.txtTableNum.Text = "0";
                    return 0;
                }
                else
                    return -1;
            }
            this.txtTableNum.Text = iEnableNum.ToString();
            return 0;
        }
        #endregion

        #region �¼�
        private void cmbOpsRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ShowTableNum() == -1) return;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                //���������δ���ı�
                if (this.txtOldOpsRoom.Tag.ToString() == this.cmbOpsRoom.Tag.ToString())
                {
                    DialogResult result = MessageBox.Show("����û�и�����������Ϣ���Ƿ�ȷ�Ϲرձ����ڣ�", "��ʾ",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        this.btnCancel_Click(sender, e);
                    }
                    else
                    {
                        this.cmbOpsRoom.Focus();
                    }
                    return;
                }
                //������Ч���ж�
                if (this.PreDateValidity() == -1 || this.ShowTableNum() == -1) return;
                //�ж�ͨ���󣬽�¼���ֵ����m_objOpsApp��س�Ա
                //����ʱ��
                this.m_objOpsApp.PreDate = this.dtpPreDate.Value;
                //������
                this.m_objOpsApp.OperateRoom.ID = this.cmbOpsRoom.Tag.ToString();
                this.m_objOpsApp.OperateRoom.Name = this.cmbOpsRoom.Text;
                strNewOpsRoomID = this.cmbOpsRoom.Tag.ToString();
                //����̨����
                this.m_objOpsApp.TableType = this.cmbTableType.Tag.ToString();
                //���޸ĵĽ�����浽���ݿ���
                //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.var.con);

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction trans = new FS.FrameWork.Management.Transaction(this.m_objOpsManager.Connection);
                //trans.BeginTransaction();

                this.m_objOpsManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //����ҵ��������Ҹ�������
                if (this.m_objOpsManager.ChangeOperatorRoom(this.m_objOpsApp) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������Ҹ�����Ϣʱ����\n����ϵͳ����Ա��ϵ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("�����Ҹ����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                Close();
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        #endregion

        #region �����л�
        private void dtpPreDate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.cmbOpsRoom.Focus();
        }

        private void cmbOpsRoom_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ShowTableNum() == 0)
                    this.cmbTableType.Focus();
            }
        }

        private void cmbTableType_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnOK.Focus();
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ����Ǽǵ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-14]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucRegistrationAnaeForm : UserControl
    {
        public ucRegistrationAnaeForm()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
            {
                this.InitCtrl();
            }
        }

        #region �ֶ�
        private OperationRecord operationRecord = new OperationRecord();
        private AnaeRecord anaeRecord = new AnaeRecord();
        private FS.HISFC.BizProcess.Interface.Operation.IAnaeFormPrint anaeFormPrint;
        ////{B9DDCC10-3380-4212-99E5-BB909643F11B}
        private FS.FrameWork.Public.ObjectHelper anaeWayHelper = null;
        #endregion

        #region ����
        /// <summary>
        ///  ����
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public AnaeRecord AnaeRecord
        {
            set
            {
                this.Clear();
                if (value == null)
                {

                    return;
                }

                this.anaeRecord = value;
                this.OperationApplication = value.OperationApplication;

                //�Ƿ����
                this.cmbCharge.Tag = FS.FrameWork.Function.NConvert.ToInt32(value.IsCharged).ToString();

                //����ʱ��
                if (value.AnaeDate != DateTime.MinValue)
                    this.dtpAnaeDate.Value = value.AnaeDate;
                else
                    this.dtpAnaeDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime();
                //����Ч��
                this.cmbAnaeResult.Tag = value.AnaeResult.ID.ToString();
                //�Ƿ���PACU
                this.cbxIsPacu.Checked = value.IsPACU;
                //����ʱ��
                if (value.InPacuDate != DateTime.MinValue)
                    this.dtpInPacuDate.Value = value.InPacuDate;
                else
                    this.dtpInPacuDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime().Date;
                //����ʱ��
                if (value.OutPacuDate != DateTime.MinValue)
                    this.dtpOutPacuDate.Value = value.OutPacuDate;
                else
                    this.dtpOutPacuDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime().Date;
                //����״̬
                this.cmbInStatus.Tag = value.InPacuStatus.ID.ToString();
                //����״̬
                this.cmbOutStatus.Tag = value.OutPacuStatus.ID.ToString();
                //�Ƿ�������ʹ
                this.cbxIsDemulcent.Checked = value.IsDemulcent;
                //��ʹ��ʽ
                this.cmbDemuKind.Tag = value.DemulcentType.ID.ToString();
                //����
                this.cmbDemuModel.Tag = value.DemulcentModel.ID.ToString();
                //��ʹ����
                this.txtDemuDays.Text = value.DemulcentDays.ToString();
                //�ι�ʱ��
                if (value.PullOutDate != DateTime.MinValue)
                    this.dtpPullOutDate.Value = value.PullOutDate;
                else
                    this.dtpPullOutDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime().Date;
                //�ι���
                this.txtPullOutOpcd.Tag = value.PullOutOperator.ID.ToString();
                // this.txtPullOutOpcd.Text = value.PullOutOperator.Name;
                //��ʹЧ��
                this.cmbDemuResult.Tag = value.DemulcentEffect.ID.ToString();
                this.txtRemark.Text = value.Memo;
                //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
                this.cmdDirection.Tag = value.Direction;
                //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
                this.cmbDemuDrug.Tag = value.DemuDrug;

            }
        }

        /// <summary>
        ///  �½�����
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperationAppllication OperationApplication
        {
            set
            {
                this.Clear();

                if (value == null)
                {
                    return;
                }
                this.anaeRecord = new AnaeRecord();
                this.anaeRecord.ExecDept.ID = Environment.OperatorDeptID;
                this.anaeRecord.OperationApplication = value;
                //����ʽ
                this.cmbAnaeType.Tag = value.AnesType.ID;
                #region ��ʾ���߻�����Ϣ
                //סԺ��/�����
                this.txtPatient.Text = value.PatientInfo.PID.ID.ToString();
                //����
                this.txtName.Text = value.PatientInfo.Name;
                //�Ա�
                this.txtSex.Text = value.PatientInfo.Sex.Name;
                //��������
                this.txtBirthday.Text = value.PatientInfo.Birthday.ToString();
                //����
                this.txtDept.Text = Environment.GetDept(value.PatientInfo.PVisit.PatientLocation.Dept.ID.ToString()).Name;
                //������
                this.txtBedNo.Text = value.PatientInfo.PVisit.PatientLocation.Bed.ID.ToString();
                
                //{B9DDCC10-3380-4212-99E5-BB909643F11B}

                if (anaeWayHelper == null)
                {
                    anaeWayHelper = new FS.FrameWork.Public.ObjectHelper();
                    FS.HISFC.BizProcess.Integrate.Manager mgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    ArrayList al = mgr.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ANESWAY);
                    anaeWayHelper.ArrayObject = al;

                }

                this.lblAnaeWay.Text = this.anaeWayHelper.GetName(value.AnesWay);

                #endregion

                //������Ա��Ϣ
                this.InitAnaeDoct(value);
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// �ؼ���ʼ��
        /// </summary>
        public void InitCtrl()
        {
            this.Clear();
            this.imageList1.Images.Add(FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.R��Ա));
            //����ʽcombox
            try
            {
                this.cmbAnaeType.Items.Clear();
                ArrayList alAnaeType = Environment.IntegrateManager.GetConstantList(EnumConstant.ANESTYPE);
                this.cmbAnaeType.AddItems(alAnaeType);
            }
            catch { }
            //����Ч��combox
            try
            {
                this.cmbAnaeResult.Items.Clear();
                ArrayList alAnaeResult = Environment.IntegrateManager.GetConstantList(EnumConstant.EFFECT);
                this.cmbAnaeResult.AddItems(alAnaeResult);
            }
            catch { }
            //��(PACU)��״̬combox
            try
            {
                this.cmbInStatus.Items.Clear();
                ArrayList alInStatus = Environment.IntegrateManager.GetConstantList(EnumConstant.PACUSTATUS);
                this.cmbInStatus.AddItems(alInStatus);
            }
            catch { }
            //��(PACU)��״̬combox
            try
            {
                this.cmbOutStatus.Items.Clear();
                ArrayList alOutStatus = Environment.IntegrateManager.GetConstantList(EnumConstant.PACUSTATUS);
                this.cmbOutStatus.AddItems(alOutStatus);
            }
            catch { }
            //��ʹ��ʽ
            try
            {
                this.cmbDemuKind.Items.Clear();
                ArrayList alDemuKind = Environment.IntegrateManager.GetConstantList(EnumConstant.DEMUKIND);
                this.cmbDemuKind.AddItems(alDemuKind);
            }
            catch { }
            //����
            try
            {
                this.cmbDemuModel.Items.Clear();
                ArrayList alDemuModel = Environment.IntegrateManager.GetConstantList(EnumConstant.DEMUMODEL);
                this.cmbDemuModel.AddItems(alDemuModel);
            }
            catch { }
            //��ʹЧ��combox
            try
            {
                this.cmbDemuResult.Items.Clear();
                ArrayList alDemuResult = Environment.IntegrateManager.GetConstantList(EnumConstant.EFFECT);
                this.cmbDemuResult.AddItems(alDemuResult);
            }
            catch { }
            //һЩ��־Combox��ֵ
            ArrayList alFlag = new ArrayList();
            NeuObject obj = new NeuObject();
            obj.ID = "1";
            obj.Name = "��";
            alFlag.Add(obj.Clone());
            obj.ID = "0";
            obj.Name = "��";
            alFlag.Add(obj.Clone());
            cmbCharge.AddItems((ArrayList)(alFlag.Clone()));		//�Ƿ����
            FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.BizLogic.Operation.OpsTableManage opsTable = new FS.HISFC.BizLogic.Operation.OpsTableManage();
            FS.HISFC.Models.Base.Employee objEmp = (FS.HISFC.Models.Base.Employee)opsTable.Operator;
            txtPullOutOpcd.AddItems(managerMgr.QueryEmployeeByDeptID(objEmp.Dept.ID));
            //��ʼ���������Ա�б�		
            //this.lvPersons.Dept = this.ParentForm.var.User.Dept.Clone();
            //this.lvPersons.ShowDeptPerson();
            //FS.HISFC.Components.Common.Controls.l
            //this.UcCtrlEnabled(false);
            this.lvPersons.DeptID = Environment.OperatorDeptID;
            //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}feng.ch����ȥ��
            alFlag = Environment.IntegrateManager.GetConstantList("DIRECTION");
            this.cmdDirection.AddItems(alFlag);
            //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}��ʹ��ҩ
            this.cmbDemuDrug.Items.Clear();
            alFlag = Environment.IntegrateManager.GetConstantList("DEMUDRUG");
            this.cmbDemuDrug.AddItems(alFlag);

        }
        /// <summary>
        /// ��տؼ��е�����
        /// </summary>
        public void Clear()
        {

            txtPatient.Text = "";
            txtName.Text = "";
            txtSex.Text = "";
            txtBirthday.Text = "";
            txtDept.Text = "";
            txtBedNo.Text = "";
            cmbCharge.SelectedIndex = -1;
            //����������סԺ������־
            //rdbIn.Visible = false;
            //rdbOut.Visible = false;
            //this.m_objAnaeRec.m_objOpsApp.Pasource = "2";//סԺ
            DateTime now = Environment.AnaeManager.GetDateTimeFromSysDateTime();

            this.cmbAnaeType.SelectedIndex = -1;
            this.dtpAnaeDate.Value = now;
            this.cmbAnaeResult.SelectedIndex = -1;
            this.cbxIsPacu.Checked = false;
            this.dtpInPacuDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime();
            this.dtpOutPacuDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime();
            this.cmbInStatus.SelectedIndex = -1;
            this.cmbOutStatus.SelectedIndex = -1;
            this.cbxIsDemulcent.Checked = false;
            this.cmbDemuKind.SelectedIndex = -1;
            this.cmbDemuModel.SelectedIndex = -1;
            this.txtDemuDays.Text = "";
            this.dtpPullOutDate.Value = Environment.AnaeManager.GetDateTimeFromSysDateTime();
            this.txtPullOutOpcd.Text = "";
            this.cmbDemuResult.SelectedIndex = -1;
            this.txtRemark.Text = "";
            this.lvAnaeDoct.Items.Clear();
            this.lvAnaeHelper.Items.Clear();
            this.lvPersons.Refresh();
            //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
            this.cmdDirection.Tag = null;
            //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
            this.cmbDemuDrug.Tag = null;
        }

        /// <summary>
        /// ��ʼ��������Ա��Ϣ
        /// </summary>
        /// <param name="myOpsApp"></param>		
        public void InitAnaeDoct(OperationAppllication myOpsApp)
        {
            this.lvAnaeDoct.Items.Clear();
            this.lvAnaeHelper.Items.Clear();

            //��������ɫ��ʾ
            foreach (ArrangeRole role in myOpsApp.RoleAl)
            {
                ListViewItem item = new ListViewItem();
                item.Tag = role;
                item.ImageIndex = 0;
                item.Text = role.Name;
                //��Ա״̬�����ࡢֱ�䡢�Ӱ��
                try
                {
                    if (role.RoleOperKind.ID != null)
                    {
                        //ֱ��
                        if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.ZL.ToString())
                            item.Text = item.Text + "|��";
                        //�Ӱ�
                        else if (role.RoleOperKind.ID.ToString() == EnumRoleOperKind.JB.ToString())
                            item.Text = item.Text + "|��";
                    }

                    //���ݽ�ɫ�������ͬlistView�в�����
                    if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())
                    {
                        this.lvAnaeDoct.Items.Add(item);
                        this.lvPersons.RemoveEmployee(role.ID);
                    }
                    else if (role.RoleType.ID.ToString() == EnumOperationRole.AnaesthesiaHelper.ToString())//��������
                    {
                        this.lvAnaeHelper.Items.Add(item);
                        this.lvPersons.RemoveEmployee(role.ID);
                    }


                }
                catch { }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="strStatus">������״̬��"NULL"�޲�����"RECO"�Ǽǣ�"NEW"���ǣ�"MODIFY"�޸�</param>
        /// <returns>0 success -1 fail</returns>
        public int Save(OperType operType)
        {
            try
            {
                if (this.GetCtrlInfo() == -1)
                    return -1;
            }
            catch { }
            //���ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction trans = new
            //    FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);             
            //trans.BeginTransaction();

            Environment.AnaeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            Environment.OperationManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //����ǼǺ����뵥������״̬��
            this.anaeRecord.OperationApplication.IsAnesth = true;//������
            switch (operType)
            {
                case OperType.Reco://�Ǽ�
                    //����������¼
                    if (Environment.AnaeManager.AddAnaeRecord(this.anaeRecord) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������ǼǼ�¼ʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }

                    //�����������뵥��������-------add by sunm
                    if (Environment.OperationManager.DoAnaeRecord(this.anaeRecord.OperationApplication.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����������Ϣʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }

                    //���޸Ĺ�������ǼǼ�¼�е��������뵥��Ϣ����
                    if (Environment.OperationManager.UpdateApplication(this.anaeRecord.OperationApplication) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����������Ϣʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    break;
                case OperType.New://����
                    if (this.txtPatient.Text != "")
                    {
                        //���������¼
                        if (Environment.AnaeManager.AddAnaeRecord(this.anaeRecord) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("��������ǼǼ�¼ʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }

                        //���Ӳ��ǵ����������¼
                        if (Environment.OperationManager.CreateApplication(this.anaeRecord.OperationApplication) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("�������������¼ʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return -1;
                        }
                    }
                    break;
                case OperType.Modify:
                    //�޸�����ǼǼ�¼
                    if (Environment.AnaeManager.UpdateAnaeRecord(this.anaeRecord) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("��������ǼǼ�¼ʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    //���޸Ĺ��������ǼǼ�¼�е��������뵥��Ϣ����
                    if (Environment.OperationManager.UpdateApplication(this.anaeRecord.OperationApplication) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����������Ϣʱ��������\n����ϵͳ����Ա��ϵ��", "��ʾ",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return -1;
                    }
                    break;
               

            }
            MessageBox.Show("����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }

        /// <summary>
        /// ����������ʾ����Ϣ���뵽m_objAnaeRec��Ա������
        /// </summary>
        /// <return>0 success -1 fail</return>
        private int GetCtrlInfo()
        {
            try
            {
                if (this.txtPatient.Text.Trim() == "")
                {
                    MessageBox.Show("��������������סԺ�Ż�����ţ�", "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPatient.Focus();
                    return -1;
                }
                //�Ƿ����
                if (this.cmbCharge.Tag.ToString() != "")
                    this.anaeRecord.IsCharged = FS.FrameWork.Function.NConvert.ToBoolean(int.Parse(this.cmbCharge.Tag.ToString()));
                //����ʽ
                this.anaeRecord.OperationApplication.AnesType.ID = this.cmbAnaeType.Tag.ToString();
                this.anaeRecord.OperationApplication.AnesType.Name = this.cmbAnaeType.Text;
                //����ʱ��
                this.anaeRecord.AnaeDate = this.dtpAnaeDate.Value;
                //����Ч��
                this.anaeRecord.AnaeResult.ID = this.cmbAnaeResult.Tag.ToString();
                this.anaeRecord.AnaeResult.Name = this.cmbAnaeResult.Text;
                //����ҽʦ
                ArrayList alRole = new ArrayList();
                foreach (ListViewItem lviAnae in this.lvAnaeDoct.Items)
                {
                    ArrangeRole role = lviAnae.Tag as ArrangeRole;
                    alRole.Add(lviAnae.Tag);
                }
                //��������
                foreach (ListViewItem lviAnaeHelper in this.lvAnaeHelper.Items)
                {
                    ArrangeRole role = lviAnaeHelper.Tag as ArrangeRole;
                    alRole.Add(lviAnaeHelper.Tag);
                }
                this.anaeRecord.OperationApplication.RoleAl = alRole;


                //�Ƿ���PACU
                this.anaeRecord.IsPACU = this.cbxIsPacu.Checked;
                //�жϳ���PACU��ʱ��ĺ����ԣ�
                if (this.dtpInPacuDate.Value > this.dtpOutPacuDate.Value)
                {
                    MessageBox.Show("��PACU��ʱ�� ��ӦС�� ��PACU��ʱ��", "��ʾ",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dtpOutPacuDate.Focus();
                    return -1;
                }
                //����ʱ��
                this.anaeRecord.InPacuDate = this.dtpInPacuDate.Value;
                //����ʱ��
                this.anaeRecord.OutPacuDate = this.dtpOutPacuDate.Value;
                //����״̬
                this.anaeRecord.InPacuStatus.ID = this.cmbInStatus.Tag.ToString();
                this.anaeRecord.InPacuStatus.Name = this.cmbInStatus.Text;
                //����״̬
                this.anaeRecord.OutPacuStatus.ID = this.cmbOutStatus.Tag.ToString();
                this.anaeRecord.OutPacuStatus.Name = this.cmbOutStatus.Text;
                //�Ƿ���ʹ
                this.anaeRecord.IsDemulcent = this.cbxIsDemulcent.Checked;
                //��ʹ��ʽ
                this.anaeRecord.DemulcentType.ID = this.cmbDemuKind.Tag.ToString();
                this.anaeRecord.DemulcentType.Name = this.cmbDemuKind.Text;
                //����
                this.anaeRecord.DemulcentModel.ID = this.cmbDemuModel.Tag.ToString();
                this.anaeRecord.DemulcentModel.Name = this.cmbDemuModel.Text;
                //��ʹ����
                if (this.txtDemuDays.Text != "")
                    this.anaeRecord.DemulcentDays = int.Parse(this.txtDemuDays.Text);
                //�ι�ʱ��
                this.anaeRecord.PullOutDate = this.dtpPullOutDate.Value;
                //�ι���
                if (this.txtPullOutOpcd.Tag != null)
                {
                    this.anaeRecord.PullOutOperator.ID = this.txtPullOutOpcd.Tag.ToString();
                    this.anaeRecord.PullOutOperator.Name = this.txtPullOutOpcd.Text;
                }
                //��ʹЧ��
                this.anaeRecord.DemulcentEffect.ID = this.cmbDemuResult.Tag.ToString();
                this.anaeRecord.DemulcentEffect.Name = this.cmbDemuResult.Text;
                //��ע
                this.anaeRecord.Memo = this.txtRemark.Text;
                //{C7BDDFBF-BD3A-43c7-8057-432EC8B59338}
                if (this.cmdDirection.Tag != null)
                {
                    this.anaeRecord.Direction = this.cmdDirection.Tag.ToString();
                }
                //{26E31402-7D3C-4798-B2BE-C34F06C4FCC7}
                if (this.cmbDemuDrug.Tag != null)
                {
                    this.anaeRecord.DemuDrug = this.cmbDemuDrug.Tag.ToString();
                }
            }
            catch { return -1; }
            return 0;
        }

        public int Print()
        {
            if (this.anaeFormPrint == null)
            {
                this.anaeFormPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Operation.IAnaeFormPrint)) as FS.HISFC.BizProcess.Interface.Operation.IAnaeFormPrint;
                if (this.anaeFormPrint == null)
                {
                    MessageBox.Show("��ýӿ�IanaeFormPrint��������ϵͳ����Ա��ϵ��");

                    return -1;
                }
            }
            if (this.GetCtrlInfo() == -1)
                return -1;

            this.anaeFormPrint.AnaeRecord = this.anaeRecord;
            return this.anaeFormPrint.Print();
        }

        public enum OperType
        {
            /// <summary>
            /// ����
            /// </summary>
            New,
            /// <summary>
            /// �Ǽ�
            /// </summary>
            Reco,
            /// <summary>
            /// �޸�
            /// </summary>
            Modify,
            /// <summary>
            /// �޲���
            /// </summary>
            Null,
            
           
        }
        #endregion

        private void lvPersons_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item == null)
                return;

            if (e.Button == MouseButtons.Left)
            {

                DragDropEffects dropEffect = (sender as ListView).DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Move);
            }
        }

        private void lvAnaeDoct_DragDrop(object sender, DragEventArgs e)
        {

            ListViewItem item = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;

            if (sender == this.lvAnaeDoct)
            {
                //�������Ը�ֵ
                ArrangeRole myRole = new ArrangeRole(item.Tag as NeuObject);
                //��ɫ����

                myRole.RoleType.ID = EnumOperationRole.Anaesthetist;//��ɫ����
                myRole.OperationNo = this.anaeRecord.OperationApplication.ID;
                myRole.ForeFlag = "1";//����¼��			
                item.Tag = myRole;

                this.anaeRecord.OperationApplication.RoleAl.Add(myRole);//������Ա��ɫ����				
                this.anaeRecord.OperationApplication.User = FS.FrameWork.Management.Connection.Operator as Employee;//����Ա
            }
            else if (sender == this.lvAnaeHelper)
            {
                //�������Ը�ֵ
                ArrangeRole myRole = new ArrangeRole(item.Tag as NeuObject);
                //��ɫ����

                myRole.RoleType.ID = EnumOperationRole.AnaesthesiaHelper;//��ɫ����
                myRole.OperationNo = this.anaeRecord.OperationApplication.ID;
                myRole.ForeFlag = "1";//����¼��			
                item.Tag = myRole;

                this.anaeRecord.OperationApplication.RoleAl.Add(myRole);//������Ա��ɫ����				
                this.anaeRecord.OperationApplication.User = FS.FrameWork.Management.Connection.Operator as Employee;//����Ա
            }
            else if (sender == this.lvPersons)
            {
                EnumOperationRole role;
                if (item.ListView == this.lvAnaeDoct)
                    role = EnumOperationRole.Anaesthetist;
                else
                    role = EnumOperationRole.AnaesthesiaHelper;

                this.anaeRecord.OperationApplication.RemoveRole((item.Tag as NeuObject).ID, role);
            }

            item.ListView.Items.Remove(item);

            (sender as ListView).Items.Add(item);
        }

        private void lvAnaeDoct_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
    }
}

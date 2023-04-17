using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: �����շ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-20]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucFeeForm :UserControl
    {
        public ucFeeForm()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
                this.Clear();
        }
        #region �ֶ�
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        
        FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
        //{F3C1935C-24E9-47a4-B7AE-4EA237A972C9} 
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;


        /// <summary>
        /// �Ƿ���ʾ������{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        private bool isShowFeeRate = false;

        /// <summary>
        /// �Ƿ�ֻ��ʾ����ҽ��  ����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
        /// </summary>
        private bool isOnlyUO = false;

        /// <summary>
        /// �����շ��Ƿ�������ҽ�� ����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
        /// </summary>
        private bool isNeedUOOrder = false;

        /// <summary>
        /// �����շ�ʱ�Ƿ���²���Ա��Ϣ
        /// </summary>
        private bool isUpdateOpsOper = false;


        //{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
        ucRegistrationTree.EnumListType listType;

        //[Category("�ؼ�����"), Description("�ؼ����ͣ��������շ�")]
        public ucRegistrationTree.EnumListType ListType
        {
            get
            {
                return listType;
            }
            set
            {
                this.listType = value;
                this.InitControlName();
            }
        }

       

        #endregion
        #region ����

        /// <summary>
        /// �����ص�ҩƷ����// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        [Category("�ؼ�����"), Description("�����ص�ҩƷ���ʣ��á�,�����ֿ�")]
        public string NoAddDrugQuality
        {
            get
            {
                return this.ucInpatientCharge1.NoAddDrugQuality;
            }
            set
            {
                this.ucInpatientCharge1.NoAddDrugQuality = value;
            }
        }
        /// <summary>
        /// �����շ�ʱ�Ƿ���²���Ա��Ϣ
        /// </summary>
        [Category("�ؼ�����"), Description("���� �����շ�ʱ�Ƿ���²���Ա��Ϣ ")]
        public bool IsUpdateOpsOper
        {
            get
            {
                return isUpdateOpsOper;
            }
            set
            {
                isUpdateOpsOper = value;
            }
        }

        /// <summary>
        /// �����շ��Ƿ�������ҽ�� ����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
        /// </summary>
        [Category("�ؼ�����"), Description("���� �����շ��Ƿ�������ҽ�� ")]
        public bool IsNeedUOOrder
        {
            get
            {
                return
                    isNeedUOOrder;
            }
            set
            {
                isNeedUOOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ�ֻ��ʾ����ҽ�� ����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
        /// </summary>
        [Category("�ؼ�����"), Description("���� �Ƿ�ֻ��ʾ����ҽ�� ")]
        public bool IsOnlyUO
        {
            get
            {
                return isOnlyUO;
            }
            set
            {
                isOnlyUO = value;
            }
        }

        [Category("�ؼ�����"), Description("���øÿؼ����ص���Ŀ��� ҩƷ:drug ��ҩƷ undrug ����: all")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType ������Ŀ���
        {
            get
            {
                return ucInpatientCharge1.������Ŀ���;
            }
            set
            {
                ucInpatientCharge1.������Ŀ��� = value;
            }
        }
        /// <summary>
        /// �ؼ�����
        /// </summary>
        [Category("�ؼ�����"), Description("��û������øÿؼ�����Ҫ����"), DefaultValue(1)]
        public FS.HISFC.Components.Common.Controls.ucInpatientCharge.FeeTypes �ؼ�����
        {
            get
            {
                return this.ucInpatientCharge1.�ؼ�����;
            }
            set
            {
                this.ucInpatientCharge1.�ؼ����� = value;
            }
        }

        /// <summary>
        /// �Ƿ�����շѻ��߻���0���۵���Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("��û��������Ƿ�����շѻ��߻���"), DefaultValue(false)]
        public bool IsChargeZero
        {
            get
            {
                return this.ucInpatientCharge1.IsChargeZero;
            }
            set
            {
                this.ucInpatientCharge1.IsChargeZero = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ������"), DefaultValue(false)]
        public bool IsShowFeeRate
        {
            get { return this.ucInpatientCharge1.IsShowFeeRate; }
            set
            {
                 
                this.ucInpatientCharge1.IsShowFeeRate = value;
            }
        }

        [Category("�ؼ�����"), Description("�Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.ucInpatientCharge1.MessageType;
            }
            set
            {
                ucInpatientCharge1.MessageType = value;
            }
        }
        [Category("�ؼ�����"), Description("����Ϊ���Ƿ���ʾ��������")]
        public bool IsJudgeQty
        {
            get
            {
                return this.ucInpatientCharge1.IsJudgeQty;
            }
            set
            {
                this.ucInpatientCharge1.IsJudgeQty = value;
            }
        }

        [Category("�ؼ�����"), Description("ִ�п����Ƿ�Ĭ��Ϊ��½����")]
        public bool DefaultExeDeptIsDeptIn
        {
            get
            {
                return this.ucInpatientCharge1.DefaultExeDeptIsDeptIn;
            }
            set
            {
                this.ucInpatientCharge1.DefaultExeDeptIsDeptIn = value;
            }
        }
        // ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
        public OperationAppllication operationAppllication = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperationAppllication OperationAppllication
        {
            set
            {
                if (value == null)
                {
                    this.Clear();
                    return;
                }

                //this.PatientInfo = value.PatientInfo;
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                patientInfo = radtManager.GetPatientInfomation(value.PatientInfo.ID);
                if (patientInfo == null)
                {
                    MessageBox.Show("�Ҳ�������סԺ��Ϣ��");
                    this.Clear();
                    return;
                }
                PatientInfo = patientInfo;
                if (value.IsHeavy)
                    this.lbOwn.Text = "ͬ��ʹ���Է���Ŀ";
                else
                    this.lbOwn.Text = "��ͬ��ʹ���Է���Ŀ";
                OperationAppllication apply = value;
                this.cmbDoctor.Tag = apply.OperationDoctor.ID;//{F3C1935C-24E9-47a4-B7AE-4EA237A972C9};
                
                foreach(FS.HISFC.Models.Operation.ArrangeRole role in apply.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString())
                    {
                        this.ncmbScrubNurse.Tag = role.ID;
                        continue;
                    }
                    if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())
                    {
                        this.ncmbTourNurse.Tag = role.ID;
                        continue;
                    }
                    if (this.listType == ucRegistrationTree.EnumListType.Anaesthesia)
                    {
                        if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())
                        {
                            this.cmbDoctor.Tag = role.ID;
                            continue;
                        }
                    }
                }
                //{7AC68FBC-6BB7-4c66-B24F-4E0E3474A531}              
                if (this.cmbDoctor.Tag != null && this.cmbDoctor.Tag!="")
                {
                    this.ucInpatientCharge1.RecipeDoctCode = this.cmbDoctor.Tag.ToString();
                }
                else
                {
                    this.ucInpatientCharge1.RecipeDoctCode = Environment.OperatorID;
                }
                // ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
                operationAppllication = value;
                //��ʾ�ݴ�����
                try
                {
                    this.SetValue();
                }
                catch
                {

                }
            }
            // ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
            get
            {
                return this.operationAppllication;
            }
        }

        ////{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
        private void InitControlName()
        {
            if (this.listType == ucRegistrationTree.EnumListType.Anaesthesia)
            {
                this.neuLabel3.Text = "����ҽ��";
                this.neuLabel1.Text = "�����շ�";
            }
        }
      
        private FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                try
                {
                    if (value.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("���߲�����Ժ״̬�����ܽ����շѲ���"));
                        this.Clear();

                        return;
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message.ToString());
                    return;
                }

                #region {2FA6F515-92FA-4251-8573-8DD5FC1E5E1C}
                this.lblBedNO.Text = value.PVisit.PatientLocation.Bed.ID.Substring(4);
                this.lblNurseCell.Text = value.PVisit.PatientLocation.NurseCell.Name;
                #endregion
                this.ucInpatientCharge1.Clear();
                this.lbName.Text = value.Name;
                this.lbAge.Text = value.Age;
                this.lbSex.Text = value.Sex.Name;
                this.lbPatient.Text = value.PID.PatientNO;
                this.lbDept.Text = value.PVisit.PatientLocation.Dept.Name;
                this.lbPayKind.Text = Environment.GetPayKind(value.Pact.PayKind.ID).Name;
                //this.ucInpatientCharge1.RecipeDoctCode = Environment.OperatorID;
                //Ϊ�˵������������Ѻ�Ŀ�������
                this.ucInpatientCharge1.RecipeDept = Environment.OperatorDept;
                this.cmbDept.Tag = Environment.OperatorDept.ID;
                //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                    this.ucInpatientCharge1.DefautStockDept = new FS.FrameWork.Models.NeuObject();
                    this.ucInpatientCharge1.DefautStockDept.ID = this.cmbStockDept.Tag.ToString();
                //����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��

                FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
                ArrayList alOrder = orderIntegrate.QueryOrder(value.ID, FS.HISFC.Models.Order.EnumType.SHORT, 1);
                ArrayList alOrder2 = orderIntegrate.QueryOrder(value.ID, FS.HISFC.Models.Order.EnumType.SHORT, 2);
                alOrder.AddRange(alOrder2);

                ArrayList alUO = new ArrayList();

                foreach (FS.HISFC.Models.Order.Inpatient.Order ord in alOrder)
                {
                    FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject(); 

                    neuObj.ID = ord.ID;
                    neuObj.Name = ord.Item.Name;

                    if (isOnlyUO)
                    {
                        if (ord.Item.SysClass.ID.ToString() == "UO")
                        {                           
                            alUO.Add(neuObj);                            
                        }
                    }
                    else
                    {
                        alUO.Add(neuObj);
                    }
                }

                this.cmbOrder.Tag = null;
                this.cmbOrder.Text = "";
                this.cmbOrder.AddItems(alUO);
                
                
                this.ucInpatientCharge1.PatientInfo = value;
              

                this.cmbDoctor.Focus();
            }
        }

        private string defaultStorageDept=string.Empty;
        public string DefaultStorageDept
        {
            get
            {
                return defaultStorageDept;
            }
            set
            {
                defaultStorageDept = value;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            #region {2FA6F515-92FA-4251-8573-8DD5FC1E5E1C}
            this.lblBedNO.Text = "";
            this.lblNurseCell.Text = "";
            #endregion
            this.lbPayKind.Text = "";
            this.lbName.Text = "";
            this.lbPatient.Text = "";
            this.lbDept.Text = "";
            this.lbSex.Text = "";
            this.lbOwn.Text = "";
            this.lbAge.Text = string.Empty;
            this.lbDate.Text = Environment.OperationManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");

            //appObj = new FS.HISFC.Object.Operator.OpsApplication();
            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox5.Checked = false;

            this.ucInpatientCharge1.Clear();
            this.cmbDoctor.Tag = "";
            this.ncmbScrubNurse.Tag = "";
            this.ncmbTourNurse.Tag = "";
            //����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
            this.cmbOrder.Tag = null;
            this.cmbOrder.Text = "";
            //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
            if (this.cmbStockDept.Items.Count > 0)
            {
                this.cmbStockDept.Tag = defaultStorageDept;
            }
        }
        /// <summary>
        /// ���棬�����շ�
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //// ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
            this.ucInpatientCharge1.OperationNO = this.operationAppllication.ID;
            //>>{74326E54-1315-4c07-A53E-7E515C364015}��ȡѡ���ȡҩ����20120730kjl
            this.ucInpatientCharge1.DefautStockDept.ID = this.cmbStockDept.Tag.ToString();
            //<<

            //����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
            if (this.isNeedUOOrder)
            {
                if ( this.cmbOrder == null || string.IsNullOrEmpty(this.cmbOrder.Tag.ToString()))
                {
                    MessageBox.Show("��ѡ�񱾴��շѹ���������ҽ��.");

                    return -1;
                }
            }
    
            this.ucInpatientCharge1.OrderID = this.cmbOrder.Tag.ToString();
            this.ucInpatientCharge1.IsPopSHowInvoice = true;
            this.ucInpatientCharge1.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("020");
            ucInpatientCharge1.RecipeDoctCode = cmbDoctor.Tag.ToString();
            if (this.ucInpatientCharge1.Save() < 0) return -1;
            #region ����������Ա��Ϣ
            if (this.IsUpdateOpsOper)
            {
                if (this.OperationAppllication != null && !string.IsNullOrEmpty(this.OperationAppllication.ID))
                {
                    FS.HISFC.Models.Operation.OperationAppllication applicationInfo = Environment.OperationManager.GetOpsApp(this.OperationAppllication.ID);
                    if(this.cmbDoctor.SelectedIndex != -1)
                    {
                        applicationInfo.OperationDoctor.ID = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                        applicationInfo.OperationDoctor.Name = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                    }
                    bool iscontainWashingHandNurse1 = false;
                    bool iscontainItinerantNurse1 = false;
                    FS.HISFC.Models.Operation.ArrangeRole roleTmp = null;
                    foreach (FS.HISFC.Models.Operation.ArrangeRole role in applicationInfo.RoleAl)
                    {
                        if (role.RoleType.ID.ToString() == EnumOperationRole.Operator.ToString())
                        {
                            roleTmp = (FS.HISFC.Models.Operation.ArrangeRole)role.Clone();
                            if (this.cmbDoctor.SelectedIndex != -1)
                            {
                                role.ID = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                                role.Name = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                            }
                        }
                        if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString())
                        {
                            iscontainWashingHandNurse1 = true;
                            if (this.ncmbScrubNurse.SelectedIndex != -1)
                            {
                                role.ID = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                                role.Name = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                            }
                        }
                        if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())
                        {
                            iscontainItinerantNurse1 = true;
                            if (this.ncmbTourNurse.SelectedIndex != -1)
                            {
                                role.ID = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                                role.Name = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                            }
                        }
                    }
                    if (!iscontainItinerantNurse1 && this.ncmbTourNurse.SelectedIndex != -1)
                    {
                        FS.HISFC.Models.Operation.ArrangeRole arranyRole = (FS.HISFC.Models.Operation.ArrangeRole)roleTmp.Clone();
                        arranyRole.RoleType.ID = EnumOperationRole.ItinerantNurse1;
                        arranyRole.ID = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                        arranyRole.Name = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                        applicationInfo.RoleAl.Add(arranyRole);
                    }

                    if (!iscontainWashingHandNurse1 && this.ncmbScrubNurse.SelectedIndex != -1)
                    {
                        FS.HISFC.Models.Operation.ArrangeRole arranyRole = (FS.HISFC.Models.Operation.ArrangeRole)roleTmp.Clone();
                        arranyRole.RoleType.ID = EnumOperationRole.WashingHandNurse1;
                        arranyRole.ID = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                        arranyRole.Name = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                        applicationInfo.RoleAl.Add(arranyRole);
                    }
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (Environment.OperationManager.UpdateApplicationByFee(applicationInfo) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            #endregion

            MessageBox.Show(this.ucInpatientCharge1.SucessMsg);



            this.Print();

            this.ucInpatientCharge1.Clear();
            this.Clear();
            return 1;
        }
        #region �ݴ洦�� 2012-12-07 chengym
        /// <summary>
        /// �ݴ�
        /// </summary>
        /// <returns></returns>
        public int TempSave()
        {
            //// ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
            this.ucInpatientCharge1.OperationNO = this.operationAppllication.ID;
            //>>{74326E54-1315-4c07-A53E-7E515C364015}��ȡѡ���ȡҩ����20120730kjl
            this.ucInpatientCharge1.DefautStockDept.ID = this.cmbStockDept.Tag.ToString();
            //<<

            //����ѡ������ҽ������  {B9932D8E-ACFF-4a90-A252-B99136DD5285} �����շѹ�������ҽ��
            if (this.isNeedUOOrder)
            {
                if (this.cmbOrder == null || string.IsNullOrEmpty(this.cmbOrder.Tag.ToString()))
                {
                    MessageBox.Show("��ѡ�񱾴��շѹ���������ҽ��.");

                    return -1;
                }
            }

            this.ucInpatientCharge1.OrderID = this.cmbOrder.Tag.ToString();
            this.ucInpatientCharge1.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("020");
            if (this.ucInpatientCharge1.TemparorySave() < 0) return -1;
            //MessageBox.Show(this.ucInpatientCharge1.SucessMsg);
            this.ucInpatientCharge1.Clear();
            this.Clear();

            return 1;
        }
        /// <summary>
        /// ��ʾ�ݴ�
        /// </summary>
        /// <returns></returns>
        private int SetValue()
        {
            this.ucInpatientCharge1.OperationNO = this.operationAppllication.ID; 
            if (this.ucInpatientCharge1.SetValue() == -1)
            {
                return -1;
            }
            return 0;
        }
        #endregion
        /// <summary>
        /// ɾ����ǰ��
        /// </summary>
        /// <returns></returns>
        public int DelRow()
        {
            return this.ucInpatientCharge1.DelRow();
        }
        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}


        public void InsertGroup(string groupID)
        {
            frmChooseGroupDetails frm = new frmChooseGroupDetails();
            frm.GroupID = groupID;
          DialogResult dr =   frm.ShowDialog();
          if (dr == DialogResult.Cancel)
          {
              return;
          }
          else
          {
              FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���������Ϣ");
              Application.DoEvents();
              this.ucInpatientCharge1.AddGroupDetail(groupID,frm.AlReturnDetails); 



              FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
          }

            frm.Dispose();

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���������Ϣ");
            //Application.DoEvents();
            ////this.ucInpatientCharge1.AddGroupDetail(groupID); 

            

            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        public int Print()
        {
            return this.print.PrintPreview(this);
        }
        #endregion

        #region �¼�
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Environment.DesignMode)
            {
                if (!this.IsUpdateOpsOper)
                {
                    this.neuLabel15.Visible = false;
                    this.neuLabel16.Visible = false;
                    this.ncmbScrubNurse.Visible = false;
                    this.ncmbTourNurse.Visible = false;
                }
                FS.HISFC.Models.Base.Employee em = (FS.HISFC.Models.Base.Employee)Environment.AnaeManager.Operator;
                this.ucInpatientCharge1.Init(em.Dept.ID);
                //�������ҽ�� {F3C1935C-24E9-47a4-B7AE-4EA237A972C9}
                FS.HISFC.BizProcess.Integrate.Manager conMag = new FS.HISFC.BizProcess.Integrate.Manager();

                ArrayList aNurse = conMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N, ((FS.HISFC.Models.Base.Employee)Environment.AnaeManager.Operator).Dept.ID);
                if (aNurse != null)
                {
                    this.ncmbScrubNurse.AddItems(aNurse);
                    this.ncmbTourNurse.AddItems(aNurse);
                }
                ArrayList alDept = conMag.GetDepartment();
                if (alDept != null)
                {
                    this.cmbDept.AddItems(alDept);
                }
                //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                ArrayList alStockDept = conMag.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.P.ToString());
                if (alStockDept != null)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = "default";
                    obj.Name = "Ĭ��ҩ��";
                    alStockDept.Insert(0,obj);
                    this.cmbStockDept.AddItems(alStockDept);
                    this.cmbStockDept.Tag = defaultStorageDept;
                }
            }
        }

        public void InitDoctList()
        {
            FS.HISFC.BizProcess.Integrate.Manager conMag = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList aDoc = new ArrayList();
            if (this.listType == ucRegistrationTree.EnumListType.Anaesthesia)
            {
                aDoc = conMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            }
            else
            {
                aDoc = conMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            }

            if (aDoc != null)
            {
                this.cmbDoctor.AddItems(aDoc);
            }
        }

        #endregion
        //{F3C1935C-24E9-47a4-B7AE-4EA237A972C9} 
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo != null)
            {
                this.patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoctor.Tag.ToString();
                this.patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoctor.Text;
                this.ucInpatientCharge1.RecipeDoctCode = this.cmbDoctor.Tag.ToString();
                this.ucInpatientCharge1.RecipeDept = ((FS.HISFC.Models.Base.Employee)this.cmbDoctor.SelectedItem).Dept;
            }
        }
        //{F3C1935C-24E9-47a4-B7AE-4EA237A972C9} 
        private void cmbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucInpatientCharge1.Focus();
            }


        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = null;
            try
            {
                obj = this.cmbDept.SelectedItem as FS.FrameWork.Models.NeuObject;
            }
            catch (Exception)
            {

                return;
            }



            this.ucInpatientCharge1.RecipeDept = obj;

        }
    }
}

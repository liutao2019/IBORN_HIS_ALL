using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Components.Common.Classes;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmRegistrationByDoctor : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmRegistrationByDoctor(string patientName) 
        {
            InitializeComponent();
            this.txtName.Text = patientName;
        }

        #region ����

        /// <summary>
        /// �Զ����ɵĿ���
        /// </summary>
        protected string autoCardNO = string.Empty;

        /// <summary>
        /// �����Һ���ؽӿ�
        /// </summary>
        public FS.HISFC.BizProcess.Interface.Order.IAfterQueryRegList IAfterQueryRegList = null;

        /// <summary>
        /// ������ˮ��
        /// </summary>
        protected string clinicNO = string.Empty;

        /// <summary>
        /// û�йҺŻ���,���ŵ�һλ��־,Ĭ����9��ͷ
        /// </summary>
        protected string noRegFlagChar = "9";

        /// <summary>
        /// �Һ���Ϣʵ��
        /// </summary>
        protected FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// ����ҽ��ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Order.OutPatient.Order orderManagement = new FS.HISFC.BizLogic.Order.OutPatient.Order();
        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �Ƿ�������ң�������ҹҺż���ʼ��������
        /// </summary>
        bool isOrdinaryRegDept = false;

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regManagement = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��������ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ����Ա
        /// </summary>
        protected FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        private FS.HISFC.BizLogic.RADT.InPatient radtMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private FS.HISFC.Models.Base.Employee doct = null;

        /// <summary>
        /// �������Զ��Һŵĺ�ͬ��λ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper noAutoRegPactHelper = null;

        /// <summary>
        /// �ҺŹ�����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
       

        #endregion

        #region ����

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        /// <summary>
        /// ���ֳ���
        /// </summary>
        private const string CLASS1DESEASE = "CLASS1DESEASE";
        /// <summary>
        /// һ������
        /// </summary>
        private ArrayList Class1Desease = new ArrayList();

        /// <summary>
        /// ����Һż���
        /// </summary>
        private string emergencyLevlCode;

        /// <summary>
        /// ����Һż���
        /// </summary>
        public string EmergencyLevlCode
        {
            get
            {
                return emergencyLevlCode;
            }
            set
            {
                emergencyLevlCode = value;
            }
        }

        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.register;
            }
        }

        /// <summary>
        /// �Һż��������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper regLevlHelper = null;

        /// <summary>
        /// ��½��Ա
        /// </summary>
        private  FS.HISFC.Models.Base.Employee logEmpl = null;

        /// <summary>
        /// ��½��Ա
        /// </summary>
        public  FS.HISFC.Models.Base.Employee LogEmpl
        {
            get
            {
                if (logEmpl == null)
                {
                    logEmpl = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator);
                }
                return logEmpl;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private void InitControl()
        {
            //��ʼ�����Ʋ��� 

            //{3F495C28-F952-43fb-8B19-C63DDAB2F749}
            if(ctlMgr.QueryControlerInfo("DZ0001")=="1")
            {
              IsdiseaseMust=true;
            }
            else
            {
            IsdiseaseMust=false;
            }
            //��ʼ����ͬ��λ
            ArrayList pactList = this.interMgr.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("��ʼ����ͬ��λ����!" + this.interMgr.Err);

                return;
            }
            this.cmbPact.AddItems(pactList);

            //��ʼ���Ա�
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

            //��ÿ���ǰ��λ����
            this.noRegFlagChar = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");

            this.autoCardNO = this.feeManagement.GetAutoCardNO();
            if (autoCardNO == string.Empty || autoCardNO == "" || autoCardNO == null)
            {
                MessageBox.Show("������￨�ų���!" + this.feeManagement.Err);

                return;
            }
            //autoCardNO = this.noRegFlagChar + autoCardNO;
            autoCardNO = this.autoCardNO.PadLeft(10, '0');
            //this.txtCardNo.Text = this.autoCardNO;

            this.clinicNO = this.orderManagement.GetSequence("Registration.Register.ClinicID");
            if (clinicNO == string.Empty || clinicNO == "" || clinicNO == null)
            {
                MessageBox.Show("����������ų���!" + this.orderManagement.Err);

                return;
            }

            this.cmbSex.Tag = "M";

            this.cmbPact.Tag = "1";

            this.doct = this.interMgr.GetEmployeeInfo(this.employee.ID);
            if (this.doct == null)
            {
                MessageBox.Show(this.interMgr.Err);
            }

            this.lblTip.Text = "";

            if (noAutoRegPactHelper == null)
            {
                noAutoRegPactHelper = new FS.FrameWork.Public.ObjectHelper();
                noAutoRegPactHelper.ArrayObject = this.interMgr.GetConstantList("NoAutoRegPact");
            }

            #region ��ȡ���йҺż���
            if (regLevlHelper == null)
            {
                regLevlHelper = new FS.FrameWork.Public.ObjectHelper();

                //��ȡ���еĹҺż���
                ArrayList al = regManagement.QueryAllRegLevel();

                regLevlHelper.ArrayObject = al;

                //��Ч�ĹҺż���
                ArrayList alValidReglevl = new ArrayList();

                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("��ѯ���йҺż�����󣡻ᵼ�²��չҺŷѴ���!\r\n����ϵ��Ϣ������ά��" + regManagement.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    bool isValidEmergency = true;
                    foreach (FS.HISFC.Models.Registration.RegLevel regLevl in al)
                    {
                        if (regLevl.IsValid)
                        {
                            alValidReglevl.Add(regLevl);

                            if (regLevl.IsEmergency)
                            {
                                emergencyLevlCode = regLevl.ID;
                                //break;
                            }
                        }
                        else if (regLevl.IsEmergency)
                        {
                            isValidEmergency = false;
                        }
                    }

                    this.cmbRegLevl.AddItems(alValidReglevl);
                }
            }
            #endregion

            this.SetEnabled(false);

            #region �����Ƿ���������Һ�

            btAutoCardNo.Visible = false;
            if (FrameWork.WinForms.Classes.Function.IsManager()
                //||����Ҫ�����Ʋ���������,���������˰�~
                )
            {
                btAutoCardNo.Visible = true;
            }

            #endregion

            //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
            this.Class1Desease = this.interMgr.QueryConstantList(CLASS1DESEASE);

            if (Class1Desease != null)
            {
                ArrayList deptDesease = new ArrayList();
                HISFC.Models.Base.Employee oper = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = oper.Dept as HISFC.Models.Base.Department;
                foreach (FS.FrameWork.Models.NeuObject obj in this.Class1Desease)
                {
                    if (obj.Memo.Contains(dept.ID) || obj.Memo.Contains("ALL"))
                    {
                        deptDesease.Add(obj);
                    }
                }

                if (deptDesease.Count > 0)
                {
                    this.cmbClass1Desease.AddItems(deptDesease);
                }
            }

            this.cmbClass1Desease.SelectedIndexChanged += new EventHandler(cmbClass1Desease_SelectedIndexChanged);
        }

        //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
        private void cmbClass1Desease_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbClass2Desease.Items.Clear();
            string class1desease = this.cmbClass1Desease.Tag.ToString();
            if (string.IsNullOrEmpty(class1desease))
            {
                return;
            }
            string queryCode = CLASS1DESEASE + class1desease;

            ArrayList class2desease = this.interMgr.QueryConstantList(queryCode);
            if (class2desease == null)
            {
                return;
            }

            this.cmbClass2Desease.AddItems(class2desease);
        }

        /// <summary>
        /// �Զ����ɿ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAutoCardNo_Click(object sender, EventArgs e)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

            this.autoCardNO = regMgr.AutoGetCardNO().ToString(); //this.feeManagement.GetAutoCardNO();
            if (autoCardNO == string.Empty || autoCardNO == "" || autoCardNO == null)
            {
                MessageBox.Show("������￨�ų���!" + this.feeManagement.Err);
                return;
            }
            //autoCardNO = this.noRegFlagChar + autoCardNO;
            autoCardNO = this.autoCardNO.PadLeft(10, '0');
            this.txtCardNo.Text = this.autoCardNO;

            this.SetEnabled(true);
            this.txtName.Focus();
        }

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        /// <returns></returns>
        private int SetRegister()
        {
            DateTime now = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.ID = clinicNO;
            this.register.Name = this.txtName.Text.Trim();
            //this.register.Card.ID = autoCardNO;
            //this.register.PID.CardNO = autoCardNO;
            this.register.Card.ID = this.txtCardNo.Text;
            this.register.PID.CardNO = this.txtCardNo.Text;
            this.register.IDCard = this.txtIDCard.Text;

            if (this.register.PID.CardNO.Length < 10)
            {
                this.register.PID.CardNO.PadLeft(10, '0');
            }

            this.register.PhoneHome = this.txtPhone.Text;
            this.register.AddressHome = this.txtAddress.Text;

            #region ��ͬ��λ

            if (this.cmbPact.Tag == null || string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
            {
                MessageBox.Show("��ѡ���ͬ��λ��");
                return -1;
            }

            FS.HISFC.Models.Base.PactInfo pactObj = interMgr.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
            if (pactObj == null)
            {
                MessageBox.Show("��ȡ��ͬ��λ��Ϣ����" + interMgr.Err);
                return -1;
            }
            this.register.Pact = pactObj;
            #endregion


            this.register.Sex.ID = this.cmbSex.Tag.ToString();
            this.register.Birthday = this.dtPickerBirth.Value;
            this.register.DoctorInfo.SeeDate = now; 
            this.register.DoctorInfo.SeeNO = -1;
            this.register.DoctorInfo.Templet.Dept = this.employee.Dept;

            this.register.InputOper.ID = this.employee.ID;
            this.register.InputOper.OperTime = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.DoctorInfo.SeeDate = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.DoctorInfo.Templet.Begin = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.DoctorInfo.Templet.End = this.orderManagement.GetDateTimeFromSysDateTime();
            this.register.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;

            #region ���
            if (this.register.DoctorInfo.SeeDate.Hour < 12 && this.register.DoctorInfo.SeeDate.Hour > 6)
            {
                //����
                this.register.DoctorInfo.Templet.Noon.ID = "1";
            }
            else if (this.register.DoctorInfo.SeeDate.Hour > 12 && this.register.DoctorInfo.SeeDate.Hour < 18)
            {
                //����
                this.register.DoctorInfo.Templet.Noon.ID = "2";
            }
            else
            {
                //����
                this.register.DoctorInfo.Templet.Noon.ID = "3";
            }
            #endregion

            #region �Һż���

            this.register.DoctorInfo.Templet.RegLevel = this.cmbRegLevl.SelectedItem as FS.HISFC.Models.Registration.RegLevel;

            #endregion

            this.register.IsFee = false;
            this.register.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
            this.register.IsSee = false;
            this.register.PVisit.InState.ID = "N";

            //{5D855B3C-5A4F-42c9-931D-333D0A01D809}
            this.register.Class1Desease = this.cmbClass1Desease.Tag.ToString();
            this.register.Class2Desease = this.cmbClass2Desease.Tag.ToString();

            //{75ADC0C9-77FC-45ee-8E74-8CDDE328FA33}
            //Ժ�������Ƽ�����Ƽ����ҽ������������
            int isHospitalFirst = this.regMgr.IsFirstRegister("1", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, this.register.DoctorInfo.Templet.Doct.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
            int isRootDeptFirst = this.regMgr.IsFirstRegister("2", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, this.register.DoctorInfo.Templet.Doct.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
            int isDeptFirst = this.regMgr.IsFirstRegister("3", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, this.register.DoctorInfo.Templet.Doct.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);
            int isDoctFirst = this.regMgr.IsFirstRegister("4", this.register.PID.CardNO, this.register.DoctorInfo.Templet.Dept.ID, FS.FrameWork.Management.Connection.Operator.ID, this.regMgr.GetDateTimeFromSysDateTime().AddYears(-1).Date);

            this.register.HospitalFirstVisit = isHospitalFirst > 0 ? "0" : "1";
            this.register.RootDeptFirstVisit = isRootDeptFirst > 0 ? "0" : "1";
            this.register.IsFirst = isDeptFirst > 0 ? false : true;
            this.register.DoctFirstVist = isDoctFirst > 0 ? "0" : "1";

            register.DoctorInfo.Templet.Doct = employee;

            //return this.register;
            return 1;
        }

        /// <summary>
        /// ��Ч��У��
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        private bool CheckRegister(FS.HISFC.Models.Registration.Register reg)
        {
            if (reg.ID.Trim() == "" || reg.ID == null)
            {
                MessageBox.Show("�������Ų���Ϊ�գ�");
                return false;
            }
            if (reg.Name.Trim() == "" || reg.Name == null)
            {
                MessageBox.Show("��������Ϊ�գ�");
                return false;
            }
            if (reg.PID.CardNO.Trim() == "" || reg.PID.CardNO == null)
            {
                MessageBox.Show("���￨�Ų���Ϊ�գ�");
                return false;
            }
            if (reg.Sex.ID.ToString().Trim() == "" || reg.Sex.ID == null)
            {
                MessageBox.Show("�Ա𲻿�Ϊ�գ�");
                return false;
            }

            FS.HISFC.Models.Base.Const conObj = noAutoRegPactHelper.GetObjectFromID(cmbPact.Tag.ToString()) as FS.HISFC.Models.Base.Const;

            if (this.cmbPact.Tag != null && !string.IsNullOrEmpty(this.cmbPact.Tag.ToString()) && conObj != null)
            {
                MessageBox.Show("��ͬ��λ��" + cmbPact.Text + "��" + conObj.Memo);
                return false;
            }

            if (IAfterQueryRegList != null)
            {
                if (IAfterQueryRegList.OnConfirmRegInfo(this.register) == -1)
                {
                    MessageBox.Show(IAfterQueryRegList.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            //{3F495C28-F952-43fb-8B19-C63DDAB2F749}
            if (cmbClass1Desease.Text.ToString() == "" || cmbClass2Desease.Text.ToString() == "")
            {
                #region ���������ж�һ�����������Ƿ�����д
                string deptCode = this.LogEmpl.Dept.ID;
                bool IsEkDept = false;
                if (deptCode == "5021")
                {
                   IsEkDept = true;
                }
                #endregion
                if (IsdiseaseMust == true || IsEkDept == true)
                {
                    MessageBox.Show("һ���������ֲ���Ϊ��");
                    return false;
                }
                else
                {
                    return true;
                }
            }


            return true;
        }

        private int InsertRegInfo(FS.HISFC.Models.Registration.Register reg)
        {
            this.regManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iReturn = -1;
            reg.InputOper.ID = this.employee.ID;
            reg.InputOper.Name = this.employee.Name;
            //reg.InputOper.OperTime = reg.DoctorInfo.SeeDate;
            iReturn = this.regManagement.Insert(reg);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (regManagement.DBErrCode != 1)//���������ظ�
                {
                    MessageBox.Show("����Һ���Ϣ����!" + regManagement.Err);

                    return -1;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return iReturn;
        }

        #endregion

        //{3F495C28-F952-43fb-8B19-C63DDAB2F749}
        #region ��������
        /// <summary>
        /// ����������
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
        bool IsdiseaseMust = false;
        #endregion
       
        private void btnOK_Click(object sender, EventArgs e)
        {
            //����Ҫ�ж�һ�£���ȡ������Ϣ���ǲ������޸Ĺ�����
            if (patientInfo != null
                && !string.IsNullOrEmpty(patientInfo.PID.CardNO)
                && patientInfo.PID.CardNO != txtCardNo.Text)
            {
                MessageBox.Show("�޸�����ź���س�ȷ�ϣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.txtCardNo.Focus();
                return;
            }

            if (patientInfo != null && string.IsNullOrEmpty(this.cmbRegLevl.Text))
            {
                //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                MessageBox.Show("��ѡ��Һż���", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (this.SetRegister() == -1)
            {
                return;
            }

            #region �жϹҺ���Ϣ
            if (!this.CheckRegister(this.register))
            {
                return;
            }
            #endregion           

            #region ���滼�߻�����Ϣ

            #region �жϻ�����Ϣ

            if (string.IsNullOrEmpty(this.patientInfo.Card.ID))
            {
                this.patientInfo.Name = this.txtName.Text.Trim();
                this.patientInfo.Card.ID = this.txtCardNo.Text;
                this.patientInfo.PID.CardNO = this.txtCardNo.Text;

                this.patientInfo.PhoneHome = this.txtPhone.Text;
                this.patientInfo.AddressHome = this.txtAddress.Text;
                this.patientInfo.IDCard = this.txtIDCard.Text;

                #region ��ͬ��λ

                if (this.cmbPact.Tag == null || string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
                {
                    MessageBox.Show("��ѡ���ͬ��λ��");
                    return;
                }
                FS.HISFC.Models.Base.PactInfo pactObj = interMgr.GetPactUnitInfoByPactCode(this.cmbPact.Tag.ToString());
                if (pactObj == null)
                {
                    MessageBox.Show("��ȡ��ͬ��λ��Ϣ����" + interMgr.Err);
                    return;
                }
                this.patientInfo.Pact = pactObj;
                #endregion

                this.patientInfo.Sex.ID = this.cmbSex.Tag.ToString();
                this.patientInfo.Birthday = this.dtPickerBirth.Value;

                //�����жϣ�����ҽ����Ϊ�޸Ŀ��ţ����¹Һŵ���Ϣ�ͻ���ʵ�ʷ���Ŀ��Ų�һ��
                FS.HISFC.Models.RADT.Patient patientCommonInfo = this.interMgr.QueryComPatientInfo(patientInfo.PID.CardNO);
                if (!string.IsNullOrEmpty(patientCommonInfo.PID.CardNO)  
                    && patientCommonInfo.Name != patientInfo.Name)
                {
                    MessageBox.Show("���ڿ��Ŵ��س�ȷ�ϣ�\r\n\r\nԭ�򣺿��š�" + patientInfo.PID.CardNO + "����Ӧ��������" + patientCommonInfo.Name + "������ʾ��������" + patientInfo.Name + "����һ�£�\r\n�����޸Ļ�����Ϣ���뻼�ߵ������շѴ��޸ģ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            //{2888444F-50BA-4956-A5F7-D71F0C6448BB}
            #region �ж��Ƿ����Ѵ������ҽ���ĹҺ�
            DateTime now = regManagement.GetDateTimeFromSysDateTime();

            int tmp = regManagement.QueryRegisterByCardNODoctTime(register.PID.CardNO, register.DoctorInfo.Templet.Dept.ID,employee.ID, now.Date);

            if (tmp > 0)
            {
                MessageBox.Show("�û��߽����Ѿ��ҹ����ĺţ��������������б��в��ң�");
                return;
            }
            #endregion


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.regManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.register.InputOper.ID = this.employee.ID;
            register.InputOper.Name = this.employee.Name;
            int iReturn = this.regManagement.Insert(register);
            if (iReturn == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (regManagement.DBErrCode != 1)//���������ظ�
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����Һ���Ϣ����!" + regManagement.Err);
                    return;
                }
            }

            /*
             * 2020-02-12 huyungui
             * {067831BF-DDA5-4ac3-958A-4DD0BE5B085F}
             * ��Ϊ�������պ͹���ҽ������Ҫ���֣����ܹ���ҽ�����Һ�ѡ��ĺ�ͬ��λ����Ӱ�쵽���߻�����Ϣ�ĺ�ͬ��λ�����Բ���update
            this.radtMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.radtMgr.UpdatePatientInfo(this.patientInfo) <= 0)
            {
                if (this.radtMgr.InsertPatientInfo(this.patientInfo) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���뻼�߻�����Ϣ����" + radtMgr.Err);
                    return;
                }
            }
             * */

            FS.FrameWork.Management.PublicTrans.Commit();

            #endregion
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmRegistrationByDoctor_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        private void btnCaecel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SetEnabled(bool val)
        {
            //this.cmbPact.Enabled = val;
            this.cmbSex.Enabled = val;
            this.txtName.Enabled = val;
            this.txtIDCard.Enabled = val;
            //this.dtPickerBirth.Enabled = val;
            this.txtCardNo.Enabled = !val;
            this.txtPhone.Enabled = val; //�����绰���ַ
            this.txtAddress.Enabled = val;
        }

        private void Clear()
        {
            this.txtName.Text = "";

            this.txtIDCard.Text = "";

            this.txtPhone.Text = "";

            this.txtAddress.Text = "";
            cmbPact.Tag = null;
            this.lblTip.Text = "";

            this.cmbRegLevl.Tag = null;

            this.cmbSex.Tag = null;
            this.dtPickerBirth.Value = DateTime.Now;

        }

        /// <summary>
        /// ���û��߻�����Ϣ
        /// </summary>
        /// <param name="patientObj"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.Patient patientObj)
        {
            if (patientObj != null)
            {
                this.SetEnabled(false);

                this.txtCardNo.Text = patientObj.PID.CardNO;

                this.txtName.Text = patientObj.Name;

                this.txtIDCard.Text = patientObj.IDCard;

                this.txtPhone.Text = patientObj.PhoneHome;  //�����ӵ���ϵ�绰���ͥסַ by zhy 

                this.txtAddress.Text = patientObj.AddressHome;

                #region ��ͬ��λ

                //this.cmbPact.Enabled = true;
                this.cmbPact.Tag = patientObj.Pact.ID;

                if (this.cmbPact.Tag == null || string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
                {
                    this.cmbPact.Tag = "1";
                }

                this.lblTip.Text = "";

                #region ��ͬ��λȫ���ԷѴ���

                ArrayList alOwnFeeRegDept = this.conManager.GetList("OwnFeeRegDept");
                if (alOwnFeeRegDept == null)
                {
                    MessageBox.Show("��ȡ�Էѿ������ʧ�ܣ�" + conManager.Err);
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOwnFeeRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.employee.Dept.ID)
                    {
                        ArrayList alOwnFeeRegLevl = this.conManager.GetList("OwnFeeRegLevl");
                        if (alOwnFeeRegLevl == null || alOwnFeeRegLevl.Count == 0)
                        {
                            MessageBox.Show("��ȡ�ԷѹҺż���ʧ�ܣ�" + conManager.Err);
                        }

                        foreach (FS.HISFC.Models.Base.Const obj in alOwnFeeRegLevl)
                        {
                            if (obj.IsValid)
                            {
                                this.cmbPact.Tag = obj.ID;
                                this.lblTip.Text = "��ʾ��ϵͳ���ñ�����ֻ�ܹҺš�" + cmbPact.Text + "����ͬ��λ��";
                                //this.cmbPact.Enabled = false;
                                break;
                            }
                        }

                        break;
                    }
                }
                #endregion

                #endregion

                #region �Һż���

                string regLevl = "";

                isOrdinaryRegDept = false;

                #region ����Һſ���
                ArrayList alOrdinaryRegDept = this.conManager.GetList("OrdinaryRegLevlDept");
                if (alOrdinaryRegDept == null)
                {
                    MessageBox.Show("��ȡ����Һſ���ʧ�ܣ�" + conManager.Err);
                    return;
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryRegDept)
                {
                    if (constObj.IsValid && constObj.ID.Trim() == this.employee.Dept.ID)
                    {
                        isOrdinaryRegDept = true;
                        break;
                    }
                }

                #endregion

                //����
                if (isOrdinaryRegDept)
                {
                    ArrayList alOrdinaryLevl = this.conManager.GetList("OrdinaryRegLevel");
                    if (alOrdinaryLevl == null || alOrdinaryLevl.Count == 0)
                    {
                        MessageBox.Show("��ȡ��ͨ�����Ӧ�ĹҺż������" + conManager.Err);
                        return;
                    }

                    foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryLevl)
                    {
                        if (constObj.IsValid)
                        {
                            regLevl = constObj.ID.Trim();
                            break;
                        }
                    }
                }
                else
                {
                    //�Ƿ���
                    bool isEmerg = this.regManagement.IsEmergency(this.employee.Dept.ID);

                    string diagItemCode = "";
                    if (isEmerg && !string.IsNullOrEmpty(emergencyLevlCode))
                    {
                        regLevl = this.emergencyLevlCode;
                    }
                    else
                    {
                        //�Ǽ���Һ�ͳһΪ��ͨ�Һż���
                        if (this.regManagement.GetSupplyRegInfo(employee.ID, this.doct.Level.ID.ToString(), employee.Dept.ID, ref regLevl, ref diagItemCode) == -1)
                        {
                            MessageBox.Show(regManagement.Err);
                            return;
                        }

                        //{4DE128D5-7CDD-4c4c-8B7E-3A887FD5E6BA}
                        //����8�㵽����6��ĹҺ�ͳһΪ��ͨ����
                        DateTime now = this.feeManagement.GetDateTimeFromSysDateTime();
                        if (now.Hour > 8 && now.Hour < 18)
                        {
                            regLevl = "1";
                        }
                    }
                }

                FS.HISFC.Models.Registration.RegLevel regLevlObj = this.regLevlHelper.GetObjectFromID(regLevl) as FS.HISFC.Models.Registration.RegLevel;
                if (regLevlObj == null)
                {
                    //{7F00D216-EFE7-48be-A8B3-CAE08FB347E0}
                    regLevlObj = new FS.HISFC.Models.Registration.RegLevel();
                    regLevlObj.ID = "1";
                    //MessageBox.Show("��ѯ�Һż�����󣬱���[" + regLevl + "]������ϵ��Ϣ������ά��!");
                    //return;
                }
               // {78F152F3-0A93-4502-A47D-802314849489}
               // MessageBox.Show(this.employee.Name + this.employee.Level.ID);
                if (this.employee.Level.ID.Trim() == "09") //����ҽʦ
                {
                    cmbRegLevl.Tag = 4;
                }
                else if (this.employee.Level.ID.Trim() == "10") //������ҽʦ
                {
                    cmbRegLevl.Tag = 3;
                }
                else if (this.employee.Level.ID.Trim() == "11") //����ҽʦ
                {
                    cmbRegLevl.Tag = 2;
                }
                else
                    this.cmbRegLevl.Tag = 1;

                //�жϼ������{A40886D6-8636-410c-9718-B879A74B09D0}
                HISFC.Models.Base.Employee oper = FrameWork.Management.Connection.Operator as HISFC.Models.Base.Employee;
                HISFC.Models.Base.Department dept = oper.Dept as HISFC.Models.Base.Department;
                if (dept.Name.Contains("����"))
                {
                    if (MessageBox.Show(this, "��ǰ�����Ǽ�����ң��Ƿ�Ҽ���ţ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        cmbRegLevl.Tag = 5;
                    }
                }
                #endregion

                this.cmbSex.Tag = patientObj.Sex.ID;
                if (patientObj.Birthday > new DateTime(1800, 1, 1))
                {
                    this.dtPickerBirth.Value = patientObj.Birthday;
                }
            }
        }

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int a = 0;
                //����������֣�����Ϊ�ǿ��Ų�ѯ�����������Ʋ�ѯ
                if (int.TryParse(txtCardNo.Text.Trim().Substring(1), out a))// {5D579726-0CDC-4f7d-BF02-EC6673B6BF41}
                {
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                    string cardNO = this.txtCardNo.Text;
                    int flag = feeIntegrate.ValidMarkNO(cardNO, ref accountCard);

                    if (flag > 0)
                    {
                        cardNO = accountCard.Patient.PID.CardNO;
                    }
                    //���ش�����
                    else
                    {
                        MessageBox.Show(feeIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    this.txtCardNo.Text = cardNO;

                    if (!string.IsNullOrEmpty(this.txtCardNo.Text))
                    {
                        this.txtCardNo.Text = this.txtCardNo.Text.PadLeft(10, '0');
                    }

                    if (!string.IsNullOrEmpty(txtCardNo.Text.Trim()))
                    {
                        Clear();

                        this.patientInfo = this.radtMgr.QueryComPatientInfo(this.txtCardNo.Text);
                        if (patientInfo != null && !string.IsNullOrEmpty(patientInfo.PID.CardNO))
                        {
                            if (patientInfo.Memo == "���ϻ���")
                            {
                                MessageBox.Show("�˻����Ѿ����ϣ�");
                                return;
                            }
                            this.SetPatientInfo(this.patientInfo);
                        }
                    }
                }
                else
                {
                    frmQueryPatientByName frmQuery = new frmQueryPatientByName();
                    //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                    frmQuery.IsFliterUnValid = true;
                    frmQuery.QueryByName(txtCardNo.Text.Trim());
                    frmQuery.SelectedPatient += new frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                    frmQuery.ShowDialog(this);
                }

                if (this.patientInfo != null
                    && !string.IsNullOrEmpty(txtName.Text))
                {
                    this.btnOK.Focus();
                }
            }
        }

        void frmQuery_SelectedPatient(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            this.patientInfo = pInfo;
            if (patientInfo != null && !string.IsNullOrEmpty(patientInfo.PID.CardNO))
            {
                this.SetPatientInfo(this.patientInfo);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmQueryPatientByName frmQuery = new frmQueryPatientByName();
                //{51C02BB2-BBC3-45c2-B985-5E14ECEB0943}
                frmQuery.IsFliterUnValid = true;
                frmQuery.QueryByName(txtCardNo.Text.Trim());
                frmQuery.SelectedPatient += new frmQueryPatientByName.GetPatient(frmQuery_SelectedPatient);
                frmQuery.ShowDialog(this);
            }
        }
        /// <summary>
        /// ˢ��// {5D579726-0CDC-4f7d-BF02-EC6673B6BF41}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btShuaKa_Click(object sender, EventArgs e)
        {
            string mCardNo = "";
            string error = "";
            if (Function.OperMCard(ref mCardNo, ref error) == -1)
            {
                MessageBox.Show(error);
                return;
            }
            this.txtCardNo.Text = ";" + mCardNo;
            txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
        }
    }
}


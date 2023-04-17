using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Management;
using FS.HISFC.Models.Registration;
using FarPoint.Win.Spread;
using System.Xml;
namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.IOutpatientInfomation
{
    public partial class ucPatientInfo : UserControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation
    {
        /// <summary>
        /// ucPopSelected<br></br>
        /// [��������: ���ﻼ�߻�����ϢUC]<br></br>
        /// [�� �� ��: ����]<br></br>
        /// [����ʱ��: 2006-2-5]<br></br>
        /// <�޸ļ�¼
        ///		�޸���=''
        ///		�޸�ʱ��='yyyy-mm-dd'
        ///		�޸�Ŀ��=''
        ///		�޸�����=''
        ///  />
        /// </summary>
        public ucPatientInfo()
        {
            InitializeComponent();
        }

        #region ����

        #region ���Ʊ���

        /// <summary>
        /// û�йҺŻ���,���ŵ�һλ��־,Ĭ����9��ͷ
        /// </summary>
        protected string noRegFlagChar = "9";

        /// <summary>
        /// �Ƿ���Ը��Ļ��߻�����Ϣ
        /// </summary>
        protected bool isCanModifyPatientInfo = false;

        /// <summary>
        /// ҽ��,������������Ƿ�Ҫ��ȫƥ��
        /// </summary>
        protected bool isDoctDeptCorrect = false;

        /// <summary>
        /// �Ƿ��շ�ʱ����ԹҺ�ҽ������
        /// </summary>
        protected bool isRegWhenFee = false;

        /// <summary>
        /// �Ƿ�Ĭ�ϵȼ�����
        /// </summary>
        protected bool isClassCodePre = false;

        /// <summary>
        /// �Ƿ���Ը��Ļ�����Ϣ
        /// </summary>
        protected bool isCanModifyChargeInfo = false;

        /// <summary>
        /// ��ͬ�շ����е��շ���Ϣ
        /// </summary>
        ArrayList feeSameDetails = new ArrayList();
        /// <summary>
        /// �ҺŽ���Ĭ�ϵ��������뷨
        /// </summary>
        private InputLanguage CHInput = null;
        
        /// <summary>
        /// �Ƿ����ȫԺ���п���
        /// </summary>
        private bool isLoadAllDept = false;
        /// <summary>
        /// �����շѶദ���Ƿ�Ĭ��ȫѡ��1��0��
        /// </summary>
        private bool isSelectedAllRecipe = false;
        #endregion

        /// <summary>
        /// �Ƿ�ֱ���շѻ���
        /// </summary>
        protected bool isRecordDirectFee = false;

        /// <summary>
        /// �Ƿ����������Ŀ
        /// </summary>
        protected bool isCanAddItem = false;

        /// <summary>
        /// ���ĵ���Ŀ��Ϣ
        /// </summary>
        protected ArrayList modifyFeeDetails = null;

        /// <summary>
        /// ҽ�����ڿ���
        /// </summary>
        protected string doctDeptCode = string.Empty;

        /// <summary>
        /// ���߷�����Ϣ����
        /// </summary>
        private ArrayList feeDetails = null;

        /// <summary>
        /// ��ǰѡ�е��շ������е���Ŀ��Ϣ����
        /// </summary>
        private ArrayList feeDetailsSelected = null;

        /// <summary>
        /// �Һſ��Ҽ���
        /// </summary>
        private ArrayList regDeptList = new ArrayList();

        /// <summary>
        /// ��ǰ�շ�����
        /// </summary>
        private string recipeSequence = string.Empty;

        /// <summary>
        /// ��ͬ��λ���޶���Ϣ����
        /// </summary>
        private ArrayList relations = null;
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContextMenuStrip1 = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ҽ���ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy interfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #region ҵ������
        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration regInterMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        #endregion

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = null;

        /// <summary>
        /// ��һ�����߹ҺŻ�����Ϣ
        /// </summary>
        protected FS.HISFC.Models.Registration.Register prePatientInfo = null;

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �Ű����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Schema schemaMgr = new FS.HISFC.BizLogic.Registration.Schema();
        /// <summary>
        /// �ۼƽ��
        /// </summary>
        private decimal addUpCost = 0m;
        /// <summary>
        /// �Ƿ�ʼ�ۼ�
        /// </summary>
        private bool isBeginAddUpCost = false;
        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        private bool isAddUp = false;

        /// <summary>
        /// �˻�����
        /// </summary>
        protected bool isAccountTerimalFee = false;

        #region �շѴ��ɷ��޸Ŀ���ҽ��,��������{3D863FFD-DAB6-43a7-80C8-DFD35B585BC2}
        ///<summary>
        ///�Ƿ�ֱ���շѻ���
        /// </summary>
        private bool isDirectFeePatient = true ;
        ///<summary>
        ///�ɷ��޸Ŀ���ҽ������������
        /// </summary>
        private bool isCanChangeDoct = true ; 

        #endregion

        /// <summary>
        /// �ÿؼ��Ƿ���Ҫ������ݼ�
        /// </summary>
        private bool isNeedFastShort = false;

        /// <summary>
        /// ��ݼ��ַ���
        /// </summary>
        private string keyString = "";

        /// <summary>
        /// ��һ��ݼ�
        /// </summary>
        private Keys firsKey;

        /// <summary>
        /// �ڶ���ݼ�
        /// </summary>
        private Keys secondKey;

        #endregion


        #region IOutpatientInfomation ��Ա

        /// <summary>
        /// ������л��۱�����Ϣ.
        /// </summary>
        public ArrayList FeeSameDetails
        {
            get
            {
                feeSameDetails = new ArrayList();
                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    //fpRecipeSeq_Sheet1.Rows[i].Tag�±���ͬһ�շ����еķ�����ϸ��Ϣ,����ΪArrayList
                    feeSameDetails.Add(this.fpRecipeSeq_Sheet1.Rows[i].Tag);
                }
                return feeSameDetails;
            }
            set { }
        }

        /// <summary>
        /// ��һ�����߹ҺŻ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PrePatientInfo 
        {
            get 
            {
                return this.prePatientInfo;
            }
            set 
            {
                this.prePatientInfo = value;
            }
        }

        /// <summary>
        /// û�йҺŻ���,���ŵ�һλ��־
        /// </summary>
        public string NoRegFlagChar
        {
            get
            {
                return this.noRegFlagChar;
            }
            set
            {
                this.noRegFlagChar = value;
            }
        }

        /// <summary>
        /// �����л��¼�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething ChangeFocus;        

        /// <summary>
        /// ��ͬ��λ�仯�¼�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething PactChanged;

        /// <summary>
        /// ���߹ҺŻ�����Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            get
            {
                return this.patientInfo;
            }
            set
            {
                this.patientInfo = value;
                if (patientInfo == null)
                {
                    this.tbCardNO.SelectAll();
                    this.tbCardNO.Focus();

                    return;
                }
                if (patientInfo != null)
                {
                    if (patientInfo.ID == "")//���������һ�򿪴�������Ӧcrtl + X
                    {
                        return;
                    }

                    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

                    this.tbCardNO.Text = patientInfo.PID.CardNO;
                    this.tbName.Text = patientInfo.Name;
                    this.cmbSex.Tag = patientInfo.Sex.ID;
                    //this.tbAge.Text = (nowTime.Year - patientInfo.Birthday.Year).ToString();
                    this.tbAge.Text = this.outpatientManager.GetAge(patientInfo.Birthday);


                    //this.cmbDoct.Tag = patientInfo.DoctorInfo.Templet.Doct.ID;
                    this.cmbDoct.Tag = patientInfo.SeeDoct.ID;

                    if (!string.IsNullOrEmpty(patientInfo.SeeDoct.Dept.ID))
                    {
                        this.cmbRegDept.Tag = patientInfo.SeeDoct.Dept.ID;
                    }
                    else
                    {
                        this.cmbRegDept.Tag = patientInfo.DoctorInfo.Templet.Dept.ID;
                    }

                    #region ��ͬ��λ�ж�
                    if (patientInfo.Pact.PayKind.ID != "02")
                    {
                        this.cmbPact.Tag = patientInfo.Pact.ID;
                    }
                    else
                    {
                        this.cmbPact.SelectedIndexChanged -= new EventHandler(cmbPact_SelectedIndexChanged);
                        this.cmbPact.Tag = this.patientInfo.Pact.ID;
                        this.cmbPact.SelectedIndexChanged += new EventHandler(cmbPact_SelectedIndexChanged);
                    }

                    if (!string.IsNullOrEmpty(this.cmbPact.Tag.ToString()))
                    {
                        this.patientInfo.Pact = this.GetPactInfoByPactCode(this.cmbPact.Tag.ToString());
                    }

                    if (this.patientInfo.Pact.PayKind.ID == "02")
                    {
                        this.patientInfo.Pact.ID = this.cmbPact.Tag.ToString();
                        this.patientInfo.Pact.Name = this.cmbPact.Text;
                        this.SetRelations();
                        this.PactChanged();
                        this.PriceRuleChanaged();
                    }
                    #endregion
                    this.tbMCardNO.Text = patientInfo.SSN;
                    if (!string.IsNullOrEmpty(patientInfo.LSH))
                    {
                        this.tbJZDNO.Text = patientInfo.LSH;
                    }
                    if (this.patientInfo.Pact.IsNeedMCard)
                    {
                        if (this.tbMCardNO.Text == string.Empty)
                        {
                            MessageBox.Show(Language.Msg("������ҽ��֤��"));
                            this.tbMCardNO.Focus();
                            return;
                        }
                    }
                    //if(this.patientInfo.Pact.PayKind.ID == "03")
                    //{
                    //    if (this.tbJZDNO.Text.Trim() == string.Empty)
                    //    {
                    //        MessageBox.Show(Language.Msg("��������˵���"));
                    //        this.tbJZDNO.Focus();
                    //        return;
                    //    }
                    //}
                    if (this.patientInfo.Pact.PayKind.ID == "01")//�Է�
                    {
                        this.cmbClass.Enabled = false;
                        this.tbMCardNO.Enabled = false;
                        this.cmbRebate.Enabled = false;
                        this.tbJZDNO.Enabled = false;
                    }
                    else if (this.patientInfo.Pact.PayKind.ID == "02")//ҽ��
                    {
                        this.cmbClass.Enabled = false;
                        this.tbMCardNO.Enabled = true;
                        this.cmbRebate.Enabled = false;
                        this.tbJZDNO.Enabled = false;
                    }
                    else//����
                    {
                        this.cmbClass.Enabled = true;
                        this.cmbRebate.Enabled = false;
                        this.tbMCardNO.Enabled = true;
                        this.tbJZDNO.Enabled = true;
                    }

                    if (!this.IsCanModifyChargeInfo)//�����Ը��ĹҺ���Ϣ!.
                    {
                        foreach (Control c in this.Controls)
                        {
                            //�����
                            if (c.GetType().BaseType == typeof(TextBox))
                            {
                                if (c.Text != "" && !c.Equals(this.tbCardNO))
                                {
                                    c.Enabled = false;//�����޸�;
                                }
                            }
                            //������
                            if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                            {
                                if (c.Text != "")
                                {
                                    c.Enabled = false;//�����޸�;
                                }
                            }
                        }
                    }

                    if (this.patientInfo.Name == "")
                    {
                        this.tbName.Focus();
                    }
                    else
                    {
                        //ֱ���շ�
                        if (!this.isRecordDirectFee)
                        {
                            //������ÿ�ݼ�ѡ����һ���շѻ�����Ϣ����������ѡ�����λ��
                            //��������ѡ��ҽ��λ��
                            //if (isPrRInfoSelected)
                            //{
                            //    this.cmbRegDept.Focus();
                            //    isPrRInfoSelected = false;
                            //}
                            //else
                            //{
                                this.cmbDoct.Focus();
                            //}
                        }
                        else
                        {

                        }
                    }
                }
            }
        }

        /// <summary>
        /// ��շ���
        /// </summary>
        public void Clear()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
            this.tbCardNO.Text = string.Empty;
            this.tbName.Text = string.Empty;
            this.cmbSex.Tag = string.Empty;
            this.cmbRegDept.Tag = string.Empty;
            this.cmbPact.SelectedIndexChanged -= new EventHandler(cmbPact_SelectedIndexChanged);
            this.cmbPact.Tag = string.Empty;
            this.cmbPact.SelectedIndexChanged += new EventHandler(cmbPact_SelectedIndexChanged);
            this.patientInfo = null;
            this.cmbDoct.Tag = string.Empty;
            this.tbAge.Text = string.Empty;
            this.tbMCardNO.Text = string.Empty;
            this.tbJZDNO.Text = string.Empty;
            this.cmbClass.SelectedIndexChanged -= new EventHandler(cmbClass_SelectedIndexChanged);
            this.cmbClass.Tag = string.Empty;
            this.cmbClass.SelectedIndexChanged += new EventHandler(cmbClass_SelectedIndexChanged);
            this.cmbRebate.Tag = string.Empty;
            this.fpRecipeSeq_Sheet1.RowCount = 0;
            this.tbCardNO.Focus();
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitControlParams() 
        {
            //��ÿ���ǰ��λ����
            this.noRegFlagChar = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES, false, "9");
            
            //�Ƿ���Ը��Ļ��߻�����Ϣ
            this.isCanModifyPatientInfo = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_REG_INFO, false, true);

            //ҽ��,������������Ƿ�Ҫ��ȫƥ��
            this.isDoctDeptCorrect = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOCT_DEPT_INPUT_CORRECT, false, false);

            //�Ƿ��շ�ʱ����ԹҺ�ҽ������
            this.isRegWhenFee = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.REG_WHEN_FEE, false, false);

            //�Ƿ�Ĭ�ϵȼ�����
            this.isClassCodePre = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CLASS_CODE_PRE, false, false);

            //�Ƿ���Ը��Ļ�����Ϣ
            this.isCanModifyChargeInfo = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.MODIFY_CHARGE_INFO, false, true);

            //�Ƿ����ȫԺ���п���
            this.isLoadAllDept = this.controlParamIntegrate.GetControlParam<bool>("MZ9928", false, false);
            // �����շѶദ���Ƿ�Ĭ��ȫѡ��1��0��
            this.isSelectedAllRecipe = this.controlParamIntegrate.GetControlParam<bool>("MZ0204", false, false);

            //�����շ��Ƿ����ÿ�ݼ�
            this.isNeedFastShort = this.controlParamIntegrate.GetControlParam<bool>("MZFAST", false, false);

            if (this.isNeedFastShort)
            {
                //��ݼ��ַ�����ȫѡ����
                this.keyString = this.controlParamIntegrate.GetControlParam<string>("MZALLR", false, "");
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Init()
        {
			try
			{
                if (this.InitControlParams() == -1) 
                {
                    MessageBox.Show("��ʼ������ʧ��!");

                    return -1;
                }

                this.cmbDoct.IsListOnly = true;
                this.cmbRegDept.IsListOnly = true;
                this.cmbSex.IsListOnly = true;
                this.cmbClass.IsListOnly = true;
                this.cmbPact.IsListOnly = true;
                this.cmbSex.IsListOnly = true;

                //�Ƿ����ȫԺ���п���
                if (!this.isLoadAllDept)
                {
                    //��ʼ�� �Һſ���
                    this.regDeptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
                    if (this.regDeptList == null)
                    {
                        MessageBox.Show("��ʼ���Һſ��ҳ���!" + this.managerIntegrate.Err);

                        return -1;
                    }

                    //this.cmbRegDept.AddItems(this.regDeptList);
                    //�������סԺ����  xingz ������
                    ArrayList alDeptInPatient = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
                    this.regDeptList.AddRange(alDeptInPatient);
                }
                else
                {
                    this.regDeptList = this.managerIntegrate.GetDeptmentAllValid();
                    if (this.regDeptList == null)
                    {
                        MessageBox.Show("��ʼ���Һſ��ҳ���!" + this.managerIntegrate.Err);

                        return -1;
                    }
                }
                this.cmbRegDept.AddItems(this.regDeptList);
                deptHelper.ArrayObject = this.regDeptList;

                //{D7A8536F-63EB-4378-9EE1-149BB9C872F1}��������xml�ļ�,��ʼ����ѡ��ͬ��λ
                InitPact();
                //{D7A8536F-63EB-4378-9EE1-149BB9C872F1}��������xml�ļ�,��ʼ����ѡ��ͬ��λ    ����

                //��ʼ���Ż���Ϣ
                FS.HISFC.Models.Base.Const tempConst = new FS.HISFC.Models.Base.Const();
				tempConst.ID = "NO";
				tempConst.Name = "���Żݱ���";
                ArrayList ecoList = new ArrayList();
                ecoList.Add(tempConst);
                this.cmbRebate.AddItems(ecoList);

				//��ʼ���Ա�
                ArrayList sexListTemp = new ArrayList();
                ArrayList sexList = new ArrayList();
                sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
                FS.HISFC.Models.Base.Spell spell = null;
                foreach (FS.FrameWork.Models.NeuObject neuObj in sexListTemp)
                {
                    spell = new FS.HISFC.Models.Base.Spell();
                    if (neuObj.ID == "M")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "1";
                    }
                    else if (neuObj.ID == "F")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "2";
                    }
                    else if (neuObj.ID == "O")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "3";
                    }
                    else
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                    }
                    sexList.Add(spell);
                }
				this.cmbSex.AddItems(sexList);

				//��ʼ��ҽ���б�����һ���޹���ҽ�������999
				ArrayList doctList = new ArrayList();
				doctList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
                if (doctList == null) 
                {
                    MessageBox.Show("��ʼ��ҽ���б����!" + this.managerIntegrate.Err);

                    return -1;
                }
                FS.HISFC.Models.Base.Employee pNone = new FS.HISFC.Models.Base.Employee();
				pNone.ID = "999";
				pNone.Name = "�޹���";
				pNone.SpellCode = "WGS";
				pNone.UserCode = "999";
				doctList.Add(pNone);
                this.cmbDoct.AddItems(doctList);
				
				this.cmbDoct.IsLike = !isDoctDeptCorrect;
				this.cmbRegDept.IsLike = !isDoctDeptCorrect;

                //��ʼ��FP
                InputMap im;
                im = fpRecipeSeq.GetInputMap(InputMapMode.WhenAncestorOfFocused);
                im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
			}					 
			catch
            {
                return -1;
            }

            return 1;
        }

        //{D7A8536F-63EB-4378-9EE1-149BB9C872F1}��������xml�ļ�,��ʼ����ѡ��ͬ��λ

        /// <summary>
        /// ��ʼ����ͬ��λ xml�ļ����ò��� 1 ֻ��ѡ��xml��ά���ĺ�ͬ��λ  2 �ų�xmlά���ĺ�ͬ��λ  . ����ֵ���к�ͬ��λ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitPact()
        {
            //��ʼ����ͬ��λ
            ArrayList pactList = this.pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("��ʼ����ͬ��λ����!" + this.pactManager.Err);

                return -1;
            }

            ArrayList pactListFinal = new ArrayList();

            string fileName = Application.StartupPath + "\\Setting\\Profiles\\FeePactSetting.xml";
            
            XmlDocument xd = new XmlDocument();
            try
            {
                xd.Load(fileName);
            }
            catch (Exception ex)
            {

                xd = null;
            }
            
            if (xd != null)
            {
                XmlNode xnPactList = xd.SelectSingleNode("/setting/pactlist");
                if (xnPactList != null)
                {
                    string flag = xnPactList.Attributes["flag"].InnerText;
                    if (flag == "1")//ֻ�����º�ͬ��λ
                    {
                        ArrayList alPact = new ArrayList();

                        foreach (XmlNode xn in xnPactList.ChildNodes)
                        {
                            alPact.Add(xn.InnerText);
                        }
                        if (alPact.Count > 0)
                        {
                            foreach (string s in alPact)
                            {
                                foreach (FS.HISFC.Models.Base.PactInfo p in pactList)
                                {
                                    if (s == p.ID)
                                    {
                                        pactListFinal.Add(p);
                                    }
                                }
                            }
                        }
                        else
                        {
                            pactListFinal = pactList;
                        }
                    }
                    else if (flag == "2") //�ų����º�ͬ��λ
                    {
                        ArrayList alPact = new ArrayList();

                        foreach (XmlNode xn in xnPactList.ChildNodes)
                        {
                            alPact.Add(xn.InnerText);
                        }

                        ArrayList tempPactList = new ArrayList();

                        if (alPact.Count > 0)
                        {
                            foreach (string s in alPact)
                            {
                                foreach (FS.HISFC.Models.Base.PactInfo p in pactList)
                                {
                                    if (s == p.ID)
                                    {
                                        tempPactList.Add(p);
                                    }
                                }
                            }
                            foreach (FS.HISFC.Models.Base.PactInfo p in tempPactList)
                            {
                                pactList.Remove(p);
                            }

                            pactListFinal = pactList;
                        }
                        else
                        {
                            pactListFinal = pactList;
                        }
                    }
                    else //���к�ͬ��λ
                    {
                        pactListFinal = pactList;
                    }
                }
            }
            else //{A84AB263-19B8-465c-BA62-3052AFC04A23}
            {
                pactListFinal = pactList;
            }

            this.cmbPact.AddItems(pactListFinal);

            return 1;
        }

        //{D7A8536F-63EB-4378-9EE1-149BB9C872F1}��������xml�ļ�,��ʼ����ѡ��ͬ��λ, �޸Ľ���

        /// <summary>
        /// ����޸���Ϣ
        /// </summary>
        public void DealModifyDetails()
        {
            if (this.modifyFeeDetails == null)
            {
                return;
            }
            ArrayList recipeSeqs = new ArrayList();
            FS.FrameWork.Models.NeuObject obj = null;
            int currRow = this.fpRecipeSeq_Sheet1.ActiveRowIndex;
            for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            {
                if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text == "True")
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = i.ToString();
                    obj.Memo = this.fpRecipeSeq_Sheet1.Cells[i, 3].Tag.ToString();
                    recipeSeqs.Add(obj);
                }
            }
            ArrayList sortList = new ArrayList();
            while (modifyFeeDetails.Count > 0)
            {
                ArrayList sameNotes = new ArrayList();
                FeeItemList compareItem = modifyFeeDetails[0] as FeeItemList;

                foreach (FeeItemList f in modifyFeeDetails)
                {
                    if (f.RecipeSequence == compareItem.RecipeSequence)
                    {
                        sameNotes.Add(f.Clone());
                    }
                }
                sortList.Add(sameNotes);
                foreach (FeeItemList f in sameNotes)
                {
                    for (int i = modifyFeeDetails.Count - 1; i >= 0; i--)
                    {
                        FeeItemList b = this.modifyFeeDetails[i] as FeeItemList;
                        if (f.RecipeSequence == b.RecipeSequence)
                        {
                            this.modifyFeeDetails.Remove(b);
                        }
                    }
                }

            }
            foreach (ArrayList temp in sortList)
            {
                FeeItemList fTemp = ((FeeItemList)temp[0]);
                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[i, 3].Tag.ToString() == fTemp.RecipeSequence)
                    {
                        this.fpRecipeSeq_Sheet1.Rows[i].Tag = temp;
                        decimal cost = 0;
                        foreach (FeeItemList f in temp)
                        {
                            cost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                        }
                        
                        this.fpRecipeSeq_Sheet1.Cells[i, 3].Text = cost.ToString();
                        this.fpRecipeSeq_Sheet1.Rows[i].Tag = temp;

                        break;
                    }
                }
            }

            foreach (FS.FrameWork.Models.NeuObject objSeq in recipeSeqs)
            {
                bool find = false;
                foreach (ArrayList temp in sortList)
                {
                    FeeItemList fTemp = ((FeeItemList)temp[0]);
                    if (fTemp.RecipeSequence == objSeq.Memo)
                    {
                        find = true;
                    }
                }
                if (!find)
                {
                    this.fpRecipeSeq_Sheet1.Rows[Convert.ToInt32(objSeq.ID)].Tag = new ArrayList();
                    this.fpRecipeSeq_Sheet1.Cells[Convert.ToInt32(objSeq.ID), 3].Text = "0.00";
                }
            }
        }

        /// <summary>
        /// �Ƿ�������Ӵ���
        /// </summary>
        public void IFCanAddItem()
        {
            int currRow = this.fpRecipeSeq_Sheet1.ActiveRowIndex;
            int selectRows = 0;
            int selectRow = 0;
            for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            {
                if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text == "True")
                {
                    selectRows++;
                }
            }
            if (selectRows > 1)
            {
                this.isCanAddItem = false;

                return;
            }
            if (selectRows == 0)
            {
                this.isCanAddItem = true;

                return;
            }
            if (selectRows == 1)
            {
                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text == "True")
                    {
                        selectRow = i;
                    }
                }
            }

            if (selectRow != currRow)
            {
                this.isCanAddItem = false;
                return;
            }

            this.isCanAddItem = true;
        }

        /// <summary>
        /// �����µ��շ�������Ϣ
        /// </summary>
        public void SetNewRecipeInfo()
        {
            int row = this.fpRecipeSeq_Sheet1.ActiveRowIndex;
            string deptName = this.cmbRegDept.Text;
            string deptCode = this.cmbRegDept.Tag.ToString();
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Text = deptName;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Tag = deptCode;
            try
            {
                foreach (FeeItemList f in (ArrayList)fpRecipeSeq_Sheet1.Rows[row].Tag)
                {
                    ((Register)f.Patient).DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
                    ((Register)f.Patient).DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();
                    f.RecipeOper.Dept.ID = this.patientInfo.DoctorInfo.Templet.Doct.User01;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        /// <summary>
        /// ���ÿ��Ը��ĵĹҺ���Ϣ
        /// </summary>
        /// <param name="feeItemList">������ϸ</param>
        /// <param name="isCanModify">trueĳЩ�ֶο����޸ã�false ĳЩ�ֶβ������޸�</param>
        public void SetRegInfoCanModify(FeeItemList feeItemList, bool isCanModify)
        {
            if (isCanModify)
            {
                //modify {3D863FFD-DAB6-43a7-80C8-DFD35B585BC2}
                if (this.isDirectFeePatient == false && this.isCanChangeDoct == false)
                {
                    this.cmbRegDept.Enabled = false;
                    this.cmbDoct.Enabled = false;
                }
                else
                {
                    this.cmbRegDept.Enabled = true;
                    this.cmbDoct.Enabled = true;
                }
                //end modify
                if (feeItemList != null)
                {
                    if (!string.IsNullOrEmpty(feeItemList.RecipeOper.Dept.ID))
                    {
                        this.cmbRegDept.Tag = feeItemList.RecipeOper.Dept.ID;
                    }
                    else
                    {
                        this.cmbRegDept.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;
                    }
                    this.cmbDoct.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID;
                }
            }
            else
            {
                this.cmbRegDept.Enabled = false;
                this.cmbDoct.Enabled = false;

                if (!string.IsNullOrEmpty(((Register)feeItemList.Patient).SeeDoct.Dept.ID))
                {
                    this.cmbRegDept.Tag = ((Register)feeItemList.Patient).SeeDoct.Dept.ID;
                }
                else
                {
                    this.cmbRegDept.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;
                }
                this.cmbDoct.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID;
            }

            if (!string.IsNullOrEmpty(feeItemList.Order.Patient.Pact.ID))
            {
                if (this.cmbPact.Tag == null || !feeItemList.Order.Patient.Pact.ID.Equals(this.cmbPact.Tag.ToString()))
                {
                    this.cmbPact.Tag = feeItemList.Order.Patient.Pact.ID;
                    this.cmbPact_SelectedIndexChanged(null, null);
                }
            }
        }

        /// <summary>
        /// �����´���
        /// </summary>
        public void AddNewRecipe()
        {
            if (this.patientInfo == null)
            {
                return;
            }
            //��������
            this.fpRecipeSeq_Sheet1.Rows.Add(this.fpRecipeSeq_Sheet1.RowCount, 1);

            //�õ����һ�е�����
            int row = this.fpRecipeSeq_Sheet1.RowCount - 1;

            //�������һ��Ϊ���
            this.fpRecipeSeq_Sheet1.ActiveRowIndex = row;
            //�������һ�е�TagΪԤ���ķ�����ϸ������
            this.fpRecipeSeq_Sheet1.Rows[row].Tag = new ArrayList();

            for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            {
                this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = false;
                this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
            }
            this.fpRecipeSeq_Sheet1.Cells[row, 0].Value = true;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
            this.fpRecipeSeq_Sheet1.Cells[row, 1].ForeColor = Color.Blue;
            this.fpRecipeSeq_Sheet1.Cells[row, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
            this.fpRecipeSeq_Sheet1.Cells[row, 2].ForeColor = Color.Blue;
            this.fpRecipeSeq_Sheet1.Cells[row, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
            this.fpRecipeSeq_Sheet1.Cells[row, 3].ForeColor = Color.Blue;
            this.fpRecipeSeq_Sheet1.Cells[row, 0].Value = true;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Text = this.cmbRegDept.Text;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Tag = this.cmbRegDept.Tag.ToString();

            //if (string.IsNullOrEmpty(this.patientInfo.Pact.ID))
            //{
                this.fpRecipeSeq_Sheet1.Cells[row, 2].Text = "�¿�";
                this.fpRecipeSeq_Sheet1.Cells[row, 2].Tag = "";
            //}
            //else
            //{
            //    this.fpRecipeSeq_Sheet1.Cells[row, 2].Text = "�¿�" + "[" + this.patientInfo.Pact.Name + "]";
            //    this.fpRecipeSeq_Sheet1.Cells[row, 2].Tag = this.patientInfo.Pact.ID;
            //}


            this.fpRecipeSeq_Sheet1.Cells[row, 3].Text = "0.00";

            //�����ʱ�շ����к�
            string recipeSeqTemp = this.outpatientManager.GetRecipeSequence();

            if (recipeSeqTemp == "-1" || recipeSeqTemp == null || recipeSeqTemp == string.Empty)
            {
                MessageBox.Show("����շ���ų���!" + this.outpatientManager.Err);
                this.fpRecipeSeq_Sheet1.Rows.Remove(row, 1);

                return;
            }

            this.fpRecipeSeq_Sheet1.Cells[row, 3].Tag = recipeSeqTemp;
            this.recipeSequence = recipeSeqTemp;

            //�ж��Ƿ�������Ӵ���
            this.IFCanAddItem();

            feeDetailsSelected = new ArrayList();

            //�����շ����б���¼�
            RecipeSeqChanged();

            if (this.patientInfo.Name == null || this.patientInfo.Name == string.Empty)
            {
                this.tbName.Focus();
            }
            else
            {
                if (this.isRecordDirectFee)
                {
                    this.cmbSex.Focus();
                }
                else
                {
                    this.cmbRegDept.Focus();
                }
            }
        }

        /// <summary>
        /// ����Copy�Ĵ���
        /// </summary>
        public void AddCopyRecipe()
        {
            if (this.patientInfo == null)
            {
                return;
            }

            if (feeDetailsSelected == null || feeDetailsSelected.Count == 0)
            {
                return;
            }

            //�����ʱ�շ����к�
            string recipeSeqTemp = this.outpatientManager.GetRecipeSequence();

            if (recipeSeqTemp == "-1" || recipeSeqTemp == null || recipeSeqTemp == string.Empty)
            {
                MessageBox.Show("����շ���ų���!" + this.outpatientManager.Err);

                return;
            }
            //�ж��Ƿ�������Ӵ���
            ArrayList alNewFeeInfo = feeDetailsSelected.Clone() as ArrayList;
            decimal sumFeeCost = 0.00M;
            this.recipeSequence = recipeSeqTemp;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in alNewFeeInfo)
            {
                feeItemList.RecipeNO = string.Empty;
                feeItemList.RecipeSequence = this.recipeSequence;
                feeItemList.SequenceNO = -1;
                feeItemList.FTSource = "0";//���ƵĴ���������Դ������0
                sumFeeCost += feeItemList.FT.OwnCost + feeItemList.FT.PayCost + feeItemList.FT.PubCost;
            }

            //��������
            this.fpRecipeSeq_Sheet1.Rows.Add(this.fpRecipeSeq_Sheet1.RowCount, 1);

            //�õ����һ�е�����
            int row = this.fpRecipeSeq_Sheet1.RowCount - 1;

            //�������һ��Ϊ���
            this.fpRecipeSeq_Sheet1.ActiveRowIndex = row;
            //�������һ�е�TagΪԤ���ķ�����ϸ������
            this.fpRecipeSeq_Sheet1.Rows[row].Tag = new ArrayList();

            for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            {
                this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = false;
                this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
            }
            this.fpRecipeSeq_Sheet1.Cells[row, 0].Value = true;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
            this.fpRecipeSeq_Sheet1.Cells[row, 1].ForeColor = Color.Blue;
            this.fpRecipeSeq_Sheet1.Cells[row, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
            this.fpRecipeSeq_Sheet1.Cells[row, 2].ForeColor = Color.Blue;
            this.fpRecipeSeq_Sheet1.Cells[row, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
            this.fpRecipeSeq_Sheet1.Cells[row, 3].ForeColor = Color.Blue;
            this.fpRecipeSeq_Sheet1.Cells[row, 0].Value = true;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Text = this.cmbRegDept.Text;
            this.fpRecipeSeq_Sheet1.Cells[row, 1].Tag = this.cmbRegDept.Tag.ToString();

            //if (string.IsNullOrEmpty(this.patientInfo.Pact.ID))
            //{
                this.fpRecipeSeq_Sheet1.Cells[row, 2].Text = "�¿�";
                this.fpRecipeSeq_Sheet1.Cells[row, 2].Tag = "";
            //}
            //else
            //{
            //    this.fpRecipeSeq_Sheet1.Cells[row, 2].Text = "�¿�" + "[" + this.patientInfo.Pact.Name + "]";
            //    this.fpRecipeSeq_Sheet1.Cells[row, 2].Tag = this.patientInfo.Pact.ID;
            //}

            this.fpRecipeSeq_Sheet1.Cells[row, 3].Text = sumFeeCost.ToString("F2");

            this.fpRecipeSeq_Sheet1.Cells[row, 3].Tag = recipeSeqTemp;
            this.fpRecipeSeq_Sheet1.Rows[row].Tag = new ArrayList(alNewFeeInfo);

            IFCanAddItem();
            feeDetailsSelected = alNewFeeInfo;

            //�����շ����б���¼�
            RecipeSeqChanged();


            if (this.patientInfo.Name == null || this.patientInfo.Name == string.Empty)
            {
                this.tbName.Focus();
            }
            else
            {
                if (this.isRecordDirectFee)
                {
                    this.cmbSex.Focus();
                }
                else
                {
                    this.cmbRegDept.Focus();
                }
            }
        }

        public void AddCopyRecipe(int num)
        {
            for (int i = 0; i < num; i++)
            {
                this.AddCopyRecipe();
            }
        }

        /// <summary>
        /// ���»�ùҺ���Ϣ
        /// </summary>
        public void GetRegInfo()
        {
            if (this.patientInfo == null)
            {
                return;
            }
            this.patientInfo.DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
            this.patientInfo.DoctorInfo.Templet.Dept.Name = this.cmbRegDept.Text;
            this.patientInfo.DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();
            this.patientInfo.DoctorInfo.Templet.Doct.Name = this.cmbDoct.Text;
            this.patientInfo.Pact.ID = this.cmbPact.Tag.ToString();
            this.patientInfo.Pact.Name = this.cmbPact.Text;
            this.patientInfo.SSN = this.tbMCardNO.Text;

            if (this.cmbSex.Tag != null)
            {
                this.patientInfo.Sex.ID = this.cmbSex.Tag.ToString();
            }
            this.patientInfo.Name = this.tbName.Text;

            this.patientInfo.Age = this.tbAge.Text;

            //���˵���
            this.PatientInfo.LSH = this.tbJZDNO.Text;
            //�ȼ�
            if (this.cmbClass.Tag != null)
            {
                this.patientInfo.User03 = this.cmbClass.Tag.ToString();
            }
        }

        /// <summary>
        /// ���ùҺ���Ϣ
        /// </summary>
        public void SetRegInfo()
        {
            DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
            if (this.patientInfo == null)
            {
                return;
            }
            this.tbMCardNO.Text = this.patientInfo.SSN;
            this.tbName.Text = this.patientInfo.Name;
            //this.tbAge.Text = (nowTime.Year - this.patientInfo.Birthday.Year).ToString(); 
            this.tbAge.Text = this.outpatientManager.GetAge(patientInfo.Birthday);

            this.cmbSex.Tag = this.patientInfo.Sex.ID.ToString();
            this.patientInfo.DoctorInfo.Templet.Doct.User01 = this.doctDeptCode;

            this.cmbPact.SelectedIndexChanged -= new EventHandler(cmbPact_SelectedIndexChanged);
            this.cmbPact.Tag = this.patientInfo.Pact.ID;
            this.cmbPact.SelectedIndexChanged += new EventHandler(cmbPact_SelectedIndexChanged);
        }

        /// <summary>
        /// ��֤�Һ���Ϣ�Ƿ�Ϸ�
        /// </summary>
        /// <returns>true�Ϸ� false���Ϸ�</returns>
        public bool IsPatientInfoValid()
        {
            if (this.cmbSex.Tag == null || this.cmbSex.Tag.ToString() == string.Empty) 
            {
                MessageBox.Show(Language.Msg("�������Ա�!"));
                this.cmbSex.Focus();

                return false;
            }
            
            if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("������ҽ��!"));
                this.cmbDoct.Focus();
                return false;
            }
            if (this.tbName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뻼������!"));
                this.tbName.Focus();

                return false;
            }

            string[] spChar = new string[] { "@", "#", "$", "%", "^", "&", "[", "]", "|", "'" };
            if (FS.FrameWork.Public.String.TakeOffSpecialChar(this.tbName.Text, spChar).Trim() == string.Empty) 
            {
                MessageBox.Show(Language.Msg("���������Ĵ��������ַ�,����ȥ���ַ���,����Ч����,����������!"));
                this.tbName.Focus();

                return false;
            }

            if (this.cmbRegDept.Tag == null || this.cmbRegDept.Tag.ToString() == string.Empty)
            {
                MessageBox.Show(Language.Msg("�����뿪������"));
                this.cmbRegDept.Focus();

                return false;
            }
            if (this.cmbClass.alItems.Count > 0)
            {
                if (this.cmbClass.Tag == null || this.cmbClass.Tag.ToString() == string.Empty)
                {
                    MessageBox.Show(Language.Msg("������ȼ�����!"));
                    this.cmbClass.Focus();

                    return false;
                }
            }
            if (this.cmbPact.Text.Trim().Length <= 0 || this.cmbPact.Tag == null)
            {
                MessageBox.Show(Language.Msg("��ѡ���ͬ��λ!"));
                this.cmbPact.Focus();

                return false;
            }
            if (this.patientInfo.Pact.IsNeedMCard && this.tbMCardNO.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("������ҽ��֤��!"));
                this.tbMCardNO.Text = string.Empty;
                this.tbMCardNO.Focus();

                return false;
            }
            if (this.patientInfo.Pact.PayKind.ID == "03" && this.tbJZDNO.Text.Trim() == string.Empty 
                && ((FS.HISFC.Models.Base.Employee)this.outpatientManager.Operator).EmployeeType.ID.ToString() == FS.HISFC.Models.Base.EnumEmployeeType.F.ToString())
            {
                MessageBox.Show(Language.Msg("��������˵���!"));
                this.tbJZDNO.Text = string.Empty;
                this.tbJZDNO.Focus();

                return false;
            }
            if (this.tbAge.Text == "")
            {
                MessageBox.Show(Language.Msg("����������!"));
                this.tbAge.Focus();
                return false;
            }

            //Ԥ�����س�
            //this.tbName_Leave(null, new KeyEventArgs(Keys.Enter));
            ////this.tbAge_KeyDown(null, new KeyEventArgs(Keys.Enter));
            //this.cmbSex_KeyDown(null, new KeyEventArgs(Keys.Enter));

            #region ��������ʱ����

            if (this.patientInfo.Pact.PayKind.ID == "03")
            {
                interfaceProxy.SetPactCode(this.patientInfo.Pact.ID);
                bool isBlack = interfaceProxy.IsInBlackList(this.patientInfo);
                if (isBlack)
                {
                    MessageBox.Show(this.interfaceProxy.ErrMsg);
                    return false;
                }
            }

            #endregion

            this.tbAge_KeyDown(this.tbAge, new KeyEventArgs(Keys.Enter));

            return true;
        }

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        public ArrayList FeeDetails
        {
            get
            {
                return this.feeDetails;
            }
            set
            {
                this.feeDetails = value;

                //����û��߻��۱������Ϣ��,�����շ����з���,Ĭ����ʾ��һ���շ������µ����з�����Ϣ.
                this.SetChargeInfo();
            }
        }

        /// <summary>
        /// ѡ���Ҫ�շ���Ϣ
        /// </summary>
        public ArrayList FeeDetailsSelected
        {
            get
            {
                return this.feeDetailsSelected;
            }
            set
            {
                this.feeDetailsSelected = value;
            }
        }

        /// <summary>
        /// �Ƿ����������Ŀ
        /// </summary>
        public bool IsCanAddItem
        {
            get
            {
                this.IFCanAddItem();
                
                return this.isCanAddItem;
            }
            set
            {
                this.isCanAddItem = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ը��Ļ�����Ϣ
        /// </summary>
        public bool IsCanModifyChargeInfo
        {
            get
            {
                return this.isCanModifyChargeInfo;
            }
            set
            {
                this.isCanModifyChargeInfo = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ը��Ļ��߻�����Ϣ
        /// </summary>
        public bool IsCanModifyPatientInfo
        {
            get
            {
                return this.isCanModifyPatientInfo;
            }
            set
            {
                this.isCanModifyPatientInfo = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ĭ�ϵȼ�����
        /// </summary>
        public bool IsClassCodePre
        {
            get
            {
                return this.isClassCodePre;
            }
            set
            {
                this.isClassCodePre = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ҫ��ҽ��,����ȫƥ��
        /// </summary>
        public bool IsDoctDeptCorrect
        {
            get
            {
                return this.isDoctDeptCorrect;
            }
            set
            {
                this.isDoctDeptCorrect = value;
            }
        }

        /// <summary>
        /// �Ƿ�ֱ���շѻ���
        /// </summary>
        public bool IsRecordDirectFee
        {
            get
            {
                return this.isRecordDirectFee;
            }
            set
            {
                this.isRecordDirectFee = value;
            }
        }

        /// <summary>
        /// �Ƿ�ҽ�������շ�ʱ��ͬʱ�Һ�
        /// </summary>
        public bool IsRegWhenFee
        {
            get
            {
                return this.isRegWhenFee;
            }
            set
            {
                this.isRegWhenFee = value;
            }
        }

        /// <summary>
        /// ���ĵķ�����Ϣ
        /// </summary>
        public ArrayList ModifyFeeDetails
        {
            get
            {
                return this.modifyFeeDetails;
            }
            set
            {
                this.modifyFeeDetails = value;
            }
        }

        /// <summary>
        /// ��������,�Żݵ�,�۸����仯�󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething PriceRuleChanaged;

        /// <summary>
        /// �շ����б仯�󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething RecipeSeqChanged;

        /// <summary>
        /// ɾ���շ����к󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRecipeDeleted RecipeSeqDeleted;

        /// <summary>
        /// ��ǰ�շ�����
        /// </summary>
        public string RecipeSequence
        {
            get
            {
                return this.recipeSequence;   
            }
            set
            {
                this.recipeSequence = value;
            }
        }

        /// <summary>
        /// ������ұ仯�󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeDoctAndDept SeeDeptChanaged;

        /// <summary>
        /// ����ҽ�������仯�󴥷�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeDoctAndDept SeeDoctChanged;
        
        /// <summary>
        /// �����뿨����Ч�󴥷�,��ҪΪ�˿�����ʾ�Һ���Ϣ�ؼ���λ�á�
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateEnter InputedCardAndEnter;

        /// <summary>
        /// ���ŷ�Ʊ�ۼƽ��
        /// </summary>
        public decimal AddUpCost
        {
            set
            {
                addUpCost = value;
                //this.lblAddUpCost.Text = addUpCost.ToString();
            }
            get
            {
                return addUpCost;
            }
        }
        /// <summary>
        /// �Ƿ�ʼ�ۼ�
        /// </summary>
        public bool IsBeginAddUpCost
        {
            get
            {
                return isBeginAddUpCost;
            }
            set
            {
                isBeginAddUpCost = value;
                if (!value)
                {
                    this.AddUpCost = 0;
                }
            }
        }
        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        public bool IsAddUp
        {
            set
            {
                isAddUp = value;
                if (!value)
                {
                    //this.plAddUp.Visible = false;
                    this.IsBeginAddUpCost = false;
                }
            }
            get
            {
                return isAddUp ;
            }
        }
        #endregion

        #region ����

        #region ˽�з���

        /// <summary>
        /// �õ���������￨��
        /// </summary>
        /// <param name="input">����Ŀ���</param>
        /// <returns>�����0��10λ�ֳ������￨��</returns>
        private string FillCardNO(string input)
        {
            return input.PadLeft(10, '0');
        }

        /// <summary>
        /// �л�����
        /// </summary>
        private void NextFocus(Control nowControl)
        {
            SendKeys.Send("{TAB}");

            foreach (Control c in this.plMain.Controls)
            {
                 if (c.TabIndex > nowControl.TabIndex)
                {
                    if (c.Enabled == true && c.GetType() != typeof( FS.FrameWork.WinForms.Controls.NeuSpread)
                        && (c is FS.FrameWork.WinForms.Controls.NeuTextBox || c is FS.FrameWork.WinForms.Controls.NeuComboBox))
                    {
                        return;
                    }
                }
            }

            this.ChangeFocus();
        }

        /// <summary>
        /// ������֤����Ŀ����Ƿ�Ϸ�
        /// </summary>
        /// <param name="cardNO">����Ŀ���</param>
        /// <returns>�Ϸ�����true ���Ϸ�����false</returns>
        private bool IsInputCardNOValid(string cardNO)
        {
            //�������Ŀ�����һ�����߶���ո�,��ô��Ϊû������.
            if (cardNO.Trim() == string.Empty)
            {
                this.tbCardNO.SelectAll();
                this.tbCardNO.Focus();

                return false;
            }
            //�������Ŀ��ų��ȴ��� 1,���Ҳ��ǿո�.
            if (cardNO.Length >= 1)
            {
                //����û�����û�йҺ�ֱ���շѻ���,������������Ϣ�Ѿ�����¼��,��ô�ڿ��Żس���ʱ�������Ϣ.ֱ���л������������.
                if (this.noRegFlagChar == cardNO.Substring(0, 1) && this.patientInfo != null && this.patientInfo.ID != string.Empty && cardNO.Length == 10)
                {
                    if (this.patientInfo.PID.CardNO != cardNO)
                    {

                    }
                    else
                    {
                        this.tbName.Focus();

                        return false;
                    }
                }
            }
            return true;
        }  

        /// <summary>
        /// ��ý��������Ϣ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>���������Ϣ, nullʧ��</returns>
        private FS.HISFC.Models.Base.PactInfo GetPactInfoByPactCode(string pactCode)
        {
            FS.HISFC.Models.Base.PactInfo p = null;

            p = this.pactManager.GetPactUnitInfoByPactCode(pactCode);
            if (p == null)
            {
                MessageBox.Show("��ú�ͬ��λ��Ϣ����!" + this.pactManager.Err);

                return null;
            }

            return p;
        }

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        private void SetChargeInfo()
        {
            if (this.feeDetails == null)
            {
                return;
            }
            this.fpRecipeSeq_Sheet1.RowCount = 0;
            if (feeDetails.Count == 0)
            {
                this.AddNewRecipe();
                return;
            }
            ArrayList sortList = new ArrayList();
            while (feeDetails.Count > 0)
            {
                ArrayList sameNotes = new ArrayList();
                FeeItemList compareItem = feeDetails[0] as FeeItemList;
                foreach (FeeItemList f in feeDetails)
                {
                    if (f.RecipeSequence == compareItem.RecipeSequence)
                    {
                        sameNotes.Add(f);
                    }
                }
                sortList.Add(sameNotes);
                foreach (FeeItemList f in sameNotes)
                {
                    feeDetails.Remove(f);
                }
            }
            this.fpRecipeSeq_Sheet1.RowCount = 0;
            this.fpRecipeSeq_Sheet1.RowCount = sortList.Count;

            FS.FrameWork.Public.ObjectHelper objHelperDept = new FS.FrameWork.Public.ObjectHelper();
            objHelperDept.ArrayObject = this.regDeptList;
            FS.FrameWork.Public.ObjectHelper objHelperPact = new FS.FrameWork.Public.ObjectHelper();
            objHelperPact.ArrayObject = this.cmbPact.alItems;
            for (int i = 0; i < sortList.Count; i++)
            {
                ArrayList tempSameSeqs = sortList[i] as ArrayList;
                decimal cost = 0;
                foreach (FeeItemList f in tempSameSeqs)
                {
                    cost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                }
                FeeItemList tempFeeItemRowOne = ((FeeItemList)tempSameSeqs[0]).Clone();

                if (!string.IsNullOrEmpty(tempFeeItemRowOne.RecipeOper.Dept.ID))
                {
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].Text = objHelperDept.GetName(tempFeeItemRowOne.RecipeOper.Dept.ID);
                }
                else
                {
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].Text = objHelperDept.GetName(
                        ((FS.HISFC.Models.Registration.Register)tempFeeItemRowOne.Patient).DoctorInfo.Templet.Dept.ID);
                }

                //if (string.IsNullOrEmpty(tempFeeItemRowOne.Order.Patient.Pact.ID))
                //{
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].Text = (tempFeeItemRowOne.Order.ID.Length > 0 ? "����" : "����");
                //}
                //else
                //{
                //    this.fpRecipeSeq_Sheet1.Cells[i, 2].Text = (tempFeeItemRowOne.Order.ID.Length > 0 ? "����" : "����") + "[" + objHelperPact.GetName(tempFeeItemRowOne.Order.Patient.Pact.ID) + "]";
                //    this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag = tempFeeItemRowOne.Order.Patient.Pact.ID;
                //}

                this.fpRecipeSeq_Sheet1.Cells[i, 3].Text = cost.ToString();
                this.fpRecipeSeq_Sheet1.Rows[i].Tag = tempSameSeqs;
                this.fpRecipeSeq_Sheet1.Cells[i, 3].Tag = tempFeeItemRowOne.RecipeSequence;

                if (this.isSelectedAllRecipe)
                {
                    //ȫѡ������£�������ڶ��ͬ��λ��������ֻ���һ����ͬ�Ĵ���ȫѡ
                    if (this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag != null && this.fpRecipeSeq_Sheet1.Cells[0, 2].Tag != null)
                    {
                        if (!this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag.ToString().Equals(this.fpRecipeSeq_Sheet1.Cells[0, 2].Tag.ToString()))
                        {
                            continue;
                        }
                    }

                    // �����շѶദ���Ƿ�Ĭ��ȫѡ
                    this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = true;

                    if (i == 0)
                    {
                        this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                        this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Blue;
                        this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                        this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Blue;
                        this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                        this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Blue;

                        this.feeDetailsSelected = new ArrayList();
                        this.recipeSequence = tempFeeItemRowOne.RecipeSequence;
                    }

                    this.feeDetailsSelected.AddRange((ArrayList)tempSameSeqs.Clone());

                    SetRegInfoCanModify(tempFeeItemRowOne, true);
                    
                }
                else if (i == 0)
                {
                    this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = true;
                    this.fpRecipeSeq_Sheet1.Cells[0, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[0, 1].ForeColor = Color.Blue;
                    this.fpRecipeSeq_Sheet1.Cells[0, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[0, 2].ForeColor = Color.Blue;
                    this.fpRecipeSeq_Sheet1.Cells[0, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[0, 3].ForeColor = Color.Blue;
                    this.feeDetailsSelected = new ArrayList();
                    this.recipeSequence = tempFeeItemRowOne.RecipeSequence;

                    this.feeDetailsSelected = (ArrayList)tempSameSeqs.Clone();

                    SetRegInfoCanModify(tempFeeItemRowOne, true);
                }
            }
        }

        /// <summary>
        /// ���ú�ͬ��λ�����
        /// </summary>
        private void SetRelations()
        {
            relations = this.managerIntegrate.QueryRelationsByPactCode(this.patientInfo.Pact.ID);
            //���û���޶���ôֱ�ӽ�����ת
            if (relations == null || relations.Count == 0)
            {
                this.cmbClass.ClearItems();
                this.cmbClass.Tag = string.Empty;
                this.cmbClass.alItems.Clear();
            }
            else//���޶�
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                ArrayList displays = new ArrayList();
                this.cmbClass.alItems.Clear();
                foreach (FS.HISFC.Models.Base.PactStatRelation p in relations)
                {

                    if (obj.ID != p.Group.ID)
                    {
                        obj = new FS.FrameWork.Models.NeuObject();
                        displays.Add(obj);
                        obj.ID = p.Group.ID;
                        obj.Name += p.StatClass.Name + ": " + p.Quota.ToString() + " ";
                    }
                    else
                    {
                        obj.Name += p.StatClass.Name + ": " + p.Quota.ToString() + " ";
                    }
                }
                this.cmbClass.AddItems(displays);
                //���ֻ��һ���޶�,Ĭ��ѡ���һ��.
                if (displays.Count >= 1)
                {
                    if (this.isClassCodePre)
                    {
                        this.cmbClass.SelectedIndex = 0;

                        this.patientInfo.User03 = cmbClass.Tag.ToString();
                    }
                    else
                    {
                        this.cmbClass.Tag = string.Empty;
                        this.cmbClass.Text = string.Empty;
                    }
                }
            }
        }

        /// <summary>
        /// ��ͬ��λ�仯ʱ��ѡ��Ĵ�����Ϣ���Ÿı�
        /// </summary>
        private void SetRecipePact()
        {
            if (string.IsNullOrEmpty(this.patientInfo.Pact.ID))
            {
                return;
            }

            bool isRefreshItemList = false;
            this.feeDetailsSelected = new ArrayList();
            for (int iRow = 0; iRow < this.fpRecipeSeq_Sheet1.Rows.Count; iRow++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpRecipeSeq_Sheet1.Cells[iRow, 0].Value) == true)
                {
                    ArrayList alFeeDetail = this.fpRecipeSeq_Sheet1.Rows[iRow].Tag as ArrayList;
                    if (alFeeDetail != null && alFeeDetail.Count > 0)
                    {
                        FS.HISFC.Models.Fee.Outpatient.FeeItemList firstItemList = alFeeDetail[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                        if (!string.IsNullOrEmpty(firstItemList.Order.Patient.Pact.ID))
                        {
                            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList itemList in alFeeDetail)
                            {
                                itemList.Order.Patient.Pact.ID = this.patientInfo.Pact.ID;
                                itemList.Order.Patient.Pact.Name = this.patientInfo.Pact.Name;
                                itemList.Order.Patient.Pact.PayKind.ID = this.patientInfo.Pact.PayKind.ID;
                                itemList.Order.Patient.Pact.PayKind.Name = this.patientInfo.Pact.PayKind.Name;
                            }
                            this.fpRecipeSeq_Sheet1.Cells[iRow, 2].Text = (firstItemList.Order.ID.Length > 0 ? "����" : "����") + "[" + this.patientInfo.Pact.Name + "]";
                            this.fpRecipeSeq_Sheet1.Cells[iRow, 2].Tag = this.patientInfo.Pact.ID;
                            isRefreshItemList = true;
                        }

                        this.feeDetailsSelected.AddRange(alFeeDetail);
                    }
                }
            }

            if (isRefreshItemList)
            {
                this.RecipeSeqChanged();
            }
        }



        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="age"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public void GetAgeNumber(string age, ref int year, ref int month, ref int day)
        {
            year = 0;
            month = 0;
            day = 0;
            age = age.Replace("_", "");
            int ageIndex = age.IndexOf("��");
            int monthIndex = age.IndexOf("��");
            int dayIndex = age.IndexOf("��");

            if (ageIndex > 0)
            {
                year = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, ageIndex));
            }
            if (ageIndex >= 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, monthIndex - ageIndex - 1));
            }
            if (ageIndex < 0 && monthIndex > 0 && monthIndex > ageIndex)
            {
                month = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, monthIndex));//ֻ����
            }
            if (monthIndex >= 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(monthIndex + 1, dayIndex - monthIndex - 1));
            }
            if (ageIndex < 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(0, dayIndex));//ֻ����
            }
            if (ageIndex >= 0 && monthIndex < 0 && dayIndex > 0 && dayIndex > monthIndex)
            {
                day = FS.FrameWork.Function.NConvert.ToInt32(age.Substring(ageIndex + 1, dayIndex - ageIndex - 1));//ֻ���꣬��
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="sysdate"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public DateTime GetAge(DateTime sysdate, int iyear, int iMonth, int iDay)
        {
            int year = sysdate.Year - iyear;
            int m = sysdate.Month - iMonth;
            if (m <= 0)
            {
                if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month + m;
                }
            }

            int day = sysdate.Day - iDay;
            if (day <= 0)
            {
                if (m > 0)
                {
                    m = m - 1;
                    DateTime dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }
                else if (year > 0)
                {
                    year = year - 1;
                    DateTime dt = new DateTime(year, 1, 1);
                    m = dt.AddYears(1).AddDays(-1).Month - 1;
                    dt = new DateTime(year, m + 1, 1).AddMonths(-1);
                    day = dt.AddMonths(1).AddDays(-1).Day + day;
                }

                if (m <= 0)
                {
                    if (year > 0)
                    {
                        year = year - 1;
                        DateTime dt = new DateTime(year, 1, 1);
                        m = dt.AddYears(1).AddDays(-1).Month + m;
                    }
                }
            }
            else
            {
                DateTime dt = new DateTime(year, m, 1);
                if (day > dt.AddMonths(1).AddDays(-1).Day)
                {
                    day = dt.AddMonths(1).AddDays(-1).Day;
                }
            }

            return new DateTime(year, m, day);
        }
        #endregion

        #endregion

        #region �¼�

        /// <summary>
        /// ���Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cardNO = this.tbCardNO.Text.Trim();

                if (!IsInputCardNOValid(cardNO)) 
                {
                    return;
                }

                //����Ѿ�¼�����Ϣ.����ѡ����Ϣ,����.
                this.Clear();

                //����������������"/"��"+"��ͷ����Ϊ���շ�ʱ�����û�йҺ�
                //����Һ�ҵ�񣬲�ͨ����������ݼ�����Ϣ
                //�������ݵȴ�����Ա����
                //if (cardNO.StartsWith("/") || cardNO.StartsWith("+"))//���뷽ʽΪ"/"+�����������ǲ��Һ�ֱ�������������
                //{
                //    //������￨��
                //    string autoCardNO = this.outpatientManager.GetAutoCardNO();
                //    if (autoCardNO == string.Empty)
                //    {
                //        MessageBox.Show("������￨�ų���!" + this.outpatientManager.Err);
                //        this.tbCardNO.Focus();

                //        return;
                //    }

                //    autoCardNO = this.noRegFlagChar + autoCardNO;

                //    //��þ�����ˮ��
                //    string clinicNO = this.outpatientManager.GetSequence("Registration.Register.ClinicID");
                //    if (clinicNO == string.Empty)
                //    {
                //        MessageBox.Show("����������ų���!" + this.outpatientManager.Err);
                //        this.tbCardNO.Focus();

                //        return;
                //    }
                //    this.patientInfo = new FS.HISFC.Models.Registration.Register(); //ʵ�����Һ���Ϣʵ��
                //    //ȥ��'/'��û�������
                //    string name = cardNO.Remove(0, 1);
                //    this.tbCardNO.Text = autoCardNO;
                //    this.tbName.Text = name;
                //    this.cmbSex.Tag = "M";
                //    this.tbAge.Text = "";
                //    this.cmbPact.Tag = "1";
                  
                //    this.isRecordDirectFee = true;
                //    this.patientInfo.ID = clinicNO;
                //    this.patientInfo.Name = name;
                //    //this.patientInfo.Card.ID = autoCardNO;
                //    this.patientInfo.PID.CardNO = autoCardNO;
                //    this.patientInfo.Pact.PayKind.ID = "01";
                //    this.patientInfo.Pact.ID = "1";
                //    //this.patientInfo.Birthday = DateTime.Now.AddYears(-20);
                //    this.patientInfo.Birthday = DateTime.MinValue;
                //    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                //    this.patientInfo.DoctorInfo.SeeDate = nowTime;
                //    this.fpRecipeSeq_Sheet1.RowCount = 0;
                //    this.AddNewRecipe();

                //    this.isRecordDirectFee = false;
                //}
                //else //�������뻼�����￨�����
                {
                    FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
                    if (feeIntegrate.ValidMarkNO(cardNO, ref accountCard) > 0)
                    {
                        cardNO = accountCard.Patient.PID.CardNO;
                        decimal vacancy = 0m;
                        if (feeIntegrate.GetAccountVacancy(accountCard.Patient.PID.CardNO, ref vacancy) > 0)
                        {
                            if (isAccountTerimalFee)
                            {
                                MessageBox.Show("�˻�������ȥ�ն��շѣ�");
                                return;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(feeIntegrate.Err);
                        return;
                    }
                    cardNO = accountCard.Patient.PID.CardNO;

                    bool isValid = false;

                    string tmpOrgCardNo = cardNO;
                    //��俨�ŵ�10λ����0
                    cardNO = this.FillCardNO(cardNO);
                    this.patientInfo = new FS.HISFC.Models.Registration.Register(); //ʵ�����Һ���Ϣʵ��

                    //add
                    this.isDirectFeePatient = false;
                    //end add

                    //������ʾ������Ϣ�ؼ�
                    isValid = InputedCardAndEnter(cardNO, tmpOrgCardNo, this.tbCardNO.Location, this.tbCardNO.Height);

                    if (isValid) //�����õĻ��߻�����Ϣ��Ч����ô��ת���㵽ѡ��ҽ��
                    {
                        if (this.isCanModifyPatientInfo)
                        {
                            //if (this.patientInfo.DoctorInfo.Templet.Doct.ID != null && this.patientInfo.DoctorInfo.Templet.Doct.ID.Length > 0)
                            //if(this.patientInfo.SeeDoct.ID != null && this.patientInfo.SeeDoct.ID != ""&&this.patientInfo.SeeDoct.Name!=null&&this.patientInfo.SeeDoct.Name!="")
                            if (this.patientInfo.SeeDoct.ID != null && this.patientInfo.SeeDoct.ID != "" && this.patientInfo.SeeDoct.Name != null )
                            {
                                this.ChangeFocus();
                            }
                            else
                            {
                                this.cmbDoct.Focus();
                            }

                            this.cmbDoct.Enabled = true;
                            this.cmbRegDept.Enabled = true;
                        }
                        else
                        {
                            if (this.patientInfo.SeeDoct.ID != null && this.patientInfo.SeeDoct.ID != "" && this.patientInfo.SeeDoct.Name != null)
                            {
                                this.cmbDoct.Enabled = false;
                                ChangeFocus();
                            }
                            else
                            {
                                this.cmbDoct.Focus();
                            }

                            this.cmbRegDept.Enabled = false;
                        }
                    }
                    else //�����Ч���ţ���ô�������뿨��
                    {
                        this.tbCardNO.SelectAll();
                        this.tbCardNO.Focus();
                    }
                }

                FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen ioutScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen)) as FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen;
                if (ioutScreen == null)
                {
                    return;
                }
                else
                {
                    ioutScreen.ClearInfo();
                    ioutScreen.ShowInfo(this.patientInfo);
                }
            }

            if (e.KeyCode == Keys.Space)//{7EEF23C0-631F-4cfa-9DFA-E62453A2307A}
            {
                FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
                if (FS.HISFC.Components.Common.Classes.Function.QueryComPatientInfo(ref p) == 1)
                {
                    this.tbCardNO.Text = p.PID.CardNO;
                    this.tbCardNO_KeyDown(null, new KeyEventArgs(Keys.Enter));
                }
            }
        }

        /// <summary>
        /// UC��ʼ���¼�,��tbCardNO���佹��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucPatientInfo_Load(object sender, EventArgs e)
        {
            initInputMenu();
            tbName.Enter += new EventHandler(tbName_Enter);
            readInputLanguage();
            this.tbCardNO.Focus();
            isAccountTerimalFee = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
        }

        void tbName_Enter(object sender, EventArgs e)
        {
            if (this.CHInput != null) InputLanguage.CurrentInputLanguage = this.CHInput;
        }
        /// <summary>
        /// �������뷨�б�
        /// </summary>
        private void initInputMenu()
        {
            plMain.ContextMenuStrip = this.neuContextMenuStrip1;
            for (int i = 0; i < InputLanguage.InstalledInputLanguages.Count; i++)
            {
                InputLanguage t = InputLanguage.InstalledInputLanguages[i];
                System.Windows.Forms.ToolStripMenuItem m = new ToolStripMenuItem();
                m.Text = t.LayoutName;
                //m.Checked = true;
                m.Click += new EventHandler(m_Click);

                this.neuContextMenuStrip1.Items.Add(m);
            }
        }
        #region ���뷨�˵��¼�
        private void m_Click(object sender, EventArgs e)
        {
            foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
            {
                if (sender == m)
                {
                    m.Checked = true;
                    this.CHInput = this.getInputLanguage(m.Text);
                    //�������뷨
                    this.saveInputLanguage();
                }
                else
                {
                    m.Checked = false;
                }
            }
        }
        /// <summary>
        /// ��ȡ��ǰĬ�����뷨
        /// </summary>
        private void readInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();

            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                this.CHInput = this.getInputLanguage(node.Attributes["currentmodel"].Value);

                if (this.CHInput != null)
                {
                    foreach (ToolStripMenuItem m in this.neuContextMenuStrip1.Items)
                    {
                        if (m.Text == this.CHInput.LayoutName)
                        {
                            m.Checked = true;
                        }
                    }
                }

                //��ӵ�������

            }
            catch (Exception e)
            {
                MessageBox.Show("��ȡ�Һ�Ĭ���������뷨����!" + e.Message);
                return;
            }
        }

        private void addContextToToolbar()
        {
            FS.FrameWork.WinForms.Controls.NeuToolBar main = null;

            foreach (Control c in FindForm().Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuToolBar))
                {
                    main = (FS.FrameWork.WinForms.Controls.NeuToolBar)c;
                }
            }

            ToolBarButton button = null;

            if (main != null)
            {
                foreach (ToolBarButton b in main.Buttons)
                {
                    if (b.Text == "���뷨") button = b;
                }
            }

            //if(button != null)
            //{
            //    ToolStripDropDownButton drop = (ToolStripDropDownButton)button;
            //    foreach(ToolStripMenuItem m in neuContextMenuStrip1.Items)
            //    {
            //        drop.DropDownItems.Add(m);
            //    }
            //}
        }

        /// <summary>
        /// �������뷨���ƻ�ȡ���뷨
        /// </summary>
        /// <param name="LanName"></param>
        /// <returns></returns>
        private InputLanguage getInputLanguage(string LanName)
        {
            foreach (InputLanguage input in InputLanguage.InstalledInputLanguages)
            {
                if (input.LayoutName == LanName)
                {
                    return input;
                }
            }
            return null;
        }
        /// <summary>
        /// ���浱ǰ���뷨
        /// </summary>
        private void saveInputLanguage()
        {
            if (!System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml"))
            {
                FS.HISFC.Components.Common.Classes.Function.CreateFeeSetting();
            }
            if (this.CHInput == null) return;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
                XmlNode node = doc.SelectSingleNode("//IME");

                node.Attributes["currentmodel"].Value = this.CHInput.LayoutName;

                doc.Save(FS.FrameWork.WinForms.Classes.Function.SettingPath + "/feeSetting.xml");
            }
            catch (Exception e)
            {
                MessageBox.Show("����Һ�Ĭ���������뷨����!" + e.Message);
                return;
            }
        }
        #endregion
        /// <summary>
        /// ҽ�������б����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string name = this.cmbDoct.Text;
                if (name == null || name == string.Empty)
                {
                    MessageBox.Show(Language.Msg("������ҽ��"));
                    this.cmbDoct.Focus();

                    return;
                }

                this.cmbDoct_SelectedIndexChanged(sender, e);
                
                if (this.isCanModifyPatientInfo)
                {
                    //this.cmbPact.Focus();
                    this.cmbRegDept.Focus();
                }
                else
                {
                    this.NextFocus(this.cmbDoct);
                }

            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.cmbRegDept.Focus();
                this.cmbRegDept.SelectAll();
            }
        }

        /// <summary>
        /// ҽ��ѡ���б������仯�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            //modify {3D863FFD-DAB6-43a7-80C8-DFD35B585BC2}
            if (this.patientInfo == null)
            {   
                //���ʱ����
                this.isDirectFeePatient = true;
                this.isCanChangeDoct = true;
                this.cmbDoct.Enabled = true;
                this.cmbRegDept.Enabled = true;

                return;
            }
            this.isCanChangeDoct = this.controlParamIntegrate.GetControlParam<bool>("MZ0205", true, true);

            if (this.isDirectFeePatient == false && this.isCanChangeDoct == false)
            {
                this.cmbDoct.Enabled = false;
                this.cmbRegDept.Enabled = false;

            }
            else
            {
                this.isDirectFeePatient = true;
            }
            //end modify 
            this.patientInfo.DoctorInfo.Templet.Doct.ID = this.cmbDoct.Tag.ToString();
            this.patientInfo.DoctorInfo.Templet.Doct.Name = this.cmbDoct.Text;
            string recipeSeq = string.Empty;

            FS.HISFC.Models.Base.Employee person = this.managerIntegrate.GetEmployeeInfo(this.patientInfo.DoctorInfo.Templet.Doct.ID);
            if (person == null)
            {
                MessageBox.Show("���ҽ����Ϣ����!" + managerIntegrate.Err);

                return;
            }

            bool isDoctDeptSame = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.DOCT_CONFIRM_DEPT, false, true);

            if (isDoctDeptSame && this.patientInfo.PID.CardNO.Substring(0, 1) == this.noRegFlagChar) 
            {
                DateTime sysDate = FS.FrameWork.Function.NConvert.ToDateTime(outpatientManager.GetSysDateTime());
                FS.HISFC.Models.Registration.Schema schema = schemaMgr.GetSchema(this.patientInfo.DoctorInfo.Templet.Doct.ID, sysDate);
                if (schema != null)
                {
                    this.cmbRegDept.Tag = schema.Templet.Dept.ID;
                }
                else
                {
                this.cmbRegDept.Tag = person.Dept.ID;
                }
            }

            if (!string.IsNullOrEmpty(patientInfo.SeeDoct.Dept.ID))
            {
                this.patientInfo.DoctorInfo.Templet.Doct.User01 = patientInfo.SeeDoct.Dept.ID;
            }
            else if (!string.IsNullOrEmpty(patientInfo.DoctorInfo.Templet.Dept.ID))
            {
                this.patientInfo.DoctorInfo.Templet.Doct.User01 = patientInfo.DoctorInfo.Templet.Dept.ID;
            }
            else
            {
                this.patientInfo.DoctorInfo.Templet.Doct.User01 = person.Dept.ID;
            }
            
            
            if (this.fpRecipeSeq_Sheet1.RowCount > 0)
            {
                int row = this.fpRecipeSeq_Sheet1.ActiveRowIndex;

                //this.fpRecipeSeq_Sheet1.Cells[row, 1].Text = this.deptHelper.GetName(person.Dept.ID);//this.patientInfo.DoctorInfo.Templet.Doct.Name;
                //this.fpRecipeSeq_Sheet1.Cells[row, 1].Tag = person.Dept.ID;//this.patientInfo.DoctorInfo.Templet.Doct.ID;
                try
                {
                    foreach (FeeItemList f in (ArrayList)fpRecipeSeq_Sheet1.Rows[row].Tag)
                    {
                        ((Register)f.Patient).DoctorInfo.Templet.Doct = this.patientInfo.DoctorInfo.Templet.Doct.Clone();
                        f.RecipeOper.Dept.ID = this.patientInfo.DoctorInfo.Templet.Doct.User01.Clone().ToString();
                        recipeSeq = f.RecipeSequence;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    return;
                }

                this.SeeDoctChanged(recipeSeq, this.patientInfo.DoctorInfo.Templet.Doct.User01.Clone().ToString(), this.patientInfo.DoctorInfo.Templet.Doct.Clone());
            }
        }

        /// <summary>
        /// ������ҷ����仯�󴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo == null)
            {
                return;
            }

            this.patientInfo.DoctorInfo.Templet.Dept.ID = this.cmbRegDept.Tag.ToString();
            this.patientInfo.DoctorInfo.Templet.Dept.Name = this.cmbRegDept.Text;
            this.patientInfo.DoctorInfo.Templet.Doct.User01 = this.cmbRegDept.Tag.ToString();
            string recipeSeq = string.Empty;

            if (this.fpRecipeSeq_Sheet1.RowCount > 0)
            {
                int row = this.fpRecipeSeq_Sheet1.ActiveRowIndex;
                this.fpRecipeSeq_Sheet1.Cells[row, 1].Text = this.patientInfo.DoctorInfo.Templet.Dept.Name;
                this.fpRecipeSeq_Sheet1.Cells[row, 1].Tag = this.patientInfo.DoctorInfo.Templet.Dept.ID;

                try
                {
                    foreach (FeeItemList f in (ArrayList)fpRecipeSeq_Sheet1.Rows[row].Tag)
                    {
                        f.RecipeOper.Dept = this.patientInfo.DoctorInfo.Templet.Dept.Clone();
                        recipeSeq = f.RecipeSequence;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    return;
                }

                this.SeeDeptChanaged(recipeSeq, string.Empty, this.patientInfo.DoctorInfo.Templet.Dept.Clone());
            }
        }

        /// <summary>
        /// ������һس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRegDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null)
                {
                    return;
                }

                string name = this.cmbRegDept.Text;

                if (name == null || name == string.Empty)
                {
                    MessageBox.Show(Language.Msg("�����뿴�����"));
                    this.cmbRegDept.Focus();

                    return;
                }
                if (this.isCanModifyPatientInfo)
                {
                    //this.cmbDoct.Focus();
                    this.cmbPact.Focus();
                }
                else
                {
                    NextFocus(this.cmbRegDept);
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.tbAge.Focus();
                this.tbAge.SelectAll();
            }
        }

        /// <summary>
        /// ��ͬ��λ�س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null)
                {
                    return;
                }

                string name = this.cmbPact.Text;
                
                if (name == null || name == string.Empty)
                {
                    MessageBox.Show(Language.Msg("�������ͬ��λ"));
                    this.cmbPact.Focus();

                    return;
                }
                if (this.cmbPact.Tag == null || this.cmbPact.Tag.ToString() == string.Empty)
                {
                    MessageBox.Show(Language.Msg("�������ͬ��λ"));
                    this.cmbPact.Focus();

                    return;
                }
                if (this.patientInfo.Pact.ID != this.cmbPact.Tag.ToString())
                {
                    this.patientInfo.Pact = this.GetPactInfoByPactCode(this.cmbPact.Tag.ToString());
                    if (this.patientInfo.Pact == null)
                    {
                        this.cmbPact.Focus();

                        return;
                    }

                    //������ͬ��λ�仯�¼�
                    this.PactChanged();

                    if (this.patientInfo.Pact.PayKind.ID == "01")
                    {
                        this.tbMCardNO.Text = string.Empty;
                    }
                    //��øú�ͬ��λ�µ��޶�
                    relations = this.managerIntegrate.QueryRelationsByPactCode(this.patientInfo.Pact.ID);
                    this.cmbClass.ClearItems();
                    this.cmbClass.Tag = string.Empty;
                    this.cmbClass.alItems.Clear();
                }
                //���û���޶���ôֱ�ӽ�����ת
                if (relations == null || relations.Count == 0)
                {
                    if (this.patientInfo.Pact.IsNeedMCard)
                    {
                        if (this.isCanModifyPatientInfo)
                        {
                            this.tbMCardNO.Focus();
                        }
                        else
                        {
                            NextFocus(this.cmbPact);
                        }
                    }
                    else
                    {
                        if (!this.IsPatientInfoValid())
                        {
                            return;
                        }
                        //������ת�����¼�
                        ChangeFocus();
                    }
                }
                else//���޶�
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    ArrayList displays = new ArrayList();
                    foreach (FS.HISFC.Models.Base.PactStatRelation p in relations)
                    {

                        if (obj.ID != p.Group.ID)
                        {
                            obj = new FS.FrameWork.Models.NeuObject();
                            displays.Add(obj);
                            obj.ID = p.Group.ID;
                            obj.Name += p.StatClass.Name + ": " + p.Quota.ToString() + " ";
                        }
                        else
                        {
                            obj.Name += p.StatClass.Name + ": " + p.Quota.ToString() + " ";
                        }
                    }
                    if (this.patientInfo.Pact.ID != this.cmbPact.Tag.ToString())
                    {
                        this.cmbClass.AddItems(displays);
                    }
                    //���ֻ��һ���޶�,Ĭ��ѡ���һ��.
                    if (displays.Count >= 1)
                    {
                        if (this.isClassCodePre)
                        {
                            if (this.patientInfo.Pact.ID != this.cmbPact.Tag.ToString())
                            {
                                this.cmbClass.SelectedIndex = 0;
                            }

                            if (this.cmbClass.Tag == null || this.cmbClass.Tag.ToString() == string.Empty)
                            {
                                MessageBox.Show(Language.Msg("������ȼ�����"));
                                this.cmbClass.Focus();

                                return;
                            }
                            this.patientInfo.User03 = this.cmbClass.Tag.ToString();
                        }
                        else
                        {
                            this.cmbClass.Tag = string.Empty;
                            this.cmbClass.Text = string.Empty;
                        }
                    }
                   
                    if (this.patientInfo.Pact.IsNeedMCard)
                    {
                        if (this.isCanModifyPatientInfo)
                        {
                            NextFocus(this.cmbClass);
                        }
                        else
                        {
                            NextFocus(this.cmbClass);
                        }
                    }
                    else 
                    {
                        this.ChangeFocus();
                    }
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.cmbDoct.Focus();
                this.cmbDoct.SelectAll();
            }
        }

        /// <summary>
        /// ��ͬ��λ�л������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo == null)
            {
                return;
            }
            //�����ͬ��λ��ɾ����������ѡ���ͬ��λʱδ����������ʵ��
            //{7E761CF9-3F36-4c28-A6AD-AAFBA9114AB6}

            try
            {
                this.patientInfo.Pact.ID = this.cmbPact.Tag.ToString();
                this.patientInfo.Pact.Name = this.cmbPact.Text;
            }
            catch { }

            this.patientInfo.Pact = this.GetPactInfoByPactCode(this.cmbPact.Tag.ToString());
            if (this.patientInfo.Pact == null)
            {
                this.cmbPact.Focus();

                return;
            }

            if (this.patientInfo.Pact.PayKind.ID == "01")//�Է�
            {
                this.cmbClass.Enabled = false;
                this.tbMCardNO.Enabled = false;
                this.cmbRebate.Enabled = false;
                this.tbMCardNO.Text = string.Empty;

                this.tbJZDNO.Enabled = false;
                this.tbJZDNO.Text = string.Empty;
            }
            else if (this.patientInfo.Pact.PayKind.ID == "02")//ҽ��
            {
                this.cmbClass.Enabled = false;
                this.tbMCardNO.Enabled = true;
                this.cmbRebate.Enabled = false;

                this.tbJZDNO.Enabled = false;
                this.tbJZDNO.Text = string.Empty;

                this.cmbRebate.SelectedIndexChanged -= new EventHandler(cmbRebate_SelectedIndexChanged);
                this.cmbRebate.Tag = string.Empty;
                this.cmbRebate.Text = string.Empty;
                this.cmbRebate.SelectedIndexChanged += new EventHandler(cmbRebate_SelectedIndexChanged);

                #region ҽ������û�йҺ�ʱ���շ�ʱ�Զ��Ǽ�

                if (this.isRegWhenFee)
                {
                    bool iResult = true;
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.interfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    this.interfaceProxy.SetPactCode(this.patientInfo.Pact.ID);

                    if (this.interfaceProxy.Connect() == -1) 
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.interfaceProxy.Rollback();
                        MessageBox.Show("����ҽ������!" + this.interfaceProxy.ErrMsg);
                        iResult = false;
                    }

                    //����Ϊ�ղ�����
                    if (this.patientInfo.DoctorInfo.Templet.Dept.ID == null || this.patientInfo.DoctorInfo.Templet.Dept.ID == string.Empty)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.interfaceProxy.Rollback();
                        MessageBox.Show(Language.Msg("�Һſ��Ҳ���Ϊ�գ�"));
                        iResult = false;
                    }

                    //{3676F424-0B1E-479b-9ABB-11D3B25AC8AE} ����������Ͳ�ִ��ҽ���Һ�By GXLei
                    if (iResult)
                    {
                        //��ȡҽ���Ǽ���Ϣ
                        if (this.interfaceProxy.GetRegInfoOutpatient(this.patientInfo) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.interfaceProxy.Rollback();
                            MessageBox.Show(interfaceProxy.ErrMsg);
                            iResult = false;
                        }
                    }
                    //{FFF43E1D-C9D6-4cfa-9A38-D0C619A486C3} ҽ������ֱ�ӹҺ�By GXLei
                    if (iResult)
                    {
                        if (this.interfaceProxy.UploadRegInfoOutpatient(this.patientInfo) != 1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.interfaceProxy.Rollback();
                            MessageBox.Show(interfaceProxy.ErrMsg);
                            iResult = false;
                        }
                    }

                    //�Ͽ�����
                    if (this.interfaceProxy.Disconnect() != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.interfaceProxy.Rollback();
                        MessageBox.Show(interfaceProxy.ErrMsg);
                        iResult = false;
                    }
                    if (iResult)
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                        interfaceProxy.Commit();

                        this.SetRegInfo();
                    }
                }

                #endregion
            }
            else//����
            {
                this.cmbClass.Enabled = true;
                this.tbMCardNO.Enabled = true;
                this.tbJZDNO.Enabled = true;
                this.cmbRebate.Enabled = false;
                this.cmbRebate.SelectedIndexChanged -= new EventHandler(cmbRebate_SelectedIndexChanged);
                this.cmbRebate.Tag = string.Empty;
                this.cmbRebate.Text = string.Empty;
                this.cmbRebate.SelectedIndexChanged += new EventHandler(cmbRebate_SelectedIndexChanged);
            }

            //�ı��������
            //if (sender != null)
            //{
            //    this.SetRecipePact();
            //}

            //������ͬ��λ�¼�
            this.PactChanged();

            relations = this.managerIntegrate.QueryRelationsByPactCode(this.patientInfo.Pact.ID);
            //���û���޶���ôֱ�ӽ�����ת
            if (relations == null || relations.Count == 0)
            {
                cmbClass.ClearItems();
                cmbClass.Tag = string.Empty;
                cmbClass.alItems.Clear();
            }
            else//���޶�
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                ArrayList displays = new ArrayList();
                cmbClass.alItems.Clear();
                foreach (FS.HISFC.Models.Base.PactStatRelation p in relations)
                {

                    if (obj.ID != p.Group.ID)
                    {
                        obj = new FS.FrameWork.Models.NeuObject();
                        displays.Add(obj);
                        obj.ID = p.Group.ID;
                        obj.Name += p.StatClass.Name + ": " + p.Quota.ToString() + " ";
                    }
                    else
                    {
                        obj.Name += p.StatClass.Name + ": " + p.Quota.ToString() + " ";
                    }
                }
                this.cmbClass.AddItems(displays);
                //���ֻ��һ���޶�,Ĭ��ѡ���һ��.
                if (displays.Count >= 1)
                {
                    if (this.isClassCodePre)
                    {
                        this.cmbClass.SelectedIndex = 0;
                        
                        this.patientInfo.User03 = cmbClass.Tag.ToString();
                    }
                    else
                    {
                        this.cmbClass.Tag = string.Empty;
                        this.cmbClass.Text = string.Empty;
                    }
                }
            }
            this.PriceRuleChanaged();
        }

        /// <summary>
        /// �Żݷ����仯����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRebate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo == null)
            {
                return;
            }
            
            this.patientInfo.Pact = this.GetPactInfoByPactCode(this.cmbPact.Tag.ToString());
            if (this.patientInfo.Pact == null)
            {
                this.cmbPact.Focus();

                return;
            }
         
            this.patientInfo.User03 = this.cmbClass.Tag.ToString();
            this.patientInfo.User02 = this.cmbRebate.Tag.ToString();

            this.PactChanged();
        }

        /// <summary>
        /// ҽ��֤�Żس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null)
                {
                    return;
                }
                if (this.patientInfo.Pact.IsNeedMCard)
                {
                    if (this.tbMCardNO.Text == string.Empty)
                    {
                        MessageBox.Show(Language.Msg("������ҽ��֤��"));
                        this.tbMCardNO.Focus();

                        return;
                    }
                    else
                    {
                        this.patientInfo.SSN = this.tbMCardNO.Text.Trim();
                        if (!this.IsPatientInfoValid())
                        {
                            return;
                        }

                        ChangeFocus();
                    }
                }
                else if(this.patientInfo.Pact.PayKind.ID=="02")
                {
                    if (this.patientInfo.IDCard==null||this.patientInfo.IDCard=="")
                    {
                        this.patientInfo.IDCard = this.tbMCardNO.Text;
                    }
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.cmbClass.Focus();
                this.cmbClass.SelectAll();
            }
        }

        /// <summary>
        /// ��ͬ��λ��������仯��ʱ�򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo == null)
            {
                return;
            }
            this.patientInfo.Pact = this.GetPactInfoByPactCode(this.cmbPact.Tag.ToString());
            if (this.patientInfo.Pact == null)
            {
                this.cmbPact.Focus();

                return;
            }
           
            this.patientInfo.User03 = cmbClass.Tag.ToString();

            this.PactChanged();
        }

        /// <summary>
        /// ��ͬ��λ�����س�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbClass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null)
                {
                    return;
                }
                string name = this.cmbClass.Text;

                if (this.cmbClass.alItems.Count > 0)
                {
                    if (name == null || name == string.Empty)
                    {
                        MessageBox.Show(Language.Msg("������ȼ�����"));
                        this.cmbClass.Focus();

                        return;
                    }
                    if (this.cmbClass.Tag == null || this.cmbClass.Tag.ToString() == string.Empty)
                    {
                        MessageBox.Show(Language.Msg("������ȼ�����"));
                        this.cmbClass.Focus();

                        return;
                    }
                    this.patientInfo.User03 = cmbClass.Tag.ToString();
                    this.cmbClass.Text = cmbClass.Tag.ToString();
                }

                if (this.patientInfo.Pact.IsNeedMCard)
                {
                    if (this.isCanModifyPatientInfo)
                    {
                        this.tbMCardNO.Focus();
                    }
                    else
                    {
                        NextFocus(this.cmbClass);
                    }
                }
                else
                {
                    if (this.cmbDoct.Tag == null || this.cmbDoct.Tag.ToString() == string.Empty)
                    {
                        MessageBox.Show(Language.Msg("������ҽ��!"));
                        this.cmbDoct.Focus();

                        return;
                    }
                    else
                    {
                        //������ת�����¼�
                        if (!this.IsPatientInfoValid())
                        {
                            return;
                        }
                        ChangeFocus();
                    }
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.cmbPact.Focus();
                this.cmbPact.SelectAll();
            }
        }

        /// <summary>
        /// ��������ؼ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpRecipeSeq_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == 0)//���ѡ���
            {
                if (this.fpRecipeSeq_Sheet1.RowCount <= 1)
                {
                    this.fpRecipeSeq_Sheet1.Cells[0, 0].Value = true;

                    return;
                }
                this.fpRecipeSeq.StopCellEditing();
                ArrayList selectedItems = new ArrayList();
                this.feeDetailsSelected = new ArrayList();
                this.fpRecipeSeq_Sheet1.ActiveRowIndex = e.Row;

                int selectedCount = 0;
                int selectedRow = 0;

                //string pactID = string.Empty;
                //string pactIDNow = string.Empty;
                //if (this.fpRecipeSeq_Sheet1.Cells[e.Row, 2].Tag != null)
                //{
                //    pactID = this.fpRecipeSeq_Sheet1.Cells[e.Row, 2].Tag.ToString();
                //}

                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text.ToString() == "True")
                    {
                        //if (this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag != null)
                        //{
                        //    pactIDNow = this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag.ToString();
                        //}
                        //if (pactID.Equals(pactIDNow))
                        //{
                            selectedItems.Add(this.fpRecipeSeq_Sheet1.Rows[i].Tag);
                            selectedCount++;
                            selectedRow = i;
                        //}
                        //else
                        //{
                        //    this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = false;
                        //}
                    }
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
                }

                //����ֻʣ��ѡ����һ��
                if (selectedCount == 1)
                {
                    fpRecipeSeq_Sheet1.ActiveRowIndex = selectedRow;
                    this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].ForeColor = Color.Blue;
                    this.fpRecipeSeq_Sheet1.Cells[selectedRow, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[selectedRow, 2].ForeColor = Color.Blue;
                    this.fpRecipeSeq_Sheet1.Cells[selectedRow, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[selectedRow, 3].ForeColor = Color.Blue;

                    foreach (ArrayList al in selectedItems)
                    {
                        foreach (FeeItemList f in al)
                        {
                            this.feeDetailsSelected.Add(f);
                        }
                    }
                    ArrayList alTemp = new ArrayList();
                    alTemp = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[selectedRow].Tag;
                    if (alTemp.Count > 0)
                    {
                        SetRegInfoCanModify(((FeeItemList)alTemp[0]), true);
                    }
                    else
                    {
                        //{F132172F-59C0-40cc-ACCA-DA3362D53689}
                        if (this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].Tag != null)
                        {
                            this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].Tag.ToString();
                        }
                        this.cmbDoct.Tag = null;
                    }
                    this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[selectedRow, 3].Tag.ToString();
                    this.IFCanAddItem();
                    this.RecipeSeqChanged();
                }
                else
                {
                    this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].ForeColor = Color.Blue;
                    this.fpRecipeSeq_Sheet1.Cells[e.Row, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[e.Row, 2].ForeColor = Color.Blue;
                    this.fpRecipeSeq_Sheet1.Cells[e.Row, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                    this.fpRecipeSeq_Sheet1.Cells[e.Row, 3].ForeColor = Color.Blue;

                    foreach (ArrayList al in selectedItems)
                    {
                        foreach (FeeItemList f in al)
                        {
                            this.feeDetailsSelected.Add(f);
                        }
                    }
                    ArrayList alTemp = new ArrayList();
                    alTemp = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[e.Row].Tag;
                    if (alTemp.Count > 0)
                    {
                        SetRegInfoCanModify(((FeeItemList)alTemp[0]), true);
                    }
                    else
                    {
                        //{F132172F-59C0-40cc-ACCA-DA3362D53689}
                        if (this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].Tag != null)
                        {
                            this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].Tag.ToString();
                        }
                        this.cmbDoct.Tag = null;
                    }
                    this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[e.Row, 3].Tag.ToString();
                    this.IFCanAddItem();
                    this.RecipeSeqChanged();
                }
                
            }
        }

        /// <summary>
        /// ���������¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpRecipeSeq_CellClick(object sender, CellClickEventArgs e)
        {
            if (this.fpRecipeSeq_Sheet1.RowCount == 0)
            {
                return;
            }
            this.fpRecipeSeq_Sheet1.ActiveRowIndex = e.Row;
            if (e.Column != 0)
            {
                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = false;
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
                }
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 0].Value = true;
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 2].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[e.Row, 3].ForeColor = Color.Blue;


                this.feeDetailsSelected = new ArrayList();
                this.feeDetailsSelected = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[e.Row].Tag;
                if (this.feeDetailsSelected.Count > 0)
                {
                    this.SetRegInfoCanModify(((FeeItemList)feeDetailsSelected[0]), true);
                }
                else
                {
                    //{F132172F-59C0-40cc-ACCA-DA3362D53689}
                    if (this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].Tag != null)
                    {
                        this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[e.Row, 1].Tag.ToString();
                    }
                    this.cmbDoct.Tag = null;
                }
                this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[e.Row, 3].Tag.ToString();
                this.IFCanAddItem();
                this.RecipeSeqChanged();
                this.cmbRegDept.Focus();
            }
        }

        /// <summary>
        /// ����˵������ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem1_Click(object sender, EventArgs e)
        {
            this.AddNewRecipe();
        }

        /// <summary>
        /// ����˵���ɾ��ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem2_Click(object sender, EventArgs e)
        {
            if (this.fpRecipeSeq_Sheet1.RowCount == 0)
            {
                return;
            }
            int row = this.fpRecipeSeq_Sheet1.ActiveRowIndex;
            string deptName = this.fpRecipeSeq_Sheet1.Cells[row, 1].Text;
            string tempFlag = this.fpRecipeSeq_Sheet1.Cells[row, 2].Text;
            DialogResult result = MessageBox.Show("�Ƿ�ɾ��" + deptName + "��" + tempFlag + "������Ϣ?", "��ʾ!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                if (RecipeSeqDeleted((ArrayList)this.fpRecipeSeq_Sheet1.Rows[row].Tag) == -1)
                {
                    return;
                }
                this.fpRecipeSeq_Sheet1.Rows.Remove(row, 1);
            }
            if (this.fpRecipeSeq_Sheet1.RowCount == 1)//ֻ��һ�е�ʱ��Ĭ��ѡ��!
            {
                this.fpRecipeSeq_Sheet1.ActiveRowIndex = 0;

                this.fpRecipeSeq_Sheet1.Cells[0, 0].Value = true;
                this.fpRecipeSeq_Sheet1.Cells[0, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[0, 1].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[0, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[0, 2].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[0, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[0, 3].ForeColor = Color.Blue;

                this.feeDetailsSelected = new ArrayList();
                feeDetailsSelected = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[0].Tag;
                if (this.feeDetailsSelected.Count > 0)
                {
                    this.SetRegInfoCanModify(((FeeItemList)feeDetailsSelected[0]), true);
                }
                else
                {
                    this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[0, 1].Tag.ToString();
                    this.cmbDoct.Tag = null;
                }
                this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[0, 3].Tag.ToString();
                this.RecipeSeqChanged();
                this.cmbRegDept.Focus();

            }
            if (this.fpRecipeSeq_Sheet1.RowCount == 0)
            {
                this.AddNewRecipe();
            }
        }

        /// <summary>
        /// ���ư�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.AddCopyRecipe();
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem6_Click(object sender, EventArgs e)
        {
            this.AddCopyRecipe(2);
        }

        /// <summary>
        /// ������������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem9_Click(object sender, EventArgs e)
        {
            //ucInputTimes uc = new ucInputTimes();
            //if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc) == DialogResult.OK)
            //{
            //    int times = uc.Times-1;
            //    this.AddCopyRecipe(times);
            //}
        }

        /// <summary>
        /// �����Ƿ���Ը��Ļ�����Ϣ����,���Ʋ˵���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuContexMenu1_Popup(object sender, EventArgs e)
        {
            if (!this.isCanModifyChargeInfo)//�������޸�
            {
                this.menuItem2.Enabled = false;
            }
            else
            {
                this.menuItem2.Enabled = true; ;
            }
        }

        /// <summary>
        /// �����س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null || (this.patientInfo.ID != null && this.patientInfo.ID.Length <= 0))
                {
                    return;
                }
                if (this.tbName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show(Language.Msg("����������!"));
                    this.tbName.Focus();

                    return;
                }
                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.tbName.Text, 40))
                {
                    MessageBox.Show(Language.Msg("�����������!"));
                    this.tbName.SelectAll();
                    this.tbName.Focus();

                    return;
                }
                this.patientInfo.Name = this.tbName.Text;
                if (this.isCanModifyPatientInfo)
                {
                    NextFocus(this.tbName);
                }
                else
                {
                    this.cmbSex.Focus();
                }

                FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen ioutScreen = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen)) as FS.HISFC.BizProcess.Interface.FeeInterface.IOutScreen;
                if (ioutScreen == null)
                {
                    return;
                }
                else
                {
                    ioutScreen.ClearInfo();
                    ioutScreen.ShowInfo(this.patientInfo);
                }
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.tbCardNO.Focus();
                this.tbCardNO.SelectAll();
            }
            //Ԥ�����յĲ��س�
            if (this.patientInfo != null)
            {
                this.patientInfo.Name = this.tbName.Text;
            }
        }

        /// <summary>
        /// �Ա�س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSex_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null)
                {
                    return;
                }
                if (this.cmbSex.Text == "��")
                {
                    this.patientInfo.Sex.ID = "M";
                }
                else if (this.cmbSex.Text == "Ů")
                {
                    this.patientInfo.Sex.ID = "F";
                }
                else
                {
                    this.patientInfo.Sex.ID = "U";
                }

                if (this.isCanModifyPatientInfo)
                {
                    NextFocus(this.cmbSex);
                }
                else
                {
                    this.tbAge.Focus();
                }

            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.tbName.Focus();
                this.tbName.SelectAll();
            }
        }

        /// <summary>
        /// ����س��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.patientInfo == null)
                {
                    return;
                }
                int age = 0;
                int month = 0;
                int day = 0;
                string text = this.tbAge.Text.Trim();
                if (!FS.FrameWork.Public.String.IsNumeric(text))
                {
                    //�����ַ�����ȡ��������
                    this.GetAgeNumber(text, ref age, ref month, ref day);
                }
                else
                {
                    age = FS.FrameWork.Function.NConvert.ToInt32(text);
                }

                if (age < 0)
                {
                    MessageBox.Show(Language.Msg("���䲻��С��0"));
                    this.tbAge.Focus();
                    this.tbAge.SelectAll();

                    return;
                }
                if (age > 300)
                {
                    MessageBox.Show(Language.Msg("���䲻�ܴ���300"));
                    this.tbAge.Focus();
                    this.tbAge.SelectAll();

                    return;
                }
                try
                {
                    this.patientInfo.Birthday = this.GetAge(this.outpatientManager.GetDateTimeFromSysDateTime(), age, month, day);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg("�������벻�Ϸ�!") + ex.Message);
                    this.tbAge.Focus();
                    this.tbAge.SelectAll();

                    return;
                }
                if (this.isCanModifyPatientInfo)
                {
                    this.cmbDoct.Focus();
                }
                else
                {
                    NextFocus(this.tbAge);
                }

                //this.PriceRuleChanaged();
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.cmbSex.Focus();
                this.cmbSex.SelectAll();
            }
        }

        /// <summary>
        /// �뿪��������򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbName_Leave(object sender, EventArgs e)
        {
            InputLanguage.CurrentInputLanguage = InputLanguage.DefaultInputLanguage;
            //if (this.patientInfo == null || (this.patientInfo.ID != null && this.patientInfo.ID.Length <= 0))
            //{
            //    return;
            //}
            //if (this.tbName.Text == string.Empty)
            //{
            //    MessageBox.Show(Language.Msg("����������!"));
            //    this.tbName.Focus();

            //    return;
            //}
            //if (!FS.FrameWork.Public.String.ValidMaxLengh(this.tbName.Text, 40))
            //{
            //    MessageBox.Show(Language.Msg("�����������!"));
            //    this.tbName.SelectAll();
            //    this.tbName.Focus();

            //    return;
            //}
            if (this.patientInfo != null)
            {
                this.patientInfo.Name = this.tbName.Text;
            }
        }

        /// <summary>
        /// �Żݻس��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbRebate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp)
            {
                this.tbMCardNO.Focus();
                this.tbMCardNO.SelectAll();
            }
        }

        #endregion
        public void CustomMethod() { }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <returns></returns>
        public void SetReciptDept()
        {
            try
            {
                if (this.patientInfo != null)
                {
                    FS.HISFC.Models.Registration.Schema schema = this.regInterMgr.GetSchema(this.patientInfo.DoctorInfo.Templet.Doct.ID, this.patientInfo.DoctorInfo.SeeDate);
                    if (schema != null && schema.Templet.Dept.ID != "")
                    {
                        this.patientInfo.DoctorInfo.Templet.Dept = schema.Templet.Dept.Clone();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.isNeedFastShort)
            {
                if (!string.IsNullOrEmpty(keyString))
                {
                    try
                    {
                        string[] str = keyString.Split('|');
                        if (str.Length == 1)
                        {
                            firsKey = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), str[0]);
                        }
                        else if (str.Length == 2)
                        {
                            firsKey = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), str[0]);
                            secondKey = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), str[1]);
                        }

                        if ((firsKey.GetHashCode() + secondKey.GetHashCode()) == keyData.GetHashCode())
                        {
                            this.RChooseAllRecipes();
                        }
                    }
                    catch (Exception ex)
                    {

                    }

                }
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ȫѡ����
        /// </summary>
        private void RChooseAllRecipes()
        {
            if (this.fpRecipeSeq_Sheet1.RowCount <= 1)
            {
                this.fpRecipeSeq_Sheet1.Cells[0, 0].Value = true;

                return;
            }


            //for (int iRow = 0; iRow < this.fpRecipeSeq_Sheet1.Rows.Count; iRow++)
            //{
            //    if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpRecipeSeq_Sheet1.Cells[iRow, 0].Value) == true)
            //    {
            //        continue;
            //    }
            //    this.fpRecipeSeq_Sheet1.Cells[iRow, 0].Value = true;

            //    #region ��������
            //    //FarPoint.Win.Spread.SpreadView s = new SpreadView();
            //    //s.Sheets.Add(this.fpRecipeSeq_Sheet1);
            //    //EditorNotifyEventArgs e = new EditorNotifyEventArgs(s, this.fpRecipeSeq, iRow, 0);
            //    //this.fpRecipeSeq_ButtonClicked(null, e);
            //    #endregion

            //}

            this.fpRecipeSeq.StopCellEditing();
            ArrayList selectedItems = new ArrayList();
            this.feeDetailsSelected = new ArrayList();
            if (this.fpRecipeSeq_Sheet1.ActiveRowIndex < 0)
            {
                this.fpRecipeSeq_Sheet1.ActiveRowIndex = 0;
            }
            int selectedCount = 0;
            int selectedRow = 0;

            //string pactID = string.Empty;
            //string pactIDNow = string.Empty;
            //if (this.fpRecipeSeq_Sheet1.Cells[this.fpRecipeSeq_Sheet1.ActiveRowIndex, 2].Tag != null)
            //{
            //    pactID = this.fpRecipeSeq_Sheet1.Cells[this.fpRecipeSeq_Sheet1.ActiveRowIndex, 2].Tag.ToString();
            //}

            for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            {
                if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text.ToString() == "True")
                {
                    //if (this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag != null)
                    //{
                    //    pactIDNow = this.fpRecipeSeq_Sheet1.Cells[i, 2].Tag.ToString();
                    //}
                    //if (pactID.Equals(pactIDNow))
                    //{
                        this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = true;
                        selectedItems.Add(this.fpRecipeSeq_Sheet1.Rows[i].Tag);
                        selectedCount++;
                        selectedRow = i;
                    //}
                    //else
                    //{
                    //    this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = false;
                    //}
                }
                this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
            }

            //����ֻʣ��ѡ����һ��
            if (selectedCount == 1)
            {
                fpRecipeSeq_Sheet1.ActiveRowIndex = selectedRow;
                this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[selectedRow, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[selectedRow, 2].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[selectedRow, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[selectedRow, 3].ForeColor = Color.Blue;

                foreach (ArrayList al in selectedItems)
                {
                    foreach (FeeItemList f in al)
                    {
                        this.feeDetailsSelected.Add(f);
                    }
                }
                ArrayList alTemp = new ArrayList();
                alTemp = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[selectedRow].Tag;
                if (alTemp.Count > 0)
                {
                    SetRegInfoCanModify(((FeeItemList)alTemp[0]), true);
                }
                else
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].Tag != null)
                    {
                        this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[selectedRow, 1].Tag.ToString();
                    }
                    this.cmbDoct.Tag = null;
                }
                this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[selectedRow, 3].Tag.ToString();
                this.IFCanAddItem();
                this.RecipeSeqChanged();
            }
            else
            {
                this.fpRecipeSeq_Sheet1.Cells[0, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[0, 1].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[0, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[0, 2].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[0, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[0, 3].ForeColor = Color.Blue;

                foreach (ArrayList al in selectedItems)
                {
                    foreach (FeeItemList f in al)
                    {
                        this.feeDetailsSelected.Add(f);
                    }
                }
                ArrayList alTemp = new ArrayList();
                alTemp = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[0].Tag;
                if (alTemp.Count > 0)
                {
                    SetRegInfoCanModify(((FeeItemList)alTemp[0]), true);
                }
                else
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[0, 1].Tag != null)
                    {
                        this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[0, 1].Tag.ToString();
                    }
                    this.cmbDoct.Tag = null;
                }
                this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[0, 3].Tag.ToString();
                this.IFCanAddItem();
                this.RecipeSeqChanged();
            }
            
        }

        /// <summary>
        /// ��һ�Ŵ���
        /// </summary>
        private void PreviousRecipe()
        {
            if (this.patientInfo == null || this.patientInfo.ID == "")
            {
                return;
            }
            if (this.fpRecipeSeq_Sheet1.Rows.Count == 0)
            {
                return;
            }

            if (this.fpRecipeSeq_Sheet1.Rows.Count == 1)       //ֻ��һ�ŷ�
            {
                return;
            }
            else if (this.fpRecipeSeq_Sheet1.Rows.Count > 1)  //���ŷ�����
            {
                int currentRow = this.fpRecipeSeq_Sheet1.ActiveRow.Index;
                //��һ�Ŵ���
                if (currentRow == 0) 
                {
                    this.ChooseRecipe(currentRow, 2);
                    return;
                }
                else if (currentRow > 0 && currentRow <= (this.fpRecipeSeq_Sheet1.Rows.Count - 1))
                {
                    this.ChooseRecipe(currentRow - 1, 2);
                    return;
                }


            }
        }

        /// <summary>
        /// ��һ�Ŵ���
        /// </summary>
        private void NextRecipe()
        {
            if (this.patientInfo == null || this.patientInfo.ID == "")
            {
                return;
            }
            if (this.fpRecipeSeq_Sheet1.Rows.Count == 0)
            {
                return;
            }

            if (this.fpRecipeSeq_Sheet1.Rows.Count == 1)       //ֻ��һ�ŷ�
            {
                return;
            }
            else if (this.fpRecipeSeq_Sheet1.Rows.Count > 1)  //���ŷ�����
            {
                int currentRow = this.fpRecipeSeq_Sheet1.ActiveRow.Index;
                //���һ�ŷ�
                if (currentRow == this.fpRecipeSeq_Sheet1.Rows.Count -1)
                {
                    this.ChooseRecipe(currentRow, 2);
                    return;
                }
                else if (currentRow >= 0 && currentRow < (this.fpRecipeSeq_Sheet1.Rows.Count - 1))
                {
                    this.ChooseRecipe(currentRow + 1, 2);
                    return;
                }
            }
        }

        /// <summary>
        /// ѡ�񴦷�
        /// </summary>
        private void ChooseRecipe(int iRow, int iColumn)
        {
            if (this.fpRecipeSeq_Sheet1.RowCount == 0)
            {
                return;
            }
            this.fpRecipeSeq_Sheet1.ActiveRowIndex = iRow;

            if (iColumn != 0)
            {
                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    this.fpRecipeSeq_Sheet1.Cells[i, 0].Value = false;
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
                }
                this.fpRecipeSeq_Sheet1.Cells[iRow, 0].Value = true;
                this.fpRecipeSeq_Sheet1.Cells[iRow, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[iRow, 1].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[iRow, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[iRow, 2].ForeColor = Color.Blue;
                this.fpRecipeSeq_Sheet1.Cells[iRow, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Bold);
                this.fpRecipeSeq_Sheet1.Cells[iRow, 3].ForeColor = Color.Blue;


                this.feeDetailsSelected = new ArrayList();
                this.feeDetailsSelected = (ArrayList)this.fpRecipeSeq_Sheet1.Rows[iRow].Tag;
                if (this.feeDetailsSelected.Count > 0)
                {
                    this.SetRegInfoCanModify(((FeeItemList)feeDetailsSelected[0]), true);
                }
                else
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[iRow, 1].Tag != null)
                    {
                        this.cmbRegDept.Tag = this.fpRecipeSeq_Sheet1.Cells[iRow, 1].Tag.ToString();
                    }
                    this.cmbDoct.Tag = null;
                }
                this.recipeSequence = this.fpRecipeSeq_Sheet1.Cells[iRow, 3].Tag.ToString();
                this.IFCanAddItem();
                this.RecipeSeqChanged();
                this.cmbRegDept.Focus();
            }
        }

        /// <summary>
        /// ȫѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem6_Click_1(object sender, EventArgs e)
        {
            this.RChooseAllRecipes();
        }

        /// <summary>
        /// ��һ�Ŵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem8_Click(object sender, EventArgs e)
        {
            this.PreviousRecipe();
        }

        /// <summary>
        /// ��һ�Ŵ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuItem10_Click(object sender, EventArgs e)
        {
            this.NextRecipe();
        }



    }
}

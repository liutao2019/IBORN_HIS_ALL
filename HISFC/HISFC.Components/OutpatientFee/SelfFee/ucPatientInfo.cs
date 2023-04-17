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
namespace FS.HISFC.Components.OutpatientFee.SelfFee
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
                    this.tbAge.Text = (nowTime.Year - patientInfo.Birthday.Year).ToString();
                    this.cmbRegDept.Tag = patientInfo.DoctorInfo.Templet.Dept.ID;
                    this.cmbDoct.Tag = patientInfo.DoctorInfo.Templet.Doct.ID;

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

                    this.patientInfo.Pact = this.GetPactInfoByPactCode(this.cmbPact.Tag.ToString());

                    if (this.patientInfo.Pact.PayKind.ID == "02")
                    {
                        this.patientInfo.Pact.ID = this.cmbPact.Tag.ToString();
                        this.patientInfo.Pact.Name = this.cmbPact.Text;
                        this.SetRelations();
                        this.PactChanged();
                        this.PriceRuleChanaged();
                    }

                    this.tbMCardNO.Text = patientInfo.SSN;

                    if (this.patientInfo.Pact.IsNeedMCard)
                    {
                        System.Windows.Forms.KeyEventArgs e = new KeyEventArgs(System.Windows.Forms.Keys.Enter);

                        this.tbMCardNO_KeyDown(null, e);
                    }

                    if (this.patientInfo.Pact.PayKind.ID == "01")//�Է�
                    {
                        this.cmbClass.Enabled = false;
                        this.tbMCardNO.Enabled = false;
                        this.cmbRebate.Enabled = true;
                    }
                    else if (this.patientInfo.Pact.PayKind.ID == "02")//ҽ��
                    {
                        this.cmbClass.Enabled = false;
                        this.tbMCardNO.Enabled = true;
                        this.cmbRebate.Enabled = false;
                    }
                    else//����
                    {
                        this.cmbClass.Enabled = true;
                        this.cmbRebate.Enabled = false;
                        this.tbMCardNO.Enabled = true;
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
                
                //��ʼ�� �Һſ���
                this.regDeptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.C);
                if (this.regDeptList == null) 
                {
                    MessageBox.Show("��ʼ���Һſ��ҳ���!" + this.managerIntegrate.Err);

                    return -1;
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
				this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

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
            //int currRow = this.fpRecipeSeq_Sheet1.ActiveRowIndex;
            //int selectRows = 0;
            //int selectRow = 0;
            //for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            //{
            //    if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text == "True")
            //    {
            //        selectRows++;
            //    }
            //}
            //if (selectRows > 1)
            //{
            //    this.isCanAddItem = false;

            //    return;
            //}
            //if (selectRows == 0)
            //{
            //    this.isCanAddItem = false;

            //    return;
            //}
            //if (selectRows == 1)
            //{
            //    for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
            //    {
            //        if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text == "True")
            //        {
            //            selectRow = i;
            //        }
            //    }
            //}

            //if (selectRow != currRow)
            //{
            //    this.isCanAddItem = false;
            //    return;
            //}

            this.isCanAddItem = false;
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
            //if (isCanModify)
            //{
            //    this.cmbRegDept.Enabled = true;
            //    this.cmbDoct.Enabled = true;
            //    if (feeItemList != null)
            //    {
            //        this.cmbRegDept.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;
            //        this.cmbDoct.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID;
            //    }

            //    return;
            //}
            //else
            //{
            //    this.cmbRegDept.Enabled = false;
            //    this.cmbDoct.Enabled = false;
            //    this.cmbRegDept.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Dept.ID;
            //    this.cmbDoct.Tag = ((Register)feeItemList.Patient).DoctorInfo.Templet.Doct.ID;
            //}
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
            this.fpRecipeSeq_Sheet1.Cells[row, 2].Text = "�¿�";
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
            try
            {
                this.patientInfo.Age = this.tbAge.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show("�������벻�Ϸ�!" + ex.Message);
                return;
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
            this.tbAge.Text = (nowTime.Year - this.patientInfo.Birthday.Year).ToString();
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

            #region ��������ʱ����

            //if (this.patientInfo.PayKind.ID == "03" && this.patientInfo.McardID != null && this.patientInfo.McardID.Length > 0)
            //{
            //    bool isBlack = myInterface.ExistBlackList(this.patientInfo.Pact.ID, this.patientInfo.McardID);
            //    if (isBlack)
            //    {
            //        MessageBox.Show("�����Ѿ��ں�������,�������շ�!");
            //        this.tbMCardNo.Focus();
            //        return false;
            //    }
            //}

            #endregion

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
                this.lblAddUpCost.Text = addUpCost.ToString();
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
                    this.plAddUp.Visible = false;
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

            for (int i = 0; i < sortList.Count; i++)
            {
                ArrayList tempSameSeqs = sortList[i] as ArrayList;
                decimal cost = 0;
                foreach (FeeItemList f in tempSameSeqs)
                {
                    cost += f.FT.OwnCost + f.FT.PayCost + f.FT.PubCost;
                }
                FeeItemList tempFeeItemRowOne = ((FeeItemList)tempSameSeqs[0]).Clone();
                this.fpRecipeSeq_Sheet1.Cells[i, 1].Text = objHelperDept.GetName(
                    ((FS.HISFC.Models.Registration.Register)tempFeeItemRowOne.Patient).DoctorInfo.Templet.Dept.ID);
                this.fpRecipeSeq_Sheet1.Cells[i, 2].Text = tempFeeItemRowOne.Order.ID.Length > 0 ? "����" : "����";
                this.fpRecipeSeq_Sheet1.Cells[i, 3].Text = cost.ToString();
                this.fpRecipeSeq_Sheet1.Rows[i].Tag = tempSameSeqs;
                this.fpRecipeSeq_Sheet1.Cells[i, 3].Tag = tempFeeItemRowOne.RecipeSequence;
                if (i == 0)
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

        #endregion

        #endregion

        #region �¼�
        public void CustomMethod() { }
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
                //    this.tbAge.Text = "20";
                //    this.cmbPact.Tag = "1";
                  
                //    this.isRecordDirectFee = true;
                //    this.patientInfo.ID = clinicNO;
                //    this.patientInfo.Name = name;
                //    //this.patientInfo.Card.ID = autoCardNO;
                //    this.patientInfo.PID.CardNO = autoCardNO;
                //    this.patientInfo.Pact.PayKind.ID = "01";
                //    this.patientInfo.Pact.ID = "1";
                //    this.patientInfo.Birthday = DateTime.Now.AddYears(-20);
                //    DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();
                //    this.patientInfo.DoctorInfo.SeeDate = nowTime;
                //    this.fpRecipeSeq_Sheet1.RowCount = 0;
                //    this.AddNewRecipe();

                //    //#region ���Ӱ��������һ�����Ϣ
                //    //if (this.InputedCardAndEnter1 != null && name.Length > 0)
                //    //{
                //    //    InputedCardAndEnter1(name, this.patientInfo, this.tbCardNo.Location, this.tbCardNo.Height);
                //    //}
                //    //#endregion

                //    this.isRecordDirectFee = false;
                //}
                ////else if (cardNo.StartsWith("*"))//��Ժְ��
                ////{
                ////    //ȥ��'/'��û��߹���
                ////    string workNo = cardNo.Remove(0, 1);

                ////    if (workNo.Length <= 0)
                ////    {
                ////        return;
                ////    }
                ////    workNo = workNo.PadLeft(6, '0');
                ////    workNo = workNo.ToUpper();
                ////    this.isQuery = false;
                ////    //������￨��
                ////    string cardNoAuto = myOutPatient.GetAutoCardNo();
                ////    if (cardNoAuto == string.Empty)
                ////    {
                ////        MessageBox.Show("������￨�ų���!");
                ////        this.tbCardNo.Focus();
                ////        return;
                ////    }
                ////    cardNoAuto = Clinic.Function.Charge.GetControlValue(myCtrl, null, FS.Common.Clinic.Class.Const.NO_REG_CARD_RULES, "9") + cardNoAuto;

                ////    //��þ�����ˮ��
                ////    string clinicNo = myOutPatient.GetSequence("Registration.Register.ClinicID");
                ////    if (clinicNo == string.Empty)
                ////    {
                ////        MessageBox.Show("����������ų���!");
                ////        this.tbCardNo.Focus();
                ////        return;
                ////    }
                ////    this.patientInfo = new FS.HISFC.Models.Registration.Register(); //ʵ�����Һ���Ϣʵ��

                ////    this.patientInfo.ID = clinicNo;
                ////    this.patientInfo.CardNo = cardNoAuto;

                ////    FS.HISFC.Models.RADT.Person p = this.myPerson.GetPersonByID(workNo);

                ////    if (p == null)
                ////    {
                ////        MessageBox.Show("���Ա����Ϣ����" + this.myPerson.Err, "��ʾ");
                ////        return;
                ////    }

                ////    this.patientInfo.Name = p.Name;
                ////    this.patientInfo.SexID = p.Sex.ID.ToString();
                ////    this.patientInfo.Sex = p.Sex;
                ////    this.patientInfo.McardID = workNo;
                ////    if (p.BirthDay != System.DateTime.MinValue)
                ////    {
                ////        this.patientInfo.Birthday = p.BirthDay;
                ////    }
                ////    else
                ////    {
                ////        this.patientInfo.Birthday = DateTime.Now.AddYears(-20);
                ////    }
                ////    this.patientInfo.RegDoct.ID = "999";
                ////    this.patientInfo.RegDoct.Name = "�޹���";
                ////    string empRegDept = Clinic.Function.Charge.GetControlValue(myCtrl, null, FS.Common.Clinic.Class.Const.EMPLOYEE_SEE_DEPT, string.Empty);
                ////    this.patientInfo.RegDept.ID = empRegDept;
                ////    this.patientInfo = this.patientInfo;

                ////    this.AddNewRecipe();

                ////    this.cmbRegDept.Focus();
                ////}
                //else //�������뻼�����￨�����
                //{
                    bool isValid = false;

                    string tmpOrgCardNo = cardNO;
                    //��俨�ŵ�10λ����0
                    cardNO = this.FillCardNO(cardNO);
                    this.patientInfo = new FS.HISFC.Models.Registration.Register(); //ʵ�����Һ���Ϣʵ��
                    //������ʾ������Ϣ�ؼ�
                    isValid = InputedCardAndEnter(cardNO, tmpOrgCardNo, this.tbCardNO.Location, this.tbCardNO.Height);

                    if (isValid) //�����õĻ��߻�����Ϣ��Ч����ô��ת���㵽ѡ��ҽ��
                    {
                        if (this.isCanModifyPatientInfo)
                        {
                            if (this.patientInfo.DoctorInfo.Templet.Doct.ID != null && this.patientInfo.DoctorInfo.Templet.Doct.ID.Length > 0)
                            {
                                this.ChangeFocus();
                            }
                            else
                            {
                                this.cmbDoct.Focus();
                            }
                        }
                        else
                        {
                            this.NextFocus(this.tbCardNO);
                        }
                    }
                    else //�����Ч���ţ���ô�������뿨��
                    {
                        this.tbCardNO.SelectAll();
                        this.tbCardNO.Focus();
                    }
                //}
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
                    this.cmbPact.Focus();
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
            if (this.patientInfo == null)
            {
                return;
            }
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
                this.cmbRegDept.Tag = person.Dept.ID;
            }

            this.patientInfo.DoctorInfo.Templet.Doct.User01 = person.Dept.ID;

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
                    this.cmbDoct.Focus();
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
                this.cmbRebate.Enabled = true;
                this.tbMCardNO.Text = string.Empty;
            }
            else if (this.patientInfo.Pact.PayKind.ID == "02")//ҽ��
            {
                this.cmbClass.Enabled = false;
                this.tbMCardNO.Enabled = true;
                this.cmbRebate.Enabled = false;

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
                this.cmbRebate.Enabled = false;
                this.cmbRebate.SelectedIndexChanged -= new EventHandler(cmbRebate_SelectedIndexChanged);
                this.cmbRebate.Tag = string.Empty;
                this.cmbRebate.Text = string.Empty;
                this.cmbRebate.SelectedIndexChanged += new EventHandler(cmbRebate_SelectedIndexChanged);
            }

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

                for (int i = 0; i < this.fpRecipeSeq_Sheet1.RowCount; i++)
                {
                    if (this.fpRecipeSeq_Sheet1.Cells[i, 0].Text.ToString() == "True")
                    {
                        selectedItems.Add(this.fpRecipeSeq_Sheet1.Rows[i].Tag);
                    }
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 1].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 2].ForeColor = Color.Black;
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].Font = new Font("����", 9, System.Drawing.FontStyle.Regular);
                    this.fpRecipeSeq_Sheet1.Cells[i, 3].ForeColor = Color.Black;
                }
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
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                this.tbCardNO.Focus();
                this.tbCardNO.SelectAll();
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
                try
                {
                    age = FS.FrameWork.Function.NConvert.ToInt32(this.tbAge.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg("�������벻�Ϸ�!") + ex.Message);
                    this.tbAge.Focus();
                    this.tbAge.SelectAll();

                    return;
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
                this.patientInfo.Birthday = this.outpatientManager.GetDateTimeFromSysDateTime().AddYears(-age);

                if (this.isCanModifyPatientInfo)
                {
                    this.cmbRegDept.Focus();
                }
                else
                {
                    NextFocus(this.tbAge);
                }

                this.PriceRuleChanaged();
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
            //this.patientInfo.Name = this.tbName.Text;
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
    }
}

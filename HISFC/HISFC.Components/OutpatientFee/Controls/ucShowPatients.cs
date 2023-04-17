using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    /// <summary>
    /// ucShowPatients<br></br>
    /// [��������: ����Ŀ��Ŷ���һ������ѡ����UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-2-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucShowPatients : UserControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucShowPatients()
        {
            InitializeComponent();

            pnlBottom.Visible = false;
        }

        #region ����

        /// <summary>
        /// ���￨��
        /// </summary>
        private string cardNO = string.Empty;

        /// <summary>
        /// /���������ĹҺ���Ϣ��Ŀ
        /// </summary>
        private int personCount;
        
        /// <summary>
        /// ���Ʋ���������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper controlerHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �Һ���Ϣʵ��
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = new FS.HISFC.Models.Registration.Register();
        /// <summary>
        /// 
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// �Һ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Registration.Registration registerManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager examiIntegrate = new FS.HISFC.BizProcess.Integrate.PhysicalExamination.ExamiManager();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ��ѡ����������Ϣ
        /// </summary>
        public delegate void GetPatient(FS.HISFC.Models.Registration.Register register);

        /// <summary>
        /// ��ѡ����������Ϣ�󴥷�
        /// </summary>
        public event GetPatient SelectedPatient;

        /// <summary>
        /// ԭʼ����
        /// </summary>
        private string orgCardNO = string.Empty;

        /// <summary>
        /// ���뿨�Ż���������ʽ
        /// </summary>
        public string operType = "1";//1 ֱ�����뻼�߿��Ż򷽺� 2 ����/+����

        /// <summary>
        ///  ��������
        /// </summary>
        private string regName;

        /// <summary>
        /// �Һ���Ч����
        /// ������ʾֻ��ѯ���컼��
        /// </summary>
        private int validDays = 10000;

        /// <summary>
        /// �ҺŴ�������Ч����
        /// ������ʾֻ��ѯ���컼��
        /// </summary>
        private int recipeNOValidDays = 10000;

        /// <summary>
        /// �Ƿ��ùҺŴ����Ŵ��Ŀ��ż������߻�����Ϣ
        /// </summary>
        private bool isUseRecipeNOReplaceCardNO = false;
        /// <summary>
        /// �����շ����Ƿ������Һ�
        /// </summary>
        private bool isCanReRegister = false;

        #endregion

        #region ����

        /// <summary>
        /// �ҺŴ�������Ч����
        /// </summary>
        public int RecipeNOValidDays
        {
            get
            {
                return this.recipeNOValidDays;
            }
            set
            {
                this.recipeNOValidDays = value;
            }
        }

        /// <summary>
        /// �Ƿ��ùҺŴ����Ŵ��Ŀ��ż������߻�����Ϣ
        /// </summary>
        public bool IsUseRecipeNOReplaceCardNO
        {
            get
            {
                return this.isUseRecipeNOReplaceCardNO;
            }
            set
            {
                this.isUseRecipeNOReplaceCardNO = value;
            }
        }

        /// <summary>
        /// �Һ���Ч����
        /// </summary>
        public int ValidDays
        {
            get
            {
                return this.validDays;
            }
            set
            {
                this.validDays = value;
            }
        }

        /// <summary>
        /// ԭʼ����,����0
        /// </summary>
        public string OrgCardNO
        {
            set
            {
                this.orgCardNO = value;
            }
        }

        /// <summary>
        /// ���߹Һſ���
        /// </summary>
        public string CardNO
        {
            get
            {
                return this.cardNO;
            }
            set
            {
                try
                {
                    this.cardNO = value;
                    if (this.cardNO == string.Empty || this.cardNO == null)
                    {
                        return;
                    }
                    //����cardNO ��÷��������ĹҺ���Ϣ
                    FillPatientInfoByCardNO();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString() + this.Name.ToString());
                }
            }
        }

        /// <summary>
        /// ���������ĹҺ���Ϣ��Ŀ
        /// </summary>
        public int PersonCount
        {
            get
            {
                return this.personCount;
            }
        }
        /// <summary>
        /// �����շ����Ƿ������Һ�
        /// </summary>
        public bool IsCanReRegister
        {
            get { return this.isCanReRegister; }
            set 
            { 
                this.isCanReRegister = value;
                pnlBottom.Visible = this.isCanReRegister;
            }
        }

        /// <summary>
        /// ѡ���Ļ��ߵĹҺŻ�����Ϣ
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
            }
        }

        /// <summary>
        /// ���Ʋ���������
        /// </summary>
        public FS.FrameWork.Public.ObjectHelper ControlerHelper
        {
            set
            {
                this.controlerHelper = value;
            }
            get
            {
                return this.controlerHelper;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string RegName
        {
            get
            {
                return this.regName;
            }
            set
            {
                this.regName = value;
                if (this.regName != null && this.regName.Length > 0)
                {
                    //this.FillPatientInfoByName();
                }
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��÷��������Ļ�����Ϣ
        /// </summary>
        /// <param name="cardNO">���߹Һſ���</param>
        /// <returns>���������ĹҺ���Ϣ����</returns>
        private ArrayList QueryPatientInfosByCardNO(string cardNO)
        {
            ArrayList patients = null;

            this.validDays = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_REG_DAYS, false, 10000);

            if (this.validDays == 0)
            {
                this.validDays = 10000;//���û��ά������ôĬ�ϹҺ�һֱ��Ч;
            }

            //��õ�ǰϵͳʱ��
            DateTime dtQueryBeginTime = this.outpatientManager.GetDateTimeFromSysDateTime();

            //������ʾֻ��ѯ���컼��
            if (validDays < 0)
            {
                dtQueryBeginTime = dtQueryBeginTime.Date;
            }
            else
            {
                dtQueryBeginTime = dtQueryBeginTime.AddDays(-validDays);
            }


            //�����Ч�����ڵĹҺ���Ϣ
            patients = this.registerManager.QueryValidPatientsByCardNO(cardNO, dtQueryBeginTime);
            
            //���û�и��������ĹҺ���Ϣ.����һ����ArrayList
            if (patients == null)
            {
                patients = new ArrayList();
            }

            #region û�����������
           
            ////���Ǽ���Ϣ.
            ArrayList checkPatients = new ArrayList();

            //��������Ϣ 
            checkPatients = QueryCheckPatients(cardNO);

            //�������������Ϣ����ô���
            if (checkPatients != null)
            {
                patients.AddRange(checkPatients);
            }
            //else   //{5CFE4556-5B65-4c45-B9F4-3AE9A5681562}
            //{
            //    patients = null;
            //}

            #endregion

            //��ò���,�Ƿ���Ҫ������ҺŴ����ż���������Ϣ

            this.isUseRecipeNOReplaceCardNO = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.REG_RECIPE_NO_RELPACE_CARD_NO, false, false);

            //��Ҫ
            if (this.isUseRecipeNOReplaceCardNO)
            {
                try
                {
                    long orgCardNumber = System.Convert.ToInt64(this.orgCardNO);
                }
                catch(Exception e) 
                {
                    MessageBox.Show("ת�����ﴦ���ų���!���ǺϷ�����" + e.Message);

                    return null;
                }

                this.recipeNOValidDays = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.REG_RECIPE_NO_VALID_DAYS, false, 10000);

                //������ʾֻ��ѯ���컼��
                if (recipeNOValidDays < 0)
                {
                    dtQueryBeginTime = dtQueryBeginTime.Date;
                }
                else
                {
                    dtQueryBeginTime = dtQueryBeginTime.AddDays(-recipeNOValidDays);
                }

                ArrayList recipePatients = registerManager.QueryValidPatientsBySeeNO(this.orgCardNO, dtQueryBeginTime);
                if (recipePatients == null)
                {
                    MessageBox.Show("���ݴ����Ż�û�����Ϣ����!" + registerManager.Err);

                    return null;
                }
                else
                {
                    patients.AddRange(recipePatients);
                }
            }

            return patients;
        }

        #region û�����������

        /// <summary>
        /// �����컼�ߵĻ�����Ϣ��ת���ɹҺ�ʵ���ͬ���ߵĹҺ���Ϣ�ϲ�
        /// </summary>
        /// <param name="cardNO">���ߵ�����</param>
        /// <returns>����ת���ĹҺ���Ϣʵ��</returns>
        private ArrayList QueryCheckPatients(string cardNO)
        {
            ArrayList checkPatients = new ArrayList();

            checkPatients = this.examiIntegrate.QueryCollectivityRegisterByCardNO(cardNO);

            if (checkPatients == null)
            {
                return null;
            }
            ArrayList alChkToRegInfos = new ArrayList();

            foreach (FS.HISFC.Models.PhysicalExamination.Register chkRegister in checkPatients)
            {
                FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();

                register.ID = chkRegister.ChkClinicNo; //
                register.PID.CardNO = chkRegister.PID.CardNO;//���￨��
                register.Name = chkRegister.Name;//��������
                register.Sex.ID = chkRegister.Sex.ID;//�Ա�
                register.MaritalStatus = chkRegister.MaritalStatus;//����״��
                register.Country = chkRegister.Country;//����
                register.Height = chkRegister.Height;//���
                register.Weight = chkRegister.Weight;//����
                register.ChkKind = chkRegister.ChkKind;//1 ���� 2 ����
                register.CompanyName = chkRegister.Company.Name;//��λ
                register.SSN = chkRegister.SSN;//ҽ��֤��
                register.DoctorInfo.SeeDate = chkRegister.CheckTime;//�������
                register.IDCard = chkRegister.IDCard;//���֤��
                register.Birthday = chkRegister.Birthday;//����
                register.Profession = chkRegister.Profession;//ְҵ
                register.PhoneBusiness = chkRegister.PhoneBusiness;//��λ�绰
                register.BusinessZip = chkRegister.BusinessZip;//��λ��������
                register.AddressHome = chkRegister.AddressHome;//��ͥסַ
                register.PhoneHome = chkRegister.PhoneHome;//��ͥ�绰
                register.HomeZip = chkRegister.HomeZip;//��ͥ��������
                register.Nationality = chkRegister.Nationality;//����
                register.Pact.PayKind = chkRegister.Pact.PayKind;//�������
                register.DIST = chkRegister.DIST;//����
                register.Pact.ID = "1";//�Է�
                register.Pact.PayKind.ID = "01";
                register.DoctorInfo.Templet.Dept = chkRegister.Operator.Dept;

                alChkToRegInfos.Add(register);
            }

            return alChkToRegInfos;
        }

        #endregion

        /// <summary>
        /// �����������Ļ�����Ϣ��ʾ���б��У����ֻ��һ��������ʾ�ÿؼ���ֱ�ӻ��߻��ߵĹҺ�ʵ��
        /// </summary>
        private void FillPatientInfoByCardNO()
        {
            ArrayList patients = this.QueryPatientInfosByCardNO(this.cardNO);

            DisplayPatients(patients);
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="patients">��ѯ�����Ļ����б�</param>
        private void DisplayPatients(ArrayList patients)
        {
            if (patients == null || patients.Count <= 0)
            {
                this.personCount = 0;
                this.patientInfo = null;
                // ��û��߻�����Ϣ���� �� û���ҵ����������Ļ�����Ϣ
                if (isCanReRegister)
                {
                    this.patientInfo = GetRegInfoFromPatientInfo(this.cardNO);
                }

                this.SelectedPatient(this.patientInfo);

                return;
            }
            //�Ƿ����û�д����ĹҺţ�����Һų���2�Ҷ�û�����򲻹���
            bool isSetpatientInfo = this.controlParamIntegrate.GetControlParam<bool>("MZ9104", true, false);
            if (isSetpatientInfo)
            {
                patients = this.GetchargeItems(patients);
            }
            // ���ֻ�ҵ�һ�����������Ļ�����Ϣ���Ҳ������Һ�
            // ��ô����ʾ�ؼ���ֱ�ӷ��ػ��ߵĹҺ�ʵ��
            // 
            if (patients.Count == 1 && !isCanReRegister)
            {
                this.personCount = 1;
                this.patientInfo = patients[0] as FS.HISFC.Models.Registration.Register;
                this.SelectedPatient(this.patientInfo);

                return;
            }

            //����գ�����fp�ĸ�ʽ����������Ȼ��"��"�ı�־λ��׼;
            this.neuSpread1_Sheet1.Rows.Count = 0;

            //����ж�����������Ļ�����Ϣ���ڿؼ����б�����ʾ������Ϣ���Һ�ʵ���ڸ��е�tag������
            this.neuSpread1_Sheet1.RowCount = 1; //Ĭ��ֻ��һ��
            FS.HISFC.Models.Registration.Register patient = null;
            this.Show();
      
            this.personCount = patients.Count;

            this.neuSpread1_Sheet1.RowCount = personCount;
            int index = 0;
            for (int i = 0; i < personCount; i++)//{3EF17191-E618-42A9-A86E-6C63DE7AEE3C}������ʾδ�ɷѵģ���һ��ѭ��ֻ��ʾδ�ɷѵ�
            {
                patient = patients[i] as FS.HISFC.Models.Registration.Register;
                ArrayList chargeItems = new ArrayList();
                chargeItems = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(patient.ID);
                if (chargeItems != null && chargeItems.Count > 0)
                {
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = patient.OrderNO.ToString();
                    this.neuSpread1_Sheet1.Cells[index, 0 + 1].Text = patient.PID.CardNO;
                    this.neuSpread1_Sheet1.Cells[index, 1 + 1].Text = patient.Name;
                    this.neuSpread1_Sheet1.Cells[index, 2 + 1].Text = patient.DoctorInfo.Templet.Dept.Name;
                    this.neuSpread1_Sheet1.Cells[index, 3 + 1].Text = patient.DoctorInfo.Templet.RegLevel.Name;
                    this.neuSpread1_Sheet1.Cells[index, 4 + 1].Text = patient.DoctorInfo.SeeDate.ToString();
                    if (patient.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
                    {
                        this.neuSpread1_Sheet1.Cells[index, 5 + 1].Text = "��Ч";
                    }
                    else if (patient.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                    {
                        this.neuSpread1_Sheet1.Cells[index, 5 + 1].ForeColor = Color.Red;
                        this.neuSpread1_Sheet1.Cells[index, 5 + 1].Text = "����";
                    }
                    else if (patient.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                    {
                        this.neuSpread1_Sheet1.Cells[index, 5 + 1].ForeColor = Color.Red;
                        this.neuSpread1_Sheet1.Cells[index, 5 + 1].Text = "�˷�";
                    }
                    this.neuSpread1_Sheet1.Cells[index, 6 + 1].Text = patient.DoctorInfo.Templet.Doct.Name;

                    this.neuSpread1_Sheet1.Rows[index].Label = "��";
                    this.neuSpread1_Sheet1.RowHeader.Rows[index].BackColor = Color.Green;
                    this.neuSpread1_Sheet1.Rows[index].Tag = patient;


                }
                else continue;

                index++;
            }
            for (int i = 0; i < personCount; i++)//{3EF17191-E618-42A9-A86E-6C63DE7AEE3C}��ʾ�ɷѵ�
            {
                patient = patients[i] as FS.HISFC.Models.Registration.Register;

                ArrayList chargeItems = new ArrayList();
                chargeItems = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(patient.ID);
                if (chargeItems != null && chargeItems.Count > 0)
                {
                    continue;
                }

                this.neuSpread1_Sheet1.Cells[index, 0].Text = patient.OrderNO.ToString();
                this.neuSpread1_Sheet1.Cells[index, 0 + 1].Text = patient.PID.CardNO;
                this.neuSpread1_Sheet1.Cells[index, 1 + 1].Text = patient.Name;
                this.neuSpread1_Sheet1.Cells[index, 2 + 1].Text = patient.DoctorInfo.Templet.Dept.Name;
                this.neuSpread1_Sheet1.Cells[index, 3 + 1].Text = patient.DoctorInfo.Templet.RegLevel.Name;
                this.neuSpread1_Sheet1.Cells[index, 4 + 1].Text = patient.DoctorInfo.SeeDate.ToString();
                if (patient.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Valid)
                {
                    this.neuSpread1_Sheet1.Cells[index, 5 + 1].Text = "��Ч";
                }
                else if (patient.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Cancel)
                {
                    this.neuSpread1_Sheet1.Cells[index, 5 + 1].ForeColor = Color.Red;
                    this.neuSpread1_Sheet1.Cells[index, 5 + 1].Text = "����";
                }
                else if (patient.Status == FS.HISFC.Models.Base.EnumRegisterStatus.Back)
                {
                    this.neuSpread1_Sheet1.Cells[index, 5 + 1].ForeColor = Color.Red;
                    this.neuSpread1_Sheet1.Cells[index, 5 + 1].Text = "�˷�";
                }
                this.neuSpread1_Sheet1.Cells[index, 6 + 1].Text = patient.DoctorInfo.Templet.Doct.Name;

                //ArrayList chargeItems = new ArrayList();
                //chargeItems = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(patient.ID);
                //if (chargeItems != null && chargeItems.Count > 0)
                //{
                //    this.neuSpread1_Sheet1.Rows[index].Label = "��";
                //    this.neuSpread1_Sheet1.RowHeader.Rows[index].BackColor = Color.Green;
                //}
                this.neuSpread1_Sheet1.Rows[index].Tag = patient;

                index++;
            }
        }
        /// </summary>
        /// ����û�д����ĹҺż�¼��
        /// </summary>
        /// <param name="row">��ǰ��</param>
        /// <summary>
        private ArrayList GetchargeItems(ArrayList patients)
        {
            if (patients.Count == 1)
            {
                return patients;
            }
            ArrayList newpatients = new ArrayList();
            foreach (FS.HISFC.Models.Registration.Register patient in patients)
            {
                ArrayList chargeItems = new ArrayList();
                chargeItems = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(patient.ID);
                if (chargeItems != null && chargeItems.Count > 0)
                {
                    newpatients.Add(patient);
                }
            }
            //������еĹҺŶ�û�д���������ԭ��������
            if (newpatients.Count == 0)
            {
                return patients;
            }
            else
            {
                return newpatients;
            }
        }
        /// <summary>
        /// ˫�����س���ѡ����
        /// </summary>
        /// <param name="row">��ǰ��</param>
        private void SelectPatient(int row)
        {
            this.SelectedPatient((FS.HISFC.Models.Registration.Register)this.neuSpread1_Sheet1.Rows[row].Tag);
            this.FindForm().Close();
        }

        /// <summary>
        /// �����������Ļ�����Ϣ��ʾ���б��У����ֻ��һ��������ʾ�ÿؼ���ֱ�ӻ��߻��ߵĹҺ�ʵ��
        /// </summary>
        private void FillPatientInfoByName()
        {
            ArrayList patients = this.QueryPatientsByName(this.regName);

            this.DisplayPatients(patients);
        }

        /// <summary>
        /// ���ݻ���������ѯ����
        /// </summary>
        /// <param name="name">��������</param>
        /// <returns>�ɹ� ���ط��������Ļ��߻�����Ϣ ʧ�ܷ���null û�в��ҵ����ݷ���ArrayList.Count = 0</returns>
        private ArrayList QueryPatientsByName(string name)
        {
            if (this.validDays == 0)
            {
                this.validDays = 10000;//���û��ά������ôĬ�ϹҺ�һֱ��Ч;
            }

            ArrayList patients = this.registerManager.QueryValidPatientsByName(this.regName);

            //���û�и��������ĹҺ���Ϣ.����һ����ArrayList
            if (patients == null)
            {
                MessageBox.Show("���ݻ������������Ϣ����" + this.registerManager.Err);

                patients = new ArrayList();
            }

            return patients;
        }

        #endregion 

        /// <summary>
        /// ˫��FP�¼�,ѡ��ǰ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int row = e.Row;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                if (this.neuSpread1_Sheet1.Rows[row].Tag != null)
                {
                    this.SelectPatient(row);
                }
            }
        }

        /// <summary>
        /// FP�س��¼� ,ѡ��ǰ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    if (this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag != null)
                    {
                        //����ǿ���ѡ��ʽ,��ôѡ�е�ǰ��
                        if (this.operType == "1")
                        {
                            this.SelectPatient(this.neuSpread1_Sheet1.ActiveRowIndex);
                        }
                        else//���������ѡ��ʽ,���ѡ�����������1 ��ôѡ��ǰ��
                        {
                            if (this.neuSpread1_Sheet1.SelectionCount >= 1)
                            {
                                this.SelectPatient(this.neuSpread1_Sheet1.ActiveRowIndex);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (this.operType == "1")
                {
                    this.FindForm().Close();
                    this.SelectedPatient(null);
                }
                else
                {
                    this.FindForm().Close();
                    this.SelectedPatient(this.patientInfo);
                }
            }
            else if (keyData == Keys.Enter)
            {
                if (this.operType == "2")
                {
                    this.FindForm().Close();
                    SelectedPatient(this.patientInfo);
                }
            }
            
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// ���ؼ���ý����ʱ��,��FP��ý���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucShowPatients_Enter(object sender, EventArgs e)
        {
            this.neuSpread1.Focus();
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            string strCardNo = this.cardNO;
            if (string.IsNullOrEmpty(strCardNo))
            {
                if (this.neuSpread1_Sheet1.Rows.Count > 0)
                {
                    if (this.neuSpread1_Sheet1.Rows[0].Tag != null)
                    {
                        strCardNo = ((FS.HISFC.Models.Registration.Register)this.neuSpread1_Sheet1.Rows[0].Tag).PID.CardNO;
                    }
                }

                if (string.IsNullOrEmpty(strCardNo))
                {
                    MessageBox.Show("��ȡ������Ϣʧ�ܣ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            this.patientInfo = GetRegInfoFromPatientInfo(strCardNo);

            this.FindForm().Close();
            SelectedPatient(this.patientInfo);
        }

        /// <summary>
        /// ���ݻ�����Ϣ��ȡ�Һ���Ϣ
        /// </summary>
        /// <param name="cardNO">���߿���</param>
        /// <returns>�Һ�ʵ��</returns>
        private FS.HISFC.Models.Registration.Register GetRegInfoFromPatientInfo(string cardNO)
        {
            if (string.IsNullOrEmpty(cardNO))
            {
                return null;
            }

            #region ��ȡ���߻�����Ϣ

            FS.HISFC.Models.RADT.PatientInfo patInfo = manager.QueryComPatientInfo(cardNO);
            if (patInfo == null)
            {
                MessageBox.Show(manager.Err);
                return null;
            }

            #endregion

            FS.HISFC.Models.Registration.Register regObj = new FS.HISFC.Models.Registration.Register();

            //���Һű��
            regObj.User01 = "1";


            FS.HISFC.Models.Base.Employee oper = this.manager.GetEmployeeInfo(this.outpatientManager.Operator.ID);
            try
            {
                //ϵͳ���ҺŻ��ߣ���ˮ��Ϊ�º�
                //����regObj.IsFee�ж��Ƿ��ǲ��Һ�
                regObj.ID = this.outpatientManager.GetSequence("Registration.Register.ClinicID");
                regObj.TranType = FS.HISFC.Models.Base.TransTypes.Positive;//������
                regObj.PID = patInfo.PID;

                //����ʱ����ж��Ƿ���
                //regObj.DoctorInfo.Templet.RegLevel.IsEmergency = (this.cmbRegLevel.SelectedItem as FS.HISFC.Models.Registration.RegLevel).IsEmergency;

                //regObj.DoctorInfo.Templet.Dept.ID = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.ID;
                //regObj.DoctorInfo.Templet.Dept.Name = ((FS.HISFC.Models.Base.Employee)this.contrlManager.Operator).Dept.Name;
                //regObj.DoctorInfo.Templet.Doct.ID = this.contrlManager.Operator.ID;
                //regObj.DoctorInfo.Templet.Doct.Name = this.contrlManager.Operator.Name;

                regObj.Name = patInfo.Name;//��������
                regObj.Sex = patInfo.Sex;//�Ա�
                regObj.Birthday = patInfo.Birthday;//��������			

                regObj.RegType = FS.HISFC.Models.Base.EnumRegType.Reg;
                regObj.InputOper.ID = oper.ID;

                DateTime nowTime = this.outpatientManager.GetDateTimeFromSysDateTime();

                regObj.InputOper.OperTime = nowTime;
                regObj.DoctorInfo.SeeDate = nowTime;
                regObj.DoctorInfo.Templet.Begin = nowTime;
                regObj.DoctorInfo.Templet.End = nowTime;

                #region ���
                if (regObj.DoctorInfo.SeeDate.Hour < 12 && regObj.DoctorInfo.SeeDate.Hour > 6)
                {
                    //����
                    regObj.DoctorInfo.Templet.Noon.ID = "1";
                }
                else if (regObj.DoctorInfo.SeeDate.Hour > 12 && regObj.DoctorInfo.SeeDate.Hour < 18)
                {
                    //����
                    regObj.DoctorInfo.Templet.Noon.ID = "2";
                }
                else
                {
                    //����
                    regObj.DoctorInfo.Templet.Noon.ID = "3";
                }
                #endregion

                //����ר�ҿ��޶� �Ȳ�����


                //��ͬ��λ���ݰ쿨��¼��ȡ���������ȡ����
                regObj.Pact = patInfo.Pact;
                if (string.IsNullOrEmpty(regObj.Pact.ID))
                {
                    regObj.Pact.ID = "1";
                    regObj.Pact.Name = "��ͨ";
                    regObj.Pact.PayKind.ID = "01";
                    regObj.Pact.PayKind.Name = "�Է�";
                }

                #region �Һż���

                //�Ƿ���
                //bool isEmerg = this.regInterMgr.IsEmergency(this.GetReciptDept().ID);

                //string regLevl = "";
                //string diagItemCode = "";
                //if (isEmerg)
                //{
                //    ArrayList emergRegLevlList = this.conManager.GetList("EmergencyLevel");
                //    if (emergRegLevlList == null || emergRegLevlList.Count == 0)
                //    {
                //        MessageBox.Show("��ȡ�����ʧ�ܣ�" + conManager.Err);
                //        return null;
                //    }
                //    else if (emergRegLevlList.Count > 0)
                //    {
                //        regLevl = ((FS.FrameWork.Models.NeuObject)emergRegLevlList[0]).ID.Trim();
                //    }
                //    if (string.IsNullOrEmpty(regLevl))
                //    {
                //        MessageBox.Show("��ȡ����Ŵ�������ϵ��Ϣ�ƣ�");
                //        return null;
                //    }
                //}
                //else
                //{
                //    if (this.regInterMgr.GetSupplyRegInfo(oper.Level.ID.ToString(), ref regLevl, ref diagItemCode) == -1)
                //    {
                //        MessageBox.Show(regInterMgr.Err);
                //        return null;
                //    }
                //}

                //FS.HISFC.Models.Registration.RegLevel regLevlObj = this.regInterMgr.QueryRegLevelByCode(regLevl);
                //if (regLevlObj == null)
                //{
                //    MessageBox.Show("��ѯ�Һż�����󣬱���[" + regLevl + "]������ϵ��Ϣ������ά��" + regInterMgr.Err);
                //    return null;
                //}

                //regObj.DoctorInfo.Templet.RegLevel = regLevlObj;
                #endregion

                regObj.SSN = patInfo.SSN;//ҽ��֤��

                regObj.PhoneHome = patInfo.PhoneHome;//��ϵ�绰
                regObj.AddressHome = patInfo.AddressHome;//��ϵ��ַ
                regObj.CardType = patInfo.IDCardType; //֤������

                regObj.IsFee = false;
                regObj.Status = FS.HISFC.Models.Base.EnumRegisterStatus.Valid;
                //֮ǰΪʲô��Ϊtrue�أ���
                regObj.IsSee = false;
                regObj.CancelOper.ID = "";
                regObj.CancelOper.OperTime = DateTime.MinValue;
                regObj.IDCard = patInfo.IDCard;

                regObj.PVisit.InState.ID = "N";

                //���ܴ���
                if (patInfo.IsEncrypt)
                {
                    regObj.IsEncrypt = true;
                    regObj.NormalName = FS.FrameWork.WinForms.Classes.Function.Encrypt3DES(patInfo.Name);
                    regObj.Name = "******";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return regObj;
        }
    }
}

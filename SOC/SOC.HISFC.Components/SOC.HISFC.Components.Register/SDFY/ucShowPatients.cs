using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.Register.SDFY
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
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee outpatientManager = new FS.HISFC.BizProcess.Integrate.Fee();

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
        /// �Һ�ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();


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
        /// </summary>
        private int validDays = 10000;

        /// <summary>
        /// �ҺŴ�������Ч����
        /// </summary>
        private int recipeNOValidDays = 10000;

        /// <summary>
        /// �Ƿ��ùҺŴ����Ŵ��Ŀ��ż������߻�����Ϣ
        /// </summary>
        private bool isUseRecipeNOReplaceCardNO = false;

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

            //this.validDays = this.controlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.VALID_REG_DAYS, false, 10000);

            //if (this.validDays == 0)
            //{
            //    this.validDays = 10000;//���û��ά������ôĬ�ϹҺ�һֱ��Ч;
            //}

            //��õ�ǰϵͳʱ��
            DateTime nowTime = this.regManager.GetDateTimeFromSysDateTime().Date;

            //�����Ч�����ڵĹҺ���Ϣ
            patients = this.registerManager.QueryValidPatientsByCardNO(cardNO, nowTime);
            
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
            //if (checkPatients != null)
            //{
            //    patients.AddRange(checkPatients);
            //}
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

                ArrayList recipePatients = registerManager.QueryValidPatientsBySeeNO(this.orgCardNO, nowTime.AddDays(-recipeNOValidDays));
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
            //��û��߻�����Ϣ����
            if (patients == null)
            {
                this.personCount = 0;
                this.patientInfo = null;
                //this.SelectedPatient(null);

                return;
            }
            //û���ҵ����������Ļ�����Ϣ
            if (patients.Count == 0)
            {
                this.personCount = 0;
                this.patientInfo = null;
                //this.SelectedPatient(null);

                return;
            }
            //���ֻ�ҵ�һ�����������Ļ�����Ϣ����ô����ʾ�ؼ���ֱ�ӷ��ػ��ߵĹҺ�ʵ��
            //if (patients.Count == 1)
            //{
            //    this.personCount = 1;
            //    this.patientInfo = patients[0] as FS.HISFC.Models.Registration.Register;
            //    //this.SelectedPatient(this.patientInfo);

            //    return;
            //}
            //����ж�����������Ļ�����Ϣ���ڿؼ����б�����ʾ������Ϣ���Һ�ʵ���ڸ��е�tag������
            this.neuSpread1_Sheet1.RowCount = 1; //Ĭ��ֻ��һ��
            FS.HISFC.Models.Registration.Register patient = null;
            this.Show();
      
            this.personCount = patients.Count;

            this.neuSpread1_Sheet1.RowCount = personCount;
            int index = 0;
            for (int i = personCount - 1; i >= 0; i--)
            {
                patient = patients[i] as FS.HISFC.Models.Registration.Register;

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
                ArrayList chargeItems = new ArrayList();
                chargeItems = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(patient.ID);
                if (chargeItems != null && chargeItems.Count > 0)
                {
                    this.neuSpread1_Sheet1.Rows[index].Label = "��";
                    this.neuSpread1_Sheet1.RowHeader.Rows[index].BackColor = Color.Green;
                }
                this.neuSpread1_Sheet1.Rows[index].Tag = patient;

                index++;
            }
        }

        /// <summary>
        /// ˫�����س���ѡ����
        /// </summary>
        /// <param name="row">��ǰ��</param>
        private void SelectPatient(int row)
        {
            //this.SelectedPatient((FS.HISFC.Models.Registration.Register)this.neuSpread1_Sheet1.Rows[row].Tag);
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
                    //this.SelectedPatient(null);
                }
                else
                {
                    this.FindForm().Close();
                    //this.SelectedPatient(this.patientInfo);
                }
            }
            else if (keyData == Keys.Enter)
            {
                if (this.operType == "2")
                {
                    this.FindForm().Close();
                    //SelectedPatient(this.patientInfo);
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.FrameWork.WinForms.Classes;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.BizLogic.Fee;
using Neusoft.SOC.HISFC.BizProcess.CommonInterface;

namespace Neusoft.SOC.Local.Order.ZhuHai.ZDLY.PrePayIn
{
    public partial class ucPrepayIn : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucPrepayIn()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo;

        /// <summary>
        /// adt�ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        /// <summary>
        /// �������תתҵ���
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.RADT radtIntegrate = new Neusoft.HISFC.BizProcess.Integrate.RADT();


        #region ��ѯ����
        DataTable dtPrepayIn = new DataTable();
        DataView dvPrepayIn;
        #endregion
        //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
        protected Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return myPatientInfo;
            }
            set
            {
                if (value == null)
                    myPatientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
                else
                    myPatientInfo = value;
            }
        }

        #endregion

        #region ҵ������

        /// <summary>
        /// Managerҵ���
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();




        private Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();


        Neusoft.HISFC.BizLogic.Fee.InPatient inpatient = new InPatient();
        /// <summary>
        /// ��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfo = new PactUnitInfo();

        Neusoft.FrameWork.Public.ObjectHelper myObjHelper = new Neusoft.FrameWork.Public.ObjectHelper();//��ͬ��λ
        Neusoft.FrameWork.Public.ObjectHelper operObjHelper = new Neusoft.FrameWork.Public.ObjectHelper();//��Ա��������

        Neusoft.HISFC.Models.RADT.MaritalStatusEnumService maritalService = new Neusoft.HISFC.Models.RADT.MaritalStatusEnumService();



        /// <summary>
        /// ��λ������
        /// </summary>
        Neusoft.HISFC.BizLogic.Manager.Bed managerBed = new Neusoft.HISFC.BizLogic.Manager.Bed();

        /// <summary>
        ///���ҵ���
        /// </summary>
        Neusoft.HISFC.Models.HealthRecord.ICD MyIcd = null;

        /// <summary>
        ///������
        /// </summary>
        private CommonController commonController = CommonController.CreateInstance();

        #endregion

        #region ��ʼ��

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);

            toolBarService.AddToolButton("ȡ��ԤԼ", "ȡ��ԤԼ", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
            toolBarService.AddToolButton("��Ժ֪ͨ��", "��ӡ��Ժ֪ͨ��", (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.D��ӡִ�е�, true, false, null);

            return this.toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Clear();
                    break;
                case "ȡ��ԤԼ":
                        this.CancelPre();
                        break;
                //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
                case "��Ժ֪ͨ��":
                        this.PrintNotice();
                        break;
                default:
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();

            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();

            return base.OnPrint(sender, neuObject);
        }


        public void initEvents()
        {
            this.ucQueryInpatientNo2.myEvent += new Neusoft.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo2_myEvent);
            this.dtpBirthDay.TextChanged += new EventHandler(dtpBirthDay_TextChanged);
            this.txtAgeOne.TextChanged += new EventHandler(txtAge_TextChanged);
            this.txtAgeTwo.TextChanged += new EventHandler(txtAge_TextChanged);
            this.txtAgeThree.TextChanged += new EventHandler(txtAge_TextChanged);
        }



        /// <summary>
        /// ��ʼ���ؼ�,����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        protected virtual int Init()
        {

            Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ�����ڣ����Ժ�^^");
            Application.DoEvents();

            try
            {
                //��ʼ��סԺ�����б�
                this.cmbDept.AddItems(this.managerIntegrate.GetDepartment(EnumDepartmentType.I));   //this.myDept.GetInHosDepartment());
                //��ʿվ
                this.cmbNurseCell.AddItems(this.managerIntegrate.GetDepartment(EnumDepartmentType.N)); //this.myDept.GetDeptment(EnumDepartmentType.N));

                //����״��
                //this.cmbMarriage.AddItems(Neusoft.HISFC.Models.RADT.MaritalStatusEnumService.List());

                //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPactUnit.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                //this.cmbPactUnit.AddItems(this.pactUnitInfo.QueryPactUnitInPatient());

                //ְҵ��Ϣ
                this.cmbPos.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PROFESSION));

                //����
                this.cmbCountry.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.COUNTRY));

                //��������Ϣ
                this.cmbHomePlace.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.DIST));

                //��ϵ�˵�ַ��Ϣ
                //this.cmbLinkManAddr.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.AREA));

                //��ͥסַ��Ϣ
                //this.txtHomeAddr.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.AREA));

                //ԤԼҽ��
               // this.cmbPreDoc.AddItems(this.managerIntegrate.QueryEmployee(EnumEmployeeType.D));
                 

                //��ϵ�˹�ϵ
                //this.cmbLinkManRel.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //����
                //this.cmbNationality.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.NATION));

                //�Ա�			
                this.cmbSex.AddItems(Neusoft.HISFC.Models.Base.SexEnumService.List());

                //����
                //this.dtBirthday.Value = this.inpatient.GetDateTimeFromSysDateTime(); //this.myInpatient.GetDateTimeFromSysDateTime();
                //����Ա
                //this.txtOper.Text =this.managerIntegrate. this.myInpatient.Operator.Name;

                //ԤԼʱ��
                this.dtPreDate.Value = this.inpatient.GetDateTimeFromSysDateTime();

                //�������
                this.cmbPayKind.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PAYKIND));
                this.cmbPayKind.SelectedIndex = 0;
                this.dtBegin.Value = this.inpatient.GetDateTimeFromSysDateTime().AddDays(-1);
                this.dtEnd.Value = this.inpatient.GetDateTimeFromSysDateTime().AddDays(1);
                //this.cmbPreDoc.Tag = this.inpatient.Operator.ID;

                //���
                this.ucDiagnose1.Init();
                this.ucDiagnose1.SelectItem += new Neusoft.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
                //this.ActiveControl = this.txtCardNo;
                //this.lblDocName.Text = Neusoft.FrameWork.Management.Connection.Operator.Name;
                //this.lblDocID.Text = Neusoft.FrameWork.Management.Connection.Operator.ID;
               
            }
            catch (Exception e)
            {
                Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(e.Message);

                return -1;
            }

            //this.RefreshPatientLists();
            this.InitInterface();
            Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }
        #endregion

        #region  ����

        public void Close()
        {
            this.FindForm().Close();
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Clear()
        {
            foreach (Control c in this.tabPage1.Controls)
            {
                if (c.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (Control ctr in c.Controls)
                    {
                        if (ctr.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuTextBox)
                            || ctr.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            if (ctr.Name != "txtOper" || ctr.Name != "cmbPreDoc")
                            {
                                ctr.Text = "";
                                ctr.Tag = "";
                            }
                        }

                    }
                }
            }
            this.dtPreDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
            //this.dtBirthday.Value = this.inpatient.GetDateTimeFromSysDateTime();
            //this.txtCardNo.Focus();
            this.ucQueryInpatientNo2.Text = "";
            this.dtpBirthDay.Text = "";
        }

        /// <summary>
        ///  ��֤�����������Ϊ����Ժ״̬�Ƿ���true
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool validCard(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            bool bRet = true;
            try
            {
                if (obj.PID.CardNO != null)//������Ų�Ϊ��
                {
                    //�������Ϊ�Ǽ�״̬����Ժ״̬���ؼ�
                    if (obj.PVisit.InState.ID.ToString() == "R" || obj.PVisit.InState.ID.ToString() == "I")
                    {
                        bRet = false;
                    }
                }
            }
            catch { }
            return bRet;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="CardNo"></param>
        private void searchCard(string CardNo)
        {
            ArrayList al = new ArrayList();
            //neusoft.HISFC.Models.RADT.PatientInfo objPreIn = new neusoft.HISFC.Models.RADT.PatientInfo();
            Neusoft.HISFC.Models.RADT.PatientInfo objPreIn = new Neusoft.HISFC.Models.RADT.PatientInfo();
            //���ػ�����ϢסԺ����
            al = this.managerIntegrate.QueryPatientInfoByCardNO(CardNo);// this.myInpatient.GetPatientInfoByCardNO(CardNo);
            if (al.Count > 0)
            {
                objPreIn = (Neusoft.HISFC.Models.RADT.PatientInfo)al[0];
                if (this.validCard(objPreIn))//�ж��Ƿ�Ϊ����Ժ״̬
                {
                    //��û�����Ϣ����UI
                    this.getPrePatient(objPreIn);
                }
                else
                {
                    MessageBox.Show("�û��ߴ�����Ժ״̬!");
                }
            }
            else
            {
                objPreIn = this.managerIntegrate.QueryComPatientInfo(CardNo);//������Ϣ��
                if (this.validCard(objPreIn))//�ж��Ƿ�Ϊ����Ժ״̬
                {
                    //��û�����Ϣ����UI
                    this.getPrePatient(objPreIn);
                }
                else
                {
                    MessageBox.Show("�û��ߴ�����Ժ״̬!");
                }
            }
        }

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        private void getPrePatient(Neusoft.HISFC.Models.RADT.PatientInfo obj)
        {
            try
            {
                if (obj.PID.Name != null)
                {
                    this.ucQueryInpatientNo2.Text = obj.Name;//����                     
                    this.cmbDept.Tag = obj.PVisit.PatientLocation.Dept.ID;//סԺ����
                    this.cmbDept.Text = obj.PVisit.PatientLocation.Dept.Name;
                    //this.dtPreDate.Value = obj.PVisit.InTime;//ԤԼ����----------

                    this.txtCardNo2.Text = obj.PID.CardNO;
                    this.cmbSex.Tag = obj.Sex.ID;//�Ա�
                    //this.cmbPactUnit.Tag = obj.Pact.ID;//��ͬ��λ
                    this.cmbPayKind.Tag = obj.Pact.PayKind.ID;//�������

                    //this.dtBirthday.Value = obj.Birthday;//��������
                    this.dtpBirthDay.Text = obj.Birthday.ToString();//��������
                   // this.cmbMarriage.Tag = obj.MaritalStatus.ID;//����״��
                    this.txtIdentity.Text = obj.IDCard;//����֤��
                    this.cmbPos.Tag = obj.Profession.ID;//ְҵ
                    //this.cmbHomePlace.Tag = obj.DIST;//������
                    //this.cmbCountry.Tag = obj.Country.ID;//����

                    this.txtHomeAddrd.Text = obj.AddressHome;//��ͥסַ
                    //this.txtHomeTel.Text = obj.PhoneHome;//��ͥ�绰
                    this.txtWorkUnit.Text = obj.CompanyName;//������λ
                    this.txtLinkMan.Text = obj.Kin.Name;//��ϵ��
                    //this.txtLinkManAddr.Text = obj.Kin.RelationAddress;//��ϵ��סַ
                    this.txtLinkTel.Text = obj.Kin.RelationPhone;//��ϵ�˵绰
                    //this.txtWorkTel.Text = obj.PhoneBusiness;//������λ�绰
                    //this.cmbNationality.Tag = obj.Nationality.ID;//����
                    //this.cmbLinkManRel.Tag = obj.Kin.Relation.ID;//��ϵ�˹�ϵ
                    //this.cmbBedNo.Tag = obj.PVisit.PatientLocation.Bed.ID;//������
                   // this.cmbPreDoc.Tag = obj.PVisit.AdmittingDoctor.ID;//ԤԼҽ��
                    if (obj.Diagnoses.Count > 0)
                        this.txtInDiagnose.Tag = (obj.Diagnoses[0] as Neusoft.FrameWork.Models.NeuObject).ID;//������ϱ���

                    this.txtInDiagnose.Text = obj.ClinicDiagnose;//�����������

                    //this.txtSSN.Text = obj.SSN;//ҽ��֤��
                    //{E9EC275C-F044-40f1-BDDA-0F17410983EB}
                    this.cmbNurseCell.Tag = obj.PVisit.PatientLocation.NurseCell.ID;
                    //this.lblDocName.Text = Neusoft.FrameWork.Management.Connection.Operator.Name;
                    this.lblDocID.Text = Neusoft.FrameWork.Management.Connection.Operator.ID;

                    //if (obj.ExtendFlag1 == "0") 
                    //{
                    //    this.checkBox1.Checked = true;
                    //}
                    //else if (obj.ExtendFlag1 == "1")
                    //{
                    //    this.checkBox2.Checked = true;
                    //}
                    //else if (obj.ExtendFlag1 == "2")
                    //{
                    //    this.checkBox3.Checked = true;
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.Save();
        }

        /// <summary>
        /// ����ԤԼ���ߵǼ���Ϣ
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            try
            {
                //��Ч���ж�
                if (!this.checkValid()) return -1;
                //�õ�PatientInfoʵ��
                this.setPrePatient();
                //������
                //Neusoft.FrameWork.Management.Transaction t = new Neusoft.FrameWork.Management.Transaction(inpatient.Connection);// myInpatient.Connection);
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                this.managerIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
                if (this.managerIntegrate.InsertPreInPatient(this.PatientInfo) < 1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();

                    if (this.managerIntegrate.DBErrCode == 1)
                    {
                        MessageBox.Show("������ԤԼ�Ǽ�!");
                        //this.txtCardNo.Focus();
                        //this.txtCardNo.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("ԤԼ�Ǽ�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return -1;
                }

                #region addby xuewj 2010-3-15

                //if (this.cmbPreDoc.SelectedIndex >= 0)
                //{
                //    this.myPatientInfo.PVisit.ReferringDoctor.ID = this.cmbPreDoc.Tag.ToString();
                //    this.myPatientInfo.PVisit.ReferringDoctor.Name = this.cmbPreDoc.Text;

                //    this.myPatientInfo.PVisit.AdmittingDoctor.ID = string.Empty;
                //}

                if (this.adt == null)
                {
                    this.adt = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IHE.IADT)) as Neusoft.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this.adt != null)
                {
                    this.adt.PreRegInpatient(myPatientInfo);
                }
                #endregion

                //�ύ
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ԤԼ�Ǽǳɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Clear();
                this.neuTabControl1.SelectedIndex = 1;
                this.QueryData();

                DialogResult dr = MessageBox.Show("�Ƿ��ӡ��Ժ֪ͨ����", "��ʾ", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    if (this.iPrintInHosNotice != null)
                    {
                        this.iPrintInHosNotice.SetValue(this.PatientInfo);
                        this.iPrintInHosNotice.Print();
                    }
                }

                return 0;
            }
            catch
            {
                MessageBox.Show("ԤԼ�Ǽ�ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        /// <summary>
        /// �ж�����Ϸ���
        /// </summary>
        /// <returns></returns>
        private bool checkValid()
        {
            bool bRet = true;
            //�ж���Ϻ�
            //if (this.txtCardNo.Text == null || this.txtCardNo.Text.Trim() == "")
            //{
            //    MessageBox.Show("�����벡����!", "��ʾ");
            //    return false;
            //}

            //�ж�����
            if (this.ucQueryInpatientNo2.Text == "")
            {
                MessageBox.Show("����д������");
                return false;
            }
            //�ж��Ա�
            if (this.cmbSex.Text == "" || this.cmbSex.Tag == null)
            {
                MessageBox.Show("�������Ա�");
                return false;
            }

            //
            if (this.dtpBirthDay.Text == "")
            {
                MessageBox.Show("������������ڣ�");
                return false;
            }

            //�жϿ���
            if (this.cmbDept.Text == "" || this.cmbDept.Tag == null)
            {
                MessageBox.Show("��������ң�");
                return false;
            }
            //���ҳ���
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 18))
            {
                MessageBox.Show("�����������,����������!");
                return false;
            }
            if (this.cmbDept.Tag == null)
            {
                MessageBox.Show("��������ң�");
                return false;
            }
            //�жϺ�ͬ��λ
            //if (this.cmbPactUnit.Text == "" || this.cmbPactUnit.Tag == null)
            //{
            //    MessageBox.Show("�������ͬ��λ��");
            //    return false;
            //}
            //�������� 
            //if (this.dtpBirthDay.ToString() > this.inpatient.GetDateTimeFromSysDateTime().ToString())
            //{
            //    MessageBox.Show("�������ڴ��ڵ�ǰ����,����������!");
            //    this.dtpBirthDay.Focus();
            //    return false;
            //}

           
            //�ж��ַ�������ϵ�˵绰
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtLinkTel.Text, 20))
            {
                MessageBox.Show("��ϵ�˵绰¼�볬����");
                this.txtLinkTel.Focus();
                return false;
            }
            //��ͥ�绰
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtLinkTel.Text, 20))
            {
                MessageBox.Show("��ͥ�绰¼�볬����");
                this.txtLinkTel.Focus();
                return false;
            }
            //����֤
            if (!Neusoft.FrameWork.Public.String.ValidMaxLengh(this.txtIdentity.Text, 18))
            {
                MessageBox.Show("����֤����¼�볬��18λ��");
                this.txtIdentity.Focus();
                return false;
            }
            if (this.cmbPayKind.Text == "" || this.cmbPayKind.Tag == null)
            {
                MessageBox.Show("���������Ϊ�գ�" + "\n" + "��ѡ��������");
                this.cmbPayKind.Focus();
                return false;
            }
            return bRet;
        }

        /// <summary>
        /// ��Ͽؼ��¼�
        /// </summary>
        private void loadEven()
        {
            //�س���ת����
            foreach (Control c in this.neuGroupBox1.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }
            foreach (Control c in this.neuGroupBox2.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }
            foreach (Control c in this.neuGroupBox3.Controls)
            {
                c.KeyDown += new KeyEventHandler(c_KeyDown);
            }

        }

        /// <summary>
        /// ��ui��û�����Ϣ
        /// </summary>
        private void setPrePatient()
        {
            try
            {
                if (this.myPatientInfo == null)
                {
                    myPatientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
                }

                this.myPatientInfo.PID.CardNO = txtCardNo2.Text;//������
                 
                if (this.cmbDept.Tag != null)
                {
                    this.myPatientInfo.PVisit.PatientLocation.Dept.ID = cmbDept.Tag.ToString();//סԺ����
                }

                if (this.cmbNurseCell.Tag != null)
                {
                    //��ʿվ
                    this.myPatientInfo.PVisit.PatientLocation.NurseCell.ID = cmbNurseCell.Tag.ToString();
                }

                this.myPatientInfo.PVisit.PatientLocation.Dept.Name = this.cmbDept.Text.ToString();
                this.myPatientInfo.PVisit.InTime = this.dtPreDate.Value;//ԤԼ����----------

                this.myPatientInfo.Name = this.ucQueryInpatientNo2.Text.ToString();//����
                if (this.cmbSex.Tag != null)
                {
                    this.myPatientInfo.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�
                }
               
                if (this.cmbPayKind.Tag != null)
                    this.myPatientInfo.Pact.PayKind.ID = this.cmbPayKind.Tag.ToString();//�������


              
                //if (this.cmbMarriage.Tag != null)
                    //this.myPatientInfo.MaritalStatus.ID = this.cmbMarriage.Tag.ToString();//����״��
                this.myPatientInfo.IDCard = this.txtIdentity.Text.ToString();//����֤��
                if (this.cmbPos.Tag != null)
                    this.myPatientInfo.Profession.ID = this.cmbPos.Tag.ToString();//ְҵ
                //if (this.cmbHomePlace.Tag != null)
                //    this.myPatientInfo.DIST = this.cmbHomePlace.Tag.ToString();//������
                //if (this.cmbCountry.Tag != null)
                //    this.myPatientInfo.Country.ID = this.cmbCountry.Tag.ToString();//����

                this.myPatientInfo.AddressHome = this.txtHomeAddrd.Text.ToString();//��ͥסַ
                this.myPatientInfo.PhoneHome = this.txtLinkTel.Text;//��ͥ�绰
                this.myPatientInfo.CompanyName = this.txtWorkUnit.Text;//������λ
                //this.myPatientInfo.Kin.ID = this.txtLinkMan.Text;//��ϵ��
                this.myPatientInfo.Kin.Name = this.txtLinkMan.Text;
                this.myPatientInfo.Birthday = Convert.ToDateTime(this.dtpBirthDay.Text);
                this.myPatientInfo.Kin.RelationPhone = this.txtLinkTel.Text;//��ϵ�˵绰
            
                //if (this.cmbNationality.Tag != null)
                    //this.myPatientInfo.Nationality.ID = this.cmbNationality.Tag.ToString();//����
               
                if (this.cmbBedNo.Tag != null)
                {
                    this.myPatientInfo.PVisit.PatientLocation.Bed.ID = this.cmbBedNo.Tag.ToString();//������
                }
          
                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                if (this.txtInDiagnose.Tag != null)
                {
                    obj.ID = this.txtInDiagnose.Tag.ToString();
                    obj.Name = this.txtInDiagnose.Text;
                }
                this.myPatientInfo.Diagnoses.Add(obj);//��Ժ���	
                this.myPatientInfo.ClinicDiagnose = this.txtInDiagnose.Text; ;//�����������
                //this.myPatientInfo.SSN = this.txtSSN.Text;//ҽ��֤��

                //��Ժʱ���
                //if (this.checkBox1.Checked == true)
                //{
                //    this.myPatientInfo.ExtendFlag1 = "0";
                //}
                //else if (this.checkBox2.Checked == true)
                //{
                //    this.myPatientInfo.ExtendFlag1 = "1";
                //}
                //else if (this.checkBox3.Checked == true)
                //{
                //    this.myPatientInfo.ExtendFlag1 = "2";
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        /// <summary>
        /// ȡ��ԤԼ�Ǽ�
        /// </summary>
        /// <returns></returns>
        public int CancelPre()
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                MessageBox.Show("�л�����ѯҳ��!!", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return -1;
            }
            if (this.fpMainInfo_Sheet1.Rows.Count == 0) return -1;
            string CarNo = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            string HappenNo = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            if (MessageBox.Show("ȷ��Ҫȡ��ԤԼ�Ǽ�" + CarNo + "�ţ�", "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.Cancel)
                return -1;

            Neusoft.HISFC.Models.RADT.PatientInfo patient = this.managerIntegrate.QueryPreInPatientInfoByCardNO(HappenNo, CarNo);
            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerIntegrate.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);
            int iRet = this.managerIntegrate.UpdatePreInPatientState(CarNo, "1", HappenNo);
            if (iRet < 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.FrameWork.WinForms.Classes.Function.Msg("ȡ��ԤԼ�Ǽ�ʧ��" + this.managerIntegrate.Err, 211);
                iRet = -1;
            }
            if (iRet == 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("������¼�ѱ�ȡ��!");
                this.QueryData();
                iRet = -1;
            }
            if (iRet > 0)
            {

                #region addby xuewj 2010-3-15
                if (this.adt == null)
                {
                    this.adt = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IHE.IADT)) as Neusoft.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this.adt != null && patient != null)
                {
                    this.adt.CancelPreRegInpatient(patient);
                }
                #endregion

                Neusoft.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("ȡ���ɹ�!");
                this.QueryData();
                iRet = 1;
            }

            return iRet;
        }

        /// <summary>
        /// ���ݺ�ͬ��λ��ʾ����֧���������
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        private Neusoft.FrameWork.Models.NeuObject GetPactUnitByID(string strID)
        {
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();

            PactInfo p = managerIntegrate.GetPactUnitInfoByPactCode(strID);
            if (p == null)
            {
                MessageBox.Show("������ͬ��λ����" + this.managerIntegrate.Err, "��ʾ");
                return null;
            }
            if (p.PayKind.ID == "" || p.PayKind == null)
            {
                MessageBox.Show("�ú�ͬ��λ�Ľ������û��ά��", "��ʾ");
                return null;
            }
            else
            {
                switch (p.PayKind.ID)
                {
                    case "01":
                        obj.Name = "�Է�"; obj.ID = "01";
                        break;
                    case "02":
                        obj.Name = "����";
                        obj.ID = "02";
                        break;
                    case "03":
                        obj.Name = "������ְ";
                        obj.ID = "03";
                        break;
                    case "04":
                        obj.Name = "��������";
                        obj.ID = "04";
                        break;
                    case "05":
                        obj.Name = "���Ѹ߸�";
                        obj.ID = "05";
                        break;
                    default:
                        break;
                }
            }
            return obj;
        }

        /// <summary>
        /// ���ݷ�����Ż��ʵ��
        /// </summary>
        /// <param name="strNo"></param>
        /// <param name="strCardNo"></param>
        private void setPatient(string strNo, string strCardNo)
        {
            this.myPatientInfo = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNo, strCardNo);
            this.getPrePatient(myPatientInfo);
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            if (this.neuTabControl1.SelectedTab == this.tabPage2)
            {
                Neusoft.FrameWork.WinForms.Classes.Print p = new Print();

                if (((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).IsManager)
                {
                    p.PrintPreview(this.Panel1);
                }
                else
                {
                    p.PrintPage(0, 0, Panel1);
                    p.PrintPage(0, 0, Panel1);
                }
            }
        }

        /// <summary>
        /// ������Ͽؼ���ʾ
        /// </summary>
        private void SetLocation()
        {
            if (this.ucDiagnose1.Visible) return;
            this.ucDiagnose1.Top = this.neuGroupBox2.Top ;
            this.ucDiagnose1.Left =  this.txtInDiagnose.Left;
            this.ucDiagnose1.Visible = true;
        }
        /// <summary>
        /// �������value
        /// </summary>
        private void SetValue()
        {
            this.txtInDiagnose.Text = this.MyIcd.Name;
            this.txtInDiagnose.Tag = this.MyIcd.ID;
            this.ucDiagnose1.Visible = false;
            cmbNurseCell.Focus();
        }

        /// <summary>
        /// ��ȡ�����ַ�
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        private void getAge(int year, int month, int day)
        {
            if (year < 0)
            {
                year = 0;
            }
            if (month < 0)
            {
                month = 0;
            }
            if (day < 0)
            {
                day = 0;
            }
            this.txtAgeOne.Text = year.ToString();
            this.txtAgeTwo.Text = month.ToString();
            this.txtAgeThree.Text = day.ToString();
        }

        private void convertBirthdayByAge(bool isUpdateAgeText)
        {
            int iyear = Neusoft.FrameWork.Function.NConvert.ToInt32(this.txtAgeOne.Text.Trim());
            int iMonth = Neusoft.FrameWork.Function.NConvert.ToInt32(this.txtAgeTwo.Text.Trim());
            int iDay = Neusoft.FrameWork.Function.NConvert.ToInt32(this.txtAgeThree.Text.Trim());

            DateTime birthDay = new DateTime();
            
            
            birthDay = commonController.GetSystemTime().AddMilliseconds(-iDay).AddMinutes(-iMonth).AddHours(-iyear);
            
            if (birthDay < DateTime.MinValue || birthDay > DateTime.MaxValue)
            {
                MessageBox.Show("��������������������룡");
                this.getAge(0, 0, 0);
                return;
            }              
            this.dtpBirthDay.TextChanged -= new EventHandler(dtpBirthDay_TextChanged);
            this.dtpBirthDay.Text = birthDay.ToString("yyyy-MM-dd HH:mm:ss");
            this.dtpBirthDay.TextChanged += new EventHandler(this.dtpBirthDay_TextChanged);             
        }

        #endregion

        #region �¼�
        private void ucPrepayIn_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Init();
                initEvents();
                loadEven();
                InitQuery();
            }
        }

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                System.Windows.Forms.SendKeys.Send("{tab}");
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                e.Handled = false;
            }

        }

        /// <summary>
        /// �����¼�����
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    {
                        if (this.ucDiagnose1.Visible)
                            this.ucDiagnose1.PriorRow();
                        break;
                    }
                case Keys.Down:
                    {
                        if (this.ucDiagnose1.Visible)
                            this.ucDiagnose1.NextRow();
                        break;
                    }
                case Keys.Escape:
                    {
                        if (this.ucDiagnose1.Visible)
                            this.ucDiagnose1.Visible = false;
                        break;
                    }
                case Keys.Space:
                    {
                        if (this.txtInDiagnose.ContainsFocus)
                        {
                            this.SetLocation();
                        }
                        break;
                    }

                case Keys.Enter:
                    {
                        if (this.txtInDiagnose.ContainsFocus)
                        {
                            if (this.ucDiagnose1.Visible)
                            {
                                if (ucDiagnose1_SelectItem(Keys.Enter) == 0)
                                {
                                    SetValue();
                                }
                            }
                        }
                        break;
                    }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        void ucQueryInpatientNo2_myEvent()
        {


            if (this.ucQueryInpatientNo2.InpatientNo== null || this.ucQueryInpatientNo2.InpatientNo.Length==0)
                {
                    MessageBox.Show("סԺ���߲����ڻ����ѳ�Ժ");
                    //this.clear();
                    return;
                }


                //Neusoft.HISFC.Models.Account.AccountCard accountCardObj = new Neusoft.HISFC.Models.Account.AccountCard();
            //if (this.feeIntegrate.ValidMarkNO(txtCardNo, ref accountCardObj) <= 0)
            //{
            //    MessageBox.Show(this.feeIntegrate.Err);
            //    return;
            //}

                //this.txtCardNo.Text = accountCardObj.Patient.PID.CardNO;
                //this.ucQueryInpatientNo1.Text = accountCardObj.Patient.Name;
                //this.cmbSex.Tag = accountCardObj.Patient.Sex.ID;
                //this.dtBirthday.Value = accountCardObj.Patient.Birthday;
                //this.txtSSN.Text = accountCardObj.Patient.SSN;
                //this.txtHomeAddrd.Text = accountCardObj.Patient.AddressHome;

           // this.myPatientInfo = this.radtIntegrate.QueryInPatientByName(this.ucQueryInpatientNo1.txtInputCode);
            this.myPatientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo2.InpatientNo);
            if (this.myPatientInfo == null)
            {
                MessageBox.Show("���һ���ʧ��!");
                this.ucQueryInpatientNo2.Focus();
                return;
            }
            this.getPrePatient(myPatientInfo);
        }

        private void dtpBirthDay_TextChanged(object sender, EventArgs e)
        {
            if (this.dtpBirthDay.Text == null)
            {
                return;
            }
            DateTime birthday = new DateTime();

            int iyear = 0;
            int iMonth = 0;
            int iDay = 0;

            
            birthday = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.dtpBirthDay.Text);
            if (birthday < new DateTime(1700, 1, 1))
            {
                return;
            }
            commonController.GetAge(birthday, ref iyear, ref iMonth, ref iDay);
            this.txtAgeOne.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAgeTwo.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.txtAgeThree.TextChanged -= new EventHandler(txtAge_TextChanged);
            this.getAge(iyear, iMonth, iDay);
            this.txtAgeOne.TextChanged += new EventHandler(txtAge_TextChanged);
            this.txtAgeTwo.TextChanged += new EventHandler(txtAge_TextChanged);
            this.txtAgeThree.TextChanged += new EventHandler(txtAge_TextChanged);
            

        }

        private void txtAge_TextChanged(object sender, EventArgs e)
        {
            this.convertBirthdayByAge(false);
        }

        private void cmbPactUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //string strID = this.cmbPactUnit.Tag.ToString();
                Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
                //obj = this.GetPactUnitByID(strID);
                this.cmbPayKind.Tag = obj.ID;
                this.cmbPayKind.Text = obj.Name;
            }
            catch { }
        }

        /// <summary>
        /// ͨ����ʿվ���˴�λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNurseCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ArrayList arrBed = new ArrayList();
                string strID = this.cmbNurseCell.Tag.ToString();
                arrBed = managerBed.GetUnoccupiedBed(this.cmbNurseCell.Tag.ToString());
                this.cmbBedNo.AddItems(arrBed);
            }
            catch { }
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            try
            {
                //��ǰ������
                int iRow = e.Row;
                //��ȡ�������
                string strNo = this.fpMainInfo_Sheet1.Cells[iRow, 0].Text.Trim();
                string strCardNo = this.fpMainInfo_Sheet1.Cells[iRow, 1].Text.Trim();
                //���ԤԼ�Ǽ�ʵ�巵�ظ�����
                this.setPatient(strNo, strCardNo);
                this.neuTabControl1.SelectedIndex = 0;
            }
            catch { }
        }

        private void txtInDiagnose_TextChanged(object sender, EventArgs e)
        {
            if (ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Filter(this.txtInDiagnose.Text.Trim());
            }
        }

        private int ucDiagnose1_SelectItem(Keys key)
        {
            int result = this.ucDiagnose1.GetItem(ref MyIcd);
            if (result < 0) return -1;
            SetValue();
            return 1;
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                //this.txtCardNo.Focus();
                //this.txtCardNo.SelectAll();
            }
        }

        #endregion

        #region ��ѯ
        /// <summary>
        /// ��ʼ��DataTable
        /// </summary>
        private void SetDataTable()
        {
            this.fpMainInfo_Sheet1.RowCount = 0;

            Type str = typeof(String);
            Type date = typeof(DateTime);

            Type dec = typeof(Decimal);
            Type bo = typeof(bool);
            #region ԤԼ�Ǽ��б�

            dtPrepayIn.Columns.AddRange(new DataColumn[]{new DataColumn("�������", str),
															new DataColumn("������", str),
															new DataColumn("��������", str),
															new DataColumn("�Ա�", str),
															new DataColumn("��ͬ��λ", str),
															new DataColumn("סԺ����", str),
															new DataColumn("ԤԼ����", str),
															new DataColumn("��ǰ״̬", str),															
															new DataColumn("��ͥ��ַ", str),
															new DataColumn("��ͥ�绰", str),
															new DataColumn("��ϵ��", str),
															new DataColumn("��ϵ�˵绰", str),
															new DataColumn("��ϵ�˵�ַ", str),
															new DataColumn("����Ա", str),
															new DataColumn("����ʱ��", str)});



            #endregion
        }

        private void InitQuery()
        {
            //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //this.myObjHelper.ArrayObject = this.managerIntegrate.GetConstantList(EnumConstant.PACTUNIT);
            this.myObjHelper.ArrayObject = this.pactUnitInfo.QueryPactUnitAll();

            this.operObjHelper.ArrayObject = this.managerIntegrate.QueryEmployeeAll();
            SetDataTable();
            QueryData();

        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        public void QueryData()
        {
            string PrepayinState = this.GetState();
            string Begin = this.dtBegin.Value.ToShortDateString() + " 00:00:00";
            string End = this.dtEnd.Value.ToShortDateString() + " 23:59:59";
            this.QueryData(PrepayinState, Begin, End);
        }

        /// <summary>
        /// ����ԤԼ״̬��ʱ���������
        /// </summary>
        /// <param name="PrepayinState"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        private void QueryData(string PrepayinState, string begin, string end)
        {
            this.dtPrepayIn.Clear();
            try
            {
                ArrayList arrPrein = new ArrayList();

                //arrPrein = this.myInpatient.GetPreInPatientInfoByDateAndState(PrepayinState, begin, end);
                arrPrein = this.managerIntegrate.QueryPreInPatientInfoByDateAndState(PrepayinState, begin, end);
                string strName = "", strStateName = "";
                if (arrPrein == null)
                    return;
                foreach (Neusoft.HISFC.Models.RADT.PatientInfo obj in arrPrein)
                {
                    #region��ȡ�Ա�����
                    switch (obj.Sex.ID.ToString())
                    {
                        case "U":
                            strName = "δ֪";
                            break;
                        case "M":
                            strName = "��";
                            break;
                        case "F":
                            strName = "Ů";
                            break;
                        case "O":
                            strName = "����";
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region �Ǽ�״̬
                    switch (obj.User02.ToString())
                    {
                        case "0":
                            strStateName = "ԤԼ�Ǽ�";
                            break;
                        case "1":
                            strStateName = "ȡ��ԤԼ�Ǽ�";
                            break;
                        case "2":
                            strStateName = "ԤԼתסԺ";
                            break;
                        default:
                            break;
                    }
                    #endregion

                    #region ȡ��ͬ��λ������Ա����
                    obj.Pact.Name = this.myObjHelper.GetName(obj.Pact.ID);
                    string strOperID = obj.User03.Substring(0, 6);
                    string strOperName = this.operObjHelper.GetName(strOperID);
                    #endregion

                    #region ��DataTable��������
                    DataRow row = this.dtPrepayIn.NewRow();
                    row["�������"] = obj.User01;
                    row["������"] = obj.PID.CardNO;
                    row["��������"] = obj.Name;
                    row["�Ա�"] = strName;
                    row["��ͬ��λ"] = obj.Pact.Name;//��ת��
                    row["סԺ����"] = obj.PVisit.PatientLocation.Dept.Name;
                    row["ԤԼ����"] = obj.PVisit.InTime;//.Date_In;
                    row["��ǰ״̬"] = strStateName;//��ת��
                    row["��ͥ��ַ"] = obj.AddressHome;
                    row["��ͥ�绰"] = obj.PhoneHome;
                    row["��ϵ��"] = obj.Kin.ID;
                    row["��ϵ�˵绰"] = obj.Kin.Memo;
                    row["��ϵ�˵�ַ"] = obj.Kin.User01;
                    row["����Ա"] = strOperName;
                    row["����ʱ��"] = obj.User03.Substring(6, 10);

                    this.dtPrepayIn.Rows.Add(row);
                    #endregion
                }

                dvPrepayIn = new DataView(this.dtPrepayIn);
                this.fpMainInfo_Sheet1.DataSource = this.dvPrepayIn;
                this.initFp();

            }
            catch { }
        }

        /// <summary>
        /// ����fp����
        /// </summary>
        private void initFp()
        {
            try
            {
                int im = 3;
                this.fpMainInfo_Sheet1.OperationMode = (FarPoint.Win.Spread.OperationMode)im;
                this.fpMainInfo_Sheet1.Columns.Get(0).Width = 0F;
                this.fpMainInfo_Sheet1.Columns.Get(1).Width = 100F;
                this.fpMainInfo_Sheet1.Columns.Get(2).Width = 72F;
                this.fpMainInfo_Sheet1.Columns.Get(3).Width = 48F;
                this.fpMainInfo_Sheet1.Columns.Get(5).Width = 88F;
                this.fpMainInfo_Sheet1.Columns.Get(6).Width = 100F;
                this.fpMainInfo_Sheet1.Columns.Get(9).Width = 95F;
                this.fpMainInfo_Sheet1.Columns.Get(10).Width = 102F;
                this.fpMainInfo_Sheet1.Columns.Get(11).Width = 127F;
                this.fpMainInfo_Sheet1.Columns.Get(12).Width = 85F;
                //			this.fpMainInfo_Sheet1.Columns.Get(13).Width = 80F;
                this.fpMainInfo_Sheet1.Columns.Get(14).Width = 85F;
            }
            catch { }
        }

        /// <summary>
        /// �鿴��ǰ��ѯ��״̬
        /// </summary>
        /// <returns></returns>
        private string GetState()
        {
            string state = string.Empty;
            if (this.RbtPrePatient.Checked)
            {
                state = "0";
            }
            if (this.RbtCancelPre.Checked)
            {
                state = "1";
            }
            if (this.RbtChange.Checked)
            {
                state = "2";
            }
            return state;
        }

        /// <summary>
        /// ����ԤԼ״̬��ѯ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbtCheckChange(object sender, EventArgs e)
        {
            string PrepayinState = this.GetState();
            string Begin = this.dtBegin.Value.ToShortDateString() + " 00:00:00";
            string End = this.dtEnd.Value.ToShortDateString() + " 23:59:59";
            this.QueryData(PrepayinState, Begin, End);
        }

        #endregion


        /// <summary>
        /// {C3AA974A-D98C-455b-ABDC-68781DB0306F}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int PrintPreview(object sender, object neuObject)
        {
            string strNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            string cardNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            Neusoft.HISFC.Models.RADT.PatientInfo p = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNO, cardNO);

            if (p == null)
            {
                MessageBox.Show("��ӡ��Ժ֪ͨ��ʱ����ѯ����ԤԼ��Ϣʧ�ܡ�\n" + this.managerIntegrate.Err);
                return -1;
            }

            this.iPrintInHosNotice.SetValue(p);
            this.iPrintInHosNotice.PrintView();

            return base.PrintPreview(sender, neuObject);
        }
        //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
        private Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice iPrintInHosNotice = null;

        //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
        public int InitInterface()
        {

            iPrintInHosNotice = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice)) as Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice;
            return 1;
        }
        //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
        private int PrintNotice()
        {
            string strNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            string cardNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            Neusoft.HISFC.Models.RADT.PatientInfo p = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNO, cardNO);

            if (p == null)
            {
                MessageBox.Show("��ӡ��Ժ֪ͨ��ʱ����ѯ����ԤԼ��Ϣʧ�ܡ�\n" + this.managerIntegrate.Err);
                return -1;
            }

            this.iPrintInHosNotice.SetValue(p);
            this.iPrintInHosNotice.Print();


            return 1;

        }

        #region IInterfaceContainer ��Ա
        //{C3AA974A-D98C-455b-ABDC-68781DB0306F}
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice);

                return type;
            }
        }

        #endregion

        /// <summary>
        ///  ��ʿվ�б�����ұ仯 {E9EC275C-F044-40f1-BDDA-0F17410983EB}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList alNurseCell = new ArrayList();

            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = this.cmbDept.Tag.ToString();
            alNurseCell = this.managerIntegrate.QueryNurseStationByDept(obj);

            //if (alNurseCell.Count !=null)
            //{
            //    //this.cmbNurseCell.ClearItems();
            //    this.cmbNurseCell.AddItems(alNurseCell);
            //}
            if (alNurseCell != null)
            {
                Neusoft.FrameWork.Models.NeuObject nurseCell = alNurseCell[0] as Neusoft.FrameWork.Models.NeuObject;
                if (nurseCell != null)
                {
                    this.cmbNurseCell.Tag = nurseCell.ID;
                }
            }
        }

        

       
        
    }
}
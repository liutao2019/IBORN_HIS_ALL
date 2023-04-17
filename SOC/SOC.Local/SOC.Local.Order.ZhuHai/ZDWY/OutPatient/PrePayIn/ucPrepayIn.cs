using System;
using System.Collections;
using System.Data;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Classes;
using FS.HISFC.Models.Base;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PrePayIn
{
    public partial class ucPrepayIn : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer,FS.HISFC.BizProcess.Interface.Fee.IPrePayIn
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucPrepayIn()
        {
            InitializeComponent();
        }

        #region ����

        DataTable _dtPrepayIn = new DataTable();
        DataView _dvPrepayIn;

        protected FS.FrameWork.WinForms.Forms.ToolBarService _toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        //FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo _myPatientInfo = null;

        /// <summary>
        /// adt�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.IHE.IADT _adt = null;

        /// <summary>
        /// ��Ժ֪ͨ����ӡ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.IPrintInHosNotice _iPrintInHosNotice = null;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        bool isNew = false;
        #endregion

        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get
            {
                return _myPatientInfo;
            }
            set
            {
                if (value == null)
                {
                    _myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }
                else
                {
                    _myPatientInfo = value;

                    if (_myPatientInfo!= null)
                    {
                        this.SetPrepatient(_myPatientInfo, true);// {F6204EF5-F295-4d91-B81A-736A268DD394}
                    }
                }
            }
        }
        public void ShowDialog()
        {            
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(this);
        }


        private bool isShowButton = false;
        /// <summary>
        /// �Ƿ���ʾ����ؼ�
        /// </summary>
        public bool IsShowButton
        {
            get
            {
                return isShowButton;
            }
            set
            {
                isShowButton = value;
                this.btSave.Visible = isShowButton;
                this.btPrint.Visible = isShowButton;
                this.btQuery.Visible = isShowButton;

            }
        }
        #endregion

        #region ҵ������

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.Fee _feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.BizLogic.Manager.Bed bedMgr = new FS.HISFC.BizLogic.Manager.Bed();
        FS.HISFC.Models.HealthRecord.ICD icdMgr = null;
        FS.HISFC.BizLogic.Fee.PactUnitInfo PactUnit = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        
        #endregion

        #region ��ʼ��

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            _toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            _toolBarService.AddToolButton("ȡ��ԤԼ", "ȡ��ԤԼ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            _toolBarService.AddToolButton("��Ժ֪ͨ��", "��ӡ��Ժ֪ͨ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D��ӡִ�е�, true, false, null);
            _toolBarService.AddToolButton("ˢ��", "ˢ��", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);

            return _toolBarService;
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

                case "��Ժ֪ͨ��":
                    this.PrintNotice();
                    break;

                case "ˢ��":// {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}
                    string mCardNO = "";
                    string error = "";
                    if (FS.HISFC.Components.Common.Classes.Function.OperMCard(ref mCardNO, ref error) < 0)
                    {
                        MessageBox.Show("����ʧ�ܣ���ȷ���Ƿ���ȷ�������ƿ���\n" + error);
                        return;
                    }

                    mCardNO = ";" + mCardNO;
                    this.txtCardNo.Focus();
                    this.txtCardNo.SelectAll();
                    this.txtCardNo.Text = mCardNO;
                    this.txtCardNo_KeyDown(null, new KeyEventArgs(Keys.Enter));
                    break;

                default: break;
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

        /// <summary>
        /// ��ʼ���ؼ�,����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        protected virtual int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڳ�ʼ�����ڣ����Ժ�^^");
            Application.DoEvents();
            try
            {
                //��ʼ��סԺ�����б�
                this.cmbDept.AddItems(this.managerIntegrate.GetDepartment(EnumDepartmentType.I));   //this.myDept.GetInHosDepartment());
                //��ʿվ
                this.cmbNurseCell.AddItems(this.managerIntegrate.GetDepartment(EnumDepartmentType.N)); //this.myDept.GetDeptment(EnumDepartmentType.N));

                //����״��
                this.cmbMarriage.AddItems(FS.HISFC.Models.RADT.MaritalStatusEnumService.List());

                //��ͬ��λ{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
                //this.cmbPactUnit.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PACTUNIT));
                this.cmbPactUnit.AddItems(SOC.HISFC.BizProcess.Cache.Fee.GetAllPactUnit());

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
                this.cmbPreDoc.AddItems(this.managerIntegrate.QueryEmployee(EnumEmployeeType.D));

                //��ϵ�˹�ϵ
                this.cmbLinkManRel.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.RELATIVE));

                //����
                this.cmbNationality.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.NATION));

                //�Ա�			
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());

                //����
                this.dtBirthday.Value = this.bedMgr.GetDateTimeFromSysDateTime(); //this.myInpatient.GetDateTimeFromSysDateTime();
                //����Ա
                //this.txtOper.Text =this.managerIntegrate. this.myInpatient.Operator.Name;

                //ԤԼʱ��
                this.dtPreDate.Value = this.bedMgr.GetDateTimeFromSysDateTime();
                //����������Ϣ// {67256EED-3DE6-4179-9C49-9A4D115DE309}
                this.cmbPatientType.AddItems(this.managerIntegrate.GetConstantList("PatientType"));
                this.cmbPatientType.SelectedIndex = 0;
                //�������
                this.cmbPayKind.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PAYKIND));
                this.cmbPayKind.SelectedIndex = 0;
                this.dtBegin.Value = this.bedMgr.GetDateTimeFromSysDateTime().AddDays(-1);
                this.dtEnd.Value = this.bedMgr.GetDateTimeFromSysDateTime().AddDays(1);
                this.cmbPreDoc.Tag = this.bedMgr.Operator.ID;

                //���
                this.ucDiagnose1.Init();
                this.ucDiagnose1.SelectItem += new FS.HISFC.Components.Common.Controls.ucDiagnose.MyDelegate(ucDiagnose1_SelectItem);
                this.ActiveControl = this.txtCardNo;

                this.cmbCircs.AddItems(managerIntegrate.GetConstantList(EnumConstant.INCIRCS));
                this.cmbCircs.SelectedIndex = 0;

                this.InitInterface();


                cmbDept.SelectedIndex = -1;
                cmbDept.Tag = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.bedMgr.Operator.ID).Dept.ID;


                if (_myPatientInfo != null)// {D59C1D74-CE65-424a-9CB3-7F9174664504}
                {
                    this.SetPrepatient(_myPatientInfo, true);// {F6204EF5-F295-4d91-B81A-736A268DD394}
                }
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

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
                if (c.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox)) continue;

                foreach (Control ctr in c.Controls)
                {
                    if (ctr.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuTextBox)
                        || ctr.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                    {
                        if (ctr.Name != "txtOper" || ctr.Name != "cmbPreDoc")
                        {
                            ctr.Text = "";
                            ctr.Tag = "";
                        }
                    }

                }
            }

            this.dtPreDate.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            this.dtBirthday.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            this.cmbCircs.SelectedIndex = 0;
            if (this.cmbPreDoc.Items.Count > 0)
            {
                cmbPreDoc.Tag=this.bedMgr.Operator.ID;
            }
            this.cmbPatientType.Tag = "P";
            this.cmbPayKind.Tag = "01";
            this.cmbPactUnit.Tag = "1";
            this.txtCardNo.Focus();
            cmbDept.SelectedIndex = -1;
            cmbDept.Tag = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.bedMgr.Operator.ID).Dept.ID;
        }

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        private void SetPrepatient(FS.HISFC.Models.RADT.PatientInfo pInfo, bool isNew)
        {
            this.txtCardNo.Text = pInfo.PID.CardNO;//������
            txtCardNo.Tag = pInfo.User01;

            this.cmbDept.Tag = pInfo.PVisit.PatientLocation.Dept.ID;//סԺ����
            this.cmbPatientType.Tag = pInfo.PatientType.ID;// {67256EED-3DE6-4179-9C49-9A4D115DE309}
            if (this.cmbPatientType.Items.Count > 0)
            {
                this.cmbPatientType.SelectedIndex = 0;
            }
            this.cmbDept.Text = pInfo.PVisit.PatientLocation.Dept.Name;
            this.cmbNurseCell.Tag = pInfo.PVisit.PatientLocation.NurseCell.ID;
            // {D59C1D74-CE65-424a-9CB3-7F9174664504}
            if (cmbDept.Tag == null
                || string.IsNullOrEmpty(cmbDept.Tag.ToString()))
            {
                cmbDept.SelectedIndex = -1;
                cmbDept.Tag = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.bedMgr.Operator.ID).Dept.ID;
            }

            if (pInfo.PVisit.InTime >= DateTime.MaxValue || pInfo.PVisit.InTime <= DateTime.MinValue || pInfo.PVisit.InTime == null)
            {

                this.dtPreDate.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            }
            else
            {
                this.dtPreDate.Value = pInfo.PVisit.InTime;//ԤԼ����----------
            }

            this.txtName.Text = pInfo.Name;//����
            if (this.cmbSex.Items.Count == 0)
            {                
                this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            }
            this.cmbSex.Tag = pInfo.Sex.ID;//�Ա�

            if (string.IsNullOrEmpty(this.cmbSex.Tag.ToString()) || string.IsNullOrEmpty(cmbSex.Text))
            {
                this.cmbSex.SelectedIndex = 1;
            }
            this.cmbPactUnit.Tag = pInfo.Pact.ID;//��ͬ��λ 
            //this.cmbPactUnit.Text = pInfo.Pact.Name;//��ͬ��λ

            if (this.cmbPayKind.Items.Count == 0)
            {
                this.cmbPayKind.AddItems(this.managerIntegrate.GetConstantList(EnumConstant.PAYKIND));
            }
            this.cmbPayKind.Tag = pInfo.Pact.PayKind.ID;//�������
            if (string.IsNullOrEmpty(this.cmbPayKind.Tag.ToString()))// {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            {
                FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();
                pact = this.PactUnit.GetPactUnitInfoByPactCode(pInfo.Pact.ID);
                this.cmbPayKind.Tag = pact.PayKind.ID;

            }

            if (DateTime.MaxValue > pInfo.Birthday && pInfo.Birthday > DateTime.MinValue)
            {
                this.dtBirthday.Value = pInfo.Birthday;//��������// {F6204EF5-F295-4d91-B81A-736A268DD394}
            }
            else
            {
                this.dtBirthday.Value = this.bedMgr.GetDateTimeFromSysDateTime();
            }
            this.cmbMarriage.Tag = pInfo.MaritalStatus.ID;//����״��
            this.txtIdentity.Text = pInfo.IDCard;//���֤��
            this.cmbPos.Tag = pInfo.Profession.ID;//ְҵ
            this.cmbHomePlace.Tag = pInfo.AreaCode;//������
            this.cmbCountry.Tag = pInfo.Country.ID;//����

            this.txtHomeAddr.Text = pInfo.AddressHome;//��ͥסַ
            this.txtHomeTel.Text = pInfo.PhoneHome;//��ͥ�绰
            this.txtWorkUnit.Text = pInfo.CompanyName;//������λ
            this.txtLinkMan.Text = pInfo.Kin.Name;//��ϵ��
            this.txtLinkManAddr.Text = pInfo.Kin.RelationAddress;//��ϵ��סַ
            this.txtLinkTel.Text = pInfo.Kin.RelationPhone;//��ϵ�˵绰
            this.txtWorkTel.Text = pInfo.PhoneBusiness;//������λ�绰
            this.cmbNationality.Tag = pInfo.Nationality.ID;//����
            this.cmbLinkManRel.Tag = pInfo.Kin.Relation.ID;//��ϵ�˹�ϵ
            this.cmbBedNo.Tag = pInfo.PVisit.PatientLocation.Bed.ID;//������

            this.cmbPreDoc.Tag = pInfo.PVisit.AdmittingDoctor.ID;//ԤԼҽ��

            #region �����������

            if (isNew)
            {
                //if (pInfo.Diagnoses.Count > 0)
                //{
                //    this.txtInDiagnose.Tag = (pInfo.Diagnoses[0] as FS.FrameWork.Models.NeuObject).ID;//������ϱ���
                //}

                //this.txtInDiagnose.Text = pInfo.ClinicDiagnose;//�����������

                string sql = @"
                        select * from 
                        (select f.diag_name
                        from met_cas_diagnose f,fin_opr_register r
                        where /*f.diag_kind='1'
                        and*/ f.inpatient_no=r.clinic_code
                        and r.card_no='{0}'
                        order by r.reg_date,f.diag_kind desc
                        )
                        where rownum=1";

                string strDiag = "";
                sql = string.Format(sql, pInfo.ID);
                strDiag = bedMgr.ExecSqlReturnOne(sql, "");
                if (strDiag == "-1")
                {
                    strDiag = "";
                }

                if (!string.IsNullOrEmpty(strDiag))
                {
                    txtInDiagnose.Text = strDiag.TrimEnd('��');
                }

                //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(pInfo.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
                //if (al == null)
                //{
                //    MessageBox.Show("��ѯ�����Ϣ����\r\n" + diagManager.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return; 
                //}

                //string strDiag = "";
                //foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
                //{
                //    if (diag != null && diag.IsValid)
                //    {
                //        strDiag += diag.DiagInfo.ICD10.Name + "��";
                //    }
                //}
                //txtInDiagnose.Text = strDiag.TrimEnd('��');
            }
            else
            {
                txtInDiagnose.Text = pInfo.ClinicDiagnose;
            }

            #endregion

            this.txtSSN.Text = pInfo.SSN;//ҽ��֤��

            //Ѻ��
            this.numericUpDownForegift.Value = pInfo.FT.PrepayCost;

            //ָ��
            this.txtMemo.Text = pInfo.Memo;

            //��Ժ���
            if (pInfo.PVisit.Circs.ID == "")
            {
                if (this.cmbCircs.Items.Count > 0)
                {
                    this.cmbCircs.SelectedIndex = 0;
                }
            }
            else
            {
                this.cmbCircs.Tag = pInfo.PVisit.Circs.ID;
            }

            this.isNew = isNew;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            try
            {
                return this.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ԤԼ�Ǽ�ʧ�ܣ�" + "\r\n" + ex.Message + "\r\n" + ex.TargetSite + "\r\n" + ex.StackTrace, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }

        /// <summary>
        /// ����ԤԼ���ߵǼ���Ϣ
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //��Ч���ж�
            if (!this.CheckValid())
            {
                return -1;
            }

            //�õ�PatientInfoʵ��
            this.GetPrePatient();

            //������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (isNew)
            {
                if (this.managerIntegrate.InsertPreInPatient(this.PatientInfo) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    if (this.managerIntegrate.DBErrCode == 1)
                    {
                        MessageBox.Show("������ԤԼ�Ǽ�!");
                        this.txtCardNo.Focus();
                        this.txtCardNo.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("ԤԼ�Ǽ�ʧ�ܣ�\r\n" + managerIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return -1;
                }
            }
            else
            {
                if (this.managerIntegrate.UpdatePreInPatientByHappenNo(this.PatientInfo) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    if (this.managerIntegrate.DBErrCode == 1)
                    {
                        MessageBox.Show("�û��߸���ԤԼ�Ǽ�ʧ��!\r\n" + managerIntegrate.Err);
                        this.txtCardNo.Focus();
                        this.txtCardNo.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show("����ԤԼ�Ǽ�ʧ�ܣ�\r\n" + managerIntegrate.Err, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return -1;
                }
            }

            if (this.cmbPreDoc.SelectedIndex >= 0)
            {
                this._myPatientInfo.PVisit.ReferringDoctor.ID = this.cmbPreDoc.Tag.ToString();
                this._myPatientInfo.PVisit.ReferringDoctor.Name = this.cmbPreDoc.Text;

                this._myPatientInfo.PVisit.AdmittingDoctor.ID = string.Empty;
            }

            if (this._adt == null)
            {
                this._adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
            }
            if (this._adt != null)
            {
                this._adt.PreRegInpatient(_myPatientInfo);
            }

            //�ύ
            FS.FrameWork.Management.PublicTrans.Commit();
            if (isNew)
            {
                MessageBox.Show("ԤԼ�Ǽǳɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("����ԤԼ��Ϣ�ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            this.Clear();

            this.neuTabControl1.SelectedIndex = 1;
            this.QueryData();

            if (null == _iPrintInHosNotice)
            {
                MessageBox.Show("�޷���ӡ��Ժ֪ͨ��", "��ʾ", MessageBoxButtons.OK);
            }
            else
            {

                DialogResult dr = MessageBox.Show("�Ƿ��ӡ��Ժ֪ͨ����", "��ʾ", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    _iPrintInHosNotice.SetValue(this.PatientInfo);
                    _iPrintInHosNotice.Print();
                }
            }

            if (isShowButton)
            {
                this.Close();
            }

            return 0;
        }

        /// <summary>
        /// �ж�����Ϸ���
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            //�ж���Ϻ�
            if (this.txtCardNo.Text == null || this.txtCardNo.Text.Trim() == "")
            {
                MessageBox.Show("�����벡����!", "��ʾ");
                this.txtCardNo.Focus();
                return false;
            }

            //�ж�����
            if (this.txtName.Text == "")
            {
                MessageBox.Show("����д������");
                this.txtName.Focus();
                return false;
            }
            //�жϿ���
            if (this.cmbDept.Text == "" || this.cmbDept.Tag == null)
            {
                MessageBox.Show("��������ң�");
                this.cmbDept.Focus();
                return false;
            }


            //�жϻ�������// {67256EED-3DE6-4179-9C49-9A4D115DE309}
            if (this.cmbPatientType.Text == "" || this.cmbPatientType.Tag == null)
            {
                MessageBox.Show("��ѡ�������ͣ�");
                this.cmbPatientType.Focus();
                return false;
            }
            //���ҳ���
            //if (!FS.FrameWork.Public.String.ValidMaxLengh(this.cmbDept.Text, 16))
            //{
            //    MessageBox.Show("�����������,����������!");
            //    return false;
            //}
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
            if (this.dtBirthday.Value > this.bedMgr.GetDateTimeFromSysDateTime())
            {
                MessageBox.Show("�������ڴ��ڵ�ǰ����,����������!");
                this.dtBirthday.Focus();
                return false;
            }

            //�ж��Ա�
            if (this.cmbSex.Text == "" || this.cmbSex.Tag == null)
            {
                MessageBox.Show("�������Ա�");
                this.cmbSex.Focus();
                return false;
            }
            //�ж��ַ�������ϵ�˵绰
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtLinkTel.Text, 20))
            {
                MessageBox.Show("��ϵ�˵绰¼�볬����");
                this.txtLinkTel.Focus();
                return false;
            }
            //��ͥ�绰
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtHomeTel.Text, 20))
            {
                MessageBox.Show("��ͥ�绰¼�볬����");
                this.txtHomeTel.Focus();
                return false;
            }
            //���֤
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtIdentity.Text, 18))
            {
                MessageBox.Show("���֤����¼�볬��18λ��");
                this.txtIdentity.Focus();
                return false;
            }
            if (this.cmbPactUnit.Text == "" || this.cmbPactUnit.Tag == null)
            {
                MessageBox.Show("���������Ϊ�գ�" + "\n" + "��ѡ��������");
                this.cmbPactUnit.Focus();
                return false;
            }

            //����״��
            if (this.cmbMarriage.Text == "" || this.cmbMarriage.Tag == null) 
            {
                MessageBox.Show("����״������Ϊ�գ�" + "\n" + "��ѡ�����״��");
                this.cmbMarriage.Focus();
                return false;
            }

            //��Ժ��Ҫ���
            if (this.txtInDiagnose.Text == "")
            {
                MessageBox.Show("��Ժ��Ҫ��ϲ���Ϊ�գ�" + "\n" + "��ѡ����Ժ��Ҫ���");
                this.txtInDiagnose.Focus();
                return false;
            }


            return true;
        }

        /// <summary>
        /// ��Ͽؼ��¼�
        /// </summary>
        private void LoadEvents()
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
        private void GetPrePatient()
        {
            if (this._myPatientInfo == null)
            {
                _myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            }

            this._myPatientInfo.PID.CardNO = txtCardNo.Text;//������
            if (this.cmbDept.Tag != null)
            {
                this._myPatientInfo.PVisit.PatientLocation.Dept.ID = cmbDept.Tag.ToString();//סԺ����
            }

            if (txtCardNo.Tag != null
                && !string.IsNullOrEmpty(txtCardNo.Tag.ToString()))
            {
                _myPatientInfo.User01 = txtCardNo.Tag.ToString();
            }

            if (this.cmbNurseCell.Tag != null)
            {
                //��ʿվ
                this._myPatientInfo.PVisit.PatientLocation.NurseCell.ID = cmbNurseCell.Tag.ToString();
            }

            this._myPatientInfo.PVisit.PatientLocation.Dept.Name = this.cmbDept.Text;
            this._myPatientInfo.PVisit.InTime = this.dtPreDate.Value;//ԤԼ����----------
            //��������
            this._myPatientInfo.PatientType.ID = this.cmbPatientType.Tag.ToString();
            this._myPatientInfo.PatientType.Name = this.cmbPatientType.Text;

            this._myPatientInfo.Name = this.txtName.Text;//����
            if (this.cmbSex.Tag != null)
            {
                this._myPatientInfo.Sex.ID = this.cmbSex.Tag.ToString();//�Ա�
            }
            //if (this.cmbPactUnit.Tag != null)
            //    this._myPatientInfo.Pact.ID = this.cmbPactUnit.Tag.ToString();//��ͬ��λ

            // {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            if (this.cmbPactUnit.Tag != null)
            {
                //this._myPatientInfo.Pact.PayKind.ID = this.cmbPayKind.Tag.ToString();//�������
                this._myPatientInfo.Pact.ID = this.cmbPactUnit.Tag.ToString();

                FS.HISFC.Models.Base.PactInfo p = new FS.HISFC.Models.Base.PactInfo();
                p = this.PactUnit.GetPactUnitInfoByPactCode(this.cmbPactUnit.Tag.ToString());
                this._myPatientInfo.Pact.PayKind.ID = p.PayKind.ID;
            }


            this._myPatientInfo.Birthday = this.dtBirthday.Value;//��������
            if (this.cmbMarriage.Tag != null)
                this._myPatientInfo.MaritalStatus.ID = this.cmbMarriage.Tag.ToString();//����״��
            this._myPatientInfo.IDCard = this.txtIdentity.Text;//���֤��
            if (this.cmbPos.Tag != null)
                this._myPatientInfo.Profession.ID = this.cmbPos.Tag.ToString();//ְҵ
            if (this.cmbHomePlace.Tag != null)
                this._myPatientInfo.AreaCode = this.cmbHomePlace.Tag.ToString();//������
            if (this.cmbCountry.Tag != null)
                this._myPatientInfo.Country.ID = this.cmbCountry.Tag.ToString();//����

            this._myPatientInfo.AddressHome = this.txtHomeAddr.Text;//��ͥסַ
            this._myPatientInfo.PhoneHome = this.txtHomeTel.Text;//��ͥ�绰
            this._myPatientInfo.CompanyName = this.txtWorkUnit.Text;//������λ
            this._myPatientInfo.Kin.Name = this.txtLinkMan.Text;//��ϵ��
            this._myPatientInfo.Kin.RelationAddress = this.txtLinkManAddr.Text;//��ϵ��סַ
            this._myPatientInfo.Kin.RelationPhone = this.txtLinkTel.Text;//��ϵ�˵绰
            this._myPatientInfo.PhoneBusiness = this.txtWorkTel.Text;//������λ�绰
            if (this.cmbNationality.Tag != null)
                this._myPatientInfo.Nationality.ID = this.cmbNationality.Tag.ToString();//����
            if (this.cmbLinkManRel.Tag != null)
                this._myPatientInfo.Kin.Relation.ID = this.cmbLinkManRel.Tag.ToString();//��ϵ�˹�ϵ
            this._myPatientInfo.PVisit.PatientLocation.Bed.ID = string.Empty;//this.cmbBedNo.Tag.ToString();//������
            if (this.cmbPreDoc.Tag != null)
                this._myPatientInfo.PVisit.AdmittingDoctor.ID = this.cmbPreDoc.Tag.ToString();//ԤԼҽ��
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            
            // {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            if (this.txtInDiagnose.Tag != null)
            {
                obj.ID = this.txtInDiagnose.Tag.ToString();
                obj.Name = this.txtInDiagnose.Text;
            }
            else
            {
                obj.ID = "MS999";
                obj.Name = this.txtInDiagnose.Text;
            }
            this._myPatientInfo.Diagnoses.Add(obj);//��Ժ���
            this._myPatientInfo.MSDiagnoses = this.neuTextBox1.Text;//�������
            this._myPatientInfo.ClinicDiagnose = this.txtInDiagnose.Text; ;//�����������
            this._myPatientInfo.SSN = this.txtSSN.Text;//ҽ��֤��

            _myPatientInfo.FT.PrepayCost = this.numericUpDownForegift.Value;
            _myPatientInfo.Memo = this.txtMemo.Text;

            _myPatientInfo.PVisit.Circs.ID = this.cmbCircs.Tag.ToString();//��Ժ���
            _myPatientInfo.PVisit.Circs.Name = this.cmbCircs.Text;//��Ժ���
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

            FS.HISFC.Models.RADT.PatientInfo patient = this.managerIntegrate.QueryPreInPatientInfoByCardNO(HappenNo, CarNo);
            if (patient.User02 == "2")
            {
                MessageBox.Show("�û����Ѱ���סԺ�Ǽǣ��޷�ȡ��ԤԼ��");
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            int iRet = this.managerIntegrate.UpdatePreInPatientState(CarNo, "1", HappenNo);
            if (iRet < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.Msg("ȡ��ԤԼ�Ǽ�ʧ��" + this.managerIntegrate.Err, 211);
                iRet = -1;
            }
            if (iRet == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("������¼�ѱ�ȡ��!");
                this.QueryData();
                iRet = -1;
            }
            if (iRet > 0)
            {

                #region addby xuewj 2010-3-15
                if (this._adt == null)
                {
                    this._adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
                }
                if (this._adt != null && patient != null)
                {
                    this._adt.CancelPreRegInpatient(patient);
                }
                #endregion

                FS.FrameWork.Management.PublicTrans.Commit();
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
        private FS.FrameWork.Models.NeuObject GetPactUnitByID(string strID)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

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
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            if (this.neuTabControl1.SelectedTab != this.tabPage2) return;

            FS.FrameWork.WinForms.Classes.Print p = new Print();

            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                p.PrintPreview(this.Panel1);
            }
            else
            {
                p.PrintPage(0, 0, Panel1);
                p.PrintPage(0, 0, Panel1);
            }
        }

        /// <summary>
        /// ������Ͽؼ���ʾ
        /// </summary>
        void SetLocation()
        {
            if (!this.ucDiagnose1.Visible)
            {
                return;
            }
            this.ucDiagnose1.Top = this.neuGroupBox2.Top;
            this.ucDiagnose1.Left = this.txtInDiagnose.Left;
            this.ucDiagnose1.Visible = true;
        }

        /// <summary>
        /// �������value
        /// </summary>
        void SetValue()
        {
            this.txtInDiagnose.Text = this.icdMgr.Name;
            this.txtInDiagnose.Tag = this.icdMgr.ID;
            this.ucDiagnose1.Visible = false;

            cmbNurseCell.Focus();
        }

        #endregion

        #region �¼�
        void ucPrepayIn_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;

            Init();
            LoadEvents();
            InitQuery();
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

        private void txtCardNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.txtCardNo.Text.Trim() == "")
                {
                    return;
                }
                string txtCardNo = this.txtCardNo.Text.Trim();

                this.Clear();

                FS.HISFC.Models.Account.AccountCard accountCardObj = new FS.HISFC.Models.Account.AccountCard();
                if (this._feeIntegrate.ValidMarkNO(txtCardNo, ref accountCardObj) <= 0)
                {
                    MessageBox.Show(this._feeIntegrate.Err);
                    return;
                }
                ArrayList arrPrein = this.managerIntegrate.GetPrepayInByCardNoAndDate(accountCardObj.Patient.PID.CardNO);

                if (arrPrein.Count != null && arrPrein.Count > 0)// {6BF1F99D-7307-4d05-B747-274D24174895}
                {
                    string strMessTip = "�û����Ѿ�����ԤԼ��Ϣ�����£�\r\n";
                    int i = 1;
                    foreach (FS.HISFC.Models.RADT.PatientInfo PatientInfo in arrPrein)
                    {
                         string doctName =  SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(PatientInfo.PVisit.AdmittingDoctor.ID);
                         string nurseDeptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(PatientInfo.PVisit.PatientLocation.NurseCell.ID);
                         strMessTip += i + ".ԤԼ���ڣ�" + PatientInfo.PVisit.InTime.ToShortDateString() + ";   ԤԼ���ң�" + PatientInfo.PVisit.PatientLocation.Dept.Name + ";   ԤԼ������" + nurseDeptName + ";   ԤԼҽ����" + doctName +"\r\n";
                         i++;
                    }

                    DialogResult dr = MessageBox.Show(strMessTip + "\r\n�Ƿ����°���ԤԼ��Ժ��", "��ʾ", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.No)
                    {
                        this.neuTabControl1.SelectedTab = this.tabPage2;
                        for (int index = 0; index < this.fpMainInfo_Sheet1.RowCount; index ++)
                        {
                            string cardNo = this.fpMainInfo_Sheet1.Cells[index, 1].Text;
                            if (cardNo == accountCardObj.Patient.PID.CardNO)
                            {
                                this.fpMainInfo.ActiveSheet.ActiveColumnIndex = index;
                                break;
                            }
                        }
                        return;
                    }
                }
                //this.txtCardNo.Text = accountCardObj.Patient.PID.CardNO;
                //this.txtName.Text = accountCardObj.Patient.Name;
                //this.cmbSex.Tag = accountCardObj.Patient.Sex.ID;
                //this.dtBirthday.Value = accountCardObj.Patient.Birthday;
                //this.txtSSN.Text = accountCardObj.Patient.SSN;
                //this.txtHomeAddr.Text = accountCardObj.Patient.AddressHome;

                this.SetPrepatient(accountCardObj.Patient, true);
            }
        }

        private void cmbPactUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strID = this.cmbPactUnit.Tag.ToString();
            FS.HISFC.Models.Base.PactInfo pact = new FS.HISFC.Models.Base.PactInfo();// {D5390E6B-48F5-45d3-B956-3BC952BDE1EA}
            pact = this.PactUnit.GetPactUnitInfoByPactCode(strID);
            this.cmbPayKind.Tag = pact.PayKind.ID;
            //this.cmbPayKind.Text = pact.PayKind.Name;
        }

        /// <summary>
        /// ͨ����ʿվ���˴�λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbNurseCell_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList arrBed = new ArrayList();
            string strID = this.cmbNurseCell.Tag.ToString();
            arrBed = bedMgr.GetUnoccupiedBed(this.cmbNurseCell.Tag.ToString());
            if (arrBed == null)
            {
                MessageBox.Show("��ѯ��ʿվ��λ��Ϣʧ�ܣ�\r\n\r\n" + bedMgr.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (arrBed.Count == 0)
            {
                lblNoBed.Visible = true;
            }
            else
            {
                lblNoBed.Visible = false;
            }

            this.cmbBedNo.AddItems(arrBed);
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��ǰ������
            int iRow = e.Row;
            //��ȡ�������
            string strNo = this.fpMainInfo_Sheet1.Cells[iRow, 0].Text.Trim();
            string strCardNo = this.fpMainInfo_Sheet1.Cells[iRow, 1].Text.Trim();
            //���ԤԼ�Ǽ�ʵ�巵�ظ�����
            this._myPatientInfo = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNo, strCardNo);
            this.SetPrepatient(_myPatientInfo, false);

            this.neuTabControl1.SelectedIndex = 0;
        }

        private void txtInDiagnose_TextChanged(object sender, EventArgs e)
        {
            if (this.ckShow.Checked)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
            {
                ucDiagnose1.Visible = false;
            }
            else
            {
                ucDiagnose1.Visible = true;
            }
            if (string.IsNullOrEmpty(this.txtInDiagnose.Text))
            {
                ucDiagnose1.Visible = false;
            }
            this.SetLocation();
            if (ucDiagnose1.Visible)
            {
                this.ucDiagnose1.Filter(this.txtInDiagnose.Text.Trim());
            }
        }

        private int ucDiagnose1_SelectItem(Keys key)
        {
            int result = this.ucDiagnose1.GetItem(ref icdMgr);
            if (result < 0) return -1;
            SetValue();
            return 1;
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                this.txtCardNo.Focus();
                this.txtCardNo.SelectAll();
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

            _dtPrepayIn.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("�������", str),
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
			    new DataColumn("����ʱ��", str),
            });
        }

        private void InitQuery()
        {
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
            this._dtPrepayIn.Clear();

            ArrayList arrPrein = this.managerIntegrate.QueryPreInPatientInfoByDateAndState(PrepayinState, begin, end);
            if (arrPrein == null) return;

            string strName = "", strStateName = "";
            foreach (FS.HISFC.Models.RADT.PatientInfo obj in arrPrein)
            {
                #region ȡ�Ա�����
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

                if (!string.IsNullOrEmpty(obj.Pact.ID))
                {
                    PactInfo pactInfo = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(obj.Pact.ID);
                    if (null != pactInfo) obj.Pact.Name = pactInfo.Name;
                }
                string strOperID = obj.User03.Substring(0, 6);
                string strOperName = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(strOperID);

                #endregion

                #region ��DataTable��������
                DataRow row = this._dtPrepayIn.NewRow();
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

                this._dtPrepayIn.Rows.Add(row);
                #endregion
            }

            _dvPrepayIn = new DataView(this._dtPrepayIn);
            this.fpMainInfo_Sheet1.DataSource = this._dvPrepayIn;
            this.initFp();
        }

        /// <summary>
        /// ����fp���
        /// </summary>
        private void initFp()
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

            this.fpMainInfo_Sheet1.Columns.Get(14).Width = 85F;
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
        /// Ԥ����Ժ֪ͨ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int PrintPreview(object sender, object neuObject)
        {
            string strNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            string cardNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            FS.HISFC.Models.RADT.PatientInfo p = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNO, cardNO);

            if (p == null)
            {
                MessageBox.Show("��ӡ��Ժ֪ͨ��ʱ����ѯ����ԤԼ��Ϣʧ�ܡ�\n" + this.managerIntegrate.Err);
                return -1;
            }

            this._iPrintInHosNotice.SetValue(p);
            this._iPrintInHosNotice.PrintView();

            return base.PrintPreview(sender, neuObject);
        }

        public int InitInterface()
        {
            //_iPrintInHosNotice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice)) as FS.HISFC.BizProcess.Interface.IPrintInHosNotice;
            _iPrintInHosNotice = new ucOutPatientNoticeIBORN();
            return 1;
        }

        private int PrintNotice()
        {
            string strNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;
            string cardNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 1].Text;
            FS.HISFC.Models.RADT.PatientInfo p = this.managerIntegrate.QueryPreInPatientInfoByCardNO(strNO, cardNO);

            if (p == null)
            {
                MessageBox.Show("��ӡ��Ժ֪ͨ��ʱ����ѯ����ԤԼ��Ϣʧ�ܡ�\n" + this.managerIntegrate.Err);
                return -1;
            }

            this._iPrintInHosNotice.SetValue(p);
            this._iPrintInHosNotice.Print();

            return 1;
        }

        #region IInterfaceContainer ��Ա
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] type = new Type[1];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice);

                return type;
            }
        }

        #endregion
        /// <summary>
        /// ���ݿ����Ҷ�Ӧ�Ĳ���
        /// </summary>
        /// <param name="deptStatCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public static ArrayList QueryNurseByDept(string deptCode)
        {
            FS.HISFC.BizLogic.Manager.DepartmentStatManager deptStatMgr = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

            ArrayList alNurse = new ArrayList();
            ArrayList al = deptStatMgr.LoadByParent("01", deptCode);
            if (al == null || al.Count == 0)
            {
                al = deptStatMgr.LoadByChildren("01", deptCode);
                if (al != null)
                {
                    foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                    {
                        alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.PardepCode, deptStat.PardepName, ""));
                    }
                }
            }
            else
            {
                foreach (FS.HISFC.Models.Base.DepartmentStat deptStat in al)
                {
                    alNurse.Add(new FS.FrameWork.Models.NeuObject(deptStat.DeptCode, deptStat.DeptName, ""));
                }
            }
            return alNurse;
        }
        /// <summary>
        ///  ��ʿվ�б�����ұ仯 {E9EC275C-F044-40f1-BDDA-0F17410983EB}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmbNurseCell.ClearItems();

            ArrayList alNurseCell = new ArrayList();

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = this.cmbDept.Tag.ToString();
            //alNurseCell = this.managerIntegrate.QueryNurseStationByDept(obj);

            alNurseCell = QueryNurseByDept(obj.ID);

            if (alNurseCell != null
                && alNurseCell.Count != 0)
            {
                this.cmbNurseCell.AddItems(alNurseCell);
                cmbNurseCell.SelectedIndex = 0;
            }
            else
            {
                cmbNurseCell.Text = "";

                cmbNurseCell.Tag = null;
            }
        }

        private void btSave_Click(object sender, EventArgs e)// {F6204EF5-F295-4d91-B81A-736A268DD394}
        {
            this.Save();
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("�Ƿ��ӡ��Ժ֪ͨ����", "��ʾ", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.PrintNotice();
            }

        }

        private void btQuery_Click(object sender, EventArgs e)
        {
            this.QueryData();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)// {9B75C463-0167-41d4-B42E-DB869D5EFC11}
        {

            if (e.KeyData == Keys.Enter)
            {

                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.txtCardNo.Text = frmQuery.PatientInfo.PID.CardNO;
                    txtCardNo_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
            }
        }

        private void ckShow_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckShow.Checked)
            {
                this.ucDiagnose1.Visible = false;
            }
        }

        private void txtInDiagnose_TextChanged_1(object sender, EventArgs e)
        {

        }





    }
}

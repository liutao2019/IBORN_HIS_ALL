using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.ShenZhen.BinHai.Report
{
    public partial class ucPatientFeeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientFeeQuery()
        {
            InitializeComponent();

            //��ϸ��Ϣ��ʾ����
            //this.frmPatientFeeByMinFee.Controls.Add(this.ucPatientFeeByMinFee);
            //this.frmPatientFeeByMinFee.Size = this.ucPatientFeeByMinFee.Size;
            //this.ucPatientFeeByMinFee.Dock = DockStyle.Fill;
            //this.frmPatientFeeByMinFee.StartPosition = FormStartPosition.CenterParent;
            //this.frmPatientFeeByMinFee.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            //this.frmPatientFeeByMinFee.Visible = false;
        }

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime BeginTime = System.DateTime.MaxValue;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime EndTime = System.DateTime.MaxValue;

        /// <summary>
        /// ��ϸ��Ϣ�ؼ�
        /// </summary>
        //FS.HISFC.Fee.ucPatientFeeByMinFee ucPatientFeeByMinFee = new FS.UFC.InpatientFee.Fee.ucPatientFeeByMinFee();

        /// <summary>
        /// ��ϸ��Ϣ����
        /// </summary>
        Form frmPatientFeeByMinFee = new Form();

        private bool Timefalg = true;

        private bool isAll = false;
        private bool isInvoiceNo = false;
        private string invoiceNo = string.Empty;
        
        /// <summary>
        /// סԺ���תҵ���
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        /// <summary>
        /// ���ҵ���
        /// </summary>
        FS.HISFC.BizLogic.HealthRecord.Diagnose diagnoseManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ��Ա��Ϣҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient feeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatient = new FS.HISFC.Models.RADT.PatientInfo();

        #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
        /// <summary>
        /// ���Ұ�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper departmentHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ��Ա������
        /// </summary>
        FS.FrameWork.Public.ObjectHelper employeeHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ��С���ð�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper constantHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();

        #region DataTalbe��ر���

        string pathNameMainInfo = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientMainInfo.xml";
        string pathNamePrepay = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientPrepay.xml";
        string pathNameFee = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFee.xml";
        string pathNameDrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDrugList.xml";
        string pathNameUndrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientUndrugList.xml";
        string pathNameBalance = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientBalance.xml";
        string pathNameDiagnose = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDiagnose.xml";
        string pathNameChange = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientChange.xml";
        string pathNameFeeclass = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFeeclass.xml";
        string pathNameDiagnoseList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDiagnoseList.xml";
        /// <summary>
        /// ��������Ϣ
        /// </summary>
        DataTable dtMainInfo = new DataTable();

        /// <summary>
        /// ��������Ϣ��ͼ
        /// </summary>
        DataView dvMainInfo = new DataView();

        /// <summary>
        /// ҩƷ��ϸ
        /// </summary>
        DataTable dtDrugList = new DataTable();

        /// <summary>
        /// ҩƷ��ϸ��ͼ
        /// </summary>
        DataView dvDrugList = new DataView();

        /// <summary>
        /// ��ҩƷ��Ϣ
        /// </summary>
        DataTable dtUndrugList = new DataTable();

        /// <summary>
        /// ��ҩƷ��Ϣ��ͼ
        /// </summary>
        DataView dvUndrugList = new DataView();

        /// <summary>
        /// Ԥ������Ϣ
        /// </summary>
        DataTable dtPrepay = new DataTable();

        /// <summary>
        /// Ԥ������ͼ
        /// </summary>
        DataView dvPrepay = new DataView();

        /// <summary>
        /// ���û�����Ϣ
        /// </summary>
        DataTable dtFee = new DataTable();

        /// <summary>
        /// ���û�����Ϣ��ͼ
        /// </summary>
        DataView dvFee = new DataView();

        /// <summary>
        /// �շ���Ŀ����
        /// </summary>
        DataTable dtFeeclass = new DataTable();

        /// <summary>
        /// �շ���Ŀ������ͼ
        /// </summary>
        DataView dvFeeclass = new DataView();

        /// <summary>
        /// ���Ժ�����Ϣ
        /// </summary>
        DataTable dtDiagnoseList = new DataTable();

        /// <summary>
        /// ���Ժ�����Ϣ��ͼ
        /// </summary>
        DataView dvDiagnoseList = new DataView();

        /// <summary>
        /// ���ý�����Ϣ
        /// </summary>
        DataTable dtBalance = new DataTable();

        /// <summary>
        /// ���ý�����Ϣ��ͼ
        /// </summary>
        DataView dvBalance = new DataView();

        /// <summary>
        /// �����Ϣ
        /// </summary>
        DataTable dtChange = new DataTable();
        /// <summary>
        /// �����Ϣ��ͼ
        /// </summary>
        DataView dvChange = new DataView();

        /// <summary>
        /// ҩƷ��ʱ��Ϣ��ͼ
        /// </summary>
        DataView dvDrugListTemp = new DataView();

        /// <summary>
        /// ��ҩƷ��ʱ��Ϣ��ͼ
        /// </summary>
        DataView dvUndrugListTemp = new DataView();

        string filterInput = "1=1";	//�������������
        #endregion

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        //{BEE371A5-5E62-41ab-B116-AB3840C70A9C}
        /// <summary>
        /// ���ν��������ϸ
        /// </summary>
        ArrayList alFeeItemLists = new ArrayList();

        /// <summary>
        /// �洢����ӡ��Ʊ
        /// </summary>
        ArrayList alBalanceListHead = new ArrayList();

        //{B7A6B07C-165A-4a7b-B53C-A959F765D94D}
        ArrayList alBalancePay = new ArrayList();

        /// <summary>
        /// ����
        /// </summary>
        //protected FS.FrameWork.Management.Transaction t;
        /// <summary>
        /// ҽ�ƴ����ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
       
        #endregion

        #region ˽�з���

        private void InitHashTable()
        {
            foreach (TabPage t in this.neuTabControl1.TabPages)
            {
                foreach (Control c in t.Controls)
                {
                    if (c is FarPoint.Win.Spread.FpSpread)
                    {
                        this.hashTableFp.Add(t, c);
                    }
                }
            }
            this.hashTableFp.Add(tabPage2, (Control)this.fpDrugList);
            this.hashTableFp.Add(tabPage3, (Control)this.fpUndrugList);
        }

        /// <summary>
        /// ��ʼ�� 
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init()
        {
            //��ʼ������
            if (this.InitDept() == -1)
            {
                return -1;
            }

            this.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(System.DateTime.Now.ToShortDateString() + " 00:00:00");
            this.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(System.DateTime.Now.ToShortDateString() + " 23:59:59"); ;

            //��ʼ����ͬ��λ
            if (this.InitPact() == -1)
            {
                return -1;
            }

            //��ʼ����Ժ״̬
            if (this.InitInState() == -1)
            {
                return -1;
            }

            if (this.InitDataTable() == -1)
            {
                return -1;
            }
            #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
            //��ʼ��������
            if (this.InitHelper() == -1)
            {
                return -1;
            }
            #endregion
            this.InitHashTable();
            this.InitContr();

            //FS.Report.InpatientFee.ucFinIpbPatientDayFee uc1 = new FS.Report.InpatientFee.ucFinIpbPatientDayFee();
            //uc1.Dock = DockStyle.Fill;
            //uc1.Visible = true;
            //this.tpDayFee.Controls.Add(uc1);

            return 1;
        }

        /// <summary>
        /// ��ʼ����Ժ״̬
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitInState()
        {
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "ȫ��";

            ArrayList inStateList = FS.HISFC.Models.RADT.InStateEnumService.List();

            inStateList.Add(objAll);

            this.cmbPatientState.AddItems(inStateList);

            return 1;
        }

        #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitHelper()
        {
            int rtnValue = 0;
            FS.HISFC.BizProcess.Integrate.Manager m = new FS.HISFC.BizProcess.Integrate.Manager();

            departmentHelper.ArrayObject = m.GetDepartment();
            employeeHelper.ArrayObject = m.QueryEmployeeAll();
            constantHelper.ArrayObject = m.QueryConstantList("MINFEE");

            return rtnValue;
        }
        #endregion

        /// <summary>
        /// ��ʼ����ͬ��λ
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        private int InitPact()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "ȫ��";

            ArrayList pactList = this.consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PACKUNIT);
            if (pactList == null)
            {
                MessageBox.Show(Language.Msg("���غ�ͬ��λ�б����!") + this.consManager.Err);

                return -1;
            }

            pactList.Add(objAll);

            findAll = pactList.IndexOf(objAll);

            this.cmbPact.AddItems(pactList);

            if (findAll >= 0)
            {
                this.cmbPact.SelectedIndex = findAll;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDept()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "ȫ��";

            ArrayList deptList = this.deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            if (deptList == null)
            {
                MessageBox.Show(Language.Msg("���ؿ����б����!") + this.deptManager.Err);

                return -1;
            }

            deptList.Add(objAll);

            findAll = deptList.IndexOf(objAll);

            this.cmbDept.AddItems(deptList);
            this.cmbHosDept1.AddItems(deptList);
            this.cmbHosDept2.AddItems(deptList);

            if (findAll >= 0)
            {
                this.cmbDept.SelectedIndex = findAll;
                this.cmbHosDept1.SelectedIndex = findAll;
                this.cmbHosDept2.SelectedIndex = findAll;
            }

            dtpBeginTime.Text = System.DateTime.Now.ToShortDateString() + " 00:00:00";
            dtpEndTime.Text = System.DateTime.Now.ToShortDateString() + " 23:59:59";
            return 1;
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="patients">������Ϣ�б�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int QueryPatient(ArrayList patients)
        {
            this.Clear();

            this.dtMainInfo.Rows.Clear();

            Cursor.Current = Cursors.WaitCursor;
            //סԺ������Ϣ
            foreach (FS.HISFC.Models.RADT.PatientInfo patient in patients)
            {
                this.SetPatientToFpMain(patient);
            }

            Cursor.Current = Cursors.Arrow;

            if (patients.Count == 1)
            {
                this.currentPatient = (FS.HISFC.Models.RADT.PatientInfo)patients[0];
                this.SetPatientInfo();
                if (this.currentPatient.PVisit.InState.ID.Equals("O"))
                {
                    this.toolBarService.SetToolButtonEnabled("ȡ��ҽ������", false);
                }
                else
                {
                    this.toolBarService.SetToolButtonEnabled("ȡ��ҽ������", true);
                }

                this.ucQueryInpatientNo1.Text = this.currentPatient.ID.Substring(4);
                //���ò�ѯʱ��
                //���ò�ѯʱ��
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                this.QueryAllInfomaition(beginTime, endTime);
            }
            fpMainInfo_Sheet1.DataSource = dtMainInfo;
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);

            return 1;
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="patient">�ɹ� 1 ʧ�� -1</param>
        private void SetPatientToFpMain(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //����������
            FS.HISFC.BizLogic.Manager.Constant conMger = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject();
            neuObj = conMger.GetConstant("SZPERSONTYPE", patient.PVisit.MedicalType.ID);
           

            DataRow row = this.dtMainInfo.NewRow();

            try
            {

                row["סԺ��ˮ��"] = patient.ID;
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["סԺ����"] = patient.PVisit.PatientLocation.Dept.Name;
                row["����"] = patient.PVisit.PatientLocation.Bed.ID;
                if (patient.Pact.ID == "1")
                    row["�������"] = patient.Pact.Name;
                else
                    row["�������"] = neuObj.Name;
                row["Ԥ����(δ��)"] = patient.FT.PrepayCost;
                row["���úϼ�(δ��)"] = patient.FT.TotCost;
                row["���"] = patient.FT.LeftCost;
                row["�Է�"] = patient.FT.OwnCost;
                row["�Ը�"] = patient.FT.PayCost;
                row["����"] = patient.FT.PubCost;
                row["��Ժ����"] = patient.PVisit.InTime;
                row["��Ժ״̬"] = patient.PVisit.InState.Name;

                row["��Ժ����"] = patient.PVisit.OutTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : patient.PVisit.OutTime.ToString();

                row["Ԥ����(�ѽ�)"] = patient.FT.BalancedPrepayCost;
                row["���úϼ�(�ѽ�)"] = patient.FT.BalancedCost;
                if (patient.Pact.ID == "2" && patient.ExtendFlag2 == "3")
                    row["סԺ���"] = "����";
                else row["סԺ���"] = "��ͨ";
                row["��������"] = patient.BalanceDate;
                row["�������"] = patient.ClinicDiagnose;
                row["��ע"] ="" ;//patient.MemoInfo;

                this.dtMainInfo.Rows.Add(row);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);

                return;
            }
        }

        /// <summary>
        /// ���������סԺ�Ų�ѯ���߻�����Ϣ
        /// </summary>
        private void QueryPatientByInpatientNO()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("�������סԺ�Ŵ���,����������!"));
                this.Clear();

                return;
            }

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.radtManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("���߻��߻�����Ϣ����!") + this.radtManager.Err);
                this.Clear();
                return;
            }

            this.Clear();

            this.txtName.Text = patientInfo.Name;
            this.btnQuery.Focus();

            ArrayList patientListTemp = new ArrayList();

            patientListTemp.Add(patientInfo);

            this.QueryPatient(patientListTemp);
        }

        /// <summary>
        /// ��û���ҩƷ��ϸ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientDrugList(DateTime beginTime, DateTime endTime)
        {
            ArrayList drugList = new ArrayList();
            if (this.cmbHosDept1.Text == "ȫ��")
            {
                drugList = this.feeManager.GetMedItemsForInpatient(this.currentPatient.ID, beginTime, endTime);
            }
            else
            {
              //  drugList = this.feeManager.GetMedItemsForInpatientByDept(this.currentPatient.ID, beginTime, endTime, this.cmbHosDept1.Tag.ToString());
            }
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("��û���ҩƷ��ϸ����!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in drugList)
            {
                DataRow row = dtDrugList.NewRow();

                row["ҩƷ����"] = obj.Item.Name;
                FS.HISFC.Models.Pharmacy.Item medItem = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
                row["���"] = medItem.Specs;
                row["����"] = obj.Item.Price;
                row["����"] = obj.Item.Qty;
                row["����"] = obj.Days;
                row["��λ"] = obj.Item.PriceUnit;
                row["���"] = obj.FT.TotCost;
                row["�Է�"] = obj.FT.OwnCost;
                row["����"] = obj.FT.PubCost;
                row["�Ը�"] = obj.FT.PayCost;
                row["�Ż�"] = obj.FT.RebateCost;

                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["ִ�п���"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                //row["���߿���"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                row["ִ�п���"] = this.departmentHelper.GetName(obj.ExecOper.Dept.ID);
                row["���߿���"] = this.departmentHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID);
                #endregion
                row["�շ�ʱ��"] = obj.FeeOper.OperTime;

                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as FS.HISFC.Models.Base.Employee;
                //��̬��ʱ���Ȼ����ֲ鲻������
                if (empl == null || empl.Name == string.Empty)
                #endregion
                {
                    row["�շ�Ա"] = obj.FeeOper.ID;
                }
                else
                {
                    row["�շ�Ա"] = empl.Name;
                }


                row["��ҩʱ��"] = obj.ExecOper.OperTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.ExecOper.OperTime.ToString();

                FS.HISFC.Models.Base.Employee confirmOper = new FS.HISFC.Models.Base.Employee();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //confirmOper = managerIntergrate.GetEmployeeInfo(obj.ExecOper.ID);
                confirmOper = employeeHelper.GetObjectFromID(obj.ExecOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion
                if (confirmOper == null || confirmOper.Name == string.Empty)
                {
                    row["��ҩԱ"] = obj.ExecOper.ID;
                }
                else
                {
                    row["��ҩԱ"] = confirmOper.Name;
                }

                //row["��Դ"] = obj.FTSource;

                dtDrugList.Rows.Add(row);
            }
            dvDrugListTemp = new DataView(dtDrugList);
            this.AddSumInfo(dtDrugList, "ҩƷ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
            DrugListred();
        }

        /// <summary>
        /// ��ѯ���߷�ҩƷ��ϸ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientUndrugList(DateTime beginTime, DateTime endTime)
        {
            ArrayList undrugList =new ArrayList();
            if (this.cmbHosDept2.Text == "ȫ��")
            {
                undrugList = this.feeManager.QueryFeeItemLists(this.currentPatient.ID, beginTime, endTime);
            }
            else
            {
              //  undrugList = this.feeManager.QueryFeeItemListsByDept(this.currentPatient.ID, beginTime, endTime, this.cmbHosDept2.Tag.ToString());
            }
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷�ҩƷ��ϸ����!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in undrugList)
            {
                DataRow row = dtUndrugList.NewRow();

                row["��Ŀ����"] = obj.Item.Name;
                row["����"] = obj.Item.Price;
                row["����"] = obj.Item.Qty;
                row["��λ"] = obj.Item.PriceUnit;
                row["���"] = obj.FT.TotCost;
                row["�Է�"] = obj.FT.OwnCost;
                row["����"] = obj.FT.PubCost;
                row["�Ը�"] = obj.FT.PayCost;
                row["�Ż�"] = obj.FT.RebateCost;
                row["�շ�ʱ��"] = obj.FeeOper.OperTime;

                //�տ�Ա����
                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion

                if (empl == null || empl.Name == string.Empty)
                {
                    row["�շ�Ա"] = obj.FeeOper.ID;
                }
                else
                {
                    row["�շ�Ա"] = empl.Name;
                }

                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["ִ�п���"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                //row["���߿���"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                row["ִ�п���"] = this.departmentHelper.GetName(obj.ExecOper.Dept.ID);
                row["���߿���"] = this.departmentHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID);
                #endregion
                //row["��Դ"] = obj.FTSource;

                dtUndrugList.Rows.Add(row);
            }
            dvUndrugListTemp = new DataView(dtUndrugList);
            this.AddSumInfo(dtUndrugList, "��Ŀ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
            UndrugListred();
        }

        /// <summary>
        /// ��ѯ����Ԥ������Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientPrepayList(DateTime beginTime, DateTime endTime)
        {
            ArrayList prepayList = this.feeManager.QueryPrepays(this.currentPatient.ID);
            if (prepayList == null)
            {
                MessageBox.Show(Language.Msg("��û���Ԥ������ϸ����!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
            {
                FS.FrameWork.Models.NeuObject Objprepay = new FS.FrameWork.Models.NeuObject();
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                FS.HISFC.Models.Base.Department deptObj = new FS.HISFC.Models.Base.Department();
                DataRow row = dtPrepay.NewRow();

                row["Ʊ�ݺ�"] = prepay.RecipeNO;
                row["Ԥ�����"] = prepay.FT.PrepayCost;
                row["֧����ʽ"] = consManager.GetConstant("PAYMODES",prepay.PayType.ID).Name;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //   employeeObj = this.personManager.GetPersonByID(prepay.PrepayOper.ID);
                employeeObj = this.employeeHelper.GetObjectFromID(prepay.PrepayOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion
                row["����Ա"] = employeeObj.Name;
                row["��������"] = prepay.PrepayOper.OperTime;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //deptObj = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID);
                deptObj = this.departmentHelper.GetObjectFromID(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID)
                    as FS.HISFC.Models.Base.Department;
                #endregion
                row["���ڿ���"] = deptObj.Name;
                string tempBalanceStatusName = string.Empty;
                switch (prepay.BalanceState)
                {
                    case "0":
                        tempBalanceStatusName = "δ����";
                        break;
                    case "1":
                        tempBalanceStatusName = "�ѽ���";
                        break;
                    case "2":
                        tempBalanceStatusName = "�ѽ�ת";
                        break;
                }
                row["����״̬"] = tempBalanceStatusName;
                string tempPrepayStateName = string.Empty;
                switch (prepay.PrepayState)
                {
                    case "0":
                        tempPrepayStateName = "��ȡ";
                        break;
                    case "1":
                        tempPrepayStateName = "����";
                        break;
                    case "2":
                        tempPrepayStateName = "����";
                        break;
                }

                //row["��Դ"] = tempPrepayStateName;

                dtPrepay.Rows.Add(row);
            }

            this.AddSumInfo(dtPrepay, "Ʊ�ݺ�", "�ϼ�:", "Ԥ�����");

            dvPrepay.Sort = "Ʊ�ݺ� ASC";
        }

        /// <summary>
        /// ����շ���Ŀ����
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientFeeclass(DateTime beginTime, DateTime endTime)
        {
            ArrayList feeclass = this.feeManager.QueryFeeItemListSum(this.currentPatient.ID, beginTime, endTime, "");
            if (feeclass == null)
            {
                MessageBox.Show(Language.Msg("��û����շ���Ŀ����!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in feeclass)
            {
                DataRow row = dtFeeclass.NewRow();

                row["��Ŀ����"] = obj.Item.Name;
                row["���"] = obj.Item.Specs;
                row["����"] = obj.Item.Price;
                row["����"] = obj.Item.Qty;
                row["��λ"] = obj.Item.PriceUnit;
                row["���"] = obj.FT.TotCost;
                row["�Է�"] = obj.FT.OwnCost;
                row["����"] = obj.FT.PubCost;
                row["�Ը�"] = obj.FT.PayCost;
                row["�Ż�"] = obj.FT.RebateCost;




                //row["��Դ"] = obj.FTSource;

                dtFeeclass.Rows.Add(row);
            }

            this.AddSumInfo(dtFeeclass, "��Ŀ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
            deleteqltisnull();
        }



        /// <summary>
        /// ���Ժ�����Ϣ
        /// </summary>
        private void QueryPatientDiagnoseList()
        {
            ArrayList diagnoselist = this.diagnoseManager.QueryDiagnoseNoByPatientNo(this.currentPatient.ID);
            if (diagnoseManager == null)
            {
                MessageBox.Show(Language.Msg("��û����շ���Ŀ����!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.HealthRecord.Diagnose dg in diagnoselist)
            {
                DataRow row = dtDiagnoseList.NewRow();
                row["סԺ��ˮ��"] = dg.DiagInfo.Patient.ID;
                row["�������"] = dg.DiagInfo.HappenNo;
                row["���ICD��"] = dg.DiagInfo.ICD10.ID;
                row["�������"] = dg.DiagInfo.ICD10.Name;
                row["�������"] = dg.DiagInfo.DiagDate;
                row["ҽʦ����"] = dg.DiagInfo.Doctor.Name;
                row["��Ժ����"] = dg.Pvisit.InTime;
                row["��Ժ����"] = dg.Pvisit.OutTime;
                row["����Ա"] = dg.ID;
                row["����ʱ��"] = dg.OperInfo.OperTime;



                dtDiagnoseList.Rows.Add(row);
            }
        }

        /// <summary>
        /// ��û���ָ��ʱ����ڵ���С���û�����Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientFeeInfo(DateTime beginTime, DateTime endTime)
        {
            ArrayList feeInfoList = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "0");
            if (feeInfoList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��û�����ϸ����!") + this.feeManager.Err);

                return;
            }
            this.alBalanceListHead.Clear();
            ArrayList alFeeState = new ArrayList();
            alFeeState = feeCodeStat.QueryFeeCodeStatByReportCode("ZY01");
            //feeInfoList.AddRange(feeInfoListBalanced);
            SortedList alBlance = new SortedList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoList)
            {

                DataRow row = dtFee.NewRow();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["��������"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);

                row["��������"] = this.constantHelper.GetName(feeInfo.Item.MinFee.ID);
                #endregion
                row["���"] = feeInfo.FT.TotCost;
                row["�Է�"] = feeInfo.FT.OwnCost;
                row["����"] = feeInfo.FT.PubCost;
                row["�Ը�"] = feeInfo.FT.PayCost;
                row["�Żݽ��"] = feeInfo.FT.RebateCost;
                string temp = string.Empty;

                //if (feeInfo.BalanceState == "0")
                //{
                //    temp = "δ����";
                //}
                //else
                //{
                //    temp = "�ѽ���";
                //}
                row["����״̬"] = "δ����";
                row["���ô���"] = feeInfo.Item.MinFee.ID.ToString();

                dtFee.Rows.Add(row);
                #region �嵥��ӡ��������
                FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListAdd = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                //ʵ�帳ֵ

                FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
                objFeeStat = this.GetFeeStatByFeeCode(feeInfo.Item.MinFee.ID, alFeeState);
                if (objFeeStat == null)
                {
                    string feeName = "";
                    feeName = this.feeManager.GetMinFeeNameByCode(feeInfo.Item.MinFee.ID);
                    this.feeManager.Err = "��ά����Ʊ��������С����Ϊ" + feeName + "�ķ�Ʊ��Ŀ";
                    return;
                }

                balanceListAdd.FeeCodeStat.StatCate.ID = objFeeStat.ID;
                balanceListAdd.FeeCodeStat.StatCate.Name = objFeeStat.Name;

                //{467854CA-FAE4-4adf-9003-1E7B00F8456B} By JinHe
                balanceListAdd.FeeCodeStat.MinFee.ID = feeInfo.Item.MinFee.ID;

                balanceListAdd.FeeCodeStat.SortID = int.Parse(objFeeStat.Memo);


                balanceListAdd.BalanceBase.FT.TotCost = feeInfo.FT.TotCost;
                balanceListAdd.BalanceBase.FT.OwnCost = feeInfo.FT.OwnCost;
                balanceListAdd.BalanceBase.FT.PayCost = feeInfo.FT.PayCost;
                balanceListAdd.BalanceBase.FT.PubCost = feeInfo.FT.PubCost;
                //balanceListAdd.BalanceBase.Invoice.ID = invoiceNo;
                balanceListAdd.BalanceBase.Patient.IsBaby = this.currentPatient.IsBaby;
                //balanceListAdd.ID = balNo.ToString();
                //balanceListAdd.BalanceBase.ID = balNo.ToString();
                balanceListAdd.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balanceListAdd.BalanceBase.BalanceOper.ID = this.feeManager.Operator.ID;
                balanceListAdd.BalanceBase.BalanceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.feeManager.GetSysDate());
                balanceListAdd.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                //if (feeInfo.FT.TotCost != 0)
                //{
                //    alBlance.Add(objFeeStat.ID, balanceListAdd);
                //}
                this.alBalanceListHead.Add(balanceListAdd);
                #endregion
            }
            //if (alBlance.Count > 0)
            //{
            //    this.alBalanceListHead.Clear();
            //    foreach (FS.HISFC.Models.Fee.Inpatient.BalanceList al in alBlance.Values)
            //    {
            //        this.alBalanceListHead.Add(al);
            //    }
            //    alBlance.Clear();
            //}
            ArrayList feeInfoListBalanced = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "1");
            if (feeInfoListBalanced == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��û�����ϸ����!") + this.feeManager.Err);

                return;
            }
    

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoListBalanced)
            {

                DataRow row = dtFee.NewRow();

                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["��������"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                row["��������"] = this.constantHelper.GetName(feeInfo.Item.MinFee.ID);
                #endregion
                row["���"] = feeInfo.FT.TotCost;
                row["�Է�"] = feeInfo.FT.OwnCost;
                row["����"] = feeInfo.FT.PubCost;
                row["�Ը�"] = feeInfo.FT.PayCost;
                row["�Żݽ��"] = feeInfo.FT.RebateCost;
                string temp = string.Empty;

                //if (feeInfo.BalanceState == "0")
                //{
                //    temp = "δ����";
                //}
                //else
                //{
                //    temp = "�ѽ���";
                //}
                row["����״̬"] = "�ѽ���";
                row["���ô���"] = feeInfo.Item.MinFee.ID.ToString();

                dtFee.Rows.Add(row);
                #region �嵥��ӡ��������
                FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListAdd = new FS.HISFC.Models.Fee.Inpatient.BalanceList();
                //ʵ�帳ֵ

                FS.FrameWork.Models.NeuObject objFeeStat = new FS.FrameWork.Models.NeuObject();
                objFeeStat = this.GetFeeStatByFeeCode(feeInfo.Item.MinFee.ID, alFeeState);
                if (objFeeStat == null)
                {
                    string feeName = "";
                    feeName = this.feeManager.GetMinFeeNameByCode(feeInfo.Item.MinFee.ID);
                    this.feeManager.Err = "��ά����Ʊ��������С����Ϊ" + feeName + "�ķ�Ʊ��Ŀ";
                    return;
                }

                balanceListAdd.FeeCodeStat.StatCate.ID = objFeeStat.ID;
                balanceListAdd.FeeCodeStat.StatCate.Name = objFeeStat.Name;

                //{467854CA-FAE4-4adf-9003-1E7B00F8456B} By JinHe
                balanceListAdd.FeeCodeStat.MinFee.ID = feeInfo.Item.MinFee.ID;

                balanceListAdd.FeeCodeStat.SortID = int.Parse(objFeeStat.Memo);


                balanceListAdd.BalanceBase.FT.TotCost = feeInfo.FT.TotCost;
                balanceListAdd.BalanceBase.FT.OwnCost = feeInfo.FT.OwnCost;
                balanceListAdd.BalanceBase.FT.PayCost = feeInfo.FT.PayCost;
                balanceListAdd.BalanceBase.FT.PubCost = feeInfo.FT.PubCost;
                //balanceListAdd.BalanceBase.Invoice.ID = invoiceNo;
                balanceListAdd.BalanceBase.Patient.IsBaby = this.currentPatient.IsBaby;
                //balanceListAdd.ID = balNo.ToString();
                //balanceListAdd.BalanceBase.ID = balNo.ToString();
                balanceListAdd.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                balanceListAdd.BalanceBase.BalanceOper.ID = this.feeManager.Operator.ID;
                balanceListAdd.BalanceBase.BalanceOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.feeManager.GetSysDate());
                balanceListAdd.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                //if (feeInfo.FT.TotCost != 0)
                //{
                //    alBlance.Add(objFeeStat.ID, balanceListAdd);
                //}
                this.alBalanceListHead.Add(balanceListAdd);
                #endregion

            }
            this.AddSumInfo(dtFee, "��������", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Żݽ��");
        }

        /// <summary>
        /// ��û��߽�����Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientBalance(DateTime beginTime, DateTime endTime)
        {
            ArrayList balanceList = this.feeManager.QueryBalancesByInpatientNO(this.currentPatient.ID);
            if (balanceList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��ý������!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.Balance balance in balanceList)
            {
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                string temp = "";
                DataRow row = dtBalance.NewRow();

                row["��Ʊ����"] = balance.Invoice.ID;
                row["Ԥ�����"] = balance.FT.PrepayCost;
                row["�ܽ��"] = balance.FT.TotCost;
                row["�Է�"] = balance.FT.OwnCost;
                row["����"] = balance.FT.PubCost;
                row["�Ը�"] = balance.FT.PayCost;
                row["�Ż�"] = balance.FT.RebateCost;
                row["�������"] = balance.FT.ReturnCost;
                row["���ս��"] = balance.FT.SupplyCost;
                row["����ʱ��"] = balance.BalanceOper.OperTime;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //employeeObj = this.personManager.GetPersonByID(balance.BalanceOper.ID); 
                employeeObj = this.employeeHelper.GetObjectFromID(balance.BalanceOper.ID) as FS.HISFC.Models.Base.Employee;
                #endregion

                row["����Ա"] = employeeObj.Name;
                row["��������"] = balance.BalanceType.Name;

                switch (balance.CancelType)
                {
                    case FS.HISFC.Models.Base.CancelTypes.Valid:
                        temp = "��������";
                        break;
                    case FS.HISFC.Models.Base.CancelTypes.LogOut:
                        temp = "�����ٻ�";
                        break;
                    case FS.HISFC.Models.Base.CancelTypes.Reprint:
                        temp = "��Ʊ�ش�";
                        break;

                }
                row["����״̬"] = temp;

                dtBalance.Rows.Add(row);
            }

            AddSumInfo(dtBalance, "��Ʊ����", "�ϼ�:", "�ܽ��", "�Է�", "����", "�Ը�", "�Ż�", "�������", "���ս��");
        }

        /// <summary>
        /// ��û��߱����Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientChange(DateTime beginTime, DateTime endTime)
        {
            ArrayList changeList = this.radtManager.QueryPatientShiftInfoNew(this.currentPatient.ID);
            if (changeList == null)
            {
                MessageBox.Show(Language.Msg("��û��߱����Ϣ����!") + this.radtManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Invalid.CShiftData change in changeList)
            {
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                DataRow row = this.dtChange.NewRow();
                row["���"] = change.HappenNo.ToString();
                row["���ԭ��"] = change.ShitCause;
                row["ԭ����"] = change.OldDataCode;
                row["ԭ����"] = change.OldDataName;
                row["�ֱ���"] = change.NewDataCode;
                row["������"] = change.NewDataName;
                row["��ע"] = change.Mark;
                row["�����˱���"] = change.OperCode;
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //employeeObj = this.personManager.GetPersonByID(change.OperCode);
                employeeObj = this.employeeHelper.GetObjectFromID(change.OperCode) as FS.HISFC.Models.Base.Employee;
                #endregion
                if (employeeObj == null)
                {//{9205BD4C-36B9-4b64-A875-E1969D52BFBE}
                    //�Ҳ�����Ӧ��ҽ������
                    row["����������"] = change.OperCode;
                }
                else
                {
                    row["����������"] = employeeObj.Name;
                }
                row["����ʱ��"] = change.Memo;

                this.dtChange.Rows.Add(row);
            }
        }

        /// <summary>
        /// ��Ӻϼ�
        /// </summary>
        /// <param name="table">��ǰDataTalbe</param>
        /// <param name="totName">�ϼƵ�����λ��</param>
        /// <param name="disName">�ϼ�������</param>
        /// <param name="sumColName">ͳ���е�����</param>
        public void AddSumInfo(DataTable table, string totName, string disName, params string[] sumColName)
        {
            DataRow sumRow = table.NewRow();

            sumRow[totName] = disName;

            foreach (string s in sumColName)
            {
                object sum = table.Compute("SUM(" + s + ")", "");
                sumRow[s] = sum;
            }

            table.Rows.Add(sumRow);
        }

        /// <summary>
        /// ��������Ļ���������ѯ���߻�����Ϣ
        /// </summary>
        private void QueryPatientByName()
        {
            if (this.txtName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("������������Ϊ��!"));
                this.txtName.Focus();

                return;
            }

            string inputName = "%" + this.txtName.Text.Trim() + "%";
            //ȥ�������ַ�
            inputName = FS.FrameWork.Public.String.TakeOffSpecialChar(inputName, "'");
            //��������ֱ�Ӳ�ѯ������ϸ��Ϣ
            string name = this.txtName.Text;
            ArrayList patientListTemp = this.radtManager.QueryPatientInfoByName(inputName);
            if (patientListTemp == null || patientListTemp.Count == 0)
            {
                MessageBox.Show(Language.Msg("�޴˻�����Ϣ!") + this.radtManager.Err);

                this.Clear();
                this.txtName.Text = name;
                return;
            }

            if (patientListTemp.Count > 0)
            {
                this.Clear();
                this.txtName.Text = name;
                this.QueryPatient(patientListTemp);
            }

            return;
        }

        /// <summary>
        /// ��ʼ��DataTable
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDataTable()
        {
            Type str = typeof(String);
            Type date = typeof(DateTime);
            Type dec = typeof(Decimal);
            Type bo = typeof(bool);

            #region סԺ������Ϣ

            if (System.IO.File.Exists(pathNameMainInfo))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameMainInfo, dtMainInfo, ref dvMainInfo, this.fpMainInfo_Sheet1);

                dtMainInfo.PrimaryKey = new DataColumn[] { dtMainInfo.Columns["סԺ��ˮ��"] };

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);

            }
            else
            {

                dtMainInfo.Columns.AddRange(new DataColumn[]{new DataColumn("סԺ��ˮ��", str),
																new DataColumn("סԺ��", str),
																new DataColumn("����", str),
																new DataColumn("סԺ����", str),
																new DataColumn("����", str),
																new DataColumn("�������", str),
																new DataColumn("Ԥ����(δ��)", dec),
																new DataColumn("���úϼ�(δ��)", dec),
																new DataColumn("���", dec),
																new DataColumn("�Է�", dec),
																new DataColumn("�Ը�", dec),
																new DataColumn("����", dec),
																new DataColumn("��Ժ����", date),
																new DataColumn("��Ժ״̬", str),
																new DataColumn("��Ժ����", str),
																new DataColumn("Ԥ����(�ѽ�)", dec),
																new DataColumn("���úϼ�(�ѽ�)", dec),
                                                                new DataColumn("סԺ���", str),
																new DataColumn("��������", date),
                                                                new DataColumn("�������",str),
                                                                new DataColumn("��ע",str)
																});

                dtMainInfo.PrimaryKey = new DataColumn[] { dtMainInfo.Columns["סԺ��ˮ��"] };

                dvMainInfo = new DataView(dtMainInfo);

                this.fpMainInfo_Sheet1.DataSource = dvMainInfo;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);
            }

            #endregion

            #region ҩƷ��ϸ��Ϣ

            if (System.IO.File.Exists(pathNameDrugList))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameDrugList, dtDrugList, ref dvDrugList, this.fpDrugList_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpDrugList_Sheet1, pathNameDrugList);
            }
            else
            {
                dtDrugList.Columns.AddRange(new DataColumn[]{new DataColumn("ҩƷ����", str),
																new DataColumn("���", str),
																new DataColumn("����", dec),
																new DataColumn("����", dec),
																new DataColumn("����", dec),
																new DataColumn("��λ", str),
																new DataColumn("���", dec),
																new DataColumn("�Է�", dec),
																new DataColumn("����", dec),
																new DataColumn("�Ը�", dec),
																new DataColumn("�Ż�", dec),
																new DataColumn("ִ�п���",str),
																new DataColumn("���߿���",str),
																new DataColumn("�շ�ʱ��", str),
																new DataColumn("�շ�Ա", str),
																new DataColumn("��ҩʱ��", str),   
																new DataColumn("��ҩԱ", str),
				                                                new DataColumn("��Դ",str)});

                dvDrugList = new DataView(dtDrugList);

                this.fpDrugList_Sheet1.DataSource = dvDrugList;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpDrugList_Sheet1, pathNameDrugList);
            }

            #endregion

            #region ��ҩƷ��ϸ��Ϣ
            if (System.IO.File.Exists(pathNameUndrugList))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameUndrugList, dtUndrugList, ref dvUndrugList, this.fpUndrugList_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
            }
            else
            {
                dtUndrugList.Columns.AddRange(new DataColumn[]{new DataColumn("��Ŀ����", str),
																  new DataColumn("����", dec),
																  new DataColumn("����", dec),
																  new DataColumn("��λ", str),
																  new DataColumn("���", dec),
																  new DataColumn("�Է�", dec),
																  new DataColumn("����", dec),
																  new DataColumn("�Ը�", dec),
																  new DataColumn("�Ż�", dec),
																  new DataColumn("ִ�п���", str),
																  new DataColumn("���߿���",str),
																  new DataColumn("�շ�ʱ��", str),
																  new DataColumn("�շ�Ա", str),
				                                                  new DataColumn("��Դ", str)});

                dvUndrugList = new DataView(dtUndrugList);

                this.fpUndrugList_Sheet1.DataSource = dvUndrugList;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
            }

            #endregion

            #region �շ���Ŀ����

            if (System.IO.File.Exists(pathNameFeeclass))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFeeclass, dtFeeclass, ref dvFeeclass, this.fpFeeclass_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            }
            else
            {
                dtFeeclass.Columns.AddRange(new DataColumn[]{new DataColumn("��Ŀ����", str),
                                                                new DataColumn("���", str),
															    new DataColumn("����", dec),
																new DataColumn("����", dec),
																new DataColumn("��λ", str),
																new DataColumn("���", dec),
																new DataColumn("�Է�", dec),
																new DataColumn("����", dec),
																new DataColumn("�Ը�", dec),
																new DataColumn("�Ż�", dec),
																new DataColumn("ִ�п���",str),
																new DataColumn("���߿���",str),
																new DataColumn("�շ�Ա", str),
				                                                new DataColumn("��Դ",str)});

                dvFeeclass = new DataView(dtFeeclass);

                this.fpFeeclass_Sheet1.DataSource = dvFeeclass;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            }

            #endregion

            #region ���Ժ�����Ϣ
            //if (System.IO.File.Exists(pathNameDiagnoseList))
            //{
            //    FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameDiagnoseList, dtDiagnoseList, ref dvDiagnoseList, this.fpDiagnoseList_Sheet1);
            //    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnFormatProperty(this.fpDiagnoseList_Sheet1, pathNameDiagnoseList);
            //}
            //else
            {
                dtDiagnoseList.Columns.AddRange(new DataColumn[]{new DataColumn("סԺ��ˮ��",str),
                                                                new DataColumn("�������",str),
                                                                new DataColumn("���ICD��",str),
                                                                new DataColumn("�������",str),
                                                                new DataColumn("�������",date),
                                                                new DataColumn("ҽʦ����",str),
                                                                new DataColumn("��Ժ����",date),
                                                                new DataColumn("��Ժ����",date),
                                                                new DataColumn("����Ա",str),
                                                                new DataColumn("����ʱ��",str)});
                dvDiagnoseList = new DataView(dtDiagnoseList);
                this.fpDiagnoseList_Sheet1.DataSource = dvDiagnoseList;
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnFormatProperty(this.fpDiagnoseList_Sheet1, pathNameDiagnoseList);
            }
            #endregion

            #region Ԥ������Ϣ

            if (System.IO.File.Exists(pathNamePrepay))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNamePrepay, dtPrepay, ref dvPrepay, this.fpPrepay_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpPrepay_Sheet1, pathNamePrepay);
            }
            else
            {
                dtPrepay.Columns.AddRange(new DataColumn[]{new DataColumn("Ʊ�ݺ�", str),
															  new DataColumn("Ԥ�����", dec),
															  new DataColumn("֧����ʽ", str),
															  new DataColumn("����Ա", str),
															  new DataColumn("��������", date),
															  new DataColumn("���ڿ���", str),
															  new DataColumn("����״̬", str),
															  new DataColumn("��Դ", str)});

                dvPrepay = new DataView(dtPrepay);

                this.fpPrepay_Sheet1.DataSource = dvPrepay;
                dvPrepay.Sort = "Ʊ�ݺ� ASC";

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpPrepay_Sheet1, pathNamePrepay);
            }

            #endregion

            #region ��С������Ϣ

            if (System.IO.File.Exists(pathNameFee))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFee, dtFee, ref dvFee, this.fpFee_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }
            else
            {
                dtFee.Columns.AddRange(new DataColumn[]{new DataColumn("��������", str),
														   new DataColumn("���", dec),
														   new DataColumn("�Է�", dec),
														   new DataColumn("����", dec),
														   new DataColumn("�Ը�", dec),
														   new DataColumn("�Żݽ��", dec),
														   new DataColumn("����״̬", str),
                                                           new DataColumn("���ô���", str)});

                dvFee = new DataView(dtFee);

                this.fpFee_Sheet1.DataSource = dvFee;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }

            #endregion

            #region ������Ϣ
            if (System.IO.File.Exists(pathNameBalance))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameBalance, dtBalance, ref dvBalance, this.fpBalance_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpBalance_Sheet1, pathNameBalance);
            }
            else
            {
                dtBalance.Columns.AddRange(new DataColumn[]{new DataColumn("��Ʊ����", str),
															   new DataColumn("��������", str),
															   new DataColumn("����״̬", str),
															   new DataColumn("Ԥ�����", dec),
															   new DataColumn("�ܽ��", dec),
															   new DataColumn("�Է�", dec),
															   new DataColumn("����", dec),
															   new DataColumn("�Ը�", dec),
															   new DataColumn("�Ż�", dec),
															   new DataColumn("�������", dec),
															   new DataColumn("���ս��", dec),
															   new DataColumn("����ʱ��", date),
															   new DataColumn("����Ա", str)});

                dvBalance = new DataView(dtBalance);

                this.fpBalance_Sheet1.DataSource = dvBalance;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpBalance_Sheet1, pathNameBalance);
            }
            #endregion

            #region �����Ϣ
            if (System.IO.File.Exists(this.pathNameChange))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(this.pathNameChange, dtChange, ref dvChange, this.fpChange_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpChange_Sheet1, pathNameChange);
            }
            else
            {
                this.dtChange.Columns.AddRange(new DataColumn[]{new DataColumn("���", str),
															   new DataColumn("���ԭ��", str),
															   new DataColumn("ԭ����", str),
															   new DataColumn("ԭ����", str),
															   new DataColumn("�ֱ���", str),
															   new DataColumn("������", str),
															   new DataColumn("��ע", str),
															   new DataColumn("�����˱���", str),
															   new DataColumn("����������", str),
															   new DataColumn("����ʱ��", str)});

                this.dvChange = new DataView(dtChange);

                this.fpChange_Sheet1.DataSource = dvChange;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpChange_Sheet1, pathNameChange);
            }
            #endregion

            return 1;
        }

        private void deleteqltisnull()
        {
            for (int i = 0; fpFeeclass.Sheets[0].Cells[i, 3].Text.Length != 0; i++)
            {
                string str = string.Empty;
                str = fpFeeclass.ActiveSheet.Cells[i, 3].Text;
                double num = double.Parse(str);
                if (num <= 0)
                {
                    this.fpFeeclass.ActiveSheet.Rows.Remove(i, 1);
                    i--;
                }
            }
        }

        /// <summary>
        /// ҩƷ�˷��ú�ɫ������
        /// </summary>
        private void DrugListred()
        {


            //for (int i = 0; fpDrugList.Sheets[0].Cells[i, 4].Text.Length != 0; i++)
            //{
            //    string str = string.Empty;
            //    str = fpDrugList.ActiveSheet.Cells[i, 4].Text;
            //    double num = double.Parse(str);
            //    if (num <= 0)
            //    {
            //        fpDrugList.Sheets[0].Rows[i].ForeColor = Color.Red;
            //    }
            //}
        }
        /// <summary>
        /// ��ҩƷ�˷Ѻ�ɫ������
        /// </summary>
        private void UndrugListred()
        {

            //for (int i = 0; fpUndrugList.Sheets[0].Cells[i, 3].Text.Length != 0; i++)
            //{
            //    string str = string.Empty;
            //    str = fpUndrugList.ActiveSheet.Cells[i, 3].Text;
            //    double num = double.Parse(str);
            //    if (num <= 0)
            //    {
            //        fpUndrugList.Sheets[0].Rows[i].ForeColor = Color.Red;
            //    }
            //}
        }


        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        protected void SetPatientInfo()
        {
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                return;
            }

            this.lblID.Text = this.currentPatient.PID.PatientNO;//סԺ��
            this.lblName.Text = this.currentPatient.Name;//����;
            this.lblSex.Text = this.currentPatient.Sex.Name;
            this.lblAge.Text = this.currentPatient.Age;
            this.lblBed.Text = this.currentPatient.PVisit.PatientLocation.Bed.ID;
            this.lblDept.Text = this.currentPatient.PVisit.PatientLocation.Dept.Name;
            this.lblPact.Text = this.currentPatient.Pact.Name;//��ͬ��λ
            this.lblDateIn.Text = this.currentPatient.PVisit.InTime.ToShortDateString();//סԺ����
            if (this.currentPatient.PVisit.OutTime != DateTime.MinValue&&this.currentPatient.PVisit.OutTime!=new DateTime(0002,1,1))
            {
                this.lblOutDate.Text = this.currentPatient.PVisit.OutTime.ToShortDateString();
            }
            this.lblInState.Text = this.currentPatient.PVisit.InState.Name;//��Ժ״̬
            decimal TotCost = this.currentPatient.FT.TotCost + this.currentPatient.FT.BalancedCost;
            //this.lblTotCost.Text = this.currentPatient.FT.TotCost.ToString();
            this.lblTotCost.Text = TotCost.ToString();

            this.lblOwnCost.Text = this.currentPatient.FT.OwnCost.ToString();
            this.lblPubCost.Text = this.currentPatient.FT.PubCost.ToString();
            this.lblPrepayCost.Text = this.currentPatient.FT.PrepayCost.ToString();
            this.lblUnBalanceCost.Text = this.currentPatient.FT.TotCost.ToString();
            this.lblBalancedCost.Text = this.currentPatient.FT.BalancedCost.ToString();
            this.lblDiagnose.Text = this.currentPatient.MainDiagnose;
            this.lblMemo.Text = this.currentPatient.Memo;
            this.lblfreecost.Text = this.currentPatient.FT.LeftCost.ToString();
            if (this.currentPatient.CompanyName != null)
            {
                this.lblwork.Text = this.currentPatient.CompanyName.ToString();
            }
            else
            {
                this.lblwork.Text = "";
            }
            if (this.currentPatient.AddressHome != null)
            {
                this.lblhome.Text = this.currentPatient.AddressHome.ToString();
            }
            else
            {
                this.lblhome.Text = "";
            }
            if (this.currentPatient.PhoneHome != null)
            {
                this.lblTel.Text = this.currentPatient.PhoneHome.ToString();
            }
            else if (this.currentPatient.PhoneBusiness != null)
            {
                this.lblTel.Text = this.currentPatient.PhoneBusiness.ToString();
            }
            else
            {
                this.lblTel.Text = "";
            }

        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        protected void QueryAllInfomaition(DateTime beginTime, DateTime endTime)
        {
            this.QueryPatientDrugList(beginTime, endTime);

            this.QueryPatientUndrugList(beginTime, endTime);

            this.QueryPatientPrepayList(beginTime, endTime);

            this.QueryPatientFeeInfo(beginTime, endTime);

            this.QueryPatientBalance(beginTime, endTime);

            this.QueryPatientChange(beginTime, endTime);

            this.QueryPatientFeeclass(beginTime, endTime);
            this.QueryPatientDiagnoseList();
            string err = string.Empty;
            this.QueryFeeItemlist(this.currentPatient, beginTime, endTime, ref err);
        }

        /// <summary>
        /// ����"_"
        /// </summary>
        /// <param name="langth">����</param>
        /// <returns>�ɹ� "---" ʧ�� null</returns>
        private string RetrunSplit(int langth)
        {
            string s = string.Empty;

            for (int i = 0; i < langth; i++)
            {
                s += "_";
            }

            return s;
        }

        /// <summary>
        /// ���
        /// </summary>
        private void Clear()
        {
            this.lblID.Text = this.RetrunSplit(10);
            this.lblName.Text = this.RetrunSplit(5);
            this.lblSex.Text = this.RetrunSplit(4);
            this.lblAge.Text = this.RetrunSplit(4);
            this.lblBed.Text = this.RetrunSplit(10);
            this.lblDept.Text = this.RetrunSplit(10);
            this.lblPact.Text = this.RetrunSplit(10);
            this.lblDateIn.Text = this.RetrunSplit(10);
            this.lblOutDate.Text = this.RetrunSplit(10);

            this.lblInState.Text = this.RetrunSplit(6);
            this.lblTotCost.Text = this.RetrunSplit(10);
            this.lblOwnCost.Text = this.RetrunSplit(10);
            this.lblPubCost.Text = this.RetrunSplit(10);
            this.lblPrepayCost.Text = this.RetrunSplit(10);
            this.lblUnBalanceCost.Text = this.RetrunSplit(10);
            this.lblBalancedCost.Text = this.RetrunSplit(10);
            this.lblDiagnose.Text = this.RetrunSplit(20);
            this.lblMemo.Text = this.RetrunSplit(30);
            this.txtName.Text = string.Empty;
            this.lblfreecost.Text = string.Empty;
            this.lblwork.Text = string.Empty;
            this.lblTel.Text = string.Empty;
            this.lblhome.Text = string.Empty;
            dtMainInfo.Rows.Clear();
            dtDrugList.Rows.Clear();
            dtUndrugList.Rows.Clear();
            dtPrepay.Rows.Clear();
            dtFee.Rows.Clear();
            dtBalance.Rows.Clear();
            dtChange.Rows.Clear();
            dtFeeclass.Rows.Clear();
            dtDiagnoseList.Rows.Clear();
        }
        private void InitContr()
        {
            this.neuLabel21.Visible = true;
            this.lblOwnCost.Visible = true;
            this.neuLabel23.Visible = true;
            this.lblPubCost.Visible = true;
        }

        /// <summary>
        /// ��Ժ�����嵥��ӡ
        /// </summary>
        /// <param name="interfaceIndex">�ӿ���ţ�Ĭ��0��ϸ�嵥��1�����嵥��2ҽ���嵥,4ҽ�����㵥</param>
        /// <returns></returns>
        protected int BlanceListPrint(string interfaceIndex)
        {
           // FS.HISFC.BizProcess.Integrate.FeeInterface..FeeInterface.IBalanceListPrint p = null;
            FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy pSi = null;
            DateTime beginTime ;
            DateTime endTime ;
            if (Timefalg == true || this.isAll)
            {
                 beginTime = this.currentPatient.PVisit.InTime;
                 endTime = this.radtManager.GetDateTimeFromSysDateTime();
            }
            else
            {
                 currentPatient.User01 = "T";
                 currentPatient.User02 = BeginTime.ToShortDateString()+"  ��  " + EndTime.ToShortDateString();
                 beginTime = this.BeginTime;
                 endTime = this.EndTime;
            }            
            string err = "";
            this.QueryFeeItemlist(this.currentPatient, beginTime, endTime, ref err);


            long returnValue = 0;
            if (interfaceIndex == "4"&& currentPatient.Pact.ID =="2" && !currentPatient.PVisit.InState.ID.Equals("O"))
            {

                returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                //{BF6500FD-71FE-4cce-B328-D10CB7CBF22B}��Ӷ��� ע�⣺��ҪΪ�˶�ȡҽ������PreBalanceInpatientԤ�᷽��PreBalanceInpatient��Ӧ�ж�patient.SIMainInfo.Memo�Ƿ�Ϊ��
                //���Ϊ�մӱ���ȡ
                //returnValue = this.medcareInterfaceProxy.GetRegInfoInpatient(this.currentPatient);
                //if (returnValue != 1)
                //{
                //    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                //    return -1;
                //}

                this.currentPatient.SIMainInfo.SiEmployeeCode = "H0510";

                returnValue = this.medcareInterfaceProxy.PreBalanceInpatient(this.currentPatient, ref alFeeItemLists);
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }

                returnValue = this.medcareInterfaceProxy.Disconnect();
                if (returnValue != 1)
                {
                    MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);
                    return -1;
                }
            }
            else if (interfaceIndex == "4" && currentPatient.Pact.ID == "1")
            {
                MessageBox.Show("����ҽ������,���ܲ���ҽ�����㵥");
                return -1;
            
            }


            if (this.alFeeItemLists.Count > 0)
            {
                int balanceNO = 0;
                string invoiceNO = "";
                DateTime dtSys = DateTime.MinValue;
                dtSys = this.feeManager.GetDateTimeFromSysDateTime();
                //��ʼ��Balancelist�����Ա��γ�Ʊ����Ϣ
                if (interfaceIndex == "4")
                {
                    pSi = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy), FS.FrameWork.Function.NConvert.ToInt32(interfaceIndex)) as FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy;
                    pSi.PatientInfo = this.currentPatient;
                    if (this.alBalanceListHead.Count <= 0)
                        return 0;


                    if (pSi.SetValueForPrint(this.currentPatient, null, this.alBalanceListHead, this.alBalancePay) == -1)
                    {
                        this.alBalanceListHead = new ArrayList();
                        this.alBalancePay = new ArrayList();
                        return -1;
                    }

                    //����ӡ��
                    pSi.PrintPreview();

                }
                else
                {
                    //p = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceListPrint), FS.FrameWork.Function.NConvert.ToInt32(interfaceIndex)) as FS.HISFC.Integrate.FeeInterface.IBalanceListPrint;

                    //p.PatientInfo = this.currentPatient;
                    //p.BloodMinFeeCode = this.bloodMinFeeCode;

                    ////�޷�����Ϣ
                    //if (this.alBalanceListHead.Count <= 0)
                    //    return 0;

                    //if (p.SetValueForPreview(this.currentPatient, null, this.alBalanceListHead, alFeeItemLists, null) == -1)
                    //{
                    //    this.alBalanceListHead = new ArrayList();
                    //    return -1;
                    //}
                    ////����ӡ


                    //p.PrintPreview();
                }
               

                return 1;

            }
            return -1;
        }

        /// <summary>
        /// ��ȡ���߱��ν�����Ϣ
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="fromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        protected virtual int QueryFeeItemlist(FS.HISFC.Models.RADT.PatientInfo currentPatient, DateTime fromDate, DateTime ToDate, ref string errText)
        {
            //��ҩƷ��ϸ
            ArrayList alItemList = new ArrayList();

            //ҩƷ��ϸ
            ArrayList alMedicineList = new ArrayList();

            if (currentPatient.PVisit.InState.ID.Equals("O"))//�н�������
            {
                //��ѯ
               // alItemList = this.feeManager.QueryItemListstoBalance(currentPatient.ID, fromDate, ToDate);
                if (alItemList == null)
                {
                    errText = "��ѯ���߷�ҩƷ��Ϣ����" + this.feeManager.Err;
                    return -1;
                }

              //  alMedicineList = this.feeManager.QueryMedicineListstoBalance(currentPatient.ID, fromDate, ToDate);
                if (alMedicineList == null)
                {
                    errText = "��ѯ����ҩƷ��Ϣ����" + this.feeManager.Err;
                    return -1;
                }

            }
            else//��������
            {

                if (Timefalg == false)
                {
                    
                    //��ѯ
                    //if (this.isAll)
                    ////    alItemList = this.feeManager.QueryItemListsForBalanceByInvoiceNo(currentPatient.ID, "", "ALL");
                    //else if (this.isInvoiceNo)
                    ////    alItemList = this.feeManager.QueryItemListsForBalanceByInvoiceNo(currentPatient.ID, this.invoiceNo, "NO");
                    //else
                    //    alItemList = this.feeManager.QueryItemListsForBalance(currentPatient.ID, fromDate, ToDate);
                    if (alItemList == null)
                    {
                        errText = "��ѯ���߷�ҩƷ��Ϣ����" + this.feeManager.Err;
                        return -1;
                    }

                    //if (this.isAll)
                    //    alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID, "", "ALL");
                    //else if (this.isInvoiceNo)
                    //    alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID, this.invoiceNo, "NO");
                    //else
                        alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID, fromDate, ToDate);
                    if (alMedicineList == null)
                    {
                        errText = "��ѯ����ҩƷ��Ϣ����" + this.feeManager.Err;
                        return -1;
                    }
                }
                else
                {
                    //��ѯ
                    alItemList = this.feeManager.QueryItemListsForBalance(currentPatient.ID);
                    if (alItemList == null)
                    {
                        errText = "��ѯ���߷�ҩƷ��Ϣ����" + this.feeManager.Err;
                        return -1;
                    }

                    alMedicineList = this.feeManager.QueryMedicineListsForBalance(currentPatient.ID);
                    if (alMedicineList == null)
                    {
                        errText = "��ѯ����ҩƷ��Ϣ����" + this.feeManager.Err;
                        return -1;
                    }
                }
            }
            this.alFeeItemLists = new ArrayList();
            alFeeItemLists.Clear();
            //��ӻ�����Ϣ
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in alItemList)
            {
                this.alFeeItemLists.Add(item);
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList medicineList in alMedicineList)
            {
                this.alFeeItemLists.Add(medicineList);
            }

            return 1;

        }

        /// <summary>
        /// ͨ����С���û�ȡͳ�ƴ���memo���ӡ˳��
        /// </summary>
        /// <param name="feeCode"></param>
        /// <param name="alInvoice"></param>
        /// <returns></returns>
        protected FS.FrameWork.Models.NeuObject GetFeeStatByFeeCode(string feeCode, ArrayList al)
        {
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            FS.HISFC.Models.Fee.FeeCodeStat feeStat;

            for (int i = 0; i < al.Count; i++)
            {
                feeStat = (FS.HISFC.Models.Fee.FeeCodeStat)al[i];
                if (feeStat.MinFee.ID == feeCode)
                {
                    obj.ID = feeStat.StatCate.ID;

                    obj.Name = feeStat.StatCate.Name;
                    obj.Memo = feeStat.SortID.ToString().PadLeft(2,'0');
                    return obj;
                }
            }
            return null;
        }
        #endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            this.QueryPatientByInpatientNO();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            if (this.neuTabControl1.SelectedIndex == 9)
            {
                //FS.Report.InpatientFee.ucFinIpbPatientDayFee ucPatientDF = this.tpDayFee.Controls[0] as FS.Report.InpatientFee.ucFinIpbPatientDayFee;
                //ucPatientDF.PrintDW();
            }
            else
            {
                print.PrintPage(0, 0, this.neuTabControl1.SelectedTab);
            }

            return base.OnPrint(sender, neuObject);
        }
        //��ӡԤ��
        public override int PrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print printview = new FS.FrameWork.WinForms.Classes.Print();

            printview.PrintPreview(0, 0, this.neuTabControl1.SelectedTab);
            return base.OnPrintPreview(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            object obj = this.hashTableFp[this.neuTabControl1.SelectedTab];

            FarPoint.Win.Spread.FpSpread fp = obj as FarPoint.Win.Spread.FpSpread;

            SaveFileDialog op = new SaveFileDialog();

            op.Title = "��ѡ�񱣴��·��������";
            op.CheckFileExists = false;
            op.CheckPathExists = true;
            op.DefaultExt = "*.xls";
            op.Filter = "(*.xls)|*.xls";

            DialogResult result = op.ShowDialog();

            if (result == DialogResult.Cancel || op.FileName == string.Empty)
            {
                return -1;
            }

            string filePath = op.FileName;

            bool returnValue = fp.SaveExcel(filePath, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);


            return base.Export(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("��ӡ�����嵥", "���ü�������", FS.FrameWork.WinForms.Classes.EnumImageList.C��ѯ��ʷ, true, false, null);
            this.toolBarService.AddToolButton("��������嵥", "�����嵥", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            this.toolBarService.AddToolButton("������ϸ�嵥", "��ϸ�嵥", FS.FrameWork.WinForms.Classes.EnumImageList.M��ϸ, true, false, null);
            this.toolBarService.AddToolButton("ҽ���嵥", "ҽ���嵥", FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��, true, false, null);
            this.toolBarService.AddToolButton("����ҽ���嵥", "ҽ�������嵥", FS.FrameWork.WinForms.Classes.EnumImageList.Yҽ��, true, false, null);
            this.toolBarService.AddToolButton("ȡ���ϴ�", "ȡ��ҽ���ϴ�", FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            this.toolBarService.AddToolButton("ȡ��Ԥ����", "ȡ��Ԥ����", FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            this.toolBarService.AddToolButton("ȡ��ҽ������", "ȡ��ҽ������", FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "��ӡ�����嵥")
            {
                //ѡ��ʱ��Σ����û��ѡ��ͷ���
                //if (FS.FrameWork.WinForms.Classes.Function.ChooseDate(ref this.BeginTime, ref this.EndTime) == 0)
                //    return;

                if (this.currentPatient == null || string.IsNullOrEmpty(this.currentPatient.ID)) return;

                FS.FrameWork.WinForms.Forms.frmChooseDate frmDate = new FS.FrameWork.WinForms.Forms.frmChooseDate();
                if (frmDate.ShowDialog() == DialogResult.OK)
                {
                    this.BeginTime = frmDate.DateBegin;
                    this.EndTime = frmDate.DateEnd;
                    //this.isAll = frmDate;
                    //this.isInvoiceNo = frmDate.IsInvoiceNo;
                    //this.invoiceNo = frmDate.InvoiceNo;
                    Timefalg = false;
                    this.BlanceListPrint("0");
                }
                if (!frmDate.IsDisposed)
                    frmDate.Dispose();

                this.isAll = false;
                this.isInvoiceNo = false;
                this.invoiceNo = "";
            }

            if (e.ClickedItem.Text == "��������嵥")
            {
                Timefalg = true;
                this.BlanceListPrint("1");
            }
            else if (e.ClickedItem.Text == "������ϸ�嵥")
            {
                Timefalg = true;
                this.BlanceListPrint("0");
            }
            else if (e.ClickedItem.Text == "ҽ���嵥")
            {
                Timefalg = true;
                this.BlanceListPrint("2");
            }
            else if (e.ClickedItem.Text == "����ҽ���嵥")
            {
                Timefalg = true;
                this.BlanceListPrint("4");
            }
            if (e.ClickedItem.Text == "ȡ���ϴ�")
            {
                CancelUploadFee();
            }
            if (e.ClickedItem.Text == "ȡ��Ԥ����")
            {
                CancelPreBalance();
            
            }
            if (e.ClickedItem.Text == "ȡ��ҽ������")
            {
                CancelBalance(); 
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            //��Ϊ����סԺ�Ų�ѯ
            if (this.ucQueryInpatientNo1.Text.Length > 0)
            {
                this.ucQueryInpatientNo1_myEvent();
            }
            else//��Ϊ����������ѯ
            {
                this.QueryPatientByName();
            }
        }

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <returns></returns>
        private int CancelBalance()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }

            #region ҽ������



            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
            if (returnValue != 1)
            {

                // errText = this.medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                return -1;
            }

            currentPatient.SIMainInfo.User03 = "T"; //ȡ��Ԥ����
            //ȡ��ҽ���ϴ�����
            returnValue = this.medcareInterfaceProxy.CancelBalanceInpatient(currentPatient, ref alFeeItemLists);
            if (returnValue != 1)
            {
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                return -1;
            }
            #endregion

            MessageBox.Show("ȡ����Ԥ����ɹ�,�������ϴ�����!");

            return 1;


        }

        private int CancelPreBalance()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }


            #region ҽ������



            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
            if (returnValue != 1)
            {

                // errText = this.medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                return -1;
            }

            currentPatient.SIMainInfo.User03 = "T"; //ȡ��Ԥ����
            //ȡ��ҽ���ϴ�����
            returnValue = this.medcareInterfaceProxy.PreBalanceInpatient(currentPatient,ref alFeeItemLists);
            if (returnValue != 1)
            {
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                return -1;
            }
            #endregion

            MessageBox.Show("ȡ����Ԥ����ɹ�,�������ϴ�����!");

            return 1;
        
        }
        /// <summary>
        /// �����ϴ�ҽ������
        /// </summary>
        /// <returns></returns>
        protected int CancelUploadFee()
        {

            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("סԺ�Ŵ���û���ҵ��û���", 111);
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }

            #region ҽ������



            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(this.currentPatient.Pact.ID);
            if (returnValue != 1)
            {

                // errText = this.medcareInterfaceProxy.ErrMsg;
                return -1;

            }
            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                return -1;
            }

            //ȡ��ҽ���ϴ�����
            returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllInpatient(currentPatient);
            if (returnValue != 1)
            {
                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                return -1;
            }
            #endregion

            MessageBox.Show("ȡ���ϴ��ɹ�,�������ϴ�����!");

            return 1;



        }


        private void neuButton1_Click(object sender, EventArgs e)
        {
            string inState = string.Empty;//��Ժ״̬
            string deptCode = string.Empty;//סԺ���ұ���
            string pactCode = string.Empty;//��ͬ��λ����
            DateTime beginTime = this.dtpBeginTime.Value;
            DateTime endTime = this.dtpEndTime.Value;

            if (this.cmbDept.Text == string.Empty || this.cmbDept.Text == "ȫ��")
            {
                deptCode = "%";

            }
            else
            {
                deptCode = this.cmbDept.Tag.ToString();
            }
            if (this.cmbPact.Text == string.Empty || this.cmbPact.Text == "ȫ��")
            {
                pactCode = "%";
            }
            else
            {
                pactCode = this.cmbPact.Tag.ToString();
            }
            if (this.cmbPatientState.Text == string.Empty || this.cmbPatientState.Text == "ȫ��")
            {
                inState = "%";
            }
            else
            {
                inState = this.cmbPatientState.Tag.ToString();
            }
            Cursor.Current = Cursors.WaitCursor;

            if (inState.Equals("O"))  //סԺ�ѽ��㲡��
            {
                //ArrayList patientListTemp = radtManager.QueryPatientByConditonsOutpatient(pactCode, deptCode, inState, beginTime, endTime);
                //if (patientListTemp == null)
                //{
                //    MessageBox.Show(Language.Msg("���߻��߻�����Ϣ����!") + this.radtManager.Err);
                //    Cursor.Current = Cursors.Arrow;
                //    return;
                //}
              
                //Cursor.Current = Cursors.Arrow;
                //this.QueryPatient(patientListTemp);
         
            }

            else
            {
                ArrayList patientListTemp = radtManager.QueryPatientByConditons(pactCode, deptCode, inState, beginTime, endTime);
                if (patientListTemp == null)
                {
                    MessageBox.Show(Language.Msg("���߻��߻�����Ϣ����!") + this.radtManager.Err);
                    Cursor.Current = Cursors.Arrow;
                    return;
                }
                Cursor.Current = Cursors.Arrow;
                this.QueryPatient(patientListTemp);
             }
        }

        private void ucPatientFeeQuery_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpMainInfo_Sheet1.RowCount <= 0)
            {
                return;
            }
            string inpatientNO = this.fpMainInfo_Sheet1.Cells[e.Row, 0].Text;

            if (inpatientNO == null)
            {
                return;
            }

            this.currentPatient = this.radtManager.QueryPatientInfoByInpatientNO(inpatientNO);
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ѯ���߻�����Ϣ����!") + this.radtManager.Err);

                return;
            }

            this.SetPatientInfo();

            dtDrugList.Rows.Clear();
            dtUndrugList.Rows.Clear();
            dtPrepay.Rows.Clear();
            dtFee.Rows.Clear();
            dtBalance.Rows.Clear();
            dtChange.Rows.Clear();
            dtFeeclass.Rows.Clear();
            dtDiagnoseList.Rows.Clear();
            //���ò�ѯʱ��
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

            this.QueryAllInfomaition(beginTime, endTime);

        }

        private void txtNameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryPatientByName();
            }

        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.queryInfo();
            return 1;
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void queryInfo()
        {
            if (this.currentPatient != null)
            {
                if (this.currentPatient.ID != "")
                {
                    if (this.neuTabControl1.SelectedIndex == 2)
                    {
                        DateTime detailbeginTime = new DateTime(this.neuDateTimePicker1.Value.Year, this.neuDateTimePicker1.Value.Month, this.neuDateTimePicker1.Value.Day, 0, 0, 0);
                        DateTime detailendTime = new DateTime(this.neuDateTimePicker2.Value.Year, this.neuDateTimePicker2.Value.Month, this.neuDateTimePicker2.Value.Day, 23, 59, 59);
                        if (detailbeginTime > detailendTime)
                        {
                            MessageBox.Show("��ʼ���ڲ��ܴ��ڽ�������,���޸�!", "��ʾ");
                            return;
                        }
                        dtDrugList.Rows.Clear();
                        this.QueryPatientDrugList(detailbeginTime, detailendTime);
                        this.fpDrugList_Sheet1.DataSource = dvDrugListTemp;
                    }
                    else if (this.neuTabControl1.SelectedIndex == 3)
                    {
                        DateTime detailbeginTime = new DateTime(this.neuDateTimePicker4.Value.Year, this.neuDateTimePicker4.Value.Month, this.neuDateTimePicker4.Value.Day, 0, 0, 0);
                        DateTime detailendTime = new DateTime(this.neuDateTimePicker3.Value.Year, this.neuDateTimePicker3.Value.Month, this.neuDateTimePicker3.Value.Day, 23, 59, 59);
                        if (detailbeginTime > detailendTime)
                        {
                            MessageBox.Show("��ʼ���ڲ��ܴ��ڽ�������,���޸�!", "��ʾ");
                            return;
                        }
                        dtUndrugList.Rows.Clear();
                        this.QueryPatientUndrugList(detailbeginTime, detailendTime);
                        this.fpUndrugList_Sheet1.DataSource = dvUndrugListTemp;
                    }
                    else if (this.neuTabControl1.SelectedIndex == 9)
                    {
                        FS.FrameWork.WinForms.Forms.IControlable ic1 = null;
                        TreeNode node = new TreeNode();
                        ic1 = this.tpDayFee.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic1 != null)
                        {
                            ic1.SetValue(this.currentPatient, node);
                        }
                    }
                }
            }
        }

        //private void fpMainInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{

        //}

        #region IInterfaceContainer ��Ա

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];

                //type[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceListPrint);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IBalanceInvoicePrintmy);
                return type;
            }
        }

        #endregion

        private void fpFee_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.Row < 0 || e.Row == this.fpFee_Sheet1.Rows.Count - 1) return;
            string balanceState = "0";
            string feecode = "";
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();
            if (this.fpFee_Sheet1.Cells[e.Row, 6].Text == "δ����")
                balanceState = "0";
            else if (this.fpFee_Sheet1.Cells[e.Row, 6].Text == "�ѽ���")
                balanceState = "1";
            feecode = this.fpFee_Sheet1.Cells[e.Row, 7].Text;
            this.frmPatientFeeByMinFee.Text = this.fpFee_Sheet1.Cells[e.Row, 0].Text + this.fpFee_Sheet1.Cells[e.Row, 1].Text;
            //if (this.ucPatientFeeByMinFee.QueryDetailByMinFee(this.currentPatient.ID.ToString(), beginTime, endTime, balanceState, feecode))
            //{
            //    this.frmPatientFeeByMinFee.ShowDialog();
            //    return;
            //}
        }

        /// <summary>
        /// ��������1
        /// </summary>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //ȡ������
            string queryCode = this.textBox1.Text;
            queryCode = "%" + queryCode + "%";

            //���ù�������
            this.filterInput = "((ҩƷ���� LIKE '" + queryCode + "') ) " ;//+"(����� LIKE '" + queryCode + "') OR " + "(�������� LIKE '" + queryCode + "') )"

            //����ҩƷ����
            this.SetFilter();
        }

        /// <summary>
        /// ���ù�������,��������
        /// </summary>
        private void SetFilter()
        {
            //��������
            if (this.neuTabControl1.SelectedIndex == 2)
            {
                if (this.dvDrugListTemp.Table != null && this.dvDrugListTemp.Table.Rows.Count > 0)
                {
                    this.fpDrugList_Sheet1.RowCount = 0;
                    this.fpDrugList_Sheet1.DataSource = dvDrugListTemp;
                    this.dvDrugListTemp.RowFilter = this.filterInput;
                }
            }
            else if (this.neuTabControl1.SelectedIndex == 3)
            {
                if (this.dvUndrugListTemp.Table != null && this.dvUndrugListTemp.Table.Rows.Count > 0)
                {
                    this.fpUndrugList_Sheet1.RowCount = 0;
                    this.fpUndrugList_Sheet1.DataSource = dvUndrugListTemp;
                    this.dvUndrugListTemp.RowFilter = this.filterInput;
                }
            }
        }

        /// <summary>
        /// ��������2
        /// </summary>
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //ȡ������
            string queryCode = this.textBox2.Text;
            queryCode = "%" + queryCode + "%";

            //���ù�������
            this.filterInput = "((��Ŀ���� LIKE '" + queryCode + "') ) ";//+"(����� LIKE '" + queryCode + "') OR " + "(�������� LIKE '" + queryCode + "') )"

            //����ҩƷ����
            this.SetFilter();

        }
    }
}

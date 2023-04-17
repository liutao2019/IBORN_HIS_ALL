using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.WinForms.Report.InpatientFee
{
    public partial class ucPatientFeeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientFeeQuery()
        {
            InitializeComponent();
            this.fpDrugList.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpDrugList_ColumnWidthChanged);
            this.fpUndrugList.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(fpUndrugList_ColumnWidthChanged);
        }


        #region ����
               
        /// <summary>
        /// סԺ���תҵ���
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();

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

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
        ///// <summary>
        ///// ���Ұ�����
        ///// </summary>
        //FS.FrameWork.Public.ObjectHelper departmentHelper = new FS.FrameWork.Public.ObjectHelper();
        ///// <summary>
        ///// ��Ա������
        ///// </summary>
        //FS.FrameWork.Public.ObjectHelper employeeHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ��С���ð�����
        /// </summary>
        FS.FrameWork.Public.ObjectHelper constantHelper = new FS.FrameWork.Public.ObjectHelper();

        #endregion

        /// <summary>
        /// Ա��
        /// </summary>
        protected Hashtable hashTablePeople = new Hashtable();
        /// <summary>
        /// ����
        /// </summary>
        protected Hashtable hashTableDept = new Hashtable();


        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();

        #region DataTalbe��ر���

        string pathNameMainInfo = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientMainInfo.xml";
        string pathNamePrepay = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientPrepay.xml";
        string pathNameFee = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientFee.xml";
        string pathNameDrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientDrugList.xml";
        string pathNameUndrugList = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientUndrugList.xml";
        string pathNameBalance = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientBalance.xml";
        string pathNameDiagnose = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientDiagnose.xml";
        string pathNameChange = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\OldQueryPatientChange.xml";

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
        DataTable dtChange = new DataTable( );
        /// <summary>
        /// �����Ϣ��ͼ
        /// </summary>
        DataView dvChange = new DataView( );

        #endregion

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
                        break;
                    }
                }
            }
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

            //��ʼ��ҽ��
            if (this.InitDoct() == -1)
            {
                return -1;
            }

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

            //departmentHelper.ArrayObject = m.GetDepartment();
            //employeeHelper.ArrayObject = m.QueryEmployeeAll();
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

            //{B71C3094-BDC8-4fe8-A6F1-7CEB2AEC55DD}
            //ArrayList pactList = this.consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PACTUNIT);
            ArrayList pactList = this.pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                MessageBox.Show(Language.Msg("���غ�ͬ��λ�б�����!") + this.consManager.Err);

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
                MessageBox.Show(Language.Msg("���ؿ����б�����!") + this.deptManager.Err);

                return -1;
            }
            
            deptList.Add(objAll);

            findAll = deptList.IndexOf(objAll);

            this.cmbDept.AddItems(deptList);

            if (findAll >= 0)
            {
                this.cmbDept.SelectedIndex = findAll;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int InitDoct()
        {
            int findAll = 0;
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();

            objAll.ID = "%";
            objAll.Name = "ȫ��";

            ArrayList doctList = this.personManager.GetEmployee( FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (doctList == null)
            {
                MessageBox.Show(Language.Msg("����ҽ���б�����!") + this.deptManager.Err);

                return -1;
            }

            doctList.Add(objAll);

            findAll = doctList.IndexOf(objAll);

            this.cmbDoct.AddItems(doctList);

            if (findAll >= 0)
            {
                this.cmbDoct.SelectedIndex = findAll;
            }

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
                //this.ucQueryInpatientNo1.Text = this.currentPatient.ID.Substring(4);
                //���ò�ѯʱ��
                //���ò�ѯʱ��
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                this.QueryAllInfomaition(beginTime, endTime);
                if (dtBalance.Rows.Count > 0)
                {
                    this.lblInState.Text = dtBalance.Rows[0]["��������"].ToString();
                }
            }
            this.fpMainInfo_Sheet1.Columns[13].Width = 180;
            return 1;
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="patient">�ɹ� 1 ʧ�� -1</param>
        private void SetPatientToFpMain(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            DataRow row = this.dtMainInfo.NewRow();

            try
            {

                row["סԺ��ˮ��"] = patient.ID;
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["סԺ����"] = patient.PVisit.PatientLocation.Dept.Name;
                row["����"] = patient.PVisit.PatientLocation.Bed.ID;
                row["�������"] = patient.Pact.Name;
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
                //row["ҽ�����"] = patient.PVisit.MedicalType.Name;
                row["��������"] = patient.BalanceDate == new DateTime(1, 1, 1).Date ? string.Empty : patient.BalanceDate.ToString();

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
            ArrayList drugList = this.feeManager.GetMedItemsForInpatient(this.currentPatient.ID, beginTime, endTime);
            if (drugList == null)
            {
                MessageBox.Show(Language.Msg("��û���ҩƷ��ϸ����!") + this.feeManager.Err);
                
                return;
            }

            string strTemp = null;
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
                ////row["ִ�п���"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                ////row["���߿���"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                //row["ִ�п���"] = this.departmentHelper.GetName (obj.ExecOper.Dept.ID);
                //row["���߿���"] = this.departmentHelper.GetName (((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID); 
                #endregion

                strTemp = obj.ExecOper.Dept.ID;
                row["ִ�п���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID;
                row["���߿���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;


                row["�շ�ʱ��"] = obj.FeeOper.OperTime;

                //FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                //empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as  FS.HISFC.Models.Base.Employee; 
                ////��̬��ʱ���Ȼ����ֲ鲻������
                //if (empl == null || empl.Name == string.Empty)
                //#endregion
                //{
                //    row["�շ�Ա"] = obj.FeeOper.ID;
                //}
                //else
                //{
                //    row["�շ�Ա"] = empl.Name;
                //}

                strTemp = obj.FeeOper.ID;
                row["�շ�Ա"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


                row["��ҩʱ��"] = obj.ExecOper.OperTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.ExecOper.OperTime.ToString();

                //FS.HISFC.Models.Base.Employee confirmOper = new FS.HISFC.Models.Base.Employee();
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////confirmOper = managerIntergrate.GetEmployeeInfo(obj.ExecOper.ID);
                //confirmOper = employeeHelper.GetObjectFromID(obj.ExecOper.ID) as FS.HISFC.Models.Base.Employee;
                //#endregion
                //if (confirmOper == null || confirmOper.Name == string.Empty)
                //{
                //    row["��ҩԱ"] = obj.ExecOper.ID;
                //}
                //else
                //{
                //    row["��ҩԱ"] = confirmOper.Name;
                //}

                strTemp = obj.ExecOper.ID;
                row["��ҩԱ"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;
                
                //row["��Դ"] = obj.FTSource;
                row["���÷���"] = obj.Item.MinFee.ID;
                dtDrugList.Rows.Add(row);
            }

            this.AddSumInfo(dtDrugList, "ҩƷ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
        }

        /// <summary>
        /// ��ѯ���߷�ҩƷ��ϸ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientUndrugList(DateTime beginTime, DateTime endTime)
        {
            ArrayList undrugList = this.feeManager.QueryFeeItemLists(this.currentPatient.ID, beginTime, endTime);
            if (undrugList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷�ҩƷ��ϸ����!") + this.feeManager.Err);

                return;
            }

            string strTemp = string.Empty;
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
                //FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                //FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////empl = managerIntergrate.GetEmployeeInfo(obj.FeeOper.ID); 
                //empl = this.employeeHelper.GetObjectFromID(obj.FeeOper.ID) as FS.HISFC.Models.Base.Employee; 
                //#endregion

                //if (empl == null || empl.Name == string.Empty)
                //{
                //    row["�շ�Ա"] = obj.FeeOper.ID;
                //}
                //else
                //{
                //    row["�շ�Ա"] = empl.Name;
                //}

                strTemp = obj.FeeOper.ID;
                row["�շ�Ա"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////row["ִ�п���"] = this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;
                ////row["���߿���"] = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name; 
                //row["ִ�п���"] = this.departmentHelper.GetName(obj.ExecOper.Dept.ID);
                //row["���߿���"] = this.departmentHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID); 
                //#endregion

                strTemp = obj.ExecOper.Dept.ID;
                row["ִ�п���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID;
                row["���߿���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

                //row["��Դ"] = obj.FTSource;
                row["���÷���"] = obj.Item.MinFee.ID;
                dtUndrugList.Rows.Add(row);
            }

            this.AddSumInfo(dtUndrugList, "��Ŀ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
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

            string strTemp = null;
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
            {
                //FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                //FS.HISFC.Models.Base.Department deptObj = new FS.HISFC.Models.Base.Department();
                DataRow row = dtPrepay.NewRow();

                row["Ʊ�ݺ�"] = prepay.RecipeNO;
                row["Ԥ�����"] = prepay.FT.PrepayCost;
                row["֧����ʽ"] = prepay.PayType.Name;

                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////   employeeObj = this.personManager.GetPersonByID(prepay.PrepayOper.ID);
                //employeeObj = this.employeeHelper.GetObjectFromID(prepay.PrepayOper.ID) as FS.HISFC.Models.Base.Employee;
                //#endregion
                //row["����Ա"] = employeeObj.Name;

                strTemp = prepay.PrepayOper.ID;
                row["����Ա"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;

                row["��������"] = prepay.PrepayOper.OperTime;

                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////deptObj = this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID);
                //deptObj = this.departmentHelper.GetObjectFromID(((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID) 
                //    as FS.HISFC.Models.Base.Department; 
                //#endregion
                //row["���ڿ���"] = deptObj.Name;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID;
                row["���ڿ���"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


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
                row["�������"] = obj.Item.MinFee.ID;



                //row["��Դ"] = obj.FTSource;

                dtFeeclass.Rows.Add(row);
            }

            this.AddSumInfo(dtFeeclass, "��Ŀ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");

            //��֪��ΪʲôҪȥ��0����
            //deleteqltisnull();
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



            //feeInfoList.AddRange(feeInfoListBalanced);

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoList)
            {

                DataRow row = dtFee.NewRow();
                #region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                //row["��������"] = this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                
                row["��������"] = this.constantHelper.GetName ( feeInfo.Item.MinFee.ID);
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
                row["���÷���"] = feeInfo.Item.MinFee.ID;
                dtFee.Rows.Add(row);
            }

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

                dtFee.Rows.Add(row);
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

            string strTemp = null;

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

                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////employeeObj = this.personManager.GetPersonByID(balance.BalanceOper.ID); 
                //employeeObj = this.employeeHelper.GetObjectFromID(balance.BalanceOper.ID) as FS.HISFC.Models.Base.Employee; 
                //#endregion

                //row["����Ա"] = employeeObj.Name;

                strTemp = balance.BalanceOper.ID;
                row["����Ա"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;


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
        private void QueryPatientChange( DateTime beginTime, DateTime endTime )
        {
            ArrayList changeList = this.radtManager.QueryPatientShiftInfoNew(this.currentPatient.ID);
            if (changeList == null) 
            {
                MessageBox.Show(Language.Msg("��û��߱����Ϣ����!") + this.radtManager.Err);

                return;
            }

            string strTemp = null;

            foreach (FS.HISFC.Models.Invalid.CShiftData change in changeList)
            {
                //FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee( );
                DataRow row = this.dtChange.NewRow( );
                row["���"] = change.HappenNo.ToString();
                row["���ԭ��"] = change.ShitCause;
                row["ԭ����"] = change.OldDataCode;
                row["ԭ����"] = change.OldDataName;
                row["�ֱ���"] = change.NewDataCode;
                row["������"] = change.NewDataName;
                row["��ע"] = change.Mark;
                row["�����˱���"] = change.OperCode;
                //#region {8BB51796-A924-421d-A275-DA1DF775DCC8}
                ////employeeObj = this.personManager.GetPersonByID(change.OperCode);
                //employeeObj = this.employeeHelper.GetObjectFromID(change.OperCode) as FS.HISFC.Models.Base.Employee; 
                //#endregion
                //row["����������"] = employeeObj.Name;

                strTemp = change.OperCode;
                row["����������"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;

                row["����ʱ��"] = change.Memo;            

                this.dtChange.Rows.Add(row);
            }
        }

        /// <summary>
        /// ���Ӻϼ�
        /// </summary>
        /// <param name="table">��ǰDataTalbe</param>
        /// <param name="totName">�ϼƵ�����λ��</param>
        /// <param name="disName">�ϼ�������</param>
        /// <param name="sumColName">ͳ���е�����</param>
        public void AddSumInfo(DataTable table, string totName, string disName, params string[] sumColName)
        {
            DataRow sumRow = table.NewRow();

            //ʹ�õ�һ����Ϊ�ϼƵ�����
            //sumRow[totName] = disName;
            sumRow[table.Columns[0]] = disName;
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
            inputName = FS.FrameWork.Public.String.TakeOffSpecialChar(inputName,"'");
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
																new DataColumn("��������", str)/*,
																new DataColumn("ҽ�����", str)*/});

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
				                                                new DataColumn("��Դ",str),
                                                                new DataColumn("���÷���", str)});

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
				                                                  new DataColumn("��Դ", str),
                                                                  new DataColumn("���÷���", str)});

                dvUndrugList = new DataView(dtUndrugList);

                this.fpUndrugList_Sheet1.DataSource = dvUndrugList;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
            }
      
            #endregion

            #region �շ���Ŀ����

            //if (System.IO.File.Exists(pathNameFeeclass))
            //{
            //    FS.NFC.Interface.Classes.CustomerFp.CreatColumnByXML(pathNameFeeclass, dtFeeclass, ref dvFeeclass, this.fpFeeclass_Sheet1);

            //    FS.NFC.Interface.Classes.CustomerFp.ReadColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            //}
            //else
            //{
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
                                                                new DataColumn("�������", str),
                                                                new DataColumn("ִ�п���",str),
                                                                new DataColumn("���߿���",str),
                                                                new DataColumn("�շ�Ա", str),
                                                                new DataColumn("��Դ",str)});

            dvFeeclass = new DataView(dtFeeclass);

            this.fpFeeclass_Sheet1.DataSource = dvFeeclass;

            //    FS.NFC.Interface.Classes.CustomerFp.SaveColumnProperty(this.fpFeeclass_Sheet1, pathNameFeeclass);
            //}

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
														   new DataColumn("����״̬", str)});

                dvFee = new DataView(dtFee);

                this.fpFee_Sheet1.DataSource = dvFee;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }
            dtFee.Columns.Add(new DataColumn("���÷���", str));
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
            if (this.currentPatient.PVisit.OutTime != DateTime.MinValue)
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
            this.lblfreecost.Text = this.currentPatient.FT.LeftCost.ToString( );
            if (this.currentPatient.CompanyName != null)
            {
                this.lblwork.Text = this.currentPatient.CompanyName.ToString( );
            }
            else
            {
                this.lblwork.Text = "";
            }
            if (this.currentPatient.AddressHome != null)
            {
                this.lblhome.Text = this.currentPatient.AddressHome.ToString( );
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

            neuSpread1.Visible = false;
        }

        /// <summary>
        /// ����"_"
        /// </summary>
        /// <param name="langth">����</param>
        /// <returns>�ɹ� "---" ʧ�� null</returns>
        private string RetrunSplit(int langth) 
        {
            string s = string.Empty ;

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
            dtChange.Rows.Clear( );
        }
        private void InitContr()
        {
            this.neuLabel21.Visible = false;
            this.lblOwnCost.Visible = false;
            this.neuLabel23.Visible = false;
            this.lblPubCost.Visible = false;
        }


        #endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            this.QueryPatientByInpatientNO();
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPage(0, 0, this.neuTabControl1.SelectedTab);
            
            return base.OnPrint(sender, neuObject);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.neuButton1_Click(null, null);
            return base.OnQuery(sender, neuObject);
        }
        //��ӡԤ��
        public override int  PrintPreview(object sender, object neuObject)
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

        private void neuButton1_Click(object sender, EventArgs e)
        {
            string inState = string.Empty;//��Ժ״̬
            string deptCode = string.Empty;//סԺ���ұ���
            string pactCode = string.Empty;//��ͬ��λ����
            string doctCode = string.Empty;//�ܴ�ҽ��
            DateTime beginTime = this.dtpBeginTime.Value.Date;
            DateTime endTime = this.dtpEndTime.Value;

            if (this.cmbDept.Text == string.Empty || this.cmbDept.Text == "ȫ��")
            {
                deptCode = "%";
               
            }
            else
            {
                deptCode = this.cmbDept.Tag.ToString();
            }

            if (this.cmbDoct.Text == string.Empty || this.cmbDoct.Text == "ȫ��")
            {
                doctCode = "%";

            }
            else
            {
                doctCode = this.cmbDoct.Tag.ToString();
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
          
            ArrayList patientListTemp = radtManager.QueryPatientByConditons(pactCode, deptCode, inState, beginTime, endTime,doctCode);
            if (patientListTemp == null)
            {
                MessageBox.Show(Language.Msg("���߻��߻�����Ϣ����!") + this.radtManager.Err);
                Cursor.Current = Cursors.Arrow;
                return;
            }
            Cursor.Current = Cursors.Arrow;
            this.QueryPatient(patientListTemp);
        }

        private void ucPatientFeeQuery_Load(object sender, EventArgs e)
        {
            this.Init();

            this.hashTableDept.Clear();
            this.hashTablePeople.Clear();

            ArrayList tdlist = deptManager.GetDeptAllUserStopDisuse();
            foreach (FS.HISFC.Models.Base.Department tempinfo in tdlist)
            {
                hashTableDept.Add(tempinfo.ID, tempinfo.Name);
            }

            ArrayList tplist = personManager.GetEmployeeAll();
            foreach (FS.HISFC.Models.Base.Employee tempinfo in tplist)
            {
                hashTablePeople.Add(tempinfo.ID, tempinfo.Name);
            } 
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
            dtChange.Rows.Clear( );

            dtFeeclass.Rows.Clear();
            //���ò�ѯʱ��
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

            this.QueryAllInfomaition(beginTime, endTime);
            if (dtBalance.Rows.Count > 0)
            {
                this.lblInState.Text = dtBalance.Rows[0]["��������"].ToString();
            }

        }

        private void txtNameKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.QueryPatientByName();
            }
           
        }

        private void fpFee_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            neuSpread1_Sheet1.RowCount = 0;
            neuSpread1_Sheet2.RowCount = 0;
            int rowindex = 0;
            if (!neuSpread1.Visible)
            {
                neuSpread1.Visible = true;
            }

            string s = fpFee_Sheet1.Cells[e.Row, 7].Text;
            DataView dv2 = new DataView(dtDrugList, "���÷���='" + s + "'", "ҩƷ����,�շ�ʱ��", DataViewRowState.CurrentRows);
            DataView dv3 = new DataView(dtUndrugList, "���÷���='" + s + "'", "��Ŀ����,�շ�ʱ��", DataViewRowState.CurrentRows);
            //�ж������ҩƷ���Ƿ�ҩƷ
            if (dv2.Count > 0)
            {
                DataView dv = new DataView(dtFeeclass, "�������='" + s + "'", "��Ŀ����", DataViewRowState.CurrentRows);
                neuSpread1_Sheet1.DataSource = dv;
            }
            else if (dv3.Count > 0)
            {
                DataView dv1 = new DataView(dtFeeclass, "�������='" + s + "'", "��Ŀ����", DataViewRowState.CurrentRows);
                neuSpread1_Sheet2.DataSource = dv1;
            }
            if (neuSpread1_Sheet1.RowCount == 0)
            {
                neuSpread1.ActiveSheetIndex = 1;
            }
            else
            {
                neuSpread1.ActiveSheetIndex = 0;
            }
        }

        private void fpFeeclass_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void fpDrugList_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameDrugList);
        }

        private void fpUndrugList_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpUndrugList_Sheet1, pathNameUndrugList);
        }
 
        //private void fpMainInfo_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        //{

        //}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;
using FS.HISFC.BizLogic.RADT;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Fee.Inpatient;
using FS.FrameWork.Function;
using FS.FrameWork.WinForms.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Fee;

namespace FS.HISFC.Components.InpatientFee.Fee
{
    public partial class ucPatientFeeQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientFeeQuery()
        {
            InitializeComponent();
            this.fpMainInfo.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpMainInfo_SelectionChanged);
            this.fpMainInfo.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpMainInfo_CellDoubleClick);
            this.fpFee.SelectionChanged+=new FarPoint.Win.Spread.SelectionChangedEventHandler(fpFee_SelectionChanged);
            this.fpFeeDaySum.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpFeeDaySum_SelectionChanged);
            this.fpFeeDetail.MouseUp+=new MouseEventHandler(fpFeeDetail_MouseUp);
            this.fpFeeDetail.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpFeeDetail_ButtonClicked);
            this.fpFeeItemSum.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpFeeItemSum_SelectionChanged);

            //����
            this.cmbFeeTypeFilter.TextChanged += new EventHandler(cmbFeeTypeFilter_TextChanged);
            this.txtItemFilter.TextChanged += new EventHandler(txtItemFilter_TextChanged);
            this.dtpItemDayFilter.ValueChanged += new EventHandler(dtpItemDayFilter_ValueChanged);
            this.ckUnCombo.CheckedChanged += new EventHandler(ckUnCombo_CheckedChanged);
        }

        #region ����
        /// <summary>
        /// toolBarService
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();

        private  FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
        /// <summary>
        /// סԺ���תҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// ��Ա��Ϣҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// �˷�����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.ReturnApply returnApplyManager = new FS.HISFC.BizLogic.Fee.ReturnApply();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy phamarcyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.InPatient feeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ���ҵ���-met_cas_diagnose
        /// </summary>
        protected FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo currentPatient = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// Tab
        /// </summary>
        protected Hashtable hashTableFp = new Hashtable();
        protected ArrayList alAllFeeItemLists = new ArrayList();

        protected Hashtable hashTablePeople = new Hashtable();
        protected Hashtable hashTableDept= new Hashtable();

        /// <summary>
        /// ��С���ð�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper minFeeHelper = new FS.FrameWork.Public.ObjectHelper();

        private Hashtable hsMinFee = new Hashtable();
        private Hashtable hsDayFeeItemList = new Hashtable();

        private Hashtable hsQuitFeeByPackage = new Hashtable();
        /// <summary>
        /// �˷��������ҩ������Ϣ
        /// </summary>
        private Hashtable dicReturnApply = new Hashtable();

        /// <summary>
        /// ���� ��ʿվ�����Ŀ���
        /// </summary>
        private Hashtable depts = new Hashtable();
      
        #region DataTalbe��ر���

        string pathNameMainInfo = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientMainInfo.xml";
        string pathNamePrepay = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientPrepay.xml";
        string pathNameFee = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFee.xml";
        string pathNameFeeItemSum = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFeeItemSum.xml";
        string pathNameFeeDaySum = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFeeDaySum.xml";
        string pathNameFeeDetail = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientFeeDetail.xml";
        string pathNameBalance = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientBalance.xml";
        string pathNameDiagnose = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientDiagnose.xml";
        string pathNameShiftData = FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\QueryPatientShiftData.xml";

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
        DataTable dtFeeItemSum = new DataTable();

        /// <summary>
        /// ҩƷ��ϸ��ͼ
        /// </summary>
        DataView dvFeeItemSum = new DataView();

        /// <summary>
        /// ҩƷ��ϸ
        /// </summary>
        DataTable dtFeeDaySum = new DataTable();

        /// <summary>
        /// ҩƷ��ϸ��ͼ
        /// </summary>
        DataView dvFeeDaySum = new DataView();

        /// <summary>
        /// ��ҩƷ��Ϣ
        /// </summary>
        DataTable dtFeeDetail = new DataTable();

        /// <summary>
        /// ��ҩƷ��Ϣ��ͼ
        /// </summary>
        DataView dvFeeDetail = new DataView();

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
        /// ���ý�����Ϣ
        /// </summary>
        DataTable dtBalance = new DataTable();

        /// <summary>
        /// ���ý�����Ϣ��ͼ
        /// </summary>
        DataView dvBalance = new DataView();

        /// <summary>
        /// �����Ϣ��
        /// </summary>
        DataTable dtShiftData = new DataTable();

        /// <summary>
        /// �����Ϣ��ͼ
        /// </summary>
        DataView dvShiftData = new DataView();

        #endregion

        #endregion

        #region ����

        /// <summary>
        /// סԺ���߻�����Ϣ
        /// </summary>
        public PatientInfo PatientInfo
        {
            get
            {
                return this.currentPatient;
            }
            set
            {
                this.currentPatient = value;

                //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ݣ����Ժ�^^");
                //Application.DoEvents();

                this.QueryPatientByInpatientNO(currentPatient);

               // FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        [Category("��������"), Description("סԺ�������Ĭ������0סԺ�ţ�5����")]
        public int DefaultInput
        {
            get
            {
                return this.ucQueryInpatientNo1.DefaultInputType;
            }
            set
            {
                this.ucQueryInpatientNo1.DefaultInputType = value;
            }
        }

        [Category("��������"), Description("�����Ų�ѯ������Ϣʱ�������ߵ�״̬��ѯ�����ȫ����'ALL'")]
        public string PatientInState
        {
            get
            {
                return this.ucQueryInpatientNo1.PatientInState;
            }
            set
            {
                this.ucQueryInpatientNo1.PatientInState = value;
            }
        }

        /// <summary>
        /// �Ƿ��������������ҷ���
        /// </summary>
        private bool isCanQuitOtherDeptFee = true;
        [Category("��������"), Description("�Ƿ��������������ҷ��� True:���� False:������")]
        public bool IsCanQuitOtherDeptFee
        {
            get
            {
                return isCanQuitOtherDeptFee;
            }
            set
            {
                isCanQuitOtherDeptFee = value;
            }
        }

        /// <summary>
        /// �������Ƿ���Ҫ����
        /// </summary>
        private bool isCurrentDeptNeedAppy = true;
        [Category("��������"), Description("�����ҷ����Ƿ���Ҫ���룬True:��Ҫ False:����Ҫ")]
        public bool IsCurrentDeptNeedAppy
        {
            get
            {
                return isCurrentDeptNeedAppy;
            }
            set
            {
                isCurrentDeptNeedAppy = value;
            }
        }


        /// <summary>
        /// �Ƿ���������������Ա����
        /// </summary>
        private bool isCanQuitOtherOperator = true;
        [Category("��������"), Description("�Ƿ���������������Ա���ã�True:���� False:������")]
        public bool IsCanQuitOtherOperator
        {
            get
            {
                return this.isCanQuitOtherOperator;
            }
            set
            {
                this.isCanQuitOtherOperator = value;
            }
        }

        /// <summary>
        /// �Ƿ������˷Ѻ�ȡ���˷Ѳ���
        /// </summary>
        private bool isQuitFee = false;
        [Category("��������"), Description("�Ƿ������˷Ѻ�ȡ���˷Ѳ�����True:���� False:������")]
        public bool IsQuitFee
        {
            get
            {
                return this.isQuitFee;
            }
            set
            {
                this.isQuitFee = value;
            }
        }

        [Category("��������"), Description("��ʾTabҳ��˳��Ϳɼ���")]
        public string ShowTab
        {
            get
            {
                string str="";
                foreach(TabPage  tp in this.neuTabControl1.TabPages)
                {
                    str+=tp.Name+",";
                }
                return str.Length>0?str.Substring(0,str.Length-1):str;
            }
            set
            {

                this.neuTabControl1.TabPages.Clear();
                string[] strArr = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                if (strArr != null && strArr.Length > 0)
                {
                    for (int i = 0; i < strArr.Length; i++)
                    {
                       if( this.tpFeeInfo.Name.Equals(strArr[i]))
                       {
                           this.neuTabControl1.TabPages.Insert(this.neuTabControl1.TabPages.Count, tpFeeInfo);
                       }
                       else if (this.tpMainInfo.Name.Equals(strArr[i]))
                       {
                           this.neuTabControl1.TabPages.Insert(this.neuTabControl1.TabPages.Count,tpMainInfo);
                       }
                    }
                }
            }
        }

        private bool isShowCurrentDeptFee = false;
        [Category("��������"), Description("�Ƿ�ֻ��ʾ�����ҵķ���")]
        public bool IsShowCurrentDeptFee
        {
            get
            {
                return isShowCurrentDeptFee;
            }
            set
            {
                isShowCurrentDeptFee = value;
            }
        }

        #endregion

        #region ˽�з���

        private void InitHashTable() 
        {
            //���Ҳ��������Ŀ���
            depts.Clear();
            ArrayList alDept = this.deptManager.GetDeptFromNurseStation((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Nurse);
            if (alDept != null)
            {
                foreach (FS.FrameWork.Models.NeuObject obj in alDept)
                {
                    if (depts.ContainsKey(obj.ID) == false)
                    {
                        depts.Add(obj.ID, null);
                    }
                }
            }

            if (depts.ContainsKey((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID) == false)
            {
                depts.Add((this.deptManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID, null);
            }

            //������С����
            ArrayList alMinFee = this.consManager.GetAllList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (alMinFee != null)
            {
                this.minFeeHelper.ArrayObject = alMinFee;
                this.cmbFeeTypeFilter.AddItems(alMinFee);
            }
        }

        /// <summary>
        /// ��ʾ���߻�����Ϣ
        /// </summary>
        /// <param name="patient">�ɹ� 1 ʧ�� -1</param>
        private void SetPatientToFpMain(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (patient == null)
            {
                return;
            }

            DataRow row = this.dtMainInfo.NewRow();

            try
            {

                row["סԺ��ˮ��"] = patient.ID;
                row["סԺ��"] = patient.PID.PatientNO;
                row["����"] = patient.Name;
                row["סԺ����"] = patient.PVisit.PatientLocation.Dept.Name;
                row["����"] = patient.PVisit.PatientLocation.Bed.ID.Length > 4 ? patient.PVisit.PatientLocation.Bed.ID.Substring(4) : patient.PVisit.PatientLocation.Bed.ID;
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
                row["��������"] = patient.BalanceDate;

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
        private void QueryPatientByInpatientNO(PatientInfo patients)
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��Ϣ�����Ժ�...");
            Application.DoEvents();
            this.Clear();
            this.dtMainInfo.Rows.Clear();
            Cursor.Current = Cursors.WaitCursor;
            //סԺ������Ϣ
            this.SetPatientToFpMain(patients);
            Cursor.Current = Cursors.Arrow;
            this.SetPatientInfo();
            //���ò�ѯʱ��
            //���ò�ѯʱ��
            DateTime beginTime = this.currentPatient.PVisit.InTime;
            DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();
            this.QueryAllInfomaition(beginTime, endTime);
            this.fpMainInfo_Sheet1.Columns[13].Width = 180;
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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

            //this.alAllFeeItemLists.AddRange(drugList);

            //string strTemp = null;
            FS.HISFC.Models.Base.Employee employee=this.feeManager.Operator as FS.HISFC.Models.Base.Employee;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in drugList)
            {
                if (this.isShowCurrentDeptFee && obj.ExecOper.Dept.ID.Equals(employee.Dept.ID) == false&&obj.FeeOper.Dept.ID.Equals(employee.Dept.ID)==false)
                {
                    continue;
                }

                if (this.hsMinFee.ContainsKey(obj.Item.MinFee.ID)==false)
                {
                    hsMinFee[obj.Item.MinFee.ID] = new ArrayList();
                }
                ((ArrayList)hsMinFee[obj.Item.MinFee.ID]).Add(obj);

                if (this.hsDayFeeItemList.ContainsKey(obj.Item.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")) == false)
                {
                    hsDayFeeItemList[obj.Item.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")] = new ArrayList();
                }
                ((ArrayList)hsDayFeeItemList[obj.Item.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")]).Add(obj);

            //    DataRow row = dtDrugList.NewRow();
                
            //    row["ҩƷ����"] = obj.Item.Name;
            //    FS.HISFC.Models.Pharmacy.Item medItem = (FS.HISFC.Models.Pharmacy.Item)obj.Item;
            //    row["���"] = medItem.Specs;
            //    row["����"] = obj.Item.Price;
            //    row["����"] = obj.Item.Qty;
            //    row["����"] = obj.Days;
            //    row["��λ"] = obj.Item.PriceUnit;
            //    row["���"] = obj.FT.TotCost;
            //    row["�Է�"] = obj.FT.OwnCost;
            //    row["����"] = obj.FT.PubCost;
            //    row["�Ը�"] = obj.FT.PayCost;
            //    row["�Ż�"] = obj.FT.RebateCost;

            //    strTemp = obj.ExecOper.Dept.ID;
            //    row["ִ�п���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp].ToString() : "";// this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;

            //    strTemp = ((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID;
            //    row["���߿���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp].ToString() : "";// this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name;
            //    row["�շ�ʱ��"] = obj.FeeOper.OperTime;

            //    FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();

            //    strTemp = obj.FeeOper.ID;
            //    row["�շ�Ա"] = this.hashTablePeople.Contains(strTemp) ? hashTablePeople[strTemp].ToString() : strTemp;
                
            //    row["��ҩʱ��"] = obj.ExecOper.OperTime.Date == new DateTime(1, 1, 1).Date ? string.Empty : obj.ExecOper.OperTime.ToString() ;


            //    strTemp = obj.ExecOper.ID;
            //    row["��ҩԱ"] = this.hashTablePeople.Contains(strTemp) ? hashTablePeople[obj.ExecOper.ID].ToString() : strTemp;

            //    dtDrugList.Rows.Add(row);
            }

            //this.AddSumInfo(dtDrugList, "ҩƷ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
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

            //this.alAllFeeItemLists.AddRange(undrugList);
            //string strTemp = "";
            FS.HISFC.Models.Base.Employee employee=this.feeManager.Operator as FS.HISFC.Models.Base.Employee;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in undrugList)
            {
                if (this.isShowCurrentDeptFee && obj.ExecOper.Dept.ID.Equals(employee.Dept.ID) == false && obj.FeeOper.Dept.ID.Equals(employee.Dept.ID)==false)
                {
                    continue;
                }

                if (this.IsPackageItem(obj))
                {
                    #region ������Ŀ��С����
                    if (string.IsNullOrEmpty(obj.UndrugComb.MinFee.ID))
                    {
                        obj.UndrugComb.MinFee.ID = CommonController.Instance.GetItem(obj.UndrugComb.ID).MinFee.ID;
                    }
                    if (this.hsMinFee.ContainsKey(obj.UndrugComb.MinFee.ID) == false)
                    {
                        hsMinFee[obj.UndrugComb.MinFee.ID] = new ArrayList();
                    }
                    ((ArrayList)hsMinFee[obj.UndrugComb.MinFee.ID]).Add(obj);

                    if (this.hsDayFeeItemList.ContainsKey(obj.UndrugComb.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")) == false)
                    {
                        hsDayFeeItemList[obj.UndrugComb.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")] = new ArrayList();
                    }
                    ((ArrayList)hsDayFeeItemList[obj.UndrugComb.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")]).Add(obj);
                    #endregion
                }
                else
                {
                    #region ��Ŀ��С����
                    if (this.hsMinFee.ContainsKey(obj.Item.MinFee.ID) == false)
                    {
                        hsMinFee[obj.Item.MinFee.ID] = new ArrayList();
                    }
                    ((ArrayList)hsMinFee[obj.Item.MinFee.ID]).Add(obj);

                    if (this.hsDayFeeItemList.ContainsKey(obj.Item.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")) == false)
                    {
                        hsDayFeeItemList[obj.Item.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")] = new ArrayList();
                    }
                    ((ArrayList)hsDayFeeItemList[obj.Item.ID + obj.ChargeOper.OperTime.ToString("yyyy-MM-dd")]).Add(obj);
                    #endregion
                }

            //    DataRow row = dtUndrugList.NewRow();
                
            //    row["��Ŀ����"] = obj.Item.Name;
            //    row["����"] = obj.Item.Price;
            //    row["����"] = obj.Item.Qty;
            //    row["��λ"] = obj.Item.PriceUnit;
            //    row["���"] = obj.FT.TotCost;
            //    row["�Է�"] = obj.FT.OwnCost;
            //    row["����"] = obj.FT.PubCost;
            //    row["�Ը�"] = obj.FT.PayCost;
            //    row["�Ż�"] = obj.FT.RebateCost;
            //    row["�շ�ʱ��"] = obj.FeeOper.OperTime;

            //    strTemp = obj.FeeOper.ID;
            //    row["�շ�Ա"] = this.hashTablePeople.Contains(strTemp) ? hashTablePeople[strTemp].ToString() : strTemp;

            //    strTemp = obj.ExecOper.Dept.ID;
            //    row["ִ�п���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp].ToString() : strTemp;// this.deptManager.GetDeptmentById(obj.ExecOper.Dept.ID).Name;

            //    strTemp = ((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID;
            //    row["���߿���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp].ToString() : strTemp;// this.deptManager.GetDeptmentById(((FS.HISFC.Models.RADT.PatientInfo)obj.Patient).PVisit.PatientLocation.Dept.ID).Name;
            //    //row["��Դ"] = obj.FTSource;
               
            //    dtUndrugList.Rows.Add(row);
            }

            //this.AddSumInfo(dtUndrugList, "��Ŀ����", "�ϼ�:", "���", "�Է�", "����", "�Ը�", "�Ż�");
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

            string strTemp = string.Empty;
            foreach (FS.HISFC.Models.Fee.Inpatient.Prepay prepay in prepayList)
            {
                FS.HISFC.Models.Base.Employee employeeObj = new FS.HISFC.Models.Base.Employee();
                FS.HISFC.Models.Base.Department deptObj = new FS.HISFC.Models.Base.Department();
                DataRow row = dtPrepay.NewRow();

                row["Ʊ�ݺ�"] = prepay.RecipeNO;
                row["Ԥ�����"] = prepay.FT.PrepayCost;
                row["֧����ʽ"] = prepay.PayType.Name;

                strTemp = prepay.PrepayOper.ID;
                row["����Ա"] = this.hashTablePeople.Contains(strTemp) ? this.hashTablePeople[strTemp] : strTemp;

                row["��������"] = prepay.PrepayOper.OperTime;

                strTemp = ((FS.HISFC.Models.RADT.PatientInfo)prepay.Patient).PVisit.PatientLocation.Dept.ID;
                row["���ڿ���"] = this.hashTableDept.Contains(strTemp) ? this.hashTableDept[strTemp] : strTemp;

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

                dtPrepay.Rows.Add(row);
            }

            this.AddSumInfo(dtPrepay, "Ʊ�ݺ�", "�ϼ�:", "Ԥ�����");

            dvPrepay.Sort = "Ʊ�ݺ� ASC";
        }

        /// <summary>
        /// ��û���ָ��ʱ����ڵ���С���û�����Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        private void QueryPatientFeeInfo(DateTime beginTime, DateTime endTime)
        {

            FS.HISFC.Models.Base.Employee employee = this.feeManager.Operator as FS.HISFC.Models.Base.Employee;
            ArrayList feeInfoList = new ArrayList();

            ///��ʾ��ϸ
            if (this.ckUnCombo.Checked)
            {
                feeInfoList = this.feeManager.QueryFeeInfosGroupByPackageMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "0", this.isShowCurrentDeptFee ? employee.Dept.ID : "ALL");
            }
            else
            {
                feeInfoList = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "0", this.isShowCurrentDeptFee ? employee.Dept.ID : "ALL");
            }

            if (feeInfoList == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��û�����ϸ����!") + this.feeManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoList)
            {
                //if (this.isShowCurrentDeptFee && feeInfo.ExecOper.Dept.ID.Equals(employee.Dept.ID) == false && feeInfo.FeeOper.Dept.ID.Equals(employee.Dept.ID)==false)
                //{
                //    continue;
                //}
                DataRow row = dtFee.NewRow();

                row["���ô���"] = feeInfo.Item.MinFee.ID;
                row["��������"] = this.minFeeHelper.GetName(feeInfo.Item.MinFee.ID) +( feeInfo.SplitFeeFlag ? "(����)" : string.Empty );// this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                row["���"] = feeInfo.FT.TotCost;
                row["�Է�"] = feeInfo.FT.OwnCost;
                row["����"] = feeInfo.FT.PubCost;
                row["�Ը�"] = feeInfo.FT.PayCost;
                row["�Żݽ��"] = feeInfo.FT.RebateCost;
                row["����״̬"] = "δ����";
                dtFee.Rows.Add(row);
            }

            ArrayList feeInfoListBalanced =new ArrayList();

            if (this.ckUnCombo.Checked)
            {
                feeInfoListBalanced = this.feeManager.QueryFeeInfosGroupByPackageMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "1", this.isShowCurrentDeptFee ? employee.Dept.ID : "ALL");
            }
            else
            {
                feeInfoListBalanced = this.feeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.currentPatient.ID, beginTime, endTime, "1", this.isShowCurrentDeptFee ? employee.Dept.ID : "ALL");
            }
            if (feeInfoListBalanced == null)
            {
                MessageBox.Show(Language.Msg("��û��߷��û�����ϸ����!") + this.feeManager.Err);

                return;
            }

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo in feeInfoListBalanced)
            {
                //if (this.isShowCurrentDeptFee && feeInfo.ExecOper.Dept.ID.Equals(employee.Dept.ID) == false && feeInfo.FeeOper.Dept.ID.Equals(employee.Dept.ID) == false)
                //{
                //    continue;
                //}

                DataRow row = dtFee.NewRow();
                row["���ô���"] = feeInfo.Item.MinFee.ID;
                row["��������"] = this.minFeeHelper.GetName(feeInfo.Item.MinFee.ID) + (feeInfo.SplitFeeFlag ? "(����)" : string.Empty); //this.feeManager.GetComDictionaryNameByID("MINFEE", feeInfo.Item.MinFee.ID);
                row["���"] = feeInfo.FT.TotCost;
                row["�Է�"] = feeInfo.FT.OwnCost;
                row["����"] = feeInfo.FT.PubCost;
                row["�Ը�"] = feeInfo.FT.PayCost;
                row["�Żݽ��"] = feeInfo.FT.RebateCost;
                row["����״̬"] = "�ѽ���";

                dtFee.Rows.Add(row);
            }

            this.SelectFeeInfo();
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

                temp = balance.BalanceOper.ID;
                row["����Ա"] = this.hashTablePeople.Contains(temp) ? this.hashTablePeople[temp] : temp;
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
        /// ��ѯ�����Ϣ
        /// </summary>
        /// <param name="patient"></param>
        private void QueryPatientShiftData(PatientInfo patient)
        {
            ArrayList shiftDataList = this.radtManager.QueryPatientShiftInfoNew(patient.ID);
            if (shiftDataList == null)
            {
                MessageBox.Show(Language.Msg("��û��߱����Ϣ����!") + this.radtManager.Err);

                return;
            }
            foreach (FS.HISFC.Models.Invalid.CShiftData shiftData in shiftDataList)
            {
                DataRow row = dtShiftData.NewRow();

                row["�������"] = shiftData.ShitType;
                row["�������"] = shiftData.ShitCause;
                row["���ǰ����"] = shiftData.OldDataCode;
                row["���ǰ����"] = shiftData.OldDataName;
                row["��������"] = shiftData.NewDataCode;
                row["���������"] = shiftData.NewDataName;
                row["���ʱ��"] = shiftData.Memo;
                row["�����"] = this.hashTablePeople.ContainsKey(shiftData.OperCode) ? this.hashTablePeople[shiftData.OperCode].ToString() : shiftData.OperCode;

                dtShiftData.Rows.Add(row);
            }
        }

        /// <summary>
        /// �����˷�������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        private void QueryPatientReturyApply(PatientInfo patient)
        {
            ArrayList returnApplys = this.returnApplyManager.QueryReturnApplys(patient.ID, false);
            if (returnApplys == null)
            {
                MessageBox.Show(Language.Msg("����˷�������Ϣ����"));
                return;
            }
            dicReturnApply.Clear();
            foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in returnApplys)
            {
                dicReturnApply.Add(returnApply.RecipeNO + returnApply.SequenceNO.ToString(), returnApply);
            }
            ArrayList alQuitDrug = this.phamarcyIntegrate.QueryDrugReturn("AAAA"/*this.operDept.ID*/, "AAAA", patient.ID);
            if (alQuitDrug == null)
            {
                MessageBox.Show(Language.Msg("��ȡ��ҩ������Ϣ��������"));
                return;
            }
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alQuitDrug)
            {
                dicReturnApply.Add(info.RecipeNO + info.SequenceNO.ToString(), info);
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
        /// ���ӷ�����ϸ�����ݱ�
        /// </summary>
        /// <param name="feeItemList"></param>
        public int AddFeeDetailToDataTable(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {

            if (this.ckShowBackDetail.Checked == false && feeItemList.NoBackQty == 0)
            {
                return -1;
            }
            DataRow dr = this.dtFeeDetail.NewRow();
            //this.fpFeeDetail_Sheet1.Rows.Add(0, 1);
            dr["�շ�ʱ��"] = feeItemList.ChargeOper.OperTime.ToString();
            dr["�Ʒ�ʱ��"] = feeItemList.FeeOper.OperTime.ToString();
            if (feeItemList.FT.TotCost < 0)
            {
                dr["�˷�ʱ��"] = feeItemList.FeeOper.OperTime.ToString();
            }
            dr["��Ŀ����"] = this.GetItemName(feeItemList);
            dr["����"] = feeItemList.FT.TotCost >= 0 ? feeItemList.Item.Qty : -feeItemList.Item.Qty;
            dr["��������"] = feeItemList.FT.TotCost > 0 ? feeItemList.NoBackQty : 0;
            dr["���"] = feeItemList.FT.TotCost.ToString();
            dr["�շ���"] = this.hashTablePeople.ContainsKey(feeItemList.FeeOper.ID)&&this.hashTablePeople.ContainsKey(feeItemList.ChargeOper.ID) ? this.hashTablePeople[feeItemList.FeeOper.ID].ToString() : "ϵͳ";
            dr["��������"] = this.hashTableDept.ContainsKey(feeItemList.RecipeOper.Dept.ID) ? this.hashTableDept[feeItemList.RecipeOper.Dept.ID].ToString() : "ϵͳ";
            dr["����ҽ��"] = this.hashTablePeople.ContainsKey(feeItemList.RecipeOper.ID) ? this.hashTablePeople[feeItemList.RecipeOper.ID].ToString() : "ϵͳ";
            dr["������"] = feeItemList.RecipeNO;
            dr["�˷�"] = false;
            dr["������Դ"] = feeItemList.FTSource.ToString();
            dr["��������ˮ��"] = feeItemList.SequenceNO.ToString();
            dr["��������"] = feeItemList.TransType.ToString();
            if (this.dtFeeDetail.Columns.Contains("��λ"))
            {
                dr["��λ"] = feeItemList.Item.PriceUnit;
            }
            if (this.dtFeeDetail.Columns.Contains("�˷�����"))
            {
                dr["�˷�����"] = 0.00M;
            }
            if (this.dtFeeDetail.Columns.Contains("Ӧִ��ʱ��"))
            {
                dr["Ӧִ��ʱ��"] = feeItemList.ExecOrder.DateUse;
            }
            if (this.dtFeeDetail.Columns.Contains("ִ�п���"))
            {
                dr["ִ�п���"] = this.hashTableDept.ContainsKey(feeItemList.ExecOper.Dept.ID) ? this.hashTableDept[feeItemList.ExecOper.Dept.ID].ToString() : "ϵͳ";
            }
            if (this.dtFeeDetail.Columns.Contains("ȡ������"))
            {
                dr["ȡ������"] = false;
            }

           
            this.dtFeeDetail.Rows.Add(dr);

            return 1;
        }

        /// <summary>
        /// ���÷�����ϸ�ĸ�ʽ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <param name="row"></param>
        public void SetFpFeeDetailFormat(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList,int row,ReturnApply returnApply)
        {
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�շ�ʱ��"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�Ʒ�ʱ��"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�ʱ��"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["��Ŀ����"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["��������"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["��λ"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["���"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�շ���"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["��������"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����ҽ��"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["������Դ"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["������"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["��������ˮ��"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["��������"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["ȡ������"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�"].Ordinal].Locked = true;
            this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Locked = true;
            if (this.dtFeeDetail.Columns.Contains("ִ�п���"))
            {
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["ִ�п���"].Ordinal].Locked = true;
            }

            //�ѽ���Ĳ������ڲ���
            if (feeItemList.BalanceState == "1")
            {
                this.fpFeeDetail_Sheet1.RowHeader.Cells[row, 0].Text = "�ѽ�";
                this.fpFeeDetail_Sheet1.RowHeader.Cells[row, 0].ForeColor = Color.Red;
                return;
            }

            //����������Ϣ���Ƿ�����ȡ���˷�
            if (this.dtFeeDetail.Columns.Contains("����������"))
            {
                numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                numberCellType.DecimalPlaces = 2;
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].CellType = numberCellType;
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Locked = true;
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["ȡ������"].Ordinal].Locked = true;
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Value = 0.00M;
                if (feeItemList.TransType == FS.HISFC.Models.Base.TransTypes.Positive)
                {
                    if (returnApply != null)
                    {
                        this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Value = returnApply.Item.Qty;
                    }
                    else
                    {
                        if (dicReturnApply.ContainsKey(feeItemList.RecipeNO + feeItemList.SequenceNO.ToString()))
                        {
                            if (dicReturnApply[feeItemList.RecipeNO + feeItemList.SequenceNO.ToString()] is FS.HISFC.Models.Pharmacy.ApplyOut)
                            {
                                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = dicReturnApply[feeItemList.RecipeNO + feeItemList.SequenceNO.ToString()] as FS.HISFC.Models.Pharmacy.ApplyOut;
                                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Value = applyOut.Operation.ApplyQty;
                                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["������"].Ordinal].Value = this.hashTablePeople.ContainsKey(applyOut.Operation.ApplyOper.ID) ? this.hashTablePeople[applyOut.Operation.ApplyOper.ID].ToString() : "ϵͳ";
                                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["ȷ����"].Ordinal].Value = this.hashTablePeople.ContainsKey(applyOut.Operation.ApproveOper.ID) ? this.hashTablePeople[applyOut.Operation.ApproveOper.ID].ToString() : "ϵͳ";
                                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����ʱ��"].Ordinal].Value = applyOut.Operation.ApplyOper.OperTime.ToString();
                                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["ȷ��ʱ��"].Ordinal].Value = applyOut.Operation.ApproveOper.OperTime.ToString();
                            }
                            else if (dicReturnApply[feeItemList.RecipeNO + feeItemList.SequenceNO.ToString()] is FS.HISFC.Models.Fee.ReturnApply)
                            {
                                returnApply = dicReturnApply[feeItemList.RecipeNO + feeItemList.SequenceNO.ToString()] as FS.HISFC.Models.Fee.ReturnApply;
                                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Value = returnApply.Item.Qty;
                            }
                        }
                    }

                    if (FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["����������"].Ordinal].Value) > 0)
                    {
                        this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["ȡ������"].Ordinal].Locked = false;
                        this.fpFeeDetail_Sheet1.Rows[row].BackColor = Color.Aqua;
                    }
                }
            }

            //�����˷�
            if (feeItemList.NoBackQty <= 0 || feeItemList.Item.Qty <= 0)
            {
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�"].Ordinal].Locked = true;
                if (feeItemList.Item.Qty <= 0)
                {
                    this.fpFeeDetail_Sheet1.Rows[row].BackColor = Color.Pink;
                }
            }
            else
            {
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�"].Ordinal].Locked = false;
                this.fpFeeDetail_Sheet1.Rows[row].BackColor = Color.White;
            }

            bool isQuitPriv = true;
            //�����˷�Ȩ�޵�����
            if (this.isCanQuitOtherOperator==false)
            {
                //�жϵ�ǰ��¼�����շ����Ƿ�һ��
                if (feeItemList.FeeOper.ID != this.feeManager.Operator.ID)
                {
                    isQuitPriv = false;
                }
            }

            //�����˷ѿ�������
            if (this.isCanQuitOtherDeptFee==false)
            {
                if (this.depts.ContainsKey(feeItemList.FeeOper.Dept.ID)==false)
                {
                    isQuitPriv = false;
                }
            }

            //����Ƿ������˷�
            if (isQuitPriv&&this.dtFeeDetail.Columns.Contains("�˷�����"))
            {
                numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                numberCellType.DecimalPlaces = 2;
                numberCellType.MinimumValue = 0.00F;
                numberCellType.MaximumValue = Double.Parse(feeItemList.NoBackQty.ToString("F2"));//ֻ�������ֵΪ����������
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].CellType = numberCellType;
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Locked = !isQuitPriv;
                this.fpFeeDetail_Sheet1.Cells[row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Value = 0.00F;
            }
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

            this.fpMainInfo_Sheet1.DataAutoSizeColumns = false;
            this.fpPrepay_Sheet1.DataAutoSizeColumns = false;
            this.fpFeeDaySum_Sheet1.DataAutoSizeColumns = false;
            this.fpFeeDetail_Sheet1.DataAutoSizeColumns = false;
            this.fpFee_Sheet1.DataAutoSizeColumns = false;
            this.fpBalance_Sheet1.DataAutoSizeColumns = false;
            this.fpShiftData_Sheet1.DataAutoSizeColumns = false;
            this.fpFeeItemSum_Sheet1.DataAutoSizeColumns = false;

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
																new DataColumn("��������", date)/*,
																new DataColumn("ҽ�����", str)*/});

                dtMainInfo.PrimaryKey = new DataColumn[] { dtMainInfo.Columns["סԺ��ˮ��"] };

                dvMainInfo = new DataView(dtMainInfo);

                this.fpMainInfo_Sheet1.DataSource = dvMainInfo;
                
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);
            }

            #endregion

            #region ������Ŀ����

            if (System.IO.File.Exists(this.pathNameFeeItemSum))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFeeItemSum, dtFeeItemSum, ref dvFeeItemSum, this.fpFeeItemSum_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFeeItemSum_Sheet1, pathNameFeeItemSum);
            }
            else
            {
                dtFeeItemSum.Columns.AddRange(new DataColumn[]{
                                                                new DataColumn("��Ŀ����", str),
																new DataColumn("������", dec),
																new DataColumn("���", dec),
                                                                new DataColumn("��Ŀ����", str),
                                                                new DataColumn("��С���ñ���", str),
                                                                new DataColumn("ƴ����", str),
                                                                new DataColumn("�����", str),
                                                                new DataColumn("�Զ�����", str),
                                                                new DataColumn("ѡ��",bo)}
                                                                );

                dvFeeItemSum = new DataView(dtFeeItemSum);

                this.fpFeeItemSum_Sheet1.DataSource = dvFeeItemSum;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeItemSum_Sheet1, this.pathNameFeeItemSum);
            }

            #endregion

            #region ���õ������

            if (System.IO.File.Exists(this.pathNameFeeDaySum))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFeeDaySum, dtFeeDaySum, ref dvFeeDaySum, this.fpFeeDaySum_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFeeDaySum_Sheet1, pathNameFeeDaySum);
            }
            else
            {
                dtFeeDaySum.Columns.AddRange(new DataColumn[]{
																new DataColumn("�շ�ʱ��", date),
                                                                new DataColumn("��Ŀ����", str),
																new DataColumn("�շ�����", dec),
																new DataColumn("���", dec),
                                                                new DataColumn("��Ŀ����", str)}
                                                                );

                dvFeeDaySum = new DataView(dtFeeDaySum);

                this.fpFeeDaySum_Sheet1.DataSource = dvFeeDaySum;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeDaySum_Sheet1, this.pathNameFeeDaySum);
            }

            #endregion

            #region ������ϸ��Ϣ
            if (System.IO.File.Exists(this.pathNameFeeDetail))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameFeeDetail, dtFeeDetail, ref dvFeeDetail, this.fpFeeDetail_Sheet1);

                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpFeeDetail_Sheet1, pathNameFeeDetail);
            }
            else
            {
                dtFeeDetail.Columns.AddRange(new DataColumn[]{
                                                                  new DataColumn("�շ�ʱ��", str),
                                                                  new DataColumn("�Ʒ�ʱ��", str),
                                                                  new DataColumn("�˷�ʱ��", str),
																  new DataColumn("��Ŀ����", str),
																  new DataColumn("����", dec),
																  new DataColumn("��������", dec),
																  new DataColumn("��λ", str),
																  new DataColumn("���", dec),
																  new DataColumn("�շ���", str),
																  new DataColumn("��������", str),
																  new DataColumn("����ҽ��", str),
                                                                  new DataColumn("Ӧִ��ʱ��", str),
																  new DataColumn("ִ�п���", str),
				                                                  new DataColumn("������Դ", str),
				                                                  new DataColumn("�˷�", bo),
				                                                  new DataColumn("�˷�����", dec),
				                                                  new DataColumn("ȡ������", bo),
				                                                  new DataColumn("����������", dec),
				                                                  new DataColumn("������", str),
				                                                  new DataColumn("����ʱ��", str),
				                                                  new DataColumn("ȷ����", str),
				                                                  new DataColumn("ȷ��ʱ��", str),
				                                                  new DataColumn("������", str),
				                                                  new DataColumn("��������ˮ��", str),
                                                                  new DataColumn("��������", str)});

                dvFeeDetail = new DataView(dtFeeDetail);

                this.fpFeeDetail_Sheet1.DataSource = dvFeeDetail;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeDetail_Sheet1, pathNameFeeDetail);
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

            if (System.IO.File.Exists(pathNameShiftData))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.CreatColumnByXML(pathNameShiftData, dtShiftData, ref dvShiftData, this.fpShiftData_Sheet1);
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpShiftData_Sheet1, pathNameShiftData);
            }
            else
            {
                dtShiftData.Columns.AddRange(new DataColumn[]{new DataColumn("�������", str),
															   new DataColumn("�������", str),
															   new DataColumn("���ǰ����", str),
															   new DataColumn("���ǰ����", str),
															   new DataColumn("��������", str),
															   new DataColumn("���������", str),
															   new DataColumn("���ʱ��", str),//Ҫ��ʾʱ��㲻����date
															   new DataColumn("�����", str)});

                dvShiftData = new DataView(dtShiftData);

                this.fpShiftData_Sheet1.DataSource = dvShiftData;

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpShiftData_Sheet1, this.pathNameShiftData);
            }

            #endregion

            this.InitHashTable();

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

            this.ucQueryInpatientNo1.Text = this.currentPatient.PID.PatientNO;//סԺ��
            this.lblName.Text = this.currentPatient.Name;//����;
            this.lblSex.Text = this.currentPatient.Sex.Name;
            this.lblAge.Text = this.currentPatient.Age;
            this.lblBed.Text = this.currentPatient.PVisit.PatientLocation.Bed.ID.Length > 4 ? this.currentPatient.PVisit.PatientLocation.Bed.ID.Substring(4) : this.currentPatient.PVisit.PatientLocation.Bed.ID; 
            this.lblDept.Text = this.currentPatient.PVisit.PatientLocation.Dept.Name;

            if (this.currentPatient.Pact.PayKind.ID == "03")
            {
                FS.HISFC.Models.Base.PactInfo pact = this.PactManagment.GetPactUnitInfoByPactCode(this.currentPatient.Pact.ID);
                this.lblPact.Text = this.currentPatient.Pact.Name + " �Ը�������" + pact.Rate.PayRate * 100 + "%" +" ���޶�:" + this.currentPatient.FT.DayLimitCost;//��ͬ��λ
            }
            else
            {
                this.lblPact.Text = this.currentPatient.Pact.Name;//��ͬ��λ
            }

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
            this.lblFreeCost.Text = this.currentPatient.FT.LeftCost.ToString();
            this.lblDiagnose.Text = this.currentPatient.ClinicDiagnose;     //��Ժ���-������ϣ������ǻ�����Ժ�������
            this.lblMemo.Text = this.currentPatient.Memo;   //��ע��Ҫ��ʾ����

            //ȡ��Ժ����
            int day = this.radtIntegrate.GetInDays(this.currentPatient.ID);
            if (day>1)
            {
                day -= 1;
            }
            this.txtDays.Text = day.ToString();
            //ȡ����ʱ��
            this.txtArriveDate.Text = this.radtIntegrate.GetArriveDate(this.currentPatient.ID).ToShortDateString();

            //ȡ��Ժ���
            try
            {
                ArrayList alInDiag = this.diagMgr.QueryDiagnoseByInpatientNOAndPersonType(this.currentPatient.ID, "1");
                if (alInDiag != null && alInDiag.Count > 0)
                {
                    //ȡ���µ���Ժ���
                    this.lblInDiagInfo.Text = (alInDiag[0] as FS.HISFC.Models.HealthRecord.Diagnose).DiagInfo.ICD10.Name;
                }
            }
            catch (Exception ex) { }


        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        protected void QueryAllInfomaition(DateTime beginTime, DateTime endTime) 
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��Ϣ�����Ժ�...");
            Application.DoEvents();
            hsMinFee.Clear();
            hsDayFeeItemList.Clear();
            dicReturnApply.Clear();

            if (this.neuTabControl1.TabPages.Contains(this.tpFeeInfo))
            {
                this.QueryPatientReturyApply(this.currentPatient);
                this.QueryPatientDrugList(beginTime, endTime);
                this.QueryPatientUndrugList(beginTime, endTime);
                this.QueryPatientFeeInfo(beginTime, endTime);
            }

            if (this.neuTabControl1.TabPages.Contains(this.tpMainInfo))
            {
                this.QueryPatientPrepayList(beginTime, endTime);
                this.QueryPatientBalance(beginTime, endTime);
                this.QueryPatientShiftData(this.currentPatient);
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
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
        public void Clear() 
        {
            this.ucQueryInpatientNo1.Text = this.RetrunSplit(10);
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
            this.txtArriveDate.Text = this.RetrunSplit(20);
            this.txtDays.Text = this.RetrunSplit(3);
            this.lblInDiagInfo.Text = this.RetrunSplit(30);  //��Ժ���

            dtMainInfo.Rows.Clear();
            dtFeeDaySum.Rows.Clear();
            dtFeeDetail.Rows.Clear();
            dtPrepay.Rows.Clear();
            dtFee.Rows.Clear();
            dtBalance.Rows.Clear();
            dtShiftData.Clear();
            dtFeeItemSum.Rows.Clear();
            hsQuitFeeByPackage.Clear();
            dicReturnApply.Clear();
            alAllFeeItemLists = new ArrayList();
        }

        private void InitContr()
        {
            this.neuLabel21.Visible = false;
            this.lblOwnCost.Visible = false;
            this.neuLabel23.Visible = false;
            this.lblPubCost.Visible = false;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (neuObject is FS.HISFC.Models.RADT.PatientInfo)
            {
                if (this.currentPatient != null && this.currentPatient.ID.Equals(((FS.HISFC.Models.RADT.PatientInfo)neuObject).ID))
                {
                    return 1;
                }

                this.PatientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            }
            
            return base.OnSetValue(neuObject, e);
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            if (alValues != null && alValues.Count > 0)
            {
                this.Clear();
                foreach (FS.HISFC.Models.RADT.PatientInfo patientInfo in alValues)
                {
                    this.SetPatientToFpMain(patientInfo);
                }
            }
            return base.OnSetValues(alValues, e);
        }

        private void SelectFeeInfo()
        {
            if (this.fpFee_Sheet1.RowCount > 0)
            {
                this.fpFee_Sheet1.ActiveRowIndex = 0;
                this.fpFee_Sheet1.AddSelection(0, 0, 1, 1);
                this.fpFee_SelectionChanged(null, null);

            }
        }

        private void SelectFeeItemSum(string feeCode)
        {
            if (this.fpFeeItemSum_Sheet1.RowCount > 0)
            {
                //�����ǰѡ��ı���=��0�� �򲻱�
                if (this.fpFeeItemSum_Sheet1.ActiveRowIndex < 0)
                {
                    this.fpFeeItemSum_Sheet1.ActiveRowIndex = 0;
                    this.fpFeeItemSum_Sheet1.AddSelection(0, 0, 1, 1);
                    this.fpFeeItemSum_SelectionChanged(null, null);
                }
                else
                {
                    this.fpFeeItemSum_Sheet1.ActiveRowIndex = 0;
                    this.fpFeeItemSum_Sheet1.AddSelection(0, 0, 1, 1);
                    if (this.dtFeeDaySum.Rows.Count == 0 || feeCode != this.fpFeeItemSum_Sheet1.Cells[0, this.dtFeeItemSum.Columns["��Ŀ����"].Ordinal].Text)
                    {
                        this.fpFeeItemSum_SelectionChanged(null, null);
                    }
                }
            }
        }

        private void SelectFeeItemDaySum()
        {
            this.fpFeeDaySum_Sheet1.SortRows(0, false, true);
            if (this.fpFeeDaySum_Sheet1.RowCount > 0)
            {
                this.fpFeeDaySum_Sheet1.ActiveRowIndex = 0;
                this.fpFeeDaySum_Sheet1.AddSelection(0, 0, 1, 1);
                this.fpFeeDaySum_SelectionChanged(null, null);
            }
        }

        /// <summary>
        /// ���ݷ�����Ϣ��ȡ�����Ŀ��Ψһֵ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private string GetPackageItemKey(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            //�Ƿ�����ϸ��ʾ
            if (this.ckUnCombo.Checked)
            {
                return feeItemList.UndrugComb.ID + feeItemList.TransType.ToString() + feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmm") + feeItemList.ExecOrder.ID;
            }
            else
            {
                return feeItemList.Item.ID + feeItemList.TransType.ToString() + feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmm") + feeItemList.ExecOrder.ID;
            }
        }
        /// <summary>
        /// ���ݷ�����Ϣ��ȡ�����Ŀ��Ψһֵ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private string GetPackageItemKey(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList,FS.HISFC.Models.Base.TransTypes transType)
        {
            //�Ƿ�����ϸ��ʾ
            if (this.ckUnCombo.Checked)
            {
                return feeItemList.UndrugComb.ID + transType.ToString() + feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmm") + feeItemList.ExecOrder.ID;
            }
            else
            {
                return feeItemList.Item.ID + transType.ToString() + feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmm") + feeItemList.ExecOrder.ID;
            }
        }

        /// <summary>
        /// ���ݷ�����Ϣ��ȡ�����Ŀ��Ψһֵ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private string GetPackageItemQuitKey(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            //�Ƿ�����ϸ��ʾ
            if (this.ckUnCombo.Checked)
            {
                return feeItemList.UndrugComb.ID + "|" + feeItemList.TransType.ToString() + "|" + feeItemList.RecipeNO + feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmm") + feeItemList.ExecOrder.ID;
            }
            else
            {
                return feeItemList.Item.ID + "|" + feeItemList.TransType.ToString() + "|" + feeItemList.RecipeNO + feeItemList.FeeOper.OperTime.ToString("yyyyMMddHHmm") + feeItemList.ExecOrder.ID;
            }
        }

        /// <summary>
        /// ��ȡ��Ŀ����С����
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private string GetItemFeeCode(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (this.ckUnCombo.Checked)
            {
                return string.IsNullOrEmpty(feeItemList.UndrugComb.MinFee.ID) ? feeItemList.Item.MinFee.ID : feeItemList.UndrugComb.MinFee.ID;
            }
            else
            {
                return feeItemList.Item.MinFee.ID;
            }

        }
        
        /// <summary>
        /// ��ȡ��Ŀ��ID
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private string GetItemID(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (this.ckUnCombo.Checked)
            {
                return string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.ID : feeItemList.UndrugComb.ID;
            }
            else
            {
                return feeItemList.Item.ID;
            }
        }

        /// <summary>
        /// ��ȡ������Ŀ����
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private string GetItemName(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            if (this.ckUnCombo.Checked == false)
            {
                return string.IsNullOrEmpty(feeItemList.UndrugComb.Name) ? feeItemList.Item.Name :   feeItemList.UndrugComb.Name + "/"+feeItemList.Item.Name ;
            }
            else
            {
                return feeItemList.Item.Name;
            }
        }
        
        /// <summary>
        /// �Ƿ񸴺���Ŀ��ʾ
        /// </summary>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        private bool IsPackageItem(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return this.ckUnCombo.Checked ? string.IsNullOrEmpty(feeItemList.UndrugComb.ID) == false : false;
        }

        #endregion


        /// <summary>
        /// ����ToolBar�ؼ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("ˢ��", "ˢ�»�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sˢ��, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "ˢ��":

                    if (this.tv != null)
                    {
                        this.tv.Refresh();
                    }
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPage(0, 0, this.neuTabControl1.SelectedTab);
            
            return base.OnPrint(sender, neuObject);
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

        private void ucPatientFeeQuery_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.InitDataTable();
            this.InitContr();

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

            //Ĭ�Ϲ�����ʾ������ϸ
            this.ckShowBackDetail.Checked = true;
          
        }

        private void fpMainInfo_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpMainInfo_Sheet1.RowCount <= 0)
            {
                return;
            }
            string inpatientNO = this.fpMainInfo_Sheet1.Cells[this.fpMainInfo_Sheet1.ActiveRowIndex, 0].Text;

            if (this.currentPatient == null || this.currentPatient.ID.Equals(inpatientNO) == false)
            {
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

                dtFeeDaySum.Rows.Clear();
                dtFeeDetail.Rows.Clear();
                dtFeeItemSum.Rows.Clear();
                dtPrepay.Rows.Clear();
                dtFee.Rows.Clear();
                dtBalance.Rows.Clear();
                dtShiftData.Rows.Clear();

                //���ò�ѯʱ��
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                this.QueryAllInfomaition(beginTime, endTime);
            }
        }

        private void fpMainInfo_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpMainInfo_SelectionChanged(sender, null);
            this.neuTabControl1.SelectedTab = this.tpFeeInfo;
        }

        private void fpFee_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.fpFeeItemSum_Sheet1.RowCount = 0;
            this.dtFeeItemSum.Clear();
            this.txtItemFilter.Text = "";
            this.dvFeeItemSum.RowFilter = "1=1";

            this.fpFeeDaySum_Sheet1.RowCount = 0;
            this.dtFeeDaySum.Clear();
            this.dtpItemDayFilter.Checked = false;
            this.dvFeeDaySum.RowFilter = "1=1";

            this.fpFeeDetail_Sheet1.RowCount = 0;
            this.dtFeeDetail.Clear();

            string feeCode = this.fpFee_Sheet1.Cells[this.fpFee_Sheet1.ActiveRowIndex, 7].Text;
            string balanceState = this.fpFee_Sheet1.Cells[this.fpFee_Sheet1.ActiveRowIndex, 6].Text;

            Hashtable hsItemsItemSum = new Hashtable();
            if (this.hsMinFee.ContainsKey(feeCode))
            {
                this.alAllFeeItemLists = this.hsMinFee[feeCode] as ArrayList;
            }


            //�۸񼯺�
            Dictionary<string, decimal> priceCollection = new Dictionary<string, decimal>();
            Dictionary<string, string> priceItemCollection = new Dictionary<string, string>();
            Dictionary<string, string> packageCollection = new Dictionary<string, string>();

            for (int i = alAllFeeItemLists.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList item = this.alAllFeeItemLists[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                if (this.GetItemFeeCode(item)!= feeCode)
                    continue;

                string itemCode = item.Item.ID;
                string itemName = item.Item.Name;

                #region ��ʾ������Ŀ/��ϸ
                if (this.IsPackageItem(item))
                {
                    string key = this.GetPackageItemKey(item);
                    if (item.UndrugComb != null && item.UndrugComb.ID.Length > 0)
                    {
                        itemCode = item.UndrugComb.ID;
                        itemName = item.UndrugComb.Name;
                        if (priceCollection.ContainsKey(key))
                        {
                            if (priceItemCollection.ContainsKey(key + item.Item.ID) == false)
                            {
                                priceCollection[key] += Math.Abs(item.FT.TotCost);
                                priceItemCollection[key + item.Item.ID] = item.Item.ID;
                            }
                        }
                        else
                        {
                            priceCollection[key] = Math.Abs(item.FT.TotCost);
                            priceItemCollection[key + item.Item.ID] = item.Item.ID;
                        }
                    }
                }
                #endregion

                if (!hsItemsItemSum.Contains( itemCode))
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itemSum = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    itemSum.FeeOper.OperTime = item.FeeOper.OperTime;
                    itemSum.Item.ID = itemCode;
                    itemSum.Item.Name = itemName;
                    itemSum.RecipeNO = item.RecipeNO;
                    itemSum.TransType = item.TransType;
                    itemSum.NoBackQty = item.NoBackQty;
                    itemSum.UndrugComb = item.UndrugComb.Clone();
                    itemSum.ExecOrder.ID = item.ExecOrder.ID;

                    //��ȡ��Ŀ
                    FS.HISFC.Models.Base.Item baseItem = null;
                    if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                    {
                        baseItem = CommonController.Instance.GetItem(itemCode);
                    }
                    else if (item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        baseItem = this.phamarcyIntegrate.GetItem(itemCode);
                    }

                    itemSum.UndrugComb = item.UndrugComb.Clone();
                    if (this.IsPackageItem(item))
                    {
                        itemSum.Item.Qty = item.Item.Qty > 0 ? 1 : -1;
                        packageCollection.Add(this.GetPackageItemKey(item), null);
                    }
                    else
                    {
                        itemSum.Item.Qty = item.Item.Qty;
                    }

                    itemSum.FT.TotCost = item.FT.TotCost;
                    itemSum.FT.OwnCost = item.FT.OwnCost;
                    itemSum.FT.PubCost = item.FT.PubCost;
                    itemSum.Item.SpellCode = (baseItem == null ? string.Empty : baseItem.SpellCode);//ƴ����Ϊ��ʱ�쳣
                    itemSum.Item.WBCode = (baseItem == null ? string.Empty : baseItem.WBCode);
                    itemSum.Item.UserCode = (baseItem == null ? string.Empty : baseItem.UserCode);
                    itemSum.Item.Specs = (baseItem == null ? string.Empty : baseItem.Specs);
                    itemSum.FeeOper.OperTime = item.FeeOper.OperTime;
                    itemSum.ExecOrder.ID = item.ExecOrder.ID;

                    hsItemsItemSum.Add( itemSum.Item.ID, itemSum);
                }
                else
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itemSum = hsItemsItemSum[ itemCode] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    if (this.IsPackageItem(item))
                    {
                        string key = this.GetPackageItemKey(item);
                        if (packageCollection.ContainsKey(key) == false && item.UndrugComb.Qty != 0)
                        {
                            itemSum.UndrugComb.Qty += item.UndrugComb.Qty;
                            packageCollection.Add(key, null);
                        }
                    }
                    else
                    {
                        itemSum.Item.Qty += item.Item.Qty;
                    }
                    itemSum.FT.TotCost += item.FT.TotCost;
                    itemSum.FT.OwnCost += item.FT.OwnCost;
                    itemSum.FT.PubCost += item.FT.PubCost;
                }
            }

            foreach (System.Collections.DictionaryEntry objOrder in hsItemsItemSum)
            {
                //this.fpFeeSum_Sheet1.Rows.Add(0, 1);
                DataRow dr = this.dtFeeItemSum.NewRow();

                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemSum = objOrder.Value as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                dr["��Ŀ����"] = this.GetItemName(itemSum) + (string.IsNullOrEmpty(itemSum.Item.Specs) ? "" : "[" + itemSum.Item.Specs + "]");
                if (itemSum.UndrugComb != null && itemSum.UndrugComb.ID.Length > 0)
                {
                    string key = this.GetPackageItemKey(itemSum);
                    string keyPositive = this.GetPackageItemKey(itemSum, FS.HISFC.Models.Base.TransTypes.Positive);
                    if (itemSum.UndrugComb.Qty != 0)
                    {
                        dr["������"] = itemSum.UndrugComb.Qty;
                    }
                    else if (priceCollection.ContainsKey(keyPositive))
                    {

                        dr["������"] =
                               FS.FrameWork.Public.String.FormatNumberReturnString(itemSum.FT.TotCost / priceCollection[keyPositive], 0);
                    }
                    else if (priceCollection.ContainsKey(key))
                    {
                        dr["������"] =
                               FS.FrameWork.Public.String.FormatNumberReturnString(itemSum.FT.TotCost / priceCollection[key], 0);
                    }
                    else
                    {
                        dr["������"] = itemSum.Item.Qty;
                    }

                }
                else
                {
                    dr["������"] = itemSum.Item.Qty.ToString();
                }
                dr["���"] = itemSum.FT.TotCost.ToString();
                dr["��Ŀ����"] = itemSum.Item.ID;
                dr["��С���ñ���"] = feeCode;
                dr["ƴ����"] = itemSum.Item.SpellCode;
                dr["�����"] = itemSum.Item.WBCode;
                dr["�Զ�����"] = itemSum.Item.UserCode;

                dtFeeItemSum.Rows.Add(dr);
            }
            dtFeeItemSum.AcceptChanges();
            this.fpFeeItemSum_Sheet1.SortRows(0, true, true);
            this.SelectFeeItemSum("");
        }

        private void fpFeeItemSum_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {

            this.fpFeeDaySum_Sheet1.RowCount = 0;
            this.dtFeeDaySum.Clear();
            this.dtpItemDayFilter.Checked = false;
            this.dvFeeDaySum.RowFilter = "1=1";

            this.fpFeeDetail_Sheet1.RowCount = 0;
            this.dtFeeDetail.Clear();

            string feeCode = this.fpFeeItemSum_Sheet1.Cells[this.fpFeeItemSum_Sheet1.ActiveRowIndex, 4].Text;
            string itemCode = this.fpFeeItemSum_Sheet1.Cells[this.fpFeeItemSum_Sheet1.ActiveRowIndex, 3].Text;
            Hashtable hsItemsDaySum = new Hashtable();
            if (this.hsMinFee.ContainsKey(feeCode))
            {
                this.alAllFeeItemLists = this.hsMinFee[feeCode] as ArrayList;
            }

            //�۸񼯺�
            Dictionary<string, decimal> priceCollection = new Dictionary<string, decimal>();
            Dictionary<string, string> priceItemCollection = new Dictionary<string, string>();
            Dictionary<string, string> packageCollection = new Dictionary<string, string>();

            for (int i = alAllFeeItemLists.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList item = this.alAllFeeItemLists[i] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                //�ų�1����С���ò�ͬ
                //2����Ŀ���벻ͬ�����Ҹ�����Ŀ����Ҳ����ͬ��
                //3����Ŀ������ͬ������������������Ŀ��
                if (this.GetItemFeeCode(item) != feeCode||
                    this.GetItemID(item)!=itemCode
                    //|| (itemCode != item.Item.ID && item.UndrugComb.ID != itemCode) ||
                    //(itemCode == item.Item.ID && string.IsNullOrEmpty(item.UndrugComb.ID) == false)
                    )
                {
                    continue;
                }

                string itemName = item.Item.Name;

                #region ��ʾ��ϸ/������Ŀ
                if (this.IsPackageItem(item))
                {
                    string key = this.GetPackageItemKey(item);
                    if (item.UndrugComb != null && item.UndrugComb.ID.Length > 0)
                    {
                        itemCode = item.UndrugComb.ID;
                        itemName = item.UndrugComb.Name;
                        if (priceCollection.ContainsKey(key))
                        {
                            if (priceItemCollection.ContainsKey(key + item.Item.ID) == false)
                            {
                                priceCollection[key] += Math.Abs(item.FT.TotCost);
                                priceItemCollection[key + item.Item.ID] = item.Item.ID;
                            }
                        }
                        else
                        {
                            priceCollection[key] = Math.Abs(item.FT.TotCost);
                            priceItemCollection[key + item.Item.ID] = item.Item.ID;
                        }
                    }
                }
                #endregion

                if (!hsItemsDaySum.Contains(item.ChargeOper.OperTime.ToString("yyyy-MM-dd") + itemCode))
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itemSum = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();

                    itemSum.FeeOper.OperTime = item.FeeOper.OperTime;
                    itemSum.Item.ID = itemCode;
                    itemSum.Item.Name = itemName;
                    itemSum.ChargeOper.OperTime = item.ChargeOper.OperTime;
                    itemSum.FeeOper.OperTime = item.FeeOper.OperTime;
                    itemSum.RecipeNO = item.RecipeNO;
                    itemSum.TransType = item.TransType;
                    itemSum.ExecOrder.ID = item.ExecOrder.ID;
                    itemSum.UndrugComb = item.UndrugComb.Clone();

                    if (this.IsPackageItem(item))
                    {
                        itemSum.Item.Qty = item.Item.Qty > 0 ? 1 : -1;
                        packageCollection.Add(this.GetPackageItemKey(item), null);
                    }
                    else
                    {
                        itemSum.Item.Qty = item.Item.Qty;
                    }
                    itemSum.FT.TotCost = item.FT.TotCost;
                    itemSum.FT.OwnCost = item.FT.OwnCost;
                    itemSum.FT.PubCost = item.FT.PubCost;
                    hsItemsDaySum.Add(itemSum.ChargeOper.OperTime.ToString("yyyy-MM-dd") + itemSum.Item.ID, itemSum);
                }
                else
                {

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList itemSum = hsItemsDaySum[item.ChargeOper.OperTime.ToString("yyyy-MM-dd") + itemCode] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    if (this.IsPackageItem(item))
                    {
                        string key = this.GetPackageItemKey(item);
                        if (packageCollection.ContainsKey(key) == false && item.UndrugComb.Qty != 0)
                        {
                            itemSum.UndrugComb.Qty += item.UndrugComb.Qty;
                            packageCollection.Add(key, null);
                        }
                    }
                    else
                    {
                        itemSum.Item.Qty += item.Item.Qty;
                    }
                    itemSum.FT.TotCost += item.FT.TotCost;
                    itemSum.FT.OwnCost += item.FT.OwnCost;
                    itemSum.FT.PubCost += item.FT.PubCost;
                }
            }

            foreach (System.Collections.DictionaryEntry objOrder in hsItemsDaySum)
            {
                //this.fpFeeSum_Sheet1.Rows.Add(0, 1);
                DataRow dr = this.dtFeeDaySum.NewRow();

                FS.HISFC.Models.Fee.Inpatient.FeeItemList itemSum = objOrder.Value as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                dr["�շ�ʱ��"] = itemSum.ChargeOper.OperTime.ToString("yyyy-MM-dd");
                dr["��Ŀ����"] = this.GetItemName(itemSum);
                if (itemSum.UndrugComb != null && itemSum.UndrugComb.ID.Length > 0)
                {
                    string key = this.GetPackageItemKey(itemSum);
                    string keyPositive = this.GetPackageItemKey(itemSum, FS.HISFC.Models.Base.TransTypes.Positive);
                    if (itemSum.UndrugComb.Qty > 0)
                    {
                        dr["�շ�����"] = itemSum.UndrugComb.Qty;
                    }
                    else if (priceCollection.ContainsKey(keyPositive))
                    {
                        dr["�շ�����"] =
                               FS.FrameWork.Public.String.FormatNumberReturnString(itemSum.FT.TotCost / priceCollection[keyPositive], 0);
                    }
                    else if (priceCollection.ContainsKey(key))
                    {
                        dr["�շ�����"] =
                               FS.FrameWork.Public.String.FormatNumberReturnString(itemSum.FT.TotCost / priceCollection[key], 0);
                    }
                    else
                    {
                        dr["�շ�����"] = itemSum.Item.Qty;
                    }
                }
                else
                {
                    dr["�շ�����"] = itemSum.Item.Qty.ToString();
                }
                dr["���"] = itemSum.FT.TotCost.ToString();
                dr["��Ŀ����"] = itemSum.Item.ID;

                dtFeeDaySum.Rows.Add(dr);
            }
            dtFeeDaySum.AcceptChanges();
            this.SelectFeeItemDaySum();
        }

        private void fpFeeDaySum_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.fpFeeDetail_Sheet1.Rows.Remove(0, this.fpFeeDetail_Sheet1.Rows.Count);

            this.dtFeeDetail.Clear();
            string feeCode = this.fpFeeDaySum_Sheet1.Cells[this.fpFeeDaySum_Sheet1.ActiveRowIndex, 4].Text;
            string feeName = this.fpFeeDaySum_Sheet1.Cells[this.fpFeeDaySum_Sheet1.ActiveRowIndex, 1].Text;
            string feeDate = this.fpFeeDaySum_Sheet1.Cells[this.fpFeeDaySum_Sheet1.ActiveRowIndex, 0].Text;
            string minFeeCode = this.fpFeeItemSum_Sheet1.Cells[this.fpFeeItemSum_Sheet1.ActiveRowIndex, 4].Text;

            if (this.hsDayFeeItemList.ContainsKey(feeCode + feeDate))
            {
                this.alAllFeeItemLists = this.hsDayFeeItemList[feeCode + feeDate] as ArrayList;
            }
            //Hashtable �����ж��Ƿ�������Ŀ
            Hashtable hsPackageItem = new Hashtable();
            hsQuitFeeByPackage.Clear();
            //�˷������ĸ�ʽ
            numberCellType.DecimalPlaces = 2;//������λС��

            #region ���з���
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in this.alAllFeeItemLists)
            {
                if (this.GetItemFeeCode(item) != minFeeCode)
                {
                    continue;
                }

                if (this.GetItemID(item)== feeCode && item.ChargeOper.OperTime.ToString("yyyy-MM-dd") == FS.FrameWork.Function.NConvert.ToDateTime(feeDate).ToString("yyyy-MM-dd"))
                {
                    //��ȡ���ݿ�
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = this.feeManager.GetItemListByRecipeNO(item.RecipeNO, item.SequenceNO, item.TransType, item.Item.ItemType);

                    if (this.IsPackageItem(feeItemList))
                    {
                        string key = this.GetPackageItemKey(feeItemList);
                        //�����Ŀ�ĺϲ�
                        if (string.IsNullOrEmpty(feeItemList.UndrugComb.ID) == false)
                        {
                            if (hsPackageItem.ContainsKey(key))
                            {
                                ((ArrayList)hsPackageItem[key]).Add(feeItemList);
                            }
                            else
                            {
                                //�½�
                                ArrayList al = new ArrayList();
                                al.Add(feeItemList);
                                hsPackageItem[key] = al;
                            }
                            continue;
                        }
                    }

                    if (this.AddFeeDetailToDataTable(feeItemList) > 0)
                    {
                        this.fpFeeDetail_Sheet1.Rows[this.fpFeeDetail_Sheet1.RowCount - 1].Tag = feeItemList;
                        this.SetFpFeeDetailFormat(feeItemList, this.fpFeeDetail_Sheet1.RowCount - 1, null);
                    }
                }
            }
            #endregion

            #region ��������Ŀ
            //���������Ŀ
            if (hsPackageItem.Count > 0)
            {
                foreach (DictionaryEntry de in hsPackageItem)
                {
                    ArrayList al = de.Value as ArrayList;
                    if (al != null && al.Count > 0)
                    {
                        FeeItemList feeItemListFirst = ((FeeItemList)al[0]).Clone();
                        ReturnApply returnApply = null;
                        decimal sumCost = 0.00M;
                        decimal sumNobackCost = 0.00M;
                        decimal sumReturnCost = 0.00M;

                        //�۸񼯺�
                        Dictionary<string, decimal> priceCollection = new Dictionary<string, decimal>();
                        Dictionary<string, string> priceItemCollection = new Dictionary<string, string>();
                        ArrayList alList = new ArrayList();
                        ArrayList alReturnApplyList = new ArrayList();
                        foreach (FeeItemList feeItemList in al)
                        {
                            if (feeItemList.Item.PackQty == 0)
                            {
                                feeItemList.Item.PackQty = 1;
                            }

                            sumNobackCost += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.NoBackQty / feeItemList.Item.PackQty, 2);
                            sumCost += FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);

                            string key = this.GetPackageItemKey(feeItemList);
                            if (priceCollection.ContainsKey(key))
                            {
                                if (priceItemCollection.ContainsKey(key + feeItemList.Item.ID) == false)
                                {
                                    priceCollection[key] += Math.Abs(feeItemList.FT.TotCost);
                                    priceItemCollection[key + feeItemList.Item.ID] = feeItemList.Item.ID;
                                }
                            }
                            else
                            {
                                priceCollection[key] = Math.Abs(feeItemList.FT.TotCost);
                                priceItemCollection[key + feeItemList.Item.ID] = feeItemList.Item.ID;
                            }

                            if (feeItemList.NoBackQty > 0)
                            {
                                alList.Add(feeItemList);
                            }
                            if (feeItemList.TransType == FS.HISFC.Models.Base.TransTypes.Positive && dicReturnApply.ContainsKey(feeItemList.RecipeNO + feeItemList.SequenceNO))
                            {
                                alReturnApplyList.Add(feeItemList);
                                returnApply = (dicReturnApply[feeItemList.RecipeNO + feeItemList.SequenceNO] as ReturnApply).Clone();
                                 sumReturnCost += FS.FrameWork.Public.String.FormatNumber(returnApply.Item.Price * returnApply.Item.Qty / returnApply.Item.PackQty, 2);
                                   
                            }

                        }

                        FS.HISFC.Models.Fee.Item.Undrug undrug = SOC.HISFC.BizProcess.CommonInterface.CommonController.Instance.GetItem(feeItemListFirst.UndrugComb.ID);

                        feeItemListFirst.Item = undrug;
                        if (feeItemListFirst.Item.PackQty == 0)
                        {
                            feeItemListFirst.Item.PackQty = 1;
                        }
                        string keyCombo = this.GetPackageItemKey(feeItemListFirst);

                        if (feeItemListFirst.UndrugComb.Qty != 0)
                        {
                            feeItemListFirst.Item.Qty = feeItemListFirst.UndrugComb.Qty;
                        }
                        else if (priceCollection.ContainsKey(keyCombo))
                        {
                            feeItemListFirst.Item.Qty = FS.FrameWork.Public.String.FormatNumber(sumCost / priceCollection[keyCombo], 2);
                        }
                        else
                        {
                            feeItemListFirst.Item.Qty = sumCost >= 0 ? 1 : -1;
                        }

                        if (sumNobackCost <= 0)
                        {
                            feeItemListFirst.NoBackQty = 0;
                            feeItemListFirst.Item.Price = sumCost;
                            feeItemListFirst.FT.TotCost = sumCost;
                        }
                        else
                        {
                            if (feeItemListFirst.UndrugComb.Qty != 0)
                            {
                                feeItemListFirst.NoBackQty = feeItemListFirst.UndrugComb.Qty;
                            }
                            else if (priceCollection.ContainsKey(keyCombo))
                            {
                                feeItemListFirst.NoBackQty = FS.FrameWork.Public.String.FormatNumber(sumNobackCost / priceCollection[keyCombo], 2);
                            }
                            else
                            {
                                feeItemListFirst.NoBackQty = sumNobackCost >= 0 ? 1 : -1;
                            }
                            feeItemListFirst.Item.Price = sumCost;
                            feeItemListFirst.FT.TotCost = sumCost;
                        }

                        if (returnApply!=null)
                        {
                            if (priceCollection.ContainsKey(keyCombo))
                            {
                                returnApply.Item.Qty = FS.FrameWork.Public.String.FormatNumber(sumReturnCost / priceCollection[keyCombo], 2);
                            }
                            else
                            {
                                returnApply.Item.Qty = sumReturnCost >= 0 ? 1 : -1;
                            }
                        }
                        string quitKey =this.GetPackageItemQuitKey(feeItemListFirst);
                        hsQuitFeeByPackage[quitKey] = alList;
                        hsQuitFeeByPackage["Quit" +quitKey] = alReturnApplyList;

                        if (this.AddFeeDetailToDataTable(feeItemListFirst) > 0)
                        {
                            this.SetFpFeeDetailFormat(feeItemListFirst, this.fpFeeDetail_Sheet1.RowCount - 1, returnApply);
                            this.fpFeeDetail_Sheet1.Rows[this.fpFeeDetail_Sheet1.RowCount - 1].Tag = feeItemListFirst.Clone();
                        }
                    }
                }
            }

            #endregion

            this.fpFeeDetail_Sheet1.SortRows(0, true, true);
        }

        private void fpFeeDetail_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right&&this.isQuitFee)
            {
                ContextMenuStrip contextMenu1 = new ContextMenuStrip();

                ToolStripMenuItem mnuPrint = new ToolStripMenuItem();
                mnuPrint.Click += new EventHandler(mnuPrintApply_Click);
                mnuPrint.Text = "�˷�����";
                contextMenu1.Items.Add(mnuPrint);

                ToolStripSeparator spearator = new ToolStripSeparator();
                contextMenu1.Items.Add(spearator);

                mnuPrint = new ToolStripMenuItem();
                mnuPrint.Click += new EventHandler(mnuPrintCancel_Click);
                mnuPrint.Text = "ȡ���˷�����";
                contextMenu1.Items.Add(mnuPrint);

                contextMenu1.Show(this.fpFeeDetail, new Point(e.X, e.Y));
            }
        }

        private void fpFeeDetail_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //��ѡ���˷�ʱ���Զ����ȫ��
            if (e.Column == this.dtFeeDetail.Columns["�˷�"].Ordinal)
            {
                if (this.dtFeeDetail.Columns.Contains("�˷�����"))
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpFeeDetail_Sheet1.Cells[e.Row, e.Column].Value))
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = this.fpFeeDetail_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                        string key = feeItem.UndrugComb.ID + "|" + feeItem.TransType.ToString() + "|" + feeItem.RecipeNO + feeItem.ExecOrder.ID;

                        if (hsQuitFeeByPackage.ContainsKey(key))//����Ǹ�����Ŀ���������޸�����
                        {
                            this.fpFeeDetail_Sheet1.Cells[e.Row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Locked = true;
                            this.fpFeeDetail_Sheet1.Cells[e.Row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Value = feeItem.NoBackQty;
                        }
                        else
                        {
                            this.fpFeeDetail_Sheet1.Cells[e.Row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Locked = false;
                            this.fpFeeDetail_Sheet1.Cells[e.Row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Value = feeItem.NoBackQty;
                        }
                    }
                    else
                    {
                        this.fpFeeDetail_Sheet1.Cells[e.Row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Locked = true;
                        this.fpFeeDetail_Sheet1.Cells[e.Row, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Value = 0.00F;
                    }
                }
            }
        }

        void mnuPrintApply_Click(object sender, EventArgs e)
        {
            ArrayList alFee = new ArrayList();

            for (int i = 0; i < this.fpFeeDetail.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpFeeDetail.ActiveSheet.Cells[i, this.dtFeeDetail.Columns["�˷�"].Ordinal].Text == "True" &&
                    FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail.ActiveSheet.Cells[i, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Text) > 0)
                {
                    //ȡ������Ϣ
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList item = this.fpFeeDetail_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    if (item != null && item.NoBackQty > 0)
                    {
                        string key = this.GetPackageItemQuitKey(item);
                        //˵���Ǹ�����Ŀ�����
                        if (hsQuitFeeByPackage.ContainsKey(key))
                        {
                            ArrayList al = hsQuitFeeByPackage[key] as ArrayList;
                           
                            decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail.ActiveSheet.Cells[i, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Value);
                            foreach(FeeItemList f in al)
                            {
                                if (f.NoBackQty > 0)
                                {
                                    f.Item.Qty = f.NoBackQty * qty / item.NoBackQty;
                                    f.NoBackQty = 0;
                                    f.FT.TotCost = f.Item.Price * f.Item.Qty / f.Item.PackQty;
                                    f.FT.OwnCost = f.FT.TotCost;
                                    f.IsNeedUpdateNoBackQty = true;
                                    alFee.Add(f);
                                }
                            }
                        }
                        else
                        {
                            item.Item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail.ActiveSheet.Cells[i, this.dtFeeDetail.Columns["�˷�����"].Ordinal].Value);
                            item.NoBackQty = item.NoBackQty - item.Item.Qty;
                            item.FT.TotCost = item.Item.Price * item.Item.Qty / item.Item.PackQty;
                            item.FT.OwnCost = item.FT.TotCost;
                            item.IsNeedUpdateNoBackQty = true;
                            alFee.Add(item);
                        }
                    }

                }
            }


            if (alFee.Count <= 0)
            {
                MessageBox.Show("û����Ҫ�˷��������Ŀ��");
                return;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.HISFC.BizLogic.Fee.ReturnApply returnMgr = new FS.HISFC.BizLogic.Fee.ReturnApply();
            returnMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            string applyBillCode = returnMgr.GetReturnApplyBillCode();
            if (applyBillCode == null || applyBillCode == string.Empty)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��ȡ���뵥�ų���" + returnMgr.Err);
                return;
            }
            DateTime nowTime = returnMgr.GetDateTimeFromSysDateTime();
            bool isApplyTip = false;

            string msgErr = string.Empty;
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string msg = string.Empty;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in alFee)
            {
                bool isApply = true;

                if (!this.isCanQuitOtherDeptFee && this.depts.ContainsKey(feeItemList.RecipeOper.Dept.ID) == false && this.depts.ContainsKey(feeItemList.ExecOper.Dept.ID)==false)
                {
                    MessageBox.Show(string.Format("���������������ҷ��á�{0}��,��֪ͨ��{1}�������˷�", this.GetItemName(feeItemList), CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID)));
                    return;
                }

                if (feeItemList.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug&& SOC.HISFC.BizProcess.Cache.Fee.GetItem(feeItemList.Item.ID).IsNeedConfirm && this.depts.ContainsKey(feeItemList.ExecOper.Dept.ID) == false)
                  {
                    MessageBox.Show(string.Format("�����������ն�ȷ�ϵķ��á�{0}��,��֪ͨ��{1}�������˷�", this.GetItemName(feeItemList), CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID)));
                    return;
                }

                if (this.isCanQuitOtherDeptFee && this.depts.ContainsKey(feeItemList.ExecOper.Dept.ID) == false && this.isCurrentDeptNeedAppy == false)//���������������ҵģ�����ִ�п��Ҳ����ڵ�ǰ�����ģ�����Ҫ�˷�����
                {
                    isApply = false;
                }
                else if (this.depts.ContainsKey(feeItemList.ExecOper.Dept.ID) && this.isCurrentDeptNeedAppy == false)//���ִ�п����ǵ�ǰ���������Ҳ���Ҫ����ģ�ֱ���˷�
                {
                    isApply = false;
                }
                else
                {
                    if (feeItemList.ConfirmedQty > 0)
                    {
                        string key =this.GetPackageItemKey(feeItemList);
                        if (dictionary.ContainsKey(key) == false)
                        {
                            msg += string.Format(Environment.NewLine + @"{1}��{0}  {2}{3}��", this.GetItemName(feeItemList), CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID), feeItemList.ConfirmedQty, feeItemList.Item.PriceUnit);
                            dictionary[key] = msg;
                        }
                    }

                    isApplyTip = true;
                }

                if (isApply == false && feeItemList.ConfirmedQty > 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(string.Format("��֪ͨ��{1}�������˷�ȷ�ϣ���{0}  {2}{3}��", string.IsNullOrEmpty(feeItemList.UndrugComb.ID) ? feeItemList.Item.Name : feeItemList.UndrugComb.Name, CommonController.Instance.GetDepartmentName(feeItemList.ExecOper.Dept.ID), feeItemList.ConfirmedQty, feeItemList.Item.PriceUnit));
                    return;
                }

                if (this.feeIntegrate.QuitFeeApply(this.currentPatient, feeItemList, isApply, applyBillCode, nowTime, ref msgErr) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(isApply ? "�˷�����ʧ�ܣ�" : "�˷�ʧ�ܣ�" + this.feeIntegrate.Err);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("�˷ѳɹ���" + (string.IsNullOrEmpty(msg) ? (isApplyTip?"�����˷����룬��֪ͨ�˷�":"") : @"��֪ͨ���¿��ҽ����˷�ȷ�ϣ�" + Environment.NewLine + Environment.NewLine + msg));
            
            //��Ҫ���½���״̬
            //���¼����˷�������Ϣ
            this.QueryPatientReturyApply(this.currentPatient);
            this.fpFeeDaySum_SelectionChanged(null, null);
        }

        void mnuPrintCancel_Click(object sender, EventArgs e)
        {
            ArrayList alFee = new ArrayList();

            for (int i = 0; i < this.fpFeeDetail.ActiveSheet.Rows.Count; i++)
            {
                if (this.fpFeeDetail.ActiveSheet.Cells[i, this.dtFeeDetail.Columns["ȡ������"].Ordinal].Text == "True" &&
                    FS.FrameWork.Function.NConvert.ToDecimal(this.fpFeeDetail.ActiveSheet.Cells[i, this.dtFeeDetail.Columns["����������"].Ordinal].Text) > 0)
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList item = this.fpFeeDetail.ActiveSheet.Rows[i].Tag as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
                    if (item.TransType == FS.HISFC.Models.Base.TransTypes.Positive)
                    {
                        string key = "Quit" + this.GetPackageItemQuitKey(item);
                        if (hsQuitFeeByPackage.ContainsKey(key))
                        {
                            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in (ArrayList)hsQuitFeeByPackage[key])
                            {
                                //ȡ������Ϣ
                                alFee.Add(dicReturnApply[feeItemList.RecipeNO + feeItemList.SequenceNO.ToString()]);
                            }
                        }
                        else
                        {
                            //ȡ������Ϣ
                            alFee.Add(dicReturnApply[item.RecipeNO + item.SequenceNO.ToString()]);
                        }
                    }
                }
            }


            if (alFee.Count <= 0)
            {
                MessageBox.Show("û����Ҫȡ���˷��������Ŀ��");
                return;
            }

            FS.HISFC.Models.Fee.Inpatient.FeeItemList tempFeeItem = null;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.phamarcyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.returnApplyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            DateTime nowTime = this.feeManager.GetDateTimeFromSysDateTime();

            foreach (Object quitItem in alFee)
            {
                if (quitItem is FS.HISFC.Models.Pharmacy.ApplyOut)     //��ҩ����
                {
                    #region ��ҩ����ȡ�� ��ʱ��û���˷�������Ϣ

                    FS.HISFC.Models.Pharmacy.ApplyOut tempApplyOut = quitItem as FS.HISFC.Models.Pharmacy.ApplyOut;
                    //���ݴ����š�������ˮ�Ż�ȡ����״̬
                    //tempFeeItem = this.inpatientManager.GetItemListByRecipeNO(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO, true);
                    tempFeeItem = this.feeManager.GetItemListByRecipeNO(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO, FS.HISFC.Models.Base.EnumItemType.Drug);
                    if (tempFeeItem == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���ݴ����š���������Ŀ��ˮ�Ż�ȡ������ϸ��Ϣʧ��") + this.feeManager.Err);
                        return;
                    }
                    //����ҩƷ��ϸ���еĿ�����������ֹ����
                    int parm = this.feeManager.UpdateNoBackQtyForDrug(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO, -tempApplyOut.Days * tempApplyOut.Operation.ApplyQty, tempFeeItem.BalanceState);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("����ҩƷ��������ʧ��" + this.feeManager.Err));
                        return;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                        return;
                    }
                    //������ҩ����
                    parm = this.phamarcyIntegrate.CancelApplyOut(tempApplyOut.RecipeNO, tempApplyOut.SequenceNO);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(this.phamarcyIntegrate.Err);
                        return;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�������ѱ�ȡ�����޷��ٴγ���");
                        return;
                    }

                    #endregion

                    #region ��ʱ������ϢtempFeeItem��ֵ

                    tempFeeItem.Item.Qty = tempApplyOut.Days * tempApplyOut.Operation.ApplyQty;
                    tempFeeItem.Item.Price = tempApplyOut.Item.PriceCollection.RetailPrice;

                    #endregion
                }

                if (quitItem is FS.HISFC.Models.Fee.ReturnApply)       //�˷�����
                {

                    FS.HISFC.Models.Fee.ReturnApply tempReturnApply = quitItem as FS.HISFC.Models.Fee.ReturnApply;
                    //ArrayList listReturnApply = new ArrayList();

                    //if (hsQuitFeeByPackage.ContainsKey( temp.ID + temp.UndrugComb.ID))
                    //{
                    //    //�������ݿ�
                    //    ArrayList altemp = this.returnApplyManager.QueryReturnApplys(this.currentPatient.ID, false, false);
                    //    foreach (FS.HISFC.Models.Fee.ReturnApply tempReturnApply in altemp)
                    //    {
                    //        if (tempReturnApply.ApplyBillNO == temp.ApplyBillNO && tempReturnApply.UndrugComb.ID == temp.UndrugComb.ID)
                    //        {
                    //            listReturnApply.Add(tempReturnApply);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    listReturnApply.Add(temp);
                    //}

                    //foreach (FS.HISFC.Models.Fee.ReturnApply tempReturnApply in listReturnApply)
                    {
                        #region ���ݴ����š�������ˮ�Ż�ȡ������Ϣ

                        if (tempReturnApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            //���ݴ����š�������ˮ�Ż�ȡ����״̬
                            tempFeeItem = this.feeManager.GetItemListByRecipeNO(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, FS.HISFC.Models.Base.EnumItemType.Drug);
                        }
                        else
                        {
                            tempFeeItem = this.feeManager.GetItemListByRecipeNO(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, FS.HISFC.Models.Base.EnumItemType.UnDrug);
                        }
                        if (tempFeeItem == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("���ݴ����š���������Ŀ��ˮ�Ż�ȡ������ϸ��Ϣʧ��") + this.feeManager.Err);
                            return;
                        }

                        #endregion

                        if (tempReturnApply.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)     //ҩƷ�˷�����
                        {
                            #region ҩƷ�˷���������

                            #region ������ϸ���еĿ�����������ֹ����

                            int parm = this.feeManager.UpdateNoBackQtyForDrug(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, -tempReturnApply.Item.Qty, tempFeeItem.BalanceState);
                            if (parm == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("����ҩƷ��������ʧ��" + this.feeManager.Err));
                                return;
                            }
                            else if (parm == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                                return;
                            }

                            #endregion

                            //�����˵��������ʱҩƷ�Ѿ����ڲ����˺����Ч���롣���������룬����ȡ���������������ɰ�ҩ����
                            if (tempFeeItem.NoBackQty != 0 || tempFeeItem.Item.Qty != tempFeeItem.NoBackQty + tempReturnApply.Item.Qty)
                            {
                                #region ������ȡ�����
                                //�������ϰ�ҩ����
                                int returnValue = this.phamarcyIntegrate.CancelApplyOut(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO);
                                if (returnValue == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(Language.Msg("���ϰ�ҩ�������!") + phamarcyIntegrate.Err);

                                    return;
                                }
                                if (returnValue == 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(Language.Msg("��Ŀ��") + tempFeeItem.Item.Name + Language.Msg("���Ѱ�ҩ�������¼�������"));

                                    return;
                                }

                                //ȡ�շѶ�Ӧ�İ�ҩ�����¼
                                FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.phamarcyIntegrate.GetApplyOut(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO);
                                if (applyOutTemp == null)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(Language.Msg("���������Ϣ����!") + this.phamarcyIntegrate.Err);
                                    return;
                                }
                                //��ʣ���������Ͱ�ҩ����
                                applyOutTemp.Operation.ApplyOper.OperTime = nowTime;
                                applyOutTemp.Operation.ApplyQty = tempFeeItem.NoBackQty + tempReturnApply.Item.Qty;//��������ʣ������
                                applyOutTemp.Operation.ApproveQty = tempFeeItem.NoBackQty + tempReturnApply.Item.Qty;//��������ʣ������
                                applyOutTemp.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//��Ч״̬                        
                                //��ʣ���������Ͱ�ҩ����  {C37BEC96-D671-46d1-BCDD-C634423755A4}
                                if (this.phamarcyIntegrate.ApplyOut(applyOutTemp) != 1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show(Language.Msg("���²��뷢ҩ�������!") + this.phamarcyIntegrate.Err);

                                    return;
                                }

                                #endregion
                            }
                            else
                            {
                                #region ȫ�����

                                //�ָ����������¼Ϊ��Ч   ����� 
                                parm = this.phamarcyIntegrate.UndoCancelApplyOut(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO);
                                if (parm == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�ָ�����������Ч�Է�������" + this.phamarcyIntegrate.Err);
                                    return;
                                }
                                else if (parm == 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("�������ѱ�ȡ�����޷���������");
                                    return;
                                }

                                #endregion
                            }

                            #endregion
                        }
                        else
                        {
                            #region ��ҩƷ�˷���������

                            //������ϸ���еĿ�����������ֹ����
                            int parm = this.feeManager.UpdateNoBackQtyForUndrug(tempReturnApply.RecipeNO, tempReturnApply.SequenceNO, -tempReturnApply.Item.Qty, tempFeeItem.BalanceState);
                            if (parm == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("����ҩƷ��������ʧ��" + this.feeManager.Err));
                                return;
                            }
                            else if (parm == 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                                return;
                            }

                            #endregion

                            #region ���������˷�����
                            //�������ʳ�����е���������
                            //parm = mateInteger.ApplyMaterialFeeBack(tempReturnApply.MateList, true);
                            //if (parm < 0)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    MessageBox.Show(Language.Msg("����������������ʧ��" + this.inpatientManager.Err));
                            //    return;
                            //}
                            //if (parm == 0)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    MessageBox.Show(Language.Msg("���ݷ����䶯!��ˢ�´���"));
                            //    return;
                            //}
                            //parm = returnApplyManager.UpdateReturnApplyState(tempReturnApply.ApplyMateList, CancelTypes.Reprint);
                            //if (parm < 0)
                            //{
                            //    FS.FrameWork.Management.PublicTrans.RollBack();
                            //    MessageBox.Show(Language.Msg("��������������Ϣʧ�ܣ�" + this.inpatientManager.Err));
                            //    return;
                            //}
                            #endregion
                        }

                        #region ��ʱ������ϢtempFeeItem��ֵ

                        tempFeeItem.Item.Qty = tempReturnApply.Item.Qty;
                        tempFeeItem.Item.Price = tempReturnApply.Item.Price;

                        #endregion

                        //�����˷�����
                        if (this.returnApplyManager.CancelReturnApply(tempReturnApply.ID, this.returnApplyManager.Operator.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("�����˷����뷢������") + this.returnApplyManager.Err);
                            return;
                        }
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("ȡ����������ɹ���");

            //��Ҫ���½���״̬
            //���¼����˷�������Ϣ
            this.QueryPatientReturyApply(this.currentPatient);
            this.fpFeeDaySum_SelectionChanged(null, null);
        }

        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //���߻�����Ϣ
            //if (this.neuTabControl1.SelectedTab == tpMainInfo)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpMainInfo_Sheet1, pathNameMainInfo);
                //Ԥ����
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpPrepay_Sheet1, pathNamePrepay);
                //������Ϣ
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpBalance_Sheet1, pathNameBalance);
            }
            //���õ������
            //else if (this.neuTabControl1.SelectedTab == tpDrugList)
            {

                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeItemSum_Sheet1, this.pathNameFeeItemSum);
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeDaySum_Sheet1, this.pathNameFeeDaySum);
            }
            //������ϸ
            //else if (this.neuTabControl1.SelectedTab == tpUnDrugList)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFeeDetail_Sheet1, this.pathNameFeeDetail);
            }
            //���û�����Ϣ
            //else if (this.neuTabControl1.SelectedTab == tpFeeInfo)
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpFee_Sheet1, pathNameFee);
            }
            //�����Ϣ

            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpShiftData_Sheet1, this.pathNameShiftData);

            MessageBox.Show("��ʽ����ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo == string.Empty)
            {
                MessageBox.Show(Language.Msg("�û��߲�����!����֤������"));

                return;
            }

            this.currentPatient = this.radtManager.QueryPatientInfoByInpatientNO(this.ucQueryInpatientNo1.InpatientNo);
            if (this.currentPatient == null || this.currentPatient.ID == null || this.currentPatient.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("��ѯ���߻�����Ϣ����!") + this.radtManager.Err);

                return;
            }
            this.QueryPatientByInpatientNO(currentPatient);

        }

        private void dtpItemDayFilter_ValueChanged(object sender, EventArgs e)
        {
            if (this.dvFeeDaySum != null)
            {
                if (this.dtpItemDayFilter.Checked)
                {

                    this.dvFeeDaySum.RowFilter = string.Format("�շ�ʱ�� ='{0}'", this.dtpItemDayFilter.Value.ToString("yyyy-MM-dd"));
                }
                else
                {
                    this.dvFeeDaySum.RowFilter = "1=1";
                }
                this.SelectFeeItemDaySum();
            }
        }

        private void txtItemFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.dvFeeItemSum != null)
            {
                string feeCode = "";
                if (this.fpFeeItemSum_Sheet1.RowCount > 0 && this.fpFeeItemSum_Sheet1.ActiveRowIndex >= 0)
                {
                    feeCode = this.fpFeeItemSum_Sheet1.Cells[this.fpFeeItemSum_Sheet1.ActiveRowIndex, this.dtFeeItemSum.Columns["��Ŀ����"].Ordinal].Text;
                }
                this.dvFeeItemSum.RowFilter = string.Format("��Ŀ���� like '%{0}%' or ƴ���� like '%{0}%' or ����� like '%{0}%' or �Զ����� like '%{0}%'", this.txtItemFilter.Text.Trim());
                this.SelectFeeItemSum(feeCode);
            }
        }

        private void cmbFeeTypeFilter_TextChanged(object sender, EventArgs e)
        {
            if (this.dvFee != null)
            {
                this.dvFee.RowFilter = string.Format("�������� like '%{0}%'", this.cmbFeeTypeFilter.Text.Trim());
                this.SelectFeeInfo();
            }
        }

        void ckUnCombo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentPatient != null)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ��Ϣ�����Ժ�...");
                Application.DoEvents();
                hsMinFee.Clear();
                hsDayFeeItemList.Clear();
                dicReturnApply.Clear();
                dtFee.Clear();
                dtFeeDaySum.Clear();
                dtFeeDetail.Clear();
                dtFeeItemSum.Clear();
                //���ò�ѯʱ��
                DateTime beginTime = this.currentPatient.PVisit.InTime;
                DateTime endTime = this.radtManager.GetDateTimeFromSysDateTime();

                if (this.neuTabControl1.TabPages.Contains(this.tpFeeInfo))
                {
                    this.QueryPatientReturyApply(this.currentPatient);
                    this.QueryPatientDrugList(beginTime, endTime);
                    this.QueryPatientUndrugList(beginTime, endTime);
                    this.QueryPatientFeeInfo(beginTime, endTime);
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

    }
}

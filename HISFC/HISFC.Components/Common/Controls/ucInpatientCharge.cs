using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FS.FrameWork.Management;
using FS.HISFC.Models.Fee.Inpatient;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Order;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucInpatientCharge : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInpatientCharge()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ŀ�б�
        /// </summary>
        private ucItemList ucItemList = null;

        
        /// <summary>
        /// ִ�п���ѡ���б�
        /// </summary>
        private FS.FrameWork.WinForms.Controls.PopUpListBox lbDept = new FS.FrameWork.WinForms.Controls.PopUpListBox();

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        private EnumShowItemType itemKind;

        /// <summary>
        /// �����ص�ҩƷ����// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private string noAddDrugQuality = "";

        /// <summary>
        /// �ؼ�����
        /// </summary>
        private FeeTypes feeType;

        /// <summary>
        /// ��ǰ��
        /// </summary>
        private int rowCount;
      
        /// <summary>
        /// ������ڽ���С�����㣬���д����¼�ѡ���Է���
        /// </summary>
        private bool isSubTotal;

        /// <summary>
        /// �ɹ���ʾ��Ϣ
        /// </summary>
        private string sucessMsg = string.Empty;

        /// <summary>
        /// �Ƿ������۽��Ϊ0����Ŀ
        /// </summary>
        private bool isChargeZero;

        /// <summary>
        /// Ĭ��ִ�п���
        /// </summary>
        private string defaultExeDept;

        /// <summary>
        /// ��ǰҩƷ������ң���֤���׺���Ŀѡ���б�һ�£�by cube
        /// </summary>
        private string curDrugApplyDept = "";

        /// <summary>
        /// ����ҽ��
        /// </summary>
        private string recipeDoctCode;

        /// <summary>
        /// �Ƿ���֤��ʱͣ�ñ��
        /// </summary>
        private bool isJudgeValid = false;

        /// <summary>
        /// ���߻�����Ϣʵ��
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// ҽ�Ʊ���,���ѽӿ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterface = null;

        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// סԺ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ��ҩƷҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Department departmentManager = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.ComGroupTail groupDetailManager = new FS.HISFC.BizLogic.Manager.ComGroupTail();

        /// <summary>
        /// ��Ա��Ϣҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        ///// <summary>
        ///// ��ҩƷ�����Ŀҵ���
        ///// </summary>
        private FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackageManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

        /// <summary>
        /// ��ҩƷ��Ŀҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// ҩƷ�ۺ�ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntergrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntergrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ҽ��ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
        /// <summary>
        /// ҽ���߼���
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        /// <summary>
        /// �Ƿ��ж�Ƿ�ѣ������ʾ
        /// </summary>
        private FS.HISFC.Models.Base.MessType messtype = FS.HISFC.Models.Base.MessType.Y;
        FS.HISFC.Models.Base.Employee operObj = null;
        /// <summary>
        /// ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject recipeDept = null;
        private bool isJudgeQty = true; //�Ƿ��ж�����
        private bool defaultExeDeptIsDeptIn = false;

        /// <summary>
        /// �����շ���Ŀ����
        /// </summary>
        private List<FeeItemList> feeItemCollection = new List<FeeItemList>();
        //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
        private HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient IAdptIllnessInPatient = null;

        /// <summary>
        /// ��ǰ���ʵĿۿ����
        /// </summary>
        private FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// �Ƿ��ָ�����Ŀ��������{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        private bool isSplitUndrugCombItem = false;
        //{F4912030-EF65-4099-880A-8A1792A3B449} ����

        /// <summary>
        /// �Ƿ���ʾ������{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        private bool isShowFeeRate = false;

      
        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        ArrayList unDrugDeptList = new ArrayList();
        Dictionary<string, FS.FrameWork.Models.NeuObject> dictDept = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// �Ƿ񵯳���Ʊ����
        /// </summary>
        private bool isPopSHowInvoice = false;

        /// <summary>
        /// ��С������ʾ���
        /// </summary>
        ArrayList allFeecodeStat = new ArrayList();
        Dictionary<string, FS.HISFC.Models.Fee.FeeCodeStat> allFeecodeStatHelper = new Dictionary<string, FS.HISFC.Models.Fee.FeeCodeStat>();
        private FS.HISFC.BizLogic.Fee.FeeCodeStat feeCodeStatMgr = new FS.HISFC.BizLogic.Fee.FeeCodeStat();


        #endregion

        #region ����
        /// <summary>
        /// �Ƿ��ָ�����Ŀ��������{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        [Category("�ؼ�����"), Description("���øÿؼ��Ƿ��ڽ����Ϸֽ⸴����Ŀ true�ֽ� false���ֽ�")]
        public bool IsSplitUndrugCombItem 
        {
            get 
            {
                return this.isSplitUndrugCombItem;
            }
            set 
            {
                this.isSplitUndrugCombItem = value;
            }
        }
        //{F4912030-EF65-4099-880A-8A1792A3B449} ����

        /// <summary>
        /// ���߻�����Ϣʵ��
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set 
            {
                this.patientInfo = value;
            }
        }

        /// <summary>
        /// ��ǰ��
        /// </summary>
        public int RowCount 
        {
            get 
            {
                return this.rowCount;
            }
            set 
            {
                this.rowCount = value;
            }
        }

        /// <summary>
        /// �ɹ���ʾ��Ϣ
        /// </summary>
        public string SucessMsg 
        {
            get 
            {
                return this.sucessMsg;
            }
        }
        
        /// <summary>
        /// ���ص���Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("���øÿؼ����ص���Ŀ��� ҩƷ:drug ��ҩƷ undrug ����: all")]
        public EnumShowItemType ������Ŀ��� 
        {
            get 
            {
                return this.itemKind;
            }
            set 
            {
                this.itemKind = value;
            }
        }

        /// <summary>
        /// �����ص�ҩƷ����// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        [Category("�ؼ�����"), Description("�����ص�ҩƷ���ʣ��á�,�����ֿ�")]
        public string NoAddDrugQuality
        {
            get
            {
                return this.noAddDrugQuality;
            }
            set
            {
                this.noAddDrugQuality = value;
            }
        }
        /// <summary>
        /// �ؼ�����
        /// </summary>
        [Category("�ؼ�����"), Description("��û������øÿؼ�����Ҫ����"), DefaultValue(1)]
        public FeeTypes �ؼ�����
        {
            get 
            {
                return this.feeType;
            }
            set 
            {
                this.feeType = value;
            }
        }

        /// <summary>
        /// �Ƿ�����շѻ��߻���0���۵���Ŀ
        /// </summary>
        [Category("�ؼ�����"), Description("��û��������Ƿ�����շѻ��߻���"), DefaultValue(false)]
        public bool IsChargeZero 
        {
            get 
            {
                return this.isChargeZero;
            }
            set 
            {
                this.isChargeZero = value;
            }
        }
        [Category("�ؼ�����"),Description("�Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.messtype;
            }
            set
            {
                this.messtype = value;
            }
        }
        [Category("�ؼ�����"), Description("����Ϊ���Ƿ���ʾ")]
        public bool IsJudgeQty
        {
            get
            {
                return isJudgeQty;
            }
            set
            {
                isJudgeQty = value;
            }
        }

        [Category("�ؼ�����"), Description("ִ�п����Ƿ�Ĭ��Ϊ��½����")]
        public bool DefaultExeDeptIsDeptIn
        {
            get
            {
                return defaultExeDeptIsDeptIn;
            }
            set
            {
                defaultExeDeptIsDeptIn = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ������")]
        public bool IsShowFeeRate
        {
            get { return isShowFeeRate; }
            set { isShowFeeRate = value; }
        }

        /// <summary>
        /// �Ƿ񵯳���Ʊ
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ���ʾ������")]
        public bool IsPopSHowInvoice
        {
            get { return isPopSHowInvoice; }
            set { isPopSHowInvoice = value; }
        }

        /// <summary>
        /// ҽ�Ʊ���,���ѽӿ���
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy MedcareInterface 
        {
            set 
            {
                this.medcareInterface = value;
            }
        }

        /// <summary>
        /// Ĭ��ִ�п���
        /// </summary>
        public string DefaultExeDept
        {
            get
            {
                return this.defaultExeDept;
            }
            set
            {
                this.defaultExeDept = value;
            }
        }

        /// <summary>
        /// ����ҽ��
        /// </summary>
        public string RecipeDoctCode
        {
            get
            {
                return this.recipeDoctCode;
            }
            set
            {
                this.recipeDoctCode = value;
            }
        }

        /// <summary>
        /// �Ƿ���֤��ʱͣ�ñ��
        /// </summary>
        public bool IsJudgeValid 
        {
            get 
            {
                return this.isJudgeValid;
            }
            set
            {
                this.isJudgeValid = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public FS.FrameWork.Models.NeuObject RecipeDept
        {
            set
            {
                this.recipeDept = value;
            }
        }

        /// <summary>
        /// �����շ���Ŀ����
        /// </summary>
        public List<FeeItemList> FeeItemCollection
        {
            get
            {
                return this.feeItemCollection;
            }
        }
        //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
        /// <summary>
        /// Ĭ��ȡҩ����
        /// </summary>
        private FS.FrameWork.Models.NeuObject defautStockDept= null;

        public FS.FrameWork.Models.NeuObject DefautStockDept
        {
            get { return defautStockDept; }
            set { defautStockDept = value; }
        }
        //{0604764A-3F55-428f-9064-FB4C53FD8136}
        private string operationNO = string.Empty;

        public string OperationNO
        {
            get { return operationNO; }
            set { operationNO = value; }
        }

        //{FD08EA4B-B5F3-4514-AEEA-44222F5900FB} �����շѱ��б�������ҽ���� 
        private string orderID = string.Empty;

        public string OrderID
        {
            get
            {
                return orderID;
            }
            set
            {
                orderID = value;
            }
        }

        private FS.HISFC.Models.Fee.Inpatient.FTSource ftSource = new FTSource();
        public FS.HISFC.Models.Fee.Inpatient.FTSource FTSource
        {
            set
            {
                ftSource = value;
            }
        }

        #endregion

        #region ˽�з���

        /// <summary>
        /// ����Ŀ�б�����Ŀ��ӵ������б�
        /// </summary>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <param name="execDeptCode"></param>
        /// <returns></returns>
        protected virtual int AddChargeDetail(FS.HISFC.Models.Base.Item item, int row, string execDeptCode)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("����ѡ����,Ȼ���շ�!"));

                return -1;
            }
            decimal price = 0;
            decimal orgPrice = 0;
            //if (this.pactUnitManager.GetPrice(this.patientInfo, item.IsPharmacy, item.ID, ref price) == -1)
            if (item.ItemType != EnumItemType.MatItem)
            {
                if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, item, ref price, ref orgPrice) == -1)
                {
                    MessageBox.Show(Language.Msg("ȡ��Ŀ:") + item.Name + Language.Msg("�ļ۸����!") + this.pactUnitManager.Err);

                    return -1;
                }
                item.Price = price;
                item.DefPrice = orgPrice;
            }
            //ҩƷĬ�ϰ���С��λ�շ�,��ʾ�۸�ҲΪ��С��λ�۸�,�������ݿ��Ϊ��װ��λ�۸�
            //if (item.IsPharmacy)//ҩƷ
            if (item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
            }
            else//��ҩƷ
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, item.Price, false);

                price = item.Price;
            }

            //�洢��Ŀʵ����ڼ۸�ȼ���{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
            this.fpDetail_Sheet.Rows[row].Tag = item;

            //{30B79077-CDC0-4de8-822A-8B04ABB2925C}
            this.fpDetail_Sheet.Cells[row, (int)Columns.feeRate].Tag = item.Clone();

            //������
            this.fpDetail_Sheet.SetValue(row, (int)Columns.GbCode, item.GBCode, false);

            //����
            this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, item.Qty, false);

            //�жϽ�����ת
            if (item.Price != 0 || this.IsChargeZero)
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = true;
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Qty);
            }
            else
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = false;
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Price);
            }

            //��ҩ����
            this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, NConvert.ToInt32(item.User03), false);

            //if (item.IsPharmacy && item.SysClass.ID.ToString() == "PCC")
            if (item.ItemType == EnumItemType.Drug && item.SysClass.ID.ToString() == "PCC")
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = false;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Black;
            }
            else
            {
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Transparent;
            }
            //ҩƷ��ѡ��ҩƷ�շѵ�λ,Ĭ��Ϊ��С��λ
            //if (item.IsPharmacy)
            if(item.ItemType == EnumItemType.Drug)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboType.Editable = true;
                comboType.Items = (new string[]{(item as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (item as FS.HISFC.Models.Pharmacy.Item).PackUnit});
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = comboType;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = false;
                if (item.MinFee.User03 == "2")
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit;
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price,4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                }
                else
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).MinUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).MinUnit;
                }
                
            }
            else//��ҩƷ
            {
                FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = textType;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = true;
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, item.PriceUnit, false);
            }

           //�ܶ�
           this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, price * item.Qty, false);
           
            //��Ŀ����,�͹����ʾ��һ��
            if (item.Specs != null && item.Specs != string.Empty)
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Name + "{" + item.Specs + "}", false);
            }
            else
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Name, false);
            }

            //��¼�����Ŀ			
            this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "1", false);
            //��ʶҩƷ����ҩƷ
            //if (item.IsPharmacy)
            if(item.ItemType == EnumItemType.Drug)
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
            }
            else
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
            }
            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            this.fpDetail_Sheet.SetValue(row, (int)Columns.feeRate, 1);
            string deptCode = string.Empty, deptName = string.Empty;
            if (!defaultExeDeptIsDeptIn && item.ItemType != EnumItemType.Drug)
            {
                #region ��ȡĬ��ִ�п���
                //��ȡ��ĿĬ��ִ�п���
                //if (execDeptCode == null || execDeptCode == string.Empty)
                //{
                //    this.GetItemDept(item, ref deptCode, ref deptName);
                //}
                //else
                //{
                //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                //    deptCode = execDeptCode;
                //    if (dept == null)
                //    {
                //        deptName = "(��)";
                //    }
                //    else
                //    {
                //        deptName = dept.Name;
                //    }
                //}

                ////{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                //execDeptCode = this.SetExecDept(item.ID);
                //if (execDeptCode != "")
                //{
                //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                //    deptCode = dept.ID;
                //    deptName = dept.Name;
                //}

                string ExecdeptCode = string.Empty;
                if ("N,I".Contains(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID).DeptType.ID.ToString()))
                {
                    if (this.patientInfo != null)
                    {
                        ExecdeptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    else
                    {
                        ExecdeptCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.recipeDoctCode).Dept.ID;
                    }
                }
                else
                {
                    ExecdeptCode = this.operObj.Dept.ID;
                }
                
                if (item.ItemType == EnumItemType.Drug)
                {
                    ArrayList alExecDept = null;
                    FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(ExecdeptCode);
                    deptCode = dept.ID;
                    deptName = dept.Name;
                    lbDept.Items.Clear();
                    SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                    alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                    lbDept.AddItems(alExecDept);
                }
                else
                {
                    ArrayList alExecDept = null;
                    string defaultExecDept = string.Empty;
                    SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, ExecdeptCode, item.ID, ref defaultExecDept, ref alExecDept);
                    if (alExecDept == null || alExecDept.Count == 0)
                    {
                        SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                        alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                    }
                    FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(defaultExecDept);
                    deptCode = dept.ID;
                    deptName = dept.Name;
                    lbDept.Items.Clear();
                    lbDept.AddItems(alExecDept);
                }
                #endregion
            }
            else
            {
                deptCode = this.operObj.Dept.ID;
                deptName = this.operObj.Dept.Name;
            }

            this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, deptName, false);
            //��ʾ����δ�޸�
            this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

            //�Ʒ�����
            this.fpDetail_Sheet.SetValue(row, (int)Columns.feeDate, DateTime.Now, false);

            //��ֵ���շ�ʵ��
            FeeItemList feeitemlist = new FeeItemList();
            feeitemlist.Item = item;
            feeitemlist.ExecOper.Dept.ID = deptCode;
            feeitemlist.ExecOper.Dept.Name = deptName;
            feeitemlist.Days = NConvert.ToInt32(item.User03);//��ҩ����
            //ָ��ҩƷ�İ�ҩҩ��
            if (item is FS.HISFC.Models.Pharmacy.Item)
            {
                feeitemlist.StockOper.Dept.ID = item.User02;
            }

            //���渴����Ŀ
            feeitemlist.UndrugComb.ID = item.MinFee.User01;
            feeitemlist.UndrugComb.Name = item.MinFee.User02;

            feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(price * item.Qty, 2);
           

            this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, feeitemlist, false);
            //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
            if (IAdptIllnessInPatient != null)
            {
                int resultValue = IAdptIllnessInPatient.ProcessInpatientFeeDetail(this.patientInfo, ref feeitemlist);
                if (resultValue < 0) return -1;
            }
            
            return 0;
        }

        /// <summary>
        /// ��ӻ��߻�����ϸ
        /// </summary>
        /// <param name="feeItemList">������Ϣʵ��</param>
        /// <param name="row">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int AddChargeDetail(FeeItemList feeItemList, int row)
        {
            if (feeItemList != null)
            {
                FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                txtType.ReadOnly = true;

                this.fpDetail_Sheet.Rows[row].BackColor = Color.Blue;

                //��ʾ����
                if (feeItemList.Item.Specs != null && feeItemList.Item.Specs != string.Empty)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, feeItemList.Item.Name + "{" + feeItemList.Item.Specs + "}", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, feeItemList.Item.Name, false);
                }

                this.fpDetail_Sheet.Cells[row, (int)Columns.ItemName].CellType = txtType;

                //��ʾ�۸�
                decimal price = 0;
                //if (feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    price = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price / feeItemList.Item.PackQty, 4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, feeItemList.Item.Price, false);
                }

                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = true;

                //��ʾ����
                if (feeItemList.Days == 0)
                {
                    feeItemList.Days = 1;
                }
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, feeItemList.Days, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;

                //��ҩ
                //if (feeItemList.Item.IsPharmacy && feeItemList.Item.MinFee.ID == "003")
                if (feeItemList.Item.ItemType == EnumItemType.Drug && feeItemList.Item.MinFee.ID == "003")
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Black;
                }
                else
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = this.fpDetail_Sheet.Rows[row].BackColor;
                }

                //����
                feeItemList.Item.Qty = feeItemList.Item.Qty / feeItemList.Days;

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, feeItemList.Item.Qty, false);

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, feeItemList.Item.PriceUnit, false);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, feeItemList.FT.TotCost, false);

                Department dept = this.departmentManager.GetDeptmentById(feeItemList.ExecOper.Dept.ID);

                if (dept == null)
                {
                    dept = new Department();
                    dept.Name = "(��)";
                }

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, dept.Name, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Dept].CellType = txtType;

                feeItemList.ExecOper.Dept.ID = dept.ID;
                feeItemList.ExecOper.Dept.Name = dept.Name;
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, feeItemList, false);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "0", false);
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

                this.fpDetail_Sheet.SetValue(row, (int)Columns.feeDate, DateTime.Now, false);

                //if (feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
                }
            }

            return 1;
        }

        /// <summary>
        /// �����ȷ�Ϸ�ҩƷҽ����ϸ
        /// </summary>
        /// <param name="execOrder">ҽ��ִ�е���Ϣ</param>
        /// <param name="row">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        protected int AddOrderDetail(ExecOrder execOrder, int row)
        {
            if (execOrder != null)
            {
                //δ��������Ŀ
                FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                txtType.ReadOnly = true;
                this.fpDetail_Sheet.Rows[row].BackColor = Color.LightSkyBlue;

                //��Ŀ����
                if (execOrder.Order.Item.Specs != null && execOrder.Order.Item.Specs != string.Empty)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, execOrder.Order.Item.Name + "{" + execOrder.Order.Item.Specs + "}", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, execOrder.Order.Item.Name, false);
                }
                this.fpDetail_Sheet.Cells[row, (int)Columns.ItemName].CellType = txtType;

                //�۸�
                decimal price = 0;
                decimal orgPrice = 0;
                if (execOrder.Order.Unit != "[������]")
                {

                    //if (this.pactUnitManager.GetPrice(this.patientInfo, false, execOrder.Order.Item.ID, ref price) == -1)
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, execOrder.Order.Item, ref price, ref orgPrice) == -1)
                    {
                        MessageBox.Show(Language.Msg("��ȡ��Ŀ�۸����!"));

                        return -1;
                    }
                    if (price != 0)
                    {
                        execOrder.Order.Item.Price = price;
                        execOrder.Order.Item.DefPrice = orgPrice;
                    }
                }
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, execOrder.Order.Item.Price, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Locked = true;

                //����
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, execOrder.Order.Qty, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Qty].Locked = true;

                //����
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, "1", false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = this.fpDetail_Sheet.Rows[row].BackColor;
                execOrder.Order.HerbalQty = 1;

                //��λ
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, execOrder.Order.Unit, false);
                //���
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, execOrder.Order.Qty * execOrder.Order.Item.Price, false);

                //ִ�п���
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, execOrder.ExecOper.Dept.Name, false);
                this.fpDetail_Sheet.Cells[row, (int)Columns.Dept].CellType = txtType;

                //��Ŀ����
                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, execOrder, false);

                //�Ƿ�������Ŀ,0ԭ��(���ݿ���),1����,2�޸�
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "0", false);

                //ִ�п����Ƿ��޸�0,�� 1��
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

                //�շ�ҩƷ��1��0��
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
            }

            return 1;
        }

        /// <summary>
        /// ���������ϸ�������б�
        /// </summary>
        /// <param name="groupID">������ĿID</param>
        /// <param name="row">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int AddGroupDetail(string groupID, int row)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("����ѡ����,Ȼ���շ�!"));

                return -1;
            }

            ArrayList groupDetails = new ArrayList();
            //��������id��ȡ������ϸ
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(groupID);
            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            }
            int count = 0;
            for (int i = 0; i < groupDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                if (groupDetail.drugFlag == "1")//ҩƷ
                {
                    //����ҩƷid��ȡҩƷʵ��
                    FS.HISFC.Models.Pharmacy.Storage drugStorate = null;

                    drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    count++;
                    //��ӵ������б�
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.MinFee.User03 = groupDetail.unitFlag;//ʹ�������ʱ�洢���������ڵĵ�λ1����С��λ2����װ��λ add by sunm

                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    if (count > 1)
                    {
                        this.AddRow(row + count - 1);
                    }

                    this.AddChargeDetail(drugBase, row + count - 1, groupDetail.deptCode);    
                    #endregion 
                }
                else//��ҩƷ
                {
                    //���ݷ�ҩƷid��ȡ��ҩƷʵ��
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = this.undrugManager.GetValidItemByUndrugCode(groupDetail.itemCode);
                    if (undrug == null) continue;
                    count++;
                    //��ӻ�����Ŀ
                    FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //undrugBase.IsPharmacy = false;
                    undrugBase.ItemType = EnumItemType.UnDrug;
                    undrugBase.Qty = groupDetail.qty;//����
                    undrugBase.User03 = "1";//����
                    undrugBase.User03 = "1";//����
                    //{01797533-5D92-4958-A52B-61540022F202}
                    if (undrug.UnitFlag == "1")
                    {
                        undrugBase.User01 = "[������]";
                    }
                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    if (count > 1)
                    {
                        AddRow(row + count - 1);
                    }
                    this.AddChargeDetail(undrugBase, row + count - 1, groupDetail.deptCode); 
                    #endregion 
                }
            }
            return 0;
        }
        /// <summary>
        /// ���������ϸ�������б�
        /// </summary>
        /// <param name="groupID">������ĿID</param> 
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int AddGroupDetail(string groupID)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("����ѡ����,Ȼ���շ�!"));

                return -1;
            }

            ArrayList groupDetails = new ArrayList();
            //��������id��ȡ������ϸ
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(groupID);
            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            } 
            for (int i = 0; i < groupDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                if (groupDetail.drugFlag == "1")//ҩƷ
                {
                    //����ҩƷid��ȡҩƷʵ��
                    FS.HISFC.Models.Pharmacy.Storage drugStorate = null;

                    drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    //��ӵ������б�
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.MinFee.User03 = groupDetail.unitFlag;

                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵� 
                    this.AddRow(0);
                    this.AddChargeDetail(drugBase,0,groupDetail.deptCode);
                    #endregion
                }
                else//��ҩƷ
                {
                    //���ݷ�ҩƷid��ȡ��ҩƷʵ��
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = this.undrugManager.GetValidItemByUndrugCode(groupDetail.itemCode);
                    if (undrug == null) continue;
                    //��ӻ�����Ŀ
                    FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //undrugBase.IsPharmacy = false;
                    undrugBase.ItemType = EnumItemType.UnDrug;
                    undrugBase.Qty = groupDetail.qty;//����
                    undrugBase.User03 = "1";//����
                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    this.AddRow(0);
                    this.AddChargeDetail(undrugBase, 0, groupDetail.deptCode);
                    #endregion
                }
            }
            return 0;
        }

        /// <summary>
        /// ���������ϸ�������б�
        /// </summary>
        /// <param name="groupID">������ĿID</param> 
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int AddGroupDetail(string groupID,ArrayList deleteGroupsList)
        {
            if (this.patientInfo == null || this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
            {
                MessageBox.Show(Language.Msg("����ѡ����,Ȼ���շ�!"));

                return -1;
            }

          
            ArrayList groupDetails = new ArrayList();
            //��������id��ȡ������ϸ
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(groupID);
           

            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            }
            //{35D18A38-FF4D-47d0-B81F-7EFA0D9DF3F9}
            for (int i = 0; i < deleteGroupsList.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail deleteGroupDetail = deleteGroupsList[i] as FS.HISFC.Models.Fee.ComGroupTail;
                for (int j = groupDetails.Count - 1; j >= 0; j--)
                {
                    FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[j] as FS.HISFC.Models.Fee.ComGroupTail;
                    if (deleteGroupDetail.sequenceNo == groupDetail.sequenceNo)
                    {
                        groupDetails.Remove(groupDetail);
                    }

                }
            }

            if (groupDetails == null || groupDetails.Count == 0)
            {
                return -1;
            }

            for (int i = 0; i < groupDetails.Count; i++)
            {
                FS.HISFC.Models.Fee.ComGroupTail groupDetail = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                if (groupDetail.drugFlag == "1")//ҩƷ
                {
                    //����ҩƷid��ȡҩƷʵ��
                    FS.HISFC.Models.Pharmacy.Storage drugStorate = null;
                    if (string.IsNullOrEmpty(curDrugApplyDept))
                    {
                        drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    }
                    else
                    {
                        drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.curDrugApplyDept, groupDetail.itemCode);
                    }
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    //��ӵ������б�
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.MinFee.User03 = groupDetail.unitFlag;

                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    this.AddRow(0);
                    this.AddChargeDetail(drugBase, 0, groupDetail.deptCode);
                    #endregion
                }
                else//��ҩƷ
                {
                    //���ݷ�ҩƷid��ȡ��ҩƷʵ��
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;

                    undrug = this.undrugManager.GetValidItemByUndrugCode(groupDetail.itemCode);

                    

                    if (undrug == null) continue;

                    //���ָ�����Ŀ�շѣ�ҽ��Ҫ��ϸ����Խ��в��{06EF3804-FA10-4ac1-8D16-D344B46DF3C6}
                    if (undrug.UnitFlag == "1")
                    {
                        undrug.User01 = "[������]";
                    }
                    //��ӻ�����Ŀ
                    FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //undrugBase.IsPharmacy = false;
                    undrugBase.ItemType = EnumItemType.UnDrug;
                    undrugBase.Qty = groupDetail.qty;//����
                    undrugBase.User03 = "1";//����
                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    this.AddRow(0);
                    this.AddChargeDetail(undrugBase, 0, groupDetail.deptCode);
                    #endregion
                }
            }
            this.Sum();
            return 0;
        }
        /// <summary>
        /// �жϼ۸�������ִ�п����Ƿ�Ϸ�
        /// </summary>
        /// <returns>-1���Ϸ�,0�Ϸ�</returns>
        public virtual bool IsValid()
        {
            int count = 0;

            if (this.recipeDoctCode == null || this.recipeDoctCode == string.Empty) 
            {
                MessageBox.Show(Language.Msg("�����뿪��ҽ��"));

                return false;
            }

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                object obj = this.fpDetail_Sheet.GetValue(i, (int)Columns.ItemObject);
                //�����ǰ�в�����Ŀ,��ô������һ���ж�
                if (obj == null)
                {
                    continue;
                }

                count++;

                string itemName = this.fpDetail_Sheet.GetText(i, (int)Columns.ItemName);//��Ŀ����
                //�ж�����
                if (isJudgeQty)
                {
                    if (!this.IsInputValid(itemName, i, Columns.Qty, true, "��ִ����������С�ڵ�����!"))
                    {
                        return false;
                    }
                }
                decimal feeRate = FS.FrameWork.Function.NConvert.ToDecimal(this.fpDetail_Sheet.GetText(i, (int)Columns.feeRate));
                if (feeRate <= 0)
                {
                    MessageBox.Show(Language.Msg("���ñ�������С��0�����0"));
                    this.fpDetail.Focus();
                    this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.feeRate);


                    return false;
                }


                //��ȡ��ҽ����Ŀ���ж�ִ�п��Һͼ۸�
                if (obj is FeeItemList)
                {
                    //�жϼ۸�
                    if (!this.IsChargeZero)
                    {
                        if (!this.IsInputValid(itemName, i, Columns.Price, true, "����Ŀ�۸���С�ڵ�����!"))
                        {
                            return false;
                        }
                    }

                    //�жϸ���
                    if (!this.IsInputValid(itemName, i, Columns.Day, true, "�ĸ�������С�ڵ�����!"))
                    {
                        return false;
                    }

                    //ҩƷ�жϵ�λ
                    //if (((FeeItemList)obj).Item.IsPharmacy && this.fpDetail_Sheet.GetText(i, (int)Columns.IsNew) == "1")
                    if (((FeeItemList)obj).Item.ItemType == EnumItemType.Drug && this.fpDetail_Sheet.GetText(i, (int)Columns.IsNew) == "1")
                    {
                        string tempValue = this.fpDetail_Sheet.GetText(i, (int)Columns.Unit);
                        FarPoint.Win.Spread.CellType.ComboBoxCellType comboType =
                            (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].CellType;
                        if (tempValue != comboType.Items[0] && tempValue != comboType.Items[1])
                        {
                            MessageBox.Show(itemName + Language.Msg("�ķ�ҩ��λ¼�����,������¼��!"));
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.Unit);

                            return false;
                        }
                    }

                    //�жϿ���
                    if (((FeeItemList)obj).ExecOper.Dept.ID == string.Empty)
                    {
                        MessageBox.Show(itemName + Language.Msg("��ִ�п��Ҳ���Ϊ��!"));
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.Dept);

                        return false;
                    }
                }
            }
            //����ϸ������
            if (count == 0)
            {
                MessageBox.Show(Language.Msg("��¼����Ŀ��ϸ!"));
                this.fpDetail.Focus();

                return false;
            }

            return true;
        }

        /// <summary>
        /// ��Ӹ�����Ŀ��ϸ�������б�
        /// </summary>
        /// <param name="undrugCombCode">�����Ŀ����</param>
        /// <param name="undrugCombName">�����Ŀ����</param>
        /// <param name="row">��ǰ��</param>
        /// <param name="execDeptCode">ִ�п��Ҵ���</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        protected virtual int AddCompoundDetail(string undrugCombCode, string undrugCombName, int row, string execDeptCode)
        {
            ArrayList details = this.undrugPackageManager.QueryUndrugPackagesBypackageCode(undrugCombCode);

            if (details == null)
            {
                MessageBox.Show(Language.Msg("���������Ϣ����!") + this.undrugPackageManager.Err);

                return -1;
            }

            int count = 0;

            for (int i = 0; i < details.Count; i++)
            {
                FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = details[i] as FS.HISFC.Models.Fee.Item.UndrugComb;

                //���������Ŀ������
                //·־�� �Ƿ����1��true���ã� 0��false������ luzhp@FS.com
                if (undrugComb.User01 == "0")
                {
                    continue;
                }

                FS.HISFC.Models.Fee.Item.Undrug undrug = this.itemManager.GetValidItemByUndrugCode(undrugComb.ID);

                if (undrug == null)
                {
                    continue;
                }

                count++;

                //undrug.IsPharmacy = false;
                undrug.ItemType = EnumItemType.UnDrug;

                if (undrugComb.Qty == 0)
                {
                    undrug.Qty = 1;
                }
                else
                {
                    undrug.Qty = undrugComb.Qty;
                }

                undrug.User03 = "1";
                undrug.MinFee.User01 = undrugCombCode;
                undrug.MinFee.User02 = undrugCombName;

                this.AddChargeDetail(undrug, row + count - 1, execDeptCode);
            }

            return 1;
        }

        /// <summary>
        /// ��Ŀ����
        /// </summary>
        /// <returns></returns>
        protected virtual int Charge()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ////���������
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
            //t.BeginTransaction();
            //this.inpatientManager.SetTrans(t.Trans);
            //this.personManager.SetTrans(t.Trans);

            //����ʱ��
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //��������
            FS.HISFC.Models.Base.Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);
            if (employee == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("��Ա��Ϣ�����޴���Ϊ:") + this.recipeDoctCode + Language.Msg("����Ա!"));

                return -1;
            }
            string recipeDept = employee.Dept.ID;
            ArrayList feeList = new ArrayList();
            //ѭ������
            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                FeeItemList feeItemList = new FeeItemList();
                bool isNew = false;
                int returnValue = 0;

                //��Ŀ��Ϣ��ֵ
                returnValue = this.SetItem(i, PayTypes.Charged, recipeDept, operTime, ref isNew, ref feeItemList);

                //�����õ���Ŀ��ϢΪ��,������
                if (returnValue == 0)
                {
                    continue;
                }

                //{0604764A-3F55-428f-9064-FB4C53FD8136}
                //������������
                if (this.OperationNO != string.Empty)
                {
                    feeItemList.OperationNO = this.OperationNO;
                }
                //�������¼����Ŀ:
                if (isNew)
                {
                    feeList.Add(feeItemList);
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.inpatientManager.InsertMedItemList(this.patientInfo, feeItemList) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("����ҩƷ������Ϣ����!") + this.inpatientManager.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                    else
                    {
                        if (this.inpatientManager.InsertFeeItemList(this.patientInfo, feeItemList) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(Language.Msg("�����ҩƷ������Ϣ����!") + this.inpatientManager.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                }
                //�޸ĵĻ�����Ŀ��ֻ���޸�����
                else
                {
                    feeItemList.ChargeOper.OperTime = operTime;
                    feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
                    //����ԭ�еķ�����ϸ��¼�Ľ�������
                    if (this.inpatientManager.UpdateChargeInfo(feeItemList) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("����ԭ�л�����Ϣ��¼����!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }
                }
            }
            //{4FF03BBF-763D-4063-A792-A2264999E79A}
            if (IAdptIllnessInPatient != null)
            {
                int resultValue = IAdptIllnessInPatient.SaveInpatientFeeDetail(this.patientInfo, ref feeList);

                if (resultValue < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.sucessMsg = "���۳ɹ�!";

            return 1;
        }

        /// <summary>
        /// ��ҩƷ�շ�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Fee()
        {
            Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);

            if (employee == null)
            {
                MessageBox.Show(Language.Msg("�����Ա������Ϣ����!"));

                return -1;
            }

            if (this.recipeDept != null && this.recipeDept.ID != "")
            {
                employee.Dept = this.recipeDept;
            }
            else
            {
                this.recipeDept = employee.Dept;
            }

            this.fpDetail.Change -= new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);

            if (!this.IsValid()) 
            {
                this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);
                return -1;
            }

            this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);

            if (inpatientManager.GetStopAccount(this.patientInfo.ID) == "1")
            {
                MessageBox.Show(Language.Msg("�û��ߴ��ڷ���״̬�����ܽ����շѣ�"));

                return -1;
            }

            if (feeIntergrate.IsPatientLackFee(this.patientInfo))
            {
                //Ƿ���ж�
                if (this.messtype == MessType.M)
                {
                    if (MessageBox.Show(this, "�˻�����Ƿ�ѣ��Ƿ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else if (this.messtype == MessType.Y)
                {
                    MessageBox.Show(this, "�˻�����Ƿ�ѣ�����������շѣ�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    return -1;
                }
            }
            FS.HISFC.BizLogic.Fee.TemporaryFee temFee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
            //���������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.departmentManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.pharmacyIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.undrugManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            temFee.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            ArrayList firstInputFeeItemlist = new ArrayList();
            //���汾���շ���Ŀ��ϸ��Ϣ
            this.feeItemCollection = new List<FeeItemList>();

            //����ʱ��
            DateTime  operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //decimal freeCost = this.patientInfo.FT.LeftCost;//���
            //decimal moneyAlert = this.patientInfo.PVisit.MoneyAlert;//������
            //decimal totCost = 0m;//�ܽ��

            //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}���벼�����ж��Ƿ���ҩƷҵ�����ȡȡҩҩ��
            bool refreshStockDept = true;
            //ѭ������
            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                FeeItemList feeItemList = new FeeItemList();
                bool isNew = false;              
                int returnValue = 0;

                //��Ŀ��Ϣ��ֵ
                returnValue = this.SetItem(i, PayTypes.Balanced, employee.Dept.ID, operTime, ref isNew, ref feeItemList);
               

                //�����õ���Ŀ��ϢΪ��,������
                if (returnValue == 0)
                {
                    continue;
                }

                if (returnValue == -1) 
                {
                    this.feeIntergrate.Rollback();

                    return -1;
                }

                //{0604764A-3F55-428f-9064-FB4C53FD8136}
                //������������
                if (this.OperationNO != string.Empty)
                {
                    feeItemList.OperationNO = this.OperationNO;
                }
                //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                if (this.defautStockDept != null && this.defautStockDept.ID != "" 
                    && feeItemList.Item.ItemType == EnumItemType.Drug && this.defautStockDept.ID!="default")
                {
                    feeItemList.ExecOper.Dept.ID= this.defautStockDept.ID;
                    refreshStockDept = false;
                }

                //�������¼����Ŀ
                if (isNew)
                {                 
                    feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;
                    firstInputFeeItemlist.Add(feeItemList.Clone());

                    this.feeItemCollection.Add(feeItemList.Clone());
                }
                // �޸ĵĻ�����Ŀ��ֻ���޸�����
                else
                {
                    //����ԭ�еķ�����ϸ��¼�Ľ�������
                    if (this.inpatientManager.UpdateChargeInfo(feeItemList) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(Language.Msg("����ԭ�л�����Ϣ��¼����!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }

                    //������û��ܸ�������--�������ҵ��
                    if (this.inpatientManager.FeeAfterCharge(this.patientInfo, feeItemList) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(feeItemList.Item.Name + Language.Msg("������Ŀ���շѳ���!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }

                    //ҩƷ���������
                    //if (feeItemList.Item.IsPharmacy)
                    //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                    if(feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.pharmacyIntergrate.ApplyOut(this.patientInfo, feeItemList, operTime, refreshStockDept) == -1)
                        {
                            feeIntergrate.Rollback();
                            MessageBox.Show(Language.Msg(feeItemList.Item.Name + "��Ӧ����") + this.pharmacyIntergrate.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                }
            }


            //{F4912030-EF65-4099-880A-8A1792A3B449} �����ֽ����ϲ���ָ�����Ŀ��������
            if (!this.isSplitUndrugCombItem) 
            {
                SplitUndrugCombItem(ref firstInputFeeItemlist);

                // ��������{0604764A-3F55-428f-9064-FB4C53FD8136}
                //foreach ( FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist)
                //{
                //    feeItemList.OperationNO = operationNO;
                //    //{FD08EA4B-B5F3-4514-AEEA-44222F5900FB} �����շѱ��б�������ҽ���� 
                //    feeItemList.Order.ID = orderID;
                //}
            }
            ////{F4912030-EF65-4099-880A-8A1792A3B449}����

            if (this.IsPopSHowInvoice)
            {
                ArrayList allInvoiceFeeType = new ArrayList();
                Hashtable allFeeByInvoice = this.GetFeeByInvoice(firstInputFeeItemlist,ref allInvoiceFeeType);
                string PopShowMessage = string.Empty;
                allInvoiceFeeType.Sort(new CompareByID());
                if (allInvoiceFeeType.Count > 0)
                {
                    foreach (string statCode in allInvoiceFeeType)
                    {
                        PopShowMessage += (allFeeByInvoice[statCode] as FS.HISFC.Models.Fee.Inpatient.BalanceList).Name + "�� " + (allFeeByInvoice[statCode] as FS.HISFC.Models.Fee.Inpatient.BalanceList).BalanceBase.FT.TotCost + "\n";
                    }
                    PopShowMessage = "סԺ�ţ�" + (firstInputFeeItemlist[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Patient.PID.PatientNO + "         ������" + (firstInputFeeItemlist[0] as FS.HISFC.Models.Fee.Inpatient.FeeItemList).Patient.Name + "\n"  + "------------------------------------------\n"+ PopShowMessage + "\n�Ƿ�ȷ���շѣ�";
                    if ((DialogResult)MessageBox.Show(PopShowMessage, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) != DialogResult.Yes)
                    {
                        return -1;
                    }
                }
                
            }

            //���������շѺ���,��ȡ��һ��¼��ķ���
            if (this.feeIntergrate.FeeItem(this.patientInfo, ref firstInputFeeItemlist) == -1)
            {
                feeIntergrate.Rollback();
                MessageBox.Show(this.feeIntergrate.Err);
                this.feeIntergrate.MedcareInterfaceProxy.Disconnect();
                this.fpDetail.Focus();
                
                return -1;
            }
            //�Ե�һ���շѵ���Ŀ����ҩƷ������Ϣ
            //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist) 
            {
                //ҩƷ���������
                //if (feeItemList.Item.IsPharmacy)
                if(feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;

                    if (this.pharmacyIntergrate.ApplyOut(this.patientInfo, feeItemList, operTime, refreshStockDept) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(Language.Msg(feeItemList.Item.Name+"��Ӧ����") + this.pharmacyIntergrate.Err);
                        this.fpDetail.Focus();

                        return -1;
                    }
                }
            }
            //{4FF03BBF-763D-4063-A792-A2264999E79A}
            if (IAdptIllnessInPatient != null)
            {
                ArrayList feeList = new ArrayList(feeItemCollection);
                int resultValue = IAdptIllnessInPatient.SaveInpatientFeeDetail(this.patientInfo, ref feeList);

                if (resultValue < 0)
                {
                    feeIntergrate.Rollback();
                    return -1;
                }
            }
            this.feeIntergrate.MedcareInterfaceProxy.CloseAll();
            #region ɾ���ݴ���Ϣ
            //��ɾ��������  
            int ret = temFee.Delete(this.patientInfo.ID, recipeDept.ID, this.OperationNO);
            if (ret < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ���ݴ��շ���ϸ����");
                return -1;
            }
            #endregion
            this.feeIntergrate.Commit();

            this.sucessMsg = "�շѳɹ�!";

            return 1;
        }

        /// <summary>
        /// ���ݷ�����ϸ��ȡ��Ʊ������Ϣ
        /// </summary>
        /// <param name="firstInputFeeItemlist"></param>
        /// <returns></returns>
        private Hashtable GetFeeByInvoice(ArrayList firstInputFeeItemlist, ref ArrayList allInvoiceType)
        {
            Hashtable hsFeeCodeStatByInvoice = new Hashtable();

           allInvoiceType = new ArrayList();
            
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist)
            {
                FS.HISFC.Models.Fee.FeeCodeStat feeCodeStat = allFeecodeStatHelper[feeItemList.Item.MinFee.ID] as FS.HISFC.Models.Fee.FeeCodeStat;
                if (hsFeeCodeStatByInvoice.Contains(feeCodeStat.StatCate.ID))
                {
                    FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListTmp = hsFeeCodeStatByInvoice[feeCodeStat.StatCate.ID] as FS.HISFC.Models.Fee.Inpatient.BalanceList;
                    balanceListTmp.BalanceBase.FT.TotCost += feeItemList.FT.TotCost;
                }
                else
                {
                    allInvoiceType.Add(feeCodeStat.StatCate.ID);
                    FS.HISFC.Models.Fee.Inpatient.BalanceList balanceListTmp = new BalanceList();
                    balanceListTmp.Name = feeCodeStat.StatCate.Name;
                    balanceListTmp.BalanceBase.FT.TotCost = feeItemList.FT.TotCost;
                    hsFeeCodeStatByInvoice.Add(feeCodeStat.StatCate.ID, balanceListTmp);
                }
            }
            return hsFeeCodeStatByInvoice;
        }

        
        /// <summary>
        /// ��ָ�����Ŀ//{F4912030-EF65-4099-880A-8A1792A3B449}
        /// </summary>
        /// <param name="itemList">��ǰ��Ŀ�б�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int SplitUndrugCombItem(ref ArrayList itemList) 
        {
            ArrayList undrugCombItemList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in itemList)
            {
                if (f.Item.ItemType == EnumItemType.UnDrug && (f.Item.User01 == "[������]" || f.IsGroup))
                {
                    undrugCombItemList.Add(f);
                }
                else//�������¼ӵĸ�����ID add by yerl
                {
                    if (string.IsNullOrEmpty(f.Order.ID))
                        f.Order.ID = this.orderIntegrate.GetNewOrderID();
                    //���ҽ��ִ�е���ˮ��Ϊ��,���ȡ 
                    if (string.IsNullOrEmpty(f.ExecOrder.ID))
                        f.ExecOrder.ID = orderManager.GetNewOrderExecID();

                    decimal price = 0;
                    decimal orgPrice = 0;

                    //����ȡ�շѼ�
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, f.Item, ref price, ref orgPrice) < 0)
                    {
                        MessageBox.Show(Language.Msg("��ȡ������ϸ�ļ۸�ʧ��!") + this.feeIntergrate.Err);
                        return -1;
                    }
                    f.Item.Price = price;
                    f.Item.DefPrice = orgPrice;
                }
            }
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in undrugCombItemList) 
            {
                itemList.Remove(f);
            }

            ArrayList finalCombItemList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in undrugCombItemList)
            {
                ArrayList details = this.undrugPackageManager.QueryUndrugPackagesBypackageCode(f.Item.ID);
                if (details == null)
                {
                    MessageBox.Show(Language.Msg("���������Ϣ����!") + this.undrugPackageManager.Err);

                    return -1;
                }

                string orderID = this.orderIntegrate.GetNewOrderID();

                decimal rate = 1;
                for (int i = 0; i < details.Count; i++)
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = details[i] as FS.HISFC.Models.Fee.Item.UndrugComb;

                    //���������Ŀ������
                    //·־�� �Ƿ����1��true���ã� 0��false������ luzhp@FS.com
                    if (undrugComb.User01 == "0")
                    {
                        continue;
                    }

                    FS.HISFC.Models.Fee.Item.Undrug undrug = this.itemManager.GetValidItemByUndrugCode(undrugComb.ID);
                    if (undrug == null)
                    {
                        continue;
                    }
                    undrug.ItemType = EnumItemType.UnDrug;
                    if (undrugComb.Qty == 0)
                    {
                        undrug.Qty = 1 * f.Item.Qty;
                    }
                    else
                    {
                        undrug.Qty = undrugComb.Qty * f.Item.Qty;
                    }

                    undrug.User03 = "1";
                    undrug.MinFee.User01 = f.Item.ID;
                    undrug.MinFee.User02 = f.Item.Name;
                    

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList fComb = new FeeItemList();
                    fComb = f.Clone();

                    //���ҽ��ִ�е���ˮ��Ϊ��,���ȡ add by yerl
                    if (string.IsNullOrEmpty(fComb.ExecOrder.ID))
                        fComb.ExecOrder.ID = orderManager.GetNewOrderExecID();

                    fComb.NoBackQty = undrug.Qty;
                    fComb.Item = undrug;
                    fComb.UndrugComb.ID = undrug.MinFee.User01;
                    fComb.UndrugComb.Name = undrug.MinFee.User02;

                    decimal price = undrug.Price;
                    decimal orgPrice = undrug.DefPrice;

                    rate = feeIntergrate.GetItemRateForZT(f.Item.ID, undrug.ID);

                    //���ʱ ����ȡ�շѼ�
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, undrug, ref price, ref orgPrice, rate) < 0)
                    {
                        MessageBox.Show(Language.Msg("��ȡ������ϸ�ļ۸�ʧ��!") + this.feeIntergrate.Err);
                        return -1;
                    }
                    undrug.Price = price;
                    undrug.DefPrice = orgPrice;

                    fComb.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(fComb.Item.Price * fComb.Item.Qty, 2);
                    fComb.FT.OwnCost = fComb.FT.TotCost;
                    fComb.Order.ID = orderID;

                    //������Ŀ����
                    fComb.UndrugComb.Qty = f.Item.Qty;

                    finalCombItemList.Add(fComb);
                }
            }

            itemList.AddRange(finalCombItemList);

            return 1;
        }//{F4912030-EF65-4099-880A-8A1792A3B449}����


        /// <summary>
        /// ��ʾ���ܽ��
        /// </summary>
        protected virtual void Sum()
        {
            int count = this.fpDetail_Sheet.RowCount;

            if (count > 1)
            {
                count = count - 1;
                //this.fpDetail_Sheet.Cells[count, (int)Columns.TotCost].Formula = "sum(F1:F" + count.ToString() + ")";
                this.fpDetail_Sheet.Cells[count, (int)Columns.TotCost].Formula = "sum(G1:G" + count.ToString() + ")";
            }
            else if (count > 0)
            {
                this.fpDetail_Sheet.SetValue(count - 1, (int)Columns.TotCost, 0, false);
            }
        }
        
        /// <summary>
        /// ��ʼ����Ŀ��ʾ�б�(FarPoint)
        /// </summary>
        protected virtual void InitFP()
        {
            InputMap im;
            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);

            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.fpDetail_Sheet.Columns[(int)Columns.ItemObject].Visible = false;
            this.fpDetail_Sheet.Columns[(int)Columns.IsNew].Visible = false;
            this.fpDetail_Sheet.Columns[(int)Columns.IsDeptChange].Visible = false;
            this.fpDetail_Sheet.Columns[(int)Columns.IsDrug].Visible = false;

            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            this.fpDetail_Sheet.Columns[(int)Columns.feeRate].Visible = isShowFeeRate;

        }
        
        /// <summary>
        /// ��ʼ�������б�
        /// </summary>
        private int InitDept()
        {
            ArrayList deptLists = this.departmentManager.GetDeptmentAll();
            if (deptLists == null) 
            {
                MessageBox.Show(Language.Msg("���ؿ����б����!") + this.departmentManager.Err);
                
                return -1;
            }
            this.lbDept.AddItems(deptLists);
            unDrugDeptList = deptLists;//{CA82280B-51B6-4462-B63E-43F4ECF456A3}
            this.Controls.Add(this.lbDept);
            this.lbDept.Hide();
           
            this.lbDept.BorderStyle = BorderStyle.FixedSingle;
            this.lbDept.BringToFront();

            this.lbDept.SelectItem += new FS.FrameWork.WinForms.Controls.PopUpListBox.MyDelegate(lbDept_SelectItem);
           
            return 1;
        }

        /// <summary>
        /// ����ѡ���¼�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int lbDept_SelectItem(Keys key)
        {
            ProcessDept();
            this.fpDetail.Focus();
            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept, true);

            return 1;
        }

        /// <summary>
        /// ����this.fpDetail,ִ�п��ҵĻس�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessDept()
        {
            int currRow = this.fpDetail_Sheet.ActiveRowIndex;
            
            if (currRow < 0)
            {
                return 1;
            }
            
            if (this.fpDetail_Sheet.GetText(currRow, (int)Columns.Unit) == "С��")
            {
                return 1;
            }

            string IsDeptChange = this.fpDetail_Sheet.GetText(currRow, (int)Columns.IsDeptChange);

            if ((IsDeptChange == "0" || IsDeptChange == string.Empty) && this.fpDetail_Sheet.GetText(currRow,(int)Columns.Dept) == string.Empty)
            {
                MessageBox.Show(Language.Msg("ִ�п��Ҳ���Ϊ��,������!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(currRow, (int)Columns.Dept, true);

                return -1;
            }

            if (IsDeptChange == "1")
            {
                FS.FrameWork.Models.NeuObject item = null;

                int returnValue = this.lbDept.GetSelectedItem(out item);
                if (returnValue == -1 || item == null)
                {
                    return -1;
                }

                object obj = this.fpDetail_Sheet.GetValue(currRow, (int)Columns.ItemObject);
                if (obj == null)
                {
                    MessageBox.Show(Language.Msg("��ѡ����Ŀ!"));
                    this.fpDetail.Focus();
                    this.fpDetail_Sheet.SetActiveCell(currRow, (int)Columns.Dept, true);

                    return -1;
                }
                this.fpDetail.StopCellEditing();
                this.fpDetail_Sheet.SetValue(currRow, (int)Columns.Dept, item.Name);

                FeeItemList feeitemlist = (FeeItemList)obj;
                feeitemlist.ExecOper.Dept.ID = item.ID;
                feeitemlist.ExecOper.Dept.Name = item.Name;

                this.fpDetail_Sheet.SetValue(currRow, (int)Columns.ItemObject, feeitemlist);
                this.fpDetail_Sheet.SetValue(currRow, (int)Columns.IsDeptChange, "0");
            }

            this.lbDept.Visible = false;

            return 1;
        }

        /// <summary>
        /// ����ucItem/cmbdept����ʾλ��
        /// </summary>
        /// <returns></returns>
        private int SetLocation()
        {
            Control cell = this.fpDetail.EditingControl;
            if (cell == null)
            {
                return -1;
            }

            if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName)
            {
                int y = cell.Top + cell.Height + this.ucItemList.Height + 7;
                if (y <= this.Height)
                {
                    this.ucItemList.Location = new Point(cell.Location.X + cell.Left, y - this.ucItemList.Height);
                }
                else
                {

                    if (cell.Top - this.ucItemList.Height - 7 > 0)
                    {
                        this.ucItemList.Location = new Point(cell.Location.X + cell.Left, cell.Top - this.ucItemList.Height - 7);
                    }
                    else
                    {
                        this.ucItemList.Location = new Point(cell.Location.X + cell.Left, 2);
                    }
                }
            }
            else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Dept)
            {
                this.lbDept.Size = new Size(cell.Width + SystemInformation.Border3DSize.Width * 2, 150);

                int y = cell.Top + cell.Height + this.lbDept.Height + SystemInformation.Border3DSize.Height * 2;

                if (y <= this.Height)
                {
                    this.lbDept.Location = new Point(cell.Left, y - this.lbDept.Height);
                }
                else
                {
                    this.lbDept.Location = new Point(cell.Left, cell.Top - this.lbDept.Height);
                }
            }

            return 0;
        }		

        /// <summary>
        /// �ж������Cell�Ƿ�Ϸ�
        /// </summary>
        /// <param name="itemName">��Ŀ����</param>
        /// <param name="row">��ǰ��</param>
        /// <param name="col">��ǰ��</param>
        /// <param name="isNumber">�Ƿ�������</param>
        /// <param name="errText">������Ϣ</param>
        /// <returns>�Ϸ� true ���Ϸ� false</returns>
        private bool IsInputValid(string itemName, int row, Columns col, bool isNumber, string errText)
        {
            string tempValue = this.fpDetail_Sheet.GetText(row, (int)col);
            if (tempValue == string.Empty)
            {
                if (isNumber)
                {
                    tempValue = "0";
                }
            }

            if (isNumber)
            {
                decimal tempNumber = NConvert.ToDecimal(tempValue);
                if (tempNumber <= 0)
                {
                    MessageBox.Show(itemName + Language.Msg(errText));
                    this.fpDetail.Focus();
                    this.fpDetail_Sheet.SetActiveCell(row, (int)col);

                    return false;
                }
            }
            else
            {
                MessageBox.Show(itemName + Language.Msg(errText));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)col);

                return false;
            }

            return true;
        }

        #region Ĭ��ִ�п����б�
        private static Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug> dicUndrugExec = new Dictionary<string, FS.SOC.HISFC.Fee.Models.Undrug>();

        private static FS.SOC.HISFC.Fee.Models.Undrug GetUndrugExecInfo(string itemCode)
        {
            FS.SOC.HISFC.Fee.Models.Undrug item = null;
            if (dicUndrugExec.ContainsKey(itemCode))
            {
                item = dicUndrugExec[itemCode];
            }
            else
            {
                FS.SOC.HISFC.Fee.BizLogic.Undrug undrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                item = undrugMgr.GetExecInfo(itemCode);

                dicUndrugExec.Add(itemCode, item);
            }

            return item;
        }
        #endregion

        /// <summary>
        /// ������Ŀ�б��е���Ŀ��ȡ����Ŀ��Ĭ��ִ�п���
        /// </summary>
        /// <param name="item"></param>
        /// <param name="deptCode"></param>
        /// <param name="deptName"></param>
        /// <returns></returns>
        private int GetItemDept(FS.HISFC.Models.Base.Item itemInfo, ref string deptCode, ref string deptName)
        {
            ArrayList alExecDept = new ArrayList();
            string defaultExecDept = string.Empty;
            FS.SOC.HISFC.Fee.Models.Undrug item = GetUndrugExecInfo(itemInfo.ID);
            if (string.IsNullOrEmpty(item.ExecDept)
                       || item.ExecDept == "ALL")
            {
                alExecDept = null;
                defaultExecDept = item.DefaultExecDeptForIn;
                deptCode = defaultExecDept;
                deptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode);
            }
            else
            {
                string[] depts = item.ExecDept.Split('|');
                alExecDept = new ArrayList();
                string firstDept = "";
                for (int i = 0; i < depts.Length; i++)
                {
                    FS.HISFC.Models.Base.Department deptObj = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(depts[i]);
                    if (deptObj != null)
                    {
                        alExecDept.Add(deptObj);
                        if (string.IsNullOrEmpty(firstDept))
                        {
                            firstDept = deptObj.ID;
                        }
                    }
                }

                if (item.ExecDept.Contains(this.patientInfo.PVisit.PatientLocation.Dept.ID))
                {
                    defaultExecDept = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                    deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    defaultExecDept = item.DefaultExecDeptForIn;
                    deptCode = item.DefaultExecDeptForIn;
                    deptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode);
                }

                if (string.IsNullOrEmpty(defaultExecDept)
                    && !string.IsNullOrEmpty(firstDept))
                {
                    defaultExecDept = firstDept;
                    deptCode = firstDept;
                    deptName = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(deptCode);
                }
            }

            if (string.IsNullOrEmpty(defaultExecDept))
            {
                defaultExecDept = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
            }

            return 0;
        }

        #region ԭ�з�ʽ����
        /// <summary>
        /// ������Ŀ�б��е���Ŀ��ȡ����Ŀ��Ĭ��ִ�п���
        /// </summary>
        /// <param name="item">��Ŀʵ��</param>
        /// <param name="deptID">���ұ���</param>
        /// <param name="deptName">��������</param>
        /// <returns></returns>
        //private int GetItemDept(FS.HISFC.Models.Base.Item item, ref string deptCode, ref string deptName)
        //{
        //    if (item is FS.HISFC.Models.Fee.Item.Undrug)
        //    {
        //        //��÷�ҩƷĬ�ϵ�ִ�п���
        //        deptCode = (item as FS.HISFC.Models.Fee.Item.Undrug).ExecDept;
        //        if (deptCode == null || deptCode == string.Empty)
        //        {
        //            if (this.defaultExeDept != null && this.defaultExeDept != string.Empty)
        //            {
        //                deptCode = this.defaultExeDept;
        //                FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(this.defaultExeDept);
        //                if (dept == null)
        //                {
        //                    deptName = "(��)";
        //                }
        //                else
        //                {
        //                    deptName = dept.Name;
        //                }
        //            }
        //            else
        //            {
        //                if (this.patientInfo != null)
        //                {
        //                    deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
        //                    deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //��ֿ��Ҵ���������ҵĻ���Ĭ��ȡ��һ��
        //            int index = deptCode.IndexOf("|");
        //            if (index < 0)
        //            {
        //                index = deptCode.Length;
        //            }
        //            deptCode = deptCode.Substring(0, index);

        //            FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(deptCode);
        //            if (dept == null)
        //            {
        //                deptName = "(��)";
        //            }
        //            else
        //            {
        //                deptName = dept.Name;
        //            }
        //        }
        //    }
        //    else if (item is FS.HISFC.Models.Pharmacy.Item)
        //    {
        //        //���ҩƷ��ִ�п���
        //        if (this.defaultExeDept != null && this.defaultExeDept != string.Empty)
        //        {
        //            deptCode = this.defaultExeDept;
        //            FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(this.defaultExeDept);
        //            if (dept == null)
        //            {
        //                deptName = "(��)";
        //            }
        //            else
        //            {
        //                deptName = dept.Name;
        //            }
        //        }
        //        else
        //        {
        //            if (this.patientInfo != null)
        //            {
        //                deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
        //                deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        //            }
        //        }
        //    }
        //    else if (item is FS.HISFC.Models.FeeStuff.MaterialItem)
        //    {
        //        if (tempDept != null)
        //        {
        //            deptName = tempDept.Name;
        //            deptCode = tempDept.ID;
        //        }
        //        else if (this.patientInfo != null)
        //        {
        //            deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
        //            deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
        //        }
        //        else
        //        {
        //            deptName = this.operObj.Dept.Name;
        //            deptCode = this.operObj.Dept.ID;
        //        }
        //    }
        //    return 0;
        //}
        #endregion

        /// <summary>
        /// ����this.fpDetail����Ŀ���ƵĻس�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessItem()
        {
            if (this.ucItemList.Visible == false)
            {
                this.ucItemList.Visible = true;

                return 0;
            }
            try
            {
                Item item = new Item();

                int returnValue = this.ucItemList.GetSelectItem(out item);
                if (returnValue == -1 || returnValue == 0)
                {
                    return -1;
                }
                int currRow = this.fpDetail_Sheet.ActiveRowIndex;
                
                if (currRow < 0)
                {
                    return -1;
                }
                if (this.fpDetail_Sheet.GetText(currRow, (int)Columns.IsNew) == "0")
                {
                    return -1;
                }
                //{D18CDB1B-BB1E-422d-9161-65D9CEC79C05}
                string orderitemname = item.Name.ToString();
                if (orderitemname.Contains("������") && (DateTime.Now.Date.Subtract(this.patientInfo.Birthday.Date).Days + 1) > 365 * 16)//��������
                {
                    if (MessageBox.Show("��������ĿӦ�ÿ���BB����,�����Ϲ淶", "�Ƿ�ȷ��Ҫ����������", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        this.fpDetail.StopCellEditing();
                        this.fpDetail_Sheet.Rows.Remove(currRow, 1);
                        return -1;
                    }
                }
             
                if (item.User01 == "[����]")
                {
                    //�������⴦�� ������ε� �����ܵ�������  zhangjunyi@FS.com �޸�
                    if (this.AddGroupDetail(item.ID, currRow) == -1)
                    {
                        return -1;
                    }
                }
                else if (item.User01 == "[������]")
                {
                    string deptid = string.Empty;
                    if (item.User02 != null && item.User02 != string.Empty)
                    {
                        //��ָ�����ִ�п��Ҵ���������ҵĻ���Ĭ��ȡ��һ��
                        int index = item.User02.IndexOf("|");
                        if (index < 0) index = item.User02.Length;

                        deptid = item.User02.Substring(0, index);
                    }

                    //{F4912030-EF65-4099-880A-8A1792A3B449}
                    if (this.isSplitUndrugCombItem)
                    {
                        this.AddCompoundDetail(item.ID, item.Name, currRow, deptid);
                    }
                    else 
                    {
                        if (item.Price == 0 && !this.isChargeZero)
                        {
                            MessageBox.Show(Language.Msg("�۸�Ϊ0����Ŀ") + "[" + item.Name + "]" + Language.Msg("�������շ�!"));

                            return -1;
                        }
                        item.Qty = 1;
                        item.User03 = "1";//Ĭ��������Ŀ�ĸ�����Ϊ1
                        //��ӻ�����ϸ
                        this.AddChargeDetail(item, currRow, string.Empty);
                    }
                    //{F4912030-EF65-4099-880A-8A1792A3B449}����
                }
                else
                {
                    if (item.Price == 0 && !this.isChargeZero)
                    {
                        MessageBox.Show(Language.Msg("�۸�Ϊ0����Ŀ") + "[" + item.Name + "]" + Language.Msg("�������շ�!"));

                        return -1;
                    }
                    item.Qty = 1;
                    item.User03 = "1";//Ĭ��������Ŀ�ĸ�����Ϊ1
                    //��ӻ�����ϸ
                    this.AddChargeDetail(item, currRow, string.Empty);
                }

                this.Sum();//��ʾ����
                this.ucItemList.Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.fpDetail.Focus();

                return -1;
            }

            return 0;
        }

        //����this.fpDetail,�۸���������ҩ�����Ļس�
        private int SetItemProperty()
        {
            int row = this.fpDetail_Sheet.ActiveRowIndex;
            if (row < 0)
            {
                return -1;
            }

            this.fpDetail.StopCellEditing();

            object obj = new object();
            obj = this.fpDetail_Sheet.GetValue(row, (int)Columns.ItemObject);
            if (obj == null)
            {
                return -1;
            }

            ////�۸�{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            string text = string.Empty;
            //string text = this.fpDetail_Sheet.GetText(row, (int)Columns.Price);
            //if (text == null || text == string.Empty)
            //{
            //    text = "0";
            //}
            //decimal price = NConvert.ToDecimal(text);
            
            //����
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.Qty);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }
            decimal qty = NConvert.ToDecimal(text);
            
            //����
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.Day);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }

            decimal day = NConvert.ToDecimal(text);

          



            //���շѡ��˷ѵȷ��õļ��㷽������һ��{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
            //this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber(price * qty * day, 2), false);
            Item item = this.fpDetail_Sheet.Rows[row].Tag as Item;
            Item itemClone = this.fpDetail_Sheet.Cells[row, (int)Columns.feeRate].Tag as Item;

            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            //����
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.feeRate);
            if (text == null || text == string.Empty)
            {
                text = "1";
            }
            decimal feeRate = NConvert.ToDecimal(text);

            

            if (feeRate <= 0)
            {
                MessageBox.Show("��������С�ڻ����0");
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.feeRate, true);
                return -1;
            }



            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            //���ݱ���������
           // item.Price = itemClone.Price * feeRate;

            //this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Text = item.Price.ToString();
            //�۸�{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            text = this.fpDetail_Sheet.GetText(row, (int)Columns.Price);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }
            decimal price = NConvert.ToDecimal(text);


            if (itemClone.Price == 0)
            {
                item.Price = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Price)) * feeRate;
                itemClone.Price = item.Price;
            }
            else
            {
                item.Price = itemClone.Price * feeRate;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Price].Text = item.Price.ToString();
            }
            if (item.ItemType == EnumItemType.Drug)//ҩƷ
            {
             
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber((item.Price * qty / item.PackQty) * day, 2), false);

            }
            else
            {
                
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber((item.Price * qty) * day, 2), false);
            }

            this.Sum();//����ϼ�

            if (!this.IsChargeZero && price <= 0)
            {
                MessageBox.Show(Language.Msg("��Ŀ�۸���С�ڻ��ߵ�����!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Price, true);

                return -1;
            }

            if (qty <= 0 && isJudgeQty)
            {
                MessageBox.Show(Language.Msg("������������С�ڻ��ߵ�����!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Qty, true);

                return -1;
            }

            if (day <= 0)
            {
                MessageBox.Show(Language.Msg("��ҩ��������С�ڻ��ߵ�����!"));
                this.fpDetail.Focus();
                this.fpDetail_Sheet.SetActiveCell(row, (int)Columns.Day, true);

                return -1;
            }

            return 0;
        }		

        private int SetItem(int row, PayTypes payType, string recipeDeptCode, DateTime operTime, ref bool isNewItem, ref FeeItemList feeItemList)
        {
            object obj = this.fpDetail_Sheet.GetValue(row, (int)Columns.ItemObject);
            if (obj == null)
            {
                return 0;
            }
            FS.HISFC.Models.Base.Employee operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
            feeItemList = (FeeItemList)obj;
            feeItemList.FeeOper.ID = operObj.ID;
            feeItemList.FeeOper.Dept.ID = operObj.Dept.ID;
            feeItemList.Item.SpecialFlag2 = "0";
            feeItemList.FTSource = this.ftSource;
            //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            feeItemList.FTRate.ItemRate = FS.FrameWork.Function.NConvert.ToDecimal( this.fpDetail_Sheet.GetText(row, (int)Columns.feeRate));
            if (this.isJudgeValid) 
            {
                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
                {
                    //FS.HISFC.Models.Pharmacy.Item phItem = this.pharmacyIntergrate.GetItem(feeItemList.Item.ID);
                    //if (phItem == null)
                    //{
                    //    MessageBox.Show("���ҩƷ������Ϣʧ��!" + this.pharmacyIntergrate.Err);

                    //    return -1;
                    //}

                    //if (phItem.ValidState != "0")
                    //{
                    //    MessageBox.Show(phItem.Name + "�Ѿ�ͣ��!������ѡ����Ч����Ŀ");

                    //    return -1;
                    //}
                }
                else if(feeItemList.Item.ItemType == EnumItemType.UnDrug)
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrug = this.undrugManager.GetItemByUndrugCode(feeItemList.Item.ID);
                    if (undrug == null)
                    {
                        MessageBox.Show("��÷�ҩƷ������Ϣʧ��!" + this.undrugManager.Err);
                        return -1;
                    }
                    if (undrug.ValidState!="1") 
                    {
                        MessageBox.Show(feeItemList.Item.Name + "�Ѿ�ͣ��!������ѡ����Ч����Ŀ");
                        return -1;
                    }

                    feeItemList.IsGroup = undrug.UnitFlag == "1" ? true : false;
                }
            }

            feeItemList.Days = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Day));

            if (feeItemList.Days == 0) 
            {
                feeItemList.Days = 1;
            }
            //����,ҩƷת��Ϊ��С��λ����
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)    
            {	//��Ŀ��λΪ��С��λ,����ͽ����ϵĵ�λ��ͬ,֤���շѵĵ�λΪ��С��λ
                if (feeItemList.Item.PriceUnit == this.fpDetail_Sheet.GetText(row, (int)Columns.Unit))
                {
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Qty));
                }
                else//����Ϊ��װ��λ,ת��Ϊ��С��λ
                {
                    feeItemList.Item.Qty = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Qty)) * feeItemList.Item.PackQty;
                }
            }
            else
            {
                feeItemList.Item.Qty = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Qty));
            }
            //���������Բ�ҩ����,��������
            feeItemList.Item.Qty = feeItemList.Item.Qty * feeItemList.Days;

            //�۸�,ҩƷ�۸������ݿ����ǰ�װ��λ�۸�
            if (feeItemList.Item.Price == 0)
            {
                feeItemList.Item.Price = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Price));
            }

            //�����ܶ�
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
                //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                //feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID).PriceCollection.RetailPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                
                //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                //�뻤ʿվ���˷ѵȷ��õļ��㷽������һ��
                //�¼Ƽ۹����ȼ������Ƭ�۸����4��5�룬�����ܼ�
                //סԺ���������շ��������в�֣�������˴������ֳ����ķ�����Ŀ�����г���
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.DefPrice / feeItemList.Item.PackQty), 2) * feeItemList.Item.Qty;
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.DefTotCost, 2);
            
            }
            else
            {
                //feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price * feeItemList.Item.Qty, 2);

                //{D48F1F6E-8792-4b73-884C-1FD26482DC60}
                //�뻤ʿվ���˷ѵȷ��õļ��㷽������һ��
                //�¼Ƽ۹����ȼ������Ƭ�۸����4��5�룬�����ܼ�
                //סԺ���������շ��������в�֣�������˴������ֳ����ķ�����Ŀ�����г���
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.Item.Price, 2) * feeItemList.Item.Qty;
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(feeItemList.FT.TotCost, 2);
            }

            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;

            if (this.fpDetail_Sheet.GetText(row, (int)Columns.IsNew) == "1")
            {
                feeItemList.TransType = TransTypes.Positive;
                feeItemList.Patient = this.patientInfo.Clone();
                feeItemList.RecipeOper.ID = this.recipeDoctCode;
                feeItemList.RecipeOper.Dept.ID = recipeDeptCode;
                feeItemList.PayType = PayTypes.Charged;
                feeItemList.ChargeOper.ID = this.inpatientManager.Operator.ID;
                feeItemList.ChargeOper.OperTime = operTime;
                feeItemList.BalanceNO = 0;
                feeItemList.BalanceState = "0";
                isNewItem = true;
                if (payType == PayTypes.Balanced)
                {
                    feeItemList.PayType = PayTypes.Balanced;
                    feeItemList.FeeOper.ID = this.inpatientManager.Operator.ID;
                    //feeItemList.FeeOper.OperTime = operTime;
                    feeItemList.FeeOper.OperTime = NConvert.ToDateTime(this.fpDetail_Sheet.GetText(row, (int)Columns.feeDate));//��ȡ����ļƷ�����
                    feeItemList.NoBackQty = feeItemList.Item.Qty;
                }
            }
            else
            {
                isNewItem = false;
            }

            return 1;
        }

        /// <summary>
        ///ɾ��һ��
        /// </summary>
        /// <param name="row">Ҫɾ�����к�</param>
        private void RemoveRow(int row) 
        {
            this.fpDetail.EditChange -= new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDetail_EditChange);

            for (int i = 0; i < this.fpDetail_Sheet.Columns.Count; i++)
            {
                this.fpDetail_Sheet.Cells[row, i].Tag = string.Empty;
                this.fpDetail_Sheet.Cells[row, i].Text = string.Empty;
            }

            this.fpDetail.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpDetail_EditChange);

            this.fpDetail_Sheet.Rows.Remove(row, 1);
        }

        /// <summary>
        /// ���ݷ�ҪƷ�����ȡ��ҩƷִ�п���{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        /// </summary>
        /// <param name="list"></param>
        public string SetExecDept(string itemID)
        {
            string id = string.Empty;
            ArrayList undrugDept = new ArrayList();
            if (dictDept.Count != 0)
            {
                foreach (string itemCode in dictDept.Keys)
                {
                    if (itemCode.Contains(itemID))
                    {
                        undrugDept.Add(dictDept[itemCode]);
                        if (dictDept[itemCode].User02 == "1")
                        {
                            id = dictDept[itemCode].ID;
                        }
                    }
                }
                if (undrugDept.Count != 0)
                {
                    this.lbDept.AddItems(undrugDept);
                }
                else
                {
                    this.lbDept.AddItems(unDrugDeptList);
                }
            }
            return id;
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ���溯��
        /// </summary>
        /// <returns></returns>
        public virtual int Save() 
        {
            int returnValue = 0;
            feeIntergrate.MessageType = this.MessageType;
            switch (this.feeType) 
            {
                case FeeTypes.����:
                    returnValue = this.Charge();
                    break;
                case FeeTypes.�շ�:
                    returnValue = this.Fee();
                    break;
            }

            return returnValue;
        }
        #region �����ݴ湦�� 
        /// <summary>
        /// �ݴ湦��  �Ƚ���ϸ�浽��ʱ����
        /// </summary>
        /// <returns></returns>
        public int TemparorySave()
        {
            FS.HISFC.BizLogic.Fee.TemporaryFee temFee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
            //��ȡ��ϸ 
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //����ʱ��
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //ִ�п���
            string recipeDept = this.recipeDept.ID;
            ArrayList feeList = new ArrayList();
              //ѭ������
            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                FeeItemList feeItemList = new FeeItemList();
                bool isNew = false;
                int returnValue = 0;

                //��Ŀ��Ϣ��ֵ
                returnValue = this.SetItem(i, PayTypes.Charged, recipeDept, operTime, ref isNew, ref feeItemList);

                //�����õ���Ŀ��ϢΪ��,������
                if (returnValue == 0)
                {
                    continue;
                }

                //{0604764A-3F55-428f-9064-FB4C53FD8136}
                //������������
                if (this.OperationNO != string.Empty)
                {
                    feeItemList.OperationNO = this.OperationNO;
                }
                feeList.Add(feeItemList);
            }
            try
            {
                //��ɾ��������  
                int ret = temFee.Delete(this.patientInfo.ID, recipeDept, this.OperationNO);
                if (ret < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("ɾ���ɵ��շ���ϸ����");
                    return -1;
                }
                int i = 0;
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList obj in feeList)
                {
                    i++;
                    if (temFee.Insert(obj, this.patientInfo.ID, recipeDept, this.OperationNO,i.ToString()) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������ϸʧ��" + temFee.Err);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("����ɹ�");
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;

        }

        /// <summary>
        /// ��ʾ�ݴ����� 
        /// </summary>
        /// <returns> 0 û���ݴ������ ,1 �ݴ�����ݲ�����ȷ��ʾ, -1 �������</returns>
        public int SetValue()
        {
            FS.HISFC.BizLogic.Fee.TemporaryFee temfee = new FS.HISFC.BizLogic.Fee.TemporaryFee();
            ArrayList list = null;
            //��������
           
            string recipeDept = this.recipeDept.ID;
            if (this.OperationNO == "") //�������  ���
            {
                list = temfee.Query(this.patientInfo.ID, recipeDept);
            }
            else
            {
                list = temfee.Query(this.patientInfo.ID, recipeDept, this.OperationNO);
            }
            if (list == null)
            {
                MessageBox.Show("��ѯ��ϸʧ��" + temfee.Err);
                return -1;
            }
            if (list.Count == 0)
            {
                return 0;
            }
            if (this.fpDetail_Sheet.Rows.Count > 0) //��ɾ�����ݡ�
            {
                this.fpDetail_Sheet.Rows.Remove(0, this.fpDetail_Sheet.Rows.Count);
            }
            decimal price = 0;
            int row = 0;
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList item in list)
            {
                if (item.Item.ID.Substring(0, 1).ToUpper() == "Y")
                {
                    #region ҩƷ
                    //����ҩƷid��ȡҩƷʵ��
                    FS.HISFC.Models.Pharmacy.Item drug = null;
                    FS.HISFC.BizLogic.Pharmacy.Item drugManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                    drug=drugManager.GetItem(item.Item.ID);
                    if (drug == null || drug.ID == "") continue;
                    decimal itemQty = item.Item.Qty;
                    item.Item = drug;
                    item.Days = 1;//����
                    item.Item.Qty = itemQty;
                    price = drug.Price;
                   
                    #endregion
                }
                else if (item.Item.ID.Substring(0, 1).ToUpper() == "F")
                {
                    #region ��ҩƷ
                    //���ݷ�ҩƷid��ȡ��ҩƷʵ��
                    FS.HISFC.Models.Fee.Item.Undrug undrug = null;
                    FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
                    undrug = this.feeIntergrate.GetUndrugByCode(item.Item.ID);
                    if (undrug == null) continue;//||undrug.ValidState==false)continue;
                    decimal itemQty = item.Item.Qty;

                    //����ȡ�۸�
                    decimal orgPrice = 0;

                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, undrug, ref price, ref orgPrice) == -1)
                    {
                        MessageBox.Show(Language.Msg("ȡ��Ŀ:") + item.Name + Language.Msg("�ļ۸����!") + this.pactUnitManager.Err);

                        return -1;
                    }
                    undrug.Price = price;
                    undrug.DefPrice = orgPrice;

                    item.Item = undrug;
                    item.IsGroup = undrug.UnitFlag == "1" ? true : false;
                    item.Item.Qty = itemQty;
                    item.Days = 1;//����
                    price = undrug.Price;

                    #endregion
                }
                #region ��ֵ
                this.fpDetail_Sheet.Rows.Add(row, 1); //����һ��
                //ҩƷĬ�ϰ���С��λ�շ�,��ʾ�۸�ҲΪ��С��λ�۸�,�������ݿ��Ϊ��װ��λ�۸�
                //if (item.IsPharmacy)//ҩƷ
                if (item.Item.ItemType == EnumItemType.Drug)//ҩƷ
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Item.Price / item.Item.PackQty, 4);
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                }
                else//��ҩƷ
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, item.Item.Price, false);
                    price = item.Item.Price;
                }

                //�洢��Ŀʵ����ڼ۸�ȼ���{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
                this.fpDetail_Sheet.Rows[row].Tag = item.Item;
                //{30B79077-CDC0-4de8-822A-8B04ABB2925C}
                this.fpDetail_Sheet.Cells[row, (int)Columns.feeRate].Tag = item.Item.Clone();
                //������
                this.fpDetail_Sheet.SetValue(row, (int)Columns.GbCode, item.Item.GBCode, false);
                //��Ŀ����,�͹����ʾ��һ��
                if (item.Item.Specs != null && item.Item.Specs != string.Empty)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Item.Name + "{" + item.Item.Specs + "}", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemName, item.Item.Name, false);
                }
                //����
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Qty, item.Item.Qty, false);

                //ҩƷ��ѡ��ҩƷ�շѵ�λ,Ĭ��Ϊ��С��λ  //��λ
                if (item.Item.ItemType == EnumItemType.Drug)
                {
                    FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                    comboType.Editable = true;
                    comboType.Items = (new string[]{(item.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (item.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit});
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = comboType;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = false;
                    if (item.Item.MinFee.User03 == "2")
                    {
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item.Item).PackUnit, false);
                        item.Item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item.Item).PackUnit;
                        price = FS.FrameWork.Public.String.FormatNumber(item.Item.Price, 4);
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                    }
                    else
                    {
                        price = FS.FrameWork.Public.String.FormatNumber(item.Item.Price / item.Item.PackQty, 4);
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
                        this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item.Item).MinUnit, false);
                        item.Item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item.Item).MinUnit;
                    }

                }
                else//��ҩƷ
                {
                    FarPoint.Win.Spread.CellType.TextCellType textType = new FarPoint.Win.Spread.CellType.TextCellType();
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = textType;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = true;
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, item.Item.PriceUnit, false);
                }
                //��ҩ����
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Day, 1, false);

                if (item.Item.ItemType == EnumItemType.Drug && item.Item.SysClass.ID.ToString() == "PCC")
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = false;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Black;
                }
                else
                {
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].Locked = true;
                    this.fpDetail_Sheet.Cells[row, (int)Columns.Day].ForeColor = Color.Transparent;
                }
                
                //�ܶ�
                this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, price * item.Item.Qty, false);
                //��¼�����Ŀ			
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsNew, "1", false);
                //��ʶҩƷ����ҩƷ
                //if (item.IsPharmacy)
                if (item.Item.ItemType == EnumItemType.Drug)
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
                }
                else
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
                }
                //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
                this.fpDetail_Sheet.SetValue(row, (int)Columns.feeRate, 1);
                string deptCode = string.Empty, deptName = string.Empty;
                if (!defaultExeDeptIsDeptIn)
                {
                    #region ��ȡĬ��ִ�п���
                    //��ȡ��ĿĬ��ִ�п���
                    //string execDeptCode = recipeDept;
                    //if (execDeptCode == null || execDeptCode == string.Empty)
                    //{
                    //    this.GetItemDept(item.Item, ref deptCode, ref deptName);
                    //}
                    //else
                    //{
                    //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                    //    deptCode = execDeptCode;
                    //    if (dept == null)
                    //    {
                    //        deptName = "(��)";
                    //    }
                    //    else
                    //    {
                    //        deptName = dept.Name;
                    //    }
                    //}

                    ////{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                    //execDeptCode = this.SetExecDept(item.ID);
                    //if (execDeptCode != "")
                    //{
                    //    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(execDeptCode);
                    //    deptCode = dept.ID;
                    //    deptName = dept.Name;
                    //}

                    #endregion
                    Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);

                    if (employee == null)
                    {
                        MessageBox.Show(Language.Msg("�����Ա������Ϣ����!"));

                        return -1;
                    }

                    if (this.recipeDept != null && this.recipeDept.ID != "")
                    {
                        employee.Dept = this.recipeDept;
                    }
                    else
                    {
                        this.recipeDept = employee.Dept;
                    }
                    if(item.Item.ItemType == EnumItemType.Drug)
                    {
                        ArrayList alExecDept = null;
                        FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(recipeDept);
                        deptCode = dept.ID;
                        deptName = dept.Name;
                        lbDept.Items.Clear();
                        SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                        alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                        lbDept.AddItems(alExecDept);
                    }
                    else
                    {
                        ArrayList alExecDept = null;
                        string defaultExecDept = string.Empty;
                        SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, recipeDept, item.Item.ID, ref defaultExecDept, ref alExecDept);
                        if (alExecDept == null || alExecDept.Count == 0)
                        {
                            SOC.HISFC.BizProcess.Cache.Common.GetDept("1");
                            alExecDept = SOC.HISFC.BizProcess.Cache.Common.deptHelper.ArrayObject;
                        }
                        FS.FrameWork.Models.NeuObject dept = this.managerIntegrate.GetDepartment(defaultExecDept);
                        deptCode = dept.ID;
                        deptName = dept.Name;
                        lbDept.Items.Clear();
                        lbDept.AddItems(alExecDept);
                    }
                }
                else
                {
                    deptCode = this.operObj.Dept.ID;
                    deptName = this.operObj.Dept.Name;
                }

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Dept, deptName, false);
                //��ʾ����δ�޸�
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDeptChange, "0", false);

                //��ֵ���շ�ʵ��
                FeeItemList feeitemlist = new FeeItemList();
                feeitemlist.Item = item.Item;
                feeitemlist.ExecOper.Dept.ID = deptCode;
                feeitemlist.ExecOper.Dept.Name = deptName;
                feeitemlist.IsGroup = item.IsGroup;
                feeitemlist.Days = NConvert.ToInt32(item.User03);//��ҩ����
                //ָ��ҩƷ�İ�ҩҩ��
                if (item is FS.HISFC.Models.Pharmacy.Item)
                {
                    feeitemlist.StockOper.Dept.ID = item.User02;
                }

                //���渴����Ŀ
                feeitemlist.UndrugComb.ID = item.Item.MinFee.User01;
                feeitemlist.UndrugComb.Name = item.Item.MinFee.User02;

                feeitemlist.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(price * item.Item.Qty, 2);

                this.fpDetail_Sheet.SetValue(row, (int)Columns.ItemObject, feeitemlist, false);
                //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
                if (IAdptIllnessInPatient != null)
                {
                    int resultValue = IAdptIllnessInPatient.ProcessInpatientFeeDetail(this.patientInfo, ref feeitemlist);
                    if (resultValue < 0) return -1;
                }
                #endregion
            }

            this.fpDetail_Sheet.Rows.Add(this.fpDetail_Sheet.RowCount, 1);
            this.fpDetail_Sheet.SetValue(this.fpDetail_Sheet.RowCount-1, (int)Columns.Unit, "�ϼ�", false);
            this.Sum();//��ʾ����
            return 0;
        }
        #endregion
        /// <summary>
        /// ��ָ���д����һ��
        /// </summary>
        /// <param name="row">���������</param>
        public virtual void AddRow(int row)
        {
            this.fpDetail_Sheet.Rows.Add(row, 1);
            this.fpDetail_Sheet.ActiveRowIndex = row;

            this.fpDetail_Sheet.Rows[this.fpDetail_Sheet.ActiveRowIndex].Height = 23;
            this.fpDetail.Focus();

            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName);

            if (this.fpDetail_Sheet.RowCount > 1 && this.fpDetail_Sheet.GetValue(0, (int)Columns.ItemObject) != null)
            {
                this.rowCount = 1;
            }

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                this.fpDetail_Sheet.Rows[i].Locked = false;
            }
            this.fpDetail_Sheet.Rows[this.fpDetail_Sheet.RowCount - 1].Locked = true;
        }

        /// <summary>
        /// ���һ����Ŀ
        /// </summary>
        public virtual void AddRow()
        {
            this.AddRow(this.fpDetail_Sheet.RowCount - 1);
            this.fpDetail.Focus();

            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName);
        }

        /// <summary>
        /// ����// {84B3B88C-6501-495b-9F82-278358EF5DD5}
        /// </summary>
        public virtual void Group()
        {
            FS.HISFC.Components.Common.Forms.frmGroup frmGroup = new FS.HISFC.Components.Common.Forms.frmGroup();

            frmGroup.ShowDialog();

            string groupID = "";
            groupID = frmGroup.GroupID;


            this.InsertGroup(groupID);

        }
        /// <summary>
        /// // {84B3B88C-6501-495b-9F82-278358EF5DD5}
        /// </summary>
        /// <param name="groupID"></param>
        public void InsertGroup(string groupID)
        {
            FS.HISFC.Components.Common.Forms.frmChooseGroupDetails frm = new FS.HISFC.Components.Common.Forms.frmChooseGroupDetails();
            frm.GroupID = groupID;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ���������Ϣ");
                Application.DoEvents();
                this.AddGroupDetail(groupID, frm.AlReturnDetails);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }

            frm.Dispose();
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        public virtual int Init(string deptCode)
        {
            this.InitFP();
            //this.ucItemList = new ucItemList(this.itemKind);
            this.ucItemList = new ucItemList();
            this.ucItemList.enuShowItemType = this.itemKind;
            this.ucItemList.NoAddDrugQuality = this.noAddDrugQuality;
            this.Controls.Add(this.ucItemList);
            this.curDrugApplyDept = deptCode;
            this.ucItemList.Init(deptCode);
            //this.ucItemList.AddGroup(deptCode);
            this.ucItemList.Hide();
            this.ucItemList.BringToFront();
            this.ucItemList.SelectItem += new ucItemList.MyDelegate(ucItemList_SelectItem);
            this.fpDetail.CellClick += new CellClickEventHandler(fpDetail_CellClick);//{CA82280B-51B6-4462-B63E-43F4ECF456A3}
            InitDept();

            //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
            ArrayList deptList = this.feeIntergrate.QueryDeptList("ALL", "2");
            foreach (FS.FrameWork.Models.NeuObject neuObj in deptList)
            {
                dictDept.Add(neuObj.Memo + "|" + neuObj.ID, neuObj);
            }

            allFeecodeStat = feeCodeStatMgr.QueryFeeCodeStatByReportCode("ZY01");
            if (allFeecodeStat != null && allFeecodeStat.Count > 0)
            {
                foreach (FS.HISFC.Models.Fee.FeeCodeStat statCode in allFeecodeStat)
                {
                    allFeecodeStatHelper.Add(statCode.MinFee.ID, statCode);
                }
            }

            return 1;
        }

        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        void fpDetail_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Column == (int)Columns.Dept)
            {
                string deptCode = string.Empty;
                if ("N,I".Contains(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID).DeptType.ID.ToString()))
                {
                    if (this.patientInfo != null)
                    {
                        deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    else
                    {
                        deptCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.recipeDoctCode).Dept.ID;
                    }
                }
                else
                {
                    deptCode = this.operObj.Dept.ID;
                }
                if (this.fpDetail_Sheet.ActiveRow.Tag != null && this.fpDetail_Sheet.ActiveRow.Tag is Item)
                {
                    Item item = this.fpDetail_Sheet.ActiveRow.Tag as Item;
                    if (item.ItemType == EnumItemType.UnDrug)
                    {
                        ArrayList alExecDept = null;

                        string defaultExecDept = string.Empty;
                        lbDept.Items.Clear();
                        SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, deptCode, item.ID, ref defaultExecDept, ref alExecDept);
                        lbDept.AddItems(alExecDept);
                    }
                }
            }
        }

        public void ChangeDept(FrameWork.Models.NeuObject deptObj)
        {
            
            if (deptObj == null)
            {
                MessageBox.Show("��������ʧ�ܣ�");
                return;
            }
            if (tempDept != null && tempDept == deptObj) return;

            int resultValue = this.ucItemList.RefreshDataSet(deptObj.ID);
            if (resultValue < 0)
            {
                MessageBox.Show("����������Ϣʧ�ܣ�");
                return;
            }
            tempDept = deptObj;
            this.Focus();
        }
        
        /// <summary>
        /// ɾ��һ����Ŀ
        /// </summary>
        /// <returns>�ɹ�: 1 ʧ��: -1</returns>
        public virtual int DelRow()
        {
            int row = this.fpDetail_Sheet.ActiveRowIndex;

            if (this.fpDetail_Sheet.RowCount == 0)
            {
                return 0;
            }
            if (row == this.fpDetail_Sheet.RowCount - 1)
            {
                return 0;
            }

            row++;

            DialogResult result = MessageBox.Show(Language.Msg("�Ƿ�ɾ����") + row.ToString() + Language.Msg("��?"),
                Language.Msg("��ʾ"), MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No)
            {
                this.fpDetail.Focus();

                return 1;
            }

            row--;

            //��ø���Ŀ�Ƿ�Ϊ��¼����Ŀ
            string newItem = this.fpDetail_Sheet.GetText(row, (int)Columns.IsNew);

            //��¼����Ŀֱ��ɾ��
            if (newItem == string.Empty || newItem == "1")
            {
                this.fpDetail.StopCellEditing();
                this.fpDetail_Sheet.Rows.Remove(row, 1);
                row = this.fpDetail_Sheet.ActiveRowIndex;
                this.fpDetail_Sheet.SetActiveCell(row, 0);
                
                if (this.fpDetail_Sheet.RowCount == 1) 
                {
                    this.AddRow(0);
                }
            }
            else//�����ݿ��ڼ�������Ŀ
            {
                object obj = this.fpDetail_Sheet.GetValue(row, (int)Columns.ItemObject);

                if (obj == null)
                {
                    return -1;
                }

                //��ȷ�ϵ�ҽ����ֻɾ��������ϸ�����������ݿ�
                if (obj is FS.HISFC.Models.Order.ExecOrder)
                {
                    this.fpDetail_Sheet.Rows.Remove(row, 1);
                }
                //�շ���Ŀ����
                //�������ݿ⣬ɾ��������ϸ
                else if (obj is FeeItemList)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //Transaction t = new Transaction(this.inpatientManager.Connection);
                    //t.BeginTransaction();
                    this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    if (this.inpatientManager.DeleteChargeInfo((FeeItemList)obj) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("ɾ����ϸʧ��!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();

                        return -1;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.fpDetail_Sheet.Rows.Remove(row, 1);
                }
            }

            //���¼���ϼ�
            this.Sum();

            //���úϼ��еõ�����
            if (this.fpDetail_Sheet.RowCount >= 2 && this.fpDetail_Sheet.ActiveRowIndex == this.fpDetail_Sheet.RowCount - 1)
            {
                this.fpDetail_Sheet.ActiveRowIndex = this.fpDetail_Sheet.ActiveRowIndex - 1;
            }

            this.fpDetail.Focus();

            return 1;
        }

        /// <summary>
        /// С��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� 0</returns>
        public int SubTotal()
        {
            try
            {
                this.isSubTotal = true;//��ʼ����С��
                if (this.fpDetail_Sheet.RowCount < 2)
                {
                    return 0;
                }
                int curIndex = this.fpDetail_Sheet.ActiveRowIndex;
                if (this.fpDetail_Sheet.Cells[curIndex, 0].Text == string.Empty)
                {
                    if (this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text == "С��")
                    {
                        this.isSubTotal = false;
                        return 0;
                    }
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text = "С��";
                }
                else
                {
                    if (this.fpDetail_Sheet.GetText(curIndex + 1, (int)Columns.Unit) == "С��")
                    {
                        this.isSubTotal = false;
                        return 0;
                    }
                    this.fpDetail_Sheet.Rows.Add(curIndex + 1, 1);
                    curIndex++;
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text = "С��";
                }
            DOStart:
                decimal subTot = 0;
                for (int i = curIndex - 1; i >= 0; i--)
                {
                    if (this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].Text != "С��")
                    {
                        subTot += NConvert.ToDecimal(this.fpDetail_Sheet.Cells[i, (int)Columns.TotCost].Text);
                    }
                    else
                    {
                        break;
                    }
                }
                if (subTot == 0)
                {
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.Unit].Text = string.Empty;
                }
                else
                {
                    this.fpDetail_Sheet.Cells[curIndex, (int)Columns.TotCost].Text = subTot.ToString();
                }
                for (int i = curIndex + 1; i < this.fpDetail_Sheet.RowCount - 1; i++)
                {
                    if (this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].Text == "С��")
                    {
                        curIndex = i;
                        goto DOStart;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //����С��
            this.isSubTotal = false;

            return 1;
        }

        /// <summary>
        /// ��ջ��ߺ���Ŀ��ϸ
        /// </summary>
        public virtual void Clear()
        {
            if (this.fpDetail_Sheet.RowCount >= 0)
            {
                this.fpDetail_Sheet.Rows.Remove(0, this.fpDetail_Sheet.RowCount);
            }

            this.AddRow(0);
            this.AddRow(0);

            this.fpDetail_Sheet.SetValue(1, (int)Columns.Unit, "�ϼ�", false);
            this.fpDetail_Sheet.Rows[1].Locked = true;

            this.Sum();//��ʾ����

            this.rowCount = 0;
            this.patientInfo = null;
        }

        /// <summary>
        /// ���ѡ������Ŀ��Ϣ
        /// </summary>
        /// <param name="recipeDoctCode">����ҽ�����ڿ���</param>
        /// <param name="dtNow">ִ��ʱ��</param>
        /// <param name="itemType">��Ŀ��� 1 ҩƷ 2 ��ҩƷ 3 �����Ŀ 4 ������Ŀ</param>
        /// <returns>�ɹ� ����FeeItemList�ķ���List���� ʧ��: null;</returns>
        public List<FeeItemList> QueryFeeItemList(string recipeDoctCode, DateTime dtNow, string itemType) 
        {
            List<FeeItemList> list = new List<FeeItemList>();

            this.fpDetail.StopCellEditing();

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++) 
            {
                bool isNewItem = false;

                FeeItemList feeItemList = new FeeItemList();

                int returnValue = this.SetItem(i, PayTypes.Balanced, recipeDoctCode, dtNow, ref isNewItem, ref feeItemList);
                
                //û�л����Ŀ
                if(returnValue != 1)
                {
                    continue;
                }

                switch (itemType) 
                {
                    //ҩƷ
                    case "1":
                        //if (feeItemList.Item.IsPharmacy) 
                        if (feeItemList.Item.ItemType == EnumItemType.Drug) 
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //��ҩƷ:
                    case "2":
                        //if (!feeItemList.Item.IsPharmacy)
                        if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //����
                    case "3":
                        if (feeItemList.IsGroup) 
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //����
                    case "0":
                        list.Add(feeItemList);
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// ���ѡ������Ŀ��Ϣ
        /// </summary>
        /// <param name="recipeDoctCode">����ҽ�����ڿ���</param>
        /// <param name="dtNow">ִ��ʱ��</param>
        /// <param name="itemType">��Ŀ��� 1 ҩƷ 2 ��ҩƷ 3 �����Ŀ 4 ������Ŀ</param>
        /// <returns>�ɹ� ����FeeItemList��ArrayList���� ʧ��: null;</returns>
        public ArrayList QueryFeeItemArrayList(string recipeDoctCode, DateTime dtNow, string itemType)
        {
            ArrayList list = new ArrayList();

            this.fpDetail.StopCellEditing();

            for (int i = 0; i < this.fpDetail_Sheet.RowCount; i++)
            {
                bool isNewItem = false;

                FeeItemList feeItemList = new FeeItemList();

                int returnValue = this.SetItem(i, PayTypes.Balanced, recipeDoctCode, dtNow, ref isNewItem, ref feeItemList);

                //û�л����Ŀ
                if (returnValue != 1)
                {
                    continue;
                }

                switch (itemType)
                {
                    //ҩƷ
                    case "1":
                        //if (feeItemList.Item.IsPharmacy)
                        if(feeItemList.Item.ItemType == EnumItemType.Drug)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //��ҩƷ:
                    case "2":
                        //if (!feeItemList.Item.IsPharmacy)
                        if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //����
                    case "3":
                        if (feeItemList.IsGroup)
                        {
                            list.Add(feeItemList);
                        }
                        break;
                    //����
                    case "0":
                        list.Add(feeItemList);
                        break;
                }
            }

            return list;
        }

        /// <summary>
        /// ���ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <param name="recipeDoctCode">����ҽ�����ڿ���</param>
        /// <param name="dtNow">ִ��ʱ��</param>
        /// <returns>�ɹ� ����FeeItemList�ķ���List���� ʧ��: null;</returns>
        public List<FeeItemList> QueryMedItemList(string recipeDoctCode, DateTime dtNow) 
        {
            return this.QueryFeeItemList(recipeDoctCode, dtNow, "1");
        }

        /// <summary>
        /// ���ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <param name="recipeDoctCode">����ҽ�����ڿ���</param>
        /// <param name="dtNow">ִ��ʱ��</param>
        /// <returns>�ɹ� ����FeeItemList��ArrayList���� ʧ��: null;</returns>
        public ArrayList QueryMedItemArrayList(string recipeDoctCode, DateTime dtNow)
        {
            return this.QueryFeeItemArrayList(recipeDoctCode, dtNow, "1");
        }

        /// <summary>
        /// ���ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ����FeeItemList�ķ���List���� ʧ��: null;</returns>
        public List<FeeItemList> QueryMedItemList() 
        {
            return this.QueryMedItemList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        /// ���ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ����FeeItemList��ArrayList���� ʧ��: null;</returns>
        public ArrayList QueryMedItemArrayList()
        {
            return this.QueryMedItemArrayList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        /// ��÷�ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <param name="recipeDoctCode">����ҽ�����ڿ���</param>
        /// <param name="dtNow">ִ��ʱ��</param>
        /// <returns>�ɹ� ����FeeItemList�ķ���List���� ʧ��: null;</returns>
        public List<FeeItemList> QueryUndrugItemList(string recipeDoctCode, DateTime dtNow)
        {
            return this.QueryFeeItemList(recipeDoctCode, dtNow, "2");
        }

        /// <summary>
        /// ��÷�ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <param name="recipeDoctCode">����ҽ�����ڿ���</param>
        /// <param name="dtNow">ִ��ʱ��</param>
        /// <returns>�ɹ� ����FeeItemList��ArrayList���� ʧ��: null</returns>
        public ArrayList QueryUndrugItemArrayList(string recipeDoctCode, DateTime dtNow)
        {
            return this.QueryFeeItemArrayList(recipeDoctCode, dtNow, "2");
        }

        /// <summary>
        /// ��÷�ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ����FeeItemList�ķ���List���� ʧ��: null;</returns>
        public List<FeeItemList> QueryUndrugItemList()
        {
            return this.QueryUndrugItemList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }

        /// <summary>
        /// ��÷�ҩƷ��Ŀ��Ϣ
        /// </summary>
        /// <returns>�ɹ� ����FeeItemList��ArrayList���� ʧ��: null</returns>
        public ArrayList QueryUndrugItemArrayList()
        {
            return this.QueryUndrugItemArrayList(string.Empty, this.inpatientManager.GetDateTimeFromSysDateTime());
        }
        #endregion
        #region �¼�

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                #region enter��up��down�¼�
                case Keys.Enter:
                    if (this.fpDetail.ContainsFocus)
                    {
                        //��Ŀ����
                        if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName
                            ||
                            this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.GbCode)
                        {
                            this.ProcessItem();
                        }
                        //�۸�
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Price)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Qty);
                        }
                        //����
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Qty)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            if (this.fpDetail_Sheet.Cells[this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Day].Locked)
                            {
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept);
                            }
                            else
                            {
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Day);
                            }
                        }
                        //����
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Day)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept);
                        }
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Unit)
                        {
                            this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.Dept);
                        }
                        //ִ�п���
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Dept)
                        {
                            if (this.ProcessDept() == -1)
                            {
                                return true;
                            }
                            if (this.fpDetail_Sheet.Columns[(int)Columns.feeRate].Visible == false)
                            {
                                //�����һ�У��Զ�����һ��
                                if (this.fpDetail_Sheet.RowCount == this.fpDetail_Sheet.ActiveRowIndex + 2)
                                {
                                    this.AddRow(this.fpDetail_Sheet.RowCount - 1);
                                }
                                else
                                {
                                    this.fpDetail_Sheet.ActiveRowIndex++;
                                    this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName, true);
                                }
                            }
                            else
                            {
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.feeRate);
                            }
                        }
                        //{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
                        else if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.feeRate)
                        {
                            if (this.SetItemProperty() == -1)
                            {
                                return true;
                            }
                            //�����һ�У��Զ�����һ��
                            if (this.fpDetail_Sheet.RowCount == this.fpDetail_Sheet.ActiveRowIndex + 2)
                            {
                                this.AddRow(this.fpDetail_Sheet.RowCount - 1);
                            }
                            else
                            {
                                this.fpDetail_Sheet.ActiveRowIndex++;
                                this.fpDetail_Sheet.SetActiveCell(this.fpDetail_Sheet.ActiveRowIndex, (int)Columns.ItemName, true);
                            }
                        }
                    }
                    break;
                case Keys.Up:
                    if (this.fpDetail.ContainsFocus)
                    {
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.PriorRow();
                        }
                        else if (this.lbDept.Visible)
                        {
                            this.lbDept.PriorRow();
                        }
                        else
                        {
                            int currRow = this.fpDetail_Sheet.ActiveRowIndex;
                            if (currRow > 0)
                            {
                                this.fpDetail_Sheet.ActiveRowIndex = currRow - 1;
                                this.fpDetail_Sheet.AddSelection(currRow - 1, 0, 1, 1);
                            }
                        }
                    }
                    break;
                case Keys.Down:
                    if (this.fpDetail.ContainsFocus)
                    {
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.NextRow();
                        }
                        else if (lbDept.Visible)
                        {
                            this.lbDept.NextRow();
                        }
                        else
                        {
                            int currRow = this.fpDetail_Sheet.ActiveRowIndex;

                            if (currRow < this.fpDetail_Sheet.RowCount - 2)
                            {
                                this.fpDetail_Sheet.ActiveRowIndex = currRow + 1;
                                this.fpDetail_Sheet.AddSelection(currRow + 1, 0, 1, 1);
                            }
                            else
                            {
                                this.AddRow();
                            }
                        }
                    }
                    break;
                case Keys.Escape:
                    if (this.ucItemList.Visible)
                    {
                        this.ucItemList.Visible = false;
                    }
                    if (this.lbDept.Visible)
                    {
                        this.lbDept.Visible = false;
                    }
                    break;
                #endregion
                case Keys.F2:
                    if (this.fpDetail.ContainsFocus && this.ucItemList.Visible)
                    {
                        this.ucItemList.SetCurrentRow(1);
                        this.ProcessItem();
                    }
                    break;
                case Keys.F3:
                    if (this.fpDetail.ContainsFocus && this.ucItemList.Visible)
                    {
                        this.ucItemList.SetCurrentRow(2);
                        this.ProcessItem();
                    }
                    break;
                case Keys.F4:
                    if (this.fpDetail.ContainsFocus && this.ucItemList.Visible)
                    {
                        this.ucItemList.SetCurrentRow(3);
                        this.ProcessItem();
                    }
                    break;
                case Keys.F9:
                    if (!this.ucItemList.Visible)
                    {
                        this.isSubTotal = true;
                        this.SubTotal();
                        this.isSubTotal = false;
                    }
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// �����������۸��ж��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_Change(object sender, ChangeEventArgs e)
        {
            if (this.isSubTotal)
            {
                //����С�Ʒ���
                return;
            }
            switch (e.Column)
            {
                case (int)Columns.Price://�жϼ۸�Ϸ���
                    this.SetItemProperty();
                    break;
                case (int)Columns.Qty://�ж������Ϸ���
                    this.SetItemProperty();
                    break;
                case (int)Columns.Day:
                    {
                        this.SetItemProperty();
                        break;
                    }
                case (int)Columns.feeRate:
                    {
                        this.SetItemProperty();
                        break;
                    }
                    break;
            }
        }

        /// <summary>
        /// ��Ŀѡ���¼�
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int ucItemList_SelectItem(Keys key)
        {
            this.ProcessItem();
            return 0;
        }

        /// <summary>
        /// ��ʼ�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_EditModeOn(object sender, EventArgs e)
        {
            this.fpDetail.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            this.SetLocation();
            if (this.fpDetail_Sheet.ActiveColumnIndex != (int)Columns.Dept)
            {
                this.lbDept.Visible = false;
            }
            if (fpDetail_Sheet.ActiveColumnIndex != (int)Columns.ItemName
                && fpDetail_Sheet.ActiveColumnIndex != (int)Columns.GbCode)
            {
                this.ucItemList.Visible = false;
            }
            if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.Dept)
            {
                string deptCode = string.Empty;
                if ("N,I".Contains(SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(((FS.HISFC.Models.Base.Employee)this.itemManager.Operator).Dept.ID).DeptType.ID.ToString()))
                {
                    if (this.patientInfo != null)
                    {
                        deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                    else
                    {
                        deptCode = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.recipeDoctCode).Dept.ID;
                    }
                }
                else
                {
                    deptCode = this.operObj.Dept.ID;
                }
                if (this.fpDetail_Sheet.ActiveRow.Tag != null && this.fpDetail_Sheet.ActiveRow.Tag is Item)
                {
                    Item item = this.fpDetail_Sheet.ActiveRow.Tag as Item;
                    if (item.ItemType == EnumItemType.UnDrug)
                    {
                        ArrayList alExecDept = null;

                        string defaultExecDept = string.Empty;

                        lbDept.Items.Clear();

                        SOC.HISFC.BizProcess.Cache.Common.SetExecDept(false, deptCode, item.ID, ref defaultExecDept, ref alExecDept);

                        lbDept.AddItems(alExecDept);
                    }
                }
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName)
            {
                switch (e.KeyCode)
                {
                    #region F1~F10���ѡ����Ŀ,F2~F4Ϊfarpoint�ڲ�������processdialogkey����
                    case Keys.F1:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(0);
                            ProcessItem();
                        }
                        break;
                    case Keys.F5:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(4);
                            ProcessItem();
                        }
                        break;
                    case Keys.F6:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(5);
                            ProcessItem();
                        }
                        break;
                    case Keys.F7:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(6);
                            ProcessItem();
                        }
                        break;
                    case Keys.F8:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(7);
                            ProcessItem();
                        }
                        break;
                    case Keys.F9:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(8);
                            ProcessItem();
                        }
                        break;
                    case Keys.F10:
                        if (this.ucItemList.Visible)
                        {
                            this.ucItemList.SetCurrentRow(9);
                            ProcessItem();
                        }
                        break;
                    #endregion
                    case Keys.F11://�л����뷨
                        if (this.ucItemList.Visible)
                            this.ucItemList.ChangeQueryType();
                        break;
                    case Keys.PageDown://��һҳ
                        if (this.ucItemList.Visible)
                            this.ucItemList.NextPage();
                        break;
                    case Keys.PageUp://��һҳ
                        if (this.ucItemList.Visible)
                            this.ucItemList.PriorPage();

                        break;
                }
            }
        }

        /// <summary>
        /// ��������ݷ����仯ʱ,��Ҫ������Ŀ�Ĺ������ִ�п��ҵļ�������ʱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_EditChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Row == this.fpDetail_Sheet.RowCount - 1) 
            {
                return;
            }
            
            string text;
            switch (e.Column)
            {
                case (int)Columns.ItemName://��Ŀ����
                case (int)Columns.GbCode:
                    text = this.fpDetail_Sheet.ActiveCell.Text;
                    this.ucItemList.Filter(text);
                    if (!this.ucItemList.Visible)
                    {
                        this.ucItemList.Visible = true;
                        this.ucItemList.BringToFront();
                        this.ucItemList.Show();
                    }
                    //��յ�ǰ�б���
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Price, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Qty, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Unit, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.TotCost, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Dept, string.Empty, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.ItemObject, null, false);
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.IsNew, string.Empty, false);
                    break;
                case (int)Columns.Dept://����ִ�п���			
                    text = this.fpDetail_Sheet.ActiveCell.Text;
                    this.lbDept.Filter(text);
                    //��¼ִ�п����Ѿ��޸ģ�Ҫ���¸�ֵ
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.IsDeptChange, "1", false);
                    object obj = this.fpDetail_Sheet.GetValue(e.Row, (int)Columns.ItemObject);
                    if (obj != null)//һ�����ҷ����仯�����ʵ����ִ�п���
                    {
                        FeeItemList f = obj as FeeItemList;
                        f.ExecOper.Dept.ID = string.Empty;
                        f.ExecOper.Dept.Name = string.Empty;
                        this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.ItemObject, f, false);
                    }
                    if (!lbDept.Visible)
                    {
                        this.lbDept.Visible = true;
                    }
                    break;
                case (int)Columns.Qty://��¼�޸ĵ�����
                    string isNew = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.IsNew);
                    if (isNew == "0")
                    {
                        this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.IsNew, "2", false);
                    }
                    break;
            }
        }

        /// <summary>
        /// ѡ���װ��λ����С��λʱ�򴥷�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_ComboSelChange(object sender, EditorNotifyEventArgs e)
        {
            if (e.Column == (int)Columns.Unit)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = (FarPoint.Win.Spread.CellType.ComboBoxCellType)this.fpDetail_Sheet.Cells[e.Row, e.Column].CellType;

                string text = e.EditingControl.Text;
                if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 0)
                {
                    //����С��λ�շ�
                    object obj = fpDetail_Sheet.GetValue(e.Row, (int)Columns.ItemObject);
                    if (obj == null)
                    {
                        return;
                    }
                    decimal price = FS.FrameWork.Public.String.FormatNumber(
                        (obj as FeeItemList).Item.Price /
                        (obj as FeeItemList).Item.PackQty, 4);

                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Price, price, false);
                    //�����ܶ�
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Qty);//����
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal qty = NConvert.ToDecimal(text);
                    //����
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Day);
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal day = NConvert.ToDecimal(text);

                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.TotCost, price * qty * day, false);
                }
                else if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 1)
                {
                    //����װ��λ�շ�
                    object obj = fpDetail_Sheet.GetValue(e.Row, (int)Columns.ItemObject);
                    if (obj == null)
                    {
                        return;
                    }
                    decimal price = (obj as FeeItemList).Item.Price;
                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.Price, price, false);
                    //�����ܶ�
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Qty);//����
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal qty = NConvert.ToDecimal(text);
                    //����
                    text = this.fpDetail_Sheet.GetText(e.Row, (int)Columns.Day);
                    if (text == string.Empty)
                    {
                        text = "0";
                    }
                    decimal day = NConvert.ToDecimal(text);

                    this.fpDetail_Sheet.SetValue(e.Row, (int)Columns.TotCost, price * qty * day, false);
                }
            }
        }


        #endregion

        #region ö��

        /// <summary>
        /// �շ�����
        /// </summary>
        public enum FeeTypes 
        {
            /// <summary>
            /// ����
            /// </summary>
            ���� = 0,

            /// <summary>
            /// �շ�
            /// </summary>
            �շ�,

            /// <summary>
            /// �ն�ȷ��
            /// </summary>
            �ն�ȷ��,

            /// <summary>
            /// ����շ�
            /// </summary>
            ����շ�,
        }

        /// <summary>
        /// ��Ŀ���ö��
        /// </summary>
        public enum ItemKind
        {
            /// <summary>
            /// ҩƷ
            /// </summary>
            Drug,

            /// <summary>
            /// ��ҩƷ
            /// </summary>
            Undrug,

            /// <summary>
            /// ȫ����ҩƷ�ͷ�ҩƷ
            /// </summary>
            All
        }

        /// <summary>
        /// ö���У�������ʱ��Ӧ������ö��
        /// </summary>
        public enum Columns
        {
            /// <summary>
            /// ���ұ���
            /// </summary>
            GbCode,

            /// <summary>
            /// ��Ŀ����
            /// </summary>
            ItemName,

            /// <summary>
            /// �۸�
            /// </summary>
            Price,

            /// <summary>
            /// ����
            /// </summary>
            Qty,

            /// <summary>
            /// ����
            /// </summary>
            Day,

            /// <summary>
            /// ��λ
            /// </summary>
            Unit,

            /// <summary>
            /// �ܶ�
            /// </summary>
            TotCost,

            /// <summary>
            /// ִ�п���
            /// </summary>
            Dept,

            /// <summary>
            /// ��Ŀ�Ķ���,Item Instance
            /// </summary>
            ItemObject,

            /// <summary>
            /// �Ƿ�������Ŀ,0ԭ��(���ݿ���),1����,2�޸�
            /// </summary>
            IsNew,

            /// <summary>
            /// ִ�п����Ƿ��޸�0,�� 1��
            /// </summary>
            IsDeptChange,

            /// <summary>
            /// �շ�ҩƷ��1��0��
            /// </summary>
            IsDrug,

            /// <summary>
            /// �շѱ���{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
            /// </summary>
            feeRate ,

            /// <summary>
            /// ����
            /// </summary>
            BatchNo,
            /// <summary>
            /// �Ʒ�����
            /// </summary>
            feeDate
        }

        #endregion

        private void fpDetail_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        /// <summary>
        /// ˫��ʱɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDetail_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            this.DelRow();
        }

        private void ucInpatientCharge_Load(object sender, EventArgs e)
        {
            if (!FS.HISFC.Components.Common.Classes.Function.DesignMode)
            {
                operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
                //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
                IAdptIllnessInPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)) as HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient;
            }
        }

        #region IInterfaceContainer ��Ա
        //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)
                };
            }
        }

        #endregion
    }

    class CompareByID : IComparer
    {

        #region IComparer ��Ա

        public int Compare(object x, object y)
        {
            return FS.FrameWork.Function.NConvert.ToInt32(x).CompareTo(FS.FrameWork.Function.NConvert.ToInt32(y));
        }

        #endregion
    }
}

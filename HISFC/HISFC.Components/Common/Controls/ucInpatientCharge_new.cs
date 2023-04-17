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
using FS.FrameWork;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucInpatientCharge_new : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInpatientCharge_new()
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
        /// ҩƷ������
        /// </summary>
        protected FS.HISFC.BizLogic.Pharmacy.Item phaItemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

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

        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

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

        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        ArrayList unDrugDeptList = new ArrayList();
        Dictionary<string, FS.FrameWork.Models.NeuObject> dictDept = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// ���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ
        /// </summary>
        EnumDrugStorageDept drugStorageDept = EnumDrugStorageDept.CurrentLoginDept;

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
        [Category("�ؼ�����"), Description("�Ƿ��ж�Ƿ��,Y���ж�Ƿ�ѣ�����������շ�,M���ж�Ƿ�ѣ���ʾ�Ƿ�����շ�,N�����ж�Ƿ��")]
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
        //{7F5CF034-5DCD-4d10-B2FD-CDD02F45E58D}

        public bool IsShowGroupTree
        {
            get { return this.neuPanel1.Visible; }
            set { this.neuPanel1.Visible = value; }
        }

        [Category("�ؼ�����"), Description("���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ")]
        public EnumDrugStorageDept DrugStorageDept
        {
            get
            {
                return this.drugStorageDept;
            }
            set
            {
                this.drugStorageDept = value;
            }
        }

        #endregion

        #region ˽�з���
        //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
        /// <summary>
        /// ����Ŀ�б�����Ŀ��ӵ������б�
        /// </summary>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <param name="execDeptCode"></param>
        /// <param name="isFeegroup">�Ƿ��������</param>
        /// <returns></returns>
        protected virtual int AddChargeDetail(FS.HISFC.Models.Base.Item item, int row, string execDeptCode, bool isFeegroup)
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

            //{1128E694-C90B-4df1-A4AF-BB758A9F1C01} lvxl 2010-3-4
            this.fpDetail_Sheet.SetValue(row, (int)Columns.InpatientNO, patientInfo.PID.CaseNO, false);
            this.fpDetail_Sheet.SetValue(row, (int)Columns.InpatientName, patientInfo.Name, false);
            this.fpDetail_Sheet.Cells[row, (int)Columns.InpatientNO].Tag = patientInfo;
            //{1128E694-C90B-4df1-A4AF-BB758A9F1C01}


            //ҩƷĬ�ϰ���С��λ�շ�,��ʾ�۸�ҲΪ��С��λ�۸�,�������ݿ��Ϊ��װ��λ�۸�
            //if (item.IsPharmacy)//ҩƷ
            if (item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                if (item.SpecialFlag4 == "2") //��װ��λ
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price, 4);
                }
                else //��С��λ
                {
                    price = FS.FrameWork.Public.String.FormatNumber(item.Price / item.PackQty, 4);
                }

                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, price, false);
            }
            else//��ҩƷ
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.Price, item.Price, false);

                price = item.Price;
            }

            //�洢��Ŀʵ����ڼ۸�ȼ���{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
            this.fpDetail_Sheet.Rows[row].Tag = item;

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
            if (item.ItemType == EnumItemType.Drug)
            {
                FarPoint.Win.Spread.CellType.ComboBoxCellType comboType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                comboType.Editable = true;
                comboType.Items = (new string[]{(item as FS.HISFC.Models.Pharmacy.Item).MinUnit,
                                                (item as FS.HISFC.Models.Pharmacy.Item).PackUnit});
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].CellType = comboType;
                this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Locked = false;

                if (item.SpecialFlag4 == "2") //��װ��λ
                {
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.Unit, ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit, false);
                    item.PriceUnit = ((FS.HISFC.Models.Pharmacy.Item)item).PackUnit;
                }
                else
                {
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
            if (item.ItemType == EnumItemType.Drug)
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "1", false);
            }
            else
            {
                this.fpDetail_Sheet.SetValue(row, (int)Columns.IsDrug, "0", false);
            }

            string deptCode = string.Empty, deptName = string.Empty;
            if (!defaultExeDeptIsDeptIn)
            {
                #region ���λ�ȡĬ��ִ�п���
                ////��ȡ��ĿĬ��ִ�п���
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
                #endregion

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
            //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
            this.fpDetail_Sheet.Cells[row, (int)Columns.ItemName].Tag = isFeegroup;
            this.fpDetail_Sheet.Cells[row, (int)Columns.Unit].Tag = item.SysClass.User01; //�������ױ���
            if (isFeegroup)
            {
                if (!string.IsNullOrEmpty(item.MinFee.User03))
                {
                    DateTime feeDate = NConvert.ToDateTime(item.MinFee.User03);
                    DateTime now = inpatientManager.GetDateTimeFromSysDateTime();
                    DateTime beginDate = NConvert.ToDateTime(now.ToString("yyyy-MM-dd") + " 00:00:00");
                    DateTime endDate = NConvert.ToDateTime(now.ToString("yyyy-MM-dd") + " 23:59:59");
                    if (feeDate >= beginDate && feeDate <= endDate)
                    {
                        this.fpDetail_Sheet.Rows[row].ForeColor = Color.Red;
                    }

                }
            }

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

                this.fpDetail_Sheet.Rows[row].BackColor = Color.Khaki;

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
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
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

                //if (feeItemList.Item.IsPharmacy)
                if (feeItemList.Item.ItemType == EnumItemType.Drug)
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
                    drugBase.SpecialFlag4 = groupDetail.unitFlag;

                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    if (count > 1)
                    {
                        this.AddRow(row + count - 1);
                    }



                    this.AddChargeDetail(drugBase, row + count - 1, groupDetail.deptCode, false);
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
                    this.AddChargeDetail(undrugBase, row + count - 1, groupDetail.deptCode, false);
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

                    if (this.drugStorageDept == EnumDrugStorageDept.CurrentLoginDept)
                    {
                        drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.operObj.Dept.ID, groupDetail.itemCode);
                    }
                    else if (this.drugStorageDept == EnumDrugStorageDept.PatientInDept)
                    {
                        drugStorate = this.pharmacyIntergrate.GetItemForInpatient(this.patientInfo.PVisit.PatientLocation.Dept.ID, groupDetail.itemCode);
                    }
                    if (drugStorate == null || drugStorate.Item.ID == string.Empty) continue;
                    //��ӵ������б�
                    FS.HISFC.Models.Base.Item drugBase = drugStorate.Item as FS.HISFC.Models.Base.Item;
                    //drugBase.IsPharmacy = true;
                    drugBase.ItemType = EnumItemType.Drug;
                    drugBase.Qty = groupDetail.qty;
                    drugBase.User03 = "1";
                    drugBase.SpecialFlag4 = groupDetail.unitFlag;

                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    this.AddRow(0);
                    this.AddChargeDetail(drugBase, 0, groupDetail.deptCode, false);
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

                    //�����ס����С�������Ŀ����û��ֵ�˱�־���ᵼ���շ�ֱ����ȡ������Ŀ - gumzh
                    if (undrug.UnitFlag == "1")
                    {
                        undrugBase.User01 = "[������]";
                    }

                    #region ֱ���ڵ�һ������������ϸ ,�������㷨�򵥵�
                    this.AddRow(0);
                    this.AddChargeDetail(undrugBase, 0, groupDetail.deptCode, false);
                    #endregion
                }
            }
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

                this.AddChargeDetail(undrug, row + count - 1, execDeptCode, false);
            }

            return 1;
        }

        ////{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
        /// <summary>
        /// ��Ӹ�����Ŀ��ϸ�������б�
        /// </summary>
        ///<param name="feeGroup">��������</param>
        /// <param name="row">��ǰ��</param>
        /// <returns>�ɹ� 1 ʧ��: -1</returns>
        protected virtual int AddCompoundDetail(FeeGroup feeGroup, int row)
        {
            ArrayList details = this.undrugPackageManager.QueryUndrugPackagesBypackageCode(feeGroup.Item.ID);

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

                undrug.User03 = feeGroup.Days == 0 ? "1" : feeGroup.Days.ToString(); //����
                undrug.MinFee.User01 = feeGroup.Item.ID; //��Ŀ����
                undrug.MinFee.User02 = feeGroup.Item.Name;//��Ŀ����
                undrug.MinFee.Memo = feeGroup.ID; //���ױ��
                undrug.MinFee.User03 = feeGroup.FeeDate.ToString();//�շ�����
                this.AddChargeDetail(undrug, row + count - 1, feeGroup.ExecDept.ID, true);
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

            //���������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.inpatientManager.Connection);
            //t.BeginTransaction();
            this.inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.feeIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.personManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.departmentManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.pharmacyIntergrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.undrugManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            ArrayList firstInputFeeItemlist = new ArrayList();
            //���汾���շ���Ŀ��ϸ��Ϣ
            this.feeItemCollection = new List<FeeItemList>();

            //����ʱ��
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            //decimal freeCost = this.patientInfo.FT.LeftCost;//���
            //decimal moneyAlert = this.patientInfo.PVisit.MoneyAlert;//������
            //decimal totCost = 0m;//�ܽ��
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

                //�������¼����Ŀ
                if (isNew)
                {
                    //�ۿ����ȡ��Ŀ�б��е�ҩ������ feeitemlist.item.user02 houwb 2011-5-26
                    //feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;
                    firstInputFeeItemlist.Add(feeItemList.Clone());
                    feeItemCollection.Add(feeItemList.Clone());
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
                    if (this.inpatientManager.FeeAfterCharge((FS.HISFC.Models.RADT.PatientInfo)this.fpDetail_Sheet.Cells[i, (int)Columns.InpatientNO].Tag, feeItemList) == -1)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show(feeItemList.Item.Name + Language.Msg("������Ŀ���շѳ���!") + this.inpatientManager.Err);
                        this.fpDetail.Focus();
                        this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                        return -1;
                    }

                    //ҩƷ���������
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.pharmacyIntergrate.ApplyOut((FS.HISFC.Models.RADT.PatientInfo)this.fpDetail_Sheet.Cells[i, (int)Columns.InpatientNO].Tag, feeItemList, operTime, false) == -1)
                        {
                            feeIntergrate.Rollback();
                            MessageBox.Show(Language.Msg(feeItemList.Item.Name + " ��Ӧ���� ") + this.pharmacyIntergrate.Err);
                            this.fpDetail.Focus();
                            this.fpDetail_Sheet.SetActiveCell(i, (int)Columns.ItemName, false);

                            return -1;
                        }
                    }
                }
                //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
                object obj = this.fpDetail_Sheet.Cells[i, (int)Columns.ItemName].Tag;
                if (obj != null && NConvert.ToBoolean(obj))
                {
                    string feeGroupId = this.fpDetail_Sheet.Cells[i, (int)Columns.Unit].Tag.ToString();
                    if (inpatientManager.UpdateFeeGroupFeeDate(feeGroupId, operTime) <= 0)
                    {
                        feeIntergrate.Rollback();
                        MessageBox.Show("���»��߷��������շ�ʱ��ʧ�ܣ�" + inpatientManager.Err);
                        return -1;
                    }
                }
            }

            //{9A2D3D3B-954B-44e1-B198-18CE2E2A36D7} lvxl 2010-3-4
            string strPatientNO = "";
            for (int ii1 = 0; ii1 < firstInputFeeItemlist.Count; ii1++)
            {
                FeeItemList fil = (FeeItemList)firstInputFeeItemlist[ii1];
                if (!strPatientNO.Contains(fil.ExtFlag2))
                {
                    strPatientNO += fil.ExtFlag2 + "|";
                }
            }
            if (strPatientNO.Length > 0)
            {
                strPatientNO = strPatientNO.Substring(0, strPatientNO.Length - 1);
            }
            string[] alPatientNO = strPatientNO.Split('|');
            ArrayList AlFirstInputFeeItemlist = new ArrayList();
            ArrayList ListFeeItemCollection = new ArrayList();
            ArrayList AlFirstInputFeeItemlistDetail;
            ArrayList ListFeeItemCollectionDetail;
            for (int ii2 = 0; ii2 < alPatientNO.Length; ii2++)
            {
                AlFirstInputFeeItemlistDetail = new ArrayList();
                ListFeeItemCollectionDetail = new ArrayList();
                for (int ii3 = 0; ii3 < firstInputFeeItemlist.Count; ii3++)
                {
                    if (((FeeItemList)firstInputFeeItemlist[ii3]).ExtFlag2 == alPatientNO[ii2].ToString())
                    {
                        AlFirstInputFeeItemlistDetail.Add(firstInputFeeItemlist[ii3]);
                        ListFeeItemCollectionDetail.Add(firstInputFeeItemlist[ii3]);
                    }
                }
                if (AlFirstInputFeeItemlistDetail.Count > 0)
                {
                    AlFirstInputFeeItemlist.Add(AlFirstInputFeeItemlistDetail);
                }
                if (ListFeeItemCollectionDetail.Count > 0)
                {
                    ListFeeItemCollection.Add(ListFeeItemCollectionDetail);
                }
            }

            ArrayList alFeeInfo = new ArrayList();

            for (int ii4 = 0; ii4 < AlFirstInputFeeItemlist.Count; ii4++)
            {
                //FeeItemList fItemList = (FeeItemList)((ArrayList)AlFirstInputFeeItemlistDetail[ii4])[0];
                ArrayList firstInputFeeItemlistTemp = (ArrayList)AlFirstInputFeeItemlist[ii4];
                this.patientInfo = this.radtIntegrate.GetPatientInfomation(((FeeItemList)firstInputFeeItemlistTemp[0]).ExtFlag2);
                //fItemList.ExtFlag2; --------------------------------------------
                //{9A2D3D3B-954B-44e1-B198-18CE2E2A36D7}lvxl

                //{F4912030-EF65-4099-880A-8A1792A3B449} �����ֽ����ϲ���ָ�����Ŀ��������
                if (!this.isSplitUndrugCombItem)
                {

                    SplitUndrugCombItem(ref firstInputFeeItemlistTemp);

                }
                ////{F4912030-EF65-4099-880A-8A1792A3B449}����



                //���������շѺ���,��ȡ��һ��¼��ķ���
                //if (this.feeIntergrate.FeeItem(this.patientInfo, ref firstInputFeeItemlist) == -1)
                if (this.feeIntergrate.FeeItem(this.patientInfo, ref firstInputFeeItemlistTemp) == -1)
                {
                    feeIntergrate.Rollback();
                    MessageBox.Show(this.feeIntergrate.Err);
                    this.feeIntergrate.MedcareInterfaceProxy.Disconnect();
                    this.fpDetail.Focus();

                    return -1;
                }

                alFeeInfo.AddRange(firstInputFeeItemlist);

                //�Ե�һ���շѵ���Ŀ����ҩƷ������Ϣ
                //foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlist)
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList in firstInputFeeItemlistTemp)
                {
                    //ҩƷ���������
                    //if (feeItemList.Item.IsPharmacy)
                    if (feeItemList.Item.ItemType == EnumItemType.Drug)
                    {
                        //�ۿ����ȡ��Ŀ�б��е�ҩ������ feeitemlist.item.user02 houwb 2011-5-26
                        //feeItemList.StockOper.Dept.ID = feeItemList.ExecOper.Dept.ID;

                        if (this.pharmacyIntergrate.ApplyOut(this.patientInfo, feeItemList, operTime, false) == -1)
                        {
                            feeIntergrate.Rollback();
                            MessageBox.Show(Language.Msg(feeItemList.Item.Name + " ��Ӧ���� ") + this.pharmacyIntergrate.Err);
                            this.fpDetail.Focus();

                            return -1;
                        }
                    }
                }
                //{4FF03BBF-763D-4063-A792-A2264999E79A}
                if (IAdptIllnessInPatient != null)
                {
                    //ArrayList feeList = new ArrayList(feeItemCollection);
                    ArrayList feeList = (ArrayList)ListFeeItemCollection[ii4];
                    int resultValue = IAdptIllnessInPatient.SaveInpatientFeeDetail(this.patientInfo, ref feeList);

                    if (resultValue < 0)
                    {
                        feeIntergrate.Rollback();
                        return -1;
                    }
                }
            }

            //������Ϣ
            #region HL7��Ϣ����
            object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Fee), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
            if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
            {
                FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                int param = curIOrderControl.SendFeeInfo(this.patientInfo, alFeeInfo, true);
                if (param == -1)
                {
                    feeIntergrate.Rollback();
                    MessageBox.Show(curIOrderControl.Err);
                    this.fpDetail.Focus();
                    return -1;
                }
            }
            #endregion

            this.feeIntergrate.MedcareInterfaceProxy.CloseAll();
            this.feeIntergrate.Commit();

            this.sucessMsg = "�շѳɹ�!";

            return 1;
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
                if (f.Item.ItemType == EnumItemType.UnDrug && f.Item.User01 == "[������]")
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

                string orderID = this.orderManager.GetNewOrderID();

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

                    //���ʱ ����ȡ�շѼ�
                    if (this.feeIntergrate.GetPriceForInpatient(this.patientInfo, undrug, ref price, ref orgPrice) < 0)
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
                this.fpDetail_Sheet.Cells[count, (int)Columns.TotCost].Formula = "sum(H1:H" + count.ToString() + ")";
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

            if ((IsDeptChange == "0" || IsDeptChange == string.Empty) && this.fpDetail_Sheet.GetText(currRow, (int)Columns.Dept) == string.Empty)
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
                        this.ucItemList.Location = new Point(cell.Location.X+cell.Left , cell.Top - this.ucItemList.Height - 7);
                    }
                    else
                    {
                        this.ucItemList.Location = new Point(cell.Location.X + cell.Left , 2);
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

        /// <summary>
        /// ������Ŀ�б��е���Ŀ��ȡ����Ŀ��Ĭ��ִ�п���
        /// </summary>
        /// <param name="item">��Ŀʵ��</param>
        /// <param name="deptID">���ұ���</param>
        /// <param name="deptName">��������</param>
        /// <returns></returns>
        private int GetItemDept(FS.HISFC.Models.Base.Item item, ref string deptCode, ref string deptName)
        {
            if (item is FS.HISFC.Models.Fee.Item.Undrug)
            {
                //��÷�ҩƷĬ�ϵ�ִ�п���
                deptCode = (item as FS.HISFC.Models.Fee.Item.Undrug).ExecDept;
                if (deptCode == null || deptCode == string.Empty)
                {
                    if (this.defaultExeDept != null && this.defaultExeDept != string.Empty)
                    {
                        deptCode = this.defaultExeDept;
                        FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(this.defaultExeDept);
                        if (dept == null)
                        {
                            deptName = "(��)";
                        }
                        else
                        {
                            deptName = dept.Name;
                        }
                    }
                    else
                    {
                        if (this.patientInfo != null)
                        {
                            deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                            deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                        }
                    }
                }
                else
                {
                    //��ֿ��Ҵ���������ҵĻ���Ĭ��ȡ��һ��
                    int index = deptCode.IndexOf("|");
                    if (index < 0)
                    {
                        index = deptCode.Length;
                    }
                    deptCode = deptCode.Substring(0, index);

                    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(deptCode);
                    if (dept == null)
                    {
                        deptName = "(��)";
                    }
                    else
                    {
                        deptName = dept.Name;
                    }
                }
            }
            else if (item is FS.HISFC.Models.Pharmacy.Item)
            {
                //���ҩƷ��ִ�п���
                if (this.defaultExeDept != null && this.defaultExeDept != string.Empty)
                {
                    deptCode = this.defaultExeDept;
                    FS.HISFC.Models.Base.Department dept = this.departmentManager.GetDeptmentById(this.defaultExeDept);
                    if (dept == null)
                    {
                        deptName = "(��)";
                    }
                    else
                    {
                        deptName = dept.Name;
                    }
                }
                else
                {
                    if (this.patientInfo != null)
                    {
                        deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                        deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                    }
                }
            }
            else if (item is FS.HISFC.Models.FeeStuff.MaterialItem)
            {
                if (tempDept != null)
                {
                    deptName = tempDept.Name;
                    deptCode = tempDept.ID;
                }
                else if (this.patientInfo != null)
                {
                    deptName = this.patientInfo.PVisit.PatientLocation.Dept.Name;
                    deptCode = this.patientInfo.PVisit.PatientLocation.Dept.ID;
                }
                else
                {
                    deptName = this.operObj.Dept.Name;
                    deptCode = this.operObj.Dept.ID;
                }
            }
            return 0;
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
                if (string.IsNullOrEmpty(this.ucItemList.CurFilterStr))
                {
                    return -1;
                }
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
                if (this.fpDetail_Sheet.GetText(currRow, (int)Columns.IsNew) == "0")
                {
                    return -1;
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
                        this.AddChargeDetail(item, currRow, string.Empty, false);
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
                    this.AddChargeDetail(item, currRow, string.Empty, false);
                    this.ucItemList.CurFilterStr = string.Empty;
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

        //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
        /// <summary>
        /// ����this.fpDetail����Ŀ���ƵĻس�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        private int ProcessItem(FeeGroup feeGroup, int currRow)
        {
            try
            {
                Item item = new Item();
                int returnValue = this.ucItemList.GetSelectItem(feeGroup.Item.ID, feeGroup.DrugFlag, out item);

                if (returnValue == -1 || returnValue == 0)
                {

                    return -1;
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
                        //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
                        item.Qty = feeGroup.Item.Qty;
                        item.User03 = feeGroup.Days == 0 ? "1" : feeGroup.Days.ToString();//Ĭ��������Ŀ�ĸ�����Ϊ1
                        item.PriceUnit = feeGroup.Item.PriceUnit;//��λ
                        item.MinFee.Memo = feeGroup.ID;//�������ױ��
                        item.MinFee.User03 = feeGroup.FeeDate.ToString();//�շ�����
                        item.SysClass.User01 = feeGroup.ID;//�������ױ���
                        //��ӻ�����ϸ
                        this.AddChargeDetail(item, currRow, string.Empty, true);
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
                    //{1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
                    item.Qty = feeGroup.Item.Qty;
                    item.User03 = feeGroup.Days == 0 ? "1" : feeGroup.Days.ToString();//Ĭ��������Ŀ�ĸ�����Ϊ1
                    item.PriceUnit = feeGroup.Item.PriceUnit;
                    item.MinFee.Memo = feeGroup.ID;//�������ױ��
                    item.MinFee.User03 = feeGroup.FeeDate.ToString();//�շ�����
                    item.SysClass.User01 = feeGroup.ID;//�������ױ���
                    //��ӻ�����ϸ
                    this.AddChargeDetail(item, currRow, string.Empty, true);
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

            //�۸�
            string text = this.fpDetail_Sheet.GetText(row, (int)Columns.Price);
            if (text == null || text == string.Empty)
            {
                text = "0";
            }
            decimal price = NConvert.ToDecimal(text);

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
            if (item.Price == 0)
            {
                item.Price = NConvert.ToDecimal(this.fpDetail_Sheet.GetText(row, (int)Columns.Price));
            }
            if (item.ItemType == EnumItemType.Drug)//ҩƷ
            {
                if (item.SpecialFlag4 == "2")
                {
                  
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber((item.Price * qty) * day, 2), false);
                }
                else
                {
                  
                    this.fpDetail_Sheet.SetValue(row, (int)Columns.TotCost, FS.FrameWork.Public.String.FormatNumber((item.Price * qty / item.PackQty) * day, 2), false);
                }
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

        FS.HISFC.Models.Pharmacy.Storage storage = null;

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

            //{6036900E-24DA-4dce-A58B-1DE240B19F36} lvxl 2010-3-4
            feeItemList.ExtFlag2 = ((FS.HISFC.Models.RADT.PatientInfo)this.fpDetail_Sheet.Cells[row, (int)Columns.InpatientNO].Tag).ID;

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

                //�ۿ���� houwb 2011-5-26 ȥ��Ŀ�б��е�ҩ������
                feeItemList.StockOper.Dept.ID = feeItemList.Item.User02;

                if (string.IsNullOrEmpty(feeItemList.StockOper.Dept.ID))
                {
                    storage = this.phaItemMgr.GetItemForInpatient(feeItemList.ExecOper.Dept.ID, feeItemList.Item.ID);
                    if (storage == null)
                    {
                        MessageBox.Show(this.phaItemMgr.Err);
                        return -1;
                    }

                    feeItemList.StockOper.Dept.ID = storage.StockDept.ID;
                }
            }
            else if (feeItemList.Item.ItemType == EnumItemType.UnDrug)
            {
                ArrayList undrugList = this.undrugManager.Query(feeItemList.Item.ID, "1");
                if (undrugList == null)
                {
                    MessageBox.Show("��÷�ҩƷ������Ϣʧ��!" + this.undrugManager.Err);

                    return -1;
                }

                if (undrugList.Count == 0)
                {
                    MessageBox.Show(feeItemList.Item.Name + "�Ѿ�ͣ��!������ѡ����Ч����Ŀ");

                    return -1;
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
            //if (feeItemList.Item.IsPharmacy)
            if (feeItemList.Item.ItemType == EnumItemType.Drug)
            {
               
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(
                        //�뻤ʿվ���˷ѵȷ��õļ��㷽������һ��{F98CC89C-BE9A-49ca-98E2-4C700A8F5E34}
                        //feeItemList.Item.Price / feeItemList.Item.PackQty * feeItemList.Item.Qty, 2);
                        feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
                feeItemList.FT.DefTotCost = FS.FrameWork.Public.String.FormatNumber(SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(feeItemList.Item.ID).PriceCollection.RetailPrice * feeItemList.Item.Qty / feeItemList.Item.PackQty, 2);
            }
            else
            {
               
                feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber(
                        feeItemList.Item.Price * feeItemList.Item.Qty, 2);
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
                    feeItemList.FeeOper.OperTime = operTime;
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

        #region ����������{7F5CF034-5DCD-4d10-B2FD-CDD02F45E58D}

        #region ����
        FS.HISFC.BizProcess.Integrate.Manager groupManager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// ���ɿ����շ������б�
        /// </summary>
        /// <returns></returns>
        private int RefreshGroupList()
        {
            this.tvGroup.ImageList = this.tvGroup.ImageList;
            this.tvGroup.Nodes.Clear();

            TreeNode root = new TreeNode();
            root.Text = "�շ�����";
            root.ImageIndex = 0;
            root.SelectedImageIndex = 1;
            tvGroup.Nodes.Add(root);

            this.AddDeptGroup(root, (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).Dept.ID);
            root.Expand();

            return 0;
        }


        /// <summary>
        /// ���ݿ���ID ��ӿ�������
        /// </summary>
        /// <param name="parent">���ڵ�</param>
        /// <param name="deptID">���ұ���</param>
        /// <returns>�ɹ�����1 �����أ�1 �����ݷ���0</returns>
        private int AddDeptGroup(TreeNode parent, string deptID)
        {
            FS.HISFC.BizLogic.Manager.ComGroup groupManager = new FS.HISFC.BizLogic.Manager.ComGroup();
            //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
            //ArrayList al = new ArrayList();
            ////���ݿ��һ����������
            //al = groupManager.GetAllGroupList(deptID);
            //if (al == null || al.Count == 0)
            //    return 0;
            //foreach (FS.HISFC.Models.Fee.ComGroup group in al)
            //{
            //    //�������
            //    this.AddGroup(parent, group);
            //}
            ArrayList al = new ArrayList();
            //���ݿ��һ����������
            al = groupManager.GetAllGroupsByRoot("1");

            ArrayList alDeptList = new ArrayList();

            foreach (FS.HISFC.Models.Fee.ComGroup item in al)
            {
                if (item.deptCode == deptID)
                {
                    alDeptList.Add(item);
                }
            }


            if (alDeptList == null || alDeptList.Count == 0)
                return 0;
            foreach (FS.HISFC.Models.Fee.ComGroup group in alDeptList)
            {
                this.AddGroupsRecursion(parent, group);
            }
            return 1;
        }

        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        private int AddGroupsRecursion(TreeNode parent, FS.HISFC.Models.Fee.ComGroup group)
        {

            ArrayList al = this.groupManager.GetGroupsByDeptParent("1", group.deptCode, group.ID);
            if (al.Count == 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                newNode.ImageIndex = 4;
                newNode.SelectedImageIndex = 5;
                parent.Nodes.Add(newNode);

                return -1;
            }
            else
            {
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                newNode.ImageIndex = 2;
                newNode.SelectedImageIndex = 3;
                parent.Nodes.Add(newNode);
                foreach (FS.HISFC.Models.Fee.ComGroup item in al)
                {
                    //if (item.ID == "aaa")
                    //{
                    //    MessageBox.Show("aaa");
                    //}
                    //TreeNode newNode = new TreeNode();
                    //newNode.Tag = group;
                    //newNode.Text = group.Name;// +"[" + group.ID + "]";
                    //newNode.ImageIndex = 2;
                    //newNode.SelectedImageIndex = 3;
                    //parent.Nodes.Add(newNode);
                    this.AddGroupsRecursion(newNode, item);
                }
            }


            return 1;
        }

        #endregion

        #endregion

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
        /// ��ʼ������
        /// </summary>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        public virtual int Init(string deptCode)
        {
            this.InitFP();
            //this.ucItemList = new ucItemList(this.itemKind);
            this.ucItemList = new ucItemList();
            this.ucItemList.enuShowItemType = this.itemKind;
            this.Controls.Add(this.ucItemList);
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
                if (returnValue != 1)
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

        #region �������� {1E64A9A8-F0CC-449d-B16C-1C8B6D226839}
        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public virtual int SaveFeeGroup()
        {
            Employee employee = this.personManager.GetPersonByID(this.recipeDoctCode);

            if (employee == null)
            {
                MessageBox.Show(Language.Msg("�����Ա������Ϣ����!"));

                return -1;
            }
            this.fpDetail.Change -= new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);

            if (!this.IsValid())
            {
                this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);
                return -1;
            }

            this.fpDetail.Change += new FarPoint.Win.Spread.ChangeEventHandler(this.fpDetail_Change);

            //���������
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (inpatientManager.DeleteFeeGroup(this.patientInfo.ID, (inpatientManager.Operator as Employee).Dept.ID) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ɾ�����߷���������Ϣʧ�ܣ�" + inpatientManager.Err);
                return -1;
            }

            //����ʱ��
            DateTime operTime = this.inpatientManager.GetDateTimeFromSysDateTime();
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
                FeeGroup feeGroup = this.FeeItemListChangeFeeGroup(feeItemList, operTime);
                if (inpatientManager.InsertFeeGroup(feeGroup) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����������ʧ�ܣ�" + inpatientManager.Err);
                    return -1;
                }
                //��Ŀ����tagΪ�Ƿ��շ�������Ŀ��ʶ
                this.fpDetail_Sheet.Cells[i, (int)Columns.ItemName].Tag = true;
                //��λtagΪ�շ�����ID
                fpDetail_Sheet.Cells[i, (int)Columns.Unit].Tag = feeGroup.ID;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("����������׳ɹ���");
            return 1;
        }

        /// <summary>
        /// ������ʵ��ת��Ϊ��������ʵ��
        /// </summary>
        /// <param name="f"></param>
        /// <param name="operTime"></param>
        /// <returns></returns>
        private FeeGroup FeeItemListChangeFeeGroup(FeeItemList f, DateTime operTime)
        {
            FeeGroup fg = new FeeGroup();
            fg.ID = inpatientManager.GetFeeGroupID();
            fg.Patient = this.patientInfo;
            fg.NurseCell.ID = (this.inpatientManager.Operator as Employee).Dept.ID;
            fg.Item = f.Item;
            if (f.Item.ItemType == EnumItemType.Drug)
            {
                fg.DrugFlag = "1";
            }
            if (f.Item.ItemType == EnumItemType.UnDrug)
            {
                fg.DrugFlag = "2";
            }
            if (f.Item.ItemType == EnumItemType.MatItem)
            {
                fg.DrugFlag = "6";
            }
            fg.Package.ID = f.Item.MinFee.User01;
            fg.Package.Name = f.Item.MinFee.User02;
            fg.ExecDept = f.ExecOper.Dept;
            fg.Oper.ID = inpatientManager.Operator.ID;
            fg.Oper.OperTime = operTime;

            return fg;
        }

        /// <summary>
        /// �����������
        /// </summary>
        /// <returns></returns>
        public virtual int ExpFeeGroup()
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(patientInfo.ID))
            {
                MessageBox.Show("�����뻼�ߺ��ٵ������ף�");
                return -1;
            }
            string inpatientNo = this.patientInfo.ID;
            string nurseCode = (inpatientManager.Operator as Employee).Dept.ID;
            ArrayList alFeeGroup = inpatientManager.QueryPatientFeeGroup(inpatientNo, nurseCode);
            if (alFeeGroup == null)
            {
                MessageBox.Show("��ȡ���߷���������Ϣʧ�ܣ�");
                return -1;
            }

            if (alFeeGroup.Count == 0)
            {
                MessageBox.Show("�û��߲������շ����ף�");
                return -1;
            }

            foreach (FeeGroup feeGroup in alFeeGroup)
            {
                if (this.fpDetail_Sheet.Rows[this.fpDetail_Sheet.Rows.Count - 2].Tag != null)
                {
                    this.AddRow();
                }
                if (ProcessItem(feeGroup, this.fpDetail_Sheet.RowCount - 2) < 0)
                {
                    return -1;
                }
            }
            return 1;

        }
        #endregion
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
                        if (this.fpDetail_Sheet.ActiveColumnIndex == (int)Columns.ItemName)
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
                    this.SetItemProperty();
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
            if (fpDetail_Sheet.ActiveColumnIndex != (int)Columns.ItemName)
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
                    text = this.fpDetail_Sheet.ActiveCell.Text;
                    this.ucItemList.Filter(text);
                    if (!this.ucItemList.Visible)
                    {
                        this.ucItemList.Visible = true;
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
            /// סԺ��
            /// </summary>
            InpatientNO,

            /// <summary>
            /// ��������
            /// </summary>
            InpatientName,
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
            IsDrug
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

        private void ucInpatientChargeNew_Load(object sender, EventArgs e)
        {
            if (!FS.HISFC.Components.Common.Classes.Function.DesignMode)
            {
                this.fpDetail_Sheet.ActiveColumnIndex = (int)Columns.ItemName;
                operObj = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Clone();
                //{062CEAA8-16B8-4c25-B4CC-E6B24DE7D331}
                IAdptIllnessInPatient = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient)) as HISFC.BizProcess.Interface.FeeInterface.IAdptIllnessInPatient;
            }

            this.RefreshGroupList();
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
        //{7F5CF034-5DCD-4d10-B2FD-CDD02F45E58D}
        private void tvGroup_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                return;
            }


            FS.HISFC.Models.Fee.ComGroup comGroup = null;
            try
            {
                comGroup = e.Node.Tag as FS.HISFC.Models.Fee.ComGroup;
            }
            catch (Exception)
            {

                return;
            }



            ArrayList groupDetails = new ArrayList();
            //��������id��ȡ������ϸ
            groupDetails = this.groupDetailManager.GetComGroupTailByGroupID(comGroup.ID);

            if (groupDetails == null)
            {
                MessageBox.Show("��ѯ�շ�������ϸ����");
                return;
            }

            if (groupDetails.Count == 0)
            {
                MessageBox.Show("��������û��ά��������ϸ��");
                return;
            }


            this.AddGroupDetail(comGroup.ID);
        }
    }

    /// <summary>
    /// ���������ж�ҩƷ���ʱ�����Ҵ���ȡֵ
    /// </summary>
    public enum EnumDrugStorageDept
    {
        /// <summary>
        /// ����סԺ����
        /// </summary>
        PatientInDept = 0,
        /// <summary>
        /// ��ǰ��¼����
        /// </summary>
        CurrentLoginDept = 1
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// �����סԺҽ��վ��������Ŀ�ؼ�
    /// </summary>
    public partial class ucInputItem : UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucInputItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ˢ���б� ��ʱ��
        /// </summary>
        //private System.Threading.Timer initSIListTimer = null;

        /// <summary>
        /// ���̼߳���ҽ��������Ϣ
        /// </summary>
        //private System.Threading.TimerCallback initSIListCallBack = null;

        #region ��ʼ��

        public ucInputItem(bool showGroup)
        {
            // �õ����� Windows.Forms ���������������ġ�
            InitializeComponent();
            if (!DesignMode)
            {
                this.isShowGroup = showGroup;
            }
        }

        public ucInputItem(bool isListShowAlway, bool showGroup)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.bIsListShowAlways = isListShowAlway;
                this.isShowGroup = showGroup;
            }
        }

        #endregion

        #region ҵ������

        /// <summary>
        /// ѡ����Ŀ
        /// </summary>
        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler SelectedItem;//���ڷ���ȡ������Ŀ��Ϣ

        /// <summary>
        /// ���׹���
        /// </summary>
        private FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

        /// <summary>
        /// ���Ʋ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam contrIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �Ƴ��ù�����
        /// </summary>
        private FS.HISFC.BizLogic.Manager.DeptItem deptItemManager = new FS.HISFC.BizLogic.Manager.DeptItem();

        /// <summary>
        /// ���ϵĹ�����
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �����б�
        /// </summary>
        List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt = null;

        /// <summary>
        /// ���仯
        /// </summary>
        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler CatagoryChanged; //��Ŀ���仯

        /// <summary>
        /// ���ص���Ŀ��Ϣ
        /// </summary>
        protected FS.FrameWork.Models.NeuObject myFeeItem = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ͬ��λ��Ϣ
        /// </summary>
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitInfoBizLogic = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// ���뷨������ʾ��Ϣ��0:ƴ���� 1:����� 2:�Զ�����
        /// </summary>
        protected string QueryType = string.Empty;

        /// <summary>
        /// ���뷨���� 0 ƴ�� 1 ��� 2 �Զ�����
        /// </summary>
        private int intQueryType = 0;

        /// <summary>
        /// ������ʾ�������С
        /// </summary>
        private Font fontSize = null;

        /// <summary>
        /// ������ʾ�������С
        /// </summary>
        public Font FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                fontSize = value;
                this.lblCategory.Font = fontSize;
                this.cmbCategory.Font = fontSize;
                this.neuLabel1.Font = fontSize;
                this.lblItemName.Font = fontSize;
                this.txtNote.Font = fontSize;
                this.txtItemCode.Font = fontSize;
                this.txtItemName.Font = fontSize;
            }
        }

        /// <summary>
        /// �Ա����
        /// </summary>
        public const string SelfMark = "[�Ա�]";

        /// <summary>
        /// ��ʾ���б���
        /// </summary>
        protected Forms.frmShowItem frmShowItem = new Forms.frmShowItem();

        /// <summary>
        /// ��ǰFP
        /// </summary>
        public FS.FrameWork.WinForms.Controls.NeuSpread fpItemList = new FS.FrameWork.WinForms.Controls.NeuSpread();

        /// <summary>
        /// ��ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ�� (Ĭ��ΪA)
        /// </summary>
        private string drugSendType = "A";

        /// <summary>
        /// ��Ŀ������
        /// </summary>
        //[Obsolete("����", true)]
        private FS.FrameWork.Public.ObjectHelper itemHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// ��ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ�� (Ĭ��ΪA)
        /// </summary>
        public string DrugSendType
        {
            get
            {
                return drugSendType;
            }
            set
            {
                drugSendType = value;
            }
        }

        #region ���ҳ�����Ŀ

        /// <summary>
        /// ���ҳ�����Ŀ�ַ���
        /// </summary>
        string destItemId = "";

        /// <summary>
        /// ���ҳ�����Ŀ
        /// </summary>
        public ArrayList arrDeptUsed = new ArrayList();

        #endregion

        /// <summary>
        /// ��Ŀ���
        /// </summary>
        private ArrayList arrItemTypes = new ArrayList();

        /// <summary>
        /// ��ǰ��Ŀdataset
        /// </summary>
        private System.Data.DataSet MyDataSet
        {
            get
            {
                if (longOrder)
                {
                    return this.longDataSet;
                }
                else
                {
                    return this.shortDataSet;
                }
            }
        }

        /// <summary>
        /// �����б�
        /// </summary>
        private System.Data.DataSet longDataSet = null;

        /// <summary>
        /// �����б�
        /// </summary>
        private System.Data.DataSet shortDataSet = null;

        private System.Data.DataSet myDeptDataSet = null;

        /// <summary>
        /// �Ƿ���ʾ����б�
        /// </summary>
        private bool bIsShowCategory = true;

        /// <summary>
        /// ����
        /// </summary>
        protected Thread myThread = null;

        /// <summary>
        /// ������ʾ��������ʾ���ظ��ӷ���
        /// </summary>
        private bool isFristShow = true;

        /// <summary>
        /// ��ǰ��½��Ա
        /// </summary>
        protected FS.FrameWork.Models.NeuObject oper = null;

        /// <summary>
        /// ��ȡ��Ŀҽ����ǽӿ� 
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade iGetSiFlag = null;

        /// <summary>
        /// ��ȡ��Ŀҽ����ǽӿ� 
        /// </summary>
        protected FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiFlag
        {
            get
            {
                if (this.iGetSiFlag == null)
                {
                    this.iGetSiFlag = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade)) as FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade;
                }

                return this.iGetSiFlag;
            }
        }

        #region ��Ŀ�б�

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private ArrayList alLongItem = null;

        /// <summary>
        /// ������Ŀ�б�
        /// </summary>
        private ArrayList alShortItem = null;

        /// <summary>
        /// ��Ŀ�б�
        /// </summary>
        public ArrayList alItem = null;

        #endregion

        /// <summary>
        /// ������ҩƷ��Ŀ�Ƿ��������������
        /// </summary>
        bool isFilteUndrugRecipeDept = false;

        /// <summary>
        /// ҽ�������б��Ƿ���ʾ��顢�������ϸ��Ŀ
        /// 1111:�ֱ��ʾ�����顢������顢סԺ��顢סԺ����
        /// </summary>
        string isShowDetailItem;

        /// <summary>
        /// ��ǰ������Ϣ 
        /// </summary>
        private FS.HISFC.Models.RADT.Patient patient = null;

        /// <summary>
        /// ��ǰ������Ϣ 
        /// </summary>
        public FS.HISFC.Models.RADT.Patient Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;

                if (patient != null 
                    && patient.Pact != null 
                    && !string.IsNullOrEmpty(patient.Pact.ID))
                {
                    patient.Pact = SOC.HISFC.BizProcess.Cache.Fee.GetPactUnitInfo(patient.Pact.ID);
                }
            }
        }


        #region {9A40A1FE-C527-4f86-B6F5-E7F52FDD28C9}

        /// <summary>
        /// ��ϸ�б�
        /// �շ���Ŀ��ʾӦ��
        /// </summary>
        public FS.FrameWork.WinForms.Controls.NeuSpread fpItemDetal = new FS.FrameWork.WinForms.Controls.NeuSpread();

        /// <summary>
        /// ��ϸ�б�
        /// �շ���Ŀע������
        /// </summary>
        public FS.FrameWork.WinForms.Controls.NeuPanel PanelItemMemo = new FS.FrameWork.WinForms.Controls.NeuPanel();

        /// <summary>
        /// ע������label
        /// </summary>
        FS.FrameWork.WinForms.Controls.NeuLabel lblMemo = new FS.FrameWork.WinForms.Controls.NeuLabel();


        /// <summary>
        /// ��ϸ��Ϣ�ı�
        /// </summary>
        public System.Windows.Forms.TextBox txtNote = new TextBox();

        /// <summary>
        /// ��¼����
        /// </summary>
        private FS.FrameWork.Models.NeuObject deptLogin = null;

        /// <summary>
        /// ҽ��վ��Ŀѡ���б��Ƿ���ʾҩ������������{ECAE27F0-CC52-46be-A8C5-BC9F680988CD}
        /// </summary>
        private bool isShowCmbDrugDept = false;

        #endregion

        #endregion

        #region ����

        //{D5DF4996-A960-471a-9B38-5B583C8D0B2D}
        /// <summary>
        /// �ж��Ƿ�������ά���ײ͵�����
        /// </summary>
        public bool IsPackageInput = false;

        /// <summary>
        /// ��ǰ��Ŀ
        /// </summary>
        public FS.FrameWork.Models.NeuObject FeeItem
        {
            get
            {
                if (this.myFeeItem == null)
                {
                    this.myFeeItem = new FS.FrameWork.Models.NeuObject();
                }
                return this.myFeeItem;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                this.myFeeItem = value;
                this.txtItemName.Text = this.myFeeItem.Name;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        private bool isShowGroup = false;

        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        protected bool IsShowGroup
        {
            get
            {
                return isShowGroup;
            }
            set
            {
                isShowGroup = value;
            }
        }

        /// <summary>
        /// ��ʾ������У���Ŀ���ϵͳ���
        /// </summary>
        private EnumCategoryType eShowCategory = 0;

        /// <summary>
        /// ��ʾ������У���Ŀ���ϵͳ���
        /// </summary>
        public EnumCategoryType ShowCategory
        {
            get
            {
                return this.eShowCategory;
            }
            set
            {
                this.eShowCategory = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����б�
        /// </summary>
        public bool IsShowCategory
        {
            get
            {

                return this.bIsShowCategory;
            }
            set
            {
                this.panel2.Visible = value;

                this.bIsShowCategory = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        protected bool bIsShowInput = true;

        /// <summary>
        /// �Ƿ���ʾ������
        /// </summary>
        public bool IsShowInput
        {
            get
            {
                return this.bIsShowInput;
            }
            set
            {
                this.bIsShowInput = value;
                this.txtItemCode.Visible = value;
                this.txtItemName.ReadOnly = value;
                if (value)
                {
                    //this.txtItemName.Left = 103;
                    this.txtItemName.BackColor = Color.LightSteelBlue;
                }
                else
                {
                    //this.txtItemName.Left = 4;
                    this.txtItemName.BackColor = Color.White;
                }

            }
        }

        /// <summary>
        /// �Ƿ������������
        /// </summary>
        public bool IsCanInputName
        {
            set
            {
                this.txtItemName.ReadOnly = value;
                if (value)
                {
                    this.txtItemName.BackColor = Color.LightSteelBlue;
                }
                else
                {
                    this.txtItemName.BackColor = Color.White;
                }
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����������
        /// </summary>
        public bool IsCategoryDropDownList
        {
            set
            {
                if (value)
                {
                    this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
                }
                else
                {
                    this.cmbCategory.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�Ա�ҩ����
        /// </summary>
        protected bool bIsShowSelfMark = true;

        /// <summary>
        /// �Ƿ���ʾ�Ա�ҩ����
        /// </summary>
        public bool IsShowSelfMark
        {
            get
            {
                return this.bIsShowSelfMark;
            }
            set
            {
                this.bIsShowSelfMark = value;
            }
        }

        /// <summary>
        /// �Ƿ�һֱ��ʾ�б�
        /// </summary>
        protected bool bIsListShowAlways = false;
        /// <summary>
        /// �Ƿ�һֱ��ʾ�б�
        /// </summary>
        public bool IsListShowAlways
        {
            get
            {
                return this.bIsListShowAlways;
            }
            set
            {
                this.bIsListShowAlways = value;
                if (value)
                {
                    fpItemList.Dock = DockStyle.Fill;
                    fpItemList.Visible = true;
                    this.panel4.Controls.Add(fpItemList);

                    //������ǵ���ѡ�񣬾�Ĭ�ϲ���ѡ���ҳ�����Ŀ
                    this.frmShowItem.chkDeptItem.Checked = false;
                    this.isDeptUsedFlag = false;
                }
            }

        }

        /// <summary>
        /// �Ƿ�ʹ������
        /// </summary>
        private bool isUserRetailPrice2 = false;

        /// <summary>
        /// ��ʾҩƷ����ҩƷ��ȫ��
        /// </summary>
        protected EnumShowItemType eShowItemType = EnumShowItemType.All;

        /// <summary>
        /// ��ʾҩƷ����ҩƷ��ȫ��
        /// </summary>
        public EnumShowItemType ShowItemType
        {
            get
            {
                return this.eShowItemType;
            }
            set
            {
                this.eShowItemType = value;
            }
        }

        /// <summary>
        /// ���þ۽�
        /// </summary>
        public new void Focus()
        {
            this.txtItemCode.Focus();
        }

        /// <summary>
        /// ��ǰ���
        /// </summary>
        public ArrayList AlCatagory
        {
            get
            {
                return this.arrItemTypes;
            }
            set
            {
                if (value == null)
                {
                    return;
                }

                ArrayList al = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject obj in value)
                {
                    if (obj.ID != "PCC")
                    {
                        al.Add(obj);
                    }
                }

                this.cmbCategory.AddItems(al);
                this.cmbCategory.Text = FS.FrameWork.Management.Language.Msg("ȫ��");
            }
        }

        /// <summary>
        /// �Ƿ���
        /// </summary>
        private bool longOrder = false;

        /// <summary>
        /// �Ƿ���
        /// </summary>
        public bool LongOrder
        {
            set
            {
                if (value)
                {
                    this.alItem = alLongItem;
                }
                else
                {
                    this.alItem = alShortItem;
                }
                longOrder = value;
            }
        }

        /// <summary>
        /// ��ȡ�б�Ŀ��ұ��루������ȡҩƷ�б�
        /// </summary>
        protected string deptcode = string.Empty;

        /// <summary>
        /// ��ȡ�б�Ŀ��ұ��루������ȡҩƷ�б�
        /// </summary>
        public string DeptCode
        {
            set
            {
                this.deptcode = value;
                if (string.IsNullOrEmpty(deptcode))
                {
                    deptcode = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
                }
            }
        }

        //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
        /// <summary>
        /// �Ƿ����������Ŀ
        /// </summary>
        private bool isIncludeMat = false;

        public bool IsIncludeMat
        {
            get
            {
                return isIncludeMat;
            }
            set
            {
                isIncludeMat = value;
            }
        }

        /// <summary>
        /// ��ǰ�����߳�
        /// </summary>
        public ThreadState WorkThreadState
        {
            get
            {
                return this.myThread.ThreadState;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�������� (��ʿ����/��������)
        /// </summary>
        protected bool bShowDeptGroup = false;

        /// <summary>
        /// �Ƿ���ʾ�������� (��ʿ����/��������)
        /// </summary>
        public bool IsShowDeptGroup
        {
            set
            {
                this.bShowDeptGroup = value;
            }
        }

        /// <summary>
        /// �Ƿ���ҳ�����Ŀ���
        /// </summary>
        private bool isDeptUsedFlag = false;

        /// <summary>
        /// �Ƿ���ҳ�����Ŀ���
        /// </summary>
        public bool IsDeptUsedFlag
        {
            get
            {
                return isDeptUsedFlag;
            }
            set
            {
                isDeptUsedFlag = value;
            }
        }

        /// <summary>
        /// �Ƿ��Ǻ���ҽ�Ʒ�Χ��ҩ
        /// </summary>
        private bool isCompanyRang = false;

        /// <summary>
        /// ҽ��������Ŀ
        /// </summary>
        FS.HISFC.Models.SIInterface.Compare compareItem = null;

        /// <summary>
        /// ��ҩƷ����:���סԺ��ȫ��
        /// </summary>
        protected EnumUndrugApplicabilityarea eUndrugApplicabilityarea = EnumUndrugApplicabilityarea.All;

        /// <summary>
        /// ��ҩƷ����:���סԺ��ȫ��
        /// </summary>
        public EnumUndrugApplicabilityarea UndrugApplicabilityarea
        {
            get
            {
                return this.eUndrugApplicabilityarea;
            }
            set
            {
                this.eUndrugApplicabilityarea = value;
            }
        }

        /// <summary>
        /// �洢��ͬ��λ���еĶ�Ӧ
        /// </summary>
        private Hashtable hsPactColumn = null;

        /// <summary>
        /// �洢��ͬ��λ��Ӧ��ҽ�������ѣ���Ŀ
        /// </summary>
        private Hashtable hsItemPactInfo = null;

        /// <summary>
        /// ��ǰ��ͬ��λ��Ϣ
        /// </summary>
        [Obsolete("�����ˣ���Patient.Pact����", true)]
        private FS.HISFC.Models.Base.PactInfo pactInfo = null;

        /// <summary>
        /// ��ǰ��ͬ��λ��Ϣ
        /// </summary>
        [Obsolete("�����ˣ���Patient.Pact����", true)]
        public FS.HISFC.Models.Base.PactInfo PactInfo
        {
            get
            {
                return pactInfo;
            }
            set
            {
                pactInfo = value;

                if (isUserThread == 3)
                {
                    string pactCode = "all";
                    if (pactInfo == null 
                        || string.IsNullOrEmpty(pactInfo.ID)
                        || !hsPactColumn.ContainsKey(pactInfo.ID))
                    {
                        pactCode = "all";
                    }
                    else
                    {
                        pactCode = pactInfo.ID;
                    }

                    for (int i = 0; i < fpItemList.Sheets[0].ColumnCount; i++)
                    {
                        if (fpItemList.Sheets[0].Columns[i].Label == "���")
                        {
                            if (!hsPactColumn.Contains(pactCode))
                            {
                                fpItemList.Sheets[0].Columns[i].Visible = false;
                            }
                            else
                            {
                                if (i == (Int32)hsPactColumn[pactCode])
                                {
                                    fpItemList.Sheets[0].Columns[i].Visible = true;
                                }
                                else
                                {
                                    fpItemList.Sheets[0].Columns[i].Visible = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region ����

        #region ��ʼ��

        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void Init()
        {
            if (DesignMode)
            {
                return;
            }
            try
            {
                this.oper = new FS.FrameWork.Models.NeuObject();
                this.oper = this.deptItemManager.Operator as FS.FrameWork.Models.NeuObject;

                //{2A5608D8-26AD-47d7-82CC-81375722FF72}
                isFilteUndrugRecipeDept = contrIntegrate.GetControlParam<bool>("201026", false, true);

                isShowDetailItem = contrIntegrate.GetControlParam<string>("HN0003", false, "1111");

                isUserThread = contrIntegrate.GetControlParam<int>("HNMZ35", true, 0);

                isUserRetailPrice2 = contrIntegrate.GetControlParam<bool>("HNPHA2", false, false);


                this.frmShowItem.IsUserThread = isUserThread;

                if (this.fpItemList.Sheets.Count <= 0)
                {
                    this.fpItemList.Sheets.Add(new FarPoint.Win.Spread.SheetView());
                }

                AddCategory();//������
                this.InputType = 0;//Ĭ��ƴ��

                #region ���ҳ�����Ŀ

                //��ʼ���Ƴ�����Ŀ
                if (arrDeptUsed == null)
                {
                    arrDeptUsed = new ArrayList();
                }
                if (arrDeptUsed.Count <= 0 && this.isDeptUsedFlag)
                {
                    this.arrDeptUsed = deptItemManager.QueryItemByDeptID(string.IsNullOrEmpty(this.deptcode) ? ((FS.HISFC.Models.Base.Employee)this.oper).Dept.ID : this.deptcode);
                    if (arrDeptUsed != null && arrDeptUsed.Count > 0)
                    {
                        int i = 0;
                        StringBuilder sb = new StringBuilder();
                        foreach (FS.FrameWork.Models.NeuObject icd in arrDeptUsed)
                        {
                            string temStr = "'";
                            string splitStr = ",";
                            sb.Append(temStr + icd.ID.Trim() + temStr + splitStr);
                            i++;
                        }
                        destItemId = sb.ToString().Substring(0, sb.ToString().Length - 1);
                    }
                }

                #endregion

                //�����б�
                try
                {
                    if (this.bIsListShowAlways)
                    {
                        this.AddItem();
                    }
                    else
                    {
                        #region ���߳�������{B8FFCAB8-A9FF-43b2-96E2-2DF17B7F3A91}
                        //ThreadStart myThreadDelegate = new ThreadStart(this.AddItem);
                        //myThread = new Thread(myThreadDelegate);
                        //myThread.Start();
                        #endregion
                    }
                }
                catch
                { }
                #region ���ζ��̣߳�������Ĵ���Ų����{B8FFCAB8-A9FF-43b2-96E2-2DF17B7F3A91}
                #region {9A40A1FE-C527-4f86-B6F5-E7F52FDD28C9}
                this.initFPdetail();
                #endregion
                #endregion

                fpItemList.Sheets[0].DataAutoCellTypes = false;
                fpItemList.Sheets[0].DataAutoSizeColumns = false;

                if (bIsListShowAlways)
                {
                    this.RefreshFP();
                }

                if (this.bIsListShowAlways == false)
                {
                    #region ���߳�����{B8FFCAB8-A9FF-43b2-96E2-2DF17B7F3A91}
                    this.AddItem();
                    #endregion

                    frmShowItem.AddControl(fpItemList);
                }
                else
                {
                    fpItemList.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.panel4.Controls.Add(fpItemList);
                }

                frmShowItem.Owner = this.FindForm();


                frmShowItem.writeWay += new FS.HISFC.Components.Common.Forms.frmShowItem.SelectWriteWay(frmItemList_writeWay);
                frmShowItem.companyRang += new FS.HISFC.Components.Common.Forms.frmShowItem.IsCompanyRang(frmItemList_companyRang);
                frmShowItem.isDeptUsedItem += new FS.HISFC.Components.Common.Forms.frmShowItem.IsDeptUsedItem(frmItemList_isDeptUsedItem);

                frmShowItem.Init();

                frmShowItem.SetUserDefaultSetting();
                frmShowItem.Size = new Size(0, 0);
                frmShowItem.Show();
                frmShowItem.Hide();
                fpItemList.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;
                //fpItemList.Sheets[0].SetColumnAllowAutoSort(-1, true);
                //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl ����ȥ���¼�ί��
                frmShowItem.Closing -= frmItemList_Closing;
                fpItemList.CellDoubleClick -= fpSpread1_CellDoubleClick;
                fpItemList.KeyDown -= fpSpread1_KeyDown;

                frmShowItem.Closing += new CancelEventHandler(frmItemList_Closing);
                fpItemList.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
                fpItemList.KeyDown += new KeyEventHandler(fpSpread1_KeyDown);

                #region {9A40A1FE-C527-4f86-B6F5-E7F52FDD28C9}
                fpItemList.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpItemList_SelectionChanged);
                #endregion

                #region {ECAE27F0-CC52-46be-A8C5-BC9F680988CD}

                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParmMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                this.isShowCmbDrugDept = ctrlParmMgr.GetControlParam<bool>("200310", true, false);
                if (this.isShowCmbDrugDept)
                {
                    FS.HISFC.BizProcess.Integrate.Pharmacy phaManagement = new FS.HISFC.BizProcess.Integrate.Pharmacy();

                    ArrayList alDrugDept = phaManagement.QueryReciveDrugDept(this.deptcode, "A");
                    if (alDrugDept != null && alDrugDept.Count > 1)
                    {
                        FS.FrameWork.Models.NeuObject objAllTmp = new FS.FrameWork.Models.NeuObject();
                        objAllTmp.ID = "ALL";
                        objAllTmp.Name = "ȫ��";
                        alDrugDept.Insert(0, objAllTmp);
                        frmShowItem.cmbDrugDept.Visible = true;
                        frmShowItem.cmbDrugDept.AddItems(alDrugDept);
                        frmShowItem.cmbDrugDept.SelectedIndex = 0;
                        frmShowItem.cmbDrugDept.IsListOnly = true;
                        frmShowItem.cmbDrugDept.IsPopForm = false;
                        frmShowItem.cmbDrugDept.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                    else
                    {
                        frmShowItem.cmbDrugDept.Visible = false;
                    }
                }
                #endregion

                //zhangjunyi ȥ�� ��DataSet����ɸѡ��û�����壬���ҷ����±�DataView�İ�
                //if (this.myDataSet != null) fpItemList.Sheets[0].DataSource = this.myDataSet; 
                this.txtItemCode.Enter += new EventHandler(txtItemCode_Enter);
                this.txtItemCode.Leave += new EventHandler(txtItemCode_Leave);
                this.InputType = FS.FrameWork.WinForms.Classes.Function.GetInputType();

                this.fpItemList.MouseWheel += new MouseEventHandler(fpItemList_MouseWheel);

                this.fpItemList.MouseMove += new MouseEventHandler(fpItemList_MouseWheel);

                this.fpItemList.KeyPress += new KeyPressEventHandler(fpItemList_KeyPress);

                this.frmShowItem.RefreshFP();
            }
            catch (Exception ee)
            {
                MessageBox.Show("Init" + ee.Message);
            }
        }

        void fpItemList_KeyPress(object sender, KeyPressEventArgs e)
        {
            ShowItemFlag();
        }

        void fpItemList_MouseWheel(object sender, MouseEventArgs e)
        {
            ShowItemFlag();
        }

        private void ShowItemFlag()
        {
            if (isUserThread != 2)
            {
                return;
            }

            Font font_Bold = new Font(this.fpItemList.Font.FontFamily, this.fpItemList.Font.Size, FontStyle.Bold);
            Font font_Regular = new Font(this.fpItemList.Font.FontFamily, this.fpItemList.Font.Size, FontStyle.Regular);

            //ֻˢ�µ�ǰ��ʾ������
            for (int i = this.fpItemList.GetViewportTopRow(0); i < fpItemList.GetViewportBottomRow(0) + 1; i++)
            {
                if (i >= fpItemList.Sheets[0].RowCount)
                {
                    return;
                }
                #region ��ʾͣ�á�ȱҩ��Ϣ

                if (fpItemList.Sheets[0].Cells[i, (int)FS.HISFC.Components.Common.Controls.EnumMainColumnSet.LackFlag].Text == "��")
                {
                    fpItemList.Sheets[0].Rows[i].BackColor = Color.LightCoral;
                }
                else
                {
                    fpItemList.Sheets[0].Rows[i].BackColor = Color.Transparent;
                }
                #endregion

                #region ��ʾҽ���ȼ���Ϣ

                string itemCode = fpItemList.Sheets[0].Cells[i, (int)FS.HISFC.Components.Common.Controls.EnumMainColumnSet.ItemCode].Text.Trim();
                if (itemCode != "999")
                {
                    FS.HISFC.Models.Base.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                    if (item == null)
                    {
                        item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
                    }

                    if (item != null
                        && patient != null
                        && patient.Pact != null)
                    {
                        string strCompareInfo = string.Empty;
                        if (Classes.Function.IItemCompareInfo.GetCompareItemInfo(item, patient.Pact, ref compareItem, ref strCompareInfo) == -1)
                        {
                            this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Regular;
                            this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = this.fpItemList.Sheets[0].ColumnHeader.Columns[0].ForeColor;
                        }
                        else
                        {
                            //ҽ�����
                            switch (compareItem.CenterItem.ItemGrade)
                            {
                                case "1":
                                    this.fpItemList.Sheets[0].Rows[i].Label = "��";
                                    this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Bold;
                                    this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = Color.Red;
                                    break;
                                case "2":
                                    this.fpItemList.Sheets[0].Rows[i].Label = "��";
                                    this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Bold;
                                    this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = Color.Red;
                                    break;
                                default:
                                    this.fpItemList.Sheets[0].Rows[i].Label = i.ToString();
                                    this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Regular;
                                    this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = this.fpItemList.Sheets[0].ColumnHeader.Columns[0].ForeColor;
                                    break;
                            }
                        }
                    }
                }
                #endregion
            }
        }

        #endregion

        public void Clear()
        {
            this.txtItemCode.Text = "";
            this.txtItemName.Text = "";
            if (this.frmShowItem.Visible)
            {
                this.frmShowItem.Visible = false;
            }
        }

        #region ��Ŀ����
        /// <summary>
        /// ��ʼ�����
        /// </summary>
        /// <returns></returns>
        protected virtual int AddCategory()
        {
            this.cmbCategory.ShowCustomerList = false;
            if (this.eShowCategory == EnumCategoryType.ItemType)
            {
                FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();
                arrItemTypes = constant.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            }
            else if (this.eShowCategory == EnumCategoryType.SysClass)
            {
                //�ɴ˻�ȡ��Ŀ��� ��ҩ��������������ҽ����
                arrItemTypes = FS.HISFC.Models.Base.SysClassEnumService.List();
                if (eUndrugApplicabilityarea == EnumUndrugApplicabilityarea.Clinic)
                {
                    ArrayList altemp = arrItemTypes.Clone() as ArrayList;
                    arrItemTypes.Clear();
                    if (this.eShowItemType == EnumShowItemType.All)
                    {

                        for (int i = 0; i < altemp.Count; i++)
                        {
                            //ҩƷ
                            if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID.Substring(0, 1) == "P")
                            {
                                arrItemTypes.Add(altemp[i]);
                            }

                            
                            if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID.Substring(0, 1) == "U")
                            {
                                arrItemTypes.Add(altemp[i]);
                            }

                            if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID == "M"
                                || ((FS.FrameWork.Models.NeuObject)altemp[i]).ID == "MC")
                            {
                                arrItemTypes.Add(altemp[i]);
                            }
                        }
                    }
                    else if (this.eShowItemType == EnumShowItemType.Pharmacy)
                    {
                        for (int i = 0; i < altemp.Count; i++)
                        {

                            if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID.Substring(0, 1) == "P")
                            {
                                arrItemTypes.Add(altemp[i]);
                            }
                        }

                    }
                    else if (this.eShowItemType == EnumShowItemType.Undrug)
                    {
                        for (int i = 0; i < altemp.Count; i++)
                        {

                            if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID.Substring(0, 1) == "U")
                            {
                                arrItemTypes.Add(altemp[i]);
                            }
                        }

                    }
                }
                else
                {
                    if (this.eShowItemType != EnumShowItemType.All)
                    {
                        ArrayList altemp = arrItemTypes.Clone() as ArrayList;
                        arrItemTypes.Clear();
                        for (int i = 0; i < altemp.Count; i++)
                        {
                            if (this.eShowItemType == EnumShowItemType.Pharmacy)
                            {
                                if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID.Substring(0, 1) == "P")
                                {
                                    arrItemTypes.Add(altemp[i]);
                                }
                            }
                            else if (this.eShowItemType == EnumShowItemType.Undrug)
                            {
                                if (((FS.FrameWork.Models.NeuObject)altemp[i]).ID.Substring(0, 1) == "U")
                                {
                                    arrItemTypes.Add(altemp[i]);
                                }
                            }
                        }
                    }
                }
            }
            if (arrItemTypes != null)
            {
                FS.FrameWork.Models.NeuObject o = new FS.FrameWork.Models.NeuObject();
                o.Name = "ȫ��";
                this.arrItemTypes.Add(o);
                this.cmbCategory.AddItems(arrItemTypes);
            }
            else
            {
                MessageBox.Show("������ʧ�ܣ������²�����");
                return -1;
            }
            this.cmbCategory.Text = "ȫ��";
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cbCategory_SelectedIndexChanged);
            return 0;
        }

        /// <summary>
        /// ˢ��ͣ�ñ�Ƕ��߳�
        /// </summary>
        //ThreadStart freshLackFlagThreadDelegate = null;

        /// <summary>
        /// �Ƿ�����ˢ��ȱҩ���
        /// </summary>
        //bool isFreshingLackFlag = false;

        /// <summary>
        /// �����Ŀ
        /// </summary>
        /// <returns></returns>
        protected virtual int AddItems()
        {
            FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            try
            {
                FS.HISFC.Models.Base.Employee employee = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(this.oper.ID);
                ((FS.HISFC.Models.Base.Employee)oper).Level = employee.Level;
                

                //TODO: ����ҩƷ�ͷ�ҩƷ�б�
                #region ����ҩƷ
                if (this.eShowItemType == EnumShowItemType.Pharmacy)
                {
                    if (this.deptcode == string.Empty)
                    {
                        alItem = new ArrayList(itemIntegrate.QueryItemAvailableList(true).ToArray());//��ʾ����
                    }
                    else
                    {
                        //��������Ĭ��ȡҩ���һ�ȡ��Ч����б�
                        //arrAllItems = new ArrayList(itemIntegrate.QueryItemAvailableList(this.deptcode, this.oper.ID, ((FS.HISFC.Models.Base.Employee)oper).Level.ID).ToArray());
                        //��ȡ�б����������ƣ���ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ��
                        alItem = new ArrayList(itemIntegrate.QueryItemAvailableListBySendType(this.deptcode, this.oper.ID, ((FS.HISFC.Models.Base.Employee)oper).Level.ID, drugSendType).ToArray());
                    }
                }
                #endregion

                #region ���ط�ҩƷ
                else if (this.eShowItemType == EnumShowItemType.Undrug)
                {
                    if (this.isShowGroup)//��ʾ����
                    {
                        alItem = itemMgr.GetAvailableListWithGroup();
                    }
                    else//����ʾ����
                    {
                        //arrAllItems = itemMgr.QueryValidItems();
                        //arrAllItems = itemMgr.QueryValidItemsForOrder(deptcode);
                        alItem = SOC.HISFC.BizProcess.Cache.Fee.GetValidItem();
                    }

                    #region {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
                    //if (this.isIncludeMat)
                    //{
                    //    FS.HISFC.BizProcess.Integrate.Material.Material matIntergrate = new FS.HISFC.BizProcess.Integrate.Material.Material();
                    //    ArrayList al3 = new ArrayList();
                    //    if (!string.IsNullOrEmpty(this.deptcode))
                    //    {
                    //        al3 = new ArrayList(matIntergrate.QueryStockHeadItemForFee(this.deptcode).ToArray());
                    //    }
                    //    arrAllItems.AddRange(al3);
                    //}
                    #endregion

                    alItem = this.FilterUndrug(alItem);
                }
                #endregion

                #region ����ҩƷ��Ϣ
                else if (this.eShowItemType == EnumShowItemType.OutPharmacy)
                {
                    string outDept = contrIntegrate.GetControlParam<string>("BJ001", false, "");

                    alItem = new ArrayList(itemIntegrate.QueryItemAvailableListByBoJi(outDept, this.oper.ID, ((FS.HISFC.Models.Base.Employee)oper).Level.ID, drugSendType).ToArray());
                   

                }
                #endregion

                #region ����ȫ��
                else
                {
                    ArrayList al1 = null;

                    //{D5DF4996-A960-471a-9B38-5B583C8D0B2D}
                    //������ײ����룬���������ҩƷ
                    if (this.deptcode == string.Empty || this.IsPackageInput)
                    {
                        //��ҩƷ������Ϣ���ȡȫ����ЧҩƷ��Ϣ
                        al1 = new ArrayList(itemIntegrate.QueryItemAvailableList(true).ToArray());
                    }
                    else
                    {
                        //��������Ĭ��ȡҩ���һ�ȡ��Ч����б�
                        //al1 = new ArrayList(itemIntegrate.QueryItemAvailableList(this.deptcode, this.oper.ID, ((FS.HISFC.Models.Base.Employee)oper).Level.ID).ToArray());
                        //��ȡ�б����������ƣ���ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ��
                        al1 = new ArrayList(itemIntegrate.QueryItemAvailableListBySendType(this.deptcode, this.oper.ID, ((FS.HISFC.Models.Base.Employee)oper).Level.ID, drugSendType).ToArray());
                    }

                    ArrayList al2 = null;
                    if (this.isShowGroup)//��ʾ����
                    {
                        al2 = itemMgr.GetAvailableListWithGroup();
                    }
                    else//����ʾ����
                    {
                        //al2 = itemMgr.QueryValidItems();
                        //al2 = SOC.HISFC.BizProcess.Cache.Fee.GetValidItem();
                        //al2 = itemMgr.QueryValidItemsForOrder(deptcode);
                        al2 = SOC.HISFC.BizProcess.Cache.Fee.GetValidItem();
                    }

                    al2 = this.FilterUndrug(al2);

                    this.alItem = al1;
                    al1.AddRange(al2);

                    ArrayList alTempLong = new ArrayList();
                    alTempLong.AddRange(al1);
                    alTempLong.AddRange(alLongItem);
                    alLongItem = alTempLong;

                    ArrayList alTempShort = new ArrayList();
                    alTempShort.AddRange(al1);
                    //alTempShort.AddRange(alShortItem);
                    alShortItem = alTempShort;

                    #region {8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
                    //if (this.isIncludeMat)
                    //{
                    //    FS.HISFC.BizProcess.Integrate.Material.Material matIntergrate = new FS.HISFC.BizProcess.Integrate.Material.Material();
                    //    ArrayList al3 = new ArrayList();
                    //    if (!string.IsNullOrEmpty(this.deptcode))
                    //    {
                    //        al3 = new ArrayList(matIntergrate.QueryStockHeadItemForFee(this.deptcode).ToArray());
                    //    }
                    //    al1.AddRange(al3);
                    //}
                    #endregion

                    this.alItem = al1;
                }
                #endregion

                itemHelper.ArrayObject.AddRange(alItem);

                //��������ʾ��������(��������/��ʿ����)
                if (this.bShowDeptGroup)
                {
                    this.AddDeptGroup();
                }
                //*********************************

                //if (!bIsListShowAlways)
                //{
                //    this.RefreshFP();
                //}

                #region ����ҽ��������Ϣ ���� ���ҽ���޷�����

                if (isUserThread == 3)
                {
                    DataSet dsTemp = new DataSet();

                    if (this.phaManger.ExecQuery(@"select t.pact_code,t.his_code,t.center_item_grade 
                                                        from fin_com_compare t
                                                        union 
                                                        select f.pact_code,f.fee_code,'all'
                                                        from fin_com_pactunitfeecoderate f
                                                        union 
                                                        select 'all',g.item_code,g.item_grade
                                                        from FIN_COM_ITEM_EXTENDINFO g", ref dsTemp) == -1)
                    {
                        MessageBox.Show("AddItems" + phaManger.Err);
                    }
                    if (dsTemp != null)
                    {
                        hsPactColumn = new Hashtable();
                        hsItemPactInfo = new Hashtable();
                        foreach (DataRow drow in dsTemp.Tables[0].Rows)
                        {
                            if (!hsPactColumn.Contains(drow[0].ToString()))
                            {
                                hsPactColumn.Add(drow[0].ToString(), null);
                            }

                            if (!hsItemPactInfo.Contains(drow[0].ToString() + drow[1].ToString()))
                            {
                                hsItemPactInfo.Add(drow[0].ToString() + drow[1].ToString(), drow[2].ToString());
                            }
                        }

                        Classes.Function.HsItemPactInfo = hsItemPactInfo;
                    }
                }
                #endregion
            }
            catch (Exception ee)
            {
                MessageBox.Show("AddItems" + ee.Message);
            }
            return 1;
        }

        /// <summary>
        /// ���ؿ������� (��ʿ���ס���������)
        /// </summary>
        /// <returns></returns>
        protected virtual void AddDeptGroup()
        {
            if (this.deptcode == null) return;
            //�������
            FS.HISFC.BizLogic.Manager.ComGroup group = new FS.HISFC.BizLogic.Manager.ComGroup();
            ArrayList al = group.GetValidGroupList(this.deptcode);

            if (al == null)
                return;
            if (alItem == null)
            {
                this.alItem = new ArrayList();
            }
            this.alItem.AddRange(al);
        }

        /// <summary>
        /// �����б�
        /// </summary>
        public virtual void AddItem()
        {
            // [2007/02/08 ��ΰ��]
            // �ؼ���ͬ������,��������
            try
            {
                System.Threading.Monitor.Enter(this);

                bCanChange = false;
                if (this.bIsListShowAlways == false)
                {
                    this.txtItemCode.Text = "������,���Ժ�...";
                    this.txtItemCode.Enabled = false;
                }
                this.AddItems();
                RefreshFP();
                this.txtItemCode.Text = "";
                this.txtItemCode.Enabled = true;
                if (this.frmShowItem.Visible)
                {
                    this.frmShowItem.Visible = false;
                }

                bCanChange = true;
            }
            catch (Exception e)
            {
                MessageBox.Show("AddItem" + e.Message);
            }
            finally
            {
                System.Threading.Monitor.Exit(this);
                bCanChange = true;
            }

            // �ͷŹؼ���,�����߳̿��Է���           
        }

        /// <summary>
        /// ˢ���б�
        /// </summary>
        public virtual void RefreshFP()
       {
            //����FPList
            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
            if (this.alItem != null && (MyDataSet == null || this.isIncludeMat))
            {
                //MyDataSet = this.CreateDataSet(this.alItem);

                longDataSet = CreateDataSet(this.alLongItem);
                longDV = new DataView(longDataSet.Tables[0]);
                shortDataSet = CreateDataSet(this.alShortItem);
                shortDV = new DataView(this.shortDataSet.Tables[0]);

                myDeptDataSet = this.CreateDataSet(this.arrDeptUsed);

                dvDeptUsed = new DataView(myDeptDataSet.Tables[0]);

                //dv = new DataView(MyDataSet.Tables[0]);
                //fpItemList.Sheets[0].DataSource = dv;

                if (this.longOrder)
                {
                    fpItemList.Sheets[0].DataSource = longDV;
                }
                else
                {
                    fpItemList.Sheets[0].DataSource = shortDV;
                }

                //dv.Sort = "�Զ����� ASC";
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.ItemCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.ItemName].Width = 150;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.SysClassName].Width = 50;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.Specs].Width = 90;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.Price].Width = 40;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.Unit].Width = 30;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.SiFlag].Width = 40;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.SiFlag].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.Product].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.SysClassCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.SpellCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.WBCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.UserCode].Width = 50;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.RegularName].Width = 120;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.RegularNameSpellCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.WBCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.RegularNameUserCode].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.EnglishName].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.StorageQty].Visible = false;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.LackFlag].ForeColor = Color.Red;
                this.fpItemList.Sheets[0].Columns[(int)EnumMainColumnSet.LackFlag].Font = new Font(this.fpItemList.Font.FontFamily, this.fpItemList.Font.Size, FontStyle.Bold);

                //�����ҽ������ж� �Ӵ� ��ɫ
                for (int i = (int)EnumMainColumnSet.LackFlag; i < fpItemList.Sheets[0].ColumnCount; i++)
                {
                    this.fpItemList.Sheets[0].Columns[i].ForeColor = Color.Red;
                    this.fpItemList.Sheets[0].Columns[i].Font = new Font(this.fpItemList.Font.FontFamily, this.fpItemList.Font.Size, FontStyle.Bold);
                }

                if (this.IsListShowAlways == false)
                {
                    frmShowItem.DataView = MyDataView;
                    frmShowItem.RefreshFP();
                }
            }
        }

        /// <summary>
        /// ��Ŀ�б�
        /// </summary>
        /// <param name="al"></param>
        /// <returns></returns>
        protected virtual DataSet CreateDataSet(ArrayList al)
        {
            DataSet myDataSet = new DataSet();
            myDataSet.EnforceConstraints = false;//�Ƿ���ѭԼ������
            //��������
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtInt = System.Type.GetType("System.Int32");

            //�����********************************************************
            //Main Table
            DataTable dtMain;
            dtMain = myDataSet.Tables.Add("Table");
            dtMain.Columns.AddRange(new DataColumn[]
            { 
                new DataColumn("����",dtStr),//0
                new DataColumn("����", dtStr),//1
                new DataColumn("���", dtStr),//2
                new DataColumn("������",dtStr),//3
                new DataColumn("���", dtStr),//4
                new DataColumn("�۸�",dtStr),//5
                new DataColumn("��λ",dtStr),//6
                new DataColumn("ҽ�����",dtStr),//7
                new DataColumn("����", dtStr),//8
                new DataColumn("ƴ����", dtStr),//9
                new DataColumn("�����", dtStr),//10
                new DataColumn("�Զ�����", dtStr),//11
                new DataColumn("ͨ����", dtStr),//12
                new DataColumn("ͨ����ƴ����", dtStr),//13
                new DataColumn("ͨ���������", dtStr),//14
                new DataColumn("ͨ�����Զ�����", dtStr),//15
                new DataColumn("Ӣ����Ʒ��", dtStr),//16
                new DataColumn("����������", dtStr),
                new DataColumn("ִ�п���", dtStr),
                new DataColumn("������", dtStr),
                new DataColumn("��������", dtStr),
                new DataColumn("ר������", dtStr),
                new DataColumn("��ʷ�����", dtStr),
                new DataColumn("���Ҫ��", dtStr),
                new DataColumn("ע������", dtStr),
                new DataColumn("ȱ",dtStr)
            });

            if (isUserThread == 3)
            {
                if (hsPactColumn != null && hsPactColumn.Count > 0)
                {
                    Hashtable hsTemp = new Hashtable();
                    ArrayList alTemp = new ArrayList(hsPactColumn.Keys);
                    alTemp.Sort();

                    foreach (string key in alTemp)
                    {
                        dtMain.Columns.Add("���" + key);
                        //hsPactColumn[key] = dtMain.Columns.Count;
                        hsTemp.Add(key, dtMain.Columns.Count - 1);
                    }

                    hsPactColumn = hsTemp;
                }
            }

            if (this.IGetSiFlag == null)
            {
                this.iGetSiFlag = null;
            }

            string itemCode = "";


            //�����˿�����ļ۸���ʾ���˿�������ʾ�����
            FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alYKDept = conMgr.GetAllList("YkDept");
            Hashtable hsYKDept = null;
            if (alYKDept != null)
            {
                hsYKDept = new Hashtable();
                foreach (FS.FrameWork.Models.NeuObject obj in alYKDept)
                {
                    if (!hsYKDept.Contains(obj.ID))
                    {
                        hsYKDept.Add(obj.ID, obj);
                    }
                }
            }

            //��ʾ�ļ۸�
            decimal showPrice = 0;
            for (int i = 0; i < al.Count; i++)
            {
                if (al[i].GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {
                    #region ҩƷ

                    FS.HISFC.Models.Pharmacy.Item obj;
                    obj = (FS.HISFC.Models.Pharmacy.Item)al[i];
                    if (obj.User02 != string.Empty)
                    {
                        obj.User03 = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(obj.User02);
                        al[i] = obj;
                    }

                    //if (hsYKDept.Contains(((FS.HISFC.Models.Base.Employee)oper).Dept.ID))
                    //{
                    //    showPrice = obj.SpecialPrice;
                    //}
                    //else
                    //{
                    if (isUserRetailPrice2)
                    {
                        showPrice = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(obj.ID).RetailPrice2;
                    }
                    else
                    {
                        showPrice = obj.Price;
                    }
                    //}

                    dtMain.Rows.Add(new object[] {
                                    obj.ID,//����
                                    obj.Name,//����
                                    obj.SysClass.Name, //ϵͳ���
                                    obj.SysClass.ID,//ϵͳ������
                                    obj.Specs, //���
                                    showPrice, //�۸�
                                    obj.PriceUnit, //��λ
                                    FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(obj.Grade) + FS.HISFC.Components.Common.Classes.Function.ShowItemFlag(obj), //ҽ�����
                                    obj.Product.Producer.Name,//��������
                                    obj.SpellCode,//ƴ����
                                    obj.WBCode,//�����
                                    obj.UserCode, //�Զ�����
                                    obj.NameCollection.RegularName,//ͨ������������
                                    obj.NameCollection.RegularSpell.SpellCode,//ͨ������������ƴ����
                                    obj.NameCollection.RegularSpell.WBCode,//ͨ�����������������
                                    obj.NameCollection.RegularSpell.UserCode,//ͨ�������������Զ�����
                                    obj.NameCollection.EnglishName,//Ӣ����
                                    obj.User01,//�����
                                    obj.User03,//ִ�п���
                                    string.Empty,//������
                                    string.Empty,//��������
                                    string.Empty,//ר������
                                    string.Empty,//��ʷ�����
                                    string.Empty,//���Ҫ��
                                    string.Empty,//ע������
                                    obj.IsLack?"��":""//�Ƿ�ȱҩ
                    });

                    itemCode = obj.ID;

                    #endregion
                    al[i] = obj;
                }
                else if (al[i].GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))
                {
                    #region ��ҩƷ��������Ŀ
                    FS.HISFC.Models.Fee.Item.Undrug undrug = (FS.HISFC.Models.Fee.Item.Undrug)al[i];

                    #region ʹ�÷�Χ����

                    if (eUndrugApplicabilityarea == EnumUndrugApplicabilityarea.Clinic)
                    {
                        if (isShowDetailItem.Substring(0, 1) == "0" 
                            && undrug.SysClass.ID.ToString() == "UC")
                        {
                            if (undrug.UnitFlag != "1")
                            {
                                continue;
                            }
                        }
                        if (isShowDetailItem.Substring(1, 1) == "0" 
                            && undrug.SysClass.ID.ToString() == "UL")
                        {
                            if (undrug.UnitFlag != "1")
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        if (isShowDetailItem.Substring(2, 1) == "0" 
                            && undrug.SysClass.ID.ToString() == "UC")
                        {
                            if (undrug.UnitFlag != "1")
                            {
                                continue;
                            }
                        }
                        if (isShowDetailItem.Substring(3, 1) == "0" 
                            && undrug.SysClass.ID.ToString() == "UL")
                        {
                            if (undrug.UnitFlag != "1")
                            {
                                continue;
                            }
                        }
                    }
                    #endregion

                    #region ���ݸ���Ŀ���������ҹ�����Ŀ

                    bool val = false;
                    string[] deptList = null;
                    //{D353AD80-9DE0-4af8-85A1-5FE6E9466679}
                    if (!isFilteUndrugRecipeDept || undrug.DeptList == ""
                        || undrug.DeptList.ToUpper() == "ALL" || undrug.DeptList == null || this.IsPackageInput)
                    {
                        val = true;
                    }
                    else
                    {
                        if (this.deptLogin == null)
                        {
                            this.deptLogin = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept;
                        }
                        deptList = undrug.DeptList.Split('|');
                        for (int j = 0; j < deptList.Length; j++)
                        {
                            if (deptList[j].ToString() == this.deptLogin.ID.ToString())
                            {
                                val = true;
                                break;
                            }
                        }
                    }
                    #endregion

                    if (undrug.ExecDept != string.Empty)
                    {
                        try
                        {
                            string[] execDept = undrug.ExecDept.Split('|');
                            undrug.User01 = "";
                            for (int k = 0; k < execDept.Length; k++)
                            {
                                if (!string.IsNullOrEmpty(execDept[k]))
                                {
                                    undrug.User01 += (string.IsNullOrEmpty(undrug.User01) ? "" : "��") + FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(execDept[k]);
                                }
                            }
                            //this.arrAllItems[i] = obj;
                        }
                        catch { }
                    }

                    if (val)
                    {
                        if (hsYKDept.Contains(((FS.HISFC.Models.Base.Employee)oper).Dept.ID))
                        {
                            showPrice = undrug.SpecialPrice == 0 ? undrug.Price : undrug.SpecialPrice;
                        }
                        else
                        {
                            showPrice = undrug.Price;
                        }

                        undrug.Price = showPrice;

                        dtMain.Rows.Add(new Object[] { 
                                            undrug.ID,//����
                                            undrug.Name,//����
                                            undrug.SysClass.Name,//ϵͳ���
                                            undrug.SysClass.ID,//ϵͳ������
                                            undrug.Specs,//���
                                            showPrice,//�۸�
                                            undrug.PriceUnit,//��λ
                                            FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(undrug.Grade) + FS.HISFC.Components.Common.Classes.Function.ShowItemFlag(undrug),//ҽ�����
                                            string.Empty,//��������
                                            undrug.SpellCode,//ƴ����
                                            undrug.WBCode,//�����
                                            undrug.UserCode,//�Զ�����
                                            undrug.NameCollection.OtherName,//ͨ������������
                                            undrug.NameCollection.OtherSpell.SpellCode,//ͨ������������ƴ����
                                            undrug.NameCollection.OtherSpell.WBCode,//ͨ�����������������
                                            undrug.NameCollection.OtherSpell.UserCode,//ͨ�������������Զ�����
                                            string.Empty,//Ӣ����
                                            string.Empty,//�����
                                            undrug.User01,//ִ�п���
                                            undrug.CheckBody,//������
                                            undrug.DiseaseType,//��������
                                            undrug.SpecialDept,//ר������
                                            undrug.MedicalRecord,//��ʷ�����
                                            undrug.CheckRequest,//���Ҫ��
                                            undrug.Notice,//ע������
                                            undrug.IsValid ? "" : "��" //�Ƿ�ȱҩ
                        });
                    }

                    itemCode = undrug.ID;

                    #endregion
                    al[i] = undrug;
                }
                else
                {
                    #region ��������

                    FS.HISFC.Models.Fee.ComGroup obj;
                    obj = al[i] as FS.HISFC.Models.Fee.ComGroup;
                    if (obj == null) continue;
                    dtMain.Rows.Add(new Object[] {
                            obj.ID,//����
                            obj.Name,//����
                            "��ҩƷ",//ϵͳ���
                            "U",//ϵͳ������
                            string.Empty,//���
                            0.0,//�۸�
                            "[����]",//��λ
                            string.Empty,//ҽ�����
                            string.Empty,//��������
                            obj.spellCode,//ƴ����
                            string.Empty,//�����
                            obj.inputCode,//�Զ�����
                            string.Empty,//ͨ������������
                            string.Empty,//ͨ������������ƴ����
                            string.Empty,//ͨ�����������������
                            string.Empty,//ͨ�������������Զ�����
                            string.Empty,//Ӣ����
                            string.Empty,//�����
                            string.Empty,//ִ�п���
                            string.Empty,//������
                            string.Empty,//��������
                            string.Empty,//ר������
                            string.Empty,//��ʷ�����
                            string.Empty,//���Ҫ��
                            string.Empty,//ע������
                            string.Empty //�Ƿ�ȱҩ
                    });

                    itemCode = obj.ID;

                    #endregion
                    al[i] = obj;
                }

                if (isUserThread == 3)
                {
                    if (hsPactColumn != null && hsPactColumn.Count > 0)
                    {
                        foreach (string key in hsPactColumn.Keys)
                        {
                            dtMain.Rows[dtMain.Rows.Count - 1][(Int32)hsPactColumn[key]] = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(hsItemPactInfo[key + itemCode] == null ? "" : hsItemPactInfo[key + itemCode].ToString());
                        }
                    }
                }
            }
            return myDataSet;
        }

        //Hashtable hsUCULItem = null;

        /// <summary>
        /// ���˷�ҩƷ�����סԺ��ȫ����
        /// </summary>
        /// <param name="alUndrug"></param>
        /// <returns></returns>
        protected virtual ArrayList FilterUndrug(ArrayList alUndrug)
        {
            ArrayList al = new ArrayList();

            alLongItem = new ArrayList();
            alShortItem = new ArrayList();

            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in alUndrug)
            {
                if (!undrug.IsValid)
                {
                    continue;
                }

                #region �����������ҹ���

                if (!string.IsNullOrEmpty(undrug.DeptList)
                    && undrug.DeptList.Trim().ToUpper() != "ALL"
                    && !undrug.DeptList.Contains(this.deptcode))
                {
                    //{D5DF4996-A960-471a-9B38-5B583C8D0B2D}
                    //������ײͽ�����ã��򲻽������������ҹ���
                    if (!IsPackageInput)
                    {
                        continue;
                    }
                }

                #endregion

                #region ��ʹ�÷�Χ����
                if (this.eUndrugApplicabilityarea == EnumUndrugApplicabilityarea.All)
                {
                }
                else if (this.eUndrugApplicabilityarea == EnumUndrugApplicabilityarea.Clinic)
                {
                    if (undrug.ApplicabilityArea == "0"
                        || undrug.ApplicabilityArea == "1")
                    {
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    if (undrug.ApplicabilityArea == "0"
                        || undrug.ApplicabilityArea == "2")
                    {
                    }
                    else
                    {
                        continue;
                    }
                }
                #endregion

                al.Add(undrug);

                #region �������Ƿ�����������

                //if (hsUCULItem == null)
                //{
                //    hsUCULItem = new Hashtable();
                //    ArrayList alUCUL = this.interMgr.GetConstantList("LongUCUL");
                //    foreach (FS.HISFC.Models.Base.Const con in alUCUL)
                //    {
                //        if (con.IsValid && !hsUCULItem.Contains(con.ID))
                //        {
                //            hsUCULItem.Add(con.ID, null);
                //        }
                //    }
                //}
                //if (hsUCULItem != null
                //    && "UC��UL".Contains(undrug.SysClass.ID.ToString())
                //    && hsUCULItem.Contains(undrug.ID))
                //{
                //    alLongItem.Add(undrug);
                //}
                if (Classes.Function.isUCUCForLong(undrug))
                {
                    alLongItem.Add(undrug);
                }

                #endregion

                alShortItem.Add(undrug);
            }

            return al;

            #region �ɵ�����

            //if (this.eUndrugApplicabilityarea == EnumUndrugApplicabilityarea.All)
            //{
            //    return alUndrug;
            //}
            //else if (this.eUndrugApplicabilityarea == EnumUndrugApplicabilityarea.Clinic)
            //{
            //    foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in alUndrug)
            //    {
            //        if (undrug.ApplicabilityArea == "0")
            //        {
            //            al.Add(undrug);
            //        }
            //        else if (undrug.ApplicabilityArea == "1")
            //        {
            //            al.Add(undrug);
            //        }
            //    }
            //    return al;
            //}
            //else
            //{
            //    foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in alUndrug)
            //    {
            //        if (undrug.ApplicabilityArea == "0")
            //        {
            //            al.Add(undrug);
            //        }
            //        else if (undrug.ApplicabilityArea == "2")
            //        {
            //            al.Add(undrug);
            //        }
            //    }
            //    return al;
            //}

            #endregion
        }

        /// <summary>
        /// ˢ���б�
        /// {112B7DB5-0462-4432-AD9D-17A7912FFDBE} 
        /// </summary>
        [Obsolete("����", true)]
        public virtual void RefreshSIFlag()
        {
            if (this.MyDataSet != null && this.MyDataSet.Tables.Count > 0)
            {
                if (this.IGetSiFlag != null)
                {
                    foreach (DataRow dr in this.MyDataSet.Tables[0].Rows)
                    {
                        //{112B7DB5-0462-4432-AD9D-17A7912FFDBE}   ��ȡҽ����Ŀ���

                        string itemGrade = "0";
                        if (this.patient != null && this.patient.Pact.ID != "")
                        {
                            if (this.iGetSiFlag.GetSiItemGrade(this.patient.Pact.ID, dr["����"].ToString(), ref itemGrade) != -1)
                            {
                                dr["ҽ�����"] = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(itemGrade);
                            }
                        }
                        else
                        {
                            if (this.iGetSiFlag.GetSiItemGrade(dr["����"].ToString(), ref itemGrade) != -1)
                            {
                                dr["ҽ�����"] = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(itemGrade);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        protected System.Data.DataView MyDataView
        {
            set
            {
                shortDV = value;
            }
            get
            {
                if (longOrder)
                {
                    return longDV;
                }
                else
                {
                    return shortDV;
                }
            }
        }

        private DataView longDV = null;
        private DataView shortDV = null;

        /// <summary>
        /// ���ҳ�����Ŀ
        /// </summary>
        protected System.Data.DataView dvDeptUsed;

        /// <summary>
        /// �Ƿ��һ�ι��ˣ���һ�ι�����Ҫ�����߳�ˢ��ͣ�ñ��
        /// </summary>
        //bool isFirst = false;

        /// <summary>
        /// �Ƿ�����ˢ��
        /// </summary>
        //bool isInFresh = false;

        /// <summary>
        /// �Ƿ�ǰ�б���ˢ�����
        /// </summary>
        object isFreshFinish;

        /// <summary>
        /// �仯��Ŀ
        /// </summary>
        protected virtual void ChangeItem()
        {
            //TODO:�����б������뷨�й�
            if (MyDataSet == null)
            {
                return;
            }

            try
            {
                this.myShowList(); //��ʾ�б�
                //�жϵ�ǰ������DataSet
                if (MyDataSet == null)
                {
                    return;
                }
                if (MyDataSet.Tables.Count <= 0)
                {
                    return;
                }

                string sCategory = " and ������ = '" + this.cmbCategory.Tag + "'";
                if (this.cmbCategory.Text == FS.FrameWork.Management.Language.Msg("ȫ��"))
                {
                    //��Ϊȫ�������������ܲ�ͬ�����Բ�������д,by huangxw
                    sCategory = string.Empty;
                    foreach (FS.FrameWork.Models.NeuObject obj in this.cmbCategory.alItems)
                    {
                        if (obj.Name != FS.FrameWork.Management.Language.Msg("ȫ��"))
                            sCategory = sCategory + " or ������ = '" + obj.ID + "'";
                    }
                    if (sCategory != string.Empty)
                    {
                        sCategory = sCategory.Substring(3);//ȥ����һ��or
                        sCategory = " and (" + sCategory + ")";
                    }
                }
                string sInput = string.Empty;
                //ȡ������
                string[] spChar = new string[] { "@", "#", "$", "%", "^", "&", "[", "]", "|" };
                string queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(this.txtItemCode.Text.Trim(), spChar);
                queryCode = queryCode.Replace("*", "[*]");

                //�����Ƿ�ȷ���ң������Ƿ����ģ����ѯ
                if (this.frmShowItem.IsReal == false)
                {
                    queryCode = '%' + FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode) + '%';
                }
                else
                {
                    queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode) + '%';
                }
                if (queryCode == "%%")
                {
                    queryCode = "%";
                }
                //

                sInput = "(";
                if (frmShowItem.IsFilterSpellCode)
                {
                    sInput += "ƴ���� LIKE '{0}' or " + "ͨ����ƴ���� LIKE '{0}' or ";
                }
                if (frmShowItem.IsFilterWBCode)
                {
                    sInput += "����� LIKE '{0}' or " + "ͨ��������� LIKE '{0}' or ";
                }

                sInput += "�Զ����� LIKE '{0}' or " + "ͨ�����Զ����� LIKE '{0}' or " + "Ӣ����Ʒ�� LIKE '{0}' or " + "���� LIKE '{0}' or " + "ͨ���� LIKE '{0}')";


                sInput = string.Format(sInput, queryCode);

                sInput = sInput + sCategory;
                //����
                #region ȡҩ���ҹ���
                //{ECAE27F0-CC52-46be-A8C5-BC9F680988CD}
                if (isShowCmbDrugDept)
                {
                    string filterDrugDept = string.Empty;
                    string filterUndrug = string.Empty;
                    if (frmShowItem.cmbDrugDept.alItems != null && frmShowItem.cmbDrugDept.alItems.Count > 0)
                    {
                        if (frmShowItem.cmbDrugDept.Tag != null && frmShowItem.cmbDrugDept.Tag.ToString() != "ALL")
                        {
                            filterDrugDept = " and (������ in ('P','PCZ','PCC') and  ִ�п��� = '" + frmShowItem.cmbDrugDept.Text + "')";
                            filterUndrug = " or (" + sInput + "and (������ not in ('P','PCZ','PCC')))";
                        }
                        else
                        {
                            filterDrugDept = "";
                            filterUndrug = "";
                        }
                    }

                    sInput = "(" + sInput + filterDrugDept + ")" + filterUndrug;
                }
                #endregion

                #region �Ƴ�����Ŀ+����ҽ����ҩ

                sInput = string.Format(sInput, queryCode);
                string Protecteddrug = this.isCompanyRang ? "and ( ҽ�����  like  '%X%')" : " ";
                sInput = sInput + sCategory + Protecteddrug;
                if (isDeptUsedFlag && !string.IsNullOrEmpty(destItemId.Trim()))
                {
                    sInput = "( ���� in  ( " + destItemId + " ))  and  " + sInput;
                }
                #endregion

                #region ��ʾͣ��ȱҩ���

                this.FreshItemFlag();

                #endregion
                //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
                this.RefreshFP();
                //if (this.IsListShowAlways)
                //{
                fpItemList.Sheets[0].DataSource = MyDataView;
                //}

                MyDataView.RowFilter = sInput;

                dvDeptUsed.RowFilter = sInput;

                fpItemList.Sheets[0].ActiveRowIndex = 0;

                fpItemList.Sheets[0].ClearSelection();
                if (fpItemList.Sheets[0].RowCount > 0)
                {
                    fpItemList.Sheets[0].AddSelection(0, 0, 1, 1);
                }
                fpItemList_SelectionChanged(fpItemList, null);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ChangeItem" + ex.Message);
            }
        }

        private System.Threading.Timer autoRefreshTimer = null;
        private System.Threading.TimerCallback autoRefreshCallBack = null;
        private delegate void autoRefreshHandler();
        private autoRefreshHandler autoRefreshEven;

        /// <summary>
        /// ˢ�µĸ���
        /// </summary>
        int freshRowCount = 0;

        /// <summary>
        /// ˢ�´����б�
        /// </summary>
        /// <param name="param">������û��ʹ�ã�</param>
        /// <returns></returns>
        private void AutoRefreshTimerCallback(object param)
        {
            //lock (this)
            //{

            return;

            if (this.autoRefreshEven == null)
            {
                autoRefreshEven = new autoRefreshHandler(this.DoFreshItemFlag);
            }

            if (this.frmShowItem.Visible)
            {
                if (!this.IsDisposed)
                {
                    this.Invoke(this.autoRefreshEven);
                }
            }
        }

        private void DoFreshItemFlag()
        {
            //lock (isFreshFinish)
            //{

            if (isUserThread != 2)
            {
                return;
            }

            try
            {
                Font font_Bold = new Font(this.fpItemList.Font.FontFamily, this.fpItemList.Font.Size, FontStyle.Bold);
                Font font_Regular = new Font(this.fpItemList.Font.FontFamily, this.fpItemList.Font.Size, FontStyle.Regular);

                for (int i = freshRowCount * 10; i < (freshRowCount + 1) * 10; i++)
                {
                    if (i >= fpItemList.Sheets[0].RowCount)
                    {
                        return;
                    }

                    if (fpItemList.Sheets[0].Cells[i, (int)EnumMainColumnSet.LackFlag].Text == "��")
                    {
                        //this.fpItemList.Sheets[0].Rows[i].Label = "ͣ";
                        this.fpItemList.Sheets[0].Rows[i].BackColor = Color.LightCoral;
                    }
                    else
                    {
                        this.fpItemList.Sheets[0].Rows[i].BackColor = Color.Transparent;
                    }

                    #region ��ʾҽ���ȼ���Ϣ

                    string itemCode = fpItemList.Sheets[0].Cells[i, (int)FS.HISFC.Components.Common.Controls.EnumMainColumnSet.ItemCode].Text.Trim();
                    if (itemCode != "999")
                    {
                        FS.HISFC.Models.Base.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemCode);
                        if (item == null)
                        {
                            item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemCode);
                        }

                        if (item != null
                            && patient != null
                            && patient.Pact != null)
                        {
                            string strCompareInfo = string.Empty;
                            if (Classes.Function.IItemCompareInfo.GetCompareItemInfo(item, patient.Pact, ref compareItem, ref strCompareInfo) == -1)
                            {
                                this.fpItemList.Sheets[0].Rows[i].Label = i.ToString();
                                this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Regular;
                                this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = this.fpItemList.Sheets[0].ColumnHeader.Columns[0].ForeColor;
                            }
                            else
                            {
                                //ҽ�����
                                switch (compareItem.CenterItem.ItemGrade)
                                {
                                    case "1":
                                        this.fpItemList.Sheets[0].Rows[i].Label = "��";
                                        this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Bold;
                                        this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = Color.Red;
                                        break;
                                    case "2":
                                        this.fpItemList.Sheets[0].Rows[i].Label = "��";
                                        this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Bold;
                                        this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = Color.Red;
                                        break;
                                    default:
                                        this.fpItemList.Sheets[0].Rows[i].Label = i.ToString();
                                        this.fpItemList.Sheets[0].RowHeader.Rows[i].Font = font_Regular;
                                        this.fpItemList.Sheets[0].RowHeader.Rows[i].ForeColor = this.fpItemList.Sheets[0].ColumnHeader.Columns[0].ForeColor;
                                        break;
                                }
                            }
                        }
                    }
                    #endregion
                }
                this.freshRowCount += 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("DoFreshItemFlag" + ex.Message);
            }
            //}
        }

        /// <summary>
        /// ����ҽ��վˢ���б��ǣ��籣��ǣ��ķ�ʽ��0 ��ˢ�£�1 ʹ�ö��̣߳�2 ���̹߳���ˢ�£�3 ��ʼ�����أ����ݺ�ͬ��λ�����籣����У�
        /// </summary>
        int isUserThread = 0;

        /// <summary>
        /// ˢ�±��(ͣ�á�ҽ��)
        /// </summary>
        private void FreshItemFlag()
        {
            if (isUserThread == 0)
            {
                return;
            }

            if (!this.frmShowItem.Visible)
            {
                return;
            }

            this.freshRowCount = 0;
            this.isFreshFinish = false;

            if (isUserThread == 1)
            {
                if (this.autoRefreshTimer != null)
                {
                    this.autoRefreshEven = null;
                    this.autoRefreshTimer.Dispose();
                }

                if (this.autoRefreshCallBack == null)
                {
                    this.autoRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefreshTimerCallback);
                }
                this.autoRefreshTimer = new System.Threading.Timer(this.autoRefreshCallBack, null, 0, 500);
            }
            else if (isUserThread == 2)
            {
                DoFreshItemFlag();
            }
        }

        /// <summary>
        /// ���뷨���� 0 ƴ�� 1 ��� 2 �Զ�����
        /// </summary>
        public int InputType
        {
            get
            {
                return this.intQueryType;
            }
            set
            {
                this.intQueryType = value;
                ChangeQueryType();
            }
        }

        /// <summary>
        /// ���Ĺ������
        /// </summary>
        protected virtual void ChangeQueryType()
        {

            if (intQueryType > 3) intQueryType = 0;
            switch (intQueryType)
            {
                case 0:
                    QueryType = "ƴ������ʡ��Զ�����";
                    this.txtItemCode.BackColor = Color.FromArgb(255, 255, 255);
                    break;
                case 1:
                    QueryType = "ƴ����";
                    this.txtItemCode.BackColor = Color.FromArgb(255, 225, 225);
                    break;
                case 2:
                    this.txtItemCode.BackColor = Color.FromArgb(255, 200, 200);
                    QueryType = "�����";
                    break;
                case 3:
                    this.txtItemCode.BackColor = Color.FromArgb(255, 150, 150);
                    QueryType = "�Զ�����";
                    break;
                default:
                    this.txtItemCode.BackColor = Color.FromArgb(255, 255, 255);
                    QueryType = "ƴ������ʡ��Զ�����";
                    break;
            }
            this.toolTip1.SetToolTip(this.txtItemCode, "��ǰ���뷨Ϊ��" + this.QueryType + "\nF2�л����뷨��");

            this.toolTip1.InitialDelay = 0;
            this.toolTip1.ShowAlways = true;
            this.toolTip1.AutomaticDelay = 100;
            this.toolTip1.Active = true;

            frmShowItem.TipText = "��ǰ���뷨Ϊ��" + this.QueryType;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected void myShowList()
        {
            //{E68EC2D3-2E6B-4062-A194-9E3C88B1AA98}
            if (isFristShow)
            {
                this.isFristShow = false;
            }

            //��ʾ�б�
            if (this.bIsListShowAlways == false)
            {
                if (!frmShowItem.Visible)
                {
                    //�Ƿ���ʾ���ҳ�����Ŀѡ��
                    //if (isDeptUsedFlag)
                    //{
                    //    this.frmShowItem.showDeptUsedCheckBox();
                    //}
                    //else
                    //{
                    //    this.frmShowItem.hideDeptUsedCheckBox();
                    //}
                    //�޸�λ��
                    //frmItemList.Location = this.txtItemCode.PointToScreen(new Point(0, this.Bottom+50));
                    //frmItemList.Size = new Size(580, 400);

                    Point temPoint = this.txtItemCode.PointToScreen(new Point(0, this.Bottom + 50));
                    Point temPoint1 = this.txtItemCode.PointToScreen(new Point(0, this.Bottom));
                    //{D5DF4996-A960-471a-9B38-5B583C8D0B2D}
                    frmShowItem.Location = this.IsPackageInput ? new Point(temPoint1.X, temPoint1.Y) : new Point(temPoint.X, temPoint.Y);
                    frmShowItem.Size = new Size(705, 408);
                    frmShowItem.Show();
                    frmShowItem.TopMost = true;

                }
            }
        }

        /// <summary>
        /// ��ʾ�Ƴ�����Ŀ
        /// </summary>
        /// <param name="flag"></param>
        void frmItemList_isDeptUsedItem(bool flag)
        {
            if (flag)
            {
                this.isDeptUsedFlag = true;
                this.ChangeItem();
            }
            else
            {
                this.isDeptUsedFlag = false;
                this.ChangeItem();
            }
        }

        /// <summary>
        /// �Ƿ����ҽ�Ʒ�Χ��ҩ
        /// </summary>
        /// <param name="flag"></param>
        void frmItemList_companyRang(bool flag)
        {
            this.isCompanyRang = flag;
            this.ChangeItem();
        }

        /// <summary>
        /// �л����뷨{E68EC2D3-2E6B-4062-A194-9E3C88B1AA98}
        /// </summary>
        /// <param name="i"></param>
        void frmItemList_writeWay(int i)
        {
            this.intQueryType = i;
            ChangeQueryType();
        }

        /// <summary>
        /// �����Ŀ
        /// </summary>
        protected void mySelectedItem()
        {
            //TODO:ѡ����Ŀ
            try
            {
                if (this.bIsListShowAlways == false)
                {
                    if (this.frmShowItem != null)
                    {
                        this.frmShowItem.Hide();
                    }
                }
                
                int columnIndex = 0;
                for (int j = 0; j < this.fpItemList.Sheets[0].ColumnCount; j++)
                {
                    if (this.fpItemList.Sheets[0].ColumnHeader.Columns[j].Label == "ִ�п���")
                    {
                        columnIndex = j;
                        break;
                    }
                }

                FS.HISFC.Models.Base.Item item = null;
                FS.HISFC.Models.Fee.ComGroup group = null;

                for (int i = 0; i < this.alItem.Count; i++)
                {
                    item = this.alItem[i] as FS.HISFC.Models.Base.Item;
                    if (item == null)
                    {
                        if (this.alItem[i].GetType() == typeof(FS.HISFC.Models.Fee.ComGroup))
                        {
                            group = this.alItem[i] as FS.HISFC.Models.Fee.ComGroup;
                            if (group == null)
                            {
                                continue;
                            }

                            string ItemID = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 0].Text;//����
                            if (group.ID == ItemID)
                            {
                                item = new FS.HISFC.Models.Base.Item();
                                item.ID = group.ID;
                                item.Name = group.Name;
                                item.PriceUnit = "[����]";
                                this.txtItemName.Text = group.Name;

                                txtItemCode.TextChanged -= new EventHandler(txtItemCode_TextChanged);
                                this.txtItemCode.Text = string.Empty;
                                txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
                                //frmShowItem.Hide();
                                this.myFeeItem = item;

                                #region ҽ����Ӧ֢��ʾ
                                if (compareItem != null && !string.IsNullOrEmpty(compareItem.Practicablesymptomdepiction.Trim()))
                                {
                                    MessageBox.Show(Classes.Function.IItemCompareInfo.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                                if (compareItem != null && !string.IsNullOrEmpty(compareItem.Practicablesymptomdepiction.Trim()))
                                {
                                    MessageBox.Show("����Ŀ����ض�����ʹ�ã�\n" + compareItem.Practicablesymptomdepiction.Trim(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                #endregion

                                if (SelectedItem != null)
                                {
                                    SelectedItem(item);
                                }

                                return;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    if (item == null)
                    {
                        continue;
                    }

                    if (item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))//�ж���ҩƷ
                    {
                        //item.IsPharmacy = true;
                        item.ItemType = FS.HISFC.Models.Base.EnumItemType.Drug;
                    }
                    else if (item.GetType() == typeof(FS.HISFC.Models.Fee.Item.Undrug))//��ҩƷ
                    {
                        //item.IsPharmacy = false;
                        item.ItemType = FS.HISFC.Models.Base.EnumItemType.UnDrug;
                    }
                    else if (item.GetType() == typeof(FS.HISFC.Models.FeeStuff.MaterialItem))
                    {
                        item.ItemType = FS.HISFC.Models.Base.EnumItemType.MatItem;
                    }
                    else
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("����Ŀ���ͣ�") + item.GetType().ToString());
                        return;
                    }
                    this.myFeeItem = item;

                    if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)//ҩƷѡ��
                    {
                        string ItemID = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 0].Text;//����
                        string Dept = this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, columnIndex].Text;//��ҩ����

                        if (this.myFeeItem.ID == ItemID)//&& this.myFeeItem.User03 == Dept)//��ҩ������ͬ
                        {
                            if (this.myFeeItem.User03 == Dept)
                            {
                                this.txtItemName.Text = this.myFeeItem.Name;

                                txtItemCode.TextChanged -= new EventHandler(txtItemCode_TextChanged);
                                this.txtItemCode.Text = string.Empty;
                                txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
                                //frmShowItem.Hide();


                                //#region ҽ����Ӧ֢��ʾ
                                //if (Classes.Function.IItemExtendInfo.GetCompareItemInfo(this.myFeeItem.ID, ref compareItem) == -1)
                                //{
                                //    MessageBox.Show(Classes.Function.IItemExtendInfo.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //    return;
                                //}
                                //if (compareItem != null && !string.IsNullOrEmpty(compareItem.Practicablesymptomdepiction.Trim()))
                                //{
                                //    MessageBox.Show("����Ŀ����ض�����ʹ�ã�\n" + compareItem.Practicablesymptomdepiction.Trim(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //}
                                //#endregion

                                if (SelectedItem != null)
                                    SelectedItem(this.FeeItem);
                                return;
                            }
                        }

                    }
                    //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} ҽ�����İ����� by gengxl
                    else if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.MatItem)//����ѡ��
                    {
                        if (this.myFeeItem.ID == this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 0].Text
                            && item.Price == FrameWork.Function.NConvert.ToDecimal(this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 4].Text)) //������ͬ
                        {
                            this.txtItemName.Text = this.myFeeItem.Name;

                            txtItemCode.TextChanged -= new EventHandler(txtItemCode_TextChanged);
                            this.txtItemCode.Text = string.Empty;
                            txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
                            //frmShowItem.Hide();


                            //#region ҽ����Ӧ֢��ʾ
                            //if (Classes.Function.IItemExtendInfo.GetCompareItemInfo(this.myFeeItem.ID, ref compareItem) == -1)
                            //{
                            //    MessageBox.Show(Classes.Function.IItemExtendInfo.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    return;
                            //}
                            //if (compareItem != null && !string.IsNullOrEmpty(compareItem.Practicablesymptomdepiction.Trim()))
                            //{
                            //    MessageBox.Show("����Ŀ����ض�����ʹ�ã�\n" + compareItem.Practicablesymptomdepiction.Trim(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //#endregion

                            if (SelectedItem != null)
                                SelectedItem(this.FeeItem);
                            return;
                        }
                    }
                    else//��ҩƷѡ��
                    {
                        if (this.myFeeItem.ID == this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 0].Text) //������ͬ
                        {
                            this.txtItemName.Text = this.myFeeItem.Name;
                            txtItemCode.TextChanged -= new EventHandler(txtItemCode_TextChanged);
                            this.txtItemCode.Text = string.Empty;
                            txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
                            //frmShowItem.Hide();

                            //#region ҽ����Ӧ֢��ʾ
                            //if (Classes.Function.IItemExtendInfo.GetCompareItemInfo(this.myFeeItem.ID, ref compareItem) == -1)
                            //{
                            //    MessageBox.Show(Classes.Function.IItemExtendInfo.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    return;
                            //}
                            //if (compareItem != null && !string.IsNullOrEmpty(compareItem.Practicablesymptomdepiction.Trim()))
                            //{
                            //    MessageBox.Show("����Ŀ����ض�����ʹ�ã�\n" + compareItem.Practicablesymptomdepiction.Trim(), "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //}
                            //#endregion

                            if (SelectedItem != null)
                                SelectedItem(this.FeeItem);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("mySelectedItem" + ex.Message);
            }

            MessageBox.Show("error û���ҵ� " + this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 0].Text
                + "1" + this.fpItemList.Sheets[0].Cells[this.fpItemList.Sheets[0].ActiveRowIndex, 1].Text);
        }

        /// <summary>
        /// �仯������
        /// </summary>
        /// <param name="isDept"></param>
        public void ChangeDataSet(bool isDept)
        {
            if (isDept)
            {
                fpItemList.Sheets[0].DataSource = dvDeptUsed;
                frmShowItem.DataView = dvDeptUsed;
            }
            else
            {
                fpItemList.Sheets[0].DataSource = MyDataView;
                frmShowItem.DataView = MyDataView;
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ˫��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e != null)
            {
                this.mySelectedItem();
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void fpSpread1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtItemCode.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                frmShowItem.Hide();
                this.txtItemCode.Focus();
            }
            else
            {
                if (e.KeyCode.ToString().Length <= 1)
                {
                    txtItemCode.TextChanged -= new EventHandler(txtItemCode_TextChanged);
                    this.txtItemCode.Text = this.txtItemCode.Text + e.KeyCode.ToString();
                    txtItemCode.TextChanged += new EventHandler(txtItemCode_TextChanged);
                }
                this.txtItemCode.Focus();
            }
        }

        /// <summary>
        /// �ر�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void frmItemList_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            frmShowItem.Hide();
            this.txtItemName.Focus();
        }

        /// <summary>
        /// ������Ŀ�б��Ƿ�ɼ�
        /// </summary>
        /// <param name="visible"></param>
        public void SetVisibleForms(bool visible)
        {
            this.frmShowItem.Visible = visible;
        }

        /// <summary>
        /// ���ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cbCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            try
            {
                if (this.frmShowItem.Visible)
                {
                    if (keyData == Keys.Up)
                    {
                        return true;
                    }
                    else if (keyData == Keys.Down)
                    {
                        return true;
                    }
                    else
                    {
                        return base.ProcessCmdKey(ref msg, keyData);
                    }
                }
            }
            catch { }

            return base.ProcessCmdKey(ref msg, keyData);
            //    return true;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void txtItemCode_KeyUp(object sender, KeyEventArgs e)
        {
            //return;
            try
            {
                if (fpItemList.Sheets[0].ActiveRowIndex > 9)
                {
                    fpItemList.SetViewportTopRow(0, fpItemList.Sheets[0].ActiveRowIndex - 9);
                }
                if (e.KeyCode == Keys.Up)
                {
                    fpItemList.Sheets[0].ActiveRowIndex--;
                    fpItemList.Sheets[0].AddSelection(fpItemList.Sheets[0].ActiveRowIndex, 0, 1, 1);
                    fpItemList.Focus();
                }
                else if (e.KeyCode == Keys.Down)
                {
                    fpItemList.Sheets[0].ActiveRowIndex++;
                    fpItemList.Sheets[0].AddSelection(fpItemList.Sheets[0].ActiveRowIndex, 0, 1, 1);
                    fpItemList_SelectionChanged(fpItemList, null);
                    fpItemList.Focus();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (fpItemList.Sheets[0].Rows.Count > 0 && fpItemList.Sheets[0].ActiveRowIndex >= 0 && this.fpItemList.Visible)
                    {
                        mySelectedItem();
                    }
                }
                else if (e.KeyCode == Keys.F3)//��ʾѡ����Ŀ����
                {
                    if (this.bIsListShowAlways == false)
                    {
                        if (this.frmShowItem != null) this.frmShowItem.Hide();
                    }
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    frmShowItem.Hide();
                }
                //�任���뷨
                else if (e.KeyCode == Keys.F2)
                {
                    intQueryType++;
                    try
                    {
                        ChangeQueryType();//raiseevent �任���뷨

                        if (this.FindForm().Visible) System.Windows.Forms.Cursor.Position = this.txtItemCode.PointToScreen(new Point(this.panel2.Left + this.txtItemCode.Width - 2, this.panel2.Top));
                    }
                    catch { }
                }
            }
            catch { }
        }

        bool bCanChange = true;
        /// <summary>
        /// �ı��仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void txtItemCode_TextChanged(object sender, System.EventArgs e)
        {
            if (bCanChange == false)
            {
                return;
            }
            if (this.txtItemCode.Text.StartsWith("@"))
            {
                this.frmShowItem.Visible = false;
                return;
            }

            this.ChangeItem();
            this.txtItemCode.SelectionStart = this.txtItemCode.Text.Length;
            this.txtItemCode.Focus();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmbCategory_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtItemCode.Focus();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void cmbCategory_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.txtItemCode.Focus();
            try
            {
                if (frmShowItem != null && this.frmShowItem.Visible)
                {
                    this.ChangeItem();
                }
                if (this.IsListShowAlways)
                {
                    this.ChangeItem();
                }

                if (this.CatagoryChanged != null)
                {
                    this.CatagoryChanged(this.cmbCategory.alItems[this.cmbCategory.SelectedIndex] as FS.FrameWork.Models.NeuObject);
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void txtItemName_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (this.txtItemName.Text.Trim() == string.Empty)
                {
                    return;
                }
                FS.HISFC.Models.Base.Item item;
                if (this.cmbCategory.Tag.ToString().Substring(0, 1) == "P")
                {
                    FS.HISFC.Models.Pharmacy.Item obj = new FS.HISFC.Models.Pharmacy.Item();
                    item = obj;
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug obj = new FS.HISFC.Models.Fee.Item.Undrug();
                    item = obj;
                    obj.Qty = 1.0M;
                    obj.PriceUnit = "��";
                }

                if (this.cmbCategory.Text == "ȫ��" || cmbCategory.Tag.ToString() == "ALL")
                {
                    MessageBox.Show("��ѡ����Ŀ���");
                    return;
                }

                item.ID = "999";//�Զ���
                item.SysClass.ID = this.cmbCategory.Tag.ToString();

                //if (item.IsPharmacy)
                if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    try
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)item).Type.ID = item.SysClass.ID.ToString().Substring(item.SysClass.ID.ToString().Length - 1, 1);

                    }
                    catch { }
                }
                if (this.bIsShowSelfMark && this.cmbCategory.Tag.ToString().Substring(0, 1) == "P")//���Ա�ҩ����
                {
                    if (this.txtItemName.Text.TrimEnd().Length > 4)
                    {
                        if (this.txtItemName.Text.TrimEnd().Substring(this.txtItemName.Text.TrimEnd().Length - SelfMark.Length) == SelfMark)
                        {
                            item.Name = this.txtItemName.Text;//���Ա�ҩ����
                            try
                            {
                                this.myFeeItem = item;
                                if (SelectedItem != null)
                                    SelectedItem(item);
                            }
                            catch { }
                            return;
                        }
                    }

                    item.Name = this.txtItemName.Text + SelfMark;
                }
                else
                {
                    item.Name = this.txtItemName.Text;//���Ա�ҩ����
                }
                try
                {
                    this.myFeeItem = item;
                    if (SelectedItem != null) SelectedItem(item);
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void txtItemCode_Enter(object sender, EventArgs e)
        {
            this.txtItemCode.SelectAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void txtItemCode_Leave(object sender, EventArgs e)
        {
            if (frmShowItem != null && frmShowItem.Visible)
            {
                frmShowItem.Hide();
            }
        }
        #endregion

        /// <summary>
        /// ��ʼ����ϸ��Ϣ��ʾ
        /// </summary>
        /// {46983F5B-E184-4b8b-B819-AA1C34993F1B} �޸�Ϊprotected
        protected void initFPdetail()
        {
            if (this.fpItemDetal.Sheets.Count <= 0)
            {
                this.fpItemDetal.Sheets.Add(new FarPoint.Win.Spread.SheetView());
            }

            fpItemDetal.VerticalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.AsNeeded;
            fpItemDetal.HorizontalScrollBarPolicy = FarPoint.Win.Spread.ScrollBarPolicy.Never;

            if (this.bIsListShowAlways == false)
            {
                frmShowItem.AddBottomControl(fpItemDetal);
                frmShowItem.AddBottomControl(txtNote);
            }
            else
            {
                fpItemDetal.Dock = System.Windows.Forms.DockStyle.Fill;
                //���޸�
            }

            fpItemDetal.Size = new Size(0, 0);
            fpItemDetal.Show();
            fpItemDetal.Sheets[0].OperationMode = FarPoint.Win.Spread.OperationMode.SingleSelect;

            //�޸�����ҽ��������Ŀ��ʾ����{8CA036D8-1BA8-4031-A71A-9591EA8B0ACA}
            fpItemDetal.Sheets[0].DataAutoCellTypes = true;
            fpItemDetal.Sheets[0].DataAutoSizeColumns = false;
            fpItemDetal.Sheets[0].ColumnHeaderVisible = true;
            fpItemDetal.Sheets[0].Columns.Count = 8;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[0].Label = "��Ŀ����";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[0].Width = 0;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[1].Label = "����";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[1].Width = 100;

            fpItemDetal.Sheets[0].ColumnHeader.Columns[2].Label = "����";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[2].Width = 220;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[3].Label = "�۸�";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[3].Width = 40;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[4].Label = "����";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[4].Width = 40;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[5].Label = "ҽ������ҽ�����";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[5].Width = 300;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[6].Label = "��Ч";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[6].Width = 40;
            fpItemDetal.Sheets[0].ColumnHeader.Columns[7].Label = "��Ӧ֢��Ϣ";
            fpItemDetal.Sheets[0].ColumnHeader.Columns[7].Width = 200;
            fpItemDetal.Sheets[0].Columns[6].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

            #region ע������

            lblMemo.Text = string.Empty;
            lblMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            PanelItemMemo.Controls.Add(lblMemo);
            //frmShowItem.AddBottomMemoControl(PanelItemMemo);

            #endregion
        }

        FS.HISFC.BizLogic.Pharmacy.Item phaManger = new FS.HISFC.BizLogic.Pharmacy.Item();
        FS.HISFC.BizProcess.Integrate.Fee itemMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ���ҽ����Ŀ�б�
        /// </summary>
        private static Hashtable hsSIItemList = new Hashtable();

        /// <summary>
        /// ѡ����Ŀ�仯
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpItemList_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            //ѡ��仯��ˢ���շ���Ŀ�б���ʾ
            if (this.IsListShowAlways == true)
            {
                return; //ȫ����ʾʱ�����⴦��
            }

            if (fpItemList.Sheets[0].RowCount <= 0)
            {
                this.fpItemDetal.Sheets[0].Rows.Count = 0;
                return;
            }

            //��ʾ�շ���Ŀ��Ϣ
            this.fpItemDetal.Sheets[0].Rows.Count = 0;
            this.fpItemDetal.Sheets[0].SheetCornerStyle.BackColor = Color.YellowGreen;

            //string itmExtendInfo = "";
            ArrayList alExtendInfo = new ArrayList();

            string itemid = fpItemList.Sheets[0].Cells[fpItemList.Sheets[0].ActiveRowIndex, 0].Text;

            FS.HISFC.Models.Pharmacy.Item itm = null;
            FS.HISFC.Models.Fee.Item.Undrug itemtmp = null;
            itm = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(itemid);
            if (itm == null)
            {
                itemtmp = SOC.HISFC.BizProcess.Cache.Fee.GetItem(itemid);
            }
            #region ҩƷ�ͷ�ҩƷ���Ǹ�����Ŀ��

            if ((itm != null && itm.ID != "")
                || itemtmp != null && itemtmp.UnitFlag != "1")
            {
                if (Classes.Function.IItemCompareInfo != null
                    && patient != null && !string.IsNullOrEmpty(patient.ID)
                    && (patient.Pact != null && !string.IsNullOrEmpty(patient.Pact.ID)))
                {
                    FS.HISFC.Models.Base.Item item = null;
                    if (itm != null)
                    {
                        item = itm;
                    }
                    else
                    {
                        item = itemtmp;
                    }

                    FS.HISFC.Models.SIInterface.Compare compare = null;

                    string strCompareInfo = string.Empty;

                    int iRtn = Classes.Function.IItemCompareInfo.GetCompareItemInfo(item, patient.Pact, ref compare, ref strCompareInfo);
                    if (iRtn == -1)
                    {
                        MessageBox.Show("fpItemList_SelectionChanged" + Classes.Function.IItemCompareInfo.ErrInfo);
                        return;
                    }

                    //Classes.Function.IItemExtendInfo.GetCompareItemInfo(item

                    if (string.IsNullOrEmpty(strCompareInfo))
                    {
                        this.fpItemDetal.Sheets[0].RowCount = 0;
                        fpItemDetal.Visible = false;
                    }
                    else
                    {
                        this.fpItemDetal.Sheets[0].RowCount = 5;
                        txtNote.Multiline = true;
                        txtNote.Text = strCompareInfo;
                        txtNote.ReadOnly = true;
                        txtNote.Multiline = true;
                        txtNote.ForeColor = Color.LightCoral;
                        txtNote.Visible = true;
                        txtNote.ScrollBars = ScrollBars.Both;
                        fpItemDetal.Visible = false;
                    }
                }
                else
                {
                    this.fpItemDetal.Sheets[0].RowCount = 0;
                    fpItemDetal.Visible = false;
                }
            }
            #endregion
            else
            {
                //�޸�����ҽ��������Ŀ��ʾ����{8CA036D8-1BA8-4031-A71A-9591EA8B0ACA}
                lstzt = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();

                //��ҩƷ
                if (itemtmp != null && itemtmp.UnitFlag == "1")
                {
                    lstzt = SOC.HISFC.BizProcess.Cache.Fee.GetUndrugZTDetail(itemtmp.ID);

                    if (lstzt == null)
                    {
                        if (this.ztManager.QueryUnDrugztDetail(itemtmp.ID, ref lstzt) == -1)
                        {
                            MessageBox.Show("fpItemList_SelectionChanged" + this.ztManager.Err);
                            return;
                        }
                    }
                    if (lstzt.Count == 0)
                    {
                        return;
                    }

                    if (lstzt == null || lstzt.Count == 0)
                    {
                        this.fpItemDetal.Sheets[0].RowCount = 0;

                        txtNote.Visible = false;
                        this.fpItemDetal.Visible = true;
                        frmShowItem.Memo = this.lblMemo.Text;
                        frmShowItem.ResizeBottom();
                        return;
                    }

                    this.fpItemDetal.Sheets[0].RowCount = lstzt.Count;
                    string undruggrade = null;
                    for (int j = 0; j < lstzt.Count; j++)
                    {
                        FS.HISFC.Models.Fee.Item.UndrugComb obj = lstzt[j];

                        this.fpItemDetal.Sheets[0].Rows[j].ForeColor = this.fpItemDetal.Sheets[0].RowHeader.Rows[j].ForeColor;
                        try
                        {
                            this.fpItemDetal.Sheets[0].Cells[j, 0].Text = ((FS.HISFC.Models.Fee.Item.Undrug)this.itemHelper.GetObjectFromID(obj.ID)).UserCode;
                        }
                        catch
                        {
                            this.fpItemDetal.Sheets[0].Cells[j, 0].Text = obj.ID;
                        }
                        this.fpItemDetal.Sheets[0].Cells[j, 1].Text = obj.UserCode;
                        this.fpItemDetal.Sheets[0].Cells[j, 2].Text = obj.Name;
                        this.fpItemDetal.Sheets[0].Cells[j, 3].Value = obj.Price;
                        this.fpItemDetal.Sheets[0].Cells[j, 4].Value = obj.Qty;

                        if (Classes.Function.IItemCompareInfo != null
                            && patient != null
                            && patient.Pact != null)
                        {
                            string strCompareInfo = string.Empty;
                            FS.HISFC.Models.Base.Item item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(obj.ID);
                            FS.HISFC.Models.SIInterface.Compare compare = null;
                            int iRtn = Classes.Function.IItemCompareInfo.GetCompareItemInfo(obj, patient.Pact, ref compare, ref strCompareInfo);
                            if (iRtn == -1)
                            {
                                MessageBox.Show("fpItemList_SelectionChanged" + Classes.Function.IItemCompareInfo.ErrInfo);
                                return;
                            }
                            this.fpItemDetal.Sheets[0].Cells[j, 5].Text = strCompareInfo;


                            /*
                            int iRtn = Classes.Function.IItemExtendInfo.GetCompareItemInfo(obj.ID, ref compareItem);
                            if (iRtn == -1)
                            {
                                MessageBox.Show(Classes.Function.IItemExtendInfo.ErrInfo);
                                return;
                            }
                            else if (compareItem == null)
                            {
                                //ҽ�����
                                this.fpItemDetal.Sheets[0].Cells[j, 5].Text = "�Ǳ���";
                            }
                            else
                            {
                                this.fpItemDetal.Sheets[0].Cells[j, 5].Text = FS.HISFC.BizLogic.Fee.Interface.ShowItemGradeByCode(compareItem.CenterItem.ItemGrade);

                                this.fpItemDetal.Sheets[0].Cells[j, 7].Text = compareItem.Practicablesymptomdepiction;
                                this.fpItemDetal.Sheets[0].Rows[j].ForeColor = Color.LightCoral;
                            }
                            */ 
                        }

                        this.fpItemDetal.Sheets[0].Cells[j, 6].Value = obj.ValidState == "��Ч" || obj.ValidState == "1" ? true : false;
                        if (this.fpItemDetal.Sheets[0].Cells[j, 6].Value.ToString().ToLower() == "false")
                        {
                            this.fpItemDetal.Sheets[0].Rows[j].BackColor = Color.LightCoral;
                            this.fpItemDetal.Sheets[0].Rows[j].Visible = false;
                        }
                        txtNote.Visible = false;
                        fpItemDetal.Visible = true;
                    }
                }
            }
            //���ע������
            lblMemo.Text = string.Empty;
            if (itemtmp != null)
            {
                FS.FrameWork.Models.NeuObject apply = interMgr.GetConstansObj("ApplyBillClass", itemtmp.CheckApplyDept);
                if (!string.IsNullOrEmpty(apply.Memo))
                {
                    lblMemo.Text = "[ע������]" + apply.Memo;
                    this.PanelItemMemo.Size = new Size(700, 30);
                }
                else
                {
                    this.PanelItemMemo.Size = new Size(700, 0);
                }
            }
            frmShowItem.Memo = this.lblMemo.Text;
            frmShowItem.ResizeBottom();
        }

        #region IInterfaceContainer ��Ա
        //���ӽӿ�����{112B7DB5-0462-4432-AD9D-17A7912FFDBE} 
        public Type[] InterfaceTypes
        {
            get
            {
                Type[] t = new Type[2];
                t[0] = typeof(FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade);
                t[1] = typeof(FS.HISFC.BizProcess.Interface.Common.IItemCompareInfo);
                return t;
            }
        }

        #endregion

        /// <summary>
        /// ��С���ð�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper minFeeHelper = null;

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!txtItemCode.Text.StartsWith("@"))
                    {
                        return;
                    }
                    if (this.txtItemCode.Text.TrimStart('@') == string.Empty)
                    {
                        return;
                    }

                    FS.HISFC.Models.Base.Item item;

                    if (txtItemCode.Text.StartsWith("@"))
                    {
                        if (this.cmbCategory.Text == "ȫ��")
                        {
                            this.frmShowItem.Visible = false;
                            MessageBox.Show("��ѡ����Ŀ���");
                            return;
                        }


                    }

                    if (this.cmbCategory.Tag.ToString().Substring(0, 1) == "P")
                    {
                        FS.HISFC.Models.Pharmacy.Item obj = new FS.HISFC.Models.Pharmacy.Item();
                        item = obj;
                    }
                    else
                    {
                        FS.HISFC.Models.Fee.Item.Undrug obj = new FS.HISFC.Models.Fee.Item.Undrug();
                        item = obj;
                        obj.Qty = 1.0M;
                        obj.PriceUnit = "��";
                    }

                    if (txtItemCode.Text.StartsWith("@"))
                    {
                        item.SysClass.ID = this.cmbCategory.Tag.ToString();
                        item.ID = "999";
                    }

                    txtItemCode.TextChanged -= this.txtItemCode_TextChanged;
                    this.txtItemCode.Text = txtItemCode.Text.Replace("@", "");
                    txtItemCode.TextChanged += this.txtItemCode_TextChanged;

                    if (item.ID == "999")
                    {
                        //������Ա�ҩ��Ҫ������ñ� ����������Һ
                        //������С�������ȡһ��
                        if (minFeeHelper == null)
                        {
                            minFeeHelper = new FS.FrameWork.Public.ObjectHelper();
                            FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                            minFeeHelper.ArrayObject = inteMgr.GetConstantList("MINFEE");
                        }

                        if (minFeeHelper.ArrayObject != null && minFeeHelper.ArrayObject.Count > 0)
                        {
                            item.MinFee = (FS.FrameWork.Models.NeuObject)minFeeHelper.ArrayObject[0];
                        }
                        else
                        {
                            item.MinFee.ID = "001";
                            item.MinFee.Name = "��ҩ��";
                        }
                    }

                    if (item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        ((FS.HISFC.Models.Pharmacy.Item)item).Type.ID = item.SysClass.ID.ToString().Substring(item.SysClass.ID.ToString().Length - 1, 1);
                    }

                    if (item.ID == "999")
                    {
                        if (this.bIsShowSelfMark
                            && this.cmbCategory.Tag.ToString().Substring(0, 1) == "P")//���Ա�ҩ����
                        {
                            item.Name = this.txtItemCode.Text + SelfMark;//���Ա�ҩ����
                        }
                        else
                        {
                            item.Name = this.txtItemCode.Text + "[����]";//���Ա�ҩ����
                        }
                    }
                    else
                    {
                        item.Name = this.txtItemName.Text;
                    }

                    txtItemCode.TextChanged -= this.txtItemCode_TextChanged;
                    this.txtItemCode.Text = "";
                    txtItemCode.TextChanged += this.txtItemCode_TextChanged;
                    if (this.frmShowItem.Visible)
                    {
                        this.frmShowItem.Visible = false;
                    }

                    this.myFeeItem = item;
                    if (SelectedItem != null)
                    {
                        SelectedItem(item);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("txtItemCode_KeyDown" + ex.Message);
                }
            }
        }

        private void cmbCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtItemCode.Focus();
            }
        }
    }



    /// <summary>
    /// ��Ŀ�б�������
    /// </summary>
    public enum EnumMainColumnSet
    {
        /// <summary>
        /// ����
        /// </summary>
        ItemCode,

        /// <summary>
        /// ����
        /// </summary>
        ItemName,

        /// <summary>
        /// ϵͳ�������
        /// </summary>
        SysClassName,

        /// <summary>
        /// ���
        /// </summary>
        Specs,

        /// <summary>
        /// �۸�
        /// </summary>
        Price,

        /// <summary>
        /// ��λ
        /// </summary>
        Unit,

        /// <summary>
        /// �籣���
        /// </summary>
        SiFlag,

        /// <summary>
        /// ��������
        /// </summary>
        Product,

        /// <summary>
        /// ϵͳ������
        /// </summary>
        SysClassCode,

        /// <summary>
        /// ƴ����
        /// </summary>
        SpellCode,

        /// <summary>
        /// �����
        /// </summary>
        WBCode,

        /// <summary>
        /// �Զ�����
        /// </summary>
        UserCode,

        /// <summary>
        /// ͨ����
        /// </summary>
        RegularName,

        /// <summary>
        /// ͨ����ƴ����
        /// </summary>
        RegularNameSpellCode,

        /// <summary>
        /// ͨ���������
        /// </summary>
        RegularNameWBCode,

        /// <summary>
        /// ͨ�����Զ�����
        /// </summary>
        RegularNameUserCode,

        /// <summary>
        /// Ӣ������
        /// </summary>
        EnglishName,

        /// <summary>
        /// ʣ��������
        /// </summary>
        StorageQty,

        /// <summary>
        /// ִ�п���
        /// </summary>
        ExecDept,

        /// <summary>
        /// ��鲿λ
        /// </summary>
        CheckBody,

        /// <summary>
        /// ��������
        /// </summary>
        DiseaseType,

        /// <summary>
        /// ר������
        /// </summary>
        SpecialDept,

        /// <summary>
        /// ��ʷ�����
        /// </summary>
        MedicalRecord,

        /// <summary>
        /// ���Ҫ��
        /// </summary>
        CheckRequest,

        /// <summary>
        /// ע������
        /// </summary>
        Notice,

        /// <summary>
        /// ȱҩ��ͣ�ñ��
        /// </summary>
        LackFlag
    }

    /// <summary>
    /// ��ʾ���
    /// </summary>
    public enum EnumCategoryType
    {
        /// <summary>
        /// ��Ŀ���
        /// </summary>
        ItemType = 0,

        /// <summary>
        /// ϵͳ���
        /// </summary>
        SysClass = 2
    }

    /// <summary>
    /// ������Ŀ���
    /// </summary>
    public enum EnumShowItemType
    {
        /// <summary>
        /// ҩƷ
        /// </summary>
        Pharmacy,

        /// <summary>
        /// ��ҩƷ
        /// </summary>
        Undrug,

        /// <summary>
        /// ȫ��
        /// </summary>
        All,

        /// <summary>
        /// �Ƴ���
        /// </summary>
        DeptItem,

        /// <summary>
        /// ����ҩƷ
        /// </summary>
        OutPharmacy
    }

    /// <summary>
    /// ��ҩƷ����:���סԺ��ȫ��
    /// </summary>
    public enum EnumUndrugApplicabilityarea
    {
        /// <summary>
        /// ����
        /// </summary>
        All = 0,

        /// <summary>
        /// ����
        /// </summary>
        Clinic = 1,

        /// <summary>
        /// סԺ
        /// </summary>
        InHos = 2
    }
}

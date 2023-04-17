using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.Pharmacy.Plan
{
    /*
     * ��ʷ�ɹ���Ϣ ��Ҫȡ�ɹ�����״̬Ϊ"2" "3"��  ԭ����������
    */
    /// <summary>
    /// [��������: ҩƷ�ɹ��ƻ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-12]<br></br>
    /// <�޸ļ�¼>
    ///    1.����BUG��ҩƷ��ӡ�ӿڵ��ó���Bug�������ͬʱ�������ƻ��Ͳɹ��ƻ������й���ӡ������£�
    ///      �л�������ӡ�ͻ������һ����ӡ�ӿ�ʵ�� by Sunjh 2010-8-26 {D78A574D-59BE-491b-808C-38DCD26BA5EA}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucStockPlan : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer,
                                        FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucStockPlan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ʵ��ת�� �����ƻ�ʵ��ת��Ϊ�ɹ��ƻ�ʵ��
        /// </summary>
        /// <param name="inPlanObj">���ƻ�ʵ��</param>
        /// <returns>�ɹ����زɹ��ƻ�ʵ�� ʧ�ܷ���null</returns>
        public static FS.HISFC.Models.Pharmacy.StockPlan ConvertPlanType(FS.HISFC.Models.Pharmacy.InPlan inPlanObj)
        {
            FS.HISFC.Models.Pharmacy.StockPlan stockPlanObj = new FS.HISFC.Models.Pharmacy.StockPlan();

            stockPlanObj.BillNO = inPlanObj.BillNO;             //���ݺ�
            stockPlanObj.Dept = inPlanObj.Dept;                 //����
            stockPlanObj.Item = inPlanObj.Item;                 //ҩƷ��Ϣ
            stockPlanObj.StoreQty = inPlanObj.StoreQty;         //���ƿ��
            stockPlanObj.StoreTotQty = inPlanObj.StoreTotQty;   //ȫԺ���
            stockPlanObj.OutputQty = inPlanObj.OutputQty;       //��������
            stockPlanObj.PlanQty = inPlanObj.PlanQty;           //�ƻ���
            stockPlanObj.PlanOper = inPlanObj.PlanOper;         //�ƻ���
            stockPlanObj.Oper = inPlanObj.Oper;                 //������
            stockPlanObj.Extend = inPlanObj.Extend;             //��ע 

            return stockPlanObj;
        }


        #region ����

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ������λ��Ϣ
        /// </summary>
        private ArrayList alCompany = null;

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        private ArrayList alProduce = null;

        /// <summary>
        /// ������˾������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHelper = null;

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper produceHelper = null;

        /// <summary>
        /// �ɹ��Ƿ���Ҫ���
        /// </summary>
        private bool isNeedApprove = true;

        /// <summary>
        /// �Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����
        /// </summary>
        private bool isUseDefaultStockData = true;

        /// <summary>
        /// ���ڹ���
        /// </summary>
        private EnumWindowFun winFun = EnumWindowFun.�ɹ��ƻ�;

        /// <summary>
        /// ��ʷ�ɹ���¼
        /// </summary>
        private ArrayList alPlanHistory = new ArrayList();

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ��������
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// ��ǰ�����ɹ�����
        /// </summary>
        private string nowOperBillNO = "";

        /// <summary>
        /// ��ǰ����������λ
        /// </summary>
        private string nowCompanyNO = "";

        /// <summary>
        /// �ɹ��ƻ�����
        /// </summary>
        private System.Collections.Hashtable hsStockPlan = new Hashtable();

        /// <summary>
        /// �ɹ����ʱ �Ƿ������޸���Ӧ�Ĳɹ���Ϣ
        /// </summary>
        private bool isCanEditWhenApprove = false;

        /// <summary>
        /// �Ƿ������޸ļƻ������
        /// </summary>
        private bool isCanEditPrice = true;

        #endregion

        #region ����

        /// <summary>
        /// �������
        /// </summary>
        [Description("������� ���ݲ�ͬҽԺ��������"), Category("����"), DefaultValue("���ƻ���")]
        public string Title
        {
            get
            {
                return this.lbTitle.Text;
            }
            set
            {
                this.lbTitle.Text = value;
            }
        }

        /// <summary>
        /// �ɹ��Ƿ���Ҫ��� 
        /// </summary>
        [Description("�ɹ��ƻ�ָ�����Ƿ���Ҫ�ɹ����"), Category("����"), DefaultValue(true),Browsable(false)]
        public bool IsNeedApprove
        {
            get
            {
                return this.isNeedApprove;
            }
            set
            {
                this.isNeedApprove = value;
            }
        }

        /// <summary>
        /// ���ڹ���
        /// </summary>
        [Description("���ڹ���"), Category("����")]
        public EnumWindowFun WindowFun
        {
            get
            {
                return winFun;
            }
            set
            {
                this.winFun = value;

                if (value == EnumWindowFun.�ɹ��ƻ�)            //��ʱ�����޸�������� / ����� / ������˾
                {
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColApproveNum].Locked = false;
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = false;
                    
                    //this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColCompany].Locked = false;
                    //{E0ED3F4F-F895-4cc6-B6A6-B1EFFABBC5DA}
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColCompany].Locked = true;
                }
                else
                {
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColApproveNum].Locked = true;
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = true;
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColCompany].Locked = true;
                }
            }
        }

        /// <summary>
        /// �Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����
        /// </summary>
        [Description("�ɹ�ָ��ʱ�Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool UseDefaultStockData
        {
            get
            {
                return this.isUseDefaultStockData;
            }
            set
            {
                this.isUseDefaultStockData = value;
            }
        }

        /// <summary>
        /// �ɹ����ʱ �Ƿ������޸���Ӧ�Ĳɹ���Ϣ
        /// </summary>
        [Description("�ɹ����ʱ �Ƿ������޸���Ӧ�Ĳɹ���Ϣ"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsCanEditWhenApprove
        {
            get
            {
                return this.isCanEditWhenApprove;
            }
            set
            {
                this.isCanEditWhenApprove = value;
            }
        }

        /// <summary>
        /// �Ƿ������޸ļƻ������
        /// </summary>
        [Description("�Ƿ������޸ļƻ������"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool IsCanEditPrice
        {
            get
            {
                return this.isCanEditPrice;
            }
            set
            {
                this.isCanEditPrice = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�� �� ��", "���üƻ����б� �ɽ��в�������", FS.FrameWork.WinForms.Classes.EnumImageList.Z����, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "�鿴ҩƷ��������Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("�ɹ����", "��ĳ��ҩƷ���Ϊ�����ɹ���Ϣ", FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.ShowInPlanList();
            }
            if (e.ClickedItem.Text == "�� �� ��")
            {
                this.PopExpandData();
            }
            if (e.ClickedItem.Text == "�ɹ����")
            {
                this.SplitPlan();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.SaveStockPlan() == 1)
            {
                this.ShowList();
            }
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();
            ArrayList alSavePlanList = new ArrayList();

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.StockPlan stockPlan = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.StockPlan;
                if (stockPlan == null)
                {
                    MessageBox.Show(Language.Msg("����ɹ��ƻ�����ʱ ��������ת������"));
                    return -1;
                }

                stockPlan.Oper.ID = this.itemManager.Operator.ID;
                stockPlan.Oper.OperTime = sysTime;                      //������Ա��Ϣ

                stockPlan.StockPrice = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text);     //ҩƷ�����
                stockPlan.StockApproveQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveNum].Text) * stockPlan.Item.PackQty;   //�ɹ�����

                stockPlan.Item.PriceCollection.PurchasePrice = stockPlan.StockPrice;        //ҩƷ����� ��ֵΪ ҩƷ�ƻ������
                stockPlan.StockOper = stockPlan.Oper;                                       //�ɹ���

                stockPlan.ApproveOper = stockPlan.Oper;

                alSavePlanList.Add(stockPlan);
            }

            return this.Print(alSavePlanList, false);
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Export()
        {
            try
            {
                if (this.neuSpread1.Export() == 1)
                {
                    MessageBox.Show(Language.Msg("�����ɹ�"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.IsNeedApprove = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Stock_Need_Approve, true, true);
            this.IsCanEditWhenApprove = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Stock_Edit_InPlan, true, false);
            this.IsCanEditPrice = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Stock_Edit_Price, true, true);
            this.UseDefaultStockData = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Stock_Use_DefaultData, true, true);
        }

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitData()
        {
            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            this.fpSpread2_Sheet1.DefaultStyle.Locked = true;           

            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.neuSpread1_Sheet1.DefaultStyle.Locked = true;

            #region ��ȡ��������/������˾������

            //��ù�����˾�б�
            if (this.alCompany == null)
            {
                FS.HISFC.BizLogic.Pharmacy.Constant phaConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
                this.alCompany = phaConstant.QueryCompany("1");
                if (this.alCompany == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ������λ�б����"));
                    return -1;
                }

                this.companyHelper = new FS.FrameWork.Public.ObjectHelper(this.alCompany);
            }
            if (this.alProduce == null)
            {
                FS.HISFC.BizLogic.Pharmacy.Constant phaConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
                this.alProduce = phaConstant.QueryCompany("0");
                if (this.alProduce == null)
                {
                    MessageBox.Show(Language.Msg("��ȡ���������б����"));
                    return -1;
                }
                this.produceHelper = new FS.FrameWork.Public.ObjectHelper(this.alProduce);
            }

            #endregion

            this.InitControlParam();

            //�ɹ��ƻ� �����޸��������/������˾
            //�ɹ��������������Ϊ�����޸�
            if (this.winFun == EnumWindowFun.�ɹ��ƻ� || (this.winFun == EnumWindowFun.�ɹ���� && this.isCanEditWhenApprove))      
            {
                this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColApproveNum].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColApproveNum].BackColor = System.Drawing.Color.SeaShell;

                if (this.isCanEditPrice)
                {
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = false;
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].BackColor = System.Drawing.Color.SeaShell;
                }
                else
                {
                    this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = true;
                }
            }

            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ʵ����Ϣ��Fp��
        /// </summary>
        /// <param name="stockPlan">�ɹ�ʵ����Ϣ</param>
        /// <param name="rowIndex">����ӵ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Pharmacy.StockPlan stockPlan,int rowIndex)
        {
            if (stockPlan.State == "2")
            {
                this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColApproveNum].Locked = true;
                this.neuSpread1_Sheet1.Columns[(int)ColumnStockSet.ColApproveNum].BackColor = System.Drawing.Color.White;
            }

            #region ��ȡ��ʷ�ɹ���Ϣ

            ArrayList alHistory = this.itemManager.QueryHistoryStockPlan(stockPlan.Dept.ID, stockPlan.Item.ID);
            if (alHistory == null)
            {
                Function.ShowMsg("��ȡ��ʷ�ɹ���Ϣ����" + this.itemManager.Err);
                return -1;
            }

            this.alPlanHistory.Add(alHistory);

            if (!this.isUseDefaultStockData)        //��ʾ��һ�εĹ�����Ϣ
            {
                if (alHistory.Count > 0)
                {
                    FS.HISFC.Models.Pharmacy.StockPlan stockTemp = alHistory[0] as FS.HISFC.Models.Pharmacy.StockPlan;
                    //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                    //������Ѿ�ѡ���˹�����˾�Ļ�������Ĭ�ϵ�
                    if (stockPlan.Company == null || stockPlan.Company.ID == "")
                    {
                        stockPlan.Company = stockTemp.Company;
                    }
                    if (stockPlan.Item.Product.Producer == null || stockPlan.Item.Product.Producer.ID == "")
                    {
                        stockPlan.Item.Product.Producer = stockTemp.Item.Product.Producer;
                    }
                    if (stockPlan.StockPrice == 0)
                    {
                        stockPlan.StockPrice = stockTemp.StockPrice;
                    }
                }
            }

            #endregion

            if (stockPlan.Item.PackQty == 0)
            {
                stockPlan.Item.PackQty = 1;
            }

            #region ҩƷ��Ϣ

            FS.HISFC.Models.Pharmacy.Item tempItem = new FS.HISFC.Models.Pharmacy.Item();
            tempItem = this.itemManager.GetItem(stockPlan.Item.ID);
            if (tempItem == null)
            {
                Function.ShowMsg("δ��ȷ��ȡҩƷ��Ϣ" + this.itemManager.Err);
                return -1;
            }

            #endregion

            this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);

            #region Fp��ֵ

            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColTenderFlag].Value = tempItem.TenderOffer.IsTenderOffer;     //�б�ҩ��־
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColTradeName].Value = stockPlan.Item.Name;		                    //ҩƷ����
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColSpecs].Value = stockPlan.Item.Specs;		                        //���
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Value = stockPlan.StockPrice;	                        //ҩƷ�����				
            //�ƻ��ɹ�����(����װ��λ��ʾ)
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColPlanNum].Value = stockPlan.PlanQty / stockPlan.Item.PackQty;
            //�������(����װ��λ��ʾ)
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColApproveNum].Value = stockPlan.StockApproveQty / stockPlan.Item.PackQty;
            //��װ��λ
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColUnit].Value = stockPlan.Item.PackUnit;
            //��˽��
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColApproveCost].Value = stockPlan.StockApproveQty / stockPlan.Item.PackQty * stockPlan.Item.PriceCollection.PurchasePrice;

            if (this.companyHelper.GetObjectFromID(stockPlan.Company.ID) != null)
            {
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColCompany].Value = stockPlan.Company.Name;                      //������˾����
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColProduceName].Value = stockPlan.Item.Product.Producer.Name;	//��������
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColCompany].Value = this.companyHelper.GetName(tempItem.Product.Company.ID);         //������˾����
                this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColProduceName].Value = this.produceHelper.GetName(tempItem.Product.Producer.ID);	//��������               

                stockPlan.Company.ID = tempItem.Product.Company.ID;
                stockPlan.Company.Name = this.companyHelper.GetName(tempItem.Product.Company.ID);

                stockPlan.Item.Product.Producer.ID = tempItem.Product.Producer.ID;
                stockPlan.Item.Product.Producer.Name = this.produceHelper.GetName(tempItem.Product.Producer.ID);
            }

            if (stockPlan.StockPrice == 0)
            {
                if (tempItem.PriceCollection.PurchasePrice == 0)
                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Text = tempItem.PriceCollection.RetailPrice.ToString("N");
                else
                    //this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Text = tempItem.PriceCollection.PurchasePrice.ToString("N");
                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Text = tempItem.PriceCollection.PurchasePrice.ToString();//{1EC17564-2FAD-4a77-97AC-4C57076888B2}
            }

            //ȫԺ���/���ƿ�� �����ƶ����ƻ�ʱ��ֵ
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColOwnStockNum].Value = stockPlan.StoreQty / stockPlan.Item.PackQty;
            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColAllStockNum].Value = stockPlan.StoreTotQty / stockPlan.Item.PackQty;

            this.neuSpread1_Sheet1.Rows[rowIndex].Tag = stockPlan;

            #endregion

            //��ʾ���ƻ�����Ϣ
            if (rowIndex == 0)
            {
                #region ��ʾ���ƻ�����Ϣ

                //��ÿ�������
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(stockPlan.Dept.ID);
                //��ò���Ա����
                FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee person = personManager.GetPersonByID(stockPlan.Oper.ID);
                this.lbPlanBill.Text = "���ݺ�:" + stockPlan.BillNO;                            //���ƻ�����
                this.lbPlanInfo.Text = string.Format("�ƻ����� {0} �ƻ��� {1}",dept.Name,person.Name);     //��������

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ����ʵ����Ϣ��Fp��
        /// </summary>
        /// <param name="stockPlan">�ɹ�ʵ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            return this.AddDataToFp(stockPlan, this.neuSpread1_Sheet1.Rows.Count);
        }

        /// <summary>
        /// ����ʵ����Ϣ
        /// </summary>
        /// <param name="inPlan">���ƻ�ʵ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Pharmacy.InPlan inPlan)
        {
            FS.HISFC.Models.Pharmacy.StockPlan stockPlan = new FS.HISFC.Models.Pharmacy.StockPlan();

            stockPlan.BillNO = inPlan.BillNO;               //���ݺ�
            stockPlan.Dept = inPlan.Dept;                   //�ƻ�����
            stockPlan.Item = inPlan.Item;                   //ҩƷ��Ϣ
            stockPlan.StoreQty = inPlan.StoreQty;           //���ƿ��
            stockPlan.StoreTotQty = inPlan.StoreTotQty;     //ȫԺ���
            stockPlan.OutputQty = inPlan.OutputQty;         //�վ�����
            stockPlan.PlanQty = inPlan.PlanQty;             //�ƻ�����
            stockPlan.PlanOper = inPlan.PlanOper;           //�ƻ���
            stockPlan.Extend = inPlan.Extend;               //��չ�ֶ���Ϣ

            stockPlan.PlanNO = inPlan.ID;                   //�ƻ���ˮ��  ����ɹ��ƻ���Ӧ�����ƻ�����ˮ��

            stockPlan.StockApproveQty = inPlan.PlanQty;     //�������

            return this.AddDataToFp(stockPlan);
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Clear()
        {
            //���Fp��ʾ
            this.fpSpread2_Sheet1.Rows.Count = 0;
            this.neuSpread1_Sheet1.Rows.Count = 0;

            //�ɹ���Ϣ�������
            this.hsStockPlan.Clear();
            this.alPlanHistory.Clear();

            this.lbPlanBill.Text = "���ݺ�:";
            this.lbPlanInfo.Text = "�ƻ����� �ƻ���";
            this.lbCost.Text = "�ƻ��ܽ��";

            this.ClearHistoryData();
        }

        /// <summary>
        /// ������ϸ����
        /// </summary>
        public void ShowStockData()
        {
            //�������
            this.Clear();
            if (this.nowOperBillNO == null || this.nowOperBillNO.Trim() == "")
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("���ڼ������ƻ�����Ϣ..."));
            Application.DoEvents();

            ArrayList alDetail = this.itemManager.QueryStockPlanByCompany(this.privDept.ID, this.nowOperBillNO, this.nowCompanyNO);
            if (alDetail == null)
            {
                Function.ShowMsg("��ȡ�ɹ��ƻ���Ϣ����" + this.itemManager.Err);
                return;
            }

            int iCount = 1;

            foreach (FS.HISFC.Models.Pharmacy.StockPlan stockPlan in alDetail)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(iCount, alDetail.Count);
                Application.DoEvents();

                if (this.AddDataToFp(stockPlan) == 1)
                {
                    this.hsStockPlan.Add(stockPlan.ID, stockPlan);
                }                
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //���Ӻϼ�
            this.SetSum();

            this.neuSpread1_Sheet1.ActiveRowIndex = 0;
            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColStockPrice;
        }

        /// <summary>
        /// ����ϼ�
        /// </summary>
        /// <returns></returns>
        private void SetSum()
        {
            try
            {
                if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                    return;
               
                decimal costSum = 0;
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    costSum = costSum + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveCost].Text);
                }

                this.lbCost.Text = "�ƻ��ܽ��: " + costSum.ToString("N");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// �ж��Ƿ�����ȷ��������
        /// </summary>
        /// <returns></returns>
        public bool IsValidate()
        {
            int num = this.neuSpread1_Sheet1.RowCount;

            if (num <= 0)
            {
                return false;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                string trandeName = this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColTradeName].Text;

                if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Text.Trim() == "")
                {
                    MessageBox.Show(Language.Msg("������" + trandeName + " ������˾"));
                    this.neuSpread1_Sheet1.ActiveRowIndex = i;
                    return false;
                }
                //�繩����˾Ϊ"����"������Բ����빺���
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Text.Trim() != "����" && NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text) <= 0)
                {
                    MessageBox.Show(Language.Msg("������" + trandeName + " �����!��"));
                    this.neuSpread1_Sheet1.ActiveRowIndex = i;
                    return false;
                }
                if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text) <= 0)
                {
                    MessageBox.Show(Language.Msg("������" + trandeName + " ��������"));
                    this.neuSpread1_Sheet1.ActiveRowIndex = i;
                    return false;
                }
                if (this.neuSpread1_Sheet1.Columns.Get((int)ColumnStockSet.ColApproveNum).Visible && NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveNum].Text) <= 0)
                {
                    MessageBox.Show(Language.Msg("������" + trandeName + " ��˹�������"));
                    this.neuSpread1_Sheet1.ActiveRowIndex = i;
                    return false ;
                }
                //������� ���� �ƻ����� ��ʾ�Ƿ�ȷ��
                if (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text) < NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveNum].Text))
                {
                    DialogResult rs = MessageBox.Show(Language.Msg(trandeName + " ������������ڼƻ����� �Ƿ�ȷ�����ͨ��"),"",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
                    if (rs == DialogResult.No)
                        return false;
                    else
                        return true;
                }
            }
            return true;
        }

        /// <summary>
        /// �ƻ�����ӡ
        /// </summary>
        /// <param name="alPrintData">����ӡ����</param>
        /// <param name="isCue">�Ƿ������ʾ</param>
        /// <returns></returns>
        public virtual int Print(ArrayList alPrintData,bool isCue)
        {
            //ҩƷ��ӡ�ӿڵ��ó���Bug�������ͬʱ�������ƻ��Ͳɹ��ƻ������й���ӡ������£��л�������ӡ�ͻ������һ����ӡ�ӿ�ʵ�� by Sunjh 2010-8-26 {D78A574D-59BE-491b-808C-38DCD26BA5EA}
            Function.IPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint;
            //if (Function.IPrint == null)
            //{
            //    Function.IPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint)) as FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint;
            //}

            if (Function.IPrint != null)
            {
                if (isCue)
                {
                    DialogResult rs = MessageBox.Show(Language.Msg("�Ƿ��ӡ�ƻ�����"), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.No)
                    {
                        return 1;
                    }
                }

                Function.IPrint.SetData(alPrintData, FS.HISFC.BizProcess.Interface.Pharmacy.BillType.StockPlan);

                Function.IPrint.Print();
            }

            return 1;
        }

        /// <summary>
        /// ��ȡ������˾/����������Ϣ �����Թ�����˾�ĸ��ķ�������Tagʵ��
        /// </summary>
        private void PopStockCompany()
        {
            //��ǰ��¼���С���
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;
            int j = this.neuSpread1_Sheet1.ActiveColumnIndex;
            //���������򷵻�
            if (this.neuSpread1_Sheet1.RowCount == 0)
                return;

            if (i < 0) return;

            FS.HISFC.Models.Pharmacy.StockPlan stockPlanTemp = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.StockPlan;

            if (j == (int)ColumnStockSet.ColCompany)
            {
                //��ù�����˾�б�
                if (this.alCompany == null)
                {
                    FS.HISFC.BizLogic.Pharmacy.Constant phaConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
                    this.alCompany = phaConstant.QueryCompany("1");
                    if (this.alCompany == null)
                    {
                        MessageBox.Show(Language.Msg("��ȡ������λ�б����"));
                        return;
                    }
                }
                //����Ա�Դ���ѡ�񷵻ص���Ϣ
                FS.FrameWork.Models.NeuObject companyTemp = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alCompany, ref companyTemp) == 0)
                {
                    return;
                }
                else
                {                    
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Value = companyTemp.Name;       //������˾
                    stockPlanTemp.Company = companyTemp;
                }
            }
            if (j == (int)ColumnStockSet.ColProduceName)
            {
                //��ù�����˾�б�
                if (this.alProduce == null)
                {
                    FS.HISFC.BizLogic.Pharmacy.Constant phaConstant = new FS.HISFC.BizLogic.Pharmacy.Constant();
                    this.alProduce = phaConstant.QueryCompany("0");
                    if (this.alProduce == null)
                    {
                        MessageBox.Show(Language.Msg("��ȡ���������б����"));
                        return;
                    }
                }
                //����Ա�Դ���ѡ�񷵻ص���Ϣ
                FS.FrameWork.Models.NeuObject producTemp = new FS.FrameWork.Models.NeuObject();
                if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alProduce, ref producTemp) == 0)
                {
                    return;
                }
                else
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColProduceName].Value = producTemp.Name;     //������˾                    
                    stockPlanTemp.Item.Product.Producer = producTemp;
                }
            }

            //{C03DD304-AE71-4b6a-BC63-F385DB162EB7}
            //���½��޸ĵ���Ϣ���ظ�Tag
            this.neuSpread1_Sheet1.Rows[i].Tag = stockPlanTemp;
        }

        /// <summary>
        /// ���������ļ���ؼ�
        /// </summary>
        public void PopExpandData()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            ucPhaExpand uc = new ucPhaExpand();

            uc.IsOnlyPatientInOut = true;

            FS.HISFC.Models.Pharmacy.StockPlan stockPlan = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.StockPlan;
            FS.FrameWork.Models.NeuObject tempDrug = stockPlan.Item;

            FS.FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();
            tempDept.ID = "AAAA";
            tempDept.Name = "ȫԺ";

            uc.SetData(tempDept, tempDrug, 10);

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int SaveStockPlan()
        {
            if (!this.IsValidate())
            {
                return -1;
            }
            //ϵͳʱ��
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            //�������ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            List<FS.HISFC.Models.Pharmacy.StockPlan> alSavePlanList = new List<FS.HISFC.Models.Pharmacy.StockPlan>();

            bool isStock = false;

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.StockPlan stockPlan = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.StockPlan;
                if (stockPlan == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Language.Msg("����ɹ��ƻ�����ʱ ��������ת������"));
                    return -1;
                }
                if (stockPlan.State == "2")
                {
                    isStock = true;
                }

                #region �����ж�  ����ҵ���ĺ��� �޷��������жϷ���Sql���Update��

                if (stockPlan.ID != "")
                {
                    ArrayList alTemp = this.itemManager.QueryStockPlanDetail(stockPlan.ID);
                    if (alTemp == null || alTemp.Count <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("�òɹ��ƻ���Ϣ�Ѳ����� �����ѷ����仯 ���˳���������"));
                        return -1;
                    }
                    FS.HISFC.Models.Pharmacy.StockPlan stockPlanTemp = alTemp[0] as FS.HISFC.Models.Pharmacy.StockPlan;
                    if (stockPlanTemp.State != stockPlan.State)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(Language.Msg("�üƻ���Ϣ״̬�ѷ����仯 ���˳���������"));
                        return -1;
                    }
                }

                #endregion

                #region �ɹ��ƻ���ֵ

                stockPlan.Oper.ID = this.itemManager.Operator.ID;
                stockPlan.Oper.OperTime = sysTime;                      //������Ա��Ϣ

                stockPlan.StockPrice = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text);     //ҩƷ�����
                stockPlan.StockApproveQty = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveNum].Text) * stockPlan.Item.PackQty;   //�ɹ�����

                if (this.winFun == EnumWindowFun.�ɹ��ƻ�)
                {
                    #region �ɹ��ƻ��ƶ�

                    stockPlan.Item.PriceCollection.PurchasePrice = stockPlan.StockPrice;        //ҩƷ����� ��ֵΪ ҩƷ�ƻ������
                    stockPlan.StockOper = stockPlan.Oper;                                       //�ɹ���
                    //�繩����˾Ϊ���� �򲻸ı�ƻ���״̬
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Text.Trim() == "����")
                        stockPlan.State = "0";
                    else
                        stockPlan.State = "1";
                    //��ɹ�����Ҫ��� ��ֱ������״̬Ϊ2 ���
                    if (!this.isNeedApprove)
                    {
                        stockPlan.ApproveOper = stockPlan.Oper;
                        stockPlan.State = "2";
                    }

                    #endregion
                }
                else           //�ɹ���˹���
                {
                    stockPlan.ApproveOper = stockPlan.Oper;
                    stockPlan.State = "2";
                }

                #endregion

                alSavePlanList.Add(stockPlan);
            }

            #region �ɹ��ƻ�����

            int param = this.itemManager.SaveStockPlan(alSavePlanList);
            if (param == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("�ɹ��ƻ���Ϣ����ʧ��" + this.itemManager.Err));
                return -1;
            }
            if (param == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("���ƻ�������ɾ����������ɹ��ƻ� �������ƻ���Ա��ϵ"));
                return -1;
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            if (isStock)
            {
                MessageBox.Show("�ɹ����ڴ��������������� ����Ҫ�ظ����");
            }

            MessageBox.Show(Language.Msg("����ɹ�"));

            this.Print(new ArrayList(alSavePlanList.ToArray()),true);

            this.Clear();

            return 1;
        }

        #endregion

        #region ���ƻ������� ��/�𵥲���

        /// <summary>
        /// �ƻ�����ѡ��/�ϲ�
        /// </summary>
        private ucPlanList ucMergeList = null;

        /// <summary>
        /// �ɹ������
        /// </summary>
        private ucSplitPlan ucSplitPlan = null;

        /// <summary>
        /// ���ƻ�����ʾ
        /// </summary>
        /// <returns></returns>
        public int ShowInPlanList()
        {
            if (this.ucMergeList == null)
            {
                this.ucMergeList = new ucPlanList();
            }

            this.ucMergeList.OperPrivDept = this.privDept;      //Ȩ�޿���
            this.ucMergeList.State = "0";                       //����״̬ 0

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucMergeList);
            if (this.ucMergeList.Result == DialogResult.OK)     //��ʾ���ݺϲ�
            {
                List<FS.HISFC.Models.Pharmacy.InPlan> alterPlan = this.ucMergeList.AlterInPlan;

                this.Clear();

                foreach (FS.HISFC.Models.Pharmacy.InPlan inPlanObj in alterPlan)
                {
                    this.AddDataToFp(inPlanObj);
                }
            }
            return 1;
        }

        /// <summary>
        /// ���ƻ�����ֶ��Ųɹ���
        /// </summary>
        /// <returns></returns>
        public int SplitPlan()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return -1;
            }

            if (!this.neuSpread1.ContainsFocus || this.neuSpread1_Sheet1.ActiveRowIndex < 0)
            {
                MessageBox.Show(Language.Msg("��ѡ��������Ŀ"));
                return -1;
            }

            FS.HISFC.Models.Pharmacy.StockPlan stockPlan = this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.StockPlan;
            if (stockPlan == null)
            {
                MessageBox.Show(Language.Msg("��Fp�ڻ�ȡʵ�巢������"));
                return -1;
            }

            if (this.ucSplitPlan == null)
            {
                this.ucSplitPlan = new ucSplitPlan();

                this.ucSplitPlan.Init();
            }

            this.ucSplitPlan.OriginalStockPlan = stockPlan;

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucSplitPlan);
            if (this.ucSplitPlan.Result == DialogResult.OK)     //��ʾ���ݲ��
            {
                List<FS.HISFC.Models.Pharmacy.StockPlan> splitPlan = this.ucSplitPlan.SplitPlan;

                int iRemoveIndex = this.neuSpread1_Sheet1.ActiveRowIndex;

                this.neuSpread1_Sheet1.Rows.Remove(iRemoveIndex, 1);

                foreach (FS.HISFC.Models.Pharmacy.StockPlan stockPlanObj in splitPlan)
                {
                    this.AddDataToFp(stockPlanObj,iRemoveIndex);

                    iRemoveIndex++;
                }
            }

            return 1;
        }

        #endregion

        #region ��ʾ��ʷ�ɹ���¼

        /// <summary>
        /// ���ԭ��ʾ����ʷ�ɹ���Ϣ
        /// </summary>
        private void ClearHistoryData()
        {
            this.tbStockHistory.Text = " ��ʷ�ɹ���Ϣ";
            this.fpSpread2_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ������ʷ�ɹ���Ϣ
        /// </summary>
        /// <param name="stockPlan">��ʷ�ɹ���Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddHistoryDataToFp(FS.HISFC.Models.Pharmacy.StockPlan stockPlan)
        {
            int iRowIndx = this.fpSpread2_Sheet1.Rows.Count;

            this.fpSpread2_Sheet1.Rows.Add(iRowIndx, 1);           

            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColTenderFlag].Value = stockPlan.Item.TenderOffer.IsTenderOffer;		//�б�ҩ
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColStockTime].Value = stockPlan.StockOper.OperTime;		                //�ɹ�����
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColStockQty].Text = (stockPlan.StockApproveQty / stockPlan.Item.PackQty).ToString();		//�ɹ�����
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColUnit].Text = stockPlan.Item.PackUnit;				                //��λ
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColStockPrice].Text = stockPlan.StockPrice.ToString();		            //�����
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColRetailPrice].Text = stockPlan.Item.PriceCollection.RetailPrice.ToString();		    //���ۼ�
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColCompany].Text = stockPlan.Company.Name;				                //������˾
            this.fpSpread2_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColProduce].Text = stockPlan.Item.Product.Producer.Name;		                //��������

            return 1;
        }

        /// <summary>
        /// ������ʷ�ɹ���Ϣ
        /// </summary>
        /// <param name="alHistory"></param>
        private void AddHistoryDataToFp(ArrayList alHistory)
        {
            foreach (FS.HISFC.Models.Pharmacy.StockPlan info in alHistory)
            {
                this.AddHistoryDataToFp(info);
            }
        }

        /// <summary>
        /// ��ʾ��ʷ�ɹ���¼
        /// </summary>
        protected void ShowHistoryData()
        {
            if (this.neuSpread1_Sheet1.RowCount <= 0)
                return;

            if (this.alPlanHistory.Count > this.neuSpread1_Sheet1.ActiveRowIndex)
            {
                this.ClearHistoryData();

                //��ʾTabҳ����ʾ��Ϣ
                this.tbStockHistory.Text = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnStockSet.ColTradeName].Text + "[" + this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, (int)ColumnStockSet.ColSpecs].Text + "]" + " ��ʷ�ɹ���Ϣ";

                this.AddHistoryDataToFp(this.alPlanHistory[this.neuSpread1_Sheet1.ActiveRowIndex] as ArrayList);
            }
        }

        #endregion

        #region ������

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            FS.FrameWork.Models.NeuObject temp = neuObject as FS.FrameWork.Models.NeuObject;

            if (temp != null)
            {
                this.nowOperBillNO = temp.ID;       //�ɹ�����
                this.nowCompanyNO = temp.User01;    //������λ��

                this.ShowStockData();
            }

            return base.OnSetValue(neuObject, e);
        }

        /// <summary>
        /// �б�ˢ����ʾ
        /// </summary>
        protected void ShowList()
        {
            //{368C3BA2-C27A-4ed2-8062-A52A40468F93}
            if ((this.winFun == EnumWindowFun.�ɹ��ƻ� && this.IsNeedApprove) || (this.winFun == EnumWindowFun.�ɹ����))
            {
                (this.tv as tvPlanList).ShowStockPlanList(this.privDept, "1");
            }
            else
            {
                (this.tv as tvPlanList).ShowStockPlanList(this.privDept, "2");
            }
        }

        #endregion

        #region �¼�

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.privOper = this.itemManager.Operator;

                //string class2Priv = "0312";
                //if (this.winFun == EnumWindowFun.�ɹ��ƻ�)
                //{
                //    class2Priv = "0312";
                //}
                //else
                //{
                //    class2Priv = "0313";

                //    this.toolBarService.SetToolButtonEnabled("�� �� ��", false);
                //    this.toolBarService.SetToolButtonEnabled("�ɹ����", false);
                //}

                //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept(class2Priv, ref testPrivDept);

                //if (parma == -1)            //��Ȩ��
                //{
                //    MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                //    return;
                //}
                //else if (parma == 0)       //�û�ѡ��ȡ��
                //{
                //    return;
                //}

                //this.privDept = testPrivDept;

                //base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name); 

                //{52402239-DB82-41c8-A8A7-2411B9EF64F1}  ��ʼ����ӡ�ӿ�
                Function.IPrint = null;

                this.InitData();

                if (this.tv as tvPlanList == null)
                {
                    MessageBox.Show(Language.Msg("���ؼ��������ô���"));
                    return;
                }

                this.ShowList();
            }

            base.OnLoad(e);
        }

        private void fpStockApprove_EditModeOff(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnStockSet.ColStockPrice || this.neuSpread1_Sheet1.ActiveColumnIndex == (int)ColumnStockSet.ColApproveNum)
            {
                int i = this.neuSpread1_Sheet1.ActiveRowIndex;
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColTradeName].Text == "�ϼ�")
                    return;
                //����ƻ����
                try
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveCost].Text = (NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text) * NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveNum].Text)).ToString();

                    this.SetSum();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg(ex.Message));
                }
                return;
            }
        }

        /// <summary>
        /// ����س���ת�����¼�ͷ�ƶ�ʱ�ı� ��ʷ�ɹ���ʾ
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.neuSpread1.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    #region ������ת
                    int iRow = this.neuSpread1_Sheet1.ActiveRowIndex;
                    int iColumn = this.neuSpread1_Sheet1.ActiveColumnIndex;

                    switch (iColumn)
                    {
                        case (int)ColumnStockSet.ColStockPrice:
                            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColApproveNum;
                            break;
                        case (int)ColumnStockSet.ColApproveNum:
                            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColCompany;
                            break;
                        case (int)ColumnStockSet.ColCompany:
                            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColProduceName;
                            break;
                        case (int)ColumnStockSet.ColProduceName:
                            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColMemo;
                            break;
                        case (int)ColumnStockSet.ColMemo:
                            this.neuSpread1_Sheet1.ActiveColumnIndex = 0;		//ʹ��������ת����һ�� ����ֱ����ת���۸񿴲�����һ��
                            this.neuSpread1_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColStockPrice;
                            this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.ActiveRowIndex + 1;
                            this.ShowHistoryData();
                            break;
                    }
                    return true;
                    #endregion
                }
                if (keyData == Keys.Up)
                {
                    if (this.neuSpread1_Sheet1.ActiveRowIndex != 0)
                        this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.ActiveRowIndex - 1;
                    this.ShowHistoryData();
                    return true;
                }
                if (keyData == Keys.Down)
                {
                    if (this.neuSpread1_Sheet1.ActiveRowIndex != this.neuSpread1_Sheet1.Rows.Count - 1)
                    {
                        this.neuSpread1_Sheet1.ActiveRowIndex = this.neuSpread1_Sheet1.ActiveRowIndex + 1;
                    }
                    this.ShowHistoryData();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// �������ʷҩƷ�ɹ���¼�ĵ�������
        /// </summary>
        private void fpStockApprove_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //��ǰ��¼���С���
            int i = this.neuSpread1_Sheet1.ActiveRowIndex;
            int j = this.neuSpread1_Sheet1.ActiveColumnIndex;

            //�س������� 13 �ո������ 32
            if (e.KeyChar == 32)
            {
                this.PopStockCompany();
            }
            else
            {      //���µ�ΪBackspace��
                if (e.KeyChar == (char)8 && j == (int)ColumnStockSet.ColCompany)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Value = "����";  //������˾
                }
            }
        }

        /// <summary>
        /// �������ʷҩƷ�ɹ���¼�ĵ�������
        /// </summary>
        private void fpStockApprove_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��������Ϊ�л��б�����ֱ�ӷ���
            if (e.ColumnHeader || e.RowHeader)
                return;
            //���ܴ���Ϊ�ɹ���� �Ҳ������޸Ĳɹ�����Ϣ
            if (this.winFun == EnumWindowFun.�ɹ���� && !this.isCanEditWhenApprove)
                return;

            this.PopStockCompany();
        }

        /// <summary>
        /// ѡ��ͬ��ʱ��ʾ��ͬ��ʷ�ɹ���Ϣ
        /// </summary>
        private void fpStockApprove_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            
            this.ShowHistoryData();
        }

        #endregion

        #region ö��

        /// <summary>
        /// ���ڹ���
        /// </summary>
        public enum EnumWindowFun
        {
            �ɹ��ƻ�,
            �ɹ����
        }

        #endregion

        #region ������

        /// <summary>
        /// �ɹ��ƻ�������
        /// </summary>
        private enum ColumnStockSet
        {
            /// <summary>
            /// 0 �Ƿ��б�ҩ
            /// </summary>
            ColTenderFlag,
            /// <summary>
            /// 1 ҩƷ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// 2 ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// 3 �ƻ������
            /// </summary>
            ColStockPrice,
            /// <summary>
            /// 4 �ƻ�����
            /// </summary>
            ColPlanNum,
            /// <summary>
            /// 5 �������
            /// </summary>
            ColApproveNum,
            /// <summary>
            /// 6 ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// 7 ��˽��
            /// </summary>
            ColApproveCost,
            /// <summary>
            /// 8 ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// 9 ��������
            /// </summary>
            ColProduceName,
            /// <summary>
            /// 10 ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// 11 ���ҿ��
            /// </summary>
            ColOwnStockNum,
            /// <summary>
            /// 12 ȫԺ���
            /// </summary>
            ColAllStockNum
        }

        /// <summary>
        /// �ɹ��ƻ�������
        /// </summary>
        private enum ColumnHistorySet
        {
            /// <summary>
            /// 0 �Ƿ��б�ҩ
            /// </summary>
            ColTenderFlag,
            /// <summary>
            /// 0 �ɹ�����
            /// </summary>
            ColStockTime,
            /// <summary>
            /// 1 �ɹ�����
            /// </summary>
            ColStockQty,
            /// <summary>
            /// 2 ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// 3 �����
            /// </summary>
            ColStockPrice,
            /// <summary>
            /// 4 ���ۼ�
            /// </summary>
            ColRetailPrice,
            /// <summary>
            /// 5 ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// 6 ��������
            /// </summary>
            ColProduce,
            /// <summary>
            /// 7 ��ע
            /// </summary>
            ColMemo
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IBillPrint);

                return printType;
            }
        }

        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            string class2Priv = "0312";
            if (this.winFun == EnumWindowFun.�ɹ��ƻ�)
            {
                class2Priv = "0312";
            }
            else
            {
                class2Priv = "0313";

                this.toolBarService.SetToolButtonEnabled("�� �� ��", false);
                this.toolBarService.SetToolButtonEnabled("�ɹ����", false);
            }

            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept(class2Priv, ref testPrivDept);

            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show(Language.Msg("���޴˴��ڲ���Ȩ��"));
                return -1;
            }
            else if (parma == 0)       //�û�ѡ��ȡ��
            {
                return -1;
            }

            this.privDept = testPrivDept;

            base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

            return 1;
        }

        #endregion
    }
}

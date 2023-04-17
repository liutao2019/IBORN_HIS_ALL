using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Material.Plan
{
    /// <summary>
    /// 
    /// ����δ����
    /// 
    /// </summary>
    public partial class ucStockPlan : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucStockPlan()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ��Ʒ������
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem itemManager = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// �ƻ���������
        /// </summary>
        private FS.HISFC.BizLogic.Material.Plan planManager = new FS.HISFC.BizLogic.Material.Plan();

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
        [Description("�ɹ��ƻ�ָ�����Ƿ���Ҫ�ɹ����"), Category("����"), DefaultValue(true)]
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

                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = false;
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColCompany].Locked = false;
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColPlanNum].Locked = false;
                    //this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockQty].Locked = false;
                }
                else
                {

                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = true;
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColCompany].Locked = true;
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColPlanNum].Locked = true;
                    //this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockQty].Locked = true;
                }
            }
        }

        /// <summary>
        /// �Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����
        /// </summary>
        [Description("�ɹ�ָ��ʱ�Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����"), Category("����"), DefaultValue(true)]
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
        [Description("�ɹ����ʱ �Ƿ������޸���Ӧ�Ĳɹ���Ϣ"), Category("����"), DefaultValue(false)]
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
        [Description("�Ƿ������޸ļƻ������"), Category("����"), DefaultValue(true)]
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

        #region ״̬�������

        /// <summary>
        /// �����б����״̬
        /// </summary>
        private string listState = "0";

        /// <summary>
        /// ���ݱ���״̬
        /// </summary>
        private string saveState = "1";

        /// <summary>
        /// ��������״̬ ���������������״̬�����ڽ�����ʾ
        /// </summary>
        private string filterState = "";

        /// <summary>
        /// �����ƻ���״̬
        /// </summary>
        private string popPlanListState = "0";

        /// <summary>
        /// ��������״̬����
        /// </summary>
        private System.Collections.Hashtable hsFilterState = new Hashtable();

        /// <summary>
        /// �����б����״̬
        /// </summary>
        [Description("�����б����״̬"), Category("����"), DefaultValue("0")]
        public string ListState
        {
            get
            {
                return this.listState;
            }
            set
            {
                this.listState = value;
            }
        }

        /// <summary>
        /// ���ݱ���״̬
        /// </summary>
        [Description("���ݼ���״̬"), Category("����"), DefaultValue("1")]
        public string SaveState
        {
            get
            {
                return this.saveState;
            }
            set
            {
                this.saveState = value;
            }
        }

        /// <summary>
        /// ��������״̬����
        /// </summary>
        [Description("��������״̬���� ���ڶ��ʱ ��,���"), Category("����"), DefaultValue("")]
        public string FilterState
        {
            get
            {
                return this.filterState;
            }
            set
            {
                this.filterState = value;
                string[] filterCollection = value.Split(',');
                this.hsFilterState.Clear();
                foreach (string str in filterCollection)
                {
                    this.hsFilterState.Add(str, null);
                }
            }
        }

        /// <summary>
        /// �����ƻ���״̬
        /// </summary>
        [Description("�����ƻ���״̬"), Category("����"), DefaultValue("0")]
        public string PopPlanListState
        {
            get
            {
                return this.popPlanListState;
            }
            set
            {
                this.popPlanListState = value;
            }
        }

        #endregion

        #region ������

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("�� �� ��", "�ƻ����б�", FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("�� �� ��", "����ģ�����ɼƻ���", FS.FrameWork.WinForms.Classes.EnumImageList.R������, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "�� �� ��":
                    this.PopInPlanList();
                    break;
                case "�� �� ��":
                    this.PopExpandData();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.SaveStockPlan() == 1)
            {
                this.ShowPlanList();
            }

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.PrintPreview(40, 10, this.neuPanel1);
            return 1;
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ���ݳ�ʼ��
        /// </summary>
        /// <returns></returns>
        private int InitData()
        {
            this.fpStockApprove_Sheet1.DefaultStyle.Locked = true;
            this.fpHistory_Sheet1.DefaultStyle.Locked = true;

            FarPoint.Win.Spread.InputMap im;
            im = this.fpStockApprove.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            this.fpStockApprove_Sheet1.DefaultStyle.Locked = true;

            #region ��ȡ��������/������˾������

            //��ù�����˾�б�
            if (this.alCompany == null)
            {
                FS.HISFC.BizLogic.Material.ComCompany company = new FS.HISFC.BizLogic.Material.ComCompany();
                this.alCompany = company.QueryCompany("1", "A");
                if (this.alCompany == null)
                {
                    MessageBox.Show("��ȡ������λ�б����");
                    return -1;
                }

                this.companyHelper = new FS.FrameWork.Public.ObjectHelper(this.alCompany);
            }
            if (this.alProduce == null)
            {
                FS.HISFC.BizLogic.Material.ComCompany company = new FS.HISFC.BizLogic.Material.ComCompany();
                this.alProduce = company.QueryCompany("0", "A");
                if (this.alProduce == null)
                {
                    MessageBox.Show("��ȡ���������б����");
                    return -1;
                }
                this.produceHelper = new FS.FrameWork.Public.ObjectHelper(this.alProduce);
            }

            #endregion

            //�ɹ��ƻ� �����޸��������/������˾
            //�ɹ��������������Ϊ�����޸�
            if (this.winFun == EnumWindowFun.�ɹ��ƻ� || (this.winFun == EnumWindowFun.�ɹ���� && this.isCanEditWhenApprove))
            {
                if (this.isCanEditPrice)
                {
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].Locked = false;
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColPlanNum].Locked = false;
                    //this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockQty].Locked = false;
                    this.fpStockApprove_Sheet1.Columns[(int)ColumnStockSet.ColStockPrice].BackColor = System.Drawing.Color.SeaShell;
                }
            }

            return 1;
        }

        #endregion

        #region ����

        /// <summary>
        /// ����ʵ����Ϣ��Fp��
        /// </summary>
        /// <param name="plan">�ɹ�ʵ����Ϣ</param>
        /// <param name="rowIndex">����ӵ�������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Material.InputPlan plan, int rowIndex)
        {
            #region ��ȡ��ʷ�ɹ���Ϣ

            ArrayList alHistory = this.planManager.QueryHistoryInPlan(plan.StorageCode, plan.StoreBase.Item.ID, "2");
            ArrayList alHistoryIn = this.planManager.QueryHistoryInPlan(plan.StorageCode, plan.StoreBase.Item.ID, "3");
            alHistory.AddRange(alHistoryIn);
            if (alHistory == null)
            {
                Function.ShowMsg("��ȡ��ʷ�ɹ���Ϣ����" + this.itemManager.Err);
                return -1;
            }

            this.alPlanHistory.Add(alHistory);

            this.AddHistoryDataToFp(alHistory);

            #endregion

            if (plan.StoreBase.Item.PackQty == 0)
            {
                plan.StoreBase.Item.PackQty = 1;
            }

            #region ��Ʒ��Ϣ

            FS.HISFC.Models.Material.MaterialItem tempItem = new FS.HISFC.Models.Material.MaterialItem();
            tempItem = this.itemManager.GetMetItemByMetID(plan.StoreBase.Item.ID);
            if (tempItem == null)
            {
                Function.ShowMsg("δ��ȷ��ȡ��Ʒ��Ϣ" + this.itemManager.Err);
                return -1;
            }

            #endregion

            #region �Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾/�����
            if (!this.isUseDefaultStockData)
            {
                if (alHistory.Count > 0)
                {
                    //FS.HISFC.Models.Material.InputPlan planTemp = alHistory[0] as FS.HISFC.Models.Material.InputPlan;
                    plan.Company = tempItem.Company;
                    plan.Producer = tempItem.Factory;
                    plan.StoreBase.PriceCollection.PurchasePrice = tempItem.PackPrice;
                }
            }
            #endregion

            this.fpStockApprove_Sheet1.Rows.Add(rowIndex, 1);

            #region Fp��ֵ

            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColTradeName].Value = tempItem.Name;		                //��Ʒ����
            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColSpecs].Value = plan.StoreBase.Item.Specs;							//���
            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Value = plan.PlanPrice;	//��Ʒ�ƻ������				
            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColPlanNum].Value = plan.PlanNum;		//�ƻ��ɹ�����(����װ��λ��ʾ)			
            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColUnit].Value = tempItem.PackUnit;
            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColApproveCost].Value = (plan.PlanNum * plan.PlanPrice).ToString("N");

            if (this.companyHelper.GetObjectFromID(plan.Company.ID) != null)
            {
                this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColCompany].Value = plan.Company.Name;							//������˾����
                this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColProduceName].Value = plan.Producer.Name;						//��������
            }
            else
            {
                this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColCompany].Value = this.companyHelper.GetName(tempItem.Company.ID);         //������˾����
                this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColProduceName].Value = this.produceHelper.GetName(tempItem.Factory.ID);	//��������               

                plan.Company.ID = tempItem.Company.ID;
                plan.Company.Name = this.companyHelper.GetName(tempItem.Company.ID);

                plan.Producer.ID = tempItem.Factory.ID;
                plan.Producer.Name = this.produceHelper.GetName(tempItem.Factory.ID);
            }

            if (plan.PlanPrice == 0)
            {
                if (tempItem.PackPrice == 0)
                    this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Text = tempItem.Price.ToString("N");
                else
                    this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColStockPrice].Text = tempItem.PackPrice.ToString("N");
            }

            //ȫԺ���/���ƿ�� �����ƶ����ƻ�ʱ��ֵ

            #region ȡ���ƿ�� X�����װ��X��С��װ��eg:1��4֧
            string strStoreSum = (Math.Floor(plan.StoreSum / tempItem.PackQty)).ToString() + tempItem.PackUnit;
            decimal reQty = Math.Ceiling(plan.StoreSum % tempItem.PackQty);
            if (reQty > 0)
            {
                strStoreSum = strStoreSum + reQty.ToString() + tempItem.MinUnit;
            }
            #endregion

            #region ȡȫԺ��� X�����װ��X��С��װ��eg:1��4֧
            string strStoreTotSum = (Math.Floor(plan.StoreTotsum / tempItem.PackQty)).ToString() + tempItem.PackUnit;
            decimal reTotQty = Math.Ceiling(plan.StoreTotsum % tempItem.PackQty);
            if (reTotQty > 0)
            {
                strStoreTotSum = strStoreTotSum + reTotQty.ToString() + tempItem.MinUnit;
            }
            #endregion

            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColOwnStockNum].Value = strStoreSum;
            this.fpStockApprove_Sheet1.Cells[rowIndex, (int)ColumnStockSet.ColAllStockNum].Value = strStoreTotSum;

            this.fpStockApprove_Sheet1.Rows[rowIndex].Tag = plan;

            #endregion

            //��ʾ���ƻ�����Ϣ
            if (rowIndex == 0)
            {
                #region ��ʾ���ƻ�����Ϣ

                //��ÿ�������
                FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
                FS.HISFC.Models.Base.Department dept = deptManager.GetDeptmentById(plan.StorageCode);
                //��ò���Ա����
                FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
                FS.HISFC.Models.Base.Employee person = personManager.GetPersonByID(plan.StoreBase.Operation.Oper.ID);

                this.lbPlanBill.Text = "���ݺ�:" + plan.StorageCode;                            //���ƻ�����
                this.lbPlanInfo.Text = string.Format("�ƻ����� {0} �ƻ��� {1}", dept.Name, person.Name);     //��������

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// ����ʵ����Ϣ
        /// </summary>
        /// <param name="inPlan">���ƻ�ʵ����Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddDataToFp(FS.HISFC.Models.Material.InputPlan inPlan)
        {
            return this.AddDataToFp(inPlan, this.fpStockApprove_Sheet1.Rows.Count);
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Clear()
        {
            //���Fp��ʾ
            this.fpHistory_Sheet1.Rows.Count = 0;
            this.fpStockApprove_Sheet1.Rows.Count = 0;

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
        public void ShowStockData(string listNO, string companyCode)
        {
            //�������
            this.Clear();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������ƻ�����Ϣ...");
            Application.DoEvents();

            ArrayList alDetail = this.planManager.QueryInPlanDetailCom(this.privDept.ID, listNO, companyCode);
            if (alDetail == null)
            {
                Function.ShowMsg("��ȡ�ɹ��ƻ���Ϣ����" + this.planManager.Err);
                return;
            }

            foreach (FS.HISFC.Models.Material.InputPlan plan in alDetail)
            {
                if (this.hsFilterState.ContainsKey(plan.State))
                {
                    continue;
                }

                if (this.AddDataToFp(plan) == 1)
                {
                    this.hsStockPlan.Add(plan.ID, plan);
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //���Ӻϼ�
            this.SetSum();

            this.fpStockApprove_Sheet1.ActiveRowIndex = 0;
            this.fpStockApprove_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColStockPrice;
        }

        /// <summary>
        /// ����ϼ�
        /// </summary>
        /// <returns></returns>
        private void SetSum()
        {
            try
            {
                if (this.fpStockApprove_Sheet1.Rows.Count <= 0)
                    return;

                decimal costSum = 0;
                for (int i = 0; i < this.fpStockApprove_Sheet1.Rows.Count; i++)
                {
                    costSum = costSum + NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveCost].Text);
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
            int num = this.fpStockApprove_Sheet1.RowCount;

            if (num <= 0)
            {
                return false;
            }

            for (int i = 0; i < this.fpStockApprove_Sheet1.RowCount; i++)
            {
                string trandeName = this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColTradeName].Text;

                if (this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Text.Trim() == "")
                {
                    MessageBox.Show("������" + trandeName + " ������˾");
                    this.fpStockApprove_Sheet1.ActiveRowIndex = i;
                    return false;
                }
                //�繩����˾Ϊ"����"������Բ����빺���
                if (this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Text.Trim() != "����" && NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text) <= 0)
                {
                    MessageBox.Show("������" + trandeName + " �����!��");
                    this.fpStockApprove_Sheet1.ActiveRowIndex = i;
                    return false;
                }
                if (NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text) <= 0)
                {
                    MessageBox.Show("������" + trandeName + " ��������");
                    this.fpStockApprove_Sheet1.ActiveRowIndex = i;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ��ȡ������˾/����������Ϣ �����Թ�����˾�ĸ��ķ�������Tagʵ��
        /// </summary>
        private void PopStockCompany(int columnIndex)
        {
            //��ǰ��¼���С���
            int i = this.fpStockApprove_Sheet1.ActiveRowIndex;
            int j = this.fpStockApprove_Sheet1.ActiveColumnIndex;
            //���������򷵻�
            if (this.fpStockApprove_Sheet1.RowCount == 0)
            {
                return;
            }

            if (i < 0) 
            { 
                return; 
            }

            FS.HISFC.Models.Material.InputPlan stockPlanTemp = this.fpStockApprove_Sheet1.Rows[i].Tag as FS.HISFC.Models.Material.InputPlan;

            if (columnIndex == (int)ColumnStockSet.ColCompany)
            {
                //��ù�����˾�б�
                if (this.alCompany == null)
                {
                    FS.HISFC.BizLogic.Material.ComCompany company = new FS.HISFC.BizLogic.Material.ComCompany();
                    this.alCompany = company.QueryCompany("1", "A");
                    if (this.alCompany == null)
                    {
                        MessageBox.Show("��ȡ������λ�б����");
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
                    this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Value = companyTemp.Name;       //������˾
                    stockPlanTemp.Company = companyTemp;
                }
            }
            if (columnIndex == (int)ColumnStockSet.ColProduceName)
            {
                //��ù�����˾�б�
                if (this.alProduce == null)
                {
                    FS.HISFC.BizLogic.Material.ComCompany company = new FS.HISFC.BizLogic.Material.ComCompany();
                    this.alProduce = company.QueryCompany("0", "A");
                    if (this.alProduce == null)
                    {
                        MessageBox.Show("��ȡ���������б����");
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
                    this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColProduceName].Value = producTemp.Name;     //������˾                    
                    stockPlanTemp.Producer = producTemp;
                }
            }
        }

        /// <summary>
        /// ���������ļ���ؼ�
        /// </summary>
        public void PopExpandData()
        {
            if (this.fpStockApprove_Sheet1.Rows.Count <= 0)
                return;

            ucPhaExpand uc = new ucPhaExpand();

            uc.IsOnlyPatientInOut = true;

            FS.HISFC.Models.Material.InputPlan plan = this.fpStockApprove_Sheet1.Rows[this.fpStockApprove_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Material.InputPlan;
            FS.FrameWork.Models.NeuObject tempItem = plan.StoreBase.Item;

            FS.FrameWork.Models.NeuObject tempDept = new FS.FrameWork.Models.NeuObject();
            tempDept.ID = "AAAA";
            tempDept.Name = "ȫԺ";

            uc.SetData(tempDept, tempItem, 10);

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

            //FS.FrameWork.Management.Transaction t = new Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.planManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ArrayList alSavePlanList = new ArrayList();

            for (int i = 0; i < this.fpStockApprove_Sheet1.Rows.Count; i++)
            {
                FS.HISFC.Models.Material.InputPlan plan = this.fpStockApprove_Sheet1.Rows[i].Tag as FS.HISFC.Models.Material.InputPlan;
                if (plan == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ɹ��ƻ�����ʱ ��������ת������");
                    return -1;
                }

                #region �ɹ��ƻ���ֵ

                //������Ա��Ϣ
                plan.StoreBase.Operation.Oper.ID = this.planManager.Operator.ID;
                plan.StoreBase.Operation.Oper.OperTime = sysTime;

                plan.StoreBase.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text);     //��Ʒ�����
                plan.StockNum = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text) * plan.StoreBase.Item.PackQty;   //�ɹ�����
                plan.PlanNum = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text);
                plan.PlanPrice = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text);
                plan.PlanCost = plan.PlanNum * plan.PlanPrice;

                if (this.winFun == EnumWindowFun.�ɹ��ƻ�)
                {
                    #region �ɹ��ƻ��ƶ�

                    plan.StoreBase.PriceCollection.PurchasePrice = plan.PlanPrice;        //��Ʒ����� ��ֵΪ ��Ʒ�ƻ������
                    plan.StockOper = plan.StoreBase.Operation.Oper;                       //�ɹ���
                    //�繩����˾Ϊ���� �򲻸ı�ƻ���״̬
                    if (this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Text.Trim() == "����")
                        plan.State = this.listState;
                    else
                        plan.State = this.saveState;
                    //��ɹ�����Ҫ��� ��ֱ������״̬Ϊ2 ���
                    if (!this.isNeedApprove)
                    {
                        plan.StoreBase.Operation.ApproveOper = plan.StoreBase.Operation.Oper;
                        //plan.State = this.saveState;
                        plan.State = "2";
                    }

                    #endregion
                }
                else           //�ɹ���˹���
                {
                    plan.StoreBase.Operation.ExamOper.ID = this.planManager.Operator.ID;
                    plan.StoreBase.Operation.ExamOper.OperTime = sysTime;
                    plan.StoreBase.Operation.ApproveOper = plan.StoreBase.Operation.Oper;
                    plan.State = this.saveState;
                }

                #endregion

                alSavePlanList.Add(plan);
            }

            #region �ɹ��ƻ�����

            //FS.HISFC.Models.Material.InputPlan planInfo = new FS.HISFC.Models.Material.InputPlan();
            foreach (FS.HISFC.Models.Material.InputPlan input in alSavePlanList)
            {
                //planInfo = input;


                int param = this.planManager.SaveStockPlan(input);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�ɹ��ƻ���Ϣ����ʧ��" + this.itemManager.Err);
                    return -1;
                }
                if (param == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���ƻ�������ɾ����������ɹ��ƻ� �������ƻ���Ա��ϵ");
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�");

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
        //��ʱ����
        //private ucSplitPlan ucSplitPlan = null;

        /// <summary>
        /// ���ƻ�����ʾ
        /// </summary>
        /// <returns></returns>
        public int PopInPlanList()
        {
            if (this.ucMergeList == null)
            {
                this.ucMergeList = new ucPlanList();
            }

            this.ucMergeList.OperPrivDept = this.privDept;              //Ȩ�޿���
            this.ucMergeList.State = this.popPlanListState;                    //����״̬ 0

            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucMergeList);
            if (this.ucMergeList.Result == DialogResult.OK)     //��ʾ���ݺϲ�
            {
                ArrayList alterPlan = this.ucMergeList.AlterInPlan;

                this.Clear();

                foreach (FS.HISFC.Models.Material.InputPlan inPlanObj in alterPlan)
                {
                    this.AddDataToFp(inPlanObj);
                }
            }
            this.SetSum();
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
            this.fpHistory_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// ������ʷ�ɹ���Ϣ
        /// </summary>
        /// <param name="stockPlan">��ʷ�ɹ���Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int AddHistoryDataToFp(FS.HISFC.Models.Material.InputPlan stockPlan)
        {
            #region ��Ʒ��Ϣ

            FS.HISFC.Models.Material.MaterialItem tempItem = new FS.HISFC.Models.Material.MaterialItem();
            tempItem = this.itemManager.GetMetItemByMetID(stockPlan.StoreBase.Item.ID);
            if (tempItem == null)
            {
                Function.ShowMsg("δ��ȷ��ȡ��Ʒ��Ϣ" + this.itemManager.Err);
                return -1;
            }

            #endregion

            int iRowIndx = this.fpHistory_Sheet1.Rows.Count;

            this.fpHistory_Sheet1.Rows.Add(iRowIndx, 1);
            this.fpHistory_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColInTime].Value = stockPlan.StoreBase.Operation.ApproveOper.OperTime;									//�������
            this.fpHistory_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColStockQty].Text = stockPlan.StockNum.ToString();//�ɹ�����
            this.fpHistory_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColUnit].Text = tempItem.PackUnit;											//��λ
            this.fpHistory_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColStockPrice].Text = stockPlan.StoreBase.PriceCollection.PurchasePrice.ToString();								//�����
            this.fpHistory_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColCompany].Text = stockPlan.Company.Name;											//������˾
            this.fpHistory_Sheet1.Cells[iRowIndx, (int)ColumnHistorySet.ColProduce].Text = stockPlan.Producer.Name;							//��������

            return 1;
        }

        /// <summary>
        /// ������ʷ�ɹ���Ϣ
        /// </summary>
        /// <param name="alHistory"></param>
        private void AddHistoryDataToFp(ArrayList alHistory)
        {
            foreach (FS.HISFC.Models.Material.InputPlan info in alHistory)
            {
                this.AddHistoryDataToFp(info);
            }
        }

        /// <summary>
        /// ��ʾ��ʷ�ɹ���¼
        /// </summary>
        protected void ShowHistoryData()
        {
            if (this.fpStockApprove_Sheet1.RowCount <= 0)
                return;

            if (this.alPlanHistory.Count > this.fpStockApprove_Sheet1.ActiveRowIndex)
            {
                this.ClearHistoryData();

                //��ʾTabҳ����ʾ��Ϣ
                this.tbStockHistory.Text = this.fpStockApprove_Sheet1.Cells[this.fpStockApprove_Sheet1.ActiveRowIndex, (int)ColumnStockSet.ColTradeName].Text + "[" + this.fpStockApprove_Sheet1.Cells[this.fpStockApprove_Sheet1.ActiveRowIndex, (int)ColumnStockSet.ColSpecs].Text + "]" + " ��ʷ�ɹ���Ϣ";

                this.AddHistoryDataToFp(this.alPlanHistory[this.fpStockApprove_Sheet1.ActiveRowIndex] as ArrayList);
            }
        }

        #endregion

        #region ������

        /// <summary>
        /// ��ⵥ�б���ʾ
        /// </summary>
        private void ShowPlanList()
        {
            this.tvList.ShowStockPlanList(this.privDept, this.listState);
        }

        #endregion

        #region �¼�
        private void ucStockPlan_Load(object sender, System.EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.privOper = this.itemManager.Operator;

                //string class2Priv = "0512";
                //if (this.winFun == EnumWindowFun.�ɹ��ƻ�)
                //{
                //    class2Priv = "0512";
                //}
                //else
                //{
                //    class2Priv = "0513";
                //}

                //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
                //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept(class2Priv, ref testPrivDept);
                //if (parma == -1)            //��Ȩ��
                //{
                //    MessageBox.Show("���޴˴��ڲ���Ȩ��");
                //    return;
                //}
                //else if (parma == 0)       //�û�ѡ��ȡ��
                //{
                //    return;
                //}

                //this.privDept = testPrivDept;

                //base.OnStatusBarInfo(null, "�������ң� " + testPrivDept.Name);

                this.InitData();

                this.ShowPlanList();
            }
        }

        private void fpStockApprove_EditModeOff(object sender, EventArgs e)
        {
            if (this.fpStockApprove_Sheet1.RowCount == 0)
            {
                return;
            }
            int i = this.fpStockApprove_Sheet1.ActiveRowIndex;

            if (this.fpStockApprove_Sheet1.ActiveColumnIndex == (int)ColumnStockSet.ColStockPrice || this.fpStockApprove_Sheet1.ActiveColumnIndex == (int)ColumnStockSet.ColPlanNum)
            {
                
                if (this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColTradeName].Text == "�ϼ�")
                {
                    return; 
                }
                //����ƻ����
                try
                {
                    this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveCost].Text = 
                        (NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text) * NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text)).ToString();

                    this.SetSum();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                    return;
                }
            }
            //�Ա�����޸Ĺ������� ���¸�ֵtag by yuyun 08-7-31{C5CF9164-BA45-4fb6-AA9F-506EC4B3FA42}
            FS.HISFC.Models.Material.InputPlan plan = new FS.HISFC.Models.Material.InputPlan();
            plan = this.fpStockApprove_Sheet1.Rows[i].Tag as FS.HISFC.Models.Material.InputPlan;

            if (this.fpStockApprove_Sheet1.ActiveColumnIndex == (int)ColumnStockSet.ColStockPrice)
            {
                plan.PlanPrice = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColStockPrice].Text);
                plan.PlanCost = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveCost].Text);
            }
            else if (this.fpStockApprove_Sheet1.ActiveColumnIndex == (int)ColumnStockSet.ColPlanNum)
            {
                plan.PlanNum = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColPlanNum].Text);
                plan.PlanCost = NConvert.ToDecimal(this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColApproveCost].Text);
            }

            this.fpStockApprove_Sheet1.Rows[i].Tag = plan;

        }

        /// <summary>
        /// ����س���ת�����¼�ͷ�ƶ�ʱ�ı� ��ʷ�ɹ���ʾ
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (this.fpStockApprove.ContainsFocus)
            {
                if (keyData == Keys.Enter)
                {
                    #region ������ת
                    int iRow = this.fpStockApprove_Sheet1.ActiveRowIndex;
                    int iColumn = this.fpStockApprove_Sheet1.ActiveColumnIndex;

                    switch (iColumn)
                    {
                        case (int)ColumnStockSet.ColStockPrice:
                            this.fpStockApprove_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColCompany;
                            break;
                        case (int)ColumnStockSet.ColCompany:
                            this.fpStockApprove_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColProduceName;
                            break;
                        case (int)ColumnStockSet.ColProduceName:
                            this.fpStockApprove_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColMemo;
                            break;
                        case (int)ColumnStockSet.ColMemo:
                            this.fpStockApprove_Sheet1.ActiveColumnIndex = 0;		//ʹ��������ת����һ�� ����ֱ����ת���۸񿴲�����һ��
                            this.fpStockApprove_Sheet1.ActiveColumnIndex = (int)ColumnStockSet.ColStockPrice;
                            this.fpStockApprove_Sheet1.ActiveRowIndex = this.fpStockApprove_Sheet1.ActiveRowIndex + 1;
                            this.ShowHistoryData();
                            break;
                    }
                    return true;
                    #endregion
                }
                if (keyData == Keys.Up)
                {
                    if (this.fpStockApprove_Sheet1.ActiveRowIndex != 0)
                        this.fpStockApprove_Sheet1.ActiveRowIndex = this.fpStockApprove_Sheet1.ActiveRowIndex - 1;
                    this.ShowHistoryData();
                    return true;
                }
                if (keyData == Keys.Down)
                {
                    if (this.fpStockApprove_Sheet1.ActiveRowIndex != this.fpStockApprove_Sheet1.Rows.Count - 1)
                    {
                        this.fpStockApprove_Sheet1.ActiveRowIndex = this.fpStockApprove_Sheet1.ActiveRowIndex + 1;
                    }
                    this.ShowHistoryData();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// �������ʷ��Ʒ�ɹ���¼�ĵ�������
        /// </summary>
        private void fpStockApprove_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //��ǰ��¼���С���
            int i = this.fpStockApprove_Sheet1.ActiveRowIndex;
            int j = this.fpStockApprove_Sheet1.ActiveColumnIndex;

            //�س������� 13 �ո������ 32
            if (e.KeyChar == 32)
            {
                this.PopStockCompany(j);
            }
            else
            {      //���µ�ΪBackspace��
                if (e.KeyChar == (char)8 && j == (int)ColumnStockSet.ColCompany)
                {
                    this.fpStockApprove_Sheet1.Cells[i, (int)ColumnStockSet.ColCompany].Value = "����";  //������˾
                }
            }
        }

        /// <summary>
        /// �������ʷ��Ʒ�ɹ���¼�ĵ�������
        /// </summary>
        private void fpStockApprove_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            //��������Ϊ�л��б�����ֱ�ӷ���
            if (e.ColumnHeader || e.RowHeader)
                return;
            //���ܴ���Ϊ�ɹ���� �Ҳ������޸Ĳɹ�����Ϣ
            if (this.winFun == EnumWindowFun.�ɹ���� && !this.isCanEditWhenApprove)
                return;

            this.PopStockCompany(e.Column);
        }

        /// <summary>
        /// ѡ��ͬ��ʱ��ʾ��ͬ��ʷ�ɹ���Ϣ
        /// </summary>
        private void fpStockApprove_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.ShowHistoryData();
        }

        private void tvList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Clear();

            if (e.Node != null && e.Node.Parent != null)
            {
                FS.FrameWork.Models.NeuObject inPlanObj = e.Node.Tag as FS.FrameWork.Models.NeuObject;

                this.ShowStockData(inPlanObj.ID, inPlanObj.Name);
            }
        }

        private void fpStockApprove_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {

        }

        private void fpStockApprove_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.ShowHistoryData();
        }

        /// <summary>
        /// ��������˾����{C5CF9164-BA45-4fb6-AA9F-506EC4B3FA42}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            List<FS.HISFC.Models.Material.InputPlan> listPlan = new List<FS.HISFC.Models.Material.InputPlan>();

            foreach (FarPoint.Win.Spread.Row r in this.fpStockApprove_Sheet1.Rows)
            {
                listPlan.Add(this.fpStockApprove_Sheet1.Rows[r.Index].Tag as FS.HISFC.Models.Material.InputPlan);
            }

            if (listPlan == null || listPlan.Count <= 0)
            {
                MessageBox.Show("û��������Ҫ������");

                return -1;
            }
            //������е����ݰ�������˾�����ʱ�������
            FS.HISFC.BizProcess.Integrate.Material.MaterialSort.SortStockPlanByCompany(ref listPlan);

            this.SetDateToExport(listPlan);

            this.Clear();

            foreach (FS.HISFC.Models.Material.InputPlan input in listPlan)
            {
                this.AddDataToFp(input); 
            }
            return 1;
        }

        /// <summary>
        /// ��������list��������˾����{C5CF9164-BA45-4fb6-AA9F-506EC4B3FA42}
        /// </summary>
        /// <param name="listPlan"></param>
        private void SetDateToExport(List<FS.HISFC.Models.Material.InputPlan> listPlan)
        {
            try
            {
                //��������list��������µ�list�﹩��ӡ
                List<FS.HISFC.Models.Material.InputPlan>[] alExport = new List<FS.HISFC.Models.Material.InputPlan>[100];
                for (int i = 0; i < 100; i++)
                {
                    alExport[i] = new List<FS.HISFC.Models.Material.InputPlan>();
                }

                string companyID = string.Empty;
                companyID = listPlan[0].Company.ID;
                alExport[0].Add(listPlan[0]);
                int index = 0;

                if (listPlan.Count > 1)
                {
                    for (int i = 1; i < listPlan.Count; i++)
                    {
                        if (listPlan[i].Company.ID == companyID)
                        {
                            alExport[index].Add(listPlan[i]);
                        }
                        else
                        {
                            index++;
                            companyID = listPlan[i].Company.ID;
                            alExport[index].Add(listPlan[i]);
                        }
                    }
                }

                for (int i = 0; i <= index; i++)
                {
                    this.ExportInfo(alExport[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// ����{C5CF9164-BA45-4fb6-AA9F-506EC4B3FA42}
        /// </summary>
        /// <param name="list"></param>
        private void ExportInfo(List<FS.HISFC.Models.Material.InputPlan> list)
        {
            //��������list�浽fpStockApprove_Sheet1����
            this.fpStockApprove_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.Material.InputPlan input in list)
            {
                this.AddDataToFp(input);
            }
            try
            {
                string fileName = "";

                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel (*.xls)|*.*";
                dlg.FileName = list[0].Company.Name + "-" + planManager.GetSysDate();
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.fpStockApprove.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            /// 0 ��Ʒ����
            /// </summary>
            ColTradeName,
            /// <summary>
            /// 1 ���
            /// </summary>
            ColSpecs,
            /// <summary>
            /// 2 �ƻ������
            /// </summary>
            ColStockPrice,
            /// <summary>
            /// 3 �ƻ�����
            /// </summary>
            ColPlanNum,
            /// <summary>
            /// 4 ��λ
            /// </summary>
            ColUnit,
            /// <summary>
            /// 5 ��˽��
            /// </summary>
            ColApproveCost,
            /// <summary>
            /// 6 ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// 7 ��������
            /// </summary>
            ColProduceName,
            /// <summary>
            /// 8 ��ע
            /// </summary>
            ColMemo,
            /// <summary>
            /// 9 ���ҿ��
            /// </summary>
            ColOwnStockNum,
            /// <summary>
            /// 10 ȫԺ���
            /// </summary>
            ColAllStockNum
        }

        /// <summary>
        /// �ɹ��ƻ�������
        /// </summary>
        private enum ColumnHistorySet
        {
            /// <summary>
            /// 0 �������
            /// </summary>
            ColInTime,
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
            /// 4 ������˾
            /// </summary>
            ColCompany,
            /// <summary>
            /// 5 ��������
            /// </summary>
            ColProduce,
            /// <summary>
            /// 6 ��ע
            /// </summary>
            ColMemo
        }

        #endregion

        #region IPreArrange ��Ա

        public int PreArrange()
        {
            string class2Priv = "0512";
            if (this.winFun == EnumWindowFun.�ɹ��ƻ�)
            {
                class2Priv = "0512";
            }
            else
            {
                class2Priv = "0513";
            }

            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept(class2Priv, ref testPrivDept);
            if (parma == -1)            //��Ȩ��
            {
                MessageBox.Show("���޴˴��ڲ���Ȩ��");
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

using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Neusoft.FrameWork.Function;
using DiseasePay.Models;
using DiseasePay.BizLogic;

namespace FoShanDiseasePay
{
    /// <summary>
    /// 药品/医用耗材的采购结算信息
    /// </summary>
    public partial class ucMaterialPay : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucMaterialPay()
        {
            InitializeComponent();
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[8].Visible = false;
            this.neuSpread1_Sheet1.Columns[9].Visible = false;
            this.neuSpread1_Sheet1.Columns[10].Visible = false;
            this.neuSpread1_Sheet1.Columns[11].Visible = false;
        }

        #region 私有字段
        private bool isDrug = false;

        [Category("设置"), Description("是否药品")]
        public bool IsDrug
        {
            get { return isDrug; }
            set { isDrug = value; }
        }

        private DiseasePay.BizLogic.BalanceManager payMgr = new DiseasePay.BizLogic.BalanceManager();

        /// <summary>
        /// 费用管理类
        /// </summary>
        //private Neusoft.HISFC.BizProcess.Material.Pay.payMgr payMgr = new Neusoft.HISFC.BizProcess.Material.Pay.payMgr();

        /// <summary>
        /// 综合管理类
        /// </summary>
        //private Neusoft.HISFC.Interface.Material.InterfaceProxy.ManagerProxy managerProxy = new Neusoft.HISFC.Interface.Material.InterfaceProxy.ManagerProxy();

        /// <summary>
        /// 打印控件代理
        /// </summary>
        //private Neusoft.HISFC.Interface.Material.InterfaceProxy.BillPrintProxy printProxy = new Neusoft.HISFC.Interface.Material.InterfaceProxy.BillPrintProxy();

        /// <summary>
        /// 供货单位
        /// </summary>
        private ArrayList alCompany = new ArrayList();

        /// <summary>
        /// 结存明细信息
        /// </summary>
        //private ArrayList alPayDetail = new ArrayList();

        /// <summary>
        /// 查询结存标志
        /// </summary>
        private string payFlag;

        /// <summary>
        /// 权限科室
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject privDept;

        /// <summary>
        /// 操作员
        /// </summary>
        private Neusoft.FrameWork.Models.NeuObject privOper;

        /// <summary>
        /// 工具栏
        /// </summary>
        private Neusoft.FrameWork.WinForms.Forms.ToolBarService toolBarService = new Neusoft.FrameWork.WinForms.Forms.ToolBarService();

        //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        /// <summary>
        /// 结存明细信息
        /// </summary>
        private Hashtable htPayDetail = new Hashtable();
        /// <summary>
        /// 打印用流水单号
        /// </summary>
        private string strPrintPayListNo = null;

        /// <summary>
        /// 结存列表
        /// </summary>
        private List<DiseasePay.Models.BalanceHead> PayHeadList = new List<DiseasePay.Models.BalanceHead>();

        /// <summary>
        /// 供货商列表
        /// </summary>
        private List<Neusoft.FrameWork.Models.NeuObject> companyList = new List<Neusoft.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 供货公司名称
        /// </summary>
        /// {38AE0936-69C9-4543-BB4E-78998C7CCE94}
        private Hashtable htCompanyName = new Hashtable();
        #endregion

        #region 继承方法

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }

            base.OnLoad(e);
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        protected override int OnSave(object sender, object NeuObject)
        {
            if (this.rbPay.Checked)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("已结算记录不能进行付款"));
                return 0;
            }
            if (this.SavePay() == 1)
            {
                this.Query();
            }
            return 1;
        }

        public override int Export(object sender, object NeuObject)
        {
            if (this.neuSpread2.Export() == 1)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("导出成功"));
            }
            return 1;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            string type = isDrug ? "1" : "0";
            companyList = payMgr.QueryCompany(type);
            if (companyList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取供货商信息错误" + payMgr.Err));
                return;
            }
            //{38AE0936-69C9-4543-BB4E-78998C7CCE94}
            Neusoft.FrameWork.Models.NeuObject obj = new Neusoft.FrameWork.Models.NeuObject();
            obj.ID = "AAAA";
            obj.Name = Neusoft.FrameWork.Management.Language.Msg("全部");
            companyList.Insert(0, obj);
            this.cmbCompany.AddItems(new ArrayList(companyList.ToArray()));
            //{38AE0936-69C9-4543-BB4E-78998C7CCE94}
            foreach (Neusoft.FrameWork.Models.NeuObject company in companyList)
            {
                if (!this.htCompanyName.Contains(company.ID))
                {
                    this.htCompanyName.Add(company.ID, company.Name);
                }
            }

            DateTime sysTime = payMgr.GetDateTimeFromSysDateTime().AddDays(1);
            this.dtpStartDate.Text = sysTime.AddDays(-30).ToString();
            this.dtpEndDate.Text = sysTime.ToString();

            this.payFlag = "'0','1'";

            //FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            //comboBoxCellType.Items = Enum.GetNames(typeof(EnumPayType));
            //this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].CellType = comboBoxCellType;

            #region 注释掉 不要了
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            //cmbQueryCondition.Items.AddRange(Enum.GetNames(typeof(EnumSearchContion)));

            //cmbQueryCondition.Tag = EnumSearchContion.发票号.ToString();
            //cmbQueryCondition.SelectedItem.ID = EnumSearchContion.发票号.ToString();
            #endregion

            //{488136F1-77CC-4c26-973D-AF9C79467030}
            PayHeadList = this.payMgr.QueryPayList("");
            if (PayHeadList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取结存信息失败" + this.payMgr.Err));
                return;
            }
            this.SetFpCellType();
        }

        /// <summary>
        /// 多条件查询结存信息
        /// </summary>
        private void Query()
        {
            //{488136F1-77CC-4c26-973D-AF9C79467030}
            PayHeadList = this.payMgr.QueryPayList("");
            if (PayHeadList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取结存信息失败" + this.payMgr.Err));
                return;
            }

            if (this.cmbCompany.SelectedItem == null)
            {
                //MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("请选择供货公司"));
                return;
            }
            List<BalanceHead> payList = new List<BalanceHead>();
            Neusoft.FrameWork.Models.NeuObject currentCompany = this.cmbCompany.SelectedItem as Neusoft.FrameWork.Models.NeuObject;
            payList = this.payMgr.QueryPayList(this.privDept.ID, currentCompany.ID, this.payFlag, this.dtpStartDate.Value.Date, this.dtpEndDate.Value);
            if (payList == null)
            {
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取结存信息失败" + this.payMgr.Err));
                return;
            }

            //List<Input> inputList = inputMgr.QueryInputTotalForpay(this.privDept.ID, currentCompany.ID, this.dtpStartDate.Value.Date, this.dtpEndDate.Value);

            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;

            BalanceHead info = null;
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            //this.alPayDetail = new ArrayList();
            this.htPayDetail = new Hashtable();

            for (int i = 0; i < payList.Count; i++)
            {
                info = payList[i] as BalanceHead;
                if (info == null || info.PayHeadNo == "")
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("处理第") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("行结存汇总信息出错"));
                    continue;
                }
                List<BalanceHead> listTemp = this.payMgr.QueryPayDetail(info.PayHeadNo, info.InvoiceNo);
                if (listTemp == null)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取第") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("行结存明细信息出错") + this.payMgr.Err);
                    continue;
                }
                if (listTemp.Count > 0)
                {
                    //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
                    //this.alPayDetail.Add(listTemp);
                    htPayDetail.Add(info.PayHeadNo, listTemp);
                }
                this.AddPayHeadData(info);
            }
            this.SetStyle();
        }

        /// <summary>
        /// 
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        private void QueryInfo()
        {
            #region 注释掉 不要了
            #region 获得查询条件

            //string sqlWhere = string.Empty;
            //string codeCondition = string.Empty;
            //switch (this.cmbQueryCondition.Text)
            //{
            //    case "发票号":
            //        sqlWhere = "\nwhere t.invoice_no = '" + this.txtSearchCondition.Text + "'";
            //        break;
            //    case "入库单号":
            //        sqlWhere = "\nwhere t.in_list_code = '" + this.txtSearchCondition.Text.PadLeft(10, '0').ToString() + "'";
            //        break;
            //    default:
            //        break;
            //}

            #endregion
            //List<PayHead> payList = new List<PayHead>();
            //payList = this.payMgr.QueryPayList(sqlWhere);
            //if (payList == null)
            //{
            //    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取结存信息失败" + this.payMgr.Err));
            //    return;
            //}
            #endregion

            //{488136F1-77CC-4c26-973D-AF9C79467030}
            string codeCondition = string.Empty;
            List<BalanceHead> tempPayList = new List<BalanceHead>();
            //switch (this.cmbQueryCondition.Text)
            //{
            //case "发票号":
            //    codeCondition = this.txtSearchCondition.Text;
            //    foreach (PayHead tempPay in this.PayHeadList)
            //    {
            //        if (tempPay.InvoiceNo == codeCondition)
            //        {
            //            tempPayList.Add(tempPay);
            //        }
            //    }

            //    break;

            //case "入库单号":
            //    codeCondition = this.txtSearchCondition.Text.PadLeft(10, '0').ToString();
            //    foreach (PayHead tempPay in this.PayHeadList)
            //    {
            //        if (tempPay.InListCode == codeCondition)
            //        {
            //            tempPayList.Add(tempPay);
            //        }
            //    }

            //    break;

            //    default:

            //        break;

            //}
            if (tempPayList.Count == 0)
            {
                return;
            }
            foreach (Neusoft.FrameWork.Models.NeuObject company in companyList)
            {
                if (company.ID == tempPayList[0].Company.ID)
                {
                    this.cmbCompany.Text = company.Name;
                    break;
                }
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;

            BalanceHead info = null;
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            //this.alPayDetail = new ArrayList();
            this.htPayDetail = new Hashtable();

            for (int i = 0; i < tempPayList.Count; i++)
            {
                info = tempPayList[i] as BalanceHead;
                if (info == null || info.PayHeadNo == "")
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("处理第") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("行结存汇总信息出错"));
                    continue;
                }
                List<BalanceHead> listTemp = this.payMgr.QueryPayDetail(info.PayHeadNo, info.InvoiceNo);
                if (listTemp == null)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("获取第") + (i + 1).ToString() + Neusoft.FrameWork.Management.Language.Msg("行结存明细信息出错") + this.payMgr.Err);
                    continue;
                }
                if (listTemp.Count > 0)
                {
                    //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
                    //this.alPayDetail.Add(listTemp);
                    htPayDetail.Add(info.PayHeadNo, listTemp);
                }
                this.AddPayHeadData(info);
                if (info.PayState == "2")
                {
                    rbPay.Checked = true;
                }
                else
                {
                    rbUnPay.Checked = true;
                }
            }

        }

        /// <summary>
        /// 向结存汇总信息FarPoint内加入数据
        /// </summary>
        private void AddPayHeadData(DiseasePay.Models.BalanceHead pay)
        {
            int rowCount = this.neuSpread1_Sheet1.Rows.Count;

            this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColChoose].Value = true;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColChoose].Locked = false;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceNo].Text = pay.InvoiceNo;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceDate].Value = pay.InvoiceTime;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInvoiceCost].Value = pay.PurchaseCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDiscountCost].Value = pay.DiscountCost;
            //应付金额通过FarPoint公式自动设置
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPaidUpCost].Value = pay.PayCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCost].Value = pay.UnpayCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDeliveryCost].Value = pay.DeliveryCost;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayType].Value = pay.PayType;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColDept].Value = this.privDept.Name;
            this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColInListCode].Value = pay.InListCode;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].Locked = false;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].BackColor = System.Drawing.Color.SeaShell;
            //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayType].BackColor = Color.SeaShell;
            //{38AE0936-69C9-4543-BB4E-78998C7CCE94}
            if (this.htCompanyName.Contains(pay.Company.ID))
            {
                Neusoft.FrameWork.Models.NeuObject currentCompany = this.cmbCompany.SelectedItem as Neusoft.FrameWork.Models.NeuObject;
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColCompanyName].Value = htCompanyName[pay.Company.ID].ToString();
                //{E5979899-1CA2-4ed1-A86C-B7E41031E044}
                //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenAccounts].Value = currentCompany.OpenAccounts;
                //this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColOpenBank].Value = currentCompany.OpenBank;
            }
            this.neuSpread1_Sheet1.Rows[rowCount].Tag = pay;
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        private void SetFpCellType()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = 4;// Function.MoneyDecimalPlaces;

            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPaidUpCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColInvoiceCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDue].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].CellType = numberCellType;
            this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].CellType = numberCellType;

            this.neuSpread2_Sheet1.Columns[(int)ColPayDetailSet.ColDeliveryCost].CellType = numberCellType;
            this.neuSpread2_Sheet1.Columns[(int)ColPayDetailSet.ColPayCost].CellType = numberCellType;

            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = Neusoft.FrameWork.Management.Language.Msg("付款");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = Neusoft.FrameWork.Management.Language.Msg("发票号");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = Neusoft.FrameWork.Management.Language.Msg("发票日期");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = Neusoft.FrameWork.Management.Language.Msg("发票金额");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = Neusoft.FrameWork.Management.Language.Msg("优惠金额");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = Neusoft.FrameWork.Management.Language.Msg("应付金额");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = Neusoft.FrameWork.Management.Language.Msg("已付金额");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = Neusoft.FrameWork.Management.Language.Msg("本次付款");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = Neusoft.FrameWork.Management.Language.Msg("运费");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = Neusoft.FrameWork.Management.Language.Msg("付款类型");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = Neusoft.FrameWork.Management.Language.Msg("开户银行");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = Neusoft.FrameWork.Management.Language.Msg("银行帐号");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 12).Value = Neusoft.FrameWork.Management.Language.Msg("入库科室");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 13).Value = Neusoft.FrameWork.Management.Language.Msg("入库单据号");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 14).Value = Neusoft.FrameWork.Management.Language.Msg("付款凭证");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 15).Value = Neusoft.FrameWork.Management.Language.Msg("未付款凭证");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 16).Value = Neusoft.FrameWork.Management.Language.Msg("未付款凭证日期");
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 17).Value = Neusoft.FrameWork.Management.Language.Msg("供货商");

            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = Neusoft.FrameWork.Management.Language.Msg("发票号");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = Neusoft.FrameWork.Management.Language.Msg("本次付款");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = Neusoft.FrameWork.Management.Language.Msg("运费");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = Neusoft.FrameWork.Management.Language.Msg("付款类型");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = Neusoft.FrameWork.Management.Language.Msg("开户银行");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = Neusoft.FrameWork.Management.Language.Msg("银行帐号");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = Neusoft.FrameWork.Management.Language.Msg("付款人");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = Neusoft.FrameWork.Management.Language.Msg("付款凭证");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = Neusoft.FrameWork.Management.Language.Msg("未付款凭证");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = Neusoft.FrameWork.Management.Language.Msg("未付款凭证日期");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = Neusoft.FrameWork.Management.Language.Msg("付款日期");
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 11).Value = Neusoft.FrameWork.Management.Language.Msg("流水单号");
        }

        /// <summary>
        /// 向结存明细FarPoint内加入数据
        /// </summary>
        /// <param name="al">供货商结存实体数组</param>
        private void AddPayDetailData(List<BalanceHead> list)
        {
            foreach (BalanceHead pay in list)
            {
                int rowCount = this.neuSpread2_Sheet1.Rows.Count;
                this.neuSpread2_Sheet1.Rows.Add(rowCount, 1);

                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColInvoiceNo].Value = pay.InvoiceNo;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCost].Value = pay.PayCost;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColDeliveryCost].Value = pay.DeliveryCost;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayType].Text = pay.PayType;
                //this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenBank].Value = pay.Company.OpenBank;
                //this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenAccounts].Value = pay.Company.OpenAccounts;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayOper].Value = "";// this.managerProxy.GetEmployeeInfoByID(pay.PayOper.ID).Name;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCredence].Text = pay.PayCredence;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnpayCredence].Text = pay.UnpayCredence;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnCredenceDate].Text = pay.UnpayCredenceTime.ToString();
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayDate].Text = pay.OperDate.ToString();
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayListNum].Text = pay.ListNum;

                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColInvoiceNo].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCost].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColDeliveryCost].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayType].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenBank].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColOpenAccounts].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayOper].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayCredence].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnpayCredence].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColUnCredenceDate].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayDate].Locked = true;
                this.neuSpread2_Sheet1.Cells[rowCount, (int)ColPayDetailSet.ColPayListNum].Locked = true;
                this.neuSpread2_Sheet1.Rows[rowCount].Tag = pay;
            }
        }

        /// <summary>
        /// 显示供货商结存信息
        /// </summary>
        private void ShowPayDetail()
        {
            //{45FA88B5-A77C-4e2b-B6CE-8364424B0126}
            #region 代码不起作用
            //if (this.alPayDetail != null && this.alPayDetail.Count > 0)
            //{
            //    this.neuSpread2_Sheet1.Rows.Count = 0;

            //    if (this.alPayDetail.Count <= this.neuSpread1_Sheet1.ActiveRowIndex)
            //    {
            //        return;
            //    }

            //    this.AddPayDetailData(this.alPayDetail[this.neuSpread1_Sheet1.ActiveRowIndex] as List<PayHead>);
            //}
            #endregion
            if (this.htPayDetail != null && this.htPayDetail.Count > 0)
            {
                this.neuSpread2_Sheet1.Rows.Count = 0;
                if (this.htPayDetail.ContainsKey((this.neuSpread1_Sheet1.ActiveRow.Tag as BalanceHead).PayHeadNo))
                {
                    this.AddPayDetailData(this.htPayDetail[(this.neuSpread1_Sheet1.ActiveRow.Tag as BalanceHead).PayHeadNo] as List<BalanceHead>);
                }
            }
        }

        /// <summary>
        /// 清屏操作
        /// </summary>
        private void Clear()
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            this.neuSpread2_Sheet1.Rows.Count = 0;
        }

        /// <summary>
        /// {DCAEF089-F156-4a81-A476-0D29DBA7AEFD}
        /// [功能：改变可修改状态]
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetStyle()
        {
            for (int columnCount = 0; columnCount < this.neuSpread1_Sheet1.ColumnCount; columnCount++)
            {
                this.neuSpread1_Sheet1.Columns[columnCount].Locked = true;
                this.neuSpread1_Sheet1.Columns[columnCount].BackColor = Color.White;
                this.neuSpread1_Sheet1.Columns[columnCount].Width = this.neuSpread1_Sheet1.Columns[columnCount].GetPreferredWidth();
            }
            if (rbUnPay.Checked && !rbPay.Checked)
            {
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].BackColor = Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].BackColor = Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].BackColor = Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnpayCredence].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCredence].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCredence].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenBank].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenBank].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenAccounts].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColOpenAccounts].BackColor = System.Drawing.Color.SeaShell;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].Locked = false;
                this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].BackColor = Color.SeaShell;
            }
            //for (int rowCount = 0; rowCount < this.neuSpread1_Sheet1.RowCount; rowCount++)
            //{
            //    if (rbPay.Checked == true)
            //    {
            //        //设置可编辑性
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].Locked = true;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].Locked = true;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].Locked = true;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].Locked = true;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = true;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].Locked = true;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = true;

            //        //设置颜色
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("优惠金额")].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("本次付款")].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("运费")].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].BackColor = Color.White;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = Color.White;
            //    }
            //    else if (rbUnPay.Checked == true)
            //    {

            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].Locked = false;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayCost].Locked = false;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDeliveryCost].Locked = false;
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColPayType].Locked = false;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].Locked = false;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].Locked = false;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].Locked = false;
            //        if (rbPay.Checked == true)

            //            //设置颜色
            //        this.neuSpread1_Sheet1.Columns[(int)ColPayHeadSet.ColDiscountCost].BackColor = Color.SeaShell;
            //        this.neuSpread1_Sheet1.Columns[GetColIndex("本次付款")].BackColor = Color.SeaShell;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnpayCredence].BackColor = System.Drawing.Color.SeaShell;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColPayCredence].BackColor = System.Drawing.Color.SeaShell;
            //        this.neuSpread1_Sheet1.Cells[rowCount, (int)ColPayHeadSet.ColUnCredenceDate].BackColor = System.Drawing.Color.SeaShell;
            //    }
            //}
        }

        /// <summary>
        /// 查找列索引
        /// </summary>
        /// <param name="lableName"></param>
        /// <returns></returns>
        protected int GetColIndex(string lableName)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Label == lableName)
                {
                    return i;
                }
            }
            //返回-1的时候fp会默认所有列，而不会抛异常
            return -2;
        }

        #region 公有方法
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns>成功返回1 失败返回-1</returns>
        public int SavePay()
        {
            if (!this.SaveValid())
            {
                return -1;
            }

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            this.payMgr.SetTrans(Neusoft.FrameWork.Management.PublicTrans.Trans);

            BalanceHead pay = null;
            int saveCount = 0;

            string tempPayListNum = this.payMgr.GetPayListNum(this.cmbCompany.Tag.ToString());

            this.strPrintPayListNo = tempPayListNum;



            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                //if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value == null || !((bool)this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value))
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value == null || !Neusoft.FrameWork.Function.NConvert.ToBoolean(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColChoose].Value))
                    continue;

                pay = this.neuSpread1_Sheet1.Rows[i].Tag as BalanceHead;
                if (pay == null)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("保存操作过程中 发生类型转换错误"));
                    return -1;
                }
                //已结存 不再次处理
                if (pay.PayState == "2")
                {
                    //MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("第"+(i+1)+"条记录已经结算，无法进行付款操作"));
                    continue;
                }

                if (pay.DiscountCost <= 0)
                {
                    //优惠金额
                    pay.DiscountCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDiscountCost].Value);
                    pay.UnpayCost = pay.UnpayCost - pay.DiscountCost;		//未付金额
                }
                //运费
                pay.DeliveryCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDeliveryCost].Value);
                pay.Oper.ID = this.privOper.ID;
                pay.OperDate = this.payMgr.GetDateTimeFromSysDateTime();

                if (this.payMgr.UpdateInsertPayHead(pay) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("更新供货商结存信息出错" + this.payMgr.Err));
                    return -1;
                }

                //付款类型
                pay.PayType = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayType].Text;
                if (pay.PayType == "")
                {
                    pay.PayType = "现金";
                }
                //开户银行
                //pay.Company.OpenBank = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenBank].Text;
                //银行帐号
                //pay.Company.OpenAccounts = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenAccounts].Text;
                pay.PayOper.ID = this.privOper.ID;
                //本次付款
                pay.PayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCost].Value);

                if (pay.PayCost == 0)
                {
                    continue;
                }
                //付款凭证
                pay.PayCredence = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCredence].Text;
                //未付款凭证
                pay.UnpayCredence = this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColUnpayCredence].Text;
                //未付款凭证日期
                pay.UnpayCredenceTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColUnCredenceDate].Text);
                pay.UnpayCost = pay.UnpayCost - pay.PayCost;

                //更新头表的金额信息
                int returnVal = this.payMgr.UpdateHeadPayInfo(pay);
                if (returnVal < 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.payMgr.Err);
                    return -1;
                }
                else if (returnVal == 0)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新时未发现相关头表记录");
                    return -1;
                }
                //获取单内序号
                int sequenceNo = pay.SequenceNo;
                returnVal = this.payMgr.GetInvoicePaySequence(pay.PayHeadNo, pay.InvoiceNo, ref sequenceNo);
                if (returnVal != 1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.payMgr.Err);
                    return -1;
                }
                pay.SequenceNo = sequenceNo;
                //插入该条付款明细信息
                returnVal = this.payMgr.InsertPayDetail(pay, tempPayListNum);
                if (returnVal != 1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(this.payMgr.Err);
                    return -1;
                }
                saveCount++;
            }
            if (saveCount <= 0)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("没有选中或没有可保存的记录"));
                return 0;
            }


            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("保存成功"));
            //this.Print();
            return 1;
        }

        #endregion

        #region 保护方法
        /// <summary>
        /// 保存有效性判断
        /// </summary>
        /// <returns>返回是否允许保存</returns>
        protected bool SaveValid()
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
            {
                return false;
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                decimal payCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayCost].Value);
                decimal due = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColDue].Value);
                decimal paidUp = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPaidUpCost].Value);
                if (payCost > due - paidUp)
                {
                    MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("发票号" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " 本次付款不能大于未付款金额"));
                    return false;
                }
                if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColPayType].Text == "支票")
                {
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenBank].Text == "")
                    {
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("发票号" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " 付款类型为支票时需填写开户银行"));
                        return false;
                    }
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColOpenAccounts].Text == "")
                    {
                        MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("发票号" + this.neuSpread1_Sheet1.Cells[i, (int)ColPayHeadSet.ColInvoiceNo].Text + " 付款类型为支票时需填写银行帐号"));
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #endregion

        #region 工具栏设置

        /// <summary>
        /// 全选按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton(Neusoft.FrameWork.Management.Language.Msg("全选"), Neusoft.FrameWork.Management.Language.Msg("全选或全不选"), (int)Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q全选, true, false, null);
            return this.toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            if (e.ClickedItem.Text == Neusoft.FrameWork.Management.Language.Msg("全选"))
            {

                for (int j = 0; j < this.neuSpread1_Sheet1.RowCount; j++)
                {

                    this.neuSpread1_Sheet1.Cells[j, (int)ColPayHeadSet.ColChoose].Value = !(bool)this.neuSpread1_Sheet1.Cells[j, (int)ColPayHeadSet.ColChoose].Value;
                }

            }
            base.ToolStrip_ItemClicked(sender, e);
        }
        #endregion

        #region 列枚举

        /// <summary>
        /// 结存汇总信息列设置
        /// </summary>
        enum ColPayHeadSet
        {
            /// <summary>
            /// 是否付款 0
            /// </summary>
            ColChoose,
            /// <summary>
            /// 发票号 1
            /// </summary>
            ColInvoiceNo,
            /// <summary>
            /// 发票日期	2
            /// </summary>
            ColInvoiceDate,
            /// <summary>
            /// 发票金额	3
            /// </summary>
            ColInvoiceCost,
            /// <summary>
            /// 优惠金额	4
            /// </summary>
            ColDiscountCost,
            /// <summary>
            /// 应付金额	5
            /// </summary>
            ColDue,
            /// <summary>
            /// 已付金额	6
            /// </summary>
            ColPaidUpCost,
            /// <summary>
            /// 本次付款	7
            /// </summary>
            ColPayCost,
            /// <summary>
            /// 运费		8
            /// </summary>
            ColDeliveryCost,
            /// <summary>
            /// 付款类型	9
            /// </summary>
            ColPayType,
            /// <summary>
            /// 开户银行	10
            /// </summary>
            ColOpenBank,
            /// <summary>
            /// 银行帐号	11
            /// </summary>
            ColOpenAccounts,
            /// <summary>
            /// 入库科室	12
            /// </summary>
            ColDept,
            /// <summary>
            /// 入库单据号	13
            /// </summary>
            ColInListCode,
            /// <summary>
            /// 付款凭证
            /// </summary>
            ColPayCredence,
            /// <summary>
            /// 未付款凭证
            /// </summary>
            ColUnpayCredence,
            /// <summary>
            /// 未付款凭证日期
            /// </summary>
            ColUnCredenceDate,

            /// <summary>
            /// 供货商名称
            /// </summary>
            /// {38AE0936-69C9-4543-BB4E-78998C7CCE94}
            ColCompanyName
        }
        /// <summary>
        /// 结存付款明细信息行列设置
        /// </summary>
        enum ColPayDetailSet
        {
            /// <summary>
            /// 发票号
            /// </summary>
            ColInvoiceNo,
            /// <summary>
            /// 付款金额
            /// </summary>
            ColPayCost,
            /// <summary>
            /// 运费
            /// </summary>
            ColDeliveryCost,
            /// <summary>
            /// 付款类型
            /// </summary>
            ColPayType,
            /// <summary>
            /// 开户银行
            /// </summary>
            ColOpenBank,
            /// <summary>
            /// 银行帐号
            /// </summary>
            ColOpenAccounts,
            /// <summary>
            /// 付款人
            /// </summary>
            ColPayOper,
            /// <summary>
            /// 付款凭证
            /// </summary>
            ColPayCredence,
            /// <summary>
            /// 未付款凭证
            /// </summary>
            ColUnpayCredence,
            /// <summary>
            /// 未付款凭证日期
            /// </summary>
            ColUnCredenceDate,
            /// <summary>
            /// 付款日期
            /// </summary>
            ColPayDate,
            /// <summary>
            /// 结存流水单号
            /// </summary>
            ColPayListNum
        }

        /// <summary>
        /// 查询条件枚举
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        enum EnumSearchContion
        {
            发票号,
            入库单号
        }
        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            Neusoft.FrameWork.Models.NeuObject testPrivDept = new Neusoft.FrameWork.Models.NeuObject();

            string privIndex = "5514";// Neusoft.FrameWork.Function.NConvert.ToInt32(Neusoft.HISFC.BizLogic.Material.BizLogic.EnumClass2Priv.供货商结存);

            if (isDrug)
            {
                privIndex = "0310";
            }

            int parma = Neusoft.HISFC.Components.Common.Classes.Function.ChoosePivDept(privIndex, ref testPrivDept);

            if (parma == -1)            //无权限
            {
                //MessageBox.Show(Neusoft.FrameWork.Management.Language.Msg("您无此窗口操作权限"));
                return -1;
            }
            else if (parma == 0)       //用户选择取消
            {
                return -1;
            }

            this.privDept = testPrivDept;
            this.privOper = Neusoft.FrameWork.Management.Connection.Operator;
            base.OnStatusBarInfo(null, Neusoft.FrameWork.Management.Language.Msg("操作科室:") + testPrivDept.Name);

            return 1;
        }

        #endregion

        #region 事件

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.ShowPayDetail();
        }

        private void rbUnPay_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbUnPay.Checked)
            {
                this.payFlag = "'0','1'";
                this.Query();
            }
        }

        private void rbPay_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbPay.Checked)
            {
                this.payFlag = "'2'";
                this.Query();
            }
        }

        private void cmbCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        private void dtpStartDate_ValueChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            this.Query();
        }

        /// <summary>
        /// 
        /// </summary>
        /// {45FA88B5-A77C-4e2b-B6CE-8364424B0126}
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSearchCondition_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.QueryInfo();
            }
        }


        private void txtSearchCondition_MouseEnter(object sender, EventArgs e)
        {
            //this.txtSearchCondition.SelectAll();
        }

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

        }

        private void txtSearchCondition_MouseHover(object sender, EventArgs e)
        {
            //this.txtSearchCondition.SelectAll();
        }


        /// <summary>
        /// {DCAEF089-F156-4a81-A476-0D29DBA7AEFD}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {
            decimal d1 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, GetColIndex(Neusoft.FrameWork.Management.Language.Msg("应付金额"))].Value);
            decimal d2 = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, GetColIndex(Neusoft.FrameWork.Management.Language.Msg("已付金额"))].Value);
            decimal d3 = d1 - d2;
            this.neuSpread1_Sheet1.Cells[e.Row, GetColIndex(Neusoft.FrameWork.Management.Language.Msg("本次付款"))].Text = d3.ToString();
        }

        #endregion
    }

}

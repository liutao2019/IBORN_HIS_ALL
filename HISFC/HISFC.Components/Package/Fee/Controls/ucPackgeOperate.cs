using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Public;
using FS.HISFC.Models.Base;
using FS.HISFC.Components.Common.Forms;

namespace HISFC.Components.Package.Fee.Controls
{
    public partial class ucPackgeOperate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 业务类

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 套餐业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackageIntegrate = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();

        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

        /// <summary>
        /// 套餐管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Package basePackMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();

        /// <summary>
        /// 套餐管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice invoiceMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Invoice();

        /// <summary>
        /// 账户逻辑层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();


        #endregion

        #region 属性

        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.setPatientInfo();
            }
        }

        /// <summary>
        /// 民族
        /// </summary>
        private ObjectHelper NationHelper;

        /// <summary>
        /// 证件类型
        /// </summary>
        private ObjectHelper IDCardTypeHelper;

        /// <summary>
        /// 国籍
        /// </summary>
        private ObjectHelper CountryHelper;

        #endregion

        public ucPackgeOperate()
        {
            InitializeComponent();
            Inits();
            bindEvents();
        }

        private void Inits()
        {
            InitData();
            InitControls();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            //证件类型
            IDCardTypeHelper = new ObjectHelper(managerIntegrate.QueryConstantList("IDCard"));

            //民族类型
            NationHelper = new ObjectHelper(managerIntegrate.GetConstantList(EnumConstant.NATION.ToString()));

            //国籍
            CountryHelper = new ObjectHelper(managerIntegrate.GetConstantList(EnumConstant.COUNTRY.ToString()));
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControls()
        { 
            //默认查询三个月的套餐
            this.dtBegin.Value = DateTime.Now.AddMonths(-3).Date;
            this.dtEnd.Value = DateTime.Now.Date;
        }

        /// <summary>
        /// 绑定事件
        /// </summary>
        private void bindEvents()
        {
            this.txtMarkNo.KeyDown += new KeyEventHandler(txtMarkNo_KeyDown);
            this.txtName.KeyDown += new KeyEventHandler(txtName_KeyDown);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.trvInvoice.AfterSelect += new TreeViewEventHandler(trvInvoice_AfterSelect);
            this.chkAll.CheckStateChanged += new EventHandler(chkAll_CheckStateChanged);
        }

        /// <summary>
        /// 解绑事件
        /// </summary>
        private void unBindEvents()
        {
            this.chkAll.CheckStateChanged -= new EventHandler(chkAll_CheckStateChanged);
            this.trvInvoice.AfterSelect -= new TreeViewEventHandler(trvInvoice_AfterSelect);
            this.btnSearch.Click -= new EventHandler(btnSearch_Click);
            this.txtName.KeyDown -= new KeyEventHandler(txtName_KeyDown);
            this.txtMarkNo.KeyDown -= new KeyEventHandler(txtMarkNo_KeyDown);
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void setPatientInfo()
        {
            try
            {
                if (this.patientInfo == null || this.patientInfo.PID.CardNO == "")
                {
                    this.Clear();
                    return;
                }

                this.txtMarkNo.Text = this.PatientInfo.PID.CardNO;
                this.txtName.Text = this.PatientInfo.Name;
                this.txtSex.Text = this.PatientInfo.Sex.Name;
                this.txtAge.Text = this.accountManager.GetAge(patientInfo.Birthday);
                this.txtRace.Text = this.NationHelper.GetName(this.PatientInfo.Nationality.ID);
                this.txtIDCardType.Text = this.PatientInfo.IDCardType.ID;
                this.txtIDCardNO.Text = this.patientInfo.IDCard;
                this.txtCountry.Text = CountryHelper.GetName(this.PatientInfo.Country.ID);
                this.txtsiNo.Text = this.PatientInfo.SSN;
                this.btnSearch_Click(this.btnSearch,new EventArgs());
            }
            catch(Exception ex)
            {
            }
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        private void Clear()
        {
            this.txtMarkNo.SelectAll();
            this.txtMarkNo.Focus();
            this.txtName.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtRace.Text = string.Empty;
            this.txtIDCardType.Text = string.Empty;
            this.txtIDCardNO.Text = string.Empty;
            this.txtCountry.Text = string.Empty;
            this.txtsiNo.Text = string.Empty;
            this.trvInvoice.Nodes.Clear();
            this.fpPackage_Sheet1.RowCount = 0;
            this.chkAll.CheckState = CheckState.Unchecked;
        }

        /// <summary>
        /// 按回车卡号检索患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMarkNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.Clear();
                TextBox queryControl = sender as TextBox;
                string QueryStr = queryControl.Text.Trim();

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                //病历号不满10位补满
                QueryStr = QueryStr.PadLeft(10, '0');

                this.PatientInfo = accountManager.GetPatientInfoByCardNO(QueryStr);

                if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
                {
                    MessageBox.Show("未查询到有效患者！");
                    return;
                }
            }
        }

        /// <summary>
        /// 回车姓名检索患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        { 
            if (e.KeyCode == Keys.Enter)
            {
                string QueryStr = this.txtName.Text;
                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQueryPatientByConditions frmQuery = new frmQueryPatientByConditions();
                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {
                    this.PatientInfo = frmQuery.PatientInfo;
                }
            }
        }

        private void trvInvoice_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.fpPackage_Sheet1.RowCount = 0;

            if (e == null || e.Node == null)
                return;

            TreeNode selectedNode = e.Node as TreeNode;

            if (e.Node != null && selectedNode.Tag is FS.HISFC.Models.MedicalPackage.Fee.Invoice)
            {
                FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice = selectedNode.Tag as FS.HISFC.Models.MedicalPackage.Fee.Invoice;
                ArrayList packages = this.packageMgr.QueryByInvoiceNO(invoice.ID, "0");
                if (packages == null || packages.Count == 0)
                {
                    MessageBox.Show("发票号不存在费用！");
                    return;
                }

                this.SetPackage(packages);
            }
        }

        /// <summary>
        /// 根据时间和发票号查询患者押金记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.trvInvoice.Nodes.Clear();
                this.fpPackage_Sheet1.RowCount = 0;

                if (this.PatientInfo == null || string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
                {
                    return;
                }

                DateTime beginDate = this.dtBegin.Value.Date;
                DateTime endDate = this.dtEnd.Value.Date.AddDays(1);

                //查询指定时间有效的套餐记录
                ArrayList invoiceList = this.invoiceMgr.QueryInvoiceByCardNoDate(this.PatientInfo.PID.CardNO, beginDate, endDate, "0");

                if (invoiceList == null)
                {
                    MessageBox.Show(packageMgr.Err);
                    return;
                }

                if (invoiceList.Count == 0)
                {
                    return;
                }

                this.SetInvoiceTree(invoiceList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected override void OnRefresh()
        {
            TreeViewEventArgs e = new TreeViewEventArgs(this.trvInvoice.SelectedNode,TreeViewAction.ByMouse);
            trvInvoice_AfterSelect(this.trvInvoice, e);
        }

        private void chkAll_CheckStateChanged(object sender, EventArgs e)
        {
            foreach (FarPoint.Win.Spread.Row row in this.fpPackage_Sheet1.Rows)
            {
                this.fpPackage_Sheet1.Cells[row.Index, (int)PackageCols.SpecialFlag].Value = this.chkAll.CheckState == CheckState.Checked;
            }
        }

        /// <summary>
        /// 设置发票树信息显示
        /// </summary>
        private void SetInvoiceTree(ArrayList invoiceList)
        {
            TreeNode root = new TreeNode();
            root.Tag = this.PatientInfo;
            root.Text = this.PatientInfo.Name + "【" + this.PatientInfo.PID.CardNO + "】";

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Invoice invoice in invoiceList)
            {
                TreeNode invoiceNode = new TreeNode();
                invoiceNode.Name = invoice.ID;
                invoiceNode.Text = invoice.ID;
                invoiceNode.Tag = invoice;
                root.Nodes.Add(invoiceNode);
            }

            this.trvInvoice.Nodes.Add(root);
            this.trvInvoice.ExpandAll();
        }

        /// <summary>
        /// 设置套餐显示
        /// </summary>
        /// <param name="packageList"></param>
        private void SetPackage(ArrayList packageList)
        {
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                //package.PackageInfo = this.basePackMgr.QueryPackageByID(package.PackageInfo.ID);
                //package.ParentPackageInfo = this.basePackMgr.QueryPackageByID(package.ParentPackageInfo.ID);
                this.fpPackage_Sheet1.AddRows(this.fpPackage_Sheet1.RowCount, 1);
                this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag = package;

                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.SpecialFlag].Value = package.SpecialFlag == "1" ? true : false;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.InvoiceNO].Text = package.InvoiceNO;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Name].Text = package.ParentPackageInfo.Name;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.PackageName].Text = package.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.OperTime].Text = package.OperTime.ToString();
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.TotCost].Text = package.Package_Cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.RealCost].Text = package.Real_Cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.GiftCost].Text = package.Gift_cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.ETCCost].Text = package.Etc_cost.ToString("F2");
            }
        }

        /// <summary>
        /// 获取变更的套餐
        /// </summary>
        /// <returns></returns>
        private ArrayList GetNeedUpdatePackage()
        {
            try
            {
                ArrayList packageList = new ArrayList();
                foreach (FarPoint.Win.Spread.Row row in this.fpPackage_Sheet1.Rows)
                {
                    bool SpecialFlag = (bool)(this.fpPackage_Sheet1.Cells[row.Index, (int)PackageCols.SpecialFlag].Value);
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = this.fpPackage_Sheet1.Rows[row.Index].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    if ((package.SpecialFlag != "1" && SpecialFlag) || (package.SpecialFlag == "1" && !SpecialFlag))
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.Package clone = package.Clone();
                        clone.SpecialFlag = SpecialFlag ? "1" : "0";
                        packageList.Add(clone);
                    }
                }

                return packageList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            ArrayList packageList = this.GetNeedUpdatePackage();

            if (packageList == null || packageList.Count == 0)
            {
                MessageBox.Show("没有变更的数据，不需要保存", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int rtn = this.feePackageIntegrate.UpdateSpecialFlagByID(packageList);

            if (rtn < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新医保标识出错：" + this.feePackageIntegrate.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            this.OnRefresh();
            MessageBox.Show("更改特殊折扣标识成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return 1;
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PackageCols
        {
            /// <summary>
            /// {9B833B34-AE7F-4013-8F9D-CDE36A738D02}
            /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
            /// 是否医保
            /// </summary>
            SpecialFlag = 0,

            /// <summary>
            /// 发票
            /// </summary>
            InvoiceNO = 1,

            /// <summary>
            /// 套餐名称
            /// </summary>
            Name = 2,

            /// <summary>
            /// 套餐包名称
            /// </summary>
            PackageName = 3,

            /// <summary>
            /// 购买时间
            /// </summary>
            OperTime = 4,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 5,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 6,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 7,

            /// <summary>
            /// 优惠金额
            /// </summary>
            ETCCost = 8
        }
    }
}

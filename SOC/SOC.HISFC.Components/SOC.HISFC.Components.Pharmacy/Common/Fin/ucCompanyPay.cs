using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.Fin
{
    /// <summary>
    /// [功能描述: 药品供货商付款]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-08]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public partial class ucCompanyPay : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucCompanyPay()
        {
            InitializeComponent();
        }

        private FS.FrameWork.Models.NeuObject priveDept = new FS.FrameWork.Models.NeuObject();

        private System.Data.DataTable dtPayDetail = null;
        private System.Data.DataTable dtUnpayDetail = null;

        private System.Collections.Hashtable hsUnpay = new System.Collections.Hashtable();
        private System.Collections.Hashtable hsPay = new System.Collections.Hashtable();

        private string settingUnpayFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPhaFinCompanyUnpaySetting.xml";
        private string settingPayFileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPhaFinCompanyPaySetting.xml";
        private string fileName = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyFinSettring.xml";

        private FS.SOC.HISFC.BizLogic.Pharmacy.Financial financialMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Financial();

        private string curCompanyNO = "";

        #region 属性及相关变量


        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            if (this.DesignMode)
            {
                return 0;
            }
         
            return this.SetPriveDept();
        }

        /// <summary>
        /// 设置权限科室
        /// </summary>
        /// <returns></returns>
        private int SetPriveDept()
        {
            int param = Function.ChoosePrivDept("0314", "01", ref this.priveDept);
            if (param == 0 || param == -1 || this.priveDept == null || string.IsNullOrEmpty(this.priveDept.ID))
            {
                return -1;
            }
            this.nlbInfo.Text = "您选择的是【" + this.priveDept.Name + "】";
            return 1;
        }
        #endregion

        #region 方法
        private void Init()
        {
            this.ndtpEnd.Value = this.ndtpEnd.Value.Date.AddDays(1);

            SOC.HISFC.BizProcess.Cache.Pharmacy.InitCompany();
            this.ncmbCompany.AddItems(SOC.HISFC.BizProcess.Cache.Pharmacy.companyHelper.ArrayObject);

            string checkValue = "True";

            checkValue = SOC.Public.XML.SettingFile.ReadSetting(fileName, "CompanyPay", "PayCustomNONotNull", checkValue);

            this.ncbPayCustomNONotNull.Checked = FS.FrameWork.Function.NConvert.ToBoolean(checkValue);

            checkValue = "True";
            checkValue = SOC.Public.XML.SettingFile.ReadSetting(fileName, "CompanyPay", "UnclearOnQuery", checkValue);

            this.ncbUnclearOnQuery.Checked = FS.FrameWork.Function.NConvert.ToBoolean(checkValue);

            this.InitDataTable();
            this.InitFP();

            this.ncmbCompany.Select();
            this.ncmbCompany.Focus();
        }

        private void InitDataTable()
        {
            if (this.dtUnpayDetail == null)
            {
                this.dtUnpayDetail = new System.Data.DataTable();
            }

            this.dtUnpayDetail.Columns.AddRange
                (
                new System.Data.DataColumn[]
                {
                    new DataColumn("选择",typeof(bool)),
                    new DataColumn("付款编号",typeof(string)),
                    new DataColumn("发票号",typeof(string)),
                    new DataColumn("入库单号",typeof(string)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("入库时间",typeof(string)),
                    new DataColumn("入库人",typeof(string)),
                    new DataColumn("发票录入时间",typeof(string)),
                    new DataColumn("发票录入人",typeof(string)),
                    new DataColumn("审核时间",typeof(string)),
                    new DataColumn("审核人",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtUnpayDetail.Columns)
            {
                if (dc.ColumnName == "选择" || dc.ColumnName == "付款编号" || dc.ColumnName == "发票号")
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }

            this.dtUnpayDetail.DefaultView.AllowNew = true;
            this.dtUnpayDetail.DefaultView.AllowEdit = true;
            this.dtUnpayDetail.DefaultView.AllowDelete = true;

            DataColumn[] unPayKeys = new DataColumn[1];

            unPayKeys[0] = this.dtUnpayDetail.Columns["主键"];

            this.dtUnpayDetail.PrimaryKey = unPayKeys;

            if (this.dtPayDetail == null)
            {
                this.dtPayDetail = new System.Data.DataTable();
            }

            this.dtPayDetail.Columns.AddRange
                (
                new System.Data.DataColumn[]
                {
                    new DataColumn("选择",typeof(bool)),
                    new DataColumn("付款编号",typeof(string)),
                    new DataColumn("发票号",typeof(string)),
                    new DataColumn("入库单号",typeof(string)),
                    new DataColumn("购入金额",typeof(decimal)),
                    new DataColumn("零售金额",typeof(decimal)),
                    new DataColumn("付款时间",typeof(string)),
                    new DataColumn("付款人",typeof(string)),
                    new DataColumn("付款单号",typeof(string)),
                    new DataColumn("主键",typeof(string)),

                }
                );

            foreach (DataColumn dc in this.dtPayDetail.Columns)
            {
                if (dc.ColumnName == "选择")
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }

            this.dtPayDetail.DefaultView.AllowNew = true;
            this.dtPayDetail.DefaultView.AllowEdit = true;
            this.dtPayDetail.DefaultView.AllowDelete = true;

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtPayDetail.Columns["主键"];

            this.dtPayDetail.PrimaryKey = keys;
        }

        private void InitFP()
        {
            
            this.socFPUnpay_Sheet1.DataSource = this.dtUnpayDetail;
            this.socFPUnpay.EditMode = true;
            this.socFPUnpay.AllowDragFill = true;
            //int index = this.socFPUnpay.GetColumnIndex(0, "选择");
            //if (index > -1)
            //{
            //    this.socFPUnpay.Sheets[0].Columns[index].Locked = true;
            //}
            if (System.IO.File.Exists(this.settingUnpayFileName))
            {
                this.socFPUnpay.ReadSchema(this.settingUnpayFileName);
            }
            else
            {
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = (int)Function.GetCostDecimals("0314", "01");
                n.ReadOnly = true;

                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                FarPoint.Win.Spread.CellType.TextCellType tWrite = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = false;

                this.socFPUnpay.SetColumnCellType(0, "选择", c);
                this.socFPUnpay.SetColumnCellType(0, "付款编号", tWrite);
                this.socFPUnpay.SetColumnCellType(0, "发票号", tWrite);
                this.socFPUnpay.SetColumnCellType(0, "入库单号", t);
                this.socFPUnpay.SetColumnCellType(0, "购入金额", n);
                this.socFPUnpay.SetColumnCellType(0, "零售金额", n);
                this.socFPUnpay.SetColumnCellType(0, "入库时间", t);
                this.socFPUnpay.SetColumnCellType(0, "入库人", t);
                this.socFPUnpay.SetColumnCellType(0, "发票录入时间", t);
                this.socFPUnpay.SetColumnCellType(0, "发票录入人", t);
                this.socFPUnpay.SetColumnCellType(0, "审核时间", t);
                this.socFPUnpay.SetColumnCellType(0, "审核人", t);
                this.socFPUnpay.SetColumnCellType(0, "主键", t);

                this.socFPUnpay.SetColumnWith(0, "主键", 0);

            }
            this.socFPPay_Sheet1.DataSource = this.dtPayDetail;
            this.socFPPay.EditMode = true;

            if (System.IO.File.Exists(this.settingPayFileName))
            {
                this.socFPPay.ReadSchema(this.settingPayFileName);
            }
            else
            {
                FarPoint.Win.Spread.CellType.NumberCellType n = new FarPoint.Win.Spread.CellType.NumberCellType();
                n.DecimalPlaces = (int)Function.GetCostDecimals("0314", "01");
                n.ReadOnly = true;

                FarPoint.Win.Spread.CellType.CheckBoxCellType c = new FarPoint.Win.Spread.CellType.CheckBoxCellType();

                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;

                this.socFPPay.SetColumnCellType(0, "选择", c);
                this.socFPPay.SetColumnCellType(0, "付款编号", t);
                this.socFPPay.SetColumnCellType(0, "发票号", t);
                this.socFPPay.SetColumnCellType(0, "入库单号", t);
                this.socFPPay.SetColumnCellType(0, "购入金额", n);
                this.socFPPay.SetColumnCellType(0, "零售金额", n);
                this.socFPPay.SetColumnCellType(0, "付款时间", t);
                this.socFPPay.SetColumnCellType(0, "付款人", t);
                this.socFPPay.SetColumnCellType(0, "付款单号", t);
                this.socFPPay.SetColumnCellType(0, "主键", t);

                this.socFPPay.SetColumnWith(0, "主键", 0);
            }
        }

        private void ClearUnpay()
        {
            this.hsUnpay.Clear();
            this.dtUnpayDetail.Clear();
            this.socFPUnpay_Sheet1.RowCount = 0;
            this.nlbTotInfo.Text = "选择数据后计算总金额或者双击此处获取总金额";
        }

        private void ClearPay()
        {
            this.hsPay.Clear();
            this.dtPayDetail.Clear();
            this.socFPPay_Sheet1.RowCount = 0;
        }


        private int AddPayToDataTable(FS.HISFC.Models.Pharmacy.Pay pay)
        {
            if (pay == null)
            {
                Function.ShowMessage("请与系统管理员联系并报告错误：付款实体为null", MessageBoxIcon.Error);
                return -1;
            }
            if (pay.PayState == "0")
            {
                if (hsUnpay.Contains(pay.InListNO + pay.InvoiceNO))
                {
                    Function.ShowMessage("发票：" + pay.InvoiceNO + "入库单：" + pay.InListNO + "已经添加！", MessageBoxIcon.Information);
                    return 0;
                }

                hsUnpay.Add(pay.InListNO + pay.InvoiceNO, pay);

                DataRow row = this.dtUnpayDetail.NewRow();
                row["选择"] = false;
                row["付款编号"] = this.ntxtPayCustomNO.Text.Trim();
                row["发票号"] = pay.InvoiceNO;
                row["入库单号"] = pay.InListNO;
                row["购入金额"] = pay.PurchaseCost;
                row["零售金额"] = pay.RetailCost;

                row["入库时间"] = pay.User01;
                row["入库人"] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(pay.Oper.User01);

                row["发票录入时间"] = pay.User02;
                row["发票录入人"] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(pay.Oper.User02);

                row["审核时间"] = pay.User03;
                row["审核人"] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(pay.Oper.User03);

                row["主键"] = pay.InListNO + pay.InvoiceNO;

                this.dtUnpayDetail.Rows.Add(row);

            }
            else
            {
                if (this.hsPay.Contains(pay.ID))
                {
                    Function.ShowMessage("发票：" + pay.InvoiceNO + "入库单：" + pay.InListNO + "已经添加！", MessageBoxIcon.Information);
                    return -1;
                }

                hsPay.Add(pay.ID, pay);

                DataRow row = this.dtPayDetail.NewRow();
                row["选择"] = false;
                row["付款编号"] = pay.Extend1;
                row["发票号"] = pay.InvoiceNO;
                row["入库单号"] = pay.InListNO;
                row["购入金额"] = pay.PurchaseCost;
                row["零售金额"] = pay.RetailCost;

                row["付款时间"] = pay.Oper.OperTime.ToString();
                row["付款人"] = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(pay.Oper.ID);

                row["付款单号"] = pay.Extend;

                row["主键"] = pay.ID;


                this.dtPayDetail.Rows.Add(row);
            }
            return 1;
        }

        private void Query()
        {

            if (this.ncmbCompany.Tag == null || this.ncmbCompany.Tag.ToString() == "")
            {
                Function.ShowMessage("请选择供货公司", MessageBoxIcon.Information);
                this.ncmbCompany.Select();
                this.ncmbCompany.Focus();
                return;
            }
            //公司更换必须清空数据，不可以同时对多个公司付款
            if (curCompanyNO != "" && curCompanyNO != this.ncmbCompany.Tag.ToString())
            {
                this.ClearUnpay();
                this.socFPUnpay.ReadSchema(this.settingUnpayFileName);
                this.ClearPay();
                this.socFPPay.ReadSchema(this.settingPayFileName);
            }
            this.nlbCurCompany.Text = "正在对【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetCompanyName(this.ncmbCompany.Tag.ToString()) + "】付款";
            
            if (this.neuTabControl1.SelectedTab == this.tpUnpay)
            {
                if (this.QueryUnpay() == 0)
                {
                    Function.ShowMessage("没查询到数据，请您确认：\n1.是否已入库、是否已核准、是否已录入发票\n2.是否已付款", MessageBoxIcon.Information);
                }

                if (this.ncbPayCustomNONotNull.Checked)
                {
                    this.ntxtPayCustomNO.Select();
                    this.ntxtPayCustomNO.SelectAll();
                    this.ntxtPayCustomNO.Focus();
                }
                else
                {
                    this.ntxtInputInvoiceNO.Select();
                    this.ntxtInputInvoiceNO.SelectAll();
                    this.ntxtInputInvoiceNO.Focus();
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPay)
            {
                if (this.QueryPay() == 0)
                {
                    Function.ShowMessage("您要查询的数据可能还没有付款", MessageBoxIcon.Information);
                }
            }
        }

        private int QueryUnpay()
        {
            if (this.dtUnpayDetail.Rows.Count > 0)
            {
                //在查询的时候不清除现有数据可以对同一个公司的多张发票、入库单进行付款
                bool clearCurData = true;
                if (!string.IsNullOrEmpty(this.ntxtQueryInvoiceNO.Text) || !string.IsNullOrEmpty(this.ntxtInListNO.Text))
                {
                    if (this.ncbUnclearOnQuery.Checked)
                    {
                        clearCurData = false;
                    }
                }

                if (clearCurData)
                {
                    this.ClearUnpay();
                    this.dtUnpayDetail.AcceptChanges();
                    this.socFPUnpay.ReadSchema(this.settingUnpayFileName);
                }
            }

            string invoiceNO = "All";
            if (!string.IsNullOrEmpty(this.ntxtQueryInvoiceNO.Text))
            {
                invoiceNO = this.ntxtQueryInvoiceNO.Text;
            }

            string inBillNO = "All";
            if (!string.IsNullOrEmpty(this.ntxtInListNO.Text))
            {
                inBillNO = this.ntxtInListNO.Text;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候...");
            Application.DoEvents();

            ArrayList alUnpay = this.financialMgr.QueryUnpay(this.priveDept.ID, this.ncmbCompany.Tag.ToString(), invoiceNO, inBillNO);
            if (alUnpay == null)
            {
                Function.ShowMessage("查询未付款信息出错，请与系统管理员联系并报告错误：" + this.financialMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Pay pay in alUnpay)
            {
                if (this.AddPayToDataTable(pay) == -1)
                {
                    break;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return alUnpay.Count;
        }

        private int QueryPay()
        {
            this.ClearPay();
            this.socFPPay.ReadSchema(this.settingPayFileName);

            string invoiceNO = "All";
            if (!string.IsNullOrEmpty(this.ntxtQueryInvoiceNO.Text))
            {
                invoiceNO = this.ntxtQueryInvoiceNO.Text;
            }

            string inBillNO = "All";
            if (!string.IsNullOrEmpty(this.ntxtInListNO.Text))
            {
                inBillNO = this.ntxtInListNO.Text;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("请稍候...");
            Application.DoEvents();

            ArrayList alPay = this.financialMgr.QueryPay(this.priveDept.ID, this.ncmbCompany.Tag.ToString(), invoiceNO, inBillNO, this.ndtpBegin.Value, this.ndtpEnd.Value);
            if (alPay == null)
            {
                Function.ShowMessage("查询付款信息出错，请与系统管理员联系并报告错误：" + this.financialMgr.Err, MessageBoxIcon.Error);
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Pay pay in alPay)
            {
                if (this.AddPayToDataTable(pay) == -1)
                {
                    break;
                }
            }
            this.dtPayDetail.AcceptChanges();
            this.socFPPay.ReadSchema(this.settingPayFileName);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return alPay.Count;
        }

        private void Save()
        {
            this.ncmbCompany.Select();
            this.ncmbCompany.Focus();

            if (this.neuTabControl1.SelectedTab == this.tpUnpay)
            {
                if (this.Pay() > 0)
                {
                    this.QueryUnpay();
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPay)
            {
                this.CancelPay();
            }
        }

        private bool CheckValid()
        {
            bool isHaveData = false;
            int rowIndex = 0;
            foreach (DataRow dr in this.dtUnpayDetail.Rows)
            {
                dr.EndEdit();

                rowIndex++;
                if (!FS.FrameWork.Function.NConvert.ToBoolean(dr["选择"]))
                {
                    continue;
                }
                isHaveData = true;

                if (this.ncbPayCustomNONotNull.Checked && (dr.IsNull("付款编号") || string.IsNullOrEmpty(dr["付款编号"].ToString())))
                {
                    Function.ShowMessage("第" + rowIndex.ToString() + "行没有付款号！", MessageBoxIcon.Information);
                    return false;
                }

                if ((dr.IsNull("发票号") || string.IsNullOrEmpty(dr["发票号"].ToString())))
                {
                    Function.ShowMessage("第" + rowIndex.ToString() + "行没有发票号！", MessageBoxIcon.Information);
                    return false;
                }
                
            }


            if (!isHaveData)
            {
                Function.ShowMessage("请选择数据！", MessageBoxIcon.Information);
            }

            return isHaveData;
        }

        private void SetTotInfo()
        {
            
            decimal totPurchaseCost = 0;
            decimal totRetailCost = 0;
            this.socFPUnpay.EditMode = false;
            for (int index = 0; index < this.socFPUnpay_Sheet1.RowCount; index++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.socFPUnpay.GetCellText(0, index, "选择")))
                {
                    totPurchaseCost += FS.FrameWork.Function.NConvert.ToDecimal(this.socFPUnpay.GetCellText(0, index, "购入金额"));
                    totRetailCost += FS.FrameWork.Function.NConvert.ToDecimal(this.socFPUnpay.GetCellText(0, index, "零售金额"));
                }
            }

            this.nlbTotInfo.Location = new Point(this.nlbCurCompany.Location.X + this.nlbCurCompany.PreferredWidth + 20, this.nlbTotInfo.Location.Y);
            this.nlbTotInfo.Text = "已选数据购入总额：" + totPurchaseCost.ToString() + "，零售总额：" + totRetailCost.ToString();
        }

        private int Pay()
        {
            if (this.dtUnpayDetail.Rows.Count <= 0)
            {
                return 0;
            }

            

            if (!this.CheckValid())
            {
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存操作..请稍候");
            System.Windows.Forms.Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime sysTime = this.financialMgr.GetDateTimeFromSysDateTime();


            string billNO = "";
            ArrayList alPay = new ArrayList();

            foreach (DataRow dr in this.dtUnpayDetail.Rows)
            {
                if (!FS.FrameWork.Function.NConvert.ToBoolean(dr["选择"]))
                {
                    continue;
                }
                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Pay pay = hsUnpay[key] as FS.HISFC.Models.Pharmacy.Pay;

                pay.PayOper.ID = this.financialMgr.Operator.ID;
                pay.PayOper.OperTime = sysTime;

                pay.Oper.ID = this.financialMgr.Operator.ID;
                pay.Oper.OperTime = sysTime;

                if (pay.UnPayCost == 0)
                {
                    pay.PayState = "2";
                }
                else
                {
                    pay.PayState = "1";
                }

                if (string.IsNullOrEmpty(billNO))
                {
                    string errInfo = "";
                    billNO = Function.GetBillNO(this.priveDept.ID, "0314", "01", ref errInfo);

                    if (string.IsNullOrEmpty(billNO) || billNO == "-1")
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        Function.ShowMessage("获取最新付款单号出错:" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }
                }

                pay.Extend = billNO;

                if (dr.IsNull("付款编号"))
                {
                    pay.Extend1 = "";
                }
                else 
                {
                    pay.Extend1 = dr["付款编号"].ToString();
                }
                if (!dr.IsNull("发票号"))
                {
                    if (pay.InvoiceNO != dr["发票号"].ToString())
                    {
                        pay.InvoiceNO = dr["发票号"].ToString();
                        if (this.financialMgr.FinInvoiceInput(pay.StockDept.ID, pay.InListNO, pay.InvoiceNO, pay.Oper.ID, pay.Oper.OperTime) == -1)
                        {
                            pay.ID = "";
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存出错:" + this.financialMgr.Err, MessageBoxIcon.Error);
                            return -1;
                        }
                    }
                }
                //全额付款，暂时不考虑一张发票多次付款的情况
                if (this.financialMgr.FinCompanyPay(pay, false) == -1)
                {
                    pay.ID = "";
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存出错:"+this.financialMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                alPay.Add(pay);
            }

            if (alPay.Count == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("请选择数据！", MessageBoxIcon.Information);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
                string errInfo = "";
                if (Function.DealExtendBiz("0314", "01", alPay, ref errInfo) == -1)
                {
                    Function.ShowMessage("付款已经完成，但是扩展业务处理失败，请与系统管理员联系！\n请报告错误信息：" + errInfo, MessageBoxIcon.Error);
                }

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                Function.PrintBill("0314", "01", alPay);

                this.ClearUnpay();
                this.dtUnpayDetail.AcceptChanges();
                this.socFPUnpay.ReadSchema(this.settingUnpayFileName);

            }
           

            return alPay.Count;
        }

        private int CancelPay()
        {
            if (this.dtPayDetail.Rows.Count <= 0)
            {
                return 0;
            }

            bool isHaveData = false;
            foreach (DataRow dr in this.dtPayDetail.Rows)
            {
                dr.EndEdit();

                if (!FS.FrameWork.Function.NConvert.ToBoolean(dr["选择"]))
                {
                    continue;
                }
                isHaveData = true;
            }


            if (isHaveData)
            {
                DialogResult dialogResult = MessageBox.Show("确认取消当前选择的付款数据吗？", "提示>>", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在进行保存操作..请稍候");
            System.Windows.Forms.Application.DoEvents();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            DateTime sysTime = this.financialMgr.GetDateTimeFromSysDateTime();


            ArrayList alPay = new ArrayList();

            foreach (DataRow dr in this.dtPayDetail.Rows)
            {
                if (!FS.FrameWork.Function.NConvert.ToBoolean(dr["选择"]))
                {
                    continue;
                }
                string key = dr["主键"].ToString();
                FS.HISFC.Models.Pharmacy.Pay pay = this.hsPay[key] as FS.HISFC.Models.Pharmacy.Pay;
                if (pay.SequenceNO <= 0)
                {
                    pay.SequenceNO = 1;
                }
                if (this.financialMgr.FinCancelCompanyPay(pay) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("保存出错:" + financialMgr.Err, MessageBoxIcon.Error);
                    return -1;
                }

                alPay.Add(pay);
            }

            if (alPay.Count == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                Function.ShowMessage("请选择数据！", MessageBoxIcon.Information);
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                this.ClearPay();
                this.dtPayDetail.AcceptChanges();
                this.socFPPay.ReadSchema(this.settingUnpayFileName);

                Function.ShowMessage("已成功取消付款！", MessageBoxIcon.Information);

            }

            return alPay.Count;
        }

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.Init();
            this.nlbInfo.DoubleClick += new EventHandler(nlbInfo_DoubleClick);
            this.socFPPay.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(socFPPay_ColumnWidthChanged);
            this.socFPUnpay.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(socFPUnpay_ColumnWidthChanged);
            this.ntxtPayCustomNO.KeyPress += new KeyPressEventHandler(ntxtPayCustomNO_KeyPress);
            this.ncbPayCustomNONotNull.CheckedChanged += new EventHandler(ncbPayCustomNONotNull_CheckedChanged);
            this.ncbUnclearOnQuery.CheckedChanged += new EventHandler(ncbUnclearOnQuery_CheckedChanged);
            this.ntxtQueryInvoiceNO.KeyPress += new KeyPressEventHandler(billNO_KeyPress);
            this.ntxtInListNO.KeyPress += new KeyPressEventHandler(billNO_KeyPress);
            this.ntxtInputInvoiceNO.KeyPress += new KeyPressEventHandler(ntxtInputInvoiceNO_KeyPress);
            this.nlbTotInfo.DoubleClick += new EventHandler(nlbTotInfo_DoubleClick);
            this.socFPUnpay.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(socFPUnpay_ButtonClicked);
            base.OnLoad(e);
        }

        void socFPUnpay_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.socFPUnpay.StopCellEditing();
            if (this.socFPUnpay.GetColumnIndex(0, "选择") == e.Column)
            {
                if (this.socFPUnpay.Sheets[0].RowCount > 500)
                {
                    this.nlbTotInfo.Text = "数据量过大，系统已取消金额计算，若需要计算总金额请您双击此处";
                }
                else
                {
                    this.SetTotInfo();
                }
            }
        }

        void nlbTotInfo_DoubleClick(object sender, EventArgs e)
        {
            this.SetTotInfo();
        }

        void ntxtInputInvoiceNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.neuTabControl1.SelectedTab == this.tpUnpay)
                {
                    for (int index = 0; index < this.socFPUnpay_Sheet1.RowCount; index++)
                    {
                        this.socFPUnpay.SetCellValue(0, index, "发票号", this.ntxtInputInvoiceNO.Text.Trim());
                    }
                }
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
          
            return base.OnSave(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedTab == this.tpUnpay)
            {
                this.socFPUnpay.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPay)
            {
                this.socFPPay.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            }
            return base.Export(sender, neuObject);
        }


        void ncbUnclearOnQuery_CheckedChanged(object sender, EventArgs e)
        {
            string checkValue = "False";
            if (this.ncbUnclearOnQuery.Checked)
            {
                checkValue = "True";
            }

            SOC.Public.XML.SettingFile.SaveSetting(fileName, "CompanyPay", "UnclearOnQuery", checkValue);
        }

        void ncbPayCustomNONotNull_CheckedChanged(object sender, EventArgs e)
        {
            string checkValue = "False";
            if (this.ncbPayCustomNONotNull.Checked)
            {
                checkValue = "True";
            }

            SOC.Public.XML.SettingFile.SaveSetting(fileName, "CompanyPay", "PayCustomNONotNull", checkValue);
        }

        void ntxtPayCustomNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (this.neuTabControl1.SelectedTab == this.tpUnpay)
                {
                    for (int index = 0; index < this.socFPUnpay_Sheet1.RowCount; index++)
                    {
                        this.socFPUnpay.SetCellValue(0, index, "付款编号", this.ntxtPayCustomNO.Text.Trim());
                    }
                }

                this.ntxtInputInvoiceNO.Select();
                this.ntxtInputInvoiceNO.SelectAll();
                this.ntxtInputInvoiceNO.Focus();
            }
        }

        void billNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.Query();
            }
        }

        void socFPUnpay_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.socFPUnpay.SaveSchema(this.settingUnpayFileName);
        }

        void socFPPay_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.socFPPay.SaveSchema(this.settingPayFileName);
        }

       
        void nlbInfo_DoubleClick(object sender, EventArgs e)
        {
            this.SetPriveDept();

            this.ClearPay();
            this.socFPPay.ReadSchema(this.settingPayFileName);

            this.ClearUnpay();
            this.socFPUnpay.ReadSchema(this.settingUnpayFileName);
        }
        #endregion

    }
}

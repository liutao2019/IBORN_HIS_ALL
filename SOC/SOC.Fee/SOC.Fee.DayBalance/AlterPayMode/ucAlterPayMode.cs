using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Models;
using System.Reflection;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace SOC.Fee.DayBalance.AlterPayMode
{
    /// <summary>
    /// 修改支付方式
    /// </summary>
    public partial class ucAlterPayMode : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAlterPayMode()
        {
            InitializeComponent();
        }

        public bool ShowChkTans
        {
            set
            {
                this.chbTrans.Visible = value;
            }
        }
        public string InvoiceNo
        {
            get
            {
                return this.txtInvoice.Text;
            }
            set
            {
                this.txtInvoice.Text = value;
            }
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        private string xmlPath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + @".\Profile\alterPayMode.xml";

        public bool ShowBtnSave
        {
            get
            {
                return this.btnSave.Visible;
            }
            set
            {
                this.btnSave.Visible = value;
            }
        }

        public TransKind KindEnum
        {
            set
            {
                Trans_Kind = value;
            }
        }

        /// <summary>
        /// 更改情况，预交金或结算，默认为结算
        /// </summary>
        TransKind Trans_Kind = TransKind.Balance;

        /// <summary>
        /// 交易类型，正交易或反交易，默认为正交易
        /// </summary>
        string Trans_Type = "1";

        DataSet ds = null;

        /// <summary>
        /// 报表标题
        /// </summary>
        [Description("操作类型"), Category("控件设置")]
        public TransKind Transkind
        {
            get
            {
                return this.Trans_Kind;
            }
            set
            {
                this.Trans_Kind = value;
            }
        }

        SOC.Fee.DayBalance.Manager.AlterPayMode fee = new SOC.Fee.DayBalance.Manager.AlterPayMode();

        /// <summary>
        /// 是否允许日结之后修改支付方式：0允许；1不允许；
        /// </summary>
        private string isBalanceFlag = "1";

        /// <summary>
        /// 是否允许日结之后修改支付方式：0允许；1不允许
        /// </summary>
        [Description("是否允许日结之后修改支付方式：0允许；1不允许"), Category("数据")]
        public string IsBalanceFlag
        {
            get
            {
                return this.isBalanceFlag;
            }
            set
            {
                this.isBalanceFlag = value;
            }
        }
        /// <summary>
        /// 支付方式列表
        /// </summary>
        private ArrayList arlPayMode = null;

        /// <summary>
        /// 支付方式列表，现金流对照字典
        /// </summary>
        private ArrayList arlPayModeCash = null;

        /// <summary>
        /// 默认的所有支付方式
        /// </summary>
        FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellTypeAll;

        #region 方法
        private void Save()
        {
            //判断权限,是否有退其他挂号员操作的权限
            if (!CommonController.CreateInstance().JugePrive("0820", "28"))
            {
                MessageBox.Show("您没有修改支付方式权限，如需开通请走企业微信申请！");
                // CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
                this.FindForm().Close();
                return;
            }
            this.fpSpread1.StopCellEditing();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                string Org = row[4].ToString();
                string Now = row[5].ToString();

                //判断是否日结
                string balanceFlag = row[9].ToString(); //日结标志
                if (this.IsBalanceFlag == "1" && (balanceFlag == "1" || balanceFlag == "已日结"))
                {
                    MessageBox.Show("该发票已日结，不能修改支付方式!");
                    return;
                }


                //没有要更新的数据
                if (Org == Now)
                {
                    continue;
                }
                if (cmbType.Tag .ToString () == TransKind.Balance.ToString ())
                {
                    if (this.fee.UpdateInBalance(row[0].ToString(), this.Trans_Type, "1", FS.FrameWork.Function.NConvert.ToInt32(row[2]),
                        row[8].ToString(), this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改出错!" + fee.Err);
                        return;
                    }
                    if (this.fee.InsertPayWayShiftInfo(row[0].ToString(), row[1].ToString(), (int)TransKind.Balance, this.Trans_Type, this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入支付方式更改记录出错" + fee.Err);
                        return;
                    }
                }
                else if (cmbType.Tag.ToString() == TransKind.PrePay.ToString())
                {
                    if (this.fee.UpdateInPrepay(row[0].ToString(), this.Trans_Type, FS.FrameWork.Function.NConvert.ToInt32(row[2]),
                         this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改出错!" + fee.Err);
                        return;
                    }
                    if (this.fee.InsertPayWayShiftInfo(row[0].ToString(), row[1].ToString(), (int)TransKind.PrePay, this.Trans_Type, this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入支付方式更改记录出错" + fee.Err);
                        return;
                    }
                }
                else if (cmbType.Tag.ToString() == TransKind.ClinicFee.ToString())
                {
                    if (this.fee.UpdateClinicFee(row[0].ToString(), this.Trans_Type, row[8].ToString(),
                        row[2].ToString(), this.GetIDbyName(row[4].ToString()),
                        this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改出错!" + fee.Err);
                        return;
                    }

                    if (this.fee.InsertPayWayShiftInfo(row[0].ToString(), row[2].ToString(), (int)TransKind.ClinicFee, this.Trans_Type, this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入支付方式更改记录出错" + fee.Err);
                        return;
                    }
                }
                else if (cmbType.Tag.ToString() == TransKind.AccountFee.ToString())
                {
                    if (this.fee.UpdateAccount(row[0].ToString(), row[8].ToString(), FS.FrameWork.Function.NConvert.ToInt32(row[2]),
                         this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改出错!" + fee.Err);
                        return;
                    }
                    if (this.fee.InsertPayWayShiftInfo(row[0].ToString(), row[2].ToString(), (int)TransKind.AccountFee, this.Trans_Type, this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入支付方式更改记录出错" + fee.Err);
                        return;
                    }
                }

                else if (cmbType.Tag.ToString() == TransKind.Package.ToString())
                {
                    ////string sqlBoard = string.Empty;
                    ////sqlBoard = @"update fin_boa_balancehead set ext_code='{0}' where invoice_no='{1}' and ext_code='{2}'";
                    ////sqlBoard = string.Format(sqlBoard, this.GetIDbyName(row[5].ToString()), row[0].ToString(), this.GetIDbyName(row[4].ToString()));
                    if (this.fee.UpdatePackagePayMode(row[0].ToString(), row[1].ToString(),Convert .ToInt32(row[2]), this.GetIDbyName( row[4].ToString()), this.GetIDbyName( row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改出错！" + fee.Err);
                        return;
                    }
                    if (this.fee.InsertPayWayShiftInfo(row[0].ToString(), row[1].ToString(), (int)TransKind.Package, this.Trans_Type, this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入支付方式更改记录出错" + fee.Err);
                        return;
                    }
                }
                else if (cmbType.Tag.ToString() == TransKind.PackagePrePay.ToString())
                {
                    if (this.fee.UpdatePackagePrePayMode(row[0].ToString(), row[1].ToString(), this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更改出错!" + fee.Err);
                        return;
                    }
                    if (this.fee.InsertPayWayShiftInfo(row[0].ToString(), row[1].ToString(), (int)TransKind.PackagePrePay, this.Trans_Type, this.GetIDbyName(row[4].ToString()), this.GetIDbyName(row[5].ToString())) != 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("插入支付方式更改记录出错" + fee.Err);
                        return;
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("变更成功！");

            this.QueryPayModeInfo();
        }

        private string GetIDbyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return "";
            }
            if (arlPayMode == null || arlPayMode.Count <= 0)
            {
                return "";
            }

            string PayID = "";
            foreach (FS.FrameWork.Models.NeuObject obj in arlPayMode)
            {
                if (obj.Name == name)
                {
                    PayID = obj.ID;
                    break;
                }
            }

            return PayID;

            //switch (name)
            //{
            //    case "现金":
            //        return "CA";
            //    case "信用卡":
            //    case "银行卡":
            //        return "CD";
            //    case "借记卡":
            //        return "DB";
            //    case "支票":
            //        return "CH";
            //    case "汇票":
            //        return "PO";
            //    case "转押金":
            //        return "AJ";
            //    case "保险帐户":
            //        return "PS";
            //    case "院内账户":
            //        return "YS";
            //    case "捐赠款":
            //        return "JZ";
            //    case "上期留存":
            //        return "BR";
            //    case "本期预留":
            //        return "YL";
            //    case "金卡":
            //        return "JK";
            //    case "IC卡":
            //        return "IC";
            //    default:
            //        return "CA";
            //}
        }

        private string GetNameByID(string ID)
        {
            if (string.IsNullOrEmpty(ID))
            {
                return "";
            }

            if (arlPayMode == null || arlPayMode.Count <= 0)
            {
                return "";
            }

            string PayName = "";
            foreach (FS.FrameWork.Models.NeuObject obj in arlPayMode)
            {
                if (obj.ID == ID)
                {
                    PayName = obj.Name;
                    break;
                }
            }

            return PayName;



            //switch (ID)
            //{
            //    case "CA":
            //        return "现金";
            //    case "CD":
            //        return "信用卡";
            //    case "DB":
            //        return "借记卡";
            //    case "CH":
            //        return "支票";
            //    case "PO":
            //        return "汇票";
            //    case "AJ":
            //        return "转押金";
            //    case "PS":
            //        return "保险帐户";
            //    case "YS":
            //        return "院内账户";
            //    case "JZ":
            //        return "捐赠款";
            //    case "BR":
            //        return "上期留存";
            //    case "YL":
            //        return "本期预留";
            //    case "JK":
            //        return "金卡";
            //    case "IC":
            //        return "IC卡";
            //    default:
            //        return "其他";
            //}
        }
        private string[] GetPayTypeNameForFpPayType()
        {
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            arlPayMode = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            //arlPayModeCash = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODESCASH);

            if (arlPayMode == null || arlPayMode.Count <= 0)
            {
                MessageBox.Show("获取支付方式错误");
                return null;
            }
            FS.FrameWork.Models.NeuObject t;
            string[] PayName = new string[arlPayMode.Count];
            for (int i = 0; i < arlPayMode.Count; i++)
            {
                t = (FS.FrameWork.Models.NeuObject)arlPayMode[i];
                PayName[i] = t.Name;
            }

            return PayName;
        }

        /// <summary>
        /// 获取现金流的支付方式选项
        /// </summary>
        /// <returns></returns>
        private string[] GetPayTypeNameForFpCashPayType()
        {
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            arlPayModeCash = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODESCASH);

            if (arlPayModeCash == null || arlPayModeCash.Count <= 0)
            {
                MessageBox.Show("获取现金流结算方式错误");
                return null;
            }
            FS.FrameWork.Models.NeuObject t;
            string[] PayName = new string[arlPayModeCash.Count];
            for (int i = 0; i < arlPayModeCash.Count; i++)
            {
                t = (FS.FrameWork.Models.NeuObject)arlPayModeCash[i];
                if (t.Memo == "true")
                {
                    PayName[i] = t.Name;
                }
            }

            List<String> list = new List<String>();
            for(int i=0;i<PayName.Length&&PayName.Length>0;i++)
            {
                if(PayName[i]==null||""==(PayName[i].Trim()))
                {
                    continue;
                }
                else
                {
                    list.Add(PayName[i]);
                } 
            }
            String []newArray=new String[list.Count];
            for(int i=0;i<newArray.Length;i++)
            {
                newArray[i]=list[i];
            }

            return newArray;
        }

        private void InitDataSet()
        {
            this.fpSpread1_Sheet1.DataAutoCellTypes = false;

            this.ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]{
													new DataColumn("收据号"),
													new DataColumn("交易类型"),
													new DataColumn("交易序号"),
													new DataColumn("金额"),
													new DataColumn("原支付方式"),
                                                    new DataColumn("新支付方式"),
													new DataColumn("操作员"),
													new DataColumn("操作日期"),
													new DataColumn("标志"),
                                                    new DataColumn("日结状态")});

            this.ds.Tables.Add(dt);
            //dv = new DataView(dt);
            //
            this.fpSpread1_Sheet1.DataAutoSizeColumns = false;
            this.InitCellType();
            this.fpSpread1_Sheet1.DataSource = ds;
            this.fpSpread1_Sheet1.DataAutoCellTypes = false;

        }

        private void InitCellType()
        {
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellType1 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            FarPoint.Win.Spread.CellType.ComboBoxCellType comboBoxCellTypeCash = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            comboBoxCellType1.Items = this.GetPayTypeNameForFpPayType();
            comboBoxCellTypeCash.Items = this.GetPayTypeNameForFpCashPayType();

            this.fpSpread1_Sheet1.Columns[5].CellType = comboBoxCellType1;
            /*
            for (int i = 0; i < fpSpread1_Sheet1.Rows.Count; i++)
            {
                if(fpSpread1.ActiveSheet.Cells[i, 4].Value!=null){
                    if (isPayModeCashMode(fpSpread1.ActiveSheet.Cells[i, 4].Value.ToString()))
                    {
                        fpSpread1.ActiveSheet.Cells[i, 5].CellType = comboBoxCellTypeCash;
                    }
                    else
                    {
                        //fpSpread1.ActiveSheet.Cells[i, 5].CellType = comboBoxCellType1;
                    }
                }
            }
             * */
        }

        private bool isPayModeCashMode(string modeName)
        {
            string PayID;
            foreach (FS.FrameWork.Models.NeuObject obj in arlPayModeCash)
            {
                if (obj.Name == modeName)
                {
                    PayID = obj.ID;
                    if (obj.Memo == "true")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        #endregion

        private void toolBar1_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {

            //    }
            //    if (e.Button == this.tbBoard)
            //    {
            //        if (this.tbChange.Enabled)
            //        {
            //            this.tbChange.Enabled = false;
            //        }
            //        else
            //        {
            //            this.tbChange.Enabled = true;
            //        }
            //        if (this.tbInprepay.Enabled)
            //        {
            //            this.tbInprepay.Enabled = false;
            //        }
            //        else
            //        {
            //            this.tbInprepay.Enabled = true;
            //        }
            //        this.chbTrans.Enabled = false;

            //        this.lblKind.Text = "膳食收据号:";
            //        this.Trans_Kind = TransKind.BoradCharge;
            //        this.Clear();
            //    }
            //    if (e.Button == this.tbInprepay)
            //    {
            //        if (!this.chbTrans.Enabled)
            //        {
            //            this.chbTrans.Enabled = true;
            //        }
            //        if (this.tbChange.Enabled)
            //        {
            //            this.tbChange.Enabled = false;
            //        }
            //        else
            //        {
            //            this.tbChange.Enabled = true;
            //        }
            //        if (this.tbBoard.Enabled)
            //        {
            //            this.tbBoard.Enabled = false;
            //        }
            //        else
            //        {
            //            this.tbBoard.Enabled = true;
            //        }
            //        this.lblKind.Text = "预交收据号:";
            //        this.Trans_Kind = TransKind.PrePay;
            //        this.Clear();
            //    }
            //    if (e.Button == this.tbSave)
            //    {
            //        this.Save();
            //        return;
            //    }
            //    if (e.Button == this.tbQuit)
            //    {
            //        this.Close();
            //        return;
            //    }
        }

        private void Clear()
        {
            this.txtInvoice.Clear();
            this.chbTrans.Checked = false;
            try
            {
                ds.Tables[0].Rows.Clear();
                if (this.fpSpread1_Sheet1.Rows.Count > 0)
                {
                    this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
                }
            }
            catch
            {
            }

        }

        private void textBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {


        }

        protected override void OnLoad(EventArgs e)
        {
              //判断权限,是否有退其他挂号员操作的权限
            if (!CommonController.CreateInstance().JugePrive("0820", "28"))
            {
                MessageBox.Show("您没有修改支付方式权限，如需开通请走企业微信申请！");
               // CommonController.CreateInstance().MessageBox("您没有退其他操作员收费记录的权限，操作已取消，该费用的操作员是：" + CommonController.CreateInstance().GetEmployeeName(invoiceTemp.BalanceOper.ID), MessageBoxIcon.Warning);
                this.FindForm().Close();
                return;
            }
                    
            if (System.IO.File.Exists(this.xmlPath))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.fpSpread1.Sheets[0], xmlPath);
            }
            else
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], xmlPath);
            }

            this.InitDataSet();
            base.OnLoad(e);
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            this.cmbType.AddItems(managerIntegrate.QueryConstantList("BILLTYPE"));
          
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();
            return 1;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F2)
            {

            }
            return base.ProcessDialogKey(keyData);
        }


        public enum TransKind
        {
            /// <summary>
            /// 结算款
            /// </summary>
            Balance = 0,
            /// <summary>
            /// 预交金
            /// </summary>
            PrePay = 1,
            /// <summary>
            /// 门诊收费
            /// </summary>
            ClinicFee = 2,
            /// <summary>
            /// 门诊账户
            /// </summary> 
            AccountFee = 3,
            /// <summary>
            /// 膳食结算
            /// </summary>
            //BoradCharge = 4
            /// <summary>
            /// 套餐收费
            /// </summary>
            Package=4,
            /// <summary>
            /// 套餐押金
            /// </summary>
            PackagePrePay=5,



        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                this.QueryPayModeInfo();
            }
            return base.OnQuery(sender, neuObject);
        }


        public void txtInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e != null && e.KeyData != Keys.Enter)
            {
                return;
            }
            if (!string.IsNullOrEmpty(txtInvoice.Text))
            {
                this.QueryPayModeInfo();
            }
        }

        private void QueryPayModeInfo()
        {
            string InvoiceNo = string.Empty;
            if (this.cmbType.Tag.ToString () != TransKind.Package.ToString ())
            {
                if (this.cmbType.Tag .ToString ()== TransKind.PackagePrePay.ToString())
                {
                    InvoiceNo = txtInvoice.Text.Trim();
                }
                else
                {
                    //2014-09-22 by han-zf 中大五院预交票据只有10位长
                    InvoiceNo = this.txtInvoice.Text.Trim().PadLeft(10, '0');
                }
            }
            else
            {
                InvoiceNo = this.txtInvoice.Text.Trim().PadLeft(12, '0');
            }

            if (InvoiceNo == null || InvoiceNo == "")
            {
                return;
            }
            this.txtInvoice.Text = InvoiceNo;

            try
            {
                this.ds.Tables[0].Rows.Clear();
            }
            catch { }


            DataSet dsList = null;
            //交易类型
            if (this.chbTrans.Checked)
            {
                this.Trans_Type = "2";
            }
            else
            {
                this.Trans_Type = "1";
            }

           // MessageBox.Show(cmbType.Tag.ToString ());

            if (cmbType.Tag.ToString() == TransKind.Balance.ToString())
            {
                dsList = this.fee.GetPayModeInBanlance(InvoiceNo, this.Trans_Type);
            }
            else if (cmbType.Tag.ToString() == TransKind.PrePay.ToString())
            {
                if (this.chbTrans.Checked)
                {
                    this.Trans_Type = "1";
                }
                else
                {
                    this.Trans_Type = "0";
                }
                dsList = this.fee.GetPayModeInprepay(InvoiceNo, this.Trans_Type);
            }
            else if (cmbType.Tag.ToString() == TransKind.ClinicFee.ToString())
            {
                dsList = this.fee.GetPayModeClinicFee(InvoiceNo, this.Trans_Type);
            }
            else if (cmbType.Tag.ToString() == TransKind.AccountFee.ToString ())
            {
                dsList = this.fee.GetPayModeAcount(InvoiceNo, this.Trans_Type.ToString ());
            }
            else if (cmbType.Tag.ToString() == TransKind.Package.ToString ())
            {
                dsList = this.fee.GetPackagePayModeBoardFee(InvoiceNo,Trans_Type);
            }
            else if (cmbType.Tag.ToString() == TransKind.PackagePrePay.ToString())
            {
                dsList = this.fee.GetPackagePrePayModeBoardFee(InvoiceNo,Trans_Type);
            }

            if (dsList == null)
            {
                MessageBox.Show("查询数据出错!" + this.fee.Err);
                return;
            }
            try
            {
                foreach (DataRow row in dsList.Tables[0].Rows)
                {
                    this.ds.Tables[0].Rows.Add(new object[]{row[0],
															row[1],
														    row[2],
														    FS.FrameWork.Function.NConvert.ToDecimal(row[3]),
														    this.GetNameByID(row[4].ToString()),
                                                            this.GetNameByID(row[4].ToString()),
														    row[5],
														    row[6],
														    row[7],
                                                            row[8]});
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.InitCellType();
            this.ds.AcceptChanges();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

        }

        private void fpSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.fpSpread1.Sheets[0], this.xmlPath);
        }

        //{3A31B775-3A98-4b6d-ABE0-555280ED7D06}
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            string setvalue = fpSpread1.ActiveSheet.Cells[e.Row, 4].Value.ToString();
            string oldvalue = fpSpread1.ActiveSheet.Cells[e.Row, 5].Value.ToString();
            //当前选中的行数
            int activeRow = fpSpread1.ActiveSheet.ActiveRowIndex;
            //选中的如果是第五行，选择变更的支付方式发生变化
            if (e.Column == 5)
            {
                //旧的支付方式如果是现金流，则允许变更为现金流支付方式
                if (isPayModeCashMode(fpSpread1.ActiveSheet.Cells[activeRow, 4].Value.ToString()))
                {
                    //选中的是现金流类支付方式，允许操作
                    if (isPayModeCashMode(fpSpread1.ActiveSheet.Cells[activeRow, 5].Value.ToString()))
                    {

                    }
                    else
                    {
                        fpSpread1.ActiveSheet.Cells[activeRow, 5].Value = setvalue;
                        MessageBox.Show("只能修改为现金流类的支付方式！");
                    }
                }
                else
                {
                    //旧支付方式不是现金流，则不允许修改
                    fpSpread1.ActiveSheet.Cells[activeRow, 5].Value = setvalue;
                    MessageBox.Show("禁止修改非金流类项目！");
                }
            }
            txtInvoice.Focus();
        }
    }
}

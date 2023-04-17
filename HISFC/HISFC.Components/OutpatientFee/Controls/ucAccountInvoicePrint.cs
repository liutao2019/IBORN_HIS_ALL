using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    public partial class ucAccountInvoicePrint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAccountInvoicePrint()
        {
            InitializeComponent();
        }

        #region 业务层
        /// <summary>
        /// 费用综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 账户业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 挂号综合业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region 变量
        /// <summary>
        /// 所有费用明细
        /// </summary>
        ArrayList alFee = new ArrayList();
        /// <summary>
        /// 按照挂号信息分出的费用明细
        /// </summary>
        Dictionary<string, ArrayList> listFee = new Dictionary<string, ArrayList>();
        
        /// <summary>
        /// 按照挂号信息分出的支付方式信息
        /// </summary>
        Dictionary<string, ArrayList> listPay = new Dictionary<string, ArrayList>();
        #endregion

        #region 方法

        /// <summary>
        /// 显示信息
        /// </summary>
        /// <param name="accountCard"></param>
        /// <returns></returns>
        private int SetInfo(AccountCard accountCard, FS.HISFC.Models.Account.Account account)
        {
            SetPatientInfo(accountCard);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据请等待.....");
            Application.DoEvents();

            #region 预交金信息
            List<FS.HISFC.Models.Account.PrePay> list = accountManager.GetPrepayByAccountNO(account.ID, "ALL");
            if (list == null)
            {
                MessageBox.Show("查询患者预交金信息失败！" + accountManager.Err);
                this.Clear();
                return -1;
            }
            decimal prePayCost = 0m;
            SetAccountRecordToFp(list, fpFee_PrePay,ref prePayCost);
            this.txtPrePay.Text = prePayCost.ToString();
            this.txtVacancy.Text = account.BaseVacancy.ToString();
            this.txtCost.Text = (prePayCost - account.BaseVacancy).ToString();
            #endregion

            #region 显示费用信息
            alFee = this.outpatientManager.GetAccountNoPrintFeeItemList(accountCard.Patient.PID.CardNO, FS.HISFC.Models.Base.PayTypes.Balanced,true );
            if (alFee == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询患者费用明细失败！" + outpatientManager.Err);
                return -1;
            }

            this.SetFeeFp();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            #endregion
            return 1;
        }

        /// <summary>
        /// 显示账户授权信息
        /// </summary>
        /// <param name="accountCard"></param>
        /// <param name="accountEmpower"></param>
        /// <returns></returns>
        private int SetInfo(AccountCard accountCard, AccountEmpower accountEmpower)
        {
            this.SetPatientInfo(accountCard);

            this.txtPrePay.Text = accountEmpower.EmpowerLimit.ToString();
            this.txtVacancy.Text = accountEmpower.Vacancy.ToString() ;
            this.txtCost.Text = (accountEmpower.EmpowerLimit - accountEmpower.Vacancy).ToString();

            alFee = this.outpatientManager.GetAccountNoPrintFeeItemList(accountCard.Patient.PID.CardNO, FS.HISFC.Models.Base.PayTypes.Balanced,true);
            if (alFee == null)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("查询患者费用明细失败！" + outpatientManager.Err);
                return -1;
            }
            this.SetFeeFp();
            return 1;
        }

        /// <summary>
        /// 显示患者信息
        /// </summary>
        /// <param name="accountCard"></param>
        /// <returns></returns>
        private void SetPatientInfo(AccountCard accountCard)
        {
            this.txtMarkNO.Text = accountCard.MarkNO;
            this.txtName.Text = accountCard.Patient.Name;
            this.txtSex.Text = accountCard.Patient.Sex.Name;
            this.txtBirthDay.Text = accountCard.Patient.Birthday.ToString("yyyy-MM-dd");
            this.txtAge.Text = outpatientManager.GetAge(accountCard.Patient.Birthday);
            this.txtIdeNO.Text = accountCard.Patient.IDCard;
        }

        /// <summary>
        /// 显示帐户预交金数据
        /// </summary>
        /// <param name="list">预交金数据</param>
        /// <param name="sheet">sheetView</param>
        private void SetAccountRecordToFp(List<FS.HISFC.Models.Account.PrePay> list, FarPoint.Win.Spread.SheetView sheet, ref decimal cost)
        {
            int count = sheet.Rows.Count;
            if (count > 0)
            {
                sheet.Rows.Remove(0, count);
            }
            foreach (FS.HISFC.Models.Account.PrePay prepay in list)
            {
                cost += prepay.FT.PrepayCost;
                SetFp(prepay, sheet);
                
            }
        }

        /// <summary>
        /// 显示预交金信息
        /// </summary>
        /// <param name="prepay"></param>
        private void SetFp(FS.HISFC.Models.Account.PrePay prepay, FarPoint.Win.Spread.SheetView sheet)
        {
            int count = sheet.Rows.Count;
            sheet.Rows.Add(count, 1);
            sheet.Cells[count, 0].Text = prepay.InvoiceNO;
            if (prepay.FT.PrepayCost > 0)
            {
                sheet.Cells[count, 1].Text = "收取";
            }
            else
            {
                if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    sheet.Cells[count, 1].Text = "返还";

                }
                else if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    sheet.Cells[count, 1].Text = "补打";
                }
                else
                {
                    sheet.Cells[count, 1].Text = "收取";
                }
            }
            if (prepay.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                sheet.Cells[count, 1].ForeColor = Color.Red;
            }
            sheet.Cells[count, 2].Text = prepay.FT.PrepayCost.ToString();
            sheet.Cells[count, 3].Text = prepay.PrePayOper.OperTime.ToString();
            //
            FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
            empl = managerIntergrate.GetEmployeeInfo(prepay.PrePayOper.ID);

            if (empl == null)
            { prepay.PrePayOper.Name = ""; }
            else
            {
                prepay.PrePayOper.Name = empl.Name;
            }
            sheet.Cells[count, 4].Text = prepay.PrePayOper.Name;
            sheet.Rows[count].Tag = prepay;
        }

        /// <summary>
        /// 显示费用数据
        /// </summary>
        /// <param name="f"></param>
        private void SetFeeFp()
        {

            string clinicCode = string.Empty;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in alFee)
            {
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    this.SetDrugFp(f);
                }
                else
                {
                    this.SetUnDrugFp(f);
                }

                #region 将费用按照挂号信息从新分
                clinicCode = f.Patient.ID;
                if (!listFee.ContainsKey(clinicCode))
                {
                    ArrayList al = new ArrayList();
                    listFee.Add(clinicCode, al);
                }
                listFee[clinicCode].Add(f);
                #endregion
            }
        }

        /// <summary>
        /// 显示药品费用信息
        /// </summary>
        /// <param name="f">药品费用信息</param>
        private void SetDrugFp(FeeItemList f)
        {
            int count = 0;
            count = fpFee_Drug.Rows.Count;
            this.fpFee_Drug.Rows.Add(count, 1);
            this.fpFee_Drug.Cells[count, 0].Text = f.Item.Name;
            this.fpFee_Drug.Cells[count, 1].Text = f.Item.Specs;
            this.fpFee_Drug.Cells[count, 2].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            this.fpFee_Drug.Cells[count, 3].Text = f.Item.PriceUnit;
            this.fpFee_Drug.Cells[count, 4].Text = f.Item.Price.ToString();
            this.fpFee_Drug.Cells[count, 5].Text = f.FT.OwnCost.ToString() ;
        }
        
        /// <summary>
        /// 显示非药品费用信息
        /// </summary>
        /// <param name="f">非药品费用信息</param>
        private void SetUnDrugFp(FeeItemList f)
        {
            int count = 0;
            count = fpFee_Undrug.Rows.Count;
            this.fpFee_Undrug.Rows.Add(count, 1);
            this.fpFee_Undrug.Cells[count, 0].Text = f.Item.Name;
            this.fpFee_Undrug.Cells[count, 1].Text = f.FeePack == "1" ?
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / f.Item.PackQty, 2).ToString() :
                        FS.FrameWork.Public.String.FormatNumber(f.Item.Qty, 2).ToString();
            this.fpFee_Undrug.Cells[count, 2].Text = f.Item.PriceUnit;
            this.fpFee_Undrug.Cells[count, 3].Text = f.Item.Price.ToString();
            this.fpFee_Undrug.Cells[count, 4].Text = f.FT.OwnCost.ToString();
        }

        private int MakeInvoice()
        {
            if (alFee == null || alFee.Count == 0)
            {
                MessageBox.Show("该患者不存在费用！");
                return -1;
            }
            listPay.Clear();
            ArrayList al = null;

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.outpatientManager.Operator.ID);
            if (employee == null)
            {
                MessageBox.Show("获取人员信息失败！" + managerIntegrate.Err);
                return -1;
            }

            string errText = string.Empty;

            #region 获取发票号
            string invoiceNO = string.Empty, realInvoiceNO = string.Empty;
            
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
           
            //获得本次收费起始发票号
            int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee,"C", ref invoiceNO, ref realInvoiceNO, ref errText);
            if (iReturnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return -1;
            }

            FS.FrameWork.Management.PublicTrans.RollBack();
            #endregion

            Dictionary<HISFC.Models.Registration.Register, ArrayList> listInvoice = new Dictionary<FS.HISFC.Models.Registration.Register, ArrayList>();
            ArrayList balance = new ArrayList();
            FS.HISFC.Models.Registration.Register r = null;
            //根据每次挂号所生成的费用生成发票
            foreach (string key in listFee.Keys)
            {
                al = listFee[key];
                r = registerIntegrate.GetByClinic(key);
                if (r == null)
                {
                    MessageBox.Show("查询患者挂号信息失败！");
                    return -1;
                }
                balance = Class.Function.MakeInvoice(this.feeIntegrate, r, al, invoiceNO, realInvoiceNO, ref errText);
                if (balance == null)
                {
                    return -1;
                }
                listInvoice.Add(r, balance);
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            string operID = outpatientManager.Operator.ID;
            DateTime feeTime = outpatientManager.GetDateTimeFromSysDateTime();
            try
            {
                FS.HISFC.Models.Registration.Register register = null;
                ArrayList balancesList = null;
                ArrayList invoices = null;
                ArrayList invoiceDetail = null;
                ArrayList feeList = null;
                IDictionaryEnumerator ide = listInvoice.GetEnumerator();

                while (ide.MoveNext())
                {
                    register = ide.Key as FS.HISFC.Models.Registration.Register;
                    balancesList = ide.Value as ArrayList;

                    invoices = balancesList[0] as ArrayList;
                    invoiceDetail = balancesList[1] as ArrayList;
                    feeList = balancesList[2] as ArrayList;
                    //插入发票信息
                    if (this.InsertInvoices(invoices, register, feeTime, operID, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                    //插入发票明细
                    if (this.InsertInvoiceDetails(invoiceDetail, feeTime, operID, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                    //插入支付方式信息
                    if (this.InsertInvocePayMode(invoices, feeTime, operID, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        return -1;
                    }
                    feeList = listFee[register.ID];
                    //更新费用明细
                    if (this.UpdateFeeItemList(feeList, ref errText) < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                    }
                }


                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                // 更改发票打印类获取方式；兼容原来方式
                // 2011-08-04
                // 此处不作提示
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("没有设置发票打印参数，收费请维护!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //}
                ide.Reset();

                while (ide.MoveNext())
                {
                    register = ide.Key as HISFC.Models.Registration.Register;
                    balancesList = ide.Value as ArrayList;
                    invoices = balancesList[0] as ArrayList;
                    ArrayList tempal = new ArrayList();
                    foreach (ArrayList obj in balancesList[1] as ArrayList)
                    {
                        tempal.Add(obj[0]);
                    }
                    invoiceDetail = new ArrayList();
                    invoiceDetail.Add(tempal);
                    feeList = listFee[register.ID];
                    this.feeIntegrate.PrintInvoice(invoicePrintDll, register, invoices, invoiceDetail, feeList, listPay[register.ID], false, ref errText);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                this.Clear();
            }
            catch(Exception ex) 
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("打印发票失败！" + ex.Message);
                return -1;
            }

            return 1;
        }


        /// <summary>
        /// 插入发票明细
        /// </summary>
        /// <param name="invoiceDetails"></param>
        /// <param name="feeTime"></param>
        /// <param name="operID"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int InsertInvoiceDetails(ArrayList invoiceDetails,DateTime feeTime,string operID,ref string errText)
        {
            int iReturn = 0;
            foreach (ArrayList tempsInvoices in invoiceDetails)
            {
                foreach (ArrayList tempDetals in tempsInvoices)
                {
                    foreach (BalanceList balanceList in tempDetals)
                    {
                        balanceList.BalanceBase.BalanceOper.ID = operID;
                        balanceList.BalanceBase.BalanceOper.OperTime = feeTime;
                        balanceList.BalanceBase.IsDayBalanced = false;
                        balanceList.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                        balanceList.ID = balanceList.ID.PadLeft(12, '0');

                        iReturn = outpatientManager.InsertBalanceList(balanceList);
                        if (iReturn == -1)
                        {
                            errText = "插入发票明细出错!" + outpatientManager.Err;
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 插入发票主表
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        private int InsertInvoices(ArrayList invoices,FS.HISFC.Models.Registration.Register r,DateTime feeTime,string operID, ref string errText)
        {
            int iReturn = 0;
            foreach (Balance balance in invoices)
            {
                balance.BalanceOper.ID = operID;
                balance.BalanceOper.OperTime = feeTime;
                balance.Patient.Pact = r.Pact;
                //体检标志
                string tempExamineFlag = null;
                //获得体检标志 0 普通患者 1 个人体检 2 团体体检
                //如果没有赋值,默认为普通患者

                balance.ExamineFlag = "0";
                balance.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                balance.IsAuditing = false;
                balance.IsDayBalanced = false;
                balance.ID = balance.ID.PadLeft(12, '0');
                balance.IsAccount = true;
                //插入发票主表fin_opb_invoice
                iReturn = this.outpatientManager.InsertBalance(balance);
                if (iReturn == -1)
                {
                    errText = "插入结算表出错!" + outpatientManager.Err;

                    return -1;
                }
                iReturn = this.feeIntegrate.UpdateInvoiceNO(balance.Invoice.ID, balance.PrintedInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    return -1;
                }
            }            

            return 1;
        }

        /// <summary>
        /// 插入支付明细表
        /// </summary>
        /// <param name="invoices"></param>
        /// <returns></returns>
        private int InsertInvocePayMode(ArrayList invoices,DateTime feeTime,string operID,ref string errText)
        {
            foreach (Balance b in invoices)
            {
                FS.HISFC.Models.Fee.Outpatient.BalancePay payMod = new BalancePay();
                payMod.Invoice = b.Invoice;
                payMod.PayType.ID = "YS";
                payMod.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                payMod.Squence = "1";
                payMod.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                payMod.IsDayBalanced = false;
                payMod.IsAuditing = false;
                payMod.IsChecked = false;
                payMod.InputOper.ID = operID;
                payMod.InputOper.OperTime = feeTime;
                payMod.FT.RealCost = b.FT.TotCost;
                payMod.FT.ReturnCost = 0;
                payMod.FT.TotCost = b.FT.TotCost;
                payMod.InvoiceCombNO = b.CombNO;
                if (outpatientManager.InsertBalancePay(payMod) < 0)
                {
                    errText = "插入支付方式信息失败！" + outpatientManager.Err;
                    return -1;
                }
                if (!listPay.ContainsKey(b.Patient.ID))
                {
                    ArrayList al = new ArrayList();
                    listPay.Add(b.Patient.ID, al);
                }
                listPay[b.Patient.ID].Add(payMod);
            }
            return 1;
        }

        /// <summary>
        /// 更新费用明细发票信息
        /// </summary>
        /// <param name="feeList"></param>
        /// <param name="errText"></param>
        /// <returns></returns>
        private int UpdateFeeItemList(ArrayList feeList,ref string errText)
        {
            int resultValue = 0;
            foreach (FeeItemList f in feeList)
            {
                resultValue = outpatientManager.UpdateFeeItemListInvoiceInfo(f);
                if (resultValue < 0)
                {
                    errText = "更新费用明细失败！" + outpatientManager.Err;
                    return -1;
                }
                if (resultValue == 0)
                {
                    errText = "数据发生变化请刷新！";
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.txtMarkNO.Text = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtSex.Text = string.Empty;
            this.txtBirthDay.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtIdeNO.Text = string.Empty;
            this.txtCost.Text = string.Empty;
            this.txtVacancy.Text = string.Empty;
            this.txtPrePay.Text = string.Empty;
            int count = fpFee_Drug.Rows.Count;
            if (count > 0)
            {
                this.fpFee_Drug.Rows.Remove(0, count);
            }
            count = fpFee_Undrug.Rows.Count;
            if (count > 0)
            {
                this.fpFee_Undrug.Rows.Remove(0, count);
            }
            this.txtMarkNO.Focus();
            alFee.Clear();
            listPay.Clear();
            listFee.Clear();
            return;
        }
        #endregion

        #region 事件
        private void txtMarkNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            string markNO = this.txtMarkNO.Text.Trim();
            this.Clear();
            if (string.IsNullOrEmpty(markNO))
            {
                MessageBox.Show("请输入就诊卡号！");
                this.txtMarkNO.Focus();
                return;
            }
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            if (feeIntegrate.ValidMarkNO(markNO, ref accountCard) <= 0)
            {
                MessageBox.Show(feeIntegrate.Err);
                this.txtMarkNO.Text = string.Empty;
                this.txtMarkNO.Focus();
                return;
            }

            FS.HISFC.Models.Account.Account account = accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);
            if (account == null)
            {
                AccountEmpower accountEmpower = new AccountEmpower();
                if (accountManager.QueryAccountEmpowerByEmpwoerCardNO(accountCard.Patient.PID.CardNO, ref accountEmpower) < 0)
                {
                    MessageBox.Show("该患者不能存在账户或有效的授权信息！" + accountManager.Err);
                    this.Clear();
                    return;
                }
                else
                {
                    SetInfo(accountCard, accountEmpower);
                }
            }
            else
            {
                SetInfo(accountCard, account);
            }
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            MakeInvoice();
            return base.OnPrint(sender, neuObject);
        }

        private void ucAccountInvoicePrint_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtMarkNO;
            this.fpFee_PrePay.Visible = false;
        }
        #endregion
    }
}

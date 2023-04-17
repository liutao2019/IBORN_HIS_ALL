using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data; 
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.HISFC.BizProcess.Interface.Account;
using FS.HISFC.Components.Account.Forms;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    /// <summary>
    /// 停车票系统
    /// </summary>
    public partial class ucParkingTicket : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucParkingTicket()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
       
        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        
        /// <summary>
        /// 停车票状态
        /// </summary>
        private ArrayList validStateList = new ArrayList();

        /// <summary>
        /// 门诊卡号
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// 所有收费员
        /// </summary>
        private ArrayList allOper = new ArrayList();

        /// <summary>
        /// 所有卡卷信息
        /// </summary>
        private ArrayList alCardVolume = new ArrayList();

        /// <summary>
        /// 所有支付
        /// </summary>
        private ArrayList allPayModes = new ArrayList();
        /// <summary>
        /// 所有停车票类型
        /// </summary>
        private ArrayList ticketTypeList = new ArrayList();

        /// <summary>
        /// 所有停车票类型哈希
        /// </summary>
        private Hashtable hsTicketTypeList = new Hashtable();
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() < 0)
            {
                MessageBox.Show("初始化失败！");
                return;
            }
            this.ckSelect.CheckedChanged += new EventHandler(ckSelect_CheckedChanged);
            base.OnLoad(e);
        }
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolbarService.AddToolButton("收取", "收取", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("退费", "退费", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            //toolbarService.AddToolButton("全退", "全退", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("领取停车票", "领取停车票", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolbarService.AddToolButton("回退停车票", "回退停车票", FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);      
            toolbarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
          
            return toolbarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "收取":
                    {
                        this.SaveFee();
                        break;
                    }

                case "退费":
                    {
                        this.ReturnFee(false);
                        break;
                    }

                case "全退":
                    {
                        this.ReturnFee(true);
                        break;
                    }

                case "清屏":
                    {
                        this.Clear();
                        break;
                    }

                case "领取停车票":
                    {
                        this.SaveTicket();
                        break;
                    }

                case "回退停车票":
                    {
                        this.ReturnTicket();
                        break;
                    }

                    base.ToolStrip_ItemClicked(sender, e);
            }
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                ExecCmdKey();
            }
            return base.ProcessDialogKey(keyData);
        }

        //protected override int OnSave(object sender, object neuObject)
        //{
        //    this.Save();
        //    return base.OnSave(sender, neuObject);
        //}
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// 回车处理
        /// </summary>
        protected virtual void ExecCmdKey()
        {
        }
        private int Init()
        {
            this.allOper = this.conMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);
            //this.allOper = this.conMgr.QueryEmployeeAll();

            if (allOper == null)
            {
                MessageBox.Show("获取操作人出错！");
                return -1;
            }
            FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "ALL";
            cons.Name = "全部";
            validStateList.Add(cons);
            cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "1";
            cons.Name = "收取"; ;
            validStateList.Add(cons);
            cons = new FS.HISFC.Models.Base.Const();
            cons.ID = "2";
            cons.Name = "退费"; ;
            validStateList.Add(cons);
            this.cmbParkingState.AddItems(validStateList);
            this.cmbParkingState.Tag = "ALL";

            //操作人
            cons.ID = "ALL";
            cons.Name = "全部";
            allOper.Add(cons);
            this.cmbOper.AddItems(allOper);
            this.cmbOper.Text = FS.FrameWork.Management.Connection.Operator.Name;
            this.cmbOper.Tag = FS.FrameWork.Management.Connection.Operator.ID;

            ticketTypeList = this.conMgr.GetConstantList("ParkingTicketType");
            if (ticketTypeList == null)
            {
                MessageBox.Show("获取停车票类型出错！");
                return -1;
            }
            foreach (FS.HISFC.Models.Base.Const con in ticketTypeList)
            {
                if (!this.hsTicketTypeList.Contains(con.ID))
                {
                    this.hsTicketTypeList.Add(con.ID, con);
                }
            }

            this.cmbParkingType.AddItems(ticketTypeList);

            allPayModes = this.conMgr.GetConstantList("PAYMODES");
            if (allPayModes == null)
            {
                MessageBox.Show("获取支付方式出错！");
                return -1;
            }
            this.cmbPayMode.AddItems(allPayModes);


            this.dtBegTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegTime.Value.ToLongDateString() + " 00:00:00");
            this.dtEndTime.Value = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEndTime.Value.ToLongDateString() + " 23:59:59");

            this.Clear();
            return 1;
        }
        /// <summary>
        ///验证
        /// </summary>
        /// <returns></returns>
        private bool IsVaild()
        {
            if (string.IsNullOrEmpty(this.cmbParkingType.Tag.ToString()) || string.IsNullOrEmpty(this.cmbParkingType.Text))
            {
                MessageBox.Show("请选择停车票类型！");
                this.cmbParkingType.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(this.cmbPayMode.Tag.ToString()) || string.IsNullOrEmpty(this.cmbPayMode.Text))
            {
                MessageBox.Show("请选择支付方式！");
                this.cmbPayMode.Focus();
                return false;
            }

            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!rex.IsMatch(this.txtQty.Text))
            {
                MessageBox.Show("请输入正确的数量！");
                this.txtQty.Text = "0";
                this.lblTotCost.Text = "0.00";
                return false;
            }
            if (decimal.Parse(this.txtQty.Text) <= 0)
            {
                MessageBox.Show("请输入正确的数量！");
                this.txtQty.Text = "0";
                this.lblTotCost.Text = "0.00";
                this.txtQty.Focus();
                return false;

            }
            string sql = "select count(*) cc from FIN_COM_PARKINGTOTAL d where d.item_code = '{0}'and d.oper_code = '{1}'";
            sql = string.Format(sql, this.cmbParkingType.Tag.ToString(), FS.FrameWork.Management.Connection.Operator.ID);
            if (int.Parse(this.accountManager.ExecSqlReturnOne(sql)) <= 0)
            {
                MessageBox.Show("请先领取停车票后操作！");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 收取费用
        /// </summary>
        private void SaveFee()
        {
            if (!this.IsVaild()) return;

            //if (string.IsNullOrEmpty(this.txtParkingNo.Text))
            //{
            //    MessageBox.Show("请输入收取的停车票号及号段！");
            //    this.txtParkingNo.Focus();
            //    return;
            //}
            if (this.QueryQty(FS.FrameWork.Management.Connection.Operator.ID, this.cmbParkingType.Tag.ToString()) <= 0)
            {
                MessageBox.Show("请领取停车票！");
                return;
            }

            FS.HISFC.Models.Account.ParkingTicketFeeInfo ptInfo = new ParkingTicketFeeInfo();

            ptInfo.ItemCode = this.cmbParkingType.Tag.ToString();
            ptInfo.ItemName = this.cmbParkingType.Text;
            ptInfo.TransType = TransTypes.Positive;
            FS.HISFC.Models.Base.Const cons = this.hsTicketTypeList[this.cmbParkingType.Tag.ToString()] as FS.HISFC.Models.Base.Const;
            if (cons != null)
            {
                this.lblTotCost.Text = (decimal.Parse(this.txtQty.Text) * decimal.Parse(cons.UserCode)).ToString("F2");
            }
            string invoiceNo = accountManager.ExecSqlReturnOne("select SEQ_PTInvoiceNo.Nextval from dual", "");
            if(!string.IsNullOrEmpty(invoiceNo))
            {
                ptInfo.InvoiceNo = invoiceNo.PadLeft(12,'0');
            }
            ptInfo.UnitPrice = decimal.Parse(cons.UserCode);
            ptInfo.Unit = cons.Memo;
            ptInfo.Qty = decimal.Parse(this.txtQty.Text);
            ptInfo.TotCost = decimal.Parse(this.txtQty.Text) * decimal.Parse(cons.UserCode);
            ptInfo.PayMode.ID = this.cmbPayMode.Tag.ToString();
            ptInfo.PayMode.Name = this.cmbPayMode.Text;
            ptInfo.TicketNo = this.txtParkingNo.Text;
            ptInfo.Memo = this.txtMark.Text;
            ptInfo.ValidState = EnumValidState.Valid;
            ptInfo.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
            ptInfo.OperEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();
            //插入收费记录

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (accountManager.InsertParkingTicketInfo(ptInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("收费失败！" + accountManager.Err);
                return;
            }
            //更新剩余停车票数量
            if (accountManager.UpdateTicketTotalQty(ptInfo.OperEnvironment.ID, ptInfo.ItemCode, (-ptInfo.Qty).ToString(),"0") < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新停车票剩余数量失败！" + accountManager.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("收取成功！");
            this.Clear();

        }
        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="isAllReturn"></param>
        private void ReturnFee(bool isAllReturn)
        {

            if (!this.IsVaild()) return;

            decimal returnQty = 0m;
            returnQty = decimal.Parse(this.txtQty.Text);
            string sql = "select nvl((d.qty_accumulate - d.qty),0) qty from fin_com_parkingtotal d where d.item_code = '{0}' and d.oper_code = '{1}'";
            sql = string.Format(sql, this.cmbParkingType.Tag.ToString(), FS.FrameWork.Management.Connection.Operator.ID);
            if (decimal.Parse(this.accountManager.ExecSqlReturnOne(sql)) < returnQty)
            {
                MessageBox.Show("退费数量大于实际收费数量，请审核！");
                return;
            }



            FS.HISFC.Models.Account.ParkingTicketFeeInfo ptInfo = new ParkingTicketFeeInfo();

            ptInfo.ItemCode = this.cmbParkingType.Tag.ToString();
            ptInfo.ItemName = this.cmbParkingType.Text;
            ptInfo.TransType = TransTypes.Negative;
            FS.HISFC.Models.Base.Const cons = this.hsTicketTypeList[this.cmbParkingType.Tag.ToString()] as FS.HISFC.Models.Base.Const;
            if (cons != null)
            {
                this.lblTotCost.Text = (-returnQty * decimal.Parse(cons.UserCode)).ToString("F2");
            }
            string invoiceNo = accountManager.ExecSqlReturnOne("select SEQ_PTInvoiceNo.Nextval from dual", "");
            if (!string.IsNullOrEmpty(invoiceNo))
            {
                ptInfo.InvoiceNo = invoiceNo.PadLeft(12, '0');
            }
            ptInfo.UnitPrice = decimal.Parse(cons.UserCode);
            ptInfo.Unit = cons.Memo;
            ptInfo.Qty = -returnQty;
            ptInfo.TotCost = -returnQty * decimal.Parse(cons.UserCode);
            ptInfo.PayMode.ID = this.cmbPayMode.Tag.ToString();
            ptInfo.PayMode.Name = this.cmbPayMode.Text;
            ptInfo.TicketNo = this.txtParkingNo.Text;
            ptInfo.Memo = this.txtMark.Text;
            ptInfo.ValidState = EnumValidState.Valid;
            ptInfo.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
            ptInfo.OperEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();


            //插入退费记录
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            if (accountManager.InsertParkingTicketInfo(ptInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("退费失败！" + accountManager.Err);
                return;
            }

            //更新剩余停车票数量
            if (accountManager.UpdateTicketTotalQty(ptInfo.OperEnvironment.ID, ptInfo.ItemCode, this.txtQty.Text,"0") < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新停车票剩余数量失败！" + accountManager.Err);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("退费成功！");
            this.Clear();

            #region 换流程，作废该操作
            //if (!isAllReturn)
            //{
            //    System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            //    if (!rex.IsMatch(this.txtQty.Text))
            //    {
            //        MessageBox.Show("请输入正确的数量！");
            //        this.txtQty.Text = "0";
            //        this.lblTotCost.Text = "0.00";
            //        return;
            //    }
            //    if (decimal.Parse(this.txtQty.Text) <= 0)
            //    {
            //        MessageBox.Show("请输入正确的数量！");
            //        this.txtQty.Text = "0";
            //        this.lblTotCost.Text = "0.00";
            //        this.txtQty.Focus();
            //        return;

            //    }
            //    returnQty = decimal.Parse(this.txtQty.Text);
            //}

            //if (this.neuSpread1.ActiveSheet != this.neuSpread1_Sheet1)
            //{
            //    return;
            //}
            //if (this.neuSpread1_Sheet1.RowCount == 0)
            //{
            //    MessageBox.Show("请查询一条费用进行退费！");
            //    return;
            //}
            //if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1 && this.neuSpread1_Sheet1.ActiveRow == null)
            //{
            //    MessageBox.Show("请选择一条费用进行退费！");
            //    return;
            //}
            //int row = this.neuSpread1_Sheet1.ActiveRowIndex;
            //if (this.neuSpread1_Sheet1.RowCount > 0)
            //{
            //    if (this.neuSpread1_Sheet1.Rows[row].Tag != null)
            //    {
            //        FS.HISFC.Models.Account.ParkingTicketFeeInfo ptInfo = new ParkingTicketFeeInfo();
            //        ptInfo = this.neuSpread1_Sheet1.Rows[row].Tag as ParkingTicketFeeInfo;
                  
            //        if (ptInfo.ValidState != EnumValidState.Valid)
            //        {
            //            MessageBox.Show("该条费用已经作废，无法进行退费！");
            //            return;
            //        }
            //        if (ptInfo.IsBalance)//日结可退可不退，默认不可以退
            //        {
            //            MessageBox.Show("该条费用已经日结，无法进行退费！");
            //            return;
            //        }
            //        if (isAllReturn)
            //        {
            //            returnQty = ptInfo.Qty;
            //            this.txtQty.Text = ptInfo.Qty.ToString();
            //        }
            //        if (decimal.Parse(this.txtQty.Text) > ptInfo.Qty)
            //        {
            //            MessageBox.Show("退费数量大于了实际可退数量！请重新输入！");
            //            this.txtQty.Text = "0";
            //            this.txtQty.Focus();
            //            return;
            //        }
            //        else if (decimal.Parse(this.txtQty.Text) < ptInfo.Qty)
            //        {
            //            if (string.IsNullOrEmpty(this.txtParkingNo.Text))
            //            {
            //                MessageBox.Show("请输入剩余未退的停车票号及号段！");
            //                this.txtParkingNo.Focus();
            //                return;
            //            }
            //        }
            //        string date = this.accountManager.GetDateTimeFromSysDateTime().ToString();
            //        //半退全退都全退后确定是否重收
            //        //插入负记录
            //        decimal OldQty = 0m;
            //        ptInfo.TransType = TransTypes.Negative;
            //        ptInfo.ValidState = EnumValidState.Invalid;
            //        OldQty = ptInfo.Qty;
            //        ptInfo.Qty = -ptInfo.Qty;
            //        ptInfo.TotCost = -ptInfo.TotCost;
            //        ptInfo.PayMode.ID = this.cmbPayMode.Tag.ToString();
            //        ptInfo.PayMode.Name = this.cmbPayMode.Text;
            //        ptInfo.IsBalance = false;
            //        ptInfo.IsCheck = false;
            //        ptInfo.BalanceNo = "";
            //        ptInfo.BalanceEnvironment = new OperEnvironment();
            //        ptInfo.CheckEnvironment = new OperEnvironment();
            //        ptInfo.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
            //        ptInfo.OperEnvironment.OperTime = this.accountManager.GetDateTimeFromSysDateTime();

            //        FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //        accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //        //这里插入退费的负记录
            //        if (accountManager.InsertParkingTicketInfo(ptInfo) < 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            MessageBox.Show("退费失败！插入负记录失败！" + accountManager.Err);
            //            return;
            //        }
                    
            //        //这里作废原来的发票                    
            //        if (accountManager.UpdateTicketInfoState(ptInfo.InvoiceNo, "0", date) < 0)
            //        {
            //            FS.FrameWork.Management.PublicTrans.RollBack();
            //            MessageBox.Show("退费失败！更新发票状态失败！" + accountManager.Err);
            //            return;
            //        }
            //        //不是全退的，重新收取费用
            //        if (OldQty - returnQty > 0)
            //        {
            //            FS.HISFC.Models.Account.ParkingTicketFeeInfo ptInfoSaveFee = new ParkingTicketFeeInfo();
            //            //ptInfoSaveFee = ptInfo;
            //            ptInfoSaveFee.InvoiceNo = "";
            //            string invoiceNo = accountManager.ExecSqlReturnOne("select SEQ_PTInvoiceNo.Nextval from dual", "");
            //            if (!string.IsNullOrEmpty(invoiceNo))
            //            {
            //                ptInfoSaveFee.InvoiceNo = invoiceNo.PadLeft(12, '0');
            //            }
            //            ptInfoSaveFee.TransType = TransTypes.Positive;
            //            ptInfoSaveFee.ValidState = EnumValidState.Valid;
            //            ptInfoSaveFee.Qty = OldQty - returnQty;
            //            ptInfoSaveFee.TotCost = ptInfoSaveFee.Qty * ptInfo.UnitPrice;
            //            ptInfoSaveFee.Unit = ptInfo.Unit;
            //            ptInfoSaveFee.UnitPrice = ptInfo.UnitPrice;
            //            ptInfoSaveFee.ItemCode = ptInfo.ItemCode;
            //            ptInfoSaveFee.ItemName = ptInfo.ItemName;
            //            ptInfoSaveFee.OldTicketNo = ptInfo.TicketNo;
            //            ptInfoSaveFee.OldInvoiceNo = ptInfo.InvoiceNo;
            //            ptInfoSaveFee.PayMode.ID = this.cmbPayMode.Tag.ToString();
            //            ptInfoSaveFee.PayMode.Name = this.cmbPayMode.Text;
            //            ptInfoSaveFee.TicketNo = this.txtParkingNo.Text;
            //            ptInfoSaveFee.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
            //            ptInfoSaveFee.OperEnvironment.OperTime = this.accountManager.GetDateTimeFromSysDateTime();
                
            //            //这里插入重收的收费记录
            //            if (accountManager.InsertParkingTicketInfo(ptInfoSaveFee) < 0)
            //            {
            //                FS.FrameWork.Management.PublicTrans.RollBack();
            //                MessageBox.Show("退费失败！重新收费失败！" + accountManager.Err);
            //                return;
            //            }
            //        }
                    
            //    }
            //    else
            //    {
            //        MessageBox.Show("请选择一条费用进行退费！");
            //        return;
            //    }
            //}

            //FS.FrameWork.Management.PublicTrans.Commit();
            //MessageBox.Show("退费成功！");
            //this.Clear();
            #endregion

        }
        /// <summary>
        /// 领取停车票
        /// </summary>
        public void SaveTicket()
        {
            if (string.IsNullOrEmpty(this.cmbParkingType.Tag.ToString()) || string.IsNullOrEmpty(this.cmbParkingType.Text))
            {
                MessageBox.Show("请选择停车票类型！");
                this.cmbParkingType.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.cmbOper.Tag.ToString()) || string.IsNullOrEmpty(this.cmbOper.Text))
            {
                MessageBox.Show("请选择领取人员！");
                this.cmbOper.Focus();
                return;
            }

            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!rex.IsMatch(this.txtQty.Text))
            {
                MessageBox.Show("请输入正确的数量！");
                this.txtQty.Text = "0";
                this.lblTotCost.Text = "0.00";
                return;
            }
            if (decimal.Parse(this.txtQty.Text) <= 0)
            {
                MessageBox.Show("请输入正确的数量！");
                this.txtQty.Text = "0";
                this.lblTotCost.Text = "0.00";
                this.txtQty.Focus();
                return;
            }
            FS.HISFC.Models.Base.Const obj = new Const();
            obj.ID = this.cmbParkingType.Tag.ToString();
            obj.Name = this.cmbParkingType.Text;
            FS.HISFC.Models.Base.Const cons = this.hsTicketTypeList[this.cmbParkingType.Tag.ToString()] as FS.HISFC.Models.Base.Const;
            if (cons != null)
            {
                obj.UserCode = cons.UserCode;
                obj.Memo = cons.Memo;
            }
            else
            {
                MessageBox.Show("获取项目类型出错！");
                return;
            }
            obj.SpellCode = this.txtQty.Text;//借用，领取数量
            obj.OperEnvironment.ID = this.cmbOper.Tag.ToString();
            obj.OperEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();
            obj.User01 = FS.FrameWork.Management.Connection.Operator.ID;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //update
            if (accountManager.UpdateTicketTotalQty(obj.OperEnvironment.ID, obj.ID, obj.SpellCode, obj.SpellCode) <= 0)
            {
                //insert
                if (accountManager.InsertTicketTotal(obj) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("领取停车票失败！请联系管理员" + accountManager.Err);
                    return;
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("领取成功！");
            this.Clear();


        }
        /// <summary>
        /// 回退停车票（用于领取错误时）
        /// </summary>
        public void ReturnTicket()
        {
            if (string.IsNullOrEmpty(this.cmbParkingType.Tag.ToString()) || string.IsNullOrEmpty(this.cmbParkingType.Text))
            {
                MessageBox.Show("请选择停车票类型！");
                this.cmbParkingType.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.cmbOper.Tag.ToString()) || string.IsNullOrEmpty(this.cmbOper.Text))
            {
                MessageBox.Show("请选择回退人员！");
                this.cmbOper.Focus();
                return;
            }
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!rex.IsMatch(this.txtQty.Text))
            {
                MessageBox.Show("请输入正确的数量！");
                this.txtQty.Text = "0";
                this.lblTotCost.Text = "0.00";
                return;
            }
            if (decimal.Parse(this.txtQty.Text) <= 0)
            {
                MessageBox.Show("请输入正确的数量！");
                this.txtQty.Text = "0";
                this.lblTotCost.Text = "0.00";
                this.txtQty.Focus();
                return;
            }

            if (this.QueryQty(this.cmbOper.Tag.ToString(), this.cmbParkingType.Tag.ToString()) < decimal.Parse(this.txtQty.Text))
            {
                MessageBox.Show("回退数量大于实际领取数量！");
                this.txtQty.SelectAll();
                return;
            }
            FS.HISFC.Models.Base.Const obj = new Const();
            obj.ID = this.cmbParkingType.Tag.ToString();
            obj.Name = this.cmbParkingType.Text;
            FS.HISFC.Models.Base.Const cons = this.hsTicketTypeList[this.cmbParkingType.Tag.ToString()] as FS.HISFC.Models.Base.Const;
            if (cons != null)
            {
                obj.UserCode = cons.UserCode;
                obj.Memo = cons.Memo;
            }
            else
            {
                MessageBox.Show("获取项目类型出错！");
                return;
            }
            obj.SpellCode = (-decimal.Parse(this.txtQty.Text)).ToString();//借用，领取数量
            obj.OperEnvironment.ID = this.cmbOper.Tag.ToString();
            obj.OperEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            accountManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //update
            if (accountManager.UpdateTicketTotalQty(obj.OperEnvironment.ID, obj.ID, obj.SpellCode, obj.SpellCode) <= 0)
            {
                //insert
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("回退停车票失败！请确认是否领取！" + accountManager.Err);
                return;
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("回退成功！");
            this.Clear();


        }
        /// <summary>
        /// 查询剩余停车票数量
        /// </summary>
        /// <param name="operCode"></param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public int QueryQty(string operCode,string itemCode)
        {
            string sql = @"select nvl(qty,0) qty from  FIN_COM_PARKINGTOTAL where oper_code = '{0}' and item_code = '{1}' ";
            sql = string.Format(sql, operCode, itemCode);
            int qty = 0;
            qty = int.Parse(this.accountManager.ExecSqlReturnOne(sql));
            if (qty <= 0)
            {
                qty = 0;
            }
            return qty;

        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.lblSpecs.Text = string.Empty;
            this.txtInvoiceNo.Text = string.Empty;
            this.txtMark.Text = string.Empty;
            this.txtParkingNo.Text = string.Empty;
            this.txtQty.Text = "0";
            this.cmbParkingType.Text = string.Empty;
            this.cmbParkingType.Tag = "";
            this.cmbPayMode.Tag = "CA";
            this.cmbParkingState.Tag = "ALL";
            //this.cmbOper.Text = FS.FrameWork.Management.Connection.Operator.Name;
            //this.cmbOper.Tag = FS.FrameWork.Management.Connection.Operator.ID;
            this.lblTotCost.Text = "0.00" + "元";
            this.neuSpread1_Sheet1.RowCount = 0;
            this.lblBalance.Text = "剩 0 张";

        }
        /// <summary>
        /// 
        /// </summary>
        private void Query()
        {
            if (this.dtBegTime.Value > this.dtEndTime.Value)
            {
                MessageBox.Show("开始时间不能大于结束时间");
                return;
            }
            ArrayList al = new ArrayList();
            string itemCode = this.cmbParkingType.Tag.ToString();
            string ticketNo = this.txtParkingNo.Text;
            string memo = this.txtMark.Text;
            string ticketState = this.cmbParkingState.Tag.ToString();
            string operCode = this.cmbOper.Tag.ToString();
            string invoiceNo = this.txtInvoiceNo.Text;
            al = this.accountManager.QueryParkingTicketInfo(itemCode,ticketNo,memo,ticketState,operCode,invoiceNo,this.dtBegTime.Value.ToString(),this.dtEndTime.Value.ToString());
            if (al.Count <= 0 || al == null)
            {
                MessageBox.Show("没有查询到记录！");
                this.neuSpread1_Sheet1.RowCount = 0;
                return;
            }
            this.SetFP(al);

        }
        /// <summary>
        /// 设置表格
        /// </summary>
        private void SetFP(ArrayList  al)
        {
            if (al == null)
            {
                return;
            }
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet1.RowCount = al.Count;
            int count = 0;
            foreach (FS.HISFC.Models.Account.ParkingTicketFeeInfo ptInfoSaveFee in al)
            {
                this.neuSpread1_Sheet1.Cells[count, 0].Text = ptInfoSaveFee.InvoiceNo;
                string ZFStr = "";
                if (ptInfoSaveFee.TransType == TransTypes.Positive)
                {
                    ZFStr = "收取";
                }
                else
                {
                    ZFStr = "退费";
                }
                this.neuSpread1_Sheet1.Cells[count, 1].Text = ZFStr;
                this.neuSpread1_Sheet1.Cells[count, 2].Text = ptInfoSaveFee.ItemName;
                this.neuSpread1_Sheet1.Cells[count, 3].Text = ptInfoSaveFee.UnitPrice + "元" + "/" + ptInfoSaveFee.Unit;
                this.neuSpread1_Sheet1.Cells[count, 4].Text = ptInfoSaveFee.Qty.ToString();
                this.neuSpread1_Sheet1.Cells[count, 5].Text = ptInfoSaveFee.TotCost.ToString("F2");
                this.neuSpread1_Sheet1.Cells[count, 6].Text = ptInfoSaveFee.PayMode.Name;
                this.neuSpread1_Sheet1.Cells[count, 7].Text = ptInfoSaveFee.TicketNo;
                this.neuSpread1_Sheet1.Cells[count, 8].Text = ptInfoSaveFee.OldTicketNo;
                this.neuSpread1_Sheet1.Cells[count, 9].Text = ptInfoSaveFee.OldInvoiceNo;
                string validState = string.Empty;
                if (ptInfoSaveFee.ValidState == EnumValidState.Valid)
                {
                    validState = "有效";
                }
                else if (ptInfoSaveFee.ValidState == EnumValidState.Invalid)
                {
                    validState = "无效";
                }
                this.neuSpread1_Sheet1.Cells[count, 10].Text = validState;
                this.neuSpread1_Sheet1.Cells[count, 11].Text = ptInfoSaveFee.Memo;
                this.neuSpread1_Sheet1.Cells[count, 12].Text = ptInfoSaveFee.OperEnvironment.Name;
                this.neuSpread1_Sheet1.Cells[count, 13].Text = ptInfoSaveFee.OperEnvironment.OperTime.ToString();
                string balanceStr = "";
                if (ptInfoSaveFee.IsBalance)
                {
                    balanceStr = "已日结";
                }
                else
                {
                    balanceStr = "未日结";
                }

                this.neuSpread1_Sheet1.Cells[count, 14].Text = balanceStr;
                this.neuSpread1_Sheet1.Cells[count, 15].Text = ptInfoSaveFee.BalanceNo;
                this.neuSpread1_Sheet1.Cells[count, 16].Text = ptInfoSaveFee.BalanceEnvironment.Name;
                this.neuSpread1_Sheet1.Cells[count, 17].Text = ptInfoSaveFee.BalanceEnvironment.OperTime.ToString();
                string checkStr = "";
                if (ptInfoSaveFee.IsCheck)
                {
                    checkStr = "已审核";
                }
                else
                {
                    checkStr = "未审核";
                }

                this.neuSpread1_Sheet1.Cells[count, 18].Text = checkStr;
                this.neuSpread1_Sheet1.Cells[count, 19].Text = ptInfoSaveFee.CheckEnvironment.Name;
                this.neuSpread1_Sheet1.Cells[count, 20].Text = ptInfoSaveFee.CheckEnvironment.OperTime.ToString();
                if (ptInfoSaveFee.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    neuSpread1_Sheet1.Rows[count].ForeColor = Color.Red;
                }
                this.neuSpread1_Sheet1.Rows[count].Tag = ptInfoSaveFee;
                count++;
            }

        }
        private void ckSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.gbQueryCon.Visible = this.ckSelect.Checked;

        }


        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {

            if (this.neuSpread1.ActiveSheet != this.neuSpread1_Sheet1)
            {
                return;
            }

            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1 && this.neuSpread1_Sheet1.ActiveRow == null)
            {
                return;
            }
            if (e.ColumnHeader || this.neuSpread1_Sheet1.RowCount == 0) return;
            int row = e.Row;
            if (this.neuSpread1_Sheet1.RowCount > 0)
            {
                if (this.neuSpread1_Sheet1.Rows[row].Tag != null)
                {
                }
            }
        }

        private void cmbParkingType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FS.HISFC.Models.Base.Const cons = this.cmbParkingType.Tag as FS.HISFC.Models.Base.Const;
            FS.HISFC.Models.Base.Const cons = hsTicketTypeList[this.cmbParkingType.Tag.ToString()] as FS.HISFC.Models.Base.Const;
            if (cons != null)
            {
                this.lblTotCost.Text = (decimal.Parse(this.txtQty.Text) * decimal.Parse(cons.UserCode)).ToString("F2") +"元";
                this.lblSpecs.Text = cons.UserCode + "元" + "/" + cons.Memo;
            }
            else
            {
                this.lblSpecs.Text = "";
                this.lblTotCost.Text = "0.00";
            }
            if (!string.IsNullOrEmpty(this.cmbParkingType.Tag.ToString()))
            {
                if (cons != null)
                {
                    this.lblTotCost.Text = (decimal.Parse(this.txtQty.Text) * decimal.Parse(cons.UserCode)).ToString("F2") + "元";
                }
                else
                {
                    this.lblTotCost.Text = "0.00" + "元";
                }

            }
            else
            {
                this.lblTotCost.Text = "0.00" + "元";
            }
            this.lblBalance.Text = "剩 "+this.QueryQty(this.cmbOper.Tag.ToString(), this.cmbParkingType.Tag.ToString())+" 张";

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            System.Text.RegularExpressions.Regex rex = new System.Text.RegularExpressions.Regex(@"^\d+$");
            if (!string.IsNullOrEmpty(this.txtQty.Text))
            {
                if (!rex.IsMatch(this.txtQty.Text))
                {
                    MessageBox.Show("请输入正确的数量！");
                    this.txtQty.Text = "0";
                    this.lblTotCost.Text = "0.00";
                    return;
                }
            }
            else
            {
                return;
            }
            if (!string.IsNullOrEmpty(this.cmbParkingType.Tag.ToString()))
            {
                FS.HISFC.Models.Base.Const cons = this.hsTicketTypeList[this.cmbParkingType.Tag.ToString()] as FS.HISFC.Models.Base.Const;
                if (cons != null)
                {
                    this.lblTotCost.Text = (decimal.Parse(this.txtQty.Text) * decimal.Parse(cons.UserCode)).ToString("F2") + "元";
                }
                else
                {
                    this.lblTotCost.Text = "0.00" + "元";
                }

            }
            else
            {
                this.lblTotCost.Text = "0.00" + "元";
            }
        }

        private void cmbOper_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.lblBalance.Text = "剩 " + this.QueryQty(this.cmbOper.Tag.ToString(), this.cmbParkingType.Tag.ToString()) + " 张";

        }




    }
}

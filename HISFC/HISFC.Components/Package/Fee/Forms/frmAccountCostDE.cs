using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace HISFC.Components.Package.Fee.Forms
{


    public partial class frmAccountCostDE : Form
    {
        #region 属性

        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                this.RefreshPayMode();
            }
        }

        /// <summary>
        /// 设置支付方式
        /// </summary>
        public event DelegateHashtableSet SetPayModeRes;

        #endregion

        /// <summary>
        /// 账户支付分配界面
        /// </summary>
        public frmAccountCostDE()
        {
            InitializeComponent();
            this.addEvents();
            //FP热键屏蔽
            InputMap im;
            im = this.FpAccount.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        private void addEvents()
        {
            this.FpAccount.CellClick += new CellClickEventHandler(FpAccount_CellClick);
            this.FpAccount.Leave += new EventHandler(FpAccount_Leave);
            this.FpAccount.EditModeOn += new EventHandler(FpAccount_EditModeOn);
            this.FpAccount.Change += new ChangeEventHandler(FpAccount_Change);
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void delEvents()
        {
            this.FpAccount.Change -= new ChangeEventHandler(FpAccount_Change);
            this.FpAccount.EditModeOn -= new EventHandler(FpAccount_EditModeOn);
            this.FpAccount.Leave -= new EventHandler(FpAccount_Leave);
            this.FpAccount.CellClick -= new CellClickEventHandler(FpAccount_CellClick);
            this.btnOK.Click -= new EventHandler(btnOK_Click);
            this.btnCancel.Click -= new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// 账户列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpAccount_CellClick(object sender, CellClickEventArgs e)
        {
            this.FpAccount_Sheet1.SetActiveCell(e.Row, (int)AccountModeCols.TotCost);
        }

        /// <summary>
        /// 焦点离开账户列表事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpAccount_Leave(object sender, EventArgs e)
        {
            this.FpAccount.StopCellEditing();
        }

        /// <summary>
        /// 记录修改前的值
        /// </summary>
        private decimal previousTot = 0.0m;
        private decimal previousAccount = 0.0m;
        private decimal previousGift = 0.0m;

        /// <summary>
        /// 编辑模式开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpAccount_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                previousTot = Decimal.Parse(this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.ActiveRowIndex, (int)AccountModeCols.TotCost].Value.ToString());
                previousAccount = Decimal.Parse(this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.ActiveRowIndex, (int)AccountModeCols.AccountCost].Value.ToString());
                previousGift = Decimal.Parse(this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.ActiveRowIndex, (int)AccountModeCols.GiftCost].Value.ToString());
                this.FpAccount.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 当前编辑控件的按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// 金额发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpAccount_Change(object sender, ChangeEventArgs e)
        {
            this.delEvents();
            try
            {
                //输入金额
                decimal cost = 0.0m;

                if (this.FpAccount_Sheet1.Cells[e.Row, e.Column].Value == null)
                {
                    this.FpAccount_Sheet1.Cells[e.Row, e.Column].Value = 0;
                }

                if (this.FpAccount_Sheet1.Cells[e.Row, e.Column].Value != null)
                {
                    cost = Decimal.Parse(this.FpAccount_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                }

                if (cost < 0)
                {
                    throw new Exception("消费金额不能小于零！");
                }

                FS.HISFC.Models.Account.AccountDetail account = this.FpAccount_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Account.AccountDetail;

                decimal totAmount = account.BaseVacancy + account.DonateVacancy;
                decimal acountAmount = account.BaseVacancy;
                decimal giftAmount = account.DonateVacancy;

                ///输入账户消费总金额
                if (e.Column == (int)AccountModeCols.TotCost)
                {
                    if (cost <= totAmount && totAmount >= 0)
                    {
                        if (totAmount != 0)
                        {
                            decimal accountcost = 0.0m;
                            decimal giftcost = 0.0m;
                            accountcost = Math.Round((acountAmount * cost) / totAmount, 2);
                            giftcost = cost - accountcost;
                            this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.AccountCost].Value = accountcost;
                            this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.GiftCost].Value = giftcost;
                        }
                    }
                    else
                    {
                        throw new Exception("账户余额不足！");
                    }
                }
                else if (e.Column == (int)AccountModeCols.AccountCost)//调整账户金额
                {
                    decimal currentGift = 0.0m;
                    if (this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.GiftCost].Value != null)
                    {
                        currentGift = Decimal.Parse(this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.GiftCost].Value.ToString());
                    }

                    if (cost > acountAmount)
                    {
                        throw new Exception("当前基本账户余额不足！");
                    }

                    this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.TotCost].Value = currentGift + cost;

                }
                else if (e.Column == (int)AccountModeCols.GiftCost)
                {
                    decimal currentAccount = 0.0m;

                    if (this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.AccountCost].Value != null)
                    {
                        currentAccount = Decimal.Parse(this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.AccountCost].Value.ToString());
                    }

                    if (cost > giftAmount)
                    {
                        throw new Exception("当前赠送账户余额不足！");
                    }

                    this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.TotCost].Value = currentAccount + cost;
                }
            }
            catch(Exception ex)
            {
                this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.TotCost].Value = previousTot;
                this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.AccountCost].Value = previousAccount;
                this.FpAccount_Sheet1.Cells[e.Row, (int)AccountModeCols.GiftCost].Value = previousGift;
                MessageBox.Show(ex.Message);
            }
            this.addEvents();
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.patientInfo == null)
            {
                return;
            }

            this.tbMedcineNO.Text = this.patientInfo.PID.CardNO;
            this.tbName.Text = this.patientInfo.Name;
        }

        /// <summary>
        /// 设置账户列表
        /// </summary>
        private void RefreshPayMode()
        {
            this.delEvents();

            this.FpAccount_Sheet1.RowCount = 0;
            if (this.patientInfo == null)
            {
                return;
            }

            FS.HISFC.Models.Account.Account account = this.account.GetAccountByCardNoEX(this.patientInfo.PID.CardNO);
            List<FS.HISFC.Models.Account.AccountDetail> accounts = this.account.GetAccountDetail(account.ID, "ALL","1");

            if (accounts == null)
            {
                return;
            }

            //增加账户支付方式
            foreach (FS.HISFC.Models.Account.AccountDetail obj in accounts)
            {
                this.FpAccount_Sheet1.Rows.Add(this.FpAccount_Sheet1.RowCount, 1);
                this.FpAccount_Sheet1.Rows[this.FpAccount_Sheet1.RowCount - 1].Tag = obj;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.AccountType].Value = obj.AccountType.Name;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.TotCost].Value = 0.0m;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.AccountCost].Value = 0.0m;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.GiftCost].Value = 0.0m;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.AccountVacancy].Value = obj.BaseVacancy;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.GiftVacancy].Value = obj.DonateVacancy;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.AccountType].Locked = true;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.AccountVacancy].Locked = true;
                this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.RowCount - 1, (int)AccountModeCols.GiftVacancy].Locked = true;
            }

            this.addEvents();
        }

        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <returns></returns>
        public Hashtable GetDepositInfo()
        {
            ArrayList YSmodeList = new ArrayList();
            ArrayList DCmodeList = new ArrayList();
            foreach (Row row in this.FpAccount_Sheet1.Rows)
            {
                if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value != null &&
                    Double.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value.ToString()) > 0)
                {
                    if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value != null &&
                           (decimal)this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value > 0)
                    {
                        FS.HISFC.Models.Account.AccountDetail accountDetail = this.FpAccount_Sheet1.Rows[row.Index].Tag as FS.HISFC.Models.Account.AccountDetail;
                        //基本账户
                        if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value != null &&
                            Double.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value.ToString()) > 0)
                        {
                            FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = new FS.HISFC.Models.MedicalPackage.Fee.Deposit();
                            deposit.CardNO = this.patientInfo.PID.CardNO;
                            deposit.Trans_Type = "1";
                            deposit.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.JYJ;
                            deposit.Account = accountDetail.ID;
                            deposit.AccountType = accountDetail.AccountType.ID;
                            deposit.Mode_Code = "YS";
                            deposit.AccountFlag = "0";    //账户标识 0-基本账户，1-赠送账户
                            deposit.Cancel_Flag = "0";
                            deposit.Trans_Type = "1";
                            deposit.Amount = Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value.ToString());
                            if (deposit.Amount > 0)
                            {
                                YSmodeList.Add(deposit);
                            }
                        }

                        //赠送账户
                        if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value != null &&
                            Double.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value.ToString()) > 0)
                        {
                            FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = new FS.HISFC.Models.MedicalPackage.Fee.Deposit();
                            deposit.CardNO = this.patientInfo.PID.CardNO;
                            deposit.Trans_Type = "1";
                            deposit.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.JYJ;
                            deposit.Account = accountDetail.ID;
                            deposit.AccountType = accountDetail.AccountType.ID;
                            deposit.Mode_Code = "DC";
                            deposit.AccountFlag = "1";    //账户标识 0-基本账户，1-赠送账户
                            deposit.Cancel_Flag = "0";
                            deposit.Trans_Type = "1";
                            deposit.Amount = Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value.ToString());
                            if (deposit.Amount > 0)
                            {
                                DCmodeList.Add(deposit);
                            }
                        }
                    }
                }
            }

            Hashtable hsPayMode = new Hashtable();
            hsPayMode.Add("YS", YSmodeList);
            hsPayMode.Add("DC", DCmodeList);

            return hsPayMode;
        }

        public int SetPayInfo(ArrayList payArray,ref string ErrInfo)
        {
            if (payArray == null)
            {
                ErrInfo = "设置账户押金记录失败:数组为空";
                return -1;
            }

            try
            {
                this.delEvents();
                foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit payMode in payArray)
                {
                    bool isSetted = false;
                    foreach (Row row in this.FpAccount_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Account.AccountDetail obj = row.Tag as FS.HISFC.Models.Account.AccountDetail;
                        if (payMode.Account == obj.ID && payMode.AccountType == obj.AccountType.ID)
                        {
                            isSetted = true;
                            if (payMode.AccountFlag == "0")
                            {
                                this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value = payMode.Amount;
                            }
                            else
                            {
                                this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value = payMode.Amount;
                            }

                            this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value =
                                Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value.ToString()) +
                                Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value.ToString());
                            break;
                        }
                    }

                    if (!isSetted)
                    {
                        throw new Exception("设置账户押金记录失败：未找到相应的支付记录！");
                    }
                }
            }
            catch(Exception ex)
            {
                ErrInfo = "设置账户押金记录失败:" + ex.Message;
                this.addEvents();
                return -1;
            }

            this.addEvents();
            return 1;
        }

        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SetPayModeRes != null)
            {
                if (SetPayModeRes(this.GetDepositInfo(),0) < 0)
                {
                    MessageBox.Show("获取押金信息处错！");
                    return;
                }
            }

            this.Close();
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (this.FpAccount.ContainsFocus)
                {
                    if (keyData == Keys.Enter)
                    {
                        this.PutArrow(keyData);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Focus();
                this.FpAccount.Focus();
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// 方向按键
        /// </summary>
        /// <param name="key">当前的按键</param>
        private void PutArrow(Keys key)
        {
            int currRow = this.FpAccount_Sheet1.ActiveRowIndex;
            if (key == Keys.Enter)
            {
                if (this.FpAccount_Sheet1.ActiveRowIndex < this.FpAccount_Sheet1.RowCount - 1)
                {
                    this.FpAccount_Sheet1.SetActiveCell(currRow + 1, (int)AccountModeCols.TotCost);
                }
                else
                {
                    this.FpAccount.StopCellEditing();
                }
            }
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum AccountModeCols
        {
            /// <summary>
            /// 账户类型
            /// </summary>
            AccountType = 0,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 1,

            /// <summary>
            /// 账户支付
            /// </summary>
            AccountCost = 2,

            /// <summary>
            /// 赠送支付
            /// </summary>
            GiftCost = 3,

            /// <summary>
            /// 账户余额
            /// </summary>
            AccountVacancy = 4,

            /// <summary>
            /// 赠送余额
            /// </summary>
            GiftVacancy = 5
        }
    }
}

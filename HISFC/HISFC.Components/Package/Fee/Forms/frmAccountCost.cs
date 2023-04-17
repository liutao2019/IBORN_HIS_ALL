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

    public delegate int DelegateHashtableSet(Hashtable hsTable,decimal totCost);

    public partial class frmAccountCost : Form
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
        /// 费用管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Package pckMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();

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

        private bool isEmpower = false;

        /// <summary>
        /// {892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
        /// 是否是代付
        /// </summary>
        public bool IsEmpower
        {
            get { return isEmpower; }
            set
            {
                isEmpower = value;
                this.tbMedcineNO.ReadOnly = !isEmpower;
                this.btnCard.Visible = isEmpower;
            }
        }

        private string originalCardNO = string.Empty;

        /// <summary>
        /// {892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
        /// 代付时原患者卡号
        /// </summary>
        public string OriginalCardNO
        {
            get { return originalCardNO; }
            set
            {
                originalCardNO = value;

                this.RefreshFamily();
            }
        }


        private string originalName = string.Empty;

        /// <summary>
        /// {6ff15804-8b10-4f19-855a-e4c1d97e3714}
        /// 代付时原患者姓名
        /// </summary>
        public string OriginalName
        {
            get { return originalName; }
            set
            {
                originalName = value;
            }
        }


        /// <summary>
        /// 能够分配的金额
        /// </summary>
        private decimal deliverableCost = 0.0m;

        /// <summary>
        /// 能够分配的金额
        /// </summary>
        public decimal DeliverableCost
        {
            set
            {
                deliverableCost = value;
                this.tbMoeny.Text = deliverableCost.ToString("F2");
            }
        }

        /// <summary>
        /// 设置支付方式
        /// </summary>
        public event DelegateHashtableSet SetPayModeRes;

        /// <summary>
        /// 套餐集合
        /// </summary>
        protected ArrayList alPackageLists = new ArrayList();

        /// <summary>
        /// 套餐集合
        /// </summary>
        public System.Collections.ArrayList PackageLists
        {
            set
            {
                this.alPackageLists = value;
            }
            get
            {
                return this.alPackageLists;
            }
        }

        #endregion

        /// <summary>
        /// 账户支付分配界面
        /// </summary>
        public frmAccountCost()
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
            this.tbMedcineNO.KeyDown += new KeyEventHandler(tbMedcineNO_KeyDown);
            //{892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
            this.btnCard.Click += new EventHandler(btnCard_Click);
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
            this.tbMedcineNO.KeyDown -= new KeyEventHandler(tbMedcineNO_KeyDown);
            //{892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
            this.btnCard.Click -= new EventHandler(btnCard_Click);
        }

        /// <summary>
        /// {892FDDD4-0CD2-4306-87E5-ACDEF6829C76}
        /// 刷卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCard_Click(object sender, EventArgs e)
        {
            string cardNo = "";
            string error = "";
            if (FS.HISFC.Components.Registration.Function.OperMCard(ref cardNo, ref error) == -1)
            {
                MessageBox.Show("读卡失败：" + error, "提示");
                return;
            }
            cardNo = ";" + cardNo;
            this.tbMedcineNO.Text = cardNo;
            this.tbMedcineNO_KeyDown(this.tbMedcineNO, new KeyEventArgs(Keys.Enter));
        }

        /// <summary>
        /// 绑定家庭成员
        /// </summary>
        private void RefreshFamily()
        {
            if (this.IsEmpower)
            {
                this.labfamily.Visible = true;
                this.cmbFamily.Visible = true;
                ArrayList familyList = pckMgr.QueryFamlilyMember(this.OriginalCardNO, this.OriginalName);
                cmbFamily.AddItems(familyList);
            }
            else
            {
                this.labfamily.Visible = false;
                this.cmbFamily.Visible = false;
            }
           
        }


        /// <summary>
        /// 按照病历号查询患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbMedcineNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                GetInfo();
            }
        }


        private void GetInfo()
        {
            string cardNO = string.Empty;
            string queryStr = this.tbMedcineNO.Text.Trim();
            if (string.IsNullOrEmpty(queryStr))
            {
                MessageBox.Show("请输入卡号、病历号、预约号进行检索！");
                return;
            }

            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int rev = this.feeMgr.ValidMarkNO(queryStr, ref accountCard);
            if (rev > 0)
            {
                FS.HISFC.Models.RADT.PatientInfo patient = account.GetPatientInfoByCardNO(accountCard.Patient.PID.CardNO);
                if (patient == null || string.IsNullOrEmpty(patient.PID.CardNO))
                {
                    MessageBox.Show("未找到患者信息！");
                    return;
                }

                if (this.IsEmpower)
                {
                    if (string.IsNullOrEmpty(OriginalCardNO))
                    {
                        MessageBox.Show("被代付患者卡号获取失败！");
                        return;
                    }
                    else
                    {
                        if (OriginalCardNO == patient.PID.CardNO)
                        {
                            MessageBox.Show("不能自己给自己代付，请用其他账户！");
                            return;
                        }
                    }
                }

                this.PatientInfo = patient;
            }
            else
            {
                MessageBox.Show("未找到患者信息！");
                return;
            }
        
        
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

                            if (this.CountCostInfo() < 0)
                            {
                                throw new Exception("输入金额大于应缴纳金额！");
                            }
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

                    if (this.CountCostInfo() < 0)
                    {
                        throw new Exception("输入金额大于应缴纳金额！");
                    }
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

                    if (this.CountCostInfo() < 0)
                    {
                        throw new Exception("输入金额大于应缴纳金额！");
                    }
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
        /// 计算已分配总金额
        /// </summary>
        /// <returns></returns>
        private decimal CountCostInfo()
        {
            decimal totCost = 0.0m;

            foreach (Row row in this.FpAccount_Sheet1.Rows)
            {
                if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value != null)
                {
                    totCost += Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value.ToString());
                }
            }

            if (this.deliverableCost > totCost)
            {
                this.lbCost.Text = "￥" + totCost.ToString("F2");
            }

            return this.deliverableCost - totCost;
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

            //{119F302E-69D9-445c-BF56-4109D975AD98}
            if (account == null || string.IsNullOrEmpty(account.ID))
            {
                return;
            }

            List<FS.HISFC.Models.Account.AccountDetail> accounts = this.account.GetAccountDetail(account.ID, "ALL","1");

            if (accounts == null)
            {
                return;
            }

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

            //增加账户支付方式
            foreach (FS.HISFC.Models.Account.AccountDetail obj in accounts)
            {
                //账户类型
                //{ECECDF2F-BA74-4615-A240-C442BE0A0074}
                ArrayList alAccountType = managerIntegrate.GetAccountTypeList(obj.AccountType.ID);

                if (alAccountType == null || alAccountType.Count == 0)
                {
                    continue;
                }

                bool isShow = true;

                string packageType = (alAccountType[0] as FS.HISFC.Models.Base.Const).Memo;

                if (packageType != "ALL")
                {
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Package cur in this.PackageLists)
                    {
                        if (!packageType.Contains(cur.PackageInfo.PackageType))
                        {
                            isShow = false;
                            break;
                        }
                    }
                }

                if (!isShow)
                {
                    continue;
                }


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
        public Hashtable GetPayModeInfo()
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
                            FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                            payMode.Account = accountDetail.ID;
                            payMode.AccountType = accountDetail.AccountType.ID;
                            payMode.Mode_Code = "YS";
                            payMode.AccountFlag = "0";    //账户标识 0-基本账户，1-赠送账户
                            payMode.Cancel_Flag = "0";
                            payMode.Trans_Type = "1";
                            payMode.Tot_cost = payMode.Real_Cost = (decimal)this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value;
                            if (payMode.Tot_cost > 0)
                            {
                                YSmodeList.Add(payMode);
                            }
                        }

                        //赠送账户
                        if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value != null &&
                            Double.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value.ToString()) > 0)
                        {
                            FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode = new FS.HISFC.Models.MedicalPackage.Fee.PayMode();
                            payMode.Account = accountDetail.ID;
                            payMode.AccountType = accountDetail.AccountType.ID;
                            payMode.Mode_Code = "DC";
                            payMode.AccountFlag = "1";    //账户标识 0-基本账户，1-赠送账户
                            payMode.Cancel_Flag = "0";
                            payMode.Trans_Type = "1";
                            payMode.Tot_cost = payMode.Real_Cost = (decimal)this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value;
                            if (payMode.Tot_cost > 0)
                            {
                                DCmodeList.Add(payMode);
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
                ErrInfo = "设置账户支付记录失败:数组为空";
                return -1;
            }

            try
            {
                this.delEvents();
                decimal costs = 0.0m;
                foreach (FS.HISFC.Models.MedicalPackage.Fee.PayMode payMode in payArray)
                {
                    bool isSetted = false;
                    foreach (Row row in this.FpAccount_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Account.AccountDetail obj = row.Tag as FS.HISFC.Models.Account.AccountDetail;
                        if (payMode.Account == obj.ID && payMode.AccountType == obj.AccountType.ID)
                        {
                            costs += payMode.Tot_cost;
                            isSetted = true;
                            if (payMode.AccountFlag == "0")
                            {
                                this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value = payMode.Tot_cost;
                            }
                            else
                            {
                                this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value = payMode.Tot_cost;
                            }

                            this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.TotCost].Value =
                                Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.AccountCost].Value.ToString()) +
                                Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GiftCost].Value.ToString());
                            break;
                        }
                    }

                    if (!isSetted)
                    {
                        throw new Exception("设置账户支付记录失败：未找到相应的支付记录！");
                    }
                }

                this.lbCost.Text = "￥" + costs.ToString("F2");
            }
            catch(Exception ex)
            {
                ErrInfo = "设置账户支付记录失败:" + ex.Message;
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
                if (!feeMgr.CheckAccountPassWord(this.PatientInfo))
                {
                    MessageBox.Show("密码错误！");
                    return;
                }

                if (SetPayModeRes(this.GetPayModeInfo(),this.deliverableCost) < 0)
                {
                    MessageBox.Show("获取支付信息处错！");
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

        /// <summary>
        /// {6ff15804-8b10-4f19-855a-e4c1d97e3714}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFamily_SelectedValueChanged(object sender, EventArgs e)
        {
            string strno = cmbFamily.Tag.ToString().Trim();
            this.tbMedcineNO.Text = strno;
            GetInfo();
        }
    }
}

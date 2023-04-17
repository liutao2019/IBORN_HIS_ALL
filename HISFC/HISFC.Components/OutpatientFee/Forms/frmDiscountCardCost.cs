using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Models;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.HISFC.Components.OutpatientFee.Forms
{

    public partial class frmDiscountCardCost: Form
    {
        #region 属性


        /// <summary>
        /// 费用管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeMgr = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 当前患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 控制参数管理
        /// </summary>
        FS.FrameWork.Management.ControlParam contrlManager = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 卡卷业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PatientDiscountCardLogic patientCardLogic = new FS.HISFC.BizLogic.Fee.PatientDiscountCardLogic();

        /// <summary>
        /// 人员业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 当前患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
               
                this.RefreshPayMode();
            }
        }
        /// <summary>
        /// 是否可以根据名字搜索// {D55B4DFA-DA91-42b0-8163-27036100E89E}
        /// </summary>
        private bool isFindForName = false;

        /// <summary>
        /// 结算患者
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo selftPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        /// <summary>
        /// 结算患者
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo SelftPatientInfo
        {
            get
            {
                return selftPatientInfo;
            }
            set
            {
                selftPatientInfo = value;
               
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

        private bool isEmpower = false;

        /// <summary>
        /// 是否是代付
        /// </summary>
        public bool IsEmpower
        {
            get
            {
                return isEmpower;
            }

            set
            {
                this.isEmpower = value;
            }
        }

        /// <summary>
        /// 费用明细集合
        /// </summary>
        protected ArrayList alFeeDetails = new ArrayList();

        /// <summary>
        /// 门诊费用明细集合
        /// </summary>
        public System.Collections.ArrayList FeeDetails
        {
            set
            {
                this.alFeeDetails = value;
            }
            get
            {
                return this.alFeeDetails;
            }
        }

        /// <summary>
        /// 人员数组
        /// </summary>
        ArrayList personList = new ArrayList();
        #endregion

        /// <summary>
        /// 账户支付分配界面
        /// </summary>
        public frmDiscountCardCost()
        {
            InitializeComponent();

            this.addEvents();

            this.cmbCardKind.AddItems((new FS.HISFC.BizLogic.Manager.Constant().GetList("GetCardKind")));
            this.cmbCardKind.SelectedIndex = 0;
            this.personList = this.managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.F);

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
            this.FpAccount_Sheet1.SetActiveCell(e.Row, (int)AccountModeCols.CardCost);
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
        private decimal previousDiscount = 0.0m;

        /// <summary>
        /// 编辑模式开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpAccount_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                previousDiscount = Decimal.Parse(this.FpAccount_Sheet1.Cells[this.FpAccount_Sheet1.ActiveRowIndex, (int)AccountModeCols.CardCost].Value.ToString());
                
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
            this.CountCostInfo();
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
                if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardCost].Value != null)
                {
                    totCost += Decimal.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardCost].Value.ToString());
                }
            }

            if (this.deliverableCost > totCost)
            {
                this.lbCost.Text = "￥" + totCost.ToString("F2");
            }
            return this.deliverableCost - totCost;
        }


        /// <summary>
        /// 设置账户列表
        /// </summary>
        private void RefreshPayMode()
        {
            this.delEvents();

            this.FpAccount_Sheet1.RowCount = 0;

            this.addEvents();
        }

        /// <summary>
        /// 获取支付信息
        /// </summary>
        /// <returns></returns>
        public Hashtable GetPayModeInfo()
        {
            List<BalancePay> DSmodeList = new List<BalancePay>();

            foreach (Row row in this.FpAccount_Sheet1.Rows)
            {
                if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardCost].Value != null &&
                    Double.Parse(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardCost].Value.ToString()) > 0)
                {
                    BalancePay balancePay = new BalancePay();

                    balancePay.PayType.ID = "DS";
                    balancePay.PayType.Name = "购物卡";
                    if (this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.GetName].Value.ToString() != this.PatientInfo.Name)
                    {
                        this.isEmpower = true;
                    }
                    else
                    {
                        this.isEmpower = false;
                    }
                    balancePay.IsEmpPay = this.isEmpower;
                    balancePay.FT.TotCost = NConvert.ToDecimal(this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardCost].Value);
                    balancePay.FT.RealCost = balancePay.FT.TotCost;

                    if (balancePay.FT.TotCost > 0)
                    {
                        DSmodeList.Add(balancePay);
                    }
                }
                
            }

            Hashtable hsPayMode = new Hashtable();
            hsPayMode.Add("DS", DSmodeList);   //购物卡支付

            return hsPayMode;
        }

        /// <summary>
        /// 更新购物卡使用信息
        /// </summary>
        public int UpdatePatientCard()
        {
            FS.HISFC.Models.Fee.PatientDiscountCard patientCard = new FS.HISFC.Models.Fee.PatientDiscountCard();

            foreach (Row row in this.FpAccount_Sheet1.Rows)
            {
                patientCard.CardNo = this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardNO].Value.ToString();
                patientCard.CardKind = this.FpAccount_Sheet1.Cells[row.Index, (int)AccountModeCols.CardKind].Value.ToString();
                patientCard.UsedName = this.PatientInfo.Name;
                patientCard.UsedCardNo = this.PatientInfo.PID.CardNO;
                patientCard.UsedPhone = this.PatientInfo.PhoneHome;
                patientCard.UsedTime = System.DateTime.Now;
                patientCard.UsedState = "1";
                patientCard.UsedOper = this.patientCardLogic.Operator.ID;

                if (this.patientCardLogic.UpdatePatientCardByCardKindAndNo(patientCard) < 0)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 取得当前人员姓名
        /// </summary>
        /// <param name="personCode"></param>
        /// <returns></returns>
        protected string GetPersonName(string personCode)
        {
            string PersonName = string.Empty;
            if (personList != null)
            {
                foreach (FS.HISFC.Models.Base.Employee p in personList)
                {
                    if (p.ID == personCode)
                    {
                        PersonName = p.Name;
                        break;
                    }
                }
            }
            return PersonName;
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
                if (this.UpdatePatientCard() < 0)
                {
                    MessageBox.Show("更新购物卡使用信息失败！");
                    return;
                }

                if (SetPayModeRes(this.GetPayModeInfo(), this.deliverableCost) < 0)
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
                    this.FpAccount_Sheet1.SetActiveCell(currRow + 1, (int)AccountModeCols.CardCost);
                }
                else
                {
                    this.FpAccount.StopCellEditing();
                }
            }
        }

        private void txtCardNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (this.cmbCardKind.SelectedItem == null)
                {
                    MessageBox.Show("请选择卡类型！");
                }
                if (string.IsNullOrEmpty(this.txtCardNO.Text))
                {
                    MessageBox.Show("请输入卡号！");
                }
                string cardKind = this.cmbCardKind.SelectedItem.ID;
                string cardNO = this.txtCardNO.Text;

                ArrayList PatientCards = new ArrayList();
                PatientCards = this.patientCardLogic.QueryPatientCardByCardKindAndNO(cardNO, cardKind);
                if (PatientCards != null && PatientCards.Count > 0)
                {
                    //this.FpAccount_Sheet1.Rows.Count = 0;

                    foreach (FS.HISFC.Models.Fee.PatientDiscountCard info in PatientCards)
                    {

                        this.FpAccount_Sheet1.Rows.Add(0, 1);
                        this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.CardKind].Value = info.CardKind;
                        this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.CardName].Value = info.CardName;
                        this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.CardNO].Value = info.CardNo;
                        this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.GetName].Value = info.GetName;
                        this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.GetDate].Value = info.GetTime;
                        if (info.CardKind == "DW")
                        {
                            if (deliverableCost < 1000)
                            {
                                this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.CardCost].Value = deliverableCost.ToString("F2");
                            }
                            else
                            {
                                this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.CardCost].Value = "1000.00";
                            }
                            
                        }
                        this.FpAccount_Sheet1.Cells[0, (int)AccountModeCols.GetOper].Value = this.GetPersonName(info.GetOper);
                    }
                }
            }
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum AccountModeCols
        {
            /// <summary>
            /// 卡类型
            /// </summary>
            CardKind = 0,

            /// <summary>
            /// 卡类型名称
            /// </summary>
            CardName = 1,

            /// <summary>
            /// 卡号
            /// </summary>
            CardNO = 2,

            /// <summary>
            /// 领卡客户
            /// </summary>
            GetName = 3,

            /// <summary>
            /// 领卡时间
            /// </summary>
            GetDate = 4,

            /// <summary>
            /// 卡金额
            /// </summary>
            CardCost = 5,

            /// <summary>
            /// 发卡人员
            /// </summary>
            GetOper = 6,
        }

      

    }
}

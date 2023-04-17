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
    public partial class frmDeposit : Form
    {
        #region 属性

        /// <summary>
        /// 当前会员卡信息
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

        /// <summary>
        /// 会员卡信息
        /// </summary>
        public FS.HISFC.Models.Account.AccountCard AccountCardInfo
        {
            get { return this.accountCardInfo; }
            set { this.accountCardInfo = value; }
        }

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
        /// 账户支付金额
        /// </summary>
        private ArrayList accountPay = new ArrayList();

        /// <summary>
        /// 赠送支付金额
        /// </summary>
        private ArrayList giftPay = new ArrayList();

        /// <summary>
        /// 是否启用套餐押金充值数据节点模块
        /// </summary>
        private bool IsPackageDepositTopUpStatisticsPointInUse = false;

        #endregion 

        #region 业务类

        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 账户管理类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account account = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 套餐费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackgeProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();

        /// <summary>
        /// 患者信息业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtProcess = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        public frmDeposit()
        {
            InitializeComponent();
            InitPayModeHelp();
            InitControls();
        }

        /// <summary>
        /// 初始化支付方式帮助类
        /// </summary>
        private void InitPayModeHelp()
        {
            ArrayList payModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = payModes;

            this.IsPackageDepositTopUpStatisticsPointInUse = this.controlParamIntegrate.GetControlParam<bool>("CPP005", false, false);

            //FP热键屏蔽
            InputMap im;
            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPayMode.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <returns></returns>
        private int InitControls()
        {
            try
            {
                this.addEvent();
                this.cmbSex.IsListOnly = true;
                this.cmbCardType.IsListOnly = true;
                this.cmbLevel.IsListOnly = true;

                //初始化性别
                ArrayList sexListTemp = new ArrayList();
                ArrayList sexList = new ArrayList();
                sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
                FS.HISFC.Models.Base.Spell spell = null;
                foreach (FS.FrameWork.Models.NeuObject neuObj in sexListTemp)
                {
                    spell = new FS.HISFC.Models.Base.Spell();
                    if (neuObj.ID == "M")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "1";
                    }
                    else if (neuObj.ID == "F")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "2";
                    }
                    else if (neuObj.ID == "O")
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                        spell.UserCode = "3";
                    }
                    else
                    {
                        spell.ID = neuObj.ID;
                        spell.Name = neuObj.Name;
                        spell.Memo = neuObj.Memo;
                    }
                    sexList.Add(spell);
                }
                this.cmbSex.AddItems(sexList);

                //初始化证件类型列表
                ArrayList IDTypeList = constantMgr.GetList("IDCard");
                if (IDTypeList == null)
                {
                    MessageBox.Show("初始化证件类型列表出错!" + this.managerIntegrate.Err);

                    return -1;
                }
                this.cmbCardType.AddItems(IDTypeList);

                //初始化会员级别
                ArrayList memberLevel = constantMgr.GetList("MemCardType");
                if (memberLevel == null)
                {
                    MessageBox.Show("初始化会员类型列表出错!" + this.managerIntegrate.Err);

                    return -1;
                }
                this.cmbLevel.AddItems(memberLevel);
            }
            catch (Exception ex)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 添加触发事件
        /// </summary>
        private void addEvent()
        {
            this.fpPayMode.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(fpPayMode_CellClick);
            this.fpPayMode.Leave += new EventHandler(fpPayMode_Leave);
            this.fpPayMode.EditModeOn += new EventHandler(fpPayMode_EditModeOn);
            this.fpPayMode.Change += new ChangeEventHandler(fpPayMode_Change);

            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// 删除触发事件
        /// </summary>
        private void delEvent()
        {
            this.fpPayMode.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(fpPayMode_CellClick);
            this.fpPayMode.Leave -= new EventHandler(fpPayMode_Leave);
            this.fpPayMode.EditModeOn -= new EventHandler(fpPayMode_EditModeOn);
            this.fpPayMode.Change -= new ChangeEventHandler(fpPayMode_Change);

            this.btnSave.Click -= new EventHandler(btnSave_Click);
            this.btnCancel.Click -= new EventHandler(btnCancel_Click);
        }

        /// <summary>
        /// 编辑内容发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPayMode_Change(object sender, ChangeEventArgs e)
        {

            //编辑的是支付金额
            if (e.Column == (int)PayModeCols.TotCost)
            {
                this.delEvent();

                try
                {
                    //输入金额
                    decimal cost = 0.0m;
                    if (this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value != null)
                    {
                        cost = Decimal.Parse(this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                    }
                    else
                    {
                        this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value = 0;
                    }

                    if (cost < 0)
                    {
                        throw new Exception("输入金额不能小于零！");
                    }

                }
                catch (Exception ex)
                {
                    this.fpPayMode_Sheet1.Cells[e.Row, e.Column].Value = this.previousValue;
                    MessageBox.Show(ex.Message);
                }

                this.addEvent();
            }
        }

        /// <summary>
        /// 支付方式列表点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPayMode_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.Name);
            this.fpPayMode_Sheet1.SetActiveCell(e.Row, (int)PayModeCols.TotCost);


            FS.HISFC.Models.Base.Const cst = this.fpPayMode_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Base.Const;
            if ((cst.ID == "YS" || cst.ID == "DC") && e.Column == (int)PayModeCols.TotCost)
            {
                this.AccountPayShow();
            }
        }

        /// <summary>
        ///  获取会员账户支付方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountPayShow()
        {
            try
            {
                this.delEvent();

                //获取当前账户支付的金额
                ArrayList tmp = new ArrayList();
                tmp.AddRange(this.accountPay);
                tmp.AddRange(this.giftPay);


                //会员支付框
                string ErrInfo = string.Empty;
                Forms.frmAccountCostDE accountCost = new Forms.frmAccountCostDE();
                accountCost.PatientInfo = this.PatientInfo;
                if (accountCost.SetPayInfo(tmp, ref ErrInfo) < 0)
                {
                    throw new Exception(ErrInfo);
                }
                accountCost.SetPayModeRes += new DelegateHashtableSet(accountCost_SetPayModeRes);
                accountCost.ShowDialog();
            }
            catch (Exception ex)
            {
                this.addEvent();
                MessageBox.Show("获取待支付金额出错:" + ex.Message);
                return;
            }

            this.addEvent();
        }

        /// <summary>
        /// 委托
        /// </summary>
        /// <param name="hsTable"></param>
        /// <param name="totCost"></param>
        /// <returns></returns>
        private int accountCost_SetPayModeRes(Hashtable hsTable, decimal totCost)
        {
            try
            {
                if (hsTable == null)
                {
                    throw new Exception("获取会员支付方式出错！");
                }

                if (hsTable.ContainsKey("YS"))
                {
                    this.accountPay = hsTable["YS"] as ArrayList;
                }

                if (hsTable.ContainsKey("DC"))
                {
                    this.giftPay = hsTable["DC"] as ArrayList;
                }

                if (this.accountPay == null || this.giftPay == null)
                {
                    foreach (Row row in this.fpPayMode_Sheet1.Rows)
                    {
                        FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                        if (pay.ID == "YS")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }

                        if (pay.ID == "DC")
                        {
                            this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = 0.0m;
                        }
                    }

                    accountPay = new ArrayList();
                    giftPay = new ArrayList();
                    throw new Exception("获取会员支付信息出错！");
                }


                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    FS.HISFC.Models.Base.Const pay = row.Tag as FS.HISFC.Models.Base.Const;

                    if (pay.ID == "YS")
                    {
                        decimal ysCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit payMode in accountPay)
                        {
                            ysCost += payMode.Amount;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = ysCost;
                    }

                    if (pay.ID == "DC")
                    {
                        decimal gfCost = 0.0m;
                        foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit payMode in giftPay)
                        {
                            gfCost += payMode.Amount;
                        }
                        this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value = gfCost;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 存储输入值的上一个值，用于的输入值非法时进行恢复
        /// </summary>
        private double previousValue = 0.0;

        /// <summary>
        /// 当支付方式列表失去焦点的时候
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPayMode_Leave(object sender, EventArgs e)
        {
            this.fpPayMode.StopCellEditing();
        }

        /// <summary>
        /// 编辑模式开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fpPayMode_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                previousValue = Double.Parse(this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.ActiveRowIndex, this.fpPayMode_Sheet1.ActiveColumnIndex].Value.ToString());
                this.fpPayMode.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            }
            catch
            {
            }
        }

        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                PutArrow(Keys.Left);
            }
            if (e.KeyCode == Keys.Right)
            {
                PutArrow(Keys.Right);
            }
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
                if (this.fpPayMode.ContainsFocus)
                {

                    if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Enter)
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
                this.fpPayMode.Focus();
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
            int currCol = this.fpPayMode_Sheet1.ActiveColumnIndex;
            int currRow = this.fpPayMode_Sheet1.ActiveRowIndex;

            if (key == Keys.Right)
            {
                for (int i = 0; i < this.fpPayMode_Sheet1.Columns.Count; i++)
                {
                    if (i > currCol && this.fpPayMode_Sheet1.Cells[currRow, i].Locked == false)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, i, false);

                        return;
                    }
                }
            }

            if (key == Keys.Left)
            {
                for (int i = this.fpPayMode_Sheet1.Columns.Count - 1; i >= 0; i--)
                {
                    if (i < currCol && this.fpPayMode_Sheet1.Cells[currRow, i].Locked == false)
                    {
                        this.fpPayMode_Sheet1.SetActiveCell(currRow, i, false);
                        return;
                    }
                }
            }

            if (key == Keys.Up)
            {
                if (currRow > 0)
                {
                    this.fpPayMode_Sheet1.ActiveRowIndex = currRow - 1;
                    this.fpPayMode_Sheet1.SetActiveCell(currRow - 1, this.fpPayMode_Sheet1.ActiveColumnIndex);
                }
            }

            if (key == Keys.Down || key == Keys.Enter)
            {
                if (this.fpPayMode_Sheet1.ActiveRowIndex < this.fpPayMode_Sheet1.RowCount - 1)
                {
                    this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.Name);
                    this.fpPayMode_Sheet1.SetActiveCell(currRow + 1, (int)PayModeCols.TotCost);
                }
                else
                {
                    this.fpPayMode.StopCellEditing();
                }
            }
        }

        /// <summary>
        /// 刷新支付方式列表
        /// </summary>
        private void RefreshPayMode()
        {
            delEvent();

            try
            {
                this.fpPayMode_Sheet1.RowCount = 0;

                if (this.patientInfo == null)
                    return;

                if (this.helpPayMode.ArrayObject != null)
                {
                    ///增加普通支付方式
                    foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
                    {
                        this.fpPayMode_Sheet1.Rows.Add(this.fpPayMode_Sheet1.RowCount, 1);
                        this.fpPayMode_Sheet1.Rows[this.fpPayMode_Sheet1.RowCount - 1].Tag = paymode;
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Name].Text = paymode.Name;
                        this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Value = 0;

                        //账户支付
                        if (paymode.ID == "DC" || paymode.ID == "YS")
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].BackColor = System.Drawing.SystemColors.Control;
                        }

                        if (paymode.ID == "RC")
                        {
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.TotCost].Locked = true;
                            this.fpPayMode_Sheet1.Cells[this.fpPayMode_Sheet1.RowCount - 1, (int)PayModeCols.Memo].Locked = true;
                        }
                    }
                }

            }
            catch
            { 
            }
            addEvent();
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private bool SetPatientInfo()
        {
            if (this.patientInfo == null || this.patientInfo.PID.CardNO == "")
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.cmbCardType.Tag = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.cmbSex.Tag = string.Empty;
                this.tbAge.Text = string.Empty;
                this.cmbLevel.Tag = string.Empty;
                this.tbPhone.Text = string.Empty;
                this.accountPay.Clear();
                this.giftPay.Clear();
                return false;
            }

            this.tbMedicalNO.Text = this.accountCardInfo.Patient.PID.CardNO;
            this.tbName.Text = patientInfo.Name;
            this.cmbCardType.Tag = this.patientInfo.IDCardType.ID;
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.cmbSex.Tag = patientInfo.Sex.ID;
            this.tbAge.Text = this.account.GetAge(patientInfo.Birthday);
            this.cmbLevel.Tag = this.accountCardInfo.AccountLevel.ID;
            this.tbPhone.Text = this.patientInfo.PhoneHome;
            return true;
        }

        /// <summary>
        /// 缴纳押金
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                MessageBox.Show("当前患者信息为空！");
                return;
            }

            ArrayList depostList = this.GetDepositInfo();

            if (depostList == null)
            {
                MessageBox.Show("获取押金信息出错！");
                return;
            }

            if (depostList.Count == 0)
            {
                MessageBox.Show("没有需要保存的费用！");
                return;
            }

            string ErrInfo = string.Empty;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存。。。");
            if (this.feePackgeProcess.SaveDeposit(this.PatientInfo, depostList, ref ErrInfo) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ErrInfo);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            MessageBox.Show("押金保存成功！");

            //{F31B0DE2-C48A-4a86-A917-43930C602D52}
            this.Print(depostList);

            #region 套餐押金充值数据统计节点业务
            //套餐押金充值数据统计节点是否启用-CPP005
            if (this.IsPackageDepositTopUpStatisticsPointInUse)
            {
                FS.HISFC.BizProcess.Interface.StatisticsPoint.IStatisticsPoint iStatistics = new FS.HISFC.BizProcess.Integrate.StatisticsPoint.PackageDepositTopUpStatisticsPoint();
                //HISFC.Models.RADT.PatientInfo patientInfo = radtProcess.QueryComPatientInfo(prePay.Patient.PID.CardNO);
                iStatistics.SetPatient(this.PatientInfo);
                iStatistics.DoStatistics();
            }
            #endregion

            this.Clear();
            this.Close();
        }

        //{F31B0DE2-C48A-4a86-A917-43930C602D52}
        /// <summary>
        /// 打印挂号发票
        /// </summary>
        /// <param name="regObj"></param>
        private void Print(ArrayList InvoiceNO)
        {
            FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice packageinvoiceprint = null;
            packageinvoiceprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice)) as FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice;
            if (packageinvoiceprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            packageinvoiceprint.SetPrintValue(InvoiceNO);
            packageinvoiceprint.Print();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.Close();
        }

        /// <summary>
        /// 获取押金支付信息
        /// </summary>
        /// <returns></returns>
        private ArrayList GetDepositInfo()
        {
            ArrayList depositList = new ArrayList();
            //{BBF7115A-0C18-4660-AA34-FCB46A65D68F}
            string ysmemo = string.Empty;
            string dcmemo = string.Empty;

            try
            {
                foreach (Row row in this.fpPayMode_Sheet1.Rows)
                {
                    if (Double.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString()) > 0)
                    {
                        FS.HISFC.Models.Base.Const cst = this.fpPayMode_Sheet1.Rows[row.Index].Tag as FS.HISFC.Models.Base.Const;

                      

                        if (cst.ID != "YS" && cst.ID != "DC")
                        {
                            if (this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value != null &&
                               (decimal)this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value > 0)
                            {
                                FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = new FS.HISFC.Models.MedicalPackage.Fee.Deposit();

                                //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
                                FS.HISFC.Models.Base.Employee employee = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                                FS.HISFC.Models.Base.Department dept = employee.Dept as FS.HISFC.Models.Base.Department;

                                deposit.CardNO = this.patientInfo.PID.CardNO;
                                deposit.Trans_Type = "1";
                                //记录类型
                                deposit.DepositType = FS.HISFC.Models.MedicalPackage.Fee.DepositType.JYJ;
                                deposit.Mode_Code = cst.ID;
                                deposit.Amount = Decimal.Parse(this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.TotCost].Value.ToString());
                                deposit.Memo = this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Text;

                                //{E5C47B78-AB42-4423-8B0F-1658CEB5AC7C}
                                deposit.HospitalID = dept.HospitalID;
                                deposit.HospitalName = dept.HospitalName;

                                depositList.Add(deposit);
                            }
                        }
                        //{BBF7115A-0C18-4660-AA34-FCB46A65D68F}
                        else
                        {
                            if (cst.ID == "YS")
                            {
                                ysmemo = this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Text;
                            }
                            else if (cst.ID == "DC")
                            {
                                dcmemo = this.fpPayMode_Sheet1.Cells[row.Index, (int)PayModeCols.Memo].Text;
                            }
                        }
                    }
                }

                //{BBF7115A-0C18-4660-AA34-FCB46A65D68F}
                if (this.accountPay != null && this.accountPay.Count > 0)
                {
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit payMode in accountPay)
                    {
                        payMode.Memo = ysmemo;
                    }
                }

                if (this.giftPay != null && this.giftPay.Count > 0)
                {
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit payMode in giftPay)
                    {
                        payMode.Memo = dcmemo;
                    }
                }

                depositList.AddRange(this.accountPay);
                depositList.AddRange(this.giftPay);
            }
            catch 
            {
                return null;
            }

            return depositList;
        }

        public void Clear()
        {
            this.AccountCardInfo = null;
            this.PatientInfo = null;
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PayModeCols
        {
            /// <summary>
            /// 支付方式
            /// </summary>
            Name = 0,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 1,

            /// <summary>
            /// 备注
            /// </summary>
            Memo = 2
        }

    }

}

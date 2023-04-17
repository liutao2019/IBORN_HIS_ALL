using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface.Attributes;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;
using FS.SOC.HISFC.InpatientFee.Interface;

namespace FS.SOC.Local.InpatientFee.GuangZhou.GYSY
{

    /// <summary>
    /// 费用列枚举
    /// </summary>
    public enum ColumnCost
    {
        Check,
        MinFee,
        Cost,
        BalanceCost,
        FeeItem
    }

    /// <summary>
    /// 预交金列枚举
    /// </summary>
    public enum ColumnPrepay
    {
        Check,
        Date,
        Payway,
        Cost
    }

    /// <summary>
    /// 费用项目枚举
    /// </summary>
    public enum ColumnFeeItem
    {
        ItemCode,
        ItemName,
        Specise,
        FeeCode,
        Price,
        Qty,
        TotCost,
        OwnCost,
        PayCost,
        PubCost,
        DecCost
    }

    /// <summary>
    /// [功能描述: 出院结算表现类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class ucBalanceByItems : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBalanceByItems()
        {
            InitializeComponent();
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);

            this.fpCost.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCost_ButtonClicked);
            this.fpCost.DoubleClick += new EventHandler(fpCost_DoubleClick);
            this.fpPrepay.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpPrepay_ButtonClicked);

            this.txtBalanceCostSplit.KeyDown += new KeyEventHandler(txtBalanceCostSplit_KeyDown);
            this.txtBalanceCostSplit.Leave += new EventHandler(txtBalanceCostSplit_Leave);
            this.ckOutBalance.CheckedChanged += new EventHandler(ckOutBalance_CheckedChanged);
        }

       

        #region 变量

        /// <summary>
        /// toolBarService
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();

        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 实体变量
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        private FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();

        /// <summary>
        /// 结算总金额中的自费金额,自付金额,公费金额
        /// </summary>
        decimal TotalOwnCost = 0m;
        decimal TotalPayCost = 0m;
        decimal TotalPubCost = 0m;

        decimal TotalCost = 0m;
        decimal BalanceTotalCost = 0m;

        frmBalancePay frmBalancePay = new frmBalancePay();

        ArrayList alAllMinFee = new ArrayList();//需要传送到费用项目选择界面去的变量

        string strBegin = string.Empty;
        string strEnd = string.Empty;

        Hashtable htItems = new Hashtable();
        #endregion

        #region 属性
        FS.HISFC.Models.Base.EBlanceType balanceType = FS.HISFC.Models.Base.EBlanceType.ItemBalance;
        [Category("控件设置"), Description("结算类别Out:出院结算Mid:中途结算Owe:欠费结算欠费结算分为两种：一是按照预交金额结算，一是按照费用全额结算，由参数控制")]
        public FS.HISFC.Models.Base.EBlanceType BalanceType
        {
            get
            {
                return balanceType;
            }
            set
            {
                balanceType = value;
            }
        }

        bool isTransPrepay = true;
        [Category("控件设置"), Description("是否使用转押金，True使用，False不使用"), FSSetting()]
        public bool IsTransPrepay
        {
            get
            {
                return isTransPrepay;
            }
            set
            {
                isTransPrepay = value;
            }
        }


        bool isInputDerateFee = false;
        /// <summary>
        /// 结算时候是否输入减免金额
        /// </summary>
        [Category("控件设置"), Description("是否能输入减免金额，True使用，False不使用"), FSSetting()]
        public bool IsInputDerateFee
        {
            get
            {
                return isInputDerateFee;
            }
            set
            {
                isInputDerateFee = value;
            }
        }

        bool isCloseAccount = false;
        /// <summary>
        /// 结算时候是否走结算清单封帐
        /// </summary>
        [Category("控件设置"), Description("是否走结算清单封帐，True使用，False不使用"), FSSetting()]
        public bool IsCloseAccount
        {
            get
            {
                return isCloseAccount;
            }
            set
            {
                isCloseAccount = value;
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取下一打印发票号
        /// </summary>
        private void GetNextInvoiceNO()
        {
            lblNextInvoiceNO.Text = "";
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string invoiceNO = feeIntegrate.GetNextInvoiceNO("I", oper);
            if (string.IsNullOrEmpty(invoiceNO))
            {
                CommonController.CreateInstance().MessageBox(feeIntegrate.Err, MessageBoxIcon.Warning);
                return;
            }

            lblNextInvoiceNO.Text = "打印号： " + invoiceNO;
        }

        /// <summary>
        /// 住院号回车处理
        /// </summary>
        protected virtual int QueryByPatientNO(string inpatientNO, ref string errText)
        {
            //回车触发事件
            this.Clear();

            if (inpatientNO == null || inpatientNO == "")
            {
                errText = "住院号错误，没有找到该患者";
                return -1;
            }
            FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();
            patientInfo = radtMgr.GetPatientInfo(inpatientNO);
            //判断患者状态
            if (this.ValidPatient(this.patientInfo, ref errText) == -1)
            {
                return -1;
            }

            //赋上住院号
            this.ucQueryInpatientNo.TextBox.Text = patientInfo.PID.PatientNO;
            //赋值患者信息
            this.SetPatientInfo(patientInfo);

            this.txtDeratefee.ReadOnly = !IsInputDerateFee;

            if (this.feeManager.CloseAccount(this.patientInfo.ID) < 0)
            {
                errText = "关账失败，原因：" + this.feeManager.Err;
                return -1;
            }


          
            if (this.DisplayPatientCost(ref errText) == -1)
            {
                return -1;
            }

            if (this.balanceType == EBlanceType.OweMid)
            {
                this.txtBalanceCostSplit.Text = this.txtPrepayCost.Text;
                this.txtBalanceCostSplit_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }


            return 1;
        }

        /// <summary>
        /// 结算有效性判断
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        protected virtual int ValidPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, ref string errText)
        {
            //已经出院的返回
            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O.ToString()))
            {
                errText = "患者已经出院结算!";
                return -1;
            }
            //结算时候是否走结算清单封帐
            if (IsCloseAccount)
            {
                if (patientInfo.IsStopAcount == false)
                {
                    errText = "患者出院结算前，请先到结算清单处进行封帐!";
                    return -1;
                }
            }

            //中途结算判断在院状态
            if (this.BalanceType == FS.HISFC.Models.Base.EBlanceType.Mid)
            {
                #region  delete By maokb--- 出院登记患者也可以进行中途结算
                #endregion

                #region delete By maokb --医保患者大部分医保允许中途结算，在此不再卡住。
                #endregion
            }
            //出院结算判断状态
            else
            {
                string suretyCost = this.inpatientFeeManager.GetSurtyCost(patientInfo.ID);
                if (FS.FrameWork.Function.NConvert.ToDecimal(suretyCost) > 0)
                {
                    errText = "患者有未返还的担保金:" + suretyCost + "元,请返还后再进行出院结算！";
                    return -1;
                }

            }

            //转押金
            ArrayList alForegift = this.inpatientFeeManager.QueryForegif(patientInfo.ID);
            if (alForegift == null)
            {
                errText = this.inpatientFeeManager.Err;
                return -1;
            }
            if (alForegift.Count > 0)
            {
                errText = "患者存在没有打印的中结转押金票据,请打印!";
                return -1;
            }

            //退费申请、退药申请、终端确认等
            FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
            if (feeManager.IsExistApplyFee(this.patientInfo.ID))
            {
                if (BalanceType == EBlanceType.Mid)
                {
                    errText = "存在未确认的退费申请，请退费后再结算！";
                    return -1;
                }
                else
                {
                    if (CommonController.CreateInstance().MessageBox("存在未确认的退费申请，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return -1;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 验证金额是否正确
        /// </summary>
        /// <param name="errText"></param>
        /// <returns></returns>
        protected virtual bool ValidCost(ref string errText)
        {
            decimal balanceCost = 0m;
            decimal supplyCost = 0m;
            decimal splitMannalcost = 0m;
            supplyCost = decimal.Parse(this.txtShouldPay.Text);
            balanceCost = decimal.Parse(this.txtBalanceCost.Text);
            if (balanceCost <= 0)
            {
                errText = "结算金额必须大于零!";
                return false;
            }
            //判断减免金额和优惠金额
            if (this.txtDeratefee.Text.Trim() == "") this.txtDeratefee.Text = "0.00";
            if (this.txtRebateFee.Text.Trim() == "") this.txtRebateFee.Text = "0.00";
            return true;
        }

        /// <summary>
        /// 患者信息赋值
        /// </summary>
        /// <param name="patientInfo">患者信息实体</param>
        protected virtual void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                this.lblPatientInfo.Text = "姓名".PadRight(6, ' ') + "入院日期:".PadRight(6, ' ') + "出院日期:".PadRight(6, ' ') + "结算类别:".PadRight(6, ' ');
            }
            else
            {
                //诊断
                FS.SOC.HISFC.InpatientFee.BizProcess.Diagnose diagnoseMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.Diagnose();
                string diagnoseName = diagnoseMgr.QueryDiagnoseName(patientInfo.ID);
                FS.SOC.HISFC.InpatientFee.BizProcess.RADT radtMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.RADT();

                ArrayList alBabyInfo = radtMgr.QueryBabies(patientInfo.ID);
                string strBabyInfo = string.Empty;
                if (alBabyInfo != null && alBabyInfo.Count != 0)
                {
                    strBabyInfo = " 婴儿住院号:";
                    foreach (FS.HISFC.Models.RADT.PatientInfo babyInfo in alBabyInfo)
                    {
                        strBabyInfo += babyInfo.PID.PatientNO.Substring(0, 2) + ",";
                    }
                    strBabyInfo = strBabyInfo.TrimEnd(',');
                }
                this.lblPatientInfo.Text = "姓名:" + patientInfo.Name + " 入院日期:" + patientInfo.PVisit.InTime.ToString("yyyy-MM-dd") + " 出院日期:" + patientInfo.PVisit.OutTime.ToString("yyyy-MM-dd") + " 结算类别:" + patientInfo.Pact.Name + " 出院诊断:" + diagnoseName + strBabyInfo;
            }
        }

        /// <summary>
        /// 显示患者费用信息
        /// </summary>
        /// <returns>1成功 －1失败</returns>
        protected virtual int DisplayPatientCost(ref string errText)
        {
            this.ClearFee();
            this.txtDeratefee.Text = "0.00";
            this.txtDeratefee.ReadOnly = true;


            //检索费用--含优惠
            if (this.QueryFeeInfo( ref errText) == -1) return -1;

            //检索预交金
            if (this.QueryPrepayInfo(ref errText) == -1) return -1;

            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtBalanceCost.Text) == 0)
            {
                errText = "该患者没有可结算费用";
                return -1;
            }

           // ArrayList alFeeList = this.QueryFeeItemlist(this.patientInfo.ID, ref errText);
            //if (alFeeList == null)
            //{
            //    return -1;
            //}

            //if (Function.PreBalanceInpatient(this.patientInfo, alFeeList, ref errText) < 0)
            //{
            //    return -1;
            //}

          //  this.txtBalanceCostSplit.Text = this.txtBalanceCost.Text;
            //计算费用显示返还情况
         //   this.ComputeSupplyCost();

            return 1;
        }


        /// <summary>
        /// 检索患者费用汇总信息
        /// </summary>
        /// <param name="dtFeeBegin">开始时间</param>
        /// <param name="dtFeeEnd">结束时间</param>
        /// <returns>1成功 －1失败</returns>
        protected virtual int QueryFeeInfo(ref string errText)
        {
            ArrayList al = this.inpatientFeeManager.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(patientInfo.ID, "0");
            if (al == null)
            {
                errText = inpatientFeeManager.Err;
                return -1;
            }

            //优惠总金额
            decimal RebateCost = 0m;
            //单病种优惠金额
            decimal icdRebate = 0m;
            //判断是否有血押金----提取血押金FeeCode   
            string BloodFeeCode = "";
            FS.FrameWork.Management.ControlParam controlParm = new FS.FrameWork.Management.ControlParam();
            BloodFeeCode = controlParm.QueryControlerInfo("100008");

            this.fpCost_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfoClone = new FS.HISFC.Models.Fee.Inpatient.FeeInfo();
                feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                // 暂时中山医不这样处理
                if (feeInfo.Item.MinFee.ID == BloodFeeCode)
                {
                    errText = "该患者有血押金,请退还再结算!";
                    return -1;
                }
                //获取最小费用名称
                feeInfo.Item.MinFee.Name = CommonController.CreateInstance().GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                RebateCost += feeInfo.FT.RebateCost;

                this.fpCost_Sheet1.RowCount++;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Locked = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo.Clone();
                this.fpCost_Sheet1.Rows[i].Tag = feeInfo;

                alAllMinFee.Add(feeInfo.Item.MinFee);
            }

            //结算总金额赋值
            this.txtBalanceCost.Text = TotalCost.ToString("F2");

            //判断该患者是否单病种优惠提取单病种金额
            if (this.BalanceType != EBlanceType.Mid)
            {
                //单病种定额
                decimal icdFee = 0m;
                FS.HISFC.BizLogic.Fee.Ecoformula feeEcoFormula = new FS.HISFC.BizLogic.Fee.Ecoformula();
                icdFee = feeEcoFormula.GetCost(this.patientInfo.ID, "000", feeEcoFormula.GetDateTimeFromSysDateTime());
                if (icdFee < 0)
                {
                    errText = "提取单病种定额出错!";
                    return -1;
                }
                //单病种优惠时候不允许有其他优惠
                if (icdFee > 0 && icdFee < TotalCost)
                {
                    icdRebate = TotalCost - icdFee;
                    this.IsInputDerateFee = true;
                    //其他优惠为零
                    RebateCost = 0;
                    this.txtDeratefee.ReadOnly = true;
                    //{BD300517-D927-43c0-A1D3-8FB99BD10298}
                    this.txtDeratefee.Text = icdRebate.ToString("F2");
                }

                this.txtRebateFee.Text = RebateCost.ToString("F2");

            }

            return 1;
        }

        /// <summary>
        /// 检索患者预交金信息
        /// </summary>
        /// <returns>1成功 －1失败</returns>
        protected virtual int QueryPrepayInfo(ref string errText)
        {
            ArrayList al = new ArrayList();

            al = this.inpatientFeeManager.QueryPrepaysBalanced(patientInfo.ID);
            if (al == null)
            {
                errText = this.inpatientFeeManager.Err;
                return -1;
            }
            //检索是否有转入预交金--出院结算处理
            if (this.BalanceType != EBlanceType.Mid)
            {
                decimal ChangePrepay = this.inpatientFeeManager.GetTotChangePrepayCost(patientInfo.ID);
                if (ChangePrepay < 0)
                {
                    errText = "检索转入预交金出错!" + this.inpatientFeeManager.Err;
                    return -1;
                }
                if (ChangePrepay > 0)
                {
                    FS.HISFC.Models.Fee.Inpatient.Prepay prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
                    prepay.FT.PrepayCost = ChangePrepay;
                    prepay.PayType.Name = "转入预交金";
                    al.Add(prepay);
                }
            }

            //预交金额
            decimal PrepayCost = 0m;
            this.fpPrepay_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.Prepay prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)al[i];
                PrepayCost += prepay.FT.PrepayCost;

                this.fpPrepay_Sheet1.RowCount++;
                this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value = true;
                this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Date].Value = prepay.PrepayOper.OperTime;
                this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Payway].Value = prepay.PayType.Name;
                this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Cost].Value = prepay.FT.PrepayCost;
                this.fpPrepay_Sheet1.Rows[i].Tag = prepay;
            }

            this.txtPrepayCost.Text = PrepayCost.ToString("F2");

            return 1;
        }

        /// <summary>
        /// 计算患者结算补收返还费用金额
        /// </summary>
        protected virtual void ComputeSupplyCost()
        {
            if (this.patientInfo.ID == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }

            decimal BalanceCost = 0m;
            decimal PrepayCost = 0m;
            decimal BalanceOwnCost = 0m;
            decimal BalancePubCost = 0m;
            decimal BalancePayCost = 0m;
            decimal BalanceRebateCost = 0m;
            decimal DerateCost = 0m;
            decimal RealCost = 0m; 

            PrepayCost = decimal.Parse(this.txtPrepayCost.Text);
            BalanceCost = decimal.Parse(this.txtBalanceCost.Text);
            BalanceRebateCost = decimal.Parse(this.txtRebateFee.Text);
            DerateCost = decimal.Parse(this.txtDeratefee.Text);

            if (BalanceCost < 0)
            {
                CommonController.CreateInstance().MessageBox("中途结算的范围选择引起本次结算总额小于零，请对中结进行召回！", MessageBoxIcon.Warning);
                return;
            }

           

            //添加合计项
            this.BalanceTotalCost = 0M;
            BalanceRebateCost=0m;
            //获得各费用分项
            FS.HISFC.Models.Fee.Inpatient.FeeInfo f;
            for (int i = 0; i < this.fpSelectedItem_Sheet1.Rows.Count; i++)
            {

                f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                    this.htItems[this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.ItemCode].Text.ToString() 
                    + this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.ItemName].Text.ToString() 
                    + this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.Specise].Text.ToString()
                    + FS.FrameWork.Public.String.FormatNumberReturnString(FS.FrameWork.Function.NConvert.ToDecimal(this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.Price].Text),4)];
                    BalanceOwnCost += f.FT.OwnCost;
                    BalancePubCost += f.FT.PubCost;
                    BalancePayCost += f.FT.PayCost;
                    BalanceRebateCost += f.FT.RebateCost;
                    this.BalanceTotalCost += f.FT.TotCost;

            }


            this.TotalOwnCost = BalanceOwnCost;
            this.TotalPayCost = BalancePayCost;
            this.TotalPubCost = BalancePubCost;



            BalanceCost = this.BalanceTotalCost;
            this.txtBalanceCost.Text = BalanceCost.ToString("F2");
            this.txtRebateFee.Text = BalanceRebateCost.ToString("F2");
           

           

            //通过接口实现全部患者都统一用此算法
            //RealCost = BalanceCost - this.patientInfo.SIMainInfo.PubCost - DerateCost - BalanceRebateCost;
            //BalancePubCost = patientInfo.SIMainInfo.PubCost;
            RealCost = BalanceCost - this.TotalPubCost - DerateCost - BalanceRebateCost;
            BalancePubCost = TotalPubCost;
            this.txtOwnTot.Text = RealCost.ToString("F2");
            this.txtPubTot.Text = BalancePubCost.ToString("F2");

            //获取supplycost
            decimal SupplyCost = 0m;
            if (this.patientInfo.Pact.PayKind.ID == "03")
            {
                SupplyCost = RealCost - PrepayCost;
            }
            else
            {
                SupplyCost = RealCost - this.patientInfo.SIMainInfo.PayCost - PrepayCost;
            }

            //清空各支付信息框
            this.txtShouldPay.Text = "0.00";
            this.txtReturnCost.Text = "0.00";
            this.txtTransPrepayCost.Text = "0.00";

            decimal splitInvoiceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
            //设置为可用
            if (SupplyCost >= 0)//补收
            {
                if (balanceType == EBlanceType.Out||balanceType==EBlanceType.ItemBalance)
                {
                    this.txtShouldPay.Text = SupplyCost.ToString("F2");
                    this.txtBalanceCostSplit.Text = BalanceCost.ToString("F2");
                }
                else if (balanceType == EBlanceType.Owe)
                {
                    this.txtTransPrepayCost.Text = SupplyCost.ToString("F2");
                    this.txtBalanceCostSplit.Text = BalanceCost.ToString("F2");
                }
                else if (balanceType == EBlanceType.OweMid)
                {
                    this.txtTransPrepayCost.Text = SupplyCost.ToString("F2");
                    this.txtBalanceCostSplit.Text = (BalanceCost - SupplyCost).ToString("F2");
                }
            }
            else //应返 
            {
                this.txtReturnCost.Text = (-SupplyCost).ToString("F2");
            }

            

            this.AddFeeInfoSumCost();
            this.AddPrepaySumCost();
        }

        /// <summary>
        /// 清空
        /// </summary>
        protected virtual void Clear()
        {
            this.ClearFee();
            if (this.patientInfo != null && string.IsNullOrEmpty(patientInfo.ID) == false)
            {
                //开帐
                if (this.feeManager.OpenAccount(this.patientInfo.ID) == -1)
                {
                    CommonController.CreateInstance().MessageBox("开帐失败" + this.feeManager.Err, MessageBoxIcon.Warning);
                    return;
                }
            }
            //清空患者信息控件
            this.SetPatientInfo(null);
            this.ucQueryInpatientNo.Focus();

            this.ckOutBalance.CheckedChanged -= new EventHandler(ckOutBalance_CheckedChanged);
            this.balanceType = EBlanceType.ItemBalance;
            this.ckOutBalance.Checked = true;
            this.ckOutBalance.CheckedChanged += new EventHandler(ckOutBalance_CheckedChanged);

            frmBalancePay.ListBalancePay.Clear();
        }

        /// <summary>
        /// 清空费用
        /// </summary>
        private void ClearFee()
        {
            //清空farpoint
            this.fpCost_Sheet1.RowCount = 0;
            this.fpPrepay_Sheet1.RowCount = 0;
            this.txtDeratefee.Text = "0.00";
            this.txtBalanceCostSplit.Text = "0.00";
            this.txtShouldPay.Text = "0.00";
            this.txtTransPrepayCost.Text = "0.00";
            this.txtReturnCost.Text = "0.00";
            this.txtRebateFee.Text = "0.00";

            this.txtOwnTot.Text = "0.00";
            this.txtPubTot.Text = "0.00";
            this.txtPrepayCost.Text = "0.00";
            this.txtBalanceCost.Text = "0.00";

            this.TotalCost = 0M;
            this.BalanceTotalCost = 0M;
            this.TotalOwnCost = 0M;
            this.TotalPayCost = 0M;
            this.TotalPubCost = 0M;
        }

        /// <summary>
        /// 添加合计
        /// </summary>
        private void AddFeeInfoSumCost()
        {
            int row = this.fpCost_Sheet1.RowCount;
            if (row > 0)
            {
                if (this.fpCost_Sheet1.Rows[row - 1].Tag is FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                {
                    this.fpCost_Sheet1.RowCount++;
                }
                else
                {
                    row = row - 1;
                }
                this.fpCost_Sheet1.Cells[row, (int)ColumnCost.Check].Locked = true;
                this.fpCost_Sheet1.Cells[row, (int)ColumnCost.Check].Value = false;
                this.fpCost_Sheet1.Cells[row, (int)ColumnCost.MinFee].Value = "合计：";
                this.fpCost_Sheet1.Cells[row, (int)ColumnCost.Cost].Value = this.TotalCost;
                this.fpCost_Sheet1.Cells[row, (int)ColumnCost.BalanceCost].Value = this.BalanceTotalCost;
                this.fpCost_Sheet1.Rows[row].Tag = null;
            }
        }

        /// <summary>
        /// 添加合计
        /// </summary>
        private void AddPrepaySumCost()
        {
            int row = this.fpPrepay_Sheet1.RowCount;
            if (row > 0)
            {
                if (this.fpPrepay_Sheet1.Rows[row - 1].Tag is FS.HISFC.Models.Fee.Inpatient.Prepay)
                {
                    this.fpPrepay_Sheet1.RowCount++;
                }
                else
                {
                    row = row - 1;
                }

                this.fpPrepay_Sheet1.Cells[row, (int)ColumnPrepay.Check].Locked = true;
                this.fpPrepay_Sheet1.Cells[row, (int)ColumnPrepay.Check].Value = false;
                this.fpPrepay_Sheet1.Cells[row, (int)ColumnPrepay.Payway].Value = "合计：";
                this.fpPrepay_Sheet1.Cells[row, (int)ColumnPrepay.Cost].Value = this.txtPrepayCost.Text;
                this.fpPrepay_Sheet1.Rows[row].Tag = null;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return 1;
            }
            string errText = "";
            if (this.ValidPatient(this.patientInfo, ref errText) < 0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            if (!this.ValidCost(ref errText))
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }
            if (this.BalanceTotalCost == 0)
            {
                CommonController.CreateInstance().MessageBox("结算总费用必须大于零！", MessageBoxIcon.Warning);
                return -1;
            }
            if (this.TotalPubCost != 0)
            {
                if (DialogResult.No == MessageBox.Show("存在记账费用，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk))
                {
                    return -1;
                }
            }

            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
            decimal shouldPayCost = decimal.Parse(this.txtShouldPay.Text);//应收金额大于零
            decimal arrearBalanceCost = decimal.Parse(this.txtTransPrepayCost.Text);
            if (shouldPayCost+arrearBalanceCost> 0)
            {
                frmBalancePay.ArrearBalance = arrearBalanceCost;
                frmBalancePay.ShouldPay = shouldPayCost;
                frmBalancePay.ShowDialog(this);
                if (!frmBalancePay.IsOK)
                {
                    return -1;
                }

                if (frmBalancePay.ListBalancePay.Count > 0)
                {
                    listBalancePay.AddRange(frmBalancePay.ListBalancePay);
                }
            }

            decimal splitInvoiceCost = decimal.Parse(this.txtBalanceCostSplit.Text);

            #region 结算费用
            string FeeErr = "";
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo;
            for (int i = 0; i < this.fpSelectedItem_Sheet1.RowCount; i++)
            {

                feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)
                    this.htItems[this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.ItemCode].Text.ToString() 
                    + this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.ItemName].Text.ToString()
                    + this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.Specise].Text.ToString()
                    + FS.FrameWork.Public.String.FormatNumberReturnString(FS.FrameWork.Function.NConvert.ToDecimal(this.fpSelectedItem_Sheet1.Cells[i, (int)ColumnFeeItem.Price].Text),4)];
                if (feeInfo.FT.PubCost!=0)
                {
                    FeeErr += feeInfo.Item.Name+"\r\n";
                }
                feeInfo.FT.BalancedCost = feeInfo.FT.TotCost;
                listFeeInfo.Add(feeInfo.Clone());

            }
            if (!string.IsNullOrEmpty(FeeErr))
            {    
            if (DialogResult.No == MessageBox.Show(FeeErr+"存在记账金额，是否继续结算？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
            {
                return -1;
            }
            }
            #endregion

            #region 预交金及支付方式

            List<FS.HISFC.Models.Fee.Inpatient.Prepay> listPrepay = new List<FS.HISFC.Models.Fee.Inpatient.Prepay>();
            FS.HISFC.Models.Fee.Inpatient.Prepay prepay;
            for (int i = 0; i < this.fpPrepay_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value == true)
                {
                    prepay = (FS.HISFC.Models.Fee.Inpatient.Prepay)this.fpPrepay_Sheet1.Rows[i].Tag;
                    prepay.BalanceState = "1";
                    listPrepay.Add(prepay);
                }
            }
            string payTrace = decimal.Parse(this.txtShouldPay.Text) > 0 ? "1" : "2";
            //结算预交金
            if (listPrepay.Count > 0)
            {
                FS.HISFC.Models.Fee.Inpatient.BalancePay pay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                pay.TransKind.ID = "0";
                pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                pay.PayType.ID = "CA";
                pay.FT.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPrepayCost.Text);
                pay.Qty = listPrepay.Count;
                pay.RetrunOrSupplyFlag = payTrace;

                listBalancePay.Add(pay);
            }

            //返还金额
            //2013.04.01 15:06 modify by jing.zhao 
            //修改人：赵景 修改内容：明细结算返回金额插入支付方式表 
            decimal returnCost = decimal.Parse(this.txtReturnCost.Text);//应收金额大于零
            if (returnCost > 0)
            {
                frmBalancePay.ShouldPay = 0;
                frmBalancePay.Clear();
                frmBalancePay.ReturnPay = returnCost;
                frmBalancePay.ShowDialog(this);
                if (!frmBalancePay.IsOK)
                {
                    return -1;
                }

                if (frmBalancePay.ListBalancePay.Count > 0)
                {
                    listBalancePay.AddRange(frmBalancePay.ListBalancePay);
                }
                else
                {
                    CommonController.Instance.MessageBox("请选择退款的支付方式！", MessageBoxIcon.Warning);
                    return -1;
                }

                //FS.HISFC.Models.Fee.Inpatient.BalancePay pay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                //pay.TransKind.ID = "1";
                //pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                //pay.PayType.ID = "CA";
                //pay.FT.TotCost = returnCost;
                //pay.Qty = 1;
                //pay.RetrunOrSupplyFlag = payTrace;
                //listBalancePay.Add(pay);
            }
            //2013.04.01 15:06 modify by jing.zhao 


            #endregion

            List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();

            //出院结算
            if (balanceMgr.ItemBalance(this.patientInfo, this.balanceType, listFeeInfo, listPrepay, listBalancePay, splitInvoiceCost,  ref listBalance) < 0)
            {
                CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
               // FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            ////打印发票
            //if (Function.PrintInvoice(this.patientInfo, listBalance, ref errText) < 0)
            //{
            //    CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
            //    //FS.FrameWork.Management.PublicTrans.RollBack();
            //    return -1;
            //}

            //更新患者住院费用汇总表信息
            if(this.inpatientFeeManager.UpdateFeeInfo(this.patientInfo.ID)<0)
            {
                CommonController.CreateInstance().MessageBox(inpatientFeeManager.Err + inpatientFeeManager.ErrCode, MessageBoxIcon.Information);
            }

            return 1;
        }

        #endregion

        #region 事件

        void ucQueryInpatientNo_myEvent()
        {
            string errText = "";
            if (QueryByPatientNO(this.ucQueryInpatientNo.InpatientNo, ref errText) < 0)
            {
                this.Clear();
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                this.ucQueryInpatientNo.Focus();
            }
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Exit(sender, null);
        }

        void fpCost_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.EditingControl.ToString().ToLower().IndexOf("checkbox") >= 0)
            {
                //重新计算费用分解拆分
                this.TotalCost = 0M;
                FS.HISFC.Models.Fee.Inpatient.FeeInfo f;
                for (int i = 0; i < this.fpCost_Sheet1.RowCount; i++)
                {
                    f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[i].Tag;
                    if ((bool)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value == true)
                    {
                        this.TotalCost += decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value.ToString());
                    }
                    else
                    {
                        this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value = this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value;
                    }

                }
                this.txtBalanceCost.Text = this.TotalCost.ToString("F2");
                this.txtBalanceCostSplit.Text = this.TotalCost.ToString("F2");
                this.txtBalanceCostSplit_KeyDown(null, new KeyEventArgs(Keys.Enter));
            }
            else if (e.EditingControl.ToString().ToLower().IndexOf("button") >= 0)
            {
               FS.HISFC.Models.Fee.Inpatient.FeeInfo f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[e.Row].Tag;
                //弹出费用明细（窗体）
                frmFeeItems feeItems = new frmFeeItems();
                feeItems.FeeCode = f.Item.MinFee.ID;
                feeItems.Begin = this.patientInfo.PVisit.InTime.ToString();

                //DateTime dtDefalt = new DateTime(1900, 1, 1, 0, 0, 0);
                //if (this.patientInfo.PVisit.OutTime > dtDefalt)
                //{
                    feeItems.End = this.inpatientFeeManager.GetDateTimeFromSysDateTime().ToString();
                //}
                //else 
                //{
                //    feeItems.End = this.inpatientFeeManager.GetDateTimeFromSysDateTime().AddDays(-1).ToString();
                //}
                feeItems.InpatientNo = this.patientInfo.ID;
                feeItems.AlMinFee = this.alAllMinFee;

                feeItems.ButtonEvent += new frmFeeItems.ButtonHandler(feeItems_ButtonEvent);


                feeItems.ShowDialog();

            }

        }

        
        void feeItems_ButtonEvent(ArrayList  al)
        {
            if (al != null && al.Count > 0)
            {
                for(int i=0;i<=al.Count-1;++i)
                {
                   FS.HISFC.Models.Fee.Inpatient.FeeInfo fInfo=(FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                   if (!htItems.Contains(fInfo.ID + fInfo.Name + fInfo.Item.Specs+FS.FrameWork.Public.String.FormatNumberReturnString(fInfo.Item.Price,4)))
                   {
                       htItems.Add(fInfo.ID + fInfo.Name + fInfo.Item.Specs + FS.FrameWork.Public.String.FormatNumberReturnString(fInfo.Item.Price,4), fInfo);

                       this.fpSelectedItem_Sheet1.RowCount++;

                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.ItemCode].Value = fInfo.ID;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.ItemName].Value = fInfo.Name;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.Specise].Value = fInfo.Item.Specs;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.FeeCode].Value = fInfo.Memo;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.Price].Value = fInfo.Item.Price.ToString();
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.Qty].Value = fInfo.Item.Qty.ToString();
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.TotCost].Value = fInfo.FT.TotCost;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.OwnCost].Value = fInfo.FT.OwnCost;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.PayCost].Value = fInfo.FT.PayCost;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.PubCost].Value = fInfo.FT.PubCost; ;
                       this.fpSelectedItem_Sheet1.Cells[this.fpSelectedItem_Sheet1.RowCount - 1, (int)ColumnFeeItem.DecCost].Value = fInfo.FT.RebateCost;

                   }
                   
                }
                //重新计算费用
                ComputeSupplyCost();
               
            }
        }
        void fpCost_DoubleClick(object sender, EventArgs e)
        {
            //弹出费用明细（窗体）
     
        }


      


        void fpPrepay_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }

            //if (this.patientInfo.Pact.PayKind.ID == "02")
            //{
            //    return;
            //}

            decimal PrepayCost = 0m;
            for (int i = 0; i < this.fpPrepay_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Check].Value == true)
                {
                    PrepayCost = PrepayCost + decimal.Parse(this.fpPrepay_Sheet1.Cells[i, (int)ColumnPrepay.Cost].Text);
                }
            }
            this.txtPrepayCost.Text = PrepayCost.ToString("F2");
            //显示返还情况
            this.ComputeSupplyCost();
        }

        void txtBalanceCostSplit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                decimal splitInvoiceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
                decimal balanceCost = decimal.Parse(this.txtBalanceCost.Text);
                if (splitInvoiceCost > balanceCost || splitInvoiceCost < 0)
                {
                    this.txtBalanceCostSplit.Text = balanceCost.ToString("F2");
                }
                splitInvoiceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
                if (balanceType == EBlanceType.OweMid)
                {
                    decimal prepayCost = decimal.Parse(this.txtPrepayCost.Text);
                    decimal arrearCost = balanceCost - splitInvoiceCost;
                    this.txtTransPrepayCost.Text = arrearCost.ToString("F2");

                    decimal shouldCost = balanceCost - splitInvoiceCost - arrearCost;
                    this.txtShouldPay.Text = shouldCost.ToString("F2");
                }
            }
        }

        void txtBalanceCostSplit_Leave(object sender, EventArgs e)
        {
            this.txtBalanceCostSplit_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }

        void ckOutBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckOutBalance.Checked)
            {
                this.balanceType = EBlanceType.Out;
                //变化金额
                this.ComputeSupplyCost();
            }
        }
        #endregion

        #region 重载

        protected override void OnLoad(EventArgs e)
        {
            this.GetNextInvoiceNO();
            base.OnLoad(e);
            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);

            this.fpCost.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCost_ButtonClicked);
            this.fpCost.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCost_ButtonClicked);

            this.fpCost.DoubleClick -= new EventHandler(fpCost_DoubleClick);
            this.fpCost.DoubleClick += new EventHandler(fpCost_DoubleClick);
          //  this.fpPrepay.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpPrepay_ButtonClicked);

        }

        /// <summary>
        /// 增加ToolBar控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("确定", "结算患者费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B保存, true, false, null);
            toolBarService.AddToolButton("预览", "预览发票信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Y预览, true, false, null);
            toolBarService.AddToolButton("帮助", "打开帮助文件", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);
            // {11C20D21-2B6B-44db-BF03-E12049117124}
            toolBarService.AddToolButton("更新发票号", "更新下一发票号", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F分票, true, false, null);
            //2012-08-07 
            toolBarService.AddToolButton("结算清单", "打印结算清单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印清单, true, false, null);      

            return this.toolBarService;
        }

        /// <summary>
        /// 定义toolbar按钮click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "确定":
                    this.OnSave(sender, e);
                    break;
                //case "预览":
                //    this.PreviewInvoice();
                //    break;
                case "帮助":
                    break;
                case "更新发票号":
                    FS.HISFC.Components.Common.Forms.frmUpdateUsedInvoiceNo frmUpdate = new FS.HISFC.Components.Common.Forms.frmUpdateUsedInvoiceNo();
                    frmUpdate.InvoiceType = "I";
                    frmUpdate.ShowDialog();
                    GetNextInvoiceNO();
                    break;
                //2012-08-07
                case "结算清单":
                    this.PrintIBillPrint();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;

            if (patientInfo == null || patientInfo.ID == null || patientInfo.ID.Length == 0)
            {
                return -1;
            }

            string errText = "";
            if (QueryByPatientNO(this.patientInfo.ID, ref errText) < 0)
            {
                this.Clear();
                this.patientInfo.ID = null;
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                this.ucQueryInpatientNo.Focus();
            }

            return 1;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.Save() > 0)
            {
                this.Clear();
                this.patientInfo = null;
                this.ucQueryInpatientNo.Focus();
            }
            return base.OnSave(sender, neuObject);
        }

        public override int Exit(object sender, object neuObject)
        {
            if (this.patientInfo == null)
            {
                return base.Exit(sender, neuObject);
            }
            else
            {
                if (this.patientInfo.ID == null || this.patientInfo.ID.Trim() == "") return base.Exit(sender, neuObject);
            }

            this.Clear();

            return base.Exit(sender, neuObject);
        }

        #endregion
        #region 打印结算清单
        private void PrintIBillPrint()
        {
            string errInfo=string.Empty;
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint)) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {
                    BillPrint.SetData(this.patientInfo, this.inpatientFeeManager, ref errInfo);
                    if (errInfo.Length !=0)
                    {
                        MessageBox.Show(errInfo);
                        return;
                    }
                    BillPrint.Print();
            }
        }
        #endregion



        //删除没用的列表数据
        private void miDeleteRow_Click(object sender, EventArgs e)
        {
            int currRow = this.fpSelectedItem_Sheet1.ActiveRowIndex;
            string itemcode = this.fpSelectedItem_Sheet1.Cells[currRow, (int)ColumnFeeItem.ItemCode].Value.ToString();
            string itemName = this.fpSelectedItem_Sheet1.Cells[currRow, (int)ColumnFeeItem.ItemName].Value.ToString();
            string spces = this.fpSelectedItem_Sheet1.Cells[currRow, (int)ColumnFeeItem.Specise].Value.ToString();
            string price = FS.FrameWork.Public.String.FormatNumberReturnString(FS.FrameWork.Function.NConvert.ToDecimal(this.fpSelectedItem_Sheet1.Cells[currRow, (int)ColumnFeeItem.Price].Value),4);
            this.fpSelectedItem_Sheet1.Rows.Remove(currRow, 1);
            if (htItems.Contains(itemcode + itemName + spces + price))
            {
                htItems.Remove(itemcode + itemName + spces + price);
            }
            ComputeSupplyCost();
         
        }
    }
}

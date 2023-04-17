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

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{    
    /// <summary>
    /// [功能描述: 出院结算表现类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class ucOutBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutBalance()
        {
            InitializeComponent();
            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);

            this.fpCost.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCost_ButtonClicked);
            this.fpPrepay.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpPrepay_ButtonClicked);

            this.txtBalanceCostSplit.KeyDown += new KeyEventHandler(txtBalanceCostSplit_KeyDown);
            this.txtBalanceCostSplit.Leave += new EventHandler(txtBalanceCostSplit_Leave);
            this.ckOutBalance.CheckedChanged += new EventHandler(ckOutBalance_CheckedChanged);
            this.ckArrearBalance.CheckedChanged += new EventHandler(ckArrearBalance_CheckedChanged);
            this.ckArrearBalanceMid.CheckedChanged += new EventHandler(ckArrearBalanceMid_CheckedChanged);
        }

        #region 变量

        /// <summary>
        /// toolBarService
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();

        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 实体变量
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient();

        private FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();


        /// <summary>
        /// 结算总金额中的自费金额
        /// </summary>
        decimal TotalOwnCost = 0m;

        /// <summary>
        /// 结算总金额中的自付金额
        /// </summary>
        decimal TotalPayCost = 0m;

        /// <summary>
        /// 结算总金额中的公费金额
        /// </summary>
        decimal TotalPubCost = 0m;

        /// <summary>
        /// 总金额
        /// </summary>
        decimal TotalCost = 0m;

        /// <summary>
        /// 结算总金额
        /// </summary>
        decimal BalanceTotalCost = 0m;

        frmBalancePayNew frmBalancePay = new frmBalancePayNew();// {970D1FA7-19B2-4fad-992E-922156E3F10D}
        #endregion

        #region 属性
        FS.HISFC.Models.Base.EBlanceType balanceType = FS.HISFC.Models.Base.EBlanceType.Out;
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

        private int intCalcType = 0;
        [Category("控件设置"), Description("调用结算器类型 0:系统计算器 1:自定义计算器")]
        public int IntCalcType
        {
            get { return intCalcType; }
            set { intCalcType = value; }
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

        private string strZhongShanPactID = "";

        [Category("控件设置"), Description("中山医保合同单位(需要特殊处理IC卡扣费)，多个合同单位以|分隔-【废弃】")]
        public string ZhongShanPactID
        {
            set { this.strZhongShanPactID = value; }
            get { return this.strZhongShanPactID; }
        }

        #region 中山医保特殊处理,待优化
        
        /// <summary>
        /// 社保账户支付金额-PAY_COST
        /// </summary>
        decimal ICCost = 0m;

        /// <summary>
        /// 民政补助金额-该部分金额是包括在OWN_COST，通过支付方式区分
        /// </summary>
        decimal MZCost = 0m; 

        /// <summary>
        /// 中山医保住院合同单位
        /// </summary>
        private Hashtable hsZhongShanPactID = new Hashtable();

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 获取下一打印发票号
        /// </summary>
        private void GetNextInvoiceNO()
        {
            lblNextInvoiceNO.Text = "";
            FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            string invoiceNO = "";
            string realInvoiceNO = "";
            string errText = "";

            this.feeIntegrate.GetInvoiceNO(oper, "I", ref invoiceNO, ref realInvoiceNO, ref errText);

            if (string.IsNullOrEmpty(invoiceNO))
            {
                //未领取发票则弹出窗口输入
                FS.HISFC.Components.Common.Forms.frmUpdateInvoice frm = new FS.HISFC.Components.Common.Forms.frmUpdateInvoice();
                frm.InvoiceType = "I";
                frm.ShowDialog(this);

                int iReturn = this.feeIntegrate.GetInvoiceNO(oper, "I", ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturn == -1)
                {
                    CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                    return;
                }
            }

            lblNextInvoiceNO.Text = "电脑号： " + invoiceNO + ", 印刷号：" + realInvoiceNO;
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

            if (!string.IsNullOrEmpty(patientInfo.Memo))
            {
                if (DialogResult.No == MessageBox.Show("此患者标记备注信息为“" + patientInfo.Memo + "”" + System.Environment.NewLine + "\r\n是否继续结算？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
                {
                    return 0;
                }

            }
            ////判断患者状态
            //if (this.ValidPatient(this.patientInfo, ref errText) == -1)
            //{
            //    return -1;
            //}

            //获取医保登记信息
            if (patientInfo.Pact.PayKind.ID=="02")
            {
                //医保患者，获取医保主表信息
                FS.HISFC.BizLogic.Fee.Interface siMsg = new FS.HISFC.BizLogic.Fee.Interface();
                FS.HISFC.Models.RADT.PatientInfo siPatient = siMsg.GetSIPersonInfo(this.patientInfo.ID);
                if (siPatient !=null && !string.IsNullOrEmpty(siPatient.ID)&& siPatient.SIMainInfo.IsValid)
                {
                    this.patientInfo.SIMainInfo.RegNo = siPatient.SIMainInfo.RegNo;
                    this.patientInfo.SIMainInfo.HosNo = siPatient.SIMainInfo.HosNo;
                    this.patientInfo.SIMainInfo.EmplType = siPatient.SIMainInfo.EmplType;
                    this.patientInfo.SIMainInfo.InDiagnose.Name = siPatient.SIMainInfo.InDiagnose.Name;
                    this.patientInfo.SIMainInfo.InDiagnose.ID = siPatient.SIMainInfo.InDiagnose.ID;
                    this.patientInfo.SIMainInfo.AppNo = siPatient.SIMainInfo.AppNo;
                    this.patientInfo.IDCard = siPatient.IDCard;
                    this.patientInfo.CompanyName = siPatient.CompanyName;
                    this.patientInfo.Birthday = siPatient.Birthday;
                    this.patientInfo.Sex = siPatient.Sex;
                    if (!string.IsNullOrEmpty(this.patientInfo.Name) && this.patientInfo.Name.Trim() != siPatient.Name.Trim())
                    {
                        if (MessageBox.Show(this, "医保登记患者名字和在院登记名字不一致，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        {
                            return -1;
                        }
                    }
                }
                
            }

            //赋上住院号
            this.ucQueryInpatientNo.Text = patientInfo.PID.PatientNO;
            //赋值患者信息
            this.SetPatientInfo(patientInfo);

            if (patientInfo.PVisit.OutTime < new DateTime(1900, 1, 1))
            {
                errText = "该患者未做出院登记，请联系护士站，谢谢！";
                return -1;
            }

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

            //判断目前是否有婴儿未出院
            if (patientInfo.IsHasBaby)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                string motherPayAllFee = controlParamMgr.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Mother_PayAllFee, false, "0");
                if (motherPayAllFee == "0")//婴儿的费用收在妈妈的身上 
                {
                    //根据流水号查询所有的婴儿信息
                    ArrayList alBabies = this.radtIntegrate.QueryBabiesByMother(patientInfo.ID);
                    if (alBabies == null)
                    {
                        errText = "查询患者的婴儿失败，" + this.radtIntegrate.Err;
                        return -1;
                    }

                    foreach (FS.HISFC.Models.RADT.PatientInfo p in alBabies)
                    {
                        FS.HISFC.Models.RADT.PatientInfo baby = this.radtIntegrate.QueryPatientInfoByInpatientNO(p.ID);
                        if (baby == null)
                        {
                            errText = "查询患者的婴儿失败，" + this.radtIntegrate.Err;
                            return -1;
                        }
                        //说明有费用未结
                        if (p.FT.TotCost > 0)
                        {
                            errText = p.Name+"还存在未结费用，请先结算婴儿的费用";
                            return -1;
                        }
                    }
                }
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
            //出院结算判断状态
            string suretyCost = this.inpatientFeeManager.GetSurtyCost(patientInfo.ID);
            if (FS.FrameWork.Function.NConvert.ToDecimal(suretyCost) > 0)
            {
                errText = "患者有未返还的担保金:" + suretyCost + "元,请返还后再进行出院结算！";
                return -1;
            }

            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.B.ToString()) ||
                patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.C.ToString()))
            {
            }
            else
            {
                errText = "患者状态不是出院登记,不能进行出院结算！";
                return -1;
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
                if (CommonController.CreateInstance().MessageBox("存在未确认的退费申请，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
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
            decimal shouldPay = 0m;
            decimal returnCost = 0m;
            returnCost = decimal.Parse(this.txtReturnCost.Text);
            shouldPay = decimal.Parse(this.txtShouldPay.Text);
            supplyCost = decimal.Parse(this.txtShouldPay.Text);
            balanceCost = decimal.Parse(this.txtBalanceCost.Text);
            if (balanceCost <= 0)
            {
                errText = "结算金额必须大于零!";
                return false;
            }
            //splitMannalcost = decimal.Parse(this.txtMannalCost.Text);
            //if (splitMannalcost < 0)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg("手工分票金额必须大于零!", 111);
            //    return -1;
            //}

            //if (splitMannalcost >= balanceCost)
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg("手工分票金额不能大于或等于结算金额!", 111);
            //    return -1;
            //}
            
            //判断非欠费宰账
            if (this.ckOutCreditBalance.Checked == true)
            {
                if (this.patientInfo.Pact.PayKind.ID != "01")
                {
                    errText = "只有自费才能非欠费宰账!";
                    return false;
                }
                if (returnCost == 0 && shouldPay >= 0)
                {
                    errText = "非欠费宰账结算：退款金额必须大于0!";
                    return false;
                }
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
                this.neuIDNO.Text = "";
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
                //住院天数
                int days = radtMgr.GetInDays(patientInfo.ID);
                if (days>1)
                {
                    days -= 1;
                }
                string outDate=(patientInfo.PVisit.OutTime<new DateTime(1900,1,1))?"         ":patientInfo.PVisit.OutTime.ToString("yyyy-MM-dd");
                if (patientInfo.Pact.PayKind.ID == "03")
                {
                    FS.HISFC.Models.Base.PactInfo pact = this.PactManagment.GetPactUnitInfoByPactCode(patientInfo.Pact.ID);
                    string pactName = patientInfo.Pact.Name + " 自付比例：" + pact.Rate.PayRate * 100 + "%";//合同单位
                    this.lblPatientInfo.Text = "姓名:" + patientInfo.Name + " 入院日期:" + patientInfo.PVisit.InTime.ToString("yyyy-MM-dd") + " 出院日期:" + outDate + " 天数:" + days + "\r\n" + "结算类别:" + pactName + " 日限额：" + patientInfo.FT.DayLimitCost + " 出院诊断:" + diagnoseName + strBabyInfo;
                }
                else
                {
                    this.lblPatientInfo.Text = "姓名:" + patientInfo.Name + " 入院日期:" + patientInfo.PVisit.InTime.ToString("yyyy-MM-dd") + " 出院日期:" + outDate + " 天数:" + days + " 结算类别:" + patientInfo.Pact.Name + " 出院诊断:" + diagnoseName + strBabyInfo;
                }
                this.neuIDNO.Text = patientInfo.IDCard;
            }
        }

        /// <summary>
        /// 显示患者费用信息
        /// </summary>
        /// <returns>1成功 －1失败</returns>
        protected virtual int DisplayPatientCost(ref string errText)
        {
            this.ClearFee();

            //检索费用--含优惠
            if (this.QueryFeeInfo(ref errText) == -1) return -1;

            //检索预交金
            if (this.QueryPrepayInfo(ref errText) == -1) return -1;

            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtBalanceCost.Text) == 0)
            {
                errText = "该患者没有可结算费用";
                return -1;
            }

            ArrayList alFeeList = this.QueryFeeItemlist(this.patientInfo.ID, ref errText);
            if (alFeeList == null)
            {
                return -1;
            }

            if (Function.PreBalanceInpatient(this.patientInfo, alFeeList, ref errText) < 0)
            {
                return -1;
            }

            this.txtBalanceCostSplit.Text = this.txtBalanceCost.Text;
            //计算费用显示返还情况
            this.ComputeSupplyCost();

            return 1;
        }

        /// <summary>
        /// 获取患者本次结算信息
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="fromDate"></param>
        /// <param name="ToDate"></param>
        /// <returns></returns>
        protected virtual ArrayList QueryFeeItemlist(string inpatientNO,  ref string errText)
        {
            //非药品明细
            ArrayList alItemList = new ArrayList();
            //药品明细
            ArrayList alMedicineList = new ArrayList();
            //查询
            alItemList = this.inpatientFeeManager.QueryItemListsForBalance(inpatientNO);
            if (alItemList == null)
            {
                errText = "查询患者非药品信息出错" + this.inpatientFeeManager.Err;
                return null;
            }

            alMedicineList = this.inpatientFeeManager.QueryMedicineListsForBalance(inpatientNO);
            if (alMedicineList == null)
            {
                errText = "查询患者药品信息出错" + this.inpatientFeeManager.Err;
                return null;
            }
            ArrayList alFeeItemLists = new ArrayList();
            //添加汇总信息
            alFeeItemLists.AddRange(alItemList);
            alFeeItemLists.AddRange(alMedicineList);

            return alFeeItemLists;

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

            if (Function.SplitFeeItem(patientInfo, al, ref errText))
            {
                al = this.inpatientFeeManager.QueryFeeInfosAndChangeCostGroupByMinFeeByInpatientNO(patientInfo.ID, "0");
            }
            else
            {
                MessageBox.Show(this, errText, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }


            //优惠总金额
            decimal RebateCost = 0m;

            this.fpCost_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo =  (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                //获取最小费用名称
                feeInfo.Item.MinFee.Name = CommonController.CreateInstance().GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                RebateCost += feeInfo.FT.RebateCost;

                this.fpCost_Sheet1.RowCount++;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Locked = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name + (feeInfo.SplitFeeFlag ? "(加收)" : string.Empty);
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.OrgCost].Value = feeInfo.FT.DefTotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.PubCost].Value = feeInfo.FT.PubCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.OwnCost].Value = feeInfo.FT.OwnCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.DerateCost].Value = feeInfo.FT.DerateCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo.Clone();
                this.fpCost_Sheet1.Rows[i].Tag = feeInfo;

                if (feeInfo.SplitFeeFlag)
                {
                    this.fpCost_Sheet1.Rows[i].ForeColor = Color.Red;
                }
            }

            //结算总金额赋值
            this.txtBalanceCost.Text = TotalCost.ToString("F2");
            this.txtRebateFee.Text = RebateCost.ToString("F2");

            decimal DerateCost = 0m;
            //检索减免金额
            DerateCost = this.inpatientFeeManager.GetTotDerateCost(this.patientInfo.ID);
            if (DerateCost < 0)
            {
                errText = "获取减免总费用出错!" + this.inpatientFeeManager.Err;
                return -1;
            }
            this.txtDeratefee.Text = DerateCost.ToString();
            this.txtDeratefee.ReadOnly = true;

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

            if (BalanceCost == 0)
            {
                CommonController.CreateInstance().MessageBox("结算总费用必须大于零！", MessageBoxIcon.Warning);
                return;
            }

            //添加合计项
            this.BalanceTotalCost = 0M;
            //获得各费用分项
            FS.HISFC.Models.Fee.Inpatient.FeeInfo f;
            for (int i = 0; i < this.fpCost_Sheet1.Rows.Count; i++)
            {
                if ((bool)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value == true)
                {
                    f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[i].Tag;
                    BalanceOwnCost += f.FT.OwnCost;
                    BalancePubCost += f.FT.PubCost;
                    BalancePayCost += f.FT.PayCost;
                    this.BalanceTotalCost += decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Text);
                }
            }


            this.TotalOwnCost = BalanceOwnCost;
            this.TotalPayCost = BalancePayCost;
            this.TotalPubCost = BalancePubCost;
            //医保
            if (this.patientInfo.Pact.PayKind.ID == "02")
            {
                this.TotalOwnCost = this.patientInfo.SIMainInfo.OwnCost;
                this.TotalPayCost = this.patientInfo.SIMainInfo.PayCost;
                this.TotalPubCost = this.patientInfo.SIMainInfo.PubCost;
                
                #region 中山医保特殊处理（临时处理,待优化）

                //ArrayList alZhongShanPactID = new ArrayList(this.strZhongShanPactID.Split('|'));
                //if (alZhongShanPactID.Contains(this.patientInfo.Pact.ID))
                //{
                //    this.lblICCost.Visible = true;
                //    this.txtICCost.Visible = true;

                //    decimal icCost = 0M;
                //    FS.FrameWork.Models.NeuObject objGZZFEJE = this.patientInfo.SIMainInfo.ExtendProperty["GZZFEJE"];
                //    FS.FrameWork.Models.NeuObject objGZZFUJE = this.patientInfo.SIMainInfo.ExtendProperty["GZZFUJE"];
                //    if (objGZZFEJE != null)
                //    {
                //        icCost += FS.FrameWork.Function.NConvert.ToDecimal(objGZZFEJE.Memo);
                //    }
                //    if (objGZZFUJE != null)
                //    {
                //        icCost += FS.FrameWork.Function.NConvert.ToDecimal(objGZZFUJE.Memo);
                //    }

                //    this.txtICCost.Text = icCost.ToString("F2");
                //    this.ICCost = icCost;
                //}

                if (this.hsZhongShanPactID.Contains(this.patientInfo.Pact.ID))
                {
                    //社保账户支付金额-PAY_COST
                    this.lblICCost.Visible = true;
                    this.txtICCost.Visible = true;
                    this.ICCost = this.patientInfo.SIMainInfo.PayCost;
                    this.txtICCost.Text = this.ICCost.ToString("F2");

                    //民政补助金额
                    this.lblMZCost.Visible = true;
                    this.txtMZCost.Visible = true;
                    this.MZCost = this.patientInfo.SIMainInfo.HosCost;
                    this.txtMZCost.Text = this.MZCost.ToString("F2");

                }

                #endregion
            }

            //获取实付金额

            //通过接口实现全部患者都统一用此算法
            //RealCost = BalanceCost - this.patientInfo.SIMainInfo.PubCost - DerateCost - BalanceRebateCost;
            //BalancePubCost = patientInfo.SIMainInfo.PubCost;
            RealCost = BalanceCost - this.TotalPubCost - DerateCost - BalanceRebateCost;
            BalancePubCost = TotalPubCost;
            this.txtOwnTot.Text = RealCost.ToString("F2");
            this.txtPubTot.Text = BalancePubCost.ToString("F2");

            //获取supplycost
            decimal SupplyCost = 0m;
            //if (this.patientInfo.Pact.PayKind.ID == "03")
            //{
            //    SupplyCost = RealCost - PrepayCost;
            //}
            //else
            //{
            //    SupplyCost = RealCost - this.patientInfo.SIMainInfo.PayCost - PrepayCost;
            //}
            SupplyCost = RealCost - PrepayCost;

            //清空各支付信息框
            this.txtShouldPay.Text = "0.00";
            this.txtReturnCost.Text = "0.00";
            this.txtTransPrepayCost.Text = "0.00";

            decimal splitInvoiceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
            //设置为可用
            if (SupplyCost >= 0)//补收
            {
                if (balanceType == EBlanceType.Out)
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
            //if (this.patientInfo != null && string.IsNullOrEmpty(patientInfo.ID) == false)
            //{
            //    //开帐
            //    if (this.feeManager.OpenAccount(this.patientInfo.ID) == -1)
            //    {
            //        CommonController.CreateInstance().MessageBox("开帐失败" + this.feeManager.Err, MessageBoxIcon.Warning);
            //        return;
            //    }
            //}
            //清空患者信息控件
            this.SetPatientInfo(null);
            this.ucQueryInpatientNo.Focus();

            this.ckOutBalance.CheckedChanged -= new EventHandler(ckOutBalance_CheckedChanged);
            this.balanceType = EBlanceType.Out;
            this.ckOutBalance.Checked = true;
            this.ckArrearBalance.Checked = false;
            this.ckArrearBalanceMid.Checked = false;
            this.ckSplitInvoice.Checked = false;
            this.ckOutCreditBalance.Checked = false;
            this.ckOutBalance.CheckedChanged += new EventHandler(ckOutBalance_CheckedChanged);
            frmBalancePay.Clear();

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

            //处理中山医保PAY_COST金额
            this.txtICCost.Text = "0.00";
            this.txtICCost.Visible = false;
            this.lblICCost.Visible = false;
            this.ICCost = 0m;

            //民政补助金额
            this.lblMZCost.Visible = false;
            this.txtMZCost.Visible = false;
            this.MZCost = 0m;
            this.txtMZCost.Text = "0.00";

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
            this.frmBalancePay.Clear();  //先清空
            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
            decimal shouldPayCost = decimal.Parse(this.txtShouldPay.Text);//应收金额大于零
            if (shouldPayCost>0)
            {
                 frmBalancePay.ReturnPay = 0;
            }
            decimal arrearBalanceCost = decimal.Parse(this.txtTransPrepayCost.Text);
            if (shouldPayCost+arrearBalanceCost> 0)
            {
                frmBalancePay.ReturnPay = 0;
                frmBalancePay.ArrearBalance = arrearBalanceCost;
                frmBalancePay.ShouldPay = shouldPayCost;
                frmBalancePay.PatientInfo = this.patientInfo;// {970D1FA7-19B2-4fad-992E-922156E3F10D}

                //处理中山医保PAY_COST金额
                if (this.ICCost > 0)
                {
                    frmBalancePay.BalancePayCost = this.ICCost;
                }
                else
                {
                    frmBalancePay.BalancePayCost = 0;
                }

                //民政补助金额
                if (this.MZCost > 0)
                {
                    frmBalancePay.BalanceMZCost = this.MZCost;
                }
                else
                {
                    frmBalancePay.BalanceMZCost = 0;
                }


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


            #region 结算费用

            decimal splitInvoiceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();

            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo;
            for (int i = 0; i < this.fpCost_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value == true)
                {
                    feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[i].Tag;
                    feeInfo.FT.BalancedCost = decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Text);

                    listFeeInfo.Add(feeInfo.Clone());

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
            decimal returnCost = decimal.Parse(this.txtReturnCost.Text);//应收金额大于零
            if (returnCost > 0)
            {
                if (balanceType == EBlanceType.OutCredit)
                {
                    FS.HISFC.Models.Fee.Inpatient.BalancePay pay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                    pay.TransKind.ID = "1";
                    pay.TransType = TransTypes.Positive;
                    pay.PayType.ID = "ZZ";
                    pay.PayType.Name = "宰账";
                    pay.FT.TotCost = returnCost;
                    pay.RetrunOrSupplyFlag = payTrace;
                    listBalancePay.Add(pay);
                }
                else
                {
                    frmBalancePay.Clear();

                    frmBalancePay.ArrearBalance = 0;
                    frmBalancePay.ShouldPay = 0;
                    frmBalancePay.ReturnPay = returnCost;

                    //处理中山医保PAY_COST金额
                    if (this.ICCost > 0)
                    {
                        frmBalancePay.BalancePayCost = this.ICCost;
                    }
                    else
                    {
                        frmBalancePay.BalancePayCost = 0;
                    }

                    //民政补助金额
                    if (this.MZCost > 0)
                    {
                        frmBalancePay.BalanceMZCost = this.MZCost;
                    }
                    else
                    {
                        frmBalancePay.BalanceMZCost = 0;
                    }


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
            }

            #endregion

            List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();

            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //balanceMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            //long returnValue = medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            //if (returnValue != 1)
            //{
            //    medcareInterfaceProxy.Rollback();
            //    errText = medcareInterfaceProxy.ErrMsg;
            //    return -1;
            //}

            if (this.balanceType == EBlanceType.Out)
            {
                //出院结算
                if (balanceMgr.OutBalance(this.patientInfo, this.balanceType, listFeeInfo, listPrepay, listBalancePay, this.ckSplitInvoice.Checked, splitInvoiceCost, ref listBalance) < 0)
                {
                    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else if(this.balanceType== EBlanceType.Owe)
            {
                //欠费结算
                if (balanceMgr.OutBalanceOwe(this.patientInfo, listFeeInfo, listPrepay, listBalancePay, this.ckSplitInvoice.Checked, splitInvoiceCost, ref listBalance) < 0)
                {
                    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    return -1;
                }
            }
            else if (this.balanceType == EBlanceType.OutCredit)
            {
                //非欠费宰账,退费金额显示"宰账+[金额]",支付方式不显示
                if (balanceMgr.OutCreditBalance(this.patientInfo, this.balanceType, listFeeInfo, listPrepay, listBalancePay, this.ckSplitInvoice.Checked, splitInvoiceCost, ref listBalance) < 0)
                {
                    CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                    return -1;
                }
            }

            ////医保结算
            //if (Function.BalanceInpatient(ref medcareInterfaceProxy,this.patientInfo, new ArrayList(listFeeInfo), false, ref errText) < 0)
            //{
            //    CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    return -1;
            //}

            //FS.FrameWork.Management.PublicTrans.Commit();

            //打印发票
            if (Function.PrintInvoice(this.patientInfo, listBalance, ref errText) < 0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                //FS.FrameWork.Management.PublicTrans.RollBack();
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 显示计算器
        /// </summary>
        /// <returns></returns>
        public int DisplayCalc()
        {
            int tempValue = IntCalcType;
            if (tempValue == 0)
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }
            else if (tempValue == 1)
            {
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(
                new FS.HISFC.Components.Common.Controls.ucCalc());
            }
            else
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }
            return 0;
        }

        /// <summary>
        /// 显示备注
        /// </summary>
        private void SetMemo()
        {
            frmMemo fm = new frmMemo();
            if (this.patientInfo != null && !string.IsNullOrEmpty(this.patientInfo.PID.PatientNO))
            {
                fm.PantientNo = this.patientInfo.PID.PatientNO;
            }
            fm.ShowDialog();

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

        void fpPrepay_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }

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
                this.ckArrearBalance.Checked = false;
                this.ckArrearBalanceMid.Checked = false;
                this.ckOutCreditBalance.Checked = false;
                this.balanceType = EBlanceType.Out;
                //变化金额
                this.ComputeSupplyCost();
            }
        }

        void ckArrearBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckArrearBalance.Checked)
            {
                this.ckOutBalance.Checked = false;
                this.ckArrearBalanceMid.Checked = false;
                this.ckOutCreditBalance.Checked = false;
                this.balanceType = EBlanceType.Owe;
                //变化金额
                this.ComputeSupplyCost();
            }
        }

        void ckArrearBalanceMid_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckArrearBalanceMid.Checked)
            {
                this.ckOutBalance.Checked = false;
                this.ckArrearBalance.Checked = false;
                this.ckOutCreditBalance.Checked = false;
                this.balanceType = EBlanceType.OweMid;
                //变化金额
                this.ComputeSupplyCost();
            }
        }

        void ckOutCreditBalance_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ckOutCreditBalance.Checked)
            {
                this.ckOutBalance.Checked = false;
                this.ckArrearBalanceMid.Checked = false;
                this.ckArrearBalance.Checked = false;
                this.balanceType = EBlanceType.OutCredit;
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

            #region 查找【中山医保】的合同单位，根据待遇算法DLL来作为查询条件。

            try
            {
                //?如果修改接口名字的情况，这里要修改 gumzh?
                ArrayList alZS = this.PactManagment.QueryPactUnitByDLLName("ZhongShanSI.dll");

                //中山医保
                if (alZS != null && alZS.Count > 0)
                {
                    this.hsZhongShanPactID = new Hashtable();
                    foreach (FS.FrameWork.Models.NeuObject obj in alZS)
                    {
                        if (!this.hsZhongShanPactID.ContainsKey(obj.ID))
                        {
                            this.hsZhongShanPactID.Add(obj.ID, obj);
                        }
                    }
                }

            }
            catch (Exception ex)
            { }

            #endregion
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
            toolBarService.AddToolButton("计算器", "调用计算器", (int)FS.FrameWork.WinForms.Classes.EnumImageList.J计算器, true, false, null);
            toolBarService.AddToolButton("备注", "设置患者备注信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);

            toolBarService.AddToolButton("结算明细清单", "打印结算明细清单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印清单, true, false, null);

            toolBarService.AddToolButton("医保结算单", "打印医保结算单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印执行单, true, false, null);

            toolBarService.AddToolButton("拆分费用", "拆分高收费的费用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C拆包, true, false, null);

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
                case "计算器":
                    this.DisplayCalc();
                    break;
                //2012-08-07
                case "结算明细清单":
                    this.PrintIBillPrint();
                    break;
                case "结算汇总清单":
                    this.PrintIBillPrintCollect();
                    break;
                case "医保结算单":
                    this.PrintIBillPrintSI();
                    break;
                case "备注":
                    this.SetMemo();
                    break;
                case "拆分费用":
                    this.SetMemo();
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
                this.GetNextInvoiceNO();
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
            object[] appendParams = new object[1];
            appendParams[0] = "0"; //未结算 
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint),1) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {
                BillPrint.SetData(this.patientInfo, this.inpatientFeeManager, ref errInfo, appendParams);
                    if (errInfo.Length !=0)
                    {
                        MessageBox.Show(errInfo);
                        return;
                    }
                    BillPrint.Print();
            }
        }
        private void PrintIBillPrintCollect()
        {
            string errInfo = string.Empty;
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint), 2) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {
                BillPrint.SetData(this.patientInfo, this.inpatientFeeManager, ref errInfo);
                if (errInfo.Length != 0)
                {
                    MessageBox.Show(errInfo);
                    return;
                }
                BillPrint.Print();
            }
        }
        private void PrintIBillPrintSI()
        {
            string errInfo = string.Empty;
            object[] appendParams;
            string IfPreBalance = "0"; //0:不重新上传医保费用 1:重新上传医保费用
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint), 2) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {

                    appendParams = new object[1];
                    appendParams[0] = "0";
                BillPrint.SetData(this.patientInfo, this.inpatientFeeManager, ref errInfo, appendParams);
                if (errInfo.Length != 0)
                {
                    MessageBox.Show(errInfo);
                    return;
                }
                BillPrint.Print();
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.WinForms.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;
using FS.HISFC.Models.Base;
using FS.SOC.HISFC.BizProcess.CommonInterface.Attributes;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    /// <summary>
    /// 费用列枚举
    /// </summary>
    public enum ColumnCost
    {
        Check,
        /// <summary>
        /// 最小费用
        /// </summary>
        MinFee,
        /// <summary>
        /// 原始金额
        /// </summary>
        OrgCost,
        /// <summary>
        /// 金额
        /// </summary>
        Cost,
        /// <summary>
        /// 结算金额
        /// </summary>
        BalanceCost,
        /// <summary>
        /// 公费金额
        /// </summary>
        PubCost,
        /// <summary>
        /// 自费金额
        /// </summary>
        OwnCost,
        /// <summary>
        /// 减免金额
        /// </summary>
        DerateCost
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
    /// [功能描述: 在院结算表现类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-4]<br></br>
    /// </summary>
    public partial class ucInBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInBalance()
        {
            InitializeComponent();

            this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);

            this.btnExecute.Click += new EventHandler(btnExecute_Click);

            this.fpCost.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpCost_ButtonClicked);
            this.fpPrepay.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpPrepay_ButtonClicked);

            this.txtBalanceCostSplit.KeyDown+=new KeyEventHandler(txtBalanceCostSplit_KeyDown);
            this.txtBalanceCostSplit.Leave += new EventHandler(txtBalanceCostSplit_Leave);
        }

        #region 变量

        /// <summary>
        /// toolBarService
        /// </summary>
        ToolBarService toolBarService = new ToolBarService();

        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 实体变量
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

        private FS.HISFC.BizLogic.Fee.InPatient inpatientFeeManager = new FS.HISFC.BizLogic.Fee.InPatient(); 
        protected FS.HISFC.BizProcess.Integrate.Pharmacy phamarcyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        private FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();

        /// <summary>
        /// 合同单位业务类
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo PactManagment = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        private DateTime dtBegin;
        private DateTime dtEnd;


        private bool IsPreBalance = false;

        /// <summary>
        /// 结算总金额中的自费金额,自付金额,公费金额
        /// </summary>
        decimal TotalOwnCost = 0m;
        decimal TotalPayCost = 0m;
        decimal TotalPubCost = 0m;

        decimal TotalCost = 0m;
        decimal BalanceTotalCost = 0m;

        frmBalancePay frmBalancePay = new frmBalancePay();
        #endregion

        #region 属性
        FS.HISFC.Models.Base.EBlanceType blanceType = FS.HISFC.Models.Base.EBlanceType.Mid;
        [Category("控件设置"), Description("结算类别Out:出院结算Mid:中途结算Owe:欠费结算欠费结算分为两种：一是按照预交金额结算，一是按照费用全额结算，由参数控制")]
        public FS.HISFC.Models.Base.EBlanceType BlanceType
        {
            get
            {
                return blanceType;
            }
            set
            {
                blanceType = value;
            }
        }

        bool isTransPrepay = false;
        [Category("控件设置"), Description("是否使用转押金，True使用，False不使用"),FSSetting()]
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

        /// <summary>
        /// 中山医保IC卡金额
        /// </summary>
        decimal ICCost = 0m;

        /// <summary>
        /// 中山医保住院合同单位
        /// </summary>
        private Hashtable hsZhongShanPactID = new Hashtable();

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
        protected virtual int QueryByPatientNO(string inpatientNO,ref string errText)
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
            if (this.ValidPatient(this.patientInfo,ref errText) == -1)
            {
                return -1;
            }
            if (patientInfo.Pact.PayKind.ID == "02")
            {
                //医保患者，获取医保主表信息
                FS.HISFC.BizLogic.Fee.Interface siMsg = new FS.HISFC.BizLogic.Fee.Interface();
                FS.HISFC.Models.RADT.PatientInfo siPatient = siMsg.GetSIPersonInfo(this.patientInfo.ID);
                if (siPatient != null && !string.IsNullOrEmpty(siPatient.ID) && siPatient.SIMainInfo.IsValid)
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

            this.txtDeratefee.ReadOnly = !IsInputDerateFee;

            if (this.feeManager.CloseAccount(this.patientInfo.ID)<0)
            {
                errText = "关账失败，原因：" + this.feeManager.Err;
                return -1;
            }

            //显示患者此次住院的费用、预交金、减免情况
            this.dtpEnd.Value = this.inpatientFeeManager.GetDateTimeFromSysDateTime();
            DateTime dtEnd = this.inpatientFeeManager.GetDateTimeFromSysDateTime();
            this.dtpEnd.Value = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 23, 59, 59);
            

            DateTime inDate = patientInfo.PVisit.InTime;

            //获取上次结算日期
            ArrayList alBalance = inpatientFeeManager.QueryBalancesByInpatientNO(patientInfo.ID, "I");
            if (alBalance != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.Balance balancedHead in alBalance)
                {
                    if (balancedHead.CancelType == FS.HISFC.Models.Base.CancelTypes.Valid &&
                        inDate < balancedHead.EndTime)
                    {
                        inDate = balancedHead.EndTime.AddSeconds(1);
                    }
                }
            }

            this.dtpBegin.Value = inDate;

            if (this.DisplayPatientCost(ref errText) == -1)
            {
                return -1;
            }

            if (this.blanceType == EBlanceType.OweMid)
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
        protected virtual int ValidPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo,ref string errText)
        {
            //已经出院的返回
            if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.O.ToString()))
            {
                errText = "患者已经出院结算!";
                return -1;
            }

            //中途结算判断在院状态
            if (this.BlanceType == FS.HISFC.Models.Base.EBlanceType.Mid)
            {

                #region 判断未确认的退费申请

                ArrayList applys = this.feeIntegrate.QueryReturnApplys(this.patientInfo.ID, false);
                if (applys == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg(feeIntegrate.Err));
                    return -1;
                }

                ArrayList alQuitDrug = this.phamarcyIntegrate.QueryDrugReturn("AAAA"/*this.operDept.ID*/, "AAAA", this.patientInfo.ID);
                if (alQuitDrug == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("获取退药申请信息发生错误"));
                    return -1;
                }

                if (applys.Count +alQuitDrug.Count>0) //存在退费申请提示是否需要做院登记
                {
                    string itemInfo = "项目:\r\n";
                    foreach (FS.HISFC.Models.Fee.ReturnApply returnApply in applys)
                    {
                        itemInfo += returnApply.Item.Name + "--(" + CommonController.Instance.GetDepartmentName(returnApply.ExecOper.Dept.ID) + ")" + "\r\n";
                    }
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alQuitDrug)
                    {
                        itemInfo += info.Item.Name + "--(" + CommonController.Instance.GetDepartmentName(info.StockDept.ID) + ")" + "\r\n";
                    }

                    MessageBox.Show("还有未确认的退费申请，请先确认退费申请再进行中途结算？" + itemInfo, "警告"
                             , MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    return -1;
                }

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

                if (patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.B.ToString()) ||
                    patientInfo.PVisit.InState.ID.ToString().Equals(FS.HISFC.Models.Base.EnumInState.C.ToString()))
                {
                }
                else
                {
                    errText = "患者状态不是出院登记,不能进行出院结算！";
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
            //FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();
            //if (feeManager.IsExistApplyFee(this.patientInfo.ID))
            //{
            //    if (BlanceType == EBlanceType.Mid)
            //    {
            //        errText = "存在未确认的退费申请或未确认的退药申请，请退费后再结算！";
            //        return -1;
            //    }
            //    else
            //    {
            //        if (CommonController.CreateInstance().MessageBox("存在未确认的退费申请，是否继续？", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            //        {
            //            return -1;
            //        }
            //    }
            //}

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
            //付给变量方便更新费用函数使用
            dtBegin = this.dtpBegin.Value;
            dtEnd = this.dtpEnd.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
            if (dtEnd < dtBegin)
            {
                errText = "结束时间小于开始时间，请重新输入！";
                this.dtpEnd.Value = this.inpatientFeeManager.GetDateTimeFromSysDateTime();
                return -1;
            }

            //中途结算不处理减免
            this.txtDeratefee.Text = "0.00";
            this.txtDeratefee.ReadOnly = true;

            //检索费用--含优惠
            if (this.QueryFeeInfo(dtBegin, dtEnd, ref errText) == -1) return -1;

            //检索预交金
            if (this.QueryPrepayInfo(ref errText) == -1) return -1;

            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtBalanceCost.Text) == 0)
            {
                errText = "该患者没有可结算费用";
                return -1;
            }

            ArrayList alFeeList = this.QueryFeeItemlist(this.patientInfo.ID, dtBegin, dtEnd, ref errText);
            if (alFeeList == null)
            {
                return -1;
            }
            //东莞医保要用
            this.patientInfo.Insurance.User01 = dtBegin.ToString();
            this.patientInfo.Insurance.User02 = dtEnd.ToString();
            if (IsPreBalance)
            {
                if (Function.PreBalanceInpatient(this.patientInfo, alFeeList, ref errText) < 0)
                {
                    return -1;
                }
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
        protected virtual ArrayList QueryFeeItemlist(string inpatientNO, DateTime fromDate, DateTime ToDate, ref string errText)
        {
            //非药品明细
            ArrayList alItemList = new ArrayList();
            //药品明细
            ArrayList alMedicineList = new ArrayList();

            if (this.blanceType == EBlanceType.Mid)//中途结算
            {
                //查询
                alItemList = this.inpatientFeeManager.QueryItemListsForBalance(inpatientNO, fromDate, ToDate);
                if (alItemList == null)
                {
                    errText = "查询患者非药品信息出错" + this.inpatientFeeManager.Err;
                    return null;
                }

                alMedicineList = this.inpatientFeeManager.QueryMedicineListsForBalance(inpatientNO, fromDate, ToDate);
                if (alMedicineList == null)
                {
                    errText = "查询患者药品信息出错" + this.inpatientFeeManager.Err;
                    return null;
                }

            }
            else//其他结算
            {
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
        protected virtual int QueryFeeInfo(DateTime dtFeeBegin, DateTime dtFeeEnd, ref string errText)
        {
            ArrayList al = new ArrayList();
            if (dtFeeBegin > dtFeeEnd)
            {
                errText = "起始时间大于终止时间,请重新输入时间!";
                return -1;
            }

            //检索feeinfo       
            al = this.inpatientFeeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.patientInfo.ID, dtFeeBegin, dtFeeEnd, "0");
            if (al == null)
            {
                errText = inpatientFeeManager.Err;
                return -1;
            }

            if (Function.SplitFeeItem(patientInfo, dtFeeBegin, dtFeeEnd, al, ref errText))
            {
                al = this.inpatientFeeManager.QueryFeeInfosGroupByMinFeeByInpatientNO(this.patientInfo.ID, dtFeeBegin, dtFeeEnd, "0");
            }
            else
            {
                return -1;
            }


            //优惠总金额
            decimal RebateCost = 0m;

            this.fpCost_Sheet1.RowCount = 0;
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)al[i];
                //获取最小费用名称
                feeInfo.Item.MinFee.Name = CommonController.CreateInstance().GetConstantName(EnumConstant.MINFEE, feeInfo.Item.MinFee.ID);
                TotalCost += feeInfo.FT.TotCost;
                RebateCost += feeInfo.FT.RebateCost;

                this.fpCost_Sheet1.RowCount++;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value = true;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.MinFee].Value = feeInfo.Item.MinFee.Name + (feeInfo.SplitFeeFlag ? "(加收)" : string.Empty); ;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.OrgCost].Value = feeInfo.FT.DefTotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value = feeInfo.FT.TotCost;
                this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag = feeInfo.Clone();
                this.fpCost_Sheet1.Rows[i].Tag = feeInfo;

                if (feeInfo.SplitFeeFlag)
                {
                    this.fpCost_Sheet1.Rows[i].ForeColor = Color.Red;
                }
            }

            //结算总金额赋值
            this.txtBalanceCost.Text = TotalCost.ToString("F2");


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
            if (this.BlanceType !=EBlanceType.Mid)
            {
                decimal  ChangePrepay = this.inpatientFeeManager.GetTotChangePrepayCost(patientInfo.ID);
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
                FS.HISFC.Models.Fee.Inpatient.Prepay prepay =  (FS.HISFC.Models.Fee.Inpatient.Prepay)al[i];
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
            if (this.patientInfo.ID == null || this.patientInfo.ID == string.Empty)
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

                if (this.hsZhongShanPactID.Contains(this.patientInfo.Pact.ID))
                {
                    this.lblICCost.Visible = true;
                    this.txtICCost.Visible = true;
                    this.ICCost = this.patientInfo.SIMainInfo.PayCost;
                    this.txtICCost.Text = this.ICCost.ToString("F2");
                }

                #endregion
            }

            //获取实付金额

            //通过接口实现全部患者都统一用此算法
            //RealCost = BalanceCost - this.patientInfo.SIMainInfo.PubCost - DerateCost - BalanceRebateCost;
            //BalancePubCost = patientInfo.SIMainInfo.PubCost;
            RealCost = BalanceCost - this.TotalPubCost - DerateCost - BalanceRebateCost;//前面已经累加了
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

            //设置为可用
            if (SupplyCost >= 0)//补收
            {
                this.txtShouldPay.Text = SupplyCost.ToString("F2");
            }
            else //应返 
            {
                if (this.IsTransPrepay) //是否使用转押金
                {
                    if (this.BlanceType == EBlanceType.Mid)
                    {
                        this.txtTransPrepayCost.Text = (-SupplyCost).ToString("F2");
                    }
                    else
                    {
                        this.txtReturnCost.Text = (-SupplyCost).ToString("F2");
                    }
                }
                else
                {
                    this.txtReturnCost.Text = (-SupplyCost).ToString("F2");
                }
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
            if (this.patientInfo != null && string.IsNullOrEmpty(patientInfo.ID)==false)
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
            this.ckSplitInvoice.Checked = false;
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
            string errText="";
            if (this.ValidPatient(this.patientInfo, ref errText)<0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            if (!this.ValidCost(ref errText))
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            List<FS.HISFC.Models.Fee.Inpatient.BalancePay> listBalancePay = new List<FS.HISFC.Models.Fee.Inpatient.BalancePay>();
            decimal shouldPayCost = decimal.Parse(this.txtShouldPay.Text);//应收金额大于零
            if (shouldPayCost > 0)
            {
                this.frmBalancePay.Clear(); 
                frmBalancePay.ReturnPay = 0;
                frmBalancePay.ShouldPay = shouldPayCost;
                if (this.ICCost > 0)
                {
                    frmBalancePay.BalancePayCost = this.ICCost;
                }
                else
                {
                    frmBalancePay.BalancePayCost = 0;
                }

                frmBalancePay.ShowDialog(this);
                if (frmBalancePay.ListBalancePay.Count==0)
                {
                    return -1;
                }
                if (frmBalancePay.ListBalancePay.Count > 0)
                {
                    listBalancePay.AddRange(frmBalancePay.ListBalancePay);
                }
            }

            #region 结算费用

            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listFeeInfo=new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
            List<FS.HISFC.Models.Fee.Inpatient.FeeInfo> listOldFeeInfo = new List<FS.HISFC.Models.Fee.Inpatient.FeeInfo>();
            FS.HISFC.Models.Fee.Inpatient.FeeInfo feeInfo;
            for (int i = 0; i < this.fpCost_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value == true)
                {
                    feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[i].Tag;
                    feeInfo.FT.BalancedCost = decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Text);
                    listFeeInfo.Add(feeInfo.Clone());
                    feeInfo = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Tag;
                    listOldFeeInfo.Add(feeInfo.Clone());
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

            string payTrace = decimal.Parse(this.txtShouldPay.Text) > 0 ? "1" : (decimal.Parse(this.txtReturnCost.Text) > 0 ? "2" : "3");
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
            //生成转押金的支付方式

             //转押金
            decimal transPrepay = decimal.Parse(this.txtTransPrepayCost.Text);
            if (transPrepay > 0)
            {
                prepay = new FS.HISFC.Models.Fee.Inpatient.Prepay();
                prepay.FT.PrepayCost = transPrepay;
                prepay.TransferPrepayState = "1";
                listPrepay.Add(prepay);

                //生成冲负的结算转押金
                FS.HISFC.Models.Fee.Inpatient.BalancePay pay = new FS.HISFC.Models.Fee.Inpatient.BalancePay();
                pay.TransKind.ID = "0";
                pay.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                pay.PayType.ID = "CA";
                pay.FT.TotCost = -transPrepay;
                pay.Qty = 1;
                pay.RetrunOrSupplyFlag = "1";

                listBalancePay.Add(pay);
            }

            //返还金额
            decimal returnCost = decimal.Parse(this.txtReturnCost.Text);//应收金额大于零
            if (returnCost > 0)
            {
                frmBalancePay.Clear();
                frmBalancePay.ShouldPay = 0;
                frmBalancePay.ReturnPay = returnCost;
                if (this.ICCost > 0)
                {
                    frmBalancePay.BalancePayCost = this.ICCost;
                }
                else
                {
                    frmBalancePay.BalancePayCost = 0;
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


            #endregion

            List<FS.HISFC.Models.Fee.Inpatient.Balance> listBalance = new List<FS.HISFC.Models.Fee.Inpatient.Balance>();
            FS.SOC.HISFC.InpatientFee.BizProcess.Balance balanceMgr = new FS.SOC.HISFC.InpatientFee.BizProcess.Balance();

            
            //在院结算
            if (balanceMgr.InBalance(this.patientInfo, listFeeInfo, listOldFeeInfo, listPrepay, listBalancePay, this.dtBegin, this.dtEnd,this.ckSplitInvoice.Checked, ref listBalance) < 0)
            {
                CommonController.CreateInstance().MessageBox(balanceMgr.Err, MessageBoxIcon.Warning);
                return -1;
            }

            //打印发票
            if (Function.PrintInvoice(this.patientInfo, listBalance, ref errText) < 0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                return -1;
            }

            return 1;
        }

        #endregion

        #region 事件

        void ucQueryInpatientNo_myEvent()
        {
            IsPreBalance = false;
            string errText = "";
            if (QueryByPatientNO(this.ucQueryInpatientNo.InpatientNo, ref errText) < 0)
            {
                this.Clear();
                if (!string.IsNullOrEmpty(errText))
                {
                    CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
                }
               
                this.ucQueryInpatientNo.Focus();
            }
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Exit(sender, null);
        }

        void btnExecute_Click(object sender, EventArgs e)
        {
            if (this.BlanceType == EBlanceType.Out) return;
            if (this.BlanceType == EBlanceType.Owe) return;
            if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }
            // 中结允许医保结算
            // if (this.patientInfo.Pact.PayKind.ID == "02") return;
            string errText = "";

            IsPreBalance = true;
            if (this.DisplayPatientCost(ref errText) < 0)
            {
                CommonController.CreateInstance().MessageBox(errText, MessageBoxIcon.Warning);
            }

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
            if (patientInfo == null||string.IsNullOrEmpty(this.patientInfo.ID))
            {
                return;
            }

            if (this.BlanceType != EBlanceType.Mid && this.blanceType != EBlanceType.ItemBalance)
            {
                return;
            }


            decimal PrepayCost = 0m;
            for (int i = 0; i < this.fpPrepay_Sheet1.RowCount; i++)
            {
                if ((bool)this.fpPrepay_Sheet1.Cells[i,(int)ColumnPrepay.Check].Value == true)
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
                decimal balanceCost = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBalanceCostSplit.Text);
                decimal balanceCostTemp = 0m;
                if (this.fpCost_Sheet1.RowCount <= 0)
                {
                    return;
                }
                if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.ID))
                {
                    return;
                }

                if (this.patientInfo.Pact.PayKind.ID == "02")
                {
                    CommonController.CreateInstance().MessageBox("医保患者不允许按金额结算", MessageBoxIcon.Warning);
                    return;
                }

                if (balanceCost < 0)
                {
                    this.txtBalanceCostSplit.Text = this.txtBalanceCost.Text;
                    balanceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
                }

                if (balanceCost > this.TotalCost || balanceCost == 0)
                {
                    this.txtBalanceCostSplit.Text = this.TotalCost.ToString("F2");
                    balanceCost = decimal.Parse(this.txtBalanceCostSplit.Text);
                }

                decimal Rate = balanceCost / this.TotalCost;

                //结算总金额
                decimal BalanceCost = balanceCost;

                //重新计算费用
                decimal RebateCost = 0m;
                FS.HISFC.Models.Fee.Inpatient.FeeInfo f;
                FS.HISFC.Models.Fee.Inpatient.FeeInfo fTemp;
                for (int i = 0; i < this.fpCost_Sheet1.RowCount; i++)
                {
                    f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[i].Tag;
                    fTemp = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Cells[i,(int)ColumnCost.Check].Tag;
                    if ((bool)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value == true)
                    {
                        balanceCostTemp = FrameWork.Function.NConvert.ToDecimal(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value.ToString());

                        this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value = FS.FrameWork.Public.String.FormatNumber(balanceCostTemp * Rate, 2);
                        if (this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value != this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Cost].Value)
                        {
                            f.FT.PubCost = FS.FrameWork.Public.String.FormatNumber((fTemp.FT.PubCost * Rate), 2);
                            f.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((fTemp.FT.PayCost * Rate), 2);
                            f.FT.RebateCost = FS.FrameWork.Public.String.FormatNumber((fTemp.FT.RebateCost * Rate), 2);
                            f.FT.OwnCost = decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value.ToString()) - f.FT.PubCost - f.FT.PayCost - f.FT.RebateCost;
                        }

                        balanceCostTemp = decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value.ToString());
                        RebateCost = FS.FrameWork.Public.String.FormatNumber((RebateCost + f.FT.RebateCost), 2);
                        if (balanceCost > 0)
                        {
                            balanceCost -= balanceCostTemp;
                        }
                    }
                }

                //平账，平到第一个选择的数据中
                if (Math.Abs(balanceCost) > 0)
                {
                    for (int i = 0; i < this.fpCost_Sheet1.RowCount; i++)
                    {
                        f = (FS.HISFC.Models.Fee.Inpatient.FeeInfo)this.fpCost_Sheet1.Rows[i].Tag;
                        if ((bool)this.fpCost_Sheet1.Cells[i, (int)ColumnCost.Check].Value == true)
                        {
                            balanceCostTemp = FrameWork.Function.NConvert.ToDecimal(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value.ToString());
                            if (balanceCostTemp + balanceCost <= 0)
                            {
                                continue;
                            }

                            this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value = balanceCostTemp + balanceCost;
                            f.FT.OwnCost = decimal.Parse(this.fpCost_Sheet1.Cells[i, (int)ColumnCost.BalanceCost].Value.ToString()) - f.FT.PubCost - f.FT.PayCost - f.FT.RebateCost;

                            break;
                        }
                    }
                }

                this.txtBalanceCost.Text = BalanceCost.ToString("F2");
                this.txtRebateFee.Text = RebateCost.ToString("F2");
                this.ComputeSupplyCost();
            }
        }

        void txtBalanceCostSplit_Leave(object sender, EventArgs e)
        {
            this.txtBalanceCostSplit_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }
        #endregion

        #region 重载

        protected override void OnLoad(EventArgs e)
        {
            this.GetNextInvoiceNO();
            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);
            base.OnLoad(e);

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
            // chenxin 2012-09-23 
            toolBarService.AddToolButton("结算明细清单", "打印结算明细清单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印清单, true, false, null);
            // chenxin 2012-10-27 
            toolBarService.AddToolButton("医保结算单", "打印医保结算单", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印执行单, true, false, null); 
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
                case "结算明细清单":
                    this.PrintIBillPrint();
                    break;
                case "医保结算单":
                    this.PrintIBillPrintSI();
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
            if (QueryByPatientNO(this.patientInfo.ID,ref errText) < 0)
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
        // 1:明细清单, 未实现:汇总清单,未实现:医嘱清单,2:医保结算单
        private void PrintIBillPrint()
        {
            string errInfo = string.Empty;
            object[] appendParams;
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint), 1) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {
                if (this.BlanceType == EBlanceType.Mid)//中途结算
                {
                    appendParams = new object[3];
                    appendParams[0] = "0";
                    appendParams[1] = this.dtBegin;
                    appendParams[2] = this.dtEnd;
                }
                else
                {
                    appendParams = new object[1];
                    appendParams[0] = "0";
                }
                BillPrint.SetData(this.patientInfo, this.inpatientFeeManager, ref errInfo, appendParams);
                if (errInfo.Length != 0)
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
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint), 4) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {
                if (this.BlanceType == EBlanceType.Mid)//中途结算
                {
                }
                else
                {
                    BillPrint.SetData(this.patientInfo, this.inpatientFeeManager, ref errInfo);
                }
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
            string IfPreBalance = "0"; //0:不重新上传医保费用 1:重新上传医保费用 暂时不用
            FS.SOC.HISFC.InpatientFee.Interface.IBillPrint BillPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.InpatientFee.Interface.IBillPrint), 2) as FS.SOC.HISFC.InpatientFee.Interface.IBillPrint;
            if (BillPrint != null)
            {
                //if (CommonController.CreateInstance().MessageBox("是否直接预览打印医保结算单？点击『否』将重新上传医保费用再打印", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    IfPreBalance = "0";
                //}
                //else
                //{
                //    IfPreBalance = "1"; 
                //}
                if (this.BlanceType == EBlanceType.Mid)//中途结算
                {
                    appendParams = new object[3];
                    appendParams[0] = "0";
                    appendParams[1] = this.dtBegin;
                    appendParams[2] = this.dtEnd;
                }
                else
                {
                    appendParams = new object[1];
                    appendParams[0] = "0";
                }
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

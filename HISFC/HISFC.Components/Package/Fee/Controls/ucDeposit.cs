using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Base;

namespace HISFC.Components.Package.Fee.Controls
{
    public partial class ucDeposit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 业务类

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();        
        /// <summary>
        /// 民族
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper NationHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 购买套餐管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package feePackageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Fee.Package();
        /// <summary>
        /// 押金管理类
        /// </summary>
        private FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit depositMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Deposit();        
        /// <summary>
        /// 账户管理
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Account.AccountPay accountPay = new FS.HISFC.BizProcess.Integrate.Account.AccountPay();
        /// <summary>
        /// 默认查询记录的跨度(以月份为单位)
        /// </summary>
        private int defaultQueryRange = 6;
        /// <summary>
        /// 默认查询记录的跨度(以月份为单位)
        /// </summary>
        public int DefaultQueryRange
        {
            get { return this.defaultQueryRange; }
            set { this.defaultQueryRange = value; }
        }
        /// <summary>
        /// 有效押金记录
        /// </summary>
        private ArrayList RecodeList = new ArrayList();
        /// <summary>
        /// 押金结清记录
        /// </summary>
        private ArrayList HistoryList = new ArrayList();
        /// <summary>
        /// 是否需要从数据库重新查
        /// </summary>
        private bool needQueryFromDateBase = false;

        #endregion

        #region
        /// <summary>
        /// 患者基本信息
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者基本信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            get { return this.patientInfo; }
            set
            {
                this.patientInfo = value;
                this.SetPatientInfo();
                this.SetDepositInfo();
            }
        }

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
        /// 押金缴纳窗口
        /// </summary>
        private Forms.frmDeposit FrmDeposit = new HISFC.Components.Package.Fee.Forms.frmDeposit();

        /// <summary>
        /// 支付方式选择
        /// </summary>
        private Forms.frmPayModeChoose frmPayMode = new HISFC.Components.Package.Fee.Forms.frmPayModeChoose();

        #endregion

        public ucDeposit()
        {
            InitializeComponent();
            addEvents();
            InitControls();
        }

        private void addEvents()
        {
            this.txtMarkNo.KeyDown += new KeyEventHandler(txtMarkNo_KeyDown);
            this.btnShow.Click += new EventHandler(btnShow_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.dtBegin.ValueChanged += new EventHandler(dtBegin_ValueChanged);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            this.needQueryFromDateBase = true;
        }

        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            this.needQueryFromDateBase = true;
        }

        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShow_Click(object sender, EventArgs e)
        {
            this.pnlPatient.Visible = !this.pnlPatient.Visible;
        }

        /// <summary>
        /// 根据时间和备注查询患者押金记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.delEvents();
            try
            {
                if (this.patientInfo == null || string.IsNullOrEmpty(this.patientInfo.PID.CardNO))
                {
                    this.addEvents();
                    MessageBox.Show("请先检索患者！");
                }

                if (this.needQueryFromDateBase)
                {
                    DateTime beginDate = this.dtBegin.Value.Date;
                    DateTime endDate = this.dtEnd.Value.Date.AddDays(1);
                    RecodeList = feePackageProcess.GetDepositsByPatientAndDate(this.patientInfo, beginDate, endDate);
                    HistoryList = feePackageProcess.GetCostDepositsByPatientAndDate(this.patientInfo, beginDate, endDate);
                    needQueryFromDateBase = false;
                }

                this.setFpData();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            this.addEvents();
        }

        private void delEvents()
        {
            this.txtMarkNo.KeyDown -= new KeyEventHandler(txtMarkNo_KeyDown);
            this.btnShow.Click -= new EventHandler(btnShow_Click);
            this.btnSearch.Click -= new EventHandler(btnSearch_Click);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControls()
        {
            //支付方式
            ArrayList payModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = payModes;

            //填充卡类型
            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            this.cmbCardType.AddItems(al);
            markHelper.ArrayObject = al;

            //会员卡等级
            ArrayList alAccountLevel = managerIntegrate.GetConstantList("MemCardType");
            this.cmbAccountLevel.AddItems(alAccountLevel);

            //证件类型
            this.cmbIdCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
            this.pnlPatient.Visible = false;
            this.btnShow.Tag = this.pnlPatient.Visible;
            this.ActiveControl = this.txtMarkNo;

            IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper(managerIntegrate.QueryConstantList("IDCard"));
            this.ucPatientInfo.Enabled = false;
            this.ucPatientInfo.IsShowTitle = false;
        }

        protected FS.FrameWork.WinForms.Forms.ToolBarService _toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            _toolBarService.AddToolButton("刷卡", "刷卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B报警, true, false, null);
            _toolBarService.AddToolButton("缴纳押金", "缴纳押金", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S收费, true, false, null);
            _toolBarService.AddToolButton("退押金", "退押金", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T退费, true, false, null);
            _toolBarService.AddToolButton("打印票据", "打印票据", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            _toolBarService.AddToolButton("刷新", "刷新", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S刷新, true, false, null);
            return _toolBarService;
         }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "刷卡":
                    string cardNo = "";
                    string error = "";
                    //{119F302E-69D9-445c-BF56-4109D975AD98}
                    if (FS.HISFC.Components.Registration.Function.OperMCard(ref cardNo, ref error) == -1)
                    {
                        MessageBox.Show(error, "错误");
                        return;
                    }
                    if(string.IsNullOrEmpty(cardNo))
                    {
                        return;
                    }
                    this.txtMarkNo.Text = ";" + cardNo;
                    KeyEventArgs tmp = new KeyEventArgs(Keys.Enter);
                    this.txtMarkNo_KeyDown(this.txtMarkNo, tmp);
                    break;
                case "缴纳押金":
                    this.SaveDeposit();
                    break;
                case "退押金":
                    this.CancelDeposit();
                    break;
                case "打印票据":
                    break;
                case "刷新":
                    this.RefreshDeposit();
                    break;
                default:
                    break;
            }
        }

        //{DE811397-687D-4725-AA88-A7153B24FB8A}
        protected override int OnPrint(object sender, object neuObject)
        {
            ArrayList InvoiceNO = new ArrayList();
            if (this.neuSpread1.ActiveSheet != this.fpSheetRecord || this.fpSheetRecord.ActiveRow == null)
            {
                MessageBox.Show("请选择未消费的押金记录进行打印！");
                return -1;
            }

            FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = this.fpSheetRecord.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Deposit;
            InvoiceNO.Add(deposit);

            FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice packageinvoiceprint = null;
            packageinvoiceprint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice)) as FS.HISFC.BizProcess.Interface.MedicalPackage.IDepositInvoice;
            if (packageinvoiceprint == null)
            {
                MessageBox.Show(FS.FrameWork.WinForms.Classes.UtilInterface.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            packageinvoiceprint.SetPrintValue(InvoiceNO);
            packageinvoiceprint.Print();
            return 0;
        }

        /// <summary>
        /// 交押金
        /// </summary>
        private void SaveDeposit()
        {
            this.FrmDeposit.Clear();
            if (this.AccountCardInfo == null ||
               this.PatientInfo == null)
            {
                MessageBox.Show("请输入患者信息");
                return;
            }

            this.FrmDeposit.AccountCardInfo = this.AccountCardInfo;
            this.FrmDeposit.PatientInfo = this.PatientInfo;
            this.FrmDeposit.ShowDialog();
            this.PatientInfo = this.PatientInfo;
        }

        /// <summary>
        /// 退押金
        /// </summary>
        private void CancelDeposit()
        {
            if (this.neuSpread1.ActiveSheet != this.fpSheetRecord || this.fpSheetRecord.ActiveRow == null)
            {
                MessageBox.Show("请选择未消费的押金记录进行退费！");
                return;
            }

            FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit = this.fpSheetRecord.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Deposit;

            if(deposit == null)
            {
                MessageBox.Show("获取当前选中押金信息失败，请重试！");
                return;
            }

            //事务，是否退入账户

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            string CancelPayMode = deposit.Mode_Code;
            //如果缴纳押金的方式不为账户，则可以选择退费途径
            if (deposit.Mode_Code != "YS" && deposit.Mode_Code != "DC")
            {
                this.frmPayMode.SetDefalutPaymode(CancelPayMode);
                this.frmPayMode.ShowDialog();
                CancelPayMode = this.frmPayMode.PayModeCode;

                if (string.IsNullOrEmpty(CancelPayMode))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("未选择退费方式！");
                    return;
                }
            }
            else
            {
                if(MessageBox.Show("此条押金缴费方式为会员账户，退费后费用将直接退回账户！","提示",MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    MessageBox.Show("未选择退费方式！");
                    return ;
                }
                if (accountPay.OutpatientPay(PatientInfo,
                                             deposit.Account,
                                             deposit.AccountType,
                                             deposit.AccountFlag == "0" ? deposit.Amount : 0,
                                             deposit.AccountFlag == "0" ? 0 : deposit.Amount,
                                             deposit.ID, PatientInfo,
                                             FS.HISFC.Models.Account.PayWayTypes.M,
                                             0) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("退费到账户失败，退费不成功！");
                    return;
                }
            }
            
            if (this.depositMgr.DepositCancel(deposit, deposit.Amount, CancelPayMode) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(this.depositMgr.Err);
                return;
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("退费成功");

            this.RefreshDeposit();
        }

        /// <summary>
        /// 刷新押金
        /// </summary>
        private void RefreshDeposit()
        {
            this.PatientInfo = this.PatientInfo;
        }

        /// <summary>
        /// 按回车检索患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMarkNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                TextBox queryControl = sender as TextBox;
                string QueryStr = queryControl.Text.Trim();
                bool isMarkNO = false;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                if (QueryStr[0] == ';')
                {
                    isMarkNO = true;
                    QueryStr = QueryStr.Substring(1, QueryStr.Length - 1);
                }

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                //病历号默认进行补全
                if (!isMarkNO)
                {
                    QueryStr = QueryStr.PadLeft(10, '0');
                }

                this.Clear();

                //病历号查询
                if (!isMarkNO)
                {
                    //{13E56BF8-16D4-48b9-AD1F-E352C3DDFD73}
                    System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountManager.GetMarkList(QueryStr, "ALL", "1");
                    if (cardList == null || cardList.Count < 1)
                    {
                        MessageBox.Show("病历号不存在！");
                        return;
                    }
                    this.AccountCardInfo = cardList[cardList.Count - 1];
                }
                else //卡号查询
                {
                    this.AccountCardInfo = accountManager.GetAccountCard(QueryStr, "Account_CARD");
                    if (this.AccountCardInfo == null || (this.AccountCardInfo.MarkStatus != FS.HISFC.Models.Account.MarkOperateTypes.Begin))
                    {
                        MessageBox.Show("卡号不存在或者已经作废！");
                        return;
                    }
                }

                this.PatientInfo = accountManager.GetPatientInfoByCardNO(this.AccountCardInfo.Patient.PID.CardNO);

                if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
                {
                    MessageBox.Show("未查询到在患者！");
                    return;
                }
            }
        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            try
            {
                this.delEvents();
                if (this.patientInfo == null || this.patientInfo.PID.CardNO == "")
                {
                    this.txtMarkNo.SelectAll();
                    this.txtMarkNo.Focus();
                    this.cmbCardType.Tag = string.Empty;
                    this.txtName.Text = string.Empty;
                    this.txtSex.Text = string.Empty;
                    this.txtAge.Text = string.Empty;
                    this.txtRace.Tag = string.Empty;
                    this.cmbIdCardType.Tag = string.Empty;
                    this.txtIdCardNO.Tag = string.Empty;
                    this.txtCountry.Text = string.Empty;
                    this.txtsiNo.Text = string.Empty;
                    this.cmbAccountLevel.Text = string.Empty;
                    throw new Exception();
                }

                this.ucPatientInfo.CardNO = this.PatientInfo.PID.CardNO;
                this.txtMarkNo.Text = this.PatientInfo.PID.CardNO;
                this.cmbCardType.Tag = this.accountCardInfo.MarkType.ID;
                this.txtName.Text = this.PatientInfo.Name;
                this.txtSex.Text = this.PatientInfo.Sex.Name;
                this.txtAge.Text = this.accountManager.GetAge(patientInfo.Birthday);
                FS.FrameWork.Models.NeuObject tempObj = null;
                tempObj = managerIntegrate.GetConstant(FS.HISFC.Models.Base.EnumConstant.NATION.ToString(), this.PatientInfo.Nationality.ID);
                if (tempObj != null)
                {
                    this.txtRace.Text = tempObj.Name;
                }
                this.cmbIdCardType.Tag = this.PatientInfo.IDCardType.ID;
                this.txtIdCardNO.Text = this.patientInfo.IDCard;
                tempObj = managerIntegrate.GetConstant(FS.HISFC.Models.Base.EnumConstant.COUNTRY.ToString(), this.PatientInfo.Country.ID);
                if (tempObj != null)
                {
                    this.txtCountry.Text = tempObj.Name;
                }
                this.txtsiNo.Text = this.PatientInfo.SSN;
                this.cmbAccountLevel.Tag = this.accountCardInfo.AccountLevel.ID;
            }
            catch
            {
            }
            this.addEvents();
        }

        /// <summary>
        /// 设置押金信息
        /// </summary>
        private void SetDepositInfo()
        {

            if (this.PatientInfo == null)
                return;

            if (RecodeList == null)
            {
                RecodeList = new ArrayList();
            }

            if (HistoryList == null)
            {
                HistoryList = new ArrayList();
            }

            RecodeList.Clear();
            HistoryList.Clear();

            //RecodeList = feePackageProcess.GetDepositsByPatient(this.patientInfo);
            //HistoryList = feePackageProcess.GetCostDepositsByPatient(this.patientInfo);

            DateTime beginDate = this.accountManager.GetDateTimeFromSysDateTime().AddMonths(-this.DefaultQueryRange).Date;
            DateTime endDate = this.accountManager.GetDateTimeFromSysDateTime().AddDays(1).Date;

            this.dtBegin.Value = beginDate;
            this.dtEnd.Value = endDate.AddDays(-1);
            this.needQueryFromDateBase = false;

            RecodeList = feePackageProcess.GetDepositsByPatientAndDate(this.patientInfo, beginDate, endDate);
            HistoryList = feePackageProcess.GetCostDepositsByPatientAndDate(this.patientInfo, beginDate, endDate);
            this.setFpData();
        }

        /// <summary>
        /// 设置列表信息显示
        /// </summary>
        private void setFpData()
        {
            try
            {

                this.fpSheetRecord.RowCount = 0;
                this.fpSheetHistory.RowCount = 0;
                DateTime beginDate = this.dtBegin.Value.Date;
                DateTime endDate = this.dtEnd.Value.Date.AddDays(1);
                string Memo = this.tbMemo.Text.Trim();

                #region 可用押金记录

                if (RecodeList != null)
                {
                    ///可用押金根据收取日期来过滤
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in RecodeList.Cast<FS.HISFC.Models.MedicalPackage.Fee.Deposit>()
                                                                                                  .Where(t => t.OperTime >= beginDate)
                                                                                                  .Where(t => t.OperTime < endDate)
                                                                                                  .Where(t => t.Memo.Contains(Memo)||string.IsNullOrEmpty(Memo)))
                    {
                        this.fpSheetRecord.Rows.Add(this.fpSheetRecord.RowCount, 1);
                        this.fpSheetRecord.Rows[this.fpSheetRecord.RowCount - 1].Tag = deposit;
                        FS.HISFC.Models.Account.AccountDetail tmp = new FS.HISFC.Models.Account.AccountDetail();

                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.ID].Value = deposit.ID;
                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.Amount].Value = deposit.Amount.ToString("F2");
                        string Name = string.Empty;
                        foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
                        {
                            if (paymode.ID == deposit.Mode_Code)
                            {
                                Name = paymode.Name;
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(Name))
                        {
                            this.fpSheetRecord.RowCount = 0;
                            throw new Exception("查找押金信息失败！");
                        }
                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.PayMode].Text = Name;

                        if (deposit.Mode_Code == "YS" || deposit.Mode_Code == "DC")
                        {
                            List<FS.HISFC.Models.Account.AccountDetail> accountList = accountManager.GetAccountDetail(deposit.Account, deposit.AccountType,"1");
                            if (accountList == null || accountList.Count == 0)
                            {
                                this.fpSheetRecord.RowCount = 0;
                                throw new Exception("查找押金信息失败！");
                            }

                            this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.Account].Text = accountList[0].AccountType.Name;
                            if (deposit.AccountFlag == "0")
                            {
                                this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.AccountFlag].Text = "基本账户";
                            }
                            else
                            {
                                this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.AccountFlag].Text = "赠送账户";
                            }
                        }

                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.Oper].Value = deposit.Oper.ToString();
                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.OperTime].Value = deposit.OperTime.ToString();
                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.RelatedNO].Value = deposit.OriginalClinic.ToString();
                        this.fpSheetRecord.Cells[this.fpSheetRecord.RowCount - 1, (int)RecordCols.Memo].Value = deposit.Memo;
                    }
                }

                #endregion

                #region 已结算押金记录

                if (HistoryList != null)
                {
                    //根据取消时间来显示
                    foreach (FS.HISFC.Models.MedicalPackage.Fee.Deposit deposit in HistoryList.Cast<FS.HISFC.Models.MedicalPackage.Fee.Deposit>()
                                                                                                  .Where(t => t.CancelTime >= beginDate)
                                                                                                  .Where(t => t.CancelTime < endDate)
                                                                                                  .Where(t => t.Memo.Contains(Memo) || string.IsNullOrEmpty(Memo)))
                    {
                        this.fpSheetHistory.Rows.Add(this.fpSheetHistory.RowCount, 1);
                        this.fpSheetHistory.Rows[this.fpSheetHistory.RowCount - 1].Tag = deposit;
                        if (deposit.Trans_Type == "1")
                        {
                            this.fpSheetHistory.Rows[this.fpSheetHistory.RowCount - 1].ForeColor = Color.Blue;
                        }
                        else
                        {
                            this.fpSheetHistory.Rows[this.fpSheetHistory.RowCount - 1].ForeColor = Color.Red;
                        }
                        FS.HISFC.Models.Account.AccountDetail tmp = new FS.HISFC.Models.Account.AccountDetail();

                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.ID].Value = deposit.ID;
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.Amount].Value = deposit.Amount.ToString("F2");
                        string Name = string.Empty;
                        foreach (FS.HISFC.Models.Base.Const paymode in this.helpPayMode.ArrayObject)
                        {
                            if (paymode.ID == deposit.Mode_Code)
                            {
                                Name = paymode.Name;
                                break;
                            }
                        }

                        if (string.IsNullOrEmpty(Name))
                        {
                            this.fpSheetRecord.RowCount = 0;
                            throw new Exception("查找押金信息失败！");
                        }
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.PayMode].Text = Name;

                        if (deposit.Mode_Code == "DC" || deposit.Mode_Code == "YS")
                        {
                            List<FS.HISFC.Models.Account.AccountDetail> accountList = accountManager.GetAccountDetail(deposit.Account, deposit.AccountType,"1");
                            if (accountList == null || accountList.Count == 0)
                            {
                                this.fpSheetHistory.RowCount = 0;
                                throw new Exception("查找押金消费信息失败！");
                            }

                            this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.Account].Text = accountList[0].AccountType.Name;
                            if (deposit.AccountFlag == "0")
                            {
                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.AccountFlag].Text = "基本账户";
                            }
                            else
                            {
                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.AccountFlag].Text = "赠送账户";
                            }
                        }

                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.Oper].Value = deposit.Oper.ToString();
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.OperTime].Value = deposit.OperTime.ToString();
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.RelatedNO].Value = deposit.OriginalClinic.ToString();
                        switch (deposit.DepositType)
                        {
                            case FS.HISFC.Models.MedicalPackage.Fee.DepositType.JYJ:
                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.OperType].Value = "交押金";
                                break;
                            case FS.HISFC.Models.MedicalPackage.Fee.DepositType.TCXF:

                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.OperType].Value = "套餐消费";
                                break;
                            case FS.HISFC.Models.MedicalPackage.Fee.DepositType.TYJ:

                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.OperType].Value = "退押金";
                                break;
                            case FS.HISFC.Models.MedicalPackage.Fee.DepositType.TCTF:
                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.OperType].Value = "套餐退费";
                                break;
                            //{F4EC0C51-BFAD-4e34-BF17-E2749E58CAE8}
                            case FS.HISFC.Models.MedicalPackage.Fee.DepositType.XFHH:
                                this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.OperType].Value = "消费返还";
                                break;
                            default:
                                throw new Exception("查找押金消费信息的操作类型失败！");
                        }
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.CostMemo].Value = deposit.Memo;
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.CancelOper].Value = deposit.CancelOper;
                        this.fpSheetHistory.Cells[this.fpSheetHistory.RowCount - 1, (int)HistoryCols.CancelOperTime].Value = deposit.CancelTime;
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                this.fpSheetRecord.RowCount = 0;
                this.fpSheetHistory.RowCount = 0;
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 清空信息
        /// </summary>
        private void Clear()
        {
            this.PatientInfo = null;
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum RecordCols
        {
            /// <summary>
            /// 单据号
            /// </summary>
            ID = 0,

            /// <summary>
            /// 金额
            /// </summary>
            Amount = 1,

            /// <summary>
            /// 支付方式
            /// </summary>
            PayMode = 2,

            /// <summary>
            /// 支付账号
            /// </summary>
            Account = 3,

            /// <summary>
            /// 账户类型
            /// </summary>
            AccountFlag = 4,

            /// <summary>
            /// 操作人
            /// </summary>
            Oper = 5,

            /// <summary>
            /// 操作时间
            /// </summary>
            OperTime = 6,

            /// <summary>
            /// 相关单据号
            /// </summary>
            RelatedNO = 7,

            /// <summary>
            /// 备注
            /// </summary>
            Memo = 8,
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum HistoryCols
        {
            /// <summary>
            /// 单据号
            /// </summary>
            ID = 0,

            /// <summary>
            /// 金额
            /// </summary>
            Amount = 1,

            /// <summary>
            /// 支付方式
            /// </summary>
            PayMode = 2,

            /// <summary>
            /// 支付账号
            /// </summary>
            Account = 3,

            /// <summary>
            /// 账户类型
            /// </summary>
            AccountFlag = 4,

            /// <summary>
            /// 操作人
            /// </summary>
            Oper = 5,

            /// <summary>
            /// 操作时间
            /// </summary>
            OperTime = 6,

            /// <summary>
            /// 相关单据号
            /// </summary>
            RelatedNO = 7,

            /// <summary>
            /// 操作类型
            /// </summary>
            OperType = 8,

            /// <summary>
            /// 消费说明
            /// </summary>
            CostMemo = 9,

            /// <summary>
            /// 消费或者取消人
            /// </summary>
            CancelOper = 10,

            /// <summary>
            /// 消费或者取消时间
            /// </summary>
            CancelOperTime = 11
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            // {D55B4DFA-DA91-42b0-8163-27036100E89E}
            if (e.KeyCode == Keys.Enter)
            {
                FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions frmQuery = new FS.HISFC.Components.Common.Forms.frmQueryPatientByConditions();

                string QueryStr = this.txtName.Text;

                if (string.IsNullOrEmpty(QueryStr))
                {
                    return;
                }

                frmQuery.QueryByName(QueryStr);
                frmQuery.ShowDialog();

                if (frmQuery.DialogResult == DialogResult.OK)
                {

                    this.txtMarkNo.Text = frmQuery.PatientInfo.PID.CardNO;
                    this.txtMarkNo.Focus();
                    if (!string.IsNullOrEmpty(this.txtMarkNo.Text))
                    {
                        TextBox queryControl = new TextBox();
                        queryControl.Text = this.txtMarkNo.Text;
                        txtMarkNo_KeyDown(queryControl, new KeyEventArgs(Keys.Enter));
                    }
                }
            }
        }
    }
}

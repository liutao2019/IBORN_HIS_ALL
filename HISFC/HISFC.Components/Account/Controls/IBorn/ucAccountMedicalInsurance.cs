using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Account;
using FS.HISFC.BizProcess.Interface.Account;
using FS.HISFC.Components.Account.Forms;
using FS.HISFC.BizProcess.Interface.Fee;
using FS.HISFC.Models.Base;
using FarPoint.Win.Spread;

namespace FS.HISFC.Components.Account.Controls.IBorn
{
    public partial class ucAccountMedicalInsurance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        #region 变量
        /// <summary>
        /// 账户实体
        /// </summary>
        private FS.HISFC.Models.Account.Account account = null;
        /// <summary>
        /// 账户明细实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountDetail accountDetail = null;
        /// <summary>
        /// 民族
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper NationHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 证件类型
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper IdCardTypeHelp = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 卡操作实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountCardRecord accountCardRecord = null;

        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();

        /// <summary>
        /// 账户交易实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountRecord accountRecord = null;


        List<AccountMedicalInsurance> medicalInsuranceList = null;

        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 费用综合业务层 
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService _toolbarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 门诊卡号
        /// </summary>
        HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// 错误信息
        /// </summary>
        string error = string.Empty;

        /// <summary>
        /// 入出转
        /// </summary>
        HISFC.BizProcess.Integrate.RADT radtInteger = new FS.HISFC.BizProcess.Integrate.RADT();

        /// <summary>
        /// 帮助类
        /// </summary>
        FS.FrameWork.Public.ObjectHelper markHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 门诊费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outPatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 患者信息业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtProcess = new FS.HISFC.BizProcess.Integrate.RADT();


        private string HeadMediaclNO = "";
        #endregion


        /// <summary>
        /// 
        /// </summary>
        public ucAccountMedicalInsurance()
        {
            InitializeComponent();
            Setcmbpackage();

            this.cmbpackage.SelectedValueChanged += new EventHandler(cmbpackage_SelectedValueChanged);


            this.fpChildPackage.EditChange += new EditorNotifyEventHandler(fpChildPackage_EditChange);



        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "医保结束审核":
                    {
                        //Examine();
                        break;
                    }
                case "清屏":
                    {
                        this.Clear();
                        break;
                    }

            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //_toolbarService.AddToolButton("医保结束审核", "医保结束审核", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            _toolbarService.AddToolButton("清屏", "清屏", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);

            return _toolbarService;
        }


        private void fpChildPackage_EditChange(object sender, EditorNotifyEventArgs e)
        {
            Control cell = e.EditingControl;

            if (e.Column == 6)
            {
                ExpItemMedical curPackagedetail = this.fpChildPackage_Sheet1.ActiveRow.Tag as ExpItemMedical;

                int oldQty = curPackagedetail.RtnQty;

                if (curPackagedetail == null || string.IsNullOrEmpty(curPackagedetail.ItemCode))
                {
                    return;
                }

                if (this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value == null || Int32.Parse(this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString()) <= 0)
                {
                    curPackagedetail.RtnQty = 0;

                    curPackagedetail.ConfirmQty = curPackagedetail.Qty - 0;

                    this.fpChildPackage_Sheet1.Cells[e.Row, e.Column + 1].Text = curPackagedetail.ConfirmQty.ToString();
                }
                else
                {
                    int rtn = Int32.Parse(this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                    if (rtn > curPackagedetail.Qty)
                    {
                        this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Text = curPackagedetail.RtnQty.ToString();
                        return;
                    }
                    curPackagedetail.RtnQty = rtn;
                    curPackagedetail.ConfirmQty = curPackagedetail.Qty - curPackagedetail.RtnQty;

                    this.fpChildPackage_Sheet1.Cells[e.Row, e.Column + 1].Text = curPackagedetail.ConfirmQty.ToString();
                }

                if (oldQty != curPackagedetail.RtnQty)
                {
                    this.fpChildPackage_Sheet1.Cells[e.Row, 0].Text = "true";
                }

            }

            if (e.Column == 14)
            {
                ExpItemMedical curPackagedetail = this.fpChildPackage_Sheet1.ActiveRow.Tag as ExpItemMedical;
                if (this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value != null)
                {
                    string memo = this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString();
                    this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Text = memo;
                    curPackagedetail.Memo = memo;
                }

            }

        }

        private void Examine()
        {
            string cardno = this.txtMarkNo.Text.Trim();
            string name = this.txtName.Text;
            if (medicalInsuranceList == null || medicalInsuranceList.Count == 0)
            {
                MessageBox.Show("【控费明细】没有数据可审核！！");
                return;
            }
            DialogResult result = MessageBox.Show("是否医保结束审核【" + name + "】", "核对审核信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {

            }
            else
            {
                return;
            }


            int i = this.accountManager.UpdateMedicalInsuranceByCardNo(cardno, accountManager.Operator.ID);
            if (i > 0)
            {
                MessageBox.Show("结束审核成功", "转移提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetAccountInfo();
            }
            else
            {
                MessageBox.Show("结束审核失败", "转移提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }



        }

        private void ucAccountIBorn_Load(object sender, EventArgs e)
        {
            Init();
        }




        private void Init()
        {
            //填充卡类型
            ArrayList al = managerIntegrate.GetConstantList("MarkType");
            markHelper.ArrayObject = al;


            //会员卡等级
            ArrayList alAccountLevel = managerIntegrate.GetConstantList("MemCardType");
            this.cmbAccountLevel.AddItems(alAccountLevel);

            //证件类型
            this.cmbIdCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));

            this.ActiveControl = this.txtMarkNo;
        }


        private void Setcmbpackage()
        {

            ArrayList list = new ArrayList();
            List<ItemMedical> itemmed = this.accountManager.QueryAllItemMedical("ALL");
            foreach (ItemMedical item in itemmed)
            {
                FS.HISFC.Models.Base.Const con = new Const();
                con.ID = item.PackageId;
                con.Name = item.PackageName;
                list.Add(con);
            }

            this.cmbpackage.AddItems(list);
        }


        /// <summary>
        /// 回车处理
        /// </summary>
        protected virtual void ExecCmdKey()
        {
            if (this.txtMarkNo.Focused)
            {
                GetAccountInfo();
                return;
            }

        }


        /// <summary>
        /// 输入就诊卡号获取账户信息
        /// </summary>
        private void GetAccountInfo()
        {
            accountCard = new FS.HISFC.Models.Account.AccountCard();
            string markNO = this.txtMarkNo.Text.Trim();
            if (markNO == string.Empty)
            {
                MessageBox.Show("请输入就诊卡号！");
                this.txtMarkNo.Focus();
                return;
            }
            int resultValue = accountManager.GetCardByRule(markNO, ref accountCard);
            if (resultValue <= 0)
            {
                MessageBox.Show(accountManager.Err);
                this.Clear();
                return;
            }

            ClearAccountInfo();
            this.txtMarkNo.Text = accountCard.MarkNO;

            this.cmbAccountLevel.Tag = accountCard.AccountLevel.ID;

            //显示患者信息
            ShowPatienInfo(accountCard.Patient);
            //01 为身份证号，在常数维护中维护
            if (this.cmbIdCardType.Tag != null && this.cmbIdCardType.Tag.ToString() != string.Empty && this.txtIdCardNO.Text.Trim() != string.Empty)
            {
                this.txtIdCardNO.Enabled = false;
                //this.cmbPayType.Focus();
            }
            else
            {
                this.txtIdCardNO.Enabled = true;
                this.cmbIdCardType.Tag = "01";//身份证号
                this.txtIdCardNO.Focus();
            }

            //查找账户信息
            this.GetAccountByMark();

            //预交金记录
            GetRecordToFp();

            //预交金历史记录
            GetHistoryRecordToFp();

            GetExpItemmedicalToFp("");

        }


        private void cmbpackage_SelectedValueChanged(object sender, EventArgs e)
        {
            string packageId = this.cmbpackage.SelectedItem.ID;

            GetExpItemmedicalToFp(packageId);

        }


        private void GetExpItemmedicalToFp(string packageid)
        {
            if (accountCard == null)
            {
                return;
            }

            List<ExpItemMedical> list = this.accountManager.QueryExpItemMedicalALLByCardNo(accountCard.MarkNO);

            if (!string.IsNullOrEmpty(packageid))
            {
                list = list.Where(a => a.PackageId == packageid).ToList();
            }

            SetChildPackage(list);

        }


        private void SetChildPackage(List<ExpItemMedical> list)
        {

            int count = 0;
            fpChildPackage_Sheet1.Rows.Count = 0;
            fpChildPackage_Sheet1.Rows.Count = list.Count;

            decimal monty = 0.0m;

            FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
            foreach (ExpItemMedical item in list)
            {
                fpChildPackage_Sheet1.Cells[count, 1].Text = item.ItemCode;
                fpChildPackage_Sheet1.Cells[count, 2].Text = item.ItemName;
                fpChildPackage_Sheet1.Cells[count, 3].Text = item.Qty.ToString();
                fpChildPackage_Sheet1.Cells[count, 4].Text = item.ItemSubcode;
                fpChildPackage_Sheet1.Cells[count, 5].Text = item.ItemSubname;
                fpChildPackage_Sheet1.Cells[count, 6].Text = item.RtnQty.ToString();
                fpChildPackage_Sheet1.Cells[count, 7].Text = item.ConfirmQty.ToString();
                fpChildPackage_Sheet1.Cells[count, 8].Text = item.UnitPrice.ToString();
                fpChildPackage_Sheet1.Cells[count, 9].Text = item.PackageName;
                fpChildPackage_Sheet1.Cells[count, 10].Text = item.CardNo;
                fpChildPackage_Sheet1.Cells[count, 11].Text = item.CreateEnvironment.OperTime.ToString();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = managerIntergrate.GetEmployeeInfo(item.CreateEnvironment.ID);

                if (empl == null)
                {
                    item.CreateEnvironment.Name = "";
                }
                else
                {
                    item.CreateEnvironment.Name = empl.Name;
                }

                fpChildPackage_Sheet1.Cells[count, 12].Text = item.CreateEnvironment.Name;
                fpChildPackage_Sheet1.Cells[count, 13].Value = item.CancelFlag == "1" ? "是" : "否";
                fpChildPackage_Sheet1.Cells[count, 14].Text = item.Memo;

                if (item.CancelFlag == "1")
                {
                    fpChildPackage_Sheet1.Rows[count].ForeColor = Color.Red;
                    fpChildPackage_Sheet1.Rows[count].Locked = true;
                }
                else
                {
                    monty += item.RtnQty * item.UnitPrice;
                }

                HeadMediaclNO = item.ItemMedicalHeadNo;

                fpChildPackage_Sheet1.Rows[count].Tag = item;
                count++;
            }

            this.labmonty.Text = monty.ToString();


        }



        /// <summary>
        /// 获取账户预交金历史数据
        /// </summary>
        private void GetHistoryRecordToFp()
        {

            medicalInsuranceList = this.accountManager.GetMedicalInsuranceByCardNo(accountCard.MarkNO);// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (medicalInsuranceList == null)
            {
                MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.SetFPMedicalInsurance(medicalInsuranceList, this.spHistory);
        }


        /// <summary>
        /// 根据门诊账户获取卡的交易记录
        /// </summary>
        /// <returns></returns>
        private void GetRecordToFp()
        {
            if (account == null) return;
            List<PrePay> list = this.accountManager.GetPrepayByAccountNOAndType(account.ID, "1", "0");// {B2FD84F8-BAAA-43a7-A8C2-96EBD3D55C3B}
            if (list == null)
            {
                MessageBox.Show(this.accountManager.Err, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] s = { "YF", "YFT", "YFZ", "YFZT", "YFQ" };

            list = list.Where(a => s.Contains(a.PayType.ID)).ToList();

            this.SetFPAccountRecord(list, this.neuSpread1_Sheet1);
        }


        /// <summary>
        /// 显示账户预交金数据
        /// </summary>
        /// <param name="list">预交金数据</param>
        private void SetFPAccountRecord(List<PrePay> list, FarPoint.Win.Spread.SheetView sheet)
        {
            int count = 0;
            sheet.Rows.Count = list.Count;
            decimal sumcon = 0.0m;

            foreach (PrePay prepay in list)
            {

                sheet.Cells[count, 0].Text = prepay.InvoiceNO;

                sheet.Cells[count, 1].Text = prepay.AccountType.Name;
                if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    sheet.Cells[count, 2].Text = "收取";
                }
                else
                {
                    if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    {
                        sheet.Cells[count, 2].Text = "返还";

                    }
                    else if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Extend)
                    {
                        sheet.Cells[count, 2].Text = "结清余额";
                    }
                    else if (prepay.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                    {
                        sheet.Cells[count, 2].Text = "重打";
                    }
                }
                if (prepay.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    sheet.Cells[count, 2].ForeColor = Color.Red;
                }
                sheet.Cells[count, 3].Text = prepay.BaseCost.ToString();
                sheet.Cells[count, 4].Text = prepay.DonateCost.ToString();
                sheet.Cells[count, 5].Text = prepay.PrePayOper.OperTime.ToString();
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
                sheet.Cells[count, 6].Text = prepay.PrePayOper.Name;
                sheet.Rows[count].Tag = prepay;
                prepay.PayType.Name = this.cmbPayType.GetNameByID(prepay.PayType.ID);
                sheet.Cells[count, 7].Text = prepay.PayType.Name;
                sheet.Cells[count, 8].Text = "";
                sheet.Cells[count, 9].Text = "";
                sheet.Cells[count, 10].Text = prepay.Memo;
                sheet.Rows[count].Tag = prepay;

                sumcon += prepay.BaseCost;
                count++;
            }

            this.txtVacancy.Text = sumcon.ToString("F2");
        }


        private void SetFPMedicalInsurance(List<AccountMedicalInsurance> list, FarPoint.Win.Spread.SheetView sheet)
        {
            int count = 0;
            sheet.Rows.Count = list.Count;

            foreach (AccountMedicalInsurance prepay in list)
            {

                sheet.Cells[count, 0].Text = prepay.Cardno;
                sheet.Cells[count, 1].Text = prepay.Name;
                sheet.Cells[count, 2].Text = prepay.Xmbh;
                sheet.Cells[count, 3].Text = prepay.Xmmc;
                sheet.Cells[count, 4].Text = prepay.Qty.ToString();
                sheet.Cells[count, 5].Text = prepay.Createtime.ToString();
                sheet.Cells[count, 6].Text = string.IsNullOrEmpty(prepay.State) || prepay.State == "0" ? "未审核" : "已审核";
                FS.HISFC.BizProcess.Integrate.Manager managerIntergrate = new FS.HISFC.BizProcess.Integrate.Manager();
                FS.HISFC.Models.Base.Employee empl = new FS.HISFC.Models.Base.Employee();
                empl = managerIntergrate.GetEmployeeInfo(prepay.Operenvironment.ID);

                if (empl == null)
                {
                    prepay.Operenvironment.Name = "";
                }
                else
                {
                    prepay.Operenvironment.Name = empl.Name;
                }

                sheet.Cells[count, 7].Text = prepay.Operenvironment.Name;
                sheet.Cells[count, 8].Text = prepay.Operenvironment.OperTime.ToString();
                sheet.Rows[count].Tag = prepay;
                count++;
            }

        }


        /// <summary>
        /// 查找账户信息
        /// </summary>
        private void GetAccountByMark()
        {
            //检查账户信息
            this.account = this.accountManager.GetAccountByCardNoEX(accountCard.Patient.PID.CardNO);

            if (this.account != null)
            {
                List<AccountDetail> accountDetailList = new List<AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(this.account.ID, "1", "ALL");
                if (accountDetailList == null)
                {
                    MessageBox.Show("获取账户信息失败！" + this.accountManager.Err);
                    return;
                }
                if (accountDetailList.Count <= 0)
                {
                    this.txtVacancy.Text = "0.0";
                    return;
                }
                this.accountDetail = accountDetailList[0] as AccountDetail;


                this.cmbAccountLevel.Tag = this.account.AccountLevel.ID;
                this.cmbAccountLevel.Text = this.account.AccountLevel.Name;

            }
            else
            {
                this.txtVacancy.Text = "0.0";
            }
        }


        /// <summary>
        /// 显示患者基本信息
        /// </summary>
        /// <param name="CardNo"></param>
        private void ShowPatienInfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.txtName.Text = patient.Name;
            this.txtSex.Text = patient.Sex.Name;
            this.txtAge.Text = accountManager.GetAge(patient.Birthday);
            this.txtIdCardNO.Text = patient.IDCard;
            txtMarkNo.Text = patient.PID.CardNO;
            this.cmbIdCardType.Tag = patient.IDCardType.ID;
            FS.FrameWork.Models.NeuObject tempObj = null;
            tempObj = managerIntegrate.GetConstant(HISFC.Models.Base.EnumConstant.NATION.ToString(), patient.Nationality.ID);
            if (tempObj != null)
            {
                this.txtNation.Text = tempObj.Name;
            }
            tempObj = managerIntegrate.GetConstant(HISFC.Models.Base.EnumConstant.COUNTRY.ToString(), patient.Country.ID);
            if (tempObj != null)
            {
                this.txtCountry.Text = tempObj.Name;
            }
            this.txtsiNo.Text = patient.SSN;
        }

        /// <summary>
        /// 清除账户信息
        /// </summary>
        private void ClearAccountInfo()
        {
            this.txtVacancy.Text = "0.0";
            if (this.neuSpread1_Sheet1.Rows.Count > 0)
            {
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.Rows.Count);
            }
            if (this.spcard.Rows.Count > 0)
            {
                this.spcard.Rows.Remove(0, this.spcard.Rows.Count);
            }

            if (this.spHistory.Rows.Count > 0)
            {
                this.spHistory.Rows.Remove(0, this.spHistory.Rows.Count);
            }

            if (this.fpChildPackage_Sheet1.Rows.Count > 0)
            {
                this.fpChildPackage_Sheet1.Rows.Remove(0, this.fpChildPackage_Sheet1.Rows.Count);
            }

            this.account = null;
            accountRecord = null;
        }



        /// <summary>
        /// 清空显示数据
        /// </summary>
        private void Clear()
        {
            this.txtMarkNo.Text = string.Empty;
            this.txtMarkNo.Tag = string.Empty;
            this.txtName.Text = string.Empty;
            this.txtAge.Text = string.Empty;
            this.txtIdCardNO.Text = string.Empty;
            this.txtNation.Text = string.Empty;
            this.txtCountry.Text = string.Empty;
            this.txtsiNo.Text = string.Empty;
            this.accountCard = null;
            ClearAccountInfo();
            this.txtMarkNo.Focus();

        }




        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.txtName.Focused)
                {
                    txtName_KeyDown(new object(), new KeyEventArgs(Keys.Enter));
                }
                ExecCmdKey();

                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
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
                    ExecCmdKey();
                }
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (accountCard == null)
            {
                MessageBox.Show("请先选择客户再做操作！！");
                return;
            }

            DialogResult result = MessageBox.Show("确定修改【" + accountCard.Patient.Name + "】选中的套包吗", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                List<ExpItemMedical> itemdetails = new List<ExpItemMedical>();

                foreach (Row row in this.fpChildPackage_Sheet1.Rows)
                {
                    if (row.Tag != null &&
                       row.Tag is ExpItemMedical && this.fpChildPackage_Sheet1.Cells[row.Index, 0].Value != null &&
                       (bool)this.fpChildPackage_Sheet1.Cells[row.Index, 0].Value)
                    {
                        ExpItemMedical detail = (row.Tag as ExpItemMedical);

                        itemdetails.Add(detail);
                    }
                }
                bool isok = this.accountManager.UpdateExpItemMedical(accountCard.MarkNO, itemdetails);

                if (isok)
                {
                    GetAccountInfo();
                }
            }


        }


        /// <summary>
        /// 作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (this.cmbpackage.SelectedItem == null || accountCard == null)
            {
                MessageBox.Show("请选择客户或套包再做操作！！");
                return;
            }

            if (fpChildPackage_Sheet1.Rows.Count == 0)
            {
                MessageBox.Show("没有套包可作废！！");
                return;
            }


            string packageId = this.cmbpackage.SelectedItem.ID;
            DialogResult result = MessageBox.Show("是否作废" + accountCard.Patient.Name + "的【" + this.cmbpackage.SelectedItem.Name + "】", "请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                bool isok = this.accountManager.UpdateCancelFlag(accountCard.MarkNO, packageId, HeadMediaclNO);

                if (isok)
                {
                    MessageBox.Show("作废成功！！");

                    GetAccountInfo();
                }

            }
            else
            {
                return;
            }

        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            string datestar = this.dtstar.Text;

            string dateend = this.dtEnd.Text;

            if (accountCard == null)
            {
                return;
            }

            List<ExpItemMedical> list = this.accountManager.QueryExpItemMedicalALLByCardNoAndTime(accountCard.MarkNO, datestar, dateend);

            if (this.cmbpackage.SelectedItem != null)
            {
                string packageid = this.cmbpackage.SelectedItem.ID;

                if (!string.IsNullOrEmpty(packageid))
                {
                    list = list.Where(a => a.PackageId == packageid).ToList();
                }
            }

            SetChildPackage(list);

        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.HISFC.BizLogic.Fee;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Account;
using System.Collections;
using FS.HISFC.BizProcess.Interface.Account;

namespace FS.HISFC.Components.Account.Controls
{
    public partial class ucCardLoss : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 变量
        /// <summary>
        /// Acount业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 费用管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeManage = new FS.HISFC.BizProcess.Integrate.Fee();
        /// <summary>
        /// Manager业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 合同单位业务层
        /// </summary>
        private PactUnitInfo pactManager = new PactUnitInfo();
        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBar = new FS.FrameWork.WinForms.Forms.ToolBarService();
        ///// <summary>
        ///// 全局变量
        ///// </summary>
        //AccountCard cardRecord = null;
        /// <summary>
        /// 患者信息
        /// </summary>
        private HISFC.Models.RADT.PatientInfo oldPatient = new FS.HISFC.Models.RADT.PatientInfo();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        /// <summary>
        /// 就诊卡实体
        /// </summary>
        private HISFC.Models.Account.AccountCard accountCard = null;

        /// <summary>
        /// spread 各自样式实体
        /// </summary>
        FarPoint.Win.Spread.CellType.TextCellType cellType = new FarPoint.Win.Spread.CellType.TextCellType();
        /// <summary>
        /// 退卡时退费 0=不退，1=退费
        /// </summary>
        string ReturnCardReturnFee = string.Empty;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public ucCardLoss()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 控件加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCardLoss_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            Init();

            this.ReturnCardReturnFee = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.AccountConstant.ReturnCardReturnFee, false);

            this.neuSpread1.Visible = false;
            this.cmbPact.Click += new EventHandler(cmbPact_Click);
            this.cmbCardType.Click += new EventHandler(cmbCardType_Click);
            this.cmbSex.Click += new EventHandler(cmbSex_Click);
        }

        /// <summary>
        /// 性别提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSex_Click(object sender, EventArgs e)
        {
            this.neuSpread2.ActiveSheet = neuSpread2_Sheet2;
            ArrayList list = FS.HISFC.Models.Base.SexEnumService.List();
            DealConstantList(list);
        }

        /// <summary>
        /// 结束类型提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPact_Click(object sender, EventArgs e)
        {
            this.neuSpread2.ActiveSheet = neuSpread2_Sheet2;
            ArrayList list = managerIntegrate.QueryPactUnitAll();
            DealConstantList(list);
        }

        /// <summary>
        /// 证件类型提示信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCardType_Click(object sender, EventArgs e)
        {
            this.neuSpread2.ActiveSheet = neuSpread2_Sheet2;
            ArrayList list = managerIntegrate.QueryConstantList("IDCard");
            DealConstantList(list);
        }

        /// <summary>
        /// 下拉框加载显示提示信息
        /// </summary>
        /// <param name="consList">提示信息集合</param>
        private void DealConstantList(ArrayList consList)
        {

            if (consList == null || consList.Count <= 0)
            {
                return;
            }

            this.neuSpread2.Sheets[1].RowCount = 0;
            this.neuSpread2.Sheets[1].RowCount = (consList.Count / 3) + (consList.Count % 3 == 0 ? 0 : 1);

            int row = 0;
            int col = 0;

            foreach (FS.FrameWork.Models.NeuObject obj in consList)
            {
                if (col >= 5)
                {
                    col = 0;
                    row++;
                }

                this.neuSpread2.Sheets[1].SetValue(row, col, obj.ID);
                this.neuSpread2.Sheets[1].Cells[row, col].BackColor = Color.Pink;
                this.neuSpread2.Sheets[1].SetValue(row, col + 1, obj.Name);
                this.neuSpread2.Sheets[1].Cells[row, col].CellType = cellType;
                this.neuSpread2.Sheets[1].Cells[row, col + 1].CellType = cellType;
                col = col + 2;
            }
        }

        /// <summary>
        /// 初始各控件，并加载对应数据
        /// </summary>
        private void Init()
        {
            cellType.ReadOnly = true;


            //加载性别列表
            this.cmbSex.AddItems(FS.HISFC.Models.Base.SexEnumService.List());
            this.cmbSex.Text = "全部";

            //加载结算类型
            this.cmbPact.AddItems(managerIntegrate.QueryPactUnitAll());
            this.cmbPact.Tag = "1";

            //加载证件类型
            this.cmbCardType.AddItems(managerIntegrate.QueryConstantList("IDCard"));
            this.cmbCardType.Text = "身份证";
        }

        /// <summary>
        /// 初始功能按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBar.AddToolButton("患者信息查询", "患者信息查询", (int)FS.FrameWork.WinForms.Classes.EnumImageList.C查询, true, false, null);
            toolBar.AddToolButton("停卡", "停卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBar.AddToolButton("退卡", "退卡", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBar.AddToolButton("启用", "启用", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolBar;
        }

        /// <summary>
        /// 功能按键具体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "患者信息查询":
                    {
                        this.neuSpread2.ActiveSheet = neuSpread2_Sheet1;
                        QueryPatientInfo();
                        break;
                    }
                case "停卡":
                    {
                        StopCard();
                        break;
                    }
                case "退卡":
                    {
                        BackCard();
                        break;
                    }
                case "启用":
                    {
                        RecoverCard();
                        break;
                    }
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 通过合同单位编码,获得结算类别实体
        /// </summary>
        /// <param name="pactID">合同单位编码</param>
        /// <returns>成功: 结算类别实体 失败: null</returns>
        private PayKind GetPayKindFromPactID(string pactID)
        {
            FS.HISFC.Models.Base.PactInfo pact = this.pactManager.GetPactUnitInfoByPactCode(pactID);
            if (pact == null)
            {
                FS.HISFC.Models.Base.PactInfo pactTemp = new PactInfo();
                pact = pactTemp;
                return pact.PayKind;
            }

            return pact.PayKind;
        }

        /// <summary>
        /// 查询患者及卡信息
        /// </summary>
        protected virtual int QueryPatientInfo()
        {

            //获取患者的查询条件
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo.Name = this.txtName.Text.ToString();//姓名
            patientInfo.Sex.ID = this.cmbSex.Tag.ToString();//性别  
            patientInfo.Pact.ID = this.cmbPact.Tag.ToString();//合同单位
            patientInfo.Pact.PayKind = GetPayKindFromPactID(this.cmbPact.Tag.ToString());//结算类别
            patientInfo.IDCardType.ID = this.cmbCardType.Tag.ToString();//证件类型
            patientInfo.IDCard = this.txtIDNO.Text;//证件号
            patientInfo.PID.CardNO = this.txtCardNo.Text.ToString();//门诊卡号(病历号)
            patientInfo.PhoneHome = this.txtPhone.Text.Trim();//家庭电话

            //若没有填写任何查询条件则返回
            if (string.IsNullOrEmpty(patientInfo.Name) && string.IsNullOrEmpty(patientInfo.Sex.ID.ToString()) && string.IsNullOrEmpty(patientInfo.Pact.PayKind.ToString())
              && string.IsNullOrEmpty(patientInfo.IDCardType.ID) && string.IsNullOrEmpty(patientInfo.IDCard) && string.IsNullOrEmpty(patientInfo.PID.CardNO)
              && string.IsNullOrEmpty(patientInfo.PhoneHome))
            {
                return -1;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查找患者信息，请稍后...");
            Application.DoEvents();

            try
            {
                GetLostCardPatientInfo(patientInfo.Name, patientInfo.Sex, patientInfo.Pact.PayKind, patientInfo.IDCardType.ID, patientInfo.IDCard, patientInfo.PID.CardNO, patientInfo.PhoneHome);

            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show(ex.Message);
                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// 停卡功能
        /// </summary>
        private void StopCard()
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择需要停用的卡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AccountCard card = this.neuSpread1_Sheet1.ActiveRow.Tag as AccountCard;
            if (card == null)
            {
                MessageBox.Show("请选择需要停用的卡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (card.MarkStatus == MarkOperateTypes.Stop)
            {
                MessageBox.Show("该卡已停用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (card.MarkStatus == MarkOperateTypes.Cancel)
            {
                MessageBox.Show("该卡已回收！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("是否停用就诊卡【" + card.MarkNO + "】", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }

            int result = -1;

            string strNowTime = accountManager.GetSysDate();
            card.StopOper.ID = accountManager.Operator.ID;
            card.StopOper.Name = accountManager.Operator.Name;
            card.StopOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(strNowTime);

            card.MarkStatus = MarkOperateTypes.Stop;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                result = accountManager.StopBackAccountCard(card);
            }
            catch (Exception ex)
            {
                result = -1;
            }
            if (result == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("停卡操作失败！" + accountManager.Err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("停卡操作成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetNeuSpreadData(this.neuSpread1_Sheet1.ActiveRowIndex, card);

            return;

        }

        /// <summary>
        /// 退卡功能
        /// </summary>
        private void BackCard()
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择需要回收的卡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AccountCard card = this.neuSpread1_Sheet1.ActiveRow.Tag as AccountCard;
            if (card == null)
            {
                MessageBox.Show("请选择需要回收的卡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (card.MarkStatus == MarkOperateTypes.Cancel)
            {
                MessageBox.Show("该卡已回收！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("是否回收就诊卡【" + card.MarkNO + "】", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("请回收就诊卡【" + card.MarkNO + "】", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }

            int result = -1;

            DateTime nowDate = FS.FrameWork.Function.NConvert.ToDateTime(accountManager.GetSysDate());
            card.BackOper.ID = accountManager.Operator.ID;
            card.BackOper.Name = accountManager.Operator.Name;
            card.BackOper.OperTime = nowDate;

            card.MarkStatus = MarkOperateTypes.Cancel;

            // 记录卡费用
            List<AccountCardFee> lstCardFee = null;

            #region 计算退卡退费问题

            if (ReturnCardReturnFee == "1")
            {
                List<AccountCardFee> lstTempCardFee = null;
                decimal decMoney = 0;
                int iRes = accountManager.QueryAccountCardFee(card.Patient.PID.CardNO, card.MarkNO, card.MarkType.ID, out lstTempCardFee);
                if (iRes > 0 && lstTempCardFee != null && lstTempCardFee.Count > 0)
                {
                    lstCardFee = new List<AccountCardFee>();
                    AccountCardFee cardFee = null;
                    for (int idx = 0; idx < lstTempCardFee.Count; idx++)
                    {
                        if (lstTempCardFee[idx].IStatus != 1)
                        {
                            continue;
                        }
                        decMoney = lstTempCardFee[idx].Tot_cost;
                        if (decMoney <= 0)
                            continue;

                        cardFee = new AccountCardFee();
                        cardFee.InvoiceNo = lstTempCardFee[idx].InvoiceNo;
                        cardFee.Print_InvoiceNo = lstTempCardFee[idx].Print_InvoiceNo;
                        cardFee.Patient = card.Patient;
                        cardFee.MarkNO = card.MarkNO;
                        cardFee.MarkType = card.MarkType.Clone();
                        cardFee.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                        cardFee.Tot_cost = -decMoney;
                        cardFee.FeeOper = lstTempCardFee[idx].FeeOper;

                        cardFee.Oper.ID = card.BackOper.ID;
                        cardFee.Oper.Name = card.BackOper.Name;
                        cardFee.Oper.OperTime = nowDate;

                        cardFee.IsBalance = false;
                        cardFee.BalanceNo = "";
                        cardFee.IStatus = 2;

                        cardFee.FeeType = AccCardFeeType.CardFee;

                        lstCardFee.Add(cardFee);

                    }
                }
            }

            #endregion


            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                result = accountManager.StopBackAccountCard(card);
                if(result > 0)
                {
                    AccountCardFee cardFee = null;
                    for (int idx = 0; idx < lstCardFee.Count; idx++)
                    {
                        cardFee = lstCardFee[idx];

                        result = this.feeManage.SaveAccountCardFee(ref cardFee);
                        if (result <= 0)
                        {
                            break;
                        }
                        lstCardFee[idx] = cardFee;
                    }

                    
                }
            }
            catch (Exception ex)
            {
                result = -1;
            }
            if (result == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("停卡操作失败！" + accountManager.Err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            PrintCardFee(lstCardFee);

            MessageBox.Show("停卡操作成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetNeuSpreadData(this.neuSpread1_Sheet1.ActiveRowIndex, card);

            return;
        }
        /// <summary>
        /// 打印卡费用凭证
        /// </summary>
        /// <param name="lstCardFee"></param>
        private void PrintCardFee(List<AccountCardFee> lstCardFee)
        {
            if (lstCardFee == null)
                return;

            IPrintReturnCardFee iPrintReturn = FS.FrameWork.WinForms.Classes.
              UtilInterface.CreateObject(this.GetType(), typeof(IPrintReturnCardFee)) as IPrintReturnCardFee;

            if (iPrintReturn == null)
                return;

            foreach (AccountCardFee cardFee in lstCardFee)
            {
                iPrintReturn.SetValue(cardFee);
                iPrintReturn.Print();
            }
        }

        /// <summary>
        /// 启用功能（相对停卡而言）
        /// </summary>
        private void RecoverCard()
        {
            if (this.neuSpread1_Sheet1.ActiveRow == null)
            {
                MessageBox.Show("请选择需要启用的卡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            AccountCard card = this.neuSpread1_Sheet1.ActiveRow.Tag as AccountCard;
            if (card == null)
            {
                MessageBox.Show("请选择需要启用的卡", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (card.MarkStatus == MarkOperateTypes.Begin)
            {
                MessageBox.Show("该卡正常使用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (card.MarkStatus == MarkOperateTypes.Cancel)
            {
                MessageBox.Show("该卡已回收！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (DialogResult.Cancel == MessageBox.Show("是否启用就诊卡【" + card.MarkNO + "】", "系统提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                return;
            }

            int result = -1;

            card.StopOper.ID = "";
            card.StopOper.Name = "";
            card.StopOper.OperTime = DateTime.MinValue;

            card.MarkStatus = MarkOperateTypes.Begin;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            try
            {
                result = accountManager.StopBackAccountCard(card);
            }
            catch (Exception ex)
            {
                result = -1;
            }
            if (result == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("启用操作失败！" + accountManager.Err, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("启用操作成功！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetNeuSpreadData(this.neuSpread1_Sheet1.ActiveRowIndex, card);

            return;            
        }

        /// <summary>
        /// 获取当前激活card实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            int rowCount = neuSpread1.Sheets[0].RowCount;
            for (int n = 0; n < rowCount; n++)
            {
                if (n != e.Row)
                {
                    this.neuSpread1.Sheets[0].Rows[n].BackColor = Color.Empty;
                }
                else
                {
                    this.neuSpread1.Sheets[0].Rows[e.Row].BackColor = Color.Pink;
                }
            }
        }

        /// <summary>
        /// 显示符合条件的患者
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread2_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.neuSpread1.Visible = true;
            this.neuSpread1_Sheet1.RowCount = 0;
            int m = e.Row;

            FS.HISFC.Models.RADT.PatientInfo patientlost = this.neuSpread2_Sheet1.Rows[m].Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (patientlost == null)
            {
                patientlost = new FS.HISFC.Models.RADT.PatientInfo();
                patientlost.PID.CardNO = this.neuSpread2_Sheet1.Cells[m, 8].Text;
                patientlost.IDCard = this.neuSpread2_Sheet1.Cells[m, 4].Text;
                patientlost.IDCardType.ID = this.neuSpread2_Sheet1.Cells[m, 3].Text;
                patientlost.Name = this.neuSpread2_Sheet1.Cells[m, 0].Text;
                patientlost.PhoneHome = this.neuSpread2_Sheet1.Cells[m, 5].Text;
                patientlost.AddressBusiness = this.neuSpread2_Sheet1.Cells[m, 6].Text;
                patientlost.AddressHome = this.neuSpread2_Sheet1.Cells[m, 7].Text;
            }
            oldPatient = patientlost;
            GetLostCardbyPatient(patientlost);
        }


        /// <summary>
        /// 查看丢失卡的患者信息
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sex"></param>
        /// <param name="payKind"></param>
        /// <param name="idCardType"></param>
        /// <param name="IDcard"></param>
        /// <param name="cardNo"></param>
        /// <param name="PhoneHome"></param>
        private void GetLostCardPatientInfo(string name, SexEnumService sex, PayKind payKind, string idCardType, string IDcard, string cardNo, string PhoneHome)
        {
            //清除Spread
            this.neuSpread1.Sheets[0].RowCount = 0;

            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            patientInfo.Name = name;//姓名
            patientInfo.Sex = sex;//性别             
            patientInfo.Pact.PayKind = payKind;//结算类别
            patientInfo.IDCardType.ID = idCardType;//证件类型
            patientInfo.IDCard = IDcard;//证件号
            patientInfo.PID.CardNO = cardNo;//门诊卡号(病历号)
            patientInfo.PhoneHome = PhoneHome.Trim();//家庭电话
            //查找患者信息 
            List<FS.HISFC.Models.RADT.PatientInfo> patientlist = accountManager.QueryPatient(patientInfo.Name,
                                                        patientInfo.Sex.ID.ToString(),
                                                        patientInfo.Pact.ID,
                                                        patientInfo.IDCardType.ID,
                                                        patientInfo.IDCard,
                                                        patientInfo.PID.CardNO,
                                                        patientInfo.PhoneHome);

            if (patientlist == null)
            {
                if (!string.IsNullOrEmpty(accountManager.Err))
                {
                    MessageBox.Show(accountManager.Err);
                }
                return;
            }

            //患者资料
            this.neuSpread2.Sheets[0].RowCount = patientlist.Count;

            for (int m = 0; m < patientlist.Count; m++)
            {
                this.neuSpread2.Sheets[0].Cells[m, 0].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 1].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 2].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 3].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 4].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 5].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 6].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 7].CellType = cellType;
                this.neuSpread2.Sheets[0].Cells[m, 8].CellType = cellType;

                this.neuSpread2.Sheets[0].Cells[m, 0].Text = patientlist[m].Name;
                this.neuSpread2.Sheets[0].Cells[m, 1].Text = patientlist[m].Sex.ToString();
                this.neuSpread2.Sheets[0].Cells[m, 2].Text = patientlist[m].Age;
                this.neuSpread2.Sheets[0].Cells[m, 3].Text = patientlist[m].IDCardType.ID.ToString();
                this.neuSpread2.Sheets[0].Cells[m, 4].Text = patientlist[m].IDCard;
                this.neuSpread2.Sheets[0].Cells[m, 5].Text = patientlist[m].PhoneHome;
                this.neuSpread2.Sheets[0].Cells[m, 6].Text = patientlist[m].AddressBusiness;
                this.neuSpread2.Sheets[0].Cells[m, 7].Text = patientlist[m].AddressHome;
                this.neuSpread2.Sheets[0].Cells[m, 8].Text = patientlist[m].PID.CardNO;
                this.neuSpread2.Sheets[0].Rows[m].Tag = patientlist[m];

                
            }

        }

        /// <summary>
        /// 根据患者查找对应卡信息
        /// </summary>
        /// <param name="patient"></param>
        private void GetLostCardbyPatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            //清除Spread
            this.neuSpread1.Sheets[0].RowCount = 0;

            if (patient == null || patient.PID == null || string.IsNullOrEmpty(patient.PID.CardNO))
                return;


            //查找卡信息
            List<AccountCard> list = accountManager.GetLostAccountCard(patient.PID.CardNO);
 
            if (list == null)
            {
                if (!string.IsNullOrEmpty(accountManager.Err))
                {
                    MessageBox.Show(accountManager.Err);
                }
                return;
            }

            //患者对应资料
            this.neuSpread1.Sheets[0].RowCount = list.Count;

            AccountCard cardTemp = null;
            string strTemp = "";
            for (int m = 0; m < list.Count; m++)
            {
                cardTemp = list[m];
                cardTemp.Patient = patient;

                this.neuSpread1.Sheets[0].Cells[m, 0].Text = cardTemp.Patient.PID.CardNO;
                this.neuSpread1.Sheets[0].Cells[m, 1].Text = cardTemp.MarkNO;

                cardTemp.MarkType = managerIntegrate.GetConstansObj("MarkType", cardTemp.MarkType.ID);

                this.neuSpread1.Sheets[0].Cells[m, 2].Text = cardTemp.MarkType.Name;

                strTemp = "";
                switch (cardTemp.MarkStatus)
                {
                    case MarkOperateTypes.Begin:
                        strTemp = "有效";
                        break;
                    case MarkOperateTypes.Cancel:
                        strTemp = "已退卡";
                        break;
                    case MarkOperateTypes.Stop:
                        strTemp = "已停用";
                        break;
                }
                this.neuSpread1.Sheets[0].Cells[m, 3].Text = strTemp;

                strTemp = "";
                switch (cardTemp.ReFlag)
                {
                    case "0":
                        strTemp = "新发卡";
                        break;
                    case "1":
                        strTemp = "补发卡";
                        break;
                }
                this.neuSpread1.Sheets[0].Cells[m, 4].Text = strTemp;

                cardTemp.CreateOper.Name = managerIntegrate.GetEmployeeInfo(cardTemp.CreateOper.ID).Name;
                this.neuSpread1.Sheets[0].Cells[m, 5].Text = cardTemp.CreateOper.Name;
                this.neuSpread1.Sheets[0].Cells[m, 6].Text = cardTemp.CreateOper.OperTime.ToLongDateString();

                this.neuSpread1.Sheets[0].Rows[m].Tag = cardTemp;

                this.neuSpread1.Sheets[0].Cells[m, 0].CellType = cellType;
                this.neuSpread1.Sheets[0].Cells[m, 1].CellType = cellType;
                this.neuSpread1.Sheets[0].Cells[m, 2].CellType = cellType;
                this.neuSpread1.Sheets[0].Cells[m, 3].CellType = cellType;
                this.neuSpread1.Sheets[0].Cells[m, 4].CellType = cellType;
                this.neuSpread1.Sheets[0].Cells[m, 5].CellType = cellType;
                this.neuSpread1.Sheets[0].Cells[m, 6].CellType = cellType;
            }
        }

        private void SetNeuSpreadData(int index, AccountCard card)
        {
            this.neuSpread1.Sheets[0].Cells[index, 0].Text = card.Patient.PID.CardNO;
            this.neuSpread1.Sheets[0].Cells[index, 1].Text = card.MarkNO;

            //card.MarkType = managerIntegrate.GetConstansObj("MarkType", card.MarkType.ID);

            this.neuSpread1.Sheets[0].Cells[index, 2].Text = card.MarkType.Name;

            string strTemp = "";
            switch (card.MarkStatus)
            {
                case MarkOperateTypes.Begin:
                    strTemp = "有效";
                    break;
                case MarkOperateTypes.Cancel:
                    strTemp = "已退卡";
                    break;
                case MarkOperateTypes.Stop:
                    strTemp = "已停用";
                    break;
            }
            this.neuSpread1.Sheets[0].Cells[index, 3].Text = strTemp;

            strTemp = "";
            switch (card.ReFlag)
            {
                case "0":
                    strTemp = "新发卡";
                    break;
                case "1":
                    strTemp = "补发卡";
                    break;
            }
            this.neuSpread1.Sheets[0].Cells[index, 4].Text = strTemp;

            card.CreateOper.Name = managerIntegrate.GetEmployeeInfo(card.CreateOper.ID).Name;
            this.neuSpread1.Sheets[0].Cells[index, 5].Text = card.CreateOper.Name;
            this.neuSpread1.Sheets[0].Cells[index, 6].Text = card.CreateOper.OperTime.ToLongDateString();

            this.neuSpread1.Sheets[0].Rows[index].Tag = card;

        }
    }
}


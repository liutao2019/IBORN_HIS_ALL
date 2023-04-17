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
using FS.HISFC.Models.MedicalPackage.Fee;

namespace FS.HISFC.Components.Common.Forms
{
    //public delegate int DelegatePackageDetail(List<PackageDetail> packDetails);

    /// <summary>
    /// 套餐消费
    /// </summary>
    public partial class frmPackageCostOld : Form
    {

        #region 属性

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
                this.SetPackageInfo();
            }
        }

        /// <summary>
        /// 当前会员卡信息
        /// </summary>
        private FS.HISFC.Models.Account.AccountCard accountCardInfo = null;

        /// <summary>
        /// 套餐信息
        /// </summary>
        private Hashtable hsPackage = new Hashtable();

        /// <summary>
        /// 明细消费细项，以id-seq为key
        /// </summary>
        private Hashtable hsCostDetail = new Hashtable();

        /// <summary>
        /// 消费的明细
        /// </summary>
        private List<PackageDetail> costDetail = new List<PackageDetail>();

        /// <summary>
        /// 返回消费的明细
        /// </summary>
        public List<PackageDetail> CostDetail
        {
            get 
            {
                costDetail.Clear();  //清空
                foreach (string key in this.hsCostDetail.Keys)
                {
                    costDetail.Add(hsCostDetail[key] as PackageDetail);
                }
                return costDetail;
            }
        }

        /// <summary>
        /// 设置套餐结算支付方式
        /// </summary>
        public event DelegatePackageDetail SetPackPayMode;

        #endregion


        #region 业务类

        /// <summary>
        /// 账户管理类
        /// </summary>
        FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
        /// <summary>
        /// 合同单位业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.Package pckMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
        /// <summary>
        /// 套餐购买管理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail pckdMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.PackageDetail();
        /// <summary>
        /// 套餐理类
        /// </summary>
        FS.HISFC.BizLogic.MedicalPackage.Package packageMgr = new FS.HISFC.BizLogic.MedicalPackage.Package();
        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        #endregion 

        /// <summary>
        /// 构造函数
        /// </summary>
        public frmPackageCostOld()
        {
            InitializeComponent();
            InitControls();
            addEvent();
        }

        private void addEvent()
        {
            this.FpPackage.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpPackage_SelectionChanged);
            this.FpDetail.CellClick += new CellClickEventHandler(FpDetail_CellClick);
            this.FpDetail.EditModeOn += new EventHandler(FpDetail_EditModeOn);
            this.FpDetail.Change += new ChangeEventHandler(FpDetail_Change);
            this.FpDetail.Leave += new EventHandler(FpDetail_Leave);
        }

        private void delEvent()
        {
            this.FpPackage.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(fpPackage_SelectionChanged);
            this.FpDetail.CellClick -= new CellClickEventHandler(FpDetail_CellClick);
            this.FpDetail.EditModeOn -= new EventHandler(FpDetail_EditModeOn);
            this.FpDetail.Change -= new ChangeEventHandler(FpDetail_Change);
            this.FpDetail.Leave -= new EventHandler(FpDetail_Leave);
        }

        /// <summary>
        /// 初始化控件下拉列表
        /// </summary>
        private void InitControls()
        {
            //初始化合同单位
            ArrayList pactList = this.pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.pactManager.Err);
                return;
            }
            this.cmbPact.AddItems(pactList);

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
                MessageBox.Show("初始化证件类型列表出错!" + this.constantMgr.Err);

                return;
            }
            this.cmbCardType.AddItems(IDTypeList);

            //初始化会员级别
            ArrayList memberLevel = constantMgr.GetList("MemCardType");
            if (memberLevel == null)
            {
                MessageBox.Show("初始化会员类型列表出错!" + this.constantMgr.Err);

                return;
            }
            this.cmbLevel.AddItems(memberLevel);

            //FP热键屏蔽
            InputMap im;
            im = this.FpDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }

        /// <summary>
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.cmbCardType.Tag = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.cmbSex.Tag = string.Empty;
                this.tbAge.Text = string.Empty;
                this.cmbLevel.Tag = string.Empty;
                this.tbPhone.Text = string.Empty;
                this.cmbPact.Tag = string.Empty;

                this.tbMedicalNO.ReadOnly = true;
                this.tbName.ReadOnly = true;
                this.tbIDNO.ReadOnly = true;
                this.tbAge.ReadOnly = true;
                this.tbPhone.ReadOnly = true;

                return;
            }


            System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(this.PatientInfo.PID.CardNO, "Account_CARD", "1");
            if (cardList == null || cardList.Count < 1)
            {
                MessageBox.Show("病历号不存在！");
                return;
            }
            this.accountCardInfo = cardList[cardList.Count - 1];

            this.tbMedicalNO.Text = this.accountCardInfo.Patient.PID.CardNO;
            this.tbName.Text = patientInfo.Name;
            this.cmbCardType.Tag = this.patientInfo.IDCardType.ID;
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.cmbSex.Tag = patientInfo.Sex.ID;
            this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);
            this.cmbLevel.Tag = this.accountCardInfo.AccountLevel.ID;
            this.tbPhone.Text = this.patientInfo.PhoneHome;
            this.cmbPact.Tag = this.patientInfo.Pact.ID;
        }

        /// <summary>
        /// 设置套餐信息
        /// </summary>
        private void SetPackageInfo()
        {
            this.hsPackage.Clear();
            this.FpPackage_Sheet1.RowCount = 0;
            this.FpDetail_Sheet1.RowCount = 0;
            this.FpCost_Sheet1.RowCount = 0;

            if (this.patientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                return;
            }

            ArrayList packageList = this.pckMgr.QueryByCardNO(this.PatientInfo.PID.CardNO, "1", "0");
            if (packageList == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                package.PackageInfo = this.packageMgr.QueryPackageByID(package.PackageInfo.ID);
                this.FpPackage_Sheet1.AddRows(this.FpPackage_Sheet1.RowCount, 1);
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.Name].Text = package.PackageInfo.Name;
                this.FpPackage_Sheet1.Cells[this.FpPackage_Sheet1.RowCount - 1, (int)PackageCols.OperTime].Text = package.OperTime.ToString();
                this.FpPackage_Sheet1.Rows[this.FpPackage_Sheet1.RowCount - 1].Tag = package;
            }

            this.fpPackage_SelectionChanged(null, null);
        }

        /// <summary>
        /// 设置套餐明细
        /// </summary>
        private void SetPackageDetail(FS.HISFC.Models.MedicalPackage.Fee.Package package)
        {
            this.FpDetail_Sheet1.RowCount = 0;
            ArrayList details = null;//
            if (hsPackage.ContainsKey(package.ID))
            {
                details = hsPackage[package.ID] as ArrayList;
            }
            else
            {
                details = this.pckdMgr.QueryDetailByClinicNO(package.ID, "0");
                hsPackage.Add(package.ID, details);
            }

            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                decimal tmpQTY = detail.Item.Qty;
                if (detail.Item.ID.Substring(0, 1) == "Y")
                {
                    detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                }
                else
                {
                    detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                }
                detail.Item.Qty = tmpQTY;

                decimal costNum = 0.0m;
                if (this.hsCostDetail.ContainsKey(detail.ID + "-" + detail.SequenceNO))
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail cost = this.hsCostDetail[detail.ID + "-" + detail.SequenceNO] as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                    costNum = cost.Item.Qty;
                }

                this.FpDetail_Sheet1.AddRows(this.FpDetail_Sheet1.RowCount, 1);
                this.FpDetail_Sheet1.Rows[this.FpDetail_Sheet1.RowCount - 1].Tag = detail;
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.ItemName].Text = detail.Item.Name;
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.Spec].Text = detail.Item.Specs;
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.CostQty].Text = costNum.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.CostQty].Locked = false;
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.Unit].Text = detail.Unit;
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.Qty].Text = detail.Item.Qty.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.AvaliableQty].Text = detail.RtnQTY.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.ComfirmQty].Text = detail.ConfirmQTY.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.TotCost].Text = detail.Detail_Cost.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.RealCost].Text = detail.Real_Cost.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.GiftCost].Text = detail.Gift_cost.ToString();
                this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.RowCount - 1, (int)DetailCols.EtcCost].Text = detail.Etc_cost.ToString();
                this.FpDetail_Sheet1.Rows[this.FpDetail_Sheet1.RowCount - 1].Tag = detail;
            }
        }

        /// <summary>
        /// 套餐选择触发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.FpPackage_Sheet1.ActiveRow == null)
            {
                this.FpDetail_Sheet1.RowCount = 0;
                return;
            }
            FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.FpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
            this.SetPackageDetail(selectPackage);
        }

        /// <summary>
        /// cell点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpDetail_CellClick(object sender, CellClickEventArgs e)
        {
            this.FpDetail.SetActiveCell(e.Row, (int)DetailCols.CostQty);
        }


        /// <summary>
        /// 存储输入值的上一个值，用于的输入值非法时进行恢复
        /// </summary>
        private decimal previousValue = 0;

        /// <summary>
        /// 编辑模式开启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpDetail_EditModeOn(object sender, EventArgs e)
        {
            try
            {
                previousValue = decimal.Parse(this.FpDetail_Sheet1.Cells[this.FpDetail_Sheet1.ActiveRowIndex, this.FpDetail_Sheet1.ActiveColumnIndex].Value.ToString());
                this.FpDetail.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 当前编辑控件按键事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /// <summary>
        /// 值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpDetail_Change(object sender, ChangeEventArgs e)
        {
            this.delEvent();

            decimal costNum = decimal.Parse(this.FpDetail_Sheet1.Cells[e.Row, e.Column].Value.ToString());
            FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = this.FpDetail_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;

            ArrayList costArray = FS.HISFC.Components.Common.Classes.Function.CalculateCurrency(detail.Item.Qty, detail.Detail_Cost, detail.Real_Cost, detail.Gift_cost, detail.Etc_cost);
            //每一个细项的值分别存放于costArray里面
            if (costArray == null || costArray.Count != detail.Item.Qty)
            {
                this.addEvent();
                this.FpDetail_Sheet1.Cells[e.Row, e.Column].Value = previousValue;
                MessageBox.Show("获取费用明细失败！");
                return;
            }

            if (costNum > detail.RtnQTY + previousValue || costNum < 0)
            {
                this.FpDetail_Sheet1.Cells[e.Row, e.Column].Value = previousValue;
                MessageBox.Show("数量不能大于可用数量且不能小于0！");
            }
            else
            {

                FS.HISFC.Models.MedicalPackage.Fee.PackageDetail cost = null;
                decimal oldCostNum = 0.0m;
                if (this.hsCostDetail.ContainsKey(detail.ID + "-" + detail.SequenceNO))
                {
                    cost = this.hsCostDetail[detail.ID + "-" + detail.SequenceNO] as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                    oldCostNum = cost.Item.Qty;
                    detail.RtnQTY += oldCostNum;
                    detail.ConfirmQTY -= oldCostNum;
                }

                detail.RtnQTY -= costNum;
                detail.ConfirmQTY += costNum;
                this.FpDetail_Sheet1.Cells[e.Row, (int)DetailCols.AvaliableQty].Value = detail.RtnQTY;
                this.FpDetail_Sheet1.Cells[e.Row, (int)DetailCols.ComfirmQty].Value = detail.ConfirmQTY;
                if (cost == null)
                {
                    cost = new FS.HISFC.Models.MedicalPackage.Fee.PackageDetail();
                    cost = detail.Clone();
                }
                cost.Item.Qty = costNum;
                cost.RtnQTY = 0.0m;
                cost.ConfirmQTY = 0.0m;

                decimal totCost = 0.0m;
                decimal realCost = 0.0m;
                decimal giftCost = 0.0m;
                decimal etcCost = 0.0m;

                //根据可用数量推断出此次消费的是第几个至第几个细项
                for (int i = (int)(detail.Item.Qty - detail.RtnQTY - 1); i >= (int)(detail.Item.Qty - detail.RtnQTY - cost.Item.Qty); i--)
                {
                    decimal[] costdecimal = costArray[i] as decimal[];
                    totCost += costdecimal[0];
                    realCost += costdecimal[1];
                    giftCost += costdecimal[2];
                    etcCost += costdecimal[3];
                }

                cost.Detail_Cost = totCost;
                cost.Real_Cost = realCost;
                cost.Gift_cost = giftCost;
                cost.Etc_cost = etcCost;

                if (!this.hsCostDetail.ContainsKey(detail.ID + "-" + detail.SequenceNO))
                {
                    this.hsCostDetail.Add(detail.ID + "-" + detail.SequenceNO, cost);
                    this.FpCost_Sheet1.AddRows(this.FpCost_Sheet1.RowCount, 1);
                    this.FpCost_Sheet1.Rows[this.FpCost_Sheet1.RowCount - 1].Tag = cost;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.ItemName].Value = cost.Item.Name;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.Spec].Value = cost.Item.Specs;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.CostQty].Value = cost.Item.Qty;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.Unit].Value = cost.Unit;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.TotCost].Value = cost.Detail_Cost;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.RealCost].Value = cost.Real_Cost;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.GiftCost].Value = cost.Gift_cost;
                    this.FpCost_Sheet1.Cells[this.FpCost_Sheet1.RowCount - 1, (int)CostCols.EtcCost].Value = cost.Etc_cost;
                }
                else
                {
                    foreach(Row row in this.FpCost_Sheet1.Rows)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail rowcost = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                        if (rowcost.ID == detail.ID && rowcost.SequenceNO == detail.SequenceNO)
                        {
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.ItemName].Value = cost.Item.Name;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.Spec].Value = cost.Item.Specs;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.CostQty].Value = cost.Item.Qty;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.Unit].Value = cost.Unit;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.TotCost].Value = cost.Detail_Cost;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.RealCost].Value = cost.Real_Cost;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.GiftCost].Value = cost.Gift_cost;
                            this.FpCost_Sheet1.Cells[row.Index, (int)CostCols.EtcCost].Value = cost.Etc_cost;
                            
                            if (rowcost.Item.Qty == 0)
                            {
                                this.FpCost_Sheet1.Rows.Remove(row.Index, 1);
                                this.hsCostDetail.Remove(rowcost.ID + "-"+rowcost.SequenceNO);
                            }
                            break;
                        }
                    }
                }
            }

            this.addEvent();
        }

        /// <summary>
        /// 焦点离开套餐详情框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FpDetail_Leave(object sender, EventArgs e)
        {
            this.FpDetail.StopCellEditing();
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
                if (this.FpDetail.ContainsFocus)
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

            int currCol = this.FpDetail_Sheet1.ActiveColumnIndex;
            int currRow = this.FpDetail_Sheet1.ActiveRowIndex;
            if (key == Keys.Down || key == Keys.Enter)
            {
                if (this.FpDetail_Sheet1.ActiveRowIndex < this.FpDetail_Sheet1.RowCount - 1)
                {
                    this.FpDetail_Sheet1.SetActiveCell(currRow + 1, (int)DetailCols.CostQty);
                }
                else
                {
                    this.FpDetail.StopCellEditing();
                }
            }
        }

        /// <summary>
        /// 确定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.SetPackPayMode != null)
            {
                if (this.SetPackPayMode(this.CostDetail,false) < 0)
                {
                    //MessageBox.Show("获取套餐支付信息处错！");
                    return;
                }
            }

            this.Close();
        }

        #region 枚举

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PackageCols
        {
            /// <summary>
            /// 名称
            /// </summary>
            Name = 0,

            /// <summary>
            /// 够买时间
            /// </summary>
            OperTime = 1
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum DetailCols
        {
            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// 项目规格
            /// </summary>
            Spec = 1,

            /// <summary>
            /// 开立数量
            /// </summary>
            CostQty = 2,

            /// <summary>
            /// 开立单位
            /// </summary>
            Unit = 3,

            /// <summary>
            /// 总数量
            /// </summary>
            Qty = 4,

            /// <summary>
            /// 可用数量
            /// </summary>
            AvaliableQty = 5,

            /// <summary>
            /// 已确认数量
            /// </summary>
            ComfirmQty = 6,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 7,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 8,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 9,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 10
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum CostCols
        {
            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 0,

            /// <summary>
            /// 项目规格
            /// </summary>
            Spec = 1,

            /// <summary>
            /// 开立数量
            /// </summary>
            CostQty = 2,

            /// <summary>
            /// 开立单位
            /// </summary>
            Unit = 3,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 4,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 5,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 6,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 7
        }

        #endregion        
    }
}

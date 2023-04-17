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
   /// <summary>
   /// 
   /// </summary>
   /// <param name="packDetails">1</param>
   /// <param name="isOld">1</param>
   /// <returns></returns>
    public delegate int DelegatePackageDetail(List<PackageDetail> packDetails, bool isOld);

    /// <summary>
    /// 套餐消费
    /// {9D8048C5-1DC4-4dcd-9C2F-A3EF0B298C69}
    /// </summary>
    public partial class frmPackageCost : Form
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
        /// 非药品组合项目业务层
        /// {DD742F5E-B617-4068-B825-EECB4CAF4143}
        /// </summary>
        FS.HISFC.BizLogic.Fee.UndrugPackAge undrugPackAgeManager = new FS.HISFC.BizLogic.Fee.UndrugPackAge();

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
                    PackageDetail pcost = hsCostDetail[key] as PackageDetail;

                    costDetail.Add(hsCostDetail[key] as PackageDetail);
                }
                return costDetail;
            }
        }

        /// <summary>
        /// 费用明细
        /// </summary>
        protected ArrayList alFeeDetails = new ArrayList();


        /// <summary>
        /// 套餐内费用明细
        /// </summary>
        private ArrayList alPackageFeeDetails = new ArrayList();

        /// <summary>
        /// 收费明细
        /// </summary>
        public ArrayList AlFeeDetails
        {
            set
            {
                alFeeDetails = value;

                alPackageFeeDetails.Clear();
                if (alFeeDetails != null)
                {
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeitem in alFeeDetails)
                    {
                        if (feeitem.IsPackage == "1")
                        {
                            alPackageFeeDetails.Add(feeitem);
                        }
                    }
                }
            }
            get
            {
                return this.alFeeDetails;
            }
        }

        /// <summary>
        /// 设置套餐结算支付方式
        /// </summary>
        public event DelegatePackageDetail SetPackPayMode;

        /// <summary>
        /// 合同列表
        /// </summary>
        private ArrayList pactList = null;

        /// <summary>
        /// 性别
        /// </summary>
        private ArrayList sexList = null;

        /// <summary>
        /// 证件类型
        /// </summary>
        private ArrayList IDTypeList = null;

        /// <summary>
        /// 会员等级
        /// </summary>
        private ArrayList memberLevel = null;

        /// <summary>
        /// 套餐范围 {97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        private ArrayList rangeList = new ArrayList();

        /// <summary>
        /// 选择的套餐 {97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        private FS.HISFC.Models.Base.ServiceTypes selectedrange = FS.HISFC.Models.Base.ServiceTypes.A;
        /// <summary>
        /// {97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        private bool IsSelectedAll = false;
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
        public frmPackageCost()
        {
            InitializeComponent();
            this.InitControls();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void InitControls()
        {

            //初始化合同单位
            this.pactList = this.pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.pactManager.Err);
                return;
            }

            //初始化性别
            ArrayList sexListTemp = new ArrayList();
            sexListTemp = FS.HISFC.Models.Base.SexEnumService.List();
            sexList = new ArrayList();
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
            //初始化证件类型列表
            IDTypeList = constantMgr.GetList("IDCard");
            if (IDTypeList == null)
            {
                MessageBox.Show("初始化证件类型列表出错!" + this.constantMgr.Err);

                return;
            }

            //初始化会员级别
            memberLevel = constantMgr.GetList("MemCardType");
            if (memberLevel == null)
            {
                MessageBox.Show("初始化会员类型列表出错!" + this.constantMgr.Err);

                return;
            }

            //初始化套餐类型{97fbd080-edb9-f31f-41fd-33f7837851ba}
            this.rangeList = constantMgr.GetList("PACKAGERANGE");
            if (rangeList.Count == 0)
            {
                MessageBox.Show("初始化套餐类型列表出错!" + this.constantMgr.Err);

                return;
            }
            FS.FrameWork.Models.NeuObject objAllTmp = new FS.FrameWork.Models.NeuObject();
            objAllTmp.ID = "ALL";
            objAllTmp.Name = "全部";
            rangeList.Insert(0, objAllTmp);

            //初始化下拉框
            this.cmbTctype.AddItems(this.rangeList);
            this.cmbTctype.Text = FS.FrameWork.Management.Language.Msg("门诊套餐");


            //FP热键屏蔽
            InputMap im;
            im = this.fpPackageDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackageDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackageDetail.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);


            this.btnMatch.Click += new EventHandler(btnMatch_Click);
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.addEvent();
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
            if (this.alPackageFeeDetails == null || this.alPackageFeeDetails.Count == 0)
            {
                MessageBox.Show("套餐收费列表为空，匹配终止！");
                return;
            }

            //{DD742F5E-B617-4068-B825-EECB4CAF4143}
            foreach (Row row in this.fpPackageDetail_Sheet1.Rows)
            {
                this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value = 0;
            }

            //{BD46CBA6-A670-40cf-A1FD-EAFBC9797D04}
            Dictionary<string, string> tmpOrderID = new Dictionary<string, string>();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItemList in this.alPackageFeeDetails)
            {
                //{DD742F5E-B617-4068-B825-EECB4CAF4143}组合项目匹配
                if (!string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
                {
                    //说明当前套餐已被匹配
                    if (!tmpOrderID.ContainsKey(feeItemList.Order.ID))
                    {
                        tmpOrderID.Add(feeItemList.Order.ID, feeItemList.Order.ID);

                        //ArrayList undrugCombList = this.undrugPackAgeManager.QueryUndrugPackagesBypackageCode(feeItemList.UndrugComb.ID);

                        //if (undrugCombList == null)
                        //{
                        //    MessageBox.Show("获得组套明细出错!" + undrugPackAgeManager.Err);
                        //    return;
                        //}

                        //decimal qty = 0;
                        //foreach (FS.HISFC.Models.Fee.Item.UndrugComb undrugCombo in undrugCombList)
                        //{
                        //    if (undrugCombo.ID == feeItemList.ID)
                        //    {
                        //        qty = feeItemList.Item.Qty / undrugCombo.Qty;
                        //    }
                        //}
                        decimal qty = feeItemList.UndrugComb.Qty;
                        //{67E033DD-E953-4d3b-BD3A-B0A80E2D77D6}
                        //此处针对体检来的组套进行处理，体检没有传递组套的信息，而现在没办法修改体检接口
                        //因此临时在这里如果体检的组和数量为空，则调整为1（体检不允许开立同一个组合为2）
                        if (feeItemList.FTSource == "3" && !string.IsNullOrEmpty(feeItemList.UndrugComb.ID))
                        {
                            qty = 1;
                        }

                        foreach (Row row in this.fpPackageDetail_Sheet1.Rows)
                        {
                            FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                            if (feeItemList.UndrugComb.ID == detail.Item.ID)
                            {
                                decimal tmp = decimal.Parse(this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value.ToString());
                                //{D1E043F1-B598-4B8E-BEFB-8B4C48E56AB3} 更改匹配 知道匹配数量全部都匹配完了才进行下条
                                if (qty >= detail.RtnQTY - tmp)
                                {
                                    this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value = detail.RtnQTY;
                                    if (qty <= detail.RtnQTY - tmp) break;
                                    else qty = qty - detail.RtnQTY;
                                }
                                else
                                {
                                    this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value = qty + tmp;
                                }
                            }
                        }
                    }
                }
                else
                {
                    decimal qty = feeItemList.Item.Qty;
                    foreach (Row row in this.fpPackageDetail_Sheet1.Rows)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                        if (feeItemList.Item.ID == detail.Item.ID)
                        {
                            decimal tmp = decimal.Parse(this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value.ToString());
                            //   //{D1E043F1-B598-4B8E-BEFB-8B4C48E56AB3} 更改匹配 知道匹配数量全部都匹配完了才进行下条
                            if (qty >= detail.RtnQTY - tmp)
                            {
                                this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value = detail.RtnQTY;
                                if (qty <= detail.RtnQTY) break;
                                else qty = qty - detail.RtnQTY;
                            }
                            else
                            {
                                this.fpPackageDetail_Sheet1.Cells[row.Index, (int)DetailCols.CostQty].Value = feeItemList.Item.Qty + tmp;
                            }
                        }
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.SetPackPayMode != null)
            {
                bool isChecked = this.cbxCheckAll.Checked;
                if (this.SetPackPayMode(this.CostDetail, isChecked) < 0)
                {
                    return;
                }
            }

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        private void addEvent()
        {
            this.fpPackage.SelectionChanged += new SelectionChangedEventHandler(fpPackage_SelectionChanged);
            this.fpPackageDetail.CellClick += new CellClickEventHandler(fpPackageDetail_CellClick);
            this.fpPackageDetail.Change += new ChangeEventHandler(fpPackageDetail_Change);
            this.fpPackageDetail.Leave += new EventHandler(fpPackageDetail_Leave);
            this.fpPackageDetail_Sheet1.CellChanged += new SheetViewEventHandler(fpPackageDetail_Sheet1_CellChanged);
        }

        private void fpPackageDetail_Sheet1_CellChanged(object sender, SheetViewEventArgs e)
        {
            this.deleteEvent();
            try
            {
                decimal costNum = 0.0m;

                try
                {
                    costNum = decimal.Parse(this.fpPackageDetail_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                }
                catch
                {
                    costNum = 0.0m;
                }

                FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail = this.fpPackageDetail_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;

                ArrayList costArray = FS.HISFC.Components.Common.Classes.Function.CalculateCurrency(detail.Item.Qty, detail.Detail_Cost, detail.Real_Cost, detail.Gift_cost, detail.Etc_cost);
                //每一个细项的值分别存放于costArray里面
                if (costArray == null || costArray.Count != detail.Item.Qty)
                {
                    this.fpPackageDetail_Sheet1.Cells[e.Row, e.Column].Value = 0;
                    costNum = 0;
                    MessageBox.Show("获取费用明细失败！");
                }

                //数量不能为负数
                if (costNum > detail.RtnQTY || costNum < 0)
                {
                    this.fpPackageDetail_Sheet1.Cells[e.Row, e.Column].Value = 0;
                    costNum = 0;
                    MessageBox.Show("数量不能大于可用数量且不能小于0！");
                }

                FS.HISFC.Models.MedicalPackage.Fee.PackageDetail cost = null;
                string costKey = detail.ID + "-" + detail.SequenceNO;
                if (this.hsCostDetail.ContainsKey(costKey))
                {
                    cost = this.hsCostDetail[costKey] as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                }

                this.fpPackageDetail_Sheet1.Cells[e.Row, (int)DetailCols.AvaliableQty].Value = detail.RtnQTY - costNum;
                this.fpPackageDetail_Sheet1.Cells[e.Row, (int)DetailCols.ComfirmQty].Value = detail.ConfirmQTY + costNum;

                if (cost == null)
                {
                    cost = new FS.HISFC.Models.MedicalPackage.Fee.PackageDetail();
                    cost = detail.CloneAll();
                }

                cost.Item.Qty = costNum;
                cost.RtnQTY = 0.0m;
                cost.ConfirmQTY = 0.0m;

                decimal totCost = 0.0m;
                decimal realCost = 0.0m;
                decimal giftCost = 0.0m;
                decimal etcCost = 0.0m;

                //根据可用数量推断出此次消费的是第几个至第几个细项
                for (int i = (int)(detail.Item.Qty - detail.RtnQTY); i <= (int)(detail.Item.Qty - detail.RtnQTY + costNum - 1); i++)
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
                    if (cost.Item.Qty > 0)
                    {
                        this.hsCostDetail.Add(detail.ID + "-" + detail.SequenceNO, cost);
                        this.fpCost_Sheet1.AddRows(this.fpCost_Sheet1.RowCount, 1);
                        this.fpCost_Sheet1.Rows[this.fpCost_Sheet1.RowCount - 1].Tag = cost;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.PackageName].Value = cost.PackageName;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.ItemName].Value = cost.Item.Name;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.Spec].Value = cost.Item.Specs;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.CostQty].Value = cost.Item.Qty;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.Unit].Value = cost.Unit;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.TotCost].Value = cost.Detail_Cost;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.RealCost].Value = cost.Real_Cost;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.GiftCost].Value = cost.Gift_cost;
                        this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.EtcCost].Value = cost.Etc_cost;
                    }
                }
                else
                {
                    foreach (Row row in this.fpCost_Sheet1.Rows)
                    {
                        FS.HISFC.Models.MedicalPackage.Fee.PackageDetail rowcost = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                        if (rowcost.ID == detail.ID && rowcost.SequenceNO == detail.SequenceNO)
                        {
                            rowcost.Item.Qty = cost.Item.Qty;
                            this.fpCost_Sheet1.Cells[this.fpCost_Sheet1.RowCount - 1, (int)CostCols.PackageName].Value = cost.PackageName;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.ItemName].Value = cost.Item.Name;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.Spec].Value = cost.Item.Specs;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.CostQty].Value = cost.Item.Qty;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.Unit].Value = cost.Unit;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.TotCost].Value = cost.Detail_Cost;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.RealCost].Value = cost.Real_Cost;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.GiftCost].Value = cost.Gift_cost;
                            this.fpCost_Sheet1.Cells[row.Index, (int)CostCols.EtcCost].Value = cost.Etc_cost;

                            if (rowcost.Item.Qty == 0)
                            {
                                this.fpCost_Sheet1.Rows.Remove(row.Index, 1);
                                this.hsCostDetail.Remove(rowcost.ID + "-" + rowcost.SequenceNO);
                            }
                            break;
                        }
                    }
                }

            }
            catch
            {
            }
            this.addEvent();
        }

        /// <summary>
        /// 套餐选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void fpPackage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    this.deleteEvent();
        //    try
        //    {
        //        if (this.fpPackage_Sheet1.ActiveRow == null)
        //        {
        //            this.fpPackageDetail_Sheet1.RowCount = 0;
        //            throw new Exception();
        //        }
        //        FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
        //        this.SetPackageDetail(selectPackage);
        //    }
        //    catch
        //    { 
        //    }
        //    this.addEvent();
        //}

        public string PackageIDS { set; get; }

        public string ClinicCodes { set; get; }

        /// 套餐选择触发函数
        /// </summary>   //{D1E043F1-B598-4B8E-BEFB-8B4C48E56AB3} 选择多个套餐去匹配
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            // this.deleteEvent();
            if (this.fpPackage_Sheet1.ActiveRow == null)
            {
                this.fpPackageDetail_Sheet1.RowCount = 0;
                return;
            }
            ClinicCodes = PackageIDS = string.Empty;
            if (this.fpPackage_Sheet1.ActiveCell.Column.Index == 0)
            {
                string packageIDs = string.Empty;
                for (int i = 0; i < fpPackage_Sheet1.Rows.Count; i++)
                {
                    if (this.fpPackage_Sheet1.Cells[i, (int)PackageCols.option].Text.ToLower() == "true")
                    {
                        ClinicCodes += (this.fpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).ID + ",";
                        packageIDs += "'" + (this.fpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).ID + "',";
                    }
                }
                if (packageIDs.Length > 0)
                {
                    ClinicCodes = ClinicCodes.Substring(0, ClinicCodes.Length - 1);
                    PackageIDS = packageIDs = packageIDs.Substring(0, packageIDs.Length - 1);
                    this.SetPackageDetails(packageIDs);
                }
                else
                {
                    fpPackageDetail_Sheet1.RowCount = 0;
                }
            }

            //FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

        }

        /// <summary>
        /// 套餐细项单元格点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackageDetail_CellClick(object sender, CellClickEventArgs e)
        {
            try
            {
                this.fpPackageDetail.SetActiveCell(e.Row, (int)DetailCols.CostQty);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 套餐细项内容变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackageDetail_Change(object sender, ChangeEventArgs e)
        {
        }


        /// <summary>
        /// 焦点离开套餐细项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackageDetail_Leave(object sender, EventArgs e)
        {
            try
            {
                this.fpPackageDetail.StopCellEditing();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void deleteEvent()
        {
            this.fpPackageDetail_Sheet1.CellChanged -= new SheetViewEventHandler(fpPackageDetail_Sheet1_CellChanged);
            this.fpPackageDetail.Leave -= new EventHandler(fpPackageDetail_Leave);
            this.fpPackageDetail.Change -= new ChangeEventHandler(fpPackageDetail_Change);
            this.fpPackageDetail.CellClick -= new CellClickEventHandler(fpPackageDetail_CellClick);
            this.fpPackage.SelectionChanged -= new SelectionChangedEventHandler(fpPackage_SelectionChanged);
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
                this.tbGender.Text = string.Empty;
                this.tbAge.Text = string.Empty;
                this.tbPhone.Text = string.Empty;
                this.tbIDCARD.Tag = string.Empty;
                this.tbMemberClass.Text = string.Empty;
                this.tbPact.Text = string.Empty;
                return;
            }

            //{362143B2-CA84-4ae5-9CDC-79DB024D008A}
            System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(this.PatientInfo.PID.CardNO, "Account_CARD", "1");
            if (cardList != null && cardList.Count > 0)
            {
                this.accountCardInfo = cardList[cardList.Count - 1];
                this.tbMemberClass.Text = this.QueryNameByIDFromDictionnary(this.memberLevel, this.accountCardInfo.AccountLevel.ID.ToString());
            }
            else
            {
                this.tbMemberClass.Text = "院内卡";
            }

            this.tbMedicalNO.Text = this.PatientInfo.PID.CardNO;//this.accountCardInfo.Patient.PID.CardNO;
            this.tbName.Text = patientInfo.Name;
            this.tbGender.Text = this.QueryNameByIDFromDictionnary(this.sexList, patientInfo.Sex.ID.ToString());
            this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);
            this.tbPhone.Text = this.patientInfo.PhoneHome;
            this.tbIDCARD.Tag = this.QueryNameByIDFromDictionnary(this.IDTypeList, this.patientInfo.IDCardType.ID) + "/" + this.patientInfo.IDCard;
            //this.tbMemberClass.Text = this.QueryNameByIDFromDictionnary(this.memberLevel, this.accountCardInfo.AccountLevel.ID.ToString());
            this.tbPact.Text = this.QueryNameByIDFromDictionnary(this.pactList, this.patientInfo.Pact.ID);
            ArrayList familyList = pckMgr.QueryFamlilyMember(this.PatientInfo.PID.CardNO, this.PatientInfo.Name);
            cmbFamily.AddItems(familyList);
            cmbFamily.Tag = this.PatientInfo.PID.CardNO;
        }

        private string QueryNameByIDFromDictionnary(ArrayList al, string ID)
        {
            try
            {
                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    if (obj.ID == ID)
                    {
                        return obj.Name;
                    }
                }
            }
            catch
            { }
            return string.Empty;
        }

        /// <summary>
        /// 设置套餐信息
        /// </summary>
        private void SetPackageInfo()
        {

            if (this.patientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                return;
            }

            GetPackage(this.PatientInfo.PID.CardNO);
        }

        /// <summary>
        /// 获取套餐信息{DD31280F-7321-42BB-B150-4C63018ED85F}
        /// </summary>
        /// <param name="cardNO"></param>
        private void GetPackage(string cardNO)
        {
            this.hsPackage.Clear();
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackageDetail_Sheet1.RowCount = 0;
            this.fpCost_Sheet1.RowCount = 0;

            //ArrayList packageList = this.pckMgr.QueryByCardNO(cardNO, "1", "0");

            ArrayList packageList = this.pckMgr.QueryUnusedByCardNO(cardNO);
            if (packageList == null)
            {
                return;
            }
            string packageIDs = ClinicCodes = PackageIDS = string.Empty;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                package.PackageInfo = this.packageMgr.QueryPackageByID(package.PackageInfo.ID);

                //套餐包分开显示 {97fbd080-edb9-f31f-41fd-33f7837851ba}
                if (!IsSelectedAll && package.PackageInfo.UserType != selectedrange)
                {
                    continue;
                }
                this.fpPackage_Sheet1.AddRows(this.fpPackage_Sheet1.RowCount, 1);

                //{9B833B34-AE7F-4013-8F9D-CDE36A738D02}
                //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.IsSI].Value = package.SpecialFlag == "1" ? true : false;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.IsSI].Locked = true;

                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Name].Text = package.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.OperTime].Text = package.OperTime.ToString();
                this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag = package;
                if (package.PackageInfo.UserType == FS.HISFC.Models.Base.ServiceTypes.C)
                {
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.option].Text = "True";
                    this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.option].Value = "True";
                    ClinicCodes += (this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).ID + ",";
                    packageIDs += "'" + (this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).ID + "',";
                }
            }
            if (packageIDs.Length > 0)
            {
                ClinicCodes = ClinicCodes.Substring(0, ClinicCodes.Length - 1);
                PackageIDS = packageIDs = packageIDs.Substring(0, packageIDs.Length - 1);
                this.SetPackageDetails(packageIDs);
            }
            this.fpPackage_SelectionChanged(null, null);
        }

        /// <summary>
        /// 设置套餐明细
        /// </summary>
        private void SetPackageDetail(FS.HISFC.Models.MedicalPackage.Fee.Package package)
        {
            this.fpPackageDetail_Sheet1.RowCount = 0;
            ArrayList details = null;
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
                //改动detail的获取方式把。
                if (string.IsNullOrEmpty(detail.Item.Name))
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
                }

                decimal costNum = 0.0m;
                if (this.hsCostDetail.ContainsKey(detail.ID + "-" + detail.SequenceNO))
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail cost = this.hsCostDetail[detail.ID + "-" + detail.SequenceNO] as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                    costNum = cost.Item.Qty;
                }

                this.fpPackageDetail_Sheet1.AddRows(this.fpPackageDetail_Sheet1.RowCount, 1);
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ItemName].Text = detail.Item.Name;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Spec].Text = detail.Item.Specs;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.CostQty].Text = costNum.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.CostQty].Locked = false;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Unit].Text = detail.Unit;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Qty].Text = detail.Item.Qty.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.AvaliableQty].Value = detail.RtnQTY - costNum;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ComfirmQty].Value = detail.ConfirmQTY + costNum;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotCost].Text = detail.Detail_Cost.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.RealCost].Text = detail.Real_Cost.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.GiftCost].Text = detail.Gift_cost.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.EtcCost].Text = detail.Etc_cost.ToString();
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
            }
        }


        /// <summary>
        /// 设置套餐明细 根据选择的多个套餐展示多个套餐明细    //{D1E043F1-B598-4B8E-BEFB-8B4C48E56AB3} 
        /// </summary>
        /// <param name="packageIds"></param>
        private void SetPackageDetails(string packageIds)
        {
            this.fpPackageDetail_Sheet1.RowCount = 0;
            ArrayList details = this.pckdMgr.QueryDetailByClinicNOs1(packageIds, "0");
            if (details == null) return;
            if (details.Count == 0) return;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.PackageDetail detail in details)
            {
                //改动detail的获取方式把。
                if (string.IsNullOrEmpty(detail.Item.Name))
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
                }

                decimal costNum = 0.0m;
                if (this.hsCostDetail.ContainsKey(detail.ID + "-" + detail.SequenceNO))
                {
                    FS.HISFC.Models.MedicalPackage.Fee.PackageDetail cost = this.hsCostDetail[detail.ID + "-" + detail.SequenceNO] as FS.HISFC.Models.MedicalPackage.Fee.PackageDetail;
                    costNum = cost.Item.Qty;
                }

                this.fpPackageDetail_Sheet1.CellChanged -= new SheetViewEventHandler(fpPackageDetail_Sheet1_CellChanged);
                // fpPackageDetail_Sheet1_CellChanged
                this.fpPackageDetail_Sheet1.AddRows(this.fpPackageDetail_Sheet1.RowCount, 1);
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.PackageName].Text = detail.PackageName;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ItemName].Text = detail.Item.Name;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Spec].Text = detail.Item.Specs;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.CostQty].Text = costNum.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.CostQty].Locked = false;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Unit].Text = detail.Unit;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Qty].Text = detail.Item.Qty.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.AvaliableQty].Value = detail.RtnQTY - costNum;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ComfirmQty].Value = detail.ConfirmQTY + costNum;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotCost].Text = detail.Detail_Cost.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.RealCost].Text = detail.Real_Cost.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.GiftCost].Text = detail.Gift_cost.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.EtcCost].Text = detail.Etc_cost.ToString();
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
                this.fpPackageDetail_Sheet1.CellChanged += new SheetViewEventHandler(fpPackageDetail_Sheet1_CellChanged);
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
                if (this.fpPackageDetail.ContainsFocus)
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
                this.fpPackage.Focus();
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
            int currCol = this.fpPackageDetail_Sheet1.ActiveColumnIndex;
            int currRow = this.fpPackageDetail_Sheet1.ActiveRowIndex;

            if (key == Keys.Up)
            {
                if (currRow > 0)
                {
                    this.fpPackageDetail_Sheet1.ActiveRowIndex = currRow - 1;
                    this.fpPackageDetail_Sheet1.SetActiveCell(currRow - 1, this.fpPackageDetail_Sheet1.ActiveColumnIndex);
                }
            }

            if (key == Keys.Down || key == Keys.Enter)
            {
                if (currRow < this.fpPackageDetail_Sheet1.RowCount - 1)
                {
                    this.fpPackageDetail_Sheet1.SetActiveCell(currRow + 1, (int)DetailCols.CostQty);
                    this.fpPackageDetail.Focus();
                }
                else
                {
                    this.fpPackageDetail.StopCellEditing();
                }
            }
        }

        private void fpPackage_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Column == (int)PackageCols.option)
            {
                if (this.fpPackage_Sheet1.Cells[e.Row, (int)PackageCols.option].Text.ToLower() == "true")
                {
                    this.fpPackage_Sheet1.Cells[e.Row, (int)PackageCols.option].Text = "false";
                }
                else
                    fpPackage_Sheet1.Cells[e.Row, (int)PackageCols.option].Text = "true";
            }
        }

        private void cmbFamily_SelectedValueChanged(object sender, EventArgs e)
        {
            GetPackage(cmbFamily.Tag.ToString());
        }

        #region 枚举

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum PackageCols
        {
            /// <summary>
            /// 选择
            /// </summary>
            option = 0,

            /// <summary>
            /// {9B833B34-AE7F-4013-8F9D-CDE36A738D02}
            /// 是否医保
            /// </summary>
            IsSI = 1,

            /// <summary>
            /// 名称
            /// </summary>
            Name = 2,

            /// <summary>
            /// 购买时间
            /// </summary>
            OperTime = 3
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum DetailCols
        {
            /// <summary>
            /// 套餐名称
            /// </summary>
            PackageName = 0,

            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 1,

            /// <summary>
            /// 项目规格
            /// </summary>
            Spec = 2,

            /// <summary>
            /// 开立数量
            /// </summary>
            CostQty = 3,

            /// <summary>
            /// 开立单位
            /// </summary>
            Qty = 4,

            /// <summary>
            /// 总数量
            /// </summary>
            Unit = 5,

            /// <summary>
            /// 可用数量
            /// </summary>
            AvaliableQty = 6,

            /// <summary>
            /// 已确认数量
            /// </summary>
            ComfirmQty = 7,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 8,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 9,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 10,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 11
        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum CostCols
        {
            /// <summary>
            /// 套餐名称
            /// </summary>
            PackageName = 0,

            /// <summary>
            /// 名称
            /// </summary>
            ItemName = 1,

            /// <summary>
            /// 项目规格
            /// </summary>
            Spec = 2,

            /// <summary>
            /// 开立数量
            /// </summary>
            CostQty = 3,

            /// <summary>
            /// 开立单位
            /// </summary>
            Unit = 4,

            /// <summary>
            /// 总金额
            /// </summary>
            TotCost = 5,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 6,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 7,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 8
        }

        #endregion
        /// <summary>
        /// 套餐选择事件{97fbd080-edb9-f31f-41fd-33f7837851ba}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTctype_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox obx = sender as ComboBox;
            if (obx != null)
            {
                IsSelectedAll = false;
                if (obx.SelectedItem.ToString().Trim() == "门诊套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.C;
                }
                else if (obx.SelectedItem.ToString().Trim() == "住院套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.I;
                }
                else if (obx.SelectedItem.ToString().Trim() == "体检套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.T;
                }
                else if (obx.SelectedItem.ToString().Trim() == "全院套餐")
                {
                    selectedrange = FS.HISFC.Models.Base.ServiceTypes.A;
                }
                else
                {
                    IsSelectedAll = true;
                }

                if (this.PatientInfo != null)
                {
                    GetPackage(this.PatientInfo.PID.CardNO);
                }
            }
        }

        /// <summary>
        /// 取消全选{bdd11eea-5842-4b74-a3cf-4f6336fc8c70}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void BtnCheckCancel_Click(object sender, EventArgs e)
        {
            if (this.fpPackage_Sheet1.Rows.Count > 0)
            {
                for (int i = 0; i < this.fpPackage_Sheet1.Rows.Count; i++)
                {
                    this.fpPackage_Sheet1.Cells[i, (int)PackageCols.option].Text = "false";
                }
            }

            this.fpPackageDetail_Sheet1.RowCount = 0;
        }
    }
}

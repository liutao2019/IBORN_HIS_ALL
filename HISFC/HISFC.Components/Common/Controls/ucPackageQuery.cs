using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//{61223200-7CC7-4c5d-A830-922A8127BD5A}
namespace FS.HISFC.Components.Common.Controls
{
    /// <summary>
    /// 套餐查询及开立窗口
    /// </summary>
    public partial class ucPackageQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
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

        public bool detailVisible = false;

        //{0082764E-FE0E-467a-86D6-AC0762396803}
        public bool DetailVisible
        {
            get {
                return this.detailVisible;
            }
            set
            {
                this.pnlPackageDetail.Visible = value;
                this.detailVisible = value;
            }
        }
         
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
        /// 套餐查询
        /// </summary>
        public ucPackageQuery()
        {
            InitializeComponent();
            InitControls();
            addEvent();
        }

        private void addEvent()
        {
            this.fpPackage.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpPackage_SelectionChanged);
        }

        /// <summary>
        /// 初始化控件下拉列表
        /// </summary>
        private void InitControls()
        {
            //初始化合同单位
            pactList = this.pactManager.QueryPactUnitOutPatient();
            if (pactList == null)
            {
                MessageBox.Show("初始化合同单位出错!" + this.pactManager.Err);
                return;
            }
            //this.cmbPact.AddItems(pactList);

            //初始化性别
            ArrayList sexListTemp = new ArrayList();
            sexList = new ArrayList();
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
            //this.cmbSex.AddItems(sexList);
            //初始化证件类型列表
            IDTypeList = constantMgr.GetList("IDCard");
            if (IDTypeList == null)
            {
                MessageBox.Show("初始化证件类型列表出错!" + this.constantMgr.Err);

                return;
            }
            //this.cmbCardType.AddItems(IDTypeList);

            //初始化会员级别
             memberLevel = constantMgr.GetList("MemCardType");
            if (memberLevel == null)
            {
                MessageBox.Show("初始化会员类型列表出错!" + this.constantMgr.Err);

                return;
            }
            //this.cmbLevel.AddItems(memberLevel);

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
        /// 设置患者信息
        /// </summary>
        private void SetPatientInfo()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                this.tbMedicalNO.Text = string.Empty;
                this.tbName.Text = string.Empty;
                this.tbCardType.Text = string.Empty;
                this.tbIDNO.Text = string.Empty;
                this.tbSex.Text= string.Empty;
                this.tbAge.Text = string.Empty;
                this.tbLevel.Tag = string.Empty;
                this.tbPhone.Text = string.Empty;
                this.tbPact.Tag = string.Empty;

                return;
            }


            System.Collections.Generic.List<FS.HISFC.Models.Account.AccountCard> cardList = accountMgr.GetMarkList(this.PatientInfo.PID.CardNO, "ALL", "1");
            if (cardList == null || cardList.Count < 1)
            {
                MessageBox.Show("病历号不存在！");
                return;
            }
            this.accountCardInfo = cardList[cardList.Count - 1];

            this.tbMedicalNO.Text = this.accountCardInfo.Patient.PID.CardNO;
            this.tbName.Text = patientInfo.Name;
            this.tbCardType.Text = this.QueryNameByIDFromDictionnary(this.IDTypeList, this.patientInfo.IDCardType.ID);
            this.tbIDNO.Text = this.patientInfo.IDCard;
            this.tbSex.Text = this.QueryNameByIDFromDictionnary(this.sexList, patientInfo.Sex.ID.ToString());
            this.tbAge.Text = this.accountMgr.GetAge(patientInfo.Birthday);
            this.tbLevel.Text = this.QueryNameByIDFromDictionnary(this.memberLevel, this.accountCardInfo.AccountLevel.ID.ToString());
            this.tbPhone.Text = this.patientInfo.PhoneHome;
            this.tbPact.Text = this.QueryNameByIDFromDictionnary(this.pactList, this.patientInfo.Pact.ID);
        }

        /// <summary>
        /// 设置套餐信息
        /// </summary>
        private void SetPackageInfo()
        {
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpPackageDetail_Sheet1.RowCount = 0;

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
                this.fpPackage_Sheet1.AddRows(this.fpPackage_Sheet1.RowCount, 1);
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.Name].Text = package.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.TotCost].Text = package.Package_Cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.RealCost].Text = package.Real_Cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.GiftCost].Text = package.Gift_cost.ToString("F2");
                this.fpPackage_Sheet1.Cells[this.fpPackage_Sheet1.RowCount - 1, (int)PackageCols.EtcCost].Text = package.Etc_cost.ToString("F2");
                this.fpPackage_Sheet1.Rows[this.fpPackage_Sheet1.RowCount - 1].Tag = package;
            }

            this.fpPackage_SelectionChanged(null, null);
        }

        /// <summary>
        /// 设置套餐明细
        /// </summary>
        private void SetPackageDetail(FS.HISFC.Models.MedicalPackage.Fee.Package package)
        {
            this.fpPackageDetail_Sheet1.RowCount = 0;
            ArrayList details = this.pckdMgr.QueryDetailByClinicNO(package.ID, "0");
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

                this.fpPackageDetail_Sheet1.AddRows(this.fpPackageDetail_Sheet1.RowCount, 1);
                this.fpPackageDetail_Sheet1.Rows[this.fpPackageDetail_Sheet1.RowCount - 1].Tag = detail;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ItemName].Text = detail.Item.Name + "[" + detail.Item.Specs + "]" + "*" + detail.Item.Qty.ToString() + detail.Unit;
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotQty].Text = detail.Item.Qty.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.Unit].Text = detail.Unit.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.ValiableQty].Text = detail.RtnQTY.ToString();
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.TotCost].Text = detail.Detail_Cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.RealCost].Text = detail.Real_Cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.GiftCost].Text = detail.Gift_cost.ToString("F2");
                this.fpPackageDetail_Sheet1.Cells[this.fpPackageDetail_Sheet1.RowCount - 1, (int)DetailCols.EtcCost].Text = detail.Etc_cost.ToString("F2");
            }
        }

        /// <summary>
        /// 套餐选择触发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpPackage_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.fpPackage_Sheet1.ActiveRow == null)
            {
                this.fpPackageDetail_Sheet1.RowCount = 0;
                return;
            }
            FS.HISFC.Models.MedicalPackage.Fee.Package selectPackage = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
            this.SetPackageDetail(selectPackage);
        }

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
            /// 总金额
            /// </summary>
            TotCost = 1,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 2,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 3,

            /// <summary>
            /// 优惠金额
            /// </summary>
            EtcCost = 4
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
            /// 总数量
            /// </summary>
            TotQty = 1,

            /// <summary>
            /// 单位
            /// </summary>
            Unit = 2,

            /// <summary>
            /// 可开数量
            /// </summary>
            ValiableQty = 3,

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
    }
}

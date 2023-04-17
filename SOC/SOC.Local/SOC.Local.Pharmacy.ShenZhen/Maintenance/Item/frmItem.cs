using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Maintenance.Item
{
    /// <summary>
    /// [功能描述: 药品基本信息维护控件]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public partial class frmItem : Form
    {
        public frmItem()
        {
            InitializeComponent();
        }

        public delegate void SaveItemHandler(FS.HISFC.Models.Pharmacy.Item item);
        public delegate void GetNextItemHandler(int span);

        public event SaveItemHandler EndSave;
        public event GetNextItemHandler GetNextItem;

        /// <summary>
        /// 控件内操作的药品实体
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = null;

        /// <summary>
        /// 用于操作价格变动的药品实体
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item itemTemp = null;

        /// <summary>
        /// 自动规格生成字符串 0 基本剂量 1 剂量单位 2 包装数量 3 最小单位 4 包装单位
        /// </summary>
        private string autoCreateSpecs = "{0}{1}*{2}{3}/{4}";

        /// <summary>
        /// 用来标记是否已经使用
        /// </summary>
        private bool isHavingStorage = false;

        /// <summary>
        /// 每次用量单位处是否允许选择
        /// </summary>
        private bool isCanChooseOnceDoseUnit = false;

        /// <summary>
        /// 门诊和住院临瞩是否分开处理
        /// </summary>
        private bool isSplitLZAndOutPatient = false;

        /// <summary>
        /// 是否启用第二基本剂量维护
        /// </summary>
        private bool isShowSecondDosage = false;


        private SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl IItemExtendControl = null;

        private FS.HISFC.BizLogic.Fee.Interface myInterface = new FS.HISFC.BizLogic.Fee.Interface();

        private ArrayList alPhyFunction1 = new ArrayList();
        private ArrayList alPhyFunction2 = new ArrayList();
        private ArrayList alPhyFunction3 = new ArrayList();
        private Hashtable hsFormula = new Hashtable();

        #region 属性
        /// <summary>
        ///  自动规格生成字符串 0 基本剂量 1 剂量单位 2 包装数量 3 最小单位 4 包装单位
        /// </summary>
        public string AutoCreateSpecs
        {
            get
            {
                return this.autoCreateSpecs;
            }
            set
            {
                this.autoCreateSpecs = value;
            }
        }

        /// <summary>
        /// 用于区分是否新药
        /// </summary>
        public Boolean IsHavingStorage
        {
            get
            {
                return this.isHavingStorage;
            }
            set
            {
                this.isHavingStorage = value;
            }
        }

        /// <summary>
        /// 每次用量单位处是否允许选择
        /// </summary>
        public Boolean IsCanChooseOnceDoseUnit
        {
            get
            {
                return this.isCanChooseOnceDoseUnit;
            }
            set
            {
                this.isCanChooseOnceDoseUnit = value;
            }
        }

        /// <summary>
        /// 门诊和住院临瞩是否拆开
        /// </summary>
        public Boolean IsSplitLZAndOutPatient
        {
            get
            {
                return this.isSplitLZAndOutPatient;
            }
            set
            {
                this.isSplitLZAndOutPatient = value;
            }
        }


        /// <summary>
        /// 是否启用第二剂量维护
        /// </summary>
        public bool IsShowSecondDosage
        {
            get { return isShowSecondDosage; }
            set { isShowSecondDosage = value; }
        }
        #endregion


        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public int Init(FS.SOC.HISFC.Components.Pharmacy.Maintenance.BaseCache baseCache)
        {
            if (baseCache == null)
            {
                return 1;
            }

            this.isHavingStorage = false;
            this.hsFormula = baseCache.hsFormula;

            if (IItemExtendControl == null)
            {
                IItemExtendControl = FS.SOC.HISFC.Components.Pharmacy.Function.GetItemExtendControl();
                if (IItemExtendControl is Control)
                {
                    Control control = (Control)IItemExtendControl;
                    control.Dock = DockStyle.Fill;
                    this.tpExtend.Controls.Add(control);
                    string errInfo = "";
                    if (this.IItemExtendControl.Init(ref errInfo) == -1)
                    {
                        Function.ShowMessage("初始化扩展信息发生错误，请向系统管理员联系并报告错误信息：" + errInfo, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }

            this.cmbDrugType.AddItems(baseCache.drugTypeHelper.ArrayObject);
            this.cmbDrugType.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cmbSysClass.AddItems(FS.HISFC.Models.Base.SysClassEnumService.List());
            this.cmbSysClass.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cmbMinFee.AddItems(baseCache.minFeeHelper.ArrayObject);
            this.cmbMinFee.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cmbQuality.AddItems(baseCache.drugQualityHelper.ArrayObject);
            this.cmbQuality.DropDownStyle = ComboBoxStyle.DropDownList;

            this.cmbDosageForm.AddItems(baseCache.doseFormHelper.ArrayObject);
            this.cmbDosageForm.DropDownStyle = ComboBoxStyle.DropDownList;

            if (baseCache.gradeHelper.ArrayObject.Count == 0)
            {
                this.cmbGrade.Items.Add("A");
                this.cmbGrade.Items.Add("B");
                this.cmbGrade.Items.Add("C");
            }
            else
            {
                this.cmbGrade.AddItems(baseCache.gradeHelper.ArrayObject);
            }
            for (int index = 0; index < (int)FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType.End; index++)
            {
                this.ncmbLZSplitType.Items.Add(((FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType)index).ToString());
                this.ncmbSplitType.Items.Add(((FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType)index).ToString());
                this.ncmbCDSplitType.Items.Add(((FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType)index).ToString());
            }
            this.ncmbLZSplitType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ncmbCDSplitType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.ncmbSplitType.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.ncmbLZSplitType.SelectedIndex = 0;
            //this.ncmbCDSplitType.SelectedIndex = 0;
            //this.ncmbSplitType.SelectedIndex = 0;

            this.cmbPackUnit.AddItems(baseCache.pactUnitHelper.ArrayObject);
            this.cmbMinUnit.AddItems(baseCache.minUnitHelper.ArrayObject);
            this.cmbDoseUnit.AddItems(baseCache.doseUnitHelper.ArrayObject);
            this.ncmbSecondDoseUnit.AddItems(baseCache.doseUnitHelper.ArrayObject);

            this.cmbPriceForm.AddItems(baseCache.priceFormHelper.ArrayObject);
            this.cmbUsage.AddItems(baseCache.usageHelper.ArrayObject);
            this.cmbFrequency.AddItems(baseCache.frequencyHelper.ArrayObject);
            this.cmbStoreCondition.AddItems(baseCache.storeConditionHelper.ArrayObject);

            this.cmbCompany.AddItems(baseCache.companyHelper.ArrayObject);
            this.cmbTenderCompancy.AddItems(baseCache.companyHelper.ArrayObject);
            this.cmbProducer.AddItems(baseCache.producerHelper.ArrayObject);

            this.cmbPhyFunction1.AddItems(baseCache.function1Helper.ArrayObject);
            this.alPhyFunction1 = baseCache.function1Helper.ArrayObject;

            this.cmbPhyFunction2.AddItems(baseCache.function2Helper.ArrayObject);
            this.alPhyFunction2 = baseCache.function2Helper.ArrayObject;

            this.cmbPhyFunction3.AddItems(baseCache.function3Helper.ArrayObject);
            this.alPhyFunction3 = baseCache.function3Helper.ArrayObject;
            this.btSICompare.Click -= new System.EventHandler(this.btSICompare_Click);
            this.btSICompare.Click += new System.EventHandler(this.btSICompare_Click);
            this.btnSave.Click -= new EventHandler(btnSave_Click);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click -= new EventHandler(btnCancel_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.txtPurchasePrice.TextChanged += new EventHandler(txtPurchasePrice_TextChanged);
            this.txtPurchasePrice.TextChanged -= new EventHandler(txtPurchasePrice_TextChanged);
            this.ncmbSplitType.SelectedIndexChanged += new EventHandler(ncmbSplitType_SelectedIndexChanged);
            this.ncmbSplitType.SelectedIndexChanged -= new EventHandler(ncmbSplitType_SelectedIndexChanged);
            this.cmbPhyFunction1.SelectedIndexChanged -= new EventHandler(cmbPhyFunction1_SelectedIndexChanged);
            this.cmbPhyFunction1.SelectedIndexChanged += new EventHandler(cmbPhyFunction1_SelectedIndexChanged);
            this.cmbPhyFunction2.SelectedIndexChanged -= new EventHandler(cmbPhyFunction2_SelectedIndexChanged);
            this.cmbPhyFunction2.SelectedIndexChanged += new EventHandler(cmbPhyFunction2_SelectedIndexChanged);
            this.chbIsStop.CheckedChanged += new EventHandler(chbIsStop_CheckedChanged);

            this.nbtBack.Click -= new EventHandler(nbtBack_Click);
            this.nbtBack.Click += new EventHandler(nbtBack_Click);

            this.nbtNext.Click -= new EventHandler(nbtNext_Click);
            this.nbtNext.Click += new EventHandler(nbtNext_Click);

            this.txtName.KeyUp -= new KeyEventHandler(txtName_KeyUp);
            this.txtName.KeyUp += new KeyEventHandler(txtName_KeyUp);

            this.txtRegularName.KeyUp -= new KeyEventHandler(txtRegularName_KeyUp);
            this.txtRegularName.KeyUp += new KeyEventHandler(txtRegularName_KeyUp);

            this.txtFormalName.KeyUp -= new KeyEventHandler(txtFormalName_KeyUp);
            this.txtFormalName.KeyUp += new KeyEventHandler(txtFormalName_KeyUp);

            this.txtOtherName.KeyUp -= new KeyEventHandler(txtOtherName_KeyUp);
            this.txtOtherName.KeyUp += new KeyEventHandler(txtOtherName_KeyUp);

            this.neuTabControl1.SelectedIndexChanged -= new EventHandler(neuTabControl1_SelectedIndexChanged);
            this.neuTabControl1.SelectedIndexChanged += new EventHandler(neuTabControl1_SelectedIndexChanged);

            this.cmbPackUnit.TextChanged -= new EventHandler(cmbPackUnit_TextChanged);
            this.cmbPackUnit.TextChanged += new EventHandler(cmbPackUnit_TextChanged);

            this.txtMemo.Multiline = !this.IsShowSecondDosage;
            this.txtMemo.Height = 50;

            this.nlbSecondDosage.Visible = this.IsShowSecondDosage;
            this.ntxtSecondDose.Visible = this.IsShowSecondDosage;
            this.nlbSecondDoseUnit.Visible = this.IsShowSecondDosage;
            this.ncmbSecondDoseUnit.Visible = this.IsShowSecondDosage;


            return 1;
        }


        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public int Clear()
        {
            //foreach中只能迭代浏览，无法修改，所以改成for循环
            //foreach (System.Windows.Forms.Control c in this.tpNormal.Controls)
            //{
            //    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
            //    {
            //        foreach (System.Windows.Forms.Control crl in c.Controls)
            //        {

            //            if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
            //            {
            //                crl.Text = "";
            //                crl.Tag = "";
            //                continue;
            //            }
            //            if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
            //            {
            //                crl.Tag = "";
            //                crl.Text = "";
            //                continue;
            //            }
            //            if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
            //            {
            //                ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
            //            }
            //        }
            //    }
            //}

            for (int i = 0; i < this.tpNormal.Controls.Count; i++)
            {
                System.Windows.Forms.Control c = this.tpNormal.Controls[i];
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    for (int j = 0; j < c.Controls.Count; j++)
                    {
                        System.Windows.Forms.Control crl = c.Controls[j];
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            crl.Text = "";
                            crl.Tag = "";
                            continue;
                        }
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                            continue;
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
                        }

                    }
                }


            }

            //foreach中只能迭代浏览，无法修改，所以改成for循环
            //foreach (System.Windows.Forms.Control c in this.tpOther.Controls)
            //{
            //    if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
            //    {
            //        foreach (System.Windows.Forms.Control crl in c.Controls)
            //        {
            //            if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
            //            {
            //                crl.Text = null;
            //                crl.Tag = null;
            //                continue;
            //            }
            //            if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
            //            {
            //                crl.Tag = "";
            //                crl.Text = "";
            //            }
            //            if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
            //            {
            //                ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
            //            }
            //        }
            //    }
            //}

            for (int i = 0; i < this.tpOther.Controls.Count; i++)
            {
                System.Windows.Forms.Control c = this.tpOther.Controls[i];
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    for(int j=0;j<c.Controls.Count;j++)
                    {
                        System.Windows.Forms.Control crl = c.Controls[j];
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            crl.Text = null;
                            crl.Tag = null;
                            continue;
                        }
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
                        }
                    }
                }
            }


            this.cmbDrugType.Text = "";
            this.cmbDrugType.Tag = null;

            this.continueCheckBox.Checked = false;

            this.cmbPackUnit.Enabled = true;
            this.txtPackQty.Enabled = true;
            this.cmbMinUnit.Enabled = true;
            this.txtBaseDose.Enabled = true;
            this.cmbDoseUnit.Enabled = true;
            this.ntxtSecondDose.Enabled = true;
            this.ncmbSecondDoseUnit.Enabled = true;
            this.cmbOnceDoseUnit.Enabled = true;
            this.cmbOnceDoseUnit.AddItems(new ArrayList());

            if (this.IItemExtendControl != null)
            {
                this.IItemExtendControl.Clear();
            }
            this.item = null;
            this.neuTabControl1.SelectedTab = this.tpNormal;

            return 1;
        }

        /// <summary>
        /// 根据传入的Item实体信息 设置控件显示
        /// </summary>
        public void SetItem(FS.HISFC.Models.Pharmacy.Item item, bool isUsed)
        {
            this.isHavingStorage = isUsed;
            this.chbIsStop.Enabled = FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive("0300", "01");

            this.item = item;

            this.txtName.Text = item.Name;
            this.txtSpellCode.Text = item.SpellCode;
            this.txtWbCode.Text = item.WBCode;
            this.txtUserCode.Text = item.UserCode;
            this.txtRegularName.Text = item.NameCollection.RegularName;
            this.txtRegularSpellCode.Text = item.NameCollection.RegularSpell.SpellCode;
            this.txtRegularWbCode.Text = item.NameCollection.RegularSpell.WBCode;
            this.txtRegularUserCode.Text = item.NameCollection.RegularSpell.UserCode;
            this.txtFormalName.Text = item.NameCollection.FormalName;
            this.txtFormalSpellCode.Text = item.NameCollection.FormalSpell.SpellCode;
            this.txtFormalWbCode.Text = item.NameCollection.FormalSpell.WBCode;
            this.txtFormalUserCode.Text = item.NameCollection.FormalSpell.UserCode;
            this.txtOtherName.Text = item.NameCollection.OtherName;
            this.txtOtherSpellCode.Text = item.NameCollection.OtherSpell.SpellCode;
            this.txtOtherWbCode.Text = item.NameCollection.OtherSpell.WBCode;
            this.txtOtherUserCode.Text = item.NameCollection.OtherSpell.UserCode;
            this.txtEnglishName.Text = item.NameCollection.EnglishName;
            this.txtEnglishOtherName.Text = item.NameCollection.EnglishOtherName;
            this.txtEnglishRegularName.Text = item.NameCollection.EnglishRegularName;
            this.txtGbCode.Text = item.NameCollection.GbCode;
            this.txtInternationalCode.Text = item.NameCollection.InternationalCode;
            this.cmbDrugType.Tag = item.Type.ID;
            this.cmbSysClass.Tag = item.SysClass.ID;
            this.cmbMinFee.Tag = item.MinFee.ID;
            this.txtSpecs.Text = item.Specs;
            this.txtPackQty.Text = item.PackQty.ToString();
            this.cmbMinUnit.Text = item.MinUnit;
            this.txtBaseDose.Text = item.BaseDose.ToString();
            this.cmbDoseUnit.Text = item.DoseUnit;
            this.cmbQuality.Tag = item.Quality.ID;
            this.cmbDosageForm.Tag = item.DosageForm.ID;
            this.cmbPriceForm.Tag = item.PriceCollection.PriceForm.ID;
            this.cmbGrade.Tag = item.Grade;
            this.txtRetailPrice.Text = item.PriceCollection.RetailPrice.ToString();
            this.txtWholesalePrice.Text = item.PriceCollection.WholeSalePrice.ToString();
            this.txtPurchasePrice.Text = item.PriceCollection.PurchasePrice.ToString();
            this.txtTopRetailPrice.Text = item.PriceCollection.TopRetailPrice.ToString();
            this.ntxtRetailPrice2.Text = item.RetailPrice2.ToString();
            this.cmbPackUnit.Text = item.PackUnit;
            this.txtMemo.Text = item.Memo;
            this.cmbUsage.Tag = item.Usage.ID;
           
            if (item.OnceDose == 0)
            {
                this.txtOnceDose.Text = "";
            }
            else
            {
                this.txtOnceDose.Text = item.OnceDose.ToString();
            }

            this.cmbOnceDoseUnit.Enabled = this.IsCanChooseOnceDoseUnit;
            if (this.IsCanChooseOnceDoseUnit)
            {
                this.cmbOnceDoseUnit.Text = item.OnceDoseUnit;
            }
            else
            {
                this.cmbOnceDoseUnit.Text = item.DoseUnit;
            }
            if (!this.IsCanChooseOnceDoseUnit && string.IsNullOrEmpty(item.OnceDoseUnit))
            {
                this.cmbOnceDoseUnit.Text = item.DoseUnit;
            }
            this.ncmbSecondDoseUnit.Text = item.SecondDoseUnit;
            if (item.SecondBaseDose > 0)
            {
                this.ntxtSecondDose.Text = item.SecondBaseDose.ToString();
            }
            this.cmbFrequency.Tag = item.Frequency.ID;
            this.txtCaution.Text = item.Product.Caution;
            this.txtIngredient.Text = item.Ingredient;
            this.cmbStoreCondition.Text = item.Product.StoreCondition;
            if (item.Product.StoreCondition == "")
            {
                this.cmbStoreCondition.Tag = "";
            }

            this.txtExecuteStandard.Text = item.ExecuteStandard;
            this.cmbPhyFunction1.Tag = item.PhyFunction1.ID;
            this.cmbPhyFunction2.Tag = item.PhyFunction2.ID;
            this.cmbPhyFunction3.Tag = item.PhyFunction3.ID;
            this.cmbProducer.Tag = item.Product.Producer.ID;
            this.txtApprovalInfo.Text = item.Product.ApprovalInfo;
            this.txtLabel.Text = item.Product.Label;
            this.txtProducingArea.Text = item.Product.ProducingArea;
            this.cmbCompany.Tag = item.Product.Company.ID;
            this.txtBarCode.Text = item.Product.BarCode;
            this.txtBriefIntroduction.Text = item.Product.BriefIntroduction;
            this.txtManual.Text = item.Product.Manual;
            this.chbIsTenderOffer.Checked = item.TenderOffer.IsTenderOffer;
            this.cmbTenderCompancy.Tag = item.TenderOffer.Company.ID;
            this.txtContractCode.Text = item.TenderOffer.ContractNO;
            this.txtTenderPrice.Text = item.TenderOffer.Price.ToString();
            this.cmbStoreCondition.Text = item.Product.StoreCondition;
            try { this.dtpBeginDate.Value = item.TenderOffer.BeginTime; }
            catch { };
            try { this.dtpEndDate.Value = item.TenderOffer.EndTime; }
            catch { };
            this.chbIsStop.Checked = (item.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid);
            this.chbIsSelfMade.Checked = item.Product.IsSelfMade;
            this.chbIsAllergy.Checked = item.IsAllergy;
            this.chbIsGMP.Checked = item.IsGMP;
            this.chbIsOTC.Checked = item.IsOTC;
            this.chbIsShow.Checked = item.IsShow;
            this.chbIsNew.Checked = item.IsNew;
            this.chbIsLack.Checked = item.IsLack;
            this.chbIsAppend.Checked = item.IsSubtbl;
            this.txtStopReason.Text = item.ShiftMark;	//变动类型

            this.txtStopReason.Visible = this.chbIsStop.Checked;
            this.lblStopReason.Visible = this.chbIsStop.Checked;

            this.ckNostrum.Checked = item.IsNostrum;

            //by cao-lin 2012-09 增加住院临时医嘱拆分属性
            if (item.LZSplitType != null && item.LZSplitType.Trim() != "")
            {
                this.ncmbLZSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(item.LZSplitType);
            }
            else
            {
                this.ncmbLZSplitType.SelectedIndex = -1;
            }
            //end by 

            if (item.SplitType != null && item.SplitType.Trim() != "")
            {
                this.ncmbSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(item.SplitType);
            }
            else
            {
                this.ncmbSplitType.SelectedIndex = -1;
            }

            //by cube 2011-03-28 增加住院长期医嘱拆分属性
            if (!string.IsNullOrEmpty(item.CDSplitType))
            {
                this.ncmbCDSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(item.CDSplitType);
            }
            else
            {
                this.ncmbCDSplitType.SelectedIndex = -1;
            }
            //end by

            this.ncmbLZSplitType.Enabled = isSplitLZAndOutPatient;
            if (isSplitLZAndOutPatient)
            {
                if (!string.IsNullOrEmpty(item.LZSplitType))
                {
                    this.ncmbLZSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(item.LZSplitType);
                }
                else
                {
                    this.ncmbLZSplitType.SelectedIndex = -1;
                }
            }
            else
            {
                if (item.SplitType != null && item.SplitType.Trim() != "")
                {
                    this.ncmbLZSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(item.SplitType);
                }
                else
                {
                    this.ncmbLZSplitType.SelectedIndex = -1;
                }
            }

            if (item.ShowState != null && item.ShowState.Trim() != "")
            {
                this.cmbShow.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(item.ShowState);
            }
            else
            {

                this.cmbShow.SelectedIndex = -1;
            }


            if (item.CreateTime > this.dtpCreateDate.MinDate)
            {
                this.dtpCreateDate.Value = item.CreateTime;
            }

            string errInfo = "";

            if (this.IItemExtendControl != null)
            {
                if (this.IItemExtendControl.Set(item, ref errInfo) == -1)
                {
                    Function.ShowMessage("药品基本信息扩展信息显示发生问题，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                }
            }

            this.cmbPackUnit.Enabled = !isUsed;
            this.txtPackQty.Enabled = !isUsed;
            this.cmbMinUnit.Enabled = !isUsed;
            this.txtBaseDose.Enabled = !isUsed;
            this.cmbDoseUnit.Enabled = !isUsed;
            this.txtRetailPrice.Enabled = !isUsed;
            if (!string.IsNullOrEmpty(this.ntxtSecondDose.Text))
            {
                this.ntxtSecondDose.Enabled = !isUsed;
                this.ncmbSecondDoseUnit.Enabled = !isUsed;
            }

            #region  显示医保信息

            ArrayList alCompare  =new ArrayList();
         

            myInterface.GetComparealItem("2",item.ID,ref alCompare);
            this.fpSI_Sheet1.Rows.Count = 0;
            if (alCompare != null && alCompare.Count > 0)
            {
                this.btSICompare.Visible = true;
                
                int i = 0;
                foreach (FS.HISFC.Models.SIInterface.Compare compareInfo in alCompare)
                {
                    
                    this.fpSI_Sheet1.Rows.Add(i, 1);

                    this.fpSI_Sheet1.Cells[i, 0].Text = compareInfo.SpellCode.UserCode;    //编码
                    this.fpSI_Sheet1.Cells[i, 1].Text = compareInfo.CenterItem.Name;
                    this.fpSI_Sheet1.Cells[i, 2].Text = compareInfo.CenterItem.ID;
                    this.fpSI_Sheet1.Cells[i, 3].Text = compareInfo.CenterItem.ItemType;
                    this.fpSI_Sheet1.Cells[i, 4].Text = compareInfo.CenterItem.Memo.ToString();
                    if (compareInfo.CenterItem.ItemGrade == "0")
                    {
                        this.fpSI_Sheet1.Cells[i, 5].Text = "记账";
                    }
                    else
                    {
                        this.fpSI_Sheet1.Cells[i, 5].Text = "自费";
                    }
                    this.fpSI_Sheet1.Cells[i, 6].Text = compareInfo.CenterItem.OperDate.ToShortDateString();
                    this.fpSI_Sheet1.Cells[i, 7].Text = compareInfo.CenterItem.Memo;

                    i++;
                }
            }

            #endregion

        }

        /// <summary>
        /// 有效性检测
        /// </summary>
        /// <returns></returns>
        private bool CheckValid()
        {
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请录入药品名称！", MessageBoxIcon.Information);
                this.txtName.Select();
                this.txtName.Focus();
                return false;
            }

            //如果是停用药品，则不检测下面的数据是否有效
            if (this.chbIsStop.Checked)
            {
                if (string.IsNullOrEmpty(this.txtStopReason.Text))
                {
                    this.neuTabControl1.SelectedTab = this.tpNormal;
                    Function.ShowMessage("请录入停用原因！", MessageBoxIcon.Information);
                    this.txtStopReason.Select();
                    this.txtStopReason.Focus();
                    return false;
                }
                return true;
            }


            if (string.IsNullOrEmpty(this.txtSpellCode.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请录入药品名称拼音码！", MessageBoxIcon.Information);
                this.txtSpellCode.Select();
                this.txtSpellCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtWbCode.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请录入药品名称五笔码！", MessageBoxIcon.Information);
                this.txtWbCode.Select();
                this.txtWbCode.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtUserCode.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请录入药品名称自定义码！", MessageBoxIcon.Information);
                this.txtUserCode.Select();
                this.txtUserCode.Focus();
                return false;
            }

            if (this.cmbDrugType.Tag == null || this.cmbDrugType.Tag.ToString() == "" || string.IsNullOrEmpty(this.cmbDrugType.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择药品类别！", MessageBoxIcon.Information);
                this.cmbDrugType.Select();
                this.cmbDrugType.Focus();
                return false;
            }

            if (this.cmbSysClass.Tag == null || this.cmbSysClass.Tag.ToString() == "" || string.IsNullOrEmpty(this.cmbSysClass.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择系统类别！", MessageBoxIcon.Information);
                this.cmbSysClass.Select();
                this.cmbSysClass.Focus();
                return false;
            }

            if (this.cmbMinFee.Tag == null || this.cmbMinFee.Tag.ToString() == "" || string.IsNullOrEmpty(this.cmbMinFee.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择最小费用(统计分类)！", MessageBoxIcon.Information);
                this.cmbMinFee.Select();
                this.cmbMinFee.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.txtSpecs.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请录入规格！", MessageBoxIcon.Information);
                this.txtSpecs.Select();
                this.txtSpecs.Focus();
                return false;
            }

            if (this.cmbQuality.Tag == null || this.cmbQuality.Tag.ToString() == "" || string.IsNullOrEmpty(this.cmbQuality.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择药品性质！", MessageBoxIcon.Information);
                this.cmbQuality.Select();
                this.cmbQuality.Focus();
                return false;
            }

            if (this.cmbDosageForm.Tag == null || this.cmbDosageForm.Tag.ToString() == "" || string.IsNullOrEmpty(this.cmbDosageForm.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择剂型！", MessageBoxIcon.Information);
                this.cmbDosageForm.Select();
                this.cmbDosageForm.Focus();
                return false;
            }


            if (string.IsNullOrEmpty(this.ncmbLZSplitType.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择临嘱拆分类型！", MessageBoxIcon.Information);
                this.ncmbLZSplitType.Select();
                this.ncmbLZSplitType.Focus();
                return false;
            }

            if (this.cmbPackUnit.Text.Trim() == "")
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择包装单位！", MessageBoxIcon.Information);
                this.ncmbCDSplitType.Select();
                this.ncmbCDSplitType.Focus();
                return false;
            }

            if (this.cmbMinUnit.Text.Trim() == "")
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择最小单位！", MessageBoxIcon.Information);
                this.cmbMinUnit.Select();
                this.cmbMinUnit.Focus();
                return false;
            }


            if (this.cmbMinUnit.Text.Trim() == this.cmbPackUnit.Text.Trim() && FS.FrameWork.Function.NConvert.ToDecimal(this.txtPackQty.Text) != 1m)
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("包装单位和最小单位相同时包装数量必须是1", MessageBoxIcon.Information);
                this.cmbMinUnit.Select();
                this.cmbMinUnit.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(this.ncmbCDSplitType.Text))
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("请选择长嘱拆分类型！", MessageBoxIcon.Information);
                this.ncmbCDSplitType.Select();
                this.ncmbCDSplitType.Focus();
                return false;
            }

            if (this.cmbMinUnit.Text.Trim() == this.txtBaseDose.Text.Trim() && FS.FrameWork.Function.NConvert.ToDecimal(this.txtBaseDose.Text) != 1m)
            {
                this.neuTabControl1.SelectedTab = this.tpNormal;
                Function.ShowMessage("最小单位和剂量单位相同时基本剂量必须是1", MessageBoxIcon.Information);
                this.txtBaseDose.Select();
                this.txtBaseDose.Focus();
                return false;
            }


            decimal retailPrice2 = 0;
            if (!decimal.TryParse(this.ntxtRetailPrice2.Text, out retailPrice2))
            {
                if (!string.IsNullOrEmpty(this.ntxtRetailPrice2.Text))
                {
                    Function.ShowMessage("零差价录入不正确，请确认录入数字", MessageBoxIcon.Information);
                    this.ntxtRetailPrice2.Select();
                    this.ntxtRetailPrice2.Focus();
                    return false;
                }

            }

            if (retailPrice2 < 0)
            {
                Function.ShowMessage("零差价不能小于0", MessageBoxIcon.Information);
                this.ntxtRetailPrice2.Select();
                this.ntxtRetailPrice2.Focus();
                return false;
            }
            if (!string.IsNullOrEmpty(this.txtOnceDose.Text))
            {
                decimal onceOnce = 0;
                if (!decimal.TryParse(this.txtOnceDose.Text, out onceOnce))
                {
                    Function.ShowMessage("一次用量维护不正确，请确认录入数字", MessageBoxIcon.Information);
                    this.neuTabControl1.SelectedTab = this.tpOther;
                    this.txtOnceDose.Select();
                    this.txtOnceDose.Focus();
                    return false;
                }
                if (onceOnce <= 0)
                {
                    Function.ShowMessage("一次用量维护不正确，录入的值必须大于0", MessageBoxIcon.Information);
                    this.neuTabControl1.SelectedTab = this.tpOther;
                    this.txtOnceDose.Select();
                    this.txtOnceDose.Focus();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(this.cmbOnceDoseUnit.Text))
            {
                if (this.cmbOnceDoseUnit.Text != this.cmbMinUnit.Text && this.cmbOnceDoseUnit.Text != this.cmbDoseUnit.Text && this.cmbOnceDoseUnit.Text != this.ncmbSecondDoseUnit.Text)
                {
                    Function.ShowMessage("一次用量单位维护不正确，请您重新选择单位！", MessageBoxIcon.Information);
                    this.neuTabControl1.SelectedTab = this.tpOther;
                    this.cmbOnceDoseUnit.Select();
                    this.cmbOnceDoseUnit.Focus();
                    return false;
                }
            }
            if (this.isShowSecondDosage)
            {
                if (string.IsNullOrEmpty(this.ntxtSecondDose.Text) && !string.IsNullOrEmpty(this.ncmbSecondDoseUnit.Text))
                {
                    Function.ShowMessage("请录入第二基本剂量！", MessageBoxIcon.Information);
                    this.ntxtSecondDose.Select();
                    this.ntxtSecondDose.Focus();
                    return false;
                }

                if (!string.IsNullOrEmpty(this.ntxtSecondDose.Text) && string.IsNullOrEmpty(this.ncmbSecondDoseUnit.Text))
                {
                    Function.ShowMessage("请选择第二剂量单位！", MessageBoxIcon.Information);
                    this.ncmbSecondDoseUnit.Select();
                    this.ncmbSecondDoseUnit.Focus();
                    return false;
                }
                if (!string.IsNullOrEmpty(this.ntxtSecondDose.Text))
                {
                    decimal secondDoseOnce = 0;
                    if (!decimal.TryParse(this.ntxtSecondDose.Text, out secondDoseOnce))
                    {
                        Function.ShowMessage("第二基本剂量录入不正确，请确认录入数字", MessageBoxIcon.Information);
                        this.ntxtSecondDose.Select();
                        this.ntxtSecondDose.Focus();
                        return false;
                    }
                    if (secondDoseOnce <= 0)
                    {
                        Function.ShowMessage("第二基本剂量录入不正确，录入的值必须大于0", MessageBoxIcon.Information);
                        this.ntxtSecondDose.Select();
                        this.ntxtSecondDose.Focus();
                        return false;
                    }
                }
            }



            if (this.IItemExtendControl != null)
            {
                int param = this.IItemExtendControl.CheckValid();
                if (param == -1)
                {
                    this.neuTabControl1.SelectedTab = this.tpExtend;
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 获取基本信息
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item GetItem()
        {

            if (this.item == null)
            {
                this.item = new FS.HISFC.Models.Pharmacy.Item();
            }
            FS.HISFC.Models.Pharmacy.Item item = this.item.Clone();

            //先获取扩展信息，防止本地化的代码更改主要信息
            string errInfo = "";
            if (this.IItemExtendControl != null)
            {
                FS.HISFC.Models.Pharmacy.Item exItem = this.IItemExtendControl.Get(ref errInfo);
                if (exItem == null)
                {
                    Function.ShowMessage("药品基本信息扩展信息显示发生问题，请向系统管理员报告错误信息：" + errInfo, MessageBoxIcon.Error);
                    return null;
                }

                item.SpecialFlag = exItem.SpecialFlag;
                item.SpecialFlag1 = exItem.SpecialFlag1;
                item.SpecialFlag2 = exItem.SpecialFlag2;
                item.SpecialFlag3 = exItem.SpecialFlag3;
                item.SpecialFlag4 = exItem.SpecialFlag4;

                item.ExtendData1 = exItem.ExtendData1;
                item.ExtendData2 = exItem.ExtendData2;
                item.ExtendData3 = exItem.ExtendData3;
                item.ExtendData4 = exItem.ExtendData4;

                item.ExtNumber1 = exItem.ExtNumber1;
                item.ExtNumber2 = exItem.ExtNumber2;
                //item.RetailPrice2 = exItem.RetailPrice2;
            }

            item.Name = this.txtName.Text;
            item.SpellCode = this.txtSpellCode.Text;
            item.WBCode = this.txtWbCode.Text;
            item.UserCode = this.txtUserCode.Text;
            item.NameCollection.RegularName = this.txtRegularName.Text;
            item.NameCollection.RegularSpell.SpellCode = this.txtRegularSpellCode.Text;
            item.NameCollection.RegularSpell.WBCode = this.txtRegularWbCode.Text;
            item.NameCollection.RegularSpell.UserCode = this.txtRegularUserCode.Text;
            item.NameCollection.FormalName = this.txtFormalName.Text;
            item.NameCollection.FormalSpell.SpellCode = this.txtFormalSpellCode.Text;
            item.NameCollection.FormalSpell.WBCode = this.txtFormalWbCode.Text;
            item.NameCollection.FormalSpell.UserCode = this.txtFormalUserCode.Text;
            item.NameCollection.OtherName = this.txtOtherName.Text;
            item.NameCollection.OtherSpell.SpellCode = this.txtOtherSpellCode.Text;
            item.NameCollection.OtherSpell.WBCode = this.txtOtherWbCode.Text;
            item.NameCollection.OtherSpell.UserCode = this.txtOtherUserCode.Text;
            item.NameCollection.EnglishName = this.txtEnglishName.Text;
            item.NameCollection.EnglishOtherName = this.txtEnglishOtherName.Text;
            item.NameCollection.EnglishRegularName = this.txtEnglishRegularName.Text;
            item.NameCollection.GbCode = this.txtGbCode.Text;
            item.NameCollection.InternationalCode = this.txtInternationalCode.Text;
            item.PackUnit = this.cmbPackUnit.Text.ToString().Trim();
            item.MinUnit = this.cmbMinUnit.Text.ToString().Trim();
            item.DoseUnit = this.cmbDoseUnit.Text.ToString().Trim();
            item.Type.ID = this.cmbDrugType.Tag.ToString().Trim();
            item.SysClass.ID = this.cmbSysClass.Tag.ToString();
            item.MinFee.ID = this.cmbMinFee.Tag.ToString();
            item.Quality.ID = this.cmbQuality.Tag.ToString();
            item.DosageForm.ID = this.cmbDosageForm.Tag.ToString();
            item.PriceCollection.PriceForm.ID = this.cmbPriceForm.Tag.ToString();
            item.Grade = this.cmbGrade.Tag.ToString();
            item.Specs = this.txtSpecs.Text;
            item.Usage.ID = this.cmbUsage.Tag.ToString();
            item.Frequency.ID = this.cmbFrequency.Tag.ToString();
            item.Product.Caution = this.txtCaution.Text;
            item.Ingredient = this.txtIngredient.Text;
            item.Product.StoreCondition = this.Text;
            item.ExecuteStandard = this.txtExecuteStandard.Text;
            item.PhyFunction1.ID = this.cmbPhyFunction1.Tag.ToString();
            item.PhyFunction2.ID = this.cmbPhyFunction2.Tag.ToString();
            item.PhyFunction3.ID = this.cmbPhyFunction3.Tag.ToString();
            item.Product.Producer.ID = this.cmbProducer.Tag.ToString();
            item.Memo = this.txtMemo.Text;
            item.Product.ApprovalInfo = this.txtApprovalInfo.Text;
            item.Product.Label = this.txtLabel.Text;
            item.Product.ProducingArea = this.txtProducingArea.Text;
            item.Product.Company.ID = this.cmbCompany.Tag.ToString();
            item.Product.BarCode = this.txtBarCode.Text;
            item.Product.BriefIntroduction = this.txtBriefIntroduction.Text;
            item.Product.Manual = this.txtManual.Text;
            item.TenderOffer.IsTenderOffer = this.chbIsTenderOffer.Checked;
            item.TenderOffer.Company.ID = this.cmbTenderCompancy.Tag.ToString();
            item.TenderOffer.ContractNO = this.txtContractCode.Text;
            item.TenderOffer.BeginTime = this.dtpBeginDate.Value;
            item.TenderOffer.EndTime = this.dtpEndDate.Value;
            //默认的变动类型为:修改
            item.ShiftType.ID = "U";           //变动类型(U更新, M特殊修改 ,N新药, S停用, A调价)

            //如果将在用的药品停用,则记录变动时间
            if (item.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid && this.chbIsStop.Checked)
            {
                item.ShiftType.ID = "S";       //变动类型(U更新, M特殊修改 ,N新药, S停用, A调价)
            }
            //add by caolin
            if (this.chbIsStop.Checked)
            {
                item.ValidState = FS.HISFC.Models.Base.EnumValidState.Invalid;
            }
            else
            {
                item.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
            }
            item.Product.IsSelfMade = this.chbIsSelfMade.Checked;
            item.IsAllergy = this.chbIsAllergy.Checked;
            item.IsGMP = this.chbIsGMP.Checked;
            item.IsOTC = this.chbIsOTC.Checked;
            item.IsShow = this.chbIsShow.Checked;
            item.IsLack = this.chbIsLack.Checked;
            item.IsSubtbl = this.chbIsAppend.Checked;
            item.Product.StoreCondition = this.cmbStoreCondition.Text;
            item.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.txtRetailPrice.Text);
            item.PriceCollection.WholeSalePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.txtWholesalePrice.Text);
            item.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPurchasePrice.Text);
            item.PriceCollection.TopRetailPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTopRetailPrice.Text);
            item.RetailPrice2 = FS.FrameWork.Function.NConvert.ToDecimal(this.ntxtRetailPrice2.Text);
            item.TenderOffer.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTenderPrice.Text);
            item.OnceDose = FS.FrameWork.Function.NConvert.ToDecimal(this.txtOnceDose.Text);
            item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBaseDose.Text);
            item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPackQty.Text);
            item.IsNostrum = this.ckNostrum.Checked;

            item.ShiftMark = this.txtStopReason.Text;
            //item.ShiftTime = itemManager.GetDateTimeFromSysDateTime();	//变动时间

            if (ncmbLZSplitType.SelectedIndex < 0)
                item.SplitType = DBNull.Value.ToString();
            else
                item.SplitType = this.ncmbSplitType.SelectedIndex.ToString();

            //显示属性 0 全院  1 住院处  2 门诊
            if (this.cmbShow.SelectedIndex < 0)
            {
                item.ShowState = DBNull.Value.ToString();
            }
            else
            {
                item.ShowState = this.cmbShow.SelectedIndex.ToString();
            }

            //增加住院长期医嘱拆分属性
            if (this.ncmbCDSplitType.SelectedIndex < 0)
            {
                item.CDSplitType = string.Empty;
            }
            else
            {
                item.CDSplitType = this.ncmbCDSplitType.SelectedIndex.ToString();
            }

            //增加住院临时医嘱拆分属性
            if (!isSplitLZAndOutPatient)
            {
                item.LZSplitType = item.SplitType;
            }
            else
            {
                if (this.ncmbLZSplitType.SelectedIndex < 0)
                {
                    item.LZSplitType = string.Empty;
                }

                else
                {
                    item.LZSplitType = this.ncmbLZSplitType.SelectedIndex.ToString();
                }
            }
            decimal secondBaseDose = 0;
            if (decimal.TryParse(this.ntxtSecondDose.Text, out secondBaseDose))
            {
                item.SecondBaseDose = secondBaseDose;
                item.SecondDoseUnit = this.ncmbSecondDoseUnit.Text;
            }
            if (this.IsCanChooseOnceDoseUnit)
            {
                item.OnceDoseUnit = this.cmbOnceDoseUnit.Text;
            }
            else
            {
                item.OnceDoseUnit = "";
            }

            return item;

        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (this.CheckValid())
            {
                FS.HISFC.Models.Pharmacy.Item item = this.GetItem();

                if (item != null)
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
                    item.ShiftTime = itemMgr.GetDateTimeFromSysDateTime();
                    ArrayList alItem = new ArrayList();
                    string errInfo = "";


                    if (string.IsNullOrEmpty(item.ID))
                    {
                        if (itemMgr.InsertItem(item) == -1)
                        {
                            item.ID = "";
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        alItem.Add(item.Clone());
                        if (FS.SOC.HISFC.Components.Pharmacy.Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Drug, ref errInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    else
                    {
                        //在界面打开到保存期间可能存在调价，获取最新零售价避免价格被调回，理论上避免不了并发
                        if (!this.txtRetailPrice.Enabled)
                        {
                            decimal nowRetailPrice = item.PriceCollection.RetailPrice;
                            if (itemMgr.GetNowPrice(item.ID, ref nowRetailPrice) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                                return;
                            }
                            item.PriceCollection.RetailPrice = nowRetailPrice;
                        }
                        if (itemMgr.UpdateItem(item) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                            return;
                        }
                        alItem.Add(item.Clone());
                        if (FS.SOC.HISFC.Components.Pharmacy.Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Modify, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Drug, ref errInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
                            return;
                        }
                    }

                    if (this.IItemExtendControl != null)
                    {
                        if (this.IItemExtendControl.Save(item, ref errInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            Function.ShowMessage("保存失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    if (this.EndSave != null)
                    {
                        this.EndSave(item);
                    }

                    Function.ShowMessage("保存成功！", MessageBoxIcon.Information);
                    if (this.continueCheckBox.Checked)
                    {
                        this.Clear();
                    }
                    else
                    {

                        this.Hide();

                    }
                }
            }
        }

        #endregion

        #region 事件
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }



        private void btSICompare_Click(object sender, EventArgs e)
        {
            if (this.CheckValid())
            {
                FS.HISFC.Models.Pharmacy.Item item = this.GetItem();

                if (item != null)
                {
                   
                    FS.SOC.Local.Pharmacy.ShenZhen.BizLogic.CompareSI compareMgr = new FS.SOC.Local.Pharmacy.ShenZhen.BizLogic.CompareSI();

                    ArrayList alcompare = new ArrayList();
                    ArrayList altemp = new ArrayList();
                    alcompare = compareMgr.GetUnCompareDrug(item.ID);

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    if (alcompare.Count>0)
                    {
                        if (myInterface.GetComparealItem("2",item.ID, ref altemp) == -1)
                        {
                            MessageBox.Show(myInterface.Err);
                            return ;
                        }
                        if (altemp.Count> 0)
                        {

                            if (myInterface.DeleteCompareItem("2", item.ID) <= 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show(myInterface.Err);
                                return ;

                            }
                        }
                        foreach (FS.HISFC.Models.SIInterface.Compare drugCompare in alcompare)
                        {
                            if (myInterface.InsertCompareItem(drugCompare) <= 0)
                            {
                                MessageBox.Show(myInterface.Err);
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                alcompare = null;
                            }
                        }
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("未找对照医保信息,请下载审批目录，再对照");
                        return ;
                    }

                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show("对照成功!");


                    #region  显示医保信息

                    ArrayList alCompare = new ArrayList();

                    this.fpSI_Sheet1.Rows.Count = 0;
                    myInterface.GetComparealItem("2", item.ID, ref alCompare);

                    if (alCompare != null && alCompare.Count > 0)
                    {
                        this.btSICompare.Visible = true;
                       
                        int i = 0;
                        foreach (FS.HISFC.Models.SIInterface.Compare compareInfo in alCompare)
                        {

                            this.fpSI_Sheet1.Rows.Add(i, 1);

                            this.fpSI_Sheet1.Cells[i, 0].Text = compareInfo.SpellCode.UserCode;    //编码
                            this.fpSI_Sheet1.Cells[i, 1].Text = compareInfo.CenterItem.Name;
                            this.fpSI_Sheet1.Cells[i, 2].Text = compareInfo.CenterItem.ID;
                            this.fpSI_Sheet1.Cells[i, 3].Text = compareInfo.CenterItem.ItemType;
                            this.fpSI_Sheet1.Cells[i, 4].Text = compareInfo.CenterItem.Memo.ToString();
                            if (compareInfo.CenterItem.ItemGrade == "0")
                            {
                                this.fpSI_Sheet1.Cells[i, 5].Text = "记账";
                            }
                            else
                            {
                                this.fpSI_Sheet1.Cells[i, 5].Text = "自费";
                            }
                            this.fpSI_Sheet1.Cells[i, 6].Text = compareInfo.CenterItem.OperDate.ToShortDateString();
                            this.fpSI_Sheet1.Cells[i, 7].Text = compareInfo.CenterItem.Memo;

                            i++;
                        }
                    }

                    #endregion


                }
            }



        }


 

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Hide();

        }

        void chbIsStop_CheckedChanged(object sender, EventArgs e)
        {
            this.txtStopReason.Visible = this.chbIsStop.Checked;
            this.lblStopReason.Visible = this.chbIsStop.Checked;
        }

        void cmbPhyFunction2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置三级药理作用
            if (this.cmbPhyFunction2.Tag != null)
            {
                ArrayList alLevel3 = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.PhaFunction info in this.alPhyFunction3)
                {
                    if (info.ParentNode == this.cmbPhyFunction2.Tag.ToString())
                    {
                        alLevel3.Add(info.Clone());
                    }
                }
                this.cmbPhyFunction3.AddItems(alLevel3);
                this.cmbPhyFunction3.Tag = null;
                this.cmbPhyFunction3.Text = "";
            }
        }

        void cmbPhyFunction1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置二级药理作用
            if (this.cmbPhyFunction1.Tag != null)
            {
                ArrayList alLevel2 = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.PhaFunction info in this.alPhyFunction2)
                {
                    if (info.ParentNode == this.cmbPhyFunction1.Tag.ToString())
                    {
                        alLevel2.Add(info.Clone());
                    }
                }
                this.cmbPhyFunction2.AddItems(alLevel2);
                this.cmbPhyFunction2.Tag = null;
                this.cmbPhyFunction2.Text = "";
            }
            //清空三级药理作用
            this.cmbPhyFunction3.AddItems(new ArrayList());
            this.cmbPhyFunction3.Tag = null;
            this.cmbPhyFunction3.Text = "";
        }

        void nbtNext_Click(object sender, EventArgs e)
        {
            if (this.GetNextItem != null)
            {
                this.GetNextItem(1);
            }
        }

        void nbtBack_Click(object sender, EventArgs e)
        {
            if (this.GetNextItem != null)
            {
                this.GetNextItem(-1);
            }
        }

        void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = FS.SOC.HISFC.Components.Pharmacy.Function.GetSpellCode(this.txtName.Text);
            this.txtSpellCode.Text = spell.SpellCode;
            this.txtWbCode.Text = spell.WBCode;
        }

        void txtOtherName_KeyUp(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = FS.SOC.HISFC.Components.Pharmacy.Function.GetSpellCode(this.txtOtherName.Text);
            this.txtOtherSpellCode.Text = spell.SpellCode;
            this.txtOtherWbCode.Text = spell.WBCode;
        }

        void txtFormalName_KeyUp(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = FS.SOC.HISFC.Components.Pharmacy.Function.GetSpellCode(this.txtFormalName.Text);
            this.txtFormalSpellCode.Text = spell.SpellCode;
            this.txtFormalWbCode.Text = spell.WBCode;
        }

        void txtRegularName_KeyUp(object sender, KeyEventArgs e)
        {
            FS.HISFC.Models.Base.Spell spell = FS.SOC.HISFC.Components.Pharmacy.Function.GetSpellCode(this.txtRegularName.Text);
            this.txtRegularSpellCode.Text = spell.SpellCode;
            this.txtRegularWbCode.Text = spell.WBCode;
        }

        /// <summary>
        ///获取零售价调价公式 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected string GetRetailPriceComputeFormula(FS.HISFC.Models.Pharmacy.Item item)
        {
            string formula = "";

            if (hsFormula == null || hsFormula.Count == 0)
            {
                return "";
            }
            if (hsFormula.Contains(item.Type.ID))
            {
                ArrayList allDataTemp = hsFormula[item.Type.ID] as ArrayList;
                foreach (FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula retailPriceFormula in allDataTemp)
                {
                    if (retailPriceFormula.ValidState != "1")
                    {
                        continue;
                    }
                    if (retailPriceFormula.DrugType.ID == item.Type.ID)
                    {
                        if (retailPriceFormula.PriceType == "0" && item.PriceCollection.PurchasePrice >= retailPriceFormula.PriceLower && item.PriceCollection.PurchasePrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                            break;
                        }
                        else if (retailPriceFormula.PriceType == "1" && item.PriceCollection.WholeSalePrice >= retailPriceFormula.PriceLower && item.PriceCollection.WholeSalePrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                            break;
                        }
                        else if (retailPriceFormula.PriceType == "2" && item.PriceCollection.RetailPrice >= retailPriceFormula.PriceLower && item.PriceCollection.RetailPrice <= retailPriceFormula.PriceUpper)
                        {
                            formula = retailPriceFormula.Name;
                            break;
                        }
                    }
                }
                formula = formula.Replace("购入价", item.PriceCollection.PurchasePrice.ToString("F4").TrimEnd('0').TrimEnd('.'));
                formula = formula.Replace("批发价", item.PriceCollection.WholeSalePrice.ToString("F4").TrimEnd('0').TrimEnd('.'));
                formula = formula.Replace("零售价", item.PriceCollection.RetailPrice.ToString("F4").TrimEnd('0').TrimEnd('.'));

                return formula;
            }
            else
            {
                return "";
            }

        }

        /// <summary>
        /// 增加基本剂量textBox控件输入数字判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBaseDose_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str = this.txtBaseDose.Text;
            e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
            if (e.KeyChar == (char)8)   //允许输入回退键
            {
                e.Handled = false;
                return;
            }
            if (e.KeyChar == (char)45)
            {
                if (str == "")
                {
                    e.Handled = false;
                    return;
                }
                else
                {
                    e.Handled = true;
                    return;
                }
            }
            if (e.KeyChar == (char)46)
            {
                if (str == "")   //第一个不允许输入小数点
                {
                    e.Handled = true;
                    return;
                }
                else
                { //小数点不允许出现2次
                    foreach (char ch in str)
                    {
                        if (ch == (char)45)
                        {
                            continue;
                        }
                        if (char.IsPunctuation(ch))
                        {
                            e.Handled = true;
                            return;
                        }
                    }
                    e.Handled = false;
                }
            }

        }

        /// <summary>
        /// 购入价按公式生成零售价（只有新增的情况下才生成）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPurchasePrice_TextChanged(object sender, EventArgs e)
        {
            if (item != null && string.IsNullOrEmpty(item.ID))
            {
                itemTemp = new FS.HISFC.Models.Pharmacy.Item();
                itemTemp.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPurchasePrice.Text);
                itemTemp.Type.ID = this.cmbDrugType.Tag.ToString();
                if (string.IsNullOrEmpty(itemTemp.Type.ID) || itemTemp.PriceCollection.PurchasePrice == 0)
                {
                    return;
                }
                else
                {
                    string formula = this.GetRetailPriceComputeFormula(itemTemp);
                    if (!string.IsNullOrEmpty(formula))
                    {
                        this.txtRetailPrice.Text = FS.FrameWork.Public.String.ExpressionVal(formula).ToString();
                    }
                }
            }
        }

        /// <summary>
        /// 自动生成规格，只有规格处未录入才生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSpecs_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.autoCreateSpecs))
            {
                decimal baseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBaseDose.Text);
                string doseUnit = this.cmbDoseUnit.Text;

                decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPackQty.Text);
                string minUnit = this.cmbMinUnit.Text;
                string packUnit = this.cmbPackUnit.Text;
                if (string.IsNullOrEmpty(this.txtSpecs.Text))
                {
                    this.txtSpecs.Text = string.Format(this.autoCreateSpecs, baseDose.ToString(), doseUnit, packQty, minUnit, packUnit);
                }
            }
        }

        /// <summary>
        /// 如果临嘱拆分和常嘱拆分该事件触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ncmbSplitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isSplitLZAndOutPatient)
            {
                if (this.ncmbSplitType.SelectedIndex != -1)
                {
                    this.ncmbLZSplitType.SelectedIndex = this.ncmbSplitType.SelectedIndex;
                }
            }
        }


        void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (this.neuTabControl1.SelectedTab == this.tpOther)
            {
                ArrayList al = new ArrayList();

                if (this.cmbDoseUnit.Text != "")
                {
                    FS.FrameWork.Models.NeuObject neuObject = new FS.FrameWork.Models.NeuObject();
                    neuObject.ID = "0";
                    neuObject.Name = this.cmbDoseUnit.Text;
                    al.Add(neuObject);
                }
                if (this.IsCanChooseOnceDoseUnit)
                {

                    if (this.ncmbSecondDoseUnit.Text != "")
                    {
                        FS.FrameWork.Models.NeuObject neuObject = new FS.FrameWork.Models.NeuObject();
                        neuObject.ID = "1";
                        neuObject.Name = this.ncmbSecondDoseUnit.Text;
                        al.Add(neuObject);
                    }

                    if (this.cmbMinUnit.Text != "")
                    {
                        FS.FrameWork.Models.NeuObject neuObject = new FS.FrameWork.Models.NeuObject();
                        neuObject.ID = "2";
                        neuObject.Name = this.cmbMinUnit.Text;
                        al.Add(neuObject);
                    }

                }
                this.cmbOnceDoseUnit.AddItems(al);
                if (!this.IsCanChooseOnceDoseUnit)
                {
                    this.cmbOnceDoseUnit.Text = this.cmbDoseUnit.Text;
                }
            }
        }

        void cmbPackUnit_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.cmbPackUnit.Text))
            {
                this.nlbPriceUnit.Text = "元/" + this.cmbPackUnit.Text;
            }
        }



        #endregion

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                if (this.btnSave.Visible && this.btnSave.Enabled)
                    this.btnSave_Click(null, null);
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.C.GetHashCode())
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Up.GetHashCode())
            {
                if (this.GetNextItem != null)
                {
                    this.GetNextItem(-1);
                }

            }
            else if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.Down.GetHashCode())
            {
                if (this.GetNextItem != null)
                {
                    this.GetNextItem(1);
                }
            }
            return base.ProcessDialogKey(keyData);
        }


    }
}
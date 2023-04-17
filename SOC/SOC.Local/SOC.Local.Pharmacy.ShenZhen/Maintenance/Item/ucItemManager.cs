using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Maintenance.Item
{
    /// <summary>
    /// [功能描述: 药品基本信息管理]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucItemManager : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucItemManager()
        {
            InitializeComponent();
        }

        #region 变量属性

        /// <summary>
        /// 配置文件
        /// </summary>
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPPharmacyItemManagerSetting.xml";

        /// <summary>
        /// FarPoint配置文件
        /// </summary>
        [Description("汇总信息FarPoint配置文件"), Category("设置"), Browsable(true)]
        public string SettingFile
        {
            get { return settingFile; }
        }

        public bool isCanChooseOnceDoseUnit = false;
        /// <summary>
        /// 每次用量是否可以选取最小单位
        /// </summary>
        [Description("每次用量是否可以选取最小单位"), Category("设置"), Browsable(true)]
        public Boolean IsCanChooseOnceDoseUnit
        {
            get { return this.isCanChooseOnceDoseUnit; }
            set
            {
                this.isCanChooseOnceDoseUnit = value;
            }
        }

        public bool isSplitLZAndOutPatient = false;

        /// <summary>
        /// 门诊和住院临瞩是否分开维护拆分属性
        /// </summary>
        [Description("门诊和住院临瞩是否分开维护拆分属性"), Category("设置"), Browsable(true)]
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

        private bool isShowSecondDosage = false;

        /// <summary>
        /// 是否显示第二剂量维护信息
        /// </summary>
        [Description("是否显示第二剂量维护信息"), Category("设置"), Browsable(true)]
        public bool IsShowSecondDosage
        {
            get { return isShowSecondDosage; }
            set { isShowSecondDosage = value; }
        }

        /// <summary>
        /// 药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = null;

        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        FS.SOC.HISFC.Components.Pharmacy.Maintenance.BaseCache baseCache = new FS.SOC.HISFC.Components.Pharmacy.Maintenance.BaseCache();

        private frmItem frmItem = new frmItem();
        private FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

        #endregion

        #region 工具栏
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("新增", "新增", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("修改", "修改", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("复制", "复制", FS.FrameWork.WinForms.Classes.EnumImageList.F复制, true, false, null);
            toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);

            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "新增")
            {
                this.Add();
            }
            else if (e.ClickedItem.Text == "复制")
            {
                this.Copy();
            }
            else if (e.ClickedItem.Text == "修改")
            {
                this.Modify();
            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.Delete();
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        private int Init()
        {
            try
            {
                this.InitBaseData();
                this.InitDataTable();
                this.InitFarPoint();

                this.frmItem.IsCanChooseOnceDoseUnit = this.isCanChooseOnceDoseUnit;
                this.frmItem.IsSplitLZAndOutPatient = this.isSplitLZAndOutPatient;
                this.frmItem.IsShowSecondDosage = this.isShowSecondDosage;
                this.frmItem.Init(this.baseCache);

                this.frmItem.GetNextItem -= new frmItem.GetNextItemHandler(this.frmItem_GetNextItem);
                this.frmItem.GetNextItem += new frmItem.GetNextItemHandler(this.frmItem_GetNextItem);
                this.frmItem.EndSave -= new frmItem.SaveItemHandler(this.frmItem_EndSave);
                this.frmItem.EndSave += new frmItem.SaveItemHandler(this.frmItem_EndSave);


                this.nrbSortByCustomNO.Checked = FS.FrameWork.Function.NConvert.ToBoolean(SOC.Public.XML.SettingFile.ReadSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "ItemMaintenance", "SortType", "False"));
            }
            catch (Exception ex)
            {
                Function.ShowMessage("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        private void InitFarPoint()
        {
            if (!this.neuSpread.ReadSchema(this.settingFile))
            {
                this.sheetView1.ColumnHeader.Rows[0].Height = 34f;
            }
        }

        private void InitBaseData()
        {
            this.baseCache.Init();
            this.ncmbDrugType.AddItems(this.baseCache.drugTypeHelper.ArrayObject);
            this.ncmbDrugQuality.AddItems(this.baseCache.drugQualityHelper.ArrayObject);
            this.ncmbDoseModual.AddItems(this.baseCache.doseFormHelper.ArrayObject);
            this.ncmbFunction1.AddItems(this.baseCache.function1Helper.ArrayObject);
            this.ncmbFunction2.AddItems(this.baseCache.function2Helper.ArrayObject);
            this.ncmbFunction3.AddItems(this.baseCache.function3Helper.ArrayObject);
            this.ncmbCompany.AddItems(this.baseCache.companyHelper.ArrayObject);
        }

        private void InitDataTable()
        {
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型
            System.Type dtStr = System.Type.GetType("System.String");
            System.Type dtDec = System.Type.GetType("System.Decimal");
            System.Type dtDTime = System.Type.GetType("System.DateTime");
            System.Type dtBool = System.Type.GetType("System.Boolean");
            this.dtItems.Columns.AddRange(new DataColumn[]{
                new DataColumn("系统编码", dtStr),
				new DataColumn("自定义码", dtStr),
                new DataColumn("国家编码", dtStr), 
				new DataColumn("商品名", dtStr),  
				new DataColumn("通用名", dtStr),     
				new DataColumn("规格", dtStr),                                     
				new DataColumn("零售价", dtDec),     
				new DataColumn("零差价", dtDec),     
				new DataColumn("包装单位", dtStr),   
				new DataColumn("包装数量", dtDec),   
				new DataColumn("最小单位", dtStr),   
				new DataColumn("基本剂量", dtDec),   
				new DataColumn("剂量单位", dtStr),   
                new DataColumn("第二基本剂量", dtDec),   
				new DataColumn("第二剂量单位", dtStr),   
                new DataColumn("门诊拆分",dtStr),
                new DataColumn("临嘱拆分", dtStr),
                new DataColumn("长嘱拆分", dtStr),
				new DataColumn("药品性质", dtStr),   
                new DataColumn("系统类别", dtStr),
				new DataColumn("药品类型", dtStr),   
				new DataColumn("剂型", dtStr),       
				new DataColumn("价格形式", dtStr),   
				new DataColumn("药品等级", dtStr),   
				new DataColumn("批发价", dtDec),     
				new DataColumn("购入价", dtDec),     
				new DataColumn("最高零售价", dtDec), 
				new DataColumn("停用", dtBool),       
				new DataColumn("自制", dtBool),       
				new DataColumn("试敏", dtBool),       
				new DataColumn("GMP", dtBool),        
				new DataColumn("OTC", dtBool),        
				new DataColumn("显示", dtBool),       
				new DataColumn("使用方法", dtStr),   
				new DataColumn("一次用量", dtDec),   
				new DataColumn("一次用量单位", dtStr),   
				new DataColumn("频次", dtStr),       
				new DataColumn("注意事项", dtStr),   
				new DataColumn("有效成份", dtStr),   
				new DataColumn("储藏条件", dtStr),   
				new DataColumn("执行标准", dtStr),   
				new DataColumn("一级药理作用", dtStr),  
				new DataColumn("二级药理作用", dtStr), 
				new DataColumn("三级药理作用", dtStr), 
				new DataColumn("招标药", dtBool),     
				new DataColumn("供货公司", dtStr),   
				new DataColumn("生产厂家", dtStr),   
				new DataColumn("批文信息", dtStr),   
				new DataColumn("注册商标", dtStr),   
				new DataColumn("产地", dtStr),       
				new DataColumn("条形码", dtStr),     
				new DataColumn("学名", dtStr),       
				new DataColumn("别名", dtStr),       
				new DataColumn("英文商品名", dtStr),   
				new DataColumn("英文别名", dtStr),   
				new DataColumn("英文通用名", dtStr), 
				new DataColumn("备注", dtStr),     
				new DataColumn("拼音码", dtStr),
				new DataColumn("五笔码", dtStr),     															  
				new DataColumn("通用名拼音码", dtStr),     
				new DataColumn("通用名五笔码", dtStr),
                new DataColumn("通用名自定义码",  dtStr),
                new DataColumn("学名拼音码",    dtStr),
                new DataColumn("学名五笔码",    dtStr),
                new DataColumn("学名自定义码",  dtStr),
                new DataColumn("别名拼音码",    dtStr),
                new DataColumn("别名五笔码",    dtStr),
                new DataColumn("别名自定义码",  dtStr),
                new DataColumn("特殊标记1",  dtStr),
                new DataColumn("特殊标记2",  dtStr),
                new DataColumn("特殊标记3",  dtStr),
                new DataColumn("特殊标记4",  dtStr),
                new DataColumn("特殊标记5",  dtStr),
                new DataColumn("字符扩展1",  dtStr),
                new DataColumn("字符扩展2",  dtStr),
                new DataColumn("字符扩展3",  dtStr),
                new DataColumn("字符扩展4",  dtStr),
                new DataColumn("数字扩展1",  dtDec),
                new DataColumn("数字扩展2",  dtDec),
            });


            foreach (DataColumn dc in this.dtItems.Columns)
            {
                dc.ReadOnly = true;
            }



            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["系统编码"];

            this.dtItems.PrimaryKey = keys;

            this.sheetView1.DataSource = this.dtItems.DefaultView;

        }

        #endregion

        #region 查询，数据显示到FarPoint
        private void Query()
        {
            this.hsItem.Clear();
            this.dtItems.Clear();
            this.sheetView1.DataSource = null;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询...");
            Application.DoEvents();

            FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> listItem = itemMgr.QueryItemList();
            if (listItem == null)
            {
                Function.ShowMessage("查询药品基本信息出错，请向系统管理员报告错误信息：" + itemMgr.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            ArrayList alItem = new ArrayList(listItem.ToArray());
            if (this.nrbSortByCustomNO.Checked)
            {
                alItem.Sort(new CompareByCustomerCode());
            }

            int progress = 1;

            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, listItem.Count);
                if (this.Filter(item))
                {
                    continue;
                }
                if (this.AddItemObjectToDataTable(item) != 1)
                {
                    continue;
                }
            }

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.dtItems.AcceptChanges();
            this.sheetView1.DataSource = this.dtItems.DefaultView;
            this.neuSpread.ReadSchema(this.settingFile);
        }

        private bool Filter(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (this.CheckComoboxSelectedValueValid(this.ncmbDrugType) && this.ncmbDrugType.Tag.ToString() != item.Type.ID)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbDrugQuality) && this.ncmbDrugQuality.Tag.ToString() != item.Quality.ID)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbDoseModual) && this.ncmbDoseModual.Tag.ToString() != item.DosageForm.ID)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbFunction1) && this.ncmbFunction1.Tag.ToString() != item.PhyFunction1.ID)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbFunction2) && this.ncmbFunction2.Tag.ToString() != item.PhyFunction2.ID)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbFunction3) && this.ncmbFunction3.Tag.ToString() != item.PhyFunction3.ID)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbCompany) && this.ncmbCompany.Tag.ToString() != item.Product.Company.ID)
            {
                return true;
            }


            if (this.ncbStop.Checked && !this.ncbValid.Checked && item.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                return true;
            }

            if (this.ncbValid.Checked && !this.ncbStop.Checked && item.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                return true;
            }

            if (this.ncbAllergy.Checked && !item.IsAllergy)
            {
                return true;
            }

            return false;
        }

        private bool CheckComoboxSelectedValueValid(ComboBox comboBox)
        {
            if (string.IsNullOrEmpty(comboBox.Text) || comboBox.Tag == null || string.IsNullOrEmpty(comboBox.Tag.ToString()))
            {
                return false;
            }

            return true;
        }

        private int AddItemObjectToDataTable(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (item == null)
            {
                Function.ShowMessage("向DataTable中添加药品基本信息失败：药品基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtItems == null)
            {
                Function.ShowMessage("向DataTable中添加药品基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsItem.Contains(item.ID))
            {
                Function.ShowMessage("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                return -1;
            }
            else
            {
                this.hsItem.Add(item.ID, item);
            }

            DataRow row = this.dtItems.NewRow();

            this.SetDataRowValue(item, row);

            this.dtItems.Rows.Add(row);

            return 1;
        }

        private int SetDataRowValue(FS.HISFC.Models.Pharmacy.Item item, DataRow row)
        {
            row["系统编码"] = item.ID;
            row["自定义码"] = item.UserCode;
            row["国家编码"] = item.GBCode;
            row["商品名"] = item.Name;
            row["通用名"] = item.NameCollection.RegularName;
            row["规格"] = item.Specs;
            row["零售价"] = item.PriceCollection.RetailPrice;
            row["零差价"] = item.RetailPrice2;
            row["包装单位"] = item.PackUnit;
            row["包装数量"] = item.PackQty;
            row["最小单位"] = item.MinUnit;
            row["基本剂量"] = item.BaseDose;
            row["剂量单位"] = item.DoseUnit;
            row["第二基本剂量"] = item.SecondBaseDose;
            row["第二剂量单位"] = item.SecondDoseUnit;
            if (!string.IsNullOrEmpty(item.SplitType))
            {
                row["门诊拆分"] = ((FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType)(FS.FrameWork.Function.NConvert.ToInt32(item.SplitType))).ToString();
            }
            if (!string.IsNullOrEmpty(item.LZSplitType))
            {
                row["临嘱拆分"] = ((FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType)(FS.FrameWork.Function.NConvert.ToInt32(item.LZSplitType))).ToString();
            }
            if (!string.IsNullOrEmpty(item.CDSplitType))
            {
                row["长嘱拆分"] = ((FS.SOC.HISFC.Components.Pharmacy.Maintenance.EnumSplitType)(FS.FrameWork.Function.NConvert.ToInt32(item.CDSplitType))).ToString();
            }
            row["药品性质"] = baseCache.drugQualityHelper.GetName(item.Quality.ID);
            row["系统类别"] = item.SysClass.Name;
            row["药品类型"] = baseCache.drugTypeHelper.GetName(item.Type.ID);
            row["剂型"] = baseCache.doseFormHelper.GetName(item.DosageForm.ID);
            row["价格形式"] = item.PriceCollection.PriceForm.ID;
            row["药品等级"] = item.Grade;
            row["批发价"] = item.PriceCollection.WholeSalePrice;
            row["购入价"] = item.PriceCollection.PurchasePrice;
            row["最高零售价"] = item.PriceCollection.TopRetailPrice;
            row["零差价"] = item.RetailPrice2;

            row["停用"] = !(item.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid);
            row["自制"] = item.Product.IsSelfMade;
            row["试敏"] = item.IsAllergy;
            row["GMP"] = item.IsGMP;
            row["OTC"] = item.IsOTC;
            row["显示"] = item.IsShow;

            row["使用方法"] = item.Usage.ID;
            row["一次用量"] = item.OnceDose;
            row["一次用量单位"] = item.OnceDoseUnit;
            row["频次"] = item.Frequency.ID;
            row["注意事项"] = item.Product.Caution;
            row["有效成份"] = item.Ingredient;
            row["储藏条件"] = item.Product.StoreCondition;
            row["执行标准"] = item.ExecuteStandard;

            row["一级药理作用"] = baseCache.function1Helper.GetName(item.PhyFunction1.ID);
            row["二级药理作用"] = baseCache.function2Helper.GetName(item.PhyFunction2.ID);
            row["三级药理作用"] = baseCache.function3Helper.GetName(item.PhyFunction3.ID);

            row["招标药"] = item.TenderOffer.IsTenderOffer;
            row["供货公司"] = baseCache.companyHelper.GetName(item.Product.Company.ID);
            row["生产厂家"] = baseCache.producerHelper.GetName(item.Product.Producer.ID);
            row["批文信息"] = item.Product.ApprovalInfo;
            row["注册商标"] = item.Product.Label;
            row["产地"] = item.Product.ProducingArea;
            row["条形码"] = item.Product.BarCode;

            row["学名"] = item.NameCollection.FormalName;
            row["别名"] = item.NameCollection.OtherName;
            row["英文商品名"] = item.NameCollection.EnglishName;
            row["英文别名"] = item.NameCollection.EnglishOtherName;
            row["英文通用名"] = item.NameCollection.EnglishRegularName;
            row["备注"] = item.Memo;

            row["拼音码"] = item.NameCollection.SpellCode;
            row["五笔码"] = item.NameCollection.WBCode;

            row["通用名拼音码"] = item.NameCollection.RegularSpell.SpellCode;
            row["通用名五笔码"] = item.NameCollection.RegularSpell.WBCode;
            row["通用名自定义码"] = item.NameCollection.RegularSpell.UserCode;


            row["学名拼音码"] = item.NameCollection.FormalSpell.SpellCode;
            row["学名五笔码"] = item.NameCollection.FormalSpell.WBCode;
            row["学名自定义码"] = item.NameCollection.FormalSpell.UserCode;

            row["别名拼音码"] = item.NameCollection.OtherSpell.SpellCode;
            row["别名五笔码"] = item.NameCollection.OtherSpell.WBCode;
            row["别名自定义码"] = item.NameCollection.OtherSpell.UserCode;

            row["特殊标记1"] = item.SpecialFlag;
            row["特殊标记2"] = item.SpecialFlag1;
            row["特殊标记3"] = item.SpecialFlag2;
            row["特殊标记4"] = item.SpecialFlag3;
            row["特殊标记5"] = item.SpecialFlag4;

            row["字符扩展1"] = item.ExtendData1;
            row["字符扩展2"] = item.ExtendData2;
            row["字符扩展3"] = item.ExtendData3;
            row["字符扩展4"] = item.ExtendData4;

            row["数字扩展1"] = item.ExtNumber1;
            row["数字扩展2"] = item.ExtNumber1;


            return 1;
        }

        #endregion

        #region 修改、增加、删除
        private void Modify()
        {
            if ( !FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive("0301", "01"))
            {
                Function.ShowMessage("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "系统编码");
            if (this.hsItem.Contains(drugNO))
            {
                FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
                int param = storageMgr.GetDrugStorageRowNum(drugNO);
                if (param == -1)
                {
                    Function.ShowMessage("判断药品使用情况时获取库存失败，请向系统管理员联系并报告错误：" + storageMgr.Err, MessageBoxIcon.Error);
                    return;
                }
                //this.frmItem.Clear();//界面信息不用清空，会重新赋值
                this.frmItem.SetItem(hsItem[drugNO] as FS.HISFC.Models.Pharmacy.Item, param > 0);
                this.frmItem.ShowDialog();
            }
        }

        private void Add()
        {
            if (!FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive("0301", "01"))
            {
                Function.ShowMessage("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }
            //this.frmItem.Clear();
            this.frmItem.SetItem(new FS.HISFC.Models.Pharmacy.Item(), false);
            this.frmItem.ShowDialog();
        }

        private void Copy()
        {
            if (!FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive("0301", "01"))
            {
                Function.ShowMessage("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "系统编码");
            if (this.hsItem.Contains(drugNO))
            {
                FS.HISFC.Models.Pharmacy.Item item = (hsItem[drugNO] as FS.HISFC.Models.Pharmacy.Item).Clone();
                item.ID = "";
                this.frmItem.SetItem(item, false);
                this.frmItem.ShowDialog();
            }
        }

        private void Delete()
        {
            if (!FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive("0301", "01"))
            {
                Function.ShowMessage("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("删除后的信息不可以恢复，确认删除吗？", "提示>>", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "系统编码");
            FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
            int count = storageMgr.GetDrugStorageRowNum(drugNO);
            if (count > 0)
            {
                MessageBox.Show("此药品在库存中已存在,不允许删除!", "删除提示");
                return;
            }
            if (hsItem.Contains(drugNO))
            {
                FS.HISFC.Models.Pharmacy.Item item = hsItem[drugNO] as FS.HISFC.Models.Pharmacy.Item;

                FS.SOC.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                if (itemMgr.DeleteItem(item.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员联系并报告错误：" + itemMgr.Err, MessageBoxIcon.Error);
                    return;
                }

                string errInfo = "";
                ArrayList alDrug = new ArrayList();
                alDrug.Add(item);
                int param = FS.SOC.HISFC.Components.Pharmacy.Function.SendBizMessage(alDrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Drug, ref errInfo);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    Function.ShowMessage("删除失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                DataRow row = this.dtItems.Rows.Find(new string[] { item.ID });
                if (row != null)
                {
                    dtItems.Rows.Remove(row);
                }
                hsItem.Remove(item.ID);
            }
        }


        #endregion

        #region 过滤
        private void Filter()
        {
            string filter = FS.SOC.HISFC.Components.Pharmacy.Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.ntxtFilter.Text + "%");
            this.dtItems.DefaultView.RowFilter = filter;
            this.neuSpread.ReadSchema(this.settingFile);
        }
        #endregion

        #region 批量发送消息
        private void MessageAllItem()
        {
            if (hsItem == null || hsItem.Count == 0 || this.sheetView1.RowCount == 0)
            {
                return;
            }
            if (!((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                return;
            }

            string errInfo = "";
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在发送消息...");
            Application.DoEvents();
            ArrayList alItem = new ArrayList();
            for (int rowIndex = 0; rowIndex < this.sheetView1.RowCount; rowIndex++)
            {
                string drugNO = this.neuSpread.GetCellText(0, rowIndex, "系统编码");
                if (hsItem.Contains(drugNO))
                {
                    alItem.Add(hsItem[drugNO]);
                }
            }
            if (FS.SOC.HISFC.Components.Pharmacy.Function.SendBizMessage(alItem, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Add, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Drug, ref errInfo) == -1)
            {
                Function.ShowMessage("发送消息失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.None);
                return;
            }
            Function.ShowMessage("发送成功！", MessageBoxIcon.None);
        }
        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() == 1)
            {
                this.Query();
            }

            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.ncbMutiQuery.CheckedChanged += new EventHandler(ncbMutiQuery_CheckedChanged);
            this.neuSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread_CellDoubleClick);
            this.ntxtFilter.TextChanged += new EventHandler(ntxtFilter_TextChanged);
            this.ncmbFunction1.SelectedIndexChanged += new EventHandler(ncmbFunction1_SelectedIndexChanged);
            this.ncmbFunction2.SelectedIndexChanged += new EventHandler(ncmbFunction2_SelectedIndexChanged);
            this.ncmbFunction1.TextChanged += new EventHandler(ncmbFunction1_TextChanged);
            this.neuSpread.KeyDown += new KeyEventHandler(neuSpread_KeyDown);
            this.nrbSortByCustomNO.CheckedChanged += new EventHandler(nrbSortByCustomNO_CheckedChanged);
            this.nllReset.LinkClicked += new LinkLabelLinkClickedEventHandler(nllReset_LinkClicked);

            base.OnLoad(e);
        }

        void nllReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (System.IO.File.Exists(this.settingFile))
            {
                System.IO.File.Delete(this.settingFile);
            }
            try
            {
                string r = (new Random()).Next().ToString();
                this.dtItems.DefaultView.RowFilter = r + "=" + r;
            }
            catch { }

        }


        void neuSpread_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.F10)
            {
                this.MessageAllItem();
            }
        }

        public override int Export(object sender, object neuObject)
        {
            if (!FS.SOC.HISFC.Components.Pharmacy.Function.JugePrive("0301", "01"))
            {
                Function.ShowMessage("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return 0;
            }
            this.neuSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "系统编码");
            if (this.hsItem.Contains(drugNO))
            {
                ArrayList alItem = new ArrayList();
                alItem.Add(hsItem[drugNO] as FS.HISFC.Models.Pharmacy.Item);
                FS.SOC.HISFC.Components.Pharmacy.Function.PrintBill("0301", "01", alItem);
            }
            return base.OnPrint(sender, neuObject);
        }

        void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        void neuSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.Modify();
        }

        void ncbMutiQuery_CheckedChanged(object sender, EventArgs e)
        {
            this.ngbQuerySet.Visible = this.ncbMutiQuery.Checked;
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void ncmbFunction1_TextChanged(object sender, EventArgs e)
        {
            if (this.ncmbFunction1.Text == "")
            {
                this.ncmbFunction2.AddItems(this.baseCache.function2Helper.ArrayObject);
                this.ncmbFunction3.AddItems(this.baseCache.function3Helper.ArrayObject);
            }
        }

        void ncmbFunction2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置三级药理作用
            if (this.ncmbFunction2.Tag != null)
            {
                ArrayList alLevel3 = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.PhaFunction info in this.baseCache.function3Helper.ArrayObject)
                {
                    if (info.ParentNode == this.ncmbFunction2.Tag.ToString())
                    {
                        alLevel3.Add(info);
                    }
                }
                this.ncmbFunction3.AddItems(alLevel3);
                this.ncmbFunction3.Tag = null;
                this.ncmbFunction3.Text = "";
            }
        }

        void ncmbFunction1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //设置二级药理作用
            if (this.ncmbFunction1.Tag != null)
            {
                ArrayList alLevel2 = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.PhaFunction info in this.baseCache.function2Helper.ArrayObject)
                {
                    if (info.ParentNode == this.ncmbFunction1.Tag.ToString())
                    {
                        alLevel2.Add(info);
                    }
                }
                this.ncmbFunction2.AddItems(alLevel2);
                this.ncmbFunction2.Tag = null;
                this.ncmbFunction2.Text = "";
            }
            //清空三级药理作用
            this.ncmbFunction3.AddItems(new ArrayList());
            this.ncmbFunction3.Tag = null;
            this.ncmbFunction3.Text = "";
        }

        void frmItem_EndSave(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (hsItem.Contains(item.ID))
            {
                DataRow row = this.dtItems.Rows.Find(new string[] { item.ID });
                if (row != null)
                {
                    foreach (DataColumn dc in this.dtItems.Columns)
                    {
                        dc.ReadOnly = false;
                    }
                    this.SetDataRowValue(item, row);
                    row.AcceptChanges();
                    foreach (DataColumn dc in this.dtItems.Columns)
                    {
                        dc.ReadOnly = true;
                    }
                }
                hsItem[item.ID] = item;
            }
            else
            {
                this.AddItemObjectToDataTable(item);
            }

        }

        void frmItem_GetNextItem(int span)
        {
            this.sheetView1.ActiveRowIndex += span;
            this.neuSpread.ShowRow(0, this.sheetView1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "系统编码");
            if (this.hsItem.Contains(drugNO))
            {
                FS.SOC.HISFC.BizLogic.Pharmacy.Storage storageMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Storage();
                int param = storageMgr.GetDrugStorageRowNum(drugNO);
                if (param == -1)
                {
                    Function.ShowMessage("判断药品使用情况时获取库存失败，请向系统管理员联系并报告错误：" + storageMgr.Err, MessageBoxIcon.Error);
                    return;
                }

                this.frmItem.SetItem(hsItem[drugNO] as FS.HISFC.Models.Pharmacy.Item, param > 0);
            }
        }

        void nrbSortByCustomNO_CheckedChanged(object sender, EventArgs e)
        {
            SOC.Public.XML.SettingFile.SaveSetting(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacySetting.xml", "ItemMaintenance", "SortType", nrbSortByCustomNO.Checked.ToString());
        }


        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            return 1;
        }

        #endregion

        internal class CompareByCustomerCode : System.Collections.IComparer
        {
            public int Compare(object x, object y)
            {
                string oX = "";
                string oY = "";
                if (x is FS.HISFC.Models.Pharmacy.Item && y is FS.HISFC.Models.Pharmacy.Item)
                {
                    oX = (x as FS.HISFC.Models.Pharmacy.Item).UserCode;
                    oY = (y as FS.HISFC.Models.Pharmacy.Item).UserCode;
                }
                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? 1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

        }

    }
}

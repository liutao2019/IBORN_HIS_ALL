using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Item
{
    /// <summary>
    /// [功能描述: 物价基本信息管理]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
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
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPFeeItemManagerSetting.xml";

        /// <summary>
        /// 最小费用帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper minFeeObjectHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 项目等级帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemGradeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 申请单帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper applicabilityHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// 价格形式帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper itemPriceTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// FarPoint配置文件
        /// </summary>
        [Description("汇总信息FarPoint配置文件"), Category("设置"), Browsable(true)]
        public string SettingFile
        {
            get { return settingFile; }
        }

        private bool isMaterialCode = false;

        /// <summary>
        /// 是否启用物资编码
        /// </summary>
        [Description("是否启用物资编码检索物资信息"), Category("设置"), Browsable(true)]
        public Boolean IsMaterialCode
        {
            get { return this.isMaterialCode; }
            set
            {
                this.isMaterialCode = value;
            }
        }

        private bool isUserCodeAutoGenerate = false;

        /// <summary>
        /// 是否启用物资编码
        /// </summary>
        [Description("是否启用自定义码自动生成"), Category("设置"), Browsable(true), DefaultValue(false)]
        public Boolean IsUserCodeAutoGenerate
        {
            get 
            {
                return this.isUserCodeAutoGenerate; 
            }
            set
            {
                this.isUserCodeAutoGenerate = value;
            }
        }


        /// <summary>
        /// 非药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = null;

        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();
        private ucItemNew ucItem = null;
        private Form frmItem = null;
        private FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();
        private FS.SOC.HISFC.Fee.BizLogic.Undrug undrugManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region 工具栏
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("新增", "新增", FS.FrameWork.WinForms.Classes.EnumImageList.X新建, true, false, null);
            toolBarService.AddToolButton("修改", "修改", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("复制", "复制", FS.FrameWork.WinForms.Classes.EnumImageList.F复制, true, false, null);
            toolBarService.AddToolButton("删除", "删除", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("作废", "作废", FS.FrameWork.WinForms.Classes.EnumImageList.Z作废, true, false, null);
            toolBarService.AddToolButton("生成笔画码", "生成笔画码", FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);

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
            else if (e.ClickedItem.Text == "作废")
            {
                this.Invalid();
            }
            else if (e.ClickedItem.Text == "生成笔画码")
            {
                this.GenarateCharOrderString();
            }
        }

        /// <summary>
        /// 统一批量生成笔画码
        /// </summary>
        private void GenarateCharOrderString()
        {
            FS.SOC.HISFC.Fee.BizLogic.Undrug unDrugMgr = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
            List<FS.SOC.HISFC.Fee.Models.Undrug> listItem = this.undrugManager.QueryAllItemList();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Base.Item itemInfo in listItem)
            {
                string charOrderString = FS.FrameWork.Function.NConvert.ToCharSortCode(itemInfo.Name);
                itemInfo.WBCode = itemInfo.WBCode + charOrderString;
                if (itemInfo.WBCode.Length > 40)
                {
                    itemInfo.WBCode = itemInfo.WBCode.Substring(0, 40);
                }
                if (unDrugMgr.UpdateCharOrderString(itemInfo) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    CommonController.CreateInstance().MessageBox(itemInfo.Name + "笔画码生成失败，请系统管理员报告错误：" , MessageBoxIcon.Information);
                    return;
                }
            }
            
            FS.FrameWork.Management.PublicTrans.Commit();
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
                this.InitDic();
                this.InitBaseData();
                this.InitDataTable();
                this.InitFarPoint();
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 初始化常数字典
        /// </summary>
        private void InitDic()
        {
            ArrayList minFeeList = consMgr.GetAllList("MINFEE");

            ArrayList itemGradeList = consMgr.GetAllList("ITEMGRADE");

            ArrayList applicabilityList = consMgr.GetAllList("APPLICABILITYAREA");

            ArrayList itemPriceTypeList = consMgr.GetAllList("ITEMPRICETYPE");

            minFeeObjectHelper.ArrayObject = minFeeList;

            itemGradeHelper.ArrayObject = itemGradeList;

            applicabilityHelper.ArrayObject = applicabilityList;

            itemPriceTypeHelper.ArrayObject = itemPriceTypeList;
        }

        private void InitFarPoint()
        {
            try
            {
                if (!this.neuSpread.ReadSchema(this.settingFile))
                {
                    this.sheetView1.ColumnHeader.Rows[0].Height = 34f;
                }
            }
            catch
            {
                //出错了就删掉配置文件
                if (System.IO.File.Exists(settingFile))
                {
                    System.IO.File.Delete(settingFile);
                }

                this.sheetView1.ColumnHeader.Rows[0].Height = 34f;
            }

            this.sheetView1.Columns[this.neuSpread.GetColumnIndex(0, "项目编号")].AllowAutoSort = true;
            this.sheetView1.Columns[this.neuSpread.GetColumnIndex(0, "项目名称")].AllowAutoSort = true;
            this.sheetView1.Columns[this.neuSpread.GetColumnIndex(0, "自定义码")].AllowAutoSort = true;
            this.sheetView1.Columns[this.neuSpread.GetColumnIndex(0, "国家编码")].AllowAutoSort = true;
        }

        private void InitBaseData()
        {
            //最小费用
            this.ncmbFeeType.AddItems(CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.MINFEE));

            //系统类别
            this.ncmbSystemType.AddItems(FS.HISFC.Models.Base.SysClassEnumService.List());
        }

        private void InitDataTable()
        {
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型{BCEC02C6-85A6-437b-A422-273DFC6AF86E}{6F68AB52-332C-4efa-A6DD-F6BDB37B1283}
            this.dtItems.Columns.AddRange(new DataColumn[] { new DataColumn("项目编号", typeof(string)),
                                                   new DataColumn("项目名称", typeof(string)),
                                                   new DataColumn("规格", typeof(string)),
                                                   new DataColumn("单位", typeof(string)),
                                                   new DataColumn("普通价", typeof(string)),
                                                   new DataColumn("商保价", typeof(string)),
                                                   new DataColumn("医保价", typeof(string)),
                                                    new DataColumn("执行科室", typeof(string)),
                                                   new DataColumn("系统类别", typeof(string)),
                                                   new DataColumn("管理级别", typeof(string)),
                                                   new DataColumn("费用代码", typeof(string)),
                                                   new DataColumn("自定义码", typeof(string)),
                                                   new DataColumn("拼音码", typeof(string)),
                                                   new DataColumn("五笔码", typeof(string)),
                                                   new DataColumn("国家编码", typeof(string)),
                                                   new DataColumn("国际编码", typeof(string)),
                                                   
                                                   
                                                   new DataColumn("急诊加成比例", typeof(string)),
                                                   new DataColumn("计划生育标记", typeof(string)),
                                                   new DataColumn("特定诊疗项目", typeof(string)),
                                                   new DataColumn("甲乙类标志", typeof(string)),
                                                   new DataColumn("确认标志", typeof(string)),
                                                   new DataColumn("有效性标识", typeof(string)),
                                                  
                                                  
                                                   new DataColumn("设备编号", typeof(string)),
                                                   new DataColumn("标本", typeof(string)),
                                                   new DataColumn("手术编码", typeof(string)),
                                                   new DataColumn("手术分类", typeof(string)),
                                                   new DataColumn("手术规模", typeof(string)),
                                                   new DataColumn("是否对照", typeof(string)),
                                                   new DataColumn("备注", typeof(string)),
                                                   
                                                   new DataColumn("省限制", typeof(string)),
                                                   new DataColumn("市限制", typeof(string)),
                                                   new DataColumn("自费项目", typeof(string)),
                                                   new DataColumn("特殊标识1", typeof(string)),
                                                   new DataColumn("特殊标识2", typeof(string)),
                                                   new DataColumn("单价1", typeof(string)),
                                                   new DataColumn("单价2", typeof(string)),
                                                   new DataColumn("疾病分类", typeof(string)),
                                                   new DataColumn("专科名称", typeof(string)),
                                                   new DataColumn("病史及检查", typeof(string)),
                                                   new DataColumn("检查要求", typeof(string)),
                                                   new DataColumn("注意事项", typeof(string)),
                                                   new DataColumn("知情同意书", typeof(string)),
                                                   new DataColumn("检查申请单名称", typeof(string)),
                                                   new DataColumn("是否需要预约", typeof(string)),
                                                   new DataColumn("项目范围", typeof(string)),
                                                   new DataColumn("项目约束", typeof(string)),
                                                   new DataColumn("是否组套", typeof(string)),
                                                   new DataColumn("适用范围",typeof(string)),
                                                   new DataColumn("物价费用类别",typeof(string)),
                                                   new DataColumn("申请人",typeof(string)),
                                                   new DataColumn("停用时间",typeof(string)),
                                                   new DataColumn("项目别名",typeof(string)),
                                                   new DataColumn("别名自定义码",typeof(string)),
                                                   new DataColumn("注册码",typeof(string))
            });


            foreach (DataColumn dc in this.dtItems.Columns)
            {
                dc.ReadOnly = true;
            }



            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["项目编号"];

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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据...");
            Application.DoEvents();
           
            List<FS.SOC.HISFC.Fee.Models.Undrug> listItem = this.undrugManager.QueryAllItemList();
            if (listItem == null)
            {
                CommonController.CreateInstance().MessageBox("查询物价基本信息出错，请向系统管理员报告错误信息：" + undrugManager.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            Application.DoEvents();
            int progress = 1;

            foreach (FS.SOC.HISFC.Fee.Models.Undrug item in listItem)
            {
                //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, listItem.Count);
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
            this.SetFormat();
            this.InitFarPoint();
        }

        //停用的项目变成红色
        public void SetFormat()
        {
           
            for (int i = 0; i < this.sheetView1.RowCount; i++)
            {
                if (this.sheetView1.Cells[i, 16].Value.ToString() == "停用")
                {
                    this.sheetView1.Rows[i].ForeColor = Color.Red;
                }
                else
                {
                    this.sheetView1.Rows[i].ForeColor = Color.Black;
                }
            }
        }

        private bool Filter( FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            if (this.CheckComoboxSelectedValueValid(this.ncmbSystemType) && this.ncmbSystemType.Tag.Equals( item.SysClass.ID)==false)
            {
                return true;
            }

            if (this.CheckComoboxSelectedValueValid(this.ncmbFeeType) && this.ncmbFeeType.Tag.ToString() != item.MinFee.ID)
            {
                return true;
            }


            if (this.ncbStop.Checked && !this.ncbValid.Checked && item.ValidState == ((int)FS.HISFC.Models.Base.EnumValidState.Valid).ToString())
            {
                return true;
            }

            if (this.ncbValid.Checked && !this.ncbStop.Checked && item.ValidState != ((int)FS.HISFC.Models.Base.EnumValidState.Valid).ToString())
            {
                return true;
            }

            if (this.ncbUnitFlag.Checked && !FS.FrameWork.Function.NConvert.ToBoolean(item.UnitFlag))
            {
                return true;
            }
            if (this.ncbitemFlag.Checked && FS.FrameWork.Function.NConvert.ToBoolean(item.UnitFlag))
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

        private int AddItemObjectToDataTable(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            if (item == null)
            {

                CommonController.CreateInstance().MessageBox("向DataTable中添加物价基本信息失败：物价基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtItems == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加物价基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            if (this.hsItem.Contains(item.ID))
            {
                CommonController.CreateInstance().MessageBox("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
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

        private int SetDataRowValue(FS.SOC.HISFC.Fee.Models.Undrug item, DataRow row)
        {
            row["项目编号"] = item.ID;
            row["项目名称"] = item.Name;
            row["系统类别"] = item.SysClass.Name;
            row["管理级别"] = item.ManageClass.Name;
            string[] depts = item.ExecDept.Split('|');
            foreach (string dept in depts)
            {
                row["执行科室"] += SOC.HISFC.BizProcess.Cache.Common.GetDeptName(dept) + ",";
                //row["执行科室"] += CommonController.CreateInstance().GetDepartmentName(dept)+",";
            }
            row["费用代码"] = minFeeObjectHelper.GetName(item.MinFee.ID);
            //row["费用代码"] = CommonController.CreateInstance().GetConstantName(FS.HISFC.Models.Base.EnumConstant.MINFEE, item.MinFee.ID);//
            row["自定义码"] = item.UserCode;
            row["拼音码"] = item.SpellCode;
            row["五笔码"] = item.WBCode;
            row["国家编码"] = item.GBCode;
            row["国际编码"] = item.NationCode;
            row["普通价"] = item.Price; //{BCEC02C6-85A6-437b-A422-273DFC6AF86E}
            row["单位"] = item.PriceUnit;
            row["急诊加成比例"] = item.FTRate.EMCRate.ToString();
            row["计划生育标记"] = item.IsFamilyPlanning ? "是" : "否";
            row["特定诊疗项目"] = item.User01;
            //row["甲乙类标志"] = CommonController.CreateInstance().GetConstantName("ITEMGRADE", item.Grade.ID);
            row["甲乙类标志"] = itemGradeHelper.GetName(item.Grade.ID);
            switch (item.NeedConfirm)
            {
                case FS.HISFC.Models.Fee.Item.EnumNeedConfirm.None:
                    row["确认标志"] = "不需要";
                    break;
                case FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Inpatient:
                    row["确认标志"] = "住院需要";
                    break;
                case FS.HISFC.Models.Fee.Item.EnumNeedConfirm.Outpatient:
                    row["确认标志"] = "门诊需要";
                    break;
                case FS.HISFC.Models.Fee.Item.EnumNeedConfirm.All:
                    row["确认标志"] = "全部需要";
                    break;
                default:
                    break;
            }
            switch (item.ValidState)
            {
                case "0":
                    row["有效性标识"] = "停用";
                    break;
                case "1":
                    row["有效性标识"] = "在用";
                    break;
                case "2":
                    row["有效性标识"] = "废弃";
                    break;
                default:
                    row["有效性标识"] = "";
                    break;
            }
            row["规格"] = item.Specs;
            row["设备编号"] = item.MachineNO;
            row["标本"] = item.CheckBody;
            row["手术编码"] = item.OperationInfo.ID;
            row["手术分类"] = item.OperationType.ID;
            row["手术规模"] = item.OperationScale.ID;
            row["是否对照"] = item.IsCompareToMaterial ? "有" : "没有";
            row["备注"] = item.Memo;
            row["商保价"] = item.ChildPrice.ToString(); //{BCEC02C6-85A6-437b-A422-273DFC6AF86E}
            row["医保价"] = item.SpecialPrice.ToString();
            switch (item.SpecialFlag)
            {
                case "0":
                    row["省限制"] = "不限制";
                    break;
                case "1":
                    row["省限制"] = "限制";
                    break;
                default:
                    row["省限制"] = "";
                    break;
            }
            switch (item.SpecialFlag1)
            {
                case "0":
                    row["市限制"] = "不限制";
                    break;
                case "1":
                    row["市限制"] = "限制";
                    break;
                default:
                    row["市限制"] = "";
                    break;
            }
            switch (item.SpecialFlag2)
            {
                case "0":
                    row["自费项目"] = "不是";
                    break;
                case "1":
                    row["自费项目"] = "是";
                    break;
                default:
                    row["自费项目"] = "";
                    break;
            }
            switch (item.SpecialFlag3)
            {
                case "0":
                    row["特殊标识1"] = "不是";
                    break;
                case "1":
                    row["特殊标识1"] = "是";
                    break;
                default:
                    row["特殊标识1"] = "";
                    break;
            }
            switch (item.SpecialFlag4)
            {
                case "0":
                    row["特殊标识2"] = "不是";
                    break;
                case "1":
                    row["特殊标识2"] = "是";
                    break;
                default:
                    row["特殊标识2"] = "";
                    break;
            }

            row["单价1"] = item.User02;
            row["单价2"] = item.User03;
            row["疾病分类"] = item.DiseaseType.ID;
            row["专科名称"] = item.SpecialDept.ID;
            row["病史及检查"] = item.MedicalRecord;
            row["检查要求"] = item.CheckRequest;
            row["注意事项"] = item.Notice;
            row["知情同意书"] = item.IsConsent ? "需要" : "不需要";
            row["检查申请单名称"] = item.CheckApplyDept;
            row["是否需要预约"] = item.IsNeedBespeak ? "需要" : "不需要";
            row["项目范围"] = item.ItemArea;
            row["项目约束"] = item.ItemException;
            //row["适用范围"] = CommonController.CreateInstance().GetConstantName("APPLICABILITYAREA", item.ApplicabilityArea);
            row["适用范围"] = applicabilityHelper.GetName(item.ApplicabilityArea);
            row["是否组套"] = item.UnitFlag == "1" ? "是" : "否";
            //row["物价费用类别"] = CommonController.CreateInstance().GetConstantName("ITEMPRICETYPE", item.ItemPriceType);
            ;//item.ItemPriceType;
            row["物价费用类别"] = itemPriceTypeHelper.GetName(item.ItemPriceType);
            row["申请人"] = item.Oper.ID;
            row["停用时间"] = item.Oper.OperTime;
            row["项目别名"] = item.NameCollection.OtherName;
            row["别名自定义码"] = item.NameCollection.OtherSpell.UserCode;
            row["注册码"] = item.RegisterCode ;
            return 1;
        }

        #endregion

        #region 修改、增加、删除
        private void Modify()
        {
            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }
            this.InitMaintenanceForm();

            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(drugNO))
            {
                bool isUse = false;
                isUse = this.GetItemUserd(drugNO);
                this.ucItem.SetItem(hsItem[drugNO] as FS.SOC.HISFC.Fee.Models.Undrug, isUse);
                this.frmItem.ShowDialog();
            }
        }

        /// <summary>
        /// 获取项目是否使用过
        /// </summary>
        /// <param name="drugNO"></param>
        /// <returns></returns>
        private bool GetItemUserd(string itemCode)
        {
            string strSql = @"SELECT SUM(QTY)
  FROM (select COUNT(*) QTY
          from fin_opb_feedetail e
         where e.item_code = '{0}'
           AND ROWNUM = 1
        UNION ALL
        SELECT COUNT(*) QTY
          FROM FIN_IPB_ITEMLIST B
         WHERE B.ITEM_CODE = '{0}'
           AND ROWNUM = 1)";
            strSql = string.Format(strSql, itemCode);
            int returnValue = FS.FrameWork.Function.NConvert.ToInt32(undrugManager.ExecSqlReturnOne(strSql, "0"));
            return (returnValue > 0);
        }

        private void Add()
        {
            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }
            this.InitMaintenanceForm();

            this.ucItem.Clear();

            this.ucItem.IsUserCodeAutoGenerate = isUserCodeAutoGenerate;

            if (isUserCodeAutoGenerate)
            {
                this.ucItem.SetUserCode();
            }
            this.frmItem.ShowDialog();
        }

        private void Copy()
        {
            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }
            this.InitMaintenanceForm();

            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(drugNO))
            {
                FS.SOC.HISFC.Fee.Models.Undrug item = (hsItem[drugNO] as FS.SOC.HISFC.Fee.Models.Undrug).Clone();
                item.ID = "";
                this.ucItem.SetItem(item, false);
                this.frmItem.ShowDialog();
            }
        }

        //作废
        private void Invalid()
        {

            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("作废后的信息不可以恢复，确认删除吗？", "提示>>", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");

            if (hsItem.Contains(drugNO))
            {
                FS.SOC.HISFC.Fee.Models.Undrug item = hsItem[drugNO] as FS.SOC.HISFC.Fee.Models.Undrug;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (undrugManager.Invalid(item)== -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    CommonController.CreateInstance().MessageBox("作废失败，请向系统管理员联系并报告错误：" + itemManager.Err, MessageBoxIcon.Error);
                    return;
                }

                string errInfo = "";
                ArrayList alDrug = new ArrayList();
                alDrug.Add(item);
                int param = Function.SendBizMessage(alDrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug, ref errInfo);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    CommonController.CreateInstance().MessageBox("删除失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
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


        private void Delete()
        {
            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("删除后的信息不可以恢复，确认删除吗？", "提示>>", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");

            if (hsItem.Contains(drugNO))
            {
                 FS.SOC.HISFC.Fee.Models.Undrug item = hsItem[drugNO] as  FS.SOC.HISFC.Fee.Models.Undrug;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                if (itemManager.DeleteUndrugItemByCode(item.ID) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    CommonController.CreateInstance().MessageBox("删除失败，请向系统管理员联系并报告错误：" + itemManager.Err, MessageBoxIcon.Error);
                    return;
                }

                string errInfo = "";
                ArrayList alDrug = new ArrayList();
                alDrug.Add(item);
                int param = Function.SendBizMessage(alDrug, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType.Delete, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType.Undrug, ref errInfo); 
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    CommonController.CreateInstance().MessageBox("删除失败，请向系统管理员联系并报告错误：" + errInfo, MessageBoxIcon.Error);
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

        /// <summary>
        /// 设置药品维护窗口
        /// </summary>
        private void InitMaintenanceForm()
        {
            if (this.ucItem == null)
            {
                this.ucItem = new ucItemNew();
                this.ucItem.Dock = DockStyle.Fill;
                if (IsMaterialCode == true)
                {
                    ucItem.IsMaterialCodeShow = true;
                }
                else
                {
                    ucItem.IsMaterialCodeShow = false;
                }

                this.ucItem.Init();

                this.ucItem.EndSave -= new ucItemNew.SaveItemHandler(ucItem_EndSave);
                this.ucItem.EndSave += new ucItemNew.SaveItemHandler(ucItem_EndSave);
                this.ucItem.GetNextItem -= new ucItemNew.GetNextItemHandler(ucItem_GetNextItem);
                this.ucItem.GetNextItem += new ucItemNew.GetNextItemHandler(ucItem_GetNextItem);
            }
            if (this.frmItem == null)
            {
                this.frmItem = new Form();
                this.frmItem.Width = this.ucItem.Width + 10;
                this.frmItem.Height = this.ucItem.Height + 25;
                this.frmItem.Text = "物价详细信息维护";
                this.frmItem.StartPosition = FormStartPosition.CenterScreen;
                this.frmItem.ShowInTaskbar = false;
                this.frmItem.HelpButton = false;
                this.frmItem.MaximizeBox = false;
                this.frmItem.MinimizeBox = false;
              
                this.frmItem.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.frmItem.Controls.Add(this.ucItem);
                
            }
            
        }

        #endregion

        #region 过滤
        private void Filter()
        {
            string filter = Function.GetFilterStr(this.dtItems.DefaultView, (this.ckAllFilter.Checked ? "%" :this.ckRightFilter.Checked?"%":"") +this.ntxtFilter.Text.Trim() + (this.ckAllFilter.Checked?"%":this.ckLeftFilter.Checked?"%":""));
            filter = "(" + filter + ")";
            //增加系统类别，费用类别，有效性，组套
            if(this.dtItems.Columns.Contains("系统类别"))
            {
                filter += string.Format(" and 系统类别 like '%{0}%'", this.ncmbSystemType.Text.Trim());
            }

            if (this.dtItems.Columns.Contains("费用代码"))
            {
                filter += string.Format(" and 费用代码 like '%{0}%'", this.ncmbFeeType.Text.Trim());
            }

            if (this.dtItems.Columns.Contains("是否组套"))
            {
               if(this.ncbUnitFlag.Checked == true)
                   filter += string.Format(" and 是否组套 like '%{0}%'", this.ncbUnitFlag.Checked ? "是" : "");
               else if(this.ncbitemFlag.Checked == true)
                 filter += string.Format(" and 是否组套 like '%{0}%'", this.ncbUnitFlag.Checked ? "否" : "");
               else
                  filter += string.Format(" and 是否组套 like '%{0}%'", this.ncbUnitFlag.Checked ? "否" : "");
            }

            if (this.dtItems.Columns.Contains("有效性标识"))
            {
                filter += string.Format(" and 有效性标识 in ('{0}','{1}')", this.ncbStop.Checked ? "停用" : "", this.ncbValid.Checked ? "在用" : "");
            }


            this.dtItems.DefaultView.RowFilter = filter;
            this.SetFormat();
            this.InitFarPoint();
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

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在发送消息...");
            Application.DoEvents();
            ArrayList alItem = new ArrayList();
            for (int rowIndex = 0; rowIndex < this.sheetView1.RowCount; rowIndex++)
            {
                string drugNO = this.neuSpread.GetCellText(0, rowIndex, "项目编号");
                if (hsItem.Contains(drugNO))
                {
                    alItem.Add(hsItem[drugNO]);
                }
            }

            if (InterfaceManager.GetISaveAllItem().Saved(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update,alItem) == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                CommonController.CreateInstance().MessageBox("发送消息失败，请向系统管理员联系并报告错误：" + InterfaceManager.GetISaveAllItem().Err, MessageBoxIcon.None);
                return;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            CommonController.CreateInstance().MessageBox("发送成功！", MessageBoxIcon.None);
        }
        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            if (this.Init() == 1)
            {
                this.Query();
            }

            for (int i = 0; i < this.sheetView1.Columns.Count; i++)
            {
                this.sheetView1.Columns[i].ShowSortIndicator = true;
                this.sheetView1.Columns[i].AllowAutoSort = true;
            }

            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.ncbMutiQuery.CheckedChanged += new EventHandler(ncbMutiQuery_CheckedChanged);
            this.neuSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread_CellDoubleClick);
            this.ntxtFilter.TextChanged += new EventHandler(ntxtFilter_TextChanged);
            this.ncbValid.CheckedChanged += new EventHandler(ntxtFilter_TextChanged);
            this.ncbStop.CheckedChanged += new EventHandler(ntxtFilter_TextChanged);
            this.ncbUnitFlag.CheckedChanged += new EventHandler(ntxtFilter_TextChanged);
            this.ncmbFeeType.TextChanged += new EventHandler(ntxtFilter_TextChanged);
            this.ncmbSystemType.TextChanged += new EventHandler(ntxtFilter_TextChanged);
            this.neuSpread.KeyDown += new KeyEventHandler(neuSpread_KeyDown);
            base.OnLoad(e);
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
            if (!Function.JugePrive("0801", "01"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有物价基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                return 0;
            }
            this.neuSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(drugNO))
            {
                ArrayList alItem = new ArrayList();
                alItem.Add(hsItem[drugNO] as  FS.SOC.HISFC.Fee.Models.Undrug);
                Function.PrintBill("0801", "01", alItem);
            }
            return base.OnPrint(sender, neuObject);
        }

        void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        void neuSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader == false)
            {
                this.Modify();
            }
        }

        void ncbMutiQuery_CheckedChanged(object sender, EventArgs e)
        {
            this.ngbQuerySet.Visible = this.ncbMutiQuery.Checked;
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void ucItem_EndSave( FS.SOC.HISFC.Fee.Models.Undrug item)
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

        void ucItem_GetNextItem(int span)
        {
            this.sheetView1.ActiveRowIndex += span;
            this.neuSpread.ShowRow(0, this.sheetView1.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(drugNO))
            {
                this.ucItem.SetItem(hsItem[drugNO] as  FS.SOC.HISFC.Fee.Models.Undrug, false);
            }
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            return 1;
        }

        #endregion
    }
}

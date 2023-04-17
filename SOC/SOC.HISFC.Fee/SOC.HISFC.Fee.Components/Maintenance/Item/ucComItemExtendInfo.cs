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
    /// [功能描述: 基础项目属性扩展管理]<br></br>
    /// [创 建 者: xiangf]<br></br>
    /// [创建时间: 2012-05]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucComItemExtendInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucComItemExtendInfo()
        {
            InitializeComponent();
        }

        #region 变量属性

        /// <summary>
        /// FarPoint配置文件
        /// </summary>
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPComItemExtendInfoSetting.xml";

        /// <summary>
        /// FarPoint配置文件
        /// </summary>
        [Description("FarPoint配置文件"), Category("设置"), Browsable(true)]
        public string SettingFile
        {
            get { return settingFile; }
        }

        /// <summary>
        /// 基础项目信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = null;

        /// <summary>
        /// 基础项目信息缓存
        /// </summary>
        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        /// <summary>
        /// 基础项目信息维护业务层
        /// </summary>
        private FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo comItemExtendManager = new FS.SOC.HISFC.Fee.BizLogic.ComItemExtendInfo();

        /// <summary>
        /// 非药品信息缓存
        /// </summary>
        private ArrayList alItems = null;

        /// <summary>
        /// 药品信息
        /// </summary>
        private ArrayList alPharmacys = null;

        /// <summary>
        /// 选择数据窗口
        /// </summary>
        private FS.FrameWork.WinForms.Forms.frmEasyChoose frmChoose = null;

        /// <summary>
        /// 医保接口业务层(本地)
        /// </summary>
        FS.HISFC.BizLogic.Fee.Interface interfaceManager = new FS.HISFC.BizLogic.Fee.Interface();
        /// <summary>
        /// 医保代码，该合同单位的对照信息中的医保目录等级用于公费时项目的费用项目显示甲乙类
        /// </summary>
        protected string ybPactCode = string.Empty;

        #endregion
        #region 属性
        [Category("控件设置"), Description("公费费用项目目录等级参照该医保代码的对照信息的等级")]
        public string YBPactCode
        {
            get
            {
                return this.ybPactCode;
            }
            set
            {
                this.ybPactCode = value;
            }
        }
        protected string typeCode = "ALL";  //默认为全部
        [Category("药品&非药品"), Description("设置药品或者非药品或者全部：1为药品，2为非药品，ALL为全部(一定要大写)")]
        public string TypeCode
        {
            get { return typeCode; }
            set { typeCode = value; }
        }
        #endregion

        #region 工具栏

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //toolBarService.AddToolButton("保存1", "保存1", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("删除", "删除", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("刷新项目", "刷新项目列表", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B帮助, true, false, null);
            return toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //if (e.ClickedItem.Text == "保存")
            //{
            //    // this.AddOrModifyItem();
            //}
            //else 
            if (e.ClickedItem.Text == "删除")
            {
                this.Delete();
            }
            else if (e.ClickedItem.Text == "刷新项目")
            {
                this.Query();
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.AddOrModifyItem();
            return base.OnSave(sender, neuObject);
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
                this.InitDataTable();
                this.InitFarPoint();
                switch (TypeCode)
                {
                    case "1":
                        {
                            rbtnAll.Checked = false;
                            rbtnAll.Enabled = false;
                            rbtnItem.Checked = false;
                            rbtnItem.Enabled = false;
                            rbtnPharmacy.Checked = true;
                            rbtnPharmacy.Enabled = true;
                        }
                        break;
                    case "2":
                        {
                            rbtnAll.Checked = false;
                            rbtnAll.Enabled = false;
                            rbtnItem.Checked = true;
                            rbtnItem.Enabled = true;
                            rbtnPharmacy.Checked = false;
                            rbtnPharmacy.Enabled = false;
                        }
                        break;
                    case "3":
                        {
                            rbtnAll.Checked = true;
                            rbtnAll.Enabled = true;
                            rbtnItem.Checked = false;
                            rbtnItem.Enabled = true;
                            rbtnPharmacy.Checked = false;
                            rbtnPharmacy.Enabled = true;
                        }
                        break;
                    default:
                        {
                            rbtnAll.Checked = true;
                            rbtnAll.Enabled = true;
                            rbtnItem.Checked = false;
                            rbtnItem.Enabled = true;
                            rbtnPharmacy.Checked = false;
                            rbtnPharmacy.Enabled = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }

        private void InitFarPoint()
        {
            if (this.neuSpread.ReadSchema(this.settingFile))
            {
                this.sheetView1.ColumnHeader.Rows[0].Height = 30f;
            }
        }

        private void InitDataTable()
        {
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型
            this.dtItems.Columns.AddRange(new DataColumn[] { new DataColumn("维护", typeof(bool)),
                                                             new DataColumn("项目编号", typeof(string)),
                                                             new DataColumn("项目名称", typeof(string)),
                                                             new DataColumn("拼音码",typeof(string)),
                                                             new DataColumn("规格",typeof(string)),
                                                             new DataColumn("项目类别", typeof(string)),
                                                             new DataColumn("项目等级", typeof(string)),
                                                             new DataColumn("省限制", typeof(bool)),
                                                             new DataColumn("市限制", typeof(bool)),
                                                             new DataColumn("区限制", typeof(bool)),
                                                             new DataColumn("特约单位限制", typeof(bool)),
                                                             new DataColumn("自费项目", typeof(bool)),
                                                             new DataColumn("同步", typeof(bool)),
                                                             new DataColumn("肿瘤用药", typeof(bool)),
                                                             new DataColumn("操作员", typeof(string)),
                                                             new DataColumn("操作日期", typeof(string))
                                                            }
                                           );

            this.SetReadOnly(false);

            DataColumn[] keys = new DataColumn[1];
            keys[0] = this.dtItems.Columns["项目编号"];
            this.dtItems.PrimaryKey = keys;

            this.dtItems.DefaultView.AllowEdit = true;
            this.dtItems.DefaultView.AllowNew = true;
            this.dtItems.DefaultView.AllowDelete = true;

            this.sheetView1.DataAutoSizeColumns = false;
            this.sheetView1.DataSource = this.dtItems.DefaultView;

        }

        #endregion

        #region 查询，数据显示到FarPoint

        private void Query()
        {
            this.hsItem.Clear();
            this.dtItems.Clear();
            this.sheetView1.DataSource = null;
            this.ntxtFilter.Text = string.Empty;

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询数据...");
            Application.DoEvents();

            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listItem = this.comItemExtendManager.QueryItemListByItemCode("ALL",TypeCode);
            if (listItem == null)
            {
                CommonController.CreateInstance().MessageBox("查询基础项目扩展出错信息，请向系统管理员报告错误信息：" + comItemExtendManager.Err, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            Application.DoEvents();
            int progress = 1;
            foreach (FS.SOC.HISFC.Fee.Models.ComItemExtend item in listItem)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, listItem.Count);
                if (this.AddItemObjectToDtItem(item, true) != 1)
                {
                    continue;
                }
                this.sheetView1.Rows.Count += 1;
                this.sheetView1.Rows[this.sheetView1.Rows.Count - 1].Tag = "Update";
            }
            this.SetReadOnly(true);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.dtItems.AcceptChanges();
            this.sheetView1.DataSource = this.dtItems.DefaultView;
            this.neuSpread.ReadSchema(this.settingFile);
        }

        #endregion

        #region 增加、修改、删除

        private void AddOrModifyItem()
        {
            #region 增加、修改

            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listAdd = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>();
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listModify = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>();
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listDelete = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>();

            for (int i = 0; i < this.sheetView1.RowCount; i++)
            {

                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread.GetCellText(0, i, "维护")))
                {
                    string itemCode = this.neuSpread.GetCellText(0, i, "项目编号");
                    if (this.hsItem.Contains(itemCode))
                    {
                        DataRow row = this.dtItems.Rows.Find(new string[] { itemCode });
                        if (row != null)
                        {
                            FS.SOC.HISFC.Fee.Models.ComItemExtend item = (this.hsItem[itemCode] as FS.SOC.HISFC.Fee.Models.ComItemExtend).Clone();
                            item.ProvinceFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "省限制")].Value).ToString();
                            item.CityFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "市限制")].Value).ToString();
                            item.AreaFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "区限制")].Value).ToString();
                            item.SpePactFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "特约单位限制")].Value).ToString();
                            item.ZFFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "自费项目")].Value).ToString();
                            item.SynFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "同步")].Value).ToString();
                            item.MlgFlag = FS.FrameWork.Function.NConvert.ToInt32(this.sheetView1.Cells[i, this.neuSpread.GetColumnIndex(0, "肿瘤用药")].Value).ToString();
                            this.SetReadOnly(false);
                            if (this.sheetView1.Rows[i].Tag != null && this.sheetView1.Rows[i].Tag.ToString() == "Update")
                            {
                                //this.ModifyDataRowValue(item, row);
                                listModify.Add(item);
                            }
                            else
                            {
                                //this.SetDataRowValue(item, row);
                                listAdd.Add(item);
                            }
                            //this.hsItem[itemCode] = item;
                            //row.AcceptChanges();
                            this.SetReadOnly(true);
                        }

                    }
                }
                else
                {
                    this.SetReadOnly(true);
                }
            }

            #endregion

            if (listAdd.Count > 0 || listModify.Count > 0)
            {
                this.Save(listAdd, listModify, listDelete);
            }

            //this.Query();
        }

        private void Delete()
        {
            //提交
            this.neuSpread.StopCellEditing();

            if (MessageBox.Show("删除后的信息不可以恢复，确认删除吗？", "提示>>", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listAdd = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>();
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listModify = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>();
            List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listDelete = new List<FS.SOC.HISFC.Fee.Models.ComItemExtend>();

            for (int i = 0; i < this.sheetView1.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread.GetCellText(0, i, "维护")))
                {
                    string itemCode = this.neuSpread.GetCellText(0, i, "项目编号");
                    if (this.hsItem.Contains(itemCode))
                    {
                        FS.SOC.HISFC.Fee.Models.ComItemExtend item = (this.hsItem[itemCode] as FS.SOC.HISFC.Fee.Models.ComItemExtend).Clone();
                        listDelete.Add(item);
                    }
                }
            }
            this.Save(listAdd, listModify, listDelete);

        }

        #endregion

        #region 过滤

        private void Filter()
        {
            string filter = Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.ntxtFilter.Text.Trim() + "%");
            filter = "(" + filter + ")";

            if (this.rbtnPharmacy.Checked)
            {
                filter = Function.ConnectFilterStr(filter, "项目类别 = '药品'", "and");
            }
            else if (this.rbtnItem.Checked)
            {
                filter = Function.ConnectFilterStr(filter, "项目类别 = '非药品'", "and");
            }
            else if (this.rbtnAll.Checked)
            {

                filter = Function.ConnectFilterStr(filter, "项目类别 like '%'", "and");
            }

            this.dtItems.DefaultView.RowFilter = filter; 
            this.neuSpread.ReadSchema(this.settingFile);
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

            this.ntxtFilter.TextChanged += new EventHandler(ntxtFilter_TextChanged);

            this.neuSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);

            this.btnAddItem.Click += new EventHandler(btnAddItem_Click);

            base.OnLoad(e);
        }

        public override int Export(object sender, object neuObject)
        {
            this.neuSpread.ExportExcel(FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            return base.Export(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            //string drugNO = this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "项目编号");
            //if (this.hsItem.Contains(drugNO))
            //{
            //    ArrayList alItem = new ArrayList();
            //    alItem.Add(hsItem[drugNO] as  FS.SOC.HISFC.Fee.Models.Undrug);
            //    Function.PrintBill("0801", "01", alItem);
            //}
            return base.OnPrint(sender, neuObject);
        }

        #endregion

        #region 方法

        private void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            ArrayList al = new ArrayList();
            if (this.rbtnAll.Checked)
            {
                //药品
                if (alPharmacys == null || alPharmacys.Count == 0)
                {
                    alPharmacys = new ArrayList();
                    FS.SOC.HISFC.Fee.BizProcess.Pharmacy pharmacyManager = new FS.SOC.HISFC.Fee.BizProcess.Pharmacy();
                    List<FS.HISFC.Models.Pharmacy.Item> list = pharmacyManager.QueryPharmacyBriefInfo();
                    foreach (FS.HISFC.Models.Pharmacy.Item item in list)
                    {
                        item.Memo = item.Specs;
                        alPharmacys.Add(item);
                    }
                }

                al.AddRange(alPharmacys);

                if (alItems == null || alItems.Count == 0)
                {
                    FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                    alItems = itemManager.QueryBriefList("0");
                    foreach (FS.SOC.HISFC.Fee.Models.Undrug undrug in alItems)
                    {
                        undrug.Memo = undrug.Specs;
                    }
                }
                //项目
                al.AddRange(alItems);


            }
            else if (this.rbtnItem.Checked)
            {
                //if (!Function.JugePrive("0801", "08"))
                //{
                //    CommonController.CreateInstance().MessageBox("因为您没有非药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                //    return;
                //}
                //只选择项目
                if (alItems == null || alItems.Count == 0)
                {
                    FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                    alItems = itemManager.QueryBriefList("0");
                    foreach (FS.SOC.HISFC.Fee.Models.Undrug undrug in alItems)
                    {
                        undrug.Memo = undrug.Specs;
                    }
                }
                al = alItems;
            }
            else if (this.rbtnPharmacy.Checked)
            {
                //if (!Function.JugePrive("0301", "03"))
                //{
                //    CommonController.CreateInstance().MessageBox("因为您没有药品基本信息维护权限，该操作已取消！", MessageBoxIcon.Information);
                //    return;
                //}
                //只选择药品
                if (alPharmacys == null || alPharmacys.Count == 0)
                {
                    alPharmacys = new ArrayList();
                    FS.SOC.HISFC.Fee.BizProcess.Pharmacy pharmacyManager = new FS.SOC.HISFC.Fee.BizProcess.Pharmacy();
                    List<FS.HISFC.Models.Pharmacy.Item> list = pharmacyManager.QueryPharmacyBriefInfo();
                    foreach (FS.HISFC.Models.Pharmacy.Item item in list)
                    {
                        item.Memo = item.Specs;
                        alPharmacys.Add(item);
                    }
                }

                al = alPharmacys;
            }


            frmChoose = new FS.FrameWork.WinForms.Forms.frmEasyChoose(al);
            frmChoose.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(frmChoose_SelectedItem);
            frmChoose.ShowDialog(this);
        }

        private void frmChoose_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (sender == null)
            {
                return;
            }

            FS.SOC.HISFC.Fee.Models.ComItemExtend comItemExtendInfo = new FS.SOC.HISFC.Fee.Models.ComItemExtend();

            if (sender is FS.SOC.HISFC.Fee.Models.Undrug)
            {
                comItemExtendInfo.TypeCode = "2";
                comItemExtendInfo.ItemCode = ((FS.SOC.HISFC.Fee.Models.Undrug)sender).ID;
                comItemExtendInfo.ItemName = ((FS.SOC.HISFC.Fee.Models.Undrug)sender).Name;
            }
            else if (sender is FS.HISFC.Models.Pharmacy.Item)
            {
                comItemExtendInfo.TypeCode = "1";
                comItemExtendInfo.ItemCode = ((FS.HISFC.Models.Pharmacy.Item)sender).ID;
                comItemExtendInfo.ItemName = ((FS.HISFC.Models.Pharmacy.Item)sender).Name;
            }
            //医保信息
            try
            {
                FS.HISFC.Models.SIInterface.Compare objCompare = new FS.HISFC.Models.SIInterface.Compare();

                this.interfaceManager.GetCompareSingleItem(this.YBPactCode, comItemExtendInfo.ItemCode, ref objCompare);
                comItemExtendInfo.ItemGrade = objCompare.CenterItem.ItemGrade.ToString();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
            //设置默认值
            comItemExtendInfo.ProvinceFlag = "0";
            comItemExtendInfo.CityFlag = "0";
            comItemExtendInfo.AreaFlag = "0";
            comItemExtendInfo.SpePactFlag = "0";
            comItemExtendInfo.ZFFlag = "0";
            comItemExtendInfo.SpePactFlag = "0";
            comItemExtendInfo.MlgFlag = "0";
            comItemExtendInfo.OperCode = this.comItemExtendManager.Operator.ID;
            comItemExtendInfo.OperDate = this.comItemExtendManager.GetDateTimeFromSysDateTime().ToString();

            int i = this.AddItemObjectToDtItem(comItemExtendInfo, false);
            if (i > 0)
            {
                this.neuSpread.ShowRow(0, this.sheetView1.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
                this.sheetView1.ActiveRowIndex = this.sheetView1.RowCount - 1;
            }
        }

        private int AddItemObjectToDtItem(FS.SOC.HISFC.Fee.Models.ComItemExtend item, bool isModify)
        {
            if (item == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加基础项目扩展信息失败：合同单位基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtItems == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加基础项目扩展信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.hsItem.Contains(item.ItemCode))
            {
                if (isModify)
                {
                    this.hsItem[item.ItemCode] = item;
                }
                else
                {
                    CommonController.CreateInstance().MessageBox("编码：" + item.ItemCode + " 名称：" + item.ItemName + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                this.hsItem.Add(item.ItemCode, item);
            }

            DataRow row = this.dtItems.NewRow();
            if (!isModify)
            {
                row["维护"] = FS.FrameWork.Function.NConvert.ToBoolean("1");
            }
            this.SetReadOnly(false);
            this.SetDataRowValue(item, row);
            this.dtItems.Rows.Add(row);
            return 1;
        }

        private int SetDataRowValue(FS.SOC.HISFC.Fee.Models.ComItemExtend item, DataRow row)
        {
            row["项目编号"] = item.ItemCode;
            row["项目名称"] = item.ItemName;
            row["拼音码"] = item.Spell_code;
            row["规格"] = item.Specs;

            switch (item.TypeCode)
            {
                case "1":
                    row["项目类别"] = "药品";
                    break;
                case "2":
                    row["项目类别"] = "非药品";
                    break;
                default:
                    row["项目类别"] = "";
                    break;
            }
            switch (item.ItemGrade)
            {
                case "1":
                    row["项目等级"] = "甲类";
                    break;
                case "2":
                    row["项目等级"] = "乙类";
                    break;
                case "3":
                    row["项目等级"] = "丙类";
                    break;
                default:
                    row["项目等级"] = "";
                    break;
            }

            row["省限制"] = FS.FrameWork.Function.NConvert.ToBoolean(item.ProvinceFlag);
            row["市限制"] = FS.FrameWork.Function.NConvert.ToBoolean(item.CityFlag);
            row["区限制"] = FS.FrameWork.Function.NConvert.ToBoolean(item.AreaFlag);
            row["特约单位限制"] = FS.FrameWork.Function.NConvert.ToBoolean(item.SpePactFlag);
            row["自费项目"] = FS.FrameWork.Function.NConvert.ToBoolean(item.ZFFlag);
            row["同步"] = FS.FrameWork.Function.NConvert.ToBoolean(item.SynFlag);
            row["肿瘤用药"] = FS.FrameWork.Function.NConvert.ToBoolean(item.MlgFlag);
            row["操作员"] = item.OperCode;
            row["操作日期"] = item.OperDate;

            return 1;
        }

        private void SetReadOnly(bool isReadOnly)
        {
            foreach (DataColumn dc in this.dtItems.Columns)
            {
                if (dc.ColumnName.Equals("项目编号")
                    || dc.ColumnName.Equals("项目名称")
                    || dc.ColumnName.Equals("拼音码")
                    || dc.ColumnName.Equals("规格")
                    || dc.ColumnName.Equals("项目类别")
                    || dc.ColumnName.Equals("操作员")
                    || dc.ColumnName.Equals("操作日期")
                    )
                {
                    dc.ReadOnly = true;
                }
                else if (dc.ColumnName.Equals("维护"))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = isReadOnly;
                }
            }
        }

        private int Save(List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listAdd, List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listModify,
        List<FS.SOC.HISFC.Fee.Models.ComItemExtend> listDelete)
        {
            this.neuSpread.StopCellEditing();
            foreach (DataRow row in this.dtItems.Rows)
            {
                row.EndEdit();
            }

            if (listAdd.Count > 0 || listModify.Count > 0 || listDelete.Count > 0)
            {
                string errorInfo = "";
                if (new FS.SOC.HISFC.Fee.BizProcess.ComItemExtendInfo().Save(listAdd, listModify, listDelete, ref errorInfo) < 0)
                {
                    //if (errorInfo.Contains("违反唯一约束条件"))
                    //{
                    //    return -1;
                    //}
                    CommonController.CreateInstance().MessageBox(errorInfo, MessageBoxIcon.Error);
                    return -1;
                }
                for (int i = 0; i < listDelete.Count; i++)
                {
                    FS.SOC.HISFC.Fee.Models.ComItemExtend item = listDelete[i] as FS.SOC.HISFC.Fee.Models.ComItemExtend;
                    if (this.hsItem.Contains(item.ItemCode))
                    {
                        DataRow row = this.dtItems.Rows.Find(new string[] { item.ItemCode });
                        if (row != null)
                        {
                            dtItems.Rows.Remove(row);
                        }
                        hsItem.Remove(item.ItemCode);
                    }
                }
            }

            this.dtItems.AcceptChanges();

            CommonController.CreateInstance().MessageBox("保存成功！", MessageBoxIcon.Asterisk);

            return 1;
        }

        private void neuSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread.GetCellText(0, this.sheetView1.ActiveRowIndex, "维护")))
                {
                    this.SetReadOnly(false);
                }
                else
                {
                    this.SetReadOnly(true);
                }
            }
        }

        #endregion

        private void rbtnPharmacy_CheckedChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void rbtnItem_CheckedChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            this.Filter();
        }
    }
}

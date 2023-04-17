using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient.Fee
{
    public partial class ucChargeDrug : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucChargeDrug()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 药品信息数组
        /// </summary>
        ArrayList alData = new ArrayList();

        /// <summary>
        /// 录入药品对象数组
        /// </summary>
        ArrayList alItemData = new ArrayList();

        /// <summary>
        /// 保存药品信息数组
        /// </summary>
        ArrayList alItemDataSave = new ArrayList();

        /// <summary>
        /// 药品信息
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item drugItem = new FS.HISFC.Models.Pharmacy.Item();
        private FS.FrameWork.Models.NeuObject newDrugItem = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 药品业务层
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 数据表
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// 是否大包装
        /// </summary>
        private bool isPack = false;

        /// <summary>
        /// 临时价格
        /// </summary>
        private decimal price = 0;

        /// <summary>
        /// 显示格式xml文件
        /// </summary>
        private string displayXmlfilePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "DrugAideUserFp.xml";

        /// <summary>
        /// 初始化 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucChargeDrug_Load(object sender, System.EventArgs e)
        {
            this.Init();
            return;
        }

        /// <summary>
        /// 数据初始化 
        /// </summary>
        protected void Init()
        {
            #region 药品列表检索加载

            List<FS.HISFC.Models.Pharmacy.Item> drugList = itemManager.QueryItemAvailableList();
            //System.Collections.ArrayList alDrugList = new System.Collections.ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.Item info in drugList)
            {
                info.Name = info.Name + "[" + info.Specs + "]";
                info.Memo = info.NameCollection.RegularName;

                this.alData.Add(info);
            }
            //List<FS.HISFC.Object.Pharmacy.Item> newDrugList = itemManager.QueryItemList(false);
            //if (newDrugList == null)
            //{
            //    MessageBox.Show(FS.NFC.Management.Language.Msg("加载药品信息发生错误" + itemManager.Err));
            //    return;
            //}
            //this.alData = new ArrayList(newDrugList.ToArray());

            #endregion

            //if (this.InitDisplay() < 0)
            //{
            //    return;
            //}
            price = 0;
            this.neuLabel2.Text = "";
        }

        /// <summary>
        /// 初始化数据显示格式
        /// </summary>
        private int InitDisplay()
        {
            return this.InitDisplayFromDataTable();
        }

        /// <summary>
        /// 新建立DataTable
        /// </summary>
        private int InitDisplayFromDataTable()
        {
            this.dt = new DataTable();
            System.Type dtStr = System.Type.GetType("System.String");
            //System.Type dtDec = System.Type.GetType("System.Decimal");
            //System.Type dtDTime = System.Type.GetType("System.DateTime");
            //System.Type dtBool = System.Type.GetType("System.Boolean");
            this.dt.Columns.AddRange(new DataColumn[]{
                    new DataColumn("药品ID",dtStr),
                    new DataColumn("药品名称",dtStr),
                    new DataColumn("通用名",dtStr),
                    //new DataColumn("基本剂量",dtStr),
                    //new DataColumn("剂量单位",dtStr),
                    new DataColumn("零售单价",dtStr),
                    new DataColumn("数量",dtStr),
                    new DataColumn("单位",dtStr),
                    new DataColumn("金额",dtStr),
                    new DataColumn("拼音码",dtStr),
                    new DataColumn("五笔码",dtStr),
                    new DataColumn("自定义码",dtStr),

                });
            this.neuSpread1_Sheet1.DataSource = this.dt.DefaultView;
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.displayXmlfilePath);
            return 1;
        }

        #region 工具栏

        /// <summary>
        /// ToolBarService
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 增加基类窗口的菜单按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="param"></param>
        /// <param name="neuObject"></param>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("删除", "删除记录", FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("清空", "清空记录", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            return toolBarService;
        }

        /// <summary>
        /// 菜单按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "添加")
            {

            }
            else if (e.ClickedItem.Text == "删除")
            {
                this.Delete();
            }
            else if (e.ClickedItem.Text == "清空")
            {
                this.Clear();
            }
            else if (e.ClickedItem.Text == "修改更新")
            {
                //this.Modify();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        /// <summary>
        /// 药品实体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnbDrugItem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(this.alData, ref this.newDrugItem) == 0)
            {
                return;
            }
            else
            {
                //drugItem = this.itemManager.GetItem(newDrugItem.ID);
                drugItem = this.newDrugItem as FS.HISFC.Models.Pharmacy.Item;
                this.SetInternToFp(drugItem);
                this.alItemDataSave.Add(drugItem);

                #region 转化一下
                FS.HISFC.Models.Base.Const tempItem = new FS.HISFC.Models.Base.Const();
                tempItem.ID = drugItem.ID;
                tempItem.UserCode = drugItem.UserCode;
                tempItem.Name = drugItem.Name;
                tempItem.Memo = drugItem.NameCollection.RegularName;
                tempItem.SpellCode = drugItem.SpellCode;
                tempItem.WBCode = drugItem.WBCode;
                tempItem.SortID = FS.FrameWork.Function.NConvert.ToInt32(drugItem.User03);
                #endregion

                this.alItemData.Add(tempItem);
            }
        }

        /// <summary>
        /// 将药品实体装入DataTable
        /// </summary>
        /// <param name="drugItem"></param>
        private void SetInternToFp(FS.HISFC.Models.Pharmacy.Item drugItem)
        {
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            FarPoint.Win.Spread.CellType.ComboBoxCellType unitCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
            unitCell.Editable = true;
            unitCell.Items = new string[] { drugItem.MinUnit, drugItem.PackUnit };

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemID].Value = drugItem.ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemName].Value = drugItem.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRegularName].Value = drugItem.NameCollection.RegularName;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRetailPrice].Value = drugItem.PriceCollection.RetailPrice;// +" 元";
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColSpellCode].Value = drugItem.SpellCode;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColWBCode].Value = drugItem.WBCode;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColUserCode].Value = drugItem.ID;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugQty].Value = 1;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugUnit].CellType = unitCell;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugUnit].Value = unitCell.Items[0];
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColPackQty].Value = drugItem.PackQty;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugCost].Value = drugItem.PriceCollection.RetailPrice *
                NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugQty].Text) /
                NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColPackQty].Text);
            price += NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugCost].Text);
            this.neuLabel2.Text = price.ToString();

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemName].Column.Width = 250;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRetailPrice].Column.Width = 120;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugQty].Column.Width = 50;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugUnit].Column.Width = 50;

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemID].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemName].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRegularName].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRetailPrice].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColSpellCode].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColWBCode].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColUserCode].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugCost].Locked = true;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugQty].Locked = false;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugUnit].Locked = false;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColPackQty].Locked = true;

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDosageForm].Value = drugItem.DosageForm;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDegree].Value = drugItem.Degree;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColBatch].Value = drugItem.Batch;

            //this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Tag = drugItem;
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            if (this.alItemData.Count <= 0)//查询数据
            { return; }
            for (int i = 0; i < this.alItemData.Count; i++)
            {
                FS.HISFC.Models.Base.Const tempDrugItem = alItemData[i] as FS.HISFC.Models.Base.Const;
                if (this.neuSpread1_Sheet1.Rows[i].Tag == null)
                { this.neuSpread1_Sheet1.Rows[i].Tag = tempDrugItem; }
            }

            if (this.neuSpread1_Sheet1.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < this.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                if (this.neuSpread1.ActiveSheet.IsSelected(i, 0))
                {
                    FS.HISFC.Models.Base.Const tempdrugItem = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Base.Const;

                    if (MessageBox.Show(Language.Msg("确定要删除此项数据吗？"), "确定数据删除", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }

                    this.neuSpread1_Sheet1.ClearSelection();
                    this.neuSpread1_Sheet1.Rows.Remove(i, 1);
                    this.alItemData.Remove(tempdrugItem);
                }
            }

            if (!ComputCost())
            {
                return;
            }
            return;
        }

        /// <summary>
        /// 将查询的药品实体装入DataTable
        /// </summary>
        /// <param name="drugItem"></param>
        private void QueryInternToFp(FS.HISFC.Models.Base.Const drugItem)
        {
            this.neuSpread1_Sheet1.AddRows(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemID].Value = drugItem.UserCode;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColItemName].Value = drugItem.Name;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRegularName].Value = drugItem.Memo;

            FS.HISFC.Models.Pharmacy.Item drugItemTemp = this.itemManager.GetItem(drugItem.UserCode);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColRetailPrice].Value = drugItemTemp.Specs;

            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColSpellCode].Value = drugItem.SpellCode;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColWBCode].Value = drugItem.WBCode;
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColUserCode].Value = drugItem.ID;

            ArrayList alTempMonitor = (new FS.HISFC.BizLogic.Manager.Constant()).GetList("DrugMonitor");
            string temp = string.Empty;
            if (alTempMonitor != null)
            {
                if (alTempMonitor.Count > 0)
                {
                    foreach (FS.HISFC.Models.Base.Const conObjTemp in alTempMonitor)
                    {
                        if (conObjTemp.ID == drugItem.SortID.ToString())
                        {
                            temp = conObjTemp.Name;
                        }
                    }
                }
            }

            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDrugMonitor].Value = temp;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDosageForm].Value = drugItem.DosageForm;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColDegree].Value = drugItem.Degree;
            //this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, (int)ColumnSet.ColBatch].Value = drugItem.Batch;

            //this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Tag = drugItem;
        }

        /// <summary>
        /// 清空
        /// </summary>
        private void Clear()
        {
            this.alItemData.Clear();
            this.alItemDataSave.Clear();
            if (this.neuSpread1_Sheet1.RowCount > 0)
                this.neuSpread1_Sheet1.Rows.Remove(0, this.neuSpread1_Sheet1.RowCount);
            price = 0;
            this.neuLabel2.Text = "";
        }

        /// <summary>
        /// 计算金额
        /// </summary>
        /// <returns>成功 ture 失败 false</returns>
        private bool ComputCost()
        {
            decimal tmpCost = 0;
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                tmpCost += NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDrugCost].Value);
                //if (tmpCost < NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColRetailPrice].Text))
                //{
                //    MessageBox.Show("单项金额不能大于总金额!");
                //    this.neuSpread1.Focus();
                //    this.neuSpread1_Sheet1.SetActiveCell(i, (int)ColumnSet.ColDrugQty, false);
                //    return false;
                //}
            }
            price = tmpCost;
            this.neuLabel2.Text = tmpCost.ToString();
            return true;
        }

        #region 枚举类型

        /// <summary>
        /// 列设置
        /// </summary>
        private enum ColumnSet
        {
            /// <summary>
            /// 项目ID
            /// </summary>
            [Description("项目ID")]
            ColItemID,

            /// <summary>
            /// 项目名称
            /// </summary>
            [Description("项目名称")]
            ColItemName,

            /// <summary>
            /// 通用名
            /// </summary>
            [Description("通用名")]
            ColRegularName,

            /// <summary>
            /// 零售单价
            /// </summary>
            [Description("零售单价")]
            ColRetailPrice,

            /// <summary>
            /// 数量
            /// </summary>
            [Description("数量")]
            ColDrugQty,

            /// <summary>
            /// 单位
            /// </summary>
            [Description("单位")]
            ColDrugUnit,

            /// <summary>
            /// 金额
            /// </summary>
            [Description("金额")]
            ColDrugCost,

            /// <summary>
            ///拼音码
            /// </summary>
            [Description("拼音码")]
            ColSpellCode,

            /// <summary>
            /// 五笔码
            /// </summary>
            [Description("五笔码")]
            ColWBCode,

            /// <summary>
            /// 自定义码
            /// </summary>
            [Description("自定义码")]
            ColUserCode,

            /// <summary>
            /// 包装数量
            /// </summary>
            [Description("包装数量")]
            ColPackQty,
        }

        #endregion 

        private void neuSpread1_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            if (e.Column == (int)ColumnSet.ColDrugQty)
            {
                string unitString = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugUnit].Text;
                if (!string.IsNullOrEmpty(unitString))
                {
                    string tempString = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugQty].Text;
                    if (isPack)//包装单位
                    {
                        this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugCost].Text = (NConvert.ToDecimal(tempString) * NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColRetailPrice].Text)).ToString("F2");
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugCost].Text = (NConvert.ToDecimal(tempString) * NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColRetailPrice].Text)
                                / NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColPackQty].Text)).ToString("F2");
                    }
                    ComputCost();
                }
            }
        }

        private void neuSpread1_ComboSelChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e == null || sender == null)
            {
                return;
            }
            if (e.Column == (int)ColumnSet.ColDrugUnit)
            {
                try
                {
                    string tempString = this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugQty].Text;
                    if (!string.IsNullOrEmpty(tempString))
                    {
                        if (((FarPoint.Win.FpCombo)e.EditingControl).SelectedIndex == 1)//包装单位
                        {
                            this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugCost].Text = (NConvert.ToDecimal(tempString) * NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColRetailPrice].Text)).ToString("F2");
                            isPack = true;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColDrugCost].Text = (NConvert.ToDecimal(tempString) * NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColRetailPrice].Text)
                                / NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[e.Row, (int)ColumnSet.ColPackQty].Text)).ToString("F2");
                            isPack = false;
                        }
                        ComputCost();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }
    }
}

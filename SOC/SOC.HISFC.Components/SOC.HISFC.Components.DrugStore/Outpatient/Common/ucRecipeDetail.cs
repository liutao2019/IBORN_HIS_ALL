using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DrugStore.Outpatient.Common
{
    /// <summary>
    /// [功能描述: 门诊配发药处方明细显示]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// 1、这个比较关键：处方明细显示后还会在保存时从此取数据，必须保证和处方树选择的节点是一致的
    /// </summary>
    public partial class ucRecipeDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRecipeDetail()
        {
            InitializeComponent();

            this.SetFormat();

            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }
        }

        public ucRecipeDetail(IContainer container)
        {
            container.Add(this);

            InitializeComponent();

            this.SetFormat();

        }

        enum EnumColSet
        {
            选择,
            药品编码,
            药品名称,
            规格,
            组,
            用法,
            频次编码,
            频次名称,
            每次用量,
            天数,
            每剂量,
            剂数,
            总量,
            单价,
            金额,
            有效性,
            库存,
            组合号,
            备注

        }

        FS.SOC.Public.FarPoint.ColumnSet colSet = new FS.SOC.Public.FarPoint.ColumnSet();

        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyMgr = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            
        /// <summary>
        /// 金额显示四舍五舍接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Fee.ITruncFee ITruncFee = null;

        private int decimals = 2;

        /// <summary>
        /// 金额小数位数
        /// </summary>
        [Description("金额小数位数"), Category("设置"), Browsable(false)]
        public int Decimals
        {
            get { return decimals; }
            set { decimals = value; }
        }

        Function.EnumQtyShowType enumQtyShowType = Function.EnumQtyShowType.最小单位;

        /// <summary>
        /// 单位显示方式
        /// </summary>
        [Description("单位显示方式"), Category("设置"), Browsable(false)]
        public Function.EnumQtyShowType EnumQtyShowType
        {
            get { return enumQtyShowType; }
            set { enumQtyShowType = value; }
        }

        /// <summary>
        /// 是否选择当处方所有药品
        /// </summary>
        private bool checkAll = true;
        public bool CheckAll
        {
            get
            {
                return this.checkAll;
            }
            set
            {
                this.checkAll = value;
            }
        }

        private void SetFormat()
        {
            this.colSet.AddColumn(
                //列名称，宽度，可见性，锁定，tag值
               new FS.SOC.Public.FarPoint.Column[]{
                new FS.SOC.Public.FarPoint.Column(EnumColSet.选择.ToString(), 30f, false, true, null),//暂时不开发部分发药的功能
                new FS.SOC.Public.FarPoint.Column(EnumColSet.药品编码.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.药品名称.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.规格.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.组.ToString(), 15f, true, true, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.用法.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.频次编码.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.频次名称.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.每次用量.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.天数.ToString(), 60f, false, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.每剂量.ToString(), 60f, false, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.剂数.ToString(), 60f, false, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.总量.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.单价.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.金额.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.有效性.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.库存.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.组合号.ToString(), 60f, false, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.备注.ToString(), 60f, true, false, null)
                }
               );

            this.neuFpEnter1_Sheet1.Columns.Count = this.colSet.Count;
            this.neuFpEnter1_Sheet1.RowCount = 0;

            this.neuFpEnter1_Sheet1.ColumnHeader.Rows[0].Height = 34F;

            if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugedSetting.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuFpEnter1_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugedSetting.xml");
                for (int colIndex = 0; colIndex < this.neuFpEnter1_Sheet1.Columns.Count; colIndex++)
                {
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    if (this.neuFpEnter1_Sheet1.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = (FarPoint.Win.Spread.CellType.TextCellType)this.neuFpEnter1_Sheet1.Columns[colIndex].CellType;
                        t.ReadOnly = true;
                    }
                    else if (this.neuFpEnter1_Sheet1.Columns[colIndex].CellType == null)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                        t.ReadOnly = true;
                        this.neuFpEnter1_Sheet1.Columns[colIndex].CellType = t;
                    }
                }
                
            }
            else
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                for (int colIndex = 0; colIndex < this.neuFpEnter1_Sheet1.Columns.Count; colIndex++)
                {
                    this.neuFpEnter1_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                    FS.SOC.Public.FarPoint.Column c = this.colSet.GetColumn(colIndex);
                    this.neuFpEnter1_Sheet1.Columns[colIndex].Label = c.Name;
                    this.neuFpEnter1_Sheet1.Columns[colIndex].Width = c.Width;
                    this.neuFpEnter1_Sheet1.Columns[colIndex].Visible = c.Visible;
                    this.neuFpEnter1_Sheet1.Columns[colIndex].Locked = c.Locked;
                    if (colIndex != (int)EnumColSet.金额 && colIndex != (int)EnumColSet.选择 && colIndex != (int)EnumColSet.组)
                    {
                        this.neuFpEnter1_Sheet1.Columns[colIndex].CellType = t;
                    }
                }
            }
            FarPoint.Win.Spread.CellType.NumberCellType numberCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numberCellType.DecimalPlaces = this.decimals;
            numberCellType.ReadOnly = true;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.金额].CellType = numberCellType;

            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.选择].CellType = checkBoxCellType;

          

            this.neuFpEnter1.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);
            this.neuFpEnter1.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuFpEnter1_ColumnWidthChanged);
        }

        public void ShowInfo(ArrayList alRecipeDetail)
        {
            if (ITruncFee == null)
            {
                ITruncFee = (FS.HISFC.BizProcess.Interface.Fee.ITruncFee)FS.HISFC.BizProcess.Interface.Fee.InterfaceManager.GetTruncFeeType();
            }
            if (alRecipeDetail == null)
            {
                this.neuFpEnter1_Sheet1.RowCount = 0;
                return;
            }

            this.neuFpEnter1_Sheet1.RowCount = 0;//清除现有数据是必须的
            this.neuFpEnter1_Sheet1.RowCount = alRecipeDetail.Count + 1;

            decimal totCost = 0;//记录总金额
            decimal totQty = 0; 
            int index = 0;

            bool isHaveHerbal = false;//是否有草药，有草药就必须显示剂数

            for (; index < alRecipeDetail.Count; index++)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alRecipeDetail[index] as FS.HISFC.Models.Pharmacy.ApplyOut;

                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                if (item == null)
                {
                    //
                    return;
                }

                //数量显示处理
                string applyPackQty = "";
                string price = applyOut.Item.PriceCollection.RetailPrice.ToString("F4").TrimEnd('0').TrimEnd('.') + "元" + "/" + applyOut.Item.PackUnit;
                if (this.EnumQtyShowType == Function.EnumQtyShowType.包装单位 || (this.EnumQtyShowType == Function.EnumQtyShowType.门诊拆分 && item.SplitType == "1"))
                {
                    int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                    //applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                    applyQtyInt = (int)(applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
                    //applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
                    applyRe = applyOut.Operation.ApplyQty - applyQtyInt * applyOut.Item.PackQty;
                    if (applyQtyInt > 0)
                    {
                        applyPackQty += applyQtyInt.ToString() + applyOut.Item.PackUnit;
                    }
                    if (applyRe > 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    }
                }
                else
                {
                    //applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    applyPackQty = applyOut.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    price = (applyOut.Item.PriceCollection.RetailPrice / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "元" + "/" + applyOut.Item.MinUnit;
                }
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.选择].Value = true;
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.药品编码].Text = item.UserCode;
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.药品名称].Text = applyOut.Item.Name;
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.规格].Text = applyOut.Item.Specs;
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.用法].Text
                    = (string.IsNullOrEmpty(applyOut.Usage.Name) ? (SOC.HISFC.BizProcess.Cache.Common.GetUsageName(applyOut.Usage.ID)) : applyOut.Usage.Name);
                //{1731D199-20AD-4dfe-ADCA-96279E343D9E}
                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                {
                    isHaveHerbal = true;
                    //this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每剂量].Text = applyOut.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每剂量].Text = applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.剂数].Text = applyOut.Days.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.频次编码].Text = applyOut.Frequency.ID;
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.频次名称].Text
                        = (string.IsNullOrEmpty(applyOut.Frequency.Name) ? (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(applyOut.Frequency.ID)) : applyOut.Frequency.Name);
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.频次编码].Text = applyOut.Frequency.ID;
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.频次名称].Text 
                        = (string.IsNullOrEmpty(applyOut.Frequency.Name) ? (SOC.HISFC.BizProcess.Cache.Order.GetFrequencyName(applyOut.Frequency.ID)) : applyOut.Frequency.Name);
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每次用量].Text = applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                    //Days本意是用于存储草药剂数的，西药为1，部分项目可能会扩展含义
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.天数].Text = applyOut.Days.ToString("F4").TrimEnd('0').TrimEnd('.');
             
                }
                totQty += applyOut.DoseOnce;

                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.总量].Text = applyPackQty;
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.单价].Text = price;

                // {5D579726-0CDC-4f7d-BF02-EC6673B6BF41}
                FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();
                ArrayList alStorageList = new ArrayList();
                alStorageList = pharmacyMgr.QueryStorageList(applyOut.StockDept.ID, applyOut.Item.ID);
                decimal storeQty = 0m;

                foreach (FS.HISFC.Models.Pharmacy.Storage obj in alStorageList)
                {
                    storeQty += obj.StoreQty;
                    storage = obj;
                }

                decimal packQty = Math.Floor(storeQty / storage.Item.PackQty);
                decimal minQty = storeQty - packQty * storage.Item.PackQty;
                string Qty = string.Empty;
                if (packQty == 0)
                {
                    Qty = string.Format("{0}{1}", minQty, storage.Item.MinUnit);
                }
                else if (minQty == 0)
                {
                    Qty = string.Format("{0}{1}", packQty, storage.Item.PackUnit);
                }
                else
                {
                    Qty = string.Format("{0}{1} {2}{3}", packQty, storage.Item.PackUnit, minQty, storage.Item.MinUnit);
                }

                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.库存].Text = Qty;

                //decimal cost = FS.FrameWork.Function.NConvert.ToDecimal((applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty)).ToString("F" + this.decimals.ToString()));

                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.备注].Text = applyOut.Memo;
                decimal cost = 0m;
                if (ITruncFee != null)
                {
                    cost = FS.FrameWork.Function.NConvert.ToDecimal(ITruncFee.TruncFee(applyOut));
                }
                else
                {
                    cost = FS.FrameWork.Function.NConvert.ToDecimal((applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty)).ToString("F" + this.decimals.ToString()));
                }

                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.金额].Text = cost.ToString();

                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.有效性].Text = "有效";
                    this.neuFpEnter1_Sheet1.Rows[index].ForeColor = System.Drawing.Color.Black;
                    totCost += cost;
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.有效性].Text = "退费作废";
                    this.neuFpEnter1_Sheet1.Rows[index].ForeColor = System.Drawing.Color.Red;
                }
                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.组合号].Text = applyOut.CombNO;
                }
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.组合号].Text = applyOut.CombNO + applyOut.Operation.ApplyOper.OperTime.ToString();
                }

                this.neuFpEnter1_Sheet1.Rows[index].Tag = applyOut;
            }

            if (neuFpEnter1_Sheet1.Columns[(int)EnumColSet.药品编码].Visible && neuFpEnter1_Sheet1.Columns[(int)EnumColSet.药品编码].Width > 0)
            {
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.药品编码].Text = "合计";
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每剂量].Text = totQty.ToString();
            }
            else
            {
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.药品名称].Text = "合计";
                this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每剂量].Text = totQty.ToString();
            }

            this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.金额].Text = totCost.ToString();

            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.剂数].Visible = isHaveHerbal;
            //{2B385ABF-5086-4cba-8B79-A853CD894833}
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.频次编码].Visible = true;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.每剂量].Visible = isHaveHerbal;

            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.频次名称].Visible = true;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.每次用量].Visible = !isHaveHerbal;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.天数].Visible = !isHaveHerbal;

            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.每剂量].Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, System.Drawing.FontStyle.Bold);
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.总量].Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, System.Drawing.FontStyle.Bold);

            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.组].CellType = null;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.组].Locked = true;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.组].ForeColor = System.Drawing.Color.Red;
            Function.DrawCombo(this.neuFpEnter1_Sheet1, (int)EnumColSet.组合号, (int)EnumColSet.组);

        }

        /// <summary>
        /// 获取当前选择的出库申请数据
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> GetSelectInfo()
        {
            FS.SOC.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.SOC.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alSelectData = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
            for (int i = 0; i < this.neuFpEnter1_Sheet1.Rows.Count - 1; i++)
            {
                if ((bool)this.neuFpEnter1_Sheet1.Cells[i, (int)EnumColSet.选择].Value)
                {
                    //克隆，避免外部程序或接口更改数据
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = (this.neuFpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut).Clone();
                    applyOut.CostDecimals = Function.GetCostDecimals();
                    alSelectData.Add(applyOut);
                }
            }
            if (alSelectData.Count != this.neuFpEnter1_Sheet1.Rows.Count - 1)
            {
                this.CheckAll = false;
            }
            else
            {
                this.CheckAll = true;
            }
            return alSelectData;
        }

        public void Clear()
        {
            this.neuFpEnter1_Sheet1.RowCount = 0;
        }

        public void SelectNone()
        {
            for (int i = 0; i < this.neuFpEnter1_Sheet1.Rows.Count - 1; i++)
            {
                this.neuFpEnter1_Sheet1.Cells[i, (int)EnumColSet.选择].Value = "FALSE";
            }
        }

        void neuFpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuFpEnter1_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugedSetting.xml");
        }

    }
}

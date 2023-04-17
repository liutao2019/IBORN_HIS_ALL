using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ShenZhen.BinHai.Outpatient.Common
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
            组合号,
            机配信息
        }

        FS.SOC.Public.FarPoint.ColumnSet colSet = new FS.SOC.Public.FarPoint.ColumnSet();


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

        FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType enumQtyShowType = FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType.包装单位;

        /// <summary>
        /// 单位显示方式
        /// </summary>
        [Description("单位显示方式"), Category("设置"), Browsable(true)]
        public FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType EnumQtyShowType
        {
            get { return enumQtyShowType; }
            set { enumQtyShowType = value; }
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
                new FS.SOC.Public.FarPoint.Column(EnumColSet.组.ToString(), 18f, true, true, null),
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
                new FS.SOC.Public.FarPoint.Column(EnumColSet.组合号.ToString(), 60f, false, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumColSet.机配信息.ToString(), 80f, true, false, null)
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
            if (alRecipeDetail == null)
            {
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
                string price = applyOut.Item.PriceCollection.RetailPrice.ToString().TrimEnd('0').TrimEnd('.') + "元" + "/" + applyOut.Item.PackUnit;
                if (this.EnumQtyShowType == FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType.包装单位 || (this.EnumQtyShowType == FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType.门诊拆分 && item.SplitType == "1"))
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

                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                {
                    isHaveHerbal = true;
                    //this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每剂量].Text = applyOut.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.每剂量].Text = applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.剂数].Text = applyOut.Days.ToString("F4").TrimEnd('0').TrimEnd('.');
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

               // decimal cost = FS.FrameWork.Function.NConvert.ToDecimal((applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty)).ToString("F" + this.decimals.ToString()));
                decimal cost = FS.FrameWork.Function.NConvert.ToDecimal((applyOut.Item.PriceCollection.RetailPrice * (applyOut.Operation.ApplyQty / applyOut.Item.PackQty)).ToString("F" + this.decimals.ToString()));

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
                if (string.IsNullOrEmpty(applyOut.ExtFlag))
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.机配信息].Text = "未配 ";
                else
                {
                    this.neuFpEnter1_Sheet1.Cells[index, (int)EnumColSet.机配信息].Text = "已配 " + applyOut.User03 + applyOut.Item.PackUnit;
                    //if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                    //    this.neuFpEnter1_Sheet1.Rows[index].ForeColor = System.Drawing.Color.OldLace;
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
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.频次编码].Visible = isHaveHerbal;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.每剂量].Visible = isHaveHerbal;
            
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.频次名称].Visible = !isHaveHerbal;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.每次用量].Visible = !isHaveHerbal;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.每剂量].Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, System.Drawing.FontStyle.Bold);
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.总量].Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, System.Drawing.FontStyle.Bold);

            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.组].CellType = null;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.组].Locked = true;
            this.neuFpEnter1_Sheet1.Columns[(int)EnumColSet.组].ForeColor = System.Drawing.Color.Red;
            FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.DrawCombo(this.neuFpEnter1_Sheet1, (int)EnumColSet.组合号, (int)EnumColSet.组);

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
                    applyOut.CostDecimals = FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.GetCostDecimals();
                    alSelectData.Add(applyOut);
                }
            }

            return alSelectData;
        }

        public void Clear()
        {
            this.neuFpEnter1_Sheet1.RowCount = 0;
        }

        void neuFpEnter1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuFpEnter1_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\OutpatientDrugedSetting.xml");
        }

        /// <summary>
        /// 欧娲发药
        /// </summary>
        /// <param name="rtnText">返回信息</param>
        /// <returns>1成功  -1失败</returns>
        public int RowaDrug(ref string rtnText)
        {
            /** [说明]
             * 1.校验设备库存, 允许发药往下走, 反之跳出
             * 2.发送出药指令, 不予考虑成功与否(机器处理完成需等待,故跳过)
             * 3.记录出药数量于本地表中, 并显示于配药信息
             * 可能出现问题:
             *     1.不存在预扣库存概念,可能会有已发送出药指令,结果设备里的药已被其他终端指令发完;
             *     2.因不考虑出药成功与否,故记录出药数量,可能会造成出药不成功,本地却已记录并显示机配成功;
             *     ……
             * */

            try
            {
                int j = 0;
                FS.HISFC.Models.Registration.Register register = null;
                string sqlStr = @"UPDATE PHA_COM_APPLYOUT SET EXT_FLAG='{1}' WHERE APPLY_NUMBER='{0}'";

                //获取发药机出药口 

                FS.FrameWork.Models.NeuObject rowaTermial = new FS.FrameWork.Models.NeuObject();
                FS.HISFC.BizLogic.Manager.Constant ConManger = new FS.HISFC.BizLogic.Manager.Constant();

                rowaTermial = ConManger.GetConstant("RowaTerminal", FS.SOC.Local.DrugStore.ShenZhen.Common.Function.GetLocalIpv4()[0]);

                ArrayList al = FS.SOC.HISFC.BizProcess.CommonInterface.CommonController.CreateInstance().QueryConstant("RowaTerminal");
               
                for (int i = 0; i < this.neuFpEnter1_Sheet1.RowCount; i++)
                {
                    if (this.neuFpEnter1_Sheet1.Rows[i].Tag != null)
                    {

                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.neuFpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                        applyOut.Item.Product.BarCode = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID).Product.BarCode;
                        if (applyOut != null && !string.IsNullOrEmpty(applyOut.Item.Product.BarCode))  //允许机配的药品
                        {
                            j++;  //序号

                            #region 1.校验设备库存

                            #region HL7发送消息到平台

                            object resultInfo = new object();
                            string ERR = "";

                            if (FS.SOC.Local.DrugStore.ShenZhen.Common.InterfaceManager.GetISender() != null)
                            {

                                FS.SOC.Local.DrugStore.ShenZhen.Common.InterfaceManager.GetISender().Send(applyOut, ref resultInfo, ref ERR);

                            }
                            else
                            {
                                rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]获取库存消息接口未实例化, 不能配药.\r\n";
                                continue;
                            }
                            #endregion

                            #region 库存比对

                     

                            int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                            string applyPackQty = "";
                            FS.HISFC.Models.Pharmacy.ApplyOut rowaStore = resultInfo as FS.HISFC.Models.Pharmacy.ApplyOut;
                            if (rowaStore != null)
                            {
                                if (string.IsNullOrEmpty(rowaStore.ExtFlag) || rowaStore.ExtFlag == "0")
                                {
                                    rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]远程设备库存量为0, 不能配药.\r\n";
                                    continue;
                                }
                                else if (this.EnumQtyShowType == FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType.包装单位 || (this.EnumQtyShowType == FS.SOC.HISFC.Components.DrugStore.Outpatient.Common.Function.EnumQtyShowType.门诊拆分 && applyOut.Item.SplitType == "1"))
                                {
                                  

                                    //int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                                    //applyQtyInt = (int)(applyOut.Operation.ApplyQty * applyOut.Days / applyOut.Item.PackQty);
                                    applyQtyInt = (int)(applyOut.Operation.ApplyQty / applyOut.Item.PackQty);
                                    //applyRe = applyOut.Operation.ApplyQty * applyOut.Days - applyQtyInt * applyOut.Item.PackQty;
                                    applyRe = applyOut.Operation.ApplyQty - applyQtyInt * applyOut.Item.PackQty;
                                    if (applyQtyInt > 0)
                                    {
                                        applyPackQty += applyQtyInt.ToString();
                                    }
                                                              

                                    if (applyRe > 0)
                                    {
                                        rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]申请数量不能按其包装数量取整, 远程设备无法配药.\r\n";
                                        continue;
                                    }
                                }
                                else
                                {
                                    rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]非包装单位取整发药药品, 不能由远程设备配药.\r\n";
                                    continue;
                                }
                            }
                            else
                            {
                                rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]获取远程设备库存出错:" + ERR + ".\r\n";
                                continue;
                            }

                            if (FS.FrameWork.Function.NConvert.ToInt32(rowaStore.ExtFlag) < applyQtyInt)  //库存不足
                            {
                                rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]远程设备库存不足, 不能配药.\r\n";
                                continue;
                            }
                            applyOut.ExtFlag = rowaStore.ExtFlag;  //包装数量
                            applyOut.ExtFlag1 = ((FS.HISFC.Models.Base.Spell)(rowaTermial)).UserCode;

                            #endregion

                            #endregion

                            #region 2.发送出药指令

                            ArrayList alApplyOut = new ArrayList();
                            alApplyOut.Add(applyOut);

                            if (register == null || string.IsNullOrEmpty(register.ID))
                                register = (new FS.HISFC.BizLogic.Registration.Register()).GetByClinic(applyOut.PatientNO) ;

                            if (register == null || string.IsNullOrEmpty(register.ID))
                            {
                                rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]获取患者挂号信息出错, 不能配药.\r\n";
                                continue;
                            }

                            if (FS.SOC.Local.DrugStore.ShenZhen.Common.InterfaceManager.GetIOrder() != null)
                            {

                                FS.SOC.Local.DrugStore.ShenZhen.Common.InterfaceManager.GetIOrder().SendDrugInfo(register, alApplyOut, true);

                            }
                            else
                            {
                                rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]出药指令消息接口未实例化, 不能配药.\r\n";
                                continue;
                            }

                            #endregion

                            #region 3.记录出药数量

                            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            dbMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            if (dbMgr.ExecNoQuery(string.Format(sqlStr, applyOut.ID, applyQtyInt.ToString())) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                rtnText += j.ToString() + ".[" + applyOut.Item.Name + "]记录出药数量出错:" + dbMgr.Err + ".\r\n";
                                continue;
                            }
                            FS.FrameWork.Management.PublicTrans.Commit();

                            this.neuFpEnter1_Sheet1.Cells[i, (int)EnumColSet.机配信息].Text = "已配 " + applyQtyInt.ToString() + applyOut.Item.PackUnit;
                            if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                                this.neuFpEnter1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.OldLace;

                            applyOut.User03 = applyQtyInt.ToString() + "剩余库存" + (FS.FrameWork.Function.NConvert.ToInt32(applyOut.ExtFlag) - applyQtyInt).ToString();

                            rtnText += j.ToString() + ".[" + applyOut.Item.Name + "] 已机配[" + applyQtyInt.ToString() + applyOut.Item.PackUnit + "剩余库存" + (FS.FrameWork.Function.NConvert.ToInt32(applyOut.ExtFlag) - applyQtyInt).ToString() + "].\r\n";

                            #endregion
                        }
                    }
                }
                if (j == 0)
                {
                    rtnText = "待发药品中无需要机配的药品, 请手工配药!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                rtnText = ex.Message;
                return -1;
            }
            return 1;
        }

    }
}

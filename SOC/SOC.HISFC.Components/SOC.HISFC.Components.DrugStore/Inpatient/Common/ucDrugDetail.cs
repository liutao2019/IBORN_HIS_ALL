using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.DrugStore.Inpatient.Common
{
    /// <summary>
    /// [功能描述: 住院药房发药数据显示]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucDrugDetail : UserControl
    {
        public ucDrugDetail()
        {
            InitializeComponent();

            this.neuContext = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();

            this.neuDrugDetailSpread.MouseUp -= new MouseEventHandler(neuDrugDetailSpread_MouseUp);

            this.neuDrugDetailSpread.MouseUp += new MouseEventHandler(neuDrugDetailSpread_MouseUp);

            this.SetFormat();
        }

        /// <summary>
        /// 计算汇总量用，哈希表汇总
        /// </summary>
        private Hashtable hsTotInfo = new Hashtable();

        /// <summary>
        /// 计算汇总量用，记住顺序
        /// </summary>
        private ArrayList alTotInfo = new ArrayList();

        /// <summary>
        /// 摆药单高亮科室编码
        /// </summary>
        private string hightLightDept = "";
        /// <summary>
        /// 摆药单高亮科室编码
        /// </summary>
        public string HightLightDept
        {
            get { return hightLightDept; }
            set { hightLightDept = value; }
        }


        /// <summary>
        /// 总量列设置
        /// </summary>
        enum EnumTotColSet
        {
            选中,
            药品编码,
            编码,
            药品名称,
            规格,
            总量,
            单价,
            摆药量,
            发药科室
        }

        /// <summary>
        /// 总量列设置
        /// </summary>
        FS.SOC.Public.FarPoint.ColumnSet totColSet = new FS.SOC.Public.FarPoint.ColumnSet();

        /// <summary>
        /// 明细列设置
        /// </summary>
        enum EnumDetailColSet
        {
            床号,
            姓名,
            选中,
            编码,
            药品名称,
            规格,
            组,
            用法,
            每剂量,
            频次编码,
            频次名称,
            每次用量,
            滴速,
            剂数,
            总量,
            备注,
            医嘱流水号,
            开立医生,
            用药时间,
            申请时间,
            发药时间,
            货位号,
            单价,
            发药人,
            医嘱类型,
            组合号,
            发药科室,
            摆药单,
            申请科室,
            开立科室,
            发送类型
            
        }

        /// <summary>
        /// 明细列设置
        /// </summary>
        FS.SOC.Public.FarPoint.ColumnSet detailColSet = new FS.SOC.Public.FarPoint.ColumnSet();
        // {F9B890A9-D02C-4e38-BB39-F64251AF8F64
        FS.FrameWork.Public.ObjectHelper orderTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        FS.HISFC.BizLogic.Manager.OrderType orderManager = new FS.HISFC.BizLogic.Manager.OrderType();

        FS.HISFC.BizLogic.Order.Order orderone = new FS.HISFC.BizLogic.Order.Order();
        
        #region 增加右键按患者选择数据

        /// <summary>
        /// 右键菜单
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip neuContext = null;

        void neuDrugDetailSpread_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                neuContext.Items.Clear();

                ToolStripMenuItem ts1 = new ToolStripMenuItem("按人选择");
                ts1.Click += new EventHandler(ts1_Click);
                neuContext.Items.Add(ts1);
                ToolStripMenuItem ts2 = new ToolStripMenuItem("取消按人选择");
                ts2.Click += new EventHandler(ts2_Click);
                neuContext.Items.Add(ts2);
                this.neuContext.Show(this.neuDrugDetailSpread, new Point(e.X, e.Y));
            }
        }

        void ts2_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == tpDetailDrug)
            {
                int curRow = this.neuDrugDetailSpread.ActiveSheet.ActiveRowIndex;
                if (curRow < 0)
                {
                    return;
                }
                string patientName = this.neuDrugDetailSpread_Sheet1.Cells[curRow, (int)EnumDetailColSet.姓名].Text;

                for (int rowIndex = curRow; rowIndex < neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
                {
                    string patientNameTmp = this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.姓名].Text;
                    if (patientNameTmp != patientName)
                    {
                        break;
                    }
                    if (!neuDrugDetailSpread_Sheet1.Rows[rowIndex].Locked)
                    {
                        neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = false;
                    }
                }

                for (int rowIndex = curRow; rowIndex >= 0; rowIndex--)
                {
                    string patientNameTmp = this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.姓名].Text;
                    if (patientNameTmp != patientName)
                    {
                        break;
                    }
                    if (!neuDrugDetailSpread_Sheet1.Rows[rowIndex].Locked)
                    {
                        neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = false;
                    }
                }
            }
        }

        void ts1_Click(object sender, EventArgs e)
        {
            if (this.neuTabControl1.SelectedTab == tpDetailDrug)
            {
                int curRow = this.neuDrugDetailSpread.ActiveSheet.ActiveRowIndex;
                if (curRow < 0)
                {
                    return;
                }
                string patientName = this.neuDrugDetailSpread_Sheet1.Cells[curRow, (int)EnumDetailColSet.姓名].Text;

                for (int rowIndex = curRow; rowIndex < neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
                {
                    string patientNameTmp = this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.姓名].Text;

                    if (patientNameTmp != patientName)
                    {
                        break;
                    }
                    if (!neuDrugDetailSpread_Sheet1.Rows[rowIndex].Locked)
                    {
                        neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = true;
                    }
                }

                for (int rowIndex = curRow; rowIndex >= 0; rowIndex--)
                {
                    string patientNameTmp = this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.姓名].Text;
                    if (patientNameTmp != patientName)
                    {
                        break;
                    }
                    if (!neuDrugDetailSpread_Sheet1.Rows[rowIndex].Locked)
                    {
                        neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = true;
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="alDrugMessage">摆药通知</param>
        /// <param name="isAutoSelected">是否自动选中</param>
        public void ShowDrugMessage(ArrayList alDrugMessage, bool isAutoSelected)
        {
            //必须先清除数据
            this.neuDrugMessageSpread_Sheet1.RowCount = 0;
            if (alDrugMessage == null)
            {
                return;
            }

            this.neuDrugMessageSpread_Sheet1.RowCount = alDrugMessage.Count;
            int rowIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.DrugMessage drugMessage in alDrugMessage)
            {
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 0].Value = isAutoSelected;
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 1].Value = "打印";

                //将科室名称赋值
                drugMessage.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugMessage.ApplyDept.ID);
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 2].Value = drugMessage.ApplyDept.Name;
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 3].Value = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClassName(drugMessage.DrugBillClass.ID);
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 4].Value = drugMessage.SendTime;
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 5].Value = drugMessage.Name;
                string sendType = "临时发送";
                if (drugMessage.SendType == 1)
                {
                    sendType = "集中发送";
                }
                else if (drugMessage.SendType == 4)
                {
                    sendType = "紧急发送";
                }
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 6].Value = sendType;
                this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag = drugMessage;

                rowIndex++;
            }
        }

        /// <summary>
        /// 显示摆药通知信息
        /// </summary>
        /// <param name="alDrugBill">摆药单数组</param>
        /// <param name="isAutoSelected">是否自动选中</param>
        /// <param name="isAdd">是否追加到界面</param>
        public void ShowDrugClassBill(ArrayList alDrugBill, bool isAutoSelected, bool isAdd)
        {
            if (!isAdd)
            {
                //必须先清除数据
                this.neuDrugMessageSpread_Sheet1.RowCount = 0;
            }
            if (alDrugBill == null || alDrugBill.Count == 0)
            {
                return;
            }

            this.neuDrugMessageSpread_Sheet1.AddRows(this.neuDrugMessageSpread_Sheet1.RowCount, alDrugBill.Count);
            int rowIndex = 0;
            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBill in alDrugBill)
            {
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 0].Value = isAutoSelected;
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 1].Value = "打印";

                //将科室名称赋值
                drugBill.ApplyDept.Name = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(drugBill.ApplyDept.ID);
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 2].Value = drugBill.ApplyDept.Name;
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 3].Value = drugBill.DrugBillNO + SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClassName(drugBill.ID);
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 4].Value = drugBill.Oper.OperTime;
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 5].Value = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeInfo(drugBill.Oper.ID);
                string sendType = "临时发送";
                this.neuDrugMessageSpread_Sheet1.Cells[rowIndex, 6].Value = sendType;
                this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag = drugBill;

                rowIndex++;
            }
        }


        //FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 显示摆药申请明细
        /// </summary>
        /// <param name="alApplyOut">摆药申请明细</param>
        /// <param name="isAutoSelected">是否自动选中</param>
        /// <param name="qtyShowType">发药总量显示方式</param>
        /// <param name="isNurseStationApply">是否是病区申请(true集中发送、病区取药查询用 false药房摆药用)</param>
        public void ShowDetail(ArrayList alApplyOut, bool isAutoSelected, Function.EnumQtyShowType qtyShowType, bool isNurseStationApply, Function.EnumInpatintDrugApplyType DrugApplyType, bool isConcentratedSend)
        {

            orderTypeHelper.ArrayObject = orderManager.GetList();// {F9B890A9-D02C-4e38-BB39-F64251AF8F64
            //必须先清除数据
            this.neuDrugDetailSpread_Sheet1.RowCount = 0;
            this.hsTotInfo.Clear();
            this.alTotInfo.Clear();

            if (alApplyOut == null)
            {
                return;
            }

            //((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread_Sheet1)).BeginInit();

            this.neuDrugDetailSpread_Sheet1.RowCount = alApplyOut.Count;
            int rowIndex = 0;

            bool isHaveHerbal = false;//是否有草药，有草药就必须显示剂数

            FarPoint.Win.Spread.CellType.RichTextCellType richTextType = new FarPoint.Win.Spread.CellType.RichTextCellType();
            richTextType.Multiline = true;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            Application.DoEvents();

            string adrugmsg = "";
            string bdrugmsg = "";
            string ddrugmsg = "";

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(rowIndex, alApplyOut.Count);
                
                #region
                string bedNO = applyOut.BedNO;
                if (bedNO.Length > 4)
                {
                    bedNO = bedNO.Substring(4);
                }
                FS.HISFC.Models.Pharmacy.Item item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID);
                if (item == null)
                {
                    //
                    return;
                }

                if (applyOut.Item.PackQty == 0)
                {
                    applyOut.Item.PackQty = 1;
                }
                string emergency = this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Label;
                if (applyOut.SendType == 4)
                {
                    emergency = "紧急";
                }
                this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Label = emergency;
                if (applyOut.BillClassNO == "R")  //退药显示红色
                {
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Label = "退";
                }
                else
                {
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Label = "";
                }
                if (applyOut.SendType == 4)
                {
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].ForeColor = Color.Blue;
                }

                if (item.SpecialFlag == "1" || item.SpecialFlag == "2" || item.SpecialFlag == "3" || item.SpecialFlag == "4")
                {
                    string level = "";
                    if (item.SpecialFlag == "1")
                    {
                        level = "A级";
                        if(!adrugmsg.Contains(item.Name))
                            adrugmsg += item.Name+" ";
                        //MessageBox.Show("★药品【" + item.Name + "】属于" + level + "高警示药品，注意核对药品名称、规格、用法、剂量、使用浓度及滴速等!", "提示", MessageBoxButtons.OK);
                    }

                    if (item.SpecialFlag == "2")
                    {
                        level = "B级";
                        if (!bdrugmsg.Contains(item.Name))
                             bdrugmsg += item.Name + " ";
                        //MessageBox.Show("☆药品【" + item.Name + "】属于" + level + "高警示药品，注意核对药品名称、规格、用法、剂量等!", "提示", MessageBoxButtons.OK);
                    }

                    if (item.SpecialFlag == "3")
                        level = "C级";

                    if (item.SpecialFlag == "4")
                    {
                        level = "易混淆";
                        if (!ddrugmsg.Contains(item.Name))
                             ddrugmsg += item.Name + " ";
                        //MessageBox.Show("▲药品【" + item.Name + "】属于" + level + "药品，注意核对药品名称、规格等!", "提示", MessageBoxButtons.OK);
                    }
                }
                

                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.床号].Value = bedNO;  //床号
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.姓名].Value = applyOut.PatientName;  //姓名
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = isAutoSelected;
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.编码].Value = item.UserCode;
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.药品名称].Value = applyOut.Item.Name;
                
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.规格].Value = applyOut.Item.Specs;
                //this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.组];
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.用法].Value = applyOut.Usage.Name;
                if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
                {
                    isHaveHerbal = true;
                    //this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.每剂量].Text = applyOut.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.每剂量].Text = applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.剂数].Text = applyOut.Days.ToString("F4").TrimEnd('0').TrimEnd('.');
                    //{9D30F38A-83FF-46C1-BE88-25D9011EAB52}加入频次信息
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.频次编码].Text = applyOut.Frequency.ID;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.频次名称].Text = applyOut.Frequency.Name;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.每次用量].Text = applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;
                }
                else
                {
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.频次编码].Text = applyOut.Frequency.ID;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.频次名称].Text = applyOut.Frequency.Name;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.每次用量].Text = applyOut.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.DoseUnit;

                }
                if (qtyShowType == Function.EnumQtyShowType.最小单位)
                {
                    //this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.总量].Value = applyOut.Operation.ApplyQty * applyOut.Days + applyOut.Item.MinUnit;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.总量].Value = applyOut.Operation.ApplyQty + applyOut.Item.MinUnit;
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.单价].Value = (applyOut.Item.PriceCollection.RetailPrice / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + applyOut.Item.MinUnit;
                }
                else
                {
                    //数量显示处理
                    string applyPackQty = "";
                    string price = applyOut.Item.PriceCollection.RetailPrice.ToString() + "元" + "/" + applyOut.Item.PackUnit;
                    if ((qtyShowType == Function.EnumQtyShowType.中成药包装单位 && item.SysClass.ID.ToString() == "PCZ") || qtyShowType == Function.EnumQtyShowType.包装单位)
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
                        if (applyOut.SystemType == "Z2" && isNurseStationApply)
                        {
                            applyPackQty = "-" + applyPackQty;
                        }
                        this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.总量].Value = applyPackQty;
                        this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.单价].Value = price;
                    }
                    else
                    {
                        //applyPackQty = (applyOut.Operation.ApplyQty * applyOut.Days).ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                        applyPackQty = applyOut.Operation.ApplyQty.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                        if (applyOut.SystemType == "Z2" && isNurseStationApply)
                        {
                            applyPackQty = "-" + applyPackQty;
                        }
                        this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.总量].Value = applyPackQty;
                        this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.单价].Value = (applyOut.Item.PriceCollection.RetailPrice / applyOut.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.') + "元/" + applyOut.Item.MinUnit;
                    }


                }
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.备注].Value = applyOut.Memo;

                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.货位号].Value = applyOut.PlaceNO;
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.用药时间].Value = applyOut.UseTime;

                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.申请时间].Value = applyOut.Operation.ApplyOper.OperTime.ToString();
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.发药人].Value = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(applyOut.Operation.ExamOper.ID);

                if (applyOut.Operation.ExamOper.OperTime > System.DateTime.MinValue)
                {
                    this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.发药时间].Value = applyOut.Operation.ExamOper.OperTime.ToString();
                }
                // {F9B890A9-D02C-4e38-BB39-F64251AF8F64
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.医嘱类型].Value = orderTypeHelper.GetName(applyOut.OrderType.ID);

               

                string extendComboNO = "";
                if (applyOut.SystemType == "Z2")
                {
                    extendComboNO = applyOut.ID;
                }
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.组合号].Value = applyOut.CombNO + "-" + applyOut.UseTime.ToString() + extendComboNO;
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.医嘱流水号].Value = applyOut.OrderNO;
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.发药科室].Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID);
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.摆药单].Value = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugBillClassName(applyOut.BillClassNO) + (applyOut.DrugNO == "0" ? "" : applyOut.DrugNO);
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.申请科室].Value = SOC.HISFC .BizProcess .Cache .Common .GetDeptName(applyOut.ApplyDept.ID);
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.开立科室].Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.RecipeInfo.Dept.ID);
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.开立医生].Value = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(applyOut.RecipeInfo.ID);
                
                //粗体显示
                if (this.HightLightDept .Contains(applyOut.RecipeInfo.Dept.ID)&&!string.IsNullOrEmpty(this.HightLightDept)&&!string.IsNullOrEmpty(applyOut .RecipeInfo .Dept .ID))
                {
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, System.Drawing.FontStyle.Bold);
                }
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.发送类型].Value = SOC.HISFC.BizProcess.Cache.Pharmacy.GetDrugApplySendTypeName(applyOut.SendType);
                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();

                order = orderone.QueryOneOrder(applyOut.OrderNO);
                string dripspeed = "";
                if (order != null)
                    dripspeed = order.Dripspreed;
                this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.滴速].Value = dripspeed;
                if (DrugApplyType == Function.EnumInpatintDrugApplyType.全区发送)
                {
                    //当天没有集中发送，发送类型是临时，并且没有发药的才显示未发送
                    if (!isConcentratedSend && applyOut.SendType == 2 && applyOut.State == "0")
                    {
                        this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.发送类型].Value = "未发送";
                    }
                }
                else if (DrugApplyType == Function.EnumInpatintDrugApplyType.按单发送)
                {
                    if (applyOut.State != "0" || (applyOut.DrugNO != "0" && !string.IsNullOrEmpty(applyOut.DrugNO)))
                    {
                        this.neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.发送类型].Value = "已发送";
                    }
                }


                this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag = applyOut;

                //如果摆药单已作废或者不摆药，则用红色特殊显示此行
                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid || applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                {
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    //汇总信息
                    if (hsTotInfo.Contains(applyOut.Item.ID + applyOut.StockDept.ID))
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = hsTotInfo[applyOut.Item.ID + applyOut.StockDept.ID] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (applyOut.SystemType == "Z2" && isNurseStationApply)
                        {
                            //applyOutTot.Operation.ApplyQty -= applyOut.Operation.ApplyQty * applyOut.Days;
                            applyOutTot.Operation.ApplyQty -= applyOut.Operation.ApplyQty;
                        }
                        else
                        {
                            //applyOutTot.Operation.ApplyQty += applyOut.Operation.ApplyQty * applyOut.Days;
                            applyOutTot.Operation.ApplyQty += applyOut.Operation.ApplyQty;
                        }
                    }
                    else
                    {

                        FS.HISFC.Models.Pharmacy.ApplyOut applyOutTot = applyOut.Clone();
                        if (applyOut.SystemType == "Z2" && isNurseStationApply)
                        {
                            //applyOutTot.Operation.ApplyQty = -applyOut.Operation.ApplyQty * applyOut.Days;
                            applyOutTot.Operation.ApplyQty = -applyOut.Operation.ApplyQty;
                        }
                        hsTotInfo.Add(applyOutTot.Item.ID + applyOut.StockDept.ID, applyOutTot);
                        alTotInfo.Add(applyOutTot.Item.ID + applyOut.StockDept.ID);
                    }
                }
                #endregion
               
                rowIndex++;

            }

            //{CB5C628A-EA63-41e7-9D38-3F3DF2E78834}

            if(adrugmsg!="")
                MessageBox.Show("★药品【" + adrugmsg + "】属于A级高警示药品，注意核对药品名称、规格、用法、剂量、使用浓度及滴速等!", "提示", MessageBoxButtons.OK);
            if(bdrugmsg!="")
                MessageBox.Show("☆药品【" + bdrugmsg + "】属于B级高警示药品，注意核对药品名称、规格、用法、剂量等!", "提示", MessageBoxButtons.OK);
            if (ddrugmsg != "")
                MessageBox.Show("▲药品【" + ddrugmsg + "】属于易混淆药品，注意核对药品名称、规格等!", "提示", MessageBoxButtons.OK);


            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.剂数].Visible = isHaveHerbal;
            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.频次编码].Visible = isHaveHerbal;
            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.每剂量].Visible = isHaveHerbal;

            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.频次名称].Visible = !isHaveHerbal;
            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.每次用量].Visible = !isHaveHerbal;
            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.总量].Font = new System.Drawing.Font(this.Font.FontFamily, this.Font.Size, System.Drawing.FontStyle.Bold);

            Function.DrawCombo(this.neuDrugDetailSpread_Sheet1, (int)EnumDetailColSet.组合号, (int)EnumDetailColSet.组);

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.neuDrugDetailSpread_Sheet1)).EndInit();

            //显示汇总
            this.ShowTot();
        }
        
        /// <summary>
        /// 显示汇总
        /// </summary>
        private void ShowTot()
        {
            int rowIndex = 0;
            neuDrugTotSpread_Sheet1.RowCount = 0;
            neuDrugTotSpread_Sheet1.RowCount = alTotInfo.Count;
            foreach (string itemNO in alTotInfo)
            {
                if (hsTotInfo.Contains(itemNO))
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = hsTotInfo[itemNO] as FS.HISFC.Models.Pharmacy.ApplyOut;
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.药品编码].Value = applyOut.Item.ID;
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.编码].Value = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemCustomNO(applyOut.Item.ID);
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.药品名称].Value = applyOut.Item.Name;
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.规格].Value = applyOut.Item.Specs;
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.发药科室].Value = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(applyOut.StockDept.ID);

                    if (string.IsNullOrEmpty(applyOut.Item.PackUnit))
                    {
                        applyOut.Item.PackUnit = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(applyOut.Item.ID).PackUnit;
                    }

                    //数量显示处理
                    string applyPackQty = "";
                    string price = applyOut.Item.PriceCollection.RetailPrice.ToString() + "元" + "/" + applyOut.Item.PackUnit;

                    int applyQtyInt = 0;//这个取得商，是整包装单位的量，必须是整数
                    decimal applyRe = 0;//这个取得余数，是最小单位的量，可能是小数
                    applyQtyInt = (int)(Math.Abs(applyOut.Operation.ApplyQty) / applyOut.Item.PackQty);
                    applyRe = Math.Abs(applyOut.Operation.ApplyQty) - applyQtyInt * applyOut.Item.PackQty;
                    if (applyQtyInt != 0)
                    {
                       
                        applyPackQty += applyQtyInt.ToString() + applyOut.Item.PackUnit;
                    }
                    if (applyRe != 0)
                    {
                        applyPackQty += applyRe.ToString("F4").TrimEnd('0').TrimEnd('.') + applyOut.Item.MinUnit;
                    }

                    if (applyOut.Operation.ApplyQty < 0)
                    {
                        applyPackQty = "-" + applyPackQty;
                    }
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.总量].Value = applyPackQty;
                    this.neuDrugTotSpread_Sheet1.Cells[rowIndex, (int)EnumTotColSet.单价].Value = price;
                    rowIndex++;
                }
            }
        }

        /// <summary>
        /// 显示摆药单控件
        /// </summary>
        /// <param name="InpatientBills">单据接口：Control</param>
        public void ShowBill(List<FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill> listInpatientBill)
        {
            if (listInpatientBill == null)
            {
                return;
            }

            try
            {
                //汇总单是否可见
                bool tpTotBillVisible = false;
                //明细单是否可见
                bool tpDetailBillViseble = false;
                //处方单是否可见
                bool tpRecipeBillVisible = false;

                foreach (FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill InpatientBill in listInpatientBill)
                {
                    InpatientBill.WinDockStyle = DockStyle.Fill;
                    switch (InpatientBill.InpatientBillType)
                    {
                        case FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总:
                            this.tpTotBill.Controls.Add((Control)InpatientBill);
                            tpTotBillVisible = true;
                            break;
                        case FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细:
                            this.tpDetailBill.Controls.Add((Control)InpatientBill);
                            tpDetailBillViseble = true;
                            break;
                        case FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药:
                            this.tpRecipeBill.Controls.Add((Control)InpatientBill);
                            tpRecipeBillVisible = true;
                            break;
                        case FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方:
                            this.tpRecipeBill.Controls.Add((Control)InpatientBill);
                            tpRecipeBillVisible = true;
                            break;
                    }

                }
                SetTabPageVisible(false, true, true, tpTotBillVisible, tpDetailBillViseble, tpRecipeBillVisible);

            }
            catch (Exception ex)
            {
                MessageBox.Show("显示摆药单发生错误：" + ex.Message, "错误>>", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 提供可视单据打印
        /// </summary>
        /// <param name="billType">单据类型</param>
        public void PrintBill(FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType billType)
        {
            if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.标签)
            {
                return;
            }
            else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.药袋)
            {
                return;
            }
            else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.汇总)
            {
                if (!neuTabControl1.TabPages.Contains(tpTotBill))
                {
                    return;
                }

                ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill)tpTotBill.Controls[0]).Print();
            }
            else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.明细)
            {
                if (!neuTabControl1.TabPages.Contains(tpDetailBill))
                {
                    return;
                }

                ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill)tpDetailBill.Controls[0]).Print();
            }
            else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.出院带药处方)
            {
                if (!neuTabControl1.TabPages.Contains(tpRecipeBill))
                {
                    return;
                }

                ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill)tpRecipeBill.Controls[0]).Print();
            }
            else if (billType == FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.InpatientBillType.草药)
            {
                if (!neuTabControl1.TabPages.Contains(tpRecipeBill))
                {
                    return;
                }

                ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill)tpRecipeBill.Controls[0]).Print();
            }
            
        }

        /// <summary>
        /// 打印全部显示单据
        /// </summary>
        public void Print()
        {
            foreach (TabPage tp in neuTabControl1.TabPages)
            {
                if (tp.Controls.Count > 0 && tp.Controls[0] is FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill)
                {
                    ((FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IInpatientBill)tp.Controls[0]).Print();
                }
            }
        }

        /// <summary>
        /// 数据清除
        /// </summary>
        /// <param name="type">0清除摆药通知（汇总）信息 1清除摆药总量信息 2清除摆药明细数据 3汇总摆药单 4明细摆药单 5住院处方</param>
        public void Clear(int type)
        {
            if (type == 0)
            {
                this.neuDrugMessageSpread_Sheet1.RowCount = 0;
            }
            else if (type == 1)
            {
                this.neuDrugTotSpread_Sheet1.RowCount = 0;
            }
            else if (type == 2)
            {
                this.neuDrugDetailSpread_Sheet1.RowCount = 0;
            }
            else if (type == 3)
            {
                this.tpTotBill.Controls.Clear();
            }
            else if (type == 4)
            {
                this.tpDetailBill.Controls.Clear();
            }
            else if (type == 5)
            {
                this.tpRecipeBill.Controls.Clear();
            }
        }

        /// <summary>
        /// 数据全部清除
        /// </summary>
        public void Clear()
        {
            this.neuDrugMessageSpread_Sheet1.RowCount = 0;
            this.neuDrugTotSpread_Sheet1.RowCount = 0;
            this.neuDrugDetailSpread_Sheet1.RowCount = 0;

            this.tpTotBill.Controls.Clear();
            this.tpDetailBill.Controls.Clear();
            this.tpRecipeBill.Controls.Clear();
        }

        /// <summary>
        /// 设置tagpage的可见性
        /// </summary>
        /// <param name="tpDrugMessageVisible">汇总信息即摆药通知</param>
        /// <param name="tpTotDrugVisible">总量信息</param>
        /// <param name="tpDetailDrugVisible">明细信息</param>
        public void SetTabPageVisible(bool tpDrugMessageVisible, bool tpTotDrugVisible, bool tpDetailDrugVisible)
        {
            this.SetTabPageVisible(tpDrugMessageVisible, tpTotDrugVisible, tpDetailDrugVisible, false, false, false);
        }

        /// <summary>
        /// 设置tagpage的可见性
        /// </summary>
        /// <param name="tpDrugMessageVisible">汇总信息即摆药通知</param>
        /// <param name="tpTotDrugVisible">总量信息</param>
        /// <param name="tpDetailDrugVisible">明细信息</param>
        /// <param name="tpDetailBillViseble">明细摆药单</param>
        /// <param name="tpRecipeBillVisible">住院处方</param>
        /// <param name="tpTotBillVisible">汇总摆药单</param>
        public void SetTabPageVisible(bool tpDrugMessageVisible, bool tpTotDrugVisible, bool tpDetailDrugVisible, bool tpTotBillVisible, bool tpDetailBillViseble, bool tpRecipeBillVisible)
        {
            //汇总信息即摆药通知
            if (tpDrugMessageVisible && !neuTabControl1.TabPages.Contains(tpDrugMessage))
            {
                neuTabControl1.TabPages.Add(tpDrugMessage);
            }
            else if (!tpDrugMessageVisible && neuTabControl1.TabPages.Contains(tpDrugMessage))
            {
                neuTabControl1.TabPages.Remove(tpDrugMessage);
            }

            //总量信息
            if (tpTotDrugVisible && !neuTabControl1.TabPages.Contains(tpTotDrug))
            {
                neuTabControl1.TabPages.Add(tpTotDrug);
            }
            else if (!tpTotDrugVisible && neuTabControl1.TabPages.Contains(tpTotDrug))
            {
                neuTabControl1.TabPages.Remove(tpTotDrug);
            }

            //明细信息
            if (tpDetailDrugVisible && !neuTabControl1.TabPages.Contains(tpDetailDrug))
            {
                neuTabControl1.TabPages.Add(tpDetailDrug);
            }
            else if (!tpDetailDrugVisible && neuTabControl1.TabPages.Contains(tpDetailDrug))
            {
                neuTabControl1.TabPages.Remove(tpDetailDrug);
            }

            //汇总摆药单
            if (tpTotBillVisible && !neuTabControl1.TabPages.Contains(tpTotBill))
            {
                neuTabControl1.TabPages.Add(tpTotBill);
            }
            else if (!tpTotBillVisible && neuTabControl1.TabPages.Contains(tpTotBill))
            {
                neuTabControl1.TabPages.Remove(tpTotBill);
            }

            if (tpTotBillVisible)
            {
                neuTabControl1.SelectedTab = tpTotBill;
            }

            //明细摆药单
            if (tpDetailBillViseble && !neuTabControl1.TabPages.Contains(tpDetailBill))
            {
                neuTabControl1.TabPages.Add(tpDetailBill);
            }
            else if (!tpDetailBillViseble && neuTabControl1.TabPages.Contains(tpDetailBill))
            {
                neuTabControl1.TabPages.Remove(tpDetailBill);
            }

            if (tpDetailBillViseble)
            {
                neuTabControl1.SelectedTab = tpDetailBill;
            }

            //住院处方
            if (tpRecipeBillVisible && !neuTabControl1.TabPages.Contains(tpRecipeBill))
            {
                neuTabControl1.TabPages.Add(tpRecipeBill);
            }
            else if (!tpRecipeBillVisible && neuTabControl1.TabPages.Contains(tpRecipeBill))
            {
                neuTabControl1.TabPages.Remove(tpRecipeBill);
            }

            if (tpRecipeBillVisible)
            {
                neuTabControl1.SelectedTab = tpRecipeBill;
            }
        }

        /// <summary>
        /// 选中page
        /// </summary>
        /// <param name="index">索引0是总量 1汇总 2明细 3汇总摆药单 4明细摆药单 5住院处方</param>
        public void SelectTabPage(int index)
        {
            if (index == 0)
            {
                this.neuTabControl1.SelectedTab = this.tpDrugMessage;
            }
            else if (index == 1)
            {
                this.neuTabControl1.SelectedTab = this.tpTotDrug;
            }
            else if (index == 2)
            {
                this.neuTabControl1.SelectedTab = this.tpDetailDrug;
            }
            else if (index == 3)
            {
                this.neuTabControl1.SelectedTab = this.tpTotBill;
            }
            else if (index == 4)
            {
                this.neuTabControl1.SelectedTab = this.tpDetailBill;
            }
            else if (index == 5)
            {
                this.neuTabControl1.SelectedTab = this.tpRecipeBill;
            }
        }

        /// <summary>
        /// 选择数据
        /// </summary>
        /// <param name="begionUsetime">开始使用时间</param>
        /// <param name="endUsetime">结束使用时间</param>
        public void SelectData(DateTime begionUsetime, DateTime endUsetime)
        {
            bool checkValue = false;
            for (int rowIndex = 0; rowIndex < neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
            {
                checkValue = false;
                if (this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.ApplyOut)
                {

                    FS.HISFC.Models.Pharmacy.ApplyOut applyout =
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (applyout.UseTime < endUsetime && applyout.UseTime >= begionUsetime)
                    {
                        checkValue = true;
                    }
                }
                neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = checkValue;
            }
        }

        /// <summary>
        /// 选择数据
        /// </summary>
        /// <param name="checkValue">true 全选 false 全不选</param>
        public void SelectAllData(bool checkValue)
        {
            if (neuTabControl1.SelectedTab == tpDrugMessage)
            {
                for (int rowIndex = 0; rowIndex < neuDrugMessageSpread_Sheet1.RowCount; rowIndex++)
                {
                    neuDrugMessageSpread_Sheet1.Cells[rowIndex, 0].Value = checkValue;
                }
            }
            else if (neuTabControl1.SelectedTab == tpDetailDrug)
            {
                for (int rowIndex = 0; rowIndex < neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
                {
                    neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = checkValue;
                }
            }
        }

        /// <summary>
        /// 根据库存选择数据
        /// </summary>
        /// <param name="hsStock">key是药品编码 value是库存数量</param>
        public void SelectDataAsStock(Hashtable hsStock)
        {
            bool checkValue = false;
            for (int rowIndex = 0; rowIndex < neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
            {
                checkValue = false;
                if (this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.ApplyOut
                    && FS.FrameWork.Function.NConvert.ToBoolean(neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value))
                {

                    FS.HISFC.Models.Pharmacy.ApplyOut applyout =
                    this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    decimal stockQty = (decimal)hsStock[applyout.Item.ID];
                    if (stockQty >= applyout.Operation.ApplyQty)
                    {
                        checkValue = true;
                    }
                    stockQty = stockQty - applyout.Operation.ApplyQty;
                    hsStock[applyout.Item.ID] = stockQty;
                }
                neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value = checkValue;
                if (!checkValue)
                {
                    neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].BackColor = Color.Red;
                }
            }
        }

        /// <summary>
        /// 获取选择的数据
        /// </summary>
        /// <returns>实体有两种：DrugMessage、ApplyOut 但不可能是两种的混合</returns>
        public ArrayList GetSelectData()
        {
            ArrayList al = new ArrayList();
            if (this.neuTabControl1.Contains(this.tpDetailDrug))
            {
                for (int rowIndex = 0; rowIndex < this.neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value))
                    {
                        if (this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.ApplyOut)
                        {
                            al.Add(this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                    }
                }
            }
            else if (this.neuTabControl1.Contains(this.tpDrugMessage))
            {
                for (int rowIndex = 0; rowIndex < this.neuDrugMessageSpread_Sheet1.RowCount; rowIndex++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(neuDrugMessageSpread_Sheet1.Cells[rowIndex, 0].Value))
                    {
                        if (this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                        {
                            al.Add(this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                        else if (this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                        {
                            al.Add(this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                    }
                }
            }

            return al;
        }

        /// <summary>
        /// 获取选择的数据
        /// {3D84EF54-CE7A-40b1-8417-CF8FBED4A70D}
        /// </summary>
        /// <returns>实体有两种：DrugMessage、ApplyOut 但不可能是两种的混合</returns>
        public ArrayList GetSelectData(bool isSelectAll)
        {
            ArrayList al = new ArrayList();
            if (this.neuTabControl1.Contains(this.tpDetailDrug))
            {
                for (int rowIndex = 0; rowIndex < this.neuDrugDetailSpread_Sheet1.RowCount; rowIndex++)
                {
                    if (isSelectAll || FS.FrameWork.Function.NConvert.ToBoolean(neuDrugDetailSpread_Sheet1.Cells[rowIndex, (int)EnumDetailColSet.选中].Value))
                    {
                        if (this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.ApplyOut)
                        {
                            al.Add(this.neuDrugDetailSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                    }
                }
            }
            else if (this.neuTabControl1.Contains(this.tpDrugMessage))
            {
                for (int rowIndex = 0; rowIndex < this.neuDrugMessageSpread_Sheet1.RowCount; rowIndex++)
                {
                    if (isSelectAll || FS.FrameWork.Function.NConvert.ToBoolean(neuDrugMessageSpread_Sheet1.Cells[rowIndex, 0].Value))
                    {
                        if (this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.DrugMessage)
                        {
                            al.Add(this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                        else if (this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                        {
                            al.Add(this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                    }
                }
            }

            return al;
        }


        /// <summary>
        /// 获取选择的数据
        /// </summary>
        /// <returns>实体DrugBillClass</returns>
        public ArrayList GetSelectDrugBill()
        {
            ArrayList al = new ArrayList();
            if (this.neuTabControl1.Contains(this.tpDrugMessage))
            {
                for (int rowIndex = 0; rowIndex < this.neuDrugMessageSpread_Sheet1.RowCount; rowIndex++)
                {
                    if (FS.FrameWork.Function.NConvert.ToBoolean(neuDrugMessageSpread_Sheet1.Cells[rowIndex, 0].Value))
                    {
                        if (this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag is FS.HISFC.Models.Pharmacy.DrugBillClass)
                        {
                            al.Add(this.neuDrugMessageSpread_Sheet1.Rows[rowIndex].Tag);
                        }
                    }
                }
            }

            return al;
        }

        /// <summary>
        /// 格式化
        /// </summary>
        private void SetFormat()
        {
            this.totColSet.AddColumn(
                //列名称，宽度，可见性，锁定，tag值
               new FS.SOC.Public.FarPoint.Column[]{
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.选中.ToString(), 30f, false, true, null),//暂时不开发部分发药的功能
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.药品编码.ToString(), 60f, false, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.编码.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.药品名称.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.规格.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.总量.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.单价.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.摆药量.ToString(), 90f, false, false, null),//暂时不开发部分发药的功能
                new FS.SOC.Public.FarPoint.Column(EnumTotColSet.发药科室.ToString(), 90f, true, false, null),
                }
               );

            this.neuDrugTotSpread_Sheet1.Columns.Count = this.totColSet.Count;
            this.neuDrugTotSpread_Sheet1.RowCount = 0;

            this.neuDrugTotSpread_Sheet1.ColumnHeader.Rows[0].Height = 36F;
            this.neuDrugTotSpread_Sheet1.ColumnHeader.DefaultStyle.Font = new Font("宋体", 9f, FontStyle.Bold);

            if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedTotSetting.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuDrugTotSpread_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedTotSetting.xml");
                for (int colIndex = 0; colIndex < this.neuDrugTotSpread_Sheet1.Columns.Count; colIndex++)
                {
                    this.neuDrugTotSpread_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    if (this.neuDrugTotSpread_Sheet1.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = (FarPoint.Win.Spread.CellType.TextCellType)this.neuDrugTotSpread_Sheet1.Columns[colIndex].CellType;
                        t.ReadOnly = true;
                    }
                    else if (this.neuDrugTotSpread_Sheet1.Columns[colIndex].CellType == null)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                        t.ReadOnly = true;
                        this.neuDrugTotSpread_Sheet1.Columns[colIndex].CellType = t;
                    }
                }
            }
            else
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                for (int colIndex = 0; colIndex < this.neuDrugTotSpread_Sheet1.Columns.Count; colIndex++)
                {
                    this.neuDrugTotSpread_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                    FS.SOC.Public.FarPoint.Column c = this.totColSet.GetColumn(colIndex);
                    this.neuDrugTotSpread_Sheet1.Columns[colIndex].Label = c.Name;
                    this.neuDrugTotSpread_Sheet1.Columns[colIndex].Width = c.Width;
                    this.neuDrugTotSpread_Sheet1.Columns[colIndex].Visible = c.Visible;
                    this.neuDrugTotSpread_Sheet1.Columns[colIndex].Locked = c.Locked;
                    if (colIndex != (int)EnumTotColSet.选中)
                    {
                        this.neuDrugTotSpread_Sheet1.Columns[colIndex].CellType = t;
                    }
                }
                //若不存在则新建
                string filePath =  FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedTotSetting.xml";
                try
                {
                    System.IO.File.Create(filePath);
                }
                catch { }
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuDrugTotSpread_Sheet1, filePath);
            }

            FarPoint.Win.Spread.CellType.CheckBoxCellType checkBoxCellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuDrugTotSpread_Sheet1.Columns[(int)EnumTotColSet.选中].CellType = checkBoxCellType;

            this.neuDrugTotSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDrugTotSpread_ColumnWidthChanged);
            this.neuDrugTotSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDrugTotSpread_ColumnWidthChanged);

            this.detailColSet.AddColumn(
                //列名称，宽度，可见性，锁定，tag值
               new FS.SOC.Public.FarPoint.Column[]{
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.床号.ToString(), 30f, true, true, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.姓名.ToString(), 60f, true, true, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.选中.ToString(), 34f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.编码.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.药品名称.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.规格.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.组.ToString(), 20f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.用法.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.每剂量.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.频次编码.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.频次名称.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.每次用量.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.滴速.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.剂数.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.总量.ToString(), 60f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.货位号.ToString(),75f,true,false,null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.用药时间.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.单价.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.申请时间.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.发药人.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.发药时间.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.医嘱类型.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.组合号.ToString(), 120f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.医嘱流水号.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.发药科室.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.摆药单.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.申请科室.ToString(), 90f, true, false, null),
                new FS.SOC .Public .FarPoint .Column (EnumDetailColSet .开立科室.ToString(),90f,true,false,null),
                new FS.SOC .Public .FarPoint .Column (EnumDetailColSet .开立医生.ToString(),90f,true,false,null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.发送类型.ToString(), 90f, true, false, null),
                new FS.SOC.Public.FarPoint.Column(EnumDetailColSet.备注.ToString(), 90f, true, false, null)
                
                }
               );

            this.neuDrugDetailSpread_Sheet1.Columns.Count = this.detailColSet.Count;
            this.neuDrugDetailSpread_Sheet1.RowCount = 0;

            this.neuDrugDetailSpread_Sheet1.ColumnHeader.Rows[0].Height = 36F;
            this.neuDrugDetailSpread_Sheet1.ColumnHeader.DefaultStyle.Font = new Font("宋体", 9f, FontStyle.Bold);

            if (System.IO.File.Exists(FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedDetSetting.xml"))
            {
                FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuDrugDetailSpread_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedDetSetting.xml");
                for (int colIndex = 0; colIndex < this.neuDrugDetailSpread_Sheet1.Columns.Count; colIndex++)
                {
                    this.neuDrugDetailSpread_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();
                    if (this.neuDrugDetailSpread_Sheet1.Columns[colIndex].CellType is FarPoint.Win.Spread.CellType.TextCellType)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = (FarPoint.Win.Spread.CellType.TextCellType)this.neuDrugDetailSpread_Sheet1.Columns[colIndex].CellType;
                        t.ReadOnly = true;
                    }
                    else if (this.neuDrugDetailSpread_Sheet1.Columns[colIndex].CellType == null)
                    {
                        FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                        t.ReadOnly = true;
                        this.neuDrugDetailSpread_Sheet1.Columns[colIndex].CellType = t;
                    }
                }
            }
            else
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.ReadOnly = true;
                for (int colIndex = 0; colIndex < this.neuDrugDetailSpread_Sheet1.Columns.Count; colIndex++)
                {
                    this.neuDrugDetailSpread_Sheet1.ColumnHeader.Cells[0, colIndex].CellType = new FarPoint.Win.Spread.CellType.TextCellType();

                    FS.SOC.Public.FarPoint.Column c = this.detailColSet.GetColumn(colIndex);
                    this.neuDrugDetailSpread_Sheet1.Columns[colIndex].Label = c.Name;
                    this.neuDrugDetailSpread_Sheet1.Columns[colIndex].Width = c.Width;
                    this.neuDrugDetailSpread_Sheet1.Columns[colIndex].Visible = c.Visible;
                    this.neuDrugDetailSpread_Sheet1.Columns[colIndex].Locked = c.Locked;
                    if (colIndex != (int)EnumDetailColSet.选中)
                    {
                        this.neuDrugDetailSpread_Sheet1.Columns[colIndex].CellType = t;
                    }
                }
                //若不存在则新建
                string filePath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedDetSetting.xml";
                //try
                //{
                //    System.IO.FileStream fs = System.IO.File.Create(filePath);
                //    fs.Close();
                //}
                //catch { }
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuDrugDetailSpread_Sheet1, filePath);
            }

            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.选中].CellType = checkBoxCellType;
            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.床号].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;
            this.neuDrugDetailSpread_Sheet1.Columns[(int)EnumDetailColSet.姓名].MergePolicy = FarPoint.Win.Spread.Model.MergePolicy.Always;

            this.neuDrugDetailSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDrugDetailSpread_ColumnWidthChanged);
            this.neuDrugDetailSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuDrugDetailSpread_ColumnWidthChanged);

        }

        public void Export()
        {
            if (this.neuTabControl1.SelectedTab.Controls.Count> 0 && this.neuTabControl1.SelectedTab.Controls[0] is FarPoint.Win.Spread.FpSpread)
            {
                try
                {
                    string fileName = "";
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.DefaultExt = ".xls";
                    dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                    DialogResult result = dlg.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        fileName = dlg.FileName;
                        (this.neuTabControl1.SelectedTab.Controls[0] as FarPoint.Win.Spread.FpSpread).SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("导出数据发生错误>>" + ex.Message);
                }
            }
        }

        void neuDrugDetailSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuDrugDetailSpread_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedDetSetting.xml");
        }

        void neuDrugTotSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuDrugTotSpread_Sheet1, FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\FPInpatientDrugedTotSetting.xml");
            
        }
    }
}

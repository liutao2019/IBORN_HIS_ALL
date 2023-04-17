using System;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;

namespace FS.SOC.Local.Order.OrderPrint.Iboren
{
    /// <summary>
    /// 麻、精一处方
    /// </summary>
    public partial class ucRecipePrintST : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrintST
    {
        //{C42F14B0-81D2-4eae-B708-6431EA819622}
        public ucRecipePrintST()
        {
            InitializeComponent();
            this.Height = 800;
            this.fpSpreadItemsSheet.Rows.Default.Height = 30;
        }

        #region 变量

        private string err = "";

        public string ErrInfo
        {
            get { return err; }
            set { err = value; }
        }

        /// <summary>
        /// 特殊处方标记
        /// </summary>
        string speRecipeLabel = "";

        ///// <summary>
        ///// 医嘱排序
        ///// </summary>
        //OrderCompare orderCompare = new OrderCompare();

        /// <summary>
        /// 打印处方项目名称是否是通用名：1 通用名；0 商品名
        /// </summary>
        private int printItemNameType = -1;

        private Hashtable hsItem = new Hashtable();

        /// <summary>
        /// 患者挂号信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo;
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();

        #endregion

        /// <summary>
        /// 查询医嘱
        /// </summary>
        private void Query()
        {
            //从处方表获取
            ArrayList alOrder = new ArrayList();//待补充//this.orderManager.QueryOrderByRecipeNO(this.myPatientInfo.ID, this.recipeNO);

            //没有的话从费用表获取
            if (alOrder == null)
            {
                MessageBox.Show("查询处方信息出错！\r\n" + manager.Err, "错误", MessageBoxButtons.OK);
                return;
            }
            else if (alOrder.Count == 0)
            {
                return;
            }

            this.MakaLabel(alOrder);
        }

        enum EnumCol
        {
            ZH组号,
            ZBJ组标记,
            MC名称,
            //YF用法,
            ZHH组合号
        }

        private string GetName(FS.HISFC.Models.Order.OrderST order,ref decimal cost)
        {
            string itemName = "";
            if (printItemNameType == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                printItemNameType = controlMgr.GetControlParam<int>("HNMZ11", true, 1);
            }


            if (order.Item_code == "999" && !itemName.Contains("自备"))
            {
                itemName = order.Item_name + "[自备]";
            }
            else
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item_code);

                if (printItemNameType == 0)
                {
                    itemName = order.Item_name;
                }
                else
                {
                    if (string.IsNullOrEmpty(phaItem.NameCollection.RegularName))
                    {
                        itemName = phaItem.Name;
                    }
                    else
                    {
                        itemName = phaItem.NameCollection.RegularName;
                    }
                }

                if (!hsItem.ContainsKey(phaItem.ID))
                {
                    hsItem.Add(phaItem.ID, phaItem);
                }
                //{
                //    itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "×" + order.Qty.ToString() + order.Unit;
                //}
                //上面那段怎么看怎么不对
                //itemName += "  " + order.Once_dose.ToString() + order.Dose_unit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "×" + order.Days.ToString() + order.Dose_unit;
                //{FE3599DC-8D27-43f1-A847-B65FC6C6BF3B}
                //itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "   " + "×" + (order.Dose_unit == phaItem.DoseUnit ? ((int)Math.Ceiling(order.Once_dose / phaItem.BaseDose)).ToString() : order.Once_dose.ToString()) + phaItem.MinUnit;
                itemName += "  " + phaItem.BaseDose.ToString() + phaItem.DoseUnit + "*" + phaItem.PackQty.ToString() + phaItem.MinUnit + "/" + phaItem.PackUnit + "   " + "×" + order.Memo;
                if (order.Dose_unit == phaItem.DoseUnit)
                {
                    cost += (phaItem.Price / phaItem.PackQty) * ((int)Math.Ceiling(order.Once_dose / phaItem.BaseDose));
                }
                else
                {
                    cost += (phaItem.Price / phaItem.PackQty) * order.Once_dose;
                }
            }

            return itemName;
        }

        private void AddUsageShow(FS.HISFC.Models.Order.OrderST order, int row, bool isComb)
        {
            //用法+每次量+频次+备注
            string show = "";
            //{320B0B6A-D941-4258-958A-E38C3A4644EA}
            //string strDoseOnce = "每次" + order.Once_dose.ToString() + order.Dose_unit + "  ";
            string strDoseOnce = "";
            string freName = order.Fre_name;
            FS.HISFC.Models.Pharmacy.Item phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item_code);
            //{C356C087-CACE-4b52-A999-51385D48DE21}  每次剂量改为 满最小单位并且整数时，显示最小单位整数，其他情况显示剂量单位
            //if ((order.Dose_unit == phaItem.DoseUnit && (order.Once_dose / phaItem.BaseDose) - ((int)Math.Ceiling(order.Once_dose / phaItem.BaseDose)) == 0)
            //        || (order.Dose_unit != phaItem.DoseUnit && (order.Once_dose - (int)order.Once_dose) != 0))
            //{
            //    strDoseOnce += "每次" + (order.Once_dose / phaItem.BaseDose).ToString() + phaItem.MinUnit + "  ";
            //}

            if ((order.Dose_unit == phaItem.DoseUnit && (order.Once_dose / phaItem.BaseDose) - ((int)Math.Ceiling(order.Once_dose / phaItem.BaseDose)) != 0)
                    || (order.Dose_unit != phaItem.DoseUnit && (order.Once_dose - (int)order.Once_dose) != 0))
            {
                strDoseOnce += "每次" + order.Once_dose.ToString() + phaItem.DoseUnit;
            }
            else
            {
                //{85A27808-D1A6-4183-9EE1-0CB9773EF6B4}
                //处方笺打印时，药品用法的每次用量，每次用量选择以毫克为单位

                //strDoseOnce += "每次" + (order.Dose_unit == phaItem.DoseUnit ? ((int)Math.Ceiling(order.Once_dose / phaItem.BaseDose)).ToString() : order.Once_dose.ToString()) + phaItem.MinUnit;
                strDoseOnce += "每次" + order.Once_dose.ToString() + phaItem.DoseUnit;
            }


            //show = "用法：" + strDoseOnce + order.Usage_name + "  " + freName + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");
            //{FE3599DC-8D27-43f1-A847-B65FC6C6BF3B}
            //show = "用法：" + order.Usage_name + "，" + freName + "，" + strDoseOnce + (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");
            show = "用法：" + order.Usage_name + "，" + freName + "，" + strDoseOnce;//+ (string.IsNullOrEmpty(order.Memo) ? "" : "  (" + order.Memo + "）");

            //if (FS.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage_code))
            //{
            //    show += " 余液弃去";
            //}
            fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = show;
        }

        /// <summary>
        /// 生成显示信息
        /// </summary>
        /// <param name="alOrder"></param>
        public void MakaLabel(ArrayList alOrder)
        {
            if (alOrder == null)
            {
                return;
            }

            //按照sortID排序
            //alOrder.Sort(orderCompare);
            this.fpSpreadItemsSheet.RowCount = 0;

            Dictionary<string, int> dicCombNo = new Dictionary<string, int>();
            FS.HISFC.Models.Order.OrderST preOrder = null;

            decimal cost = 0;
            FarPoint.Win.Spread.CellType.TextCellType text = new FarPoint.Win.Spread.CellType.TextCellType();
            text.Multiline = true;
            text.WordWrap = true;
            for (int index = 0; index < alOrder.Count; index++)
            {
                //FS.HISFC.Models.Order.Inpatient.Order order = alOrder[index] as FS.HISFC.Models.Order.Inpatient.Order;
                FS.HISFC.Models.Order.OrderST order = alOrder[index] as FS.HISFC.Models.Order.OrderST;
                int row = fpSpreadItemsSheet.RowCount;
                fpSpreadItemsSheet.Rows.Add(row, 1);
                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].ColumnSpan = 2;
                this.label17.Text = order.Ext_memo1;
                if (!dicCombNo.ContainsKey(order.Comb_no))
                {
                    dicCombNo.Add(order.Comb_no, 1);
                    if (preOrder != null)
                    {
                        AddUsageShow(preOrder, row, dicCombNo[preOrder.Comb_no] > 1);

                        row = fpSpreadItemsSheet.RowCount;
                        fpSpreadItemsSheet.Rows.Add(row, 1);
                        //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].ColumnSpan = 2;
                    }
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZH组号].Text = order.SubCombNO.ToString() + ".";
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZH组号].Text = (dicCombNo.Keys.Count).ToString() + ".";
                }

                //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZBJ组标记].Text = "";
                //decimal ordercost = 0.0m;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].CellType = text;
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = GetName(order,ref cost);
                fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.ZHH组合号].Text = order.Comb_no;

                fpSpreadItemsSheet.Rows[row].Tag = order;

                //FS.HISFC.Models.Pharmacy.Item item1 = hsItem[order.Item_code] as FS.HISFC.Models.Pharmacy.Item;
                //cost += item1.Price;//order.FT.OwnCost + order.FT.PayCost + order.FT.PubCost + order.FT.RebateCost;
                preOrder = order;

                if (index == alOrder.Count - 1)
                {
                    row = fpSpreadItemsSheet.RowCount;
                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    AddUsageShow(order, row, dicCombNo[preOrder.Comb_no] > 1);
                }
            }

            Function.DrawComboLeft(fpSpreadItemsSheet, (Int32)EnumCol.ZHH组合号, (Int32)EnumCol.ZBJ组标记);
            for (int row = 0; row < fpSpreadItemsSheet.RowCount; row++)
            {
                if (fpSpreadItemsSheet.Rows[row].Tag != null)
                {
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                else
                {
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
            }
            if (fpSpreadItemsSheet.RowCount < 11)
            {
                  int speLevl = 1;
                  FS.HISFC.Models.Order.OrderST order = alOrder[0] as FS.HISFC.Models.Order.OrderST;
                FS.HISFC.Models.Order.Inpatient.Order order1 = new FS.HISFC.Models.Order.Inpatient.Order();
                order1.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item_code);
                int level = Function.GetItemQaulity(order1);

                //重点品种精二或全麻
                FS.HISFC.Models.Order.OrderST orderNew = alOrder[0] as FS.HISFC.Models.Order.OrderST;
                FS.HISFC.Models.Order.Inpatient.Order orderNew1 = new FS.HISFC.Models.Order.Inpatient.Order();

                FS.HISFC.Models.Pharmacy.Item itemNew = new FS.HISFC.Models.Pharmacy.Item();
                if (orderNew != null)
                {
                    //itemNew = orderNew.Item as FS.HISFC.Models.Pharmacy.Item;
                    FS.HISFC.BizLogic.Pharmacy.Item item = new FS.HISFC.BizLogic.Pharmacy.Item();
                    itemNew = item.GetItem(orderNew.Item_code);
                    if (itemNew.SpecialFlag4 == null)
                        itemNew.SpecialFlag4 = "-";
                }

                if (level > speLevl)
                {
                    speLevl = level;
                }
                if (speLevl == 3)
                {
                    //this.label9.Visible = true;
                    this.label11.Visible = true;
                    //this.label21.Visible = true;
                    this.label17.Visible = false;
                    this.label1.Visible = true;
                    this.labelAgentName.Visible = true;
                    this.label18.Visible = true;
                    this.labelAgentIdenNO.Visible = true;
                    this.lbIdenNO.Visible = true;
                    this.lbIdenNOM.Visible = true;

                    //int row = fpSpreadItemsSheet.RowCount;

                    this.lblDoc.Visible = false;
                    this.lblDoc.Height = 0;
                    this.panel4.Height = 119;

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "（以下空白）";
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                   // fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "余药量_______丢弃,确认人_________/_________";
                   // fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("楷体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    int row = fpSpreadItemsSheet.RowCount;
                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }
                else if (speLevl == 4)
                {
                    this.lblDoc.Visible = false;
                    this.lblDoc.Height = 0;
                    this.panel4.Height = 119;

                    //this.label9.Visible = true;
                    this.label11.Visible = true;
                   // this.label21.Visible = true;
                    this.label17.Visible = false;
                    this.label1.Visible = true;
                    this.labelAgentName.Visible = true;
                    this.label18.Visible = true;
                    this.labelAgentIdenNO.Visible = true;
                    this.lbIdenNO.Visible = true;
                    this.lbIdenNOM.Visible = true;

                    //int row = fpSpreadItemsSheet.RowCount;

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "（以下空白）";
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "余药量_______丢弃,确认人_________/_________";
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("楷体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    int row = fpSpreadItemsSheet.RowCount;
                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }//重点品种精二和全麻
                else if ((itemNew.SpecialFlag4.ToString() == "13" && itemNew.Quality.ID.ToString() == "Q") || (itemNew.SpecialFlag4.ToString() == "13" && itemNew.Quality.ID.ToString() == "P2"))
                {
                    //this.label9.Visible = true;
                    this.label11.Visible = true;
                    //this.label21.Visible = true;
                    this.label17.Visible = false;
                    this.label1.Visible = true;
                    this.labelAgentName.Visible = true;
                    this.label18.Visible = true;
                    this.labelAgentIdenNO.Visible = true;
                    this.lbIdenNO.Visible = true;
                    this.lbIdenNOM.Visible = true;

                    //int row = fpSpreadItemsSheet.RowCount;

                    this.lblDoc.Visible = false;
                    this.lblDoc.Height = 0;
                    this.panel4.Height = 119;

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "（以下空白）";
                    //fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                    //fpSpreadItemsSheet.Rows.Add(row, 1);
                    // fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "余药量_______丢弃,确认人_________/_________";
                    // fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("楷体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    int row = fpSpreadItemsSheet.RowCount;
                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                }

                else
                {
                    //this.label9.Visible = false;
                    this.label11.Visible = false;
                    //this.label21.Visible = false;
                    this.label17.Visible = false;
                    this.label1.Visible = false;
                    this.labelAgentName.Visible = false;
                    this.label18.Visible = false;
                    this.labelAgentIdenNO.Visible = false;
                    this.lbIdenNO.Visible = false;
                    this.lbIdenNOM.Visible = false;

                    this.panel4.Visible = false;
                    this.panel4.Height = 0;
                    this.lblDoc.Height = 70;


                    int row = fpSpreadItemsSheet.RowCount;

                    fpSpreadItemsSheet.Rows.Add(row, 1);
                    fpSpreadItemsSheet.Cells[row, (Int32)EnumCol.MC名称].Text = "(以下为空)";
                    fpSpreadItemsSheet.Rows[row].Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));


                }
            }

            this.lblPhaMoney.Text = FS.FrameWork.Public.String.ToSimpleString(cost, 2) + "元";

            FS.HISFC.Models.Order.OrderST firstOrder = alOrder[0] as FS.HISFC.Models.Order.OrderST;
            this.lblSeeDept.Text = firstOrder.Recipe_dept_name;
            FS.HISFC.Models.Base.Employee em = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            FS.HISFC.Models.Base.Department dp = em.Dept as FS.HISFC.Models.Base.Department;
            string hospitalID = dp.HospitalID;
            if (hospitalID != "IBORN")
            {
                hospitalID = "IBORN";
            }
            //if (hospitalID != "IBORN")
            //{
            //    lblphone.Visible = false;
            //}
            FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
            FS.FrameWork.Models.NeuObject hos = constantMgr.GetConstant("HOSPITALLIST", hospitalID);
            labelTitle.Text = hos.Name;
            string[] arr = hos.Memo.Split(';');
            // firstOrder.Recipe_dept_code
            //lblPhaDoc.Text = firstOrder.Recipe_dept_code.Substring(2, firstOrder.Recipe_dept_code.Length - 2);//不是医师么？
            lblPhaDoc.Text = firstOrder.Recipe_doc_code.Substring(2);//firstOrder.Recipe_doc_code.TrimStart('0').Substring(firstOrder.Recipe_doc_code.TrimStart('0').Length - 2).PadLeft(firstOrder.Recipe_doc_code.TrimStart('0').Length, '*');
            this.npbRecipeNo.Image = SOC.Public.Function.CreateBarCode(firstOrder.Recipe_no, this.npbRecipeNo.Width, this.npbRecipeNo.Height);
           this.labelSeeDate.Text = firstOrder.Ext_memo2;
           this.lblMPhaMoney.Text = FS.FrameWork.Public.String.ToSimpleString(cost, 2) + "元";
           lblPhaMDoc.Text = firstOrder.Recipe_doc_code.Substring(2);
           lblMPrintDate.Text = deptMgr.GetDateTimeFromSysDateTime().ToString(); 
            this.labelAuthorityDoc.Text = "          ";
            this.lblDrugDoct.Text = "          ";
            this.lblSendDoct.Text = "          ";

            SetSpeInfo(alOrder);
        }

        /// <summary>
        /// 打印接口
        /// </summary>
        /// <param name="regObj">挂号实体</param>
        /// <param name="reciptDept">处方开立科室（接口要求，暂不用）</param>
        /// <param name="reciptDoct">处方开立医生（接口要求，暂不用）</param>
        /// <param name="orderList">处方List</param>
        /// <param name="orderSelectList">选中的List（接口要求，暂不用）</param>
        /// <param name="IsReprint">是否补打（接口要求，暂不用）</param>
        /// <param name="IsPreview">是否预览</param>
        /// <param name="printType">打印类型（接口要求，暂不用）</param>
        /// <param name="obj">预留字段（接口要求，暂不用）</param>
        /// <returns></returns>
        public int OnInPatientPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept,
            FS.FrameWork.Models.NeuObject reciptDoct, ArrayList orderPrintList, ArrayList orderSelectList, bool IsReprint, bool IsPreview, string printType, object obj)
        {
            ArrayList alOrderST = this.ChangeOrderToOrderST(orderPrintList);
            this.MakaLabel(alOrderST);
            this.SetPatientInfo(patientInfo);
            if (IsPreview)
            {
                this.PrintRecipeView(alOrderST);
            }
            else
            {
                this.PrintRecipe();
            }
            return 1;
        }

        public int PrintRecipe()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

            print.SetPageSize(Function.GetPrintPage(false));
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.PrintDocument.DefaultPageSettings.Landscape = false;

            if (!string.IsNullOrEmpty(printer))
            {
                print.PrintDocument.DefaultPageSettings.PrinterSettings.PrinterName = this.printer;
            }
            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
            {
                print.PrintPage(5, 5, this);
            }
            else
            {
                print.PrintPage(5, 5, this);
            }
            return 1;
        }

        /// <summary>
        /// 当前处方号
        /// </summary>
        private string recipeNO = "";

        /// <summary>
        /// 当前处方号
        /// </summary>
        public string RecipeNO
        {
            get
            {
                return this.recipeNO;
            }
            set
            {
                this.recipeNO = value;
                this.speRecipeLabel = "";
                this.Query();
            }
        }

        /// <summary>
        /// 设置患者基本信息
        /// </summary>
        /// <param name="register"></param>
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo register)
        {
            if (register == null)
            {
                return ;
            }

            FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo p = new FS.HISFC.Models.RADT.PatientInfo();
            p = inpatientManager.GetPatientInfoByPatientNO(register.PID.ID);

            this.lblName.Text = register.Name;
            this.lblName1.Text = register.Name;
            int strLength = this.LengthString(register.Name);
            if (strLength > 16)
            {
                this.lblName.Visible = true;
                this.lblName1.Visible = false;
            }
            else
            {

                this.lblName.Visible = false;
                this.lblName1.Visible = true;
            }
            #region 麻精一显示代办人

            lbIdenNO.Text = register.IDCard;

            FS.HISFC.Models.RADT.PatientInfo tmpInfo = register.Clone();
            if (!string.IsNullOrEmpty(register.PID.CardNO))
            {
                FS.HISFC.BizProcess.Integrate.RADT radtMgr = new FS.HISFC.BizProcess.Integrate.RADT();
                tmpInfo = radtMgr.QueryComPatientInfo(register.PID.CardNO);
            }
            if (tmpInfo != null)
            {
                labelAgentName.Text = tmpInfo.Kin.Name;//代办人
                labelAgentIdenNO.Text = tmpInfo.Kin.Memo;//代办人身份证
            }
            #endregion
            //{C297AE62-B883-4b44-AA44-1782D2FE9474}
            FS.HISFC.BizLogic.HealthRecord.Diagnose diagManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            this.labelAge.Text = diagManager.GetAge(register.Birthday, false);//待处理
            this.labelGender.Text = register.Sex.Name;
            this.lblCardNo.Text = register.PID.CardNO.TrimStart('0') +"/" + register.PID.PatientNO.TrimStart('0');
             lblPrintDate.Text = deptMgr.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd HH:mm");

            //this.chkOwn.Checked = false;
            //this.chkPay.Checked = false;
            //this.chkPub.Checked = false;
            //this.chkOth.Checked = false;

            //if (register.Pact.PayKind.ID == "01")
            //{
            //    this.chkOwn.Checked = true;
            //}
            //else if (register.Pact.PayKind.ID == "02")
            //{
            //    this.chkPay.Checked = true;
            //}
            //else
            //{
            //    this.chkPub.Checked = true;
            //}

            //this.lblTel.Text = register.AddressHome + (!string.IsNullOrEmpty(register.PhoneHome) && !string.IsNullOrEmpty(register.AddressHome) ? "/" : "") + register.PhoneHome;
            //this.lblTel.Text = register.PhoneHome +"/" + (!string.IsNullOrEmpty(p.User01) ? p.User01 : (!string.IsNullOrEmpty(p.AddressHome) ? p.AddressHome : register.AddressHome));
             this.lblTel.Text = register.PhoneHome + "/" + (!string.IsNullOrEmpty(p.AddressHome) ? p.AddressHome : register.AddressHome);
             // {F417D766-19C0-4d3e-AB72-D774058B497E}
            #region 就诊日期

            //DateTime dtNow = deptMgr.GetDateTimeFromSysDateTime();
            DateTime dtNow = this.deptMgr.GetDateTimeFromSysDateTime();

            //if (register.SeeDoct.OperTime < new DateTime(2000, 1, 1))
            //{
            //this.labelSeeDate.Text = dtNow.ToString("yyyy.MM.dd");//待处理
            this.labelSeeDate.Text = dtNow.ToString();//待处理
            //}
            //else
            //{
            //    this.labelSeeDate.Text = register.SeeDoct.OperTime.ToString("yyyy.MM.dd");
            //}

            #endregion 就诊日期

            #region 诊断 //待处理
            //{1BBD2F14-49A6-468b-BB08-19BF0499CEF4}
            ArrayList al = diagManager.QueryCaseDiagnoseFromEmrView(register.ID);
            if (al.Count > 0)
            {
                FS.HISFC.Models.HealthRecord.Diagnose DiagnoseTemp = al[0] as FS.HISFC.Models.HealthRecord.Diagnose;
                lblDiag.Text = DiagnoseTemp.DiagInfo.ICD10.Name;
            }
                //ArrayList al = this.diagManager.QueryCaseDiagnoseForClinic(register.ID, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes.DOC);
            //if (al == null)
            //{
            //    MessageBox.Show("查询诊断信息错误！\r\n" + diagManager.Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return -1;
            //}

            //string strDiag = "";
            //foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            //{
            //    if (diag != null && diag.IsValid)
            //    {
            //        strDiag += diag.DiagInfo.ICD10.Name + "、";
            //    }
            //}
            //lblDiag.Text = strDiag.TrimEnd('、');

            #endregion

            //this.myReg = register;
            FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            accountCard = accountManager.GetAccountCardForOne(register.PID.CardNO);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

            if (accountCard != null)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = managerIntegrate.GetConstansObj("MemCardType", accountCard.AccountLevel.ID);

                this.lblAccountType.Text = obj.Name;
            }
            else
            {
                this.lblAccountType.Text = "";
            }
            return ;
        }

        public int LengthString(string str)
        {
            if (str == null || str.Length == 0) { return 0; }

            int l = str.Length;
            int realLen = l;

            #region 计算长度
            int clen = 0;//当前长度
            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。
                if ((int)str[clen] > 128) { realLen++; }
                clen++;
            }
            #endregion

            return realLen;
        }

        public ArrayList ChangeOrderToOrderST(ArrayList alOrder)
        {
            ArrayList alOrderST = new ArrayList();
            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Order.OrderST order = null;
            foreach (FS.HISFC.Models.Order.Inpatient.Order Order in alOrder)
            {
                order = new FS.HISFC.Models.Order.OrderST();
                order.Is_prine = false;
                order.Name = Order.Patient.Name;
                order.Clinic_no = Order.Patient.ID;
                order.Inouttype =  "I";
                order.Card_no = Order.Patient.PID.PatientNO;
                order.Recipe_no = Order.ID;
                order.Item_code = Order.Item.ID;
                order.Item_name = Order.Item.Name;
                order.Usage_code = Order.Usage.ID;
                order.Usage_name = Order.Usage.Name;
                order.Once_dose = Order.DoseOnce;
                order.Dose_unit = Order.DoseUnit;
                order.Fre_code = Order.Frequency.ID;
                order.Fre_name = Order.Frequency.Name;
                order.Days = Order.HerbalQty;
                order.Recipe_doc_code = Order.ReciptDoctor.ID;
                order.Recipe_doc_name = Order.ReciptDoctor.Name;
                order.Recipe_dept_code = Order.ReciptDept.ID;
                order.Recipe_dept_name = deptMgr.GetDeptmentById(Order.ReciptDept.ID).Name;
                //{FE3599DC-8D27-43f1-A847-B65FC6C6BF3B}
                order.Memo = Order.Qty + Order.Unit;   //总量和总量单位放在备注里，没其他地方可以放了
              
                //开立时间
                order.Ext_memo = Order.Item.Price.ToString();
                order.Ext_memo2 = Order.MOTime.ToString();
                order.Ext_memo1 = Order.OrderType.IsDecompose ? "1" : "0";
                alOrderST.Add(order);
            }

            return alOrderST;

        }

        /// <summary>
        /// 设置特殊处方标记
        /// </summary>
        /// <param name="alOrder"></param>
        private void SetSpeInfo(ArrayList alOrder)
        {
            int speLevl = 1;
            foreach (FS.HISFC.Models.Order.OrderST order1 in alOrder)
            {

                FS.HISFC.Models.Order.Inpatient.Order order = new FS.HISFC.Models.Order.Inpatient.Order();
                order.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order1.Item_code);
                int level = Function.GetItemQaulity(order);
                if (level > speLevl)
                {
                    speLevl = level;
                }
            }

            //3、毒麻精一；2、精二；1、普通；0、非药品
            lblSpeInfo.Visible = false;
            switch (speLevl)
            {
                case 3:
                    lblSpeInfo.Text = "麻、精一";
                    lblSpeInfo.Visible = true;
                    break;
                case 2:
                    lblSpeInfo.Text = " 精  二 ";
                    lblSpeInfo.Visible = true;
                    break;
                case 4:
                    lblSpeInfo.Text = " 普  通";
                    lblSpeInfo.Visible = true;
                    break;
                 
                #region 作废
                //case 1:
                //    if (myReg.DoctorInfo.Templet.RegLevel.IsEmergency)
                //    {
                //        lblSpeInfo.Text = "急 诊";
                //        lblSpeInfo.Visible = true;
                //    }
                //    else if (myReg.DoctorInfo.Templet.Dept.Name.Contains("儿"))
                //    {
                //        lblSpeInfo.Text = "儿 科";
                //        lblSpeInfo.Visible = true;
                //        //lblHelthInfo.Visible = true;
                //        //lblHeight.Visible = true;

                //        //lblDiag.Size = new Size(338, 14);

                //        #region 显示体重信息

                //        //FS.HISFC.BizLogic.Order.OutPatient.Order outOrderMgr = new FS.HISFC.BizLogic.Order.OutPatient.Order();
                //        //string height = "";
                //        //string weight = "";
                //        //string SBP = "";
                //        //string DBP = "";
                //        //string tem = "";//体温
                //        //string bloodGlu = ""; //血糖
                //        //if (outOrderMgr.GetHealthInfo(this.myReg.ID, ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu) == -1)
                //        //{
                //        //    this.lblHeight.Text = "";
                //        //}
                //        //else
                //        //{
                //        //    if (string.IsNullOrEmpty(weight))
                //        //    {
                //        //        this.lblHeight.Text = "";
                //        //    }
                //        //    else
                //        //    {
                //        //        this.lblHeight.Text = weight.ToString() + "千克";
                //        //    }
                //        //}
                //        #endregion
                //    }
                
                    //else
                    //{
                    //}
                    //break;
                #endregion
                default:
                    lblSpeInfo.Text = " 普  通";
                    lblSpeInfo.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// 打印机名称
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }

        public int PrintRecipeView(System.Collections.ArrayList alRecipe)
        {
            this.MakaLabel(alRecipe);
            return 1;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.Classes;

namespace FS.SOC.Local.Order.ExecBill.GYSY
{

    /// <summary>
    /// 执行单打印
    /// </summary>
    ///  
    public partial class ucExecBillFP : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {

        public ucExecBillFP()
        {
            InitializeComponent();
            //this.PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocumentPrintPage);
            this.neuSpread1.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread1_ButtonClicked);
        }

        private string[] Days = new string[] { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };

         
        #region IPrintTransFusion 成员

        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        
        FS.HISFC.BizLogic.Order.Order orderBizLogic = new FS.HISFC.BizLogic.Order.Order();
        
        FS.HISFC.BizLogic.Manager.Bed bedManager = new FS.HISFC.BizLogic.Manager.Bed();

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        
        //string speOrderType = "";

        ArrayList curValues = null; //当前显示的数据

        /// <summary>
        /// 是否补打
        /// </summary>
        bool isRePrint = false;

        /// <summary>
        /// 是否首日
        /// </summary>
        private bool isFirst;

        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        
        DateTime dt1;
        
        DateTime dt2;
        
        string usage = "";

        /// <summary>
        /// 医嘱类型 all全部；1 长嘱；0 临嘱
        /// </summary>
        private string orderType;

        string ExeBillPrint = "";

        bool isDisplayRegularName = true;
        #endregion

        #region 方法

        /// <summary>
        /// 画边框
        /// </summary>
        private void AddLine()
        {
            #region 内容

            //只显示下面的边框  bevelBorder1---普通线   bevelBorder2---粗黑线   bevelBorder3---空白线
            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 1, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder2 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            FarPoint.Win.BevelBorder bevelBorder3 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.White, 1, false, false, false, false);

            if (this.neuSpread1_Sheet1.Rows.Count > 0)//有数据
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    /////初始化时全选/////
                    this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, this.chkAll.Checked);

                    if (i != this.neuSpread1_Sheet1.RowCount - 1)//未完
                    {
                        for (int j = (int)ExecBillCols.Memo; j < (int)ExecBillCols.PrintFlag; j++)//时间点之后---普通线
                        {
                            this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder1;
                        }

                        if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┏" || this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text == "┃")//组内用空白线
                        {
                            for (int j = (int)ExecBillCols.OrderState; j <= (int)ExecBillCols.DoseOnce; j++)//空白线
                            {
                                this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder3;
                            }
                        }
                        else if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┗")//不同组---普通线
                        {
                            for (int j = (int)ExecBillCols.OrderState; j <= (int)ExecBillCols.PatientName; j++)//普通线-----不起作用？？？
                            {
                                this.neuSpread1_Sheet1.Cells[i, j].Text = " ";
                                this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder1;
                            }

                            this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
                        }


                        if (this.neuSpread1_Sheet1.RowCount > 1)
                        {
                            this.neuSpread1_Sheet1.Cells[0, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[0, (int)ExecBillCols.BedID].Text;
                            if (this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Text == "")
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag;
                            }
                            else if (this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Text == this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag.ToString())//床号相同
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.BedID].Tag;
                                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder1;
                            }
                            else //床号不同---粗黑线
                            {
                                this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Tag = this.neuSpread1_Sheet1.Cells[i + 1, (int)ExecBillCols.BedID].Text;
                                this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                                for (int j = (int)ExecBillCols.Memo; j < (int)ExecBillCols.PrintFlag; j++)
                                {
                                    this.neuSpread1_Sheet1.Cells[i, j].Border = bevelBorder2;
                                }
                            }
                        }
                    }
                    else //表尾
                    {

                     if (this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Text.Trim() == "┗")//不同组---普通线
                        {
                            for (int j = (int)ExecBillCols.OrderState; j <= (int)ExecBillCols.PatientName; j++)//普通线-----不起作用？？？
                            {
                                this.neuSpread1_Sheet1.Cells[i, j].Text = " ";
                            }
                        }
                        this.neuSpread1_Sheet1.Rows[i].Border = bevelBorder2;
                    }
                }
            }

            #endregion


            #region 标题

            FarPoint.Win.BevelBorder headerBorder = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, true, false, true);

            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnHeader.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.ColumnHeader.Rows[i].Border = headerBorder;
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        private void AddToFP(ArrayList list)
        {
            this.neuSpread1_Sheet1.Rows.Count = 0;
            if (list == null || list.Count == 0)
            {
                return;
            }
            int index = 0;
            string itemName = "";
            int length = System.Text.Encoding.GetEncoding("gb2312").GetBytes("呵呵哈哈哈哈哈哈哈哈哈呵呵呵哈哈哈").Length;
            FarPoint.Win.Spread.CellType.CheckBoxCellType chb = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
            this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].CellType = chb;

            bool bDealItemName = true;

            bDealItemName = controlParam.GetControlParam<bool>("HNHS01", false, false);

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in list)
            {
                this.neuSpread1_Sheet1.Rows.Add(index, 1);
                this.neuSpread1_Sheet1.Rows[index].Tag = execOrder;


                //状态
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.OrderState].Text = (execOrder.IsCharge == false ? "未" : "")+ (execOrder.Order.OrderType.IsCharge == false ? "[嘱]" : "");
                //床号
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.BedID].Text = execOrder.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                //姓名
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PatientName].Text = execOrder.Order.Patient.Name;
                //if (execOrder.Order.Patient.Name.Length > (int)ExecBillCols.ComboMemo)
                //{
                //    this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PatientName].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                //}

                //组号

                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ComboNo].Text = execOrder.Order.Combo.ID;

                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ComboMemo].Text = execOrder.Order.Combo.Memo;
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ComboMemo].Tag = execOrder.Order.Combo.ID;
                //名称
                //this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ItemName].Text = execOrder.Order.Item.Name + "  " + execOrder.Order.Item.Specs + "  " + execOrder.Order.Memo;
                //数量
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.OrderQty].Text = execOrder.Order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.Unit.TrimEnd(' ');
                //频次
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.FrequencyID].Text = execOrder.Order.Frequency.ID;
                //用法
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.UsageName].Text = execOrder.Order.Usage.Name;
                //每次量
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.DoseOnce].Text = execOrder.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.') + execOrder.Order.DoseUnit;
                //备注 +皮试
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Memo].Text = execOrder.Memo;
                //this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.Memo].Text = "";
                //签名
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ExecSignature].Text = "";
                //执行时间
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ExecTime].Text = ".";
                //默认全部打印
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PrintFlag].Text = "True";

                if (!string.IsNullOrEmpty(execOrder.Order.Memo))
                {
                    execOrder.Order.Memo = "(" + execOrder.Order.Memo + ")";
                }

                if (execOrder.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {

                    //处理999嘱托医嘱报空指针
                    if (isDisplayRegularName && !execOrder.Order.Item.ID.Equals("999"))
                    {
                        FS.HISFC.Models.Pharmacy.Item phaItem = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(execOrder.Order.Item.ID) as FS.HISFC.Models.Pharmacy.Item;
                        itemName = phaItem.NameCollection.RegularName + execOrder.Order.Memo + "[" + phaItem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaItem.DoseUnit + "]";
                    }
                    else
                    {

                        FS.HISFC.Models.Pharmacy.Item phaitem = (execOrder.Order.Item as FS.HISFC.Models.Pharmacy.Item);

                        //if (phaitem.MinUnit != phaitem.DoseUnit)
                        //{
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + phaitem.BaseDose.ToString("F4").TrimEnd('0').TrimEnd('.') + phaitem.DoseUnit + "]";
                        //}
                        //else
                        //{
                        //    itemName = execOrder.Order.Item.Name + execOrder.Order.Memo;
                        //}
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(execOrder.Order.Item.Specs))
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo + "[" + execOrder.Order.Item.Specs + "]";
                    }
                    else
                    {
                        itemName = execOrder.Order.Item.Name + execOrder.Order.Memo;
                    }
                }
                //名称过长自动换行
                if (System.Text.Encoding.GetEncoding("gb2312").GetBytes(itemName).Length > length)
                {
                    this.neuSpread1_Sheet1.Rows[index].Height = 2 * this.neuSpread1_Sheet1.Rows[index].Height;
                }
                this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.ItemName].Text = itemName;

                index++;
            }

            //for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
            //{
            //    if (j != this.neuSpread1_Sheet1.Columns.Count - 1)
            //    {
            //        this.neuSpread1_Sheet1.Columns[j].Locked = true;
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="newList"></param>
        private void AddConstsToTable(ArrayList list, ref ArrayList newList)
        {
            foreach (FS.HISFC.Models.Order.ExecOrder objExc in list)
            {
                //if (!objExc.IsExec)
                //{
                //    continue;
                //}

                //if (!objExc.Order.Item.IsPharmacy)
                if (objExc.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    objExc.Order.DoseOnce = 0;
                    objExc.Order.DoseUnit = "";
                }

                try
                {

                    //if (objExc.Order.Item.IsPharmacy == false)
                    if (objExc.Order.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (objExc.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (objExc.Order.IsEmergency == true)
                            {
                                if (objExc.Order.Note != "")
                                    objExc.Memo = "加急[" + objExc.Order.Note + "]";
                                else
                                    objExc.Memo = "加急";
                            }
                            else
                            {
                                if (objExc.Order.Note != "")
                                    objExc.Memo = objExc.Order.Note;
                                else
                                    objExc.Memo += "";
                            }
                        }
                        else
                        {
                            objExc.Order.DoseOnce = objExc.Order.Qty;
                            objExc.Order.DoseUnit = objExc.Order.Unit;
                            objExc.Memo += objExc.Order.Note;
                        }

                        if (objExc.Order.Memo == objExc.Memo) objExc.Memo = "";

                        if (objExc.Order.OrderType.ID == "BL")//补录医嘱,备注显示补录字样。
                            objExc.Order.Memo = objExc.Order.Memo + "[补录]";
                        FS.HISFC.Models.Fee.Item.Undrug objAssets = objExc.Order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                        newList.Add(objExc);
                    }
                    else
                    {
                        if (objExc.Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (objExc.Order.Note != "")
                                objExc.Memo = objExc.Order.Note;
                            else
                                objExc.Memo += "";
                        }
                        else
                            objExc.Memo += objExc.Order.Note;

                        if (objExc.Order.Memo == objExc.Memo) objExc.Memo = "";
                        //if (objExc.Order.Memo != "" && objExc.Order.Memo.IndexOf("需皮试") != -1)
                        //{
                        try
                        {
                            int hypotest = this.orderBizLogic.QueryOrderHypotest(objExc.Order.ID);
                            if (FS.SOC.Local.Order.Classes.Function.GetPhaItem(objExc.Order.Item.ID) != null && FS.SOC.Local.Order.Classes.Function.GetPhaItem(objExc.Order.Item.ID).IsAllergy)
                            {
                                objExc.Order.Memo += orderBizLogic.TransHypotest((FS.HISFC.Models.Order.EnumHypoTest)hypotest);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("获得皮试信息出错！", "Note");
                        }

                        if (objExc.Order.OrderType.ID == "BL")//补录医嘱,备注显示补录字样。
                            objExc.Order.Memo = objExc.Order.Memo + "[补录]";
                        FS.HISFC.Models.Pharmacy.Item objPharmacy = objExc.Order.Item as FS.HISFC.Models.Pharmacy.Item;
                        newList.Add(objExc);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("execBillPaper1");

                if (pSize == null)
                {
                    pSize = new FS.HISFC.Models.Base.PageSize("Letter", 850, 550);
                }

                //this.PrintDocument.DefaultPageSettings.Margins.Bottom = pSize.Top;
                //if (!string.IsNullOrEmpty(pSize.Printer))
                //{
                //    this.PrintDocument.PrinterSettings.PrinterName = pSize.Printer;
                //}

                //设置不打印的行不显示
                int index = 0;
                ArrayList al = new ArrayList();

                Dictionary<string, ArrayList> Dictionary = new Dictionary<string, ArrayList>();
                Dictionary<string, FS.HISFC.Models.RADT.PatientInfo> DictionaryPatientInfo = new Dictionary<string, FS.HISFC.Models.RADT.PatientInfo>();
                FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();

                ArrayList alItems = new ArrayList();

                for (index = 0; index < this.neuSpread1_Sheet1.RowCount; index++)
                {
                    if (this.neuSpread1_Sheet1.Cells[index, (int)ExecBillCols.PrintFlag].Text == "False")
                    {
                        this.neuSpread1_Sheet1.Rows[index].Visible = false;//仅设为不可见====导致无数据全选仍可打印
                    }
                    else
                    {
                        if (!object.Equals(this.neuSpread1_Sheet1.Rows[index].Tag, null))//行TAG不为空则转换赋值
                        {
                            execOrder = this.neuSpread1_Sheet1.Rows[index].Tag as FS.HISFC.Models.Order.ExecOrder;
                            al.Add(execOrder);//实体添加至ArrayList中
                        }

                        if (Dictionary.ContainsKey(execOrder.Order.Patient.ID))
                        {
                            alItems = new ArrayList();
                            alItems = Dictionary[execOrder.Order.Patient.ID] as ArrayList;
                            
                            //构造数据----大字符串
                            string str = "";
                            for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                            {
                                str += this.neuSpread1_Sheet1.Cells[index, j].Text + "|"; //列循环,分隔符
                            }
                            str = str.Substring(0, str.Length - 1);
                            alItems.Add(str);

                        }
                        else
                        {
                            alItems = new ArrayList();

                            //构造数据----大字符串
                            string str = "";
                            for (int j = 0; j < this.neuSpread1_Sheet1.Columns.Count; j++)
                            {
                                str += this.neuSpread1_Sheet1.Cells[index, j].Text + "|"; //列循环,分隔符
                            }
                            str = str.Substring(0, str.Length - 1);
                            alItems.Add(str);

                            Dictionary.Add(execOrder.Order.Patient.ID, alItems);
                            DictionaryPatientInfo.Add(execOrder.Order.Patient.ID, execOrder.Order.Patient);
                        }
                    }
                }

                //只更新打印的执行单状态
                this.curValues = al;    //实体添加进ArrayList中

                //设置“是否打印”列不显示
                this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].Visible = false;

                print.SetPageSize(pSize);
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("没有可打印的数据，请重新选择！");
                }
                else //打印程序段
                {
                    #region 按照行数打印
                    ArrayList alPrint = null;


                    int pageNo = 0;
                    foreach (string key in Dictionary.Keys)
                    {
                        ArrayList aa = Dictionary[key];

                            
                        int fromPage = 1;
                        int toPage = (Int32)Math.Ceiling((decimal)aa.Count / 10);
                        for (int page = fromPage; page <= toPage; page++)
                        {
                            alPrint = new ArrayList();
                           
                            for (int i = 0; i < aa.Count; i++)
                            {
                                if (i >= (page - 1) * 10 && i < page * 10)//每页显示10条
                                {
                                    alPrint.Add(aa[i] as string);
                                }
                            }
                            pageNo++;


                            ucExecBillPrint ucPrint = new ucExecBillPrint();
                            ucPrint = new ucExecBillPrint();
                            ucPrint.AlOrders = alPrint;
                            //ucPrint.Title = this.lblTitle.Text;
                            //ucPrint.TitleDate = this.neuLblExecTime.Text;
                            //ucPrint.Page = pageNo.ToString();//当前页

                            ucPrint.SetHeader(this.lblTitle.Text, this.neuLblExecTime.Text, this.neuLblPrintTime.Text, pageNo.ToString());
                            ucPrint.SetPatientInfo(DictionaryPatientInfo[key]);
                            ucPrint.Tag = null;

                            if (!string.IsNullOrEmpty(this.ExeBillPrint))
                            {
                                ucPrint.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, (int)ExecBillCols.ExecSignature).Value = this.ExeBillPrint;
                            }

                            if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).IsManager)
                            {
                                print.PrintPreview(5, 0, ucPrint);
                            }
                            else
                            {
                                print.PrintPage(5, 0, ucPrint);
                            }
                        }
                    }     
                    #endregion

                }
                //打印后设置“是否打印”列显示
                this.neuSpread1_Sheet1.Columns[(int)ExecBillCols.PrintFlag].Visible = true;

                #region 更新已经打印标记
                if (!this.isRePrint)//首次打印
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();

                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(orderManager.Connection);
                    //t.BeginTransaction();

                    this.orderBizLogic.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    string itemType = null;
                    for (int i = 0; i < this.curValues.Count; i++)
                    {
                        FS.HISFC.Models.Order.ExecOrder exeord = curValues[i] as FS.HISFC.Models.Order.ExecOrder;
                        if (exeord.Order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                        {
                            itemType = "1";
                        }
                        else
                        {
                            itemType = "2";
                        }
                        //通过医嘱流水号更新执行档
                        if (this.orderBizLogic.UpdateExecOrderPrintedByMoOrder(exeord.Order.ID, dt1.ToString(), dt2.ToString(), itemType) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!" + orderBizLogic.Err);
                            return;
                        }
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();

                    this.Query(myPatients, usage, dt1, dt2, isRePrint, orderType, isRePrint);
                }
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 
        /// </summary>
        public void PrintSet()
        {
            //print.ShowPrintPageDialog();
            //this.Print();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="alValues"></param>
        protected void SetValues(ArrayList alValues)
        {
            // curValues = alValues;
            if (alValues != null)
            {
                if (this.neuSpread1_Sheet1.RowCount > 0)
                {
                    this.neuSpread1_Sheet1.RowCount = 0;
                }

                ArrayList newList = new ArrayList();
                //alValues.Sort(new ComparerExecOrder());
                this.AddConstsToTable(alValues, ref newList);

                //SQL已经按照sortID、mo_order排序了，这里再根据床号、住院号排序，保证一个患者的药品非药品在一起
                newList.Sort(new ComparerExecOrder());
                this.AddToFP(newList);
                FS.SOC.Local.Order.Classes.Function.DrawComboLeft(this.neuSpread1_Sheet1, (int)(int)ExecBillCols.ComboNo, (int)ExecBillCols.ComboMemo);


                //设置执行单为变态样式
                SetOrderExecBill();


               this.AddLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetOrderExecBill()
        {
            for (int i = this.neuSpread1_Sheet1.Rows.Count - 1; i >= 0; i--)
            {
                FS.HISFC.Models.Order.ExecOrder execOrder = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                //每组医嘱数量
                int orderCount = orderBizLogic.QueryOrderCountByCombno(execOrder.Order.Patient.ID, execOrder.Order.Combo.ID);

                //ArrayList alOrderExec = orderBizLogic.QueryOrderExecCountByCombno(execOrder.Order.Patient.ID, execOrder.Order.Combo.ID, this.dt1, this.dt2, this.usage, this.isRePrint);

                string memoStr = this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.Memo].Text;

                string[] timeStr = memoStr.Split(',');

                int execCount = timeStr.Count();
                int rowCount = 0;
                if (orderCount >= execCount)
                {
                    i = i - orderCount + 1;
                    rowCount = orderCount;
                }
                else
                {
                    this.neuSpread1_Sheet1.Rows.Add(i + 1, execCount - orderCount);
                    i = i - orderCount + 1;
                    rowCount = execCount;
                }
                //string memoStr = this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.Memo].Text;

                //string[] timeStr = memoStr.Split(',');

                for (int j = 0; j < rowCount; j++)
                {
                    if (j < timeStr.Count())
                    {
                        this.neuSpread1_Sheet1.Cells[i + j, (int)ExecBillCols.Memo].Text = timeStr[j];
                        this.neuSpread1_Sheet1.Cells[i + j, (int)ExecBillCols.ExecTime].Text = ".";
                    }
                    else
                    {
                        
                        this.neuSpread1_Sheet1.Cells[i + j, (int)ExecBillCols.ComboNo].Tag = execOrder.Order.Combo.ID;//组号
                        this.neuSpread1_Sheet1.Cells[i + j, (int)ExecBillCols.Memo].Text = " ";
                        this.neuSpread1_Sheet1.Cells[i + j, (int)ExecBillCols.ExecTime].Text = ".";
                    }
                }
            }
        }

        #endregion

        #region 事件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuSpread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            //if (e.Column == (int)ExecBillCols.PrintFlag)
            //{
            //    string combNo = neuSpread1_Sheet1.Cells[e.Row, (int)ExecBillCols.ComboMemo].Tag == null ? "" : neuSpread1_Sheet1.Cells[e.Row, (int)ExecBillCols.ComboMemo].Tag.ToString();
            //    if (string.IsNullOrEmpty(combNo))
            //    {
            //        return;
            //    }
            //    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            //    {
            //        if (neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Tag != null
            //            && neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.ComboMemo].Tag.ToString() == combNo)
            //        {
            //            this.neuSpread1_Sheet1.Cells[i, (int)ExecBillCols.PrintFlag].Text = neuSpread1_Sheet1.Cells[e.Row, e.Column].Text;
            //        }
            //    }
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, this.chkAll.Checked);
            }
        }


        private void chkReverse_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
            {
                if (((bool)this.neuSpread1_Sheet1.GetValue(i, (int)ExecBillCols.PrintFlag) == true))
                {
                    this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, false);
                }
                else
                {
                    this.neuSpread1_Sheet1.SetValue(i, (int)ExecBillCols.PrintFlag, true);
                } 
            }
        }


        #endregion

        #region IPrintTransFusion 成员

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            this.orderType = orderType;
            this.isRePrint = isPrinted;
            this.isFirst = isFirst;

            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.usage = usageCode;

            //更换执行单列名为备注
            if (!string.IsNullOrEmpty(patients[0].UserCode))
            {
                this.neuSpread1_Sheet1.Columns.Get((int)ExecBillCols.ExecSignature).Label = patients[0].UserCode;
                this.ExeBillPrint = patients[0].UserCode;
            }

            this.neuLblExecTime.Text = dtBegin.ToLongDateString() + " " + Days[Convert.ToInt16(dtBegin.DayOfWeek)];
            this.neuLblPrintTime.Text = "打印时间：" + this.orderBizLogic.GetDateTimeFromSysDateTime();

            //给患者列表赋值
            this.myPatients = patients;
            //更改治疗单标题
            //this.dwMain.Modify("t_title.text= " + "'" + this.Tag.ToString() + "'");

            this.lblTitle.Text = this.Tag.ToString();

            try
            {
                FS.HISFC.BizProcess.Integrate.Manager managerMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                string hosname = managerMgr.GetHospitalName();
                //this.dwMain.Modify("t_hospitalname.text= " + "'" + hosname + "'");
            }
            catch { }

            ArrayList alOrder = new ArrayList();
            //ArrayList al = new ArrayList();
            string paramPatient = "";
            //获得in的患者id参数
            for (int i = 0; i < patients.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = patients[i] as FS.HISFC.Models.RADT.PatientInfo;
                paramPatient = "'" + p.ID + "'," + paramPatient;
                //获得护理分组

                p.PVisit.PatientLocation.Bed.Memo = bedManager.GetNurseTendGroupFromBed(p.PVisit.PatientLocation.Bed.ID);

                if (p.PVisit.PatientLocation.Bed == null)
                {
                    MessageBox.Show(bedManager.Err);
                    return;
                }
            }

            if (paramPatient == "")
            {
                paramPatient = "''";
            }
            else
            {
                paramPatient = paramPatient.Substring(0, paramPatient.Length - 1);//去掉后面的逗号
            }


            alOrder = this.orderBizLogic.QueryOrderExecBill(paramPatient, dtBegin, dtEnd, usageCode, isPrinted);//查询医嘱，传入参数

            if (alOrder == null) //为提示出错 
            {
                MessageBox.Show(orderBizLogic.Err);
                return;
            }
            else
            {
                FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();
                ArrayList alOrderTemp = new ArrayList();

                foreach (FS.HISFC.Models.Order.ExecOrder exe in alOrder)
                {
                    #region 医嘱停用后的不显示了

                    orderObj = this.orderBizLogic.QueryOneOrder(exe.Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("查询医嘱出错：" + this.orderBizLogic.Err);
                        return;
                    }

                    //长嘱
                    if (orderType == "1")
                    {
                        if (!exe.Order.OrderType.IsDecompose)
                        {
                            continue;
                        }
                    }
                    //临嘱
                    else if (orderType == "0")
                    {
                        if (exe.Order.OrderType.IsDecompose)
                        {
                            continue;
                        }
                    }

                    //停止医嘱3--不检查重整医嘱4
                    if ("3".Contains(orderObj.Status.ToString()))
                    {
                        if (exe.DateUse > orderObj.DCOper.OperTime)
                        {
                            continue;
                        }
                    }
                    #endregion

                    if (isFirst)
                    {
                        if (exe.Order.BeginTime.ToShortDateString() == exe.DateUse.ToShortDateString())
                        {
                            alOrderTemp.Add(exe);
                        }
                    }
                    else
                    {
                        alOrderTemp.Add(exe);
                    }
                }
                alOrder = alOrderTemp;
            }


            this.curValues = alOrder;

            #region 将同一条医嘱合并在一起
            ArrayList alTemp = alOrder.Clone() as ArrayList;
            alOrder = new ArrayList();
            for (int k = 0; k < alTemp.Count; k++)
            {
                bool isHave = false;

                TimeSpan span;
                string sMing = "";
                for (int j = 0; j < alOrder.Count; j++)
                {
                    sMing = "";
                    if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID == ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.ID)
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Length > 2 &&
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Memo.Substring(0, 2) == "时间")
                        {
                            //特殊频次
                        }
                        else
                        {
                            isHave = true;//包含添加时间
                            span = new TimeSpan(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                            if (span.Days == 1) sMing = "明";
                            if (span.Days == 2) sMing = "后";
                            if (span.Days > 2) sMing = "[" + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Memo += "," + sMing + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');
                            break;
                        }
                    }
                }
                if (!isHave)
                {
                    span = new TimeSpan(((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Date.Ticks - dtBegin.Date.Ticks);
                    if (span.Days == 1) sMing = "明";
                    if (span.Days == 2) sMing = "后";
                    if (span.Days > 2) sMing = "[" + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Day.ToString() + "日]";
                    if (((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)//临时医嘱暂时不显示执行时间 by zuowy
                        ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Memo = sMing + ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).DateUse.Hour.ToString().PadLeft(2, '0');

                    for (int kk = 0; kk < patients.Count; kk++)
                    {
                        if (((FS.FrameWork.Models.NeuObject)patients[kk]).ID == ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.Patient.ID)
                        {
                            ((FS.HISFC.Models.Order.ExecOrder)alTemp[k]).Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[kk]).Clone();
                            break;
                        }
                    }

                    alOrder.Add(alTemp[k]);
                }
            }

            #endregion


            this.SetValues(alOrder);
            return;
        }

        public void SetSpeOrderType(string speStr)
        {
            //this.speOrderType = speStr;
            //this.speOrderType = "";
        }

        #endregion

        private void ucExecBillFP_Load(object sender, EventArgs e)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            isDisplayRegularName = controlIntegrate.GetControlParam<bool>("HNZY01", true, true);
        }


        #region IPrintTransFusion 成员

        /// <summary>
        /// 作废的执行档是否打印
        /// </summary>
        private bool dcIsPrint = false;

        /// <summary>
        /// 作废的执行档是否打印
        /// </summary>
        public bool DCIsPrint
        {
            get
            {
                return dcIsPrint;
            }
            set
            {
                dcIsPrint = value;
            }
        }

        /// <summary>
        /// 未收费是否允许打印
        /// </summary>
        private bool noFeeIsPrint = false;

        /// <summary>
        /// 未收费是否允许打印
        /// </summary>
        public bool NoFeeIsPrint
        {
            get
            {
                return noFeeIsPrint;
            }
            set
            {
                noFeeIsPrint = value;
            }
        }

        /// <summary>
        /// 退费后的是否打印
        /// </summary>
        private bool quitFeeIsPrint = true;

        /// <summary>
        /// 退费后的是否打印
        /// </summary>
        public bool QuitFeeIsPrint
        {
            get
            {
                return quitFeeIsPrint;
            }
            set
            {
                quitFeeIsPrint = value;
            }
        }

        #endregion
    }

    public class ComparerExecOrder : IComparer
    {
        #region IComparer 成员

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.ExecOrder execOrder1 = x as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder execOrder2 = y as FS.HISFC.Models.Order.ExecOrder;
                int aa = manager.GetBed(execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID;
                int bb = manager.GetBed(execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID;
                string cc = execOrder1.Order.MOTime.ToString("yyyyMMdd") + execOrder1.Order.Combo.ID + execOrder1.Order.ID;
                string dd = execOrder2.Order.MOTime.ToString("yyyyMMdd") + execOrder2.Order.Combo.ID + execOrder2.Order.ID;
                if (aa > bb)
                {
                    return 1;
                }
                else if (aa == bb)
                {
                    return string.Compare(cc, dd);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.Drawing.Printing;
using System.IO;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 门诊处方重打
    /// </summary>
    public partial class ucRecipeReprint : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucRecipeReprint()
        {
            InitializeComponent();
        }
         
        #region 变量

        string recipeNo = "";

        private string RecipeNO
        {
            get
            {
                return recipeNo;
            }
            set
            {
                recipeNo = value;
                this.lblRecipeNO.Text = "当前处方号: " + value;
            }
        }

        /// <summary>
        /// 打印机名
        /// </summary>
        private string printer = string.Empty;

        /// <summary>
        /// 打印机名
        /// </summary>
        [Category("控件设置"), Description("打印机名")]
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

        private FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();

        /// <summary>
        /// 存储打印不同类型时使用的打印机型号
        /// </summary>
        private string fileName = Application.StartupPath + "\\Setting\\OutOrderPrint.xml";

        /// <summary>
        /// 打印机名
        /// </summary>
        private string printerName = string.Empty;

        #endregion

        #region 工具栏

        /// <summary>
        /// 工具栏
        /// </summary>
        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// 工具栏设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清屏", "清除屏幕显示的数据", FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("打印处方", "打印普通处方", (int)FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            toolBarService.AddToolButton("预览处方", "预览处方", FS.FrameWork.WinForms.Classes.EnumImageList.D打印预览, true, false, null);
            return toolBarService;
        }

        /// <summary>
        /// 工具栏事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "打印处方":
                    this.PrintRecipe(false);
                    break;
                case "预览处方":
                    this.PrintRecipe(true);
                    break;
                case "清屏":
                    Clear();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 根据条件查询处方
        /// </summary>
        private void Query()
        {
            if (this.tabControl1.SelectedIndex == 0)
            {
                Clear();

                switch (this.lblFilter.Text)
                {
                    case "处方号(F1切换)":
                        this.QueryByRecipeNO(this.tbArg.Text.Trim());
                        break;
                    case "患者卡号(F1切换)":
                        string cardNO = QueryCardNO(this.tbArg.Text.Trim());
                        if (!string.IsNullOrEmpty(cardNO))
                        {
                            this.QueryByCardNO(cardNO, this.dtpBegin.Value, this.dtpEnd.Value);
                        }
                        else
                        {
                            MessageBox.Show("没有符合条件的患者");
                            return;
                        }
                        break;
                    case "发票号(F1切换)":
                        string recipeNO = QueryRecipeNOByInvoiceNO(this.tbArg.Text.Trim());
                        if (!string.IsNullOrEmpty(recipeNO))
                        {
                            this.QueryByRecipeNO(recipeNO);
                        }
                        else
                        {
                            return;
                        }
                        break;
                    case "姓名(F1切换)":
                        QueryRecipeNoByRegisterName(this.tbArg.Text.Trim(), this.dtpBegin.Value, this.dtpEnd.Value);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.treeView1.Nodes.Clear();

                string sql = @"SELECT * FROM (
                            SELECT DISTINCT (SELECT dept_code FROM COM_EMPLOYEE WHERE EMPL_CODE = f.doct_code) ,
                            (SELECT dept_name FROM COM_DEPARTMENT WHERE DEPT_CODE = 
                                (SELECT dept_code FROM COM_EMPLOYEE WHERE EMPL_CODE = f.doct_code) ) ,
                            (SELECT empl_name FROM COM_EMPLOYEE WHERE EMPL_CODE = f.doct_code) ,
                            f.doct_code,to_char(f.fee_date,'yyyy-mm-dd'),f.CARD_NO,(SELECT NAME FROM FIN_OPR_REGISTER r
                            WHERE r.clinic_code = f.CLINIC_CODE),f.recipe_no,f.COST_SOURCE FROM FIN_OPB_FEEDETAIL f
                            WHERE f.fee_DATE >= timestamp('{0}')
                             and  f.fee_DATE <= timestamp('{1}')
                             and  f.cost_source = '1'
                             AND  f.DRUG_FLAG= '1'
                            ) ORDER BY 2";

                DataSet dsRes = new DataSet();

                sql = string.Format(sql, this.neuDateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.neuDateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss"));

                CacheManager.OutOrderMgr.ExecQuery(sql, ref dsRes);

                Hashtable hsDept = new Hashtable();
                Hashtable hsDoct = new Hashtable();

                foreach (DataRow row in dsRes.Tables[0].Rows)
                {
                    if (!hsDept.Contains(row[0].ToString()))
                    {
                        TreeNode nodeDept = new TreeNode();
                        nodeDept.Text = row[1].ToString();
                        nodeDept.Tag = row[0].ToString();

                        TreeNode nodeDoct = new TreeNode();
                        nodeDoct.Text = "医生:"+row[2].ToString();
                        nodeDoct.Tag = row[3].ToString();

                        nodeDept.Nodes.Add(nodeDoct);

                        TreeNode nodeRep = new TreeNode();
                        nodeRep.Text = "患者:" + row[6].ToString();
                        nodeRep.Tag = row[7].ToString();

                        nodeDoct.Nodes.Add(nodeRep);

                        this.treeView1.Nodes.Add(nodeDept);
                        hsDoct.Add(row[0].ToString()+row[3].ToString(), nodeDoct);
                        hsDept.Add(row[0].ToString(), nodeDept);
                    }
                    else
                    {
                        TreeNode nodeDept = hsDept[row[0].ToString()] as TreeNode;

                        if (!hsDoct.Contains(row[0].ToString() + row[3].ToString()))
                        {
                            TreeNode nodeDoct = new TreeNode();
                            nodeDoct.Text = "医生:" + row[2].ToString();
                            nodeDoct.Tag = row[3].ToString();

                            TreeNode nodeRep = new TreeNode();
                            nodeRep.Text = "患者:" + row[6].ToString();
                            nodeRep.Tag = row[7].ToString();

                            nodeDoct.Nodes.Add(nodeRep);

                            hsDoct.Add(row[0].ToString() + row[3].ToString(), nodeDoct);
                            nodeDept.Nodes.Add(nodeDoct as TreeNode);
                        }
                        else
                        {
                            TreeNode nodeRep = new TreeNode();
                            nodeRep.Text = "患者:" + row[6].ToString();
                            nodeRep.Tag = row[7].ToString();

                            TreeNode nodeDoct = hsDoct[row[0].ToString() + row[3].ToString()] as TreeNode;
                            nodeDoct.Nodes.Add(nodeRep);
                        }
                    }
                }
            }
        }

        private void SetDateControlVisible(bool visible)
        {
            this.neuPanel3.Visible = visible;
        }

        /// <summary>
        /// 根据发票号查处方号
        /// </summary>
        /// <param name="invoNO"></param>
        /// <returns></returns>
        private string QueryRecipeNOByInvoiceNO(string invoNO)
        {
            ArrayList alFeeItem = CacheManager.OutOrderMgr.QueryRecipeListByInvoiceNo(invoNO);
            if (alFeeItem == null)
            {
                MessageBox.Show("根据发票号查找费用明细失败" + CacheManager.FeeIntegrate.Err);
                return "";
            }
            else if (alFeeItem.Count == 0)
            {
                MessageBox.Show("没有该发票号对应的处方信息，请确认是否是手工处方！");
                return "";
            }
            else if (alFeeItem.Count == 0)
            {
                return (alFeeItem[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList).RecipeNO;
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (FrameWork.WinForms.Classes.Function.ChooseItem(alFeeItem, new string[] { "处方号", "开立医生", "操作时间", "病历号", "患者姓名", "发票号" }, new bool[] { }, new int[] { 60, 60, 70, 80, 70, 80 }, ref obj) != 1)
                {
                    return "";
                }
                this.QueryByRecipeNO(obj.ID);
            }
            return "";
        }

        /// <summary>
        /// 先查挂号,再查处方
        /// </summary>
        /// <param name="name"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        private void QueryRecipeNoByRegisterName(string name,DateTime beginTime,DateTime endTime)
        {
            ArrayList regList = new ArrayList();

            regList = CacheManager.RegMgr.QueryByName(name);
             if (regList == null || regList.Count <= 0)
             {
                 MessageBox.Show("按姓名获取有效挂号信息出错,该患者时间段内未挂号!");
                 return ;
             }
             else if (regList.Count == 1)
             {
                 this.QueryByCardNO((regList[0] as FS.HISFC.Models.Registration.Register).PID.CardNO, beginTime, endTime);
             }
             else
             {
                 ArrayList patientList = new ArrayList();

                 for (int i = 0; i < regList.Count; i++)
                 {
                     FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                     tempObj.ID = (regList[i] as FS.HISFC.Models.Registration.Register).PID.CardNO;
                     tempObj.Name = (regList[i] as FS.HISFC.Models.Registration.Register).Name;
                     tempObj.Memo = (regList[i] as FS.HISFC.Models.Registration.Register).Sex.Name;
                     tempObj.User01 = (regList[i] as FS.HISFC.Models.Registration.Register).Birthday.ToShortDateString();
                     tempObj.User02 = (regList[i] as FS.HISFC.Models.Registration.Register).PhoneHome;
                     patientList.Add(tempObj);
                 }

                 FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();

                 if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(patientList, new string[] { "病历号", "姓名", "性别", "出生日期", "家庭电话" }, new bool[] { true, true, true, true, true }, new int[] { 100, 80, 50, 100, 100 }, ref obj) != 1)
                 {
                     return;
                 }
                 else
                 {
                     this.QueryByCardNO(obj.ID, beginTime, endTime);
                 }
             }
        }

        private string QueryRecipeNOByName(string name, DateTime beginTime, DateTime endTime)
        {
            ArrayList invoiceList = new ArrayList();

            invoiceList = CacheManager.OutFeeMgr.QueryBalancesByName(name, beginTime, endTime);

            if (invoiceList == null || invoiceList.Count <= 0)
            {
                MessageBox.Show("获取发票信息失败!未找到对应的发票信息!");
                return "";
            }
            else if (invoiceList.Count == 1)
            {
                return this.QueryRecipeNOByInvoiceNO((invoiceList[0] as FS.HISFC.Models.Fee.Outpatient.Balance).Invoice.ID);
            }
            else
            {
                ArrayList invoiceTemp = new ArrayList();
                for (int i = 0; i < invoiceList.Count; i++)
                {
                    FS.FrameWork.Models.NeuObject tempObj = new FS.FrameWork.Models.NeuObject();
                    tempObj.ID = (invoiceList[i] as FS.HISFC.Models.Fee.Outpatient.Balance).Invoice.ID;
                    tempObj.Name = (invoiceList[i] as FS.HISFC.Models.Fee.Outpatient.Balance).Patient.Name;
                    tempObj.Memo = (invoiceList[i] as FS.HISFC.Models.Fee.Outpatient.Balance).FT.TotCost.ToString();
                    tempObj.User01 = (invoiceList[i] as FS.HISFC.Models.Fee.Outpatient.Balance).BalanceOper.ID;
                    tempObj.User02 = (invoiceList[i] as FS.HISFC.Models.Fee.Outpatient.Balance).BalanceOper.OperTime.ToString();

                    invoiceTemp.Add(tempObj);
                }
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (FrameWork.WinForms.Classes.Function.ChooseItem(invoiceTemp, new string[] { "发票号", "患者姓名", "金额", "收费员工号", "收费时间" }, new bool[] { true, true, true, false, false, false }, new int[] { 100, 100, 100, 120,120 }, ref obj) != 1)
                {
                    return "";
                }

                return QueryRecipeNOByInvoiceNO(obj.ID);
            }

        }

        private void QueryByClincNo(string clinicCode)
        {
            FS.HISFC.Models.Registration.Register regObj = CacheManager.RegInterMgr.GetByClinic(clinicCode);
            if (regObj == null)
            {
                MessageBox.Show(CacheManager.RegInterMgr.Err);
                return;
            }

            #region 查询电子方
            ArrayList al = new ArrayList();

            ArrayList alSeeNo = CacheManager.OutOrderMgr.QuerySeeNoListByCardNo(regObj.ID, regObj.PID.CardNO);
            if (alSeeNo != null && alSeeNo.Count > 0)
            {
                ArrayList alTemp = new ArrayList();
                foreach (FS.FrameWork.Models.NeuObject seeObj in alSeeNo)
                {
                    alTemp = CacheManager.OutOrderMgr.QueryOrder(seeObj.ID);
                    if (alTemp != null)
                    {
                        al.AddRange(alTemp);
                    }
                }
            }

            FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.neuSpread1_Sheet1, al, FS.HISFC.Models.Base.ServiceTypes.C);
            #endregion

            #region 查询手工方
            this.neuSpread1_Sheet2.RowCount = 0;

            this.neuSpread1_Sheet2.Columns[0].Visible = false;
            this.neuSpread1_Sheet2.Columns[10].Visible = false;
            this.neuSpread1_Sheet2.Columns[12].Visible = false;
            this.neuSpread1_Sheet2.Columns[14].Visible = false;
            this.neuSpread1_Sheet2.Columns[17].Visible = false;

            ArrayList alFee = CacheManager.FeeIntegrate.QueryAllFeeItemListsByClinicNO(regObj.ID, "ALL", "0", "0");
            if (alFee != null && alFee.Count > 0)
            {
                this.neuSpread1_Sheet2.RowCount = alFee.Count;
                this.neuSpread1_Sheet2.Columns.Count = 18;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f = null;
                FS.HISFC.Models.Fee.Item.Undrug undrugObj = null;
                FS.HISFC.Models.Pharmacy.Item phaItemObj = null;

                for (int i = 0; i < alFee.Count; i++)
                {
                    f = alFee[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        phaItemObj = CacheManager.PhaIntegrate.GetItem(f.Item.ID);
                        if (phaItemObj == null)
                        {
                            MessageBox.Show(CacheManager.FeeIntegrate.Err);
                            return;
                        }
                        //自定义码
                        this.neuSpread1_Sheet2.Cells[i, 1].Text = phaItemObj.UserCode;
                        //项目名称
                        this.neuSpread1_Sheet2.Cells[i, 2].Text = f.Item.Name + "[" + phaItemObj.Specs + "]";
                    }
                    else
                    {
                        undrugObj = CacheManager.FeeIntegrate.GetItem(f.Item.ID);
                        if (undrugObj == null)
                        {
                            MessageBox.Show(CacheManager.FeeIntegrate.Err);
                            return;
                        }
                        //自定义码
                        this.neuSpread1_Sheet2.Cells[i, 1].Text = undrugObj.UserCode;
                        //项目名称
                        this.neuSpread1_Sheet2.Cells[i, 2].Text = f.Item.Name;
                    }
                    //项目编码
                    this.neuSpread1_Sheet2.Cells[i, 0].Text = f.Item.ID;
                    //组标记
                    this.neuSpread1_Sheet2.Cells[i, 3].Text = "";
                    //单位
                    this.neuSpread1_Sheet2.Cells[i, 6].Text = f.Item.PriceUnit;

                    if (f.FeePack == "1")//包装单位
                    {
                        this.neuSpread1_Sheet2.Cells[i, 4].Text = f.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.neuSpread1_Sheet2.Cells[i, 5].Text = (f.Item.Qty / f.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                    }
                    else
                    {
                        this.neuSpread1_Sheet2.Cells[i, 4].Text = (f.Item.Price / f.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.neuSpread1_Sheet2.Cells[i, 5].Text = (f.Item.Qty).ToString("F4").TrimEnd('0').TrimEnd('.');
                    }

                    //付数/天数
                    this.neuSpread1_Sheet2.Cells[i, 7].Text = f.Order.HerbalQty.ToString();
                    //用量
                    this.neuSpread1_Sheet2.Cells[i, 8].Text = f.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    //每次用量单位
                    this.neuSpread1_Sheet2.Cells[i, 9].Text = f.Order.DoseUnit;
                    //频次
                    this.neuSpread1_Sheet2.Cells[i, 10].Text = f.Order.Frequency.ID;
                    this.neuSpread1_Sheet2.Cells[i, 11].Text = f.Order.Frequency.Name;
                    //用法
                    this.neuSpread1_Sheet2.Cells[i, 12].Text = f.Order.Usage.ID;
                    this.neuSpread1_Sheet2.Cells[i, 13].Text = f.Order.Usage.Name;
                    //执行科室
                    this.neuSpread1_Sheet2.Cells[i, 14].Text = f.ExecOper.Dept.ID;
                    this.neuSpread1_Sheet2.Cells[i, 15].Text = f.ExecOper.Dept.Name;
                    //金额
                    this.neuSpread1_Sheet2.Cells[i, 16].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost ).ToString("F4").TrimEnd('0').TrimEnd('.');
                    //组合号
                    this.neuSpread1_Sheet2.Cells[i, 17].Text = f.Order.Combo.ID;
                }

                Components.Order.Classes.Function.DrawCombo(this.neuSpread1_Sheet2, 17, 3);
            }
            #endregion
        }

        /// <summary>
        /// 根据卡号查患者处方
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="dtBegin"></param>
        /// <param name="deEnd"></param>
        private void QueryByCardNO(string cardNO, DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList alRecipeNO = CacheManager.OutOrderMgr.QueryRecipeNOByCardNO(cardNO, dtBegin, dtEnd);
            if (alRecipeNO == null)
            {
                MessageBox.Show("根据患者卡号查找处方号失败" + CacheManager.OutOrderMgr.Err);
                return;
            }
            else if (alRecipeNO.Count == 0)
            {
                MessageBox.Show("在指定时间段内没有找到该患者的有效处方号");
                return;
            }
            else if (alRecipeNO.Count == 1)
            {
                this.QueryByRecipeNO((alRecipeNO[0] as FS.FrameWork.Models.NeuObject).ID);
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                if (FrameWork.WinForms.Classes.Function.ChooseItem(alRecipeNO, new string[] { "处方号", "开立医生", "开立时间" }, new bool[] { }, new int[] { 100, 80, 120 }, ref obj) != 1)
                {
                    return;
                }
                this.QueryByRecipeNO(obj.ID);
            }
        }

        /// <summary>
        /// 根据处方号查信息
        /// </summary>
        /// <param name="recipeNO"></param>
        private void QueryByRecipeNO(string recipeNO)
        {
            this.RecipeNO = recipeNO;
            #region 电子方

            ArrayList alOrder = CacheManager.OutOrderMgr.QueryOrderByRecipeNO(recipeNO);
            if (alOrder == null)
            {
                MessageBox.Show(CacheManager.OutOrderMgr.Err);
                return;
            }
            else if(alOrder.Count==0)
            {
                return ;
            }

            this.register = CacheManager.RegInterMgr.GetByClinic(((FS.HISFC.Models.Order.OutPatient.Order)alOrder[0]).Patient.ID);

            if (register == null)
            {
                MessageBox.Show(CacheManager.RegInterMgr.Err);
                return;
            }

            FS.HISFC.Components.Common.Classes.Function.ShowOrder(this.neuSpread1_Sheet1, alOrder, FS.HISFC.Models.Base.ServiceTypes.C);

            #endregion

            #region 查询手工方
            return;
            this.neuSpread1_Sheet2.RowCount = 0;

            this.neuSpread1_Sheet2.Columns[0].Visible = false;
            this.neuSpread1_Sheet2.Columns[10].Visible = false;
            this.neuSpread1_Sheet2.Columns[12].Visible = false;
            this.neuSpread1_Sheet2.Columns[14].Visible = false;
            this.neuSpread1_Sheet2.Columns[17].Visible = false;

            ArrayList alFee = CacheManager.FeeIntegrate.QueryAllFeeItemListsByClinicNO(recipeNO, "ALL", "0", "0");
            if (alFee != null && alFee.Count > 0)
            {
                this.neuSpread1_Sheet2.RowCount = alFee.Count;
                this.neuSpread1_Sheet2.Columns.Count = 18;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f = null;
                FS.HISFC.Models.Fee.Item.Undrug undrugObj = null;
                FS.HISFC.Models.Pharmacy.Item phaItemObj = null;

                for (int i = 0; i < alFee.Count; i++)
                {
                    f = alFee[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        phaItemObj = CacheManager.PhaIntegrate.GetItem(f.Item.ID);
                        if (phaItemObj == null)
                        {
                            MessageBox.Show(CacheManager.FeeIntegrate.Err);
                            return;
                        }
                        //自定义码
                        this.neuSpread1_Sheet2.Cells[i, 1].Text = phaItemObj.UserCode;
                        //项目名称
                        this.neuSpread1_Sheet2.Cells[i, 2].Text = f.Item.Name + "[" + phaItemObj.Specs + "]";
                    }
                    else
                    {
                        undrugObj = CacheManager.FeeIntegrate.GetItem(f.Item.ID);
                        if (undrugObj == null)
                        {
                            MessageBox.Show(CacheManager.FeeIntegrate.Err);
                            return;
                        }
                        //自定义码
                        this.neuSpread1_Sheet2.Cells[i, 1].Text = undrugObj.UserCode;
                        //项目名称
                        this.neuSpread1_Sheet2.Cells[i, 2].Text = f.Item.Name;
                    }
                    //项目编码
                    this.neuSpread1_Sheet2.Cells[i, 0].Text = f.Item.ID;
                    //组标记
                    this.neuSpread1_Sheet2.Cells[i, 3].Text = "";
                    //单位
                    this.neuSpread1_Sheet2.Cells[i, 6].Text = f.Item.PriceUnit;

                    if (f.FeePack == "1")//包装单位
                    {
                        this.neuSpread1_Sheet2.Cells[i, 4].Text = f.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.neuSpread1_Sheet2.Cells[i, 5].Text = (f.Item.Qty / f.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                    }
                    else
                    {
                        this.neuSpread1_Sheet2.Cells[i, 4].Text = (f.Item.Price / f.Item.PackQty).ToString("F4").TrimEnd('0').TrimEnd('.');
                        this.neuSpread1_Sheet2.Cells[i, 5].Text = (f.Item.Qty).ToString("F4").TrimEnd('0').TrimEnd('.');
                    }

                    //付数/天数
                    this.neuSpread1_Sheet2.Cells[i, 7].Text = f.Order.HerbalQty.ToString();
                    //用量
                    this.neuSpread1_Sheet2.Cells[i, 8].Text = f.Order.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    //每次用量单位
                    this.neuSpread1_Sheet2.Cells[i, 9].Text = f.Order.DoseUnit;
                    //频次
                    this.neuSpread1_Sheet2.Cells[i, 10].Text = f.Order.Frequency.ID;
                    this.neuSpread1_Sheet2.Cells[i, 11].Text = f.Order.Frequency.Name;
                    //用法
                    this.neuSpread1_Sheet2.Cells[i, 12].Text = f.Order.Usage.ID;
                    this.neuSpread1_Sheet2.Cells[i, 13].Text = f.Order.Usage.Name;
                    //执行科室
                    this.neuSpread1_Sheet2.Cells[i, 14].Text = f.ExecOper.Dept.ID;
                    this.neuSpread1_Sheet2.Cells[i, 15].Text = f.ExecOper.Dept.Name;
                    //金额
                    this.neuSpread1_Sheet2.Cells[i, 16].Text = (f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost ).ToString("F4").TrimEnd('0').TrimEnd('.');
                    //组合号
                    this.neuSpread1_Sheet2.Cells[i, 17].Text = f.Order.Combo.ID;
                }

                Components.Order.Classes.Function.DrawCombo(this.neuSpread1_Sheet2, 17, 3);
            }
            #endregion
        }

        /// <summary>
        /// 根据文本查卡号
        /// </summary>
        /// <param name="cardNo"></param>
        private string QueryCardNO(string cardNo)
        {
            //FS.HISFC.BizProcess.Integrate.IGetCardNOByInputCardNO iCardNO =
            //         FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(),
            //         typeof(FS.HISFC.BizProcess.Integrate.Registration.IGetCardNOByInputCardNO)) as FS.HISFC.BizProcess.Integrate.Registration.IGetCardNOByInputCardNO;

            string cardNO = string.Empty;
            //if (iCardNO != null)
            //{
            //    string physicalTypeID = string.Empty;
            //    string errText = string.Empty;

            //    int returnValue = iCardNO.GetCardNOByInputCardNO(text, ref cardNO, ref physicalTypeID, ref errText);
            //    if (returnValue < 0)
            //    {
            //        MessageBox.Show("获得患者就诊卡号规则出错!" + errText);
            //        return null;
            //    }
            //}

            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            int rev = CacheManager.FeeIntegrate.ValidMarkNO(cardNo.Trim(), ref accountCard);

            if (rev > 0)
            {
                cardNO = accountCard.Patient.PID.CardNO;
            }
            return cardNO;
        }

        private void Init()
        {
            InitControl();

            this.tabControl1.Controls.Remove(this.tabPage2);
            this.neuSpread1_Sheet2.Visible = false;

            InitOrderPrint();
        }

        private void InitControl()
        {
            DateTime dtNow = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
            this.dtpBegin.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 0, 0, 0, 0);
            this.dtpEnd.Value = new DateTime(dtNow.Year, dtNow.Month, dtNow.Day, 23, 59, 59, 999);

            this.neuSpread1_Sheet2.RowCount = 0;
            this.neuSpread1_Sheet2.DefaultStyle.Locked = true;
            this.neuSpread1_Sheet2.Columns[0, 2].Visible = false;
            this.neuSpread1_Sheet2.Columns[9, 11].Visible = false;
            this.neuSpread1_Sheet2.Columns[21, 22].Visible = false;
            this.neuSpread1_Sheet2.Columns[25].Visible = false;
            this.neuSpread1_Sheet2.Columns[28].Visible = false;
            this.neuSpread1_Sheet2.Columns[30, 31].Visible = false;
            this.neuSpread1_Sheet2.Columns[33, 35].Visible = false;
            this.neuSpread1_Sheet2.Columns[41, this.neuSpread1_Sheet2.Columns.Count - 1].Visible = false;
            this.neuSpread1_Sheet2.Columns[56].Visible = true;//处方号


            #region sheet label

            this.neuSpread1_Sheet2.Columns[3].Label = "病历号";
            this.neuSpread1_Sheet2.Columns[4].Label = "挂号日期";
            this.neuSpread1_Sheet2.Columns[5].Label = "科室";
            this.neuSpread1_Sheet2.Columns[6].Label = "项目编码";
            this.neuSpread1_Sheet2.Columns[7].Label = "项目名";
            this.neuSpread1_Sheet2.Columns[8].Label = "规格";
            this.neuSpread1_Sheet2.Columns[12].Label = "单价";
            this.neuSpread1_Sheet2.Columns[13].Label = "数量";
            this.neuSpread1_Sheet2.Columns[14].Label = "天数";
            this.neuSpread1_Sheet2.Columns[15].Label = "包装数量";
            this.neuSpread1_Sheet2.Columns[16].Label = "单位";
            this.neuSpread1_Sheet2.Columns[17].Label = "个人支付";
            this.neuSpread1_Sheet2.Columns[18].Label = "账户支付";
            this.neuSpread1_Sheet2.Columns[19].Label = "公费支付";
            this.neuSpread1_Sheet2.Columns[20].Label = "基本剂量";
            this.neuSpread1_Sheet2.Columns[23].Label = "每次用量";
            this.neuSpread1_Sheet2.Columns[24].Label = "剂量单位";
            this.neuSpread1_Sheet2.Columns[26].Label = "频次";
            this.neuSpread1_Sheet2.Columns[27].Label = "频次名";
            this.neuSpread1_Sheet2.Columns[29].Label = "用法";
            this.neuSpread1_Sheet2.Columns[32].Label = "执行科室";
            this.neuSpread1_Sheet2.Columns[36].Label = "院注次数";
            this.neuSpread1_Sheet2.Columns[37].Label = "备注";
            this.neuSpread1_Sheet2.Columns[38].Label = "医生编码";
            this.neuSpread1_Sheet2.Columns[39].Label = "医生名";
            this.neuSpread1_Sheet2.Columns[40].Label = "医生科室";
            this.neuSpread1_Sheet2.Columns[56].Label = "划价日期";

            #endregion

            this.lblRecipeNO.Text = "当前处方号: ";            
        }

        /// <summary>
        /// 清屏
        /// </summary>
        private void Clear()
        {
            this.lblRecipeNO.Text = "当前处方号：";
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread1_Sheet2.RowCount = 0;
        }

        #region 设置打印机

        /// <summary>
        /// 初始化基础参数
        /// </summary>
        private void InitOrderPrint()
        {
            //获取系统打印机列表
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                //取打印机名
                string name = System.Text.RegularExpressions.Regex.Replace(PrinterSettings.InstalledPrinters[i], @"在(\s|\S)*上", "").Replace("自动", "");
                this.tbPrinter.Items.Add(name);
            }

            this.tbPrinter.SelectedIndexChanged += new EventHandler(tbPrinter_SelectedIndexChanged);

            //从XML读默认设置
            if (File.Exists(fileName))
            {
                XmlDocument file = new XmlDocument();
                file.Load(fileName);
                XmlNode node = file.SelectSingleNode("ORDERPRINT/医嘱单");
                if (node != null)
                {
                    this.printerName = node.InnerText;
                }
            }

            for (int i = 0; i < this.tbPrinter.Items.Count; i++)
            {
                if (this.tbPrinter.Items[i].ToString().Contains(this.printerName))
                {
                    this.tbPrinter.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// 选择打印机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbPrinter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.printerName = tbPrinter.SelectedItem.ToString();

            this.SetPrintValue("医嘱单", this.printerName);

            if (!string.IsNullOrEmpty(this.printerName))
            {
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    if (PrinterSettings.InstalledPrinters[i] != null && PrinterSettings.InstalledPrinters[i].ToString().Contains(this.printerName))
                    {
                        printer = PrinterSettings.InstalledPrinters[i].ToString();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置打印机
        /// </summary>
        private void SetPrintValue(string nodeName, string nodeValue)
        {
            FS.FrameWork.Xml.XML xml = new FS.FrameWork.Xml.XML();
            if (!File.Exists(fileName))
            {
                XmlDocument doc = new XmlDocument();
                xml.CreateRootElement(doc, "ORDERPRINT");
                xml.AddXmlNode(doc, doc.DocumentElement, nodeName, nodeValue);
                doc.Save(fileName);
            }
            else
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                XmlNode node = doc.SelectSingleNode("ORDERPRINT/" + nodeName);
                if (node != null)
                {
                    node.InnerText = nodeValue;
                }
                else
                {
                    if (nodeValue != null)
                    {
                        xml.AddXmlNode(doc, doc.DocumentElement, nodeName, nodeValue);
                    }
                }
                doc.Save(fileName);
            }
        }

        #endregion

        #endregion

        #region 事件

        private void lblFilter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            switch ((sender as FS.FrameWork.WinForms.Controls.NeuLinkLabel).Text)
            {
                case "处方号(F1切换)":
                    lblFilter.Text = "患者卡号(F1切换)";
                    SetDateControlVisible(true);
                    break;
                case "患者卡号(F1切换)":
                    lblFilter.Text = "发票号(F1切换)";
                    SetDateControlVisible(false);
                    break;
                case "发票号(F1切换)":
                    lblFilter.Text = "处方号(F1切换)";
                    SetDateControlVisible(false);
                    break;


                //case "处方号(F1切换)":
                //    lblFilter.Text = "患者卡号(F1切换)";
                //    SetDateControlVisible(true);
                //    break;
                //case "患者卡号(F1切换)":
                //    lblFilter.Text = "发票号(F1切换)";
                //    SetDateControlVisible(false);
                //    break;
                //case "发票号(F1切换)":
                //    lblFilter.Text = "姓名(F1切换)";
                //    SetDateControlVisible(true);
                //    break;
                //case "姓名(F1切换)" :
                //    lblFilter.Text = "患者卡号(F1切换)";
                //    SetDateControlVisible(true) ;
                //    break ;
                default:
                    break;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //切换查询条件
            if (keyData == Keys.F1)
            {
                lblFilter_LinkClicked(this.lblFilter, null);
            }
            return base.ProcessDialogKey(keyData);
        }

        private void tbArg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                this.Query();
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        public override int Print(object sender, object neuObject)
        {
            //FS.HISFC.BizProcess.Interface.IRecipePrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(UFC.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.IRecipePrint)) as FS.HISFC.BizProcess.Interface.IRecipePrint;
            //if (o == null)
            //{
            //    MessageBox.Show("处方打印接口未实现");
            //    return -1;
            //}
            //else
            //{
            //    o.RecipeNO = recipeNO;
            //    o.SetPatientInfo(this.register);

            //    o.PrintRecipe();
            //}
            return 1;
        }

        private void ucRecipeReprint_Load(object sender, EventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 打印普通处方
        /// </summary>
        /// <param name="isPreview">是否预览</param>
        /// <returns></returns>
        private int PrintRecipe(bool isPreview)
        {
            FS.HISFC.BizProcess.Interface.IRecipePrint o = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.IRecipePrint)) as FS.HISFC.BizProcess.Interface.IRecipePrint;
            if (o == null)
            {
                MessageBox.Show("处方打印接口未实现");
                return -1;
            }
            else
            {
                if (this.register == null || string.IsNullOrEmpty(this.register.ID) || string.IsNullOrEmpty(this.recipeNo))
                {
                    MessageBox.Show("没有选择处方！");
                    return -1;
                }

                o.SetPatientInfo(this.register);
                o.RecipeNO = recipeNo;
                o.Printer = printer;

                if (isPreview)
                {
                    o.PrintRecipeView(new ArrayList());//接口为何如此定义？
                }
                else
                {
                    o.PrintRecipe();
                }
            }
            return 1;
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode.Nodes.Count > 0)
                return;

            string recipeNO = this.treeView1.SelectedNode.Tag.ToString();

            //this.ucRecipePrintNew1.RecipeNO = recipeNO;
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{    
    /// <summary>
    /// [功能描述:合同单位维护的费用对照显示界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-2]<br></br>
    /// 说明：
    /// </summary>
     partial class ucFeeCodeRateDetail : FS.FrameWork.WinForms.Controls.ucBaseControl,Fee.Interface.Components.IPactFeeCodeRateDetail
    {
        public ucFeeCodeRateDetail()
        {
            InitializeComponent();
        }

        #region 域变量

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactItemRate>> selectedPactItemRateChange;
        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander filterTextChanged;

        /// <summary>
        /// 药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = new DataTable();

        /// <summary>
        /// 合同单位信息
        /// </summary>
        private System.Data.DataTable dtPacts = new DataTable();

        /// <summary>
        /// 存储变量
        /// </summary>
        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        /// <summary>
        /// 合同单位HS
        /// </summary>
        private System.Collections.Hashtable hsPacts = new Hashtable();

        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = "";

        /// <summary>
        /// 合同单位的配置文件
        /// </summary>
        private string settingPactFile = "";

        private string filter = "1=1";

        private List<FS.HISFC.Models.Base.PactInfo> pactInfoList = null;
        private string pactCodes = "''";

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

        #endregion

        #region 初始化

        private void initFarPoint()
        {
            if (!this.neuSpread.ReadSchema(this.settingFile))
            {
                this.fpItemGroup.ColumnHeader.Rows[0].Height = 34f;
            }
            this.fpItemGroup.DataAutoSizeColumns = false;

            if (!this.neuSpreadPact.ReadSchema(this.settingPactFile))
            {
                this.fpItemPact.ColumnHeader.Rows[0].Height = 34f;
            }
            this.fpItemPact.DataAutoSizeColumns = false;
        }

        private void initBaseData()
        {
            //FS.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRateMgr = new FS.SOC.HISFC.Fee.BizLogic.PactItemRate();
            ////合同单位
            //List<FS.HISFC.Models.Base.PactItemRate> pactList = pactItemRateMgr.QueryGroupByItem();

            //if (pactList == null)
            //{
            //    throw new Exception(pactItemRateMgr.Err);
            //}


            //this.loadBaseData(pactList);
        }

        private void initBaseData(string pactCode)
        {
            FS.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRateMgr = new FS.SOC.HISFC.Fee.BizLogic.PactItemRate();
            //合同单位
            List<FS.HISFC.Models.Base.PactItemRate> pactList = pactItemRateMgr.QueryGroupByItem(pactCode);

            if (pactList == null)
            {
                throw new Exception(pactItemRateMgr.Err);
            }

            this.loadBaseData(pactList);
        }

        private void loadBaseData(List<FS.HISFC.Models.Base.PactItemRate> pactList)
        {

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            Application.DoEvents();
            int progress = 1;
            int count = pactList.Count;
            //this.dtItems.Clear();
            //this.hsItem.Clear();
            Hashtable hsTemp = new Hashtable();
            this.setReadOnly(this.dtItems, false);
            foreach (FS.HISFC.Models.Base.PactItemRate item in pactList)
            {
                if (!hsTemp.Contains(item.PactItem.ID))
                {
                    hsTemp.Add(item.PactItem.ID, item);
                }
                //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, count);
                if (this.addItemObjectToDtItem(item,true) != 1)
                {
                    continue;
                }
            }
            ArrayList al = new ArrayList(hsItem.Keys);

            foreach (string key in al)
            {
                if (!hsTemp.ContainsKey(key))
                {
                    hsItem.Remove(key);
                    dtItems.Rows.Remove(this.dtItems.Rows.Find(key));
                }
            }
            this.setReadOnly(this.dtItems, true);
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.dtItems.AcceptChanges();
            //this.fpItemGroup.DataSource = this.dtItems;

            this.neuSpread_SelectionChanged(null, null);
        }

        private void initDataTable()
        {
            #region dtItems

            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型
            this.dtItems.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn("fee_code", typeof(string)),
                                                   new DataColumn("项目编号", typeof(string)),
                                                   new DataColumn("自定义码", typeof(string)),
                                                   new DataColumn("项目名称", typeof(string)),
                                                   new DataColumn("拼音码", typeof(string)),
                                                   new DataColumn("五笔码", typeof(string))
            });


            foreach (DataColumn dc in this.dtItems.Columns)
            {
                dc.ReadOnly = true;
            }



            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["项目编号"];

            this.dtItems.PrimaryKey = keys;
            this.fpItemGroup.DataAutoSizeColumns = false;
            this.fpItemGroup.DataSource = this.dtItems.DefaultView;

            this.dtItems.DefaultView.AllowEdit = true;
            this.dtItems.DefaultView.AllowNew = true;
            this.dtItems.DefaultView.AllowDelete = true;

            this.dtItems.AcceptChanges();

            #endregion

            #region dtPacts

            if (this.dtPacts == null)
            {
                this.dtPacts = new DataTable();
            }

            //定义类型
            this.dtPacts.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn("维护", typeof(bool)),
                                                    new DataColumn("合同编号", typeof(string)),
                                                    new DataColumn("合同名称", typeof(string)),
                                                    new DataColumn("项目编号", typeof(string)),
                                                    new DataColumn("项目名称", typeof(string)),
                                                    new DataColumn("公费比例",typeof(decimal)),
                                                    new DataColumn("自费比例",typeof(decimal)),
                                                    new DataColumn("自付比例",typeof(decimal)),
                                                    new DataColumn("优惠比例",typeof(decimal)),
                                                    new DataColumn("欠费比例",typeof(decimal)),
                                                    new DataColumn("限额",typeof(decimal)),
                                                    new DataColumn("拼音码",typeof(string)),
                                                    new DataColumn("五笔码",typeof(string))
            });


            foreach (DataColumn dc in this.dtPacts.Columns)
            {
                if (dc.ColumnName.Equals("维护")
                    || dc.ColumnName.Equals("公费比例")
                    || dc.ColumnName.Equals("自费比例")
                    || dc.ColumnName.Equals("自付比例")
                    || dc.ColumnName.Equals("优惠比例")
                    || dc.ColumnName.Equals("欠费比例")
                    || dc.ColumnName.Equals("限额")
                     )
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = true;
                }
            }



            keys = new DataColumn[1];

            keys[0] = this.dtPacts.Columns["合同编号"];

            this.dtPacts.PrimaryKey = keys;
            this.fpItemPact.DataAutoSizeColumns = false;
            this.dtPacts.DefaultView.AllowEdit = true;
            this.dtPacts.DefaultView.AllowNew = true;
            this.dtPacts.DefaultView.AllowDelete = true;
            this.fpItemPact.DataSource = this.dtPacts.DefaultView;

            this.dtPacts.AcceptChanges();

            #endregion
        }

        private void initEvents()
        {
            this.txtCustomCodeForItem.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.txtCustomCodeForItem.TextChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.txtCustomCodeForPact.TextChanged -= new EventHandler(txtCustomCodeForPact_TextChanged);
            this.txtCustomCodeForPact.TextChanged += new EventHandler(txtCustomCodeForPact_TextChanged);

            this.neuSpread.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);
            this.neuSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);

            this.neuSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);

            this.neuSpreadPact.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpreadPact_ColumnWidthChanged);
            this.neuSpreadPact.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpreadPact_ColumnWidthChanged);

            this.fpItemPact.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpItemPact_CellChanged);
            this.fpItemPact.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpItemPact_CellChanged);

            this.neuSpreadPact.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpreadPact_ButtonClicked);
            this.neuSpreadPact.ButtonClicked+=new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpreadPact_ButtonClicked);

            this.neuSpreadPact.CellClick -= new FarPoint.Win.Spread.CellClickEventHandler(neuSpreadPact_CellClick);
            this.neuSpreadPact.CellClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpreadPact_CellClick);

            this.ckAllPact.CheckedChanged -= new EventHandler(ckAllPact_CheckedChanged);
            this.ckAllPact.CheckedChanged += new EventHandler(ckAllPact_CheckedChanged);

            this.rbtnMinFee.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnMinFee.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnItem.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnItem.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnPharmacy.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnPharmacy.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnAll.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnAll.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);

            this.btnAddPact.Click -= new EventHandler(btnAddPact_Click);
            this.btnAddPact.Click += new EventHandler(btnAddPact_Click);

            this.btnDeletePact.Click -= new EventHandler(btnDeletePact_Click);
            this.btnDeletePact.Click += new EventHandler(btnDeletePact_Click);

            this.btnAddItem.Click -= new EventHandler(btnAddItem_Click);
            this.btnAddItem.Click += new EventHandler(btnAddItem_Click);

            this.btnSavePact.Click -= new EventHandler(btnSavePact_Click);
            this.btnSavePact.Click += new EventHandler(btnSavePact_Click);
        }

        void fpItemPact_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            //this.modifySelectedItemRate();
        }

        #endregion

        #region 数据显示

        private void setReadOnly(DataTable dt ,bool isReadOnly)
        {
            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.Equals("维护")
                    || dc.ColumnName.Equals("公费比例")
                    || dc.ColumnName.Equals("自费比例")
                    || dc.ColumnName.Equals("自付比例")
                    || dc.ColumnName.Equals("优惠比例")
                    || dc.ColumnName.Equals("欠费比例")
                    || dc.ColumnName.Equals("限额")
                     )
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = isReadOnly;
                }
            }

            if (isReadOnly)
            {
                this.fpItemPact.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpItemPact_CellChanged);
                this.fpItemPact.CellChanged += new FarPoint.Win.Spread.SheetViewEventHandler(fpItemPact_CellChanged);
            }
            else
            {
                this.fpItemPact.CellChanged -= new FarPoint.Win.Spread.SheetViewEventHandler(fpItemPact_CellChanged);
            }
        }

        private int addItemObjectToDtPact(FS.HISFC.Models.Base.PactItemRate item)
        {
            return this.addItemObjectToDtPact(item, false);
        }

        private int addItemObjectToDtPact(FS.HISFC.Models.Base.PactItemRate item,bool isModify)
        {
            if (item == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：合同单位基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtPacts == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.hsPacts.Contains(item.ID))
            {
                if (isModify)
                {
                    this.hsPacts[item.ID] = item;
                }
                else
                {
                    CommonController.CreateInstance().MessageBox("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                this.hsPacts.Add(item.ID, item);
            }

            this.setPactDataRowValue(item);


            return 1;
        }

        private int addItemObjectToDtItem(FS.HISFC.Models.Base.PactItemRate item)
        {
            return this.addItemObjectToDtItem(item, false);
        }
            
        private int addItemObjectToDtItem(FS.HISFC.Models.Base.PactItemRate item,bool isModify)
        {
            if (item == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：合同单位基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtItems == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.hsItem.Contains(item.PactItem.ID))
            {
                if (isModify)
                {
                    this.hsItem[item.PactItem.ID] = item;
                }
                else
                {
                    CommonController.CreateInstance().MessageBox("编码：" + item.PactItem.ID + " 名称：" + item.PactItem.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                this.hsItem.Add(item.PactItem.ID, item);
            }

           

            this.setItemDataRowValue(item);


            return 1;
        }

        private int setItemDataRowValue(FS.HISFC.Models.Base.PactItemRate pactItemRate)
        {
            DataRow row = this.dtItems.Rows.Find(pactItemRate.PactItem.ID);
            if (row == null)
            {
                row = this.dtItems.NewRow();
                row["项目编号"] = pactItemRate.PactItem.ID;
                this.dtItems.Rows.Add(row);
            }

            row["fee_code"] = pactItemRate.ItemType;
            row["项目名称"] = pactItemRate.PactItem.Name;
            row["自定义码"] = string.IsNullOrEmpty(pactItemRate.UserCode)? pactItemRate.PactItem.ID:pactItemRate.UserCode;
            row["拼音码"] = pactItemRate.SpellCode;
            row["五笔码"] = pactItemRate.WBCode;
            return 1;
        }

        private int setPactDataRowValue(FS.HISFC.Models.Base.PactItemRate pactItemRate)
        {
            DataRow row = this.dtPacts.Rows.Find(pactItemRate.ID);
            if (row == null)
            {
                row = this.dtPacts.NewRow();
                row["合同编号"] = pactItemRate.ID;
                this.dtPacts.Rows.Add(row);
            }

            row["维护"] = this.ckAllPact.Checked;
            row["项目编号"] = pactItemRate.PactItem.ID;
            row["项目名称"] = pactItemRate.PactItem.Name;
            row["合同名称"] = pactItemRate.Name;
            row["公费比例"] = pactItemRate.Rate.PubRate;
            row["自费比例"] = pactItemRate.Rate.OwnRate;
            row["自付比例"] = pactItemRate.Rate.PayRate;
            row["优惠比例"] = pactItemRate.Rate.RebateRate;
            row["欠费比例"] = pactItemRate.Rate.ArrearageRate;
            row["限额"] = pactItemRate.Rate.Quota;
            row["拼音码"] = pactItemRate.SpellCode;
            row["五笔码"] = pactItemRate.WBCode;
            return 1;
        }

        private void selectPact()
        {
            //查找项目对应的合同单位信息
            string feeCode = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
            if (this.hsItem.Contains(feeCode))
            {
                FS.HISFC.Models.Base.PactItemRate undrug = this.hsItem[feeCode] as FS.HISFC.Models.Base.PactItemRate;
                
                //查找对应的合同单位
                FS.SOC.HISFC.Fee.BizLogic.PactItemRate pactItemRateMgr = new FS.SOC.HISFC.Fee.BizLogic.PactItemRate();
                List<FS.HISFC.Models.Base.PactItemRate> list = pactItemRateMgr.QueryByItem(undrug.ItemType, undrug.PactItem.ID,this.pactCodes);
                if (list != null)
                {
                    this.hsPacts.Clear();
                    this.dtPacts.Clear();
                    this.setReadOnly(this.dtPacts, false);
                    foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in list)
                    {
                        pactItemRate.PactItem.ID = undrug.PactItem.ID;
                        pactItemRate.PactItem.Name = undrug.PactItem.Name;

                        this.addItemObjectToDtPact(pactItemRate);
                    }
                    this.setReadOnly(this.dtPacts, true);

                    this.dtPacts.AcceptChanges();
                }
            }

            this.modifySelectedItemRate();
            this.showModifyInfo();

        }

        private void modifySelectedItemRate()
        {
            List<FS.HISFC.Models.Base.PactItemRate> list = new List<FS.HISFC.Models.Base.PactItemRate>();
            for (int i = 0; i < this.fpItemPact.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpreadPact.GetCellText(0, i, "维护")))
                {
                    string feeCode = this.neuSpreadPact.GetCellText(0, i, "合同编号");
                    if (this.hsPacts.Contains(feeCode))
                    {
                        FS.HISFC.Models.Base.PactItemRate pactItemRate = (this.hsPacts[feeCode] as FS.HISFC.Models.Base.PactItemRate).Clone();
                        pactItemRate.Rate.PubRate = Convert.ToDecimal(this.fpItemPact.Cells[i,this.neuSpreadPact.GetColumnIndex(0,"公费比例")].Text.Trim());
                        pactItemRate.Rate.OwnRate = Convert.ToDecimal(this.fpItemPact.Cells[i,this.neuSpreadPact.GetColumnIndex(0,"自费比例")].Text.Trim());
                        pactItemRate.Rate.PayRate = Convert.ToDecimal(this.fpItemPact.Cells[i,this.neuSpreadPact.GetColumnIndex(0,"自付比例")].Text.Trim());
                        pactItemRate.Rate.RebateRate = Convert.ToDecimal(this.fpItemPact.Cells[i,this.neuSpreadPact.GetColumnIndex(0,"优惠比例")].Text.Trim());
                        pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(this.fpItemPact.Cells[i,this.neuSpreadPact.GetColumnIndex(0,"欠费比例")].Text.Trim());
                        pactItemRate.Rate.Quota = Convert.ToDecimal(this.fpItemPact.Cells[i,this.neuSpreadPact.GetColumnIndex(0,"限额")].Text.Trim());
                        list.Add(pactItemRate);
                    }
                }
            }

            if (list.Count == 0)
            {
                if (this.fpItemPact.ActiveRowIndex >= 0&&this.fpItemPact.RowCount>0)
                {
                    string feeCode = this.neuSpreadPact.GetCellText(0, this.fpItemPact.ActiveRowIndex, "合同编号");
                    int i = this.fpItemPact.ActiveRowIndex;
                    if (this.hsPacts.Contains(feeCode))
                    {
                        FS.HISFC.Models.Base.PactItemRate pactItemRate = (this.hsPacts[feeCode] as FS.HISFC.Models.Base.PactItemRate).Clone();
                        pactItemRate.Rate.PubRate = Convert.ToDecimal(this.fpItemPact.Cells[i, this.neuSpreadPact.GetColumnIndex(0, "公费比例")].Text.Trim());
                        pactItemRate.Rate.OwnRate = Convert.ToDecimal(this.fpItemPact.Cells[i, this.neuSpreadPact.GetColumnIndex(0, "自费比例")].Text.Trim());
                        pactItemRate.Rate.PayRate = Convert.ToDecimal(this.fpItemPact.Cells[i, this.neuSpreadPact.GetColumnIndex(0, "自付比例")].Text.Trim());
                        pactItemRate.Rate.RebateRate = Convert.ToDecimal(this.fpItemPact.Cells[i, this.neuSpreadPact.GetColumnIndex(0, "优惠比例")].Text.Trim());
                        pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(this.fpItemPact.Cells[i, this.neuSpreadPact.GetColumnIndex(0, "欠费比例")].Text.Trim());
                        pactItemRate.Rate.Quota = Convert.ToDecimal(this.fpItemPact.Cells[i, this.neuSpreadPact.GetColumnIndex(0, "限额")].Text.Trim());
                        list.Add(pactItemRate);
                    }
                }
            }

            if (this.selectedPactItemRateChange != null )
            {
                this.selectedPactItemRateChange(list);
            }

            this.showModifyInfo();
        }

        private void showModifyInfo()
        {
            int ColumnIndex1 = this.neuSpreadPact.GetColumnIndex(0, "维护");
            int ColumnIndex2 = this.neuSpreadPact.GetColumnIndex(0, "合同名称");
            string modifyInfo = "";
            for (int i = 0; i < this.fpItemPact.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpItemPact.Cells[i, ColumnIndex1].Value))
                {
                    modifyInfo += this.fpItemPact.Cells[i, ColumnIndex2].Text + ";";
                }
            }
            this.lbModifyInfo.Text = "正在维护：" + modifyInfo;
        }

        private void addNewPactItemRate(string feeCode)
        {
            if (this.pactInfoList != null)
            {
                if (this.hsItem.Contains(feeCode))
                {
                    FS.HISFC.Models.Base.PactItemRate undrug = this.hsItem[feeCode] as FS.HISFC.Models.Base.PactItemRate;

                    FS.HISFC.Models.Base.PactItemRate newPactItemRate = null;
                    this.setReadOnly(this.dtPacts, false);
                    foreach (FS.HISFC.Models.Base.PactInfo pactInfo in this.pactInfoList)
                    {
                        newPactItemRate = undrug.Clone();
                        newPactItemRate.ID = pactInfo.ID;
                        newPactItemRate.Name = pactInfo.Name;
                        newPactItemRate.Rate = new FS.HISFC.Models.Base.FTRate();
                        newPactItemRate.Rate.OwnRate = 1.00M;
                        this.addItemObjectToDtPact(newPactItemRate, true);
                    }
                    this.setReadOnly(this.dtPacts, true);

                    this.modifySelectedItemRate();
                }
                this.fpItemPact.ActiveRowIndex = this.fpItemPact.RowCount - 1;
                this.neuSpreadPact.ShowRow(0, this.fpItemPact.ActiveRowIndex, FarPoint.Win.Spread.VerticalPosition.Center);

            }
        }

        #endregion

        #region 过滤

        private void filterItem()
        {
            this.filter = Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.txtCustomCodeForItem.Text + "%");

            filter = "(" + filter + ")";

            if (this.rbtnMinFee.Checked)
            {
                filter = Function.ConnectFilterStr(filter, "fee_code = '0'", "and");
            }
            else if (this.rbtnPharmacy.Checked)
            {
                filter = Function.ConnectFilterStr(filter, "fee_code = '1'", "and");
            }
            else if (this.rbtnItem.Checked)
            {
                filter = Function.ConnectFilterStr(filter, "fee_code = '2'", "and");
            }
            else if (this.rbtnAll.Checked)
            {

                filter = Function.ConnectFilterStr(filter, "fee_code like '%'", "and");
            }


            this.dtItems.DefaultView.RowFilter = filter;
            this.neuSpread.ReadSchema(this.settingFile);

            if (this.filterTextChanged != null)
            {
                this.filterTextChanged();
            }

        }

        private void filterPact()
        {
            this.filter = Function.GetFilterStr(this.dtPacts.DefaultView, "%" + this.txtCustomCodeForPact.Text + "%");
            filter = Function.ConnectFilterStr(filter, string.Format("合同编号 like '%{0}%'", this.txtCustomCodeForPact.Text), "or");
            filter = Function.ConnectFilterStr(filter, string.Format("合同名称 like '%{0}%'", this.txtCustomCodeForPact.Text), "or");
            filter = "(" + filter + ")";

            this.dtPacts.DefaultView.RowFilter = filter;
            this.neuSpreadPact.ReadSchema(this.settingPactFile);

            if (this.filterTextChanged != null)
            {
                this.filterTextChanged();
            }

        }

        #endregion

        #region 事件触发

        void nTxtCustomCode_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
            this.neuSpread_SelectionChanged(sender,null);
            this.txtCustomCodeForItem.Focus();
        }

        void neuSpread_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.Valid())
            {
                if (this.Save() < 0)
                {
                    return;
                }
            }

            if (this.fpItemGroup.ActiveRowIndex >= 0 && this.fpItemGroup.RowCount > 0)
            {
                this.selectPact();
            }
            else
            {
                this.dtPacts.Clear();
                this.showModifyInfo();
            }
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void txtCustomCodeForPact_TextChanged(object sender, EventArgs e)
        {
            this.filterPact();
        }

        void neuSpreadPact_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.neuSpreadPact.StopCellEditing();
            this.modifySelectedItemRate();
            if (this.dtPacts != null && this.dtPacts.GetChanges() == null)
            {
                this.dtPacts.AcceptChanges();
            }
        }

        void neuSpreadPact_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpreadPact.SaveSchema(this.settingPactFile);
        }

        void ckAllPact_CheckedChanged(object sender, EventArgs e)
        {
            int i=this.neuSpreadPact.GetColumnIndex(0,"维护");
            if (this.fpItemPact.RowCount > 0)
            {
                this.fpItemPact.Cells[0, i, this.fpItemPact.RowCount - 1, i].Value = this.ckAllPact.Checked;
            }
            if (this.dtPacts != null && this.dtPacts.GetChanges() == null)
            {
                this.dtPacts.AcceptChanges();
            }
            this.modifySelectedItemRate();
            this.showModifyInfo();
        }

        void ckMinFee_CheckedChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton)
            {
                if (((RadioButton)sender).Checked)
                {
                    this.filterItem();
                    this.neuSpread_SelectionChanged(null, null);
                }
            }
        }

        void neuSpreadPact_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            this.modifySelectedItemRate();
        }

        void btnAddPact_Click(object sender, EventArgs e)
        {
            //判断权限
            if (!Function.JugePrive("0802", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位对照维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            if (this.fpItemGroup.RowCount > 0 && this.fpItemGroup.ActiveRowIndex >= 0)
            {
                string feeCode = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
                this.addNewPactItemRate(feeCode);
            }

        }

        void btnDeletePact_Click(object sender, EventArgs e)
        {
            //判断权限
            if (!Function.JugePrive("0802", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位对照维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            this.DeleteItem();  
        }

        void btnAddItem_Click(object sender, EventArgs e)
        {
            //判断权限
            if (!Function.JugePrive("0802", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位对照维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            ArrayList al =new ArrayList();
            if (this.rbtnAll.Checked)
            {
                //加载所有数据
               al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.MINFEE);
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
                   alItems.AddRange(itemManager.QueryBriefList("1"));
                   foreach (FS.SOC.HISFC.Fee.Models.Undrug undrug in alItems)
                   {
                       undrug.Memo = undrug.Specs;
                   }
               }
               //项目
               al.AddRange(alItems);

               
            }
            else if (this.rbtnMinFee.Checked)
            {
                //只选择最小费用
                al = CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            }
            else if (this.rbtnItem.Checked)
            {
                //只选择项目
                if (alItems == null || alItems.Count == 0)
                {
                    FS.SOC.HISFC.Fee.BizLogic.Undrug itemManager = new FS.SOC.HISFC.Fee.BizLogic.Undrug();
                    alItems = itemManager.QueryBriefList("0");
                    alItems.AddRange(itemManager.QueryBriefList("1"));
                    foreach (FS.SOC.HISFC.Fee.Models.Undrug undrug in alItems)
                    {
                        undrug.Memo = undrug.Specs;
                    }
                }
                al = alItems;
            }
            else if (this.rbtnPharmacy.Checked)
            {
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

        void frmChoose_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            if (sender == null)
            {
                return;
            }

            FS.HISFC.Models.Base.PactItemRate pactItemRate = new FS.HISFC.Models.Base.PactItemRate();
            pactItemRate.PactItem = sender;
            if (sender is FS.SOC.HISFC.Fee.Models.Undrug)
            {
                pactItemRate.ItemType = "2";
                pactItemRate.UserCode = ((FS.SOC.HISFC.Fee.Models.Undrug)sender).UserCode;
            }
            else if (sender is FS.HISFC.Models.Pharmacy.Item)
            {
                pactItemRate.ItemType = "1";
                pactItemRate.UserCode = ((FS.HISFC.Models.Pharmacy.Item)sender).UserCode;
            }
            else
            {
                pactItemRate.ItemType = "0";
            }
            this.setReadOnly(this.dtItems, false);
            int i = this.addItemObjectToDtItem(pactItemRate);
            this.setReadOnly(this.dtItems, true);
            if (i > 0)
            {
                this.neuSpread.ShowRow(0, this.fpItemGroup.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
                this.fpItemGroup.ActiveRowIndex = this.fpItemGroup.RowCount - 1;
                this.neuSpread_SelectionChanged(null, null);
                this.addNewPactItemRate(pactItemRate.PactItem.ID);
            }
        }

        void btnSavePact_Click(object sender, EventArgs e)
        {
            //判断权限
            if (!Function.JugePrive("0802", "02"))
            {
                CommonController.CreateInstance().MessageBox("因为您没有合同单位对照维护权限，该操作已取消！", MessageBoxIcon.Information);
                return;
            }

            this.Save();
        }

        void neuSpreadPact_Change(object sender, FarPoint.Win.Spread.ChangeEventArgs e)
        {
            this.modifySelectedItemRate();
        }

        #endregion

        #region IPactFeeCodeRateDetail 成员

        public void AddItemRange(List<FS.HISFC.Models.Base.PactInfo> pactInfoList)
        {
            this.pactInfoList = pactInfoList;

            if (this.pactInfoList != null && this.pactInfoList.Count > 0)
            {
                string strPactCode = null;
                foreach (FS.HISFC.Models.Base.PactInfo pactInfo in this.pactInfoList)
                {
                    strPactCode += "'" + pactInfo.ID + "',";
                }
                strPactCode = strPactCode ?? "'',";
                this.pactCodes = strPactCode.Remove(strPactCode.Length - 1);
                this.initBaseData(this.pactCodes);
                this.filterItem();
            }

        }

        public void AddItemRange(List<FS.HISFC.Models.Base.PactItemRate> pactInfoList)
        {
            if (pactInfoList != null)
            {
                this.setReadOnly(this.dtPacts, false);
                this.setReadOnly(this.dtItems, false);
                foreach (FS.HISFC.Models.Base.PactItemRate pactItemRate in pactInfoList)
                {
                    this.addItemObjectToDtItem(pactItemRate,true);
                    this.addItemObjectToDtPact(pactItemRate,true);
                }
                this.setReadOnly(this.dtPacts, true);
                this.setReadOnly(this.dtItems, true);

                Application.DoEvents();
            }
        }

        public int DeleteItem()
        {
            if (this.dtPacts != null)
            {
                //提交
                this.neuSpreadPact.StopCellEditing();

                if (CommonController.CreateInstance().MessageBox("确认删除选择的项目？", MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }

                int num = 0;
                while (this.fpItemPact.RowCount > 0 && FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpreadPact.GetCellText(0, 0, "维护")) )
                {
                    num++;
                    this.fpItemPact.Rows.Remove(0, 1);
                }

                if (num == 0&&this.fpItemPact.ActiveRowIndex>=0)
                {
                    this.fpItemPact.Rows.Remove(this.fpItemPact.ActiveRowIndex, 1);
                }

                this.modifySelectedItemRate();
            }
            return 1;
        }

        public int Save()
        {
            this.neuSpreadPact.StopCellEditing();
            foreach (DataRow row in this.dtPacts.Rows)
            {
                row.EndEdit();
            }


            List<FS.HISFC.Models.Base.PactItemRate> listDelete = new List<FS.HISFC.Models.Base.PactItemRate>();
            //先删除
            DataTable dt = this.dtPacts.GetChanges(DataRowState.Deleted);

            if (dt != null)
            {
                dt.RejectChanges();

                foreach (DataRow row in dt.Rows)
                {
                    FS.HISFC.Models.Base.PactItemRate pactItemRate =hsPacts[row["合同编号"].ToString()] as FS.HISFC.Models.Base.PactItemRate;
                    listDelete.Add(pactItemRate);
                }
            }

            List<FS.HISFC.Models.Base.PactItemRate> listModify = new List<FS.HISFC.Models.Base.PactItemRate>();
            dt = this.dtPacts.GetChanges(DataRowState.Modified);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FS.HISFC.Models.Base.PactItemRate pactItemRate = hsPacts[row["合同编号"].ToString()] as FS.HISFC.Models.Base.PactItemRate;
                    pactItemRate.Rate.PubRate = Convert.ToDecimal(row["公费比例"].ToString().Trim());
                    pactItemRate.Rate.OwnRate = Convert.ToDecimal(row["自费比例"].ToString().Trim());
                    pactItemRate.Rate.PayRate = Convert.ToDecimal(row["自付比例"].ToString().Trim());
                    pactItemRate.Rate.RebateRate = Convert.ToDecimal(row["优惠比例"].ToString().Trim());
                    pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(row["欠费比例"].ToString().Trim());
                    pactItemRate.Rate.Quota = Convert.ToDecimal(row["限额"].ToString().Trim());
                    listModify.Add(pactItemRate);
                }
            }

            List<FS.HISFC.Models.Base.PactItemRate> listAdd = new List<FS.HISFC.Models.Base.PactItemRate>();
            dt = this.dtPacts.GetChanges(DataRowState.Added);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FS.HISFC.Models.Base.PactItemRate pactItemRate = hsPacts[row["合同编号"].ToString()] as FS.HISFC.Models.Base.PactItemRate;
                    pactItemRate.Rate.PubRate = Convert.ToDecimal(row["公费比例"].ToString().Trim());
                    pactItemRate.Rate.OwnRate = Convert.ToDecimal(row["自费比例"].ToString().Trim());
                    pactItemRate.Rate.PayRate = Convert.ToDecimal(row["自付比例"].ToString().Trim());
                    pactItemRate.Rate.RebateRate = Convert.ToDecimal(row["优惠比例"].ToString().Trim());
                    pactItemRate.Rate.ArrearageRate = Convert.ToDecimal(row["欠费比例"].ToString().Trim());
                    pactItemRate.Rate.Quota = Convert.ToDecimal(row["限额"].ToString().Trim());
                    listAdd.Add(pactItemRate);
                }
            }

            if (listAdd.Count > 0 || listModify.Count > 0 || listDelete.Count > 0)
            {
                string errorInfo = "";
                if (new FS.SOC.HISFC.Fee.BizProcess.PactItemRate().Save(listAdd, listModify, listDelete, ref errorInfo) < 0)
                {
                    CommonController.CreateInstance().MessageBox(errorInfo, MessageBoxIcon.Error);
                    return -1;
                }
            }

            this.dtPacts.AcceptChanges();

            CommonController.CreateInstance().MessageBox("保存成功！", MessageBoxIcon.Asterisk);

            return 1;
        }

        public bool Valid()
        {
            this.neuSpreadPact.StopCellEditing();
            foreach (DataRow row in this.dtPacts.Rows)
            {
                row.EndEdit();
            }

            DataTable dt = this.dtPacts.GetChanges(DataRowState.Added | DataRowState.Modified | DataRowState.Deleted);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (CommonController.CreateInstance().MessageBox("数据发生变化，是否保存", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    return true;
                }
            }

            return false;
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactItemRate>> SelectedPactItemRateChange
        {
            get
            {
                return selectedPactItemRateChange;
            }
            set
            {
                selectedPactItemRateChange = value;
            }
        }

        #endregion

        #region IDataDetail 成员

        public int Clear()
        {
            this.txtCustomCodeForItem.Text = string.Empty;
            this.txtCustomCodeForPact.Text = string.Empty;

            this.pactInfoList = null;
            this.pactCodes = "''";
            return 1;
        }

        public string Filter
        {
            set 
            {
                filter = value;
            }
        }

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander FilterTextChanged
        {
            get
            {
                return filterTextChanged;
            }
            set
            {
                filterTextChanged = value;
            }
        }

        public FS.SOC.Windows.Forms.FpSpread FpSpread
        {
            get { return this.neuSpread; }
        }

        public string Info
        {
            set { }
        }

        public int Init()
        {
            try
            {
                this.initDataTable();
                this.initBaseData();
                this.initFarPoint();
                this.initEvents();
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }
            return 1;
        }

        public bool IsContainsFocus
        {
            get { return this.neuSpread.Focused; }
        }

        public int SetFocus()
        {
            return 1;
        }

        public int SetFocusToFilter()
        {
            this.txtCustomCodeForItem.Select();
            this.txtCustomCodeForItem.Focus();
            return 1;
        }

        public string SettingFileName
        {
            set
            {
                settingFile = value;
                settingPactFile =FS.FrameWork.WinForms.Classes.Function.SettingPath+ "SOC.Fee.Pact.DetailPact.xml";
            }
        }

        #endregion
    }
}

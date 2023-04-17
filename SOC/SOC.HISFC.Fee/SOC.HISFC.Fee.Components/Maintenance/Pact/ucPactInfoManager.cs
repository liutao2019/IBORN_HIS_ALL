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

namespace FS.SOC.HISFC.Fee.Components.Maintenance.Pact
{
    /// <summary>
    /// [功能描述:合同单位维护的合同单位显示界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-2]<br></br>
    /// 说明：
    /// </summary>
     partial class ucPactInfoManager : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.SOC.HISFC.Fee.Interface.Components.IPactInfoQuery
    {
        public ucPactInfoManager()
        {
            InitializeComponent();
        }

        #region 域变量

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactInfo>> selectedPactInfoChange;
        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander filterTextChanged;

        /// <summary>
        /// 药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = new DataTable();
        /// <summary>
        /// 存储变量
        /// </summary>
        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        FS.SOC.HISFC.Fee.BizLogic.PactInfo pactInfoMgr = new FS.SOC.HISFC.Fee.BizLogic.PactInfo();

        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = "";

        private string filter = "1=1";

        private List<FS.HISFC.Models.Base.PactInfo> pactInfoList = null;
        #endregion

        #region 初始化

        private void initFarPoint()
        {
            if (!this.neuSpread.ReadSchema(this.settingFile))
            {
                this.fpItemGroup.ColumnHeader.Rows[0].Height = 34f;
            }
            this.fpItemGroup.DataAutoSizeColumns = false;
        }

        private void initBaseData()
        {
            //复合项目
            ArrayList pactList = pactInfoMgr.QueryPactUnitAll();

            if (pactList == null)
            {
                throw new Exception(pactInfoMgr.Err);
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载数据...");
            Application.DoEvents();
            int progress = 1;
            int count = pactList.Count;
            this.setReadOnly(false);

            foreach (FS.HISFC.Models.Base.PactInfo item in pactList)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress++, count);
                if (this.addItemObjectToDataTable(item) != 1)
                {
                    continue;
                }
            }
            this.ncmbSystemType.AddItems(CommonController.CreateInstance().QueryConstant(FS.HISFC.Models.Base.EnumConstant.SYSTYPE));
            this.setReadOnly(true);
            this.filterItem();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.dtItems.AcceptChanges();
            this.fpItemGroup.DataSource = this.dtItems.DefaultView;
        }

        private void initDataTable()
        {
            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            //定义类型
            this.dtItems.Columns.AddRange(new DataColumn[] { 
                                                   new DataColumn("维护", typeof(bool)),
                                                   new DataColumn("项目编号", typeof(string)),
                    new DataColumn("项目名称", typeof(string)),
                    new DataColumn("结算类别", typeof(string)),
                    new DataColumn("价格形式", typeof(string)),
                    new DataColumn("系统类别", typeof(string)),
                    new DataColumn("公费比例", typeof(decimal)),
                    new DataColumn("自付比例", typeof(decimal)),
                    new DataColumn("自费比例", typeof(decimal)),
                    new DataColumn("优惠比例", typeof(decimal)),
                    new DataColumn("欠费比例", typeof(decimal)),
                    new DataColumn("婴儿标志", typeof(bool)),
                    new DataColumn("是否监控", typeof(bool)),
                    new DataColumn("标志", typeof(string)),
                    new DataColumn("需医疗证", typeof(bool)),
                    new DataColumn("日限额", typeof(decimal)),
                    new DataColumn("月限额", typeof(decimal)),
                    new DataColumn("年限额", typeof(decimal)),
                    new DataColumn("一次限额", typeof(decimal)),
                    new DataColumn("床位上限", typeof(decimal)),
                    new DataColumn("空调上限", typeof(decimal)),
                    new DataColumn("简称", typeof(string)),
                    new DataColumn("序号", typeof(int)),
                    new DataColumn("待遇算法DLL",typeof(string)),
                    new DataColumn("待遇算法DLL描述",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string)),
                    new DataColumn("有效性",typeof(string))
            });


            this.setReadOnly(true);

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["项目编号"];

            this.dtItems.DefaultView.AllowEdit = true;
            this.dtItems.DefaultView.AllowNew = true;
            this.dtItems.DefaultView.AllowDelete = true;

            this.dtItems.PrimaryKey = keys;
            this.fpItemGroup.DataAutoSizeColumns = false;
            this.fpItemGroup.DataSource = this.dtItems;//.DefaultView;

        }

        private void initEvents()
        {
            this.txtCustomCode.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.txtCustomCode.TextChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.neuSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);

            this.neuSpread.ButtonClicked -= new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);
            this.neuSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);

            this.neuSpread.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);
            this.neuSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);

            this.ckAllPact.CheckedChanged -= new EventHandler(ckAllPact_CheckedChanged);
            this.ckAllPact.CheckedChanged += new EventHandler(ckAllPact_CheckedChanged);

            this.ckUnValid.CheckedChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.ckUnValid.CheckedChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.ckValid.CheckedChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.ckValid.CheckedChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.ncmbSystemType.TextChanged += new EventHandler(ntxtFilter_TextChanged);
        }

        #endregion

        #region 数据显示

        private void setReadOnly(bool isReadOnly)
        {
            foreach (DataColumn dc in this.dtItems.Columns)
            {
                if (dc.ColumnName.Equals("维护")
                     )
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = isReadOnly;
                }
            }
        }

        private int addItemObjectToDataTable(FS.HISFC.Models.Base.PactInfo item)
        {
            return this.addItemObjectToDataTable(item, false);
        }

        private int addItemObjectToDataTable(FS.HISFC.Models.Base.PactInfo item, bool isModify)
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
            if (this.hsItem.Contains(item.ID))
            {
                if (isModify)
                {
                    this.hsItem[item.ID] = item;
                }
                else
                {
                    CommonController.CreateInstance().MessageBox("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                this.hsItem.Add(item.ID, item);
            }

            this.setDataRowValue(item);

            return 1;
        }

        private int setDataRowValue(FS.HISFC.Models.Base.PactInfo pactInfo)
        {

            DataRow row = this.dtItems.Rows.Find(pactInfo.ID);
            if (row == null)
            {
                row = this.dtItems.NewRow();
                row["项目编号"] = pactInfo.ID;
                this.dtItems.Rows.Add(row);
            }

            row["项目名称"] = pactInfo.Name;
            pactInfo.PayKind.Name = PactInfoClass.PayKindTypeEditor.PayKindHelper.GetName(pactInfo.PayKind.ID);
            row["结算类别"] = pactInfo.PayKind.Name;
            row["价格形式"] = PactInfoClass.PriceFormTypeEditor.PriceFormHelper.GetName(pactInfo.PriceForm);
            row["系统类别"] = PactInfoClass.SystemTypeEditor.SystemTypeHelper.GetName(pactInfo.PactSystemType);
            row["公费比例"] = pactInfo.Rate.PubRate;
            row["自付比例"] = pactInfo.Rate.PayRate;
            row["自费比例"] = pactInfo.Rate.OwnRate;
            row["优惠比例"] = pactInfo.Rate.RebateRate;
            row["欠费比例"] = pactInfo.Rate.ArrearageRate;
            row["婴儿标志"] = pactInfo.Rate.IsBabyShared;
            row["是否监控"] = pactInfo.IsInControl;
            row["标志"] = PactInfoClass.ItemTypeEditor.ItemTypeHelper.GetName(pactInfo.ItemType);
            row["需医疗证"] = pactInfo.IsNeedMCard;
            row["日限额"] = pactInfo.DayQuota;
            row["月限额"] = pactInfo.MonthQuota;
            row["年限额"] = pactInfo.YearQuota;
            row["一次限额"] = pactInfo.OnceQuota;
            row["床位上限"] = pactInfo.BedQuota;
            row["空调上限"] = pactInfo.AirConditionQuota;
            row["简称"] = pactInfo.ShortName;
            row["序号"] = pactInfo.SortID;
            row["待遇算法DLL"] = pactInfo.PactDllName;
            row["待遇算法DLL描述"] = PactInfoClass.SIDllTypeEditor.SIDllHelper.GetName(pactInfo.PactDllName);
            row["拼音码"] = pactInfo.SpellCode;
            row["五笔码"] = pactInfo.WBCode;
            row["有效性"] = pactInfo.ValidState;


           
            return 1;
        }

        private void modify()
        {
            if (pactInfoList == null)
            {
                pactInfoList = new List<FS.HISFC.Models.Base.PactInfo>();
            }
            List<FS.HISFC.Models.Base.PactInfo> list = new List<FS.HISFC.Models.Base.PactInfo>();

            bool isModify = false;
            for (int i = 0; i < this.fpItemGroup.RowCount; i++)
            {
                if (FS.FrameWork.Function.NConvert.ToBoolean(this.neuSpread.GetCellText(0, i, "维护")))
                {
                    string feeCode = this.neuSpread.GetCellText(0, i, "项目编号");
                    if (this.hsItem.Contains(feeCode))
                    {
                        FS.HISFC.Models.Base.PactInfo undrug = this.hsItem[feeCode] as FS.HISFC.Models.Base.PactInfo;
                        if (pactInfoList.Contains(undrug) == false)
                        {
                            isModify = true;
                        }
                        list.Add(undrug);
                    }
                }
            }

            if (this.fpItemGroup.ActiveRowIndex >= 0 && this.fpItemGroup.RowCount > 0)
            {
                string feeCode = this.neuSpread.GetCellText(0, this.fpItemGroup.ActiveRowIndex, "项目编号");
                if (this.hsItem.Contains(feeCode))
                {
                    FS.HISFC.Models.Base.PactInfo undrug = this.hsItem[feeCode] as FS.HISFC.Models.Base.PactInfo;
                    if (pactInfoList.Contains(undrug) == false)
                    {
                        isModify = true;
                    }
                    if (list.Contains(undrug)==false)
                    {
                        list.Add(undrug);
                    }
                }
            }

            if (isModify || list.Count != this.pactInfoList.Count)
            {
                if (this.selectedPactInfoChange != null)
                {
                    this.selectedPactInfoChange(list);
                }
            }

            this.pactInfoList = new List<FS.HISFC.Models.Base.PactInfo>(list);

            this.showModifyInfo();
        }

        private void showModifyInfo()
        {
            //int ColumnIndex1 = this.neuSpread.GetColumnIndex(0, "维护");
            //int ColumnIndex2 = this.neuSpread.GetColumnIndex(0, "项目名称");
            //string modifyInfo = "";
            //for (int i = 0; i < this.fpItemGroup.RowCount; i++)
            //{
            //    if (FS.FrameWork.Function.NConvert.ToBoolean(this.fpItemGroup.Cells[i, ColumnIndex1].Value))
            //    {
            //        modifyInfo += this.fpItemGroup.Cells[i, ColumnIndex2].Text + ";";
            //    }
            //}
            //this.lbModifyInfo.Text = "正在维护：" + modifyInfo;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.modify();
        }
        #endregion

        #region 过滤

        private void filterItem()
        {
            this.filter = Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.txtCustomCode.Text + "%");
            filter = "(" + filter + ")";
            //增加有效性显示
            if (this.dtItems.Columns.Contains("有效性"))
            {
                if (this.ckUnValid.Checked&&this.ckValid.Checked)
                {
                    filter += " and 有效性 like '%'";
                }
                else if(this.ckValid.Checked)
                {
                    filter += " and 有效性 = '1'";
                }
                else if (this.ckUnValid.Checked)
                {
                    filter += " and 有效性 = '0'";
                }
            }
            //增加系统类别过滤
            if (this.dtItems.Columns.Contains("系统类别"))
            {
                filter += string.Format(" and 系统类别 like '%{0}%'", this.ncmbSystemType.Text.Trim());
            }

            //if (this.dtItems.Columns.Contains("费用代码"))
            //{
            //    filter += string.Format(" and 费用代码 like '%{0}%'", this.ncmbFeeType.Text);
            //}

            this.dtItems.DefaultView.RowFilter = filter;
            this.neuSpread.ReadSchema(this.settingFile);

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
        }

        void ntxtFilter_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
        }

        void nTxtCustomCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpItemGroup.RowCount > 0 && this.fpItemGroup.ActiveRowIndex >= 0)
                {
                    this.modify();
                }
            }
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void neuSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            this.modify();
        }

        void neuSpread_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            this.modify();
        }

        void ckAllPact_CheckedChanged(object sender, EventArgs e)
        {
            int i = this.neuSpread.GetColumnIndex(0, "维护");
            if (this.fpItemGroup.RowCount > 0)
            {
                this.fpItemGroup.Cells[0, i, this.fpItemGroup.RowCount - 1, i].Value = this.ckAllPact.Checked;
            }
            this.modify();
        }

        #endregion

        #region IPactInfoQuery 成员

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<List<FS.HISFC.Models.Base.PactInfo>> SelectedPactInfoChange
        {
            get
            {
                return this.selectedPactInfoChange;
            }
            set
            {
                this.selectedPactInfoChange = value;
            }
        }

        public void AddItemRange(List<FS.HISFC.Models.Base.PactInfo> pactInfoList)
        {
            if (pactInfoList != null)
            {
                this.setReadOnly(false);
                foreach (FS.HISFC.Models.Base.PactInfo pactInfo in pactInfoList)
                {
                    this.addItemObjectToDataTable(pactInfo, true);
                }
                this.setReadOnly(true);
            }
        }

        public int AddItem(FS.HISFC.Models.Base.PactInfo pactInfo)
        {
            if (pactInfo != null)
            {
                if (string.IsNullOrEmpty(pactInfo.ID))
                {
                    pactInfo.ID = pactInfoMgr.GetNewPactCode();
                    pactInfo.ValidState = "1";
                    pactInfo.Rate.OwnRate = 1.00M;
                }

                this.setReadOnly(false);
                int i= this.addItemObjectToDataTable(pactInfo);
                this.setReadOnly(true);

                this.fpItemGroup.ActiveRowIndex = this.fpItemGroup.RowCount - 1;
                this.neuSpread.ShowRow(0, this.fpItemGroup.RowCount - 1, FarPoint.Win.Spread.VerticalPosition.Center);
                this.neuSpread_SelectionChanged(null, null);

                return i;
            }

            return 1;
        }

        public void Delete()
        {
            if (this.dtItems != null)
            {

                //提交
                this.neuSpread.StopCellEditing();
                foreach (DataRow row in this.dtItems.Rows)
                {
                    row.EndEdit();
                }

                DataRow[] rows = this.dtItems.Select("维护='true'");
                if (rows != null)
                {
                    foreach (DataRow row in rows)
                    {
                        if (row.RowState != DataRowState.Added)
                        {
                            CommonController.CreateInstance().MessageBox("不能删除已经保存的数据，名称：" + row["项目名称"] + "，编码：" + row["项目编号"], System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }

                        this.dtItems.Rows.Remove(row);
                    }

                    this.modify();
                }
            }
        }

        public  int Save()
        {
            this.neuSpread.StopCellEditing();
            foreach (DataRow row in this.dtItems.Rows)
            {
                row.EndEdit();
            }

            if (!this.Valid())
            {
                return -1;
            }

            List<FS.HISFC.Models.Base.PactInfo> listDelete = new List<FS.HISFC.Models.Base.PactInfo>();
            //先删除
            DataTable dt = this.dtItems.GetChanges(DataRowState.Deleted);

            if (dt != null)
            {
                dt.RejectChanges();

                foreach (DataRow row in dt.Rows)
                {
                    FS.HISFC.Models.Base.PactInfo pactItemRate = this.hsItem[row["项目编号"].ToString()] as FS.HISFC.Models.Base.PactInfo;
                    listDelete.Add(pactItemRate);
                }
            }

            List<FS.HISFC.Models.Base.PactInfo> listModify = new List<FS.HISFC.Models.Base.PactInfo>();
            dt = this.dtItems.GetChanges(DataRowState.Modified);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FS.HISFC.Models.Base.PactInfo pactItemRate = this.hsItem[row["项目编号"].ToString()] as FS.HISFC.Models.Base.PactInfo;
                    if (!this.Valid(pactItemRate))
                    {
                        return -1;
                    }
                    listModify.Add(pactItemRate);
                }
            }

            List<FS.HISFC.Models.Base.PactInfo> listAdd = new List<FS.HISFC.Models.Base.PactInfo>();
            dt = this.dtItems.GetChanges(DataRowState.Added);

            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    FS.HISFC.Models.Base.PactInfo pactItemRate = this.hsItem[row["项目编号"].ToString()] as FS.HISFC.Models.Base.PactInfo;
                    if (!this.Valid(pactItemRate))
                    {
                        return -1;
                    }
                    listAdd.Add(pactItemRate);
                }
            }

            if (listAdd.Count > 0 || listModify.Count > 0 || listDelete.Count > 0)
            {
                string errorInfo = "";
                if (new FS.SOC.HISFC.Fee.BizProcess.PactInfo().Save(listAdd, listModify, listDelete, ref errorInfo) < 0)
                {
                    CommonController.CreateInstance().MessageBox(errorInfo, MessageBoxIcon.Error);
                    return -1;
                }
            }

            this.dtItems.AcceptChanges();

            CommonController.CreateInstance().MessageBox("保存成功！", MessageBoxIcon.Asterisk);
            
            return 1;
        }

        public bool Valid()
        {
            for (int i = 0; i < this.fpItemGroup.Rows.Count; i++)
            {
                if (this.fpItemGroup.Cells[i, this.neuSpread.GetColumnIndex(0, "项目编号")].Text.Trim() == string.Empty)
                {
                    CommonController.CreateInstance().MessageBox(string.Format("第{0}行项目编号不能为空！", i + 1), MessageBoxIcon.Warning);
                    return false;
                }

                if (this.fpItemGroup.Cells[i, this.neuSpread.GetColumnIndex(0, "项目名称")].Text.Trim() == string.Empty)
                {
                    CommonController.CreateInstance().MessageBox(string.Format("第{0}行项目名称不能为空！", i + 1), MessageBoxIcon.Warning);
                    return false;
                }

                if (this.fpItemGroup.Cells[i, this.neuSpread.GetColumnIndex(0, "结算类别")].Text.Trim() == string.Empty)
                {
                    CommonController.CreateInstance().MessageBox(string.Format("第{0}行结算类别不能为空，编码：{1}，名称：{2}！", i + 1, this.neuSpread.GetCellText(0, i, "项目编号"), this.neuSpread.GetCellText(0, i, "项目名称")), MessageBoxIcon.Warning);
                    return false;
                }

                if (this.fpItemGroup.Cells[i, this.neuSpread.GetColumnIndex(0, "价格形式")].Text.Trim() == string.Empty)
                {
                    CommonController.CreateInstance().MessageBox(string.Format("第{0}行价格形式不能为空，编码：{1}，名称：{2}！", i + 1, this.neuSpread.GetCellText(0, i, "项目编号"), this.neuSpread.GetCellText(0, i, "项目名称")), MessageBoxIcon.Warning);
                    return false;
                }

                if (this.fpItemGroup.Cells[i, this.neuSpread.GetColumnIndex(0, "系统类别")].Text.Trim() == string.Empty)
                {
                    CommonController.CreateInstance().MessageBox(string.Format("第{0}行显示类别不能为空，编码：{1}，名称：{2}！", i + 1, this.neuSpread.GetCellText(0, i, "项目编号"), this.neuSpread.GetCellText(0, i, "项目名称")), MessageBoxIcon.Warning);
                    return false;
                }

                if (this.fpItemGroup.Cells[i, this.neuSpread.GetColumnIndex(0, "待遇算法DLL")].Text.Trim() == string.Empty)
                {
                    CommonController.CreateInstance().MessageBox(string.Format("第{0}行待遇算法DLL不能为空，编码：{1}，名称：{2}！", i + 1, this.neuSpread.GetCellText(0, i, "项目编号"), this.neuSpread.GetCellText(0, i, "项目名称")), MessageBoxIcon.Warning);
                    return false;
                }
            }
            return true;
        }

        public bool Valid(FS.HISFC.Models.Base.PactInfo pactItemRate)
        {
            if (string.IsNullOrEmpty(pactItemRate.ID))
            {
                CommonController.CreateInstance().MessageBox("项目编号不能为空！", MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrEmpty(pactItemRate.Name))
            {
                CommonController.CreateInstance().MessageBox("项目名称不能为空！", MessageBoxIcon.Warning);
                return false;
            }

            if (pactItemRate.PayKind.ID == null||pactItemRate.PayKind.ID.Length==0)
            {
                CommonController.CreateInstance().MessageBox(string.Format("结算类别不能为空，编码：{0}，名称：{1}！",pactItemRate.ID,pactItemRate.Name), MessageBoxIcon.Warning);
                return false;
            }

            if (pactItemRate.PriceForm == null || pactItemRate.PriceForm.Length == 0)
            {
                CommonController.CreateInstance().MessageBox(string.Format("价格形式不能为空，编码：{0}，名称：{1}！", pactItemRate.ID, pactItemRate.Name), MessageBoxIcon.Warning);
                return false;
            }

            if (pactItemRate.PactSystemType == null || pactItemRate.PactSystemType.Length == 0)
            {
                CommonController.CreateInstance().MessageBox(string.Format("显示类别不能为空，编码：{0}，名称：{1}！", pactItemRate.ID, pactItemRate.Name), MessageBoxIcon.Warning);
                return false;
            }

            if (pactItemRate.PactDllName == null || pactItemRate.PactDllName.Length == 0)
            {
                CommonController.CreateInstance().MessageBox(string.Format("待遇算法不能为空，编码：{0}，名称：{1}！", pactItemRate.ID, pactItemRate.Name), MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        #endregion

        #region IDataDetail 成员

        public int Clear()
        {
            this.txtCustomCode.Text = "";
            return 1;
        }

        public string Filter
        {
            set { this.filter = value; }
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
                this.initEvents();
                this.initDataTable();
                this.initBaseData();
                this.initFarPoint();
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
            if (this.ckAllPact.Focused)
            {
                this.SelectNextControl(this.ckAllPact, false, false, false, false);
            }

            return 1;
        }

        public int SetFocusToFilter()
        {
            this.txtCustomCode.Select();
            this.txtCustomCode.Focus();
            return 1;
        }

        public string SettingFileName
        {
            set { this.settingFile = value; }
        }

        #endregion

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            neuSpread.Dispose();
            dtItems.Dispose();
            hsItem.Clear();
            this.txtCustomCode.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
        }

        #endregion
    }
}

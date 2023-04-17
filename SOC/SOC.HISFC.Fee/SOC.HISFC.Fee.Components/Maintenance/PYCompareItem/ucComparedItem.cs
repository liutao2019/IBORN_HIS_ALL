using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem
{
    /// <summary>
    /// 针对整个单个合同单位对照
    /// </summary>
    public partial class ucComparedItem : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucComparedItem()
        {
            InitializeComponent();
        }
        DataTable dtPact=new DataTable();
        FS.SOC.HISFC.Fee.BizLogic.PYCompareItem compareMsg = new  FS.SOC.HISFC.Fee.BizLogic.PYCompareItem();

        string strPacts = "";

        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = "";

        private string filter = "1=1";


        protected override void OnLoad(EventArgs e)
        {
            Init();
            base.OnLoad(e);
        }
        private void initFarPoint()
        {
            if (!this.neuSpread.ReadSchema(this.settingFile))
            {
                this.fpPactList.ColumnHeader.Rows[0].Height = 34f;
            }
            this.fpPactList.DataAutoSizeColumns = false;
        }

        private void initBaseData(string hiscode,string strPacts)
        {
            //先清空
            if (dtPact != null && dtPact.Rows.Count > 0)
            {
                dtPact.Rows.Clear();
            }
            //已对照项目列表
            List<FS.SOC.HISFC.Fee.Models.CompareItemModel> pactList = this.compareMsg.QueryComparedItem(hiscode, strPacts);

            if (pactList == null)
            {
                throw new Exception(compareMsg.Err);
            }

            foreach (FS.SOC.HISFC.Fee.Models.CompareItemModel item in pactList)
            {
                if (this.addItemObjectToDataTable(item) != 1)
                {
                    continue;
                }
            }
            this.fpPactList.DataSource = this.dtPact.DefaultView;
        }

        private void initDataTable()
        {
            if (this.dtPact == null)
            {
                this.dtPact = new DataTable();
            }

            //定义类型
            this.dtPact.Columns.AddRange(new DataColumn[] { 
                new DataColumn("选择",typeof(bool)),
                                                   new DataColumn("单位编码", typeof(string)),
                    new DataColumn("单位名称", typeof(string)),
                    new DataColumn("本地代码", typeof(string)), 
                    new DataColumn("本地名称", typeof(string)), 
                    new DataColumn("中心代码",typeof(string)),
                    new DataColumn("中心名称",typeof(string)),
                    new DataColumn("自定义码",typeof(string))
                    
            });


            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtPact.Columns["单位编码"];

            this.dtPact.PrimaryKey = keys;
            this.fpPactList.DataAutoSizeColumns = false;
            this.fpPactList.DataSource = this.dtPact.DefaultView;

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

        }
        public int Init()
        {
            try
            {
                this.initEvents();
                this.initDataTable();
                this.initFarPoint();
            }
            catch (Exception ex)
            {
                CommonController.CreateInstance().MessageBox("初始化失败，请系统管理员报告错误：" + ex.Message, MessageBoxIcon.Information);
                return -1;
            }

            return 1;
        }


        private int addItemObjectToDataTable(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            return this.addItemObjectToDataTable(item, false);
        }

        private int addItemObjectToDataTable(FS.SOC.HISFC.Fee.Models.CompareItemModel item, bool isModify)
        {
            if (item == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：合同单位基本信息为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.dtPact == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
     
            this.setDataRowValue(item);

            return 1;
        }

        private int setDataRowValue(FS.SOC.HISFC.Fee.Models.CompareItemModel pactInfo)
        {

            DataRow row = this.dtPact.Rows.Find(pactInfo.PactCode);
            if (row == null)
            {
                row = this.dtPact.NewRow();
                row["单位编码"] = pactInfo.PactCode;
                this.dtPact.Rows.Add(row);
            }
            row["选择"] = false;
            row["单位名称"] = pactInfo.PactName;
            row["本地代码"] = pactInfo.HisCode;
            row["本地名称"] = pactInfo.HisName;
            row["中心代码"] = pactInfo.CenterCode;
            row["中心名称"] = pactInfo.CenterName;
            row["自定义码"] = pactInfo.HisUserCode;
           

            return 1;
        }

        void ckAllPact_CheckedChanged(object sender, EventArgs e)
        {
            int i = this.neuSpread.GetColumnIndex(0, "维护");
            if (this.fpPactList.RowCount > 0)
            {
                //  this.fpPactList.Cells[0, i, this.fpPactList.RowCount - 1, i].Value = this.ckAllPact.Checked;
                if (true == this.ckAllPact.Checked)
                {

                    this.fpPactList.Cells[0, i, this.fpPactList.RowCount - 1, i].Text = "True";
                    //  this.fpPactList.Cells[0, i, this.fpPactList.RowCount - 1, i].Value = true;
                }
                else
                {

                    this.fpPactList.Cells[0, i, this.fpPactList.RowCount - 1, i].Text = "False";
                    //  this.fpPactList.Cells[0, i, this.fpPactList.RowCount - 1, i].Value = false;
                }
            }

        }
        void nTxtCustomCode_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
        }

        void nTxtCustomCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.fpPactList.RowCount > 0 && this.fpPactList.ActiveRowIndex >= 0)
                {

                }
            }
        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
        }

        void neuSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {

        }

        void neuSpread_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {

        }
        #region 过滤

        private void filterItem()
        {
            this.filter = Function.GetFilterStr(this.dtPact.DefaultView, "%" + this.txtCustomCode.Text + "%");
            filter = "(" + filter + ")";
            //增加系统类别，费用类别，有效性，组套
            //if (this.dtPact.Columns.Contains("系统类别"))
            //{
            //    filter += string.Format(" and 系统类别 like '%{0}%'", this.ncmbSystemType.Text);
            //}

            //if (this.dtPact.Columns.Contains("费用代码"))
            //{
            //    filter += string.Format(" and 费用代码 like '%{0}%'", this.ncmbFeeType.Text);
            //}

            this.dtPact.DefaultView.RowFilter = filter;
            this.neuSpread.ReadSchema(this.settingFile);

            //if (this.filterTextChanged != null)
            //{
            //    this.filterTextChanged();
            //}

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

        //public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander FilterTextChanged
        //{
        //    get
        //    {
        //        return filterTextChanged;
        //    }
        //    set
        //    {
        //        filterTextChanged = value;
        //    }
        //}

        public FS.SOC.Windows.Forms.FpSpread FpSpread
        {
            get { return this.neuSpread; }
        }

        public string Info
        {
            set { }
        }

        public bool IsContainsFocus
        {
            get { return this.neuSpread.Focused; }
        }

        public int SetFocus()
        {
            //更新当前价格
            if (this.fpPactList.ActiveRowIndex >= 0)
            {
                //string pactNO = this.neuSpread.GetCellText(0, this.fpPactList.ActiveRowIndex, "单位编号");
                //if (this.hsPact.Contains(pactNO))
                //{
                //    FS.HISFC.Models.Base.PactInfo pact = this.hsPact[pactNO] as FS.HISFC.Models.Base.PactInfo;

                //}
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

        public FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        public void SetLocalItem(FS.SOC.HISFC.Fee.Models.CompareItemModel item)
        {
            currentItem = item;
            if (strPacts != null && strPacts != string.Empty)
            {

              initBaseData(item.HisCode, this.strPacts);

            }
        }
        public void SetComparedItem(string strPacts)
        {
            this.strPacts = strPacts;
            initBaseData(currentItem.HisCode, strPacts);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            compareMsg.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            for (int i = 0; i <= this.fpPactList.Rows.Count - 1; ++i)
            {
               
                if (this.fpPactList.Cells[i,0].Text.Trim().ToLower()=="true")
                {
                    FS.SOC.HISFC.Fee.Models.CompareItemModel item = new FS.SOC.HISFC.Fee.Models.CompareItemModel();
                    item.PactCode = this.fpPactList.Cells[i, 1].Text.Trim();
                    item.HisCode = this.fpPactList.Cells[i, 3].Text.Trim();
         
                    if (-1 == compareMsg.Delete(item))
                    {
                        MessageBox.Show("取消医保对照出错！"+compareMsg.Err);
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }                   
                }
               
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            initBaseData(currentItem.HisCode, this.strPacts);
        }

    }
}

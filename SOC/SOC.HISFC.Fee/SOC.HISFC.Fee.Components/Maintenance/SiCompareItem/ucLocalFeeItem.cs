﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Fee.Components.Maintenance.SiCompareItem
{    
    /// <summary>
    /// [功能描述:合同单位维护的费用对照显示界面]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-2]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucLocalFeeItem : FS.FrameWork.WinForms.Controls.ucBaseControl, Fee.Interface.Components.IPactCompareItem
    {
        public ucLocalFeeItem(int itemtype)
        {
            this.item_type = itemtype;
            InitializeComponent();
        }

        #region 域变量


        int intUnDrug = 0;
        public int IntUnDrug
        {
            get
            {
                return this.intUnDrug;
            }
            set
            {
                this.intUnDrug = value;
            }
        }

        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Fee.Models.CompareItemModel> selectedPactItemRateChange;
        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander filterTextChanged;


        public delegate void SelectedItemChingedHandle(FS.SOC.HISFC.Fee.Models.CompareItemModel item);
        public event SelectedItemChingedHandle SelectedItemChingedEvent;

        string strPacts = string.Empty;
        public string StrPacts
        {
            get { return strPacts; }
            set { strPacts = value; }
        }

        /// <summary>
        /// 项目信息的数据集
        /// </summary>
        private System.Data.DataTable dtItems = new DataTable();

        /// <summary>
        /// 存储变量
        /// </summary>
        private System.Collections.Hashtable hsItem = new System.Collections.Hashtable();

        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.SettingPath+ "SOC.Fee.SiCompareItem.LocalFeeItem.xml";

        private string filter = "1=1";

        /// <summary>
        /// 非药品信息缓存
        /// </summary>
        private ArrayList alItems = null;

        /// <summary>
        /// 药品信息
        /// </summary>
        private ArrayList alPharmacys = null;



        private int item_type = 1;
        public int ItemType
        {
            get { return item_type; }
            set { item_type = value; }
        }

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

        private void initDataTable()
        {
            #region dtItems

            if (this.dtItems == null)
            {
                this.dtItems = new DataTable();
            }

            foreach (DataColumn dc in this.dtItems.Columns)
            {
                dc.ReadOnly = true;
            }

            DataColumn[] keys = new DataColumn[1];

            keys[0] = this.dtItems.Columns["项目编号"];

            this.dtItems.PrimaryKey = keys;
            this.fpItemGroup.DataAutoSizeColumns = false;
            this.fpItemGroup.DataSource = this.dtItems.DefaultView;

            this.dtItems.AcceptChanges();

            #endregion
        }

        private void initEvents()
        {
            this.txtCustomCodeForItem.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
            this.txtCustomCodeForItem.TextChanged += new EventHandler(nTxtCustomCode_TextChanged);

            this.neuSpread.SelectionChanged -= new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);
            this.neuSpread.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(neuSpread_SelectionChanged);

            this.neuSpread.ColumnWidthChanged -= new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);
            this.neuSpread.ColumnWidthChanged += new FarPoint.Win.Spread.ColumnWidthChangedEventHandler(neuSpread_ColumnWidthChanged);

  
            this.rbtnItem.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnItem.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnPharmacy.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnPharmacy.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnAll.CheckedChanged -= new EventHandler(ckMinFee_CheckedChanged);
            this.rbtnAll.CheckedChanged += new EventHandler(ckMinFee_CheckedChanged);
   }

        protected override void  OnLoad(EventArgs e)
        {
            Init();
            base.OnLoad(e);
        }
       


        #endregion

        #region 数据显示
        private void initBaseData()
        {
           
            FS.SOC.HISFC.Fee.BizLogic.SiCompareItem compareItemMgr = new FS.SOC.HISFC.Fee.BizLogic.SiCompareItem();
            //项目
         //   List<FS.SOC.HISFC.Fee.Models.CompareItemModel> itemList = null;

            DataSet ds = new DataSet();
            compareItemMgr.QueryLocalItems(ref ds);
            if (ds == null)
            {
                throw new Exception(compareItemMgr.Err);
            }
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.dtItems = ds.Tables[0];
                //foreach (DataRow dr in dtItems.Rows)
                //{
                //    if (!this.hsItem.Contains(dr["项目编号"].ToString()))
                //    {
                //        this.hsItem.Add(dr["项目编号"].ToString(),dr);
                //    }
                //}

            }

         
        }
       
        private void setReadOnly(DataTable dt ,bool isReadOnly)
        {
            foreach (DataColumn dc in dt.Columns)
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


        #endregion

        #region 过滤

        private void filterItem()
        {
            this.filter = Function.GetFilterStr(this.dtItems.DefaultView, "%" + this.txtCustomCodeForItem.Text + "%");

            filter = "(" + filter + ")";

            if (this.rbtnPharmacy.Checked)
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
        #endregion

        #region 事件触发

        void nTxtCustomCode_TextChanged(object sender, EventArgs e)
        {
            this.filterItem();
            if (item_type == 1)
            {
                this.neuSpread_SelectionChanged(sender, null);
            }
            this.txtCustomCodeForItem.Focus();
        }

        public FS.SOC.HISFC.Fee.Models.CompareItemModel currentItem = null;
        void neuSpread_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            if (this.SelectedItemChingedEvent != null)
            {
                if (this.fpItemGroup.Rows.Count<=0)
                {
                    return;
                }
                //获取当前行项目编号
                string key = this.fpItemGroup.Cells[this.fpItemGroup.ActiveRowIndex, 1].Text;
                DataRow row = this.dtItems.Rows.Find(key);
                FS.SOC.HISFC.Fee.Models.CompareItemModel item = new FS.SOC.HISFC.Fee.Models.CompareItemModel();
                item.HisCode = row["项目编号"].ToString();
                item.HisName=row["项目名称"].ToString();
                item.WBCode = row["五笔码"].ToString();
                item.SpellCode = row["拼音码"].ToString();
                item.UserCode = row["自定义码"].ToString();

                item.User01 = item_type.ToString();//保存类型 1:本地项目 2：医保项目
                currentItem = item;
                this.SelectedItemChingedEvent(item);
                
            }
          

        }

        void neuSpread_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            this.neuSpread.SaveSchema(this.settingFile);
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

        #endregion

        #region IPactFeeCodeRateDetail 成员
        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.SOC.HISFC.Fee.Models.CompareItemModel> SelectedPactItemRateChange
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
                
                initBaseData();
                this.initDataTable();
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
            }
        }

        #endregion
    }
}

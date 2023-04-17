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

namespace FS.SOC.HISFC.Fee.Components.Maintenance.PYCompareItem
{
    /// <summary>
    /// [功能描述:根据医保类型显示合同单位列]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-2]<br></br>
    /// 说明：
    /// </summary>
    public partial class ucCenterPact : FS.FrameWork.WinForms.Controls.ucBaseControl,IDisposable
    {
        public ucCenterPact()
        {
            InitializeComponent();
        }

        #region 域变量

     //   private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<string> selectedCenterInfoChange;
        private FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.FilterTextChangeHander filterTextChanged;

       public delegate void ItemSelectedHandle(string strType,string strPacts);
       public event ItemSelectedHandle itemSelectedEvent;



       public delegate void QueryCheckedPactHandle(Hashtable listPact);
       public event QueryCheckedPactHandle getCheckedPactEvent;


        /// <summary>
        /// 药品基本信息的数据集
        /// </summary>
        private System.Data.DataTable dtPact = new DataTable();
        /// <summary>
        /// 存储变量
        /// </summary>
        private System.Collections.Hashtable hsPact = new System.Collections.Hashtable();

        FS.SOC.HISFC.Fee.BizLogic.PYCompareItem compareMsg = new FS.SOC.HISFC.Fee.BizLogic.PYCompareItem();

        string strPact = "";
        /// <summary>
        /// Fp设置文件
        /// </summary>
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.SettingPath + "SOC.Fee.CompareItem.CenterPact.xml";

        private string filter = "1=1";


        public string StrPact
        {
            get { return strPact; }
        }


        #endregion

        #region 初始化
        private void setReadOnly(bool isReadOnly)
        {
            foreach (DataColumn dc in this.dtPact.Columns)
            {
                if (dc.ColumnName.Equals("维护"))
                {
                    dc.ReadOnly = false;
                }
                else
                {
                    dc.ReadOnly = isReadOnly;
                }
            }
        }
        private void initFarPoint()
        {
            if (!this.neuSpread.ReadSchema(this.settingFile))
            {
                this.fpPactList.ColumnHeader.Rows[0].Height = 34f;
            }
            this.fpPactList.DataAutoSizeColumns = false;
        }

        private void initBaseData(string centerType)
        {
            //先清空
            if (dtPact!=null && dtPact.Rows.Count>0)
            {
                dtPact.Rows.Clear();             
            }
            hsPact.Clear();
            //合同单位列表
            List<FS.HISFC.Models.Base.PactInfo> pactList = this.compareMsg.QueryPactUnit(centerType);

            if (pactList == null)
            {
                throw new Exception(compareMsg.Err);
            }


            setReadOnly(false);
            foreach (FS.HISFC.Models.Base.PactInfo item in pactList)
            {
                strPact += "'"+item.ID.Trim()+"',";
                if (this.addItemObjectToDataTable(item) != 1)
                {
                    continue;
                }
            }
            setReadOnly(true);

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
                                                   new DataColumn("维护", typeof(bool)),
                                                   new DataColumn("单位编码", typeof(string)),
                    new DataColumn("单位名称", typeof(string)),
                    new DataColumn("结算类别", typeof(string)), 
                    new DataColumn("单位简称", typeof(string)), 
                    new DataColumn("待遇算法DLL",typeof(string)),
                    new DataColumn("待遇算法DLL描述",typeof(string)),
                    new DataColumn("拼音码",typeof(string)),
                    new DataColumn("五笔码",typeof(string))
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

            this.ckAllPact.CheckedChanged -= new EventHandler(ckAllPact_CheckedChanged);
            this.ckAllPact.CheckedChanged += new EventHandler(ckAllPact_CheckedChanged);

            this.neuSpread.ButtonClicked -=new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);
             this.neuSpread.ButtonClicked+=new FarPoint.Win.Spread.EditorNotifyEventHandler(neuSpread_ButtonClicked);
   
        }

        private void initCenterType()
        {
            ArrayList alCenterType = compareMsg.QueryCenterType();
            if (alCenterType == null)
            {
                throw new Exception(compareMsg.Err);
            }
            this.neuCenterType.AddItems(alCenterType);
           
            this.neuCenterType.SelectedIndexChanged -= new EventHandler(neuCenterType_SelectedIndexChanged);
            this.neuCenterType.SelectedIndexChanged +=new EventHandler(neuCenterType_SelectedIndexChanged);

            if (alCenterType.Count > 0)
            {
                this.neuCenterType.SelectedIndex = 0;
            }
 
        }

        protected override void OnLoad(EventArgs e)
        {
            Init();
            base.OnLoad(e);
        }
        #endregion

        #region 数据显示


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
            if (this.dtPact == null)
            {
                CommonController.CreateInstance().MessageBox("向DataTable中添加合同单位项目基本信息失败：DataTable为null", System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }
            if (this.hsPact.Contains(item.ID))
            {
                if (isModify)
                {
                    this.hsPact[item.ID] = item;
                }
                else
                {
                    CommonController.CreateInstance().MessageBox("编码：" + item.ID + " 名称：" + item.Name + " 已经重复！", System.Windows.Forms.MessageBoxIcon.Information);
                    return -1;
                }
            }
            else
            {
                this.hsPact.Add(item.ID, item);
            }

            this.setDataRowValue(item);

            return 1;
        }

        private int setDataRowValue(FS.HISFC.Models.Base.PactInfo pactInfo)
        {

            DataRow row = this.dtPact.Rows.Find(pactInfo.ID);
            if (row == null)
            {
                row = this.dtPact.NewRow();
                row["单位编码"] = pactInfo.ID;
                this.dtPact.Rows.Add(row);
            }

            row["单位名称"] = pactInfo.Name;
            pactInfo.PayKind.Name = pactInfo.PayKind.ID;
            row["结算类别"] = pactInfo.PayKind.Name;
            row["单位简称"] = pactInfo.ShortName;
            row["待遇算法DLL"] = pactInfo.PactDllName;
            row["待遇算法DLL描述"] = pactInfo.PactDllDescription;
            row["拼音码"] = pactInfo.SpellCode;
            row["五笔码"] = pactInfo.WBCode;

           
            return 1;
        }
        #endregion

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


        void ckAllPact_CheckedChanged(object sender, EventArgs e)
        {
            int i = this.neuSpread.GetColumnIndex(0, "维护");
            if (this.fpPactList.RowCount > 0)
            {
              //  this.fpPactList.Cells[0, i, this.fpPactList.RowCount - 1, i].Value = this.ckAllPact.Checked;
                if (true==this.ckAllPact.Checked)
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
            
             GetCheckedPact();

        }

        /// <summary>
        /// 医保类型选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuCenterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            strPact = "";
            string  centerTypeID = this.neuCenterType.Tag.ToString();
           
            //加载合同单位
            this.initBaseData(centerTypeID);

            this.ckAllPact_CheckedChanged(null, null);

            //还要加载医保中心项目
            strPact = strPact.Trim(',');
      
            if (this.itemSelectedEvent != null)
            {
                try
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载医保中心数据...");
                    Application.DoEvents();
                    this.itemSelectedEvent(this.neuCenterType.Text, strPact);
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
                catch
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }




 
        }

        #endregion

        #region IPactInfoQuery 成员

        public FS.SOC.HISFC.BizProcess.CommonInterface.Delegate.ItemSelectedChange<FS.HISFC.Models.Base.Spell> SelectedCenterInfoChange
        {
            get
            {
              //  return this.selectedPactInfoChange;
                return null;
            }
            set
            {
               // this.selectedPactInfoChange = value;
            }
        }

        public void AddItemRange(List<FS.HISFC.Models.Base.PactInfo> pactInfoList)
        {
            if (pactInfoList != null)
            {

                foreach (FS.HISFC.Models.Base.PactInfo pactInfo in pactInfoList)
                {
                    this.addItemObjectToDataTable(pactInfo, true);
                }

            }
        }

        public void Delete()
        {

        }

        public  int Save()
        {   
            return 1;
        }

        public bool Valid()
        {
            
            return true;
        }

        public bool Valid(FS.HISFC.Models.Base.PactInfo pactItemRate)
        {
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
                this.initFarPoint();
                this.initCenterType();
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

        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            neuSpread.Dispose();
            dtPact.Dispose();
            hsPact.Clear();
            this.txtCustomCode.TextChanged -= new EventHandler(nTxtCustomCode_TextChanged);
        }

        #endregion

        private Hashtable QueryCheckedPact()
        {
            Hashtable listPact = new Hashtable();
            for (int i = 0; i <= fpPactList.Rows.Count - 1; ++i)
            {
                if (this.fpPactList.Cells[i, 0].Text.ToLower() == "true")
                    listPact.Add(this.fpPactList.Cells[i, 1].Text, this.fpPactList.Cells[i, 2].Text);
            }
            return listPact;
        }

        private void GetCheckedPact()
        {
            Hashtable listPact = this.QueryCheckedPact();
            if (this.getCheckedPactEvent!=null)
            {
                getCheckedPactEvent(listPact);
            }

        }

        private void neuSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column==0)
            {
                GetCheckedPact();
            }
            
        }
       
       


    }


}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.Controls
{
    public partial class ucFilterItem : UserControl, FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient
    {
        public ucFilterItem()
        {
            InitializeComponent();
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {


        }

        #region 变量

        /// <summary>
        /// 执行科室代码
        /// </summary>
        protected string deptCode = string.Empty;

        /// <summary>
        /// 是否判断库存
        /// </summary>
        protected bool isJudgeStore = false;

        /// <summary>
        /// 是否模糊查询
        /// </summary>
        protected bool isQueryLike = false;

        /// <summary>
        /// 是否选择项目后关闭uc 默认为true
        /// </summary>
        protected bool isSelectAndClose = true;

        /// <summary>
        /// 是否选择了项目
        /// </summary>
        protected bool isSelectItem = false;

        /// <summary>
        /// 默认显示行数
        /// </summary>
        protected int itemCount = 10;

        /// <summary>
        /// 显示项目类别
        /// </summary>
        protected FS.HISFC.Models.Base.ItemTypes itemType = FS.HISFC.Models.Base.ItemTypes.All;

        /// <summary>
        /// 当前选择的项目
        /// </summary>
        protected FS.HISFC.Models.Base.Item nowItem = new FS.HISFC.Models.Base.Item();

        /// <summary>
        /// 当前传递的过滤对象
        /// </summary>
        protected object objectFilterObject = new object();

        /// <summary>
        /// 查询方式
        /// </summary>
        protected string queryType = string.Empty;

        /// <summary>
        /// 项目集合
        /// </summary>
        DataSet dsItem = new DataSet();


        #endregion

        #region 私有方法

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="inputChar">输入字符串</param>
        private void SetFilter(string inputChar)
        {
            string filterString = string.Empty;

            if (inputChar == string.Empty)
            {
                filterString = "1 = 1";
            }
            else
            {
                filterString = "SPELL_CODE like '%" + inputChar.ToUpper() + "%'";
            }

            this.dataWindowControl1.SetFilter(filterString);

            this.dataWindowControl1.Filter();

            if (this.dataWindowControl1.RowCount > 0)
            {
                this.dataWindowControl1.SelectRow(1, true);
            }
        }

        /// <summary>
        /// 根据名称获得当前列号
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <returns>成功 当前列号 失败 -1</returns>
        private short GetCurrentColumn(string columnName)
        {
            Sybase.DataWindow.GraphicObjectColumn g = null;

            for (short i = 1; i < this.dataWindowControl1.ColumnCount; i++)
            {
                g = this.dataWindowControl1.GetColumnObjectByNumber(i);

                if (g != null)
                {
                    if (g.Name.ToLower() == columnName.ToLower())
                    {
                        g = null;

                        return i;
                    }
                }
            }

            g = null;

            return -1;
        }

        /// <summary>
        /// 获得当前项目
        /// </summary>
        private int GetItem()
        {
            int row = this.dataWindowControl1.CurrentRow;

            if (row < 1 || this.dataWindowControl1.RowCount == 0)
            {
                this.isSelectItem = false;

                return -1;
            }

            string itemCode = this.dataWindowControl1.GetItemString(row, this.GetCurrentColumn("item_code"));
            string exeDeptCode = this.dataWindowControl1.GetItemString(row, this.GetCurrentColumn("exe_dept"));
            string drugFlag = this.dataWindowControl1.GetItemString(row, this.GetCurrentColumn("drug_flag"));
            //by niuxy

            decimal unit_price = this.dataWindowControl1.GetItemDecimal(row, this.GetCurrentColumn("unit_price"));

            this.isSelectItem = true;

            //by niuxy
            this.SelectedItem(itemCode, drugFlag, exeDeptCode, unit_price);

            this.Visible = false;

            return 1;
        }

        #endregion

        #region IChooseItemForOutpatient 成员

        /// <summary>
        /// 选择项目的方式
        /// </summary>
        public FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes ChooseItemType
        {
            get
            {
                return FS.HISFC.BizProcess.Integrate.FeeInterface.ChooseItemTypes.ItemChanging;
            }
            set 
            {

            }
        }

        /// <summary>
        /// 执行科室代码
        /// </summary>
        public string DeptCode
        {
            get
            {
                return this.deptCode;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// 得到当前对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int GetSelectedItem(ref FS.HISFC.Models.Base.Item item)
        {
            return this.GetItem();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        public int Init()
        {
            this.Visible = false;

            return 1;
        }

        public string InputPrev
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        /// <summary>
        /// 是否判断库存
        /// </summary>
        public bool IsJudgeStore
        {
            get
            {
                return this.isJudgeStore;
            }
            set
            {
                this.isJudgeStore = value;
            }
        }

        /// <summary>
        /// 是否模糊查询
        /// </summary>
        public bool IsQueryLike
        {
            get
            {
                return this.isQueryLike;
            }
            set
            {
                this.isQueryLike = value;
            }
        }

        /// <summary>
        /// 是否选择项目后关闭uc
        /// </summary>
        public bool IsSelectAndClose
        {
            get
            {
                return this.isSelectAndClose;
            }
            set
            {
                this.isSelectAndClose = value;
            }
        }

        /// <summary>
        /// 是否选择了项目
        /// </summary>
        public bool IsSelectItem
        {
            get
            {
                return this.isSelectItem;
            }
            set
            {
                this.isSelectItem = value;
            }
        }

        /// <summary>
        /// 默认选择行数
        /// </summary>
        public int ItemCount
        {
            get
            {
                return this.itemCount;
            }
            set
            {
                this.itemCount = value;
            }
        }

        /// <summary>
        /// 显示项目类别
        /// </summary>
        public FS.HISFC.Models.Base.ItemTypes ItemType
        {
            get
            {
                return this.itemType;
            }
            set
            {
                this.itemType = value;
            }
        }

        /// <summary>
        /// 当前选择的项目
        /// </summary>
        public FS.HISFC.Models.Base.Item NowItem
        {
            get
            {
                return this.nowItem;
            }
            set
            {
                this.nowItem = value;
            }
        }

        /// <summary>
        /// 当前传递的过滤对象
        /// </summary>
        public object ObjectFilterObject
        {
            get
            {
                return this.objectFilterObject;
            }
            set
            {
                this.objectFilterObject = value;
            }
        }

        /// <summary>
        /// 查询方式
        /// </summary>
        public string QueryType
        {
            get
            {
                return this.queryType;
            }
            set
            {
                this.queryType = value;
            }
        }

        public FS.HISFC.Models.Base.ItemKind ItemKind
        {
            get
            {
                return FS.HISFC.Models.Base.ItemKind.All;
            }
            set { }
        }
        /// <summary>
        /// 选择项目事件
        /// </summary>
        public event FS.HISFC.BizProcess.Integrate.FeeInterface.WhenGetItem SelectedItem;

        /// <summary>
        /// 设置数据DataSet
        /// </summary>
        /// <param name="dsItem">项目集合</param>
        public void SetDataSet(DataSet dsItem)
        {
            this.dsItem = dsItem;

            this.dataWindowControl1.SetRedrawOff();

            this.dataWindowControl1.Retrieve(this.dsItem.Tables[0]);

            this.dataWindowControl1.SetRedrawOn();
        }

        /// <summary>
        /// 当前过滤字符串
        /// </summary>
        /// <param name="inputChar">当前输入字符串</param>
        /// <param name="inputType">模糊查询方式</param>
        public void SetInputChar(object sender, string inputChar, FS.HISFC.Models.Base.InputTypes inputType)
        {
            if (!this.Visible)
            {
                this.Show();
            }

            this.Show();

            this.BringToFront();

            this.SetFilter(inputChar);
        }

        /// <summary>
        /// 设置坐标
        /// </summary>
        /// <param name="p">当前坐标</param>
        public void SetLocation(Point p)
        {
            this.Location = p;
        }

        /// <summary>
        /// 下一行
        /// </summary>
        public void NextRow()
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollNextRow);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        public void NextPage()
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollNextPage);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// 上一行
        /// </summary>
        public void PriorRow()
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollPriorRow);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// 上一页
        /// </summary>
        public void PriorPage()
        {
            this.dataWindowControl1.Scroll(Sybase.DataWindow.ScrollAction.ScrollNextPage);

            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        /// <summary>
        /// 选择当前项目
        /// </summary>
        /// <returns>成功 1 失败-1</returns>
        public int GetSelectedItem()
        {
            return this.GetItem();
        }

        #endregion

        private void dataWindowControl1_Click(object sender, EventArgs e)
        {
            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        private void dataWindowControl1_RowFocusChanged(object sender, Sybase.DataWindow.RowFocusChangedEventArgs e)
        {
            this.dataWindowControl1.SelectRow(0, false);

            this.dataWindowControl1.SelectRow(this.dataWindowControl1.CurrentRow, true);
        }

        private void dataWindowControl1_DoubleClick(object sender, EventArgs e)
        {
            this.GetItem();
        }

        private void dataWindowControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.GetItem();
            }
        }

        #region IChooseItemForOutpatient 成员


        public FS.HISFC.BizProcess.Interface.FeeInterface.IGetSiItemGrade IGetSiItemGrade
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        public FS.HISFC.Models.Registration.Register RegPatientInfo
        {
            get
            {
                throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion
    }
}

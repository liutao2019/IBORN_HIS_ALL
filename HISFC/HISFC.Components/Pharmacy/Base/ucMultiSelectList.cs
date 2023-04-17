using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [控件描述: 支持多重选择的列表选择界面]
    /// [创 建 人: Sunjh]
    /// [创建时间: 2010-9-17]
    /// [说    明: 出库时可以选择入库单进行出库，支持多选按药品进行合并数量 
    ///            {F543EA9D-B8BA-4a84-BF16-745BE023E92F}]
    /// </summary>
    public partial class ucMultiSelectList : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMultiSelectList()
        {
            InitializeComponent();
        }

        public ucMultiSelectList(string typeQuery, string stockDept, ArrayList alStates)
        {
            InitializeComponent();
            this.queryType = typeQuery;
            this.deptCode = stockDept;
            this.cbbStates.Items.Clear();
            this.cbbStates.AddItems(alStates);
            this.cbbStates.SelectedIndex = 0;
        }

        #region 变量

        /// <summary>
        /// 药品管理业务类
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 查询类型 0入库 1出库
        /// </summary>
        private string queryType = "";

        /// <summary>
        /// 查询科室
        /// </summary>
        private string deptCode = "";

        /// <summary>
        /// 是否显示时间选择控件
        /// </summary>
        private bool isShowDateTimeControls = false;

        /// <summary>
        /// 是否显示状态选择控件
        /// </summary>
        private bool isShowStatesControls = false;

        public delegate void SelectListHandler(string listCollections);

        public event SelectListHandler SelectListEvent;

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示时间选择控件
        /// </summary>
        public bool IsShowDateTimeControls
        {
            get 
            { 
                return isShowDateTimeControls; 
            }
            set 
            { 
                isShowDateTimeControls = value; 
            }
        }

        /// <summary>
        /// 是否显示状态选择控件
        /// </summary>
        public bool IsShowStatesControls
        {
            get 
            {
                return isShowStatesControls; 
            }
            set 
            { 
                isShowStatesControls = value;
            }
        }

        #endregion

        #region 方法

        public void ShowData()
        {
            this.fpList.RowCount = 0;
            ArrayList al = new ArrayList();
            if (this.queryType == "0")
            {
                if (this.isShowDateTimeControls)
                {
                    al = this.itemManager.QueryInputList(this.deptCode, "AAAA", this.cbbStates.SelectedItem.ID, this.dtpBegin.Value, this.dtpEnd.Value);
                }
                else
                {
                    al = this.itemManager.QueryInputList(this.deptCode, "AAAA", this.cbbStates.SelectedItem.ID, System.DateTime.MinValue, this.itemManager.GetDateTimeFromSysDateTime());
                }

                if (al == null)
                {
                    return;
                }

                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Input inObj = al[i] as FS.HISFC.Models.Pharmacy.Input;
                    this.fpList.RowCount = i + 1;
                    this.fpList.Cells[i, 0].Text = "False";
                    this.fpList.Cells[i, 1].Text = inObj.InListNO;
                    this.fpList.Cells[i, 2].Text = inObj.Operation.ApproveOper.OperTime.ToString();
                }
                FarPoint.Win.Spread.CellType.CheckBoxCellType chkType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.fpList.Columns[0].CellType = chkType;
            }
        }

        #endregion

        #region 事件

        private void btnQuery_Click(object sender, EventArgs e)
        {
            this.ShowData();
        }

        protected override void OnLoad(EventArgs e)
        {
            this.dtpBegin.Value = System.DateTime.Now.AddDays(-1).Date;
            this.dtpEnd.Value = this.dtpBegin.Value.AddDays(7).Date;
            if (this.isShowDateTimeControls)
            {
                this.dtpBegin.Enabled = true;
                this.dtpEnd.Enabled = true;
            }
            else
            {
                this.dtpBegin.Enabled = false;
                this.dtpEnd.Enabled = false;
            }

            if (this.isShowStatesControls)
            {
                this.cbbStates.Enabled = true;
            }
            else
            {
                this.cbbStates.Enabled = false;
            }

            base.OnLoad(e);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string listStr = "";
            for (int i = 0; i < this.fpList.RowCount; i++)
            {
                if (this.fpList.Cells[i, 0].Text == "True")
                {
                    listStr = "," + this.fpList.Cells[i, 1].Text.Trim();
                }
            }
            listStr = listStr.Substring(1);
            this.SelectListEvent(listStr);
            this.FindForm().Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion
    }
}

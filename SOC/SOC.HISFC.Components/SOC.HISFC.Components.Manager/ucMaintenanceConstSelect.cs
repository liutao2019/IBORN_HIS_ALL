using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FarPoint.Win.Spread;

namespace FS.SOC.HISFC.Components.Manager
{
    public partial class ucMaintenanceConstSelect : FS.FrameWork.WinForms.Controls.ucMaintenance
    {
        public ucMaintenanceConstSelect()
            : base("const")
        {
            InitializeComponent();
            Init();
        }

        private FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();

        protected override string SQL
        {
            get
            {
                return base.SQL + string.Format(" where TYPE='{0}'", ((Control)this).Text);
            }
        }

        protected override string GetDefaultValue(string fieldName)
        {
            if (fieldName == "TYPE")
            {
                return ((Control)this).Text;
            }
            else
            {
                return base.GetDefaultValue(fieldName);
            }
        }
        private FarPoint.Win.Spread.Column GetColumnByName(string name)
        {
            foreach (Column column in this.fpSpread1_Sheet1.Columns)
            {
                if (column.Label == name)
                    return column;
            }

            return null;
        }
        protected override void fpSpread1_Sheet1_CellChanged(object sender, FarPoint.Win.Spread.SheetViewEventArgs e)
        {
            base.fpSpread1_Sheet1_CellChanged(sender, e);

            //如果没有装载数据完成，则不做处理
            if (!this.isDataLoaded)
                return;

            //如果改变了名称，则拼音码、五笔码自动发生变化
            //{1B10BCB7-8133-4282-8479-9C41FE5A23FD} 区域语言转换
            if (this.fpSpread1_Sheet1.Columns[e.Column].Label == FS.FrameWork.Management.Language.Msg("名称"))
            {
                //{1B10BCB7-8133-4282-8479-9C41FE5A23FD} 区域语言转换
                Column column = this.GetColumnByName(FS.FrameWork.Management.Language.Msg("拼音码"));
                if (column != null /*&& this.fpSpread1_Sheet1.Cells[e.Row,column.Index].Text.Length==0*/)
                {
                    this.fpSpread1_Sheet1.Cells[e.Row, column.Index].Text = FS.FrameWork.Public.String.GetSpell(this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                }
                //{1B10BCB7-8133-4282-8479-9C41FE5A23FD} 区域语言转换
                column = this.GetColumnByName(FS.FrameWork.Management.Language.Msg("五笔码"));
                if (column != null)
                {
                    FS.HISFC.Models.Base.ISpell spCode = this.spellManager.Get(this.fpSpread1_Sheet1.Cells[e.Row, e.Column].Text);
                    if (spCode != null)
                        this.fpSpread1_Sheet1.Cells[e.Row, column.Index].Text = spCode.WBCode;
                }
            }
        }

        /// <summary>
        /// 非药品项目列表
        /// </summary>
        private ArrayList alConst_UndrugItem = null;

        /// <summary>
        /// 非药品项目帮助类
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper constHelper_UndrugItem = null;

        /// <summary>
        /// 费用管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        private int Init()
        {
            this.fpSpread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(neuSpread1_CellDoubleClick);

            #region 初始化列表

            //非药品项目
            alConst_UndrugItem = this.feeIntegrate.QueryValidItems();
            if (alConst_UndrugItem == null)
            {
                MessageBox.Show("查询收费项目出错：" + this.feeIntegrate.Err);
                return -1;
            }
            constHelper_UndrugItem = new FS.FrameWork.Public.ObjectHelper(alConst_UndrugItem);

            #endregion

            this.fpSpread1_Sheet1.Columns[1].Locked = true;

            //return this.Query();
            return 1;
        }

        /// <summary>
        /// 双击弹出选择项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader || e.RowHeader)
            {
                return;
            }
            if (this.fpSpread1_Sheet1.ActiveCell != null && e.Column == 1)
            {
                this.PopItem(this.alConst_UndrugItem, 2);
            }
        }

        /// <summary>
        /// 弹出常数选择
        /// </summary>
        private void PopItem(ArrayList al, int index)
        {
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject();
            if (FS.FrameWork.WinForms.Classes.Function.ChooseItem(al, ref info) == 0)
            {
                return;
            }
            else
            {
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 1].Value = info.ID;
                this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, 2].Value = info.Name;

                this.fpSpread1_Sheet1.ActiveColumnIndex = 2;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.fpSpread1.ContainsFocus)
            {
                if (keyData == Keys.Space)
                {
                    this.PopItem(this.alConst_UndrugItem, 2);
                }
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}

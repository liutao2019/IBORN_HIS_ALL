using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using System.Collections;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Account.Controls.IBorn
{

    /// <summary>
    /// 设置检索框位置
    /// </summary>
    public partial class ucPatientPayItems : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        //select * from bd_com_itemmedical   ;select * from bd_com_itemmedicaldetail

        /// <summary>
        /// 设置检索框位置
        /// </summary>
        public event FS.HISFC.Components.Account.Controls.IBorn.ucPatientMedicalInsurance.DelegateBoolPointSet SetSelectorPosition;


        /// <summary>
        /// 设置检索条件
        /// </summary>
        public event FS.HISFC.Components.Account.Controls.IBorn.ucPatientMedicalInsurance.DelegateTripleStringSet SetSelectorFliter;


        /// <summary>
        /// 套餐发生改变
        /// </summary>
        public event FS.HISFC.Components.Account.Controls.IBorn.ucPatientMedicalInsurance.DelegateVoidSet PackageChange;


        /// <summary>
        /// 设置支付金额
        /// </summary>
        public event FS.HISFC.Components.Account.Controls.IBorn.ucPatientMedicalInsurance.DelegateVoidSet SetFeeInfoCost;

        /// <summary>
        /// 获取套餐选择器的显示状态
        /// </summary>
        public event FS.HISFC.Components.Account.Controls.IBorn.ucPatientMedicalInsurance.DelegateBoolGet GetSelectorVisible;



        /// <summary>
        /// 按键事件
        /// </summary>
        public event FS.HISFC.Components.Account.Controls.IBorn.ucPatientMedicalInsurance.DelegateKeysSet SelectorKeyPress;







        public ucPatientPayItems()
        {
            InitializeComponent();

            InitFp();
            InitEvent();
        }

        /// <summary>
        /// 初始化fp
        /// </summary>
        private void InitFp()
        {
            InitPackageFp();
            InitChildPackageFp();
            //this.rightMenu = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
        }

        /// <summary>
        /// 初始化事件
        /// </summary>
        private void InitEvent()
        {
            this.Leave += new EventHandler(ucChargeInfo_Leave);

            this.fpPackage.EditChange += new EditorNotifyEventHandler(fpPackage_EditChange);
            this.fpPackage.Leave += new EventHandler(fpPackage_Leave);
            this.fpPackage.CellClick += new CellClickEventHandler(fpPackage_CellClick);

            this.fpChildPackage.EditChange += new EditorNotifyEventHandler(fpChildPackage_EditChange);
            this.fpChildPackage.Leave += new EventHandler(fpChildPackage_Leave);
            this.fpChildPackage.CellClick += new CellClickEventHandler(fpChildPackage_CellClick);
        }

        private void InitPackageFp()
        {

            //FP热键屏蔽
            InputMap im;
            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            //FP列隐藏

        }

        private void InitChildPackageFp()
        {
            //FP热键屏蔽
            InputMap im;
            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Left, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Right, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpChildPackage.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F4, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

        }


        #region 触发事件

        /// <summary>
        /// 离开划价窗体事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucChargeInfo_Leave(object sender, EventArgs e)
        {
            this.fpPackage.StopCellEditing();
            this.fpChildPackage.StopCellEditing();
        }

        #region 套餐表格

        private void fpPackage_EditChange(object sender, EditorNotifyEventArgs e)
        {
            Control cell = e.EditingControl;
            Point p = GetChooseItemLocation(cell);
            SetSelectorPosition(true, p);
            if (this.fpPackage_Sheet1.ActiveCell != null)
            {
                string Str = string.Empty;
                if (this.fpPackage_Sheet1.ActiveCell.Value != null)
                {
                    Str = this.fpPackage_Sheet1.ActiveCell.Value.ToString();
                }
                this.SetSelectorFliter(Str, "1", "ALL");
            }
        }

        private void fpPackage_Leave(object sender, EventArgs e)
        {
            this.fpPackage.StopCellEditing();
        }

        private void fpPackage_CellClick(object sender, CellClickEventArgs e)
        {
            // this.fpPackage_Sheet1.SetActiveCell(e.Row, (int)Columns.InputCode);
            if (e.Column == (int)PackageColumns.Select)
                return;

            this.fpPackage_Sheet1.ActiveRowIndex = e.Row;
            foreach (Row row in this.fpPackage_Sheet1.Rows)
            {
                if (row.Index != e.Row)
                {
                    this.fpPackage_Sheet1.Cells[row.Index, 0].Value = false;
                }
                else
                {
                    this.fpPackage_Sheet1.Cells[row.Index, 0].Value = true;
                }
            }

            //this.PackageChange();
        }

        private void fpPackage_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (this.fpPackage_Sheet1.RowCount == 0)
            {
                return;
            }

            this.fpPackage_Sheet1.ActiveRowIndex = e.Row;

            //this.PackageChange();
        }

        #endregion

        #region 项目包表格
        /// <summary>
        /// 确认框勾选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpChildPackage_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            //this.SetFeeInfoCost();
        }

        /// <summary>
        /// 划价框点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpChildPackage_CellClick(object sender, CellClickEventArgs e)
        {
            this.fpChildPackage_Sheet1.SetActiveCell(e.Row, (int)Columns.InputCode);
            // this.SetDetailInfo(e.Row);
        }

        /// <summary>
        /// 输入框编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpChildPackage_EditChange(object sender, EditorNotifyEventArgs e)
        {
            Control cell = e.EditingControl;

            if (e.Column == (int)Columns.PackageNum)
            {
                ItemMedicalDetail curPackagedetail = this.fpChildPackage_Sheet1.ActiveRow.Tag as ItemMedicalDetail;

                if (curPackagedetail == null || string.IsNullOrEmpty(curPackagedetail.ItemCode))
                {
                    return;
                }

                if (this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value == null || Int32.Parse(this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString()) <= 0)
                {
                    curPackagedetail.ItemNum = 0;
                }
                else
                {
                    curPackagedetail.ItemNum = Int32.Parse(this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                }
                return;

            }

        }

        /// <summary>
        /// 焦点离开划价信息列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpChildPackage_Leave(object sender, EventArgs e)
        {
            this.fpChildPackage.StopCellEditing();
        }

        #endregion

        #endregion



        /// <summary>
        /// 按键处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {

                if (this.fpChildPackage.ContainsFocus || this.fpPackage.ContainsFocus)
                {
                    if (keyData == Keys.Escape)
                    {
                        this.SetSelectorPosition(false, new Point());
                        return true;
                    }

                    if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Enter)
                    {
                        this.PutArrow(keyData);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Focus();
                this.fpChildPackage.Focus();
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// 方向按键
        /// </summary>
        /// <param name="key">当前的按键</param>
        private void PutArrow(Keys key)
        {
            FarPoint.Win.Spread.SheetView sv = null;

            if (this.fpChildPackage.ContainsFocus)
            {
                sv = fpChildPackage_Sheet1;
            }
            else if (this.fpPackage.ContainsFocus)
            {
                sv = this.fpPackage_Sheet1;
            }

            int currCol = sv.ActiveColumnIndex;
            int currRow = sv.ActiveRowIndex;

            if (key == Keys.Right)
            {
                for (int i = 0; i < sv.Columns.Count; i++)
                {
                    if (i > currCol && sv.Cells[currRow, i].Locked == false)
                    {
                        sv.SetActiveCell(currRow, i, false);

                        return;
                    }
                }
            }

            if (key == Keys.Left)
            {
                for (int i = sv.Columns.Count - 1; i >= 0; i--)
                {
                    if (i < currCol && sv.Cells[currRow, i].Locked == false)
                    {
                        sv.SetActiveCell(currRow, i, false);
                        return;
                    }
                }
            }

            if (key == Keys.Up)
            {
                if (!this.GetSelectorVisible())
                {
                    if (currRow > 0)
                    {
                        sv.ActiveRowIndex = currRow - 1;
                        sv.SetActiveCell(currRow - 1, sv.ActiveColumnIndex);
                        //this.SetDetailInfo(this.fpChildPackage_Sheet1.ActiveRowIndex);
                    }
                }
                else
                {
                    this.SelectorKeyPress(key);
                }
            }

            if (key == Keys.Down)
            {
                if (!this.GetSelectorVisible())
                {
                    string temp = sv.Cells[sv.ActiveRowIndex, (int)Columns.PackageName].Text;
                    if (temp != string.Empty)
                    {
                        AddRow(sv.ActiveRowIndex);
                    }
                }
                else
                {
                    this.SelectorKeyPress(key);
                }
            }

            if (key == Keys.Enter)
            {
                if (!this.GetSelectorVisible())
                {
                    string temp = sv.Cells[sv.ActiveRowIndex, (int)Columns.PackageName].Text;

                    if (temp != string.Empty)
                    {
                        if (this.fpChildPackage.ContainsFocus)
                        {
                            if (this.fpChildPackage_Sheet1.ActiveCell.Column.Index == (int)Columns.PackageNum)
                            {
                                AddRow(sv.ActiveRowIndex);
                            }
                            else
                            {
                                sv.SetActiveCell(this.fpChildPackage_Sheet1.ActiveCell.Row.Index, (int)Columns.PackageNum);
                            }
                        }
                        else
                        {
                            AddRow(sv.ActiveRowIndex);
                        }
                    }
                }
                else
                {
                    this.SelectorKeyPress(key);
                }
            }
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="row">当前行</param>
        private void AddRow(int row)
        {
            FarPoint.Win.Spread.SheetView sv = null;

            if (this.fpChildPackage.ContainsFocus)
            {
                sv = fpChildPackage_Sheet1;
            }
            else if (this.fpPackage.ContainsFocus)
            {
                sv = this.fpPackage_Sheet1;
            }


            if (row == sv.RowCount - 1)
            {
                sv.Rows.Add(sv.RowCount, 1);
                if (this.fpPackage.ContainsFocus)
                {
                    //this.SetPackageColumnEnable(sv.RowCount - 1);
                }
                else
                {
                    //this.SetColumnEnable(sv.RowCount - 1);
                }

                sv.SetActiveCell(sv.RowCount - 1, (int)Columns.InputCode);
            }
            else
            {
                sv.SetActiveCell(row + 1, (int)Columns.InputCode);
            }

            if (this.fpPackage.ContainsFocus)
            {
                foreach (Row currow in this.fpPackage_Sheet1.Rows)
                {
                    if (currow.Index != row + 1)
                    {
                        this.fpPackage_Sheet1.Cells[currow.Index, 0].Value = false;
                    }
                    else
                    {
                        this.fpPackage_Sheet1.Cells[currow.Index, 0].Value = true;
                    }
                }

                this.PackageChange();
            }

        }


        public void Clear()
        {
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpChildPackage_Sheet1.RowCount = 0;

            this.fpPackage_Sheet1.Rows.Add(0, 1);
            this.fpChildPackage_Sheet1.Rows.Add(0, 1);
        }


        /// <summary>
        /// 设置选择项目位置
        /// </summary>
        protected virtual Point GetChooseItemLocation(Control cell)
        {
            if (this.fpChildPackage.ContainsFocus)
            {
                Point p = new Point(SystemInformation.Border3DSize.Height * 2 + this.fpChildPackage.Location.X + cell.Location.X,
                        this.Parent.Location.Y + this.plLeftBottom.Location.Y + this.fpChildPackage.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                return p;
            }
            else
            {
                Point p = new Point(SystemInformation.Border3DSize.Height * 2 + this.fpPackage.Location.X + cell.Location.X,
                        this.Parent.Location.Y + this.plLeft.Location.Y + this.fpPackage.Location.Y + cell.Location.Y + cell.Height + SystemInformation.Border3DSize.Height * 2);
                return p;
            }
        }

        /// <summary>
        /// 根据收费列表设置收费项目
        /// </summary>
        /// <param name="packageList"></param>
        public void SetRecipePackage(ArrayList packageList)
        {
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpChildPackage_Sheet1.RowCount = 0;
            foreach (ItemMedical package in packageList)
            {
                this.fpPackage.StopCellEditing();
                this.fpPackage_Sheet1.Rows.Add(this.fpPackage_Sheet1.RowCount, 1);
                this.fpPackage_Sheet1.SetActiveCell(this.fpPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
                int currRow = this.fpPackage_Sheet1.RowCount - 1;
                this.fpPackage_Sheet1.Rows[currRow].Tag = package;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.Select].Value = true;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.InputCode].Value = package.PackageId;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageName].Value = package.PackageName;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageType].Value = "";
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.TotCost].Value = package.PackageCost;
            }

            this.fpPackage_Sheet1.Rows.Add(this.fpPackage_Sheet1.RowCount, 1);
            this.SetPackageColumnEnable(this.fpPackage_Sheet1.RowCount - 1);
            if (this.fpPackage_Sheet1.RowCount == 1)
            {
                this.fpPackage_Sheet1.SetActiveCell(this.fpPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
            }
            else
            {
                this.fpPackage_Sheet1.SetActiveCell(this.fpPackage_Sheet1.RowCount - 2, (int)Columns.InputCode);
            }

            this.PackageChange();
        }


        public void SetPackageInfo(ItemMedical package)
        {
            this.fpPackage.StopCellEditing();
            int currRow = this.fpPackage_Sheet1.ActiveCell.Row.Index;

            this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.Select].Value = true;
            this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.InputCode].Value = package.PackageId;
            this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageName].Value = package.PackageName;
            this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageType].Value = "";
            this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.TotCost].Value = package.PackageCost;

            this.fpPackage_Sheet1.Rows[currRow].Tag = package;

            if (currRow == this.fpPackage_Sheet1.RowCount - 1)
            {
                this.fpPackage.Focus();
                this.fpPackage_Sheet1.Rows.Add(this.fpPackage_Sheet1.RowCount, 1);
                this.SetPackageColumnEnable(this.fpChildPackage_Sheet1.RowCount - 1);
            }
        }

        /// <summary>
        /// 根据子套餐列表设置收费项目
        /// </summary>
        /// <param name="packageList"></param>
        public void SetChildPackage(List<ItemMedicalDetail> packageList)
        {
            this.fpChildPackage_Sheet1.RowCount = 0;
            foreach (ItemMedicalDetail package in packageList)
            {
                this.fpChildPackage.StopCellEditing();
                this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
                this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
                int currRow = this.fpChildPackage_Sheet1.RowCount - 1;
              
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.Select].Value = true;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.InputCode].Value = package.ItemCode;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageName].Value = package.ItemName;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageNum].Value = package.ItemNum;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.InputCodeSoon].Value = package.ItemSubcode;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageNameSoon].Value = package.ItemSubname;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.TotCost].Value = package.UnitPrice;
                package.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
                package.OperEnvironment.OperTime = DateTime.Now;
                package.CreateEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
                package.CreateEnvironment.OperTime = DateTime.Now;
                this.fpChildPackage_Sheet1.Rows[currRow].Tag = package;
            }

            this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
            if (this.fpChildPackage_Sheet1.RowCount == 1)
            {
                this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
            }
            else
            {
                this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 2, (int)Columns.InputCode);
            }
        }



        /// <summary>
        /// 获取套餐包
        /// </summary>
        /// <returns></returns>
        public List<ItemMedicalDetail> GetPackageSelected()
        {

            List<ItemMedicalDetail> itemdetails = new List<ItemMedicalDetail>();

            ItemMedical itemMedical = new ItemMedical();
            object packageitem = this.fpPackage_Sheet1.Rows[0].Tag;

            if (packageitem != null && packageitem is ItemMedical &&
                   (bool)this.fpPackage_Sheet1.Cells[0, (int)PackageColumns.Select].Value)
            {
                itemMedical = packageitem as ItemMedical;
            }

            if (string.IsNullOrEmpty(itemMedical.PackageId))
            {
                 return itemdetails;
            }

            foreach (Row row in this.fpChildPackage_Sheet1.Rows)
            {
                if (row.Tag != null &&
                   row.Tag is ItemMedicalDetail &&
                   (bool)this.fpChildPackage_Sheet1.Cells[row.Index, (int)PackageColumns.Select].Value)
                {
                    ItemMedicalDetail detail = (row.Tag as ItemMedicalDetail);
                    detail.ItemMediacl = itemMedical;

                    itemdetails.Add(detail);
                }
            }

            return itemdetails;

        }


        /// <summary>
        /// 设置新增行列属性
        /// </summary>
        /// <param name="row"></param>
        private void SetPackageColumnEnable(int row)
        {
            for (int j = 0; j < this.fpPackage_Sheet1.Columns.Count; j++)
            {
                if (j == (int)PackageColumns.InputCode || j == (int)PackageColumns.Select)
                {
                    this.fpPackage_Sheet1.Cells[row, j].Locked = false;
                }
                else
                {
                    this.fpPackage_Sheet1.Cells[row, j].Locked = true;
                }
            }
        }


        /// <summary>
        /// 套餐列枚举
        /// </summary>
        private enum PackageColumns
        {
            /// <summary>
            /// 选择
            /// </summary>
            Select = 0,

            /// <summary>
            /// 输入码
            /// </summary>
            InputCode = 1,

            /// <summary>
            /// 名称
            /// </summary>
            PackageName = 2,

            /// <summary>
            /// 套餐类别
            /// </summary>
            PackageType = 3,

            /// <summary>
            /// 套餐金额
            /// </summary>
            TotCost = 4




        }

        /// <summary>
        /// 列枚举
        /// </summary>
        private enum Columns
        {
            /// <summary>
            /// 选择
            /// </summary>
            Select = 0,

            /// <summary>
            /// 输入码
            /// </summary>
            InputCode = 1,

            /// <summary>
            /// 名称
            /// </summary>
            PackageName = 2,

            /// <summary>
            /// 数量
            /// </summary>
            PackageNum = 3,

            /// <summary>
            /// 子编码
            /// </summary>
            InputCodeSoon = 4,

            /// <summary>
            /// 子名称
            /// </summary>
            PackageNameSoon = 5,

            /// <summary>
            /// 项目金额
            /// </summary>
            TotCost = 6

        }

    }
}

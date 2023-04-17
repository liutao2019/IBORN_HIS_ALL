using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace HISFC.Components.Package.Fee.Controls
{

    public partial class ucChargeInfo : UserControl
    {
        /// <summary>
        /// 设置检索框位置
        /// </summary>
        public event DelegateBoolPointSet SetSelectorPosition;

        /// <summary>
        /// 设置检索条件
        /// </summary>
        public event DelegateTripleStringSet SetSelectorFliter;

        /// <summary>
        /// 通过PackageID获取细项
        /// </summary>
        public event DelegateArrayListGetString GetPackageDetailByID;

        /// <summary>
        /// 通过GUID获取子项目
        /// </summary>
        public event DelegateHashtableGet GetPackageDetialHsTable;

        /// <summary>
        /// 获取套餐选择器的显示状态
        /// </summary>
        public event DelegateBoolGet GetSelectorVisible;

        /// <summary>
        /// 按键事件
        /// </summary>
        public event DelegateKeysSet SelectorKeyPress;

        /// <summary>
        /// 设置支付金额
        /// </summary>
        public event DelegateVoidSet SetFeeInfoCost;

        /// <summary>
        /// 套餐发生改变
        /// </summary>
        public event DelegateVoidSet PackageChange;

        /// <summary>
        /// 删除子套餐
        /// </summary>
        public event DelegateVoidSet DeleteChildPackage;

        /// <summary>
        /// 非药品
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 药品
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 套餐业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();

        /// <summary>
        /// 自选套餐窗口
        /// </summary>
        HISFC.Components.Package.Fee.Forms.frmChoosePackageItem ChoosePackageItemFrm = new HISFC.Components.Package.Fee.Forms.frmChoosePackageItem();


        public ucChargeInfo()
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
            this.rightMenu = new FS.FrameWork.WinForms.Controls.NeuContextMenuStrip();
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
            this.fpPackage.ButtonClicked += new EditorNotifyEventHandler(fpPackage_ButtonClicked);


            this.fpChildPackage.EditChange += new EditorNotifyEventHandler(fpChildPackage_EditChange);
            this.fpChildPackage.Leave += new EventHandler(fpChildPackage_Leave);
            this.fpChildPackage.CellClick += new CellClickEventHandler(fpChildPackage_CellClick);
            this.fpChildPackage.ButtonClicked += new EditorNotifyEventHandler(fpChildPackage_ButtonClicked);

            this.btnAddChildPackage.Click += new EventHandler(btnAddChildPackage_Click);
            this.btnDelChildPackage.Click += new EventHandler(btnDelChildPackage_Click);
            this.btnSetChildPackage.Click += new EventHandler(btnSetChildPackage_Click);
            this.fpChildPackage.MouseUp += new MouseEventHandler(fpSpread1_MouseUp);
        }

        private void UnBindEvent()
        {
            this.Leave -= new EventHandler(ucChargeInfo_Leave);

            this.fpPackage.EditChange -= new EditorNotifyEventHandler(fpPackage_EditChange);
            this.fpPackage.Leave -= new EventHandler(fpPackage_Leave);
            this.fpPackage.CellClick -= new CellClickEventHandler(fpPackage_CellClick);
            this.fpPackage.ButtonClicked -= new EditorNotifyEventHandler(fpPackage_ButtonClicked);


            this.fpChildPackage.EditChange -= new EditorNotifyEventHandler(fpChildPackage_EditChange);
            this.fpChildPackage.Leave -= new EventHandler(fpChildPackage_Leave);
            this.fpChildPackage.CellClick -= new CellClickEventHandler(fpChildPackage_CellClick);
            this.fpChildPackage.ButtonClicked -= new EditorNotifyEventHandler(fpChildPackage_ButtonClicked);

            this.btnAddChildPackage.Click -= new EventHandler(btnAddChildPackage_Click);
            this.btnDelChildPackage.Click -= new EventHandler(btnDelChildPackage_Click);
            this.btnSetChildPackage.Click -= new EventHandler(btnSetChildPackage_Click);
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

            //FP列隐藏
            this.fpChildPackage_Sheet1.Columns[(int)Columns.PackageType].Visible = false;
            this.fpChildPackage_Sheet1.Columns[(int)Columns.PackageRange].Visible = false;
            this.fpChildPackage_Sheet1.Columns[(int)Columns.GiftCost].Visible = false;
            this.fpChildPackage_Sheet1.Columns[(int)Columns.changeFlag].Visible = false;
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
                this.SetSelectorFliter(Str,"1","ALL");
            }
        }

        private void fpPackage_Leave(object sender, EventArgs e)
        {
            this.fpPackage.StopCellEditing();
        }

        private void fpPackage_CellClick(object sender, CellClickEventArgs e)
        {
            this.fpPackage_Sheet1.SetActiveCell(e.Row, (int)Columns.InputCode);

            //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
            if (e.Column == (int)PackageColumns.Select || e.Column == (int)PackageColumns.IsSpecial)
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

            this.PackageChange();
        }

        private void fpPackage_ButtonClicked(object sender, EditorNotifyEventArgs e)
        {
            if (this.fpPackage_Sheet1.RowCount == 0)
            {
                return;
            }

            this.fpPackage_Sheet1.ActiveRowIndex = e.Row;

            this.PackageChange();
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
            this.SetFeeInfoCost();
        }

        /// <summary>
        /// 划价框点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpChildPackage_CellClick(object sender, CellClickEventArgs e)
        {
            this.fpChildPackage_Sheet1.SetActiveCell(e.Row, (int)Columns.InputCode);
            this.SetDetailInfo(e.Row);
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
                FS.HISFC.Models.MedicalPackage.Fee.Package curPackage = this.fpChildPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

                if (curPackage == null || string.IsNullOrEmpty(curPackage.PackageInfo.ID))
                {
                    return;
                }

                if (this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value == null || Int32.Parse(this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString()) <= 0)
                {
                    curPackage.PackageNum = 0;
                }
                else
                {
                    curPackage.PackageNum = Int32.Parse(this.fpChildPackage_Sheet1.Cells[e.Row, e.Column].Value.ToString());
                }

                curPackage.Package_Cost = curPackage.PackageNum * Decimal.Parse(curPackage.PackageInfo.User01);
                curPackage.Real_Cost = curPackage.PackageNum * Decimal.Parse(curPackage.PackageInfo.User01);
                curPackage.Etc_cost = 0.0m;
                curPackage.Gift_cost = 0.0m;

                this.fpChildPackage_Sheet1.Cells[e.Row, (int)Columns.TotCost].Value = curPackage.Package_Cost.ToString();
                this.fpChildPackage_Sheet1.Cells[e.Row, (int)Columns.RealCost].Value = curPackage.Real_Cost.ToString();
                this.fpChildPackage_Sheet1.Cells[e.Row, (int)Columns.GiftCost].Value = curPackage.Gift_cost.ToString();
                this.fpChildPackage_Sheet1.Cells[e.Row, (int)Columns.ETCCost].Value = curPackage.Etc_cost.ToString();
                this.SetFeeInfoCost();
                return;
                
            }

            Point p = GetChooseItemLocation(cell);
            SetSelectorPosition(true, p);
            if (this.fpChildPackage_Sheet1.ActiveCell != null)
            {
                string Str = string.Empty;

                if (this.fpChildPackage_Sheet1.ActiveCell.Value != null)
                {
                    Str = this.fpChildPackage_Sheet1.ActiveCell.Value.ToString();
                }

                string parentCode = string.Empty;
                FS.HISFC.Models.MedicalPackage.Fee.Package curParent = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

                if (curParent == null)
                {
                    parentCode = "0";
                }
                else
                {
                    parentCode = curParent.PackageInfo.ID;
                }

                this.SetSelectorFliter(Str, "2", parentCode);
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

        /// <summary>
        /// 右键菜单{98BFB7F0-5EDE-4bc1-8DC4-3EEF393ECF65}
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuContextMenuStrip rightMenu = null;

        /// <summary>
        /// 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_MouseUp(object sender, MouseEventArgs e)
        {
            //先清除
            this.rightMenu.Items.Clear();

            FarPoint.Win.Spread.Model.CellRange c = this.fpChildPackage.GetCellFromPixel(0, 0, e.X, e.Y);

            #region 右键菜单

            if (e.Button == MouseButtons.Right)
            {

                //1、套餐全选15E1CDD6-FBA9-4d93-8204-0BC788EBC265
                ToolStripMenuItem mnuItemPackageAll = new ToolStripMenuItem();
                mnuItemPackageAll.Click += new EventHandler(全选ToolStripMenuItem_Click);
                mnuItemPackageAll.Text = "全选";
                this.rightMenu.Items.Add(mnuItemPackageAll);

                //2、套餐反选15E1CDD6-FBA9-4d93-8204-0BC788EBC265
                ToolStripMenuItem mnuItemPackageCancel = new ToolStripMenuItem();
                mnuItemPackageCancel.Click += new EventHandler(全不选ToolStripMenuItem_Click);
                mnuItemPackageCancel.Text = "全不选";
                this.rightMenu.Items.Add(mnuItemPackageCancel);

                //3、单项目折扣
                ToolStripMenuItem mnuItemDiscount = new ToolStripMenuItem();
                mnuItemDiscount.Click += new EventHandler(反选mnuItemDiscount_Click);
                mnuItemDiscount.Text = "反选";
                this.rightMenu.Items.Add(mnuItemDiscount);

                this.rightMenu.Show(this.fpChildPackage, new Point(e.X, e.Y));
            }

            #endregion


        }

        #endregion

        #endregion

        #region 内部函数

        /// <summary>
        /// 新增一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddChildPackage_Click(object sender, EventArgs e)
        {
            this.AddPriceRow();
        }

        /// <summary>
        /// 删除一行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelChildPackage_Click(object sender, EventArgs e)
        {
            this.DeleteChildPackage();
        }

        /// <summary>
        /// 配置套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetChildPackage_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.MedicalPackage.Fee.Package currentchild = this.GetCurrentFeeRow();
            if (currentchild == null)
            {
                return;
            }

            FS.HISFC.Models.MedicalPackage.Package tmp = this.packageProcess.GetPackage(currentchild.PackageInfo.ID);

            if (tmp.SpecialFlag != "1")
            {
                MessageBox.Show("当前套餐包不能自定义项目！", "提示");
                return;
            }

            if (string.IsNullOrEmpty(currentchild.User01.ToString()))
            {
                currentchild.User01 = Guid.NewGuid().ToString();
            }

            Hashtable packageDetails = this.GetPackageDetialHsTable();
            ArrayList currentchildList = new ArrayList();
            if (packageDetails.ContainsKey(currentchild.User01))
            {
                currentchildList = packageDetails[currentchild.User01] as ArrayList;
            }

            ChoosePackageItemFrm.SetPackageDetails(currentchildList);

            if (ChoosePackageItemFrm.ShowDialog() == DialogResult.OK)
            {
                ArrayList newCurrentchildList = (ArrayList)ChoosePackageItemFrm.CurrentDetailList.Clone();

                if (packageDetails.ContainsKey(currentchild.User01))
                {
                    packageDetails[currentchild.User01] = newCurrentchildList;
                }
                else
                {
                    packageDetails.Add(currentchild.User01, newCurrentchildList);
                }

                currentchild.PackageInfo.User01 = this.GetTotFee(newCurrentchildList).ToString();
                this.SetDetailInfo(this.fpChildPackage_Sheet1.ActiveCell.Row.Index);
                this.RefreshCurrentChildData();
            }
        }

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
        /// 全选{98BFB7F0-5EDE-4bc1-8DC4-3EEF393ECF65}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Row row in fpChildPackage_Sheet1.Rows)
            {
                fpChildPackage_Sheet1.Cells[row.Index, 0].Text = "true";
            }
        }

        /// <summary>
        /// 全不选{98BFB7F0-5EDE-4bc1-8DC4-3EEF393ECF65}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 全不选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Row row in fpChildPackage_Sheet1.Rows)
            {
                //FeeItemList rowFee = row.Tag as FeeItemList;
                //if (rowFee != null)
                //    rowFee.IsPackage = "0";
                fpChildPackage_Sheet1.Cells[row.Index, 0].Text = "false";
            }
        }

        /// <summary>
        /// 反选{98BFB7F0-5EDE-4bc1-8DC4-3EEF393ECF65}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 反选mnuItemDiscount_Click(object sender, EventArgs e)
        {
            foreach (Row row in fpChildPackage_Sheet1.Rows)
            {
                if (fpChildPackage_Sheet1.Cells[row.Index, 0].Text == "False")
                    fpChildPackage_Sheet1.Cells[row.Index, 0].Text = "true";
                else if(fpChildPackage_Sheet1.Cells[row.Index, 0].Text == "True")
                    fpChildPackage_Sheet1.Cells[row.Index, 0].Text = "false";
            }
        }

        /// <summary>
        /// 方向按键
        /// </summary>
        /// <param name="key">当前的按键</param>
        private void PutArrow(Keys key)
        {
            FarPoint.Win.Spread.SheetView sv = null;

            if(this.fpChildPackage.ContainsFocus)
            {
                sv = fpChildPackage_Sheet1;
            }
            else if(this.fpPackage.ContainsFocus)
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
                        this.SetDetailInfo(this.fpChildPackage_Sheet1.ActiveRowIndex);
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
        /// 设置明细
        /// </summary>
        /// <param name="rowIndex"></param>
        private void SetDetailInfo(int rowIndex)
        {
            this.fpDetail_Sheet1.RowCount = 0;
            if (rowIndex >= 0)
            {
                FS.HISFC.Models.MedicalPackage.Fee.Package curpackage = this.fpChildPackage_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
                if (curpackage != null && curpackage.PackageInfo != null)
                {
                    ArrayList details = new ArrayList();
                    Hashtable packageDetails = this.GetPackageDetialHsTable();
                    FS.HISFC.Models.MedicalPackage.Package tmp = this.packageProcess.GetPackage(curpackage.PackageInfo.ID);
                    if (tmp.SpecialFlag == "1")
                    {
                        if (packageDetails.ContainsKey(curpackage.User01))
                        {
                            details = packageDetails[curpackage.User01] as ArrayList;
                        }
                    }
                    else
                    {
                        details = this.GetPackageDetailByID(curpackage.PackageInfo.ID);
                    }

                    foreach (FS.HISFC.Models.MedicalPackage.PackageDetail detail in details)
                    {
                        //{98A19959-7CD4-4f69-839D-D7020687D3A3}
                        if (string.IsNullOrEmpty(detail.Item.Name))
                        {
                            decimal tmpQTY = detail.Item.Qty;
                            if (detail.Item.SysClass.ID.ToString().Equals("P") ||
                                detail.Item.SysClass.ID.ToString().Equals("PCC") ||
                                detail.Item.SysClass.ID.ToString().Equals("PCZ"))
                            {
                                detail.Item = itemIntegrate.GetItem(detail.Item.ID);
                            }
                            else
                            {
                                detail.Item = itemMgr.GetUndrugByCode(detail.Item.ID);
                            }

                            detail.Item.Qty = tmpQTY;
                        }

                        this.fpDetail_Sheet1.Rows.Add(this.fpDetail_Sheet1.RowCount, 1);
                        string showStr = string.Empty;
                        showStr = detail.Item.Name;
                        showStr += "[";
                        showStr += detail.Item.Specs;
                        showStr += "]";
                        showStr += "*";
                        showStr += detail.Item.Qty.ToString();
                        showStr += detail.Unit;
                        this.fpDetail_Sheet1.Cells[this.fpDetail_Sheet1.RowCount - 1, 0].Value = showStr;
                    }
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
                    this.SetPackageColumnEnable(sv.RowCount - 1);
                }
                else
                {
                    this.SetColumnEnable(sv.RowCount - 1);
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

            this.SetDetailInfo(this.fpChildPackage_Sheet1.ActiveRowIndex);
        }

        /// <summary>
        /// 设置新增行列属性
        /// </summary>
        /// <param name="row"></param>
        private void SetPackageColumnEnable(int row)
        {
            for (int j = 0; j < this.fpPackage_Sheet1.Columns.Count; j++)
            {
                //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
                if (j == (int)PackageColumns.InputCode || j == (int)PackageColumns.Select || j == (int)PackageColumns.IsSpecial)
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
        /// 设置新增行列属性
        /// </summary>
        /// <param name="row"></param>
        private void SetColumnEnable(int row)
        {
            for (int j = 0; j < this.fpChildPackage_Sheet1.Columns.Count; j++)
            {
                if (j == (int)Columns.InputCode || j == (int)Columns.Select || j == (int)Columns.PackageNum)
                {
                    this.fpChildPackage_Sheet1.Cells[row, j].Locked = false;
                }
                else
                {
                    this.fpChildPackage_Sheet1.Cells[row, j].Locked = true;
                }
            }
        }

        #endregion

        #region 外部调用

        /// <summary>
        /// 根据收费列表设置收费项目
        /// </summary>
        /// <param name="packageList"></param>
        public void SetRecipePackage(ArrayList packageList)
        {
            this.fpPackage_Sheet1.RowCount = 0;
            this.fpChildPackage_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                this.fpPackage.StopCellEditing();
                this.fpPackage_Sheet1.Rows.Add(this.fpPackage_Sheet1.RowCount, 1);
                this.fpPackage_Sheet1.SetActiveCell(this.fpPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
                int currRow = this.fpPackage_Sheet1.RowCount - 1;
                this.fpPackage_Sheet1.Rows[currRow].Tag = package;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.Select].Value = true;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.InputCode].Value = package.PackageInfo.ID;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageName].Value = package.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageType].Value = package.PackageInfo.User02;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.TotCost].Value = package.Package_Cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.RealCost].Value = package.Real_Cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.GiftCost].Value = package.Gift_cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.ETCCost].Value = package.Etc_cost;
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

        /// <summary>
        /// 根据子套餐列表设置收费项目
        /// </summary>
        /// <param name="packageList"></param>
        public void SetChildPackage(ArrayList packageList)
        {
            this.fpChildPackage_Sheet1.RowCount = 0;
            foreach (FS.HISFC.Models.MedicalPackage.Fee.Package package in packageList)
            {
                this.fpChildPackage.StopCellEditing();
                this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
                this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
                int currRow = this.fpChildPackage_Sheet1.RowCount - 1;
                this.fpChildPackage_Sheet1.Rows[currRow].Tag = package;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.Select].Value = true;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.InputCode].Value = package.PackageInfo.ID;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageName].Value = package.PackageInfo.Name;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ParentPackageName].Value = package.ParentPackageInfo.Name;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageNum].Value = package.PackageNum.ToString();
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackagePrice].Value = package.PackageInfo.User01;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageType].Value = package.PackageInfo.User02;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageRange].Value = package.PackageInfo.User03;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.TotCost].Value = package.Package_Cost.ToString();
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.RealCost].Value = package.Real_Cost.ToString();
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.GiftCost].Value = package.Gift_cost.ToString();
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ETCCost].Value = package.Etc_cost.ToString();
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PricingOper].Value = package.DelimitOper;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PricingDate].Value = package.DelimitTime.ToString();
            }

            this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
            this.SetColumnEnable(this.fpChildPackage_Sheet1.RowCount - 1);
            if (this.fpChildPackage_Sheet1.RowCount == 1)
            {
                this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
            }
            else
            {
                this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 2, (int)Columns.InputCode);
            }

            this.SetDetailInfo(this.fpChildPackage_Sheet1.ActiveRowIndex);
            this.SetFeeInfoCost();
        }

        /// <summary>
        /// 设置当前行收费项目
        /// </summary>
        /// <param name="PackageFee"></param>
        public void SetPackageInfo(FS.HISFC.Models.MedicalPackage.Fee.Package PackageFee)
        {
            if(PackageFee.PackageInfo.PackageClass == "1")
            {
                this.fpPackage.StopCellEditing();
                int currRow = this.fpPackage_Sheet1.ActiveCell.Row.Index;

                this.fpPackage_Sheet1.Rows[currRow].Tag = PackageFee;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.Select].Value = true;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.InputCode].Value = PackageFee.PackageInfo.ID;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageName].Value = PackageFee.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageType].Value = PackageFee.PackageInfo.User02;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.TotCost].Value = PackageFee.Package_Cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.RealCost].Value = PackageFee.Real_Cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.GiftCost].Value = PackageFee.Gift_cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.ETCCost].Value = PackageFee.Etc_cost;


                if (currRow == this.fpPackage_Sheet1.RowCount - 1)
                {
                    this.fpPackage.Focus();
                    this.fpPackage_Sheet1.Rows.Add(this.fpPackage_Sheet1.RowCount, 1);
                    this.SetPackageColumnEnable(this.fpChildPackage_Sheet1.RowCount - 1);
                }
            }
            else if (PackageFee.PackageInfo.PackageClass == "2")
            {
                string parentCode = string.Empty;
                FS.HISFC.Models.MedicalPackage.Fee.Package curParent = this.fpPackage_Sheet1.ActiveRow.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

                if (curParent == null)
                {
                }

                this.fpChildPackage.StopCellEditing();
                int currRow = this.fpChildPackage_Sheet1.ActiveCell.Row.Index;

                this.fpChildPackage_Sheet1.Rows[currRow].Tag = PackageFee;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.Select].Value = true;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.InputCode].Value = PackageFee.PackageInfo.ID;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageName].Value = PackageFee.PackageInfo.Name;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ParentPackageName].Value = PackageFee.ParentPackageInfo.Name;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageNum].Value = PackageFee.PackageNum;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageType].Value = PackageFee.PackageInfo.User02;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackageRange].Value = PackageFee.PackageInfo.User03;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PackagePrice].Value = PackageFee.PackageInfo.User01;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.TotCost].Value = PackageFee.Package_Cost;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.RealCost].Value = PackageFee.Real_Cost;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.GiftCost].Value = PackageFee.Gift_cost;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.ETCCost].Value = PackageFee.Etc_cost;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PricingOper].Value = PackageFee.DelimitOper;
                this.fpChildPackage_Sheet1.Cells[currRow, (int)Columns.PricingDate].Value = PackageFee.DelimitTime.ToString();

                if (currRow == this.fpChildPackage_Sheet1.RowCount - 1)
                {
                    this.fpChildPackage_Sheet1.Rows.Add(this.fpChildPackage_Sheet1.RowCount, 1);
                    this.fpChildPackage.Focus();
                    this.SetColumnEnable(this.fpChildPackage_Sheet1.RowCount - 1);
                }

                this.SetDetailInfo(this.fpChildPackage_Sheet1.ActiveRowIndex);
                this.SetFeeInfoCost();
            }
        }

        public void RefreshParentData()
        {
            this.fpPackage.StopCellEditing();

            for (int i = 0; i < this.fpPackage_Sheet1.Rows.Count - 1; i++)
            {
                FS.HISFC.Models.MedicalPackage.Fee.Package curPackage = this.fpPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

                if (curPackage != null)
                {
                    this.fpPackage_Sheet1.Rows[i].Tag = curPackage;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.InputCode].Value = curPackage.PackageInfo.ID;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.PackageName].Value = curPackage.PackageInfo.Name;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.PackageType].Value = curPackage.PackageInfo.User02;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.TotCost].Value = curPackage.Package_Cost;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.RealCost].Value = curPackage.Real_Cost;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.GiftCost].Value = curPackage.Gift_cost;
                    this.fpPackage_Sheet1.Cells[i, (int)PackageColumns.ETCCost].Value = curPackage.Etc_cost;
                }
            }
        }

        public void RefreshData()
        {
            this.fpChildPackage.StopCellEditing();
            for (int i = 0; i < this.fpChildPackage_Sheet1.Rows.Count - 1;i++ )
            {
                FS.HISFC.Models.MedicalPackage.Fee.Package package = this.fpChildPackage_Sheet1.Rows[i].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

                if (package == null || string.IsNullOrEmpty(package.PackageInfo.ID))
                {
                    continue;
                }

                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.InputCode].Value = package.PackageInfo.ID;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PackageName].Value = package.PackageInfo.Name;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ParentPackageName].Value = package.ParentPackageInfo.Name;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PackageNum].Value = package.PackageNum;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PackagePrice].Value = package.PackageInfo.User01;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PackageType].Value = package.PackageInfo.User02;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PackageRange].Value = package.PackageInfo.User03;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.TotCost].Value = package.Package_Cost.ToString();
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.RealCost].Value = package.Real_Cost.ToString();
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.GiftCost].Value = package.Gift_cost.ToString();
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.ETCCost].Value = package.Etc_cost.ToString();
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PricingOper].Value = package.DelimitOper;
                this.fpChildPackage_Sheet1.Cells[i, (int)Columns.PricingDate].Value = package.DelimitTime.ToString();
            }
 
        }

        public void RefreshCurrentPackageData()
        {
            this.fpPackage.StopCellEditing();

            int currRow = this.fpPackage_Sheet1.ActiveCell.Row.Index;
            FS.HISFC.Models.MedicalPackage.Fee.Package curPackage = this.fpPackage_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

            if (curPackage != null)
            {
                this.fpPackage_Sheet1.Rows[currRow].Tag = curPackage;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.InputCode].Value = curPackage.PackageInfo.ID;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageName].Value = curPackage.PackageInfo.Name;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.PackageType].Value = curPackage.PackageInfo.User02;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.TotCost].Value = curPackage.Package_Cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.RealCost].Value = curPackage.Real_Cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.GiftCost].Value = curPackage.Gift_cost;
                this.fpPackage_Sheet1.Cells[currRow, (int)PackageColumns.ETCCost].Value = curPackage.Etc_cost;
            }
        }

        public void RefreshCurrentChildData()
        {
            this.fpChildPackage.StopCellEditing();
            int row = this.fpChildPackage_Sheet1.ActiveRowIndex;

            FS.HISFC.Models.MedicalPackage.Fee.Package package = this.fpChildPackage_Sheet1.Rows[row].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;

            if (package == null || string.IsNullOrEmpty(package.PackageInfo.ID))
            {
                return;
            }

            package.Package_Cost = package.PackageNum * Decimal.Parse(package.PackageInfo.User01);
            package.Real_Cost = package.PackageNum * Decimal.Parse(package.PackageInfo.User01);
            package.Etc_cost = 0.0m;
            package.Gift_cost = 0.0m;

            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.InputCode].Value = package.PackageInfo.ID;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.PackageName].Value = package.PackageInfo.Name;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.ParentPackageName].Value = package.ParentPackageInfo.Name;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.PackageNum].Value = package.PackageNum;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.PackagePrice].Value = package.PackageInfo.User01;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.PackageType].Value = package.PackageInfo.User02;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.PackageRange].Value = package.PackageInfo.User03;
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.TotCost].Value = package.Package_Cost.ToString();
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.RealCost].Value = package.Real_Cost.ToString();
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.GiftCost].Value = package.Gift_cost.ToString();
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.ETCCost].Value = package.Etc_cost.ToString();
            this.fpChildPackage_Sheet1.Cells[row, (int)Columns.PricingOper].Value = package.DelimitOper;
            this.SetFeeInfoCost();
        }

        /// <summary>
        /// 获取当前编辑的套餐条目
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Fee.Package GetCurrentParentFeeRow()
        {
            if (this.fpPackage_Sheet1.ActiveCell == null)
                return null;

            int currRow = this.fpPackage_Sheet1.ActiveCell.Row.Index;
            return this.fpPackage_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
        }

        /// <summary>
        /// 获取当前编辑的条目
        /// </summary>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Fee.Package GetCurrentFeeRow()
        {
            if (this.fpChildPackage_Sheet1.ActiveCell == null)
                return null;

            int currRow = this.fpChildPackage_Sheet1.ActiveCell.Row.Index;
            return this.fpChildPackage_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
        }

        /// <summary>
        /// 按照是否勾选获取当前的费用条目
        /// </summary>
        /// <param name="Checked"></param>
        /// <returns></returns>
        public ArrayList GetCurrentFeeRow(bool Checked)
        {
            ArrayList CheckedFeeRow = new ArrayList();
            foreach (Row row in this.fpChildPackage_Sheet1.Rows)
            {
                if (row.Tag != null && 
                    row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Package &&
                    (bool)this.fpChildPackage_Sheet1.Cells[row.Index, (int)Columns.Select].Value == Checked)
                {
                    CheckedFeeRow.Add(row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package);
                }
            }
            return CheckedFeeRow;
        }

        /// <summary>
        /// 按照是否勾选获取当前的费用条目
        /// </summary>
        /// <param name="Checked"></param>
        /// <returns></returns>
        public ArrayList GetAllFeeRow()
        {
            ArrayList CheckedFeeRow = new ArrayList();
            foreach (Row row in this.fpChildPackage_Sheet1.Rows)
            {
                if (row.Tag != null &&
                    row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Package)
                {
                    CheckedFeeRow.Add(row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package);
                }
            }
            return CheckedFeeRow;
        }

        /// <summary>
        /// 按照是否勾选获取当前的费用条目
        /// </summary>
        /// <param name="Checked"></param>
        /// <returns></returns>
        public ArrayList GetFeeRow()
        {
            ArrayList FeeRow = new ArrayList();
            foreach (Row row in this.fpChildPackage_Sheet1.Rows)
            {
                if (row.Tag != null && row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Package)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package current = (row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).Clone();

                    if(current.PackageNum <= 0)
                    {
                        return null;
                    }
                    else if (current.PackageNum == 1)
                    {
                        FeeRow.Add((row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).Clone());
                    }
                    else
                    {
                        decimal RealCost = current.Real_Cost;
                        decimal EtcCost = current.Etc_cost;

                        for (int i = 0; i < current.PackageNum; i++)
                        {
                            decimal tmpReal = 0.0m;
                            decimal tmpEtc = 0.0m;
                            FS.HISFC.Models.MedicalPackage.Fee.Package tmp = current.Clone();
                            //数量不为1时，只有第一次进来时第一条取的是原先的id,其余记录需要创建新的id
                            current.ID = ""; 
                            tmp.Package_Cost = current.Package_Cost / tmp.PackageNum;

                            if (i != current.PackageNum - 1)
                            {
                                tmpReal = Math.Floor(current.Real_Cost * 100 / current.PackageNum) / 100;
                                tmpEtc = Math.Floor(current.Etc_cost * 100 / current.PackageNum) / 100;
                            }
                            else
                            {
                                tmpReal = RealCost;
                                tmpEtc = EtcCost;
                            }

                            if (RealCost >= tmpReal)
                            {
                                RealCost -= tmpReal;
                            }
                            else
                            {
                                RealCost = 0;
                                tmpReal = RealCost;
                            }

                            if (EtcCost >= tmpEtc)
                            {
                                EtcCost -= tmpEtc;
                            }
                            else
                            {
                                tmpEtc = EtcCost;
                                EtcCost = 0;
                            }

                            if (tmp.Package_Cost > tmpReal + tmpEtc)
                            {
                                decimal diff = tmp.Package_Cost - tmpReal - tmpEtc;

                                if (RealCost + EtcCost >= diff)
                                {
                                    if (RealCost >= diff)
                                    {
                                        tmpReal += diff;
                                        RealCost -= diff;
                                    }
                                    else
                                    {
                                        tmpReal += RealCost;
                                        RealCost = 0;
                                        diff -= RealCost;

                                        if (EtcCost >= diff)
                                        {
                                            EtcCost -= diff;
                                            tmpEtc += diff;
                                        }
                                        else
                                        {
                                            return null;
                                        }
                                    }
                                }
                                else
                                {
                                    return null;
                                }
                            }
                            
                            tmp.Real_Cost = tmpReal;
                            tmp.Etc_cost = tmpEtc;
                            tmp.PackageNum = 1;

                            FeeRow.Add(tmp);
                        }
                    }

                }
            }
            return FeeRow;
        }

        /// <summary>
        /// 按照是否勾选获取当前的费用条目
        /// </summary>
        /// <param name="Checked"></param>
        /// <returns></returns>
        public ArrayList GetCurrentPackageFeeRow(bool Checked)
        {
            ArrayList CheckedPackageFeeRow = new ArrayList();
            foreach (Row row in this.fpPackage_Sheet1.Rows)
            {
                if (row.Tag != null &&
                    row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Package &&
                    (bool)this.fpPackage_Sheet1.Cells[row.Index, (int)PackageColumns.Select].Value == Checked)
                {
                    CheckedPackageFeeRow.Add(row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package);
                }
            }
            return CheckedPackageFeeRow;
        }



        /// <summary>
        /// {56809DCA-CD5A-435e-86F0-93DE99227DF4}
        /// 获取当前选择的套餐
        /// </summary>
        /// <param name="Checked"></param>
        /// <returns></returns>
        public ArrayList GetPackageSelected()
        {
            ArrayList CheckedPackageFeeRow = new ArrayList();
            foreach (Row row in this.fpPackage_Sheet1.Rows)
            {
                if (row.Tag != null &&
                    row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Package &&
                    (bool)this.fpPackage_Sheet1.Cells[row.Index, (int)PackageColumns.Select].Value)
                {

                    //{A777B7DF-AB62-4603-A0F6-B3643AD442F0}
                    FS.HISFC.Models.MedicalPackage.Fee.Package package = (row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package).Clone();

                    //医保套餐
                    if (this.fpPackage_Sheet1.Cells[row.Index, (int)PackageColumns.IsSpecial].Value != null &&
                        (bool)this.fpPackage_Sheet1.Cells[row.Index, (int)PackageColumns.IsSpecial].Value)
                    {
                        //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
                        //package.PayKind_Code = "2";
                        package.SpecialFlag = "1";
                    }
                    //普通套餐
                    else
                    {
                        //{56809DCA-CD5A-435e-86F0-93DE99227DF4}
                        //package.PayKind_Code = "1";
                        package.SpecialFlag = "0";
                    }

                    CheckedPackageFeeRow.Add(package);
                }
            }
            return CheckedPackageFeeRow;
        }

        /// <summary>
        /// 按照是否勾选获取当前的费用条目
        /// </summary>
        /// <param name="Checked"></param>
        /// <returns></returns>
        public ArrayList GetAllPackageFeeRow()
        {
            ArrayList CheckedPackageFeeRow = new ArrayList();
            foreach (Row row in this.fpPackage_Sheet1.Rows)
            {
                if (row.Tag != null &&
                    row.Tag is FS.HISFC.Models.MedicalPackage.Fee.Package)
                {
                    CheckedPackageFeeRow.Add(row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package);
                }
            }
            return CheckedPackageFeeRow;
        }

        /// <summary>
        /// 获取价格信息
        /// </summary>
        /// <returns></returns>
        public Hashtable GetCostInfo()
        {
            Hashtable hsCost = new Hashtable();

            hsCost.Add("TOT", 0.0m);  //套餐金额
            hsCost.Add("REAL", 0.0m); //实收金额
            hsCost.Add("ETC", 0.0m);  //优惠金额

            hsCost.Add("ACTU", 0.0m);  //实际支付
            hsCost.Add("GIFT", 0.0m);  //赠送支付
            hsCost.Add("DEPO", 0.0m);  //押金支付
            hsCost.Add("ROUND", 0.0m); //四舍五入

            foreach (Row row in this.fpChildPackage_Sheet1.Rows)
            {
                //if (row.Tag != null && (bool)this.fpChildPackage_Sheet1.Cells[row.Index, 0].Value)
                if (row.Tag != null)
                {
                    FS.HISFC.Models.MedicalPackage.Fee.Package curpackage = row.Tag as FS.HISFC.Models.MedicalPackage.Fee.Package;
                    hsCost["TOT"] = (decimal)hsCost["TOT"] + Decimal.Parse(curpackage.Package_Cost.ToString());
                    hsCost["REAL"] = (decimal)hsCost["REAL"] + Decimal.Parse(curpackage.Real_Cost.ToString());
                    hsCost["GIFT"] = (decimal)hsCost["GIFT"] + Decimal.Parse(curpackage.Gift_cost.ToString());
                    hsCost["ETC"] = (decimal)hsCost["ETC"] + Decimal.Parse(curpackage.Etc_cost.ToString());
                }
            }
            return hsCost;
        }

        /// <summary>
        /// 增加套餐记录
        /// </summary>
        public void AddPackageRow()
        {
            this.fpPackage.Focus();
            if (this.fpPackage_Sheet1.RowCount == 0)
            {
                this.AddRow(-1);
            }
            this.fpPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
        }

        /// <summary>
        /// 删除套餐记录
        /// </summary>
        public void DelPackageRow()
        {
            if (this.fpPackage_Sheet1.ActiveRow == null)
            {
                return;
            }

            int row = this.fpPackage_Sheet1.ActiveRow.Index;
            this.fpPackage.StopCellEditing();
            this.fpPackage_Sheet1.Rows.Remove(row, 1);

            this.SetFeeInfoCost();
            this.PackageChange();
        }

        /// <summary>
        /// 增加一条划价记录
        /// </summary>
        public void AddPriceRow()
        {
            this.fpChildPackage.Focus();
            if (this.fpChildPackage_Sheet1.RowCount == 0)
            {
                this.AddRow(-1);
            }
            this.fpChildPackage_Sheet1.SetActiveCell(this.fpChildPackage_Sheet1.RowCount - 1, (int)Columns.InputCode);
        }

        /// <summary>
        /// 删除划价记录
        /// </summary>
        public void DelPriceRow()
        {
            if (this.fpChildPackage_Sheet1.ActiveRow == null)
            {
                return;
            }

            int row = this.fpChildPackage_Sheet1.ActiveRow.Index;
            this.fpChildPackage.StopCellEditing();
            this.fpChildPackage_Sheet1.Rows.Remove(row, 1);
            this.SetFeeInfoCost();
        }

        public decimal GetTotFee(ArrayList PackageDetailList)
        {
            decimal totFee = 0.0m;
            foreach (FS.HISFC.Models.MedicalPackage.PackageDetail tmp in PackageDetailList)
            {
                if (tmp.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (tmp.UnitFlag.Equals("0"))
                    {
                        totFee += Math.Round((tmp.Item.Price / tmp.Item.PackQty) * tmp.Item.Qty, 2);
                    }
                    else
                    {
                        totFee += tmp.Item.Price * tmp.Item.Qty;
                    }
                }
                else
                {
                    totFee += tmp.Item.Price * tmp.Item.Qty;
                }
            }

            return totFee;
        }

        /// <summary>
        /// 清空界面
        /// </summary>
        public void Clear()
        {
            this.fpPackage.StopCellEditing();
            this.fpChildPackage.StopCellEditing();
            this.fpDetail.StopCellEditing();

            this.fpPackage_Sheet1.RowCount = 0;
            this.fpChildPackage_Sheet1.RowCount = 0;
            this.fpDetail_Sheet1.RowCount = 0;
        }

        #endregion

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
            TotCost = 4,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 5,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 6,

            /// <summary>
            /// 优惠金额
            /// </summary>
            ETCCost = 7,

            /// <summary>
            /// 是否特殊折扣
            /// </summary>
            IsSpecial = 8
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
            /// 名称
            /// </summary>
            ParentPackageName = 3,

            /// <summary>
            /// 数量
            /// </summary>
            PackageNum = 4,

            /// <summary>
            /// 套餐类别
            /// </summary>
            PackageType = 5,

            /// <summary>
            /// 套餐范围
            /// </summary>
            PackageRange = 6,

            /// <summary>
            /// 单价
            /// </summary>
            PackagePrice = 7,

            /// <summary>
            /// 套餐金额
            /// </summary>
            TotCost = 8,

            /// <summary>
            /// 实收金额
            /// </summary>
            RealCost = 9,

            /// <summary>
            /// 赠送金额
            /// </summary>
            GiftCost = 10,

            /// <summary>
            /// 优惠金额
            /// </summary>
            ETCCost = 11,

            /// <summary>
            /// 划价人
            /// </summary>
            PricingOper = 12,

            /// <summary>
            /// 划价日期
            /// </summary>
            PricingDate = 13,

            /// <summary>
            /// 是否修改
            /// </summary>
            changeFlag = 14
        }
    }
}

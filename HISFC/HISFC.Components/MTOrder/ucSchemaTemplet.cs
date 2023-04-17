using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FS.WinForms.Controls;

namespace FS.HISFC.Components.MTOrder
{
    public partial class ucSchemaTemplet : UserControl
    {
        public ucSchemaTemplet()
        {
            InitializeComponent();

            this.fpSpread1.Change      += new ChangeEventHandler(fpSpread1_Change);
            this.fpSpread1.EditModeOff += new EventHandler(fpSpread1_EditModeOff);

            this.fpSpread1.EditModeOn += new System.EventHandler(this.fpSpread1_EditModeOn);
            this.fpSpread1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(this.fpSpread1_EditChange);
        }

        #region 域
        //--------------------------------------------------------------------
        /// <summary>
        /// 模板集合
        /// </summary>
        private DataTable dsItems;
        private DataView dv;
        private List<FS.HISFC.Models.Base.Const> MTTypeList = new List<FS.HISFC.Models.Base.Const>();

        /// <summary>
        /// 显示星期
        /// </summary>
        private DayOfWeek week = DayOfWeek.Monday;
        /// <summary>
        /// 显示星期
        /// </summary>
        public DayOfWeek Week
        {
            get { return week; }
            set { week = value; }
        }
        /// <summary>
        /// 当前医技项目
        /// </summary>
        public FS.HISFC.Models.Base.Const MedTechType { get; set; }

#region 属性
        /// <summary>
        /// 停班颜色
        /// </summary>
        public Color StopColor { get; set; }

        /// <summary>
        /// 是否检查表格变更并提示保存
        /// </summary>
        public bool IsCheckChangceAndSave { get; set; }
#endregion
        #region 管理类
        /// <summary>
        /// 常数管理类(获取医技列表和医技类型)
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();
        /// <summary>
        /// 医技排班模板管理
        /// </summary>
        private FS.HISFC.BizLogic.MedicalTechnology.Templet templetMgr = new FS.HISFC.BizLogic.MedicalTechnology.Templet();
        /// <summary>
        /// 科室管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager doctMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region 列表
        /// <summary>
        /// 医技项目列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTech = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 医生列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbDoct = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        /// <summary>
        /// 医技部位列表
        /// </summary>
        private FS.FrameWork.WinForms.Controls.NeuListBoxPopup lbTechType = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
        #endregion
        #endregion

        #region 列
        protected enum cols
        {
            ID = 0,
            /// <summary>
            /// 项目代码
            /// </summary>
            ItemCode,
            /// <summary>
            /// 项目名称
            /// </summary>
            ItemName,
            /// <summary>
            /// 医技部位代码
            /// </summary>
            TypeCode,
            /// <summary>
            /// 医技部位名称
            /// </summary>
            TypeName,
            /// <summary>
            /// 医生工员
            /// </summary>
            DoctCode,
            /// <summary>
            /// 医生姓名
            /// </summary>
            DoctName,
            /// <summary>
            /// 开始时间
            /// </summary>
            BeginTime,
            /// <summary>
            /// 结束时间
            /// </summary>
            EndTime,
            /// <summary>
            /// 门诊预约限额
            /// </summary>
            BookLimit,
            /// <summary>
            /// 住院预约限额
            /// </summary>
            HostLimit,
            /// <summary>
            /// 是否停诊
            /// </summary>
            IsStop,
            /// <summary>
            /// 停诊原因
            /// </summary>
            StopReason,
            /// <summary>
            /// 备注
            /// </summary>
            Remark
        }
        #endregion

        #region 初始化
        /// <summary>
        ///  初始化
        /// </summary>        
        /// <param name="w"></param>
        /// <param name="type"></param>
        public void Init(DayOfWeek w)
        {            
            this.week = w;
            this.initDataSet();
            initTech();
            initTechType();
            initDoct();
            this.initFp();
            
            this.visible(false);
            SetFpFormat();
        }

        /// <summary>
        /// Init DataSet
        /// </summary>
        private void initDataSet()
        {
            dsItems = new DataTable("Templet");

            dsItems.Columns.AddRange(new DataColumn[]
			{
				new DataColumn(cols.ID.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.ItemCode.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.ItemName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.TypeCode.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.TypeName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.DoctCode.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.DoctName.ToString(),System.Type.GetType("System.String")),
				new DataColumn(cols.BeginTime.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.EndTime.ToString(),System.Type.GetType("System.DateTime")),
				new DataColumn(cols.BookLimit.ToString(),System.Type.GetType("System.Decimal")),
                new DataColumn(cols.HostLimit.ToString(),System.Type.GetType("System.Decimal")),
				new DataColumn(cols.IsStop.ToString(),System.Type.GetType("System.Boolean")),
				new DataColumn(cols.StopReason.ToString(),System.Type.GetType("System.String")),
                new DataColumn(cols.Remark.ToString(),System.Type.GetType("System.String"))
			});

        }
        /// <summary>
        /// 初始化farpoint
        /// </summary>
        private void initFp()
        {
            InputMap im;

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F2, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.F3, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        /// <summary>
        /// 初始化医技列表
        /// </summary>
        private void initTech()
        {
            this.lbTech.ItemSelected += new EventHandler(lbTech_SelectItem);
            this.groupBox1.Controls.Add(this.lbTech);
            this.lbTech.BackColor = this.label1.BackColor;
            this.lbTech.Font = new Font("宋体", 11F);
            this.lbTech.BorderStyle = BorderStyle.None;
            this.lbTech.Cursor = Cursors.Hand;
            this.lbTech.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbTech.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);
            //得到医技列表
            ArrayList al = constMgr.GetList("MT#MINFEE");

            if (al == null)
            {
                MessageBox.Show("获取[医技信息列表]时出错!" + constMgr.Err, "提示");
                return;
            }

            this.lbTech.AddItems(al);
        }

        /// <summary>
        /// 初始化医技部位列表
        /// </summary>
        private void initTechType()
        {
            //得到医技列表
            ArrayList al = constMgr.GetList("MTType");

            if (al == null)
            {
                MessageBox.Show("获取[医技类型]时出错!" + constMgr.Err, "提示");
                return;
            }

            foreach (FS.HISFC.Models.Base.Const c in al)
            {
                MTTypeList.Add(c);
            }
        }
        private void initTechType(string ItemCode)
        {
            groupBox1.Controls.Remove(lbTechType);
            this.lbTechType = new FS.FrameWork.WinForms.Controls.NeuListBoxPopup();
            this.lbTechType.ItemSelected += new EventHandler(lbTechType_SelectItem);
            this.groupBox1.Controls.Add(this.lbTechType);
            this.lbTechType.BackColor = this.label1.BackColor;
            this.lbTechType.Font = new Font("宋体", 11F);
            this.lbTechType.BorderStyle = BorderStyle.None;
            this.lbTechType.Cursor = Cursors.Hand;
            this.lbTechType.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbTechType.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            ArrayList al = new ArrayList();
            if (!string.IsNullOrEmpty(ItemCode))
                MTTypeList.Where(t => t.Memo == ItemCode).ToList().ForEach(t => al.Add(t));
            else
                MTTypeList.ForEach(t => al.Add(t));

            this.lbTechType.AddItems(al);
        }
        /// <summary>
        /// 初始化出诊医生
        /// </summary>
        private void initDoct()
        {
            this.lbDoct.ItemSelected += new EventHandler(lbDoct_SelectItem);
            this.groupBox1.Controls.Add(this.lbDoct);
            this.lbDoct.BackColor = this.label1.BackColor;
            this.lbDoct.Font = new Font("宋体", 11F);
            this.lbDoct.BorderStyle = BorderStyle.None;
            this.lbDoct.Cursor = Cursors.Hand;
            this.lbDoct.Location = new Point(this.label1.Left + 1, this.label1.Top + 1);
            this.lbDoct.Size = new Size(this.label1.Width - 2, this.label1.Height - 2);

            //得到医生列表
            ArrayList al = doctMgr.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (al == null)
            {
                MessageBox.Show("获取[门诊医生列表]时出错!" + doctMgr.Err, "提示");
                return;
            }

            this.lbDoct.AddItems(al);
            this.lbDoct.BringToFront();
        }
        #endregion

        #region 查找

        #endregion
        
        #region FarPoint操作
        /// <summary>
        /// 设置下来列表位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOn(object sender, System.EventArgs e)
        {

            fpSpread1.EditingControl.KeyDown += new KeyEventHandler(EditingControl_KeyDown);
            fpSpread1.EditingControl.DoubleClick += new EventHandler(EditingControl_DoubleClick);

            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.ItemName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.TypeName ||
                this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.DoctName)
            {
                if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.TypeName)
                {
                    initTechType(fpSpread1_Sheet1.Cells[fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ItemCode].Text);
                }
                this.setLocation();
                this.visible(false);
            }
            else
            { this.visible(false); }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditModeOff(object sender, EventArgs e)
        {
            try
            {
                this.fpSpread1.EditingControl.KeyDown -= new KeyEventHandler(EditingControl_KeyDown);
                this.fpSpread1.EditingControl.DoubleClick -= new EventHandler(EditingControl_DoubleClick);
            }
            catch { }
        }

        /// <summary>
        /// 检索下来列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            string text = this.fpSpread1_Sheet1.ActiveCell.Text.Trim();
           text = FS.FrameWork.Public.String.TakeOffSpecialChar(text, "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\");
            if (col == (int)cols.ItemName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.ItemName].Text = "";
                this.lbTech.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.TypeName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.TypeName].Text = "";
                this.lbTechType.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
            else if (col == (int)cols.DoctName)
            {
                this.fpSpread1_Sheet1.Cells[row, (int)cols.DoctName].Text = "";
                this.lbDoct.Filter(text);

                if (this.groupBox1.Visible == false) this.visible(true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread1_Change(object sender, ChangeEventArgs e)
        {
            if (this.fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.IsStop)
            {
                string cellText = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, this.fpSpread1_Sheet1.ActiveColumnIndex);

                if (cellText.ToUpper() == "FALSE")
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = Color.MistyRose;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = Color.MistyRose;
                }
                else
                {
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime].BackColor = SystemColors.Window;
                    this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime].BackColor = SystemColors.Window;
                }
            }
        }
        /// <summary>
        /// 回车处理
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                #region enter
                if (this.fpSpread1.ContainsFocus)
                {
                    int col = this.fpSpread1_Sheet1.ActiveColumnIndex;

                    if (col == (int)cols.ItemName)
                    {
                        if (this.selectTech() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.TypeName, false);
                    }
                    else if (col == (int)cols.TypeName)
                    {
                        if (this.selectTechType() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.DoctName, false);
                    }
                    else if (col == (int)cols.DoctName)
                    {
                        if (this.selectDoct() == -1) return false;
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BeginTime, false);
                    }
                    else if (col == (int)cols.BeginTime)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.EndTime, false);
                    }
                    else if (col == (int)cols.EndTime)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.BookLimit, false);
                    }
                    else if (col == (int)cols.BookLimit)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.HostLimit, false);
                    }
                    else if (col == (int)cols.HostLimit)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.IsStop, false);
                    }
                    else if (col == (int)cols.IsStop)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.StopReason, false);
                    }
                    else if (col == (int)cols.StopReason)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.Remark, false);
                    }
                    else if (col == (int)cols.Remark)
                    {
                        this.Add();
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Up)
            {
                #region up
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.priorRow(); }
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow > 0)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow - 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow - 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Down)
            {
                #region down
                if (this.fpSpread1.ContainsFocus)
                {
                    if (this.groupBox1.Visible)
                    { this.nextRow(); }
                    else
                    {
                        int CurrentRow = fpSpread1_Sheet1.ActiveRowIndex;
                        if (CurrentRow >= 0 && CurrentRow <= fpSpread1_Sheet1.RowCount - 2)
                        {
                            fpSpread1_Sheet1.ActiveRowIndex = CurrentRow + 1;
                            fpSpread1_Sheet1.AddSelection(CurrentRow + 1, 0, 1, 0);
                        }
                    }
                    return true;
                }
                #endregion

            }
            else if (keyData == Keys.Escape)
            {
                this.visible(false);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region 下拉控件功能
        /// <summary>
        /// 设置groupBox1的显示位置
        /// </summary>
        /// <returns></returns>
        private void setLocation()
        {
            Control cell = fpSpread1.EditingControl;
            if (cell == null) return;

            int y = fpSpread1.Top + cell.Top + cell.Height + this.groupBox1.Height + 7;
            if (y <= this.Height)
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, y - this.groupBox1.Height);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, y - this.groupBox1.Height);
                }
            }
            else
            {
                if (fpSpread1.Left + cell.Left + this.groupBox1.Width + 20 <= this.Width)
                {
                    this.groupBox1.Location = new Point(fpSpread1.Left + cell.Left + 20, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
                else
                {
                    this.groupBox1.Location = new Point(this.Width - this.groupBox1.Width - 10, fpSpread1.Top + cell.Top - this.groupBox1.Height - 7);
                }
            }
        }

        /// <summary>
        /// 设置控件是否可见
        /// </summary>
        /// <param name="visible"></param>
        private void visible(bool visible)
        {
            if (visible == false)
            { this.groupBox1.Visible = false; }
            else
            {
                int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
                if (col == (int)cols.ItemName)
                {
                    this.lbTech.BringToFront();
                    this.groupBox1.Visible = true;
                }
                else if (col == (int)cols.TypeName)
                {
                    this.lbTechType.BringToFront();
                    this.groupBox1.Visible = true;
                }else if (col == (int)cols.DoctName)
                {
                    this.lbDoct.BringToFront();
                    this.groupBox1.Visible = true;
                }
            }
        }
        /// <summary>
        /// 前一行
        /// </summary>
        private void nextRow()
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.ItemName)
            {
                this.lbTech.NextRow();
            }
            else if (col == (int)cols.TypeName)
            {
                this.lbTechType.NextRow();
            }
            else if (col == (int)cols.DoctName)
            {
                this.lbDoct.NextRow();
            }
        }
        /// <summary>
        /// 上一行
        /// </summary>
        private void priorRow()
        {
            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;
            if (col == (int)cols.ItemName)
            {
                this.lbTech.PriorRow();
            }
            else if (col == (int)cols.TypeName)
            {
                this.lbTechType.PriorRow();
            }
            else if (col == (int)cols.DoctName)
            {
                this.lbDoct.PriorRow();
            }
        }

        /// <summary>
        /// 选择医技项目
        /// </summary>
        /// <returns></returns>
        private int selectTech()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbTech.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemCode, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string ItemCode = this.fpSpread1_Sheet1.GetText(row, (int)cols.ItemCode);

                if (string.IsNullOrEmpty(ItemCode))
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemName, "", false);
            }

            return 0;
        }
        /// <summary>
        /// 选择医技类型
        /// </summary>
        /// <returns></returns>
        private int selectTechType()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbTechType.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.TypeName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.TypeCode, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string TypeCode = this.fpSpread1_Sheet1.GetText(row, (int)cols.TypeCode);

                if (string.IsNullOrEmpty(TypeCode))
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.TypeName, "", false);
            }

            return 0;
        }
        /// <summary>
        /// 选择医技
        /// </summary>
        /// <returns></returns>
        private int selectDoct()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            FS.FrameWork.Models.NeuObject obj;

            this.fpSpread1.StopCellEditing();

            if (this.groupBox1.Visible)
            {
                obj = new FS.FrameWork.Models.NeuObject();
                obj = this.lbDoct.GetSelectedItem();
                if (obj == null) return -1;

                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName, obj.Name, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctCode, obj.ID, false);
                this.visible(false);
            }
            else
            {
                string DoctCode = this.fpSpread1_Sheet1.GetText(row, (int)cols.DoctCode);

                if (string.IsNullOrEmpty(DoctCode))
                    this.fpSpread1_Sheet1.SetValue(row, (int)cols.DoctName, "", false);
            }

            return 0;
        }
        /// <summary>
        /// 选择医技
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbTech_SelectItem(object sender, EventArgs e)
        {
            this.selectTech();
            this.visible(false);
            return;
        }

        /// <summary>
        /// 选择医技
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbTechType_SelectItem(object sender, EventArgs e)
        {
            this.selectTechType();
            this.visible(false);
            return;
        }

        /// <summary>
        /// 选择医技
        /// </summary>
        /// <param name="sender"></param>
        ///<param name="e"></param>
        private void lbDoct_SelectItem(object sender, EventArgs e)
        {
            this.selectDoct();
            this.visible(false);
            return;
        }

        #endregion
       
        private void EditingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;
                if (t.SelectionStart == 0 && t.SelectionLength == 0)
                {
                    int row = 0, column = 0;
                    if (fpSpread1_Sheet1.ActiveColumnIndex == (int)cols.ItemName && fpSpread1_Sheet1.ActiveRowIndex != 0)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex - 1;
                        column = (int)cols.Remark;
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex != (int)cols.ItemName)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex;
                        column = this.getPriorVisibleColumn(this.fpSpread1_Sheet1.ActiveColumnIndex);

                    }
                    if (column != -1)
                    {
                        //屏蔽压缩显示报错

                        if ((column == (int)cols.ItemName || column == (int)cols.TypeName||column==(int)cols.DoctName) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        fpSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
            if (e.KeyCode == Keys.Right)
            {
                FarPoint.Win.Spread.CellType.GeneralEditor t = fpSpread1.EditingControl as FarPoint.Win.Spread.CellType.GeneralEditor;

                if (t.Text == null || t.Text == "" || t.SelectionStart == t.Text.Length)
                {
                    int row = fpSpread1_Sheet1.RowCount - 1, column = fpSpread1_Sheet1.ColumnCount - 1;
                    if (fpSpread1_Sheet1.ActiveColumnIndex == column && fpSpread1_Sheet1.ActiveRowIndex != row)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex + 1;

                        if (dv[row].Row.RowState == DataRowState.Added)
                        {
                            column = (int)cols.ItemName;
                        }
                        else
                        {
                            column = (int)cols.TypeName;
                        }
                    }
                    else if (fpSpread1_Sheet1.ActiveColumnIndex != column)
                    {
                        row = fpSpread1_Sheet1.ActiveRowIndex;

                        column = this.getNextVisibleColumn(this.fpSpread1_Sheet1.ActiveColumnIndex);
                    }

                    if (column != -1)
                    {
                        //屏蔽压缩显示报错
                        if ((column == (int)cols.ItemName || column == (int)cols.TypeName || column == (int)cols.DoctName) && dv[row].Row.RowState != DataRowState.Added)
                        {
                            return;
                        }

                        fpSpread1_Sheet1.SetActiveCell(row, column, true);
                    }
                }
            }
        }

        private void EditingControl_DoubleClick(object sender, EventArgs e)
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            if (row < 0) return;

            int col = this.fpSpread1_Sheet1.ActiveColumnIndex;

            if (col == (int)cols.BeginTime || col == (int)cols.EndTime)
            {
                //显示行状态
                this.showColor(row);
            }
        }
        /// <summary>
        /// 设置行显示颜色
        /// </summary>
        /// <param name="row"></param>
        private void showColor(int row)
        {
            //显示行状态
            if (this.fpSpread1_Sheet1.GetText(row, (int)cols.IsStop).ToUpper() == "TRUE")
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.ColumnCount; j++)
                {
                    this.fpSpread1_Sheet1.Cells[row, j].BackColor = StopColor;
                }
            }
            else
            {
                for (int j = 0; j < this.fpSpread1_Sheet1.ColumnCount; j++)
                {
                    this.fpSpread1_Sheet1.Cells[row, j].BackColor = SystemColors.Window;
                }
                this.fpSpread1_Sheet1.Cells[row, (int)cols.BeginTime].BackColor = Color.MistyRose;
                this.fpSpread1_Sheet1.Cells[row, (int)cols.EndTime].BackColor = Color.MistyRose;
            }
        }
        /// <summary>
        /// 设置行显示颜色（全部）
        /// </summary>
        private void showColor()
        {
            int rowCount = this.fpSpread1_Sheet1.RowCount;
            if (rowCount > 0)
            {
                for (int i = 0; i < rowCount; i++)
                {
                    this.showColor(i);
                }
            }
        }

        private int getNextVisibleColumn(int col)
        {
            int count = this.fpSpread1_Sheet1.Columns.Count;

            while (col < count - 1)
            {
                col++;

                if (this.fpSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
        }

        private int getPriorVisibleColumn(int col)
        {
            while (col > 0)
            {
                col--;

                if (this.fpSpread1_Sheet1.Columns[col].Visible)
                {
                    return col;
                }
            }

            return -1;
        }

        #region 公有函数
        /// <summary>
        /// 按医技项目查询一日排班记录
        /// </summary>
        /// <param name="ItemCode"></param>
        public void Query(FS.HISFC.Models.Base.Const Type)
        {
            this.MedTechType = Type;
            string ItemCode="ALL";
            if (Type != null)
                ItemCode = Type.ID;

            List<FS.HISFC.Models.MedicalTechnology.Templet> templetList = this.templetMgr.Query(week, ItemCode);
            if (templetList == null)
            {
                MessageBox.Show("查询排班模板信息出错!" + this.templetMgr.Err, "提示");
                return;
            }

            dsItems.Rows.Clear();

            try
            {
                templetList.ForEach(templet =>
                    {
                        dsItems.Rows.Add(new object[]{
                            templet.ID,
                            templet.ItemCode,
                            templet.ItemName,
                            templet.TypeCode,
                            templet.TypeName,
                            templet.DoctCode,
                            templet.DoctName,
                            templet.BeginTime,
                            templet.EndTime,
                            templet.BookLimit,
                            templet.HostLimit,
                            templet.IsStop,
                            templet.StopReason,
                            templet.Remark
                        });
                    });
            }
            catch (Exception e)
            {
                MessageBox.Show("查询排班信息生成DataSet时出错!" + e.Message, "提示");
                return;
            }
            dsItems.AcceptChanges();

            dv = dsItems.DefaultView;
            dv.AllowDelete = true;
            dv.AllowEdit = true;
            dv.AllowNew = true;
            //this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.Rows.Count);
            this.fpSpread1_Sheet1.DataSource = dv;
            this.fpSpread1_Sheet1.DataMember = "Templet";

            this.SetFpFormat();

            //
            if (this.fpSpread1_Sheet1.RowCount > 0)
            {
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    this.showColor(i);
                }
            }

        }

        /// <summary>
        /// 设置Farpoint显示格式
        /// </summary>
        private void SetFpFormat()
        {
            FarPoint.Win.Spread.CellType.NumberCellType numbCellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            numbCellType.DecimalPlaces = 0;
            numbCellType.MaximumValue = 9999;
            numbCellType.MinimumValue = 0;

            FarPoint.Win.Spread.CellType.DateTimeCellType dtCellType = new FarPoint.Win.Spread.CellType.DateTimeCellType();
            dtCellType.DateTimeFormat = FarPoint.Win.Spread.CellType.DateTimeFormat.UserDefined;
            dtCellType.UserDefinedFormat = "HH:mm";

            this.fpSpread1_Sheet1.Columns[(int)cols.ID].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)cols.ItemCode].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)cols.TypeCode].Visible = false;
            this.fpSpread1_Sheet1.Columns[(int)cols.DoctCode].Visible = false;


            //没办法,要求压缩重复显示项，就不能再让人修改了，阉了它
            //if (!this.IsShowDoctType)
            //{
            //    if (this.fpSpread1_Sheet1.RowCount > 0)
            //    {
            //        this.fpSpread1_Sheet1.Cells[0, (int)cols.DeptName, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DeptName].CellType = txtCellType;
            //        this.fpSpread1_Sheet1.Cells[0, (int)cols.DoctName, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DoctName].CellType = txtCellType;
            //        this.fpSpread1_Sheet1.Cells[0, (int)cols.Noon, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.Noon].CellType = txtCellType;
            //        this.fpSpread1_Sheet1.Cells[0, (int)cols.DoctType, this.fpSpread1_Sheet1.RowCount - 1, (int)cols.DoctType].CellType = txtCellType;
            //    }
            //}

            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].ForeColor = Color.Red;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].Font = new Font("宋体", 9, FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].CellType = numbCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].ForeColor = Color.Purple;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].Font = new Font("宋体", 9, FontStyle.Bold);
            this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].CellType = dtCellType;
            this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].CellType = dtCellType;

            this.fpSpread1_Sheet1.Columns[(int)cols.ItemName].Width = 80F;
            this.fpSpread1_Sheet1.Columns[(int)cols.TypeName].Width = 220F;
            this.fpSpread1_Sheet1.Columns[(int)cols.DoctName].Width = 100F;
            this.fpSpread1_Sheet1.Columns[(int)cols.BeginTime].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.EndTime].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.BookLimit].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.HostLimit].Width = 50F;
            this.fpSpread1_Sheet1.Columns[(int)cols.IsStop].Width = 40F;
            this.fpSpread1_Sheet1.Columns[(int)cols.StopReason].Width = 110F;
            this.fpSpread1_Sheet1.Columns[(int)cols.Remark].Width = 120F;
        }
#region private void Span
        /// <summary>
        /// 抑止重复显示
        /// </summary>
        //private void Span()
        //{
        //    ///
        //    int colLastDept = 0;
        //    int colLastDoct = 0;
        //    int colLastNoon = 0;
        //    int rowCnt = this.fpSpread1_Sheet1.RowCount;

        //    FarPoint.Win.Spread.CellType.TextCellType txtCellType = new FarPoint.Win.Spread.CellType.TextCellType();
        //    txtCellType.ReadOnly = true;

        //    for (int i = 0; i < rowCnt; i++)
        //    {
        //        //显示行状态
        //        //if (this.fpSpread1_Sheet1.GetText(i, (int)cols.Valid).ToUpper() == "FALSE")
        //        //{
        //        //    this.fpSpread1_Sheet1.Cells[i, (int)cols.BeginTime].BackColor = Color.Red;
        //        //    this.fpSpread1_Sheet1.Cells[i, (int)cols.EndTime].BackColor = Color.Red;
        //        //}
        //        this.showColor(i);

        //        if (i > 0 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
        //        {
        //            if (i - colLastDept > 1)
        //            {
        //                this.fpSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept, 1);
        //            }

        //            colLastDept = i;
        //        }

        //        //最后一行处理
        //        if (i > 0 && i == rowCnt - 1 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName))
        //        {
        //            this.fpSpread1_Sheet1.Models.Span.Add(colLastDept, (int)cols.DeptName, i - colLastDept + 1, 1);
        //        }

        //        ///医生
        //        ///
        //        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct &&
        //            i > 0 &&
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName))
        //        {
        //            if (i - colLastDoct > 1)
        //            {
        //                this.fpSpread1_Sheet1.Models.Span.Add(colLastDoct, (int)cols.DoctName, i - colLastDoct, 1);
        //            }
        //            colLastDoct = i;
        //        }

        //        //最后一行
        //        if (this.SchemaType == FS.HISFC.Models.Base.EnumSchemaType.Doct &&
        //            i > 0 &&
        //            i == rowCnt - 1 && this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName))
        //        {
        //            this.fpSpread1_Sheet1.Models.Span.Add(colLastDoct, (int)cols.DoctName, i - colLastDoct + 1, 1);
        //        }

        //        ///午别
        //        ///
        //        if (i > 0 &&
        //            (this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.Noon) ||
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) != this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName)))
        //        {
        //            if (i - colLastNoon > 1)
        //            {
        //                this.fpSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.Noon, i - colLastNoon, 1);
        //            }
        //            colLastNoon = i;
        //        }
        //        //最后一行
        //        if (i > 0 && i == rowCnt - 1 &&
        //            (this.fpSpread1_Sheet1.GetText(i, (int)cols.Noon) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.Noon) ||
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DeptName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DeptName) ||
        //            this.fpSpread1_Sheet1.GetText(i, (int)cols.DoctName) == this.fpSpread1_Sheet1.GetText(i - 1, (int)cols.DoctName)))
        //        {
        //            this.fpSpread1_Sheet1.Models.Span.Add(colLastNoon, (int)cols.Noon, i - colLastNoon + 1, 1);
        //        }

        //    }
        //}
#endregion

        /// <summary>
        /// 
        /// </summary>
        public void Add()
        {

            this.fpSpread1_Sheet1.Rows.Add(this.fpSpread1_Sheet1.RowCount, 1);

            this.fpSpread1_Sheet1.ActiveRowIndex = this.fpSpread1_Sheet1.RowCount - 1;
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;

            this.fpSpread1_Sheet1.SetValue(row, (int)cols.ID, templetMgr.GetSequence("MedicalTechnology.Templet.ID"), false);
            //如果医技类型不为空(选择了树节点)
            if (MedTechType != null)
            {
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemCode, MedTechType.ID, false);
                this.fpSpread1_Sheet1.SetValue(row, (int)cols.ItemName, MedTechType.Name, false);
            }
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.IsStop, false, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.BookLimit, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.HostLimit, 0, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.BeginTime, DateTime.Now, false);
            this.fpSpread1_Sheet1.SetValue(row, (int)cols.EndTime, DateTime.Now, false);


            this.fpSpread1.Focus();

            string ItemCode = this.fpSpread1_Sheet1.GetText(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ItemCode);


            if (string.IsNullOrEmpty(ItemCode))
            {
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.ItemName, false);
            }
            else
            {
                this.fpSpread1_Sheet1.SetActiveCell(this.fpSpread1_Sheet1.ActiveRowIndex, (int)cols.TypeName, false);
            }
            
        }
        /// <summary>
        /// 删除
        /// </summary>
        public void Del()
        {
            int row = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (row < 0 || this.fpSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("是否删除该条排班?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.fpSpread1.Focus();
                return;
            }

            this.fpSpread1.StopCellEditing();

            this.fpSpread1_Sheet1.Rows.Remove(row, 1);

            this.fpSpread1.Focus();
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public void DelAll()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0) return;

            if (MessageBox.Show("是否删除全部排班?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                this.fpSpread1.Focus();
                return;
            }

            this.fpSpread1_Sheet1.Rows.Remove(0, this.fpSpread1_Sheet1.RowCount);

            this.fpSpread1.Focus();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public int Save()
        {
            this.fpSpread1.StopCellEditing();

            if (fpSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }
            //{92A54BE2-EFAB-4a69-AA52-618B6250A326}
            foreach (DataRow dr in dsItems.Rows)
            {
                dr.EndEdit();
                
            }
            //增加
            DataTable dtAdd = dsItems.GetChanges(DataRowState.Added);
            //修改
            DataTable dtModify = dsItems.GetChanges(DataRowState.Modified);
            //删除
            DataTable dtDel = dsItems.GetChanges(DataRowState.Deleted);

            //验证
            if (Valid(dtAdd) == -1) return -1;
            if (Valid(dtModify) == -1) return -1;
            //转为实体集合
            IList alAdd = this.GetChanges(dtAdd);
            if (alAdd == null) return -1;
            IList alModify = this.GetChanges(dtModify);
            if (alModify == null) return -1;

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction SQLCA = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //SQLCA.BeginTransaction();

            this.templetMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            string rtnText = "";
            if (dtDel != null)
            {
                dtDel.RejectChanges();

                if (this.SaveDel(this.templetMgr, dtDel, ref rtnText) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(rtnText, "提示");
                    return -1;
                }
            }

            if (this.SaveAdd(this.templetMgr, alAdd, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "提示");
                return -1;
            }

            if (this.SaveModify(this.templetMgr, alModify, ref rtnText) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(rtnText, "提示");
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            dsItems.AcceptChanges();
            this.SetFpFormat();
            this.showColor();
            return 0;
        }
        /// <summary>
        /// 是否修改数据？
        /// </summary>
        /// <returns></returns>
        public bool IsChange()
        {
            if (!IsCheckChangceAndSave)
                return false;

            this.fpSpread1.StopCellEditing();

            if (fpSpread1_Sheet1.RowCount > 0)
            {
                dsItems.Rows[fpSpread1_Sheet1.ActiveRowIndex].EndEdit();
            }

            DataTable dt = dsItems.GetChanges();

            if (dt == null || dt.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 保存增加的记录
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alAdd"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveAdd(FS.HISFC.BizLogic.MedicalTechnology.Templet templetMgr, IList alAdd, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.MedicalTechnology.Templet templet in alAdd)
                {
                    if (templetMgr.Insert(templet) == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 保存修改记录
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="alModify"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveModify(FS.HISFC.BizLogic.MedicalTechnology.Templet templetMgr, IList alModify, ref string Err)
        {
            try
            {
                foreach (FS.HISFC.Models.MedicalTechnology.Templet templet in alModify)
                {
                    int returnValue = 0;

                    returnValue = templetMgr.Delete(templet.ID);

                    if (returnValue == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }

                    if (templetMgr.Insert(templet) == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 保存删除记录
        /// </summary>
        /// <param name="templetMgr"></param>
        /// <param name="dvDel"></param>
        /// <param name="Err"></param>
        /// <returns></returns>
        private int SaveDel(FS.HISFC.BizLogic.MedicalTechnology.Templet templetMgr, DataTable dvDel, ref string Err)
        {
            try
            {
                foreach (DataRow row in dvDel.Rows)
                {
                    if (templetMgr.Delete(row["ID"].ToString()) == -1)
                    {
                        Err = templetMgr.Err;
                        return -1;
                    }
                }
            }
            catch (Exception e)
            {
                Err = e.Message;
                return -1;
            }

            return 0;
        }
        /// <summary>
        /// 将表中数据转换为实体集合
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.MedicalTechnology.Templet> GetChanges(DataTable dt)
        {
            List<FS.HISFC.Models.MedicalTechnology.Templet> tempList = new List<FS.HISFC.Models.MedicalTechnology.Templet>();

            if (dt != null)
            {
                try
                {
                    DateTime current = DateTime.Parse("2000-01-01 00:00:00");
                    DateTime bookingTime;

                    foreach (DataRow row in dt.Rows)
                    {
                        FS.HISFC.Models.MedicalTechnology.Templet templet = new FS.HISFC.Models.MedicalTechnology.Templet();

                        templet.ID = row[cols.ID.ToString()].ToString();
                        templet.Week = this.week;
                        templet.ItemCode= row[cols.ItemCode.ToString()].ToString();
                        templet.ItemName = row[cols.ItemName.ToString()].ToString();
                        templet.TypeCode = row[cols.TypeCode.ToString()].ToString();
                        templet.TypeName = row[cols.TypeName.ToString()].ToString();
                        templet.DoctCode = row[cols.DoctCode.ToString()].ToString();
                        templet.DoctName = row[cols.DoctName.ToString()].ToString();

                        bookingTime = DateTime.Parse(row[cols.BeginTime.ToString()].ToString());
                        templet.BeginTime = DateTime.Parse(current.ToString("yyyy-MM-dd") + " " + bookingTime.Hour +
                            ":" + bookingTime.Minute + ":00");

                        bookingTime = DateTime.Parse(row[cols.EndTime.ToString()].ToString());
                        templet.EndTime = DateTime.Parse(current.ToString("yyyy-MM-dd") + " " + bookingTime.Hour +
                            ":" + bookingTime.Minute + ":00");

                        templet.BookLimit = FS.FrameWork.Function.NConvert.ToInt32(row[cols.BookLimit.ToString()].ToString());
                        templet.HostLimit = FS.FrameWork.Function.NConvert.ToInt32(row[cols.HostLimit.ToString()].ToString());
                        templet.IsStop = FS.FrameWork.Function.NConvert.ToBoolean(row[cols.IsStop.ToString()]);
                        templet.Remark = row[cols.Remark.ToString()].ToString();
                        templet.StopReason = row[cols.StopReason.ToString()].ToString();
                        templet.OperCode = FS.FrameWork.Management.Connection.Operator.ID;
                        templet.OperDate = DateTime.Now;
                        templet.IsValid = true;
                        tempList.Add(templet);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("生成实体集合时出错!" + e.Message, "提示");
                    return null;
                }
            }

            return tempList;
        }
        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int Valid(DataTable dt)
        {
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (string.IsNullOrEmpty(row[cols.ItemCode.ToString()].ToString()) || string.IsNullOrEmpty(row[cols.ItemName.ToString()].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("医技项目不能为空"), "提示");
                        return -1;
                    }
                    if (string.IsNullOrEmpty(row[cols.TypeCode.ToString()].ToString().Trim()) || string.IsNullOrEmpty(row[cols.TypeName.ToString()].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("医技类型不能为空"), "提示");
                        return -1;
                    }
                    if (string.IsNullOrEmpty(row[cols.DoctCode.ToString()].ToString().Trim()) || string.IsNullOrEmpty(row[cols.DoctName.ToString()].ToString()))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("医生姓名不能为空"), "提示");
                        return -1;
                    }

                    if (row[cols.BeginTime.ToString()] == null || row[cols.BeginTime.ToString()].ToString().Trim() == "" ||
                        row[cols.EndTime.ToString()] == null || row[cols.EndTime.ToString()].ToString().Trim() == "")
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "请输入预约时间段" ), "提示" );
                        return -1;
                    }
                    if (DateTime.Parse(row[cols.BeginTime.ToString()].ToString()).TimeOfDay >= DateTime.Parse(row[cols.EndTime.ToString()].ToString()).TimeOfDay)
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "开始时间不能大于结束时间" ), "提示" );
                        return -1;
                    }


                    if (row[cols.BookLimit.ToString()].ToString() == null || row[cols.BookLimit.ToString()].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约限额必须录入"), "提示");
                        return -1;
                    }
                    if (row[cols.HostLimit.ToString()].ToString() == null || row[cols.HostLimit.ToString()].ToString() == "")
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("预约限额必须录入"), "提示");
                        return -1;
                    }
                    if (FS.FrameWork.Public.String.ValidMaxLengh(row[cols.Remark.ToString()].ToString(), 50) == false)
                    {
                        MessageBox.Show( FS.FrameWork.Management.Language.Msg( "备注不能超过25个汉字" ), "提示" );
                        return -1;
                    }
                    row[cols.Remark.ToString()] = FS.FrameWork.Public.String.TakeOffSpecialChar(row[cols.Remark.ToString()].ToString(), "#", "%", "[", "'", "]", ",", "$", "(", ")", "|", "\'", "\"", "\\", "*", "^", "@", "!");
                }
                if (this.valid() < 0) return -1;
                
            }

            return 0;
        }
        /// <summary>
        /// 核对有没有重复时间段
        /// </summary>
        /// <returns></returns>
        private int valid()
        {

            if (this.fpSpread1_Sheet1.RowCount <= 0) return -1;
            for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
            {
                int count = MTTypeList.Where(t => t.ID == fpSpread1_Sheet1.Cells[i, (int)cols.TypeCode].Text && t.Memo == fpSpread1_Sheet1.Cells[i, (int)cols.ItemCode].Text).Count();
                if (count < 1)
                {
                    MessageBox.Show("第" + (i + 1) + "行医技项目与类型不匹配", "提示", MessageBoxButtons.OK);
                    return -1;
                }

                if (this.fpSpread1_Sheet1.GetValue(i, (int)cols.IsStop).ToString().ToUpper() != "TRUE")
                {
                    DateTime beginDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(i, (int)cols.BeginTime));
                    DateTime endDTi = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(i, (int)cols.EndTime));
                    for (int j = i + 1; j < this.fpSpread1_Sheet1.RowCount; j++)
                    {
                        if (this.fpSpread1_Sheet1.GetValue(i, (int)cols.IsStop).ToString().ToUpper() != "TRUE")
                        {
                            DateTime beginDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(j, (int)cols.BeginTime));
                            DateTime endDTj = FS.FrameWork.Function.NConvert.ToDateTime(this.fpSpread1_Sheet1.GetValue(j, (int)cols.EndTime));
                            if (//this.fpSpread1_Sheet1.GetValue(i, (int)cols.ItemCode).ToString() == this.fpSpread1_Sheet1.GetValue(j, (int)cols.ItemCode).ToString() &&
                                this.fpSpread1_Sheet1.GetValue(i, (int)cols.DoctCode).ToString() != "000000" &&
                                this.fpSpread1_Sheet1.GetValue(i, (int)cols.DoctCode).ToString() == this.fpSpread1_Sheet1.GetValue(j, (int)cols.DoctCode).ToString() &&
                                ((beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay > beginDTi.TimeOfDay) ||
                                (beginDTj.TimeOfDay < endDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay) ||
                                (beginDTj.TimeOfDay >= beginDTi.TimeOfDay && endDTj.TimeOfDay <= endDTi.TimeOfDay) ||
                                (beginDTj.TimeOfDay <= beginDTi.TimeOfDay && endDTj.TimeOfDay >= endDTi.TimeOfDay)))
                            {
                                MessageBox.Show("第" + (j + 1).ToString() + "行排班时间有误");
                                return -1;
                            }
                        }
                    }
                }

            }
            return 0;
        }

        #endregion
    }
}

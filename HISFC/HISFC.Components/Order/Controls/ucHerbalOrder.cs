using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// {49026086-DCA3-4af4-A064-58F7479C324A}
    /// </summary>
    public delegate void RefreshGroupTree();
    /// <summary>
    /// [功能描述: 草药医嘱开立]<br></br>
    /// [创 建 者: dorian]<br></br>
    /// [创建时间: 2007-10]<br></br>
    /// <修改记录
    ///  />
    /// </summary>
    public partial class ucHerbalOrder : UserControl
    {
        public ucHerbalOrder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.btnOK.Click += new EventHandler(btnOK_Click);
                this.btnCancel.Click += new EventHandler(btnCancel_Click);
                this.btnDel.Click += new EventHandler(btnDel_Click);
                this.Load += new EventHandler(ucHerbalOrder_Load);

                this.cmbMemo.KeyDown += new KeyEventHandler(cmbMemo_KeyDown);

                if (this.alLong == null || this.alShort == null)
                {
                    try
                    {
                        this.DataInit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        public ucHerbalOrder(bool isClinic, FS.HISFC.Models.Order.EnumType orderType, string deptCode)
            : this()
        {
            this.isClinic = isClinic;
            this.DeptCode = deptCode;
            this.OrderType = orderType;
            //this.fpEnter1_Sheet1.Rows.Count = 0;
            this.fpEnter1_Sheet1.Rows.Add(0, 1);

            this.cmbMemo.KeyDown += new KeyEventHandler(cmbMemo_KeyDown);


            if (this.alLong == null || this.alShort == null)
            {
                try
                {
                    this.DataInit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #region 属性

        /// <summary>
        /// 是否门诊使用
        /// </summary>
        private bool isClinic = false;

        /// <summary>
        /// 是否门诊使用
        /// </summary>
        public bool IsClinic
        {
            set
            {
                this.isClinic = value;

                //this.cmbOrderType.Visible = !value;
            }
        }

        /// <summary>
        /// 医嘱类别 0 长嘱 1 临嘱
        /// </summary>
        private FS.HISFC.Models.Order.EnumType orderType;

        /// <summary>
        /// 医嘱类别 0 长嘱 1 临嘱
        /// </summary>
        public FS.HISFC.Models.Order.EnumType OrderType
        {
            set
            {
                this.orderType = value;
            }
        }

        /// <summary>
        /// 开方医生科室
        /// </summary>
        private string deptCode = "";

        /// <summary>
        /// 开方医生所在科室
        /// </summary>
        public string DeptCode
        {
            set
            {
                this.deptCode = value;
            }
        }

        /// <summary>
        /// 开立后的草药医嘱信息
        /// </summary>
        ArrayList alOrder = new ArrayList();

        /// <summary>
        /// 开立后的草药医嘱信息
        /// </summary>
        public ArrayList AlOrder
        {
            get
            {
                if (this.alOrder == null)
                    this.alOrder = new ArrayList();
                return this.alOrder;
            }
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
            set
            {
                this.Clear();
                this.alOrder = value;
                this.SetValue(alOrder);
            }
        }

        /// <summary>
        /// 患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = null;

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            set
            {
                if (value == null)
                {
                    MessageBox.Show("患者信息赋值错误");
                    return;
                }
                this.patient = value;
            }
        }

        //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// <summary>
        /// 调用类型 A 开立 M修改
        /// </summary>
        private EnumOpenType openType;

        /// <summary>
        /// 调用类型 A 开立 M修改
        /// </summary>
        public EnumOpenType OpenType
        {
            get
            {
                return openType;
            }
            set
            {
                openType = value;
            }
        }

        #region 是否保存

        //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// <summary>
        /// 保存还是取消
        /// </summary>
        private bool isCancel = true;


        bool IsShowHerbalUsage = false;
        //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        /// <summary>
        /// 保存还是取消
        /// </summary>
        public bool IsCancel
        {
            get
            {
                return isCancel;
            }
            set
            {
                isCancel = value;
            }
        }
        #endregion

        #endregion

        #region 域变量

        /// <summary>
        /// 长期医嘱类型
        /// </summary>
        ArrayList alLong = null;

        /// <summary>
        /// 临时医嘱类型
        /// </summary>
        ArrayList alShort = null;

        FS.FrameWork.Public.ObjectHelper orderTypeHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// 项目信息
        /// </summary>
        ArrayList alItem = null;

        /// <summary>
        /// 用法信息
        /// </summary>
        ArrayList alUsage = new ArrayList();

        #region {C35FECE5-305E-452c-B22D-65BDEA3624AD}

        private ucPCCItemList ucItemList = null;

        private int currRow = -1;

        #endregion
        #endregion

        #region {49026086-DCA3-4af4-A064-58F7479C324A}
        /// <summary>
        /// 存组套时使用-刷新组套列表
        /// </summary>
        public event RefreshGroupTree refreshGroup;

        #endregion

        /// <summary>
        /// 数据加载初始化
        /// </summary>
        protected void DataInit()
        {
            #region 医嘱类别加载

            IsShowHerbalUsage = CacheManager.ContrlManager.GetControlParam<bool>("LHMZ03", false, false);

            ArrayList alOrderType = (CacheManager.InterMgr.QueryOrderTypeList());//医嘱类型
            foreach (FS.HISFC.Models.Order.OrderType obj in alOrderType)
            {
                if (obj.IsDecompose)
                {
                    if (alLong == null)
                        alLong = new ArrayList();
                    alLong.Add(obj);
                }
                else
                {
                    if (alShort == null)
                        alShort = new ArrayList();
                    alShort.Add(obj);
                }
            }
            #endregion

            #region 频次加载

            this.cmbFrequency.AddItems(SOC.HISFC.BizProcess.Cache.Order.QueryFrequency());
            if (cmbFrequency.Items.Count > 0)
            {
                this.cmbFrequency.SelectedIndex = 0;
            }
            #endregion


            #region 用法

            if (IsShowHerbalUsage)
            {
                alUsage.Clear();
                //FS.HISFC.Models.Base.Const usage = new FS.HISFC.Models.Base.Const();
                //this.alUsage.Add(usage);
                this.alUsage.AddRange(Classes.Function.HerbalUsageHelper.ArrayObject);
            }
            else
            {
                this.alUsage = Classes.Function.HelperUsage.ArrayObject;
            }

            if (this.alUsage == null)
            {
                MessageBox.Show("获取用法列表出错!");
                return;
            }

            this.cmbMemo.AddItems(this.alUsage);
            if (cmbMemo.alItems.Count > 0)
            {
                this.cmbMemo.SelectedIndex = 0;
            }
            #endregion

            #region 备注
            ArrayList alBoilPCCUsage = new ArrayList();
            ArrayList memoAl = CacheManager.GetConList("BoilPCCUsage");
            if (memoAl == null || memoAl.Count == 0)
            {
                FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
                obj1.ID = "";
                obj1.Name = "";
                memoAl.Add(obj1);
                obj1 = new FS.FrameWork.Models.NeuObject();
                obj1.ID = "0";
                obj1.Name = "先煎";
                memoAl.Add(obj1);
                obj1 = new FS.FrameWork.Models.NeuObject();
                obj1.ID = "1";
                obj1.Name = "后下";
                memoAl.Add(obj1);
                obj1 = new FS.FrameWork.Models.NeuObject();
                obj1.ID = "2";
                obj1.Name = "另煎";
                memoAl.Add(obj1);
                obj1 = new FS.FrameWork.Models.NeuObject();
                obj1.ID = "3";
                obj1.Name = "包煎";
                memoAl.Add(obj1);
                //this.cmbMemo.DataSource = memoAl;
                //this.cmbMemo.DisplayMember = "Name";
                //this.cmbMemo.ValueMember = "ID";
                //this.cmbMemo.AddItems(memoAl);
                //cmbMemo.SelectedIndex = 0;

                alBoilPCCUsage.AddRange(memoAl);
            }
            else
            {
                FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();

                alBoilPCCUsage.Add(obj1);
                //this.cmbMemo.AddItems(alBoilPCCUsage);
                //if (alBoilPCCUsage.Count > 0)
                //{
                //    cmbMemo.SelectedIndex = 0;
                //}
                alBoilPCCUsage.AddRange(memoAl);
            }
            #endregion



            this.fpEnter1.SetWidthAndHeight(150, 100);
            this.fpEnter1.SetIDVisiable(this.fpEnter1_Sheet1, (int)ColumnSet.ColTradeName, false);
            this.fpEnter1.SetColumnList(this.fpEnter1_Sheet1, (int)ColumnSet.ColUsage, alBoilPCCUsage); //备注

            fpEnter1_Sheet1.Columns[(Int32)ColumnSet.ColUnit].Locked = true;

            this.fpEnter1.ShowListWhenOfFocus = true;
            this.fpEnter1.SetItem += new FS.FrameWork.WinForms.Controls.NeuFpEnter.setItem(fpEnter1_SetItem);
            this.fpEnter1.KeyEnter += new FS.FrameWork.WinForms.Controls.NeuFpEnter.keyDown(fpEnter1_KeyEnter);

            #region {C35FECE5-305E-452c-B22D-65BDEA3624AD}
            this.fpEnter1.EditChange += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpEnter1_EditChange);

            #endregion
            FarPoint.Win.Spread.InputMap im;

            im = this.fpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = this.fpEnter1.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused);
            im.Put(new FarPoint.Win.Spread.Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            #region {C35FECE5-305E-452c-B22D-65BDEA3624AD}
            this.ucItemList = new ucPCCItemList();
            this.pnInputItem.Controls.Add(this.ucItemList);
            this.ucItemList.Init(deptCode);
            this.ucItemList.Hide();
            this.ucItemList.BringToFront();
            this.ucItemList.SelectItem += new ucPCCItemList.MyDelegate(ucItemList_SelectItem);
            #endregion

        }
        #region {C35FECE5-305E-452c-B22D-65BDEA3624AD}

        private void fpEnter1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == (int)ColumnSet.ColTradeName)
            {
                this.SetLocation();
                this.currRow = e.Row;
                string text = this.fpEnter1_Sheet1.ActiveCell.Text;
                this.ucItemList.Filter(text);
                if (!this.ucItemList.Visible)
                {
                    this.ucItemList.Visible = true;
                }
            }
        }

        private int ucItemList_SelectItem(Keys key)
        {
            this.ProcessItem();
            this.fpEnter1.Select();
            this.fpEnter1.Focus();
            return 0;
        }


        string errInfo = "";

        /// <summary>
        /// 处理this.fpDetail，项目名称的回车
        /// </summary>
        /// <returns>成功 1 失败 -1</returns>
        private int ProcessItem()
        {
            if (this.ucItemList.Visible == false)
            {
                this.currRow = this.fpEnter1_Sheet1.ActiveRowIndex;
                this.SetLocation();
                this.ucItemList.Filter("");
                this.ucItemList.Visible = true;

                return 0;
            }
            try
            {
                FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();

                int returnValue = this.ucItemList.GetSelectItem(out item);
                if (returnValue == -1 || returnValue == 0)
                {
                    return -1;
                }

                int currRow = this.fpEnter1_Sheet1.ActiveRowIndex;
                if (currRow < 0)
                {
                    return -1;
                }
                if (this.fpEnter1_Sheet1.GetText(currRow, (int)ColumnSet.ColTradeName) == "0")
                {
                    return -1;
                }

                if (Components.Order.OutPatient.Classes.Function.CheckDrugState(this.patient,null, new FS.FrameWork.Models.NeuObject(deptCode, "", ""), item, true, ref errInfo) <= 0)
                {
                    MessageBox.Show(errInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                item.Qty = 1;

                //添加划价明细
                this.SetSelectItem(item);

                this.ucItemList.Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                this.fpEnter1.Focus();

                return -1;
            }

            return 0;
        }

        private int SetLocation()
        {
            Control cell = this.fpEnter1.EditingControl;
            if (cell == null)
            {
                return -1;
            }

            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
            {
                int x = cell.Left;
                int y = cell.Top + cell.Height + 7 + this.neuGroupBox1.Height;
                if (y + ucItemList.Height + neuGroupBox2.Height > this.Height)
                {
                    y = this.Height - ucItemList.Height - neuGroupBox2.Height;
                    x = cell.Left + cell.Width;
                }

                //if (y <= this.Height)
                //{
                this.ucItemList.Location = new Point(x + 20, y);
                //}
                //else
                //{
                //    this.ucItemList.Location = new Point(cell.Left + 20, cell.Top - 7 + this.neuGroupBox1.Height);
                //}
            }
            return 0;
        }

        #endregion

        /// <summary>
        /// 由列表获取所选择项目
        /// </summary>
        /// <returns>成功返回1 出错返回－1</returns>
        protected int GetSelectItem()
        {
            int currentRow = this.fpEnter1_Sheet1.ActiveRowIndex;
            if (currentRow < 0) return 0;
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
            {
                #region {C35FECE5-305E-452c-B22D-65BDEA3624AD}
                this.ProcessItem();
                #endregion
                return 0;
            }
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColUsage)
            {
                //获取选中的信息
                FS.FrameWork.WinForms.Controls.PopUpListBox listBox = this.fpEnter1.getCurrentList(this.fpEnter1_Sheet1, (int)ColumnSet.ColUsage);
                FS.FrameWork.Models.NeuObject item = null;
                int rtn = listBox.GetSelectedItem(out item);
                if (item == null) return -1;
                this.SetSelectItem(item);
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// 处理由列表内所选择的项目
        /// </summary>
        /// <param name="obj">由弹出列表内所选择的项目</param>
        /// <returns>成功返回1 出错返回－1</returns>
        protected int SetSelectItem(FS.FrameWork.Models.NeuObject obj)
        {
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
            {
                if (hsPCCItem.Contains(obj.ID))
                {
                    MessageBox.Show("已经添加相同的项目【" + obj.Name + "】，不允许重复添加！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
                else
                {
                    hsPCCItem.Add(obj.ID, null);
                }

                FS.HISFC.Models.Pharmacy.Item item = CacheManager.PhaIntegrate.GetItem(obj.ID);
                if (item == null)
                {
                    MessageBox.Show("获取药品信息失败!" + CacheManager.PhaIntegrate.Err);
                    return -1;
                }
                item.User02 = obj.User02;		//取药药房编码
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColTradeName].Text = obj.Name;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColSpecs].Text = item.Specs;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString() + "/" + item.PriceUnit;


                //带出维护的默认每次用量
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColNum].Text = item.OnceDose.ToString();
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUnit].Text = item.DoseUnit;
                //this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Text = item.Usage.Name;
                this.fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].Tag = item;

                this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColNum;
                return 1;
            }
            if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColUsage)
            {
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Text = obj.Name;
                this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Tag = obj;

                if (this.fpEnter1_Sheet1.ActiveRowIndex == this.fpEnter1_Sheet1.Rows.Count - 1)
                {
                    this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.Rows.Count, 1);
                    this.fpEnter1_Sheet1.ActiveRowIndex = this.fpEnter1_Sheet1.Rows.Count - 1;
                }
                else
                {
                    this.fpEnter1_Sheet1.ActiveRowIndex = this.fpEnter1_Sheet1.ActiveRowIndex + 1;
                }
                this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColTradeName;
                return 1;
            }
            return 1;
        }

        /// <summary>
        /// 清除界面显示
        /// </summary>
        public void Clear()
        {
            this.alOrder = new ArrayList();
            this.fpEnter1_Sheet1.Rows.Count = 0;
            this.fpEnter1_Sheet1.Rows.Count = 1;
            this.txtNum.Text = "";
            this.dtEnd.Checked = false;

            hsPCCItem = new Hashtable();
        }

        /// <summary>
        /// 医嘱有效性检查
        /// </summary>
        /// <returns>无错误返回1 出错返回－1</returns>
        protected int Valid()
        {
            if (this.patient == null)
            {
                MessageBox.Show("患者信息未正确赋值");
                return -1;
            }
            if (this.cmbOrderType.Text == "")
            {
                MessageBox.Show("请选择医嘱类型");
                this.cmbOrderType.Select();
                this.cmbOrderType.Focus();
                return -1;
            }
            if (this.cmbFrequency.Text == "" || this.cmbFrequency.Tag == null || this.cmbFrequency.Tag.ToString() == null)
            {
                MessageBox.Show("请选择本剂草药用药频次");
                this.cmbFrequency.Text = string.Empty;
                this.cmbFrequency.Select();
                this.cmbFrequency.Focus();

                return -1;
            }
            if (this.txtNum.Text == "")
            {
                MessageBox.Show("请选择草药剂数！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txtNum.Select();
                this.txtNum.Focus();
                return -1;
            }
            if (FS.FrameWork.Function.NConvert.ToInt32(this.txtNum.Text) == 0)
            {
                MessageBox.Show("剂数只能为大于0的整数!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }
            if (this.cmbMemo.Text == "")
            {
                MessageBox.Show("请选择本剂草药的用法！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.cmbMemo.Select();
                this.cmbMemo.Focus();
                return -1;
            }
            if (this.dtEnd.Checked)
            {
                if (FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text) >= FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text))
                {
                    MessageBox.Show("医嘱停止时间不能大于等于医嘱开始时间！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }
            }
            for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            {
                if (this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColTradeName].Text == "")
                    continue;
                if (this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text == "")
                {
                    MessageBox.Show("请输入第" + (i + 1).ToString() + "行草药每剂量");
                    this.fpEnter1_Sheet1.ActiveRowIndex = i;
                    return -1;
                }

                //if (this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text == "")//&&!IsShowHerbalUsage
                //{
                //    MessageBox.Show("请输入第" + (i + 1).ToString() + "行草药用法");
                //    this.fpEnter1_Sheet1.ActiveRowIndex = i;
                //    return -1;
                //}

                #region addby xuewj 2010-03-22 {792D952F-1649-43bd-A12E-603F59AD3CDC} 每次剂量特殊字符判断
                string colNum = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text;
                if (colNum != FS.FrameWork.Public.String.TakeOffSpecialChar(colNum))
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行草药数量包含特殊字符，请重新输入!");
                    this.fpEnter1_Sheet1.ActiveRowIndex = i;
                    return -1;
                }
                #endregion

                #region 判断中药颗粒剂是否是袋装的整数倍
                //{9C7FFA35-05DF-4888-A88D-6A63DA65887C}
                decimal colNumDe = Math.Round(decimal.Parse(colNum),2);
                FS.HISFC.Models.Pharmacy.Item curItem = this.fpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.Item;
                if (curItem.DosageForm.ID == "231" && colNumDe % curItem.PackQty != 0)
                {
                    MessageBox.Show("第" + (i + 1).ToString() + "行草药数量必须为包装规格的整数倍，请重新输入!");
                    this.fpEnter1_Sheet1.ActiveRowIndex = i;
                    return -1;
                }
                #endregion
            }
            return 1;
        }

        /// <summary>
        /// 医嘱保存
        /// </summary>
        protected int Save()
        {
            if (this.Valid() == -1)
                return -1;

            string comboID = "";
            try
            {
                comboID = CacheManager.OutOrderMgr.GetNewOrderComboID();//添加组合号;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取医嘱组合号出错" + ex.Message);
                return -1;
            }
            FS.FrameWork.Models.NeuObject usageObj = null;
            FS.HISFC.Models.Order.Inpatient.Order inOrder;
            FS.HISFC.Models.Order.OutPatient.Order outOrder;

            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
            this.alOrder = new ArrayList();
            for (int i = 0; i < this.fpEnter1_Sheet1.Rows.Count; i++)
            {
                if (!this.isClinic)
                {
                    inOrder = new FS.HISFC.Models.Order.Inpatient.Order();
                    inOrder.Item = this.fpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.Item;
                    if (inOrder.Item == null || this.fpEnter1_Sheet1.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    //患者信息
                    inOrder.Patient = this.patient;
                    //医嘱组合号
                    inOrder.Combo.ID = comboID;

                    if (subCombNo > 0)
                    {
                        inOrder.SubCombNO = subCombNo;
                    }

                    //医嘱类型
                    inOrder.OrderType = this.orderTypeHelper.GetObjectFromID(this.cmbOrderType.Tag.ToString()) as FS.HISFC.Models.Order.OrderType;

                    inOrder.Usage.ID = this.cmbMemo.Tag.ToString();
                    inOrder.Usage.Name = this.cmbMemo.Text;

                    //单位  {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                    inOrder.Unit = (inOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;

                    //剂数
                    inOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.txtNum.Text);
                    //煎药方式

                    //备注
                    //usageObj = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text;

                    inOrder.Memo = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text;
                    
                    
                    //频次
                    inOrder.Frequency = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(this.cmbFrequency.Tag.ToString());
                    //每次量
                    if (this.orderType == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        inOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text);
                        inOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text) * inOrder.HerbalQty;
                        inOrder.Unit = (inOrder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit;
                    }
                    else
                    {
                        inOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text);

                        inOrder.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text) * inOrder.HerbalQty;
                        inOrder.Unit = (inOrder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit;


                        //计算总量
                        if (Components.Order.Classes.Function.ReComputeQty(inOrder) == -1)
                        {
                            return -1;
                        }
                    }

                    inOrder.DoseUnit = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUnit].Text;

                    inOrder.BeginTime = dtBegin.Value;
                    if (this.dtEnd.Checked)
                    {
                        inOrder.EndTime = dtEnd.Value;
                    }
                    //取药药房
                    inOrder.StockDept.ID = inOrder.Item.User02;

                    this.alOrder.Add(inOrder);
                }
                else if (this.isClinic)
                {
                    outOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                    outOrder.Item = this.fpEnter1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.Item;

                    if (outOrder.Item == null || this.fpEnter1_Sheet1.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    //患者信息
                    outOrder.Patient = this.patient;
                    //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
                    if (this.openType == EnumOpenType.Modified) //修改
                    {
                        //医嘱组合号
                        outOrder.Combo.ID = comboID;
                    }
                    else  //开立
                    {
                        //医嘱组合号
                        outOrder.Combo.ID = comboID;
                    }
                    //医嘱类型
                    //order.OrderType = this.orderTypeHelper.GetObjectFromID(this.cmbOrderType.Tag.ToString()) as FS.HISFC.Models.Order.OrderType;
                    
                    //用法
                    outOrder.Usage.ID = this.cmbMemo.Tag.ToString();
                    outOrder.Usage.Name = this.cmbMemo.Text;

                    //单位 {AE53ACB5-3684-42e8-BF28-88C2B4FF2360}
                    outOrder.Unit = (outOrder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;

                    //剂数
                    outOrder.HerbalQty = FS.FrameWork.Function.NConvert.ToInt32(this.txtNum.Text);

                    //usageObj = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text
                    //备注
                    outOrder.Memo = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUsage].Text;
                    
                    //频次
                    outOrder.Frequency = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(this.cmbFrequency.Tag.ToString());

                    outOrder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text);
                    outOrder.DoseUnit = this.fpEnter1_Sheet1.Cells[i, (int)ColumnSet.ColUnit].Text;

                    //计算总量
                    if (Components.Order.Classes.Function.ReComputeQty(outOrder) == -1)
                    {
                        return -1;
                    }

                    outOrder.BeginTime = dtBegin.Value;
                    if (this.dtEnd.Checked)
                    {
                        outOrder.EndTime = dtEnd.Value;
                    }
                    //取药药房
                    outOrder.StockDept.ID = outOrder.Item.User02;

                    this.alOrder.Add(outOrder.Clone());
                }
            }
            return 1;
        }

        /// <summary>
        /// 当前方号
        /// </summary>
        int subCombNo = -1;

        Hashtable hsPCCItem = new Hashtable();

        //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        protected int SetValue(ArrayList alHerbalOrder)
        {
            hsPCCItem = new Hashtable();

            FS.HISFC.Models.Order.Inpatient.Order inOrder = null;
            FS.HISFC.Models.Order.OutPatient.Order outOrder = null;
            FS.HISFC.Models.Pharmacy.Item item = null;
            object obj = new object();
            subCombNo = -1;

            //foreach (object obj in alHerbalOrder)
            for (int i = alHerbalOrder.Count - 1; i >= 0; i--)
            {
                obj = alHerbalOrder[i];
                this.fpEnter1_Sheet1.AddRows(0, 1);
                if (obj.GetType().ToString() == "FS.HISFC.Models.Order.OutPatient.Order") //门诊
                {
                    outOrder = obj as FS.HISFC.Models.Order.OutPatient.Order;

                    if (!hsPCCItem.Contains(outOrder.Item.ID))
                    {
                        hsPCCItem.Add(outOrder.Item.ID, null);
                    }

                    if (subCombNo == -1)
                    {
                        subCombNo = outOrder.SubCombNO;
                    }

                    item = new FS.HISFC.Models.Pharmacy.Item();
                    item = CacheManager.PhaIntegrate.GetItem(outOrder.Item.ID);
                    if (item == null)
                    {
                        MessageBox.Show("获取药品信息失败!" + CacheManager.PhaIntegrate.Err);
                        return -1;
                    }

                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColTradeName].Text = item.Name; //obj.Name;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColSpecs].Text = item.Specs; //item.Specs;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString() + "/" + item.PriceUnit;

                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUnit].Text = outOrder.DoseUnit;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUsage].Text = outOrder.Memo;
                    this.txtNum.Text = outOrder.HerbalQty.ToString();

                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColNum].Text = outOrder.DoseOnce.ToString();
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColTradeName].Text = outOrder.Item.Name;
                    //this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUsage].Tag = outOrder.Usage;
                    this.cmbFrequency.Text = outOrder.Frequency.ID;//执行科室
                    this.cmbMemo.Text = outOrder.Usage.Name;
                    this.cmbMemo.Tag = outOrder.Usage.ID;
                    //所有扣库科室都重取
                    item.User02 = outOrder.StockDept.ID;
                    this.fpEnter1_Sheet1.Rows[0].Tag = item;
                }
                else
                {
                    inOrder = obj as FS.HISFC.Models.Order.Inpatient.Order;

                    if (!hsPCCItem.Contains(inOrder.Item.ID))
                    {
                        hsPCCItem.Add(inOrder.Item.ID, null);
                    }

                    if (subCombNo == -1)
                    {
                        subCombNo = inOrder.SubCombNO;
                    }

                    item = new FS.HISFC.Models.Pharmacy.Item();
                    item = CacheManager.PhaIntegrate.GetItem(inOrder.Item.ID);
                    if (item == null)
                    {
                        MessageBox.Show("获取药品信息失败!" + CacheManager.PhaIntegrate.Err);
                        return -1;
                    }

                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColTradeName].Text = item.Name; //obj.Name;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColSpecs].Text = item.Specs; //item.Specs;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColPrice].Text = item.PriceCollection.RetailPrice.ToString() + "/" + item.PriceUnit;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUnit].Text = item.DoseUnit;
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUsage].Text = inOrder.Memo;
                    this.txtNum.Text = inOrder.HerbalQty.ToString();

                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColNum].Text = inOrder.DoseOnce.ToString();
                    this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColTradeName].Text = inOrder.Item.Name;
                    //this.fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUsage].Tag = inOrder.Usage;
                    this.cmbFrequency.Text = inOrder.Frequency.ID;//执行科室
                    this.cmbMemo.Text = inOrder.Usage.Name;
                    this.cmbMemo.Tag = inOrder.Usage.ID;
                    item.User02 = inOrder.StockDept.ID;
                    this.fpEnter1_Sheet1.Rows[0].Tag = item;
                }
            }
            return 1;
        }


        #region 事件
        private void ucHerbalOrder_Load(object sender, EventArgs e)
        {
            //{1BC3713E-0307-44df-80EB-44288BB06727}
            if (this.ParentForm != null)
            {
                this.ParentForm.ControlBox = true;
            }
            this.cmbFrequency.Focus();

            if (isClinic)
            {
                ArrayList al = new ArrayList();
                al.Add(new FS.FrameWork.Models.NeuObject("MZCF", "门诊处方", "MZCF"));
                cmbOrderType.AddItems(al);
                cmbOrderType.SelectedIndex = 0;
            }
            else
            {
                if (this.orderType == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    //this.cmbOrderType.DataSource = alLong;
                    //this.cmbOrderType.DisplayMember = "Name";
                    //this.cmbOrderType.ValueMember = "ID";
                    this.cmbOrderType.AddItems(alLong);
                    this.orderTypeHelper.ArrayObject = this.alLong;
                    if (alLong.Count > 0)
                    {
                        this.cmbOrderType.SelectedIndex = 0;
                    }
                }
                else
                {
                    //this.cmbOrderType.DataSource = alShort;
                    //this.cmbOrderType.DisplayMember = "Name";
                    //this.cmbOrderType.ValueMember = "ID";
                    this.cmbOrderType.AddItems(alShort);
                    this.orderTypeHelper.ArrayObject = this.alShort;
                    if (alShort.Count > 0)
                    {
                        this.cmbOrderType.SelectedIndex = 0;
                    }
                }
            }
        }

        private int fpEnter1_KeyEnter(Keys key)
        {
            if (key == Keys.Enter)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
                {
                    if (this.GetSelectItem() == -1)
                    {
                        MessageBox.Show("由列表获取所选择项目出错");
                        return -1;
                    }
                    return 1;
                }
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColNum)
                {
                    //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
                    if (fpEnter1_Sheet1.RowCount > 0 && fpEnter1_Sheet1.ActiveRowIndex > 0)
                    {//不是第一行，和第一行一样
                        //FS.FrameWork.Models.NeuObject obj = fpEnter1_Sheet1.Cells[0, (int)ColumnSet.ColUsage].Tag as FS.FrameWork.Models.NeuObject;
                        //if (obj == null)
                        //{
                        //    MessageBox.Show("请输入第一行的用法！");
                        //    return 0;
                        //}
                        //this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Text = obj.Name;
                        //this.fpEnter1_Sheet1.Cells[this.fpEnter1_Sheet1.ActiveRowIndex, (int)ColumnSet.ColUsage].Tag = obj;

                        if (this.fpEnter1_Sheet1.ActiveRowIndex == this.fpEnter1_Sheet1.Rows.Count - 1)
                        {
                            this.fpEnter1_Sheet1.Rows.Add(this.fpEnter1_Sheet1.Rows.Count, 1);
                            this.fpEnter1_Sheet1.ActiveRowIndex = this.fpEnter1_Sheet1.Rows.Count - 1;
                        }
                        else
                        {
                            this.fpEnter1_Sheet1.ActiveRowIndex = this.fpEnter1_Sheet1.ActiveRowIndex + 1;
                        }
                        this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColTradeName;
                        return 1;
                    }
                    this.fpEnter1_Sheet1.ActiveColumnIndex = (int)ColumnSet.ColUsage;
                    return 1;
                }
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColUsage)
                {
                    if (this.GetSelectItem() == -1)
                    {
                        MessageBox.Show("由列表获取所选择项目出错");
                        return -1;
                    }
                }
            }
            #region {C35FECE5-305E-452c-B22D-65BDEA3624AD}
            if (key == Keys.Up)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
                {
                    if (this.ucItemList.Visible)
                    {
                        this.ucItemList.PriorRow();
                        if (currRow > 0)
                        {
                            this.fpEnter1_Sheet1.ActiveRowIndex = this.currRow;
                            SendKeys.Send("{RIGHT}");
                        }
                    }
                }
            }
            if (key == Keys.Down)
            {
                if (this.fpEnter1_Sheet1.ActiveColumnIndex == (int)ColumnSet.ColTradeName)
                {
                    if (this.ucItemList.Visible)
                    {
                        this.ucItemList.NextRow();
                        if (currRow < this.fpEnter1_Sheet1.RowCount - 1)
                        {
                            this.fpEnter1_Sheet1.ActiveRowIndex = this.currRow;
                            SendKeys.Send("{RIGHT}");
                        }
                    }
                }
            }

            if (key == Keys.Escape)
            {
                if (this.ucItemList.Visible)
                {
                    this.ucItemList.Visible = false;
                }
            }
            #endregion
            return 0;
        }

        private int fpEnter1_SetItem(FS.FrameWork.Models.NeuObject obj)
        {
            if (this.SetSelectItem(obj) == -1)
            {
                MessageBox.Show("处理所选择项目失败");
            }
            return 0;
        }


        private void btnOK_Click(object sender, EventArgs e)
        {
            this.IsCancel = false;
            if (this.Save() == 1)
            {
                if (this.ParentForm != null)
                {
                    this.ParentForm.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.isCancel = true;
            //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
            this.alOrder = new ArrayList();
            if (this.ParentForm != null)
            {
                this.ParentForm.Close();
            }
        }

        private void btnDel_Click(object sender, System.EventArgs e)
        {
            if (this.fpEnter1_Sheet1.Rows.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = fpEnter1_Sheet1.Rows[this.fpEnter1_Sheet1.ActiveRowIndex].Tag as FS.HISFC.Models.Pharmacy.Item;
                if (phaItem != null)
                {
                    if (hsPCCItem.Contains(phaItem.ID))
                    {
                        hsPCCItem.Remove(phaItem.ID);
                    }
                }

                this.fpEnter1_Sheet1.RemoveRows(this.fpEnter1_Sheet1.ActiveRowIndex, 1);
                this.fpEnter1.SetAllListBoxUnvisible();
            }

            if (this.fpEnter1_Sheet1.RowCount == 0)
            {
                this.fpEnter1_Sheet1.Rows.Add(0, 1);
                this.fpEnter1_Sheet1.SetActiveCell(0, 0);
            }
        }

        #endregion

        #region 列设置
        private enum ColumnSet
        {
            /// <summary>
            /// 药品名称
            /// </summary>
            ColTradeName,
            /// <summary>
            /// 规格
            /// </summary>
            ColSpecs,
            /// <summary>
            /// 价格
            /// </summary>
            ColPrice,
            /// <summary>
            /// 数量
            /// </summary>
            ColNum,
            /// <summary>
            /// 单位
            /// </summary>
            ColUnit,
            /// <summary>
            /// 用法
            /// </summary>
            ColUsage
        }
        #endregion

        //{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988}
        private void btnNewRecipe_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.OpenType = FS.HISFC.Components.Order.Controls.EnumOpenType.Add;
        }

        /// <summary>
        /// 存组套
        /// {DC0E8BDB-D918-4c14-8474-3D2E6F986A57}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveGroup_Click(object sender, EventArgs e)
        {
            if (this.Save() == 1)
            {
                FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();
                if (this.isClinic)
                {
                    group.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
                }
                else
                {
                    group.InpatientType = FS.HISFC.Models.Base.ServiceTypes.I;
                }

                try
                {
                    group.IsManager = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager;
                }
                catch
                { }

                if (this.alOrder.Count > 0)
                {
                    group.alItems = this.alOrder;
                    group.ShowDialog();
                }

                #region {49026086-DCA3-4af4-A064-58F7479C324A}
                if (this.refreshGroup != null)
                {
                    this.refreshGroup();
                }
                #endregion
            }
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public void SetFocus()
        {
            if (this.cmbOrderType.Visible)
            {
                this.cmbOrderType.Focus();
            }
            else
            {
                this.cmbFrequency.Focus();
            }
        }

        private void cmbMemo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.fpEnter1.Focus();
                this.fpEnter1.SetActiveViewport(0, 0);
                if (this.fpEnter1_Sheet1.RowCount == 0)
                {
                    this.fpEnter1_Sheet1.Rows.Add(0, 1);
                    this.fpEnter1_Sheet1.SetActiveCell(0, 0);
                }
                else
                {
                    this.fpEnter1_Sheet1.SetActiveCell(this.fpEnter1_Sheet1.RowCount - 1, 0);
                }
            }
        }

        private void cmbOrderType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbFrequency.Focus();
            }
        }

        private void cmbFrequency_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtNum.Focus();
            }
        }

        private void txtNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cmbMemo.Focus();
            }
        }

    }

    /// <summary>
    /// 草药操作类型
    /// </summary>
    public enum EnumOpenType
    {
        /// <summary>
        /// 新增
        /// </summary>
        Add,

        /// <summary>
        /// 修改
        /// </summary>
        Modified
    }
}

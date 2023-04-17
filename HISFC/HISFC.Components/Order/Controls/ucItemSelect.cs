using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.Controls
{
    /// <summary>
    /// 获取最大方号
    /// </summary>
    /// <returns></returns>
    public delegate int GetMaxSubCombNoEvent(FS.HISFC.Models.Order.Inpatient.Order order,int sheetIndex);
    public delegate int GetSameSubCombNoOrderEvent(int sortID, ref FS.HISFC.Models.Order.Inpatient.Order order);

    /// <summary>
    /// 删除组号
    /// </summary>
    /// <param name="subCombNo"></param>
    /// <returns></returns>
    public delegate int DeleteSubCombNoEvent(int subCombNo,bool isLong);

    /// <summary>
    /// [功能描述: 医嘱项目选择控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucItemSelect : UserControl
    {
        public ucItemSelect()
        {
            InitializeComponent();
            
        }
        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler CatagoryChanged;

        #region 初始化

        public void Init()
        {
            if (DesignMode)
            {
                return;
            }
            if (FS.FrameWork.Management.Connection.Operator.ID == "")
            {
                return;
            }

            #region 设置tip
            tooltip.SetToolTip(this.ucInputItem1, "输入拼音码查询，开立医嘱(ESC取消列表)");
            tooltip.SetToolTip(this.txtQuantity, "输入总数量(回车输入结束)");
            tooltip.SetToolTip(this.dtBegin, "输入医嘱开始执行时间");
            tooltip.SetToolTip(this.dtEnd, "输入医嘱结束执行时间");
            #endregion
            try
            {
                this.ucInputItem1.DeptCode = CacheManager.LogEmpl.Dept.ID;//科室看自己科室的药品项目
                this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
                this.ucOrderInputByType1.InitControl(null, null, null);

                this.cmbOrderType1.DropDownStyle = ComboBoxStyle.DropDownList;

                this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.InHos;

                this.isCanEditUnitAndQTY = controlParam.GetControlParam("ZYLZ01", true, false);// {4D67D981-6763-4ced-814E-430B518304E2}
                //发药类型：O 门诊处方、I 住院医嘱、A 全部
                this.ucInputItem1.DrugSendType = "I";
                this.ucInputItem1.Init();//初始化项目列表
            }
            catch { }
            try
            {
                SetLongOrShort(false);
                ArrayList alResQuality = CacheManager.InterMgr.QueryConstantList("LongOrderResQuality");
                if (alResQuality != null)
                {
                    this.hsResQuality = new Hashtable();
                    foreach (FS.FrameWork.Models.NeuObject info in alResQuality)
                    {
                        this.hsResQuality.Add(info.ID, null);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.dtEnd.MinDate = DateTime.MinValue;
            this.dtEnd.Value = DateTime.Today.AddDays(1);
            this.dtEnd.Checked = false;


            AddEvent();
        }

        /// <summary>
        /// 添加事件
        /// </summary>
        private void AddEvent()
        {
            this.DeleteEvent();
            this.ucOrderInputByType1.ItemSelected += new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.cmbOrderType1.SelectedIndexChanged += new System.EventHandler(this.cmbOrderType1_SelectedIndexChanged);

            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave += new EventHandler(txtQuantity_Leave);

            this.cmbUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.TextChanged += new EventHandler(cmbUnit_TextChanged);
            this.ucOrderInputByType1.Leave += new EventHandler(ucOrderInputByType1_Leave);

            this.dtBegin.ValueChanged += new System.EventHandler(this.dtBegin_ValueChanged);
            this.dtEnd.ValueChanged += new EventHandler(dtEnd_ValueChanged);

            this.dtBegin.CloseUp += new EventHandler(dtBegin_CloseUp);
            this.dtEnd.CloseUp += new EventHandler(dtEnd_CloseUp);

            FS.HISFC.Components.Order.OutPatient.Classes.LogManager.Write("ucItemSelect添加事件!\r\n");
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void DeleteEvent()
        {
            this.ucOrderInputByType1.ItemSelected -= new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.cmbOrderType1.SelectedIndexChanged -= new System.EventHandler(this.cmbOrderType1_SelectedIndexChanged);

            this.ucInputItem1.SelectedItem -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            this.txtQuantity.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            this.txtQuantity.Leave -= new EventHandler(txtQuantity_Leave);

            this.cmbUnit.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.TextChanged -= new EventHandler(cmbUnit_TextChanged);
            this.ucOrderInputByType1.Leave -= new EventHandler(ucOrderInputByType1_Leave);

            this.dtBegin.ValueChanged -= new System.EventHandler(this.dtBegin_ValueChanged);
            this.dtEnd.ValueChanged -= new EventHandler(dtEnd_ValueChanged);

            this.dtBegin.CloseUp -= new EventHandler(dtBegin_CloseUp);
            this.dtEnd.CloseUp -= new EventHandler(dtEnd_CloseUp);

            FS.HISFC.Components.Order.OutPatient.Classes.LogManager.Write("ucItemSelect删除事件!\r\n");
        }
        #endregion

        #region 变量
        /// <summary>
        /// 医嘱变化时候用
        /// </summary>
        public event ItemSelectedDelegate OrderChanged;//

        /// <summary>
        /// 获取最大方号
        /// </summary>
        public event GetMaxSubCombNoEvent GetMaxSubCombNo;

        /// <summary>
        /// 获得相同方号医嘱
        /// </summary>
        public event GetSameSubCombNoOrderEvent GetSameSubCombNoOrder;

        /// <summary>
        /// 删除组号
        /// </summary>
        public event DeleteSubCombNoEvent DeleteSubComnNo;

        /// <summary>
        /// 当前操作类型
        /// </summary>
        public Operator OperatorType = Operator.Query;

        /// <summary>
        /// 当前行
        /// </summary>
        public int CurrentRow = -1;

        /// <summary>
        /// 是否进行组套编辑功能
        /// </summary>
        public bool EditGroup = false;
    
        /// <summary>
        /// 是否新增
        /// </summary>
        protected bool dirty = false;

        /// <summary>
        /// ToolTip
        /// </summary>
        protected ToolTip tooltip = new ToolTip();

        /// <summary>
        /// 长期医嘱限制性药品性质
        /// </summary>
        System.Collections.Hashtable hsResQuality = new Hashtable();
        /// <summary>
        /// 控制参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 住院临时医嘱是否可以编辑总量和单位// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        private bool isCanEditUnitAndQTY = false;
        /// <summary>
        /// 存储药品信息，只能存储实时性不高的信息
        /// </summary>
        [Obsolete("作废，用order里面的GetPHAItem方法", true)]
        Hashtable hsPhaItem = new Hashtable();

        #endregion

        #region 属性

        /// <summary>
        /// 当前医嘱
        /// </summary>
        protected FS.HISFC.Models.Order.Inpatient.Order order = null;

        /// <summary>
        /// 当前医嘱
        /// </summary>
        [DefaultValue(null)]
        public FS.HISFC.Models.Order.Inpatient.Order Order
        {
            get
            {
                return this.order;
            }
            set
            {
                try
                {
                    if (value == null)
                    {
                        return;
                    }

                    this.DeleteEvent();

                    this.order = value;

                    if (this.isNurseCreate)
                    {
                        if (this.order.ReciptDoctor.ID != FS.FrameWork.Management.Connection.Operator.ID)
                        {
                            MessageBox.Show("护士不允许修改他人开立的医嘱!");
                            return;
                        }
                    }

                    dirty = false;

                    this.LongOrShort = (int)this.order.OrderType.Type;
                    this.ucOrderInputByType1.IsNew = false;//修改旧医嘱

                    this.ucOrderInputByType1.Order = value;

                    this.ucInputItem1.FeeItem = this.order.Item;
                    this.cmbOrderType1.Tag = this.order.OrderType.ID;

                    ShowOrder(this.order);//读进来的医嘱

                    dirty = true;
                }
                catch
                {
                }
                finally
                {
                    AddEvent();
                }
            }
        }

        protected int longOrShort = 0;

        /// <summary>
        /// 长嘱 0 or临时医嘱 1
        /// </summary>
        public int LongOrShort
        {
            get
            {
                return longOrShort;
            }
            set
            {
                if (DesignMode) return;
                if (longOrShort == value) return;
                if (value == 0)
                {
                    this.SetLongOrShort(false);
                    
                }
                else
                {
                    this.SetLongOrShort(true);   
                }
                longOrShort = value;
            }
        }

        /// <summary>
        /// 是否护士开立
        /// </summary>
        private bool isNurseCreate = false;

        /// <summary>
        /// 是否护士开立
        /// </summary>
        [DefaultValue(false)]
        public bool IsNurseCreate
        {
            set
            {
                this.isNurseCreate = value;
            }
        }
        
        #endregion

        #region 事件
        protected bool bPermission = false;//是否知情同意书

        private void SetQty(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            if (inOrder.Item.ID == "999")
            {
                this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//可以更改
                this.cmbUnit.Enabled = this.txtQuantity.Enabled;
            }
            else
            {
                if (!isCanEditUnitAndQTY)// {4D67D981-6763-4ced-814E-430B518304E2}
                {
                    if (!inOrder.OrderType.IsDecompose && inOrder.Item.ItemType == EnumItemType.Drug)
                    {
                        // 1 包装单位总量取整 3 包装单位每次量取整
                        Components.Order.Classes.Function.GetSplitType(ref inOrder);

                        if (!string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType)
                            && "1、3".Contains(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType))
                        {
                            cmbUnit.Enabled = false;
                        }
                        else
                        {
                            this.cmbUnit.Enabled = true;
                        }

                        //这里设置为大部分药品都不允许修改总量，除了个别外用药（如滴眼液等）
                        //这里表示是外用药
                        if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(inOrder.Item.ID).SpecialFlag2 == "1")
                        {
                            txtQuantity.Enabled = true;
                            cmbUnit.Enabled = true;
                        }
                        else
                        {
                            txtQuantity.Enabled = false;
                            this.cmbUnit.Enabled = false;
                        }
                    }
                    else
                    {
                        //if (inOrder.Item.SysClass.ID.ToString() == "UL"
                        //    || inOrder.Item.SysClass.ID.ToString() == "UC")
                        //{
                        //    this.txtQuantity.Enabled = false;
                        //    cmbUnit.Enabled = false;
                        //}
                        //else
                        {
                            txtQuantity.Enabled = true;
                            cmbUnit.Enabled = true;
                        }
                    }
                }
                else
                {

                    txtQuantity.Enabled = true;
                    cmbUnit.Enabled = true;

                    Components.Order.Classes.Function.GetSplitType(ref inOrder);

                    // {4D67D981-6763-4ced-814E-430B518304E2}
                    if (!inOrder.OrderType.IsDecompose && inOrder.Item.ItemType == EnumItemType.Drug)
                    {
                        if (!string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType)
                            && "1、3".Contains(((FS.HISFC.Models.Pharmacy.Item)inOrder.Item).LZSplitType))
                        {
                            cmbUnit.Enabled = false;
                        }
                        else
                        {
                            this.cmbUnit.Enabled = true;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// 医嘱变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="changedField"></param>
        protected virtual void ucOrderInputByType1_ItemSelected(FS.HISFC.Models.Order.Inpatient.Order order, string changedField)
        {
            this.DeleteEvent();
            dirty = true;
            
            if (order.OrderType.IsDecompose == false
                && order.Item.ItemType == EnumItemType.Drug && order.Frequency.ID != "")
            {
                this.txtQuantity.Text = order.Qty.ToString();
                this.cmbUnit.Tag = order.Unit;
            }
            
            this.myOrderChanged(order,changedField);
            dirty = false;
            this.AddEvent();
        }

        protected void myOrderChanged(object sender,string enumOrderFieldList)
        {
            try
            {
                if (this.CurrentRow == -1)
                {
                    this.CurrentRow = 0;
                    this.OperatorType = Operator.Add;//添加
                }
                else
                {
                    this.OperatorType = Operator.Modify;
                }

                this.order = sender as FS.HISFC.Models.Order.Inpatient.Order;//控件传出的对象
                this.ucOrderInputByType1.IsNew = false;//修改旧医嘱
                this.ucOrderInputByType1.Order = this.order;

                this.OrderChanged(order, enumOrderFieldList);
            }
            catch { }
        }

        /// <summary>
        /// 数量变化-跳到下一级完成其它输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e == null || e.KeyChar == 13)
            {
                if (this.cmbUnit.Enabled)
                {
                    this.cmbUnit.Focus();
                }
                else
                {
                    this.txtCombNo.Focus();
                }
            }
        }

        /// <summary>
        /// 单位keyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.order == null) return;
            if (e == null || e.KeyChar == 13)
            {
                this.txtCombNo.Focus();
            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (this.order == null)
            {
                this.ucOrderInputByType1.IsQtyChanged = false;
                return;
            }

            if (this.order.Qty == FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuantity.Value))
            {
                return;
            }

            if (this.order.Qty != FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuantity.Value))
            {
                this.ucOrderInputByType1.IsQtyChanged = true;
                this.order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQuantity.Value);
                myOrderChanged(this.order, ColmSet.Z总量);
            }
            else
            {
                this.ucOrderInputByType1.IsQtyChanged = false;
            }

            //this.txtQuantity_KeyPress(sender, null);
        }

        /// <summary>
        /// 校验开始时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtBegin_ValueChanged(object sender, System.EventArgs e)
        {
            /*
             * 1、开始时间不能早于当天时间24小时
             * 2、开始时间不能早于上一条医嘱的开始时间
             * 
             * */

            if (this.order == null)
            {
                return;
            }

            //临时使用本地时间判断
            //应该在此处获取系统时间 但考虑效率问题 先使用本地时间判断
            //只有补录医嘱可以设置开立时间小于当前时间
            //if (this.dtBegin.Value < DateTime.Now &&
            //    this.order.OrderType.ID != "BL" && //补录医嘱
            //    this.order.OrderType.ID != "BZ")//补录嘱托
            //{
            //    this.dtBegin.Value = this.order.BeginTime;
            //    return;
            //}

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            if (dtBegin.Value < order.MOTime.AddHours(-24))
            {
                if (order.BeginTime > new DateTime(2000, 1, 1))
                {
                    dtBegin.Value = order.BeginTime;
                }
                else
                {
                    dtBegin.Value = dtNow;
                }
                Components.Order.Classes.Function.ShowBalloonTip(5, "警告", "开始时间选择错误，不能早于当前时间24小时！\r\n\r\n请重新选择开始时间！", ToolTipIcon.Warning);
                return;
            }

            if (this.order.BeginTime != this.dtBegin.Value)
            {
                this.dtBegin.Value = new DateTime(this.dtBegin.Value.Year, this.dtBegin.Value.Month, this.dtBegin.Value.Day, this.dtBegin.Value.Hour, this.dtBegin.Value.Minute, 0);
                this.order.BeginTime = this.dtBegin.Value;
                dirty = true;
                myOrderChanged(this.order, ColmSet.K开始时间);
                dirty = false;
            }
        }

        /// <summary>
        /// 停止时间变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            if (this.order == null) 
                return;

            if (this.txtQuantity.Text == "")
                return;

            if (this.dtEnd.Value.Date <= this.dtBegin.Value.Date 
                && this.dtEnd.Checked)
            {
                this.dtEnd.Value = this.dtBegin.Value;

                Components.Order.Classes.Function.ShowBalloonTip(5, "警告", "结束时间选择错误，不能早于开始时间！\r\n\r\n请重新选择结束时间！", ToolTipIcon.Warning);

                return;
            }

            DateTime dtNow = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            try
            {
                if (this.dtEnd.Checked == false)
                {
                    this.order.EndTime = System.DateTime.MinValue;
                }
                else
                {
                    if (dtEnd.Value < dtNow.AddHours(-24))
                    {
                        dtBegin.Value = dtNow;
                        Components.Order.Classes.Function.ShowBalloonTip(5, "警告", "结束时间选择错误，不能早于当前时间24小时！\r\n\r\n请重新选择结束时间！", ToolTipIcon.Warning);
                        return;
                    }
                    this.order.EndTime = this.dtEnd.Value;//停止时间
                }
                dirty = true;
                myOrderChanged(this.order, ColmSet.T停止时间);
                dirty = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dtBegin_CloseUp(object sender, System.EventArgs e)
        {
            this.dtBegin.Value = new DateTime(this.dtBegin.Value.Year, this.dtBegin.Value.Month, this.dtBegin.Value.Day, 0, 0, 0);
        
        }


        private void dtEnd_CloseUp(object sender, System.EventArgs e)
        {
            if (this.dtEnd.Value.Date <= this.dtBegin.Value.Date && this.dtEnd.Checked)
            {
                MessageBox.Show("医嘱终止时间不能小于起始时间，请更改", "提示");
            }

            this.dtEnd.Value = new DateTime(this.dtEnd.Value.Year, this.dtEnd.Value.Month, this.dtEnd.Value.Day, 23, 59, 59);
        }

        private Panel PanelEnd
        {
            get
            {
                return this.panelEndDate;
            }
        }

        /// <summary>
        /// 过滤系统类别
        /// </summary>
        /// <param name="isShort"></param>
        /// <param name="alSysClass"></param>
        /// <returns></returns>
        private ArrayList FilterSysClassForNurse(bool isShort, ArrayList alSysClass)
        {
            System.Collections.ArrayList al = FS.HISFC.Models.Base.SysClassEnumService.List();
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "ALL";
            objAll.Name = "全部";
            al.Add(objAll);
            
            //护士医嘱屏蔽些东西

            System.Collections.ArrayList rAl = new ArrayList();
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MR")//非药品，转科，转床
                {

                }
                else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UO")//手术
                {
                }
                //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UC")//检查
                //{
                //}
                //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UL")	//检验
                //{
                //}
                else if (obj.ID.Length >= 1 && obj.ID.Substring(0, 1) == "P")//药
                {
                }
                else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MC")//会诊
                {
                }
                else
                {
                    rAl.Add(obj);
                }
            }
            return rAl;
        }

        /// <summary>
        /// 设置医嘱类型
        /// </summary>
        /// <param name="b"></param>
        protected void SetLongOrShort(bool isShort)
        {
            dirty = false;

            //长期医嘱停止日期
            this.PanelEnd.Visible = !isShort;
            PanelEnd.Visible = false;

            this.cmbOrderType1.AddItems(FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderType(isShort));
             
            if (this.isNurseCreate)
            {
                this.ucInputItem1.AlCatagory = this.FilterSysClassForNurse(isShort, SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(isShort));
            }
            else
            {
                this.ucInputItem1.AlCatagory = FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderSysType(isShort);
            }
            if (cmbOrderType1.Items.Count > 0)
            {
                this.cmbOrderType1.SelectedIndex = 0;
            }

            ucInputItem1.LongOrder = !isShort;
        }

        /// <summary>
        /// 医嘱类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOrderType1_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbOrderType1.SelectedIndex < 0)
            {
                return;
            }
            FS.HISFC.Models.Order.OrderType obj = null;

            if (this.LongOrShort == 0) //长期医嘱
            {
                obj = SOC.HISFC.BizProcess.Cache.Order.GetOrderType(false)[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;
            }
            else //临时医嘱
            {
                obj = SOC.HISFC.BizProcess.Cache.Order.GetOrderType(true)[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;
                //出院带药，请假带药，可能要输入天数
            }
        
            if (obj.IsCharge == false)
            {
                this.ucInputItem1.IsCanInputName = false;
            }
            else
            {
                this.ucInputItem1.IsCanInputName = true;
            }

            this.ucInputItem1.Focus();

        }

        /// <summary>
        /// 医嘱类型更改
        /// </summary>
        /// <param name="charge">是否收费医嘱类型</param>
        private void GeChargeableOrderType(bool charge)
        {
            return;

            //判断当前医嘱收费类型
            FS.HISFC.Models.Order.OrderType ordertype = this.cmbOrderType1.SelectedItem as FS.HISFC.Models.Order.OrderType;
            if (ordertype != null)
            {
                if (ordertype.IsCharge == charge)
                    return;
            }
            //不符合，查找第一个符合的收费类型
            foreach (FS.HISFC.Models.Order.OrderType obj in this.cmbOrderType1.alItems)
            {
                if (obj.IsCharge == charge)
                {
                    this.cmbOrderType1.Tag = obj.ID;
                    return;
                }
            }
        }

        private void cmbUnit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string unit = this.cmbUnit.Text.Trim();
                if (FS.FrameWork.Public.String.ValidMaxLengh(unit, 16) == false)
                {
                    MessageBox.Show("单位超长!", "提示");
                    return;
                }
                if (this.order.Unit != unit && dirty == true)
                {
                    this.order.Unit = unit;//更新单位
                    myOrderChanged(this.order,ColmSet.Z总量单位);
                }
            }
            catch { }
        }
       
        /// <summary>
        /// 当前选择的医嘱类型
        /// </summary>
        public FS.HISFC.Models.Order.OrderType SelectedOrderType
        {
            get
            {
                return this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;
            }
        }

        /// <summary>
        /// 项目类别选择变化
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                FS.FrameWork.Models.NeuObject obj = sender;
                if (obj.ID.Length > 0 && obj.ID.Substring(0, 1) == "M")
                {
                    GeChargeableOrderType(false);
                }
                else
                {
                    GeChargeableOrderType(true);
                }
            }
            catch { }
            if (CatagoryChanged != null) CatagoryChanged(sender);
        }

        void ucOrderInputByType1_Leave(object sender, EventArgs e)
        {
            this.ucInputItem1.Focus();
        }

        /// <summary>
        /// 设置项目输入框是否可见
        /// </summary>
        /// <param name="vlue"></param>
        public void SetInputControlVisible(bool vlue)
        {
            this.ucInputItem1.SetVisibleForms(vlue);
        }

        /// <summary>
        /// 设置输入框获得焦点，只是用于leave事件生效
        /// </summary>
        public void SetFocus()
        {
            this.ucInputItem1.Focus();
        }

        /// <summary>
        /// 选择项目
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                this.DeleteEvent();

                this.Clear(false);

                if (this.ucInputItem1.FeeItem == null)
                {
                    return;
                }

                //判断缺药、停用
                FS.HISFC.Models.Pharmacy.Item itemObj = null;
                string errInfo = "";
                
                FS.HISFC.Models.Order.OrderType obj = null;

                obj = FS.SOC.HISFC.BizProcess.Cache.Order.GetOrderType(longOrShort == 1)[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;

                if (ucInputItem1.FeeItem.ID != "999" && obj.IsCharge)
                {
                    if (Components.Order.Classes.Function.CheckDrugState(null,
                        new FS.FrameWork.Models.NeuObject(this.ucInputItem1.FeeItem.User02, this.ucInputItem1.FeeItem.User03, ""),
                        (FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem,
                        false, ref itemObj, ref errInfo) == -1)
                    {
                        MessageBox.Show(errInfo);
                        return;
                    }

                    //{1BBD2F14-49A6-468b-BB08-19BF0499CEF4}
                    if (itemObj != null && itemObj.Product != null && !string.IsNullOrEmpty(itemObj.Product.Caution))
                    {
                        MessageBox.Show(itemObj.Product.Caution);
                    }
                }

                if (!this.EditGroup)		//当实现对组套修改功能时 不需对知情同意情况进行判断
                {
                    //判断当前输入的项目是否知情同意书
                    this.bPermission = Classes.Function.IsPermission(this.patientInfo,
                        (FS.HISFC.Models.Order.OrderType)this.cmbOrderType1.SelectedItem,
                        (FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem);
                }

                if (this.order != null && this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item == this.order.Item) //不重复
                {
                    this.txtQuantity.Focus();
                    return;
                }

                //收费医嘱却是999编码的返回
                if (this.ucInputItem1.FeeItem.ID == "999" && obj.IsCharge)
                {
                    return;
                }
                //过敏史判断
                //项目变化-指向新行
                this.CurrentRow = -1;

                this.OperatorType = Operator.Add;

                //设置新医嘱
                this.SetNewOrder();


                if (!((FS.HISFC.Models.Order.OrderType)this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex]).IsCharge)
                {
                    this.cmbOrderType1.SelectedIndex = 0;
                }

                if (this.order != null && this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item == this.order.Item) //不重复
                {
                    this.txtQuantity.Focus();
                    int length = this.txtQuantity.DecimalPlaces == 0 ? this.txtQuantity.Value.ToString().Length : this.txtQuantity.Value.ToString().Length + 1 + this.txtQuantity.DecimalPlaces;
                    this.txtQuantity.Select(0, length);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.AddEvent();
            }
        }
        #endregion

        #region 函数

        /// <summary>
        /// 读取医嘱信息-控制控件显示状态
        /// </summary>
        /// <param name="myOrder"></param>
        protected int ShowOrder(FS.HISFC.Models.Order.Inpatient.Order myOrder)
        {
            if (myOrder == null) 
                return 0;

            this.txtCombNo.Value = myOrder.SubCombNO;

            #region 设置医嘱开立时间

            try//设置停止时间
            {
                //开始时间
                if (this.order.BeginTime > new DateTime(2000, 1, 1))
                {
                    dtBegin.Value = order.BeginTime;
                }

                //结束时间
                if (this.order.EndTime > new DateTime(2000, 1, 1)
                    && PanelEnd.Visible)
                {
                    this.dtEnd.Checked = true;//非最小日期，设置结束日期
                    this.dtEnd.Value = this.order.EndTime;
                }
            }
            catch
            {
            }

            #endregion

            //项目
            if (myOrder.Item.ItemType == EnumItemType.Drug)
            {
                if (this.LongOrShort == 0) //长期医嘱，不显示总量
                {
                }
                else
                {
                    //药品 临时医嘱，频次为空，默认为需要时候服用prn
                    if (myOrder.Frequency.ID == null || myOrder.Frequency.ID == "")
                    {
                        myOrder.Frequency.ID = Classes.Function.GetDefaultFrequencyID();//临时医嘱默认为需要时执行
                    }
                }

                this.txtQuantity.Text = myOrder.Qty.ToString(); //总量

                this.cmbUnit.Items.Clear();

                if (myOrder.Item.ID == "999" || !myOrder.OrderType.IsCharge)
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//可以更改
                    this.cmbUnit.Enabled = this.txtQuantity.Enabled;
                }
                else
                {
                    FS.HISFC.Models.Pharmacy.Item item = Components.Order.Controls.Order.GetPHAItem(myOrder.Item.ID);

                    this.cmbUnit.Items.Add(item.MinUnit);//单位
                    this.cmbUnit.Items.Add(item.PackUnit);//单位
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;//只能选择

                    if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                    {
                        if (this.cmbUnit.Items.Count > 0)
                        {
                            this.cmbUnit.SelectedIndex = 0;
                            myOrder.Unit = this.cmbUnit.Text;
                            myOrder.Item.PriceUnit = myOrder.Unit;
                        }
                    }
                    else
                    {
                        this.cmbUnit.Text = myOrder.Unit;
                    }

                    if (myOrder.OrderType.IsCharge) //自定义药品
                    {
                        if (item.PackQty == 0)//检查包装数量
                        {
                            MessageBox.Show("该药品的包装数量为零！");
                            return -1;
                        }
                        if (item.BaseDose == 0)//检查基本剂量
                        {
                            MessageBox.Show("该药品的基本剂量为零！");
                            return -1;
                        }
                        if (item.DosageForm.ID == "")//检查剂型
                        {
                            MessageBox.Show("该药品的剂型为空！");
                            return -1;
                        }
                    }

                    SetQty(myOrder);
                }
            }
            else if (myOrder.Item.ItemType == EnumItemType.UnDrug)//非药品
            {
                //如果执行科室为空--付给本科科室
                //if (myOrder.ExeDept.ID == "")
                //{
                //    if (item.ExecDept == "")
                //    {
                //        myOrder.ExeDept.ID = myOrder.Patient.PVisit.PatientLocation.Dept.ID;////执行科室?????可能需要修改
                //        myOrder.ExeDept.Name = myOrder.Patient.PVisit.PatientLocation.Dept.Name;
                //    }
                //    else if (item.ExecDepts != null && item.ExecDepts.Count > 0)
                //    {
                //        try
                //        {
                //            myOrder.ExeDept.ID = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item).ExecDepts[0].ToString();
                //        }
                //        catch { }
                //    }
                //}

                if (myOrder.Item.ID != "999")
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item);
                    if (myOrder.CheckPartRecord == ""
                        && myOrder.Item.SysClass.ID.ToString() == "UC") //检查检体部位
                    {
                        myOrder.CheckPartRecord = item.CheckBody;
                    }
                    if (myOrder.Sample.Name == "" 
                        && myOrder.Item.SysClass.ID.ToString() == "UL") //检查检体部位
                    {
                        myOrder.Sample.Name = item.CheckBody;
                    }
                }

                this.cmbUnit.Items.Clear();

                if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                {
                    string unit = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item).PriceUnit;
                    if (unit == null || unit == "")
                    {
                        unit = "次";
                    }
                    this.cmbUnit.Items.Add(unit);
                    if (this.cmbUnit.Items.Count > 0)
                    {
                        this.cmbUnit.SelectedIndex = 0;
                        myOrder.Unit = this.cmbUnit.Text;
                        myOrder.Item.PriceUnit = myOrder.Unit;
                    }
                }
                else
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.Text = myOrder.Unit;
                }
                if (myOrder.Qty == 0)
                {
                    //this.txtQuantity.Text = "1.00"; //总量
                    //myOrder.Qty = 1;
                }
                else
                {
                    this.txtQuantity.Text = myOrder.Qty.ToString();
                }
                if (myOrder.Frequency.ID == "")
                {
                    myOrder.Frequency.ID = "QD";//临时医嘱默认QD
                }
            }
            else
            {
                MessageBox.Show("无法识别的类型！");
                return -1;
            }

            this.SetQty(myOrder);

            return 0;
        }

        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// 患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                //bool isRefresh = false;
                ////{CE481BFE-9211-48eb-8921-50D04858CB39} 增加value != null的判断 Added by Gengxl
                //if (value != null 
                //    && this.patientInfo != null
                //    && this.patientInfo.ID != value.ID)
                //{
                //    isRefresh = true;
                //}
                this.patientInfo = value;
                //{112B7DB5-0462-4432-AD9D-17A7912FFDBE}  患者信息
                this.ucInputItem1.Patient = value;

                //if (isRefresh)
                //{
                //    if (this.patientInfo.Pact.PayKind.ID == "02")
                //    {
                //        FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在刷新医保项目类别标记..");
                //        Application.DoEvents();

                //        //this.ucInputItem1.RefreshSIFlag();

                //        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //    }
                //}
            }
        }

        protected FS.FrameWork.Models.NeuObject myReciptDept = null;

        /// <summary>
        /// 开立科室
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDept()
        {
            if (this.myReciptDept == null)
            {
                myReciptDept = new FS.FrameWork.Models.NeuObject();
                this.myReciptDept.ID = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.ID; //开立科室
                this.myReciptDept.Name = ((FS.HISFC.Models.Base.Employee)this.GetReciptDoc()).Dept.Name;
            }
            return this.myReciptDept;
        }

        protected FS.FrameWork.Models.NeuObject myReciptDoc = null;

        /// <summary>
        /// 获取开立医生
        /// </summary>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetReciptDoc()
        {
            if (this.myReciptDoc == null)
            {
                myReciptDoc = new FS.FrameWork.Models.NeuObject();
                myReciptDoc = CacheManager.InOrderMgr.Operator.Clone();
            }
            return this.myReciptDoc;
        }

        /// <summary>
        /// 设置新医嘱
        /// </summary>
        protected void SetNewOrder()
        {
            if (this.DesignMode)
            {
                return;
            }

            //定义个新医嘱对象
            this.order = new FS.HISFC.Models.Order.Inpatient.Order();//重新设置医嘱

            order.Patient = this.patientInfo;

            dirty = false;
            try
            {
                if (this.ucInputItem1.FeeItem.ID == "999")//自己录的项目
                {
                    this.order.Item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                }
                else
                {
                    string err = "";
                    //药品
                    if (this.ucInputItem1.FeeItem.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                    {
                        this.order.Item = Components.Order.Controls.Order.GetPHAItem(ucInputItem1.FeeItem.ID);

                        this.order.Item.User01 = this.ucInputItem1.FeeItem.User01;
                        this.order.Item.User02 = this.ucInputItem1.FeeItem.User02;//传递取药药房
                        this.order.Item.User03 = this.ucInputItem1.FeeItem.User03;

                        order.StockDept.ID = ucInputItem1.FeeItem.User02;
                        order.StockDept.Name = ucInputItem1.FeeItem.User03;

                        if (this.GetReciptDept() != null)
                        {
                            order.ReciptDept.ID = this.GetReciptDept().ID;
                            order.ReciptDept.Name = this.GetReciptDept().Name;
                        }
                        if (this.GetReciptDoc() != null)
                        {
                            order.ReciptDoctor.ID = this.GetReciptDoc().ID;
                            order.ReciptDoctor.Name = this.GetReciptDoc().Name;
                        }


                        if (CacheManager.OrderIntegrate.FillPharmacyItemWithStockDept(ref order, out err) == -1)
                        {
                            MessageBox.Show(err);
                            return;
                        }
                        if (order == null)
                        {
                            MessageBox.Show("转换出错!", "ucItemSelect");
                            return;
                        }
                        int ret = 0;
                        string error = "";
                        // {E97273E4-CF5A-47bf-97C6-8025504486C4}
                        if (null != this.patientInfo && !EditGroup)
                        {
                            ret = Components.Order.Classes.Function.JudgePatientAllergy("2", this.patientInfo.PID, this.order, ref error);
                        }
                        else
                        {
                            ret = 1;
                        }

                        if (ret <= 0)
                        {
                            return;
                        }
                    }
                    else//非药品
                    {
                        try
                        {
                            FS.HISFC.Models.Fee.Item.Undrug itemTemp = SOC.HISFC.BizProcess.Cache.Fee.GetItem(ucInputItem1.FeeItem.ID);
                            if (itemTemp == null)
                            {
                                MessageBox.Show("获得非药品信息失败！\r\n" + ucInputItem1.FeeItem.Name);
                                return;
                            }
                            this.order.Item = itemTemp;

                            this.order.Item.Qty = this.txtQuantity.Value;

                            if (CacheManager.OrderIntegrate.FillFeeItem(ref order, out err) == -1)
                            {
                                MessageBox.Show(err);
                                return;
                            }
                            if (order == null)
                            {
                                MessageBox.Show("转换出错!", "ucItemSelect");
                                return;
                            }

                            //检查要求是否为空 暂时由此判断该项目为检查还是检验		
                            if (itemTemp.SysClass.ID.ToString() == "UL")
                            {
                                //设置复合项目明细所属大项编码、样本类型
                                this.order.Sample.Name = itemTemp.CheckBody;
                            }
                            else
                            {
                                this.order.CheckPartRecord = itemTemp.CheckBody;
                            }
                        }
                        catch
                        {
                            MessageBox.Show("转换出错!", "ucItemSelect");
                        }
                    }
                    //传递知情同意书
                    this.order.IsPermission = bPermission;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (this.order.SubCombNO == 0)
            {
                this.order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Value);
            }
            else
            {
                this.txtCombNo.Value = this.order.SubCombNO;
            }

            #region 设置医嘱开立时间

            try//设置停止时间
            {

                if (order.MOTime == DateTime.MinValue)
                {
                    this.order.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();//开立时间
                }

                //开始时间
                if (order.BeginTime < new DateTime(2000, 1, 1))
                {
                    this.order.BeginTime = Classes.Function.GetDefaultMoBeginDate(this.order.OrderType.IsDecompose ? 0 : 1);
                    dtBegin.Value = order.BeginTime;
                }

                //结束时间
                if (this.order.EndTime <= this.dtEnd.MaxDate)
                {
                    if (this.order.EndTime == DateTime.MinValue) //最小日期不设置结束日期
                    {
                        this.dtEnd.Checked = false;
                    }
                    else
                    {
                        this.dtEnd.Checked = true;//非最小日期，设置结束日期
                        this.dtEnd.Value = this.order.EndTime;
                    }
                }

                //为什么要这样处理？？
                if (this.PanelEnd.Visible)//临时医嘱不需要显示停止时间
                {
                    this.dtEnd.Value = DateTime.Today.AddDays(1);
                    this.dtEnd.Checked = false;
                }
            }
            catch
            {
            }

            #endregion

            if (this.order.StockDept.ID == null || order.StockDept.ID == "")
            {
                order.StockDept.ID = ucInputItem1.FeeItem.User02; //扣库科室,可能要变需要注意
                order.StockDept.Name = ucInputItem1.FeeItem.User03;//扣库科室
            }

            //{E57F256E-1722-4b36-809A-D46BD7A9AB55}
            ucOrderInputByType1.SetQtyValue -= new SetQtyValue(ucOrderInputByType1_SetQtyValue);
            ucOrderInputByType1.SetQtyValue += new SetQtyValue(ucOrderInputByType1_SetQtyValue);

            ////计算总量
            //if (order.HerbalQty == 0)
            //{
            //    order.HerbalQty = 1;
            //}
            //Components.Order.Classes.Function.ReComputeQty(order);

            this.order.OrderType = this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;

            //显示给界面
            if (ShowOrder(this.order) == -1)
            {
                return;
            }

            //this.order.ReciptDept = CacheManager.LogEmpl.Dept.Clone();//开立科室
            order.ReciptDept.ID = CacheManager.LogEmpl.Dept.ID;

            //手术室开立处理
            if (!string.IsNullOrEmpty(this.order.ReciptDept.ID) && string.IsNullOrEmpty(this.order.ExeDept.ID))
            {
                if ("1,2".Contains((SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.order.ReciptDept.ID).SpecialFlag)))
                {
                    order.ExeDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(this.order.ReciptDept.ID);
                }
            }

            this.order.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;//录入人
            this.order.Oper.Name = FS.FrameWork.Management.Connection.Operator.Name;

            if (this.order.OrderType.ID == "CZ")        //长期医嘱
            {
                if (this.order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {                    
                    string drugQuality = ((FS.HISFC.Models.Pharmacy.Item)this.order.Item).Quality.ID;
                    if (this.hsResQuality.ContainsKey(drugQuality))
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg(this.order.Item.Name + " 该性质药品不允许开立长期医嘱"),"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            if (this.txtQuantity.Enabled)
            {
                this.txtCombNo.Focus();
                //this.txtQuantity.Focus();//focus
            }
            else
            {
                this.txtCombNo.Focus();
                //this.ucOrderInputByType1.Focus();
            }

            this.ucOrderInputByType1.IsNew = true;//新的
            
            //初始化新项目信息 设置医嘱频次
            Classes.Function.SetDefaultOrderFrequency(this.order);
            if (this.order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                this.order.Usage.ID = (this.order.Item as FS.HISFC.Models.Pharmacy.Item).Usage.ID;
                this.order.Usage.Name = Classes.Function.HelperUsage.GetName(this.order.Usage.ID);
            }
        
            this.ucOrderInputByType1.Order = this.order;//传递给选择类型
            dirty = true;
            myOrderChanged(this.order,ColmSet.ALL);
        }

        /// <summary>
        /// ucOrderInputByType1里面计算总量后，反馈到界面显示
        /// </summary>
        /// <param name="inOrder"></param>
        void ucOrderInputByType1_SetQtyValue(FS.HISFC.Models.Order.Inpatient.Order inOrder)
        {
            this.txtQuantity.Text = inOrder.Qty.ToString(); //总量

            #region 单位

            if (inOrder.Item.ID != "999")
            {
                cmbUnit.Text = inOrder.Unit;
                inOrder.Item.PriceUnit = inOrder.Unit;
            }
            #endregion
        }

        #endregion

        #region  清屏、医嘱类型修改函数 

        /// <summary>
        /// 清空医嘱显示
        /// </summary>
        /// <param name="isResetBeginTime">点击开立时重新获取开始时间</param>
        public void Clear(bool isResetBeginTime)
        {
            try
            {
                this.order = null;
                //this.ucInputItem1.txtItemCode.Text = "";			//项目编码
                //this.ucInputItem1.txtItemName.Text = "";			//项目名称
                this.txtQuantity.Text = "1";					//总量
                this.dtEnd.Checked = false;
                ucInputItem1.Clear();
                this.cmbUnit.Items.Clear();
                this.ucOrderInputByType1.Clear();
                this.txtCombNo.Value = this.GetMaxSubCombNo(null, -1);
                this.ucOrderInputByType1.IsQtyChanged = false;
                //默认医嘱类型为长期医嘱或临时医嘱，防止出现类似当前操作的医嘱类型选择“出院带药”时，给下一个病人开医嘱时也是“出院带药”的情况
                //this.cmbOrderType1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQuantity_Enter(object sender, EventArgs e)
        {
            int length = this.txtQuantity.DecimalPlaces == 0 ? this.txtQuantity.Value.ToString().Length : this.txtQuantity.Value.ToString().Length + 1 + this.txtQuantity.DecimalPlaces;
            this.txtQuantity.Select(0, length);
        }

        private void txtCombNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e != null && e.KeyChar == 13)
            {
                if (this.order.Item.ItemType != EnumItemType.Drug)//非药品跳回 新加
                {
                    //this.ucInputItem1.Focus();
                    this.ucOrderInputByType1.Focus();
                }
                else
                {
                    this.ucOrderInputByType1.Focus();
                }
            }
        }
        #endregion      

        private void txtCombNo_Enter(object sender, EventArgs e)
        {
            this.txtCombNo.Select(0, this.txtCombNo.Value.ToString().Length);
        }

        private void txtCombNo_Leave(object sender, EventArgs e)
        {            
            if (this.order != null)
            {
                if (this.txtCombNo.Value <= 0)
                {
                    MessageBox.Show("方号必须大于0！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                decimal getValue = GetMaxSubCombNo(order, -1);

                //处理录入错误导致的方号过大问题
                if (((Int32)this.txtCombNo.Value) > getValue + 100)
                {
                    txtCombNo.Value = getValue;
                }

                if (((Int32)this.txtCombNo.Value) != this.order.SubCombNO)
                {
                    int subCombTemp = this.order.SubCombNO;
                    this.order.SubCombNO = (Int32)this.txtCombNo.Value;
                    order.SortID = 0;

                    if (this.GetSameSortID((Int32)this.txtCombNo.Value, this.order) == -1)
                    {
                        this.order.SubCombNO = subCombTemp;
                        this.txtCombNo.Value = subCombTemp;
                        this.txtCombNo.Focus();
                        return;
                    }

                    //此处删除组号列表中
                    if (this.DeleteSubComnNo != null)
                    {
                        this.DeleteSubComnNo(subCombTemp, order.OrderType.IsDecompose);
                    }

                    this.myOrderChanged(this.order, ColmSet.Z组号);
                }
            }
        }

        /// <summary>
        /// 处理方号相同问题
        /// </summary>
        private int GetSameSortID(int sortID, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            FS.HISFC.Models.Order.Inpatient.Order orderTemp = null;
            if (this.GetSameSubCombNoOrder(sortID, ref orderTemp) == -1)
            {
                return -1;
            }

            if (orderTemp != null)
            {
                if (Classes.Function.ValidComboOrder(order, orderTemp, true) == -1)
                {
                    return -1;
                }
                order.Frequency = orderTemp.Frequency;
                order.HerbalQty = orderTemp.HerbalQty;
                order.FirstUseNum = orderTemp.FirstUseNum;
                order.BeginTime = orderTemp.BeginTime;
                order.EndTime = orderTemp.EndTime;

                if (!Classes.Function.IsSameUsage(order.Usage.ID, orderTemp.Usage.ID))
                {
                    order.Usage = orderTemp.Usage;
                }
                order.InjectCount = orderTemp.InjectCount;
                //order.ExeDept = orderTemp.ExeDept;
                order.Combo.ID = orderTemp.Combo.ID;
                order.SubCombNO = orderTemp.SubCombNO;
                //order.SortID = orderTemp.SortID + 1;
                order.SortID = 0;
            }
            //修改方号时，取消组合
            else
            {
                order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();
                //order.SortID = FS.FrameWork.Function.NConvert.ToInt32((order.SubCombNO.ToString() + "00001"));
                order.SortID = 0;
            }
            return 1;
        }


        #region 统一处理消息弹出框

        /// <summary>
        /// 统一弹出
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        public DialogResult MessageBoxShow(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text, caption, buttons, icon);
        }

        public DialogResult MessageBoxShow(string text)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text);
        }

        public DialogResult MessageBoxShow(string text, string caption)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text, caption);
        }

        public DialogResult MessageBoxShow(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(owner, text, caption, buttons, icon);
        }

        public DialogResult MessageBoxShow(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            this.ucInputItem1.SetVisibleForms(false);
            return MessageBox.Show(text, caption, buttons, icon, defaultButton);
        }
        #endregion
        ///// <summary>
        ///// 开始时间
        ///// add by zhaorong at 2013-7-23 开始时间默认时间：00:00:00
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dtBegin_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTime beginTime = this.dtBegin.Value;
        //    this.dtBegin.Value = beginTime.Date
        //}
        ///// <summary>
        ///// 结束时间
        ///// add by zhaorong at 2013-7-23 结束时间默认时间：23:59:59
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void dtEnd_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTime endTime = this.dtEnd.Value;
        //    this.dtEnd.Value =endTime
        //}
    }
    /// <summary>
    /// 医嘱操作
    /// </summary>
    public enum Operator
    {
        Add, 
        Modify, 
        Delete, 
        Query
    }
}

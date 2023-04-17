using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 门诊处方开立 项目选择控件
    /// </summary>
    public partial class ucOutPatientRecipeItemSelect : UserControl
    {
        public ucOutPatientRecipeItemSelect()
        {
            InitializeComponent();
        }


        #region 变量
        protected ToolTip tooltip = new ToolTip(); //ToolTip

        protected bool dirty = false;//是新的时候

        /// <summary>
        /// 是否有处方权
        /// </summary>
        //public bool isHaveOrderPower = false;
        /// <summary>
        /// 是否处理明细处方权
        /// </summary>
        //public bool isControlDrugOrder = false;

        /// <summary>
        /// 当前行
        /// </summary>
        public int CurrentRow = -1;

        /// <summary>
        /// 是否进行组套编辑功能
        /// </summary>
        public bool EditGroup = false;

        protected bool isLisDetail = false;

        /// <summary>
        /// 是否知情同意书
        /// </summary>
        protected bool bPermission = false;

        /// <summary>
        /// 是否默认开立的天数和上个项目的天数相同
        /// </summary>
        private bool isDaysLikePreOrder = false;

        /// <summary>
        /// 是否频次、用法不同时允许修改组号进行组合，组合后频次、用法更改
        /// </summary>
        private bool isChangeSubCombNoAlways = false;

        /// <summary>
        /// 是否计算辅材
        /// </summary>
        public bool isChangeSubComb = false;

        /// <summary>
        /// 是否显示不计算辅材选项
        /// </summary>
        private bool isChkChangeSubComb = false;

        /// <summary>
        /// 药品总量是否允许修改
        /// </summary>
        private bool isDrugCanEditQTY = true;

        /// <summary>
        /// 是否可以开立描述医嘱
        /// </summary>
        protected string pValue = "0";

        /// <summary>
        /// 增加项目前操作接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem IBeforeAddItem = null;

        /// <summary>
        /// 医嘱变化时候用
        /// </summary>
        public event ItemSelectedDelegate OrderChanged;
        /// <summary>
        /// 当前操作类型
        /// </summary>
        public Operator OperatorType = Operator.Query;

        public event FS.FrameWork.WinForms.Forms.SelectedItemHandler CatagoryChanged;

        /// <summary>
        /// 是否在编辑组套状态下
        /// </summary>
        private bool isEditGroup = false;

        /// <summary>
        /// 是否在编辑组套状态下
        /// </summary>
        public bool IsEditGroup
        {
            get
            {
                return isEditGroup;
            }
            set
            {
                isEditGroup = value;
            }
        }

        #endregion

        #region 属性

        protected FS.HISFC.Models.Order.OutPatient.Order order;

        /// <summary>
        /// 医嘱
        /// </summary>
        public FS.HISFC.Models.Order.OutPatient.Order CurrOrder
        {
            get
            {
                return this.order;
            }
            set
            {
                if (DesignMode)
                {
                    return;
                }

                if (value == null)
                {
                    return;
                }

                try
                {
                    DeleteEvent();
                    this.order = value;

                    if (dirty == false)//不是变化时候--传入时候
                    {
                        this.ucOrderInputByType1.IsNew = false;//修改旧医嘱

                        this.ucOrderInputByType1.Order = value;

                        this.ucInputItem1.FeeItem = this.order.Item;

                        ReadOrder(this.order);//读进来的医嘱
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("CurrOrder" + ex.Message);
                }
                finally
                {
                    AddEvent();
                }
            }

        }

        /// <summary>
        /// 当前患者信息
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = null;

        /// <summary>
        /// 当前患者信息
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            set
            {
                if (DesignMode)
                {
                    return;
                }
                this.patientInfo = value;
                this.ucInputItem1.Patient = value;
            }
        }

        /// <summary>
        /// 是否显示Lis详细信息
        /// </summary>
        public bool IsLisDetail
        {
            set
            {
                this.isLisDetail = value;
            }
        }

        #endregion

        #region 方号相关

        public delegate int GetMaxSubCombNoEvent(FS.HISFC.Models.Order.OutPatient.Order order);

        /// <summary>
        /// 获取最大方号
        /// </summary>
        public event GetMaxSubCombNoEvent GetMaxSubCombNo;

        public delegate int GetSameSubCombNoOrderEvent(int sortID, ref FS.HISFC.Models.Order.OutPatient.Order order);

        /// <summary>
        /// 获得相同方号医嘱
        /// </summary>
        public event GetSameSubCombNoOrderEvent GetSameSubCombNoOrder;

        #endregion

        #region 初始化

        /// <summary>
        /// 进度条当前进度,用于加载项目时显示进度条
        /// </summary>
        int progress = 1;

        /// <summary>
        /// 初始化加载
        /// </summary>
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
            Classes.LogManager.Write("【开始初始化项目选择控件】");

            #region 设置tip
            tooltip.SetToolTip(this.ucInputItem1, "输入拼音码查询，开立医嘱(ESC取消列表)");
            tooltip.SetToolTip(this.txtQTY, "输入总数量(回车输入结束)");
            #endregion
            try
            {
                //this.ucInputItem1.DeptCode = "";//科室看到全部项目
                this.ucInputItem1.DeptCode = CacheManager.LogEmpl.Dept.ID;
                this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
                this.ucInputItem1.FontSize = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

                this.ucOrderInputByType1.InitControl(null, null, null);

                this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.Clinic;

                //发药类型：O 门诊处方、I 住院医嘱、A 全部
                this.ucInputItem1.DrugSendType = "O";

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载项目列表信息..", 0, false);
                Application.DoEvents();

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress, 100);
                Application.DoEvents();

                this.ucInputItem1.Init();//初始化项目列表

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


                //组号功能{98522448-B392-4d67-8C4D-A10F605AFDA5}
                if (this.GetMaxSubCombNo != null)
                {
                    this.txtCombNo.Text = FS.FrameWork.Function.NConvert.ToDecimal(this.GetMaxSubCombNo(null)).ToString();
                }

                this.ucInputItem1.Focus();

                IBeforeAddItem = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem)) as FS.HISFC.BizProcess.Interface.Order.IBeforeAddItem;

            }
            catch { }

            //门诊是否可以开立描述医嘱
            this.pValue = Classes.Function.GetBatchControlParam("200004", false, "0");

            isChangeSubCombNoAlways = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ29", false, "0"));
            isDaysLikePreOrder = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ30", false, "0"));
            isChkChangeSubComb = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ50", false, "0"));
            if (isChkChangeSubComb)
            {
                this.chkDrugEmerce.Visible = true;
            }

            this.AddEvent();

            Classes.LogManager.Write("【结束初始化项目选择控件】");
        }

        void cmbUnit_Leave(object sender, EventArgs e)
        {
            this.cmbUnit_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 修改项目类别
        /// </summary>
        /// <param name="sender"></param>
        void ucInputItem1_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            //try
            //{
            //    FS.FrameWork.Models.NeuObject obj = sender;
            //    if (obj.ID.Length > 0 && obj.ID.Substring(0, 1) == "M")
            //    {
            //        this.ucInputItem1.IsCanInputName = false;
            //    }
            //    else
            //    {
            //        this.ucInputItem1.IsCanInputName = true;
            //    }
            //}
            //catch { }
        }

        #endregion

        #region 函数

        /// <summary>
        /// 增加事件
        /// </summary>
        private void AddEvent()
        {
            DeleteEvent();

            //单位
            cmbUnit.Leave += new EventHandler(cmbUnit_Leave);
            this.cmbUnit.SelectedIndexChanged += new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.Leave += new EventHandler(cmbUnit_Leave);

            //数量
            this.txtQTY.ValueChanged += new System.EventHandler(this.txtQTY_ValueChanged);
            //this.txtQTY.ValueChanged += new EventHandler(txtQTY_ValueChanged);
            txtQTY.ValueChanged += new EventHandler(txtQTY_Leave); 
            this.txtQTY.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            this.txtQTY.Leave += new EventHandler(txtQTY_Leave);

            //项目每次量、频次等变化
            this.ucOrderInputByType1.ItemSelected += new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.ucOrderInputByType1.FocusLost += new ucOrderInputByType.FocusLostHandler(ucOrderInputByType1_Leave);

            //选择项目事件
            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            //组号
            txtCombNo.KeyDown += new KeyEventHandler(txtCombNo_KeyDown);
            txtCombNo.Leave += new EventHandler(txtCombNo_Leave);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void DeleteEvent()
        {
            //单位
            cmbUnit.Leave -= new EventHandler(cmbUnit_Leave);
            this.cmbUnit.SelectedIndexChanged -= new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);
            this.cmbUnit.Leave -= new EventHandler(cmbUnit_Leave);

            //数量
            this.txtQTY.ValueChanged -= new System.EventHandler(this.txtQTY_ValueChanged);
            this.txtQTY.ValueChanged -= new EventHandler(txtQTY_ValueChanged);
            this.txtQTY.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            this.txtQTY.Leave -= new EventHandler(txtQTY_Leave);

            //项目每次量、频次等变化
            this.ucOrderInputByType1.ItemSelected -= new ItemSelectedDelegate(ucOrderInputByType1_ItemSelected);
            this.ucOrderInputByType1.FocusLost -= new ucOrderInputByType.FocusLostHandler(ucOrderInputByType1_Leave);

            //选择项目事件
            this.ucInputItem1.SelectedItem -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            //组号
            txtCombNo.KeyDown -= new KeyEventHandler(txtCombNo_KeyDown);
            txtCombNo.Leave -= new EventHandler(txtCombNo_Leave);
        }

        private ArrayList OrderCatatagory()
        {
            System.Collections.ArrayList al = FS.HISFC.Models.Base.SysClassEnumService.List();
            FS.FrameWork.Models.NeuObject objAll = new FS.FrameWork.Models.NeuObject();
            objAll.ID = "ALL";
            objAll.Name = "全部";
            al.Add(objAll);
            //屏蔽些东西

            System.Collections.ArrayList rAl = new ArrayList();
            //foreach (FS.FrameWork.Models.NeuObject obj in al)
            //{
            //if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MR")//非药品，转科，转床
            //{

            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MF")//膳食
            //{
            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UN")//护理级别
            //{
            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "UJ")	//计量
            //{
            //}
            //else if (obj.ID.Length > 1 && obj.ID.Substring(0, 2) == "MC")//会诊
            //{
            //}
            //else
            //{
            //rAl.Add(obj);
            //}
            //}
            rAl = al;
            return rAl;
        }

        /// <summary>
        /// 设置总量等信息是否可见
        /// {6531391D-6FB3-47f9-A634-BE11D8C830E0}
        /// </summary>
        /// <param name="isVisible"></param>
        private void SetQtyControlVisible(bool isVisible)
        {

            /*
            txtQTY.Visible = isVisible;
            txtQTY.TabStop = isVisible;
            neuLabel1.Visible = isVisible;
            neuLabel1.TabStop = isVisible;
            cmbUnit.Visible = isVisible;
            cmbUnit.TabStop = isVisible;
             * */

            txtQTY.Visible = true;
            txtQTY.TabStop = true;
            neuLabel1.Visible = true;
            neuLabel1.TabStop = true;
            cmbUnit.Visible = true;
            cmbUnit.TabStop = true;
        }

        /// <summary>
        /// 清空记录的天数
        /// </summary>
        public void ClearDays()
        {
            this.ucOrderInputByType1.UseDays = 1;
        }

        /// <summary>
        /// 清空医嘱显示
        /// </summary>
        /// <param name="isGetFoucus"></param>
        public void Clear(bool isGetFoucus)
        {
            try
            {
                this.order = null;
                this.ucInputItem1.txtItemCode.Text = "";			//项目编码
                this.ucInputItem1.txtItemName.Text = "";			//项目名称
                //this.txtQTY.Text = "";					//总量
                SetQtyValue("");
                this.ucInputItem1.SetVisibleForms(false);
                this.cmbUnit.Items.Clear();
                this.ucOrderInputByType1.Clear();
                isGetFoucus = true;
                if (isGetFoucus)
                {
                    this.ucInputItem1.Focus();
                }
                this.ucOrderInputByType1.IsQtyChanged = false;
            }
            catch (Exception ex)
            {
                this.MessageBoxShow(ex.Message);
            }
        }

        /// <summary>
        /// 设置总量
        /// </summary>
        /// <param name="qty"></param>
        private void SetQtyValue(string qty)
        {
            try
            {
                if (string.IsNullOrEmpty(qty))
                {
                    txtQTY.DecimalPlaces = 0;
                    txtQTY.Text = "";
                }
                else
                {
                    decimal decQTY = FS.FrameWork.Function.NConvert.ToDecimal(qty);

                    txtQTY.DecimalPlaces = decQTY.ToString("F6").TrimEnd('0').Length - decQTY.ToString("F6").IndexOf('.');
                    txtQTY.Text = qty;
                }
            }
            catch
            {
                txtQTY.Text = qty;
            }
        }

        /// <summary>
        /// 读取医嘱信息-控制控件显示状态
        /// </summary>
        /// <param name="myOrder"></param>
        protected int ReadOrder(FS.HISFC.Models.Order.OutPatient.Order myOrder)
        {
            if (myOrder == null)
            {
                return 0;
            }

            txtQTY.Enabled = true;//ReadOnly还是可以通过上下箭头修改总量
            cmbUnit.Enabled = true;

            //项目
            if (myOrder.Item.ItemType == EnumItemType.Drug)//药品
            {
                FS.HISFC.Models.Pharmacy.Item item = ((FS.HISFC.Models.Pharmacy.Item)myOrder.Item);
                if (myOrder.Frequency.ID == null || myOrder.Frequency.ID == "")
                {
                    myOrder.Frequency.ID = Components.Order.Classes.Function.GetDefaultFrequencyID();//门诊医嘱默认为需要时执行
                }

                //this.txtQTY.Text = myOrder.Qty.ToString(); //总量
                SetQtyValue(myOrder.Qty.ToString());//总量
                this.cmbUnit.Items.Clear();

                if (myOrder.Item.ID != "999") //自定义药品
                {
                    if (item.PackQty == 0)//检查包装数量
                    {
                        MessageBoxShow("该药品的包装数量为零！");
                        return -1;
                    }
                    if (item.BaseDose == 0)//检查基本剂量
                    {
                        MessageBoxShow("该药品的基本剂量为零！");
                        return -1;
                    }
                    if (item.DosageForm.ID == "")//检查剂型
                    {
                        MessageBoxShow("该药品的剂型为空！");
                        return -1;
                    }
                }

                if (myOrder.StockDept.ID == null || myOrder.StockDept.ID == "")
                {
                    myOrder.StockDept.ID = item.User02; //取药药房,可能要变需要注意
                    myOrder.StockDept.Name = item.User03;//取药药房
                }

                #region 单位

                if (myOrder.Item.ID == "999")
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//可以更改
                    this.cmbUnit.Enabled = this.txtQTY.Enabled;
                }
                else
                {
                    this.cmbUnit.Items.Add((this.ucInputItem1.FeeItem as FS.HISFC.Models.Pharmacy.Item).MinUnit);//单位 
                    this.cmbUnit.Items.Add((this.ucInputItem1.FeeItem as FS.HISFC.Models.Pharmacy.Item).PackUnit);//单位
                    this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;//只能选择
                    this.cmbUnit.Enabled = true;

                    if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                    {
                        if (this.cmbUnit.Items.Count > 0)
                        {
                            this.cmbUnit.SelectedIndex = 0;
                        }
                        myOrder.Unit = this.cmbUnit.Text;
                    }
                    else
                    {
                        this.cmbUnit.Text = myOrder.Unit;
                    }

                    // 1 包装单位总量取整 3 包装单位每次量取整
                    Components.Order.Classes.Function.GetSplitType(ref myOrder);

                    if (((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).LZSplitType != null
                        && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).SplitType)
                        && "1、3".Contains(((FS.HISFC.Models.Pharmacy.Item)myOrder.Item).SplitType))
                    {
                        cmbUnit.Enabled = false;
                    }
                    else
                    {
                        this.cmbUnit.Enabled = true;
                    }
                }

                #endregion

                if (myOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    txtQTY.Enabled = false;//ReadOnly还是可以通过上下箭头修改总量
                    cmbUnit.Enabled = false;
                }

                //药品总量是否允许修改
                this.isDrugCanEditQTY = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ51", false, "1"));
                if (!isDrugCanEditQTY)
                {
                    if ("P".Equals(myOrder.Item.SysClass.ID.ToString())
                        || "PCZ".Equals(myOrder.Item.SysClass.ID.ToString()))
                    {
                        if (!"999".Equals(myOrder.Item.ID))
                        {
                            txtQTY.Enabled = false;//ReadOnly还是可以通过上下箭头修改总量
                            cmbUnit.Enabled = false;
                        }
                    }
                }
                
            }
            else if (myOrder.Item.ItemType == EnumItemType.UnDrug)//非药品
            {
                FS.HISFC.Models.Fee.Item.Undrug item = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item);

                //如果执行科室为空--付给本科科室
                //if (myOrder.ExeDept.ID == "")
                //{
                //    if (item.ExecDept == "")
                //    {
                //        myOrder.ExeDept = myOrder.Patient.PVisit.PatientLocation.Dept.Clone();////执行科室?????可能需要修改
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
                if (myOrder.CheckPartRecord == "" && myOrder.Item.SysClass.ID.ToString() == "UC") //检查检体部位
                {
                    myOrder.CheckPartRecord = item.CheckBody;
                }
                if (myOrder.Sample.Name == "" && myOrder.Item.SysClass.ID.ToString() == "UL") //检查检体部位
                {
                    myOrder.Sample.Name = item.CheckBody;
                }
                if (myOrder.Frequency.ID == null || myOrder.Frequency.ID == "")
                {
                   myOrder.Frequency.ID = Components.Order.Classes.Function.GetDefaultFrequencyID();//门诊医嘱默认QD
                }

                //this.ShowTotal(true);

                this.cmbUnit.Items.Clear();

                if (myOrder.Unit == null || myOrder.Unit.Trim() == "")
                {
                    string unit = ((FS.HISFC.Models.Fee.Item.Undrug)myOrder.Item).PriceUnit;
                    if (unit == null || unit == "") unit = "次";
                    this.cmbUnit.Items.Add(unit);
                    if (this.cmbUnit.Items.Count > 0)
                    {
                        this.cmbUnit.SelectedIndex = 0;

                        myOrder.Unit = this.cmbUnit.Text;
                    }
                }
                else
                {
                    this.cmbUnit.Items.Add(myOrder.Unit);
                    this.cmbUnit.Text = myOrder.Unit;
                }
                if (myOrder.Qty == 0)
                {
                    //this.txtQTY.Text = "1.00"; //总量
                    SetQtyValue("1.00");
                    myOrder.Qty = 1;
                }
                else
                {
                    //this.txtQTY.Text = myOrder.Qty.ToString();
                    SetQtyValue(myOrder.Qty.ToString());
                }
            }
            else
            {
                MessageBoxShow("无法识别的类型！");
                return -1;
            }

            //组号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
            if (myOrder.SubCombNO > 0)
            {
                this.txtCombNo.Text = FS.FrameWork.Function.NConvert.ToDecimal(myOrder.SubCombNO).ToString();
            }
            else
            {
                this.txtCombNo.Text = this.GetMaxSubCombNo(myOrder).ToString();
            }

            return 0;
        }

        /// <summary>
        /// 设置新医嘱
        /// </summary>
        protected void SetNewOrder()
        {
            if (this.DesignMode)
                return;
            //定义个新医嘱对象
            this.order = new FS.HISFC.Models.Order.OutPatient.Order();//重新设置医嘱

            dirty = false;
            try
            {
                if (this.ucInputItem1.FeeItem.ID == "999")//自己录的项目
                {
                    this.order.Item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                }
                else
                {
                    this.SetQtyControlVisible(false);

                    //药品
                    if (this.ucInputItem1.FeeItem.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                    {
                        //this.order.Item = pharmacyManager.GetItem(this.ucInputItem1.FeeItem.ID);
                        order.Item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(ucInputItem1.FeeItem.ID);
                        this.order.Item.User01 = this.ucInputItem1.FeeItem.User01;
                        this.order.Item.User02 = this.ucInputItem1.FeeItem.User02;//传递取药药房
                        this.order.Item.User03 = this.ucInputItem1.FeeItem.User03;

                        this.SetQtyControlVisible(false);
                    }
                    else//非药品
                    {
                        this.SetQtyControlVisible(true);

                        try
                        {
                            if (((FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem).PriceUnit != "[复合项]")
                            {
                                FS.HISFC.Models.Fee.Item.Undrug itemTemp = null;
                                //itemTemp = itemManager.GetItem(this.ucInputItem1.FeeItem.ID);
                                itemTemp = SOC.HISFC.BizProcess.Cache.Fee.GetItem(ucInputItem1.FeeItem.ID);

                                this.order.Item = itemTemp;

                                //执行科室赋值 开立项目同时赋值执行科室 
                                //----Edit By liangjz 07-03
                                //if (itemTemp.ExecDept != null && itemTemp.ExecDept != "")
                                //{
                                //    this.order.ExeDept.ID = itemTemp.ExecDept;

                                //    if (order.ExeDept.ID.Length > 4)
                                //    {
                                //        order.ExeDept.ID = order.ExeDept.ID.Substring(0, 4);
                                //    }
                                //}
                                //else
                                //{
                                //    this.order.ExeDept = this.order.Patient.PVisit.PatientLocation.Dept.Clone();
                                //}

                                //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                                //string deptid = this.ucOrderInputByType1.SetExecDept(this.order.Item.ID);
                                //if (!string.IsNullOrEmpty(deptid))
                                //{
                                //    this.order.ExeDept.ID = deptid;
                                //}
                                //-----

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
                            else
                            {
                                FS.HISFC.Models.Fee.Item.Undrug itemTemp = null;
                                itemTemp = (FS.HISFC.Models.Fee.Item.Undrug)this.ucInputItem1.FeeItem;
                                this.order.Item = itemTemp;
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
                                //this.order.Item.MinFee.ID = "fh";
                            }
                        }
                        catch
                        {
                            MessageBoxShow("转换出错!", "ucItemSelect");
                        }
                    }

                }
            }
            catch
            {
                return;
            }

            this.order.SubCombNO = this.GetMaxSubCombNo(this.order);

            this.ucOrderInputByType1.SetQtyValue += new SetQtyValue(ucOrderInputByType1_SetQtyValue);

            #region
            ////显示给界面  add by liuww 2012-06-08
            if (ReadOrder(this.order) == -1)
            {
                return;
            }
            #endregion

            //设置医嘱开立时间
            FS.FrameWork.Management.DataBaseManger manager = new FS.FrameWork.Management.DataBaseManger();
            DateTime dtNow = manager.GetDateTimeFromSysDateTime();

            this.order.MOTime = dtNow;//开立时间
            this.order.BeginTime = dtNow;//开始时间
            this.order.Item.PriceUnit = this.cmbUnit.Text;
            this.order.Unit = this.cmbUnit.Text;

            this.order.ReciptDept = CacheManager.LogEmpl.Dept.Clone();//开立科室
            this.order.Oper.ID = FS.FrameWork.Management.Connection.Operator.ID;//录入人
            this.order.Oper.Name = FS.FrameWork.Management.Connection.Operator.Name;

            //医嘱类型
            //this.order.OrderType = this.cmbOrderType1.alItems[this.cmbOrderType1.SelectedIndex] as FS.HISFC.Models.Order.OrderType;


            if (this.txtQTY.Enabled)
            {
                this.txtQTY.Focus();//focus
                this.txtQTY.Select(0, this.txtQTY.Text.Length);
            }
            else
            {
                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                this.txtCombNo.Focus();
                //this.ucOrderInputByType1.Focus();
            }
            //if (this.cmbUnit.Items.Count > 0) this.cmbUnit.SelectedIndex = 0;//默认选择第一个。
            this.ucOrderInputByType1.IsNew = true;//新的

            //初始化新项目信息 设置医嘱频次用法            
            if (this.order.Item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
            {
                this.order.Usage.ID = (this.order.Item as FS.HISFC.Models.Pharmacy.Item).Usage.ID;
                this.order.Usage.Name = Order.Classes.Function.HelperUsage.GetName(this.order.Usage.ID);
            }
            else
            {
            }

            if (this.order.HerbalQty == 0)
            {
                //this.order.HerbalQty = 1;//更新草药付数
                if (isDaysLikePreOrder & this.order.Item.ItemType == EnumItemType.Drug)
                {
                    this.order.HerbalQty = ucOrderInputByType1.UseDays > 0 ? ucOrderInputByType1.UseDays : 1;
                }
                else
                {
                    this.order.HerbalQty = 1;//更新草药付数
                }
            }

            dirty = true; //新的
            this.ucOrderInputByType1.Order = this.order;//传递给选择类型
            dirty = false;//更新过了

            this.myOrderChanged(this.order, EnumOrderFieldList.Item);
        }

        /// <summary>
        /// 计算总量后反馈显示到界面
        /// </summary>
        /// <param name="inOrder"></param>
        void ucOrderInputByType1_SetQtyValue(FS.HISFC.Models.Order.OutPatient.Order outOrder)
        {
            this.txtQTY.Text = outOrder.Qty.ToString(); //总量

            #region 单位

            if (outOrder.Item.ID == "999")
            {
                this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDown;//可以更改
                this.cmbUnit.Enabled = this.txtQTY.Enabled;
            }
            else
            {
                if (outOrder.Item.ItemType == EnumItemType.Drug)
                {
                    // 1 包装单位总量取整 3 包装单位每次量取整
                    Components.Order.Classes.Function.GetSplitType(ref outOrder);

                    if (((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).LZSplitType != null
                        && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).SplitType)
                        && "1、3".Contains(((FS.HISFC.Models.Pharmacy.Item)outOrder.Item).SplitType))
                    {
                        cmbUnit.Enabled = false;
                    }
                    else
                    {
                        this.cmbUnit.Enabled = true;
                    }

                    //药品总量是否允许修改
                    this.isDrugCanEditQTY = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ51", false, "1"));
                    if (!isDrugCanEditQTY)
                    {
                        if ("P".Equals(outOrder.Item.SysClass.ID.ToString())
                            || "PCZ".Equals(outOrder.Item.SysClass.ID.ToString()))
                        {
                            cmbUnit.Enabled = false;
                        }
                    }
                }
                cmbUnit.Text = outOrder.Unit;
                outOrder.Item.PriceUnit = outOrder.Unit;
            }
            #endregion
        }

        /// <summary>
        /// 处理方号相同问题
        /// 增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
        /// </summary>
        private int GetSameSortID(int sortID, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            FS.HISFC.Models.Order.OutPatient.Order orderTemp = null;
            if (this.GetSameSubCombNoOrder(sortID, ref orderTemp) == -1)
            {
                return -1;
            }

            if (orderTemp != null)
            {
                //增加控制参数，不同用法、频次的，修改组号是否允许组合
                if (Classes.Function.ValidComboOrder(order, orderTemp, true, isChangeSubCombNoAlways) == -1)
                {
                    return -1;
                }

                order.Frequency = orderTemp.Frequency.Clone();
                order.HerbalQty = orderTemp.HerbalQty;

                if (!Classes.Function.IsSameUsage(order.Usage.ID, orderTemp.Usage.ID))
                {
                    order.Usage = orderTemp.Usage;
                }
                order.InjectCount = orderTemp.InjectCount;
                //order.ExeDept = orderTemp.ExeDept;
                order.Combo.ID = orderTemp.Combo.ID;
                order.SubCombNO = orderTemp.SubCombNO;
            }
            //修改方号时，取消组合
            else
            {
                order.Combo.ID = CacheManager.OutOrderMgr.GetNewOrderComboID();
            }
            return 1;
        }

        /// <summary>
        /// 是否可以根据上下键修改选择处方
        /// </summary>
        /// <returns></returns>
        public bool IsCanChangeSelectOrder()
        {
            return this.ucOrderInputByType1.IsCanChangeSelectOrder();
        }

        protected void myOrderChanged(object sender, EnumOrderFieldList enumOrderFieldList)
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

                this.order = sender as FS.HISFC.Models.Order.OutPatient.Order;//控件传出的对象

                this.OrderChanged(order, enumOrderFieldList);
            }
            catch (Exception ex)
            {
                MessageBoxShow(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 处理组套医嘱（拆分）
        /// </summary>
        private void DealGroupOrder(FS.FrameWork.Models.NeuObject group)
        {
            if (group == null || group.ID.Length <= 0)
            {
                return;
            }
            ArrayList alGroupDetail = null;

            try
            {
                ////alGroupDetail = this.CacheManager.InterMgr.GetComGroupTailByGroupID(group.ID);
            }
            catch
            {
                MessageBoxShow("获得组套明细信息出错！");
                return;
            }
            if (alGroupDetail == null || alGroupDetail.Count <= 0)
            {
                return;
            }
            ////OutPatient.frmGroupDetail frm = new frmGroupDetail();

            ////frm.alGroupDel = alGroupDetail;
            ////frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            ////frm.ShowDialog();
            ////if (frm.alOrderItem.Count <= 0)
            ////{
            ////    return;
            ////}
            ////for (int i = 0; i < frm.alOrderItem.Count; i++)
            ////{
            ////    this.ucItem1.FeeItem = (FS.neHISFC.Components.Object.neuObject)frm.alOrderItem[i];
            ////    this.CurrentRow = -1;
            ////    this.SetOrder();
            ////}
        }

        protected virtual void ucOrderInputByType1_ItemSelected(FS.HISFC.Models.Order.OutPatient.Order order, EnumOrderFieldList changedField)
        {
            DeleteEvent();
            dirty = true;

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                this.cmbUnit.Tag = order.Unit;
                this.cmbUnit.Text = order.Unit;

                SetQtyControlVisible(false);
            }
            else
            {
                SetQtyControlVisible(true);
            }
            //this.txtQTY.Text = order.Qty.ToString();
            SetQtyValue(order.Qty.ToString());

            this.myOrderChanged(order, changedField);
            dirty = false;
            AddEvent();
        }

        #endregion

        #region 事件

        /// <summary>
        /// 数量变化-跳到下一级完成其它输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.order == null)
            {
                return;
            }

            if (e == null || e.KeyChar == 13)
            {
                if (e != null)
                {
                    //if (this.order.Item.IsPharmacy == false)//非药品跳回 新加
                    if (this.order.Item.ItemType != EnumItemType.Drug)//非药品跳回 新加
                    {
                        //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                        this.txtCombNo.Focus();
                        this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                        //this.ucOrderInputByType1.Focus();
                    }
                    else
                    {
                        if (this.cmbUnit.CanFocus)
                        {
                            this.cmbUnit.Focus();
                        }
                        else
                        {
                            this.txtCombNo.Focus();
                            this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                        }
                    }
                }
            }
        }

        private void txtQTY_Leave(object sender, EventArgs e)
        {
            if (this.order == null)
                return;

            if (this.order == null)
            {
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                this.ucOrderInputByType1.IsQtyChanged = false;
                return;
            }
            else if (this.order.Qty == FS.FrameWork.Function.NConvert.ToDecimal(this.txtQTY.Text))
            {
                //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
                this.ucOrderInputByType1.IsQtyChanged = false;
                return;
            }
            //{BE53FA00-A480-41f8-836F-915C11E0C1E4}
            else if (this.order.Qty == 0)
            {
                this.ucOrderInputByType1.IsQtyChanged = false;
            }
            else
            {
                this.ucOrderInputByType1.IsQtyChanged = true;
            }

            if (this.order.Qty != FS.FrameWork.Function.NConvert.ToDecimal(this.txtQTY.Text))
            {
                this.order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtQTY.Text);
                myOrderChanged(this.order, EnumOrderFieldList.Qty);
            }

            if (order.Item.ItemType == EnumItemType.Drug)
            {
                if (order.Item.SysClass.ID.ToString() != "PCC")
                {
                    if (order.Qty.ToString("F4").TrimEnd('0').TrimEnd('.').Contains("."))
                    {
                        Components.Order.Classes.Function.ShowBalloonTip(4, "警告", "项目【" + order.Item.Name + "】总量错误，请注意修改！\r\n\r\n不允许总量有小数！", System.Windows.Forms.ToolTipIcon.Warning);
                    }
                }
            }
        }

        private void txtQTY_Enter(object sender, EventArgs e)
        {
            this.txtQTY.Select(0, this.txtQTY.Text.Length);
        }

        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            try
            {
                DeleteEvent();
                this.ucInputItem1.SetVisibleForms(false);

                if (DesignMode)
                {
                    return;
                }

                if (this.ucInputItem1.FeeItem == null)
                    return;

                this.ucOrderInputByType1.IsQtyChanged = false;

                if (!this.EditGroup)		//当实现对组套修改功能时 不需对知情同意情况进行判断
                {
                    if (this.patientInfo != null)
                    {
                        #region 判断项目开立权限

                        string error = "";

                        int ret = 1;

                        FS.HISFC.Models.Order.OutPatient.Order addOrder = new FS.HISFC.Models.Order.OutPatient.Order();
                        addOrder.Item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;

                        //权限判断
                        ret = SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.JudgeEmplPriv(addOrder, CacheManager.LogEmpl, CacheManager.LogEmpl.Dept, FS.HISFC.Models.Base.DoctorPrivType.LevelDrug, true, ref error);

                        if (ret <= 0)
                        {
                            MessageBoxShow(error, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        //过敏史判断
                        ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.patientInfo.PID, addOrder, ref error);

                        if (ret <= 0)
                        {
                            return;
                        }

                        #endregion

                        //判断缺药、停用
                        FS.HISFC.Models.Pharmacy.Item itemObj = null;
                        FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();
                        string errInfo = "";

                        FS.HISFC.Models.Order.Order orderTemp = new FS.HISFC.Models.Order.Order();
                        orderTemp.Item = (Item)this.ucInputItem1.FeeItem;
                        if (orderTemp.Item.ItemType == EnumItemType.Drug)
                        {
                            if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckDrugState(null,
                                new FS.FrameWork.Models.NeuObject(this.ucInputItem1.FeeItem.User02, this.ucInputItem1.FeeItem.User03, ""), null,
                                (FS.HISFC.Models.Base.Item)this.ucInputItem1.FeeItem,
                                  true, ref itemObj, ref storage, ref errInfo) <= 0)
                            {
                                MessageBoxShow(errInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            //FS.HISFC.Models.Fee.Item.Undrug undrugObj = null;
                            //if (SOC.HISFC.BizProcess.OrderIntegrate.OrderBase.CheckUnDrugState(orderTemp, ref undrugObj, ref errInfo) == -1)
                            //{
                            //    MessageBoxShow(errInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //    return;
                            //}
                        }

                        if (this.patientInfo != null)
                        {
                            #region 药物过敏判断 {30C09D02-8A87-4078-9420-023A6AC61DE9}
                            ArrayList alDrugAllergy = CacheManager.DiagMgr.QueryDrugAllergyByNo(this.patientInfo.PID.CardNO);

                            if (alDrugAllergy != null && alDrugAllergy.Count > 0)
                            {
                                foreach (FS.HISFC.Models.HealthRecord.Diagnose drugAllergyObj in alDrugAllergy)
                                {
                                    if ((this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ID == drugAllergyObj.DiagInfo.ID)
                                    {
                                        MessageBoxShow("患者对此种药物过敏，请重新选择！");
                                        return;
                                    }
                                }
                            }
                            #endregion
                        }

                        //选择项目判断
                        if (IBeforeAddItem != null)
                        {
                            ArrayList al = new ArrayList();
                            al.Add(orderTemp);

                            if (this.IBeforeAddItem.OnBeforeAddItemForOutPatient(this.patientInfo, CacheManager.LogEmpl, CacheManager.LogEmpl.Dept, al) == -1)
                            {
                                if (!string.IsNullOrEmpty(IBeforeAddItem.ErrInfo))
                                {
                                    MessageBoxShow(IBeforeAddItem.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                return;
                            }
                        }
                    }
                }

                if (this.order != null && this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item == this.order.Item) //不重复
                {
                    if ((this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ItemType == EnumItemType.Drug)
                    {
                        SetQtyControlVisible(false);
                        //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                        this.txtCombNo.Focus();
                        this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                        //this.ucOrderInputByType1.Focus();
                    }
                    else
                    {
                        SetQtyControlVisible(true);

                        this.txtQTY.Focus();
                        this.txtQTY.Select(0, this.txtQTY.Text.Length);
                    }

                    return;
                }

                //项目变化-指向新行
                this.CurrentRow = -1;

                this.OperatorType = Operator.Add;

                //设置新医嘱
                this.SetNewOrder();

                //数量变化
                if (((ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ItemType == EnumItemType.Drug
                    && ucInputItem1.FeeItem.ID != "999")
                    || !this.txtQTY.Enabled)
                {
                    SetQtyControlVisible(false);
                    //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                    this.txtCombNo.Focus();
                    this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
                    //this.ucOrderInputByType1.Focus();
                }
                else
                {
                    SetQtyControlVisible(true);

                    this.txtQTY.Focus();
                    this.txtQTY.Select(0, this.txtQTY.Text.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucInputItem1_SelectedItem" + ex.Message);
            }
            finally
            {
                AddEvent();
            }
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
        /// 单位keyPress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.order == null) return;
            if (e == null || e.KeyChar == 13)
            {
                //增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}
                this.txtCombNo.Focus();
                this.txtCombNo.Select(0, this.txtCombNo.Text.Length);
            }
        }

        /// <summary>
        /// 单位选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string unit = this.cmbUnit.Text.Trim();
                if (FS.FrameWork.Public.String.ValidMaxLengh(unit, 16) == false)
                {
                    MessageBoxShow("单位超长!", "提示");
                    return;
                }

                if (this.order.Unit != unit)
                {
                    #region 判断是否是最小单位

                    if (this.order.Item.ItemType == EnumItemType.Drug)
                    {
                        if (this.order.Item.ID == "999")
                        {
                            this.order.MinunitFlag = "1";
                        }
                        else
                        {
                            if (this.cmbUnit.SelectedIndex == 0)
                            {
                                this.order.MinunitFlag = "1";
                            }
                            else
                            {
                                this.order.MinunitFlag = "0";
                            }
                        }
                    }
                    #endregion

                    this.order.Unit = unit;//更新单位
                    myOrderChanged(this.order, EnumOrderFieldList.Unit);
                }
            }
            catch { }
        }

        #endregion

        private void txtQTY_ValueChanged(object sender, EventArgs e)
        {
            int places = txtQTY.Value.ToString("F6").TrimEnd('0').Length - txtQTY.Value.ToString("F6").IndexOf('.');

            this.txtQTY.DecimalPlaces = places;

            return;
        }

        /// <summary>
        /// 获取当前控件的名称 只是用于跳转
        /// </summary>
        /// <returns></returns>
        public bool RecycleTab()
        {
            return ucOrderInputByType1.RecycleTab();
        }


        #region 增加方号功能 {98522448-B392-4d67-8C4D-A10F605AFDA5}

        private void txtCombNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucOrderInputByType1.Focus();
            }
        }

        private void txtCombNo_Leave(object sender, EventArgs e)
        {
            if (this.order == null)
            {
                return;
            }

            int i = (FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text));
            txtCombNo.Value = i;

            if ((FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text)) != this.order.SubCombNO)
            {
                int subCombTemp = this.order.SubCombNO;
                this.order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text);

                if (this.GetSameSortID(FS.FrameWork.Function.NConvert.ToInt32(this.txtCombNo.Text), this.order) == -1)
                {
                    this.order.SubCombNO = subCombTemp;
                    this.txtCombNo.Text = subCombTemp.ToString();
                    this.txtCombNo.Focus();
                    return;
                }
                //Components.Order.Classes.Function.ReComputeQty(order);

                this.CurrOrder = this.order;
                // 第一次进入门诊医生开立界面，如果组套开立，并修改第一条医嘱组号来实现组合功能，出现错误{81639443-4D8D-4e2e-8D9B-48F52F6F12AC}
                //this.OrderChanged(this.order, EnumOrderFieldList.SubComb);
                this.myOrderChanged(this.order, EnumOrderFieldList.SubComb);
            }
        }
        //佛四不想提示计算辅材

        private void chkDrugEmerce_CheckedChanged(object sender, EventArgs e)
        {
            this.isChangeSubComb = this.chkDrugEmerce.Checked;
        }
        //默认计算辅材
        public void changeChkDrugEmerce()
        {
            this.chkDrugEmerce.Checked = false;
            this.isChangeSubComb = false;
        }
        #endregion

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
    }

    ///// <summary>
    ///// 医嘱操作
    ///// </summary>
    //public enum Operator
    //{
    //    Add,
    //    Modify,
    //    Delete,
    //    Query
    //}

    ///// <summary>
    ///// 医嘱变化类型
    ///// </summary>
    //public enum EnumOrderFieldList
    //{
    //    /// <summary>
    //    /// !
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("!")]
    //    WarningFlag = 0,

    //    /// <summary>
    //    /// 警
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("警")]
    //    Warning = 1,

    //    /// <summary>
    //    /// 医嘱类型
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("医嘱类型")]
    //    OrderType = 2,

    //    /// <summary>
    //    /// 医嘱流水号
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("医嘱流水号")]
    //    OrderID = 3,

    //    /// <summary>
    //    /// 医嘱状态
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("医嘱状态")]
    //    Status = 4,

    //    /// <summary>
    //    /// 组合号
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("组合号")]
    //    CombNo = 5,

    //    /// <summary>
    //    /// 主药
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("主药")]
    //    MainDrug = 6,

    //    /// <summary>
    //    /// 项目编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("项目编码")]
    //    ItemCode = 7,

    //    /// <summary>
    //    /// 医嘱名称
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("医嘱名称")]
    //    ItemName = 8,

    //    /// <summary>
    //    /// 组合
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("组合")]
    //    CombFlag = 9,

    //    /// <summary>
    //    /// 总量
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("总量")]
    //    Qty = 10,

    //    /// <summary>
    //    /// 总量单位
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("总量单位")]
    //    Unit = 11,

    //    /// <summary>
    //    /// 每次用量
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("每次用量")]
    //    DoseOnce = 12,

    //    /// <summary>
    //    /// 单位
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("单位")]
    //    DoseUnit = 13,

    //    /// <summary>
    //    /// 付数/天数
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("付数/天数")]
    //    HerbalQty = 14,

    //    /// <summary>
    //    /// 频次编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("频次编码")]
    //    FrequencyCode = 15,

    //    /// <summary>
    //    /// 频次名称
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("频次名称")]
    //    Frequency = 16,

    //    /// <summary>
    //    /// 用法编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("用法编码")]
    //    UsageCode = 17,

    //    /// <summary>
    //    /// 用法名称
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("用法名称")]
    //    Usage = 18,

    //    /// <summary>
    //    /// 院注次数
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("院注次数")]
    //    InjNum = 19,

    //    /// <summary>
    //    /// 规格
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("规格")]
    //    Specs = 20,

    //    /// <summary>
    //    /// 单价
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("单价")]
    //    Price = 21,

    //    /// <summary>
    //    /// 金额
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("金额")]
    //    TotalCost = 22,

    //    /// <summary>
    //    /// 开始时间
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("开始时间")]
    //    BeginDate = 23,

    //    /// <summary>
    //    /// 开立医生
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("开立医生")]
    //    ReciptDoct = 24,

    //    /// <summary>
    //    /// 执行科室编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("执行科室编码")]
    //    ExecDeptCode = 25,

    //    /// <summary>
    //    /// 执行科室
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("执行科室")]
    //    ExeDept = 26,

    //    /// <summary>
    //    /// 加急
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("加急")]
    //    Emc = 27,

    //    /// <summary>
    //    /// 检查部位
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("检查部位")]
    //    CheckBody = 28,

    //    /// <summary>
    //    /// 样本类型
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("样本类型")]
    //    Sample = 29,

    //    /// <summary>
    //    /// 取药药房编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("取药药房编码")]
    //    DrugDeptCode = 30,

    //    /// <summary>
    //    /// 取药药房
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("取药药房")]
    //    DrugDept = 31,

    //    /// <summary>
    //    /// 备注
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("备注")]
    //    Memo = 32,

    //    /// <summary>
    //    /// 录入人编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("录入人编码")]
    //    InputOperCode = 33,

    //    /// <summary>
    //    /// 录入人
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("录入人")]
    //    InputOper = 34,

    //    /// <summary>
    //    /// 开立科室
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("开立科室")]
    //    ReciptDept = 35,

    //    /// <summary>
    //    /// 开立时间
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("开立时间")]
    //    MoDate = 36,

    //    /// <summary>
    //    /// 停止时间
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("停止时间")]
    //    EndDate = 37,

    //    /// <summary>
    //    /// 停止人编码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("停止人编码")]
    //    DCOperCode = 38,

    //    /// <summary>
    //    /// 停止人
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("停止人")]
    //    DCOper = 39,

    //    /// <summary>
    //    /// 顺序号
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("顺序号")]
    //    OrderNo = 40,

    //    /// <summary>
    //    /// 皮试代码
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("皮试代码")]
    //    HypoTestCode = 41,

    //    /// <summary>
    //    /// 皮试
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("皮试")]
    //    HypoTest = 42,


    //    /// <summary>
    //    /// 整个项目编码（新加）
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("项目")]
    //    Item = 43,

    //    /// <summary>
    //    /// 方号
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("方号")]
    //    SubComb = 44,

    //    /// <summary>
    //    /// 煎药方式
    //    /// </summary>
    //    [FS.FrameWork.Public.Description("煎药方式")]
    //    SpeUsage=45
    //}
}
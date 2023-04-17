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
    /// [功能描述: 医嘱附材修改]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucSubtblManager : UserControl
    {
        public ucSubtblManager()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// operFlag "1" 保存操作 "2" 删除操作
        /// isShowSubtblFlag  返回是否需要添加"@"标志
        /// sender 删除/停止的医嘱附材记录
        /// </summary>
        public delegate void ShowSubtblFlagEvent(string operFlag, bool isShowSubtblFlag, object sender);
        /// <summary>
        /// 是否需要在医嘱内添加"@"附材标志
        /// </summary>
        public event ShowSubtblFlagEvent ShowSubtblFlag;
        private string orderID;										//医嘱流水号		
        private string comboNo;										//医嘱组合号
        private bool isDCOrder = false;								//当前操作的医嘱是否为已停止的
        private ArrayList addSubInfo;							    //本次所增加的附材
        private ArrayList editSubInfo;								//本次更改的附材
        private FS.HISFC.Models.Base.Employee myOperator;		//操作员
        private FS.HISFC.Models.RADT.PatientInfo patientInfo;	//当前操作的患者信息 可以用于单独开立

        /// <summary>
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        private string isFeeWhenPriceZero = "-1";

        #endregion

        #region 属性
        /// <summary>
        /// 医嘱流水号
        /// </summary>
        public string OrderID
        {
            set
            {
                this.orderID = value;
            }
        }

        /// <summary>
        /// 医嘱组合号
        /// </summary>
        public string ComboNo
        {
            set
            {
                this.comboNo = value;
                this.Clear();
                this.QuerySubtbl();
            }
        }

        /// <summary>
        /// 当前操作的医嘱是否为已停止的
        /// </summary>
        public bool IsDCOrder
        {
            set
            {
                this.isDCOrder = value;
            }
        }
        /// <summary>
        /// 当前操作员
        /// </summary>
        protected FS.HISFC.Models.Base.Employee Operator
        {
            get
            {
                if (myOperator == null) 
                    myOperator = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                return myOperator;
            }
            set
            {
                this.myOperator = value;
            }
        }


        /// <summary>
        /// 当前查询的患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                if (this.patientInfo != value)
                {
                    this.patientInfo = value;
                    this.Clear();
                }
            }
        }
        
        /// <summary>
        /// 是否纵向显示 
        /// </summary>
        public bool IsVerticalShow
        {
            set
            {
                if (value)
                {
                    this.Width = 452;
                }
                else
                {
                    this.Width = 750;
                }

            }
        }
        /// <summary>
        /// 本次增加的附材
        /// </summary>
        public ArrayList AddSubInfo
        {
            get
            {
                if (this.addSubInfo == null)
                    this.addSubInfo = new ArrayList();
                return this.addSubInfo;
            }
        }
        /// <summary>
        /// 本次更改的附材
        /// </summary>
        public ArrayList EditSubInfo
        {
            get
            {
                if (this.editSubInfo == null)
                    this.editSubInfo = new ArrayList();
                return this.editSubInfo;
            }
        }

        /// <summary>
        /// 药品和非药品的价格为0是否收费：0不收取；1收取。默认值为0不收取
        /// </summary>
        public string IsFeeWhenPriceZero
        {
            get
            {
                if (this.isFeeWhenPriceZero == "-1")
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam con = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    this.isFeeWhenPriceZero = con.GetControlParam<string>("FEE001", false, "0");
                }
                return this.isFeeWhenPriceZero;
            }
        }

        #endregion

        #region 初始化函数
        protected void InitSubtbl()
        {
            this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
            this.ucInputItem1.ShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;
            this.ucInputItem1.IsShowDeptGroup = false;
            this.ucInputItem1.DeptCode = this.Operator.Dept.ID;
            //{8F86BB0D-9BB4-4c63-965D-969F1FD6D6B2} 医嘱附材绑定物资 by gengxl
            this.ucInputItem1.IsIncludeMat = true;

            this.ucInputItem1.Init();
        }
       
        #endregion

        #region 方法

        /// <summary>
        /// 设置右侧显示界面布局
        /// </summary>
        public void SetRightShow()
        {
            this.panelItem.Size = new Size(724, 62);
            this.neuLabel2.Location = new Point(2, 43);
            this.txtQty.Location = new Point(45, 39);
            this.txtUnit.Location = new Point(98, 38);
        }

        /// <summary>
        /// 查询附材
        /// </summary>
        protected void QuerySubtbl()
        {
            //检索添加附材
            if (this.comboNo == "")
                return;
            ArrayList al = CacheManager.InOrderMgr.QuerySubtbl(this.comboNo);
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("查询医嘱附材出错！") + CacheManager.InOrderMgr.Err);
            }
            try
            {
                this.sheetView1.RowCount = 0;
            }
            catch { }
            //判断当前医嘱是否为停止医嘱
            if (CacheManager.InOrderMgr.QueryOneOrderState(this.orderID) == 3)
            {
                this.isDCOrder = true;
            }
            else
            {
                this.isDCOrder = false;
            }
            for (int i = 0; i < al.Count; i++)
            {
                this.AddSubtbl((FS.HISFC.Models.Order.Inpatient.Order)al[i], false);
            }
        }
        /// <summary>
        /// 添加一条附材
        /// </summary>
        protected void AddSubtbl(FS.HISFC.Models.Order.Inpatient.Order order, bool isNew)
        {
            if (order == null)
                return;

            //对已停止附材不显示
            if (!isNew && order.Status == 3)
            {
                if (!this.isDCOrder)
                    return;
            }

            if (!this.IsFee(order))  //order.Item.Price == 0)
            {
                MessageBox.Show("价格为零，不符合附材条件！");
                return;
            }

            //更改执行科室为患者科室
            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//this.Operator.Nurse.Clone();
            //order.ReciptDept = order.Patient.PVisit.PatientLocation.Dept.Clone();//this.Operator.Nurse.Clone();

            //此处不再修改开立科室和执行科室，保证附材和医嘱的开立、执行科室一样
            //对于检验的附材，特殊处理执行科室为开立科室
            //if (order.Item.SysClass.ID.ToString() == "UL")
            //{
            //    order.ExeDept.ID = order.ReciptDept.ID;
            //    order.ExeDept.Name = order.ReciptDept.Name;
            //}

            order.Oper.ID = this.Operator.ID;
            order.Oper.Name = this.Operator.Name;
            //order.ReciptDoctor.ID = this.Operator.ID;
            //order.ReciptDoctor.Name = this.Operator.Name;
            order.IsSubtbl = true;

            //修改开立时间
            order.MOTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();

            //不修改开始时间，避免开始时间比下次分解时间晚
            //order.BeginTime = order.MOTime;

            //添加一行
            this.sheetView1.Rows.Add(0, 1);
            this.sheetView1.Cells[0, 0].Text = order.Item.ID;				//编码
            //this.sheetView1.Cells[0, 1].Text = order.Item.Name;			//名称

            if (string.IsNullOrEmpty(order.Item.Specs))
            {
                if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(this.Operator.Nurse.Clone().ID))
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "【" + SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpecialPrice + "元】";//名称
                }
                else
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "【" + order.Item.Price + "元】";//名称
                }
            }
            else
            {
                if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(this.Operator.Nurse.Clone().ID))
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "【" + order.Item.Specs + "】" + "【" + SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpecialPrice + "元】";//名称
                }
                else
                {
                    this.sheetView1.Cells[0, 1].Text = order.Item.Name + "【" + order.Item.Specs + "】" + "【" + order.Item.Price + "元】";//名称
                }
            }
            if (order.Item.Price == 0)
            {
                this.sheetView1.Cells[0, 3].Locked = false;				//价格
            }
            else
            {
                this.sheetView1.Cells[0, 3].Locked = true;					//价格
            }

            if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(this.Operator.Nurse.Clone().ID))
            {
                this.sheetView1.Cells[0, 3].Value = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).SpecialPrice;			//价格
            }
            else
            {
                this.sheetView1.Cells[0, 3].Value = order.Item.Price;			//价格
            }
            if (order.Item.Qty == 0)
                order.Item.Qty = 1;
            this.sheetView1.Cells[0, 4].Value = order.Item.Qty;			//数量
            this.txtQty.Text = order.Item.Qty.ToString();
            this.sheetView1.Cells[0, 5].Text = order.Item.PriceUnit;		//单位 
            order.Unit = order.Item.PriceUnit;
            this.txtUnit.Text = order.Item.PriceUnit;
            this.sheetView1.Cells[0, 6].Text = order.Frequency.ID;			//频次

            this.sheetView1.Cells[0, 7].Text = order.BeginTime.ToString();	//开始时间
            this.sheetView1.Cells[0, 8].Text = order.EndTime.ToString();	//结束时间
            this.sheetView1.Cells[0, 9].Text = order.Memo;				//备注1
            //FS.HISFC.Models.Base.Department mydept = new FS.HISFC.Models.Base.Department();
            //mydept = this.deptManager.GetDepartment(order.ReciptDept.ID);
            //this.sheetView1.Cells[0, 10].Text = mydept.Name;		//执行科室
            this.sheetView1.Cells[0, 10].Text = SOC.HISFC.BizProcess.Cache.Common.GetDeptName(order.ExeDept.ID);		//执行科室
            this.sheetView1.Cells[0, 11].Text = isNew ? "1" : "0";				//是否新增加的 0 由数据库检索的原附材 1 新附材
            this.sheetView1.Rows[0].Tag = order;
        }
        /// <summary>
        /// 保存附材
        /// </summary>
        public int SaveSubtbl()
        {
            //如当前操作的为已停止医嘱则不允许更改附材
            if (this.isDCOrder)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("已停止医嘱不允许更改附材"));
                return -1;
            }
            //保存本次所增加的附材
            try
            {
                if (this.addSubInfo == null)
                    this.addSubInfo = new ArrayList();
                else
                    this.addSubInfo.Clear();
                if (this.editSubInfo == null)
                    this.editSubInfo = new ArrayList();
                else
                    this.editSubInfo.Clear();
            }
            catch { }

            #region 事务声明
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #endregion

            FS.HISFC.Models.Order.Inpatient.Order order;
            for (int i = 0; i < this.sheetView1.Rows.Count; i++)
            {
                if (this.sheetView1.Rows[i].Tag != null)
                {
                    try
                    {
                        #region 实体赋值
                        order = this.sheetView1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;
                        if (order == null)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("处理附材保存过程中出错！ 发生类型转换错误");
                            return -1;
                        }
                        try
                        {
                            order.Qty = System.Convert.ToDecimal(sheetView1.Cells[i, 4].Value);	//数量
                        }
                        catch
                        {
                            order.Qty = 1;
                        }
                        if (order.Qty == 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show(order.Item.Name + "数量应大于零!");
                            return -1;
                        }
                       
                        order.Unit = order.Item.PriceUnit;												//单位
                        order.ExtendFlag1 = this.sheetView1.Cells[i, 9].Text;							//备注
                        
                        #endregion

                        // 判断医嘱状态 处理并发
                        FS.HISFC.Models.Order.Inpatient.Order od = CacheManager.InOrderMgr.QueryOneOrder(orderID);
                        if (od.Status == 3)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("医嘱状态已发生变化！已停止医嘱不允许更改附材");
                            return -1;
                        }
                        //临时医嘱
                        if (od.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                        {
                            if (od.Status != 0)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();;
                                MessageBox.Show("对已审核/执行的临时医嘱不允许进行附材修改！");
                                return -1;
                            }
                        }
                        if (od.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)//非药品
                        {
                            //不再重取执行科室，否则终端确认附材的确认有问题
                            //order.ExeDept = order.Patient.PVisit.PatientLocation.Dept.Clone();
                        }
                        order.IsSubtbl = true; //附材标记

                        if (CacheManager.InOrderMgr.SetOrder(order) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show(CacheManager.InOrderMgr.Err);
                            return -1;
                        }

                        if (this.sheetView1.Cells[i, 11].Text == "1")			//本次添加的附材
                        {
                            this.addSubInfo.Add(order);
                        }
                        else
                        {
                            this.editSubInfo.Add(order);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存错误！" + ex.Message);
                    }
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            this.SetSubtblFlag("1", null);
            return 0;
        }
        /// <summary>
        /// 删除附材
        /// </summary>
        public int DelSubtbl(object order)
        {
            FS.HISFC.Models.Order.Inpatient.Order myOrder = order as FS.HISFC.Models.Order.Inpatient.Order;
            if (myOrder == null)
            {
                MessageBox.Show("删除附材过程中出错！ 发生类型转换错误");
                return -1;
            }
            if (myOrder.ID.Trim() != "")
            {
                if (myOrder.Status == 3)
                {
                    MessageBox.Show("已停止医嘱无法更改附材");
                    return 0;
                }
                if (myOrder.OrderType.Type == FS.HISFC.Models.Order.EnumType.SHORT)
                {
                    if (myOrder.Status != 0)
                    {
                        MessageBox.Show("已审核/执行的临嘱不允许进行附材修改！");
                        return 0;
                    }
                }
                if (MessageBox.Show("是否彻底删除附材" + myOrder.Item.Name, "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    if (myOrder.Status != 1 && myOrder.Status != 2)
                    {
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                        //t.BeginTransaction();
                        CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        //对未审核医嘱删除附材
                        if (CacheManager.InOrderMgr.DeleteOrder(myOrder) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();;
                            MessageBox.Show("无法删除!" + CacheManager.InOrderMgr.Err);
                            return -1;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                    else
                    {//对已审核、或执行医嘱停止附材
                        if (this.DCSub(myOrder) == -1)
                        {
                            return -1;
                        }
                    }
                }
                else
                {
                    return -1;
                }
            }
            return 1;
        }
        /// <summary>
        /// 如果已经审核或执行，则停止附材
        /// </summary>
        /// <returns></returns>
        public int DCSub(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.InOrderMgr.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.InOrderMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            order.DCOper.ID = this.Operator.ID;
            order.DCOper.Name = this.Operator.Name;
            order.EndTime = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime();
            order.Status = 3;
            order.DcReason.Name = "护士站审核查询停止";
            if (CacheManager.InOrderMgr.DcOneOrder(order) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show("删除医嘱失败!" + CacheManager.InOrderMgr.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            return 0;
        }
        /// <summary>
        /// 设置是否需要显示医嘱附材标志
        /// </summary>
        /// <param name="operFlag">操作标志 "1" 保存操作 "2" 删除操作</param>
        /// <param name="sender">删除/停止的附材医嘱</param>
        private void SetSubtblFlag(string operFlag, object sender)
        {
            if (this.sheetView1.Rows.Count > 0)
            {
                if (this.ShowSubtblFlag != null)
                    this.ShowSubtblFlag(operFlag, true, sender);
            }
            else
            {
                if (this.ShowSubtblFlag != null)
                    this.ShowSubtblFlag(operFlag, false, sender);
            }
        }
        /// <summary>
        /// 清空数据显示
        /// </summary>
        public void Clear()
        {
            try
            {
                this.ucInputItem1.FeeItem = new FS.FrameWork.Models.NeuObject();
                this.txtQty.Text = "";
                this.txtUnit.Text = "";
              
                this.sheetView1.Rows.Count = 0;
                if (this.addSubInfo != null)
                    this.addSubInfo.Clear();
            }
            catch
            { }
        }

        /// <summary>
        /// 是否符合收费条件
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsFee( FS.HISFC.Models.Order.Inpatient.Order order)
        {
            if (this.IsFeeWhenPriceZero == "1")
            {
                return true;
            }

            if (order.Item.Price > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            #region 初始化 加载附材项目信息
            this.InitSubtbl();
            #endregion


            #region 事件定义
            this.Spread1.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread1_CellDoubleClick);
            this.Spread1.SelectionChanged += new FarPoint.Win.Spread.SelectionChangedEventHandler(fpSpread1_SelectionChanged);
            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.txtQty.Leave += new EventHandler(txtNum_Leave);
            this.btnSave.Click += new EventHandler(btnOK_Click);
            this.btnDelete.Click += new EventHandler(btnDelete_Click);

            #endregion
        }
        
        void btnDelete_Click(object sender, EventArgs e)
        {
            int rowCount = this.sheetView1.RowCount;
            for (int i = rowCount - 1; i >= 0; i--)
            {
                if (this.sheetView1.IsSelected(i,0) ==true)
                {
                    if (this.sheetView1.Rows[i].Tag != null)
                    {
                        if (this.sheetView1.Cells[i, 11].Text == "0")				//由数据库内检索的附材
                        {
                            if (this.DelSubtbl(this.sheetView1.Rows[i].Tag) != 1)
                            {
                                return;
                            }
                        }
                        object temp = this.sheetView1.Rows[i].Tag;
                        this.sheetView1.Rows.Remove(i, 1);
                        //更新附材维护标志
                        this.SetSubtblFlag("2", temp);
                    }
                }
            }
            
        }

        void ucInputItem1_SelectedItem(FS.FrameWork.Models.NeuObject sender)
        {
            ucItem1_SelectedItem();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (!e.RowHeader && !e.ColumnHeader)
            {
                if (this.sheetView1.ActiveRow.Tag != null)
                {
                    if (this.sheetView1.Cells[e.Row, 11].Text == "0")				//由数据库内检索的附材
                    {
                        if (this.DelSubtbl(this.sheetView1.ActiveRow.Tag) != 1)
                        {
                            return;
                        }
                    }
                    object temp = this.sheetView1.ActiveRow.Tag;
                    this.sheetView1.Rows.Remove(this.sheetView1.ActiveRowIndex, 1);
                    //更新附材维护标志
                    this.SetSubtblFlag("2", temp);
                }
            }
        }

        private void ucItem1_SelectedItem()
        {
            try
            {
                FS.HISFC.Models.Order.Inpatient.Order order = CacheManager.InOrderMgr.QueryOneOrder(this.orderID);
                if (this.orderID != "" && order == null)			//医嘱流水号不为空且未获取有效order实体
                {
                    MessageBox.Show("获取医嘱信息出错！" + CacheManager.InOrderMgr.Err);
                    return;
                }
                if (order == null)
                {
                    return;
                }
                //{A94C1C0F-EEC1-471e-9CCE-3ED8DE582AB8}  医嘱类型处理
                if (!order.OrderType.IsCharge)
                {
                    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
                    {
                        order.OrderType.ID = "CZ";
                        order.OrderType.Name = "长期医嘱";
                        order.OrderType.IsCharge = true;
                    }
                    else
                    {
                        order.OrderType.ID = "LZ";
                        order.OrderType.Name = "临时医嘱";
                        order.OrderType.IsCharge = true;
                    }
                }

                //此处不再修改开立科室和执行科室，保证附材和医嘱的开立、执行科室一样
                //对于检验的附材，特殊处理执行科室为开立科室
                //这里判断的是原医嘱项目
                if (order.Item.SysClass.ID.ToString() == "UL")
                {
                    order.ExeDept.ID = order.ReciptDept.ID;
                    order.ExeDept.Name = order.ReciptDept.Name;
                }

                FS.HISFC.Models.Base.Item item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                if (item == null)
                {
                    MessageBox.Show("获取所选择的项目信息出错!");
                    return;
                }
                if (item.PriceUnit == "[组套]")		//处理对护士维护的套餐的开立
                {
                    //#region 添加护士维护的套餐
                    //FS.HISFC.BizLogic.Manager.ComGroupTail group = new FS.HISFC.BizLogic.Manager.ComGroupTail();
                    //ArrayList groupDetails = new ArrayList();
                    ////根据组套id获取组套明细
                    //groupDetails = group.GetComGroupTailByGroupID(item.ID);
                    //if (groupDetails == null || groupDetails.Count == 0) return;
                    //FS.HISFC.Models.Order.Order info;
                    //for (int i = 0; i < groupDetails.Count; i++)
                    //{
                    //    FS.HISFC.Models.Fee.ComGroupTail obj = groupDetails[i] as FS.HISFC.Models.Fee.ComGroupTail;
                    //    if (obj == null)
                    //    {
                    //        MessageBox.Show("无法添加附材 获取套餐明细出错！");
                    //        return;
                    //    }
                    //    if (obj.drugFlag == "1")//药品
                    //    {
                    //        info = order.Clone();
                    //        //根据药品id获取药品实体
                    //        FS.HISFC.Models.Pharmacy.Item drug = null;
                    //        FS.HISFC.BizLogic.Pharmacy.Item drugManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                    //        drug = drugManager.GetItemForInpatient(patientInfo.PVisit.PatientLocation.Dept.ID, obj.itemCode);
                    //        if (drug == null || drug.ID == "") continue;
                    //        FS.HISFC.Models.Base.Item drugBase = drug as FS.HISFC.Models.Base.Item;
                    //        drugBase.isPharmacy = true;
                    //        drugBase.Amount = obj.qty;
                    //        info.Item = drugBase;
                    //        info.ID = "";
                    //        this.AddSubtbl(info, true);
                    //    }
                    //    else//非药品
                    //    {
                    //        info = order.Clone();
                    //        //根据非药品id获取非药品实体
                    //        FS.HISFC.Models.Fee.Item undrug = null;
                    //        FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();
                    //        undrug = undrugManager.GetItem(obj.itemCode);
                    //        if (undrug == null) continue;
                    //        //添加划价项目
                    //        FS.HISFC.Models.Base.Item undrugBase = undrug as FS.HISFC.Models.Base.Item;
                    //        undrugBase.isPharmacy = false;
                    //        undrugBase.Amount = obj.qty;//数量
                    //        info.Item = undrugBase;
                    //        info.ID = "";
                    //        this.AddSubtbl(info, true);
                    //    }
                    //}
                    //#endregion

                    return;
                }
                else
                {
                    order.Item = item.Clone();
                    order.ID = "";										//医嘱流水号
                    this.AddSubtbl(order, true);
                }
            }
            catch
            {
                MessageBox.Show("获得医嘱出错！" + CacheManager.InOrderMgr.Err);
            }
            this.txtQty.Focus();
        }


        private void txtNum_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.sheetView1.Rows.Count > 0)
                    this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, 4].Value = this.txtQty.Value;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, 4].Value = 1;				//数量
            }
        }

      

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveSubtbl();
        }

      

        private void fpSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            FS.HISFC.Models.Order.Inpatient.Order order = this.sheetView1.Rows[this.sheetView1.ActiveRowIndex].Tag as FS.HISFC.Models.Order.Inpatient.Order;
            if (order == null)
            {
                MessageBox.Show("获取医嘱实体信息时发生类型转换错误");
                return;
            }
            try
            {
                this.ucInputItem1.FeeItem = order.Item;
                this.txtQty.Text = this.sheetView1.Cells[this.sheetView1.ActiveRowIndex, 4].Text;
                this.txtUnit.Text = order.Unit;
         }
            catch
            { }
        }

      
        #endregion

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                ucInputItem1.txtItemCode.Focus();
            }
        }

        private void txtQty_Enter(object sender, EventArgs e)
        {
            this.txtQty.Select(0, this.txtQty.Value.ToString().Length);
        }
    }
}

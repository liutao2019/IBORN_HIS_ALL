using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.SOC.HISFC.Components.InPateintOrder.Controls
{
    /// <summary>
    /// [功能描述: 医嘱分解控件的说]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucOrderExecConfirm : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderExecConfirm()
        {
            InitializeComponent();
        }

        #region 变量

        protected FS.HISFC.BizLogic.Order.Order orderManagement = new FS.HISFC.BizLogic.Order.Order();
        protected FS.HISFC.BizProcess.Integrate.Fee feeManagement = new FS.HISFC.BizProcess.Integrate.Fee();
        protected FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();

        protected DateTime dt;

        /// <summary>
        /// 药品全选情况
        /// </summary>
        bool tab0AllSelect = false;

        /// <summary>
        /// 非药品全选情况
        /// </summary>
        bool tab1AllSelect = false;

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManager = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 科室业务类
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager deptManager = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// 科室列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        bool bOnQuery = false;

        /// <summary>
        /// 用法对应的分解时间
        /// </summary>
        private Hashtable hsUsageAndTime = new Hashtable();

        /// <summary>
        /// 小时计费医嘱的频次代码
        /// </summary>
        string frequencyID = "";

        #endregion

        #region 属性

        //protected ArrayList al = new ArrayList();

        ///// <summary>
        ///// 人员列表
        ///// </summary>
        //private ArrayList alPatients
        //{
        //    get
        //    {
        //        if (al == null) al = new ArrayList();
        //        return al;
        //    }
        //    set
        //    {
        //        this.al = value;
        //    }
        //}

        /// <summary>
        /// 人员列表
        /// </summary>
        private ArrayList alPatients = new ArrayList();

        /// <summary>
        /// 默认分解执行天数
        /// </summary>
        protected int intDyas = 1;

        /// <summary>
        /// 默认分解执行天数
        /// </summary>
        [Category("分解设置"), Description("默认分解执行天数")]
        public int Days
        {
            set
            {
                this.intDyas = value;
                //this.txtDays.Value = (decimal)value;
                this.txtDays.Maximum = this.intDyas;
            }
            get
            {
                return this.intDyas;
            }
        }

        /// <summary>
        /// 显示的信息
        /// </summary>
        [Category("分解设置"), Description("显示给用户的提示信息。")]
        public string Tip
        {
            get
            {
                return this.neuLabel3.Text;
            }
            set
            {
                this.neuLabel3.Text = value;
            }
        }

        /// <summary>
        /// 欠费判断模式
        /// </summary>
        protected EnumLackFee lackfee = EnumLackFee.不判断欠费;

        /// <summary>
        /// 欠费操作
        /// </summary>
        [Category("分解设置"), Description("欠费判断模式")]
        public EnumLackFee 欠费操作
        {
            get
            {
                return this.lackfee;
            }
            set
            {
                this.lackfee = value;
            }
        }

        /// <summary>
        /// 欠费患者审核保存时是否同时计费
        /// </summary>
        private bool lackFeeDealModel = true;

        /// <summary>
        /// 欠费患者审核保存时是否同时计费
        /// </summary>
        [Category("分解设置"), Description("欠费患者审核保存时是否同时计费？")]
        public bool LackFeeDealModel
        {
            get
            {
                return lackFeeDealModel;
            }
            set
            {
                lackFeeDealModel = value;
            }
        }

        /// <summary>
        /// 当前患者因为出错保存不成功时，是否允许继续保存其他患者？
        /// </summary>
        protected bool isSaveErrContinue = true;

        /// <summary>
        /// 保存出错后，是否继续进行其它患者保存
        /// </summary>
        [Category("分解设置"), Description("当前患者因为出错保存不成功时，是否允许继续保存其他患者？")]
        public bool IsSaveErrContinue
        {
            get
            {
                return this.isSaveErrContinue;
            }
            set
            {
                this.isSaveErrContinue = value;
            }
        }

        #endregion

        #region 函数
        /// <summary>
        /// 初始化FpSpread
        /// </summary>
        private void InitControl()
        {
            this.fpOrderExecBrowser1.IsShowRowHeader = false;
            this.fpOrderExecBrowser2.IsShowRowHeader = false;
            this.TabControl1.SelectedIndex = 1;
            this.TabControl1.SelectedIndex = 0;

            this.fpOrderExecBrowser1.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);
            this.fpOrderExecBrowser2.fpSpread.CellDoubleClick += new FarPoint.Win.Spread.CellClickEventHandler(fpSpread_CellDoubleClick);

            this.fpOrderExecBrowser1.fpSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_ButtonClicked);
            this.fpOrderExecBrowser2.fpSpread.ButtonClicked += new FarPoint.Win.Spread.EditorNotifyEventHandler(fpSpread_ButtonClicked);

            #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
            this.deptHelper.ArrayObject = this.deptManager.GetDeptmentAllValid();
            #endregion

            //取小时收费 {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
            frequencyID = controlManager.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.MetConstant.Hours_Frequency_ID, true);

            FS.HISFC.BizLogic.Manager.Constant myCnst = new FS.HISFC.BizLogic.Manager.Constant();

            #region 先屏蔽

            ArrayList alItems = myCnst.GetAllList("USAGEDIVTIME");

            ArrayList alItemsUsage = myCnst.GetAllList("USAGE");

            FS.HISFC.Models.Base.Const None = new FS.HISFC.Models.Base.Const();
            None.ID = "NONE";
            None.Name = "非药品通用";
            None.UserCode = "NONE";
            alItemsUsage.Add(None);

            foreach (FS.HISFC.Models.Base.Const cnst in alItemsUsage)
            {
                if (!hsUsageAndTime.Contains(cnst.ID))
                {
                    cnst.Memo = "";

                    if (cnst.UserCode.Length > 0)
                    {
                        foreach (FS.HISFC.Models.Base.Const usage in alItems)
                        {
                            if (usage.ID == cnst.UserCode)
                            {
                                cnst.Memo = usage.Memo;
                            }
                        }
                    }

                    hsUsageAndTime.Add(cnst.ID, cnst);
                }
            }
            #endregion
        }

        /// <summary>
        /// 查询执行档--分解医嘱
        /// </summary>
        /// <returns></returns>
        public int RefreshExec()
        {
            string DeptCode = ""; //病区

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.orderManagement.Connection);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            ArrayList alOrders = null;
            FS.HISFC.BizProcess.Integrate.RADT pManager = new FS.HISFC.BizProcess.Integrate.RADT();
            try
            {
                #region 分解审核过的医嘱
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在分解，请稍候...");
                Application.DoEvents();

                this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                pManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


                //获取全部的频次,供每条医嘱使用，避免每条都查数据库
                ArrayList alFrequency = new ArrayList();
                alFrequency = this.frequencyManagement.GetAll(DeptCode);
                FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(alFrequency);

                //分解的操作时间,避免分解时每条医嘱都查询系统时间
                DateTime dt = new DateTime();
                dt = this.orderManagement.GetDateTimeFromSysDateTime();
                FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
                string errInfo = "";

                //对每个患者的医嘱分解
                for (int i = 0; i < this.alPatients.Count; i++)
                {
                    //患者信息
                    patientInfo = alPatients[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (patientInfo == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show("获取患者基本信息出错：患者信息为空！");
                        return -1;
                    }
                    else if (Classes.Function.CheckPatientState(patientInfo.ID, ref patientInfo, ref errInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(errInfo);
                        return -1;
                    }

                    //由医嘱主表内检索医嘱状态为1 或 2的医嘱
                    alOrders = orderManagement.QueryValidOrderWithSubtbl(patientInfo.ID, FS.HISFC.Models.Order.EnumType.LONG);
                    if (alOrders == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack(); ;
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        MessageBox.Show(orderManagement.Err);
                        return -1;
                    }

                    FS.HISFC.Models.Order.Inpatient.Order order = null;

                    #region 分解参数传入

                    orderManagement.AlAllOrders = alOrders;
                    orderManagement.HsUsageAndTime = this.hsUsageAndTime;

                    #endregion

                    //分解患者的医嘱
                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        order = (FS.HISFC.Models.Order.Inpatient.Order)alOrders[j];

                        #region 更改科室

                        DeptCode = order.ReciptDept.ID;//开单科室
                        //医嘱实体中的患者在院科室重新赋值
                        order.Patient.PVisit.PatientLocation.Dept.ID = patientInfo.PVisit.PatientLocation.Dept.ID;
                        order.Patient.PVisit.PatientLocation.NurseCell.ID = patientInfo.PVisit.PatientLocation.NurseCell.ID;

                        #endregion

                        #region 获取频次时间点

                        //如果为特殊频次就给频次赋值，否则为原来的频次
                        FS.HISFC.Models.Order.Frequency f = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID)
                            as FS.HISFC.Models.Order.Frequency;
                        if (order.Frequency.Times.Length == f.Times.Length && order.Frequency.Time != f.Time)
                        {
                            //特殊频次
                        }
                        else
                        {
                            order.Frequency = (FS.HISFC.Models.Order.Frequency)helper.GetObjectFromID(order.Frequency.ID).Clone();

                            if (order.Frequency == null)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(orderManagement.Err);
                                return -1;
                            }
                        }

                        #endregion

                        //{97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
                        //小时收费医嘱，分解时间到当前时间
                        if (order.Frequency.ID == frequencyID)
                        {
                            if (orderManagement.DecomposeOrderToNow(order, 0, false, dt) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(orderManagement.Err);
                                return -1;
                            }
                        }
                        else
                        {
                            //对医嘱进行分解
                            if (orderManagement.DecomposeOrder(order, this.intDyas, false, dt) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                MessageBox.Show(orderManagement.Err);
                                return -1;
                            }
                        }
                    }
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                #endregion
            }
            catch (Exception ex)
            {
                FS.FrameWork.Management.PublicTrans.RollBack(); ;
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //MessageBox.Show("分解出错！" + ex.Message + this.orderManagement.iNum.ToString());
                MessageBox.Show("分解出错！" + ex.Message);
                return -1;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            this.RefreshQuery();
            return 0;
        }

        /// <summary>
        /// 更新查询显示
        /// </summary>
        /// <returns></returns>
        protected int RefreshQuery()
        {
            if (bOnQuery) 
                return 0;
            bOnQuery = true;
            try
            {
                this.txtName.Items.Clear();
                ArrayList alOrders = null;

                #region 查询显示
                this.fpOrderExecBrowser1.Clear();
                this.fpOrderExecBrowser2.Clear();

                FS.FrameWork.Public.ObjectHelper orderNameHlpr = new FS.FrameWork.Public.ObjectHelper();
                FS.FrameWork.Public.ObjectHelper deptNameHlpr = new FS.FrameWork.Public.ObjectHelper();
                FS.FrameWork.Models.NeuObject objTmp = new FS.FrameWork.Models.NeuObject();

                for (int i = 0; i < this.alPatients.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo p = this.alPatients[i] as FS.HISFC.Models.RADT.PatientInfo;
                    //if (feeManagement.IsPatientLackFee(p) == true) //欠费患者
                    //{
                    //    switch (this.lackfee)
                    //    {
                    //        case EnumLackFee.不判断欠费:
                    //            break;
                    //        case EnumLackFee.欠费不允许分解:
                    //            MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                    //                string.Format("患者{0}已经欠费,剩余金额{1}.不能进行分解操作。", p.Name, p.FT.LeftCost.ToString())));
                    //            continue;
                    //            break;
                    //        case EnumLackFee.欠费提示允不允许分解:
                    //            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                    //                string.Format("患者{0}已经欠费,剩余金额{1}.是否继续分解操作。", p.Name, p.FT.LeftCost.ToString())), "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                    //            {
                    //                continue;
                    //            }
                    //            break;

                    //    }
                    //}
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询" + p.Name + "，请稍候...");

                    #region 查询分解操作

                    alOrders = orderManagement.QueryExecOrderIsExec(p.ID, "1", false);//查询确认的药品 

                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;
                        order.Order.Patient = p;

                        #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}

                        string drugDept = this.deptHelper.GetName(order.Order.StockDept.ID);

                        #endregion

                        if (order.Order.OrderType.IsDecompose)
                        {
                            this.fpOrderExecBrowser1.AddRow(order);
                            objTmp = new FS.FrameWork.Models.NeuObject(order.Order.Item.Name, order.Order.Item.Name, "");
                            if (orderNameHlpr.GetObjectFromID(objTmp.ID) == null)
                            {
                                orderNameHlpr.ArrayObject.Add(objTmp);
                            }
                            #region {13EAF764-E1CA-4d5a-8250-056AD1DEE61B}
                            if (!string.IsNullOrEmpty(drugDept))
                            {
                                objTmp = new FS.FrameWork.Models.NeuObject(drugDept, drugDept, "");
                                if (deptNameHlpr.GetObjectFromID(objTmp.ID) == null)
                                {
                                    deptNameHlpr.ArrayObject.Add(objTmp);
                                }
                            }
                            #endregion
                        }
                    }
                    //非药品
                    alOrders = orderManagement.QueryExecOrderIsExec(p.ID, "2", false);//查询未执行的非药品
                    for (int j = 0; j < alOrders.Count; j++)
                    {
                        FS.HISFC.Models.Order.ExecOrder order = alOrders[j] as FS.HISFC.Models.Order.ExecOrder;
                        order.Order.Patient = p;
                        //显示需要护士站确认的非药品
                        if ((((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).IsNeedConfirm == false ||
                            order.Order.ExeDept.ID == order.Order.ReciptDept.ID ||
                            order.Order.ExeDept.ID == NurseStation.ID)) //护士站收费或者执行科室＝＝科室  
                        {
                            if (order.Order.OrderType.IsDecompose) //长期医嘱
                            {
                                this.fpOrderExecBrowser2.AddRow(order);
                                objTmp = new FS.FrameWork.Models.NeuObject(order.Order.Item.Name, order.Order.Item.Name, "");
                                if (orderNameHlpr.GetObjectFromID(objTmp.ID) == null)
                                {
                                    orderNameHlpr.ArrayObject.Add(objTmp);
                                }
                            }
                        }
                    }

                    #endregion

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                }
                #endregion

                objTmp = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
                orderNameHlpr.ArrayObject.Insert(0, objTmp);
                this.txtName.AddItems(orderNameHlpr.ArrayObject);
                this.txtName.Tag = "ALL";
                objTmp = new FS.FrameWork.Models.NeuObject("ALL", "全部", "");
                deptNameHlpr.ArrayObject.Insert(0, objTmp);
                this.txtDrugDeptName.AddItems(deptNameHlpr.ArrayObject);
                this.txtDrugDeptName.Tag = "ALL";

                this.fpOrderExecBrowser1.RefreshComboNo();
                this.fpOrderExecBrowser2.RefreshComboNo();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                //护士重新计算时，先计算2天 然后在计算1天的时候 删除多余的数据（界面上删除）
                this.fpOrderExecBrowser1.DeleteRow(this.intDyas);
                this.fpOrderExecBrowser2.DeleteRow(this.intDyas);
                this.tabPage1.Text = "药品" + "【" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "条】";
                this.tabPage2.Text = "非药品" + "【" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "条】";
            }
            catch
            {
            }
            bOnQuery = false;

            return 0;
        }

        /// <summary>
        /// 护士站属性
        /// </summary>
        protected FS.FrameWork.Models.NeuObject NurseStation
        {
            get
            {
                return ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.Clone();
            }
        }

        /// <summary>
        /// 发送医嘱
        /// </summary>
        /// <returns></returns>
        public int ComfirmExec()
        {
            if (FS.FrameWork.WinForms.Classes.Function.Msg("是否确定要保存？", 422) == DialogResult.No)
            {
                return -1;
            }
            this.btnSave.Enabled = false;
            FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存数据，请稍候...");
            Application.DoEvents();
            dt = this.orderManagement.GetDateTimeFromSysDateTime();

            FS.HISFC.Models.Order.ExecOrder order = null;
            string inpatientNo = "";
            List<FS.HISFC.Models.Order.ExecOrder> alOrders = null;
            FS.HISFC.Models.RADT.PatientInfo patient = null;

            //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB} 欠费判断
            FS.HISFC.Models.Base.MessType messType = FS.HISFC.Models.Base.MessType.M;
            switch (欠费操作)
            {
                case EnumLackFee.不判断欠费:
                    messType = FS.HISFC.Models.Base.MessType.N;
                    break;
                case EnumLackFee.欠费不允许分解:
                    messType = FS.HISFC.Models.Base.MessType.Y;
                    break;
                case EnumLackFee.欠费提示允不允许分解:
                    messType = FS.HISFC.Models.Base.MessType.M;
                    break;
            }

            orderIntegrate.MessageType = messType;
            //{B2E4E2ED-08CF-41a8-BF68-B9DF7454F9BB}

            string lackFeeInfo = "";
            string errInfo = "";

            ///是否欠费
            bool isLackFee = false;

            #region 药品

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;

                    if (inpatientNo != order.Order.Patient.ID)
                    {
                        if (patient != null) //上一个患者
                        {
                            if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return -1;
                            }

                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, true, !isLackFee, false) == -1)
                            {
                                orderIntegrate.fee.Rollback();
                                this.btnSave.Enabled = true;

                                //FS.FrameWork.Management.PublicTrans.RollBack();;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                #region {C88D3BEB-EA3F-455f-BD5D-0A997699CC2C}
                                //添加提示，某个患者出错以后，可选择是否继续其它病人分解
                                //MessageBox.Show(orderIntegrate.Err);
                                //return -1;
                                if (this.isSaveErrContinue)
                                {
                                    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                                        string.Format("{0}.是否继续执行分解其他患者的操作。", orderIntegrate.Err)), "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return -1;
                                    }
                                    this.btnSave.Enabled = true;
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存数据，请稍候...");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    MessageBox.Show(orderIntegrate.Err);
                                    return -1;
                                }
                                #endregion
                            }
                            //}{B3173852-136F-4c4b-9FAC-E15EB879C619}代码在这里弄个}不知道为什么？这么写要人命啊
                            else
                            {
                                //orderIntegrate.fee.Commit();
                                FS.FrameWork.Management.PublicTrans.Commit();
                                //FS.FrameWork.Management.Transaction 
                                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                                //t.BeginTransaction();
                                //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            }
                        }//{B3173852-136F-4c4b-9FAC-E15EB879C619}}上面的}应该在这里啊
                        inpatientNo = order.Order.Patient.ID;
                        //patient = radtIntegrate.GetPatientInfomation(inpatientNo);
                        if (Classes.Function.CheckPatientState(inpatientNo, ref patient, ref errInfo) == -1)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(errInfo);
                            return -1;
                        }

                        isLackFee = false;
                        //欠费提示
                        if (feeManagement.IsPatientLackFee(patient))
                        {
                            if (!lackFeeInfo.Contains(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patient.Name))
                            {
                                lackFeeInfo += "\r\n" + patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patient.Name;
                            }
                            if (!lackFeeDealModel)
                            {
                                isLackFee = true;
                            }
                        }

                        alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();
                    }
                    alOrders.Add(order);

                }
            }
            if (patient != null) //上一个患者
            {
                if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, true, !isLackFee, false) == -1)
                {
                    orderIntegrate.fee.Rollback();
                    this.btnSave.Enabled = true;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(orderIntegrate.Err);
                    return -1;
                }
                else
                {
                    // orderIntegrate.fee.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //FS.FrameWork.Management.Transaction 
                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();
                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                }
            }

            #endregion

            #region 非药品

            alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();
            patient = null;
            inpatientNo = "";
            for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].RowCount; i++)
            {
                if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Text.ToUpper() == "TRUE")
                {
                    order = this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (inpatientNo != order.Order.Patient.ID)
                    {
                        if (patient != null) //上一个患者
                        {
                            if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                            {
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                return -1;
                            }


                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, false, !isLackFee, false) == -1)
                            {
                                orderIntegrate.fee.Rollback();
                                this.btnSave.Enabled = true;
                                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                                //FS.FrameWork.Management.PublicTrans.RollBack();;
                                #region {C88D3BEB-EA3F-455f-BD5D-0A997699CC2C}
                                //添加提示，某个患者出错以后，可选择是否继续其它病人分解
                                //MessageBox.Show(orderIntegrate.Err);
                                //return -1;
                                if (this.isSaveErrContinue)
                                {
                                    if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(
                                        string.Format("{0}.是否继续执行分解其他患者的操作。", orderIntegrate.Err)), "提示", MessageBoxButtons.YesNo) == DialogResult.No)
                                    {
                                        return -1;
                                    }
                                    this.btnSave.Enabled = false; ;
                                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();

                                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存数据，请稍候...");
                                    Application.DoEvents();
                                }
                                else
                                {
                                    MessageBox.Show(orderIntegrate.Err);
                                    return -1;
                                }
                                #endregion
                            }
                            else
                            {
                                //orderIntegrate.fee.Commit();
                                FS.FrameWork.Management.PublicTrans.Commit();
                                //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                                ////t.BeginTransaction();
                                //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                                //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            }
                        }
                        inpatientNo = order.Order.Patient.ID;
                        //patient = radtIntegrate.GetPatientInfomation(inpatientNo);
                        if (Classes.Function.CheckPatientState(inpatientNo, ref patient, ref errInfo) == -1)
                        {
                            //FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                            MessageBox.Show(errInfo);
                            return -1;
                        }

                        isLackFee = false;

                        //欠费提示
                        if (feeManagement.IsPatientLackFee(patient))
                        {
                            if (!lackFeeInfo.Contains(patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patient.Name))
                            {
                                lackFeeInfo += "\r\n" + patient.PVisit.PatientLocation.Bed.ID.Substring(4) + "床  " + patient.Name;
                            }

                            if (!lackFeeDealModel)
                            {
                                isLackFee = true;
                            }
                        }

                        alOrders = new List<FS.HISFC.Models.Order.ExecOrder>();

                    }
                    alOrders.Add(order);

                }
            }
            if (patient != null) //上一个患者
            {
                if (Classes.Function.CheckMoneyAlert(patient, new ArrayList(alOrders), messType) == -1)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return -1;
                }


                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (orderIntegrate.ComfirmExec(patient, alOrders, NurseStation.ID, dt, false, !isLackFee, false) == -1)
                {
                    orderIntegrate.fee.Rollback();
                    this.btnSave.Enabled = true;
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    //FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(orderIntegrate.Err);
                    return -1;
                }
                else
                {
                    //orderIntegrate.fee.Commit();
                    FS.FrameWork.Management.PublicTrans.Commit();
                    //FS.FrameWork.Management.Transaction 
                    //FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                    //t.BeginTransaction();
                    //orderIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                }
            }

            #endregion

            //orderIntegrate.fee.Commit();
            //FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.btnSave.Enabled = true;
            bOnQuery = false;
            this.RefreshQuery();

            string ExecTip = "还有未分解保存项目：\r\n\r\n";
            bool show = false;
            if (fpOrderExecBrowser1.GetFpRowCount(0) > 0)
            {
                ExecTip += "\r\n\r\n" + tabPage1.Text;
                show = true;
            }
            if (fpOrderExecBrowser2.GetFpRowCount(0) > 0)
            {
                ExecTip += "\r\n\r\n" + tabPage2.Text;
                show = true;
            }
            if (show)
            {
                Classes.Function.ShowBalloonTip(3, "注意", ExecTip + "\r\n", ToolTipIcon.Warning);
            }

            if (!string.IsNullOrEmpty(lackFeeInfo))
            {
                if (messType == FS.HISFC.Models.Base.MessType.N && !lackFeeDealModel)
                {
                    MessageBox.Show("以下患者存在欠费情况！\r\n" + lackFeeInfo, "欠费提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {                    
                    Classes.Function.ShowBalloonTip(10, "欠费提示", "以下患者存在欠费情况！\r\n" + lackFeeInfo, System.Windows.Forms.ToolTipIcon.Info);
                }
            }

            return 0;
        }

        /// <summary>
        /// 选择过滤项目
        /// {ED1068B5-53FD-4bf4-A270-49AE1A70D225}
        /// </summary>
        private void CheckFilteredData()
        {
            for (int i = 0; i < this.CurrentBrowser.fpSpread.Sheets[0].Rows.Count; i++)
            {
                if (this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].BackColor == Color.LightSkyBlue)
                {
                    FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder();
                    order = this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (order.Order.Combo.ID != "" && order.Order.Combo.ID != "0")
                    {
                        //这里比较慢，如果同组可以分开，此处不要
                        for (int j = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; j >= 0; j--)
                        {
                            FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[j].Tag;
                            if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                            {
                                this.CurrentBrowser.fpSpread.Sheets[0].Cells[j, 1].Value = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 整合组合过滤函数
        /// {ED1068B5-53FD-4bf4-A270-49AE1A70D225}
        /// </summary>
        /// <param name="isMatchAll"></param>
        /// <param name="prmsDate">执行开始、结束时间</param>
        /// <param name="prmsStr">名称、药房</param>
        protected void SetFilteredFlag(bool isMatchAll, DateTime[] prmsDate, string[] prmsStr)
        {
            bool isHaveFilter = true;
            bool isAllOrderNames = (prmsStr[0] == "全部");
            bool isAllDeptNames = (prmsStr[1] == "全部");
            bool isAllTime = (prmsDate[0].ToString() == prmsDate[1].ToString());
            if (isAllOrderNames && isAllDeptNames && isAllTime)
            {
                isHaveFilter = false;
            }

            FS.HISFC.Models.Order.ExecOrder order = null;
            //初始化显示，药品
            if (this.TabControl1.SelectedIndex == 0)
            {
                bool b = false;
                //恢复原来的颜色
                //间隔颜色显示
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                if (isHaveFilter)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                        DateTime splitTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 9].Text);
                        if (isMatchAll)
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text == prmsStr[0])
                             && (isAllDeptNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 11].Text == prmsStr[1])
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                        else
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text.Contains(prmsStr[0]))
                             && (isAllDeptNames || this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 11].Text.Contains(prmsStr[1]))
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                    }
                }
            }
            //非药品
            else
            {
                //恢复原来的颜色
                bool b = false;
                //恢复原来的颜色
                //间隔颜色显示
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                if (isHaveFilter)
                {
                    for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                    {
                        DateTime splitTime = FS.FrameWork.Function.NConvert.ToDateTime(this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 9].Text);
                        if (isMatchAll)
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text == prmsStr[0])
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                        else
                        {
                            if ((isAllOrderNames || this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text.Contains(prmsStr[0]))
                             && (isAllTime || (splitTime >= prmsDate[0] && splitTime <= prmsDate[1])))
                            {
                                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region 事件
        //不分解医嘱
        private void fpSpread_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            order = this.CurrentBrowser.CurrentExecOrder;

            if (order == null) return;
            if (MessageBox.Show("确认不分解" + order.DateUse.ToString() + "的医嘱[" + order.Order.Item.Name + "] ?", "提示", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            //作废执行档医嘱为不审核
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(this.orderManagement.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.orderManagement.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj = this.orderManagement.Operator;
            ArrayList alDel = new ArrayList();
            if (order.Order.Combo.ID == "" || order.Order.Combo.ID == "0")
            {
                if (this.orderManagement.DcExecImmediate((FS.HISFC.Models.Order.Order)order.Order, obj) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack(); ;
                    MessageBox.Show(this.orderManagement.Err);
                    return;
                }
                this.CurrentBrowser.fpSpread.Sheets[0].Rows.Remove(this.fpOrderExecBrowser1.fpSpread.Sheets[0].ActiveRowIndex, 1);
            }
            else //组合医嘱，作废组合号相同及使用时间相同的医嘱
            {
                for (int i = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
                {
                    FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag;
                    if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                    {
                        if (this.orderManagement.DcExecImmediate(objorder, obj) <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack(); ;
                            MessageBox.Show(this.orderManagement.Err);
                            return;
                        }
                        //alDel.Add(i);
                        this.CurrentBrowser.fpSpread.Sheets[0].Rows.Remove(i, 1);
                    }
                }
            }


            FS.FrameWork.Management.PublicTrans.Commit();
            this.tabPage1.Text = "【" + this.fpOrderExecBrowser1.GetFpRowCount(0).ToString() + "条】";
            this.tabPage2.Text = "【" + this.fpOrderExecBrowser2.GetFpRowCount(0).ToString() + "条】";

        }
        /// <summary>
        /// 返回当前的FpSpread页
        /// </summary>
        protected fpOrderExecBrowser CurrentBrowser
        {
            get
            {
                if (this.TabControl1.SelectedIndex == 0)
                {
                    return this.fpOrderExecBrowser1;
                }
                else
                {
                    return this.fpOrderExecBrowser2;
                }
            }
        }


        /// <summary>
        /// 重新计算按钮事件 已经不再使用 默认为1天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCaculate_Click(object sender, System.EventArgs e)
        {
            if (MessageBox.Show("确定要重新计算并分解医嘱吗?\n计算需要一段时间！", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.intDyas = (int)(this.txtDays.Value);
                RefreshExec();
            }
        }

        /// <summary>
        /// fpSpread_ButtonClicked事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (this.CurrentBrowser.fpSpread.Sheets[0].RowCount <= 0)
            {
                return;
            }
            if (e.Column == 1)
            {
                string checkValue = this.CurrentBrowser.fpSpread.Sheets[0].Cells[e.Row, e.Column].Text;

                FS.HISFC.Models.Order.ExecOrder order = new FS.HISFC.Models.Order.ExecOrder();
                order = this.CurrentBrowser.CurrentExecOrder;
                if (order.Order.Combo.ID != "" && order.Order.Combo.ID != "0")
                {
                    for (int i = this.CurrentBrowser.fpSpread.Sheets[0].RowCount - 1; i >= 0; i--)
                    {
                        FS.HISFC.Models.Order.ExecOrder objorder = (FS.HISFC.Models.Order.ExecOrder)this.CurrentBrowser.fpSpread.Sheets[0].Rows[i].Tag;
                        if (objorder.Order.Combo.ID == order.Order.Combo.ID && objorder.DateUse == order.DateUse)
                        {
                            this.CurrentBrowser.fpSpread.Sheets[0].Cells[i, 1].Text = checkValue;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// 输入查询的名称,查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox2_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //回车结束
            if (e.KeyCode != Keys.Enter)
                return;
            string name = this.txtName.Text.Trim();
            if (name == "") return;

            this.SetDrugFlag(name, false);
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            bool b = false;
            if (this.chkAll.Checked)
            { //全选
                b = true;
            }
            else
            {//取消
                b = false;
            }

            //经常会漏，默认全部选中

            for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
                tab0AllSelect = b;
            }

            for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
            {
                this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Value = b;
                tab1AllSelect = b;
            }
            /*
            if (this.TabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser1.ColumnIndexSelection].Value = b;
                    tab0AllSelect = b;
                }
            }
            else
            {
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, this.fpOrderExecBrowser2.ColumnIndexSelection].Value = b;
                    tab1AllSelect = b;
                }
            }
             * */
        }
        /// <summary>
        /// 重新计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbtnCalculate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(FS.FrameWork.Management.Language.Msg(string.Format("是否分解{0}天的医嘱信息！", this.txtDays.Value.ToString())),
                "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //{5617A654-9738-4db4-A006-5A80B44F0841} 重新计算时需要对患者列表赋值
                this.alPatients = this.GetSelectedTreeNodes();

                intDyas = (int)this.txtDays.Value;
                this.RefreshExec();
            }
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TabControl1.SelectedIndex == 0)
            {
                this.chkAll.Checked = tab0AllSelect;
            }
            else
            {
                this.chkAll.Checked = tab1AllSelect;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ComfirmExec();
            this.btnSave.Enabled = true;

        }

        private void txtDays_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }


        private void txtDrugDeptName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            CheckFilteredData();
        }

        private void dtpBeginTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }

        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            DateTime[] prmsDate = new DateTime[] { this.dtpBeginTime.Value, this.dtpEndTime.Value };
            string[] prmsStr = new string[] { this.txtName.Text, this.txtDrugDeptName.Text };
            SetFilteredFlag(false, prmsDate, prmsStr);
        }
        #endregion

        #region 方法

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            TreeView tv = sender as TreeView;
            if (tv != null && this.tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return null;
        }
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            if (tv != null && tv.CheckBoxes == false)
                tv.CheckBoxes = true;
            return base.OnSetValue(neuObject, e);
        }

        protected override int OnSetValues(ArrayList alValues, object e)
        {
            //{5617A654-9738-4db4-A006-5A80B44F0841} 查询时也需要对天数赋值
            intDyas = (int)this.txtDays.Value;

            this.alPatients = alValues;
            this.RefreshExec();
            return 0;
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return ComfirmExec();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Print(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.PrintPreview(this.TabControl1);
            return 0;
        }

        /// <summary>
        /// 设置打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int SetPrint(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
            p.ShowPageSetup();
            p.PrintPreview(this.TabControl1);
            return 0;
        }
        #endregion

        /// <summary>
        /// 对制定医嘱以特殊颜色显示
        /// </summary>
        /// <param name="filterStr">满足匹配的名称</param>
        /// <param name="isMatchingAll">是否需全部匹配</param>
        protected void SetDrugFlag(string filterStr, bool isMatchingAll)
        {
            FS.HISFC.Models.Order.ExecOrder order = null;
            //初始化显示，药品
            if (this.TabControl1.SelectedIndex == 0)
            {
                bool b = false;
                //恢复原来的颜色
                //间隔颜色显示
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                for (int i = 0; i < this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    order = this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].Tag as FS.HISFC.Models.Order.ExecOrder;
                    if (isMatchingAll)
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text == filterStr)
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        if (this.fpOrderExecBrowser1.fpSpread.Sheets[0].Cells[i, 2].Text.IndexOf(filterStr) != -1)
                        {
                            this.fpOrderExecBrowser1.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                }
            }
            //非药品
            else
            {
                //恢复原来的颜色
                bool b = false;
                //恢复原来的颜色
                //间隔颜色显示
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (b)
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.Linen;
                    }
                    else
                    {
                        this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.White;
                    }
                    b = !b;
                }
                for (int i = 0; i < this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows.Count; i++)
                {
                    if (isMatchingAll)
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text == filterStr)
                        {
                            this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                    else
                    {
                        if (this.fpOrderExecBrowser2.fpSpread.Sheets[0].Cells[i, 2].Text.IndexOf(filterStr) != -1)
                        {
                            this.fpOrderExecBrowser2.fpSpread.Sheets[0].Rows[i].BackColor = Color.LightSkyBlue;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum EnumLackFee
    {
        不判断欠费,
        欠费不允许分解,
        欠费提示允不允许分解
    }
}

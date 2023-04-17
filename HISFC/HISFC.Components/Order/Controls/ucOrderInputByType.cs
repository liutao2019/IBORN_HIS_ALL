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
    /// 项目选择变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="changedField"></param>
    public delegate void ItemSelectedDelegate(FS.HISFC.Models.Order.Inpatient.Order sender, string changedField);

    /// <summary>
    /// 计算总量后反馈总量显示
    /// </summary>
    /// <param name="inOrder"></param>
    public delegate void SetQtyValue(FS.HISFC.Models.Order.Inpatient.Order inOrder);

    /// <summary>
    /// 医嘱输入控件
    /// </summary>
    public partial class ucOrderInputByType : UserControl
    {
        public ucOrderInputByType()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 项目变化选择及变化事件
        /// </summary>
        public event ItemSelectedDelegate ItemSelected;
        /// <summary>
        /// 离开事件
        /// </summary>
        public new event System.EventHandler Leave;
        protected FS.HISFC.Models.Order.Inpatient.Order myorder = null;
        protected bool dirty;


        /// <summary>
        /// 设置总量
        /// </summary>
        public event SetQtyValue SetQtyValue;

        /// <summary>
        /// 非药品是否显示频次
        /// </summary>
        protected bool isUndrugShowFrequency = true;

        public bool IsNew = true;

        /// <summary>
        /// 总量是否人为修改过
        /// </summary>
        private bool isQtyChanged = false;

        /// <summary>
        /// 总量是否人为修改过
        /// </summary>
        public bool IsQtyChanged
        {
            get
            {
                return isQtyChanged;
            }
            set
            {
                isQtyChanged = value;
                //为了保证草药正确此处屏蔽 
                //isQtyChanged = false;
            }
        }

        //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
        Dictionary<string, FS.FrameWork.Models.NeuObject> dictDept = new Dictionary<string, FS.FrameWork.Models.NeuObject>();

        /// <summary>
        /// 可以输入第二单位的项目
        /// </summary>
        private Hashtable hsSecondUnitItem = new Hashtable();

        /// <summary>
        /// 允许的首日量最大值
        /// </summary>
        private decimal maxFirstOrderDays = 0;

        #endregion

        #region 属性
        
        /// <summary>
        /// 当前Order
        /// </summary>
        public FS.HISFC.Models.Order.Inpatient.Order Order
        {
            get
            {
                this.GetOrderInfo();
                return this.myorder;
            }
            set
            {
                if (value == null) return;
                this.myorder = value;
                this.ShowOrder();
            }
        }
  
        /// <summary>
        /// 是否非药品显示频次
        /// </summary>
        public bool IsUndrugShowFrequency
        {
            get
            {
                return isUndrugShowFrequency;
            }
            set
            {
                isUndrugShowFrequency = value;
            }
        }
      
        #endregion

        #region 函数
        protected void SetPanelVisible(int i)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            switch (i)
            {
                case 1:
                    this.panel1.Visible = true;
                    this.panelFrequency.BringToFront();
                    this.panelFrequency.Visible = true;
                    break;
                case 2:
                    this.panel2.Visible = true;
                    this.panelFrequency.Visible = false;
                    break;
                case 3:
                    this.panel3.Visible = true;
                    this.panelFrequency.BringToFront();
                    this.panelFrequency.Visible = true;
                    break;
            }
        }
        protected int GetVisiblePanel()
        {
            if (this.panel1.Visible) return 1;
            if (this.panel2.Visible) return 2;
            if (this.panel3.Visible) return 3;
            return 0;
        }
        private ComboBox MemoComboBox
        {
            get
            {
                switch (this.GetVisiblePanel())
                {
                    case 1:
                        return this.cmbMemo1;
                    case 2:
                        return this.cmbMemo2;
                    case 3:
                        return this.cmbMemo3;
                    default:
                        return null;
                }
            }
        }
        private FS.FrameWork.WinForms.Controls.NeuComboBox UsageComboBox
        {
            get
            {
                switch (this.GetVisiblePanel())
                {
                    case 1:
                        return this.cmbUsage1;
                    case 2:
                        return this.cmbUsage2;
                    case 3:
                        return this.cmbUsage3;
                    default:
                        return null;
                }
            }
        }

        #region 默认执行科室接口

        /// <summary>
        /// 设置执行科室列表
        /// </summary>
        /// <param name="item"></param>
        /// <param name="execDeptCode"></param>
        private void SetExecDept(FS.HISFC.Models.Order.Order order, string execDeptCode, bool isNew)
        {
            string execDept = "";
            ArrayList alExecDept = null;

            Components.Order.Classes.Function.SetExecDept(false, order.ReciptDept.ID, order.Item.ID, order.ExeDept.ID, ref execDept, ref alExecDept);
            if (alExecDept == null)
            {
                alExecDept = SOC.HISFC.BizProcess.Cache.Common.GetValidDept();
            }

            cmbExeDept.AddItems(alExecDept);

            if (isNew)
            {
                cmbExeDept.Tag = execDept;
            }
            else
            {
                cmbExeDept.Tag = execDeptCode;
            }
            //    try
            //    {
            //        cmbExeDept.AddItems(Components.Order.Classes.Function.GetExecDepts(((FS.HISFC.Models.Base.Employee)CacheManager.ConManager.Operator).Dept, order));
            //    }
            //    catch
            //    {
            //        cmbExeDept.AddItems(SOC.HISFC.BizProcess.Cache.Common.GetValidDept());
            //    }

            //    if (isNew)
            //    {
            //        if (order.Item.ID == "999")
            //        {
            //            execDeptCode = "";
            //        }
            //        else
            //        {
            //            execDeptCode = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID).ExecDept;
            //        }
            //    }
            //    string[] execDept = execDeptCode.Split('|');

            //    for (int k = 0; k < execDept.Length; k++)
            //    {
            //        if (!string.IsNullOrEmpty(execDept[k]))
            //        {
            //            execDeptCode = execDept[k];

            //            if (SOC.HISFC.BizProcess.Cache.Common.GetDept(order.ReciptDept.ID).DeptType.ID.ToString() ==
            //                   SOC.HISFC.BizProcess.Cache.Common.GetDept(execDept[k]).DeptType.ID.ToString())
            //            {
            //                execDeptCode = execDept[k];
            //                break;
            //            }
            //        }
            //    }

            //    cmbExeDept.Tag = execDeptCode;

            //    if (string.IsNullOrEmpty(cmbExeDept.Text)
            //        || cmbExeDept.SelectedIndex == -1)
            //    {
            //        if (cmbExeDept.alItems.Count > 0)
            //        {
            //            try
            //            {
            //                bool isRecipt = false;

            //                foreach (FS.FrameWork.Models.NeuObject obj in cmbExeDept.alItems)
            //                {
            //                    if (obj.ID == execDeptCode)
            //                    {
            //                        cmbExeDept.Tag = obj.ID;
            //                        isRecipt = false;
            //                        break;
            //                    }
            //                    if (obj.ID == order.ReciptDept.ID)
            //                    {
            //                        isRecipt = true;
            //                    }
            //                }

            //                if (isRecipt)
            //                {
            //                    cmbExeDept.Tag = order.ReciptDept.ID;
            //                }
            //                else
            //                {
            //                    string ReciptDeptTypeID = SOC.HISFC.BizProcess.Cache.Common.GetDept(order.ReciptDept.ID).DeptType.ID.ToString();
            //                    for (int i = 0; i < cmbExeDept.alItems.Count; i++)
            //                    {
            //                        if (ReciptDeptTypeID ==
            //                            SOC.HISFC.BizProcess.Cache.Common.GetDept(((FS.FrameWork.Models.NeuObject)cmbExeDept.alItems[i]).ID).DeptType.ID.ToString())
            //                        {
            //                            cmbExeDept.SelectedIndex = i;
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //            }

            //            if (cmbExeDept.SelectedIndex == -1)
            //            {
            //                cmbExeDept.SelectedIndex = 0;
            //            }
            //        }
            //    }

            //    if (cmbExeDept.Tag != null && !string.IsNullOrEmpty(cmbExeDept.Tag.ToString()))
            //    {
            //        order.ExeDept.ID = cmbExeDept.Tag.ToString();
            //    }
        }

        #endregion

        /// <summary>
        /// 设置医嘱
        /// </summary>
        protected void ShowOrder()
        {
            dirty = true;

            this.DeleteEvent();

            try
            {
                //药品
                //if (this.myorder.Item.IsPharmacy)
                if (this.myorder.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    //草药
                    FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                    item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(this.myorder.Item.ID);
                        
                    if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                    {
                        if (this.GetVisiblePanel() != 2)
                        {
                            this.SetPanelVisible(2);
                        }

                        if (this.IsNew)
                        {
                            //一次剂量为零，默认显示基本剂量
                            if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose == 0M)
                            {
                                //add by liuww
                                //this.txtDoseOnce1.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose.ToString();
                                this.txtDoseOnce1.Text = "0";
                            }
                            else
                            {
                                this.txtDoseOnce1.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose.ToString();
                            }

                            this.cmbMiniUnit1.Items.Clear();
                            ArrayList alDoseUnit1 = new ArrayList();
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            //alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit, ""));

                            this.cmbMiniUnit1.AddItems(alDoseUnit1);
                            //this.cmbMiniUnit1.Tag = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit;
                            this.cmbMiniUnit1.Tag = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;

                            this.myorder.DoseOnce = decimal.Parse(this.txtDoseOnce1.Text);
                            this.myorder.DoseOnceDisplay = this.myorder.DoseOnce.ToString(); 
                            this.myorder.DoseUnit = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                           
                            this.myorder.DoseUnitDisplay = this.myorder.DoseUnit;
                        }
                        else
                        {
                            this.txtDoseOnce1.Text = this.myorder.DoseOnceDisplay.ToString();

                            this.cmbMiniUnit1.Items.Clear();
                            ArrayList alDoseUnit1 = new ArrayList();
                            //alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit, ""));
                            //alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, ""));
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            this.cmbMiniUnit1.AddItems(alDoseUnit1);

                            if (string.IsNullOrEmpty(myorder.DoseUnit))
                            {
                                myorder.DoseUnit = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit;
                            }

                            this.cmbMiniUnit1.Tag = string.IsNullOrEmpty(myorder.DoseUnitDisplay) ? this.myorder.DoseUnit : myorder.DoseUnitDisplay;

                            this.txtDoseOnce1.Text = string.IsNullOrEmpty(myorder.DoseOnceDisplay) ? this.myorder.DoseOnce.ToString().TrimEnd('0').TrimEnd('.') : myorder.DoseOnceDisplay;

                            this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            this.cmbMemo2.Text = this.myorder.Memo;
                        }
                    }
                    else//西药，中成药
                    {
                        if (this.GetVisiblePanel() != 1)
                        {
                            this.SetPanelVisible(1);
                        }

                        try
                        {
                            if (this.myorder.HerbalQty < 1)
                            {
                                this.myorder.HerbalQty = 1;
                            }
                            this.txtDays.Value = this.myorder.HerbalQty;

                            if (myorder.OrderType.ID == "CD")
                            {
                                this.txtDays.Enabled = true;
                            }
                            else
                            {
                                this.txtDays.Enabled = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            string s = ex.Message;
                        }

                        if (this.IsNew)
                        {
                            //一次剂量为零，默认显示基本剂量
                            if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose == 0M)
                            {
                                //没有维护一次剂量的，界面不显示
                                //this.txtDoseOnce.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose.ToString(); 
                                this.txtDoseOnce.Text = "0";
                            }
                            else
                            {
                                this.txtDoseOnce.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose.ToString();
                            }

                            this.cmbMiniUnit.Items.Clear();
                            ArrayList alDoseUnit1 = new ArrayList();
                            
                            //if (this.hsSecondUnitItem.Contains(this.myorder.Item.ID))//增加判断
                            //{
                            //    alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, ""));
                            //}
                            //alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit, ""));

                            //alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, ""));
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit,""));
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            // {4D67D981-6763-4ced-814E-430B518304E2}

                            this.cmbMiniUnit.AddItems(alDoseUnit1);
                            if (!string.IsNullOrEmpty(item.OnceDoseUnit))
                            {
                                this.cmbMiniUnit.Tag = item.OnceDoseUnit;
                                myorder.DoseUnitDisplay = item.OnceDoseUnit;
                            }
                            else
                            {
                                //没有维护每次剂量单位
                                this.cmbMiniUnit.Tag = item.DoseUnit;
                                myorder.DoseUnitDisplay = item.DoseUnit;
                            }

                            //{5E64EEF0-FCF1-483e-840B-5A1CE3F7A4DF}
                            if (this.myorder.ID == "999")
                            {
                                this.myorder.DoseUnit = this.cmbMiniUnit.Text;
                            }
                            else
                            {
                                this.myorder.DoseUnit = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                                this.myorder.DoseOnceDisplay = this.txtDoseOnce.Text.ToString();
                                if (this.myorder.DoseUnitDisplay != (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit)
                                {
                                    this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue * (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose;
                                }
                                else
                                {
                                    this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue;//FS.FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce.Text);
                                }
                            }
                        }
                        else
                        {

                            this.txtDoseOnce.Text = this.myorder.DoseOnceDisplay.ToString();

                            this.cmbMiniUnit.Items.Clear();
                            ArrayList alDoseUnit1 = new ArrayList();
                            // {4D67D981-6763-4ced-814E-430B518304E2}
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));

                            this.cmbMiniUnit.AddItems(alDoseUnit1);
                            this.cmbMiniUnit.Tag = string.IsNullOrEmpty(myorder.DoseUnitDisplay) ? ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit : myorder.DoseUnitDisplay;
                        }

                        //可不可以编辑每次用量单位
                        if (this.myorder.Item.ID == "999")
                        {
                            this.cmbMiniUnit.DropDownStyle = ComboBoxStyle.DropDown;
                        }
                        else
                        {
                            this.cmbMiniUnit.DropDownStyle = ComboBoxStyle.DropDownList;
                        }
                        this.cmbMemo1.Text = this.myorder.Memo;
                        this.chkDrugEmerce.Checked = this.myorder.IsEmergency;
                        this.neuTextExpressBox1.Text = this.myorder.Dripspreed;
                    }
                }
                else//非药品-描述医嘱
                {
                    if (this.GetVisiblePanel() != 3)
                        this.SetPanelVisible(3);

                    if (myorder.OrderType.IsDecompose)
                    {
                        this.panelFrequency.Visible = this.isUndrugShowFrequency;//长期医嘱,非药品医嘱显不显示频次
                    }
                    else
                    {
                        this.panelFrequency.Visible = false;//临时医嘱，就是不用显示非药品频次

                        cmbCheckPartRecord.Visible = false;
                        txtSample.Visible = false;
                        neuLabel9.Visible = false;

                        if (this.myorder.Item.SysClass.ID.ToString() == "UL")
                        {
                            //this.txtSample.ClearItems();
                            //this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperSample.ArrayObject);
                            neuLabel9.Visible = true;
                            this.neuLabel9.Text = "样本:";
                            this.cmbCheckPartRecord.Visible = false;
                            this.txtSample.Visible = true;
                            this.txtSample.Text = this.myorder.Sample.Name;//{8023164F-FA4B-4701-AD80-867EEFC2F029}
                        }
                        else
                        {
                            //this.txtSample.ClearItems();
                            //this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperCheckPart.ArrayObject);
                            neuLabel9.Visible = true;
                            this.neuLabel9.Text = "部位:";
                            this.cmbCheckPartRecord.Visible = true;
                            this.txtSample.Visible = false;
                            this.cmbCheckPartRecord.Text = this.myorder.CheckPartRecord;
                        }
                    }

                    //执行科室
                    //if (this.IsNew)
                    //{
                    //    FS.HISFC.Models.Fee.Item.Undrug undrug = myorder.Item as FS.HISFC.Models.Fee.Item.Undrug;

                    //    //string execDeptCode = "";

                    //    ////执行科室赋值 开立项目同时赋值执行科室
                    //    //if (undrug.ExecDept != null && undrug.ExecDept != "")
                    //    //{
                    //    //    execDeptCode = undrug.ExecDept;
                    //    //}
                    //    SetExecDept(myorder, myorder, IsNew);
                    //}
                    //else
                    //{
                    //    //this.cmbExeDept.Tag = this.myorder.ExeDept.ID;
                    //    SetExecDept(myorder, myorder.ExeDept.ID, IsNew);
                    //    //if (cmbExeDept.Tag == null || string.IsNullOrEmpty(cmbExeDept.Tag.ToString()))
                    //    //{
                    //    //    cmbExeDept.Tag = myorder.ReciptDept.ID;
                    //    //}
                    //}
                    SetExecDept(myorder, myorder.ExeDept.ID, IsNew);
                    if (cmbExeDept.Tag != null)
                    {
                        myorder.ExeDept.ID = cmbExeDept.Tag.ToString();
                    }
                    this.cmbMemo3.Text = this.myorder.Memo;
                    this.chkEmerce.Checked = this.myorder.IsEmergency;
                }

            }
            catch { };

            if (this.myorder.Frequency.ID != "")
            {
                this.cmbFrequency.Tag = this.myorder.Frequency.ID;
                this.myorder.Frequency.Name = cmbFrequency.SelectedItem.Name;
                if (cmbFrequency.SelectedItem != null)
                {
                    if (this.myorder.Frequency.Time == "25:00")
                    {
                        this.myorder.Frequency.Time = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Time;
                    }
                }
                this.lnkTime.Text = this.myorder.Frequency.Time;
            }
            this.cmbUsage1.Tag = this.myorder.Usage.ID;
            this.cmbUsage2.Tag = this.myorder.Usage.ID;
            this.cmbUsage3.Tag = this.myorder.Usage.ID;

            #region 首日量

            SetDayOnceUp();
            #endregion

            this.SetDaysVisible();

            this.AddEvent();
            dirty = false;
            if (IsNew)
            {
                this.CalculateTotal();
            }
        }

        /// <summary>
        /// 设置首日量
        /// </summary>
        private void SetDayOnceUp()
        {
            if (this.myorder != null 
                && this.myorder.OrderType.IsDecompose == true 
                && this.myorder.Frequency.Times.Length > 0)
            {
                this.txtFirstDay.Visible = true;
                this.lblFirstDay.Visible = true;

                if (this.myorder.FirstUseNum == null || this.myorder.FirstUseNum == "")
                {
                    int count = Classes.Function.GetFirstOrderDays(myorder, CacheManager.ConManager.GetDateTimeFromSysDateTime());
                    if (count < 0)
                    {
                        this.txtFirstDay.Text = "";
                    }
                    else
                    {
                        this.txtFirstDay.Text = count.ToString();
                        this.myorder.FirstUseNum = txtFirstDay.Text;
                    }
                }
                else
                {
                    this.txtFirstDay.Text = myorder.FirstUseNum;
                }

                maxFirstOrderDays = myorder.Frequency.Times.Length;
            }
            else
            {
                this.txtFirstDay.Visible = false;
                this.lblFirstDay.Visible = false;
            }
        }

        /// <summary>
        /// 从界面获取当前医嘱信息
        /// </summary>
        protected virtual void GetOrderInfo()
        {
            if (this.dirty)
            {
                return;
            }

            if (this.myorder == null)
            {
                return;
            }

            this.DeleteEvent();

            if (UsageComboBox == null || this.UsageComboBox.SelectedItem == null)
            {
                this.myorder.Usage.ID = "";
                this.myorder.Usage.Name = "";
            }
            else
            {
                this.myorder.Usage.ID = this.UsageComboBox.SelectedItem.ID;
                this.myorder.Usage.Name = this.UsageComboBox.SelectedItem.Name;
            }
            if (this.cmbFrequency.SelectedItem == null)
            {
                this.myorder.Frequency.ID = "";
                this.myorder.Frequency.Name = "";
                this.myorder.Frequency.Time = "";
            }
            else
            {
                this.myorder.Frequency.ID = this.cmbFrequency.SelectedItem.ID;
                this.myorder.Frequency.Name = this.cmbFrequency.SelectedItem.Name;
                this.myorder.Frequency.Time = this.lnkTime.Text;
            }

            switch (this.GetVisiblePanel())
            {
                case 1://西
                    try
                    {
                        this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue;//decimal.Parse(this.txtDoseOnce.Text);

                        if (this.txtDoseOnce.InputText.Length > 8)
                        {
                            this.myorder.DoseOnceDisplay = this.txtDoseOnce.InputText.Trim().Substring(0, 8);
                        }
                        else
                        {
                            this.myorder.DoseOnceDisplay = this.txtDoseOnce.InputText.Trim();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("每次用量输入不正确!", "提示");
                        return;
                    }
                    //{5E64EEF0-FCF1-483e-840B-5A1CE3F7A4DF}
                    //((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = (this.cmbMiniUnit.Text);
                    this.myorder.DoseUnitDisplay = this.cmbMiniUnit.Text;

                    //{E57F256E-1722-4b36-809A-D46BD7A9AB55}
                    //取每次剂量的时候需要判断最小单位与计量单位是否相等
                    if (!string.IsNullOrEmpty(this.cmbMiniUnit.Text) && this.myorder.DoseUnitDisplay != this.myorder.DoseUnit)
                    {
                        this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue * (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose;
                    }

                    this.myorder.Memo = this.cmbMemo1.Text;
                    this.myorder.IsEmergency = this.chkDrugEmerce.Checked;

                    //出院带药天数
                    if (!this.myorder.OrderType.IsDecompose && this.myorder.OrderType.ID == "CD")
                    {
                        this.myorder.HerbalQty = this.txtDays.Value;
                    }

                    this.myorder.Dripspreed = this.neuTextExpressBox1.Text;

                    break;
                case 2://草

                    if (this.txtDoseOnce1.InputText.Length > 8)
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnce1.InputText.Trim().Substring(0, 8);
                    }
                    else
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnce1.InputText.Trim();
                    }

                    this.myorder.HerbalQty = decimal.Parse(this.txtFu.Text);
                    this.myorder.Memo = this.cmbMemo2.Text;
                    break;
                case 3://非
                    if (this.cmbExeDept.Tag != null)
                    {
                        this.myorder.ExeDept.ID = this.cmbExeDept.Tag.ToString();
                        this.myorder.ExeDept.Name = this.cmbExeDept.Text;
                    }
                    this.myorder.Memo = this.cmbMemo3.Text;
                    this.myorder.IsEmergency = this.chkEmerce.Checked;

                    this.myorder.Sample.Name = this.txtSample.Text;//{8023164F-FA4B-4701-AD80-867EEFC2F029}
                    if (this.txtSample.Tag != null) 
                        this.myorder.Sample.ID = this.txtSample.Tag.ToString();
                    if (this.cmbCheckPartRecord.Tag != null)
                    {
                        myorder.CheckPartRecord = cmbCheckPartRecord.Text;
                    }
                    break;
                default:
                    break;
            }

            this.AddEvent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (FS.FrameWork.Management.Connection.Operator.ID == "") 
                return;
            if (DesignMode == false)
            {
                ArrayList al1 = new ArrayList();
                try
                {
                    al1 = CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.ORDERMEMO);
                }
                catch
                {
                    return;
                }
                this.cmbMemo1.AddItems(al1);
                this.cmbMemo2.AddItems(al1);
                this.cmbMemo3.AddItems(al1);
            }
        }

        /// <summary>
        /// 文本框跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                this.GetOrderInfo();
                if (((Control)sender).Name.Length > 7
                    && ((Control)sender).Name.Substring(0, 7) == "cmbMemo")
                {
                    if (this.Leave != null)
                    {
                        this.Leave(sender, null);
                    }
                }
                else if (sender == this.cmbExeDept)
                {
                    if (myorder != null)
                    {
                        if (myorder.Item.SysClass.ID.ToString() == "UC")
                        {
                            cmbCheckPartRecord.Focus();
                        }
                        else if (myorder.Item.SysClass.ID.ToString() == "UL")
                        {
                            txtSample.Focus();
                        }
                        else
                        {
                            this.cmbFrequency.Focus();
                        }
                    }

                    //if (this.isUndrugShowFrequency == false)
                    //{
                    //    if (this.Leave != null)
                    //    {
                    //        this.Leave(sender, null);
                    //    }
                    //}
                }
                else if (sender == cmbCheckPartRecord)
                {
                    cmbMemo3.Focus();
                }
                else if (sender == txtSample)
                {
                    cmbMemo3.Focus();
                }
                else if (sender == this.cmbMiniUnit)
                {
                    this.cmbFrequency.Focus();
                }
                else if (sender == this.txtFu)
                {
                    this.cmbUsage2.Focus();
                }
                else if (sender == this.cmbFrequency)
                {
                    if (this.txtFirstDay.Visible)
                    {
                        this.txtFirstDay.Focus();
                        this.txtFirstDay.Select(0, this.txtFirstDay.Text.Length);
                    }
                    else if (this.txtDays.Visible)
                    {
                        if (txtDays.Enabled)
                        {
                            this.txtDays.Focus();
                            this.txtDays.Select(0, this.txtDays.Value.ToString().Length);
                        }
                        else
                        {
                            switch (this.GetVisiblePanel())
                            {
                                case 1:
                                    this.cmbUsage1.Focus();
                                    break;
                                case 2:
                                    this.cmbUsage2.Focus();
                                    break;
                                default:
                                    if (this.Leave != null) this.Leave(sender, null);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (this.GetVisiblePanel())
                        {
                            case 1:
                                this.cmbUsage1.Focus();
                                break;
                            case 2:
                                this.cmbUsage2.Focus();
                                break;
                            default:
                                if (this.Leave != null) this.Leave(sender, null);
                                break;
                        }
                    }
                }
                else if (sender == this.txtDays)
                {
                    switch (this.GetVisiblePanel())
                    {
                        case 1:
                            this.cmbUsage1.Focus();
                            break;
                        case 2:
                            this.cmbUsage2.Focus();
                            break;
                        default:
                            if (this.Leave != null) this.Leave(sender, null);
                            break;
                    }
                }
                else if (sender == this.txtFirstDay)
                {
                    switch (this.GetVisiblePanel())
                    {
                        case 1:
                            if (string.IsNullOrEmpty(this.txtFirstDay.Text)
                                || string.IsNullOrEmpty(this.myorder.FirstUseNum))
                            {
                                MessageBox.Show("首日量为空，请重新输入！");
                                this.txtFirstDay.SelectAll();
                                return;
                            }
                            this.cmbUsage1.Focus();
                            break;
                        case 2:
                            this.cmbUsage2.Focus();
                            break;
                        default:
                            this.cmbMemo3.Focus();
                            break;
                            //if (this.Leave != null)
                            //{
                            //    this.Leave(sender, null);
                            //}
                            break;
                    }
                }
                else if (sender == this.txtDoseOnce && this.cmbMiniUnit.DropDownStyle == ComboBoxStyle.DropDown)
                {
                    //{76DF3C65-8215-4327-ACE2-10B307ACBA59}
                    this.cmbMiniUnit.Focus();
                    //this.ComputeDoseOnceMultiple();
                }
                else if (sender == this.txtDoseOnce && this.cmbMiniUnit.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    this.cmbFrequency.Focus();
                    //this.ComputeDoseOnceMultiple();
                }
                else if (sender == this.txtDoseOnce1)
                {
                    this.txtFu.Focus();
                    this.txtFu.SelectAll();
                }
                else if (sender == this.neuTextExpressBox1)
                {
                    this.txtFu.Focus();
                    this.txtFu.SelectAll();
                }
                else if (sender == this.cmbMiniUnit1)
                {
                    this.txtFu.Focus();
                    this.txtFu.SelectAll();
                }
                else
                {
                    System.Windows.Forms.SendKeys.Send("{tab}");
                }
                e.Handled = true;
            }
        }

        private void cmbFrequency_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbFrequency.SelectedIndex < 0)
                return;

            if (this.IsNew) 
                return;
            if (this.myorder == null) 
                return;

            string time = "";
            if (this.myorder.Frequency.ID == this.cmbFrequency.SelectedItem.ID)
            {
                time = this.myorder.Frequency.Time;//获得频次时间点,当然也可以用IsNew来代替
            }

            this.myorder.Frequency = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Clone();
            if (time != "")
            {
                this.myorder.Frequency.Time = time;//赋回时间点
            }
            this.lnkTime.Text = this.myorder.Frequency.Time;

            this.myorder.FirstUseNum = "";
            this.SetDayOnceUp();
            this.myorder.FirstUseNum = this.txtFirstDay.Text;

            this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, ColmSet.P频次);
            }
        }

        void CheckedChanged(object sender, EventArgs e)
        {
            if (this.IsNew) return;
            if (this.myorder == null) return;
            switch(this.GetVisiblePanel())
            {
                case 1:
                    this.myorder.IsEmergency = this.chkDrugEmerce.Checked;
                    break;
                case 2:
                    this.myorder.IsEmergency = false;
                    break;
                default:
                    this.myorder.IsEmergency = this.chkEmerce.Checked;
                    break;
            
             }
            if (this.ItemSelected != null)
            {
                this.ItemSelected(this.myorder, ColmSet.J急);
            }
        }

        /// <summary>
        /// 自动计算总量
        /// </summary>
        private void CalculateTotal()
        {
            if (myorder == null)
            {
                return;
            }
            else if (myorder.Item.ID == "999")
            {
                return;
            }

            //修改过总量后不再计算
            if (this.isQtyChanged)
            {
                return;
            }


            dirty = true;
            //西药天数
            decimal Days = 0;
            Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDays.Value);

            if (!this.txtDays.Visible)
            {
                Days = 1;
            }

            //如果没有输入天数时则不处理
            if (Days <= 0)
            {
                return;
            }

            //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
            if (Classes.Function.ReComputeQty(this.Order) == -1)
            {
                return;
            }
            if (SetQtyValue != null)
            {
                SetQtyValue(Order);
            }
        }

        void Mouse_Leave(object sender, EventArgs e)
        {
            if (this.IsNew) return;
            if (this.dirty) return;
            if (this.myorder == null) return;
            switch (((Control)sender).Name)
            {
                case "txtDoseOnce":

                    //作为显示的剂量内容
                    if (this.myorder.DoseUnitDisplay != (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit)
                    {
                        this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue * (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose;
                    }
                    else
                    {
                        this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue;//FS.FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce.Text);
                    }

                    if (this.txtDoseOnce.InputText.Length > 8)
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnce.InputText.Trim().Substring(0, 8);
                    }
                    else
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnce.InputText.Trim();
                    }

                    this.txtDoseOnce.Text = this.myorder.DoseOnceDisplay;
                    this.txtDoseOnce.Select(this.txtDoseOnce.Text.Length, 0);

                    this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}

                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, ColmSet.M每次用量);
                    }

                    break;

                // {6B70B558-72C9-4DEF-874F-DABD0A9B5198}
                case "neuTextExpressBox1":
                     this.myorder.Dripspreed=this.neuTextExpressBox1.Text;
                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, ColmSet.D滴速);
                    }
                    break;

                case "txtDoseOnce1":
                    //作为显示的剂量内容
                    //if (this.myorder.DoseUnitDisplay != (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit)
                    //{
                    //    this.myorder.DoseOnce = this.txtDoseOnce1.ComputeValue * (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose;
                    //}
                    //else
                    //{
                        this.myorder.DoseOnce = this.txtDoseOnce1.ComputeValue;
                    //}

                    if (this.txtDoseOnce1.InputText.Length > 8)
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnce1.InputText.Trim().Substring(0, 8);
                    }
                    else
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnce1.InputText.Trim();
                    }

                    this.txtDoseOnce1.Text = this.myorder.DoseOnceDisplay;
                    this.txtDoseOnce1.Select(this.txtDoseOnce1.Text.Length, 0);

                    this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}

                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, ColmSet.M每次用量);
                    }
                    break;
                case "txtFrequency":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.P频次);
                    break;
                case "txtFu":

                    this.Order.HerbalQty = decimal.Parse(this.txtFu.Text);
                    this.CalculateTotal();

                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.F付数);
                    break;
                case "cmbMiniUnit":

                    //{5E64EEF0-FCF1-483e-840B-5A1CE3F7A4DF}
                    this.myorder.DoseUnitDisplay = this.cmbMiniUnit.Text;

                    if (this.myorder.ID == "999")
                    {
                        this.myorder.DoseUnit = this.cmbMiniUnit.Text;
                    }
                    else
                    {
                        this.myorder.DoseUnit = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;

                        if (this.myorder.DoseUnitDisplay != (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit)
                        {
                            this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue * (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose;
                        }
                        else
                        {
                            this.myorder.DoseOnce = this.txtDoseOnce.ComputeValue;//FS.FrameWork.Function.NConvert.ToDecimal(this.txtDoseOnce.Text);
                        }
                    }

                    this.CalculateTotal();

                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, ColmSet.D单位);
                    }

                    break;
                case "cmbMiniUnit1":

                    this.myorder.DoseUnitDisplay = this.cmbMiniUnit1.Text;

                    //if (this.myorder.ID == "999")
                    //{
                        this.myorder.DoseUnit = this.cmbMiniUnit1.Text;
                    //}
                    //else
                    //{
                    //    if (string.IsNullOrEmpty(this.myorder.DoseUnit))
                    //    {
                    //        this.myorder.DoseUnit = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit;
                    //    }

                        //if (this.myorder.DoseUnitDisplay != (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).DoseUnit)
                        //{
                        //    this.myorder.DoseOnce = this.txtDoseOnce1.ComputeValue * (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose;
                        //}
                        //else
                        //{
                            this.myorder.DoseOnce = this.txtDoseOnce1.ComputeValue;
                        //}
                    //}

                            this.CalculateTotal();

                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.Order, ColmSet.D单位);
                    }

                    break;
                case "txtSample":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.Y样本类型);
                    break;
                case "cmbCheckPartRecord":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.J检查部位);
                    break;
                case "cmbExeDept":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.Z执行科室);
                    break;
                case "cmbMemo1":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.B备注);
                    break;
                case "cmbMemo2":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.B备注);
                    break;
                case "cmbMemo3":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.B备注);
                    break;
                case "cmbUsage1":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.Y用法);
                    break;
                case "cmbUsage2":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.Y用法);
                    break;
                case "cmbUsage3":
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.Y用法);
                    break;
                case "txtDays":

                    if (Math.Ceiling(txtDays.Value) != txtDays.Value)
                    {
                        Components.Order.Classes.Function.ShowBalloonTip(8, "警告", "天数不允许输入小数！\r\n系统自动转换为整数:" + Math.Ceiling(txtDays.Value).ToString(), ToolTipIcon.Warning);

                        txtDays.Value = Math.Ceiling(txtDays.Value);
                    }

                    this.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDays.Value);
                    this.CalculateTotal();//{59C74550-5948-4321-A029-CB3CA6A822FD}

                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.Z总量);
                    break;
                case  "txtFirstDay":

                    try
                    {
                        if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtFirstDay.Text) > this.maxFirstOrderDays)
                        {
                            this.txtFirstDay.Text = maxFirstOrderDays.ToString();
                        }

                        if (txtFirstDay.Text.TrimEnd('0').TrimEnd('.').Contains("."))
                        {
                            Components.Order.Classes.Function.ShowBalloonTip(8, "警告", "首日量不允许输入小数！\r\n系统将自动转换为整数:" + Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(this.txtFirstDay.Text)).ToString(), ToolTipIcon.Warning);
                        }

                        if (Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(this.txtFirstDay.Text)) < 0)
                        {
                            this.txtFirstDay.Text = "0";
                        }

                        txtFirstDay.Text = Math.Ceiling(FS.FrameWork.Function.NConvert.ToDecimal(this.txtFirstDay.Text)).ToString();
                    }
                    catch
                    {
                        MessageBox.Show("非法数字");
                        this.txtFirstDay.Text = "";
                    }

                    this.myorder.FirstUseNum = this.txtFirstDay.Text;

                    if (this.ItemSelected != null)
                    {
                        this.ItemSelected(this.myorder, ColmSet.S首日量);
                    }
                    break;

                case "txtMultiple"://{76DF3C65-8215-4327-ACE2-10B307ACBA59}
                    //this.ComputeDoseOnce();
                    break;
                default:
                    if (this.ItemSelected != null) this.ItemSelected(this.Order, ColmSet.ALL);
                    break;
            }
          
        }
        #endregion

        #region	"公共函数"
        /// <summary>
        /// 清空
        /// </summary>
        public virtual void Clear()
        {
            this.DeleteEvent();
            this.IsNew = true;
            this.myorder = null;
            this.txtDoseOnce.Text = "0";				//每次用量
            this.neuTextExpressBox1.Text = "";  //滴速
            this.txtDoseOnce1.Text = "0";
            this.cmbMiniUnit.ClearItems();				//每次用量单位
            this.cmbMemo1.Text = "";				//备注
            this.cmbMemo2.Text = "";
            this.cmbMemo3.Text = "";
            this.txtFu.Text = "1";					//付数
            this.cmbExeDept.Text = "";				//执行科室
            this.chkEmerce.Checked = false;			//加急
            this.chkDrugEmerce.Checked = false;		//加急
            this.txtSample.Text = "";
            cmbCheckPartRecord.Text = "";
            this.cmbFrequency.Tag = "";
            this.cmbFrequency.Text = "";
            this.cmbUsage1.Text = "";
            this.cmbUsage1.Tag = "";
            this.cmbUsage2.Text = "";
            this.cmbUsage2.Tag = "";
            this.IsNew = false;
            this.AddEvent();
        }

        /// <summary>
        /// 快速跳转
        /// </summary>
        public void SetShortKey()
        {
            if (this.txtDoseOnce.Focused || this.txtFu.Focused || this.cmbExeDept.Focused||this.neuTextExpressBox1.Focused)
            {
                if (this.Leave != null) 
                    this.Leave(null,null);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="alFrequency"></param>
        /// <param name="alDept"></param>
        public virtual void InitControl(ArrayList alFrequency, ArrayList alDept,ArrayList alUsage)
        {
            try
            {
                if (alDept == null)
                {
                    //alDept = d.GetDepartment();
                    //deptAll = alDept;
                    alDept = SOC.HISFC.BizProcess.Cache.Common.GetValidDept();
                }

                this.cmbExeDept.AddItems(alDept);
                this.cmbExeDept.IsListOnly = true;

                if (alFrequency == null)
                {
                    alFrequency = Classes.Function.HelperFrequency.ArrayObject.Clone() as ArrayList;

                    ArrayList al1 = new ArrayList(Classes.Function.HelperFrequency.ArrayObject);
                }

                this.cmbFrequency.IsShowID = true;
                this.cmbFrequency.AddItems(alFrequency);
                this.cmbFrequency.IsListOnly = true;

                #region 先屏蔽

                Hashtable hsUsageAndTime = new Hashtable();

                ArrayList alItems = CacheManager.GetConList("USAGEDIVTIME");

                foreach (FS.HISFC.Models.Base.Const cnst in Classes.Function.HelperUsage.ArrayObject)
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
                                    cnst.Memo = usage.UserCode;
                                }
                            }
                        }

                        hsUsageAndTime.Add(cnst.ID, cnst);
                    }
                }
                #endregion

                //初始化用法

                //从院注用法表获取有效用法，因为这里存储了用户的自定义编码
                ArrayList alInjectUsage = null;// constantMgr.GetAllList("InjectUsage");
                if (alInjectUsage == null || alInjectUsage.Count == 0)
                {
                    alInjectUsage = FS.HISFC.Components.Order.Classes.Function.HelperUsage.ArrayObject;
                }

                ArrayList aldrugUsage = new ArrayList();

                foreach (FS.HISFC.Models.Base.Const cnst in alInjectUsage)
                {
                    if ((hsUsageAndTime[cnst.ID] as FS.HISFC.Models.Base.Const).Memo != "F")
                        aldrugUsage.Add(cnst);
                }

                this.cmbUsage1.AddItems(aldrugUsage);
                this.cmbUsage2.AddItems(aldrugUsage);

                ArrayList alUndrugUsage = new ArrayList();

                foreach (FS.HISFC.Models.Base.Const cnst in alInjectUsage)
                {
                    if ((hsUsageAndTime[cnst.ID] as FS.HISFC.Models.Base.Const).Memo == "F" || (hsUsageAndTime[cnst.ID] as FS.HISFC.Models.Base.Const).Memo == "A")
                        alUndrugUsage.Add(cnst);
                }

                this.cmbUsage3.AddItems(alUndrugUsage);

                this.cmbUsage1.IsListOnly = true;
                this.cmbUsage2.IsListOnly = true;
                this.cmbUsage3.IsListOnly = true;
                //初始化样本类型
                this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperSample.ArrayObject);
                cmbCheckPartRecord.AddItems(HISFC.Components.Order.Classes.Function.HelperCheckPart.ArrayObject);
                    //{CA82280B-51B6-4462-B63E-43F4ECF456A3}
                ArrayList deptList = CacheManager.FeeIntegrate.QueryDeptList("ALL", "2");
                foreach (FS.FrameWork.Models.NeuObject neuObj in deptList)
                {
                    dictDept.Add(neuObj.Memo + "|" + neuObj.ID, neuObj);
                }

                ArrayList alSecondUnitItems = new ArrayList();
                alSecondUnitItems = CacheManager.GetConList("SecondUnitItem");

                foreach (FS.HISFC.Models.Base.Const item in alSecondUnitItems)
                {
                    hsSecondUnitItem.Add(item.ID, item);
                }

                this.txtDoseOnce.DotNum = 8;
                this.txtDoseOnce1.DotNum = 8;

                this.AddEvent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 批量增加事件
        /// </summary>
        private void AddEvent()
        {
            this.DeleteEvent();

            this.txtDoseOnce.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.neuTextExpressBox1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOnce1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtFirstDay.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDays.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtFu.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage2.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.cmbMiniUnit.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMiniUnit1.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.cmbMemo1.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo2.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo3.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            cmbCheckPartRecord.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            txtSample.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.cmbMemo1.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemo2.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemo3.TextChanged += new EventHandler(Mouse_Leave);

            //对于是药品的嘱托医嘱，可以自行输入每次量单位
            this.cmbMiniUnit.Leave += new EventHandler(Mouse_Leave);
            this.cmbMiniUnit1.Leave += new EventHandler(Mouse_Leave);
            txtFirstDay.Leave += new EventHandler(Mouse_Leave);
            this.txtSample.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            cmbCheckPartRecord.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbExeDept.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsage1.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsage2.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsage3.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            this.txtDoseOnce.TextChanged += new EventHandler(Mouse_Leave);
            this.neuTextExpressBox1.TextChanged += new EventHandler(Mouse_Leave);

            this.txtDoseOnce1.TextChanged += new EventHandler(Mouse_Leave);

            this.cmbMiniUnit.MouseLeave += new EventHandler(Mouse_Leave);
            this.cmbMiniUnit1.MouseLeave += new EventHandler(Mouse_Leave);

            txtSample.KeyPress += new KeyPressEventHandler(ItemKeyPress);


            this.txtDays.ValueChanged += new EventHandler(Mouse_Leave);

            this.cmbMiniUnit.SelectedIndexChanged += new EventHandler(cmbMiniUnit_SelectedIndexChanged);
            this.cmbMiniUnit1.SelectedIndexChanged += new EventHandler(cmbMiniUnit1_SelectedIndexChanged);

            this.txtFu.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbFrequency.SelectedIndexChanged += new EventHandler(cmbFrequency_SelectedIndexChanged);
            this.cmbExeDept.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.chkEmerce.CheckedChanged += new EventHandler(CheckedChanged);
            this.chkDrugEmerce.CheckedChanged += new EventHandler(CheckedChanged);

            FS.HISFC.Components.Order.OutPatient.Classes.LogManager.Write("ucOrderInputByType添加事件!\r\n");
        }

        /// <summary>
        /// 批量删除事件
        /// </summary>
        private void DeleteEvent()
        {
            this.txtDoseOnce.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.neuTextExpressBox1.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOnce1.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtFirstDay.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDays.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtFu.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage1.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsage2.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.cmbMiniUnit.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMiniUnit1.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.cmbMemo1.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo2.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemo3.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            cmbCheckPartRecord.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            txtSample.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.cmbMemo1.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbMemo2.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbMemo3.TextChanged -= new EventHandler(Mouse_Leave);

            //对于是药品的嘱托医嘱，可以自行输入每次量单位
            this.cmbMiniUnit.Leave -= new EventHandler(Mouse_Leave);
            this.cmbMiniUnit1.Leave -= new EventHandler(Mouse_Leave);
            txtFirstDay.Leave -= new EventHandler(Mouse_Leave);
            this.txtSample.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            cmbCheckPartRecord.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            this.cmbExeDept.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsage1.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsage2.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsage3.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            this.txtDoseOnce.TextChanged -= new EventHandler(Mouse_Leave);
            this.neuTextExpressBox1.TextChanged -= new EventHandler(Mouse_Leave);

            this.txtDoseOnce1.TextChanged -= new EventHandler(Mouse_Leave);

            this.cmbMiniUnit.MouseLeave -= new EventHandler(Mouse_Leave);
            this.cmbMiniUnit1.MouseLeave -= new EventHandler(Mouse_Leave);

            txtSample.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.txtDays.ValueChanged -= new EventHandler(Mouse_Leave);

            this.cmbMiniUnit.SelectedIndexChanged -= new EventHandler(cmbMiniUnit_SelectedIndexChanged);
            this.cmbMiniUnit1.SelectedIndexChanged -= new EventHandler(cmbMiniUnit1_SelectedIndexChanged);

            this.txtFu.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbFrequency.SelectedIndexChanged -= new EventHandler(cmbFrequency_SelectedIndexChanged);
            this.cmbExeDept.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.chkEmerce.CheckedChanged -= new EventHandler(CheckedChanged);
            this.chkDrugEmerce.CheckedChanged -= new EventHandler(CheckedChanged);

            FS.HISFC.Components.Order.OutPatient.Classes.LogManager.Write("ucOrderInputByType删除事件!\r\n");
        }

        void cmbMiniUnit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myorder == null)
            {
                return;
            }

            this.Mouse_Leave(this.cmbMiniUnit1, null);
        }

        void cmbMiniUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (myorder == null)
            {
                return;
            }

            this.Mouse_Leave(this.cmbMiniUnit, null);
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            switch (GetVisiblePanel())
            {
                case 1://{76DF3C65-8215-4327-ACE2-10B307ACBA59}
                    this.txtDoseOnce.Focus();
                    this.txtDoseOnce.SelectAll();
                    break;
                case 2:
                    this.txtDoseOnce1.Focus();
                    this.txtDoseOnce1.SelectAll();
                    //this.txtFu.Focus();
                    //this.txtFu.SelectAll();
                    break;
                case 3:
                    this.cmbExeDept.Focus();
                    this.cmbExeDept.SelectAll();
                    break;
                default:
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 时间点变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuLinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this.myorder == null
                || this.myorder.Frequency == null
                || this.myorder.Frequency.Times.Length > 5)
            {
                return;
            }
            
            Forms.frmSpecialFrequency f = new HISFC.Components.Order.Forms.frmSpecialFrequency();
            //if (this.myorder.OrderType.IsDecompose == false || this.myorder.Item.IsPharmacy == false)//临时医嘱,非药品医嘱 不能修改特殊频次的剂量
            if (this.myorder.OrderType.IsDecompose == false || this.myorder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)//临时医嘱,非药品医嘱 不能修改特殊频次的剂量
            {
                f.IsDoseCanModified = false;
            }

            f.Frequency = this.myorder.Frequency;
            
            if (this.myorder.ExecDose == "")
                f.Dose = this.myorder.DoseOnce.ToString();
            else
                f.Dose = this.myorder.ExecDose;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.myorder.Frequency = f.Frequency;
                if (f.Dose.IndexOf("-")>0)
                {
                    this.myorder.ExecDose = f.Dose;
                    this.myorder.Memo = "时间："+f.Frequency.Time + " 剂量："+f.Dose;
                    if (this.ItemSelected != null) this.ItemSelected(this.myorder, ColmSet.B备注);
                }
                else
                {
                    this.myorder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(f.Dose);
                    this.myorder.ExecDose = "";
                    this.myorder.Memo = "";
                    if (this.ItemSelected != null) this.ItemSelected(this.myorder, ColmSet.M每次用量);
                    if (this.ItemSelected != null) this.ItemSelected(this.myorder, ColmSet.B备注);
                }
                if (this.ItemSelected != null) this.ItemSelected(this.myorder, ColmSet.P频次);
            }
        }

        /// <summary>
        /// 是否显示天数
        /// </summary>
        private void SetDaysVisible()
        {
            //如果是临时医嘱并且是出院带药的医嘱
            //目前打开所有临嘱都允许输入天数，否则可能与每次量、频次、天数相关的附材带出不准确
            if (this.myorder == null
                || this.myorder.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug
                || this.myorder.OrderType.IsDecompose
                //|| this.myorder.OrderType.ID != "CD"
                )
            {
                this.txtDays.Visible = false;
                this.lblDays.Visible = false;
            }
            else
            {
                this.txtDays.Visible = true;
                this.lblDays.Visible = true;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Obsolete("作废，用ucOrder里面的dicColmSet替换", true)]
    public enum EnumOrderFieldList
    {
        OrderType = 0,//医嘱类型
        Item = 1,//项目全变化了
        Qty = 1,//总量
        Unit = 2,//总量单位
        BeginDate = 3,//开始日期
        EndDate = 4,//结束日期
        DoseOnce  = 5,//一次剂量
        MinUnit = 6,//每次计量单位
        Fu = 7,//付数
        Memo = 8,//备注变化
        ExeDept = 9,//执行科室变化
        Emc = 10,//加急
        Sample =11,//样本类型
        Frequency = 12,//频次
        Usage = 13, //用法
        FirstDayQty = 14,//首日量

        /// <summary>
        /// 组号
        /// </summary>
        SubCombNo=15
    }
}

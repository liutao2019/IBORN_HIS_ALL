using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    /// <summary>
    /// 项目选择变化
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="changedField"></param>
    public delegate void ItemSelectedDelegate(FS.HISFC.Models.Order.OutPatient.Order sender, EnumOrderFieldList changedField);

    /// <summary>
    /// 计算总量后反馈总量显示
    /// </summary>
    /// <param name="inOrder"></param>
    public delegate void SetQtyValue(FS.HISFC.Models.Order.OutPatient.Order outOrder);

    /// <summary>
    /// 医嘱输入控件
    /// </summary>
    public partial class ucOrderInputByType : UserControl
    {
        public ucOrderInputByType()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.lnkTime.Visible = false;
            }
        }

        #region 变量
        /// <summary>
        /// 项目变化选择及变化事件
        /// </summary>
        public event ItemSelectedDelegate ItemSelected;

        /// <summary>
        /// 设置总量
        /// </summary>
        public event SetQtyValue SetQtyValue;

        /// 上个项目的天数
        /// </summary>
        private decimal useDays = 1;

        /// <summary>
        /// 上个项目的天数
        /// </summary>
        public decimal UseDays
        {
            get
            {
                return useDays;
            }
            set
            {
                useDays = value;
            }
        }

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
                if (isQtyChanged)
                {
                    //Components.Order.Classes.Function.ShowBalloonTip(0, "提示", "已人为修改总量，则不再自动计算总量！", ToolTipIcon.Warning);
                }
                //为了保证草药正确此处屏蔽 
                //isQtyChanged = false;
            }
        }

        /// <summary>
        /// 失去焦点时的事件代理
        /// </summary>
        /// <param name="obj"></param>
        public delegate void FocusLostHandler(object sender, EventArgs e);

        /// <summary>
        /// 失去焦点时的事件
        /// 失去焦点时，项目输入控件获取焦点
        /// </summary>
        public event FocusLostHandler FocusLost;

        /// <summary>
        /// 当前医嘱信息
        /// </summary>
        protected FS.HISFC.Models.Order.OutPatient.Order myorder = null;

        protected bool dirty;
        protected bool isUndrugShowFrequency = false;
        public bool IsNew = true;

        /// <summary>
        /// 数量是否人为修改过
        /// </summary>
        private bool qtyChanged = false;

        /// <summary>
        /// 可以输入第二单位的项目
        /// </summary>
        private Hashtable hsSecondUnitItem = new Hashtable();

        /// <summary>
        /// 是否所有药品都可以开立最小单位的每次用量
        /// </summary>
        private bool isAllDrugCanUseMinUnit = false;

        /// <summary>
        /// 每次量输入模式 0 原始模式；1 每次量输入后×基本计量；2 只有输入分数时才×每次量
        /// </summary>
        private int isDoseUnitInputMode = -1;

        /// <summary>
        /// 默认每次量显示 0 基本计量显示；1 最小单位数量显示
        /// </summary>
        private string defultDoceOnceUnit = "0";

        #endregion

        #region 属性

        /// <summary>
        /// 当前Order
        /// </summary>
        public FS.HISFC.Models.Order.OutPatient.Order Order
        {
            get
            {
                this.GetOrder();
                return this.myorder;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                this.myorder = value;

                this.SetOrder();
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
                this.neuLabel17.Visible = value;
                this.cmbUsageUndrug.Visible = value;
            }
        }

        #endregion

        #region 函数


        /// <summary>
        /// 增加事件
        /// </summary>
        private void AddEvent()
        {
            DeleteEvent();

            //用法
            this.cmbUsageP.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsagePCC.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbUsageUndrug.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            //草药特殊用法：煎药方式等
            this.cmbPCCSpeUsage.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //每次用量
            this.txtDoseOnceP.TextChanged += new EventHandler(Mouse_Leave);
            //if (isDoseUnitInputMode != 1 && isDoseUnitInputMode != 2)
            //{
            //    this.txtDoseOnceP.TextChanged += new EventHandler(Mouse_Leave);
            //}
            this.txtDoseOncePCC.TextChanged += new EventHandler(Mouse_Leave);
            //this.cmbDoseUnitP.TextChanged += new EventHandler(Mouse_Leave);

            //每次用量单位
            this.cmbDoseUnitP.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbDoseUnitPCC.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //频次
            this.cmbFrequency.SelectedIndexChanged += new EventHandler(Mouse_Leave);
            this.cmbFrequencyUndrug.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //天数/付数
            this.txtDay.TextChanged += new EventHandler(Mouse_Leave);
            this.txtDaysUndrug.TextChanged += new EventHandler(Mouse_Leave);
            this.txtFu.TextChanged += new EventHandler(Mouse_Leave);

            //备注
            this.cmbMemoP.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemoPCC.TextChanged += new EventHandler(Mouse_Leave);
            this.cmbMemoUndrug.TextChanged += new EventHandler(Mouse_Leave);

            //样本类型
            this.txtSample.TextChanged += new EventHandler(Mouse_Leave);
            txtSample.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            //执行科室
            this.cmbExeDept.SelectedIndexChanged += new EventHandler(Mouse_Leave);

            //加急
            this.chkEmerceUndrug.CheckedChanged += new EventHandler(Mouse_Leave);
            this.chkEmerceDrug.CheckedChanged += new EventHandler(Mouse_Leave);

            #region ItemKeyPress

            this.txtFu.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDay.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsagePCC.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.cmbDoseUnitP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoPCC.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOncePCC.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOnceP.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbExeDept.KeyPress += new KeyPressEventHandler(ItemKeyPress);

            this.txtDaysUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequencyUndrug.KeyPress += new KeyPressEventHandler(ItemKeyPress);
            #endregion
        }
                                           

        /// <summary>
        /// 删除事件
        /// </summary>
        private void DeleteEvent()
        {
            //用法
            this.cmbUsageP.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsagePCC.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbUsageUndrug.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            //草药特殊用法：煎药方式等
            this.cmbPCCSpeUsage.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //每次用量
            this.txtDoseOnceP.TextChanged -= new EventHandler(Mouse_Leave);
            //if (isDoseUnitInputMode != 1 && isDoseUnitInputMode != 2)
            //{
            //    this.txtDoseOnceP.TextChanged -= new EventHandler(Mouse_Leave);
            //}
            this.txtDoseOncePCC.TextChanged -= new EventHandler(Mouse_Leave);

            //每次用量单位
            this.cmbDoseUnitP.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbDoseUnitPCC.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //频次
            this.cmbFrequency.SelectedIndexChanged -= new EventHandler(Mouse_Leave);
            this.cmbFrequencyUndrug.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //天数/付数
            this.txtDay.TextChanged -= new EventHandler(Mouse_Leave);
            this.txtDaysUndrug.TextChanged -= new EventHandler(Mouse_Leave);
            this.txtFu.TextChanged -= new EventHandler(Mouse_Leave);

            //备注
            this.cmbMemoP.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbMemoPCC.TextChanged -= new EventHandler(Mouse_Leave);
            this.cmbMemoUndrug.TextChanged -= new EventHandler(Mouse_Leave);

            //样本类型
            this.txtSample.TextChanged -= new EventHandler(Mouse_Leave);
            txtSample.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            //执行科室
            this.cmbExeDept.SelectedIndexChanged -= new EventHandler(Mouse_Leave);

            //加急
            this.chkEmerceUndrug.CheckedChanged -= new EventHandler(Mouse_Leave);
            this.chkEmerceDrug.CheckedChanged -= new EventHandler(Mouse_Leave);

            #region ItemKeyPress

            this.txtFu.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDay.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequency.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsagePCC.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbUsageUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.cmbDoseUnitP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoPCC.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbMemoUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOncePCC.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.txtDoseOnceP.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbExeDept.KeyPress -= new KeyPressEventHandler(ItemKeyPress);

            this.txtDaysUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            this.cmbFrequencyUndrug.KeyPress -= new KeyPressEventHandler(ItemKeyPress);
            #endregion
        }

        protected void SetPanelVisible(int i)
        {
            this.panel1.Visible = false;
            this.panel2.Visible = false;
            this.panel3.Visible = false;
            switch (i)
            {
                case 1:
                    this.panel1.Visible = true;
                    break;
                case 2:
                    this.panel2.Visible = true;
                    break;
                case 3:
                    this.panel3.Visible = true;
                    break;
            }
            this.panelFrequency.BringToFront();
        }

        protected int GetVisiblePanel()
        {
            //治疗显示频次和天数
            if (this.myorder != null && this.myorder.Item != null && this.myorder.Item.SysClass.ID.ToString() == "UZ")
            {
                this.plFreq.Visible = true;
            }
            else
            {
                this.plFreq.Visible = false;
            }

            if (this.panel1.Visible)
                return 1;
            if (this.panel2.Visible)
                return 2;
            if (this.panel3.Visible)
                return 3;

            return 0;
        }

        private ComboBox MemoComboBox
        {
            get
            {
                switch (this.GetVisiblePanel())
                {
                    case 1:
                        return this.cmbMemoP;
                    case 2:
                        return this.cmbMemoPCC;
                    case 3:
                        return this.cmbMemoUndrug;
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
                        return this.cmbUsageP;
                    case 2:
                        return this.cmbUsagePCC;
                    case 3:
                        return this.cmbUsageUndrug;
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

            Components.Order.Classes.Function.SetExecDept(true, order.ReciptDept.ID, order.Item.ID, execDeptCode, ref execDept, ref alExecDept);
            if (alExecDept == null
                || alExecDept.Count == 0)
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

            //try
            //{
            //    cmbExeDept.AddItems(Components.Order.Classes.Function.GetExecDepts(((FS.HISFC.Models.Base.Employee)CacheManager.InterMgr.Operator).Dept, order));
            //}
            //catch
            //{
            //    cmbExeDept.AddItems(SOC.HISFC.BizProcess.Cache.Common.GetValidDept());
            //}

            //string[] execDept = execDeptCode.Split('|');

            //try
            //{
            //    for (int k = 0; k < execDept.Length; k++)
            //    {
            //        if (!string.IsNullOrEmpty(execDept[k]))
            //        {
            //            execDeptCode = execDept[k];
            //            if (SOC.HISFC.BizProcess.Cache.Common.GetDept(order.ReciptDept.ID).DeptType.ID.ToString() ==
            //                SOC.HISFC.BizProcess.Cache.Common.GetDept(execDept[k]).DeptType.ID.ToString())
            //            {
            //                execDeptCode = execDept[k];
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch
            //{
            //    execDeptCode = order.ReciptDept.ID;
            //}

            //cmbExeDept.Tag = execDeptCode;

            //if (string.IsNullOrEmpty(cmbExeDept.Text)
            //    || cmbExeDept.SelectedIndex == -1)
            //{
            //    if (cmbExeDept.alItems.Count > 0)
            //    {
            //        try
            //        {
            //            bool isRecipt = false;

            //            foreach (FS.FrameWork.Models.NeuObject obj in cmbExeDept.alItems)
            //            {
            //                if (obj.ID == execDeptCode)
            //                {
            //                    cmbExeDept.Tag = obj.ID;
            //                    isRecipt = false;
            //                    break;
            //                }
            //                if (obj.ID == order.ReciptDept.ID)
            //                {
            //                    isRecipt = true;
            //                }
            //            }

            //            if (isRecipt)
            //            {
            //                cmbExeDept.Tag = order.ReciptDept.ID;
            //            }
            //            else
            //            {
            //                string ReciptDeptTypeID = SOC.HISFC.BizProcess.Cache.Common.GetDept(order.ReciptDept.ID).DeptType.ID.ToString();
            //                for (int i = 0; i < cmbExeDept.alItems.Count; i++)
            //                {
            //                    if (ReciptDeptTypeID == 
            //                        SOC.HISFC.BizProcess.Cache.Common.GetDept(((FS.FrameWork.Models.NeuObject)cmbExeDept.alItems[i]).ID).DeptType.ID.ToString())
            //                    {
            //                        cmbExeDept.SelectedIndex = i;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //        catch
            //        {
            //        }

            //        if (cmbExeDept.SelectedIndex == -1)
            //        {
            //            cmbExeDept.SelectedIndex = 0;
            //        }
            //    }
            //}

            //if (cmbExeDept.Tag != null && !string.IsNullOrEmpty(cmbExeDept.Tag.ToString()))
            //{
            //    order.ExeDept.ID = cmbExeDept.Tag.ToString();
            //}
        }

        #endregion

        /// <summary>
        /// 设置医嘱
        /// </summary>
        protected void SetOrder()
        {
            dirty = true;
            try
            {
                this.DeleteEvent();
                cmbDoseUnitP.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbDoseUnitPCC.DropDownStyle = ComboBoxStyle.DropDownList;

                //频次重新付下值  很多字段的值都不对....{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                if (!string.IsNullOrEmpty(this.myorder.Frequency.ID) && !object.Equals(this.myorder.Frequency, null))
                {
                    this.myorder.Frequency = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(this.myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Clone();
                }
                if (this.myorder.Item.ItemType == EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item item = null;

                    if (myorder.Item.ID != "999")
                    {
                        item = CacheManager.PhaIntegrate.GetItem(this.myorder.Item.ID);
                        if (item == null)
                        {
                            MessageBox.Show("查找医嘱包含的药品失败");
                            return;
                        }
                        (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit = item.MinUnit;
                        (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit = item.PackUnit;
                        (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).SplitType = item.SplitType;
                    }

                    #region 草药
                    //草药
                    if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                    {
                        if (this.GetVisiblePanel() != 2)
                        {
                            this.SetPanelVisible(2);
                        }

                        this.cmbDoseUnitPCC.Items.Clear();
                        ArrayList alDoseUnit2 = new ArrayList();

                        if (this.myorder.Item.ID == "999")
                        {
                            cmbDoseUnitPCC.DropDownStyle = ComboBoxStyle.DropDown;
                            alDoseUnit2.Add(new FS.FrameWork.Models.NeuObject(myorder.DoseUnit, myorder.DoseUnit, ""));
                        }
                        else
                        {
                            alDoseUnit2.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));

                            if (isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//增加判断
                            {
                                alDoseUnit2.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            }
                        }

                        this.cmbDoseUnitPCC.AddItems(alDoseUnit2);

                        if (this.IsNew)
                        {
                            if (this.myorder.Item.ID != "999")
                            {
                                if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose == 0M)//一次剂量为零，默认显示基本剂量
                                {
                                    //add by liuww 2013-04-02
                                    //this.txtDoseOncePCC.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose.ToString();
                                    this.txtDoseOncePCC.Text = "0";
                                }
                                else
                                {
                                    this.txtDoseOncePCC.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose.ToString();
                                }

                                //默认频次
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID))
                                {
                                    this.cmbFrequency.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID;
                                    this.myorder.Frequency = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID) as FS.HISFC.Models.Order.Frequency).Clone();

                                    if (myorder.Frequency != null)
                                    {
                                        this.lnkTime.Text = myorder.Frequency.Time;
                                    }
                                }

                                //默认用法
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID))
                                {
                                    this.cmbUsagePCC.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID;
                                    this.myorder.Usage = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage;
                                }

                                this.myorder.DoseOnce = decimal.Parse(this.txtDoseOncePCC.Text);
                                this.myorder.DoseOnceDisplay = this.myorder.DoseOnce.ToString();
                                this.myorder.DoseUnit = this.cmbDoseUnitPCC.Text;

                                this.myorder.HerbalQty = 1;
                                this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            }
                        }
                        else
                        {
                            this.cmbDoseUnitPCC.Text = string.IsNullOrEmpty(myorder.DoseUnit) ? ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit : this.myorder.DoseUnit;
                            this.txtDoseOnceP.Text = string.IsNullOrEmpty(this.myorder.DoseOnceDisplay.Trim()) ? this.myorder.DoseOnce.ToString() : this.myorder.DoseOnceDisplay;


                            if (this.myorder.HerbalQty > 0)
                            {
                                this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            }
                            else
                            {
                                this.myorder.HerbalQty = 1;
                                this.txtFu.Text = this.myorder.HerbalQty.ToString();
                            }
                            this.cmbMemoPCC.Text = this.myorder.Memo;

                            this.txtDoseOncePCC.Text = this.myorder.DoseOnce.ToString();

                            this.cmbUsagePCC.Tag = this.myorder.Usage.ID;

                            if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
                            {
                                this.cmbFrequency.Tag = myorder.Frequency.ID;

                                this.lnkTime.Text = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Time;
                            }
                        }
                    }
                    #endregion

                    #region 西药
                    else//西药，中成药
                    {
                        if (this.GetVisiblePanel() != 1)
                        {
                            this.SetPanelVisible(1);
                        }

                        this.cmbDoseUnitP.Items.Clear();
                        ArrayList alDoseUnit1 = new ArrayList();

                        if (this.myorder.Item.ID == "999")
                        {
                            cmbDoseUnitP.DropDownStyle = ComboBoxStyle.DropDown;
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(myorder.DoseUnit, myorder.DoseUnit, ""));
                        }
                        else
                        {
                            alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.DoseUnit, item.DoseUnit, ""));
                            if (this.isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//增加判断
                            {
                                alDoseUnit1.Add(new FS.FrameWork.Models.NeuObject(item.MinUnit, item.MinUnit, ""));
                            }
                        }
                        this.cmbDoseUnitP.AddItems(alDoseUnit1);
                        //{557CD6B4-D471-4e6f-ADD8-A618F4DB0318} 新开的才赋值，不是新开的就不赋值
                        if (!string.IsNullOrEmpty(item.OnceDoseUnit) && this.IsNew)
                        {
                            this.cmbDoseUnitP.Tag = item.OnceDoseUnit;
                            this.txtDoseOnceP.Text = item.OnceDose.ToString();
                            this.myorder.DoseUnit = item.OnceDoseUnit;
                        }

                        #region 新添加

                        if (this.IsNew)
                        {
                            if (myorder.Item.ID != "999")
                            {
                                //默认频次
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID))
                                {
                                    this.cmbFrequency.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID;
                                    this.myorder.Frequency = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Frequency.ID) as FS.HISFC.Models.Order.Frequency).Clone();

                                    if ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem != null)
                                    {
                                        this.lnkTime.Text = myorder.Frequency.Time;
                                    }
                                }
                                //默认用法
                                if (((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage != null
                                    && !string.IsNullOrEmpty(((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID))
                                {
                                    this.cmbUsageP.Tag = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.ID;
                                    ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage.Name = this.cmbUsageP.Text;
                                    this.myorder.Usage = ((FS.HISFC.Models.Pharmacy.Item)myorder.Item).Usage;
                                }

                                // {4D67D981-6763-4ced-814E-430B518304E2}
                                if (string.IsNullOrEmpty(this.myorder.DoseUnit))
                                {
                                    //基本计量
                                    if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose == 0M)//一次剂量为零，默认显示基本剂量
                                    {
                                        //add by liuww 
                                        //this.txtDoseOnceP.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose.ToString();
                                        this.txtDoseOnceP.Text = "0";
                                        this.cmbDoseUnitP.Text = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit;

                                        if (this.isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//增加判断
                                        {
                                            if (defultDoceOnceUnit == "1")
                                            {
                                                this.txtDoseOnceP.Text = "1";
                                                this.cmbDoseUnitP.Tag = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).MinUnit;
                                                this.cmbDoseUnitP.Text = ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).MinUnit;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.txtDoseOnceP.Text = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).OnceDose.ToString();
                                    }
                                }
                            }
                            this.myorder.DoseOnce = decimal.Parse(this.txtDoseOnceP.Text);
                            this.myorder.DoseOnceDisplay = this.myorder.DoseOnce.ToString();
                            this.myorder.DoseUnit = this.cmbDoseUnitP.Text;
                            this.txtDay.Text = this.myorder.HerbalQty == 0 ? "1" : this.myorder.HerbalQty.ToString();
                            if (myorder.HerbalQty > 0)
                            {
                                this.useDays = myorder.HerbalQty;
                            }
                        }
                        #endregion

                        #region 修改
                        else
                        {
                            this.txtDoseOnceP.Text = string.IsNullOrEmpty(this.myorder.DoseOnceDisplay.Trim()) ? this.myorder.DoseOnce.ToString() : this.myorder.DoseOnceDisplay;

                            this.cmbDoseUnitP.Text = string.IsNullOrEmpty(myorder.DoseUnit) ? ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit : myorder.DoseUnit;
                            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}天数、数量、单位
                            this.txtDay.Text = this.myorder.HerbalQty == 0 ? "1" : this.myorder.HerbalQty.ToString();

                            if (myorder.HerbalQty > 0)
                            {
                                this.useDays = myorder.HerbalQty;
                            }

                            //貌似下面这里没有吧...

                            //ArrayList alUnits = new ArrayList();
                            ////如果可以拆分包装单位  则显示最小单位  否则只显示包装单位  
                            ////0 最小单位总量取整" 数据库值 0
                            ////包装单位总量取整" 数据库值 1  口服特别是中成药、妇科用药较多
                            ////最小单位每次取整" 数据库值 2  针剂较多这样
                            ////包装单位每次取整" 数据库值 3  几乎没有用   
                            //if ((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).SplitType == "0" || (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).SplitType == "2")
                            //{
                            //    alUnits.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit, ""));
                            //}
                            //alUnits.Add(new FS.FrameWork.Models.NeuObject((this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit, (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit, ""));
                            this.cmbUsageP.Tag = this.myorder.Usage.ID;

                            this.cmbMemoP.Text = this.myorder.Memo;
                            this.chkEmerceDrug.Checked = this.myorder.IsEmergency;

                            if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
                            {
                                this.cmbFrequency.Tag = myorder.Frequency.ID;

                                this.lnkTime.Text = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Time;
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
                else//非药品-描述医嘱
                {
                    if (this.GetVisiblePanel() != 3)
                    {
                        this.SetPanelVisible(3);
                    }
                    //this.cmbExeDept.Visible = true;
                    //neuLabel7.Visible = true;

                    //this.panelFrequency.Visible = false;//门诊医嘱，就是不用显示非药品频次
                    //this.txtDay.Visible = false;

                    txtSample.Visible = true;
                    neuLabel9.Visible = true;
                    if (this.myorder.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.txtSample.ClearItems();
                        this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperSample.ArrayObject);
                        this.neuLabel9.Text = "样本:";
                        this.txtSample.Text = this.myorder.Sample.Name;
                    }
                    else if (this.myorder.Item.SysClass.ID.ToString() == "UC")
                    {
                        this.txtSample.ClearItems();
                        this.txtSample.AddItems(HISFC.Components.Order.Classes.Function.HelperCheckPart.ArrayObject);
                        this.neuLabel9.Text = "部位:";
                        if (string.IsNullOrEmpty(this.myorder.CheckPartRecord))
                        {
                            this.txtSample.Text = this.myorder.Sample.Name;
                        }
                        else
                        {
                            this.txtSample.Text = this.myorder.CheckPartRecord;
                        }
                    }
                    else
                    {
                        txtSample.Visible = false;
                        neuLabel9.Visible = false;
                    }


                    SetExecDept(myorder, myorder.ExeDept.ID, IsNew);

                    if (cmbExeDept.Tag != null)
                    {
                        myorder.ExeDept.ID = cmbExeDept.Tag.ToString();
                    }

                    if (IsNew)
                    {
                        //if (myorder.Item.ID != "999")
                        //{
                        //    if (myorder.Item.ItemType == EnumItemType.UnDrug)
                        //    {
                        //        FS.HISFC.Models.Fee.Item.Undrug undrug = myorder.Item as FS.HISFC.Models.Fee.Item.Undrug;
                        //        //this.cmbExeDept.Tag = undrug.ExecDept;
                        //        SetExecDept(myorder, undrug.ExecDept, IsNew);
                        //    }
                        //}

                        this.txtDaysUndrug.Text = "1";
                    }
                    else
                    {
                        //执行科室
                        //if (myorder.ExeDept.ID != null && myorder.ExeDept.ID != "")
                        //{
                        //    //this.cmbExeDept.Tag = this.myorder.ExeDept.ID;
                        //    SetExecDept(myorder, myorder.ExeDept.ID, IsNew);
                        //}

                        this.cmbMemoUndrug.Text = this.myorder.Memo;
                        this.chkEmerceUndrug.Checked = this.myorder.IsEmergency;

                        this.txtDaysUndrug.Text = this.myorder.HerbalQty == 0 ? "1" : this.myorder.HerbalQty.ToString();

                        if (myorder.HerbalQty > 0)
                        {
                            this.useDays = myorder.HerbalQty;
                        }

                        this.cmbUsageUndrug.Tag = this.myorder.Usage.ID;

                        if (this.myorder.Frequency != null && this.myorder.Frequency.ID != "")
                        {
                            this.cmbFrequencyUndrug.Tag = myorder.Frequency.ID;

                            this.lnkTime.Text = (FS.HISFC.Components.Order.Classes.Function.HelperFrequency.GetObjectFromID(myorder.Frequency.ID) as FS.HISFC.Models.Order.Frequency).Time;
                        }

                        //检查部位{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                        //this.cmbCheckBody.Tag = this.myorder.CheckPartRecord;
                    }
                }

                this.AddEvent();
            }
            catch
            {
            };
            //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
            dirty = false;
            if (IsNew)
            {
                this.CalculateTotal(myorder);
                //if (this.ItemSelected != null)
                //{
                //    this.ItemSelected(this.myorder, EnumOrderFieldList.Item);
                //}
            }
        }

        /// <summary>
        /// 根据非药品编码加载有权限的默认执行科室 zhao.chf
        /// </summary>
        /// <param name="undrugCode"></param>
        public ArrayList AutoLoadDefaultDept(string undrugCode)
        {
            //DongGuan.HISFC.Manager.DefaultDept defaultDeptSettingMgr = new DongGuan.HISFC.Manager.DefaultDept();
            //ArrayList al = defaultDeptSettingMgr.GetDefaultDept(undrugCode);
            //if (al == null)
            //{
            //    MessageBox.Show(defaultDeptSettingMgr.Err);
            //    return null;
            //}

            //return al;
            return null;
        }

        /// <summary>
        /// 获得项目信息
        /// </summary>
        protected virtual void GetOrder()
        {
            if (this.dirty)
                return;

            if (this.myorder == null)
                return;

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


            switch (this.GetVisiblePanel())
            {
                case 1://西

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

                    try
                    {
                        this.myorder.DoseOnce = this.txtDoseOnceP.ComputeValue;//decimal.Parse(this.txtDoseOnce.Text);

                        if (this.txtDoseOnceP.InputText.Length > 8)
                        {
                            this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim().Substring(0, 8);
                        }
                        else
                        {
                            this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim();
                        }

                    }
                    catch
                    {
                        MessageBox.Show("每次用量输入不正确!", "提示");
                        return;
                    }
                    ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = (this.cmbDoseUnitP.Text);
                    this.myorder.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemoP.Text,"'");
                    this.myorder.IsEmergency = this.chkEmerceDrug.Checked;

                    //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}天数和院外注射次数
                    this.myorder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(txtDay.Text);

                    break;
                case 2://草

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

                    this.myorder.HerbalQty = decimal.Parse(this.txtFu.Text);

                    this.myorder.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemoPCC.Text);
                    this.myorder.DoseOnce = FrameWork.Function.NConvert.ToDecimal(this.txtDoseOncePCC.Text);
                    if (this.cmbPCCSpeUsage.Tag != null)
                    {
                        //this.myorder.HerbalUsage.ID = this.cmbSpeUsage.Tag.ToString();
                    }
                    ((FS.HISFC.Models.Pharmacy.Item)this.myorder.Item).DoseUnit = this.cmbDoseUnitPCC.Text;


                    break;
                case 3://非

                    if (this.cmbFrequencyUndrug.SelectedItem == null)
                    {
                        this.myorder.Frequency.ID = "";
                        this.myorder.Frequency.Name = "";
                        this.myorder.Frequency.Time = "";
                    }
                    else
                    {
                        this.myorder.Frequency.ID = this.cmbFrequencyUndrug.SelectedItem.ID;
                        this.myorder.Frequency.Name = this.cmbFrequencyUndrug.SelectedItem.Name;
                        this.myorder.Frequency.Time = this.lnkTime.Text;
                    }

                    if (this.cmbExeDept.Tag != null)
                    {
                        this.myorder.ExeDept.ID = this.cmbExeDept.Tag.ToString();
                        this.myorder.ExeDept.Name = this.cmbExeDept.Text;
                    }
                    this.myorder.Memo = FS.FrameWork.Public.String.TakeOffSpecialChar(this.cmbMemoUndrug.Text);
                    this.myorder.IsEmergency = this.chkEmerceUndrug.Checked;

                    if (myorder.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.myorder.Sample.Name = this.txtSample.Text;

                        if (this.txtSample.Tag != null)
                        {
                            this.myorder.Sample.ID = this.txtSample.Tag.ToString();
                        }
                    }
                    else
                    {
                        myorder.CheckPartRecord = txtSample.Text;
                    }

                    this.myorder.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(txtDaysUndrug.Text);
                    //检查部位{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                    //if (this.cmbCheckBody.Tag != null)
                    //{
                    //    this.myorder.CheckPartRecord = this.cmbCheckBody.Tag.ToString();
                    //}
                    break;
                default:
                    break;
            }


            this.AddEvent();

        }
        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            //Classes.LogManager.Write("【开始初始化项目输入控件】");

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
                this.cmbMemoP.AddItems(al1);
                this.cmbMemoPCC.AddItems(al1);
                this.cmbMemoUndrug.AddItems(al1);
                //{5EB232FD-EE54-49de-9C22-E8628E96E8CF}草药特殊用法
                ArrayList alSpeUsage = new ArrayList();
                alSpeUsage = CacheManager.GetConList("SPEUSAGE");
                if (alSpeUsage != null && alSpeUsage.Count > 0)
                {
                    this.cmbPCCSpeUsage.AddItems(alSpeUsage);
                }

                ArrayList alSecondUnitItems = new ArrayList();
                alSecondUnitItems = CacheManager.GetConList("SecondUnitItem");

                foreach (FS.HISFC.Models.Base.Const item in alSecondUnitItems)
                {
                    hsSecondUnitItem.Add(item.ID, item);
                }

                this.txtDoseOnceP.DotNum = 8;

                this.isAllDrugCanUseMinUnit = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("MZ0093", false, "0"));
                isDoseUnitInputMode = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ39", false, "0"));

                string val = Classes.Function.GetBatchControlParam("HNMZ41", false, "00");
                defultDoceOnceUnit = val.Substring(0, 1);

                //this.isAllDrugCanUseMinUnit = this.ctrlMgr.GetControlParam<bool>("MZ0093", false, false);
                //isDoseUnitInputMode = this.ctrlMgr.GetControlParam<int>("HNMZ39", false, 0);

                //string val = this.ctrlMgr.GetControlParam<string>("HNMZ41", false, "00");
                //defultDoceOnceUnit = val.Substring(0, 1);
            }
            //Classes.LogManager.Write("【结束初始化项目输入控件】");

        }

        void ucOrderTermInputByType_Leave(object sender, EventArgs e)
        {
            if (FocusLost != null)
            {
                FocusLost(sender, e);
            }
        }

        /// <summary>
        /// 文本框跳转
        /// 此处只控制跳转吧，医嘱改变放到Mouse_Leave里面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemKeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dirty)
                return;

            if (this.myorder == null)
                return;

            if (e.KeyChar == 13)
            {
                this.GetOrder();

                if (((Control)sender).Name.Length > 7
                    && ((Control)sender).Name.Substring(0, 7) == "cmbMemo"
                    && sender != this.cmbMemoUndrug)
                {
                    if (this.FocusLost != null)
                    {
                        this.FocusLost(sender, null);
                    }
                }
                //else if (((Control)sender).Name == "cmbExeDept" 
                //    && this.isUndrugShowFrequency == false)
                //{
                //    if (this.FocusLost != null)
                //        this.FocusLost(sender, null);
                //}
                else if (sender == this.txtDoseOnceP)
                {
                    if ((this.cmbDoseUnitP.Items != null && this.cmbDoseUnitP.Items.Count > 1)
                        || (myorder != null && myorder.Item != null && myorder.Item.ID == "999"))
                    {
                        this.cmbDoseUnitP.Focus();
                    }
                    else
                    {
                        this.cmbFrequency.Focus();
                    }
                }
                else if (sender == this.txtDoseOncePCC)
                {
                    if (myorder != null && myorder.Item != null && myorder.Item.ID == "999")
                    {
                        cmbDoseUnitPCC.Focus();
                    }
                    else
                    {
                        this.txtFu.Focus();
                        this.txtFu.Select(0, txtFu.Value.ToString().Length);
                    }
                }
                else if (sender == this.cmbDoseUnitP)
                {
                    this.cmbFrequency.Focus();
                }

                else if (sender == this.cmbDoseUnitPCC)
                {
                    this.txtFu.Focus();
                }
                else if (sender == this.txtFu)
                {
                    //this.cmbFrequency.Focus();
                    this.cmbUsagePCC.Focus();
                }
                //{59C74550-5948-4321-A029-CB3CA6A822FD}
                else if (sender == this.txtDay)
                {
                    this.cmbUsageP.Focus();
                }
                else if (sender == this.cmbUsageP)
                {
                    this.cmbMemoP.Focus();
                }
                else if (sender == this.cmbExeDept)
                {
                    if (isUndrugShowFrequency)
                    {
                        cmbUsageUndrug.Focus();
                    }
                    else
                    {
                        if (this.Order != null
                            && "UC、UL".Contains(this.Order.Item.SysClass.ID.ToString()))
                        {
                            this.txtSample.Focus();
                        }
                        else
                        {
                            this.cmbMemoUndrug.Focus();
                        }
                    }
                }
                else if (sender == this.txtSample)
                {
                    this.cmbMemoUndrug.Focus();
                }
                else if (sender == this.cmbUsageUndrug)
                {
                    if (this.Order != null
                        && "UC、UL".Contains(this.Order.Item.SysClass.ID.ToString()))
                    {
                        this.txtSample.Focus();
                    }
                    else
                    {
                        this.cmbMemoUndrug.Focus();
                    }
                }
                else if (sender == this.cmbFrequency)
                {
                    switch (this.GetVisiblePanel())
                    {
                        case 1:
                            //{59C74550-5948-4321-A029-CB3CA6A822FD}
                            this.txtDay.Focus();
                            this.txtDay.Select(0, this.txtDay.Text.Length);
                            break;
                        case 2:
                            this.cmbUsagePCC.Focus();
                            break;
                        default:
                            if (this.FocusLost != null)
                                this.FocusLost(sender, null);
                            break;
                    }
                }
                else if (sender == this.cmbMemoUndrug)
                {
                    if (this.plFreq.Visible)
                    {
                        this.cmbFrequencyUndrug.Focus();
                    }
                    else
                    {
                        if (this.FocusLost != null)
                        {
                            this.FocusLost(sender, null);
                        }
                    }
                }
                else if (sender == this.cmbFrequencyUndrug)
                {
                    this.txtDaysUndrug.Focus();
                    this.txtDaysUndrug.Select(0, this.txtDaysUndrug.Text.Length);
                }
                else if (sender == this.txtDaysUndrug)
                {
                    if (this.FocusLost != null)
                    {
                        this.FocusLost(sender, null);
                    }
                }
                else
                {
                    System.Windows.Forms.SendKeys.Send("{tab}");
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// 是否可以根据上下键修改选择处方
        /// </summary>
        /// <returns></returns>
        public bool IsCanChangeSelectOrder()
        {
            if (this.cmbUsageP.Focused ||
                this.cmbUsagePCC.Focused ||
                this.cmbUsageUndrug.Focused ||
                cmbDoseUnitP.Focused ||
                cmbDoseUnitPCC.Focused ||
                cmbFrequency.Focused ||
                cmbFrequencyUndrug.Focused ||
                cmbMemoP.Focused ||
                cmbMemoPCC.Focused ||
                cmbMemoUndrug.Focused ||
                cmbExeDept.Focused ||
                cmbPCCSpeUsage.Focused ||
                txtSample.Focused)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 处理改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Mouse_Leave(object sender, EventArgs e)
        {
            if (this.dirty)
                return;

            if (this.myorder == null)
                return;

            #region 每次用量

            if ((Control)sender == txtDoseOnceP)
            {
                FS.HISFC.Models.Pharmacy.Item item = this.myorder.Item as FS.HISFC.Models.Pharmacy.Item;

                //1 每次量输入后×基本计量；
                if (isDoseUnitInputMode == 1)
                {
                    this.myorder.DoseOnce = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose * txtDoseOnceP.ComputeValue;
                    this.txtDoseOnceP.Text = this.myorder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.myorder.DoseOnceDisplay = txtDoseOnceP.Text;
                }
                //2 只有输入分数时才×每次量
                else if (isDoseUnitInputMode == 2)
                {
                    //通过包含 “/”判断输入的是分数
                    if (txtDoseOnceP.Text.Contains("/") && !txtDoseOnceP.Text.StartsWith("/") && !txtDoseOnceP.Text.EndsWith("/"))
                    {
                        this.myorder.DoseOnce = (this.myorder.Item as FS.HISFC.Models.Pharmacy.Item).BaseDose * txtDoseOnceP.ComputeValue;
                    }
                    else
                    {
                        this.myorder.DoseOnce = txtDoseOnceP.ComputeValue;
                    }
                    this.txtDoseOnceP.Text = this.myorder.DoseOnce.ToString("F4").TrimEnd('0').TrimEnd('.');
                    this.myorder.DoseOnceDisplay = txtDoseOnceP.Text;
                }
                else
                {
                    //作为显示的剂量内容
                    string doseOnce = this.txtDoseOnceP.Text;
                    if (doseOnce.EndsWith("+") && myorder.Item.ID != "999")
                    {
                        if (this.myorder != null && this.myorder.Item.ItemType == EnumItemType.Drug)
                        {
                            try
                            {
                                if (isAllDrugCanUseMinUnit || this.hsSecondUnitItem.Contains(item.ID))//增加判断
                                {
                                    this.cmbDoseUnitP.Text = item.MinUnit;
                                    this.txtDoseOnceP.Text = doseOnce.TrimEnd('+');
                                }
                                else
                                {
                                    if (hsSecondUnitItem.ContainsKey(this.myorder.Item.ID))
                                    {
                                        this.cmbDoseUnitP.Text = item.MinUnit;
                                        this.txtDoseOnceP.Text = doseOnce.TrimEnd('+');
                                    }
                                    else
                                    {
                                        this.txtDoseOnceP.Text = doseOnce.TrimEnd('+');
                                    }
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }

                    if (this.txtDoseOnceP.InputText.Length > 8)
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim().Substring(0, 8);
                    }
                    else
                    {
                        this.myorder.DoseOnceDisplay = this.txtDoseOnceP.InputText.Trim();
                    }

                    this.myorder.DoseOnce = this.txtDoseOnceP.ComputeValue;
                }

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                }
            }
            //草药每次用量
            else if ((Control)sender == txtDoseOncePCC)
            {
                if (this.myorder.Item.SysClass.ID.ToString() == "PCC")
                {
                    try
                    {
                        this.myorder.DoseOnce = FrameWork.Function.NConvert.ToDecimal(this.txtDoseOncePCC.Text);
                        this.myorder.DoseOnceDisplay = this.txtDoseOncePCC.InputText.Trim();
                        if (myorder.Item.ID != "999")
                        {
                            this.myorder.Qty = Math.Round(this.myorder.HerbalQty * this.myorder.DoseOnce / ((HISFC.Models.Pharmacy.Item)this.myorder.Item).BaseDose, 2);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("每剂量输入错误：\r\n" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtDoseOncePCC.Focus();
                        this.txtDoseOncePCC.SelectAll();
                        return;
                    }
                }

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                }

            }
            #endregion

            #region 每次用量单位
            //西药每次用量
            else if ((Control)sender == cmbDoseUnitP)
            {
                if (myorder == null)
                {
                    return;
                }
                this.myorder.DoseUnit = this.cmbDoseUnitP.Text;

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseUnit);
                }
            }

            else if ((Control)sender == cmbDoseUnitPCC)
            {
                if (myorder == null)
                {
                    return;
                }
                this.myorder.DoseUnit = this.cmbDoseUnitPCC.Text;

                this.CalculateTotal(this.myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.DoseUnit);
                }
            }
            #endregion

            #region 频次
            else if ((Control)sender == cmbFrequency)
            {
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

                this.CalculateTotal(myorder);

                CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Frequency);
                }
            }
            //治疗项目的频次
            else if ((Control)sender == cmbFrequencyUndrug)
            {
                this.Order.Frequency = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequencyUndrug.SelectedItem).Clone();
                this.CalculateTotal(myorder);
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Frequency);
                }

            }
            #endregion

            #region 天数/付数
            else if ((Control)sender == txtFu)
            {
                this.Order.HerbalQty = decimal.Parse(this.txtFu.Text);
                this.CalculateTotal(myorder);
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.HerbalQty);
                }

            }

            else if ((Control)sender == txtDay)//{59C74550-5948-4321-A029-CB3CA6A822FD}
            {
                try
                {
                    this.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Text);
                }
                catch
                {
                    MessageBox.Show("天数输入错误，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                this.CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.HerbalQty);
                }

                if (Order.HerbalQty > 0)
                {
                    this.useDays = Order.HerbalQty;
                }

            }
            //治疗项目的天数
            else if ((Control)sender == txtDaysUndrug)//{59C74550-5948-4321-A029-CB3CA6A822FD}
            {
                try
                {
                    this.Order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDaysUndrug.Text);
                }
                catch
                {
                    MessageBox.Show("天数输入错误，请重新输入！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.CalculateTotal(myorder);//{59C74550-5948-4321-A029-CB3CA6A822FD}

                //this.CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.HerbalQty);
                }

                if (Order.HerbalQty > 0)
                {
                    this.useDays = Order.HerbalQty;
                }

            }
            #endregion

            #region 样本类型
            else if ((Control)sender == txtSample)
            {
                if (this.ItemSelected != null)
                {
                    if (Order != null && Order.Item.SysClass.ID.ToString() == "UL")
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.Sample);
                    }
                    else
                    {
                        this.ItemSelected(this.Order, EnumOrderFieldList.CheckBody);
                    }
                }

            }
            #endregion

            #region 执行科室
            else if ((Control)sender == cmbExeDept)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.ExeDept);
                }

            }
            #endregion

            #region 检查部位
            //else if((Control)sender==  )
            //{
            //    if (this.ItemSelected != null)
            //    {
            //        this.ItemSelected(this.Order, EnumOrderFieldList.CheckBody);
            //    }

            //    }
            #endregion

            #region 备注
            else if ((Control)sender == cmbMemoP)
            {
                if (this.cmbMemoP.Text.Contains("皮试"))
                {
                    this.myorder.HypoTest = FS.HISFC.Models.Order.EnumHypoTest.NeedHypoTest;
                }
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                }

            }
            else if ((Control)sender == cmbMemoPCC)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                }

            }
            else if ((Control)sender == cmbMemoUndrug)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Memo);
                }

            }
            #endregion

            #region 用法
            else if ((Control)sender == cmbUsageP)
            {
                this.CalculateInjNum();

                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                }
            }
            else if ((Control)sender == cmbUsagePCC)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                }
            }

            else if ((Control)sender == cmbUsageUndrug)
            {
                this.myorder.Usage.ID = this.cmbUsageUndrug.Tag.ToString();
                this.myorder.Usage.Name = this.cmbUsageUndrug.Text;
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.Usage);
                }
            }
            #endregion

            #region 数量

            //数量变化{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
            //else if((Control)sender==  txtDrugQty)
            //{
            //    if (this.ItemSelected != null)
            //    {
            //        this.ItemSelected(this.Order, EnumOrderFieldList.DrugQty);
            //    }
            //    }
            #endregion

            #region 加急
            else if ((Control)sender == chkEmerceDrug)
            {
                this.myorder.IsEmergency = this.chkEmerceDrug.Checked;
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Emc);
                }
            }
            else if ((Control)sender == chkEmerceUndrug)
            {
                this.myorder.IsEmergency = this.chkEmerceUndrug.Checked;
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Emc);
                }
            }
            #endregion

            #region 煎药方式

            else if ((Control)sender == cmbPCCSpeUsage)
            {
                if (this.ItemSelected != null)
                {
                    this.ItemSelected(this.Order, EnumOrderFieldList.SpeUsage);
                }
            }
            #endregion
            //else
            //{
            //    if (this.ItemSelected != null)
            //    {
            //        this.ItemSelected(this.Order, EnumOrderFieldList.Item);
            //    }
            //}
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
            this.txtDoseOnceP.Text = "0";				//每次用量
            //this.txtMultiple.Text = "0";
            this.cmbDoseUnitP.Tag = "";				//每次用量单位
            this.cmbMemoP.Text = "";				//备注
            this.cmbMemoPCC.Text = "";
            this.cmbMemoUndrug.Text = "";
            this.txtFu.Text = "0";					//付数
            this.cmbExeDept.Text = "";				//执行科室
            this.chkEmerceUndrug.Checked = false;			//加急
            this.chkEmerceDrug.Checked = false;		//加急
            this.txtSample.Text = "";
            this.cmbFrequency.Tag = "";
            this.cmbFrequency.Text = "";
            //this.txtFrequency.Text = "";
            this.cmbFrequencyUndrug.Tag = "";
            this.cmbFrequencyUndrug.Text = "";
            this.cmbUsageP.Text = "";
            this.cmbUsageP.Tag = "";
            this.cmbUsagePCC.Text = "";
            this.cmbUsagePCC.Tag = "";
            this.cmbUsageUndrug.Text = "";
            this.cmbUsageUndrug.Tag = "";


            this.txtDay.Text = "1";
            this.txtDaysUndrug.Text = "1";
            //this.cmbCheckBody.Tag = "";
            this.qtyChanged = false;
            if (myorder != null)
            {
                myorder.Item.User02 = qtyChanged ? "1" : "0";
            }


            this.AddEvent();
        }

        /// <summary>
        /// 快速跳转
        /// </summary>
        public void SetShortKey()
        {
            if (this.txtDoseOnceP.Focused || this.txtFu.Focused || this.cmbExeDept.Focused)
            {
                if (this.FocusLost != null)
                    this.FocusLost(null, null);
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        /// <param name="alFrequency"></param>
        /// <param name="alDept"></param>
        public virtual void InitControl(ArrayList alFrequency, ArrayList alDept, ArrayList alUsage)
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在初始化界面显示..", 50, false);
                Application.DoEvents();

                if (alDept == null)
                {
                    //alDept = CacheManager.InterMgr.GetDepartment();
                    alDept = SOC.HISFC.BizProcess.Cache.Common.GetDept();
                }
                this.cmbExeDept.AddItems(alDept);
                this.cmbExeDept.IsListOnly = true;

                if (alFrequency == null)
                {
                    alFrequency = FS.HISFC.Components.Order.Classes.Function.HelperFrequency.ArrayObject.Clone() as ArrayList;
                }

                this.cmbFrequency.IsShowID = true;
                this.cmbFrequency.AddItems(alFrequency);
                this.cmbFrequency.IsListOnly = true;

                this.cmbFrequencyUndrug.IsShowID = true;
                this.cmbFrequencyUndrug.AddItems(alFrequency);
                this.cmbFrequencyUndrug.IsListOnly = true;

                //初始化用法
                //从院注用法表获取有效用法，因为这里存储了用户的自定义编码
                ArrayList alInjectUsage = new ArrayList();//constantMgr.GetAllList("InjectUsage");
                ArrayList alTemp = null;
                if (alTemp == null || alTemp.Count == 0)
                {
                    alTemp = new ArrayList();
                    foreach (FS.HISFC.Models.Base.Const con in FS.HISFC.Components.Order.Classes.Function.HelperUsage.ArrayObject)
                    {
                        alTemp.Add(con.Clone());
                    }
                }


                foreach (FS.HISFC.Models.Base.Const usageObj in alTemp)
                {
                    usageObj.UserCode = "";
                    alInjectUsage.Add(usageObj);
                }

                this.cmbUsageP.AddItems(alInjectUsage);
                this.cmbUsagePCC.AddItems(alInjectUsage);
                this.cmbUsageUndrug.AddItems(alInjectUsage);

                this.cmbUsageP.IsListOnly = true;
                this.cmbUsagePCC.IsListOnly = true;
                this.cmbUsageUndrug.IsListOnly = true;
                //初始化样本类型
                this.txtSample.AddItems(CacheManager.GetConList(FS.HISFC.Models.Base.EnumConstant.LABSAMPLE));
                this.txtSample.AppendItems(CacheManager.GetConList("CHECKPART"));
                //检查部位{5EB232FD-EE54-49de-9C22-E8628E96E8CF}
                //this.cmbCheckBody.AddItems(CacheManager.GetConList("CHECKBODY"));
            }
            catch (Exception ex)
            {
                MessageBox.Show("ucOrderInputByType" + ex.Message);
            }

            if (isDoseUnitInputMode == -1)
            {
                isDoseUnitInputMode = FS.FrameWork.Function.NConvert.ToInt32(Classes.Function.GetBatchControlParam("HNMZ39", false, "0"));
                //isDoseUnitInputMode = this.ctrlMgr.GetControlParam<int>("HNMZ39", false, 0);
            }
            this.AddEvent();

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        /// <summary>
        /// 设置焦点
        /// </summary>
        public new void Focus()
        {
            qtyChanged = false;
            if (myorder != null)
            {
                myorder.Item.User02 = qtyChanged ? "1" : "0";
            }
            switch (GetVisiblePanel())
            {
                case 1:
                    this.txtDoseOnceP.Focus();
                    this.txtDoseOnceP.SelectAll();
                    break;
                case 2:
                    this.txtDoseOncePCC.Focus();
                    this.txtDoseOncePCC.SelectAll();
                    break;
                case 3:
                    this.cmbExeDept.Focus();
                    cmbExeDept.SelectAll();

                    //增加用法跳转
                    //this.cmbUsageUndrug.Focus();
                    //this.cmbUsageUndrug.SelectAll();
                    //this.cmbMemo3.Focus();
                    //this.cmbMemo3.SelectAll();
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

            if (this.myorder == null || this.myorder.Frequency == null || this.myorder.Frequency.Times.Length > 5)
                return;

            FS.HISFC.Components.Order.Forms.frmSpecialFrequency f = new FS.HISFC.Components.Order.Forms.frmSpecialFrequency();

            f.IsDoseCanModified = false;//门诊医嘱 不能修改特殊频次的剂量

            f.Frequency = this.myorder.Frequency.Clone();

            if (this.myorder.ExecDose == "")
                f.Dose = this.myorder.DoseOnce.ToString();
            else
                f.Dose = this.myorder.ExecDose;
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.myorder.Frequency = f.Frequency.Clone();
                if (f.Dose.IndexOf("-") > 0)
                {
                    this.myorder.ExecDose = f.Dose;
                    this.myorder.Memo = "时间：" + f.Frequency.Time + " 剂量：" + f.Dose;
                    if (this.ItemSelected != null)
                        this.ItemSelected(this.myorder, EnumOrderFieldList.Memo);
                }
                else
                {
                    this.myorder.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(f.Dose);
                    this.myorder.ExecDose = "";

                    this.myorder.Memo = "";
                    if (this.ItemSelected != null)
                        this.ItemSelected(this.myorder, EnumOrderFieldList.DoseOnce);
                    if (this.ItemSelected != null)
                        this.ItemSelected(this.myorder, EnumOrderFieldList.Memo);
                }
                if (this.ItemSelected != null)
                    this.ItemSelected(this.myorder, EnumOrderFieldList.Frequency);
            }
        }

        /// <summary>
        /// 自动计算总量 add by  {59C74550-5948-4321-A029-CB3CA6A822FD}
        /// </summary>
        private void CalculateTotal(FS.HISFC.Models.Order.OutPatient.Order orderObj)
        {
            if (orderObj == null)
            {
                return;
            }

            if (orderObj.Item.SysClass.ID.ToString() == "M")
            {
                return;
            }

            //人为修改过总量后，不再计算
            //非药品因为需要人为输入数量，则总是会自动计算  先限制为治疗吧
            if (isQtyChanged)// && Order.Item.SysClass.ID.ToString() != "UZ")
            {
                //Components.Order.Classes.Function.ShowBalloonTip(4, "提示", "之前已人为修改总量，此处不再自动计算！", ToolTipIcon.Warning);
                return;
            }


            dirty = true;
            //西药天数
            decimal Days = 0;
            if (this.GetVisiblePanel() == 1)
            {
                Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDay.Text);
            }
            else if (this.GetVisiblePanel() == 3)
            {
                Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtDaysUndrug.Text);
            }
            else
            {
                Days = FS.FrameWork.Function.NConvert.ToDecimal(this.txtFu.Text);
            }

            //如果没有输入天数时则不处理
            if (Days <= 0)
            {
                dirty = false;
                return;
            }
            else
            {
                orderObj.HerbalQty = Days;
            }

            //{37C70D4B-53A8-46a4-B9CB-FF4583E9DFAE}
            if (Components.Order.Classes.Function.ReComputeQty(orderObj) == -1)
            {
                dirty = false;
                return;
            }

            if (SetQtyValue != null)
            {
                SetQtyValue(orderObj);
            }

            dirty = false;
        }

        /// <summary>
        /// 自动计算院注次数
        /// </summary>
        private void CalculateInjNum()
        {
            if (myorder == null)
            {
                return;
            }
            dirty = true;
            decimal Frequence = 0;
            if (this.cmbFrequency.SelectedItem != null)
            {
                this.myorder.Frequency = ((FS.HISFC.Models.Order.Frequency)this.cmbFrequency.SelectedItem).Clone();
            }

            if (this.myorder.Frequency.Days[0] == "0" || string.IsNullOrEmpty(this.myorder.Frequency.Days[0]))
            {
                this.myorder.Frequency.Days[0] = "1";
                Frequence = this.myorder.Frequency.Times.Length;
            }
            else
            {
                try
                {
                    Frequence = Math.Round(this.myorder.Frequency.Times.Length / FS.FrameWork.Function.NConvert.ToDecimal(this.myorder.Frequency.Days[0]), 2);
                }
                catch
                {
                    Frequence = this.myorder.Frequency.Times.Length;
                }
            }
            //用法
            if (this.cmbUsageP.Tag != null)
            {
                this.myorder.Usage.ID = this.cmbUsageP.Tag.ToString();
                this.myorder.Usage.Name = this.cmbUsageP.Text;
            }
            //院注次数
            if (this.cmbUsageP.Tag != null
                //&& Classes.Function.HsUsageAndSub.Contains(cmbUsage1.Tag.ToString())
                && Classes.Function.CheckIsInjectUsage(cmbUsageP.Tag.ToString())
                )
            {
                this.myorder.InjectCount = (int)Math.Ceiling((double)(Frequence * this.myorder.HerbalQty));
            }
            else
            {
                this.myorder.InjectCount = 0;
            }
            dirty = false;
        }

        /// <summary>
        /// 获取当前控件的名称 只是用于跳转
        /// </summary>
        /// <returns></returns>
        public bool RecycleTab()
        {
            if (this.ActiveControl != null)
            {
                if (this.ActiveControl.Name == "cmbMemo1"
                   || this.ActiveControl.Name == "cmbMemo2"
                   || this.ActiveControl.Name == "cmbMemo3"
                   || this.ActiveControl.Name == "txtDays1")
                {
                    if (this.FocusLost != null)
                        this.FocusLost(this.ActiveControl, null);
                    return true;
                }
            }
            return false;
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            if (this.txtDay.Focused )
            {
                if (keyData == Keys.Up )
                {
                    this.txtDay.Value += 1;
                }
                else if (keyData == Keys.Down)
                {
                    if (txtDay.Value >= 1)
                    {
                        txtDay.Value -= 1;
                    }
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                return true;
            }
            else if (txtDaysUndrug.Focused)
            {
                if (keyData == Keys.Up)
                {
                    this.txtDaysUndrug.Value += 1;
                }
                else if (keyData == Keys.Down)
                {
                    if (txtDaysUndrug.Value >= 1)
                    {
                        txtDaysUndrug.Value -= 1;
                    }
                }
                else
                {
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                return true;
            }
            else
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Forms
{
    /// <summary>
    /// 委托
    /// </summary>
    public delegate void GroupConfirmHandler();

    /// <summary>
    /// [功能描述: 组套选择控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11]<br></br>
    /// <说明
    ///		 通过传入的Fp 显示Fp的列信息 并可维护是否显示/排序等信息
    ///  />
    /// </summary>
    public partial class frmSelectGroup : Form
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public frmSelectGroup()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 组套类别
        /// </summary>
        protected FS.HISFC.Models.Base.ServiceTypes inpatientType = FS.HISFC.Models.Base.ServiceTypes.I;

        private FS.HISFC.BizLogic.Manager.UndrugztManager ztManager = new FS.HISFC.BizLogic.Manager.UndrugztManager();

        FS.HISFC.BizLogic.Manager.Group groupManager = new FS.HISFC.BizLogic.Manager.Group();

        /// <summary>
        /// 组套选择确认
        /// </summary>
        public event GroupConfirmHandler GroupConfirm;

        /// <summary>
        /// 当前选择的组套
        /// </summary>
        private FS.HISFC.Models.Base.Group currentGroup = new FS.HISFC.Models.Base.Group();

        /// <summary>
        /// 组套类别
        /// </summary>
        [DefaultValue(FS.HISFC.Models.Base.ServiceTypes.I)]
        public FS.HISFC.Models.Base.ServiceTypes InpatientType
        {
            get
            {
                return inpatientType;
            }
            set
            {
                inpatientType = value;
            }
        }

        /// <summary>
        /// 是否模式对话框显示组套明细 0不是模式对话框
        /// </summary>
        public int IsShowDetailDialog = -1;

        /// <summary>
        /// 是否编辑组套模式
        /// </summary>
        private bool isEditGroup = false;

        /// <summary>
        /// 是否编辑组套模式
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

        /// <summary>
        /// 全院组套的执行科室是否开立后重取 0 不重取、1 重取
        /// 000 三位分别表示全院、科室、个人组套 
        /// </summary>
        string isReGetExecDept = "-1";

        /// <summary>
        /// 所有项目默认是否全选
        /// </summary>
        int isAllSelected = -1;

        /// <summary>
        /// 是否有科室组套修改权限
        /// </summary>
        private bool isHaveDeptEditPower = false;

        /// <summary>
        /// 是否有科室组套修改权限
        /// </summary>
        public bool IsHaveDeptEditPower
        {
            get
            {
                return isHaveDeptEditPower;
            }
            set
            {
                isHaveDeptEditPower = value;
            }
        }

        /// <summary>
        /// 是否有全院组套修改权限
        /// </summary>
        private bool isHaveAllEditPower = false;

        /// <summary>
        /// 是否启用零差价
        /// </summary>
        private bool isUseRetailPrice2 = false;

        /// <summary>
        /// 是否有全院组套修改权限
        /// </summary>
        public bool IsHaveAllEditPower
        {
            get
            {
                return isHaveAllEditPower;
            }
            set
            {
                isHaveAllEditPower = value;
            }
        }

        /// <summary>
        /// 合同单位信息
        /// </summary>
        private FS.HISFC.Models.Base.PactInfo pact;

        /// <summary>
        /// 合同单位信息
        /// </summary>
        public FS.HISFC.Models.Base.PactInfo Pact
        {
            set
            {
                pact = value;
            }
        }

        private FS.HISFC.BizProcess.Integrate.Common.ControlParam contrlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private FS.HISFC.BizProcess.Integrate.Order orderIntergrate = new FS.HISFC.BizProcess.Integrate.Order();

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(FS.HISFC.Models.Base.Group groupInfo)
        {
            if (isReGetExecDept == "-1")
            {
                isReGetExecDept = contrlMgr.GetControlParam<string>("HNZY02", false, "111");
                if (isReGetExecDept == "1")
                {
                    isReGetExecDept = "100";
                }
                else if (isReGetExecDept == "0")
                {
                    isReGetExecDept = "000";
                }
            }

            if (isAllSelected == -1)
            {
                isAllSelected = contrlMgr.GetControlParam<int>("HNMZ97", false, 1);
            }

            isUseRetailPrice2 = contrlMgr.GetControlParam<bool>("HNPHA2", false, false);

            this.spread1_Sheet1.RowCount = 0;
            neuSpread1_Sheet1.RowCount = 0;

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在显示组套明细,请稍等！", 50, false);


            try
            {
                Application.DoEvents();

                ArrayList myalItems = groupManager.GetAllItem(groupInfo);
                if (myalItems == null || myalItems.Count == 0)
                {
                    return;
                }

                currentGroup = groupInfo;

                this.InpatientType = groupInfo.UserType;

                this.spread1_Sheet1.Columns.Remove(0, 1);

                Classes.Function.ShowOrder(this.spread1_Sheet1, myalItems, 1, this.inpatientType);

                this.spread1_Sheet1.Columns.Add(0, 1);
                this.spread1_Sheet1.Columns[0].Label = "选择";
                this.spread1_Sheet1.Columns[0].CellType = new FarPoint.Win.Spread.CellType.CheckBoxCellType();
                this.spread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.Normal;
                this.spread1_Sheet1.SelectionPolicy = FarPoint.Win.Spread.Model.SelectionPolicy.MultiRange;
                this.spread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Row;
                this.spread1_Sheet1.GrayAreaBackColor = System.Drawing.Color.White;

                for (int i = 1; i < this.spread1_Sheet1.ColumnCount; i++)
                {
                    this.spread1_Sheet1.Columns[i].Locked = true;
                }
                this.spread1_Sheet1.Columns[0].Locked = false;
                if (groupInfo.Memo == "不可选")
                {
                    this.spread1_Sheet1.Columns[0].Locked = true;
                }
                if (this.groupManager.QueryIsSelectAll(groupInfo.ID) == 1)
                {
                    isAllSelected = 1;
                    this.cbxIsSelectAll.CheckedChanged -= this.cbxIsSelectAll_CheckedChanged;
                    this.cbxIsSelectAll.Checked = true;
                    this.cbxIsSelectAll.CheckedChanged += this.cbxIsSelectAll_CheckedChanged;
                }
                else
                {
                    isAllSelected = 0;
                    this.cbxIsSelectAll.CheckedChanged -= this.cbxIsSelectAll_CheckedChanged;
                    this.cbxIsSelectAll.Checked = false;
                    this.cbxIsSelectAll.CheckedChanged += this.cbxIsSelectAll_CheckedChanged;
                }

                this.chkAll.CheckedChanged -= new EventHandler(chkAll_CheckedChanged);
                this.chkAll.Checked = FS.FrameWork.Function.NConvert.ToBoolean(isAllSelected);
                this.chkAll.CheckedChanged += new EventHandler(chkAll_CheckedChanged);


                FS.HISFC.Models.SIInterface.Compare compareItem = null;
                Font font_Bold = new Font(this.spread1.Font.FontFamily, this.spread1.Font.Size, FontStyle.Bold);
                Font font_Regular = new Font(this.spread1.Font.FontFamily, this.spread1.Font.Size, FontStyle.Regular);

                //默认全部选中
                for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
                {
                    this.spread1_Sheet1.SetValue(i, 0, isAllSelected == 1 ? true : false, false);
                    FS.HISFC.Models.Order.Order order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;
                    if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        if (isUseRetailPrice2)
                        {

                            this.spread1_Sheet1.Cells[i, 3].Value = order.Item.Name + "【" + SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID).RetailPrice2.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }
                        else
                        {
                            this.spread1_Sheet1.Cells[i, 3].Value = order.Item.Name + "【" + ((FS.HISFC.Models.Pharmacy.Item)order.Item).Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                        }
                    }
                    else
                    {
                        this.spread1_Sheet1.Cells[i, 3].Value = order.Item.Name + "【" + order.Item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】";
                    }

                    if (isEditGroup)
                    {
                        order.ExtendFlag2 = "";
                    }
                    else
                    {
                        if (order != null && order.ExtendFlag2 == "N")
                        {
                            this.spread1_Sheet1.Rows[i].ForeColor = Color.Red;
                            this.spread1_Sheet1.SetValue(i, 0, false, false);
                        }
                    }

                    #region 显示医保等级信息

                    //不显示医保等级【私立医院不显示医保等级；但是公立医院显示医保等级；暂时不显示】
                    if (false && pact != null && !string.IsNullOrEmpty(pact.ID))
                    {
                        string strCompareInfo = "";
                        if (Classes.Function.IItemCompareInfo.GetCompareItemInfo(order.Item, pact, ref compareItem, ref strCompareInfo) == -1)
                        {
                            this.spread1_Sheet1.RowHeader.Rows[i].Font = font_Regular;
                            this.spread1_Sheet1.RowHeader.Rows[i].ForeColor = this.spread1_Sheet1.ColumnHeader.Columns[0].ForeColor;
                        }
                        else
                        {
                            if (compareItem != null)
                            {
                                //医保标记
                                switch (compareItem.CenterItem.ItemGrade)
                                {
                                    case "1":
                                        this.spread1_Sheet1.Rows[i].Label = "甲";
                                        this.spread1_Sheet1.RowHeader.Rows[i].Font = font_Bold;
                                        this.spread1_Sheet1.RowHeader.Rows[i].ForeColor = Color.Red;
                                        break;
                                    case "2":
                                        this.spread1_Sheet1.Rows[i].Label = "乙";
                                        this.spread1_Sheet1.RowHeader.Rows[i].Font = font_Bold;
                                        this.spread1_Sheet1.RowHeader.Rows[i].ForeColor = Color.Red;
                                        break;
                                    default:
                                        this.spread1_Sheet1.Rows[i].Label = i.ToString();
                                        this.spread1_Sheet1.RowHeader.Rows[i].Font = font_Regular;
                                        this.spread1_Sheet1.RowHeader.Rows[i].ForeColor = this.spread1_Sheet1.ColumnHeader.Columns[0].ForeColor;
                                        break;
                                }
                            }

                        }
                    }

                    #endregion
                }

                FS.HISFC.Models.Order.Order ord = null;
                this.ShowGroupDetail(spread1_Sheet1.ActiveRowIndex, ref ord);

                //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Init" + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ArrayList Orders
        {
            get
            {
                ArrayList alOrders = new ArrayList();
                string comboID = "";
                string newComboID = "";
                FS.HISFC.BizLogic.Order.Order ordermanager = new FS.HISFC.BizLogic.Order.Order();
                FS.HISFC.BizLogic.Manager.OrderType orderType = new FS.HISFC.BizLogic.Manager.OrderType();
                //FS.FrameWork.Public.ObjectHelper objecthelper = new FS.FrameWork.Public.ObjectHelper(orderType.GetList());
                string err = "";
                for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
                {
                    if (this.spread1_Sheet1.Cells[i, 0].Text.ToUpper() == "TRUE")
                    {
                        if (this.InpatientType == FS.HISFC.Models.Base.ServiceTypes.I)
                        {
                            FS.HISFC.Models.Order.Inpatient.Order order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Inpatient.Order;

                            if (order.ExtendFlag2 == "N")
                                continue;
                            order.User01 = "";
                            order.User02 = "";
                            order.ID = "";
                            if (string.IsNullOrEmpty(order.Combo.ID) || string.IsNullOrEmpty(comboID) || 
                                order.Combo.ID != comboID)//新的
                            {
                                comboID = order.Combo.ID;
                                newComboID = ordermanager.GetNewOrderComboID();
                                order.Combo.ID = newComboID;
                            }
                            else if (order.Combo.ID == comboID)
                            {
                                order.Combo.ID = newComboID;
                            }
                            string strOrderName = order.Name;
                            //填充项目基本信息  Add By liangjz 2005-08
                            //if (order.Item.IsPharmacy)

                            if (!this.isEditGroup)
                            {
                                if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                                {
                                    if (orderIntergrate.FillPharmacyItem(ref order, out err) == -1)
                                    {
                                        MessageBox.Show("获得药品信息出错！" + string.Format("错误原因，［{0}］药品可能已经停用！", strOrderName));
                                        return null;
                                    }
                                }
                                else
                                {
                                    if (orderIntergrate.FillFeeItem(ref order, out err) == -1)
                                    {
                                        MessageBox.Show("获得非药品信息出错！" + string.Format("错误原因，［{0}］非药品可能已经停用！", strOrderName));
                                        return null;
                                    }
                                }
                            }

                            order.OrderType = SOC.HISFC.BizProcess.Cache.Order.GetOrderType(order.OrderType.ID);

                            if (isReGetExecDept.Substring(0,1) == "1")
                            {
                                //全院组套，执行科室默认为空，在医生站重取 houwb 2011-6-14
                                if (currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.All)
                                {
                                    order.ExeDept = new FS.FrameWork.Models.NeuObject();
                                }
                            }
                            try
                            {
                                if (isReGetExecDept.Substring(1, 1) == "1")
                                {
                                    if (currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)
                                    {
                                        order.ExeDept = new FS.FrameWork.Models.NeuObject();
                                    }
                                }
                            }
                            catch
                            { }

                            try
                            {
                                if (isReGetExecDept.Substring(2, 1) == "1")
                                {
                                    if (currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Doctor)
                                    {
                                        order.ExeDept = new FS.FrameWork.Models.NeuObject();
                                    }
                                }
                            }
                            catch { }

                            alOrders.Add(order);
                        }
                        else
                        {
                            FS.HISFC.Models.Order.OutPatient.Order order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.OutPatient.Order;
                            if (order.ExtendFlag2 == "N")
                                continue;
                            order.User01 = "";
                            order.User02 = "";
                            order.ID = "";
                            if (order.Combo.ID != "" && order.Combo.ID != comboID)//新的
                            {
                                comboID = order.Combo.ID;
                                newComboID = ordermanager.GetNewOrderComboID();
                                order.Combo.ID = newComboID;
                            }
                            else if (order.Combo.ID == comboID)
                            {
                                order.Combo.ID = newComboID;
                            }
                            //填充项目基本信息
                            //if (order.Item.IsPharmacy)
                            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (order.Item.ID == "999")
                                {
                                    ((FS.HISFC.Models.Pharmacy.Item)order.Item).Type.ID = order.Item.SysClass.ID.ToString().Substring(order.Item.SysClass.ID.ToString().Length - 1, 1);
                                }
                                else
                                {
                                    FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                                    item = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);
                                    //item = pManagement.GetItem(order.Item.ID);
                                    if (item == null || item.IsStop)
                                    {
                                        MessageBox.Show("获得非药品信息出错！" + string.Format("错误原因，［{0}］药品可能已经停用！", order.Item.Name));
                                        return null;
                                    }
                                    else
                                    {
                                        order.Item.MinFee = item.MinFee;
                                        order.Item.Price = item.Price;
                                        order.Item.Name = item.Name;
                                        order.Item.SysClass = item.SysClass.Clone();//付给系统类别
                                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).IsAllergy = item.IsAllergy;
                                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).PackUnit = item.PackUnit;
                                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).MinUnit = item.MinUnit;
                                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).BaseDose = item.BaseDose;
                                        ((FS.HISFC.Models.Pharmacy.Item)order.Item).DosageForm = item.DosageForm;
                                        if (order.Unit == item.MinUnit)
                                        {
                                            order.MinunitFlag = "1";
                                        }
                                        else
                                        {
                                            order.MinunitFlag = "0";
                                        }
                                    }
                                    #region {DC0E8BDB-D918-4c14-8474-3D2E6F986A57}
                                    //if (item.SysClass.ID.ToString() == "PCC")
                                    //{
                                    //    order.Qty = order.Qty * order.HerbalQty;
                                    //}
                                    #endregion
                                }
                            }
                            else
                            {
                                if (order.Item.ID != "999")
                                {
                                    //FS.HISFC.Models.Fee.Item.Undrug item = itemManagement.GetValidItemByUndrugCode(order.Item.ID);
                                    FS.HISFC.Models.Fee.Item.Undrug item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(order.Item.ID);
                                    if (item == null || !item.IsValid)
                                    {
                                        MessageBox.Show("获得非药品信息出错！" + string.Format("错误原因，［{0}］非药品可能已经停用！", order.Item.Name));
                                        return null;
                                    }
                                    else
                                    {
                                        ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = item.IsNeedConfirm;
                                        order.Item.Price = item.Price;
                                        order.Item.MinFee = item.MinFee;
                                        order.Item.SysClass = item.SysClass.Clone();
                                    }
                                }
                            }

                            if (isReGetExecDept.Substring(0, 1) == "1")
                            {
                                //全院组套，执行科室默认为空，在医生站重取 houwb 2011-6-14
                                if (currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.All)
                                {
                                    order.ExeDept = new FS.FrameWork.Models.NeuObject();
                                }
                            }

                            try
                            {
                                if (isReGetExecDept.Substring(1, 1) == "1")
                                {
                                    if (currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)
                                    {
                                        order.ExeDept = new FS.FrameWork.Models.NeuObject();
                                    }
                                }
                            }
                            catch { }

                            try
                            {
                                if (isReGetExecDept.Substring(2, 1) == "1")
                                {
                                    if (currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Doctor)
                                    {
                                        order.ExeDept = new FS.FrameWork.Models.NeuObject();
                                    }
                                }
                            }
                            catch { }

                            alOrders.Add(order);
                        }
                    }
                }
                return alOrders;
            }
        }

        #endregion

        #region 事件



        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.spread1_Sheet1.Columns[0].CellType.GetType() != typeof(FarPoint.Win.Spread.CellType.CheckBoxCellType))
            {
                return;
            }

            FS.HISFC.Models.Order.Order order = null;
            //全部选中
            for (int i = 0; i < spread1_Sheet1.RowCount; i++)
            {
                order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;
                if (order == null)
                {
                    continue;
                }
                else if (order.ExtendFlag2 == "N")
                {

                }
                else
                {
                    spread1_Sheet1.SetValue(i, 0, this.chkAll.Checked, false);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.IsShowDetailDialog == 0)
            {
                this.Hide();

                if (this.GroupConfirm != null)
                {
                    this.GroupConfirm();
                }
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void ShowGroupDetail(int rowIndex, ref FS.HISFC.Models.Order.Order order)
        {
            order = this.spread1_Sheet1.Rows[rowIndex].Tag as FS.HISFC.Models.Order.Order;
            if (order == null)
            {
                return;
            }

            #region 查询复合项目明细

            this.neuSpread1_Sheet1.RowCount = 0;

            List<FS.HISFC.Models.Fee.Item.UndrugComb> lstzt = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();

            lstzt = SOC.HISFC.BizProcess.Cache.Fee.GetUndrugZTDetail(order.ID);
            if (lstzt == null)
            {
                lstzt = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();
                if (this.ztManager.QueryUnDrugztDetail(order.ID, ref lstzt) == -1)
                {
                    MessageBox.Show(this.ztManager.Err);
                    return;
                }
            }
            if (lstzt != null && lstzt.Count != 0)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = null;
                FS.HISFC.Models.Pharmacy.Item phaItem = null;

                FS.HISFC.Models.SIInterface.Compare compareItem = null;
                Font font_Bold = new Font(this.neuSpread1.Font.FontFamily, this.neuSpread1.Font.Size, FontStyle.Bold);
                Font font_Regular = new Font(this.neuSpread1.Font.FontFamily, this.neuSpread1.Font.Size, FontStyle.Regular);

                for (int i = 0; i < lstzt.Count; i++)
                {
                    this.neuSpread1_Sheet1.Rows.Add(0, 1);

                    if (lstzt[i].ID.Substring(0, 1) == "Y")
                    {
                        //phaItem = pManagement.GetItem(lstzt[i].ID);
                        //if (phaItem == null)
                        //{
                        //    MessageBox.Show(pManagement.Err);
                        //    return;
                        //}
                        phaItem = SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(lstzt[i].ID);

                        this.neuSpread1_Sheet1.Cells[0, 0].Text = phaItem.UserCode;

                        this.neuSpread1_Sheet1.Cells[0, 1].Value = string.IsNullOrEmpty(phaItem.Specs) ?
                           (lstzt[i].Name + "【" + phaItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】")
                            : ("【" + phaItem.Specs + "】" + lstzt[i].Name + "【" + phaItem.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】");
                    }
                    else
                    {
                        //item = itemManagement.GetItemByUndrugCode(lstzt[i].ID);
                        //if (item == null)
                        //{
                        //    MessageBox.Show(itemManagement.Err);
                        //    return;
                        //}
                        item = SOC.HISFC.BizProcess.Cache.Fee.GetItem(lstzt[i].ID);

                        this.neuSpread1_Sheet1.Cells[0, 0].Text = item.UserCode;

                        this.neuSpread1_Sheet1.Cells[0, 1].Value = string.IsNullOrEmpty(item.Specs) ?
                            (lstzt[i].Name + "【" + item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】") :
                            ("【" + item.Specs + "】" + lstzt[i].Name + "【" + item.Price.ToString("F4").TrimEnd('0').TrimEnd('.') + "元】");
                    }

                    this.neuSpread1_Sheet1.Cells[0, 2].Value = lstzt[i].Qty;
                    this.neuSpread1_Sheet1.Cells[0, 3].Value = true;

                    if (lstzt[i].ValidState != "1" && lstzt[i].ValidState.Trim() != "有效")
                    {
                        this.neuSpread1_Sheet1.Cells[0, 3].Value = false;
                        this.neuSpread1_Sheet1.Rows[0].ForeColor = Color.Red;
                    }

                    #region 显示医保等级信息

                    //不显示医保等级【私立医院不显示医保等级；但是公立医院显示医保等级；暂时不显示】
                    if (false && pact != null && !string.IsNullOrEmpty(pact.ID))
                    {
                        string strCompareInfo = string.Empty;
                        if (Classes.Function.IItemCompareInfo.GetCompareItemInfo(item, pact, ref compareItem, ref strCompareInfo) == -1)
                        {
                            this.neuSpread1_Sheet1.Cells[0, 4].Text = "";
                            this.neuSpread1_Sheet1.Cells[0, 4].ForeColor = Color.Black;
                        }
                        else
                        {
                            if (compareItem != null)
                            {
                                //医保标记
                                switch (compareItem.CenterItem.ItemGrade)
                                {
                                    case "1":
                                        this.neuSpread1_Sheet1.Cells[0, 4].Text = "甲";
                                        this.neuSpread1_Sheet1.Cells[0, 4].ForeColor = Color.Red;
                                        break;
                                    case "2":
                                        this.neuSpread1_Sheet1.Cells[0, 4].Text = "乙";
                                        this.neuSpread1_Sheet1.Cells[0, 4].ForeColor = Color.Red;
                                        break;
                                    default:
                                        this.neuSpread1_Sheet1.Cells[0, 4].Text = "";
                                        this.neuSpread1_Sheet1.Cells[0, 4].ForeColor = Color.Black;
                                        break;
                                }
                            }
                        }
                    }

                    #endregion
                }
            }
            #endregion
        }

        private void spread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            FS.HISFC.Models.Order.Order order = null;

            this.ShowGroupDetail(e.Row, ref order);

            if (e.Column == 0)
            {
                if (order.ExtendFlag2 == "N")
                {
                    MessageBox.Show("停用或缺药项目，不允许勾选！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.neuSpread1_Sheet1.Cells[e.Row, e.Column].Value = false;
                }
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            #region 组套修改判断

            if (this.currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)
            {
                string sysClass = "groupManager";
                string error = "";

                FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

                int ret = docAbility.CheckPopedom(docAbility.Operator.ID, "DEPT", sysClass, false, ref error);

                if (ret <= 0)
                {
                    MessageBox.Show(error);
                    return;
                }
            }

            if (this.currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.All)
            {
                string sysClass = "groupManager";
                string error = "";

                FS.HISFC.BizLogic.Order.Medical.Ability docAbility = new FS.HISFC.BizLogic.Order.Medical.Ability();

                int ret = docAbility.CheckPopedom(docAbility.Operator.ID, "ALL", sysClass, false, ref error);

                if (ret <= 0)
                {
                    MessageBox.Show(error);
                    return;
                }
            }

            #endregion

            //删除选中行
            string itemName = "";
            for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
            {
                //if (spread1_Sheet1.IsSelected(i, 0))
                if (this.spread1_Sheet1.Cells[i, 0].Text.ToUpper() == "TRUE")
                {
                    FS.HISFC.Models.Order.Order order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;
                    itemName += order.Item.Name + (string.IsNullOrEmpty(order.Item.Specs) ? "" : "[" + order.Item.Specs + "]") + "\r\n";
                }
            }

            if (MessageBox.Show("是否删除以下项目:\r\n\r\n" + itemName + "\r\n删除之后不可恢复，是否继续？", "警告", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
            {
                //if (spread1_Sheet1.IsSelected(i, 0))
                if (this.spread1_Sheet1.Cells[i, 0].Text.ToUpper() == "TRUE")
                {
                    FS.HISFC.Models.Order.Order order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;

                    this.groupManager.DeleteSingleOrder(order.User01);
                }
            }

            MessageBox.Show("删除成功！");

            this.neuSpread1_Sheet1.RowCount = 0;

            Init(this.currentGroup);
        }

        /// <summary>
        /// 记录是否默认全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxIsSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.All)
            {
                if (!this.isHaveAllEditPower)
                {
                    MessageBox.Show("您没有全院组套修改权限！\r\n如有疑问请联系信息科！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            else if (this.currentGroup.Kind == FS.HISFC.Models.Base.GroupKinds.Dept)
            {
                if (!this.isHaveDeptEditPower)
                {
                    MessageBox.Show("您没有当前科室组套修改权限！\r\n如有疑问请联系信息科！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            if (this.groupManager.UpdateIsSelectAll(this.currentGroup.ID, this.cbxIsSelectAll.Checked) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("更新是否全选标记出错！" + this.groupManager.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
        }

        private void spread1_ButtonClicked(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
            if (e.Column == 0)
            {
                FS.HISFC.Models.Order.Order tmpOrder = spread1_Sheet1.Rows[e.Row].Tag as FS.HISFC.Models.Order.Order;
                string combNo = "";
                if (tmpOrder != null)
                {
                    combNo = tmpOrder.Combo.ID;

                    //默认全部选中
                    for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
                    {
                        tmpOrder = spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;
                        if (tmpOrder != null && tmpOrder.Combo.ID == combNo && i != e.Row)
                        {
                            spread1_Sheet1.Cells[i, 0].Value = spread1_Sheet1.Cells[e.Row, 0].Value;
                        }
                    }
                }
            }
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            int index = this.spread1_Sheet1.ActiveRowIndex;
            for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
            {
                this.spread1_Sheet1.Cells[i, 5].Text = string.Empty;
            }
            if (index > 0)
            {
                string comboDown = this.spread1_Sheet1.Cells[index, 2].Text;
                string comboUp = this.spread1_Sheet1.Cells[index - 1, 2].Text;
                int j = 0;

                #region 移动
                if (this.spread1_Sheet1.Cells[index, 2].Text != this.spread1_Sheet1.Cells[index - 1, 2].Text)
                {

                    //下面的上移
                    for (int i = index; i < this.spread1_Sheet1.RowCount; i++)
                    {
                        if (comboDown == this.spread1_Sheet1.Cells[i, 2].Text)
                        {

                            this.spread1_Sheet1.MoveRow(i, index - 1, true);
                            j++;

                        }
                    }


                    //上面的下移
                    int length = index - 1;
                    for (int i = 0; i <= length; i++)
                    {
                        if (comboUp == this.spread1_Sheet1.Cells[i, 2].Text)
                        {
                            this.spread1_Sheet1.MoveRow(i, index-1+j, true);
                            i--;
                            length--;

                        }
                    }
                }
                else
                {
                    this.spread1_Sheet1.MoveRow(index, index - 1, true);
                }
                #endregion

                for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
                {
                    if (this.spread1_Sheet1.Cells[i, 2].Text == comboDown)
                    {
                        this.spread1_Sheet1.ActiveRowIndex = i;
                        break;
                    }
                    
                }

            }

            FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.spread1_Sheet1, 2, 5);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            int index = this.spread1_Sheet1.ActiveRowIndex;

            for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
            {
                this.spread1_Sheet1.Cells[i, 5].Text = string.Empty;
            }

            if (index < this.spread1_Sheet1.RowCount - 1)
            {
                index = index + 1;
                string comboDown = this.spread1_Sheet1.Cells[index, 2].Text;
                string comboUp = this.spread1_Sheet1.Cells[index - 1, 2].Text;
                int j = 0;

                #region 移动
                if (this.spread1_Sheet1.Cells[index, 2].Text != this.spread1_Sheet1.Cells[index - 1, 2].Text)
                {

                    //下面的上移
                    for (int i = index; i < this.spread1_Sheet1.RowCount; i++)
                    {
                        if (comboDown == this.spread1_Sheet1.Cells[i, 2].Text)
                        {

                            this.spread1_Sheet1.MoveRow(i, index - 1, true);
                            j++;

                        }
                    }


                    //上面的下移
                    int length = index - 1;
                    for (int i = 0; i <= length; i++)
                    {
                        if (comboUp == this.spread1_Sheet1.Cells[i, 2].Text)
                        {
                            this.spread1_Sheet1.MoveRow(i, index - 1 + j, true);
                            i--;
                            length--;

                        }
                    }
                }
                else
                {
                    this.spread1_Sheet1.MoveRow(index, index - 1, true);
                }
                #endregion

                for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
                {
                    if (this.spread1_Sheet1.Cells[i, 2].Text == comboUp)
                    {
                        this.spread1_Sheet1.ActiveRowIndex = i;
                        break;
                    }

                }
            }

            FS.HISFC.Components.Common.Classes.Function.DrawCombo(this.spread1_Sheet1, 2, 5);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            for (int i = 0; i < this.spread1_Sheet1.RowCount; i++)
            {
                FS.HISFC.Models.Order.Order order = this.spread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Order.Order;
                if (groupManager.UpdateGroupDetailSortID(this.currentGroup.ID, order.User01, i) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("更新序号失败！");
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("更新成功！");
        }

        #endregion

       
    }
}
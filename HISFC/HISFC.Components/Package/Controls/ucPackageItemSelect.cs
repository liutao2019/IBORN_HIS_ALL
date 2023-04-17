using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Controls
{
    public delegate void SelectItemDelegate(FS.HISFC.Models.Base.Item item);

    public delegate void ModifyDetailDelegate(FS.HISFC.Models.MedicalPackage.PackageDetail detail,EnumDetailFieldList field);

    public partial class ucPackageItemSelect : UserControl
    {

        /// <summary>
        /// 右下角气泡
        /// </summary>
        protected ToolTip tooltip = new ToolTip();

        /// <summary>
        /// 进度条当前进度,用于加载项目时显示进度条
        /// </summary>
        int progress = 1;

        /// <summary>
        /// 当前选中项目包
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentChildPackage = null;

        /// <summary>
        /// 当前编辑的明细
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.PackageDetail currentDetail = null;

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package CurrentChildPackage
        {
            get { return this.currentChildPackage; }
            set
            {
                this.currentChildPackage = (value == null) ? null : value.Clone(); ;
                this.currentDetail = null;
                this.clear();
            }
        }

        /// <summary>
        /// 当前编辑的明细
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.PackageDetail CurrentDetail
        {
            get { return this.currentDetail; }
            set 
            {
                this.currentDetail = (value == null) ? null : value.Clone(); ;
            }
        }

        /// <summary>
        /// 项目选择器选择项目后调用代理函数
        /// </summary>
        public SelectItemDelegate SetNewDetail;
        
        /// <summary>
        /// 修改函数
        /// </summary>
        public ModifyDetailDelegate ModifyDetail;

        /// <summary>
        /// 套餐项目选择控件
        /// </summary>
        public ucPackageItemSelect()
        {
            InitializeComponent();
            Init();
        }

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

            #region 设置tip
            tooltip.SetToolTip(this.ucInputItem1, "输入拼音码查询，增加套餐项目(ESC取消列表)");
            tooltip.SetToolTip(this.txtQTY, "输入总数量(回车输入结束)");
            #endregion
            try
            {
                this.ucInputItem1.DeptCode = "";//科室看到全部项目

                this.ucInputItem1.ShowCategory = FS.HISFC.Components.Common.Controls.EnumCategoryType.SysClass;
                this.ucInputItem1.UndrugApplicabilityarea = FS.HISFC.Components.Common.Controls.EnumUndrugApplicabilityarea.All;

                //发药类型：O 门诊处方、I 住院医嘱、A 全部
                this.ucInputItem1.DrugSendType = "A";
                //{0FF4B806-1507-4cfa-A269-6FBA9B044473}
                this.ucInputItem1.ShowItemType = FS.HISFC.Components.Common.Controls.EnumShowItemType.Undrug;
                this.ucInputItem1.IsPackageInput = true;

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载项目列表信息..", 0, false);
                Application.DoEvents();

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(progress, 100);
                Application.DoEvents();

                this.ucInputItem1.Init();//初始化项目列表

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();


                this.ucInputItem1.Focus();
            }
            catch { }

            this.AddEvent();
        }

        /// <summary>
        /// 清楚界面信息显示
        /// </summary>
        private void clear()
        {
            this.DeleteEvent();
            this.ucInputItem1.Clear();
            this.cmbUnit.Items.Clear();
            this.txtMemo.Text = "";
            this.AddEvent();
        }

        /// <summary>
        /// 增加事件
        /// </summary>
        private void AddEvent()
        {
            DeleteEvent();

            //单位
            this.cmbUnit.KeyPress += new KeyPressEventHandler(cmbUnit_KeyPress);
            this.cmbUnit.SelectedIndexChanged += new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.Leave += new EventHandler(cmbUnit_Leave);

            //数量
            this.txtQTY.KeyPress += new KeyPressEventHandler(txtQTY_KeyPress);
            this.txtQTY.ValueChanged += new EventHandler(txtQTY_ValueChanged);
            this.txtQTY.Leave += new EventHandler(txtQTY_Leave);

            //备注
            this.txtMemo.KeyPress += new KeyPressEventHandler(txtMemo_KeyPress);

            //选择项目事件
            this.ucInputItem1.CatagoryChanged += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);
            this.ucInputItem1.SelectedItem += new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
        }

        /// <summary>
        /// 删除事件
        /// </summary>
        private void DeleteEvent()
        {
            //单位
            this.cmbUnit.Leave -= new EventHandler(this.cmbUnit_Leave);
            this.cmbUnit.SelectedIndexChanged -= new EventHandler(cmbUnit_SelectedIndexChanged);
            this.cmbUnit.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.cmbUnit_KeyPress);

            //数量
            this.txtQTY.ValueChanged -= new System.EventHandler(this.txtQTY_ValueChanged);
            this.txtQTY.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.txtQTY_KeyPress);
            this.txtQTY.Leave -= new EventHandler(txtQTY_Leave);

            //选择项目事件
            this.ucInputItem1.SelectedItem -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_SelectedItem);
            this.ucInputItem1.CatagoryChanged -= new FS.FrameWork.WinForms.Forms.SelectedItemHandler(ucInputItem1_CatagoryChanged);

            //备注
            this.txtMemo.KeyPress -= new KeyPressEventHandler(txtMemo_KeyPress);
        }

        #region 事件

        void cmbUnit_Leave(object sender, EventArgs e)
        {
            this.cmbUnit_SelectedIndexChanged(sender, e);
        }

        void cmbUnit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e == null || e.KeyChar == 13)
            {
                this.txtMemo.Focus();
            }
        }

        void cmbUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentDetail == null)
            {
                return;
            }

            if (this.currentDetail.Unit != this.cmbUnit.Text)
            {
                #region 判断是否是最小单位

                if (this.currentDetail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    if (this.cmbUnit.SelectedIndex == 0)
                    {
                        this.currentDetail.UnitFlag = "0";
                    }
                    else
                    {
                        this.currentDetail.UnitFlag = "1";
                    }
                }
                #endregion

                this.currentDetail.Unit = this.cmbUnit.Text;//更新单位


                this.ModifyDetail(this.currentDetail, EnumDetailFieldList.Unit);
            }
        }

        void txtQTY_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.currentDetail == null)
            {
                return;
            }

            if (e == null || e.KeyChar == 13)
            {
                this.txtMemo.Focus();
            }
        }

        void txtQTY_ValueChanged(object sender, EventArgs e)
        {
            if (this.currentDetail == null)
            {
                return;
            }

            if (this.txtQTY.Value == 0 || (int)this.txtQTY.Value < this.txtQTY.Value)
            {
                MessageBox.Show("总量输入错误,请输入大于零的整数！");
                if (this.txtQTY.Value == 0)
                {
                    this.txtQTY.Value = 1;
                }
                else
                {
                    this.txtQTY.Value = (int)this.txtQTY.Value;
                }
            }

            if (this.txtQTY.Value == this.currentDetail.Item.Qty)
            {
                return;
            }

            this.currentDetail.Item.Qty = this.txtQTY.Value;

            this.ModifyDetail(this.currentDetail, EnumDetailFieldList.Qty);
        }

        void txtMemo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.currentDetail == null)
            {
                return;
            }

            this.currentDetail.Memo = this.txtMemo.Text;
            if (e == null || e.KeyChar == 13)
            {
                this.ModifyDetail(this.currentDetail, EnumDetailFieldList.Memo);
                this.ucInputItem1.Focus();
            }
        }

        void txtQTY_Leave(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void ucInputItem1_CatagoryChanged(FS.FrameWork.Models.NeuObject sender)
        {
            //throw new NotImplementedException();
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


                if (this.currentDetail != null && (this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item).ID == this.currentDetail.Item.ID) //不重复
                {
                    this.txtQTY.Focus();
                    this.txtQTY.Select(0, this.txtQTY.Text.Length);
                    return;
                }

                this.txtQTY.ValueChanged -= new System.EventHandler(this.txtQTY_ValueChanged);
                this.txtMemo.KeyPress -= new KeyPressEventHandler(txtMemo_KeyPress);
                this.txtQTY.Text = "0";
                this.txtMemo.Text = "";
                this.txtQTY.ValueChanged += new System.EventHandler(this.txtQTY_ValueChanged);
                this.txtMemo.KeyPress += new KeyPressEventHandler(txtMemo_KeyPress);


                FS.HISFC.Models.Base.Item item = this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item;
                this.currentDetail = new FS.HISFC.Models.MedicalPackage.PackageDetail();

                if (item.ID == "999")//自己录的项目
                {
                    this.currentDetail.Item = item;
                }

                //药品
                if (item.GetType() == typeof(FS.HISFC.Models.Pharmacy.Item))
                {
                    //this.order.Item = pharmacyManager.GetItem(this.ucInputItem1.FeeItem.ID);
                    this.currentDetail.Item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(item.ID);
                    this.currentDetail.Item.User01 = item.User01;
                    this.currentDetail.Item.User02 = item.User02;//传递取药药房
                    this.currentDetail.Item.User03 = item.User03;
                }
                else
                {
                    if (item.PriceUnit != "[复合项]")
                    {
                        this.currentDetail.Item = FS.SOC.HISFC.BizProcess.Cache.Fee.GetItem(item.ID);
                    }
                    else
                    {
                        this.currentDetail.Item = item;
                    }
                }

                this.PresentDetail();

                //设置新明细
                this.SetNewDetail(this.ucInputItem1.FeeItem as FS.HISFC.Models.Base.Item);

                this.txtQTY.Focus();
                this.txtQTY.Select(0, this.txtQTY.Text.Length);
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

        public int PresentDetail()
        {
            this.clear();
            if (this.currentDetail == null)
                return -1;
            FS.HISFC.BizLogic.Fee.Item itemMgr = new FS.HISFC.BizLogic.Fee.Item();
            FS.HISFC.BizProcess.Integrate.Pharmacy itemIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
            decimal tmpQTY = this.currentDetail.Item.Qty;

            if (this.currentDetail.Item.SysClass.ID.ToString().Equals("P") ||
               this.currentDetail.Item.SysClass.ID.ToString().Equals("PCC") ||
               this.currentDetail.Item.SysClass.ID.ToString().Equals("PCZ"))
            {
                this.currentDetail.Item = itemIntegrate.GetItem(this.currentDetail.Item.ID);
            }
            else
            {
                this.currentDetail.Item = itemMgr.GetUndrugByCode(this.currentDetail.Item.ID);
            }

            this.currentDetail.Item.Qty = tmpQTY;

            //删除触发函数
            this.DeleteEvent();

            if (this.currentDetail.Item.Qty == 0)
            {
                this.currentDetail.Item.Qty = 1;
            }

            this.txtQTY.Text = this.currentDetail.Item.Qty.ToString();
            this.txtMemo.Text = this.currentDetail.Memo;

            this.cmbUnit.Items.Clear();

            if (this.currentDetail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item item = ((FS.HISFC.Models.Pharmacy.Item)this.currentDetail.Item);
                if (this.currentDetail.Item.ID != "999") //自定义药品
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

                this.cmbUnit.Items.Add((this.currentDetail.Item as FS.HISFC.Models.Pharmacy.Item).MinUnit);//单位 
                this.cmbUnit.Items.Add((this.currentDetail.Item as FS.HISFC.Models.Pharmacy.Item).PackUnit);//单位
                this.cmbUnit.DropDownStyle = ComboBoxStyle.DropDownList;//只能选择

                if (this.currentDetail.Unit == null || this.currentDetail.Unit.Trim() == "")
                {
                    if (this.cmbUnit.Items.Count > 0)
                    {
                        this.cmbUnit.SelectedIndex = 0;
                        this.currentDetail.Unit = this.cmbUnit.Text;
                        this.currentDetail.UnitFlag = "0";
                        (this.currentDetail.Item.Qty * this.currentDetail.Item.Price / this.currentDetail.Item.PackQty).ToString();
                    }
                }
                else
                {
                    this.cmbUnit.Text = this.currentDetail.Unit;
                }

                this.currentDetail.Unit = this.cmbUnit.Text;
            }
            else if (this.currentDetail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Fee.Item.Undrug item = (FS.HISFC.Models.Fee.Item.Undrug)this.currentDetail.Item;
                this.cmbUnit.Items.Clear();

                if (this.currentDetail.Unit == null || this.currentDetail.Unit.Trim() == "")
                {
                    string unit = ((FS.HISFC.Models.Fee.Item.Undrug)this.currentDetail.Item).PriceUnit;
                    if (unit == null || unit == "") unit = "次";
                    this.cmbUnit.Items.Add(unit);
                    if (this.cmbUnit.Items.Count > 0)
                    {
                        this.cmbUnit.SelectedIndex = 0;
                        this.currentDetail.Unit = this.cmbUnit.Text;
                        this.currentDetail.UnitFlag = "0";
                    }
                }
                else
                {
                    this.cmbUnit.Items.Add(this.currentDetail.Unit);
                    this.cmbUnit.Text = this.currentDetail.Unit;
                }
            }

            this.ucInputItem1.FeeItem = this.currentDetail.Item;

            //触发事件添加
            this.AddEvent();


            return 1;
        }
        #endregion
    }

    /// <summary>
    /// 明细变化类型
    /// </summary>
    public enum EnumDetailFieldList
    {
        /// <summary>
        /// 计价数量
        /// </summary>
        Qty = 0,

        /// <summary>
        /// 计价单位
        /// </summary>
        Unit = 1,

        /// <summary>
        /// 备注
        /// </summary>
        Memo = 2,

    }
}

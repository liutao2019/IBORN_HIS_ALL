using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FS.FrameWork.WinForms.Controls;
using FS.FrameWork.WinForms.Forms;
namespace HISFC.Components.Package.Controls
{
    public partial class ucPackageManage : ucBaseControl
    {

        /// <summary>
        /// 工具条
        /// </summary>
        protected ToolBarService _toolBarService = new ToolBarService();

        /// <summary>
        /// 套餐维护业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 控制参数业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 右下角气泡
        /// </summary>
        protected ToolTip tooltip = new ToolTip();
       
        string edpitEmps = "";

        /// <summary>+        /// 当前登录人
        /// </summary>
        FS.HISFC.Models.Base.Employee currEmp = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentPackage = null;

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package CurrentPackage
        {
            get { return this.currentPackage; }
            set
            {
                this.currentPackage = (value == null) ? null : value.Clone(); ;
                this.ucPackgeDetail1.SetPackageInfo(this.currentPackage);
            }
        }

        /// <summary>
        /// 套餐类别
        /// </summary>
        private ArrayList categoryList = null;

        /// <summary>
        /// 套餐类别
        /// </summary>
        public ArrayList CategoryList
        {
            get { return this.categoryList; }
            set
            {
                this.categoryList = value;
            }
        }

        /// <summary>
        /// 套餐范围
        /// </summary>
        private ArrayList rangeList = null;

        /// <summary>
        /// 套餐范围
        /// </summary>
        public ArrayList RangeList
        {
            get { return rangeList; }
            set { rangeList = value; }
        }

        /// <summary>
        /// 套餐状态
        /// </summary>
        private ArrayList statusList = null;

        /// <summary>
        /// 套餐状态
        /// </summary>
        public ArrayList StatusList
        {
            get { return statusList; }
            set { statusList = value; }
        }

        /// <summary>
        /// 套餐修改对话框
        /// </summary>
        private Forms.frmPackage packageForm = new HISFC.Components.Package.Forms.frmPackage();


        /// <summary>
        /// 是否在编辑模式
        /// </summary>
        private bool editMode = false;

        /// <summary>
        /// 默认是否显示作废的套餐
        /// </summary>
        private bool showInvalid;

        /// <summary>
        /// 是否显示作废的套餐
        /// </summary>
        [Description("是否显示作废的套餐")]
        public bool ShowInvalid
        {
            get { return this.showInvalid; }
            set
            {
                this.showInvalid = value;
            }
        }

        /// <summary>
        /// 默认允许修改已作废的套餐
        /// </summary>
        private bool isAllowModifyInvalidPackage;

        /// <summary>
        /// 是否允许修改作废的套餐
        /// </summary>
        [Description("是否允许修改作废的套餐")]
        public bool IsAllowModifyInvalidPackage
        {
            get { return this.isAllowModifyInvalidPackage; }
            set
            {
                this.isAllowModifyInvalidPackage = value;
            }
        }

        public ucPackageManage()
        {
            InitializeComponent();
            initControl();
        }

        /// <summary>
        /// 初始化分类树
        /// </summary>
        private void initControl()
        {
            try
            {
                setControlsVisible();
                this.chkIsShowInvalid.Checked = this.showInvalid;
                initCategoryTree();
                initPackageDic();
                initPacakgeTree();
                this.chkIsShowInvalid.CheckedChanged += new EventHandler(chkIsShowInvalid_CheckedChanged);
                this.packageForm.RefreshPackageInfo = this.RefreshPackage;

                this.edpitEmps = this.controlParamIntegrate.GetControlParam<string>("MZ0205");

                currEmp = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 设置控件可见模式
        /// </summary>
        private void setControlsVisible()
        {
            this.pnlLeft.Visible = !editMode;
            if (editMode)
            {
                this._toolBarService.SetToolButtonEnabled("新建套餐", false);
                this._toolBarService.SetToolButtonEnabled("修改套餐", false);
                this._toolBarService.SetToolButtonEnabled("作废套餐", false);
                this._toolBarService.SetToolButtonEnabled("保存套餐", true);
                this._toolBarService.SetToolButtonEnabled("取消", true);
            }
            else
            {
                this._toolBarService.SetToolButtonEnabled("新建套餐", true);
                this._toolBarService.SetToolButtonEnabled("修改套餐", true);
                this._toolBarService.SetToolButtonEnabled("作废套餐", true);
                this._toolBarService.SetToolButtonEnabled("保存套餐", false);
                this._toolBarService.SetToolButtonEnabled("取消", false);
            }
        }

        private void initCategoryTree()
        {
            try
            {
                this.CategoryList = constantMgr.GetList("PACKAGETYPE");
                System.Windows.Forms.TreeNode rootNode = new TreeNode("全部", 0, 0);
                foreach (FS.HISFC.Models.Base.Const cst in categoryList)
                {
                    TreeNode node = new TreeNode(cst.Name,7,7);
                    node.Tag = cst;
                    rootNode.Nodes.Add(node);
                }
                this.tvCategoryNew.Nodes.Add(rootNode);
                this.tvCategoryNew.ExpandAll();
                this.tvCategoryNew.Nodes[0].EnsureVisible();

                this.tvCategoryNew.SelectedIndexChanged += new HISFC.Components.Package.Components.WTreeListWinEx.EventHandler(tvCategoryNew_SelectedIndexChanged);
            }
            catch (Exception ex)
            {
                throw new Exception("initCategoryTree发生错误："+ex.Message);
            }
        }

        private void initPackageDic()
        {
            this.RangeList = constantMgr.GetList("PACKAGERANGE");
            this.StatusList = constantMgr.GetList("PACKAGESTATUS");

        }

        private void initPacakgeTree()
        {
            this.tvPackageNew.SelectedIndexChanged += new HISFC.Components.Package.Components.WTreeListWinEx.EventHandler(tvPackageNew_SelectedIndexChanged);
        }

        /// <summary>
        /// 初始化工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            _toolBarService.AddToolButton("新建套餐", "新建套餐", (int)FS.FrameWork.WinForms.Classes.EnumImageList.R入库单, true, false, null);
            _toolBarService.AddToolButton("修改套餐", "修改套餐", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S申请单, true, false, null);

            _toolBarService.AddToolButton("修改项目包", "修改项目包", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手动录入, true, false, null);
            _toolBarService.AddToolButton("退出修改", "退出修改", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S手工录入取消, true, false, null);

            return _toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if ((e.ClickedItem.Text == "新建套餐" || e.ClickedItem.Text == "修改套餐" || e.ClickedItem.Text == "作废套餐") && this.editMode)
            {
                return;
            }

            if ((e.ClickedItem.Text == "保存套餐" || e.ClickedItem.Text == "取消") && !this.editMode)
            {
                return;
            }

            switch (e.ClickedItem.Text)
            {
                case "新建套餐":
                    this.PackageOperation(false);
                    break;
                case "修改套餐":
                    this.PackageOperation(true);
                    break;
                case "修改项目包":
                    if (this.ModifyPackage() >= 0)
                    {
                        this.SetEditMode(true);
                        this.ucPackgeDetail1.SetEditMode(true);
                    }
                    break;
                case "退出修改":
                    this.RefreshPackage(this.currentPackage);
                    this.SetEditMode(false);
                    this.ucPackgeDetail1.SetEditMode(false);
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 套餐操作
        /// </summary>
        /// <returns></returns>
        private void PackageOperation(bool isEdit)
        {

            if (packageForm == null)
            {
                packageForm = new HISFC.Components.Package.Forms.frmPackage();
                packageForm.RefreshPackageInfo = this.RefreshPackage;
            }

            if (isEdit)
            {
                if (this.CurrentPackage == null || this.CurrentPackage.PackageClass != "1")
                {
                    MessageBox.Show("请选择需要进行操作的套餐！");
                    return;
                }

                packageForm.CurrentPackage = this.CurrentPackage;
                packageForm.EditMode = isEdit;
            }

            packageForm.RangeList = this.RangeList;
            packageForm.CategoryList = this.CategoryList;
            packageForm.StatusList = this.StatusList;

            packageForm.ShowDialog();
        }

        /// <summary>
        /// 修改套餐
        /// </summary>
        /// <returns></returns>
        private int ModifyPackage()
        {
            string oprcode = currEmp.ID;

            if (!edpitEmps.Contains(oprcode))
            {
                MessageBox.Show("你没权限修改套餐！");
                return -1;
            }
            if (this.currentPackage == null)
            {
                MessageBox.Show("请选择要修改的套餐！");
                return -1;
            }

            if (this.currentPackage.IsValid == false && !this.IsAllowModifyInvalidPackage)
            {
                MessageBox.Show("该套餐已经是作废状态,无法修改");
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 设置编辑状态
        /// </summary>
        /// <param name="isEdit">是否编辑状态</param>
        public void SetEditMode(bool isEdit)
        {
            this.editMode = isEdit;
            this.setControlsVisible();
        }

        /// <summary>
        /// 刷新套餐
        /// </summary>
        /// <param name="packagecur"></param>
        private void RefreshPackage(FS.HISFC.Models.MedicalPackage.Package packagecur)
        {
            this.tvPackageNew.Nodes.Clear();
            this.tvPackageNew.Focus();
            ArrayList packageList = new ArrayList();
            if (this.tvCategoryNew.SelectedNode.Tag is string)
            {
                packageList = this.packageProcess.QueryParentPackage("ALL");
            }
            else if (this.tvCategoryNew.SelectedNode.Tag is FS.HISFC.Models.Base.Const)
            {
                FS.HISFC.Models.Base.Const cst = this.tvCategoryNew.SelectedNode.Tag as FS.HISFC.Models.Base.Const;
                if (cst != null)
                {
                    packageList = this.packageProcess.QueryParentPackage(cst.ID);
                }
            }

            if (packageList == null)
            {
                return;
            }

            foreach (FS.HISFC.Models.MedicalPackage.Package package in packageList)
            {
                TreeNode packageNode = new TreeNode(package.Name,7,7);
                packageNode.Tag = package;
                if (!package.IsValid )
                {
                    if (this.chkIsShowInvalid.Checked)
                    {
                        packageNode.ForeColor = Color.Red;
                        packageNode.Text = "[作废]" + package.Name;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    packageNode.ForeColor = Color.Black;
                }
                this.tvPackageNew.Nodes.Add(packageNode);

                if (packagecur != null)
                {
                    if (packagecur.ID == package.ID)
                    {
                        this.tvPackageNew.SelectedNode = packageNode;
                    }
                }
            }

            if (this.tvPackageNew.SelectedNode == null)
            {
                this.CurrentPackage = null;
            }
        }

        #region 触发函数

        /// <summary>
        /// 类别树刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvCategoryNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CurrentPackage = null;
            if (this.tvCategoryNew.SelectedNode == null)
            {
                return;
            }

            this.RefreshPackage(null);
        }

        /// <summary>
        /// 选择具体某一个套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvPackageNew_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tvPackageNew.SelectedNode != null)
            {
                this.CurrentPackage = (this.tvPackageNew.SelectedNode.Tag as FS.HISFC.Models.MedicalPackage.Package).Clone();
            }
        }

        /// <summary>
        /// 是否显示作废套餐
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkIsShowInvalid_CheckedChanged(object sender, EventArgs e)
        {
            this.RefreshPackage(this.CurrentPackage);
        }

        #endregion
    }
}

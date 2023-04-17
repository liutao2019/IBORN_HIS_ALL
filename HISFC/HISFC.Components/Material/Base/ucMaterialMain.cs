using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.WinForms.Forms;

namespace FS.HISFC.Components.Material.Base
{
    public partial class ucMaterialMain : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IPreArrange
    {
        public ucMaterialMain()
        {
            InitializeComponent();
        }

        #region 变量定义

        /// <summary>
        /// 物资实体
        /// </summary>
        private FS.HISFC.BizLogic.Material.MetItem Item = new FS.HISFC.BizLogic.Material.MetItem();

        /// <summary>
        /// 列设置控件
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucSetColumn ucSetColumn = null;

        /// <summary>
        /// XML文件路径
        /// </summary>
        private string filePath = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\MaterialItem.xml";
        
        //过滤条件
        private string filterTree = "0"; //树型节点选择过滤条件
        private string filterInput = " 1=1 "; //输入码过滤条件
        private string filterValid = " 1=1 ";

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string storageCode;

        /// <summary>
        /// 权限
        /// </summary>
        private bool isEditExpediency;

        #endregion

        #region 初始化树形控件

        /// <summary>
        /// 得到下级科目信息
        /// </summary>
        /// <param name="parm">本级科目编码</param>
        /// <returns>下级科目信息数组</returns>
        public ArrayList GetHasChildren(string parm)
        {
            FS.HISFC.BizLogic.Material.Baseset matBase = new FS.HISFC.BizLogic.Material.Baseset();

            return matBase.QueryKindAllByPreID(parm);
        }


        /// <summary>
        /// 添加TreeView的节点信息
        /// </summary>
        /// <param name="preID">上级科目编码</param>
        /// <param name="curNode">上级节点</param>
        public void InsertNode(System.Windows.Forms.TreeNode node, string preID, string storagecode)
        {
            ArrayList al = new ArrayList();

            try
            {
                //取子节点信息
                al = this.GetHasChildren(preID);

                if (al.Count <= 0)
                {
                    return;
                }

                //添加子节点信息
                foreach (FS.HISFC.Models.Material.MaterialKind materialKind in al)
                {

                    TreeNode kindTree = new TreeNode(materialKind.Name, 2, 1);
                    kindTree.ImageIndex = 0;
                    kindTree.SelectedImageIndex = 0;

                    kindTree.Tag = materialKind.ID;

                    node.Nodes.Add(kindTree);

                    if (!materialKind.EndGrade)
                    {
                        this.InsertNode(kindTree, materialKind.ID, storagecode);
                    }

                }
            }
            catch { }
        }

        /// <summary>
        /// 初始化TreeView
        /// </summary>
        public void InitTreeView()
        {
            this.neuTreeView1.ImageList = this.neuTreeView1.groupImageList;

            TreeNode title = new TreeNode("全部科目信息", 1, 2);
            title.ImageIndex = 4;

            title.Tag = "0";

            //添加根节点
            this.neuTreeView1.Nodes.Add(title);

            ArrayList al = new ArrayList();

            try
            {
                //取默认一级科目
                al = this.GetHasChildren("0");

                if (al.Count > 0)
                {
                    this.InsertNode(title, "0", storageCode);
                }
            }
            catch { }

            this.neuTreeView1.ExpandAll();
        }

        #endregion

        #region 初始化工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override ToolBarService OnInit(object sender, object NeuObject, object param)
        {
            toolBarService.AddToolButton("增加", "新增物品", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            toolBarService.AddToolButton("修改", "修改当前物品信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X修改, true, false, null);
            toolBarService.AddToolButton("删除", "删除当前物品信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            toolBarService.AddToolButton("设置", "设置列显示格式", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S设置, true, false, null);
 
            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "增加":
                    if (this.isEditExpediency)
                    {
                        //增加
                        this.ucMaterialQuery1.MatKind = this.filterTree;
                        this.ucMaterialQuery1.storageCode = this.storageCode;
                        this.ucMaterialQuery1.Add();
                    }
                    else
                    {
                        MessageBox.Show("您无增加权限");
                    }
                    break;
                case "修改":
                    if (this.isEditExpediency)
                    {
                        this.ucMaterialQuery1.Modify();
                    }
                    else
                    {
                        MessageBox.Show("您无修改权限");
                    }
                    break;
                case "删除":
                    if (this.isEditExpediency)
                    {
                        this.ucMaterialQuery1.Delete();
                    }
                    else
                    {
                        MessageBox.Show("您无删除权限");
                    }
                    break;
                case "设置":

                    SetupColumn();
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        public override int Export(object sender, object NeuObject)
        {
            this.ExportInfo();
            return 1;
        }

        protected override int OnPrint(object sender, object NeuObject)
        {
            return 1;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置显示列
        /// </summary>
        public void SetupColumn()
        {
            this.ucSetColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
            this.ucSetColumn.FilePath = filePath;
            this.ucSetColumn.DisplayEvent += new EventHandler(this.uc_GoDisplay);
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "显示设置";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucSetColumn);
        }


        /// <summary>
        /// 设置过滤条件,过滤数据
        /// </summary>
        private void SetFilter()
        {
            //组合过滤条件
            string filter;

            if (this.filterTree == "0")
            {
                filter = this.filterInput;
            }
            else
            {
                filter = "科目编码 like '" + this.filterTree + "%'and " + this.filterInput;
            }

            //过滤数据
            this.ucMaterialQuery1.SetFilter(filter);
        }


        /// <summary>
        /// 导出数据为Excel格式
        /// </summary>
        private void ExportInfo()
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    this.ucMaterialQuery1.fpMaterialQuery.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 事件

        private void ucMaterialMain_Load(object sender, EventArgs e)
        {
            //this.isEditExpediency = false;
            //this.ucMaterialQuery1.EditExpediency = false;

            //FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            //int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0501", ref testPrivDept);
            //if (parma == -1)            //无权限
            //{
            //    MessageBox.Show("您无此窗口操作权限");
            //    return;
            //}
            //else if (parma == 0)       //用户选择取消
            //{
            //    return;
            //}

            //this.isEditExpediency = true;
            //this.ucMaterialQuery1.EditExpediency = true;

            //this.storageCode = testPrivDept.ID;

            //base.OnStatusBarInfo(null, "操作科室： " + testPrivDept.Name);

            this.InitTreeView();

            List<FS.HISFC.Models.Material.MaterialItem> al = new List<FS.HISFC.Models.Material.MaterialItem>();
            al = Item.QueryMetItemAll();			
            if (al == null)
            {
                MessageBox.Show(this.Item.Err, "错误提示");
                return;
            }

            if (this.ucMaterialQuery1.InitDataSet(al) != 1)
            {
                return;
            }
        }

        private void uc_GoDisplay(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.ucMaterialQuery1.fpMaterialQuery_Sheet1, filePath);
            List<FS.HISFC.Models.Material.MaterialItem> al = new List<FS.HISFC.Models.Material.MaterialItem>();
            al = this.Item.QueryMetItemAll();//.GetMetItemList();
            if (al == null)
            {
                MessageBox.Show(this.Item.Err, "错误提示");
                return;
            }
            if (this.ucMaterialQuery1.InitDataSet(al) != 1) return;

        }

        private void neuTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //设置过滤条件
            this.filterTree = e.Node.Tag.ToString();

            //过滤数据
            this.SetFilter();
        }

        private void txtInputCode_TextChanged(object sender, EventArgs e)
        {
            if (this.ucMaterialQuery1.DefaultDataView.Table.Rows.Count == 0) return;

            //取输入码
            string queryCode = this.txtInputCode.Text;
            if (this.chbMisty.Checked)
            {
                queryCode = "%" + queryCode + "%";
            }
            else
            {
                queryCode = queryCode + "%";
            }

            //设置过滤条件
            this.filterInput = "((拼音码 LIKE '" + queryCode + "') OR " +
                "(五笔码 LIKE '" + queryCode + "') OR " +
                "(自定义码 LIKE '" + queryCode + "') OR " +
                "(物品名称 LIKE '" + queryCode + "'))";

            //过滤数据
            this.SetFilter();
        }

        private void txtInputCode_KeyDown(object sender, KeyEventArgs e)
        {
            //上箭头选择上一条记录
            if (e.KeyCode == Keys.Up)
            {
                if (this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex > 0)
                {
                    this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex--;
                    return;
                }
            }

            //下箭头选择下一条记录
            if (e.KeyCode == Keys.Down)
            {
                if (this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex < ucMaterialQuery1.fpMaterialQuery_Sheet1.RowCount)
                {
                    this.ucMaterialQuery1.fpMaterialQuery_Sheet1.ActiveRowIndex++;
                    return;
                }
            }
        }

        private void txtInputCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                //选中文本
                this.txtInputCode.SelectAll();
                //修改物品信息
                this.ucMaterialQuery1.Modify();
            }
        }

        #endregion

        #region IPreArrange 成员

        public int PreArrange()
        {
            this.isEditExpediency = false;
            this.ucMaterialQuery1.EditExpediency = false;

            FS.FrameWork.Models.NeuObject testPrivDept = new FS.FrameWork.Models.NeuObject();
            int parma = FS.HISFC.Components.Common.Classes.Function.ChoosePivDept("0501", ref testPrivDept);
            if (parma == -1)            //无权限
            {
                MessageBox.Show("您无此窗口操作权限");
                return -1;
            }
            else if (parma == 0)       //用户选择取消
            {
                return -1;
            }

            this.isEditExpediency = true;
            this.ucMaterialQuery1.EditExpediency = true;

            this.storageCode = testPrivDept.ID;

            base.OnStatusBarInfo(null, "操作科室： " + testPrivDept.Name);
            return 1;
        }

        #endregion
    }
}

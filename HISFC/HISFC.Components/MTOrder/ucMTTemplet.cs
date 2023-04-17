using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.MTOrder
{
    public partial class ucMTTemplet : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMTTemplet()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucTempletForm_Load);
            this.treeView1.BeforeSelect += new TreeViewCancelEventHandler(treeView1_BeforeSelect);
            this.treeView1.AfterSelect  += new TreeViewEventHandler(treeView1_AfterSelect);

            this.tabControl1.Deselecting += new TabControlCancelEventHandler(tabControl1_SelectionChanging);
            this.tabControl1.Selected += new TabControlEventHandler(tabControl1_SelectionChanged);
            
            this.cmbMedTech.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbMedTech.KeyDown += new KeyEventHandler(cmbDept_KeyDown);
        }

        #region 变量
        private bool isCheckChangceAndSave = true;
        /// <summary>
        /// 是否检查表格变更并提示保存
        /// </summary>
        [Category("设置"), Description("是否检查表格变更并提示保存")]
        public bool IsCheckChangceAndSave
        {
            get { return isCheckChangceAndSave; }
            set { isCheckChangceAndSave = value; }
        }

        private Color stopColor = Color.Yellow;
        /// <summary>
        /// 停班颜色
        /// </summary>
        [Category("设置"), Description("停班颜色")]
        public Color StopColor
        {
            get { return stopColor; }
            set { stopColor = value; }
        }

        /// <summary>
        /// 排班控件数组,方便操作
        /// </summary>
        protected ucSchemaTemplet[] controls;

        private FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region 事件
        private void ucTempletForm_Load(object sender, EventArgs e)
        {


            this.InitArray();

            this.InitControls();

            this.InitMT();

            this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));
        }

        private void treeView1_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (controls[tabControl1.SelectedIndex].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("数据已经改变,是否保存"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (controls[tabControl1.SelectedIndex].Save() == -1)
                    {
                        e.Cancel = true;
                        controls[tabControl1.SelectedIndex].Focus();
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node == null) return;

            //如果当前结点为父结点,那么medTechType为空
            FS.HISFC.Models.Base.Const medTechType = node.Tag as FS.HISFC.Models.Base.Const;

            controls[this.tabControl1.SelectedIndex].Query(medTechType);

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            //Ctrl+S保存
            if (keyData.GetHashCode() == Keys.Control.GetHashCode() + Keys.S.GetHashCode())
            {
                this.Save();
                return true;
            }
            else
            if (keyData == Keys.F1)
            {
                this.cmbMedTech.Focus();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void tabControl1_SelectionChanging(object sender, EventArgs e)
        {
            if (controls[tabControl1.SelectedIndex].IsChange())
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("数据已经修改,是否保存变动"), "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (controls[tabControl1.SelectedIndex].Save() == -1)
                    {
                        this.tabControl1.SelectedIndex = TabIndex ;
                        return;
                    }
                }
            }
        }

        private void tabControl1_SelectionChanged(object sender, EventArgs e)
        {
            //object obj = this.tabControl1.TabPages[this.tabControl1.SelectedIndex].Tag;

            //if (obj == null || obj.ToString() == "")
            //{
            //    this.treeView1.SelectedNode = this.treeView1.Nodes[0];
            //    this.treeView1_AfterSelect(new object(), new System.Windows.Forms.TreeViewEventArgs(new TreeNode(), System.Windows.Forms.TreeViewAction.Unknown));
            //    this.tabControl1.TabPages[this.tabControl1.SelectedIndex].Tag = "Has Retrieve";
            //}
            this.treeView1_AfterSelect(null, null);

        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (TreeNode node in this.treeView1.Nodes[0].Nodes)
            {
                if ((node.Tag as FS.HISFC.Models.Base.Const).ID == this.cmbMedTech.Tag.ToString())
                {
                    this.treeView1.SelectedNode = node;
                    break;
                }
            }
        }

        private void cmbDept_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                controls[tabControl1.SelectedIndex].Focus();
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.toolBarService.AddToolButton("增加", "",(int)FS.FrameWork.WinForms.Classes.EnumImageList.T添加, true, false, null);
            this.toolBarService.AddToolButton("删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.S删除, true, false, null);
            this.toolBarService.AddToolButton("全部删除", "", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q取消, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "增加":
                    this.Add();
                    break;
                case "删除":
                    this.Del();
                    break;
                case "全部删除":
                    this.DelAll();
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            this.Save();

            return 0;
        }
        #endregion

        #region 函数
        /// <summary>
        /// 初始化数组
        /// </summary>
        private void InitArray()
        {
            controls = new ucSchemaTemplet[7];
            
            controls[0] = this.ucSchemaMon;
            controls[1] = this.ucSchemaTue;
            controls[2] = this.ucSchemaWed;
            controls[3] = this.ucSchemaThu;
            controls[4] = this.ucSchemaFri;
            controls[5] = this.ucSchemaSat;
            controls[6] = this.ucSchemaSun;
            //赋值
            foreach (ucSchemaTemplet uc in controls)
            {
                uc.StopColor = StopColor;
                uc.IsCheckChangceAndSave = IsCheckChangceAndSave;
            }
        }

        /// <summary>
        /// 初始化模板控间
        /// </summary>
        private void InitControls()
        {
            ucSchemaTemplet obj;

            for (int i = 1; i <= 7; i++)
            {
                obj = controls[i-1];
                DayOfWeek d = (DayOfWeek)i;
                if (i == 7) d = DayOfWeek.Sunday;
                obj.Init(d);
            }
        }

        /// <summary>
        /// 生成医技左边树列表
        /// </summary>
        private void InitMT()
        {
            this.treeView1.Nodes.Clear();
            TreeNode parent = new TreeNode(FS.FrameWork.Management.Language.Msg("医技列表"));
            this.treeView1.ImageList = this.treeView1.deptImageList;
            parent.ImageIndex = 5;
            parent.SelectedImageIndex = 5;
            this.treeView1.Nodes.Add(parent);

            FS.HISFC.BizLogic.Manager.Constant Mgr = new FS.HISFC.BizLogic.Manager.Constant();

            ArrayList al = Mgr.GetList("MT#MINFEE");
            if (al == null)
            {
                MessageBox.Show("获取医技列表时出错!" + Mgr.Err, "提示");
                return;
            }

            foreach (FS.HISFC.Models.Base.Const MedTech in al)
            {
                TreeNode node = new TreeNode();
                node.Text = MedTech.Name;
                node.ImageIndex = 0;
                node.SelectedImageIndex = 1;
                node.Tag = MedTech;

                parent.Nodes.Add(node);
            }

            this.cmbMedTech.AddItems(al);
            parent.ExpandAll();
        }

        /// <summary>
        /// 增加
        /// </summary>
        private void Add()
        {
            controls[tabControl1.SelectedIndex].Add();
        }
        /// <summary>
        /// 删除
        /// </summary>
        private void Del()
        {

            controls[tabControl1.SelectedIndex].Del();
        }

        /// <summary>
        /// 删除全部
        /// </summary>
        private void DelAll()
        {
            controls[tabControl1.SelectedIndex].DelAll();
        }

        /// <summary>
        /// 保存
        /// </summary>
        private void Save()
        {
            if (controls[tabControl1.SelectedIndex].Save() == -1)
            {
                controls[tabControl1.SelectedIndex].Focus();
                return;
            }

            MessageBox.Show( FS.FrameWork.Management.Language.Msg( "保存成功" ), "提示" );

            controls[tabControl1.SelectedIndex].Focus();
        }

        #endregion

    }
}

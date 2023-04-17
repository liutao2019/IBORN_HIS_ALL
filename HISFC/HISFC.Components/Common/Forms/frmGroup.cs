using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Operation;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Common.Forms
{
    public partial class frmGroup : Form
    {
        public frmGroup()
        {
            InitializeComponent();

            this.Load += new EventHandler(ucGroup_Load);
        }
        void ucGroup_Load(object sender, EventArgs e)
        {

            this.RefreshGroupList();
        }

        FS.HISFC.BizProcess.Integrate.Manager groupManager = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 组套编号
        /// </summary>
        public string GroupID = "";
        /// <summary>
        /// 生成科室收费组套列表
        /// </summary>
        /// <returns></returns>
        private int RefreshGroupList()
        {
            this.tvGroup.Nodes.Clear();

            TreeNode root = new TreeNode();
            root.Text = "模板";
            root.ImageIndex = 22;
            root.SelectedImageIndex = 22;
            tvGroup.Nodes.Add(root);
            string deptCode = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            System.Collections.ArrayList groups = this.groupManager.GetValidGroupListByRoot(deptCode);
            if (groups != null)
            {
                foreach (FS.HISFC.Models.Fee.ComGroup group in groups)
                {
                    this.AddGroupsRecursion(root, group);
                }
            }
            root.Expand();

            return 0;
        }

        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}
        private int AddGroupsRecursion(TreeNode parent, FS.HISFC.Models.Fee.ComGroup group)
        {

            System.Collections.ArrayList al = this.groupManager.GetGroupsByDeptParent("1", group.deptCode, group.ID);
            if (al.Count == 0)
            {
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                parent.Nodes.Add(newNode);

                return -1;
            }
            else
            {
                TreeNode newNode = new TreeNode();
                newNode.Tag = group;
                newNode.Text = group.Name;// +"[" + group.ID + "]";
                parent.Nodes.Add(newNode);
                foreach (FS.HISFC.Models.Fee.ComGroup item in al)
                {
                    this.AddGroupsRecursion(newNode, item);
                }
            }


            return 1;
        }

        private void tvGroup_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FS.HISFC.Models.Fee.ComGroup comGroup = e.Node.Tag as FS.HISFC.Models.Fee.ComGroup;
            if (comGroup == null)
            {
                return;
            }

            GroupID = comGroup.ID;

            this.Dispose();

        }
    }
}

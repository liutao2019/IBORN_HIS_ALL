using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucPhaStoreAlert : Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhaStoreAlert()
        {
            InitializeComponent();
        }

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI);

            TreeNode parentTreeNode = new TreeNode("���в���");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;
                parentTreeNode.Nodes.Add(deptNode);
            }

            deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;
                parentTreeNode.Nodes.Add(deptNode);
            }

            parentTreeNode.ExpandAll();

            return base.OnDrawTree();
        }

        protected override int OnRetrieve(params object[] objects)
        {

            TreeNode selectNode = tvLeft.SelectedNode;

            if (selectNode.Level == 0)
            {
                return -1;
            }
            string deptCode = selectNode.Tag.ToString();

            return base.OnRetrieve(deptCode);
        }

    }
}


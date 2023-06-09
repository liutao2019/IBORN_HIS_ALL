using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbExeDept : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinIpbExeDept()
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
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);

            TreeNode parentTreeNode = new TreeNode("���в���");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;
                parentTreeNode.Nodes.Add(deptNode);
            }

            deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
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
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            TreeNode selectNode = tvLeft.SelectedNode;

            if (selectNode.Level == 0)
            {
                return -1;
            }
            string deptCode = selectNode.Tag.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode);
        }
    }
}


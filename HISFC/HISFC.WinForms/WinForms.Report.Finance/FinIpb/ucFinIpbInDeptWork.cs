using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.Report.Finance.FinIpb
{
    public partial class ucFinIpbInDeptWork :FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        /// <summary>
        /// 住院科室工作量统计
        /// </summary>
        public ucFinIpbInDeptWork()
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
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.N);

            TreeNode parentTreeNode = new TreeNode("全部");
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;
                parentTreeNode.Nodes.Add(deptNode);
            }

            //deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            //foreach (FS.HISFC.Models.Base.Department dept in deptList)
            //{
            //    TreeNode deptNode = new TreeNode();
            //    deptNode.Tag = dept.ID;
            //    deptNode.Text = dept.Name;
            //    parentTreeNode.Nodes.Add(deptNode);
            //}

            parentTreeNode.ExpandAll();

            return base.OnDrawTree();
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
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


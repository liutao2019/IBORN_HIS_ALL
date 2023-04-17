using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.FinIpb
{
    public partial class ucFinIpbFeeGongShi : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbFeeGongShi()
        {
            InitializeComponent();
        }

        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.Manager.Constant mgr = new FS.HISFC.BizLogic.Manager.Constant();


        /// <summary>
        /// 绘制科室树
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            TreeNode parentTreeNode = new TreeNode("所有科室");
            parentTreeNode.Tag = "ALL";
            parentTreeNode.Text = "所有科室";
            tvLeft.Nodes.Add(parentTreeNode);
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

            string deptCode = selectNode.Tag.ToString();


            FS.HISFC.Models.Base.Employee employee;
            this.employee = mgr.Operator as FS.HISFC.Models.Base.Employee;
            string deptName = this.employee.Dept.Name;
            string deptCode1 = this.employee.Dept.ID;
            dwMain.Modify("t_dept.text = '" + deptName.ToString() + "'");
            dwMain.Modify("t_date.text = '打印时间：" + base.beginTime.ToString() + "'");
            dwMain.Retrieve(deptCode1);

            return 1;
        }


        

    }
}

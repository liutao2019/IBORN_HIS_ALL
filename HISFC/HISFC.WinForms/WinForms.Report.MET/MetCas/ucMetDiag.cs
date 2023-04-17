using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.Report.MET.MetCas
{
    public partial class ucMetDiag : FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetDiag()
        {
            InitializeComponent();
            base.LeftControl = QueryControls.Tree;
        }

        /** private string reportCode = string.Empty;
        private string reportName = string.Empty;
      

        protected override void OnLoad()
        {
            this.Init();

            base.OnLoad();
            //�������--����-����
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            System.Collections.ArrayList list = manager.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.I.ToString());
            foreach (FS.HISFC.Models.Base.Department con in list)
            {
                DepartmentC.Items.Add(con);
            }
            if (DepartmentC.Items.Count >= 0)
            {
                DepartmentC.SelectedIndex = 0;
                reportCode = ((FS.HISFC.Models.Base.Department)DepartmentC.Items[0]).ID;
                reportName = ((FS.HISFC.Models.Base.Department)DepartmentC.Items[0]).Name;
            }
        }
        */
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            TreeNode parentTreeNode = new TreeNode("ȫ��");
            parentTreeNode.Tag = "00";
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

            //if (selectNode.Level == 0)
            //{
            //    return -1;
            //}
            string deptCode = selectNode.Tag.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode, Diagnose1.Text);
        }
       
        /**private void DepartmentC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DepartmentC.SelectedIndex > 0)
            {
                reportCode = ((FS.HISFC.Models.Base.Department)DepartmentC.Items[this.DepartmentC.SelectedIndex]).ID;
                reportName = ((FS.HISFC.Models.Base.Department)DepartmentC.Items[this.DepartmentC.SelectedIndex]).Name;
            }
        }
        */
        
       
    }
}

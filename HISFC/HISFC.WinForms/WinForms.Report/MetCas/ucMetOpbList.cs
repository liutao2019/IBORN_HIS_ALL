using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.WinForms.Report.MetCas
{
    public partial class ucMetOpbList : Common.ucQueryBaseForDataWindow
    {
        public ucMetOpbList()
        {
            InitializeComponent();
            base.LeftControl = QueryControls.Tree;
        }
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizLogic.Manager.DepartmentStatManager myDepartmentStatManager = new FS.HISFC.BizLogic.Manager.DepartmentStatManager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList deptList = this.myDepartmentStatManager.LoadDepartmentStatAndByNodeKind("71", "0");

            TreeNode parentTreeNode = new TreeNode("全部");
            parentTreeNode.Tag = "All";
            parentTreeNode.Text = "全院";
            tvLeft.Nodes.Add(parentTreeNode);
            foreach (FS.HISFC.Models.Base.DepartmentStat dept in deptList)
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
            string deptName = selectNode.Text.ToString();

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, deptCode, deptName);
        }

        private void ucMetOpbList_Click(object sender, EventArgs e)
        {
            DateTime sysTime = this.myDepartmentStatManager.GetDateTimeFromSysDateTime();

            this.dtpBeginTime.Text = sysTime.AddDays(-1).ToShortDateString() + " 00:00:00";
            this.dtpEndTime.Text = sysTime.ToShortDateString() + " 00:00:00";
        }

    }
}

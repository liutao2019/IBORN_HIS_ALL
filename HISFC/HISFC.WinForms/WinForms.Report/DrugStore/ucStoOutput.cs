using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.WinForms.Report.DrugStore
{
    public partial class ucStoOutput : Report.Common.ucQueryBaseForDataWindow
    {
        public ucStoOutput()
        {
            InitializeComponent();
        }
        #region ����

        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion
        /// <summary>
        /// ������
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {

            ArrayList deptList = this.managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P);
            if (deptList == null)
            {
                return -1;
            }
            TreeNode parentTreeNode = new TreeNode("���п���");
            this.tvLeft.Nodes.Add(parentTreeNode);

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

            TreeNode selectNode = this.tvLeft.SelectedNode;

            if (selectNode.Level == 0)
            {
                return -1;
            }

            string deptCode = selectNode.Tag.ToString();

            //this.dwMain.Retrieve();

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode);
        }
    }
}


using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucOutputQuery : Common.ucQueryBaseForDataWindow
    {
        public ucOutputQuery()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ����ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region ����

        /// <summary>
        /// ����,�ɹ� 1 ʧ�� -1
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (this.tvLeft == null) 
            {
                return -1;
            }
            
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

            this.dwMain.Retrieve(deptCode, base.beginTime, base.endTime);
            
            return base.OnRetrieve();
        }

        #endregion
    }
}

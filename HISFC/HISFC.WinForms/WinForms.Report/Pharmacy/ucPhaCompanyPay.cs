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
    public partial class ucPhaCompanyPay : Report.Common.ucQueryBaseForDataWindow
    {
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        //FS.HISFC.BizLogic.Manager.DeptItem
        public ucPhaCompanyPay()
        {
            InitializeComponent();
        }
        //protected override void OnLoad()
        //{
        //    this.Init();

        //    base.OnLoad();
        //}
        /// <summary>
        /// ������˾treeview
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (this.tvLeft == null)
            {
                return -1;
            }
            FS.HISFC.BizLogic.Pharmacy.Constant pha = new FS.HISFC.BizLogic.Pharmacy.Constant();

            ArrayList deptList = pha.QueryCompany("1");

            if (deptList == null) 
            { 
                return -1;
            }
            TreeNode parentTreeNode = new TreeNode("���й�˾");

            this.tvLeft.Nodes.Add(parentTreeNode);

            foreach (FS.HISFC.Models.Pharmacy.Company dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name ;
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

            //this.dwMain.Retrieve(base.beginTime, base.endTime,deptCode);

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode);
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.WinForms.Report.MetOpd
{
    public partial class ucMetOpdPatient : Common .ucQueryBaseForDataWindow 
    {
        public ucMetOpdPatient()
        {
            InitializeComponent();
            base.LeftControl = QueryControls.Tree;
        }
        
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        protected override int OnDrawTree()
        {
            if (tvLeft == null)
            {
                return -1;
            }
            ArrayList deptList = managerIntegrate.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            TreeNode parentTreeNode = new TreeNode("È«²¿");
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
            // return -1;
            //}
            string deptCode = selectNode.Tag.ToString();
           // MessageBox.Show(deptCode);
            string deptName = selectNode.Text.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode);
        }

        private void ucMetOpdPatient_Load(object sender, EventArgs e)
        {
            
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 00, 00, 00);
        }
    }
}

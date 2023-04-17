using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.Report.MET.MetOpd
{
    public partial class ucMetOpdThing :FSDataWindow.Controls.ucQueryBaseForDataWindow
    {
        public ucMetOpdThing()
        {
            InitializeComponent();
            base.LeftControl = QueryControls.Tree;
        }


        //private string reportCode = string.Empty;
        //private string reportName = string.Empty;

        //protected override void OnLoad()
        //{
        // this.Init();

        // base.OnLoad();
        // //�������--����-����
        // FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        // System.Collections.ArrayList list = manager.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.I.ToString());
        // foreach (FS.HISFC.Models.Base.Department con in list)
        // {
        // deptname.Items.Add(con);
        // }
        // if (deptname.Items.Count >= 0)
        // {
        // deptname.SelectedIndex = 0;
        // reportCode = ((FS.HISFC.Models.Base.Department)deptname.Items[0]).ID;
        // reportName = ((FS.HISFC.Models.Base.Department)deptname.Items[0]).Name;
        // }
        //}
        //private void Department_SelectedIndexChanged(object sender, EventArgs e)
        //{
        // if (deptname.SelectedIndex > 0)
        // {
        // reportCode = ((FS.HISFC.Models.Base.Department)deptname.Items[this.deptname.SelectedIndex]).ID;
        // reportName = ((FS.HISFC.Models.Base.Department)deptname.Items[this.deptname.SelectedIndex]).Name;
        // }
        //}

        //protected override int OnRetrieve(params object[] objects)
        //{


        // if (base.GetQueryTime() == -1)
        // {
        // return -1;
        // }



        // return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value, reportCode,reportName);
        //}
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

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
            // return -1;
            //}
            string deptCode = selectNode.Tag.ToString();
           // MessageBox.Show(deptCode);
            string deptName = selectNode.Text.ToString();

            return base.OnRetrieve(base.beginTime, base.endTime, deptCode);
        }

        private void ucMetOpdThing_Load(object sender, EventArgs e)
        {
            
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 00, 00, 00);
        }
    }
}

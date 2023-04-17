using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.DrugStore
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ucMonth : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucMonth()
        {
            InitializeComponent();
        }

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.Models.Base.Employee emp = new FS.HISFC.Models.Base.Employee();

        protected override int OnDrawTree()
        {
            if (this.tvLeft == null)
            {
                return -1;
            }

            System.Collections.ArrayList deptList = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            System.Collections.ArrayList deptList2 = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.T);
            System.Collections.ArrayList deptList3 = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.OP);
            deptList.AddRange(deptList2);
            deptList.AddRange(deptList3);

            if (deptList == null)
            {
                return -1;
            }

            TreeNode parentNode = new TreeNode("ȫ��");

            this.tvLeft.Nodes.Add(parentNode);

            foreach (FS.HISFC.Models.Base.Department dept in deptList)
            {
                TreeNode deptNode = new TreeNode();
                deptNode.Tag = dept.ID;
                deptNode.Text = dept.Name;

                parentNode.Nodes.Add(deptNode);
            }

            parentNode.ExpandAll();

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

            string deptName = selectNode.Text;
            string deptCode = selectNode.Tag.ToString();
            string deptPcode =this.employee.Dept.ID;
            string deptPname = this.employee.Dept.Name;

              this.dwMain.Retrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,deptCode,deptName,deptPcode,deptPname);
              this.dwMain.Modify("t_5.text = '" + this.dtpBeginTime.Value.ToString() + "��" + this.dtpEndTime.Value.ToString() + "'");
              this.dwMain.Modify("t_4.text = '" + deptPname + "'");
              this.dwMain.Modify("t_3.text = '" + deptName + "'");
              return 1;
        }
       
    }
}

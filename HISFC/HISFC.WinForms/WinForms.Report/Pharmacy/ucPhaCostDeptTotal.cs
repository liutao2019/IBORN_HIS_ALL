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
    /// <summary>
    /// ҩƷ����ͳ�ƻ��ܱ���ȡҩ���һ���
    /// </summary>
    public partial class ucPhaCostDeptTotal : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhaCostDeptTotal()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ��д�����������������п������б�
        /// </summary>
        /// <returns></returns>
        protected override int OnDrawTree()
        {
            if (this.tvLeft == null)
            {
                return -1;
            }

            //��֧������
            this.isSort = false;

            try
            {
                FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
                ArrayList deptList = manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.PI);
                deptList.AddRange(manager.GetDepartment(FS.HISFC.Models.Base.EnumDepartmentType.P));

                TreeNode root = new TreeNode("���п�����");
                root.Tag = "ROOT";

                TreeNode node;
                FS.HISFC.Models.Base.Department dept;
                foreach (Object obj in deptList)
                {
                    dept = obj as FS.HISFC.Models.Base.Department;
                    node = new TreeNode();
                    node.Text = dept.Name;
                    node.Tag = dept.ID;
                    root.Nodes.Add(node);
                }

                this.tvLeft.Nodes.Add(root);
                root.ExpandAll();
                //this.cmbQuery.alItems = deptList;

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ���ݷ����쳣\n"+ex.Message,"��ʾ");
                return -1;
            }

        }

        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="objects"></param>
        /// <returns></returns>
        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            string deptCode = this.tvLeft.SelectedNode.Tag.ToString().Equals("ROOT") ? "ALL" : this.tvLeft.SelectedNode.Tag.ToString();
            return base.OnRetrieve(deptCode, this.beginTime, this.endTime, this.tvLeft.SelectedNode.Text);
        }

    }
}


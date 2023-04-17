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
    /// ҩƷ����ͳ�Ʊ�
    /// <br>����������</br>
    /// </summary>
    public partial class ucPhaStoDetpCostList : FS.WinForms.Report.Common.ucQueryBaseForDataWindow
    {
        public ucPhaStoDetpCostList()
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
                 

                return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("��ʼ���ݷ����쳣\n" + ex.Message, "��ʾ");
                return -1;
            }

        }

        private string drugqualcode = string.Empty;
        private string drugqualname = string.Empty;
        /// <summary>
        /// ��ʼ��
        /// </summary>
        protected override void OnLoad()
        {
            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList list = manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
            FS.HISFC.Models.Base.Const obj = new FS.HISFC.Models.Base.Const();
            obj.ID = "ALL";
            obj.Name = "ȫ��";
            obj.SpellCode = "QB";
            obj.WBCode = "WU";
            list.Add(obj);
            this.neuComboBox1.Items.Add(obj);
            foreach (FS.HISFC.Models.Base.Const con in list)
            {
                neuComboBox1.Items.Add(con);
            }

            this.neuComboBox1.alItems.Add(obj);
            this.neuComboBox1.alItems.AddRange(list);

            if (neuComboBox1.Items.Count > 0)
            {
                neuComboBox1.SelectedIndex = 0;
                drugqualcode = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).ID;
                drugqualname = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[0]).Name;
            }
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (this.GetQueryTime() == -1)
            {
                return -1;
            }

            
            string dept = this.tvLeft.SelectedNode.Tag.ToString();
            if (dept.Equals("ROOT"))
            {
                return -1;
            }

            objects = new object[] { this.beginTime, this.endTime, dept, drugqualcode};

            return base.OnRetrieve(objects);
        }

        private void neuComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuComboBox1.SelectedIndex > -1)
            {
                drugqualcode = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).ID;
                drugqualname = ((FS.HISFC.Models.Base.Const)neuComboBox1.Items[this.neuComboBox1.SelectedIndex]).Name;
 
            }
        }
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Privilege
{
    /// <summary>
    /// [��������: Config����]<br></br>
    /// [������:   �ſ���]<br></br>
    /// [����ʱ��: 2008.6.12]<br></br>
    /// <˵��>
    ///  ѡ����ĶԻ���
    /// </˵��>
    /// </summary>
    public partial class SelectClassForm : Form
    {
        public delegate void GetClassName(string Name);
        public event GetClassName GetName;
        public ArrayList classNameList = null;

        public SelectClassForm(ArrayList list)
        {
            InitializeComponent();
            this.BackColor = FS.FrameWork.WinForms.Classes.Function.GetSysColor(FS.FrameWork.WinForms.Classes.EnumSysColor.Blue);
            classNameList = list;

        }

        private void SelectClassForm_Load(object sender, EventArgs e)
        {
            if (classNameList.Count != 0)
            {
                classNameList.Sort(new CompareClassName());
                for (int i = 0; i < classNameList.Count; i++)
                {
                    if (classNameList[i].ToString().Contains("+"))
                    {
                        continue;
                    }
                    TreeNode newNode = new TreeNode();
                    newNode.Text = classNameList[i].ToString();
                    treeView1.Nodes.Add(newNode);
                }
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            GetName(e.Node.Text);
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            GetName(textBox1.Text.Trim());
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = e.Node.Text.Trim();
        }


    }
}

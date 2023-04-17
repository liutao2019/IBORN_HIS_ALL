using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ����̨���οؼ�]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-04]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucOperationTableTree : FS.HISFC.Components.Common.Controls.baseTreeView
    {
        public ucOperationTableTree()
        {
            InitializeComponent();
            //if (!Environment.DesignMode)
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
                this.Init();
        }

        public ucOperationTableTree(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
            //if(!Environment.DesignMode)
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
                this.Init();
        }

#region �ֶ�
        private FS.HISFC.BizLogic.Operation.OpsTableManage tableManager = new FS.HISFC.BizLogic.Operation.OpsTableManage();

#endregion
        private void Init()
        {
            this.Nodes.Clear();
            TreeNode root = new TreeNode();
            root.Text = "����̨";
            root.ImageIndex = 18;
            root.SelectedImageIndex = 18;
            this.Nodes.Add(root);

            //��ȡ����̨�б�			
            ArrayList tables = this.tableManager.GetOpsTableByDept(Environment.OperatorDeptID);

            //��������б�
            if (tables != null)
            {
                foreach (FS.HISFC.Models.Operation.OpsTable table in tables)
                {
                    if (table.IsValid == false) 
                        continue;

                    TreeNode node = new TreeNode();
                    node.Tag = table;
                    node.Text = "[" + table.ID + "]" + table.Name;
                    node.ImageIndex = 37;
                    node.SelectedImageIndex = 37;
                    root.Nodes.Add(node);
                }
            }

            root.Expand();
        }
    }
}

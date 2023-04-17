using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Manager.Controls
{
    /// <summary>
    /// ���б�,�г��û��ܿ�������
    /// </summary>
    public class tvGroup:FS.FrameWork.WinForms.Controls.NeuTreeView
    {
        public tvGroup()
        {
            if (DesignMode == false)
            {
                try
                {
                    sysGroupManager = new FS.HISFC.BizLogic.Manager.SysGroup();
                    this.Init();
                    this.RefreshGroupList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������б����") + ex.Message);
                }
            }
               
        }
        
        protected FS.HISFC.Models.Base.Employee person = null;
        protected FS.HISFC.Models.Admin.SysGroup curGroup = null;
       
        protected FS.HISFC.BizLogic.Manager.SysGroup sysGroupManager = null;
        
        protected virtual void Init()
        {
            person = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            if (person == null) return;
            curGroup = new FS.HISFC.Models.Admin.SysGroup();
            if (person.IsManager)
            {
                
                curGroup.ID = "ROOT";
                curGroup.Name = "ϵͳ��";
                curGroup.ParentGroup.ID = "ROOT";
                curGroup.ParentGroup.Name = "ROOT";
            }
            else
            {
                curGroup.ID = person.CurrentGroup.ID ;
                curGroup.Name = person.CurrentGroup.Name;
            }

            this.HideSelection = false;

            TreeNode node = new TreeNode(curGroup.Name);
            node.ImageIndex = 2;
            node.SelectedImageIndex = 3;
            node.Tag = curGroup;
            this.Nodes.Add(node);
        }
        /// <summary>
        /// ˢ���б�
        /// </summary>
        public void RefreshGroupList()
        {
            //���ﲻ�ܰ�ȫ����ϵͳ���г�������Ҫ���й��ˡ� 	
            if (person == null)
            {
                this.Nodes.Clear();

                return;
            }
            ArrayList al;
            if (person.IsManager)
            {
                al = sysGroupManager.GetList();
            }
            else
            {
                //������ԱȨ�޽��й���.
                al = person.PermissionGroup;
            }


            this.Nodes[0].Nodes.Clear();
            foreach (FS.HISFC.Models.Admin.SysGroup obj in al)
            {
                TreeNode node = new TreeNode(obj.Name);
                node.ImageIndex = 2;
                node.SelectedImageIndex = 3;
                node.Tag = obj;
                this.Nodes[0].Nodes.Add(node);

            }
            foreach (TreeNode node in this.Nodes[0].Nodes)
            {
                TreeNode pNode = null;
                SearchParentNode(this.Nodes[0], node.Tag, ref pNode);
                if (pNode != null)
                {
                    node.Parent.Nodes.Remove(node);
                    pNode.Nodes.Add(node);
                }
            }
            this.ExpandAll();
            if (this.Nodes[0].Nodes.Count > 0)
            {
                this.SelectedNode = this.Nodes[0].Nodes[0]; //Ĭ��ѡ���һ���ڵ�
            }
        }
        private void SearchParentNode(TreeNode rootNode, object obj, ref TreeNode rtnNode)
        {
            foreach (TreeNode node in rootNode.Nodes)
            {
                FS.HISFC.Models.Admin.SysGroup o = node.Tag as FS.HISFC.Models.Admin.SysGroup;

                this.SearchParentNode(node, obj, ref rtnNode);

                if (o != null)
                {
                    try
                    {
                        if (o.ID == ((FS.HISFC.Models.Admin.SysGroup)obj).ParentGroup.ID)
                        {
                            rtnNode = node;
                        }
                    }
                    catch { }
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
  
    }
}

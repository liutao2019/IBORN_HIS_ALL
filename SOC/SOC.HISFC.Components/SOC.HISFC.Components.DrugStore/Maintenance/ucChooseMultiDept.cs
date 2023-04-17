using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DrugStore.Maintenence
{
    public partial class ucChooseMultiDept : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucChooseMultiDept( )
        {
            InitializeComponent( );
            this.InitTreeView( );
        }

        public ucChooseMultiDept( IContainer container )
        {
            container.Add( this );

            InitializeComponent( );

            this.InitTreeView( );
        }

        #region ����

        /// <summary>
        /// �����б�
        /// </summary>
        private List<FS.HISFC.Models.Base.Department> alDept = new List<FS.HISFC.Models.Base.Department>();

        #endregion

        #region ����

        /// <summary>
        /// ��ѡ�еĿ����б�
        /// </summary>
        public List<FS.HISFC.Models.Base.Department> SelectedDeptList
        {
            get
            {
                return alDept;
            }
            set
            {
                alDept = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��ʼ��ȫԺ�����б�
        /// </summary>
        private void InitTreeView( )
        {
            TreeNode nodeParent = null;
            TreeNode node = null;
            string parentCode = "";
            try
            {
                //��ȡ�����б�
                FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department( );
                ArrayList al = dept.GetDeptmentAllOrderByDeptType( );
                if( al == null )
                {
                    MessageBox.Show( dept.Err );
                    return;
                }
                foreach( FS.HISFC.Models.Base.Department obj in al )
                {
                    //{9E7FB328-89B3-4f43-A417-2EC3ACFC7093}
                    //����Ƿ�������ͣ�� �򲻼�
                    if (obj.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid || obj.ValidState == FS.HISFC.Models.Base.EnumValidState.Ignore)
                    {
                        continue;
                    }
                    //��Ӹ����ڵ�
                    if( obj.DeptType.ID.ToString() != parentCode )
                    {
                        nodeParent = new TreeNode( );
                        nodeParent.Text = obj.DeptType.Name;
                        nodeParent.Tag = obj;
                        nodeParent.ImageIndex = 0;
                        nodeParent.SelectedImageIndex = 0;
                        this.tvDeptList.Nodes.Add( nodeParent );
                        parentCode = obj.DeptType.ID.ToString();
                    }

                    node = new TreeNode( );
                    node.Text = obj.Name;
                    node.Tag = obj;
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 0;
                    nodeParent.Nodes.Add( node );
                }
            }
            catch { }
        }

        /// <summary>
        /// ���ѡ�п����б�
        /// </summary>
        private void AddSelectedDept( )
        {
            //��������е����ݡ�
            this.alDept.Clear( );
            if( this.tvDeptList.Nodes.Count == 0 ) return;
            foreach( TreeNode node in this.tvDeptList.Nodes )
            {
                foreach( TreeNode tvNode in node.Nodes )
                {
                    //��ѡ�е���浽������
                    if( tvNode.Checked )
                    {
                        this.alDept.Add( tvNode.Tag as FS.HISFC.Models.Base.Department);
                    }
                }
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �߼�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuLinkLabel1_LinkClicked( object sender , LinkLabelLinkClickedEventArgs e )
        {
            //ucTreeNodeSearch uc = new ucTreeNodeSearch( this.tvDeptList );
            //FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "����";
            //FS.FrameWork.WinForms.Classes.Function.ShowControl( uc );
        }

        /// <summary>
        /// ȷ�Ϸ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click( object sender , EventArgs e )
        {
            this.AddSelectedDept( );
            this.ParentForm.Close( );
        }
         /// <summary>
         ///  �˳�
         /// </summary>
         /// <param name="sender"></param>
         /// <param name="e"></param>
        private void btnCancel_Click( object sender , EventArgs e )
        {
            this.alDept.Clear( );
            this.ParentForm.Close( );
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad( e );
        }

        /// <summary>
        /// ������ǰ�ڵ�������ӽڵ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvDeptList_AfterCheck( object sender , TreeViewEventArgs e )
        {
            if( e.Node != null )
            {
                if( this.tvDeptList.CheckBoxes )
                {
                    foreach( TreeNode node in e.Node.Nodes )
                    {
                        if( node.Checked != e.Node.Checked )
                        {
                            node.Checked = e.Node.Checked;
                        }
                    }
                }
            }
        }

        #endregion
 
    }
}

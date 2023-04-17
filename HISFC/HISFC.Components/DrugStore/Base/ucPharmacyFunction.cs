using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Base
{
    /// <summary>
    /// [�ؼ�����:ucPharmacyFunction]<br></br>
    /// [��������: ҩ������ά��<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-17]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPharmacyFunction : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPharmacyFunction( )
        {
            InitializeComponent( );
        }

        #region ����

        //ҩ��������
        private FS.HISFC.BizLogic.Pharmacy.Constant pharmacyConstant = new FS.HISFC.BizLogic.Pharmacy.Constant( );
        private FS.HISFC.Models.Pharmacy.PhaFunction functionObject = null;
        //����ҩ����������
        ucPharmacyFunctionProperty ucProperty = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ������޸ı༭
        /// </summary>
        public bool IsCanEdit
        {
            set
            {
                this.toolBarService.SetToolButtonEnabled("�޸�", value);
                this.menuModify.Enabled = value;
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��ListView
        /// </summary>
        private void InitListView( )
        {
            //����б�
            this.lvFunctionList.Items.Clear( );
            if( this.tvFunction.SelectedNode.Nodes.Count > 0 )
            {
                foreach( TreeNode node in this.tvFunction.SelectedNode.Nodes )
                {
                    ListViewItem lvi = new ListViewItem( );
                    lvi.Text = node.Text;
                    lvi.Tag = node;
                    lvi.ImageIndex = node.ImageIndex;
                    this.lvFunctionList.Items.Add( lvi );
                }
            }
        }

        /// <summary>
        /// �ݹ�����������ڵ�
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="newnodecode"></param>
        /// <param name="newnode"></param>
        private void GetAllNode( TreeNodeCollection tnc , string newnodecode , TreeNode newnode ) //����������
        {
            foreach( TreeNode node in tnc )
            {
                if( node.Nodes.Count != 0 )
                {
                    GetAllNode( node.Nodes , newnodecode , newnode );
                }
                if( newnodecode == node.Tag.ToString( ) )
                {
                    node.Nodes.Add( newnode );
                    SettreeImage( node , false );
                }
            }
        }

        /// <summary>
     /// �������ڵ�ͼ�� true ��ʾ��Ҷ�ӽڵ�
     /// </summary>
     /// <param name="node"></param>
     /// <param name="iFtrue"></param>
        public void SettreeImage( TreeNode node , bool iFtrue )
        {
            if( iFtrue )
            {
                node.ImageIndex = 0;
                node.SelectedImageIndex = 0;
            }
            else
            {
                node.ImageIndex = 2;
                node.SelectedImageIndex = 1;
            }
        }


        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService( );

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "����ҩ������" , FS.FrameWork.WinForms.Classes.EnumImageList.T��� , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ��ҩ������" , FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ�� , true , false , null );
            this.toolBarService.AddToolButton( "�޸�" , "�޸�ҩ������" , FS.FrameWork.WinForms.Classes.EnumImageList.X�޸� , true , false , null );
            this.toolBarService.AddToolButton( "�ϲ�" , "�����ϲ�Ŀ¼" , FS.FrameWork.WinForms.Classes.EnumImageList.X��һ�� , true , false , null );
            return this.toolBarService;
        }

        /// <summary>
        /// ��������ť�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked( object sender , ToolStripItemClickedEventArgs e )
        {
            switch( e.ClickedItem.Text )
            {
                case "����":
                    this.menuAdd_Click( sender , e );
                    break;
                case "ɾ��":
                    this.menuDelete_Click( sender , e );
                    break;
                case "�޸�":
                    this.menuModify_Click( sender , e );
                    break;
                case "�ϲ�":
                    TreeNode node;
                    if( this.tvFunction.Nodes.Count == 0 )
                    {
                        return;
                    }
                    node = this.tvFunction.SelectedNode.Parent;
                    if( node != null )
                    {
                        this.tvFunction.SelectedNode = node;
                    }
                    break;
            }

        }

        #endregion

        #region �¼�
        /// <summary>
        /// ���ڵ�ѡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvFunction_AfterSelect( object sender , TreeViewEventArgs e )
        {
            if (e.Node != null)
            {
                this.InitListView();
                //���ù�������ť
                if (e.Node.Nodes.Count > 0)
                {
                    this.toolBarService.SetToolButtonEnabled("ɾ��", false);
                    this.menuDelete.Enabled = false;
                }
                else
                {
                    this.toolBarService.SetToolButtonEnabled("ɾ��", true);
                    this.menuDelete.Enabled = true;
                }

                if (e.Node.Tag == null || e.Node.Text == "ҩ������")
                {
                    this.IsCanEdit = false;
                }
                else
                {
                    this.IsCanEdit = true;
                }
            }
            else
            {
                this.IsCanEdit = false;
            }

        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            //ֻչ�����ĵ�һ��
            if( this.tvFunction.Nodes.Count > 0 )
            {
                this.tvFunction.Nodes[ 0 ].Expand( );
                //Ĭ��ѡ�и��ڵ�
                this.tvFunction.SelectedNode = this.tvFunction.Nodes[ 0 ];
            }

            //���ù�����
            this.toolBarService.SetToolButtonEnabled( "ɾ��" , false );
            this.toolBarService.SetToolButtonEnabled( "�޸�" , false );
            this.menuModify.Enabled = false;
            this.menuDelete.Enabled = false;

            base.OnLoad( e );
        }

        #region �˵��¼�

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAdd_Click( object sender , EventArgs e )
        {
            string nodecode;
            //{FF5503FA-0057-413e-BF08-5A8C1DCF7ED8}  ҩ�����ü���У��
            int girdLevel;
            //���ѡ���˽ڵ㣬��ѡ��Ľڵ���Ϊ���ڵ㣬������ӵ����ڵ���
            if( this.tvFunction.SelectedNode != null)
            {
                nodecode = this.tvFunction.SelectedNode.Tag.ToString( );
                girdLevel = this.tvFunction.SelectedNode.Level;         //�������ڵ�������ҩ�����ü���  {FF5503FA-0057-413e-BF08-5A8C1DCF7ED8} 
            }
            else
            {
                nodecode = "-1";
                girdLevel = 0;                                             //���ڵ��� ҩ�����ü���Ϊ 1        {FF5503FA-0057-413e-BF08-5A8C1DCF7ED8} 
            }
            if (girdLevel == 3)           //���ڵ��Ѿ��������ڵ�
            {
                MessageBox.Show( "ҩ�������֧���������࣬�����ٽ����ļ��������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information );
                return;
            }
            //��ʼ��ʵ��
            functionObject = new FS.HISFC.Models.Pharmacy.PhaFunction( );
            //��ʼ���ؼ�
            ucProperty = new ucPharmacyFunctionProperty( nodecode, "INSERT", girdLevel + 1);
            //���ڱ���
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "���ҩ������";
            DialogResult dlg = FS.FrameWork.WinForms.Classes.Function.PopShowControl( ucProperty );
            if( dlg == DialogResult.OK )
            {
                TreeNode tn = new TreeNode( );
                //��ȡ��������ʵ��
                functionObject = ( FS.HISFC.Models.Pharmacy.PhaFunction )this.pharmacyConstant.QueryPhaFunctionNodeName( )[ 0 ];
                //�����¼ӽڵ�
                tn.Tag = functionObject.ID;
                tn.Text = functionObject.Name;
                tn.ImageIndex = 0;
                tn.SelectedImageIndex = 0;
                if( this.tvFunction.SelectedNode != null )
                {
                    this.tvFunction.SelectedNode.Nodes.Add( tn );
                    this.tvFunction.SelectedNode.ImageIndex = 2;
                    this.tvFunction.SelectedNode.SelectedImageIndex = 1;
                }
                else
                {
                    this.tvFunction.Nodes.Add( tn );
                }

                //��ӵ�ListView
                ListViewItem lvi = new ListViewItem( );
                lvi.Text = tn.Text;
                lvi.Tag = tn.Tag;
                lvi.ImageIndex = tn.ImageIndex;
                this.lvFunctionList.Items.Add( lvi );
            }

        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDelete_Click( object sender , EventArgs e )
        { 
            //������ڵ���Ϊ���򲻴���
            if( this.tvFunction.Nodes.Count == 0 )
            {
                return;
            }

            TreeNode node = null;
            //����б�����ѡ�еĽڵ�
            if( this.lvFunctionList.Focused == true && this.lvFunctionList.SelectedItems.Count > 0 )
            {
                node = this.lvFunctionList.SelectedItems[ 0 ].Tag as TreeNode;
            }
            else //�б���û��ѡ�еĽڵ㣬��ȡ����ǰѡ�еĽڵ�
            {
                node = this.tvFunction.SelectedNode;
            }
            //����ýڵ���û���ӽڵ������ɾ������������ɾ��
            if( node.Nodes.Count == 0 )
            {
                //��ʼ���ؼ�
                ucProperty = new ucPharmacyFunctionProperty( node.Tag.ToString(), "DELETE", this.tvFunction.SelectedNode.Level);
                //���ڱ���
                FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "ɾ��ҩ������";
                DialogResult dlg = FS.FrameWork.WinForms.Classes.Function.PopShowControl( ucProperty );
                if( dlg == DialogResult.OK )
                {
                    TreeNode tn = new TreeNode( );
                    tn = node.Parent;
                    node.Remove( );                       //ɾ�����ڵ�
                    if( this.tvFunction.Nodes.Count > 0 )     //�ж��������ɾ�����ڵ�
                    {
                        if (this.lvFunctionList.SelectedItems.Count > 0)
                            this.lvFunctionList.SelectedItems[ 0 ].Remove( );//ɾ��listview �ڵ�

                        if( tn.Nodes.Count == 0 )
                        {
                            tn.ImageIndex = 0;
                            tn.SelectedImageIndex = 0;
                        }
                    }

                }
            }
        }

        /// <summary>
        /// �޸�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuModify_Click( object sender , EventArgs e )
        {
            //�����ǰ��û�нڵ�������ʾ
            if( this.tvFunction.Nodes.Count == 0 )
            {
                return;
            }
            //���嵱ǰ�ڵ�͸��ڵ�
            TreeNode node , nodep;
            if( this.lvFunctionList.Focused == true && this.lvFunctionList.SelectedItems.Count > 0 )
            {
                node = this.lvFunctionList.SelectedItems[ 0 ].Tag as TreeNode;
                nodep = node.Parent;
            }
            else
            {
                node = this.tvFunction.SelectedNode;
            }

            //��ʼ��ʵ��
            functionObject = new FS.HISFC.Models.Pharmacy.PhaFunction( );
            //��ʼ���ؼ�
            ucProperty = new ucPharmacyFunctionProperty( node.Tag.ToString( ) , "UPDATE",this.tvFunction.SelectedNode.Level  );
            //���ڱ���
            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "�޸�����ά��";
            DialogResult dlg = FS.FrameWork.WinForms.Classes.Function.PopShowControl( ucProperty );

            object myobj = new object( );
            if( dlg == DialogResult.OK )
            {         
                functionObject = ( FS.HISFC.Models.Pharmacy.PhaFunction )this.pharmacyConstant.QueryPhaFunctionNodeName( )[ 0 ];
                //ȡ�����¸��ĵĽڵ������
                node.Text = functionObject.Name;
                //������µĲ��Ǹ��ڵ�
                if (this.lvFunctionList.SelectedItems.Count > 0)
                {
                    this.lvFunctionList.SelectedItems[0].Text = functionObject.Name;
                    //�¸��Ľڵ�ĸ��ڵ��ԭ�����ڵ㲻ͬ������LOAD
                    if (functionObject.ParentNode != node.Parent.Tag.ToString())
                    {
                        myobj = node;
                        nodep = node.Parent;
                        node.Remove();
                        //�ӵ�ǰ�б���ɾ��
                        this.lvFunctionList.SelectedItems[0].Remove();
                        //�ݹ��������������ӵ��¸��ڵ���
                        GetAllNode(this.tvFunction.Nodes, functionObject.ParentNode, (TreeNode)myobj);
                        //�����ǰ�޸ĵĽڵ�ĸ��ڵ�û���ӽڵ㣬������ӽڵ��־Ϊ0����Ϊ��ǰ�ڵ�ĵĽڵ�����Ѿ����ģ�
                        if (nodep.Nodes.Count == 0)
                        {
                            //Ҷ�ӽڵ����nodekind
                            this.pharmacyConstant.UpdateFunctionnNodekind(nodep.Tag.ToString(), 0);
                            SettreeImage(nodep, true);
                        }
                    }
                }
                else
                {
                    this.tvFunction.InitTreeView();

                    if (this.tvFunction.Nodes.Count > 0)
                        this.tvFunction.Nodes[0].Expand();
                }
            }
        }

        /// <summary>
        /// �б���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowList_Click( object sender , EventArgs e )
        {
            this.menuShowList.Checked = true;
            this.menuShowLarge.Checked = false;
            this.menuShowSmall.Checked = false;

            this.lvFunctionList.View = View.List;
        }

        /// <summary>
        ///  ��ͼ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowLarge_Click( object sender , EventArgs e )
        {
            this.menuShowLarge.Checked = true;
            this.menuShowSmall.Checked = false;
            this.menuShowList.Checked = false;

            this.lvFunctionList.View = View.LargeIcon;
        }

        /// <summary>
        /// Сͼ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowSmall_Click( object sender , EventArgs e )
        {
            this.menuShowLarge.Checked = false;
            this.menuShowSmall.Checked = true; 
            this.menuShowList.Checked = false;

            this.lvFunctionList.View = View.SmallIcon;
        }

        #endregion

        /// <summary>
        /// �б���ͼ�����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFunctionList_Click( object sender , EventArgs e )
        {
            TreeNode node;
            if( this.lvFunctionList.SelectedItems.Count > 0 && this.lvFunctionList.Focused == true )
            {
  
                node = this.lvFunctionList.SelectedItems[ 0 ].Tag as TreeNode;
                if (node == null)
                {
                    return;
                }

                //���ѡ�нڵ����ӽڵ�
                if( node.Nodes.Count > 0 )
                {
                    this.menuDelete.Enabled = false;
                    this.menuModify.Enabled = true;
                    this.toolBarService.SetToolButtonEnabled( "ɾ��" , false );
                    this.toolBarService.SetToolButtonEnabled( "�޸�" , true );
                }
                else
                {
                    this.menuDelete.Enabled = true;
                    this.menuModify.Enabled = true;
                    this.toolBarService.SetToolButtonEnabled( "ɾ��" , true );
                    this.toolBarService.SetToolButtonEnabled( "�޸�" , true );
                }
            }
            else
            {
                this.menuDelete.Enabled = false;
                this.menuModify.Enabled = false;
                this.toolBarService.SetToolButtonEnabled( "ɾ��" , false );
                this.toolBarService.SetToolButtonEnabled( "�޸�" , false );
                return;
            }
        }

        /// <summary>
        /// �б���ͼ˫���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvFunctionList_DoubleClick( object sender , EventArgs e )
        {
            if( this.lvFunctionList.SelectedItems.Count > 0 && this.lvFunctionList.Focused )
            {
                //��ǰ�ڵ��»��нڵ���򿪣��������Ӧ
                TreeNode node = this.lvFunctionList.SelectedItems[ 0 ].Tag as TreeNode;
                if (node == null)
                {
                    return;
                }
                if( node.Nodes.Count >= 1 )
                {
                    this.tvFunction.SelectedNode = node;
                }
            }
        }

        #endregion



    }
}

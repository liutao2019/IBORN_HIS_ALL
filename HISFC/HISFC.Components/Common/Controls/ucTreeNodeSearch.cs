using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
{
    public partial class ucTreeNodeSearch : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tv">Ҫ���ҵ�Ŀ����</param>
        public ucTreeNodeSearch( TreeView tv )
        {
            InitializeComponent( );

            this.InitTreeViee( tv );
        }
        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="tv">Ҫ���ҵ�Ŀ����</param>
        /// <param name="list">���ҵ��б�</param>
        public ucTreeNodeSearch( TreeView tv,ArrayList list)
        {
            InitializeComponent( );
            this.InitTreeViee( tv , list );
        }

        #region ����

        FS.FrameWork.WinForms.Classes.Function fun = new FS.FrameWork.WinForms.Classes.Function( );
        System.Windows.Forms.TreeView treeView = null;
        private int CurrentNode;
        
        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="tv"></param>
        /// <returns></returns>
        private int InitTreeViee( System.Windows.Forms.TreeView tv )
        {
            treeView = tv;
            treeView.HideSelection = false;
            this.comSearchText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            return 0;
        }
        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="ItemList"></param>
        private int InitTreeViee( System.Windows.Forms.TreeView tv , ArrayList ItemList )
        {
            this.treeView = tv;
            treeView.HideSelection = false;
            ArrayList list = new ArrayList( );
            FS.HISFC.Models.Base.Spell obj = null;
            if( ItemList == null )
            {

                foreach( TreeNode node in tv.Nodes )
                {
                    obj = new FS.HISFC.Models.Base.Spell( );
                    obj.ID = node.Text;
                    obj.Name = node.Text;
                    list.Add( obj );
                }
            }
            else if( ItemList.Count == 0 )
            {
                foreach( TreeNode node in tv.Nodes )
                {
                    obj = new FS.HISFC.Models.Base.Spell( );
                    obj.ID = node.Text;
                    obj.Name = node.Text;
                    list.Add( obj );
                }
            }
            else
            {
                list = ItemList;
            }
            this.comSearchText.Items.Add( list );
            return 0;
        }
        #endregion

        #region �ָ���ʼλ��
        private void cbExact_CheckedChanged( object sender , System.EventArgs e )
        {
            //�ָ�����ʼλ�ÿ�ʼ��
            this.CurrentNode = 0;
            fun.LaserNode = 0;
        }

        private void cbUper_CheckedChanged( object sender , System.EventArgs e )
        {
            //�ָ�����ʼλ�ÿ�ʼ��
            this.CurrentNode = 0;
            fun.LaserNode = 0;
        }

        private void rbText_CheckedChanged( object sender , System.EventArgs e )
        {
            //�ָ�����ʼλ�ÿ�ʼ��
            this.CurrentNode = 0;
            fun.LaserNode = 0;
        }

        private void rbTag_CheckedChanged( object sender , System.EventArgs e )
        {
            //�ָ�����ʼλ�ÿ�ʼ��
            this.CurrentNode = 0;
            fun.LaserNode = 0;
        }
        #endregion

        #region �¼�
        /// <summary>
        /// ������һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLookup_Click( object sender , System.EventArgs e )
        {
            //ָʾ��ѯ ���ƻ���tag Ĭ�ϲ�����
            bool TextOrTag = false;
            //��ѯ���ַ���
            string strSearch = "";
            if( this.rbTag.Checked )
            {
                TextOrTag = true;
            }
            if( comSearchText.DropDownStyle == System.Windows.Forms.ComboBoxStyle.Simple )
            {
                strSearch = comSearchText.Text;
            }
            else
            {
                #region ������ѡ��
                if( this.rbText.Checked )
                {
                    strSearch = comSearchText.Text;
                }
                else
                {
                    if( comSearchText.Tag != null )
                    {
                        strSearch = comSearchText.Tag.ToString( );
                    }
                    else
                    {
                        strSearch = "";
                    }
                }
                #endregion
            }
            this.treeView.SelectedNode = fun.FindTreeNodeByDepth( treeView.Nodes , strSearch , TextOrTag , cbExact.Checked , this.cbUper.Checked );

            if( this.CurrentNode >= treeView.GetNodeCount( true ) )
            {
                if( fun.LaserNode == 0 && treeView.SelectedNode == null )
                {
                    MessageBox.Show( "���Ҳ��� (" + comSearchText.Text + ")" );
                }
                else if( fun.LaserNode != 0 && treeView.SelectedNode == null )
                {
                    MessageBox.Show( "���һص���ʼ��" );
                    this.CurrentNode = 0;
                    fun.LaserNode = 0;
                }

            }
            fun.LaserNode = this.CurrentNode;
            this.CurrentNode = 0;
        }
        /// <summary>
        /// �رմ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click( object sender , System.EventArgs e )
        {
            this.ParentForm.Close( );
        }

        private void comSearchText_KeyDown( object sender , System.Windows.Forms.KeyEventArgs e )
        {
            if( e.KeyData.GetHashCode( ) == Keys.Enter.GetHashCode( ) )
            {
                btnLookup_Click( new object( ) , new System.EventArgs( ) );
            }
        }
        #endregion 

    }
}

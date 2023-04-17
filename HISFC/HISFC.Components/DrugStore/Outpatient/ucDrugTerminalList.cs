using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Function;

namespace Neusoft.UFC.DrugStore.Outpatient
{
    /// <summary>
    /// [�ؼ�����: ucDrugTerminalList]<br></br>
    /// [��������: �����ն��б�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-24]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDrugTerminalList : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucDrugTerminalList( )
        {
            InitializeComponent( );
        }

        #region ����

        /// <summary>
        /// ����ԱȨ�޿���
        /// </summary>
        private Neusoft.NFC.Object.NeuObject privDept = new Neusoft.NFC.Object.NeuObject( );
        /// <summary>
        /// ҵ��������
        /// </summary>
        private Neusoft.HISFC.Management.Pharmacy.DrugStore drugStore = new Neusoft.HISFC.Management.Pharmacy.DrugStore( );
        /// <summary>
        /// �Ƿ���ʾ������ն�
        /// </summary>
        private bool isShowSpecialTerminal = true;
        /// <summary>
        /// �ն�ѡ�����
        /// </summary>
        /// <param name="drugTerminal">��ǰѡ�е��ն�ʵ��</param>
        public delegate void SelectTerminalHandler( Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal );
        /// <summary>
        /// �ն�ѡ���¼�
        /// </summary>
        public event SelectTerminalHandler SelectTerminalEvent;
        /// <summary>
        /// �ն�˫���¼�����
        /// </summary>
        /// <param name="drugTerminal">��ǰѡ�е��ն�ʵ��</param>
        public delegate void SelectTerminalDoubleClickedHandler( Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal );
        /// <summary>
        ///  �ն�˫���¼�
        /// </summary>
        public event SelectTerminalDoubleClickedHandler SelectTerminalDoubleClickedEvent;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ���ʾ������ն�
        /// </summary>
        [Description( "�Ƿ���ʾ�����ն�" ) , Category( "����" ) , DefaultValue( true )]
        public bool IsShowSpecialTerminal
        {
            get
            {
                return this.isShowSpecialTerminal;
            }
            set
            {
                this.isShowSpecialTerminal = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾ��ҩ����
        /// </summary>
        [Description( "�Ƿ���ʾ��ҩ����" ) , Category( "����" ) , DefaultValue( true )]
        public bool IsShowSendDrugWindow
        {
            get
            {
                if( this.neuTabControl1.Contains( this.tabPage1 ) )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if( value )
                {
                    if( !this.neuTabControl1.Contains( this.tabPage1 ) )
                    {
                        this.neuTabControl1.TabPages.Add( this.tabPage1 );
                    }
                }
                else
                {
                    if( this.neuTabControl1.Contains( this.tabPage1 ) )
                    {
                        this.neuTabControl1.TabPages.Remove( this.tabPage1 );
                    }
                }
            }
        }
        /// <summary>
        ///�Ƿ���ʾ��ҩ̨
        /// </summary>
        [Description( "�Ƿ���ʾ��ҩ̨" ) , Category( "����" ) , DefaultValue( true )]
        public bool IsShowPrepareTerminal
        {
            get
            {
                if( this.neuTabControl1.Contains( this.tabPage2 ) )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if( value )
                {
                    if( !this.neuTabControl1.Contains( this.tabPage2 ) )
                    {
                        this.neuTabControl1.TabPages.Add( this.tabPage2 );
                    }
                }
                else
                {
                    if( this.neuTabControl1.Contains( this.tabPage2 ) )
                    {
                        this.neuTabControl1.TabPages.Remove( this.tabPage2 );
                    }
                }
            }
        }
        /// <summary>
        /// �Ƿ���ʾѡ���
        /// </summary>
        [Description( "�Ƿ���ʾѡ���" ) , Category( "����" ) , DefaultValue( false )]
        public bool IsShowCheckBox
        {
            get
            {
                if( this.neuTabControl1.Contains( this.tabPage1 ) )
                {
                    return this.neuListView1.CheckBoxes;
                }
                else
                {
                    return this.neuListView2.CheckBoxes;
                }
            }
            set
            {
                if( this.neuTabControl1.Contains( this.tabPage1 ) )
                {
                    this.neuListView1.CheckBoxes = value;
                }
                if( this.neuTabControl1.Contains( this.tabPage2 ) )
                {
                    this.neuListView2.CheckBoxes = value;
                }
            }
        }
        /// <summary>
        /// ��ǰѡ���ն��б�
        /// </summary>
        [Description( "��ǰѡ�е��ն��б�,������ʾCheckBoxʱ��Ч" ) , Category( "����" )]
        public List<Neusoft.HISFC.Object.Pharmacy.DrugTerminal> SelectedTerminal
        {
            get
            {
                List<Neusoft.HISFC.Object.Pharmacy.DrugTerminal> selectednodes = new  List<Neusoft.HISFC.Object.Pharmacy.DrugTerminal>( );
                ListView lvi;
                if(this.neuTabControl1.SelectedTab ==this.tabPage1)
                {
                    lvi = this.neuListView1;
                }
                else
                {
                    lvi = this.neuListView2;
                }
                foreach( ListViewItem item in lvi.Items)
                {
                    if( item.Checked)
                    {

                        selectednodes.Add( item.Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal );
                    }
                    else
                    {
                        selectednodes = null;
                    }
                }
                return selectednodes;
            }
        }

        #endregion

        #region  ����

            /// <summary>
            /// ��ʼ����ҩ�����б������Ϣ
            /// </summary>
            private void InitSendDrugWindowHeader( )
            {
                this.neuListView1.SuspendLayout( );
                this.neuListView1.Columns.Clear( );
                this.neuListView1.Items.Clear( );
                this.neuListView1.Columns.Add( "����" , 140 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "�ն�����" , 70 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "�Ƿ�ر�" , 70 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "������" , 55 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "����ն�" , 100 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "����ˢ�¼��" , 100 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "����Ļˢ�¼��" , 100 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "��ʾ����" , 80 , HorizontalAlignment.Left );
                this.neuListView1.Columns.Add( "��ע" , 200 , HorizontalAlignment.Left );
                this.neuListView1.ResumeLayout( );
            }

            /// <summary>
            /// ��ҩ̨�б������Ϣ
            /// </summary>
            private void InitPrepareTerminalHeader( )
            {
                this.neuListView2.SuspendLayout( );
                this.neuListView2.Columns.Clear( );
                this.neuListView2.Items.Clear( );
                this.neuListView2.Columns.Add( "����" , 140 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "�ն�����" , 70 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "�Ƿ�ر�" , 70 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "������" , 55 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "��ҩ����" , 100 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "����ն�" , 100 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "����ˢ�¼��" , 100 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "�Ƿ��Զ���ӡ" , 100 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "��ʾ����" , 80 , HorizontalAlignment.Left );
                this.neuListView2.Columns.Add( "��ע" , 200 , HorizontalAlignment.Left );
                this.neuListView2.ResumeLayout( );
            }

            /// <summary>
            /// ��ʼ�������ն�����
            /// </summary>
            public void InitDeptTerminal( string privDeptCode )
            {
                this.privDept.ID = privDeptCode;
                //��ʼ����ҩ̨
                this.InitData( Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ̨ );
                //��ʼ����ҩ����
                this.InitData( Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ���� );
            }

            /// <summary>
            /// ���ݿ��ҡ��ն����ͳ�ʼ��
            /// </summary>
            /// <param name="enumType"></param>
            protected virtual void InitData( Neusoft.HISFC.Object.Pharmacy.EnumTerminalType enumType )
            {
                //���ݿⷿ���롢�ն����ͼ�������
                ArrayList al = drugStore.QueryDrugTerminalByDeptCode( this.privDept.ID , ( NConvert.ToInt32( enumType ) ).ToString( ) );
                if( al == null )
                {
                    MessageBox.Show( this.drugStore.Err );
                    return;
                }
                Neusoft.HISFC.Object.Pharmacy.DrugTerminal info;
                for( int i = 0 ; i < al.Count ; i++ )
                {
                    info = al[ i ] as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                    if( info == null )
                    {
                        continue;
                    }
                    if( !this.IsShowSpecialTerminal && info.TerminalProperty == Neusoft.HISFC.Object.Pharmacy.EnumTerminalProperty.��ͨ )
                    {
                        continue;
                    }
                    //��ӽڵ�
                    this.SetItem( info );
                }
            }

            /// <summary>
            /// ����ListView
            /// </summary>
            /// <param name="info">��ǰ��DrugTerminalʵ��</param>
            private void SetItem( Neusoft.HISFC.Object.Pharmacy.DrugTerminal info )
            {
                ListViewItem item = new ListViewItem( );
                this.SetItem( info , item );
                //��ҩ����
                if( info.TerminalType == Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ���� )
                {
                    this.neuListView1.Items.Add( item ).Selected = true;
                }
                else //��ҩ̨
                {
                    this.neuListView2.Items.Add( item ).Selected = true;
                }
            }

            /// <summary>
            /// ����ListView
            /// </summary>
            /// <param name="info">��ǰ��DrugTerminalʵ��</param>
            /// <param name="item">��ǰ��ListViewItem</param>
            private void SetItem( Neusoft.HISFC.Object.Pharmacy.DrugTerminal info , ListViewItem item )
            {
                //���
                item.SubItems.Clear( );
                //�ն�����
                item.Name = info.ID;
                item.Text = info.Name;
                item.Tag = info;
                item.ImageIndex = 0;
                item.StateImageIndex = 1;
                //�ն�����
                item.SubItems.Add( ( ( Neusoft.HISFC.Object.Pharmacy.EnumTerminalProperty )NConvert.ToInt32( info.TerminalProperty ) ).ToString( ) );
                //�Ƿ�ر�
                item.SubItems.Add( info.IsClose ? "��" : "��" );
                //������
                item.SubItems.Add( info.AlertQty.ToString( ) );
                //��ҩ����
                if( info.TerminalType == Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ̨)//��ҩ̨
                {
                    item.SubItems.Add( this.GetTerminalName( info , "0" ) );
                }
                //����ն�
                item.SubItems.Add( this.GetTerminalName( info , "1" ) );
                //����ˢ�м��
                item.SubItems.Add( info.RefreshInterval1.ToString( ) );
                //��ҩ����
                if( info.TerminalType == Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ���� )
                {
                    //����Ļ��ʾ���
                    item.SubItems.Add( info.RefreshInterval2.ToString( ) );
                }
                else //��ҩ̨
                {
                    //�Ƿ��Զ���ӡ
                    item.SubItems.Add( info.IsAutoPrint ? "��" : "��" );
                }
                //��ʾ����
                item.SubItems.Add( info.ShowQty.ToString( ) );
                //��ע
                item.SubItems.Add( info.Memo );
                //�رյ��ն��ú�ɫ��ʾ
                if( info.IsClose )
                {
                    item.ForeColor = System.Drawing.Color.Red;
                }

            }
            /// <summary>
            /// ��ȡ�ն�����
            /// </summary>
            /// <param name="drugTerminal">�ն�ʵ��</param>
            /// <param name="type">����0��ҩ����1����ն�</param>
            /// <returns>����</returns>
            private string GetTerminalName( Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal , string type )
            {
                string tempStr;
                if( type == "0" )
                {
                    tempStr = drugTerminal.SendWindow.ID;
                }
                else
                {
                    tempStr = drugTerminal.ReplaceTerminal.ID;
                }

                if( !string.IsNullOrEmpty( tempStr ) )
                {
                    int index = tempStr.IndexOf( ">" );
                    if( index == -1 )
                    {
                        Neusoft.HISFC.Object.Pharmacy.DrugTerminal temp = new Neusoft.HISFC.Object.Pharmacy.DrugTerminal( );
                        temp = this.drugStore.GetDrugTerminalById( drugTerminal.SendWindow.ID );
                        if( temp != null )
                        {
                            tempStr = "<" + temp.ID + ">" + temp.Name;
                        }
                    }
                    else
                    {
                        //<���> ���� 
                        tempStr = drugTerminal.SendWindow.ID;
                    }
                }
                return tempStr;
            }

            #endregion

        #region �¼�

            /// <summary>
            /// ��ʼ��
            /// </summary>
            /// <param name="e"></param>
            protected override void OnLoad( EventArgs e )
            {
                //��ʼ������
                this.InitPrepareTerminalHeader( );
                this.InitSendDrugWindowHeader( );
                base.OnLoad( e );
            }

            /// <summary>
            ///  ��ҩ����ѡ���¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void neuListView1_SelectedIndexChanged( object sender , EventArgs e )
            {
                if( this.neuListView1.SelectedItems.Count > 0 )
                {
                    if( this.SelectTerminalEvent != null )
                    {
                        this.SelectTerminalEvent( this.neuListView1.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal );
                    }
                }

            }

            /// <summary>
            /// ��ҩ̨ѡ���¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void neuListView2_SelectedIndexChanged( object sender , EventArgs e )
            {
                if( this.neuListView2.SelectedItems.Count > 0 )
                {
                    if( this.SelectTerminalEvent != null )
                    {
                        this.SelectTerminalEvent( this.neuListView2.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal );
                    }
                }
            }
            /// <summary>
            /// ��ҩ�����ն�˫���¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void neuListView1_DoubleClick( object sender , EventArgs e )
            {
                if( this.neuListView1.SelectedItems.Count > 0 )
                {
                    if( this.SelectTerminalDoubleClickedEvent != null )
                    {
                        this.SelectTerminalDoubleClickedEvent( this.neuListView1.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal );
                    }
                }
            }
            /// <summary>
            /// ��ҩ̨�ն�˫���¼�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void neuListView2_DoubleClick( object sender , EventArgs e )
            {
                if( this.neuListView2.SelectedItems.Count > 0 )
                {
                    if( this.SelectTerminalDoubleClickedEvent != null )
                    {
                        this.SelectTerminalDoubleClickedEvent( this.neuListView2.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal );
                    }
                }
            }
            #endregion



        }
    }

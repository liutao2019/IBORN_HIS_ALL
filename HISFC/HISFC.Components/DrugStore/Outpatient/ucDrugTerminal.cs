using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.NFC.Function;
using Neusoft.NFC.Management;

namespace Neusoft.UFC.DrugStore.Outpatient
{
    /// <summary>
    /// [�ؼ�����:ucDrugTerminal]<br></br>
    /// [��������: �����ն�����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-24]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucDrugTerminal : Neusoft.NFC.Interface.Controls.ucBaseControl
    {
        public ucDrugTerminal( )
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

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.toolBarService.GetToolButton( "����" ).Enabled;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled( "����" , value );
                this.toolBarService.SetToolButtonEnabled( "ɾ��" , value );
                this.toolBarService.SetToolButtonEnabled( "����" , value );
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
        private void InitDeptTerminal( )
        {
            this.InitPrepareTerminalHeader( );
            this.InitSendDrugWindowHeader( );
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
                //��ӽڵ�
                this.SetItem( info );
            }
        }

        /// <summary>
        /// �����ն�����
        /// </summary>
        /// <param name="drugTerminalClass">�ն�ʵ��</param>
        private void SetTerminalProperty( Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal )
        {
            try
            {
                if( drugTerminal == null )
                {
                    this.propertyGrid1.SelectedObject = null;
                    return;
                }
                DrugTerminalClass property = new DrugTerminalClass( this.privDept.ID , drugTerminal.TerminalType.GetHashCode( ).ToString( ) );

                property.���� = drugTerminal.Name;
                property.��� = drugTerminal.TerminalType;
                property.���� = drugTerminal.TerminalProperty;

                //��ȡ����ն�����
                property.����ն� = GetTerminalName( drugTerminal , "1" );
                //��ȡ��ҩ��������
                property.��ҩ���� = GetTerminalName( drugTerminal , "0" );

                property.�Ƿ�ر� = drugTerminal.IsClose ? "��" : "��";
                property.�Ƿ��Զ���ӡ = drugTerminal.IsAutoPrint ? "��" : "��";
                property.����ˢ�¼�� = drugTerminal.RefreshInterval1;
                property.��ʾˢ�¼�� = drugTerminal.RefreshInterval2;
                property.������ = drugTerminal.AlertQty;
                property.��ʾ���� = drugTerminal.ShowQty;
                property.��ע = drugTerminal.Memo;

                //���Կؼ���ֵ
                this.propertyGrid1.SelectedObject = property;
                this.propertyGrid1.Focus( );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
                return;
            }
        }

        /// <summary>
        /// �����Կؼ��ڶ�ȡ���ĵ�����ֵ
        /// </summary>
        private Neusoft.HISFC.Object.Pharmacy.DrugTerminal GetTerminalProperty( )
        {
            try
            {
                DrugTerminalClass info = ( ( DrugTerminalClass )this.propertyGrid1.SelectedObject );
                if( info == null )
                    return null;

                Neusoft.HISFC.Object.Pharmacy.DrugTerminal temp = new Neusoft.HISFC.Object.Pharmacy.DrugTerminal( );
                //��ҩ����
                if( this.neuTabControl1.SelectedTab == this.tabPage1 )
                {
                    temp = ( Neusoft.HISFC.Object.Pharmacy.DrugTerminal )this.neuListView1.SelectedItems[ 0 ].Tag;
                }
                else//��ҩ̨
                {
                    temp = ( Neusoft.HISFC.Object.Pharmacy.DrugTerminal )this.neuListView2.SelectedItems[ 0 ].Tag;
                }

                temp.Name = info.����;
                temp.TerminalType = info.���;
                temp.TerminalProperty = info.����;
                //��ȡ����ն˱��
                string tempStr = info.����ն�;
                if( tempStr != "" && tempStr != "�����" )
                {		//��tempStr Ϊ��ʼ����ʱ Ϊ�� 
                    int index = tempStr.IndexOf( ">" );
                    temp.ReplaceTerminal.ID = tempStr.Substring( 1 , index - 1 );
                }
                else
                {
                    temp.ReplaceTerminal.ID = "";
                }

                temp.IsClose = info.�Ƿ�ر� == "��" ? true : false;
                temp.IsAutoPrint = info.�Ƿ��Զ���ӡ == "��" ? true : false;
                temp.RefreshInterval1 = info.����ˢ�¼��;
                temp.RefreshInterval2 = info.��ʾˢ�¼��;
                temp.AlertQty = info.������;
                temp.ShowQty = info.��ʾ����;
                //��ȡ��ҩ�����ն˱��
                string temp1 = info.��ҩ����;
                if( !string.IsNullOrEmpty( temp1 ) )
                {
                    int index1 = temp1.IndexOf( ">" );
                    temp.SendWindow.ID = temp1.Substring( 1 , index1 - 1 );
                }

                temp.Memo = info.��ע;

                return temp;
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
                return null;
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
            if( info.TerminalType == Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ̨ )//��ҩ̨
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
        /// �����ն�
        /// </summary>
        private void AddTerminal( )
        {
            Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = new Neusoft.HISFC.Object.Pharmacy.DrugTerminal( );
            //��ҩ̨
            if( this.neuTabControl1.SelectedTab == this.tabPage1 )
            {
                info.Name = "�½���ҩ����";
                info.TerminalType = Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ����;
                info.User01 = "New";
            }
            else
            {
                info.Name = "�½���ҩ̨";
                info.TerminalType = Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ̨;
                info.User01 = "New";
            }
            //��ӽڵ�
            this.SetItem( info );

        }

        /// <summary>
        /// ɾ���ն�
        /// </summary>
        private void DeleteTerminal( )
        {
            Neusoft.HISFC.Object.Pharmacy.DrugTerminal drugTerminal;
            //���û��ѡ�нڵ㣬�򷵻�
            //��ҩ����
            if( neuTabControl1.SelectedTab == this.tabPage1 )
            {
                if( this.neuListView1.SelectedItems.Count > 0 )
                {
                    drugTerminal = this.neuListView1.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                }
                else
                {
                    return;
                }
            }
            else //��ҩ̨
            {
                if( this.neuListView2.SelectedItems.Count > 0 )
                {
                    drugTerminal = this.neuListView2.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                }
                else
                {
                    return;
                }
            }
            //ȷ��ɾ��
            DialogResult result = MessageBox.Show( Neusoft.NFC.Management.Language.Msg( "ɾ���󽫲��ָܻ����Ƿ������" ) , Neusoft.NFC.Management.Language.Msg( "��ʾ" ) , MessageBoxButtons.YesNo , MessageBoxIcon.Question , MessageBoxDefaultButton.Button2 );
            if( result == DialogResult.No )
            {
                return;
            }
            //�ж��Ƿ��Ѿ����������
            if( !( drugTerminal.User01 == "New" ) )
            {
                int parm;
                //�жϸ��ն��Ƿ�Ϊ�����ն˵�����նˡ���ʾ����Ա
                parm = this.drugStore.IsReplaceFlag( drugTerminal.ID );
                if( parm == -1 )
                {
                    return;
                }
                if( parm == 1 )
                {
                    //��ʾ�û��Ƿ�ȷ��ɾ��
                    DialogResult res = MessageBox.Show( Neusoft.NFC.Management.Language.Msg( "������¼Ϊ�����ն˵�����նˡ�ȷ�Ͻ���ɾ����" ) , Neusoft.NFC.Management.Language.Msg( "��ʾ" ) , MessageBoxButtons.YesNo , MessageBoxIcon.Question , MessageBoxDefaultButton.Button2 , MessageBoxOptions.RightAlign );
                    if( res == DialogResult.No )
                    {
                        return;
                    }
                }

                //�������ݿ�����
                Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction( Neusoft.NFC.Management.Connection.Instance );
                t.BeginTransaction( );

                try
                {
                    this.drugStore.SetTrans( t.Trans );
                    //ɾ������
                    if( this.drugStore.DeleteDrugTerminal( drugTerminal.ID ) == -1 )
                    {
                        t.RollBack( );
                        MessageBox.Show( this.drugStore.Err );
                        return;
                    }
                }
                catch( Exception ex )
                {
                    t.RollBack( );
                    MessageBox.Show( ex.Message );
                    return;
                }

                t.Commit( );
                MessageBox.Show( "ɾ���ɹ�" );
            }

            //���б���ɾ��
            // ��ҩ����
            if( neuTabControl1.SelectedTab == this.tabPage1 )
            {
                if( this.neuListView1.SelectedItems.Count > 0 )
                {
                    this.neuListView1.Items.Remove( this.neuListView1.SelectedItems[ 0 ] );
                }

            }
            else //��ҩ̨
            {
                if( this.neuListView2.SelectedItems.Count > 0 )
                {
                    this.neuListView2.Items.Remove( this.neuListView2.SelectedItems[ 0 ] );
                }
            }

        }

        /// <summary>
        ///  �����ն�
        /// </summary>
        private void SaveTerminal( )
        {
            ListView currListView;
            //���û��ѡ�нڵ㣬�򷵻�
            //��ҩ����
            if( neuTabControl1.SelectedTab == this.tabPage1 )
            {
                if( this.neuListView1.Items.Count > 0 )
                {
                    currListView = this.neuListView1;
                }
                else
                {
                    return;
                }
            }
            else //��ҩ̨
            {
                if( this.neuListView2.Items.Count > 0 )
                {
                    currListView = this.neuListView2;
                }
                else
                {
                    return;
                }
            }
            //�ж��Ƿ���Ϣ��������
            if( this.IsValid( ) == -1 )
            {
                return;
            }
            //�ж��Ƿ����������Ϣ
            if( this.isSameName( ) == -1 )
            {
                return;
            }

            Neusoft.HISFC.Object.Pharmacy.DrugTerminal info;
            //�������ݿ�����
            Neusoft.NFC.Management.Transaction t = new Neusoft.NFC.Management.Transaction( Neusoft.NFC.Management.Connection.Instance );
            t.BeginTransaction( );
            try
            {
                this.drugStore.SetTrans( t.Trans );
                bool isSave = true;
                //��������
                for( int i = 0 ; i < currListView.Items.Count ; i++ )
                {
                    info = currListView.Items[ i ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                    info.Dept.ID = this.privDept.ID;
                    if( this.drugStore.SetDrugTerminal( info ) == -1 )
                    {	//�Ƚ��и��²������������������
                        isSave = false;
                        t.RollBack( );
                        MessageBox.Show( Language.Msg( "�����" ) + i.ToString( ) + Language.Msg( "��ʱ����\n" ) + this.drugStore.Err );
                        break;
                    }
                }

                if( isSave )
                {
                    t.Commit( );
                    MessageBox.Show( Language.Msg( "����ɹ�" ) );
                }
                else
                {
                    return;
                }
            }
            catch( Exception ex )
            {
                t.RollBack( );
                MessageBox.Show( ex.Message );
                return;
            }

            //�����Ƿ������ı��
            for( int i = 0 ; i < currListView.Items.Count ; i++ )
            {
                info = currListView.Items[ i ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                info.User01 = "";
                currListView.Items[ i ].Tag = info;
            }
        }

        /// <summary>
        /// �ж��Ƿ���Ϣ��������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �������󷵻�-2</returns>
        private int IsValid( )
        {

            ListView currListView;
            //��ҩ����
            if( this.neuTabControl1.SelectedTab == this.tabPage1 )
            {
                currListView = this.neuListView1;
            }
            else//��ҩ̨
            {
                currListView = this.neuListView2;
            }

            Neusoft.HISFC.Object.Pharmacy.DrugTerminal info;
            int iCloseNum = 0;
            //�Ƿ�ر�������ͨ��ҩ̨
            bool closeFlag = true;
            for( int i = 0 ; i < currListView.Items.Count ; i++ )
            {
                info = currListView.Items[ i ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;

                #region �ն�ͨ���ж�

                if( info == null )
                {
                    MessageBox.Show( Language.Msg( "��" ) + ( i + 1 ).ToString( ) + Language.Msg( "������ת������" ) );
                    return -2;
                }

                if( info.Name == "" )
                {
                    MessageBox.Show( Language.Msg( "�������" ) + ( i + 1 ).ToString( ) + Language.Msg( "���ն�����" ) );
                    return -1;
                }

                if( info.RefreshInterval1.ToString( ) == "" )
                {
                    MessageBox.Show( Language.Msg( "�����õ�" ) + ( i + 1 ).ToString( ) + Language.Msg( "�г���ˢ��ʱ����" ) );
                    return -1;
                }
                if( info.RefreshInterval2.ToString( ) == "" )
                {
                    MessageBox.Show( Language.Msg( "�����õ�" ) + ( i + 1 ).ToString( ) + Language.Msg( "�д�ӡ/��ʾˢ�¼��" ) );
                    return -1;
                }
                if( info.AlertQty.ToString( ) == "" )
                {
                    MessageBox.Show( Language.Msg( "�����õ�" ) + ( i + 1 ).ToString( ) + Language.Msg( "�о�����" ) );
                    return -1;
                }
                if( info.ShowQty.ToString( ) == "" )
                {
                    MessageBox.Show( Language.Msg( "�����õ�" ) + ( i + 1 ).ToString( ) + Language.Msg( "�д���Ļ��ʾ��������" ) );
                    return -1;
                }
                #endregion

                //������ر�������ͨ��ҩ̨
                if( info.TerminalProperty == Neusoft.HISFC.Object.Pharmacy.EnumTerminalProperty.��ͨ && !info.IsClose )
                {
                    closeFlag = false;
                }
                //��ҩ̨�Ѵ��ڷ�ҩ����ʱ�Ž��д����ж�
                if( info.TerminalType == Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ̨ )
                {
                    if( info.SendWindow.ID == "" )
                    {
                        MessageBox.Show( Language.Msg( "��Ϊ��" ) + ( i + 1 ).ToString( ) + Language.Msg( "����ҩ̨���÷�ҩ����" ) );
                        return -1;
                    }
                    if( info.ReplaceTerminal.ID == info.ID )
                    {
                        MessageBox.Show( Language.Msg( "��" ) + info.Name + Language.Msg( "���������ҩ̨����ʱ�������Լ�����Լ�" ) );
                        return -1;
                    }
                }
                //��ҩ���ڲ�����ر�
                if( info.TerminalType == Neusoft.HISFC.Object.Pharmacy.EnumTerminalType.��ҩ���� )
                {
                    if( info.IsClose )
                    {
                        MessageBox.Show( "������رշ�ҩ���� �ر���Ӧ����ҩ̨�����Դﵽ�رշ�ҩ������ͬ��Ч��" );
                        return -1;
                    }
                }
                else
                {
                    if( info.IsClose )
                    {
                        iCloseNum = iCloseNum + 1;
                    }
                }
            }
            if( iCloseNum == currListView.Items.Count )
            {
                MessageBox.Show( Language.Msg( "������ر�������ҩ̨" ) );
                return -1;
            }
            if( closeFlag )
            {
                MessageBox.Show( Language.Msg( "������ر�������ͨ��ҩ̨" ) );
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �ж��Ƿ����������Ϣ
        /// </summary>
        /// <returns>�������ظ��ɹ�����1 ʧ�ܷ���-1</returns>
        private int isSameName( )
        {
            ListView currListView;
            //��ҩ����
            if( this.neuTabControl1.SelectedTab == this.tabPage1 )
            {
                currListView = this.neuListView1;
            }
            else//��ҩ̨
            {
                currListView = this.neuListView2;
            }

            for( int i = 0 ; i < currListView.Items.Count - 1 ; i++ )
            {
                string name = currListView.Items[ i ].Text;

                for( int j = i + 1 ; j < currListView.Items.Count ; j++ )
                {
                    if( currListView.Items[ j ].Text == name )
                    {
                        MessageBox.Show( Language.Msg( "�� " ) + ( i + 1 ).ToString( ) + Language.Msg( " ���ն�������� " ) + ( j + 1 ).ToString( ) + Language.Msg( " �������ظ���\n����������" ) );
                        return -1;
                    }
                }
            }
            return 1;
        }
        #endregion

        #region �¼�

        /// <summary>
        /// �����Կؼ�ʧȥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void propertyGrid1_Leave( object sender , EventArgs e )
        {
            //��ҩ����
            if( this.neuTabControl1.SelectedTab == this.tabPage1 )
            {
                if( this.neuListView1.SelectedItems.Count > 0 )
                {
                    this.SetItem( this.GetTerminalProperty( ) , this.neuListView1.SelectedItems[ 0 ] );
                }
            }
            else //��ҩ̨
            {
                if( this.neuListView2.SelectedItems.Count > 0 )
                {
                    this.SetItem( this.GetTerminalProperty( ) , this.neuListView2.SelectedItems[ 0 ] );
                }
            }

        }

        /// <summary>
        /// ��ҩ����ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuListView1_SelectedIndexChanged( object sender , EventArgs e )
        {
            if( this.neuListView1.SelectedItems.Count > 0 )
            {
                Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = this.neuListView1.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                this.SetTerminalProperty( info );
            }
        }

        /// <summary>
        /// ��ҩ̨ѡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuListView2_SelectedIndexChanged( object sender , EventArgs e )
        {

            if( this.neuListView2.SelectedItems.Count > 0 )
            {
                Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = this.neuListView2.SelectedItems[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                this.SetTerminalProperty( info );
            }

        }

        /// <summary>
        /// �л���ҩ̨����ҩ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuTabControl1_SelectedIndexChanged( object sender , EventArgs e )
        {
            Neusoft.HISFC.Object.Pharmacy.DrugTerminal info = null;
            if( this.neuTabControl1.SelectedTab == tabPage1 )
            {
                if( this.neuListView1.Items.Count > 0 )
                {
                    this.neuListView1.Items[ 0 ].Selected = true;
                    info = this.neuListView1.Items[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                }
            }
            else
            {
                if( this.neuListView2.Items.Count > 0 )
                {
                    this.neuListView2.Items[ 0 ].Selected = true;
                    info = this.neuListView2.Items[ 0 ].Tag as Neusoft.HISFC.Object.Pharmacy.DrugTerminal;
                }
            }

            this.SetTerminalProperty( info );
        }

        /// <summary>
        /// �����ն�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuAdd_Click( object sender , EventArgs e )
        {
            this.AddTerminal( );
        }

        /// <summary>
        /// ɾ���ն�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuDelete_Click( object sender , EventArgs e )
        {
            this.DeleteTerminal( );
        }

        /// <summary>
        /// �б���ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowList_Click( object sender , EventArgs e )
        {
            if( this.neuTabControl1.SelectedTab == tabPage1 )
            {
                this.neuListView1.View = View.Details;
            }
            else
            {
                this.neuListView2.View = View.Details;
            }
            //����״̬
            this.menuShowLarge.Checked = false;
            this.menuShowSmall.Checked = false;
            this.menuShowList.Checked = true;
        }

        /// <summary>
        /// ��ͼ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowLarge_Click( object sender , EventArgs e )
        {
            if( this.neuTabControl1.SelectedTab == tabPage1 )
            {
                this.neuListView1.View = View.LargeIcon;
            }
            else
            {
                this.neuListView2.View = View.LargeIcon;
            }
            //����״̬
            this.menuShowLarge.Checked = true;
            this.menuShowSmall.Checked = false;
            this.menuShowList.Checked = false;
        }

        /// <summary>
        /// Сͼ����ʾ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuShowSmall_Click( object sender , EventArgs e )
        {
            if( this.neuTabControl1.SelectedTab == tabPage1 )
            {
                this.neuListView1.View = View.SmallIcon;
            }
            else
            {
                this.neuListView2.View = View.SmallIcon;
            }
            //����״̬
            this.menuShowLarge.Checked = false;
            this.menuShowSmall.Checked = true;
            this.menuShowList.Checked = false;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            //�жϲ���Ա�Ƿ�ӵ�в���Ȩ�� 
            if( Neusoft.HISFC.Integrate.Pharmacy.ChoosePiv( "0300" ) )
            {
                this.IsEdit = true;
            }
            else
            {
                this.IsEdit = false;
            }

            //��ȡ��������
            this.privDept = ( ( Neusoft.HISFC.Object.Base.Employee )this.drugStore.Operator ).Dept;
            //��ʼ����������
            this.InitDeptTerminal( );
            //ѡ��tabpage1
            this.neuTabControl1.SelectedIndex = 0;
            base.OnLoad( e );
        }

        /// <summary>
        /// �����ҩ̨����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <returns></returns>
        protected override int OnSave( object sender , object NeuObject )
        {
            this.SaveTerminal( );
            return base.OnSave( sender , NeuObject );
        }

        #endregion

        #region ��������Ϣ

        ///// <summary>
        ///// �����¼�ί��
        ///// </summary>
        //private event System.EventHandler ToolButtonClicked;
        /// <summary>
        /// ���幤��������
        /// </summary>
        protected Neusoft.NFC.Interface.Forms.ToolBarService toolBarService = new Neusoft.NFC.Interface.Forms.ToolBarService( );

        /// <summary>
        /// ��ʼ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="NeuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override Neusoft.NFC.Interface.Forms.ToolBarService OnInit( object sender , object NeuObject , object param )
        {
            //this.ToolButtonClicked += new EventHandler( ToolButton_clicked );
            //���ӹ�����
            this.toolBarService.AddToolButton( "����" , "�����ն�" , 0 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ���ն�" , 1 , true , false , null );
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
                    this.AddTerminal( );
                    break;
                case "ɾ��":
                    this.DeleteTerminal( );
                    break;
            }

        }

        #endregion



    }
}

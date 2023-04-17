using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.OutBase
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
    public partial class ucDrugTerminal : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDrugTerminal( )
        {
            InitializeComponent( );

            this.propertyGrid1.PropertyValueChanged += new PropertyValueChangedEventHandler(propertyGrid1_PropertyValueChanged);
        }    

        #region ����

        /// <summary>
        /// ����ԱȨ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject( );

        /// <summary>
        /// ҵ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore( );

        /// <summary>
        /// �򿪽���ʱ�Ƿ�������ʾ��ҩ����Tabpage
        /// </summary>
        private bool isShowSendDrugWindowFirst = false;

        /// <summary>
        /// ������Ƿ�ˢ�½���
        /// </summary>
        private bool isSaveRefresh = false;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        public bool IsEdit
        {
            set
            {
                this.toolBarService.SetToolButtonEnabled( "����" , value );
                this.toolBarService.SetToolButtonEnabled( "ɾ��" , value );
                this.toolBarService.SetToolButtonEnabled( "����" , value );
            }
        }

        /// <summary>
        /// �򿪽���ʱ�Ƿ�������ʾ��ҩ����Tabpage
        /// </summary>
        [Description( "�򿪽���ʱ�Ƿ�������ʾ��ҩ����Tabҳ" ),Category("����"),DefaultValue(false)]
        public bool IsShowSendDrugWindowFirst
        {
            get
            {
                return isShowSendDrugWindowFirst;
            }
            set
            {
                isShowSendDrugWindowFirst = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�����ϲ�������
        /// </summary>
        [Description("�Ƿ���ʾ�����ϲ�������"), Category("����"), DefaultValue(true)]
        public bool IsShowPropertyToolBar
        {
            get
            {
                return this.propertyGrid1.ToolbarVisible;
            }
            set
            {
                this.propertyGrid1.ToolbarVisible = value;
            }
        }

        /// <summary>
        /// �Ƿ񱣴��ˢ�½���
        /// </summary>
        [Description("�Ƿ񱣴��ˢ�½���"), Category("����"), DefaultValue(false),Browsable(false)]
        public bool IsSaveRefresh
        {
            get
            {
                return isSaveRefresh;
            }
            set
            {
                isSaveRefresh = value;
            }
        }
        #endregion

        #region  ����

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.IsSaveRefresh = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Terminal_Save_Refresh, true, false);
        }

        /// <summary>
        /// ��ʼ����ҩ�����б������Ϣ
        /// </summary>
        private void InitSendDrugWindowHeader( )
        {
            this.lvSendTerminal.SuspendLayout( );
            this.lvSendTerminal.Columns.Clear( );
            this.lvSendTerminal.Items.Clear( );
            this.lvSendTerminal.Columns.Add( "����" , 140 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "�ն�����" , 70 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "�Ƿ�ر�" , 70 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "������" , 55 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "����ն�" , 100 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "����ˢ�¼��" , 100 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "����Ļˢ�¼��" , 100 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "��ʾ����" , 80 , HorizontalAlignment.Left );
            this.lvSendTerminal.Columns.Add( "��ע" , 200 , HorizontalAlignment.Left );
            this.lvSendTerminal.ResumeLayout( );
        }

        /// <summary>
        /// ��ҩ̨�б������Ϣ
        /// </summary>
        private void InitPrepareTerminalHeader( )
        {
            this.lvDrugTerminal.SuspendLayout( );
            this.lvDrugTerminal.Columns.Clear( );
            this.lvDrugTerminal.Items.Clear( );
            this.lvDrugTerminal.Columns.Add( "����" , 140 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "�ն�����" , 70 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "�Ƿ�ر�" , 70 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "������" , 55 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "��ҩ����" , 100 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "����ն�" , 100 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "����ˢ�¼��" , 100 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "�Ƿ��Զ���ӡ" , 100 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "��ʾ����" , 80 , HorizontalAlignment.Left );
            this.lvDrugTerminal.Columns.Add( "��ע" , 200 , HorizontalAlignment.Left );
            this.lvDrugTerminal.ResumeLayout( );
        }

        /// <summary>
        /// ��ʼ�������ն�����
        /// </summary>
        private void InitDeptTerminal( )
        {
            this.InitPrepareTerminalHeader( );
            this.InitSendDrugWindowHeader( );
            //��ʼ����ҩ̨
            this.InitData( FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ̨ );
            //��ʼ����ҩ����
            this.InitData(FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ����);
        }

        /// <summary>
        /// ���ݿ��ҡ��ն����ͳ�ʼ��
        /// </summary>
        /// <param name="enumType"></param>
        protected virtual void InitData( FS.HISFC.Models.Pharmacy.EnumTerminalType enumType )
        {
            //���ݿⷿ���롢�ն����ͼ�������
            ArrayList al = drugStore.QueryDrugTerminalByDeptCode( this.privDept.ID , ( NConvert.ToInt32( enumType ) ).ToString( ) );
            if( al == null )
            {
                MessageBox.Show( this.drugStore.Err );
                return;
            }
            FS.HISFC.Models.Pharmacy.DrugTerminal info;
            for( int i = 0 ; i < al.Count ; i++ )
            {
                info = al[ i ] as FS.HISFC.Models.Pharmacy.DrugTerminal;
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
        private void SetTerminalProperty( FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal )
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
                property.��ӡ���� = drugTerminal.TerimalPrintType;

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
        private FS.HISFC.Models.Pharmacy.DrugTerminal GetTerminalProperty( )
        {
            try
            {
                DrugTerminalClass info = ( ( DrugTerminalClass )this.propertyGrid1.SelectedObject );
                if( info == null )
                    return null;

                FS.HISFC.Models.Pharmacy.DrugTerminal temp = new FS.HISFC.Models.Pharmacy.DrugTerminal( );
                //��ҩ����
                if( this.neuTabControl1.SelectedTab == this.tpSend )
                {
                    temp = ( FS.HISFC.Models.Pharmacy.DrugTerminal )this.lvSendTerminal.SelectedItems[ 0 ].Tag;
                }
                else//��ҩ̨
                {
                    temp = ( FS.HISFC.Models.Pharmacy.DrugTerminal )this.lvDrugTerminal.SelectedItems[ 0 ].Tag;
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
                temp.TerimalPrintType = info.��ӡ����;

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
        private string GetTerminalName( FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal , string type )
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
                    FS.HISFC.Models.Pharmacy.DrugTerminal temp = new FS.HISFC.Models.Pharmacy.DrugTerminal( );
                    temp = this.drugStore.GetDrugTerminalById( tempStr );
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
        private void SetItem( FS.HISFC.Models.Pharmacy.DrugTerminal info )
        {
            ListViewItem item = new ListViewItem( );
            this.SetItem( info , item );
            //��ҩ����
            if( info.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ���� )
            {
                this.lvSendTerminal.Items.Add( item ).Selected = true;
            }
            else //��ҩ̨
            {
                this.lvDrugTerminal.Items.Add( item ).Selected = true;
            }
        }

        /// <summary>
        /// ����ListView
        /// </summary>
        /// <param name="info">��ǰ��DrugTerminalʵ��</param>
        /// <param name="item">��ǰ��ListViewItem</param>
        private void SetItem( FS.HISFC.Models.Pharmacy.DrugTerminal info , ListViewItem item )
        {
            if (info == null)
            {
                return;
            }

            //���
            item.SubItems.Clear( );
            //�ն�����
            item.Name = info.ID;
            item.Text = info.Name;
            item.Tag = info;
            item.ImageIndex = 0;
            item.StateImageIndex = 1;
            //�ն�����
            item.SubItems.Add( ( ( FS.HISFC.Models.Pharmacy.EnumTerminalProperty )NConvert.ToInt32( info.TerminalProperty ) ).ToString( ) );
            //�Ƿ�ر�
            item.SubItems.Add( info.IsClose ? "��" : "��" );
            //������
            item.SubItems.Add( info.AlertQty.ToString( ) );
            //��ҩ����
            if( info.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ̨ )//��ҩ̨
            {
                item.SubItems.Add( this.GetTerminalName( info , "0" ) );
            }
            //����ն�
            item.SubItems.Add( this.GetTerminalName( info , "1" ) );
            //����ˢ�м��
            item.SubItems.Add( info.RefreshInterval1.ToString( ) );
            //��ҩ����
            if( info.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ���� )
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
            FS.HISFC.Models.Pharmacy.DrugTerminal info = new FS.HISFC.Models.Pharmacy.DrugTerminal( );
            //��ҩ̨
            if( this.neuTabControl1.SelectedTab == this.tpSend )
            {
                info.Name = "�½���ҩ����";
                info.TerminalType = FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ����;
                info.User01 = "New";
            }
            else
            {
                info.Name = "�½���ҩ̨";
                info.TerminalType = FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ̨;
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
            FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal;
            //���û��ѡ�нڵ㣬�򷵻�
            //��ҩ����
            if( neuTabControl1.SelectedTab == this.tpSend )
            {
                if (this.lvSendTerminal.Items.Count == 1)
                {
                    MessageBox.Show(Language.Msg("ϵͳ��Ӧ���ٱ���һ����ҩ���� ����ȫ��ɾ��"));
                    return;
                }
                if( this.lvSendTerminal.SelectedItems.Count > 0 )
                {
                    drugTerminal = this.lvSendTerminal.SelectedItems[ 0 ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                    //�жϸ÷�ҩ�����Ƿ������ö�Ӧ������ҩ̨
                    if (!this.IsSendWindowValid(drugTerminal.ID))
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }
            }
            else //��ҩ̨
            {
                if (this.lvDrugTerminal.Items.Count == 1)
                {
                    MessageBox.Show(Language.Msg("ϵͳ��Ӧ���ٱ���һ����ҩ̨ ����ȫ��ɾ��"));
                    return;
                }

                if( this.lvDrugTerminal.SelectedItems.Count > 0 )
                {
                    drugTerminal = this.lvDrugTerminal.SelectedItems[ 0 ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                    //����̨����ʱ ����Ƿ�����̨ȫ���ر� ���ȫ���ر�Ӧ���ٿ���һ��̨
                    if (!drugTerminal.IsClose)
                    {
                        bool isAllClose = true ;
                        for (int i = 0; i < this.lvDrugTerminal.Items.Count; i++)
                        {
                            FS.HISFC.Models.Pharmacy.DrugTerminal tempTerminal = this.lvDrugTerminal.Items[i].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                            if (tempTerminal.ID != drugTerminal.ID && !tempTerminal.IsClose)
                            {
                                isAllClose = false;
                                break;
                            }
                        }
                        if (isAllClose)
                        {
                            MessageBox.Show(Language.Msg("ɾ����̨�� ����̨ȫ�����ڹر�״̬ Ӧ���ٿ���һ̨���ٹرձ�̨"));
                            return;
                        }
                    }
                }
                else
                {
                    return;
                }
            }

            if (this.drugStore.IsHaveRecipe(drugTerminal.ID))
            {
                MessageBox.Show(Language.Msg("���ն˻�����δȡҩ���� ���ܽ���ɾ������"));
                return;
            }

            //ȷ��ɾ��
            DialogResult result = MessageBox.Show( FS.FrameWork.Management.Language.Msg( "ɾ���󽫲��ָܻ����Ƿ������" ) , FS.FrameWork.Management.Language.Msg( "��ʾ" ) , MessageBoxButtons.YesNo , MessageBoxIcon.Question , MessageBoxDefaultButton.Button2 );
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
                    DialogResult res = MessageBox.Show( FS.FrameWork.Management.Language.Msg( "������¼Ϊ�����ն˵�����նˡ�ȷ�Ͻ���ɾ����" ) , FS.FrameWork.Management.Language.Msg( "��ʾ" ) , MessageBoxButtons.YesNo , MessageBoxIcon.Question , MessageBoxDefaultButton.Button2 , MessageBoxOptions.RightAlign );
                    if( res == DialogResult.No )
                    {
                        return;
                    }
                }

                //�������ݿ�����
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );
                //t.BeginTransaction( );

                try
                {
                    this.drugStore.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    //ɾ������
                    if( this.drugStore.DeleteDrugTerminal( drugTerminal.ID ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show( this.drugStore.Err );
                        return;
                    }
                }
                catch( Exception ex )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( ex.Message );
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show( "ɾ���ɹ�" );
            }

            //���б���ɾ��
            // ��ҩ����
            if( neuTabControl1.SelectedTab == this.tpSend )
            {
                if( this.lvSendTerminal.SelectedItems.Count > 0 )
                {
                    this.lvSendTerminal.Items.Remove( this.lvSendTerminal.SelectedItems[ 0 ] );
                }

            }
            else //��ҩ̨
            {
                if( this.lvDrugTerminal.SelectedItems.Count > 0 )
                {
                    this.lvDrugTerminal.Items.Remove( this.lvDrugTerminal.SelectedItems[ 0 ] );
                }
            }

        }

        /// <summary>
        ///  �����ն�
        /// </summary>
        private int SaveTerminal( )
        {
            //���»�ȡ��ǰѡ�е���Ϣ
            this.propertyGrid1_Leave(null, System.EventArgs.Empty);

            ListView currListView;
            //���û��ѡ�нڵ㣬�򷵻�
            //��ҩ����
            if( neuTabControl1.SelectedTab == this.tpSend )
            {
                if( this.lvSendTerminal.Items.Count > 0 )
                {
                    currListView = this.lvSendTerminal;
                }
                else
                {
                    return -1;
                }
            }
            else //��ҩ̨
            {
                if( this.lvDrugTerminal.Items.Count > 0 )
                {
                    currListView = this.lvDrugTerminal;
                }
                else
                {
                    return -1;
                }
            }
            //�ж��Ƿ���Ϣ��������
            if( this.IsValid( ) == -1 )
            {
                return -1;
            }
            //�ж��Ƿ����������Ϣ
            if( this.isSameName( ) == -1 )
            {
                return -1;
            }

            FS.HISFC.Models.Pharmacy.DrugTerminal info;
            //�������ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction( FS.FrameWork.Management.Connection.Instance );
            //t.BeginTransaction( );
            try
            {
                this.drugStore.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                bool isSave = true;
                //��������
                for( int i = 0 ; i < currListView.Items.Count ; i++ )
                {
                    info = currListView.Items[ i ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                    info.Dept.ID = this.privDept.ID;
                    if( this.drugStore.SetDrugTerminal( info ) == -1 )
                    {	//�Ƚ��и��²������������������
                        isSave = false;
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show( Language.Msg( "�����" ) + i.ToString( ) + Language.Msg( "��ʱ����\n" ) + this.drugStore.Err );
                        break;
                    }
                }

                if( isSave )
                {
                    FS.FrameWork.Management.PublicTrans.Commit();
                    MessageBox.Show( Language.Msg( "����ɹ�" ) );
                }
                else
                {
                    return -1;
                }
            }
            catch( Exception ex )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( ex.Message );
                return -1;
            }

            //�����Ƿ������ı��
            for( int i = 0 ; i < currListView.Items.Count ; i++ )
            {
                info = currListView.Items[ i ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                info.User01 = "";
                currListView.Items[ i ].Tag = info;
            }

            return 1;
        }

        /// <summary>
        /// �ж��Ƿ���Ϣ��������
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1 �������󷵻�-2</returns>
        private int IsValid( )
        {

            ListView currListView;
            //��ҩ����
            if( this.neuTabControl1.SelectedTab == this.tpSend )
            {
                currListView = this.lvSendTerminal;
            }
            else//��ҩ̨
            {
                currListView = this.lvDrugTerminal;
            }

            FS.HISFC.Models.Pharmacy.DrugTerminal info;
            int iCloseNum = 0;
            //�Ƿ�ر�������ͨ��ҩ̨
            bool closeFlag = true;
            for( int i = 0 ; i < currListView.Items.Count ; i++ )
            {
                info = currListView.Items[ i ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;

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

                if( info.RefreshInterval1.ToString( ) == "" || info.RefreshInterval1 == 0)
                {
                    MessageBox.Show( Language.Msg( "������" + info.Name + " ����ˢ��ʱ����" ) );
                    return -1;
                }
                if( info.RefreshInterval2.ToString( ) == "" || info.RefreshInterval2 == 0)
                {
                    MessageBox.Show( Language.Msg( "������ " + info.Name + " ��ӡ/��ʾˢ�¼��" ) );
                    return -1;
                }
                if( info.AlertQty.ToString( ) == "" || info.AlertQty == 0)
                {
                    MessageBox.Show( Language.Msg( "������ " + info.Name + " ������" ) );
                    return -1;
                }
                if( info.ShowQty.ToString( ) == "" || info.ShowQty == 0)
                {
                    MessageBox.Show( Language.Msg( "������ " + info.Name + " ����Ļ��ʾ��������" ) );
                    return -1;
                }
                #endregion

                if (!this.IsValid(info))
                {
                    return -1;
                }

                //������ر�������ͨ��ҩ̨
                if( info.TerminalProperty == FS.HISFC.Models.Pharmacy.EnumTerminalProperty.��ͨ && !info.IsClose )
                {
                    closeFlag = false;
                }
                //��ҩ̨�Ѵ��ڷ�ҩ����ʱ�Ž��д����ж�
                if( info.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ̨ )
                {
                    if( info.SendWindow.ID == "" )
                    {
                        MessageBox.Show( Language.Msg( "��Ϊ [" + info.Name + " ]��ҩ̨���÷�ҩ����" ) );
                        return -1;
                    }
                    if( info.ReplaceTerminal.ID == info.ID && info.ID != "")
                    {
                        MessageBox.Show( Language.Msg( "��" ) + info.Name + Language.Msg( "���������ҩ̨����ʱ�������Լ�����Լ�" ) );
                        return -1;
                    }
                }
                //��ҩ���ڲ�����ر�
                if( info.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ���� )
                {
                    if( info.IsClose )
                    {
                        MessageBox.Show( "������رշ�ҩ���� �ر���Ӧ����ҩ̨�����Դﵽ�رշ�ҩ������ͬ��Ч��" );
                        info.IsClose = false;
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
        /// �ж��ն˲�����Ϣ�Ƿ���Ч
        /// </summary>
        /// <param name="drugTerminal">�����ն�ʵ��</param>
        /// <returns>�ɹ�����True ��Ч����False</returns>
        private bool IsValid(FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            if (drugTerminal == null)
            {
                return false;
            }

            if (drugTerminal.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ����)
            {
                if (drugTerminal.IsClose)
                {
                    MessageBox.Show(Language.Msg("���ܶԷ�ҩ���ڽ��йر�"));
                    return false;
                }
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugTerminal.Name, 32))
            {
                MessageBox.Show(Language.Msg("�ն����ƹ��� ���ʵ�����"));
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugTerminal.Memo, 50))
            {
                MessageBox.Show(Language.Msg("�ն˱�ע���� ���ʵ�����"));
                return false;
            }
            if (drugTerminal.RefreshInterval1 > 999)
            {
                MessageBox.Show(Language.Msg("ˢ�¼��ӦС��999"));
                return false;
            }
            if (drugTerminal.RefreshInterval2 > 999)
            {
                MessageBox.Show(Language.Msg("ˢ�¼��С��999"));
                return false;
            }

            return true;
        }

        /// <summary>
        /// �ж��ն���Ϣ�Ƿ���Ч�������޸�
        /// </summary>
        /// <param name="drugTerminal">�����ն�ʵ��</param>
        /// <returns>�ɹ�����True ��Ч����False</returns>
        private bool IsValidAndModify(ref FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal)
        {
            if (drugTerminal == null)
            {
                return false;
            }

            if (drugTerminal.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ����)
            {
                if (drugTerminal.IsClose)
                {
                    MessageBox.Show(Language.Msg("���ܶԷ�ҩ���ڽ��йر�"));
                    drugTerminal.IsClose = false;
                    return false;
                }
            }

            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugTerminal.Name, 32))
            {
                MessageBox.Show(Language.Msg("�ն����ƹ��� ���ʵ�����"));
                drugTerminal.Name = drugTerminal.Name.Substring(0, 32);
                return false;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(drugTerminal.Memo, 50))
            {
                MessageBox.Show(Language.Msg("�ն˱�ע���� ���ʵ�����"));
                drugTerminal.Memo = drugTerminal.Memo.Substring(0, 32);
                return false;
            }
            if (drugTerminal.RefreshInterval1 > 999 || drugTerminal.RefreshInterval1 <= 0)
            {
                MessageBox.Show(Language.Msg("ˢ�¼��Ӧ����0��999֮��"));
                drugTerminal.RefreshInterval1 = 10;
                return false;
            }
            if (drugTerminal.RefreshInterval2 > 999 || drugTerminal.RefreshInterval2 <= 0)
            {
                MessageBox.Show(Language.Msg("ˢ�¼��Ӧ����0��999֮��"));
                drugTerminal.RefreshInterval2 = 55;
                return false;
            }
            if (drugTerminal.AlertQty > 99 || drugTerminal.AlertQty <= 0)
            {
                MessageBox.Show(Language.Msg("������Ӧ����0��99֮��"));
                drugTerminal.AlertQty = 15;
                return false;
            }
            if (drugTerminal.ShowQty > 99 || drugTerminal.ShowQty <= 0)
            {
                MessageBox.Show(Language.Msg("��ʾ����Ӧ����0��99֮��"));
                drugTerminal.ShowQty = 20;
                return false;
            }

            return true;
        }

        /// <summary>
        /// ��ҩ̨��Ӧ��ҩ�����Ƿ���Ч
        /// </summary>
        /// <returns></returns>
        private bool IsSendWindowValid(string sendTerminalID)
        {
            for (int i = 0; i < this.lvDrugTerminal.Items.Count; i++)
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal info = this.lvDrugTerminal.Items[i].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;

                //��ҩ̨�Ѵ��ڷ�ҩ����ʱ�Ž��д����ж�
                if (info.SendWindow.ID == sendTerminalID)
                {
                    MessageBox.Show(Language.Msg("�÷�ҩ���������ö�Ӧ [" + info.Name + " ]��ҩ̨ ��Ը���ҩ̨�������÷�ҩ���� Ȼ��ɾ����ҩ����"));
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �ж��Ƿ����������Ϣ
        /// </summary>
        /// <returns>�������ظ��ɹ�����1 ʧ�ܷ���-1</returns>
        private int isSameName( )
        {
            ListView currListView;
            //��ҩ����
            if( this.neuTabControl1.SelectedTab == this.tpSend )
            {
                currListView = this.lvSendTerminal;
            }
            else//��ҩ̨
            {
                currListView = this.lvDrugTerminal;
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
         

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {            
            //��ҩ����
            if (this.neuTabControl1.SelectedTab == this.tpSend)
            {
                if (this.lvSendTerminal.SelectedItems.Count > 0)
                {
                    FS.HISFC.Models.Pharmacy.DrugTerminal sendTerminal = this.GetTerminalProperty();

                    if (!this.IsValidAndModify(ref sendTerminal))
                    {
                        this.SetTerminalProperty(sendTerminal);
                    }
                    this.SetItem(sendTerminal, this.lvSendTerminal.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show(Language.Msg("��ѡ�����޸��ն�"));
                }
            }
            else //��ҩ̨
            {
                if (this.lvDrugTerminal.SelectedItems.Count > 0)
                {
                    FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal = this.GetTerminalProperty();

                    if (!this.IsValidAndModify(ref drugTerminal))
                    {
                        this.SetTerminalProperty(drugTerminal);
                    }
                    this.SetItem(drugTerminal, this.lvDrugTerminal.SelectedItems[0]);
                }
                else
                {
                    MessageBox.Show(Language.Msg("��ѡ�����޸��ն�"));
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
            if( this.lvSendTerminal.SelectedItems.Count > 0 )
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal info = this.lvSendTerminal.SelectedItems[ 0 ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
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

            if( this.lvDrugTerminal.SelectedItems.Count > 0 )
            {
                FS.HISFC.Models.Pharmacy.DrugTerminal info = this.lvDrugTerminal.SelectedItems[ 0 ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
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
            FS.HISFC.Models.Pharmacy.DrugTerminal info = null;
            if( this.neuTabControl1.SelectedTab == tpSend )
            {
                if( this.lvSendTerminal.Items.Count > 0 )
                {
                    this.lvSendTerminal.Items[ 0 ].Selected = true;
                    info = this.lvSendTerminal.Items[ 0 ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
                }
            }
            else
            {
                if( this.lvDrugTerminal.Items.Count > 0 )
                {
                    this.lvDrugTerminal.Items[ 0 ].Selected = true;
                    info = this.lvDrugTerminal.Items[ 0 ].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;
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
            if( this.neuTabControl1.SelectedTab == tpSend )
            {
                this.lvSendTerminal.View = View.Details;
            }
            else
            {
                this.lvDrugTerminal.View = View.Details;
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
            if( this.neuTabControl1.SelectedTab == tpSend )
            {
                this.lvSendTerminal.View = View.LargeIcon;
            }
            else
            {
                this.lvDrugTerminal.View = View.LargeIcon;
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
            if( this.neuTabControl1.SelectedTab == tpSend )
            {
                this.lvSendTerminal.View = View.SmallIcon;
            }
            else
            {
                this.lvDrugTerminal.View = View.SmallIcon;
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
            if( FS.HISFC.BizProcess.Integrate.Pharmacy.ChoosePiv( "0300" ) )
            {
                this.IsEdit = true;
            }
            else
            {
                this.IsEdit = false;
            }

            //��ȡ��������
            this.privDept = ( ( FS.HISFC.Models.Base.Employee )this.drugStore.Operator ).Dept;
            //��ʼ����������
            this.InitDeptTerminal( );
            //�������������ж�������ʾ�Ǹ�Tabҳ
            if( this.isShowSendDrugWindowFirst )
            {
                this.neuTabControl1.SelectedIndex = 0;
            }
            else
            {
                this.neuTabControl1.SelectedIndex = 1;
            }

            this.InitControlParam();

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
            if (this.SaveTerminal() == 1)
            {
                if (this.isSaveRefresh)
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(Language.Msg("����ˢ�½�����ʾ"));
                    Application.DoEvents();

                    this.InitDeptTerminal();

                    this.propertyGrid1.SelectedObject = null;

                    //�������������ж�������ʾ�Ǹ�Tabҳ
                    FS.HISFC.Models.Pharmacy.DrugTerminal info;
                    if (this.neuTabControl1.SelectedTab == this.tpDrug)
                    {
                        if (this.lvDrugTerminal.Items.Count > 0)
                        {
                            this.lvDrugTerminal.Items[0].Selected = true;

                            info = this.lvDrugTerminal.Items[0].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;

                            this.SetTerminalProperty(info);
                        }
                    }
                    else
                    {
                        if (this.lvSendTerminal.Items.Count > 0)
                        {
                            this.lvSendTerminal.Items[0].Selected = true;

                            info = this.lvSendTerminal.Items[0].Tag as FS.HISFC.Models.Pharmacy.DrugTerminal;

                            this.SetTerminalProperty(info);
                        }
                    }

                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }
            }

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

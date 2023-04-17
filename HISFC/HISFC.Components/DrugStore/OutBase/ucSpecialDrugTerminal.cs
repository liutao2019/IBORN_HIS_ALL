using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.OutBase
{
    /// <summary>
    /// [�ؼ�����:ucSpecialDrugTerminal]<br></br>
    /// [��������: ���������ն�ά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11-29]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='������' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��='Bug'
    ///		�޸�����=''
    ///  />
    /// <�޸ļ�¼>
    ///    1.����farpoint�Ľ������ҳ���ǩ���Ʊ��Ϊ�������� {8FE2CA47-D536-4dde-B892-44276F89593B} 
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucSpecialDrugTerminal : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucSpecialDrugTerminal( )
        {
            InitializeComponent( );


            this.neuSpread1.ActiveSheetChanged += new EventHandler(neuSpread1_ActiveSheetChanged);

            this.ucDrugTerminalList1.SelectTerminalDoubleClickedEvent += new ucDrugTerminalList.SelectTerminalDoubleClickedHandler( ucDrugTerminalList1_SelectTerminalDoubleClickedEvent );
            this.ucDrugTerminalList1.SelectTerminalEvent += new ucDrugTerminalList.SelectTerminalHandler( ucDrugTerminalList1_SelectTerminalEvent );
        }     

        #region ����

        /// <summary>
        /// �洢ҩƷ�б�
        /// </summary>
        ArrayList drugItemList;

        /// <summary>
        /// �洢�����б�
        /// </summary>
        ArrayList deptList;

        /// <summary>
        /// �洢�շ�����б�
        /// </summary>
        ArrayList feeItemList;

        /// <summary>
        /// �Һż���
        /// </summary>
        ArrayList regLevelList = new ArrayList();

        /// <summary>
        /// ��Ա����
        /// </summary>
        ArrayList patientTypeList = new ArrayList();

        /// <summary>
        /// ��Ա������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper( );

        /// <summary>
        /// ����ԱȨ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = new FS.FrameWork.Models.NeuObject( );

        /// <summary>
        /// ҵ��������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.DrugStore drugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore( );

        /// <summary>
        /// ������
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager( );

        /// <summary>
        /// ����������
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// �Ƿ���Ȩ�ޱ༭
        /// </summary>
        private bool isPrivilegeEdit = false;
        
        /// <summary>
        /// ��ǰѡ����ն�ʵ��
        /// </summary>
        FS.HISFC.Models.Pharmacy.DrugTerminal currDrugTerminal = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ�����༭
        /// </summary>
        public bool IsEdit
        {
            get
            {
                return this.isPrivilegeEdit;
            }
            set
            {
                this.isPrivilegeEdit = value;
                this.toolBarService.SetToolButtonEnabled( "����" , value );
                this.toolBarService.SetToolButtonEnabled( "ɾ��" , value );
                this.toolBarService.SetToolButtonEnabled( "����" , value );
            }
        }

        /// <summary>
        /// �Ƿ���ʾ�Һż���Sheet
        /// </summary>
        [Description("�Ƿ���ʾ�ҺŻ�����������"),Category("����"),DefaultValue(true)]
        public bool IsShowRegLevel
        {
            get
            {
                return this.neuSpread1.Sheets.Contains(this.sheetSpeRegLevel);
            }
            set
            {
                if (value)
                {
                    if (!this.neuSpread1.Sheets.Contains(this.sheetSpeRegLevel))
                    {
                        this.neuSpread1.Sheets.Add(this.sheetSpeRegLevel);
                    }
                }
                else
                {
                    if (this.neuSpread1.Sheets.Contains(this.sheetSpeRegLevel))
                    {
                        this.neuSpread1.Sheets.Remove(this.sheetSpeRegLevel);
                    }
                }
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ����neuSpread1��ʽ������
        /// </summary>
        private void InitFp( )
        {
            for( int i = 0 ; i < this.neuSpread1.Sheets.Count ; i++ )
            {
                this.neuSpread1.Sheets[ i ].Reset( );
                this.neuSpread1.Sheets[ i ].ColumnCount = 10;
                this.neuSpread1.Sheets[ i ].RowCount = 0;
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 0 ).Text = "��ҩ̨����";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 1 ).Text = "������Ŀ";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 2 ).Text = "��ע";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 3 ).Text = "����Ա";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 4 ).Text = "����ʱ��";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 5 ).Text = "��ҩ̨����";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 6 ).Text = "������Ŀ����";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 7 ).Text = "��չ��־1";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 8 ).Text = "��չ��־2";
                this.neuSpread1.Sheets[ i ].ColumnHeader.Cells.Get( 0 , 9 ).Text = "��չ��־3";
                this.neuSpread1.Sheets[ i ].Columns.Get( 0 ).Label = "��ҩ̨����";
                this.neuSpread1.Sheets[ i ].Columns.Get( 0 ).Width = 125F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 0 ).Locked = true;

                this.neuSpread1.Sheets[ i ].Columns.Get( 1 ).Label = "������Ŀ";
                this.neuSpread1.Sheets[ i ].Columns.Get( 1 ).Width = 120F;

                //�շѴ��ڵ���Ŀ���û��Լ�¼��ģ���Ӧ����д��ʵ�ʷ�ҩ���ڱ��������ļ���HISDefaultValue.xml����
                if( this.neuSpread1.Sheets[i] == this.sheetSpeFeeWindow )
                {
                    this.neuSpread1.Sheets[ i ].Columns.Get( 1 ).Locked = false;
                    //����Ƿ�ҩ���ڣ�������Ŀ���Ƶı���
                    this.neuSpread1.Sheets[ i ].Columns.Get( 1 ).BackColor = Color.LightSkyBlue;
                }
                else
                {
                    this.neuSpread1.Sheets[ i ].Columns.Get( 1 ).Locked = true;
                    this.neuSpread1.Sheets[ i ].Columns.Get( 1 ).BackColor = Color.LightYellow;
                }

                this.neuSpread1.Sheets[ i ].Columns.Get( 2 ).Label = "��ע";
                this.neuSpread1.Sheets[ i ].Columns.Get( 2 ).Width = 150F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 2 ).Locked = false;

                this.neuSpread1.Sheets[ i ].Columns.Get( 3 ).Label = "����Ա";
                this.neuSpread1.Sheets[ i ].Columns.Get( 3 ).Width = 85F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 3 ).Locked = true;

                this.neuSpread1.Sheets[ i ].Columns.Get( 4 ).Label = "����ʱ��";
                this.neuSpread1.Sheets[ i ].Columns.Get( 4 ).Width = 120F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 4 ).Locked = true;

                this.neuSpread1.Sheets[ i ].Columns.Get( 5 ).Label = "��ҩ̨����";
                this.neuSpread1.Sheets[ i ].Columns.Get( 5 ).Visible = false;
                this.neuSpread1.Sheets[ i ].Columns.Get( 5 ).Width = 77F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 6 ).Label = "������Ŀ����";
                this.neuSpread1.Sheets[ i ].Columns.Get( 6 ).Visible = false;
                this.neuSpread1.Sheets[ i ].Columns.Get( 6 ).Width = 87F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 7 ).Label = "��չ��־1";
                this.neuSpread1.Sheets[ i ].Columns.Get( 7 ).Visible = false;
                this.neuSpread1.Sheets[ i ].Columns.Get( 7 ).Width = 72F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 8 ).Label = "��չ��־2";
                this.neuSpread1.Sheets[ i ].Columns.Get( 8 ).Visible = false;
                this.neuSpread1.Sheets[ i ].Columns.Get( 8 ).Width = 71F;
                this.neuSpread1.Sheets[ i ].Columns.Get( 9 ).Label = "��չ��־3";
                this.neuSpread1.Sheets[ i ].Columns.Get( 9 ).Visible = false;
                this.neuSpread1.Sheets[ i ].Columns.Get( 9 ).Width = 67F;

                this.neuSpread1.Sheets[ i ].GrayAreaBackColor = System.Drawing.Color.White;
                this.neuSpread1.Sheets[ i ].OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                this.neuSpread1.Sheets[ i ].RowHeader.Columns.Default.Resizable = false;

                switch( i )
                {
                    case 0:
                        this.neuSpread1.Sheets[ i ].SheetName = "ҩƷ���";
                        break;
                    case 1:
                        this.neuSpread1.Sheets[ i ].SheetName = "ר�����";
                        break;
                    case 2:
                        this.neuSpread1.Sheets[ i ].SheetName = "�������";
                        break;
                    case 3:
                        this.neuSpread1.Sheets[ i ].SheetName = "�շѴ���";
                        break;
                    case 4:
                        this.neuSpread1.Sheets[ i ].SheetName = "�Һż���";
                        break;
                    case 5:
                        this.neuSpread1.Sheets[i].SheetName = "��������";
                        break;
                }

            }
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void InitConstant( )
        {
            //��ȡҩƷ����Ϣ�б�
            FS.HISFC.BizLogic.Pharmacy.Item item = new FS.HISFC.BizLogic.Pharmacy.Item( );

            List<FS.HISFC.Models.Pharmacy.Item> alDrug = item.QueryItemAvailableList( );
            if( alDrug == null )
            {
                MessageBox.Show( item.Err );
                return;
            }
            else //ת����ArrayList
            {
                this.drugItemList = new ArrayList( alDrug.ToArray( ) );
            }

            //��ȡ������Ϣ�б�
            this.deptList = manager.GetDepartment( FS.HISFC.Models.Base.EnumDepartmentType.C );
            if( deptList == null )
            {
                MessageBox.Show( manager.Err );
                return;
            }

            //��ȡ�������
            //this.feeItemList = manager.GetConstantList( FS.HISFC.Models.Base.EnumConstant.PACTUNIT );
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            this.feeItemList = feeIntegrate.QueryPactUnitAll();
            if( feeItemList == null )
            {
                MessageBox.Show(feeIntegrate.Err);
                return;
            }

            //��ȡ��Ա�б�
            ArrayList al = manager.QueryEmployeeAll( );
            if( al == null )
            {
                MessageBox.Show( manager.Err );
                return;
            }
            this.personHelper.ArrayObject = al;

            //��ȡ�Һż���(��ʱ����)
            //FS.HISFC.BizLogic.Registration.RegLevel regLevelManager = new FS.HISFC.BizLogic.Registration.RegLevel( );
            //this.regLevelList = regLevelManager.Query( );
            //if( this.regLevelList == null )
            //{
            //    MessageBox.Show( "��ȡ�Һż������" + regLevelManager.Err );
            //    return -1;
            //}
            this.patientTypeList = consMgr.GetAllList("PersonType");

            return;
        }

        /// <summary>
        /// ����������ʾ
        /// </summary>
        /// <param name="index">��ǰ��SheetIndex</param>
        private void ShowData( int index)
        {
            
            try
            {
                ArrayList al = this.drugStore.QueryDrugSPETerminalByDeptCode( this.privDept.ID , ( index + 1 ).ToString( ) );
                if( al == null )
                {
                    MessageBox.Show( Language.Msg( "��ȡ������ҩ̨��Ϣ����!" ) + this.drugStore.Err );
                    return;
                }

                this.neuSpread1.Sheets[ index ].Rows.Count = al.Count;
                FS.HISFC.Models.Pharmacy.DrugSPETerminal info;

                for( int i = 0 ; i < al.Count ; i++ )
                {
                    info = al[ i ] as FS.HISFC.Models.Pharmacy.DrugSPETerminal;
                    if( info == null )
                    {
                        continue;
                    }
                    //��ҩ̨����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 0 ].Text = info.Terminal.Name;
                    //������Ŀ����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 1 ].Text = info.Item.Name;
                    //��ע
                    this.neuSpread1.Sheets[ index ].Cells[ i , 2 ].Text = info.Memo;
                    //����Ա
                    if( this.personHelper != null )
                    {
                        this.neuSpread1.Sheets[ index ].Cells[ i , 3 ].Text = this.personHelper.GetName( info.Oper.ID );
                    }
                    //����ʱ��
                    this.neuSpread1.Sheets[ index ].Cells[ i , 4 ].Text = info.Oper.OperTime.ToString( );
                    //��ҩ̨����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 5 ].Text = info.Terminal.ID;
                    //������Ŀ����
                    this.neuSpread1.Sheets[ index ].Cells[ i , 6 ].Text = info.Item.ID;
                    // 0 ���ݿ���� 1 ���������
                    this.neuSpread1.Sheets[ index ].Cells[ i , 7 ].Text = "0";
                    //ʵ��
                    this.neuSpread1.Sheets[ index ].Rows[ i ].Tag = info;
                }
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
                return;
            }
        }

        /// <summary>
        /// �������ڹ�����Աѡ��������Ŀ
        /// </summary>
        private void ShowList( )
        {
            //���������򷵻�
            if( this.neuSpread1.ActiveSheet.Rows.Count == 0 )
            {
                return;
            }
            //��ȡ��ǰ����С���
            int i = this.neuSpread1.ActiveSheet.ActiveRowIndex;
            //����Ա�Դ���ѡ�񷵻ص���Ϣ
            FS.FrameWork.Models.NeuObject info = new FS.FrameWork.Models.NeuObject( );
            ArrayList al;
            switch( this.neuSpread1.ActiveSheetIndex )
            {
                case 0:
                    al = this.drugItemList;
                    break;
                case 1:
                    al = this.deptList;
                    break;
                case 2:
                    al = this.feeItemList;
                    break;
                //case 3:
                //    break;
                case 4:
                    al = this.regLevelList;
                    return;
                case 5:
                    al = this.patientTypeList;
                    break;
                default:
                    al = new ArrayList( );
                    return;
            }
            if( FS.FrameWork.WinForms.Classes.Function.ChooseItem( al , ref info ) == 0 )
            {
                return;
            }
            else
            {

                //������Ŀ����
                this.neuSpread1.ActiveSheet.Cells[ i , 1 ].Value = info.Name;
                //������Ŀ����
                this.neuSpread1.ActiveSheet.Cells[ i , 6 ].Value = info.ID;
            }
        }

        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        private void AddTerminal( )
        {
            //�ж��Ƿ���Ȩ�޲���
            if( !this.isPrivilegeEdit )
            {
                return;
            }

            if( this.currDrugTerminal == null )
            {
                return;
            }
            //���������ն�ʵ��
            FS.HISFC.Models.Pharmacy.DrugSPETerminal info = new FS.HISFC.Models.Pharmacy.DrugSPETerminal( );
            info.Terminal = this.currDrugTerminal;

            try
            {
                int rowCount = this.neuSpread1.ActiveSheet.Rows.Count;
                //���һ��
                this.neuSpread1.ActiveSheet.Rows.Add(rowCount, 1);
                //�ն�����
                this.neuSpread1.ActiveSheet.Cells[rowCount, 0].Value = this.currDrugTerminal.Name;
                //�ն�ID
                this.neuSpread1.ActiveSheet.Cells[rowCount, 5].Value = this.currDrugTerminal.ID;

                //����Ա
                this.neuSpread1.ActiveSheet.Cells[rowCount, 3].Value = this.drugStore.Operator.Name;
                //����ʱ��
                this.neuSpread1.ActiveSheet.Cells[rowCount, 4].Value = this.drugStore.GetDateTimeFromSysDateTime();

                //ʵ��
                this.neuSpread1.ActiveSheet.Rows[rowCount].Tag = info;
                //������־
                this.neuSpread1.ActiveSheet.Cells[rowCount, 7].Text = "1";
            }
            catch
            {
                MessageBox.Show(this.neuSpread1.ActiveSheet.Rows.Count.ToString());
            }

        }

        /// <summary>
        /// ɾ����¼
        /// </summary>
        private void DeleteTerminal( )
        {           
            if (this.neuSpread1.ActiveSheet.Rows.Count <= 0)
            {
                return;
            }

            DialogResult result = MessageBox.Show(Language.Msg( "ȷʵҪɾ����������") , "" , MessageBoxButtons.YesNo , MessageBoxIcon.Question , MessageBoxDefaultButton.Button2 );
            if( result == DialogResult.No )
            {
                return;
            }

            //�������ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //Transaction t = new Transaction( Connection.Instance );
            //t.BeginTransaction( );
            this.drugStore.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            try
            {
                int i = this.neuSpread1.ActiveSheet.ActiveRowIndex;
                if( i < 0 )
                {
                    return;
                }
                //�Ѿ���������ݡ������ݿ�ɾ��
                if( this.neuSpread1.ActiveSheet.Cells[ i , 7 ].Text == "0" )		
                {
                    FS.HISFC.Models.Pharmacy.DrugSPETerminal info = this.neuSpread1.ActiveSheet.Rows[ i ].Tag as FS.HISFC.Models.Pharmacy.DrugSPETerminal;
                    if( info == null )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show( Language.Msg( "ɾ�������ն���Ϣʱ��������ת������!" ) );
                        return;
                    }
                    //ɾ������
                    if( this.drugStore.DeleteDrugSPETerminal( info ) == -1 )
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show( this.drugStore.Err );
                        return;
                    }
                }
                //��sheet���Ƴ�
                this.neuSpread1.ActiveSheet.Rows.Remove( i , 1 );
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show( Language.Msg( "ɾ���ɹ�" ) );
            }
            catch( Exception ex )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( ex.Message );
                return;
            }
        }

        /// <summary>
        /// �������������
        /// </summary>
        /// <returns>�ɹ�����true ʧ�ܷ���false</returns>
        private bool IsValid( )
        {
            bool isSuccess = true;

            //�����Ŀ�Ƿ�¼������
            for( int i = 0 ; i < this.neuSpread1.ActiveSheet.Rows.Count ; i++ )
            {
                if (this.neuSpread1.ActiveSheet == this.sheetSpeFeeWindow)
                {
                    if (this.neuSpread1.ActiveSheet.Cells[i, 1].Text == "")
                    {
                        MessageBox.Show(Language.Msg("����������Ŀ���������շѴ��ں�"));
                        isSuccess = false;
                        break;
                    }
                }
                else
                {
                    if (this.neuSpread1.ActiveSheet.Cells[i, 1].Text == "")
                    {
                        MessageBox.Show(Language.Msg("��˫����س�ѡ��������Ŀ"));
                        isSuccess = false;
                        break;
                    }
                }

                if (!FS.FrameWork.Public.String.ValidMaxLengh(this.neuSpread1.ActiveSheet.Cells[i, 2].Text, 50))
                {
                    MessageBox.Show(Language.Msg("��ע�ֶ�¼�볬�� ���ʵ�����"));
                    return false;
                }

                if (this.neuSpread1.ActiveSheet == this.sheetSpeFeeWindow)
                {
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(this.neuSpread1.ActiveSheet.Cells[i, 1].Text, 12))
                    {
                        MessageBox.Show(Language.Msg("�շѴ���������Ŀ¼�볬�� ���ʵ�����"));
                        return false;
                    }
                }
                else
                {
                    if (!FS.FrameWork.Public.String.ValidMaxLengh(this.neuSpread1.ActiveSheet.Cells[i, 1].Text, 64))
                    {
                        MessageBox.Show(Language.Msg("������Ŀ¼�볬�� ���ʵ�����"));
                        return false;
                    }
                }
            
            }
            return isSuccess;
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void SaveTerminal( )
        {
            if( this.neuSpread1.ActiveSheet.Rows.Count <= 0 )
            {
                return;
            }
            if( !this.IsValid( ) )
            {
                return;
            }

            //��Ŀ���(1ҩƷ2ר��3�������4�ض��շѴ���5�Һż���6��������)
            int index = this.neuSpread1.ActiveSheetIndex + 1;
            //�������ݿ�����
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //Transaction t = new Transaction( Connection.Instance );
            //t.BeginTransaction( );
            this.drugStore.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ɾ��ԭ����

            try
            {
                if( this.drugStore.DeleteDrugSPETerminal( this.privDept.ID , index.ToString() ) == -1 )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show( Language.Msg("ִ�б������ ɾ��ԭ���ݹ����г���!") + this.drugStore.Err );
                    return;
                }
            }
            catch( Exception ex )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( Language.Msg("ִ�б������ ɾ��ԭ���ݹ����г���!") + ex.Message );
                return;
            }
            #endregion

            #region ����������
            try
            {
                FS.HISFC.Models.Pharmacy.DrugSPETerminal info;
                //��������
                for( int i = 0 ; i < this.neuSpread1.ActiveSheet.Rows.Count ; i++ )
                {
                    info = this.neuSpread1.ActiveSheet.Rows[ i ].Tag as FS.HISFC.Models.Pharmacy.DrugSPETerminal;
                    if( info == null )
                    {
                        continue;
                    }
                    //��Ŀ���(1ҩƷ2ר��3�������4�ض��շѴ���5�Һż���6��������)
                    info.ItemType = index.ToString();
                    //��Ŀ����
                    info.Item.Name = this.neuSpread1.ActiveSheet.Cells[ i , 1 ].Text;
                    //��Ŀ����
                    info.Item.ID = this.neuSpread1.ActiveSheet.Cells[ i , 6 ].Text;		
                    
                    //ר�����
                    if( this.neuSpread1.ActiveSheetIndex == 1 )
                    {
                        info.Item.ID = info.Item.ID.PadLeft( 4 , '0' );
                    }

                    //�շѴ���
                    if( this.neuSpread1.ActiveSheetIndex == 3 )
                    {
                        info.Item.ID = info.Item.Name;
                    }
                    //��ע
                    info.Memo = this.neuSpread1.ActiveSheet.Cells[ i , 2 ].Text;			

                    if( this.drugStore.InsertDrugSPETerminal( info ) == -1 )
                    {
                        if( this.drugStore.DBErrCode == 1 )
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show( Language.Msg("��") + ( i + 1 ).ToString( ) + Language.Msg("���������������ظ�") );
                            return;
                        }
                        else
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show( Language.Msg("�����") + ( i + 1 ).ToString( ) + Language.Msg("��ʱ����\n") + this.drugStore.Err );
                            return;
                        }
                    }
                }

            }
            catch( Exception ex )
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show( Language.Msg("ִ�б����������!") + ex.Message );
                return;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show( Language.Msg( "����ɹ�" ) );
            #endregion
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ���ڳ�ʼ��
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad( EventArgs e )
        {
            #region Ȩ���ж�

            //ȡ����ԱȨ�޿��ң���ʱ�����ڿ��Ҵ��� ��
            this.privDept = ( ( FS.HISFC.Models.Base.Employee )this.drugStore.Operator ).Dept;

            //�ж��Ƿ���ģ��ά��Ȩ��
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager user = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager( );
            List<FS.FrameWork.Models.NeuObject> alPrivDetail = user.QueryUserPrivCollection( this.drugStore.Operator.ID , "0350" , this.privDept.ID );
            if( alPrivDetail != null )
            {
                foreach( FS.FrameWork.Models.NeuObject privInfo in alPrivDetail )
                {
                    //�����ն�ά��Ȩ��
                    if( privInfo.ID == "01" )
                    {
                        this.isPrivilegeEdit = true;
                        break;
                    }
                }
            }
            else
            {
                this.isPrivilegeEdit = true;
            }

            this.IsEdit = this.isPrivilegeEdit;

            #endregion

            #region ��ʼ��

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm( Language.Msg( "���ڼ��������ն���Ϣ...." ) );
            Application.DoEvents( );

            //��ʼ��sheet��ʽ
            this.InitFp( );

            //��ʼ�������ն�����
            this.ucDrugTerminalList1.InitDeptTerminal( this.privDept.ID );

            ////���̼߳�������
            //System.Threading.ThreadStart start = new System.Threading.ThreadStart( this.InitConstant );
            //System.Threading.Thread thread = new System.Threading.Thread( start );
            //thread.Start( );

            this.InitConstant();

            //Ĭ��ѡ��ҩƷ
            this.neuSpread1.ActiveSheetIndex = 0;
            this.ShowData( this.neuSpread1.ActiveSheetIndex );

            //�ı�FarPoint�Ļس��¼���ʹ�س�ת����һ�е�ǰ��
            FarPoint.Win.Spread.InputMap im;
            im = this.neuSpread1.GetInputMap( FarPoint.Win.Spread.InputMapMode.WhenFocused );
            im.Put( new FarPoint.Win.Spread.Keystroke( Keys.Enter , Keys.None ) , FarPoint.Win.Spread.SpreadActions.MoveToNextRow );

            //���ιҺż�����
            //if (this.neuSpread1.Sheets.Contains(this.sheetSpeRegLevel))
            //{
            //    this.neuSpread1.Sheets.Remove(this.sheetSpeRegLevel);
            //}

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm( );

            #endregion

            base.OnLoad( e );
        }

        private void neuSpread1_ActiveSheetChanged(object sender, EventArgs e)
        {
            this.ShowData(this.neuSpread1.ActiveSheetIndex);
        }

        /// <summary>
        /// ˫������ն��¼�
        /// </summary>
        /// <param name="drugTerminal">ѡ�е��ն�ʵ��</param>
        void ucDrugTerminalList1_SelectTerminalDoubleClickedEvent( FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal )
        {
            if (drugTerminal != null)
            {
                this.currDrugTerminal = drugTerminal;
                this.AddTerminal();
            }
        }

        /// <summary>
        /// �ն˵����¼�
        /// </summary>
        /// <param name="drugTerminal">ѡ�е��ն�ʵ��</param>
        void ucDrugTerminalList1_SelectTerminalEvent( FS.HISFC.Models.Pharmacy.DrugTerminal drugTerminal )
        {
            this.currDrugTerminal = drugTerminal;
        }

        /// <summary>
        /// ˫��sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick( object sender , FarPoint.Win.Spread.CellClickEventArgs e )
        {
            //˫��������Ŀ������ʱ
            if( e.Column == 1 )
            {
                this.ShowList( );
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave( object sender , object neuObject )
        {
            this.SaveTerminal( );
            return base.OnSave( sender , neuObject );
        }

        /// <summary>
        /// �ո�ʱ����ѡ�񴰿�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey( Keys keyData )
        {
            if( this.neuSpread1.ContainsFocus && ( keyData == Keys.Space ) )
            {
                if( this.neuSpread1.ActiveSheet.ActiveColumnIndex == 1 )
                {
                    this.ShowList( );
                }
            }
            return base.ProcessDialogKey( keyData );
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
            this.toolBarService.AddToolButton( "����" , "���Ӽ�¼" , 0 , true , false , null );
            this.toolBarService.AddToolButton( "ɾ��" , "ɾ����¼" , 1 , true , false , null );

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
